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

        public static string GetRepositoryPath(string name)
        {
            return TestUtils.GetAbsolutePathFromRelativeToTestAssembly(Path.Combine(@"..\..\..\Repositories\", name));
        }
    }
}
