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

        [HttpGet]
        public ActionResult Details(RepositoryNavigationRequest request)
        {
            return Tree(request);
        }

        [HttpGet]
        public ActionResult Blob(RepositoryNavigationRequest request)
        {
            try
            {
                AbstractTreeNode node = null;

                var obj = Repository.Get<AbstractObject>(request.Treeish);
                if (obj is Commit)
                {
                    node = (obj as Commit).Tree.Node(request.Path);
                }
                else if (obj is Tree)
                {
                    node = (obj as Tree).Node(request.Path);
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
                    Repository, request, blob)
                    {
                        FormattedData = _hightlightingService.GenerateHtml(blob.Data, blob.Path, null)
                    };
                return View(viewModel);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(request.RepositoryName, ex);
            }
        }

        [HttpGet]
        public ActionResult Tree(RepositoryNavigationRequest request)
        {
            try
            {
                AbstractTreeNode node = null;

                var obj = Repository.Get<AbstractObject>(request.Treeish);
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

                var tree = (node as Tree).Node(request.Path) as Tree;
                if (tree == null)
                {
                    throw new InvalidOperationException("Path is not pointing to a tree.");
                }

                var viewModel = new TreeViewModel(
                    Repository,
                    request,
                    tree
                );
                return View("Tree", viewModel);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(request.RepositoryName, ex);
            }
        }

        [HttpGet]
        public ActionResult Commit(RepositoryNavigationRequest request)
        {
            try
            {
                var commit = Repository.Get<Commit>(request.Treeish);
                var viewModel = new CommitViewModel(Repository, request, commit)
                {

                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(request.RepositoryName, ex);
            }
        }

        [HttpGet]
        public ActionResult Commits(RepositoryNavigationRequest request)
        {
            try
            {

                Ref reference = null;
                if (String.IsNullOrEmpty(request.Treeish))
                {
                    reference = Repository.Branches["master"];
                }
                var viewModel = new CommitsViewModel(Repository, request.RepositoryName, request.Treeish);
                var commit = (reference.Target as Commit);
                CommitHarvester harvester = new CommitHarvester(commit, DateTime.UtcNow.AddDays(-30), 20);
                viewModel.AddCommits(harvester.Collect());
                viewModel.ApplyGrouping();

                return View(viewModel);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(request.RepositoryName, ex);
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
