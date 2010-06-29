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

namespace GitPrise.Server
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStop = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApplicationPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lnkStartAtRoot = new System.Windows.Forms.LinkLabel();
            this.lnkStartAtLocation = new System.Windows.Forms.LinkLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startBrowserAtRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startBrowserAtOriginalLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(256, 122);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(147, 28);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "&Stop server";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GitPrise Web Server";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.toolStripSeparator1,
            this.startBrowserAtRootToolStripMenuItem,
            this.startBrowserAtOriginalLocationToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(246, 98);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.showToolStripMenuItem.Text = "&Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.Location = new System.Drawing.Point(12, 122);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(80, 28);
            this.btnAbout.TabIndex = 1;
            this.btnAbout.Text = "&About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Application path";
            // 
            // txtApplicationPath
            // 
            this.txtApplicationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplicationPath.Location = new System.Drawing.Point(98, 6);
            this.txtApplicationPath.Name = "txtApplicationPath";
            this.txtApplicationPath.ReadOnly = true;
            this.txtApplicationPath.Size = new System.Drawing.Size(305, 20);
            this.txtApplicationPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 5;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(98, 35);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(86, 20);
            this.txtPort.TabIndex = 6;
            // 
            // lnkStartAtRoot
            // 
            this.lnkStartAtRoot.AutoSize = true;
            this.lnkStartAtRoot.Location = new System.Drawing.Point(98, 62);
            this.lnkStartAtRoot.Name = "lnkStartAtRoot";
            this.lnkStartAtRoot.Size = new System.Drawing.Size(102, 13);
            this.lnkStartAtRoot.TabIndex = 7;
            this.lnkStartAtRoot.TabStop = true;
            this.lnkStartAtRoot.Text = "Start browser at root";
            this.lnkStartAtRoot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkStartAtRoot_LinkClicked);
            // 
            // lnkStartAtLocation
            // 
            this.lnkStartAtLocation.AutoSize = true;
            this.lnkStartAtLocation.Location = new System.Drawing.Point(98, 88);
            this.lnkStartAtLocation.Name = "lnkStartAtLocation";
            this.lnkStartAtLocation.Size = new System.Drawing.Size(205, 13);
            this.lnkStartAtLocation.TabIndex = 8;
            this.lnkStartAtLocation.TabStop = true;
            this.lnkStartAtLocation.Text = "Start browser at original repository location";
            this.lnkStartAtLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkStartAtLocation_LinkClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // startBrowserAtRootToolStripMenuItem
            // 
            this.startBrowserAtRootToolStripMenuItem.Name = "startBrowserAtRootToolStripMenuItem";
            this.startBrowserAtRootToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.startBrowserAtRootToolStripMenuItem.Text = "Start &browser at root";
            this.startBrowserAtRootToolStripMenuItem.Click += new System.EventHandler(this.startBrowserAtRootToolStripMenuItem_Click);
            // 
            // startBrowserAtOriginalLocationToolStripMenuItem
            // 
            this.startBrowserAtOriginalLocationToolStripMenuItem.Name = "startBrowserAtOriginalLocationToolStripMenuItem";
            this.startBrowserAtOriginalLocationToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.startBrowserAtOriginalLocationToolStripMenuItem.Text = "Start browser at original &location";
            this.startBrowserAtOriginalLocationToolStripMenuItem.Click += new System.EventHandler(this.startBrowserAtOriginalLocationToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 162);
            this.Controls.Add(this.lnkStartAtLocation);
            this.Controls.Add(this.lnkStartAtRoot);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtApplicationPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 200);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "GitPrise Web Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApplicationPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.LinkLabel lnkStartAtRoot;
        private System.Windows.Forms.LinkLabel lnkStartAtLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem startBrowserAtRootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startBrowserAtOriginalLocationToolStripMenuItem;
    }
}