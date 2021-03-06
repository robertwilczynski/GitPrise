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
using Microsoft.Practices.Unity;
using GitPrise.Core.Configuration;
using GitPrise.Core.Services;
using GitPrise.Core.SyntaxHighlighting;
using GitPrise.SyntaxHighlighting.Pygments;

namespace GitPrise.Web.Infrastructure.IoC
{
    internal static class IoCRegistry
    {
        public static IUnityContainer Container = BuildContainer();

        private static IUnityContainer BuildContainer()
        {
            var container = new UnityContainer();
            container
                .RegisterType<ISettingsProvider, FileSettingsProvider>()
                .RegisterType<IRepositoryResolver, RepositoryResolver>()
                .RegisterInstance<IHighlightingService>(new PygmentsHighlightingService())
                ;
            return container;
        }
    }
}