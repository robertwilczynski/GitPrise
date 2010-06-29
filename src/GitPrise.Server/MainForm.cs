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
using CassiniDev;

namespace GitPrise.Server
{
    public partial class MainForm : Form
    {
        private readonly CassiniDevServer _server;
        private readonly Arguments _arguments;
        private readonly BrowserLauncher _launcher;

        private bool _isShutdownInProgress;

        public MainForm(Arguments arguments, BrowserLauncher launcher)
        {
            _arguments = arguments;
            _launcher = launcher;

            InitializeComponent();
            
            txtApplicationPath.Text = arguments.ApplicationPath;
            txtPort.Text = arguments.Port.ToString();
            
            _server = new CassiniDevServer();
            _server.StartServer(arguments.ApplicationPath, arguments.Port, arguments.VirtualDirectory, arguments.HostName);
            
            notifyIcon.ShowBalloonTip(
                3000, 
                "GitPrise Web Server", 
                string.Format("Server running on port {0}.", arguments.Port), 
                ToolTipIcon.Info);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_arguments.StartBrowser)
            {
                _launcher.Launch();
            }
        }

        private void ShowForm()
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!_isShutdownInProgress)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }
        
        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void lnkStartAtRoot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _launcher.Launch();
        }

        private void lnkStartAtLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _launcher.LaunchAtRoot();
        }

        private void Stop()
        {
            _isShutdownInProgress = true;
            _server.StopServer();
            Application.Exit();
        }

        private void startBrowserAtRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _launcher.LaunchAtRoot();
        }

        private void startBrowserAtOriginalLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _launcher.Launch();
        }
    }
}
