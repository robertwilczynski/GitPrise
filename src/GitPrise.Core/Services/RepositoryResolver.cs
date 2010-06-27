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
