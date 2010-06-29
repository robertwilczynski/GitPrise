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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using GitPrise.Core.Server;
using System.Net.Sockets;
using System.Net;
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

            using (var server = new GitPriseWebServer(KnownPort, webAppPath))
            {
                server.Start();
                var result = GitPriseWebServer.CheckPortAvailability(KnownPort);
                Assert.AreEqual(AvailabilityResult.InUseByGitPrise, result);
                server.Stop();
            }
        }
        
        // Must be doing something wrong here
        //[Test]
        //public void WhenPortIsTaken_CheckPortAvailability_ReturnsUnknown()
        //{
        //    var listener = new TcpListener(IPAddress.Any, KnownPort);
        //    listener.Start();
        //    listener.BeginAcceptSocket(x => 
        //    {
        //        var socket = listener.EndAcceptSocket(x);
        //        var buffer = new byte[1024];

        //        while (socket.Receive(buffer) > 0)
        //        {
        //        	
        //        }
        //        socket.Send(Encoding.UTF8.GetBytes("x"));
        //        socket.Close();
        //    }, null);
        //    var result = GitPriseWebServer.CheckPortAvailability(KnownPort);
        //    Assert.AreEqual(AvailabilityResult.InUseByGitPrise, result);
        //}
    }
}
