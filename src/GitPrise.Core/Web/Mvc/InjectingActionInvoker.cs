using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace GitPrise.Core.Web.Mvc
{
    public class InjectingActionInvoker : ControllerActionInvoker
    {
        private readonly IUnityContainer _container;

        public InjectingActionInvoker(IUnityContainer container)
        {
            _container = container;
        }

        protected override FilterInfo GetFilters(
            ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var info = base.GetFilters(controllerContext, actionDescriptor);

            // _container.BuildUp(x.GetType(), x) required, otherwise Unity seems to only parse properties on 
            // exact type (IActionFilter) and not on descendants.
            info.AuthorizationFilters.ForEach(x => _container.BuildUp(x.GetType(), x));
            info.ActionFilters.ForEach(x => _container.BuildUp(x.GetType(), x));
            info.ResultFilters.ForEach(x => _container.BuildUp(x.GetType(), x));
            info.ExceptionFilters.ForEach(x => _container.BuildUp(x.GetType(), x));

            return info;
        }
    }
}
