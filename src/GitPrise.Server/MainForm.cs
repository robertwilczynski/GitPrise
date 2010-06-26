using System;
using System.Windows.Forms;
using CassiniDev;

namespace GitPrise.Server
{
    public partial class MainForm : Form
    {
        private static CassiniDevServer _server;
        private Arguments _arguments;

        public MainForm(Arguments arguments)
        {
            _arguments = arguments;
            InitializeComponent();
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
                BrowserLauncher.Launch(_arguments.Port, _arguments.RepositoryName, _arguments.RepositoryPath);
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
            WindowState = FormWindowState.Minimized;
            e.Cancel = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _server.StopServer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            Application.Exit();
        }

    }
}
