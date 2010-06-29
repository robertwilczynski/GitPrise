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
using System.Web.Routing;
using GitPrise.Core.Web.Mvc;
using GitPrise.Web.Infrastructure.IoC;

namespace GitPrise.Web
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "About",
                "about",
                new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                "status", 
                "status",
                new { controller = "Home", action = "Status", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RepositoryShort",
                "{repositoryName}",
                new { controller = "Repository", action = "Details"}
            );

            routes.MapRoute(
                "Repository",
                "{repositoryName}/{action}/{id}/{*path}",
                new { controller = "Repository", action = "Details", id = UrlParameter.Optional, path = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(IoCRegistry.Container));
            
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}