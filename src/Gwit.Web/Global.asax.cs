using System;
using System.Web.Mvc;
using System.Web.Routing;
using Gwit.Core.Web.Mvc;
using Gwit.Web.Infrastructure.IoC;

namespace Gwit.Web
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RepositoryShort", // Route name
                "{repositoryName}", // URL with parameters
                new { controller = "Repository", action = "Details"} // Parameter defaults
            );

            routes.MapRoute(
                "Repository", // Route name
                "{repositoryName}/{action}/{id}/{*path}", // URL with parameters
                new { controller = "Repository", action = "Details", id = UrlParameter.Optional, path = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
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