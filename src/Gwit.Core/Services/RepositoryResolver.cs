using System;
using Gwit.Core.Configuration;
using System.IO;
using GitSharp;

namespace Gwit.Core.Services
{
    public class RepositoryResolver : IRepositoryResolver
    {
        private ISettingsProvider _settingsProvider;
        public RepositoryResolver(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public string GetPath(string name)
        {
            var settings = _settingsProvider.Load();
            var path = Path.Combine(settings.RepositoryRootPath, name.Git());
            return path;
        }

        public Repository GetRepository(string name)
        {
            var path = GetPath(name);
            return new Repository(path);
        }
    }
}
