using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Routing;

namespace Gwit.Core.Web.Mvc
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;

        public UnityControllerFactory(IUnityContainer container)
        {
            _container = container;
        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)_container.Resolve(controllerType);
        }
    }
}