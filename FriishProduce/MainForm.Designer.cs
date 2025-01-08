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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarImportGameFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarGameScan = new System.Windows.Forms.ToolStripButton();
            this.toolbarExport = new System.Windows.Forms.ToolStripButton();
            this.toolbarPreferences = new System.Windows.Forms.ToolStripButton();
            this.SaveProject = new System.Windows.Forms.SaveFileDialog();
            this.BrowseProject = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.new_project = new System.Windows.Forms.MenuItem();
            this.open_project = new System.Windows.Forms.MenuItem();
            this.save_project = new System.Windows.Forms.MenuItem();
            this.save_project_as = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.exit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.import_game_file = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.game_scan = new System.Windows.Forms.MenuItem();
            this.export = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.close_project = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.extract_wad_banner = new System.Windows.Forms.MenuItem();
            this.extract_wad_icon = new System.Windows.Forms.MenuItem();
            this.extract_wad_sound = new System.Windows.Forms.MenuItem();
            this.extract_wad_manual = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.updater = new System.Windows.Forms.MenuItem();
            this.check_for_updates = new System.Windows.Forms.MenuItem();
            this.auto_update = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.clear_database = new System.Windows.Forms.MenuItem();
            this.language_file_editor = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.preferences = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.about = new System.Windows.Forms.MenuItem();
            this.tabControl = new JacksiroKe.MdiTabCtrl.TabControl();
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
            this.toolbarSave,
            this.toolbarSaveAs,
            this.toolbarCloseProject,
            this.toolStripSeparator1,
            this.toolbarImportGameFile,
            this.toolStripSeparator2,
            this.toolbarGameScan,
            this.toolbarExport,
            this.toolbarPreferences});
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolbarNewProject
            // 
            this.toolbarNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarNewProject, "toolbarNewProject");
            this.toolbarNewProject.Name = "toolbarNewProject";
            this.toolbarNewProject.Tag = "new_project";
            // 
            // toolbarOpenProject
            // 
            this.toolbarOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarOpenProject, "toolbarOpenProject");
            this.toolbarOpenProject.Name = "toolbarOpenProject";
            this.toolbarOpenProject.Tag = "open_project";
            this.toolbarOpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // toolbarSave
            // 
            this.toolbarSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarSave, "toolbarSave");
            this.toolbarSave.Name = "toolbarSave";
            this.toolbarSave.Tag = "save_project";
            this.toolbarSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolbarSaveAs
            // 
            this.toolbarSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarSaveAs, "toolbarSaveAs");
            this.toolbarSaveAs.Name = "toolbarSaveAs";
            this.toolbarSaveAs.Tag = "save_project_as";
            this.toolbarSaveAs.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // toolbarCloseProject
            // 
            this.toolbarCloseProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarCloseProject, "toolbarCloseProject");
            this.toolbarCloseProject.Name = "toolbarCloseProject";
            this.toolbarCloseProject.Tag = "close_project";
            this.toolbarCloseProject.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolbarImportGameFile
            // 
            this.toolbarImportGameFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarImportGameFile, "toolbarImportGameFile");
            this.toolbarImportGameFile.Image = global::FriishProduce.Properties.Resources.page_white_cd;
            this.toolbarImportGameFile.Name = "toolbarImportGameFile";
            this.toolbarImportGameFile.Tag = "import_game_file";
            this.toolbarImportGameFile.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolbarGameScan
            // 
            this.toolbarGameScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarGameScan, "toolbarGameScan");
            this.toolbarGameScan.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.toolbarGameScan.Name = "toolbarGameScan";
            this.toolbarGameScan.Tag = "game_scan";
            this.toolbarGameScan.Click += new System.EventHandler(this.GameScan_Click);
            // 
            // toolbarExport
            // 
            this.toolbarExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarExport, "toolbarExport");
            this.toolbarExport.Name = "toolbarExport";
            this.toolbarExport.Tag = "export";
            this.toolbarExport.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // toolbarPreferences
            // 
            this.toolbarPreferences.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarPreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarPreferences, "toolbarPreferences");
            this.toolbarPreferences.Name = "toolbarPreferences";
            this.toolbarPreferences.Click += new System.EventHandler(this.Settings_Click);
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
            this.BrowseProject.Multiselect = true;
            this.BrowseProject.SupportMultiDottedExtensions = true;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.new_project,
            this.open_project,
            this.save_project,
            this.save_project_as,
            this.menuItem5,
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
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // exit
            // 
            this.exit.Index = 5;
            resources.ApplyResources(this.exit, "exit");
            this.exit.Tag = "exit";
            this.exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.import_game_file,
            this.menuItem6,
            this.game_scan,
            this.export,
            this.menuItem7,
            this.close_project});
            this.menuItem2.Tag = "project";
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // import_game_file
            // 
            resources.ApplyResources(this.import_game_file, "import_game_file");
            this.import_game_file.Index = 0;
            this.import_game_file.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            resources.ApplyResources(this.menuItem6, "menuItem6");
            // 
            // game_scan
            // 
            resources.ApplyResources(this.game_scan, "game_scan");
            this.game_scan.Index = 2;
            this.game_scan.Tag = "game_scan";
            this.game_scan.Click += new System.EventHandler(this.GameScan_Click);
            // 
            // export
            // 
            resources.ApplyResources(this.export, "export");
            this.export.Index = 3;
            this.export.Tag = "export";
            this.export.Click += new System.EventHandler(this.ExportWAD_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            resources.ApplyResources(this.menuItem7, "menuItem7");
            // 
            // close_project
            // 
            resources.ApplyResources(this.close_project, "close_project");
            this.close_project.Index = 5;
            this.close_project.Tag = "close_project";
            this.close_project.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.extract_wad_banner,
            this.extract_wad_icon,
            this.extract_wad_sound,
            this.extract_wad_manual,
            this.menuItem9,
            this.updater,
            this.menuItem8,
            this.clear_database,
            this.language_file_editor,
            this.menuItem11,
            this.preferences});
            this.menuItem3.Tag = "tools";
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // extract_wad_banner
            // 
            this.extract_wad_banner.Index = 0;
            this.extract_wad_banner.Tag = "extract_wad_banner";
            resources.ApplyResources(this.extract_wad_banner, "extract_wad_banner");
            this.extract_wad_banner.Click += new System.EventHandler(this.ExtractWAD_Click);
            // 
            // extract_wad_icon
            // 
            this.extract_wad_icon.Index = 1;
            this.extract_wad_icon.Tag = "extract_wad_icon";
            resources.ApplyResources(this.extract_wad_icon, "extract_wad_icon");
            this.extract_wad_icon.Click += new System.EventHandler(this.ExtractWAD_Click);
            // 
            // extract_wad_sound
            // 
            this.extract_wad_sound.Index = 2;
            this.extract_wad_sound.Tag = "extract_wad_sound";
            resources.ApplyResources(this.extract_wad_sound, "extract_wad_sound");
            this.extract_wad_sound.Click += new System.EventHandler(this.ExtractWAD_Click);
            // 
            // extract_wad_manual
            // 
            this.extract_wad_manual.Index = 3;
            this.extract_wad_manual.Tag = "extract_wad_manual";
            resources.ApplyResources(this.extract_wad_manual, "extract_wad_manual");
            this.extract_wad_manual.Click += new System.EventHandler(this.ExtractWAD_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 4;
            resources.ApplyResources(this.menuItem9, "menuItem9");
            // 
            // updater
            // 
            this.updater.Index = 5;
            this.updater.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.check_for_updates,
            this.auto_update});
            this.updater.Tag = "updater";
            resources.ApplyResources(this.updater, "updater");
            // 
            // check_for_updates
            // 
            this.check_for_updates.Index = 0;
            this.check_for_updates.Tag = "check_for_updates";
            resources.ApplyResources(this.check_for_updates, "check_for_updates");
            this.check_for_updates.Click += new System.EventHandler(this.Update_Click);
            // 
            // auto_update
            // 
            this.auto_update.Index = 1;
            this.auto_update.Tag = "auto_update";
            resources.ApplyResources(this.auto_update, "auto_update");
            this.auto_update.Click += new System.EventHandler(this.Update_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 6;
            resources.ApplyResources(this.menuItem8, "menuItem8");
            // 
            // clear_database
            // 
            this.clear_database.Index = 7;
            this.clear_database.Tag = "clear_database";
            resources.ApplyResources(this.clear_database, "clear_database");
            this.clear_database.Click += new System.EventHandler(this.ClearAllDatabases);
            // 
            // language_file_editor
            // 
            this.language_file_editor.Index = 8;
            resources.ApplyResources(this.language_file_editor, "language_file_editor");
            this.language_file_editor.Click += new System.EventHandler(this.LanguageFileEditor);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 9;
            resources.ApplyResources(this.menuItem11, "menuItem11");
            // 
            // preferences
            // 
            this.preferences.Index = 10;
            resources.ApplyResources(this.preferences, "preferences");
            this.preferences.Tag = "";
            this.preferences.Click += new System.EventHandler(this.Settings_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.about});
            this.menuItem4.Tag = "help";
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // about
            // 
            this.about.Index = 0;
            this.about.Tag = "about_app";
            resources.ApplyResources(this.about, "about");
            this.about.Click += new System.EventHandler(this.About_Click);
            // 
            // tabControl
            // 
            this.tabControl.BackHighColor = System.Drawing.SystemColors.ControlLight;
            this.tabControl.BackLowColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.FontBoldOnSelect = false;
            this.tabControl.MenuRenderer = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tabControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tabControl.TabBackHighColor = System.Drawing.SystemColors.Control;
            this.tabControl.TabBackHighColorDisabled = System.Drawing.Color.LightGray;
            this.tabControl.TabBackLowColor = System.Drawing.SystemColors.Window;
            this.tabControl.TabBackLowColorDisabled = System.Drawing.Color.Gainsboro;
            this.tabControl.TabCloseButtonImage = null;
            this.tabControl.TabCloseButtonImageDisabled = null;
            this.tabControl.TabCloseButtonImageHot = null;
            this.tabControl.TabGlassGradient = true;
            this.tabControl.TabHeight = 25;
            this.tabControl.TopSeparator = false;
            this.tabControl.SelectedTabChanged += new System.EventHandler(this.TabChanged);
            this.tabControl.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "mainform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Loading);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolbarCloseProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolbarPreferences;
        private System.Windows.Forms.OpenFileDialog BrowseProject;
        private System.Windows.Forms.ToolStripButton toolbarOpenProject;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.SaveFileDialog SaveProject;
        internal System.Windows.Forms.ToolStripButton toolbarExport;
        internal System.Windows.Forms.ToolStripButton toolbarSaveAs;
        internal System.Windows.Forms.ToolStripButton toolbarGameScan;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem new_project;
        private System.Windows.Forms.MenuItem open_project;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem preferences;
        private System.Windows.Forms.MenuItem exit;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem close_project;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem about;
        internal System.Windows.Forms.MenuItem game_scan;
        internal System.Windows.Forms.MenuItem export;
        private System.Windows.Forms.ToolStripDropDownButton toolbarNewProject;
        internal System.Windows.Forms.ToolStripButton toolbarSave;
        internal System.Windows.Forms.MenuItem save_project_as;
        internal System.Windows.Forms.MenuItem save_project;
        internal System.Windows.Forms.ToolStripButton toolbarImportGameFile;
        private System.Windows.Forms.MenuItem import_game_file;
        private System.Windows.Forms.MenuItem menuItem6;
        private JacksiroKe.MdiTabCtrl.TabControl tabControl;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem check_for_updates;
        private System.Windows.Forms.MenuItem auto_update;
        private System.Windows.Forms.MenuItem language_file_editor;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem updater;
        private System.Windows.Forms.MenuItem extract_wad_banner;
        private System.Windows.Forms.MenuItem extract_wad_icon;
        private System.Windows.Forms.MenuItem extract_wad_sound;
        private System.Windows.Forms.MenuItem extract_wad_manual;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem clear_database;
        private System.Windows.Forms.MenuItem menuItem11;
    }
}

