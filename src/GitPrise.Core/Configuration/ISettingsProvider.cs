using System;

namespace GitPrise.Core.Configuration
{
    public interface ISettingsProvider
    {
        Settings Load();
    }
}
