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

namespace GitPrise.Server
{
    public class BrowserLauncher
    {
        private int _port;
        private string _repositoryName;
        private string _repositoryPath;

        public BrowserLauncher(int port, string repositoryName, string repositoryPath)
        {
            _repositoryPath = repositoryPath;
            _repositoryName = repositoryName;
            _port = port;            
        }

        public void Launch()
        {
            var browserProcessInfo = new ProcessStartInfo(
                String.Format(@"http://localhost:{0}/{1}?location={2}", 
                    _port, _repositoryName, _repositoryPath));

            Process.Start(browserProcessInfo);
        }

        public void LaunchAtRoot()
        {
            Process.Start(new ProcessStartInfo(
                String.Format(@"http://localhost:{0}", _port)));
        }
    }
}
