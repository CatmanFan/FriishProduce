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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_new_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_save_project_as = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_save_as_wad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_gamefile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_image = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_retrieve_gamedata_online = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_close_project = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about_app = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStrip_NewProject = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStrip_OpenProject = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_SaveAs = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_ExportWAD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_OpenROM = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_OpenImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_UseLibRetro = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_CloseTab = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_Settings = new System.Windows.Forms.ToolStripButton();
            this.SaveProject = new System.Windows.Forms.SaveFileDialog();
            this.BrowseProject = new System.Windows.Forms.OpenFileDialog();
            this.MenuStrip.SuspendLayout();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // tabControl
            // 
            this.tabControl.BackHighColor = System.Drawing.Color.WhiteSmoke;
            this.tabControl.BackLowColor = System.Drawing.Color.White;
            this.tabControl.CloseButtonVisible = true;
            this.tabControl.FontBoldOnSelect = false;
            this.tabControl.ForeColorDisabled = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.MenuRenderer = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
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
            this.menu_save_as_wad,
            this.toolStripMenuItem1,
            this.menu_settings,
            this.toolStripSeparator4,
            this.menu_exit});
            this.menu_file.Name = "menu_file";
            resources.ApplyResources(this.menu_file, "menu_file");
            this.menu_file.Tag = "file";
            // 
            // menu_new_project
            // 
            this.menu_new_project.Image = global::FriishProduce.Properties.Resources.document;
            this.menu_new_project.Name = "menu_new_project";
            resources.ApplyResources(this.menu_new_project, "menu_new_project");
            this.menu_new_project.Tag = "new_project";
            // 
            // menu_open_project
            // 
            this.menu_open_project.Image = global::FriishProduce.Properties.Resources.folder_horizontal_open;
            this.menu_open_project.Name = "menu_open_project";
            resources.ApplyResources(this.menu_open_project, "menu_open_project");
            this.menu_open_project.Tag = "open_project";
            this.menu_open_project.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // menu_save_project_as
            // 
            resources.ApplyResources(this.menu_save_project_as, "menu_save_project_as");
            this.menu_save_project_as.Image = global::FriishProduce.Properties.Resources.disk_black;
            this.menu_save_project_as.Name = "menu_save_project_as";
            this.menu_save_project_as.Tag = "save_project_as";
            this.menu_save_project_as.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // menu_save_as_wad
            // 
            resources.ApplyResources(this.menu_save_as_wad, "menu_save_as_wad");
            this.menu_save_as_wad.Image = global::FriishProduce.Properties.Resources.wooden_box_pencil;
            this.menu_save_as_wad.Name = "menu_save_as_wad";
            this.menu_save_as_wad.Tag = "save_as_wad";
            this.menu_save_as_wad.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
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
            this.menu_exit.Image = global::FriishProduce.Properties.Resources.door_open;
            this.menu_exit.Name = "menu_exit";
            resources.ApplyResources(this.menu_exit, "menu_exit");
            this.menu_exit.Tag = "exit";
            this.menu_exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menu_project
            // 
            this.menu_project.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_open_gamefile,
            this.menu_open_image,
            this.toolStripSeparator1,
            this.menu_retrieve_gamedata_online,
            this.toolStripSeparator2,
            this.menu_close_project});
            this.menu_project.Name = "menu_project";
            resources.ApplyResources(this.menu_project, "menu_project");
            this.menu_project.Tag = "project";
            // 
            // menu_open_gamefile
            // 
            resources.ApplyResources(this.menu_open_gamefile, "menu_open_gamefile");
            this.menu_open_gamefile.Image = global::FriishProduce.Properties.Resources.disc_case;
            this.menu_open_gamefile.Name = "menu_open_gamefile";
            this.menu_open_gamefile.Tag = "open_gamefile";
            this.menu_open_gamefile.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // menu_open_image
            // 
            resources.ApplyResources(this.menu_open_image, "menu_open_image");
            this.menu_open_image.Image = global::FriishProduce.Properties.Resources.image_sunset;
            this.menu_open_image.Name = "menu_open_image";
            this.menu_open_image.Tag = "open_image";
            this.menu_open_image.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menu_retrieve_gamedata_online
            // 
            resources.ApplyResources(this.menu_retrieve_gamedata_online, "menu_retrieve_gamedata_online");
            this.menu_retrieve_gamedata_online.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.menu_retrieve_gamedata_online.Name = "menu_retrieve_gamedata_online";
            this.menu_retrieve_gamedata_online.Tag = "retrieve_gamedata_online";
            this.menu_retrieve_gamedata_online.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menu_close_project
            // 
            resources.ApplyResources(this.menu_close_project, "menu_close_project");
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.MainPanel.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.MainPanel, "MainPanel");
            this.MainPanel.Name = "MainPanel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.icon;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // ToolStrip
            // 
            resources.ApplyResources(this.ToolStrip, "ToolStrip");
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_NewProject,
            this.ToolStrip_OpenProject,
            this.ToolStrip_SaveAs,
            this.ToolStrip_ExportWAD,
            this.toolStripSeparator7,
            this.ToolStrip_OpenROM,
            this.ToolStrip_OpenImage,
            this.toolStripSeparator6,
            this.ToolStrip_UseLibRetro,
            this.toolStripSeparator8,
            this.ToolStrip_CloseTab,
            this.ToolStrip_Settings});
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // ToolStrip_NewProject
            // 
            this.ToolStrip_NewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStrip_NewProject.Image = global::FriishProduce.Properties.Resources.document;
            resources.ApplyResources(this.ToolStrip_NewProject, "ToolStrip_NewProject");
            this.ToolStrip_NewProject.Name = "ToolStrip_NewProject";
            this.ToolStrip_NewProject.Tag = "new_project";
            // 
            // ToolStrip_OpenProject
            // 
            this.ToolStrip_OpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStrip_OpenProject.Image = global::FriishProduce.Properties.Resources.folder_horizontal_open;
            resources.ApplyResources(this.ToolStrip_OpenProject, "ToolStrip_OpenProject");
            this.ToolStrip_OpenProject.Name = "ToolStrip_OpenProject";
            this.ToolStrip_OpenProject.Tag = "open_project";
            this.ToolStrip_OpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // ToolStrip_SaveAs
            // 
            this.ToolStrip_SaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_SaveAs, "ToolStrip_SaveAs");
            this.ToolStrip_SaveAs.Image = global::FriishProduce.Properties.Resources.disk_black;
            this.ToolStrip_SaveAs.Name = "ToolStrip_SaveAs";
            this.ToolStrip_SaveAs.Tag = "save_project_as";
            this.ToolStrip_SaveAs.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // ToolStrip_ExportWAD
            // 
            this.ToolStrip_ExportWAD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_ExportWAD, "ToolStrip_ExportWAD");
            this.ToolStrip_ExportWAD.Image = global::FriishProduce.Properties.Resources.wooden_box_pencil;
            this.ToolStrip_ExportWAD.Name = "ToolStrip_ExportWAD";
            this.ToolStrip_ExportWAD.Tag = "save_as_wad";
            this.ToolStrip_ExportWAD.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // ToolStrip_OpenROM
            // 
            this.ToolStrip_OpenROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_OpenROM, "ToolStrip_OpenROM");
            this.ToolStrip_OpenROM.Image = global::FriishProduce.Properties.Resources.disc_case;
            this.ToolStrip_OpenROM.Name = "ToolStrip_OpenROM";
            this.ToolStrip_OpenROM.Tag = "open_gamefile";
            this.ToolStrip_OpenROM.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // ToolStrip_OpenImage
            // 
            this.ToolStrip_OpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_OpenImage, "ToolStrip_OpenImage");
            this.ToolStrip_OpenImage.Image = global::FriishProduce.Properties.Resources.image_sunset;
            this.ToolStrip_OpenImage.Name = "ToolStrip_OpenImage";
            this.ToolStrip_OpenImage.Tag = "open_image";
            this.ToolStrip_OpenImage.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // ToolStrip_UseLibRetro
            // 
            this.ToolStrip_UseLibRetro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_UseLibRetro, "ToolStrip_UseLibRetro");
            this.ToolStrip_UseLibRetro.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.ToolStrip_UseLibRetro.Name = "ToolStrip_UseLibRetro";
            this.ToolStrip_UseLibRetro.Tag = "retrieve_gamedata_online";
            this.ToolStrip_UseLibRetro.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // ToolStrip_CloseTab
            // 
            this.ToolStrip_CloseTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_CloseTab, "ToolStrip_CloseTab");
            this.ToolStrip_CloseTab.Image = global::FriishProduce.Properties.Resources.cross;
            this.ToolStrip_CloseTab.Name = "ToolStrip_CloseTab";
            this.ToolStrip_CloseTab.Tag = "close_project";
            this.ToolStrip_CloseTab.Click += new System.EventHandler(this.CloseTab_Click);
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
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Tag = "mainform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.OpenFileDialog BrowseROM;
        internal System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.OpenFileDialog BrowseImage;
        internal MdiTabControl.TabControl tabControl;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_new_project;
        private System.Windows.Forms.ToolStripMenuItem menu_settings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem menu_project;
        private System.Windows.Forms.ToolStripMenuItem menu_open_gamefile;
        private System.Windows.Forms.ToolStripMenuItem menu_open_image;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menu_retrieve_gamedata_online;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_save_as_wad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menu_close_project;
        private System.Windows.Forms.ToolStripMenuItem menu_help;
        private System.Windows.Forms.ToolStripMenuItem menu_about_app;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton ToolStrip_OpenROM;
        private System.Windows.Forms.ToolStripButton ToolStrip_OpenImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton ToolStrip_UseLibRetro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton ToolStrip_ExportWAD;
        private System.Windows.Forms.ToolStripButton ToolStrip_CloseTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton ToolStrip_Settings;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menu_save_project_as;
        private System.Windows.Forms.ToolStripDropDownButton ToolStrip_NewProject;
        internal System.Windows.Forms.SaveFileDialog SaveProject;
        private System.Windows.Forms.OpenFileDialog BrowseProject;
        private System.Windows.Forms.ToolStripMenuItem menu_open_project;
        private System.Windows.Forms.ToolStripButton ToolStrip_SaveAs;
        private System.Windows.Forms.ToolStripButton ToolStrip_OpenProject;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

