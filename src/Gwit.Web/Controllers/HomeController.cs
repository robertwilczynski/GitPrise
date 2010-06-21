using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Gwit.Core.Services;
using Gwit.Core.Configuration;
using Gwit.Web.Models;
using GitSharp;
using Gwit.Core;

namespace Gwit.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private ISettingsProvider _configurationProvider;
        private Settings _settings;
        private IRepositoryResolver _repoResolver;

        public HomeController(
            ISettingsProvider settingsProvider,
            IRepositoryResolver repoResolver
        )
        {
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
                var isValid = Repository.IsValid(path);
                if (isValid)
                {
                    using (var repo = new Repository(path))
                    {
                        viewModel.List.Add(new RepositoryViewModel
                        {
                            Name = repo.IsBare ? Path.GetFileName(repo.Directory.TrimGit())
                            : Path.GetFileName(path.TrimGit()),
                            Description = repo.Directory,
                            Path = repo.Directory,
                            CurrentCommit = repo.Branches["master"].CurrentCommit
                        });
                    }
                }

            }

            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
