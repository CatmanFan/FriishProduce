using System.Drawing;

namespace FriishProduce
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BrowseROM = new System.Windows.Forms.OpenFileDialog();
            this.SaveWAD = new System.Windows.Forms.SaveFileDialog();
            this.BrowseImage = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new MdiTabControl.TabControl();
            this.BrowseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.NewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Project = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenROM = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UseLibRetro = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExportWAD = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowseROM
            // 
            this.BrowseROM.RestoreDirectory = true;
            // 
            // SaveWAD
            // 
            this.SaveWAD.DefaultExt = "wad";
            this.SaveWAD.RestoreDirectory = true;
            this.SaveWAD.SupportMultiDottedExtensions = true;
            // 
            // BrowseImage
            // 
            resources.ApplyResources(this.BrowseImage, "BrowseImage");
            this.BrowseImage.RestoreDirectory = true;
            // 
            // tabControl
            // 
            this.tabControl.BackHighColor = System.Drawing.Color.WhiteSmoke;
            this.tabControl.BackLowColor = System.Drawing.Color.White;
            this.tabControl.CloseButtonVisible = true;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.ForeColorDisabled = System.Drawing.Color.DarkGray;
            this.tabControl.MenuRenderer = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tabControl.TabBackHighColor = System.Drawing.Color.White;
            this.tabControl.TabBackHighColorDisabled = System.Drawing.Color.LightGray;
            this.tabControl.TabBackLowColor = System.Drawing.Color.Gainsboro;
            this.tabControl.TabBackLowColorDisabled = System.Drawing.Color.WhiteSmoke;
            this.tabControl.TabBorderEnhanced = true;
            this.tabControl.TabBorderEnhanceWeight = MdiTabControl.TabControl.Weight.Soft;
            this.tabControl.TabCloseButtonImage = null;
            this.tabControl.TabCloseButtonImageDisabled = null;
            this.tabControl.TabCloseButtonImageHot = null;
            this.tabControl.TabCloseButtonSize = new System.Drawing.Size(14, 14);
            this.tabControl.TabCloseButtonVisible = false;
            this.tabControl.TabGlassGradient = true;
            this.tabControl.TabHeight = 25;
            this.tabControl.TabMaximumWidth = 350;
            this.tabControl.TabOffset = 5;
            this.tabControl.TabPadLeft = 7;
            this.tabControl.TabPadRight = 7;
            this.tabControl.SelectedTabChanged += new System.EventHandler(this.TabChanged);
            this.tabControl.TabIndexChanged += new System.EventHandler(this.TabChanged);
            // 
            // BrowseManual
            // 
            this.BrowseManual.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.BrowseManual.ShowNewFolderButton = false;
            // 
            // MenuStrip
            // 
            this.MenuStrip.AllowMerge = false;
            this.MenuStrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File,
            this.MenuItem_Project});
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.Name = "MenuStrip";
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProject,
            this.toolStripSeparator5,
            this.MenuItem_Settings,
            this.MenuItem_About,
            this.toolStripSeparator4,
            this.MenuItem_Exit});
            this.MenuItem_File.Name = "MenuItem_File";
            resources.ApplyResources(this.MenuItem_File, "MenuItem_File");
            // 
            // NewProject
            // 
            this.NewProject.Name = "NewProject";
            resources.ApplyResources(this.NewProject, "NewProject");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // MenuItem_Settings
            // 
            this.MenuItem_Settings.Image = global::FriishProduce.Properties.Resources.wrench;
            this.MenuItem_Settings.Name = "MenuItem_Settings";
            resources.ApplyResources(this.MenuItem_Settings, "MenuItem_Settings");
            this.MenuItem_Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // MenuItem_About
            // 
            this.MenuItem_About.Name = "MenuItem_About";
            resources.ApplyResources(this.MenuItem_About, "MenuItem_About");
            this.MenuItem_About.Click += new System.EventHandler(this.About_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // MenuItem_Exit
            // 
            this.MenuItem_Exit.Image = global::FriishProduce.Properties.Resources.door_open;
            this.MenuItem_Exit.Name = "MenuItem_Exit";
            resources.ApplyResources(this.MenuItem_Exit, "MenuItem_Exit");
            this.MenuItem_Exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // MenuItem_Project
            // 
            this.MenuItem_Project.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenROM,
            this.OpenImage,
            this.OpenManual,
            this.toolStripSeparator1,
            this.UseLibRetro,
            this.toolStripSeparator2,
            this.ExportWAD,
            this.toolStripSeparator3,
            this.CloseTab});
            this.MenuItem_Project.Name = "MenuItem_Project";
            resources.ApplyResources(this.MenuItem_Project, "MenuItem_Project");
            // 
            // OpenROM
            // 
            resources.ApplyResources(this.OpenROM, "OpenROM");
            this.OpenROM.Image = global::FriishProduce.Properties.Resources.disc_blue;
            this.OpenROM.Name = "OpenROM";
            this.OpenROM.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // OpenImage
            // 
            resources.ApplyResources(this.OpenImage, "OpenImage");
            this.OpenImage.Image = global::FriishProduce.Properties.Resources.image_sunset;
            this.OpenImage.Name = "OpenImage";
            this.OpenImage.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // OpenManual
            // 
            resources.ApplyResources(this.OpenManual, "OpenManual");
            this.OpenManual.Image = global::FriishProduce.Properties.Resources.book;
            this.OpenManual.Name = "OpenManual";
            this.OpenManual.Click += new System.EventHandler(this.OpenManual_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // UseLibRetro
            // 
            resources.ApplyResources(this.UseLibRetro, "UseLibRetro");
            this.UseLibRetro.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.UseLibRetro.Name = "UseLibRetro";
            this.UseLibRetro.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // ExportWAD
            // 
            resources.ApplyResources(this.ExportWAD, "ExportWAD");
            this.ExportWAD.Image = global::FriishProduce.Properties.Resources.box_label;
            this.ExportWAD.Name = "ExportWAD";
            this.ExportWAD.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // CloseTab
            // 
            resources.ApplyResources(this.CloseTab, "CloseTab");
            this.CloseTab.Name = "CloseTab";
            this.CloseTab.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.OpenFileDialog BrowseROM;
        internal System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.OpenFileDialog BrowseImage;
        internal MdiTabControl.TabControl tabControl;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog BrowseManual;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem NewProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Settings;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_About;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Project;
        private System.Windows.Forms.ToolStripMenuItem OpenROM;
        private System.Windows.Forms.ToolStripMenuItem OpenImage;
        private System.Windows.Forms.ToolStripMenuItem OpenManual;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem UseLibRetro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ExportWAD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CloseTab;
    }
}

