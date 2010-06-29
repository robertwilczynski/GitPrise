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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GitPrise.Core.Services;
using GitPrise.Core.Configuration;
using GitPrise.Web.Models;
using GitSharp;
using GitPrise.Core;

namespace GitPrise.Web.Controllers
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

        public ActionResult Status()
        {
            return Content("GitPrise OK");
        }
    }
}
