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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SaveWAD = new System.Windows.Forms.SaveFileDialog();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolbarNewProject = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolbarOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolbarSave = new System.Windows.Forms.ToolStripButton();
            this.toolbarSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolbarCloseProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarRetrieveGameData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarSettings = new System.Windows.Forms.ToolStripButton();
            this.toolbarExport = new System.Windows.Forms.ToolStripButton();
            this.SaveProject = new System.Windows.Forms.SaveFileDialog();
            this.BrowseProject = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new MdiTabControl.TabControl();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.new_project = new System.Windows.Forms.MenuItem();
            this.open_project = new System.Windows.Forms.MenuItem();
            this.save_project = new System.Windows.Forms.MenuItem();
            this.save_project_as = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.settings = new System.Windows.Forms.MenuItem();
            this.exit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.retrieve_gamedata_online = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.export = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.close_project = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.about = new System.Windows.Forms.MenuItem();
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
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(229)))));
            this.mainPanel.Controls.Add(this.Logo);
            resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Name = "mainPanel";
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.Transparent;
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
            this.toolbarSave,
            this.toolbarSaveAs,
            this.toolbarCloseProject,
            this.toolStripSeparator5,
            this.toolbarRetrieveGameData,
            this.toolStripSeparator6,
            this.toolbarSettings,
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
            // toolbarSave
            // 
            this.toolbarSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarSave, "toolbarSave");
            this.toolbarSave.Image = global::FriishProduce.Properties.Resources.disk;
            this.toolbarSave.Name = "toolbarSave";
            this.toolbarSave.Tag = "save_project";
            this.toolbarSave.Click += new System.EventHandler(this.Save_Click);
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
            // toolbarSettings
            // 
            this.toolbarSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarSettings.Image = global::FriishProduce.Properties.Resources.cog;
            resources.ApplyResources(this.toolbarSettings, "toolbarSettings");
            this.toolbarSettings.Name = "toolbarSettings";
            this.toolbarSettings.Click += new System.EventHandler(this.Settings_Click);
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
            this.BrowseProject.SupportMultiDottedExtensions = true;
            // 
            // tabControl
            // 
            this.tabControl.BackHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(213)))), ((int)(((byte)(216)))));
            this.tabControl.BackLowColor = System.Drawing.Color.WhiteSmoke;
            this.tabControl.BorderColor = System.Drawing.Color.LightGray;
            this.tabControl.BorderColorDisabled = System.Drawing.Color.Silver;
            this.tabControl.ControlButtonForeColor = System.Drawing.Color.Black;
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
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.new_project,
            this.open_project,
            this.save_project,
            this.save_project_as,
            this.menuItem8,
            this.settings,
            this.exit});
            this.menuItem1.Tag = "file";
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // new_project
            // 
            this.new_project.Index = 0;
            resources.ApplyResources(this.new_project, "new_project");
            this.new_project.Tag = "new_project";
            // 
            // open_project
            // 
            this.open_project.Index = 1;
            resources.ApplyResources(this.open_project, "open_project");
            this.open_project.Tag = "open_project";
            this.open_project.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // save_project
            // 
            resources.ApplyResources(this.save_project, "save_project");
            this.save_project.Index = 2;
            this.save_project.Tag = "save_project";
            this.save_project.Click += new System.EventHandler(this.Save_Click);
            // 
            // save_project_as
            // 
            resources.ApplyResources(this.save_project_as, "save_project_as");
            this.save_project_as.Index = 3;
            this.save_project_as.Tag = "save_project_as";
            this.save_project_as.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 4;
            resources.ApplyResources(this.menuItem8, "menuItem8");
            // 
            // settings
            // 
            this.settings.Index = 5;
            resources.ApplyResources(this.settings, "settings");
            this.settings.Tag = "";
            this.settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // exit
            // 
            this.exit.Index = 6;
            resources.ApplyResources(this.exit, "exit");
            this.exit.Tag = "exit";
            this.exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.retrieve_gamedata_online,
            this.menuItem14,
            this.export,
            this.menuItem13,
            this.close_project});
            this.menuItem2.Tag = "project";
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // retrieve_gamedata_online
            // 
            resources.ApplyResources(this.retrieve_gamedata_online, "retrieve_gamedata_online");
            this.retrieve_gamedata_online.Index = 0;
            this.retrieve_gamedata_online.Tag = "retrieve_gamedata_online";
            this.retrieve_gamedata_online.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 1;
            resources.ApplyResources(this.menuItem14, "menuItem14");
            // 
            // export
            // 
            resources.ApplyResources(this.export, "export");
            this.export.Index = 2;
            this.export.Tag = "export";
            this.export.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 3;
            resources.ApplyResources(this.menuItem13, "menuItem13");
            // 
            // close_project
            // 
            resources.ApplyResources(this.close_project, "close_project");
            this.close_project.Index = 4;
            this.close_project.Tag = "close_project";
            this.close_project.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.about});
            this.menuItem3.Tag = "help";
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // about
            // 
            this.about.Index = 0;
            this.about.Tag = "about_app";
            resources.ApplyResources(this.about, "about");
            this.about.Click += new System.EventHandler(this.About_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "mainform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolbarCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolbarSettings;
        private System.Windows.Forms.OpenFileDialog BrowseProject;
        private System.Windows.Forms.ToolStripButton toolbarOpenProject;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.SaveFileDialog SaveProject;
        internal System.Windows.Forms.ToolStripButton toolbarExport;
        internal System.Windows.Forms.ToolStripButton toolbarSaveAs;
        private MdiTabControl.TabControl tabControl;
        internal System.Windows.Forms.ToolStripButton toolbarRetrieveGameData;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem new_project;
        private System.Windows.Forms.MenuItem open_project;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem settings;
        private System.Windows.Forms.MenuItem exit;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem close_project;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem about;
        internal System.Windows.Forms.MenuItem retrieve_gamedata_online;
        internal System.Windows.Forms.MenuItem export;
        private System.Windows.Forms.ToolStripDropDownButton toolbarNewProject;
        internal System.Windows.Forms.ToolStripButton toolbarSave;
        internal System.Windows.Forms.MenuItem save_project_as;
        internal System.Windows.Forms.MenuItem save_project;
    }
}

