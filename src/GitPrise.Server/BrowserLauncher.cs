using System;
using System.Diagnostics;

namespace GitPrise.Server
{
    class BrowserLauncher
    {
        public static void Launch(int port, string repositoryName, string repositoryPath)
        {
            var browserProcessInfo = new ProcessStartInfo(
                String.Format(@"http://localhost:{0}/{1}?location={2}", 
                    port, repositoryName, repositoryPath));
            Process.Start(browserProcessInfo);
        }
    }
}
