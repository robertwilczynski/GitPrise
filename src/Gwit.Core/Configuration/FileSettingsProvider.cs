using System;
using System.Configuration;

namespace Gwit.Core.Configuration
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
