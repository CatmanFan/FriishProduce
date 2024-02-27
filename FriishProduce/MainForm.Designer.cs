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
            this.MenuItem_Settings = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Tutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.Welcome_DoNotShow = new System.Windows.Forms.Button();
            this.PointToTutorial = new System.Windows.Forms.Label();
            this.Welcome = new System.Windows.Forms.Label();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStrip_OpenROM = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_OpenImage = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_OpenManual = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_UseLibRetro = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_ExportWAD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStrip_CloseTab = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_Settings = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_Tutorial = new System.Windows.Forms.ToolStripButton();
            this.languageEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.MainPanel.SuspendLayout();
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
            this.tabControl.ForeColorDisabled = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.tabControl, "tabControl");
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
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.GripMargin = new System.Windows.Forms.Padding(2);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_File,
            this.MenuItem_Project,
            this.MenuItem_Help});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // MenuItem_File
            // 
            this.MenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProject,
            this.MenuItem_Settings,
            this.toolStripSeparator4,
            this.MenuItem_Exit});
            this.MenuItem_File.Name = "MenuItem_File";
            resources.ApplyResources(this.MenuItem_File, "MenuItem_File");
            // 
            // NewProject
            // 
            this.NewProject.Image = global::FriishProduce.Properties.Resources.document;
            this.NewProject.Name = "NewProject";
            resources.ApplyResources(this.NewProject, "NewProject");
            // 
            // MenuItem_Settings
            // 
            this.MenuItem_Settings.Image = global::FriishProduce.Properties.Resources.wrench;
            this.MenuItem_Settings.Name = "MenuItem_Settings";
            resources.ApplyResources(this.MenuItem_Settings, "MenuItem_Settings");
            this.MenuItem_Settings.Click += new System.EventHandler(this.Settings_Click);
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
            // MenuItem_Help
            // 
            this.MenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Tutorial,
            this.languageEditorToolStripMenuItem,
            this.toolStripSeparator5,
            this.MenuItem_About});
            this.MenuItem_Help.Name = "MenuItem_Help";
            resources.ApplyResources(this.MenuItem_Help, "MenuItem_Help");
            // 
            // MenuItem_Tutorial
            // 
            resources.ApplyResources(this.MenuItem_Tutorial, "MenuItem_Tutorial");
            this.MenuItem_Tutorial.Name = "MenuItem_Tutorial";
            this.MenuItem_Tutorial.Click += new System.EventHandler(this.Tutorial_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // MenuItem_About
            // 
            this.MenuItem_About.Image = global::FriishProduce.Properties.Resources.mr_saturn;
            this.MenuItem_About.Name = "MenuItem_About";
            resources.ApplyResources(this.MenuItem_About, "MenuItem_About");
            this.MenuItem_About.Click += new System.EventHandler(this.About_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.MainPanel.Controls.Add(this.Welcome_DoNotShow);
            this.MainPanel.Controls.Add(this.PointToTutorial);
            this.MainPanel.Controls.Add(this.Welcome);
            resources.ApplyResources(this.MainPanel, "MainPanel");
            this.MainPanel.Name = "MainPanel";
            // 
            // Welcome_DoNotShow
            // 
            this.Welcome_DoNotShow.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.Welcome_DoNotShow, "Welcome_DoNotShow");
            this.Welcome_DoNotShow.Name = "Welcome_DoNotShow";
            this.Welcome_DoNotShow.UseVisualStyleBackColor = false;
            this.Welcome_DoNotShow.Click += new System.EventHandler(this.Welcome_DoNotShow_Click);
            // 
            // PointToTutorial
            // 
            resources.ApplyResources(this.PointToTutorial, "PointToTutorial");
            this.PointToTutorial.ForeColor = System.Drawing.Color.White;
            this.PointToTutorial.Name = "PointToTutorial";
            // 
            // Welcome
            // 
            this.Welcome.BackColor = System.Drawing.Color.White;
            this.Welcome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Welcome, "Welcome");
            this.Welcome.Name = "Welcome";
            // 
            // ToolStrip
            // 
            resources.ApplyResources(this.ToolStrip, "ToolStrip");
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_OpenROM,
            this.ToolStrip_OpenImage,
            this.ToolStrip_OpenManual,
            this.toolStripSeparator6,
            this.ToolStrip_UseLibRetro,
            this.toolStripSeparator7,
            this.ToolStrip_ExportWAD,
            this.toolStripSeparator8,
            this.ToolStrip_CloseTab,
            this.ToolStrip_Settings,
            this.ToolStrip_Tutorial});
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // ToolStrip_OpenROM
            // 
            this.ToolStrip_OpenROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_OpenROM, "ToolStrip_OpenROM");
            this.ToolStrip_OpenROM.Image = global::FriishProduce.Properties.Resources.disc_blue;
            this.ToolStrip_OpenROM.Name = "ToolStrip_OpenROM";
            this.ToolStrip_OpenROM.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // ToolStrip_OpenImage
            // 
            this.ToolStrip_OpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_OpenImage, "ToolStrip_OpenImage");
            this.ToolStrip_OpenImage.Image = global::FriishProduce.Properties.Resources.image_sunset;
            this.ToolStrip_OpenImage.Name = "ToolStrip_OpenImage";
            this.ToolStrip_OpenImage.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // ToolStrip_OpenManual
            // 
            this.ToolStrip_OpenManual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_OpenManual, "ToolStrip_OpenManual");
            this.ToolStrip_OpenManual.Image = global::FriishProduce.Properties.Resources.book;
            this.ToolStrip_OpenManual.Name = "ToolStrip_OpenManual";
            this.ToolStrip_OpenManual.Click += new System.EventHandler(this.OpenManual_Click);
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
            this.ToolStrip_UseLibRetro.Click += new System.EventHandler(this.UseLibRetro_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // ToolStrip_ExportWAD
            // 
            this.ToolStrip_ExportWAD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_ExportWAD, "ToolStrip_ExportWAD");
            this.ToolStrip_ExportWAD.Image = global::FriishProduce.Properties.Resources.box_label;
            this.ToolStrip_ExportWAD.Name = "ToolStrip_ExportWAD";
            this.ToolStrip_ExportWAD.Click += new System.EventHandler(this.ExportWAD_Click);
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
            // ToolStrip_Tutorial
            // 
            this.ToolStrip_Tutorial.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStrip_Tutorial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStrip_Tutorial, "ToolStrip_Tutorial");
            this.ToolStrip_Tutorial.Name = "ToolStrip_Tutorial";
            this.ToolStrip_Tutorial.Click += new System.EventHandler(this.Tutorial_Click);
            // 
            // languageEditorToolStripMenuItem
            // 
            this.languageEditorToolStripMenuItem.Name = "languageEditorToolStripMenuItem";
            resources.ApplyResources(this.languageEditorToolStripMenuItem, "languageEditorToolStripMenuItem");
            this.languageEditorToolStripMenuItem.Click += new System.EventHandler(this.languageEditorToolStripMenuItem_Click);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Settings;
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
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Tutorial;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_About;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label Welcome;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton ToolStrip_OpenROM;
        private System.Windows.Forms.ToolStripButton ToolStrip_OpenImage;
        private System.Windows.Forms.ToolStripButton ToolStrip_OpenManual;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton ToolStrip_UseLibRetro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton ToolStrip_ExportWAD;
        private System.Windows.Forms.ToolStripButton ToolStrip_CloseTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton ToolStrip_Settings;
        private System.Windows.Forms.ToolStripButton ToolStrip_Tutorial;
        private System.Windows.Forms.Label PointToTutorial;
        private System.Windows.Forms.Button Welcome_DoNotShow;
        private System.Windows.Forms.ToolStripMenuItem languageEditorToolStripMenuItem;
    }
}

