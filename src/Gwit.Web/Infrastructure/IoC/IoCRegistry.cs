using System;
using Microsoft.Practices.Unity;
using Gwit.Core.Configuration;
using Gwit.Core.Services;
using Gwit.Core.SyntaxHighlighting;
using Gwit.SyntaxHighlighting.Pygments;

namespace Gwit.Web.Infrastructure.IoC
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