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
            this.SaveWAD = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_new_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_save_project_as = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_retrieve_gamedata_online = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_close_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about_app = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolbarNewProject = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolbarOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolbarSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolbarCloseProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarRetrieveGameData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolbarExport = new System.Windows.Forms.ToolStripButton();
            this.SaveProject = new System.Windows.Forms.SaveFileDialog();
            this.BrowseProject = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new MdiTabControl.TabControl();
            this.menuStrip.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveWAD
            // 
            this.SaveWAD.DefaultExt = "wad";
            this.SaveWAD.RestoreDirectory = true;
            this.SaveWAD.SupportMultiDottedExtensions = true;
            // 
            // menuStrip
            // 
            this.menuStrip.AllowMerge = false;
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_project,
            this.menu_help});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // menu_file
            // 
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_new_project,
            this.menu_open_project,
            this.menu_save_project_as,
            this.toolStripSeparator3,
            this.menu_settings,
            this.toolStripSeparator4,
            this.menu_exit});
            this.menu_file.Name = "menu_file";
            resources.ApplyResources(this.menu_file, "menu_file");
            this.menu_file.Tag = "file";
            // 
            // menu_new_project
            // 
            this.menu_new_project.Image = global::FriishProduce.Properties.Resources.page_white;
            this.menu_new_project.Name = "menu_new_project";
            resources.ApplyResources(this.menu_new_project, "menu_new_project");
            this.menu_new_project.Tag = "new_project";
            // 
            // menu_open_project
            // 
            this.menu_open_project.Image = global::FriishProduce.Properties.Resources.folder_page;
            this.menu_open_project.Name = "menu_open_project";
            resources.ApplyResources(this.menu_open_project, "menu_open_project");
            this.menu_open_project.Tag = "open_project";
            this.menu_open_project.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // menu_save_project_as
            // 
            resources.ApplyResources(this.menu_save_project_as, "menu_save_project_as");
            this.menu_save_project_as.Image = global::FriishProduce.Properties.Resources.drive_disk;
            this.menu_save_project_as.Name = "menu_save_project_as";
            this.menu_save_project_as.Tag = "save_project_as";
            this.menu_save_project_as.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // menu_settings
            // 
            this.menu_settings.Image = global::FriishProduce.Properties.Resources.wrench;
            this.menu_settings.Name = "menu_settings";
            resources.ApplyResources(this.menu_settings, "menu_settings");
            this.menu_settings.Tag = "settings";
            this.menu_settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // menu_exit
            // 
            resources.ApplyResources(this.menu_exit, "menu_exit");
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Tag = "exit";
            this.menu_exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menu_project
            // 
            this.menu_project.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_retrieve_gamedata_online,
            this.toolStripSeparator1,
            this.menu_export,
            this.toolStripSeparator2,
            this.menu_close_project});
            this.menu_project.Name = "menu_project";
            resources.ApplyResources(this.menu_project, "menu_project");
            this.menu_project.Tag = "project";
            // 
            // menu_retrieve_gamedata_online
            // 
            resources.ApplyResources(this.menu_retrieve_gamedata_online, "menu_retrieve_gamedata_online");
            this.menu_retrieve_gamedata_online.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.menu_retrieve_gamedata_online.Name = "menu_retrieve_gamedata_online";
            this.menu_retrieve_gamedata_online.Tag = "retrieve_gamedata_online";
            this.menu_retrieve_gamedata_online.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menu_export
            // 
            resources.ApplyResources(this.menu_export, "menu_export");
            this.menu_export.Image = global::FriishProduce.Properties.Resources.package_green;
            this.menu_export.Name = "menu_export";
            this.menu_export.Tag = "export";
            this.menu_export.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menu_close_project
            // 
            resources.ApplyResources(this.menu_close_project, "menu_close_project");
            this.menu_close_project.Image = global::FriishProduce.Properties.Resources.tab_delete;
            this.menu_close_project.Name = "menu_close_project";
            this.menu_close_project.Tag = "close_project";
            this.menu_close_project.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // menu_help
            // 
            this.menu_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_about_app});
            this.menu_help.Name = "menu_help";
            resources.ApplyResources(this.menu_help, "menu_help");
            this.menu_help.Tag = "help";
            // 
            // menu_about_app
            // 
            this.menu_about_app.Image = global::FriishProduce.Properties.Resources.mr_saturn;
            this.menu_about_app.Name = "menu_about_app";
            resources.ApplyResources(this.menu_about_app, "menu_about_app");
            this.menu_about_app.Tag = "about_app";
            this.menu_about_app.Click += new System.EventHandler(this.About_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.mainPanel.Controls.Add(this.Logo);
            resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Name = "mainPanel";
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.Transparent;
            this.Logo.Image = global::FriishProduce.Properties.Resources.logo;
            resources.ApplyResources(this.Logo, "Logo");
            this.Logo.Name = "Logo";
            this.Logo.TabStop = false;
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarNewProject,
            this.toolbarOpenProject,
            this.toolbarSaveAs,
            this.toolbarCloseProject,
            this.toolStripSeparator5,
            this.toolbarRetrieveGameData,
            this.toolStripSeparator6,
            this.ToolStrip_Settings,
            this.toolbarExport});
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolbarNewProject
            // 
            this.toolbarNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarNewProject.Image = global::FriishProduce.Properties.Resources.page_white;
            resources.ApplyResources(this.toolbarNewProject, "toolbarNewProject");
            this.toolbarNewProject.Name = "toolbarNewProject";
            this.toolbarNewProject.Tag = "new_project";
            // 
            // toolbarOpenProject
            // 
            this.toolbarOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarOpenProject.Image = global::FriishProduce.Properties.Resources.folder_page;
            resources.ApplyResources(this.toolbarOpenProject, "toolbarOpenProject");
            this.toolbarOpenProject.Name = "toolbarOpenProject";
            this.toolbarOpenProject.Tag = "open_project";
            this.toolbarOpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // toolbarSaveAs
            // 
            this.toolbarSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarSaveAs, "toolbarSaveAs");
            this.toolbarSaveAs.Image = global::FriishProduce.Properties.Resources.drive_disk;
            this.toolbarSaveAs.Name = "toolbarSaveAs";
            this.toolbarSaveAs.Tag = "save_project_as";
            this.toolbarSaveAs.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // toolbarCloseProject
            // 
            this.toolbarCloseProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarCloseProject, "toolbarCloseProject");
            this.toolbarCloseProject.Image = global::FriishProduce.Properties.Resources.tab_delete;
            this.toolbarCloseProject.Name = "toolbarCloseProject";
            this.toolbarCloseProject.Tag = "close_project";
            this.toolbarCloseProject.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolbarRetrieveGameData
            // 
            this.toolbarRetrieveGameData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarRetrieveGameData, "toolbarRetrieveGameData");
            this.toolbarRetrieveGameData.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.toolbarRetrieveGameData.Name = "toolbarRetrieveGameData";
            this.toolbarRetrieveGameData.Tag = "retrieve_gamedata_online";
            this.toolbarRetrieveGameData.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // ToolStrip_Settings
            // 
            this.ToolStrip_Settings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStrip_Settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStrip_Settings.Image = global::FriishProduce.Properties.Resources.wrench;
            resources.ApplyResources(this.ToolStrip_Settings, "ToolStrip_Settings");
            this.ToolStrip_Settings.Name = "ToolStrip_Settings";
            this.ToolStrip_Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // toolbarExport
            // 
            this.toolbarExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarExport, "toolbarExport");
            this.toolbarExport.Image = global::FriishProduce.Properties.Resources.package_green;
            this.toolbarExport.Name = "toolbarExport";
            this.toolbarExport.Tag = "export";
            this.toolbarExport.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // SaveProject
            // 
            this.SaveProject.DefaultExt = "fppj";
            this.SaveProject.RestoreDirectory = true;
            this.SaveProject.SupportMultiDottedExtensions = true;
            // 
            // BrowseProject
            // 
            this.BrowseProject.DefaultExt = "fppj";
            // 
            // tabControl
            // 
            this.tabControl.BackLowColor = System.Drawing.Color.White;
            this.tabControl.BorderColor = System.Drawing.Color.LightGray;
            this.tabControl.BorderColorDisabled = System.Drawing.Color.Silver;
            this.tabControl.FontBoldOnSelect = false;
            this.tabControl.ForeColorDisabled = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.MenuRenderer = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tabControl.TabBackHighColor = System.Drawing.Color.White;
            this.tabControl.TabBackHighColorDisabled = System.Drawing.Color.WhiteSmoke;
            this.tabControl.TabBackLowColorDisabled = System.Drawing.Color.Gainsboro;
            this.tabControl.TabBorderEnhanced = true;
            this.tabControl.TabCloseButtonBackHighColor = System.Drawing.Color.White;
            this.tabControl.TabCloseButtonBackHighColorDisabled = System.Drawing.Color.WhiteSmoke;
            this.tabControl.TabCloseButtonBackHighColorHot = System.Drawing.Color.White;
            this.tabControl.TabCloseButtonBackLowColor = System.Drawing.Color.LightGray;
            this.tabControl.TabCloseButtonBackLowColorDisabled = System.Drawing.Color.Gainsboro;
            this.tabControl.TabCloseButtonBackLowColorHot = System.Drawing.Color.Silver;
            this.tabControl.TabCloseButtonBorderColor = System.Drawing.Color.LightGray;
            this.tabControl.TabCloseButtonBorderColorDisabled = System.Drawing.Color.Silver;
            this.tabControl.TabCloseButtonBorderColorHot = System.Drawing.Color.DarkGray;
            this.tabControl.TabCloseButtonForeColor = System.Drawing.Color.Black;
            this.tabControl.TabCloseButtonForeColorHot = System.Drawing.Color.Black;
            this.tabControl.TabCloseButtonImage = null;
            this.tabControl.TabCloseButtonImageDisabled = null;
            this.tabControl.TabCloseButtonImageHot = null;
            this.tabControl.TabCloseButtonSize = new System.Drawing.Size(16, 16);
            this.tabControl.TabHeight = 24;
            this.tabControl.TabMaximumWidth = 300;
            this.tabControl.TabMinimumWidth = 175;
            this.tabControl.TabOffset = 2;
            this.tabControl.TabTop = 2;
            this.tabControl.TopSeparator = false;
            this.tabControl.SelectedTabChanged += new System.EventHandler(this.TabChanged);
            this.tabControl.TabIndexChanged += new System.EventHandler(this.TabChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "mainform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_new_project;
        private System.Windows.Forms.ToolStripMenuItem menu_settings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem menu_project;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_close_project;
        private System.Windows.Forms.ToolStripMenuItem menu_help;
        private System.Windows.Forms.ToolStripMenuItem menu_about_app;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolbarCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton ToolStrip_Settings;
        private System.Windows.Forms.ToolStripDropDownButton toolbarNewProject;
        private System.Windows.Forms.OpenFileDialog BrowseProject;
        private System.Windows.Forms.ToolStripMenuItem menu_open_project;
        private System.Windows.Forms.ToolStripButton toolbarOpenProject;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.SaveFileDialog SaveProject;
        internal System.Windows.Forms.ToolStripButton toolbarExport;
        internal System.Windows.Forms.ToolStripButton toolbarSaveAs;
        internal System.Windows.Forms.ToolStripMenuItem menu_save_project_as;
        internal System.Windows.Forms.ToolStripMenuItem menu_export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private MdiTabControl.TabControl tabControl;
        internal System.Windows.Forms.ToolStripButton toolbarRetrieveGameData;
        internal System.Windows.Forms.ToolStripMenuItem menu_retrieve_gamedata_online;
    }
}

