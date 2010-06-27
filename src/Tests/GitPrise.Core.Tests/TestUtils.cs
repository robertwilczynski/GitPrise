using System;
using System.Reflection;
using System.IO;

namespace GitPrise.Core.Tests
{
    public static class TestUtils
    {
        public static string GetAbsolutePathFromRelativeToTestAssembly(string path)
        {            
            var uri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBase = uri.AbsolutePath;
            return Path.Combine(Path.GetDirectoryName(codeBase), path);
        }
    }
}
