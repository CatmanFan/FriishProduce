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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_new_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_save_project_as = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_gamefile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_image = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_retrieve_gamedata_online = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_save_as_wad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_close_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about_app = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolbarNewProject = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolbarOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolbarSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolbarCloseProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarOpenGameFile = new System.Windows.Forms.ToolStripButton();
            this.toolbarOpenImage = new System.Windows.Forms.ToolStripButton();
            this.toolbarRetrieveGameData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolbarSaveAsWAD = new System.Windows.Forms.ToolStripButton();
            this.SaveProject = new System.Windows.Forms.SaveFileDialog();
            this.BrowseProject = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new MdiTabControl.TabControl();
            this.MenuStrip.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.ToolStrip.SuspendLayout();
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
            // MenuStrip
            // 
            this.MenuStrip.AllowMerge = false;
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MenuStrip.GripMargin = new System.Windows.Forms.Padding(2);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_project,
            this.menu_help});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
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
            this.menu_open_gamefile,
            this.menu_open_image,
            this.menu_retrieve_gamedata_online,
            this.toolStripSeparator1,
            this.menu_save_as_wad,
            this.toolStripSeparator2,
            this.menu_close_project});
            this.menu_project.Name = "menu_project";
            resources.ApplyResources(this.menu_project, "menu_project");
            this.menu_project.Tag = "project";
            // 
            // menu_open_gamefile
            // 
            resources.ApplyResources(this.menu_open_gamefile, "menu_open_gamefile");
            this.menu_open_gamefile.Image = global::FriishProduce.Properties.Resources.joystick_add;
            this.menu_open_gamefile.Name = "menu_open_gamefile";
            this.menu_open_gamefile.Tag = "open_gamefile";
            this.menu_open_gamefile.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // menu_open_image
            // 
            resources.ApplyResources(this.menu_open_image, "menu_open_image");
            this.menu_open_image.Image = global::FriishProduce.Properties.Resources.image_add;
            this.menu_open_image.Name = "menu_open_image";
            this.menu_open_image.Tag = "open_image";
            this.menu_open_image.Click += new System.EventHandler(this.OpenImage_Click);
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
            // menu_save_as_wad
            // 
            resources.ApplyResources(this.menu_save_as_wad, "menu_save_as_wad");
            this.menu_save_as_wad.Image = global::FriishProduce.Properties.Resources.package_green;
            this.menu_save_as_wad.Name = "menu_save_as_wad";
            this.menu_save_as_wad.Tag = "save_as_wad";
            this.menu_save_as_wad.Click += new System.EventHandler(this.ExportWAD_Click);
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
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.MainPanel.Controls.Add(this.Logo);
            resources.ApplyResources(this.MainPanel, "MainPanel");
            this.MainPanel.Name = "MainPanel";
            // 
            // Logo
            // 
            this.Logo.Image = global::FriishProduce.Properties.Resources.icon;
            resources.ApplyResources(this.Logo, "Logo");
            this.Logo.Name = "Logo";
            this.Logo.TabStop = false;
            // 
            // ToolStrip
            // 
            resources.ApplyResources(this.ToolStrip, "ToolStrip");
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarNewProject,
            this.toolbarOpenProject,
            this.toolbarSaveAs,
            this.toolbarCloseProject,
            this.toolStripSeparator5,
            this.toolbarOpenGameFile,
            this.toolbarOpenImage,
            this.toolbarRetrieveGameData,
            this.toolStripSeparator6,
            this.ToolStrip_Settings,
            this.toolbarSaveAsWAD});
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
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
            // toolbarOpenGameFile
            // 
            this.toolbarOpenGameFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarOpenGameFile, "toolbarOpenGameFile");
            this.toolbarOpenGameFile.Image = global::FriishProduce.Properties.Resources.joystick_add;
            this.toolbarOpenGameFile.Name = "toolbarOpenGameFile";
            this.toolbarOpenGameFile.Tag = "open_gamefile";
            this.toolbarOpenGameFile.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // toolbarOpenImage
            // 
            this.toolbarOpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarOpenImage, "toolbarOpenImage");
            this.toolbarOpenImage.Image = global::FriishProduce.Properties.Resources.image_add;
            this.toolbarOpenImage.Name = "toolbarOpenImage";
            this.toolbarOpenImage.Tag = "open_image";
            this.toolbarOpenImage.Click += new System.EventHandler(this.OpenImage_Click);
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
            // toolbarSaveAsWAD
            // 
            this.toolbarSaveAsWAD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarSaveAsWAD, "toolbarSaveAsWAD");
            this.toolbarSaveAsWAD.Image = global::FriishProduce.Properties.Resources.package_green;
            this.toolbarSaveAsWAD.Name = "toolbarSaveAsWAD";
            this.toolbarSaveAsWAD.Tag = "save_as_wad";
            this.toolbarSaveAsWAD.Click += new System.EventHandler(this.ExportWAD_Click);
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
            this.tabControl.TabHeight = 25;
            this.tabControl.TabMaximumWidth = 300;
            this.tabControl.TabMinimumWidth = 150;
            this.tabControl.TabOffset = 2;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "mainform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog BrowseImage;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_new_project;
        private System.Windows.Forms.ToolStripMenuItem menu_settings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem menu_project;
        private System.Windows.Forms.ToolStripMenuItem menu_open_gamefile;
        private System.Windows.Forms.ToolStripMenuItem menu_retrieve_gamedata_online;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_close_project;
        private System.Windows.Forms.ToolStripMenuItem menu_help;
        private System.Windows.Forms.ToolStripMenuItem menu_about_app;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton toolbarOpenGameFile;
        private System.Windows.Forms.ToolStripButton toolbarRetrieveGameData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolbarCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton ToolStrip_Settings;
        private System.Windows.Forms.ToolStripDropDownButton toolbarNewProject;
        private System.Windows.Forms.OpenFileDialog BrowseProject;
        private System.Windows.Forms.ToolStripMenuItem menu_open_project;
        private System.Windows.Forms.ToolStripButton toolbarOpenProject;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.OpenFileDialog BrowseROM;
        private System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.SaveFileDialog SaveProject;
        internal System.Windows.Forms.ToolStripButton toolbarSaveAsWAD;
        internal System.Windows.Forms.ToolStripButton toolbarSaveAs;
        internal System.Windows.Forms.ToolStripMenuItem menu_save_project_as;
        internal System.Windows.Forms.ToolStripMenuItem menu_save_as_wad;
        internal System.Windows.Forms.ToolStripMenuItem menu_open_image;
        internal System.Windows.Forms.ToolStripButton toolbarOpenImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private MdiTabControl.TabControl tabControl;
    }
}

