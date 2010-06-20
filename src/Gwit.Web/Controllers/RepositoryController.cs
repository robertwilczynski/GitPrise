using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GitSharp;
using Gwit.Core.Configuration;
using System.IO;
using Gwit.Web.Models;
using Gwit.Core;
using Gwit.Core.GitSharp;
using Gwit.Core.Services;
using System.Linq;
using Gwit.Core.SyntaxHighlighting;

namespace Gwit.Web.Controllers
{
    public class RepositoryController : Controller
    {
        private ISettingsProvider _configurationProvider;
        private Settings _settings;
        private IRepositoryResolver _repoResolver;
        private IHighlightingService _hightlightingService;

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

        public ActionResult Index()
        {
            var dirs = Directory.GetDirectories(_settings.RepositoryRootPath);
            var viewModel = new RepositoriesViewModel { List = new List<RepositoryViewModel>() };
            foreach (string path in dirs)
            {
                using (var repo = new Repository(path))
                {
                    viewModel.List.Add(new RepositoryViewModel
                    {
                        Name = Path.GetFileName(repo.Directory.TrimGit()),
                        Description = repo.Directory,
                        Path = repo.Directory,
                    });
                }
            }

            return View(viewModel);
        }

        public ActionResult Details(string repositoryName)
        {
            using (var repo = _repoResolver.GetRepository(repositoryName))
            {
                Commit commit = repo.Head.CurrentCommit;
                var viewModel = new RepositoryDetailsViewModel(repo,
                    repositoryName,
                    commit,
                    new TreeViewModel(
                        repo,
                        repositoryName,
                        repo.Head.CurrentCommit,
                        new PathViewModel(repo, Request.RequestContext, repositoryName, commit.Hash, commit.Tree)
                    )
                );                
                return View(viewModel);
            }
        }

        public ActionResult Blob(string repositoryName, string id, string path)
        {
            try
            {
                using (var repo = _repoResolver.GetRepository(repositoryName))
                {
                    var commit = repo.Get<Commit>(id);
                    var tree = commit.Tree;
                    if (!String.IsNullOrEmpty(path))
                    {
                        var parts = path.Split('/');
                        var treeParts = parts.Take(parts.Length - 1);
                        var blobName = parts.Last();
                        foreach (string part in treeParts)
                        {
                            var trees = tree.Trees.ToList();
                            tree = trees.First(x => x.Name == part);
                        }
                        var blob = tree.Leaves.First(x => x.Name == blobName);

                        var viewModel = new BlobViewModel(repo, repositoryName, commit, blob, new PathViewModel(repo, this.Request.RequestContext, repositoryName, id, blob))
                        {
                            FormattedData = _hightlightingService.GenerateHtml(blob.Data, blob.Path, null)
                        };
                        return View(viewModel);
                    }
                    else
                    {
                        throw new InvalidOperationException("No path provided");
                    }

                }
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
                using (var repo = _repoResolver.GetRepository(repositoryName))
                {
                    var commit = repo.Get<Commit>(id);
                    var tree = commit.Tree;
                    if (!String.IsNullOrEmpty(path))
                    {
                        var parts = path.Split('/');
                        foreach (string part in parts)
                        {
                            var trees = tree.Trees.ToList();
                            tree = trees.First(x => x.Name == part);
                        }
                    }
                    var viewModel = new TreeViewModel(
                        repo, 
                        repositoryName, commit, tree,
                        new PathViewModel(repo, Request.RequestContext, repositoryName, commit.Hash, tree)
                    )
                    {
                    };
                    return View(viewModel);
                }
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
                using (var repo = _repoResolver.GetRepository(repositoryName))
                {
                    var commit = repo.Get<Commit>(id);
                    var viewModel = new RepositoryDetailsViewModel(repo, repositoryName, commit, new TreeViewModel(
                        repo, 
                        repositoryName,
                        commit,
                        new PathViewModel(repo, Request.RequestContext, repositoryName, commit.Hash, commit.Tree)
                    ));
                    return View(viewModel);
                }
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
                using (var repo = _repoResolver.GetRepository(repositoryName))
                {
                    Ref reference = null;
                    if (String.IsNullOrEmpty(id))
                    {
                        reference = repo.Branches["master"];
                    }
                    var viewModel = new CommitsViewModel(repo, repositoryName);
                    var commit = (reference.Target as Commit);
                    CommitHarvester harvester = new CommitHarvester(commit, DateTime.UtcNow.AddDays(-30), 20);
                    viewModel.AddCommits(harvester.Collect());
                    viewModel.ApplyGrouping();

                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(repositoryName, ex);
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
