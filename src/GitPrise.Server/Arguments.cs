using System;

namespace GitPrise.Server
{
    public class Arguments
    {
        public int Port { get; set; }
        public string ApplicationPath { get; set; }
        public string VirtualDirectory { get; set; }
        public string HostName { get; set; }
        public bool StartBrowser { get; set; }
        public string RepositoryPath { get; set; }
        public string RepositoryName { get; set; }

        public Arguments()
        {
            Port = 0;
            StartBrowser = true;
            HostName = String.Empty;
            VirtualDirectory = "/";
            ApplicationPath = "web";
            RepositoryName = "repository";
            RepositoryPath = Environment.CurrentDirectory;
        }
    }
}
