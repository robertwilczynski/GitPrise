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
using System.Windows.Forms;
using NDesk.Options;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace GitPrise.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = new Arguments 
            { 
                ApplicationPath = ConfigurationManager.AppSettings["GitPriseWebPath"] 
            };

            var showHelp = false;

            var p = new OptionSet() {
                { "a|app|applicationPath:", "path to GitPrise web application", v => arguments.ApplicationPath = v },
   	            { "rn|repositoryName:", "repository name in local browsing mode", v => arguments.RepositoryName = v },                
                { "rp|repositoryPath:", "repository path in local browsing mode", v => arguments.RepositoryPath = v },
                { "p|port:", "server port", (int v) => arguments.Port = v },
                { "nb|noBrowser", "doesn't auto start the browser", v => arguments.StartBrowser = (v == null) },
   	            { "h|?|help", "shows help", v => showHelp = (v != null) },
            };

            try
            {
                p.Parse(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(
                    "Error parsing command line arguments:{0}{1}",
                    Environment.NewLine,
                    ex));
                return;
            }

            if (showHelp || ArgumentsAreInvalid(arguments))
            {
                ShowHelp(p);
                return;
            }
            var launcher = new BrowserLauncher(arguments.Port, arguments.RepositoryName, arguments.RepositoryPath);

            if (IsServerRunning(arguments))
            {
                launcher.Launch();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(arguments, launcher));
        }

        private static bool IsServerRunning(Arguments arguments)
        {
            var url = String.Format(@"http://localhost:{0}/status", arguments.Port);
            var request = WebRequest.Create(url);
            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseContent = reader.ReadToEnd();
                    if (responseContent != "GitPrise OK")
                    {
                        throw new InvalidOperationException(string.Format(
                            "Something seems to be already listening on port {0}.", 
                            arguments.Port));
                    }
                    return true;
                }
            }
            catch (WebException ex)
            {
                // not started
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    return false;
                }
                throw;
            }            
        }

        private static bool ArgumentsAreInvalid(Arguments arguments)
        {
            if (String.IsNullOrEmpty(arguments.ApplicationPath))
            {
                Console.WriteLine("GitPrise web application path was not supplied in either configuration file or as a command line parameter.");
                return true;
            }

            if (!Directory.Exists(arguments.ApplicationPath))
            {
                Console.WriteLine(string.Format(
                    "GitPrise web application path was supplied but the directory '{0}' was not found.",
                    arguments.ApplicationPath));
                return true;
            }

            if (arguments.Port == 0)
            {
                var portValue = ConfigurationManager.AppSettings["Port"];
                int port = 0;
                if (!string.IsNullOrEmpty(portValue) && !int.TryParse(portValue, out port))
                {
                    Console.WriteLine(string.Format(
                        "Port in configuration file is set to '{0}' but the value is invalid. Please fix the configuration entry or supply the port as a command lin argument.",
                        portValue));
                    return true;
                }
                arguments.Port = port;
            }
            return false;
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: GitPrise.Server [OPTIONS]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}


