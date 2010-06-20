using System;

namespace Gwit.Core.Configuration
{
    public interface ISettingsProvider
    {
        Settings Load();
    }
}
