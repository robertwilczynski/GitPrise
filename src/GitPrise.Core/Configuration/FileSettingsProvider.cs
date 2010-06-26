using System;
using System.Configuration;

namespace GitPrise.Core.Configuration
{
    public class FileSettingsProvider : ISettingsProvider
    {

        public Settings Load()
        {
            var settings = new Settings()
            {
                RepositoryRootPath = ConfigurationManager.AppSettings["RepositoryRootPath"]
            };

            return settings;
        }
    }
}
