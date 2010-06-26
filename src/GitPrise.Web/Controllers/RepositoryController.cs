using System;
using System.Web.Mvc;
using GitSharp;
using GitPrise.Core.Configuration;
using GitPrise.Web.Models;
using GitPrise.Core.GitSharp;
using GitPrise.Core.Services;
using GitPrise.Core.SyntaxHighlighting;
using GitPrise.Web.Mvc;
using System.Text;

namespace GitPrise.Web.Controllers
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
                
                foreach (var change in commit.Changes)
                {
                    // PASTE-START : borrowed from GitSharp.Demo
                    var a = (change.ReferenceObject != null ? (change.ReferenceObject as Blob).RawData : new byte[0]);
                    var b = (change.ComparedObject != null ? (change.ComparedObject as Blob).RawData : new byte[0]);
                    
                    a = (Diff.IsBinary(a) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + a.Length) : a);
                    b = (Diff.IsBinary(b) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + b.Length) : b);
                    // PASTE-END : borrowed from GitSharp.Demo

                    var diff = new Diff(a, b);
                    viewModel.Changes.Add(new ChangeViewModel(change, diff));
                }
                
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
                var viewModel = new CommitsViewModel(Repository, request.RepositoryName, request.Treeish);
                var obj = Repository.Get<AbstractObject>(request.Treeish);
                Commit commit = null;
                if (obj.IsCommit)
                {
                    commit = obj as Commit;
                }
                else if (obj.IsTree)
                {
                    commit = (obj as Tree).GetLastCommit();
                }
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
