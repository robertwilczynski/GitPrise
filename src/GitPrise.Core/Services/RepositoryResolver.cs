using System;
using GitPrise.Core.Configuration;
using System.IO;
using GitSharp;

namespace GitPrise.Core.Services
{
    public class RepositoryResolver : IRepositoryResolver
    {
        private ISettingsProvider _settingsProvider;
        public RepositoryResolver(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        private string GetPath(string name, bool bare)
        {
            var settings = _settingsProvider.Load();
            var path = Path.Combine(settings.RepositoryRootPath, bare ? name.Git() : name);
            return path;
        }

        public Repository GetRepository(string name)
        {
            var path = String.Empty;
            if (Path.IsPathRooted(name))
            {
                path = name;
                if (Directory.Exists(path))
                {
                    return new Repository(path);
                }
            }

            path = GetPath(name, true);
            if (Directory.Exists(path))
            {
                return new Repository(path);
            }

            path = GetPath(name, false);
            if (Directory.Exists(path))
            {
                return new Repository(path);
            }

            throw new InvalidOperationException("Directory for repository '{0}' not found.".Fill(name));                        
        }
    }
}
