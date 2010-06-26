using System;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace GitPrise.Launcher
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var vsWebServer = ConfigurationManager.AppSettings["VSWebServerPath"];
            if (!File.Exists(vsWebServer))
            {
                MessageBox.Show(
                    String.Format("VSWebServerPath in configuration file points to '{0}' but this location doesn't exist.", vsWebServer));
                return;
            }

            var webDir = ConfigurationManager.AppSettings["WebApplicationDirectory"];
            if (!Directory.Exists(webDir))
            {
                MessageBox.Show(
                    String.Format("WebApplicationDirectory in configuration file points to '{0}' but this location doesn't exist.", webDir));
                return;
            }

            var portValue = ConfigurationManager.AppSettings["Port"];
            int port = 0;
            if (!String.IsNullOrEmpty(portValue) && !int.TryParse(portValue, out port))
            {
                MessageBox.Show(
                    String.Format("Port in configuration file is set to '{0}' but the value is invalid.", portValue));
                return;
            }

            var arguments = new StringBuilder(@"/nodirlist /vpath:""/""");
            if (port > 0)
            {
                arguments.Append(@" /port:").Append(port);
            }
            arguments.AppendFormat(@" /path:""{0}""", webDir);

            var processInfo = new ProcessStartInfo(vsWebServer)
            {
                Arguments = arguments.ToString()
            };
            Process.Start(processInfo);

            var browserProcessInfo = new ProcessStartInfo(
                String.Format(@"http://localhost:{0}/repo?location={1}", port, Environment.CurrentDirectory));
            Process.Start(browserProcessInfo);
        }
    }
}
