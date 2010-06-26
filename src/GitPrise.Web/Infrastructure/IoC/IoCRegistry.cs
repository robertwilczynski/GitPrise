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