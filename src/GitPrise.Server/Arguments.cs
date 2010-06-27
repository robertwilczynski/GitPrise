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
