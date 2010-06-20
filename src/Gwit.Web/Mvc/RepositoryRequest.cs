using System;
using System.Web;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Gwit.Web.Controllers;
using Gwit.Core.Configuration;
using Gwit.Core.Services;
using Gwit.Core.Web.Mvc;

namespace Gwit.Web.Mvc
{
    public class RepositoryRequest : ActionFilterAttribute
    {
        [Dependency]
        public ISettingsProvider ConfigurationProvider { get; set; }

        [Dependency]
        public IRepositoryResolver RepositoryResolver { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controller = filterContext.Controller as RepositoryController;
            if (controller == null)
            {
                throw new InvalidOperationException("{0} canonly be applied to {1}.".Fill(GetType().Name, typeof(RepositoryController).Name));
            }
            controller.RepositoryName = filterContext.RouteData.GetRequiredString("repositoryName");
            filterContext.RequestContext.SetRepositoryName(controller.RepositoryName);

            var repoLocation = (string)filterContext.RequestContext.HttpContext.Request.QueryString["location"];
            filterContext.RequestContext.SetRepositoryLocation(repoLocation);
            if (String.IsNullOrEmpty(repoLocation))
            {
                controller.RepositoryLocation = controller.RepositoryName;
            }
            else
            {
                controller.RepositoryLocation = HttpUtility.UrlDecode(repoLocation);
            }

            controller.Repository = RepositoryResolver.GetRepository(controller.RepositoryLocation);

            controller.Treeish = (string)filterContext.RouteData.Values["id"];
            controller.Treeish = controller.Treeish ?? "master";
            filterContext.RequestContext.SetTreeish(controller.Treeish);

            controller.Path = (string)filterContext.RouteData.Values["path"];
        }
    }
}