using System;
using System.Web.Mvc;
using GitSharp;
using Gwit.Core.Configuration;
using Gwit.Web.Models;
using Gwit.Core.GitSharp;
using Gwit.Core.Services;
using Gwit.Core.SyntaxHighlighting;
using Gwit.Web.Mvc;

namespace Gwit.Web.Controllers
{

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
            return Tree(repositoryName, "master", null);
        }

        public ActionResult Blob(string repositoryName, string id, string path)
        {
            try
            {
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
                    Repository, Request.RequestContext, blob)
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
                    Request.RequestContext,
                    tree
                );
                return View("Tree", viewModel);

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
                var viewModel = new CommitViewModel(Repository, Request.RequestContext, commit);
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
                var viewModel = new CommitsViewModel(Repository, repositoryName, id);
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
