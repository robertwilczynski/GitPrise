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
using System.Web;
using GitPrise.Core;
using System.Web.Mvc;
using GitPrise.Web.Models;
using GitPrise.Core.Services;
using GitPrise.Web.Controllers;
using Microsoft.Practices.Unity;
using GitPrise.Core.Configuration;

namespace GitPrise.Web.Mvc
{
    public class RepositoryRequestAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ISettingsProvider ConfigurationProvider { get; set; }

        [Dependency]
        public IRepositoryResolver RepositoryResolver { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            RepositoryController controller = GetController(filterContext);

            if (filterContext.ActionParameters.Count != 1 ||
                !filterContext.ActionParameters.ContainsKey("request") ||
                filterContext.ActionParameters["request"].GetType() != typeof(RepositoryNavigationRequest))
            {
                return;
            }

            // Building request to pass to controller action.
            var request = new RepositoryNavigationRequest
            {
                RepositoryName = filterContext.RouteData.GetRequiredString("repositoryName")
            };

            var repoLocation = (string)filterContext.HttpContext.Request.QueryString["location"];
            if (!String.IsNullOrEmpty(repoLocation))
            {
                repoLocation = HttpUtility.UrlDecode(repoLocation);
                request.RepositoryLocation = repoLocation;
            }
            else
            {
                repoLocation = request.RepositoryName;
            }

            var repository = RepositoryResolver.GetRepository(repoLocation);
            if (repository == null)
            {
                throw new RepositoryNotFoundException(request.RepositoryName)
                {
                    RepositoryLocation = repoLocation
                };
            }
            controller.Repository = repository;

            request.Treeish = (string)filterContext.RouteData.Values["id"];
            request.Treeish = request.Treeish ?? "master";
            request.Path = (string)filterContext.RouteData.Values["path"];

            // Each action should expect a request.
            filterContext.ActionParameters["request"] = request;
        }

        private RepositoryController GetController(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as RepositoryController;
            if (controller == null)
            {
                throw new InvalidOperationException("{0} can only be applied to {1}.".
                    Fill(GetType().Name, typeof(RepositoryController).Name));
            }
            return controller;
        }
    }
}