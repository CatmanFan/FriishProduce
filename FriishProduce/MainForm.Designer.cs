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
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.open_project = new System.Windows.Forms.MenuItem();
            this.open_recent = new System.Windows.Forms.MenuItem();
            this.save_project = new System.Windows.Forms.MenuItem();
            this.save_project_as = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.close_project = new System.Windows.Forms.MenuItem();
            this.close_all = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.exit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.import_game_file = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.game_scan = new System.Windows.Forms.MenuItem();
            this.export = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.preferences = new System.Windows.Forms.MenuItem();
            this.updater = new System.Windows.Forms.MenuItem();
            this.check_for_updates = new System.Windows.Forms.MenuItem();
            this.auto_update = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.extract_from_wad = new System.Windows.Forms.MenuItem();
            this.extract_wad_banner = new System.Windows.Forms.MenuItem();
            this.extract_wad_icon = new System.Windows.Forms.MenuItem();
            this.extract_wad_sound = new System.Windows.Forms.MenuItem();
            this.extract_wad_manual = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.advanced = new System.Windows.Forms.MenuItem();
            this.language_file_editor = new System.Windows.Forms.MenuItem();
            this.test_database = new System.Windows.Forms.MenuItem();
            this.clear_database = new System.Windows.Forms.MenuItem();
            this.reset_preferences = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.wiki = new System.Windows.Forms.MenuItem();
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
            this.toolStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStrip_Paint);
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
            this.toolbarGameScan.AutoToolTip = false;
            this.toolbarGameScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolbarGameScan, "toolbarGameScan");
            this.toolbarGameScan.Image = global::FriishProduce.Properties.Resources.retroarch;
            this.toolbarGameScan.Name = "toolbarGameScan";
            this.toolbarGameScan.Tag = "game_scan";
            this.toolbarGameScan.Click += new System.EventHandler(this.GameScan_Click);
            // 
            // toolbarExport
            // 
            this.toolbarExport.AutoToolTip = false;
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
            this.menuItem5,
            this.open_project,
            this.open_recent,
            this.save_project,
            this.save_project_as,
            this.menuItem6,
            this.close_project,
            this.close_all,
            this.menuItem7,
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
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // open_project
            // 
            this.open_project.Index = 2;
            resources.ApplyResources(this.open_project, "open_project");
            this.open_project.Tag = "open_project";
            this.open_project.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // open_recent
            // 
            this.open_recent.Index = 3;
            this.open_recent.Tag = "open_recent";
            resources.ApplyResources(this.open_recent, "open_recent");
            // 
            // save_project
            // 
            resources.ApplyResources(this.save_project, "save_project");
            this.save_project.Index = 4;
            this.save_project.Tag = "save_project";
            this.save_project.Click += new System.EventHandler(this.Save_Click);
            // 
            // save_project_as
            // 
            resources.ApplyResources(this.save_project_as, "save_project_as");
            this.save_project_as.Index = 5;
            this.save_project_as.Tag = "save_project_as";
            this.save_project_as.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 6;
            resources.ApplyResources(this.menuItem6, "menuItem6");
            // 
            // close_project
            // 
            resources.ApplyResources(this.close_project, "close_project");
            this.close_project.Index = 7;
            this.close_project.Tag = "close_project";
            this.close_project.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // close_all
            // 
            resources.ApplyResources(this.close_all, "close_all");
            this.close_all.Index = 8;
            this.close_all.Tag = "close_all";
            this.close_all.Click += new System.EventHandler(this.CloseAll_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 9;
            resources.ApplyResources(this.menuItem7, "menuItem7");
            // 
            // exit
            // 
            this.exit.Index = 10;
            resources.ApplyResources(this.exit, "exit");
            this.exit.Tag = "exit";
            this.exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.import_game_file,
            this.menuItem8,
            this.game_scan,
            this.export});
            this.menuItem2.Tag = "project";
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // import_game_file
            // 
            resources.ApplyResources(this.import_game_file, "import_game_file");
            this.import_game_file.Index = 0;
            this.import_game_file.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            resources.ApplyResources(this.menuItem8, "menuItem8");
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
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.preferences,
            this.updater,
            this.menuItem9,
            this.extract_from_wad,
            this.menuItem10,
            this.advanced});
            this.menuItem3.Tag = "tools";
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // preferences
            // 
            this.preferences.Index = 0;
            resources.ApplyResources(this.preferences, "preferences");
            this.preferences.Tag = "";
            this.preferences.Click += new System.EventHandler(this.Settings_Click);
            // 
            // updater
            // 
            this.updater.Index = 1;
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
            // menuItem9
            // 
            this.menuItem9.Index = 2;
            resources.ApplyResources(this.menuItem9, "menuItem9");
            // 
            // extract_from_wad
            // 
            this.extract_from_wad.Index = 3;
            this.extract_from_wad.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.extract_wad_banner,
            this.extract_wad_icon,
            this.extract_wad_sound,
            this.extract_wad_manual});
            this.extract_from_wad.Tag = "extract_from_wad";
            resources.ApplyResources(this.extract_from_wad, "extract_from_wad");
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
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            resources.ApplyResources(this.menuItem10, "menuItem10");
            // 
            // advanced
            // 
            this.advanced.Index = 5;
            this.advanced.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.language_file_editor,
            this.test_database,
            this.clear_database,
            this.reset_preferences});
            this.advanced.Tag = "advanced";
            resources.ApplyResources(this.advanced, "advanced");
            // 
            // language_file_editor
            // 
            this.language_file_editor.Index = 0;
            resources.ApplyResources(this.language_file_editor, "language_file_editor");
            this.language_file_editor.Click += new System.EventHandler(this.LanguageFileEditor);
            // 
            // test_database
            // 
            this.test_database.Index = 1;
            this.test_database.Tag = "test_database";
            resources.ApplyResources(this.test_database, "test_database");
            this.test_database.Click += new System.EventHandler(this.TestDatabase);
            // 
            // clear_database
            // 
            this.clear_database.Index = 2;
            this.clear_database.Tag = "clear_database";
            resources.ApplyResources(this.clear_database, "clear_database");
            this.clear_database.Click += new System.EventHandler(this.ClearAllDatabases);
            // 
            // reset_preferences
            // 
            this.reset_preferences.Index = 3;
            this.reset_preferences.Tag = "reset_preferences";
            resources.ApplyResources(this.reset_preferences, "reset_preferences");
            this.reset_preferences.Click += new System.EventHandler(this.ResetConfig);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.wiki,
            this.about});
            this.menuItem4.Tag = "help";
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // wiki
            // 
            this.wiki.Index = 0;
            resources.ApplyResources(this.wiki, "wiki");
            this.wiki.Click += new System.EventHandler(this.Website_Click);
            // 
            // about
            // 
            this.about.Index = 1;
            this.about.Tag = "about_app";
            resources.ApplyResources(this.about, "about");
            this.about.Click += new System.EventHandler(this.About_Click);
            // 
            // tabControl
            // 
            this.tabControl.BackHighColor = System.Drawing.Color.WhiteSmoke;
            this.tabControl.BackLowColor = System.Drawing.SystemColors.Control;
            this.tabControl.CloseButtonVisible = true;
            this.tabControl.FontBoldOnSelect = false;
            this.tabControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabControl.ForeColorDisabled = System.Drawing.Color.Gray;
            this.tabControl.KeyCloseEnabled = false;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.MenuRenderer = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tabControl.TabBackHighColor = System.Drawing.SystemColors.Control;
            this.tabControl.TabBackHighColorDisabled = System.Drawing.Color.LightGray;
            this.tabControl.TabBackLowColor = System.Drawing.SystemColors.Window;
            this.tabControl.TabBackLowColorDisabled = System.Drawing.Color.Gainsboro;
            this.tabControl.TabBorderEnhanced = true;
            this.tabControl.TabBorderEnhanceWeight = JacksiroKe.MdiTabCtrl.TabControl.Weight.Soft;
            this.tabControl.TabCloseButtonImage = null;
            this.tabControl.TabCloseButtonImageDisabled = null;
            this.tabControl.TabCloseButtonImageHot = null;
            this.tabControl.TabCloseButtonVisible = false;
            this.tabControl.TabGlassGradient = true;
            this.tabControl.TabHeight = 24;
            this.tabControl.TabMaximumWidth = 250;
            this.tabControl.TabMinimumWidth = 75;
            this.tabControl.TopSeparator = false;
            this.tabControl.TabPaintBackground += new JacksiroKe.MdiTabCtrl.TabControl.TabPaintBackgroundEventHandler(this.TabControl_Paint);
            this.tabControl.TabPaintBorder += new JacksiroKe.MdiTabCtrl.TabControl.TabPaintBorderEventHandler(this.TabControl_Paint);
            this.tabControl.SelectedTabChanged += new System.EventHandler(this.TabChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
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
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
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
        private System.Windows.Forms.MenuItem wiki;
        private System.Windows.Forms.MenuItem reset_preferences;
        private System.Windows.Forms.MenuItem open_recent;
        private System.Windows.Forms.MenuItem close_all;
        private System.Windows.Forms.MenuItem test_database;
        private System.Windows.Forms.MenuItem advanced;
        private System.Windows.Forms.MenuItem extract_from_wad;
        private System.Windows.Forms.MenuItem menuItem10;
    }
}

