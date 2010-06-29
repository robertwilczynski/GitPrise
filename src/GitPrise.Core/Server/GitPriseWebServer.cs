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
