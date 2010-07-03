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
using MbUnit.Framework;
using GitPrise.Core.Server;
using System.Reflection;
using System.IO;

namespace GitPrise.Core.Tests.Server
{
    [TestFixture]
    public class ServerStatusTests
    {
        private const int KnownPort = 12348;

        [Test]
        public void WhenServerIsNotRunning_CheckPortAvailability_ReturnsAvailable()
        {
            var result = GitPriseWebServer.CheckPortAvailability(KnownPort);
            Assert.AreEqual(AvailabilityResult.Available, result);
        }

        [Test]
        public void WhenPortIsTakenByGitPrise_CheckPortAvailability_ReturnsInUseByGitPrise()
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var dir = new DirectoryInfo(Path.GetDirectoryName(path));
            while (dir != null && dir.Name != "src")
            {
                dir = dir.Parent;
            }
            var webAppPath = Path.Combine(dir.FullName, "GitPrise.Web");

            using (var server = new GitPriseWebServer())
            {
                server.StartServer(webAppPath, KnownPort);
                var result = GitPriseWebServer.CheckPortAvailability(KnownPort);
                Assert.AreEqual(AvailabilityResult.InUseByGitPrise, result);
                server.StopServer();
            }
        }
    }
}
