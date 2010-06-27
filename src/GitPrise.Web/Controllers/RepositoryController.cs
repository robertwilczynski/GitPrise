#region License

// Copyright 2010 Robert Wilczynski (http://github.com/robertwilczynski/gitprise)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

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
            var tree = GetTreeFromRequest(request);
            var node = tree.Node(request.Path);

            if (node == null)
            {
                throw new InvalidOperationException("Invalid path");
            }

            var blob = node as Leaf;
            if (blob == null)
            {
                throw new InvalidOperationException("Path is not pointing to a tree.");
            }

            var viewModel = new BlobViewModel(Repository, request, blob)
            {
                FormattedData = _hightlightingService.GenerateHtml(blob.Data, blob.Path, null)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Tree(RepositoryNavigationRequest request)
        {
            var treeRoot = GetTreeFromRequest(request);

            var tree = treeRoot.Node(request.Path) as Tree;
            if (tree == null)
            {
                throw new InvalidOperationException("Supplied path '{0}' is not pointing to a tree."
                    .Fill(request.Path));
            }

            var viewModel = new TreeViewModel(Repository, request, tree);
            return View("Tree", viewModel);
        }

        [HttpGet]
        public ActionResult Commit(RepositoryNavigationRequest request)
        {
            var commit = Repository.Get<Commit>(request.Treeish);
            var viewModel = new CommitDetailsViewModel(Repository, request, commit);

            foreach (var change in commit.Changes)
            {
                // PASTE-START : borrowed from GitSharp.Demo
                var a = (change.ReferenceObject != null ? (change.ReferenceObject as Blob).RawData : new byte[0]);
                var b = (change.ComparedObject != null ? (change.ComparedObject as Blob).RawData : new byte[0]);

                a = (Diff.IsBinary(a) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + a.Length) : a);
                b = (Diff.IsBinary(b) == true ? Encoding.ASCII.GetBytes("Binary content\nFile size: " + b.Length) : b);
                // PASTE-END : borrowed from GitSharp.Demo

                var diff = new Diff(a, b);
                viewModel.Changes.Add(new ChangeViewModel(request, change, diff));
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Commits(RepositoryNavigationRequest request)
        {
            var commit = GetCommitFromRequest(request);

            var harvester = new CommitHarvester(commit, DateTime.UtcNow.AddDays(-30), 20);
            var viewModel = new CommitsViewModel(Repository, request);
            viewModel.AddCommits(harvester.Collect());
            viewModel.ApplyGrouping();

            return View(viewModel);
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

        private Commit GetCommitFromRequest(RepositoryNavigationRequest request)
        {
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

            if (commit == null)
            {
                throw new InvalidOperationException("Unable to resolve a commit for supplied treeish '{0}'."
                    .Fill(request.Treeish));
            }
            return commit;
        }

        private Tree GetTreeFromRequest(RepositoryNavigationRequest request)
        {
            var obj = Repository.Get<AbstractObject>(request.Treeish);

            Tree tree = null;
            if (obj is Commit)
            {
                tree = (obj as Commit).Tree;
            }
            else if (obj is Tree)
            {
                tree = obj as Tree;
            }

            if (tree == null)
            {
                throw new InvalidOperationException("Unable to resolve a tree for supplied treeish '{0}'."
                    .Fill(request.Treeish));
            }

            return tree;
        }
    }
}
