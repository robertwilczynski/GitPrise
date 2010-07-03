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

#region Original license

// /* **********************************************************************************
//  *
//  * Copyright (c) Sky Sanders. All rights reserved.
//  * 
//  * This source code is subject to terms and conditions of the Microsoft Public
//  * License (Ms-PL). A copy of the license can be found in the license.htm file
//  * included in this distribution.
//  *
//  * You must not remove this notice, or any other, from this software.
//  *
//  * **********************************************************************************/

#endregion

using System;
using CassiniDev;
using System.Net;
using System.IO;
using System.Globalization;

namespace GitPrise.Core.Server
{
    /// <summary>
    /// Hosts GitPrise web application.
    /// </summary>
    /// <remarks>
    /// Code taken from CassiniDev as theyit wrapper doesn't expose CassiniDev.Server needed to disable timeout
    /// after which hosted application is shut down.
    /// </remarks>
    public class GitPriseWebServer : IDisposable
    {

        // Fields
        private CassiniDev.Server _server;

        // Methods
        public void Dispose()
        {
            if (this._server != null)
            {
                this.StopServer();
                this._server.Dispose();
                this._server = null;
            }
        }

        public string NormalizeUrl(string relativeUrl)
        {
            return CassiniNetworkUtils.NormalizeUrl(this.RootUrl, relativeUrl);
        }

        public void StartServer(string applicationPath)
        {
            this.StartServer(applicationPath, CassiniNetworkUtils.GetAvailablePort(0x1f40, 0x2710, IPAddress.Loopback, true), "/", "localhost");
        }

        public void StartServer(string applicationPath, int port)
        {
            StartServer(applicationPath, port, "/", string.Empty);
        }

        public void StartServer(string applicationPath, int port, string virtualPath, string hostName)
        {
            IPAddress loopback = IPAddress.Loopback;
            if (!CassiniNetworkUtils.IsPortAvailable(loopback, port))
            {
                throw new Exception(string.Format("Port {0} is in use.", port));
            }
            applicationPath = Path.GetFullPath(applicationPath);
            virtualPath = string.Format("/{0}/", (virtualPath ?? string.Empty).Trim(new char[] { '/' })).Replace("//", "/");
            hostName = string.IsNullOrEmpty(hostName) ? "localhost" : hostName;
            this.StartServer(applicationPath, loopback, port, virtualPath, hostName);
        }

        public void StartServer(string applicationPath, IPAddress ipAddress, int port, string virtualPath, string hostname)
        {
            if (this._server != null)
            {
                throw new InvalidOperationException("Server already started");
            }
            this._server = new CassiniDev.Server(port, virtualPath, applicationPath, ipAddress, hostname, Int32.MaxValue);
            try
            {
                this._server.Start();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Error starting server instance.", exception);
            }
        }

        public void StopServer()
        {
            if (this._server != null)
            {
                this._server.ShutDown();
            }
        }

        // Properties
        public string RootUrl
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}{2}", new object[] { this._server.HostName, this._server.Port, this._server.VirtualPath });
            }
        }
        

        public static AvailabilityResult CheckPortAvailability(int port)
        {
            var url = String.Format(@"http://localhost:{0}/status", port);
            var request = WebRequest.Create(url);
            try
            {
                var response = request.GetResponse();
                try
                {
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
                finally
                {
                    response.Close();
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

    }

}
