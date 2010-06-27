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
