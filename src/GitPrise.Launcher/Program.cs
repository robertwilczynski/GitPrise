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
                Arguments = arguments.ToString(),
                UseShellExecute = false,
            };
            Process.Start(processInfo);
            

            var browserProcessInfo = new ProcessStartInfo(
                String.Format(@"http://localhost:{0}/repo?location={1}", port, Environment.CurrentDirectory));
            Process.Start(browserProcessInfo);
        }
    }
}
