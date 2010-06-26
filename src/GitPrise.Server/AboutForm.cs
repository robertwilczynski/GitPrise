using System;
using System.Windows.Forms;

namespace GitPrise.Server
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            txtAbout.Text = @"Using icons from : http://pixel-mixer.com/";
        }
    }
}
