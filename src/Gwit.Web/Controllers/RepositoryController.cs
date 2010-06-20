using System;
using System.Web.Mvc;
using GitSharp;
using Gwit.Core.Configuration;
using Gwit.Web.Models;
using Gwit.Core.GitSharp;
using Gwit.Core.Services;
using Gwit.Core.SyntaxHighlighting;
using System.Web;
using Microsoft.Practices.Unity;

namespace Gwit.Web.Controllers
{
    // TODO [RW] : refactor so that there's no repository name argument all around the place - replace with object and
    // action / controller level filter


    public class RepositoryRequest : ActionFilterAttribute
    {
        [Dependency]
        public ISettingsProvider ConfigurationProvider { get; set; }

        [Dependency]
        public IRepositoryResolver RepositoryResolver { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controller = filterContext.Controller as RepositoryController;
            if (controller == null)
            {
                throw new InvalidOperationException("{0} canonly be applied to {1}.".Fill(GetType().Name, typeof(RepositoryController).Name));
            }
            controller.RepositoryName = filterContext.RouteData.GetRequiredString("repositoryName");
            filterContext.RequestContext.RouteData.DataTokens.Add("RepositoryName", controller.RepositoryName);

            var repoLocation = (string)filterContext.RequestContext.HttpContext.Request.QueryString["location"];
            filterContext.RequestContext.RouteData.DataTokens.Add("RepositoryLocation", repoLocation);
            if (String.IsNullOrEmpty(repoLocation))
            {
                controller.RepositoryLocation = controller.RepositoryName;
            }
            else
            {
                controller.RepositoryLocation = HttpUtility.UrlDecode(repoLocation);
            }

            controller.Repository = RepositoryResolver.GetRepository(controller.RepositoryLocation);
            
            controller.Treeish = (string)filterContext.RouteData.Values["id"];
            controller.Treeish = controller.Treeish ?? "master";
            filterContext.RequestContext.RouteData.DataTokens.Add("Treeish", controller.Treeish);

            controller.Path = (string)filterContext.RouteData.Values["path"];
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                var repoLocation = (string)filterContext.RequestContext.HttpContext.Request.QueryString["location"];
                result.ViewData["RepositoryLocation"] = repoLocation;
                result.ViewData["RepositoryName"] = filterContext.RouteData.GetRequiredString("repositoryName");
            }
        }

        public string HtmlResultString
        {
            get { return "This screen is only accessible by selecting a the actual employee. You cannot view it through a group id."; }
        }
    }

    [RepositoryRequest]
    public class RepositoryController : Controller
    {
        private ISettingsProvider _configurationProvider;
        private Settings _settings;
        private IRepositoryResolver _repoResolver;
        private IHighlightingService _hightlightingService;

        public string RepositoryLocation { get; set; }
        public string RepositoryName { get; set; }
        public string Path { get; set; }
        public string Treeish { get; set; }
        public Repository Repository { get; set; }

        public RepositoryController(
            ISettingsProvider settingsProvider,
            IRepositoryResolver repoResolver,
            IHighlightingService hightlightingService)
        {
            _hightlightingService = hightlightingService;
            _repoResolver = repoResolver;
            _configurationProvider = settingsProvider;
            _settings = _configurationProvider.Load();
        }


        public ActionResult Details(string repositoryName)
        {
            var head = Repository.Head;
            
            var commit = head.Target as Commit;
            var tree = commit.Tree;
            var viewModel = new RepositoryDetailsViewModel(Repository,
                repositoryName,
                head.CurrentCommit,
                new TreeViewModel(
                    Repository,
                    repositoryName,
                    tree,
                    new PathViewModel(Request.RequestContext, tree)
                )
            );
            return View(viewModel);
        }

        public ActionResult Blob(string repositoryName, string id, string path)
        {
            try
            {
                //var commit = Repository.Get<Commit>(id);
                //var node = commit.Tree.Node(path);

                //if (node == null)
                //{
                //    throw new InvalidOperationException("Invalid path");
                //}

                AbstractTreeNode node = null;

                var obj = Repository.Get<AbstractObject>(id);
                if (obj is Commit)
                {
                    node = (obj as Commit).Tree.Node(path);
                }
                else if (obj is Tree)
                {
                    node = (obj as Tree).Node(path);
                }

                if (node == null)
                {
                    throw new InvalidOperationException("Invalid path");
                }

                var blob = node as Leaf;
                if (blob == null)
                {
                    throw new InvalidOperationException("Path is not pointing to a tree.");
                }

                var viewModel = new BlobViewModel(
                    Repository, repositoryName, node.Hash, blob,
                    new PathViewModel(Request.RequestContext, blob))
                    {
                        FormattedData = _hightlightingService.GenerateHtml(blob.Data, blob.Path, null)
                    };
                return View(viewModel);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(repositoryName, ex);
            }
        }

        public ActionResult Tree(string repositoryName, string id, string path)
        {
            try
            {
                AbstractTreeNode node = null;

                var obj = Repository.Get<AbstractObject>(id);                
                if (obj is Commit)
                {
                    node = (obj as Commit).Tree;
                }
                else if (obj is Tree)
                {
                    node = obj as Tree;
                }

                if (node == null)
                {
                    throw new InvalidOperationException("Invalid path");
                }

                var tree = (node as Tree).Node(path) as Tree;
                if (tree == null)
                {
                    throw new InvalidOperationException("Path is not pointing to a tree.");
                }

                var viewModel = new TreeViewModel(
                    Repository,
                    repositoryName, tree,
                    new PathViewModel(Request.RequestContext, tree)
                );
                return View(viewModel);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(repositoryName, ex);
            }
        }

        public ActionResult Commit(string repositoryName, string id)
        {
            try
            {
                var commit = Repository.Get<Commit>(id);
                var tree = commit.Tree;
                var viewModel = new RepositoryDetailsViewModel(Repository, repositoryName, commit, new TreeViewModel(
                        Repository,
                        repositoryName,
                        tree,
                        new PathViewModel(Request.RequestContext, commit.Tree)
                    ));
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(repositoryName, ex);
            }

        }

        public ActionResult Commits(string repositoryName, string id)
        {
            try
            {

                Ref reference = null;
                if (String.IsNullOrEmpty(id))
                {
                    reference = Repository.Branches["master"];
                }
                var viewModel = new CommitsViewModel(Repository, repositoryName);
                var commit = (reference.Target as Commit);
                CommitHarvester harvester = new CommitHarvester(commit, DateTime.UtcNow.AddDays(-30), 20);
                viewModel.AddCommits(harvester.Collect());
                viewModel.ApplyGrouping();

                return View(viewModel);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(repositoryName, ex);
            }

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (Repository != null)
                {
                    Repository.Dispose();
                }
            }
        }
    }
}
