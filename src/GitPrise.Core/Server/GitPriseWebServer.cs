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
using CassiniDev;
using System.Net;
using System.IO;

namespace GitPrise.Core.Server
{
    /// <summary>
    /// Hosts GitPrise web application.
    /// </summary>
    public class GitPriseWebServer : IDisposable
    {
        private readonly CassiniDevServer _server;
        private readonly int _port;
        private readonly string _webApplication;

        public GitPriseWebServer(int port, string webApplication)
        {
            _webApplication = webApplication;
            _port = port;
            _server = new CassiniDevServer();
        }

        public void Start()
        {
            _server.StartServer(_webApplication, _port, "/", string.Empty);
        }

        public static AvailabilityResult CheckPortAvailability(int port)
        {
            var url = String.Format(@"http://localhost:{0}/status", port);
            var request = WebRequest.Create(url);
            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseContent = reader.ReadToEnd();
                    if (responseContent != "GitPrise OK")
                    {
                        return AvailabilityResult.Unknown;
                    }
                    return AvailabilityResult.InUseByGitPrise;
                }
            }
            catch (WebException ex)
            {
                // not started
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    return AvailabilityResult.Available;
                }
                throw;
            }
        }

        public void Stop()
        {
            _server.StopServer();
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
