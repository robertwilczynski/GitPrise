using System;
using System.Windows.Forms;
using NDesk.Options;
using System.Configuration;
using System.IO;

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(arguments));
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


