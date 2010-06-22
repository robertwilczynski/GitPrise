using System;
using System.Web;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Gwit.Web.Controllers;
using Gwit.Core.Configuration;
using Gwit.Core.Services;
using Gwit.Core.Web.Mvc;
using Gwit.Web.Models;

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
                throw new InvalidOperationException("{0} can only be applied to {1}.".Fill(GetType().Name, typeof(RepositoryController).Name));
            }

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

            var repoLocation = (string)filterContext.RequestContext.HttpContext.Request.QueryString["location"];
            if (!String.IsNullOrEmpty(repoLocation))
            {
                repoLocation = HttpUtility.UrlDecode(repoLocation);
                request.RepositoryLocation = repoLocation;
            }
            else
            {
                repoLocation = request.RepositoryName;
            }

            controller.Repository = RepositoryResolver.GetRepository(repoLocation);

            request.Treeish = (string)filterContext.RouteData.Values["id"];
            request.Treeish = request.Treeish ?? "master";
            request.Path = (string)filterContext.RouteData.Values["path"];

            // Storing data in context for access by Html / Url helpers
            filterContext.RequestContext.SetRepositoryName(request.RepositoryName);
            filterContext.RequestContext.SetTreeish(request.Treeish);
            filterContext.RequestContext.SetRepositoryLocation(request.RepositoryLocation);
            filterContext.RequestContext.SetPath(request.Path);

            // Each action should expect a request.
            filterContext.ActionParameters["request"] = request;
        }
    }
}