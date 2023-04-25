
namespace FriishProduce
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.Settings = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.Console = new System.Windows.Forms.ComboBox();
            this.page1 = new System.Windows.Forms.Panel();
            this.page2 = new System.Windows.Forms.Panel();
            this.Patch = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DeleteBase = new System.Windows.Forms.Button();
            this.AddBase = new System.Windows.Forms.Button();
            this.baseList = new System.Windows.Forms.ComboBox();
            this.OpenROM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseROM = new System.Windows.Forms.OpenFileDialog();
            this.BrowseWAD = new System.Windows.Forms.OpenFileDialog();
            this.SaveWAD = new System.Windows.Forms.SaveFileDialog();
            this.page3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.Banner = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.Image_ModeI = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Image_Stretch = new System.Windows.Forms.ComboBox();
            this.SaveDataTitle = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.baseList_banner = new System.Windows.Forms.ComboBox();
            this.ImportBanner = new System.Windows.Forms.CheckBox();
            this.Image = new System.Windows.Forms.PictureBox();
            this.Players = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ReleaseYear = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.BannerTitle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ChannelTitle = new System.Windows.Forms.TextBox();
            this.Customize = new System.Windows.Forms.CheckBox();
            this.page4 = new System.Windows.Forms.Panel();
            this.RegionFree = new System.Windows.Forms.CheckBox();
            this.TitleID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DisableEmanual = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Options_Flash = new System.Windows.Forms.Panel();
            this.Flash_Total = new System.Windows.Forms.Label();
            this.Flash_TotalSaveDataSize = new System.Windows.Forms.ComboBox();
            this.Flash_HBM_NoSave = new System.Windows.Forms.CheckBox();
            this.Flash_SaveData = new System.Windows.Forms.CheckBox();
            this.Options_N64 = new System.Windows.Forms.Panel();
            this.N64_AllocateROM = new System.Windows.Forms.CheckBox();
            this.N64_8MBRAM = new System.Windows.Forms.CheckBox();
            this.N64_FixBrightness = new System.Windows.Forms.CheckBox();
            this.Options_NES = new System.Windows.Forms.Panel();
            this.NES_PaletteList = new System.Windows.Forms.ComboBox();
            this.NES_Palette = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BrowsePatch = new System.Windows.Forms.OpenFileDialog();
            this.BrowseImage = new System.Windows.Forms.OpenFileDialog();
            this.panel.SuspendLayout();
            this.page1.SuspendLayout();
            this.page2.SuspendLayout();
            this.page3.SuspendLayout();
            this.Banner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseYear)).BeginInit();
            this.page4.SuspendLayout();
            this.Options_Flash.SuspendLayout();
            this.Options_N64.SuspendLayout();
            this.Options_NES.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.panel.Controls.Add(this.Settings);
            this.panel.Controls.Add(this.Back);
            this.panel.Controls.Add(this.Save);
            this.panel.Controls.Add(this.Next);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // Settings
            // 
            this.Settings.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Settings.FlatAppearance.BorderSize = 0;
            this.Settings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.Settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.Settings, "Settings");
            this.Settings.Name = "Settings";
            this.Settings.UseCompatibleTextRendering = true;
            this.Settings.UseVisualStyleBackColor = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Back
            // 
            resources.ApplyResources(this.Back, "Back");
            this.Back.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Back.FlatAppearance.BorderSize = 0;
            this.Back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.Back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Back.Name = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Save
            // 
            this.Save.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Save.FlatAppearance.BorderSize = 0;
            this.Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.Save, "Save");
            this.Save.Name = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Finish_Click);
            // 
            // Next
            // 
            resources.ApplyResources(this.Next, "Next");
            this.Next.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Next.FlatAppearance.BorderSize = 0;
            this.Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Next.Name = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Console
            // 
            this.Console.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Console, "Console");
            this.Console.FormattingEnabled = true;
            this.Console.Items.AddRange(new object[] {
            resources.GetString("Console.Items")});
            this.Console.Name = "Console";
            this.Console.SelectedIndexChanged += new System.EventHandler(this.Console_Changed);
            // 
            // page1
            // 
            this.page1.Controls.Add(this.Console);
            this.page1.Controls.Add(this.label1);
            resources.ApplyResources(this.page1, "page1");
            this.page1.Name = "page1";
            // 
            // page2
            // 
            this.page2.Controls.Add(this.Patch);
            this.page2.Controls.Add(this.label3);
            this.page2.Controls.Add(this.DeleteBase);
            this.page2.Controls.Add(this.AddBase);
            this.page2.Controls.Add(this.baseList);
            this.page2.Controls.Add(this.OpenROM);
            this.page2.Controls.Add(this.label2);
            resources.ApplyResources(this.page2, "page2");
            this.page2.Name = "page2";
            // 
            // Patch
            // 
            resources.ApplyResources(this.Patch, "Patch");
            this.Patch.Name = "Patch";
            this.Patch.UseVisualStyleBackColor = true;
            this.Patch.CheckedChanged += new System.EventHandler(this.Patch_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // DeleteBase
            // 
            resources.ApplyResources(this.DeleteBase, "DeleteBase");
            this.DeleteBase.Name = "DeleteBase";
            this.DeleteBase.UseVisualStyleBackColor = true;
            this.DeleteBase.Click += new System.EventHandler(this.DeleteWAD);
            // 
            // AddBase
            // 
            resources.ApplyResources(this.AddBase, "AddBase");
            this.AddBase.Name = "AddBase";
            this.AddBase.UseVisualStyleBackColor = true;
            this.AddBase.Click += new System.EventHandler(this.AddWAD);
            // 
            // baseList
            // 
            this.baseList.DisplayMember = "Base";
            this.baseList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.baseList, "baseList");
            this.baseList.FormattingEnabled = true;
            this.baseList.Name = "baseList";
            this.baseList.SelectedIndexChanged += new System.EventHandler(this.BaseList_Changed);
            // 
            // OpenROM
            // 
            resources.ApplyResources(this.OpenROM, "OpenROM");
            this.OpenROM.Name = "OpenROM";
            this.OpenROM.UseVisualStyleBackColor = true;
            this.OpenROM.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // BrowseWAD
            // 
            resources.ApplyResources(this.BrowseWAD, "BrowseWAD");
            // 
            // SaveWAD
            // 
            this.SaveWAD.DefaultExt = "wad";
            // 
            // page3
            // 
            this.page3.Controls.Add(this.label5);
            this.page3.Controls.Add(this.Banner);
            this.page3.Controls.Add(this.Customize);
            resources.ApplyResources(this.page3, "page3");
            this.page3.Name = "page3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // Banner
            // 
            this.Banner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Banner.Controls.Add(this.label14);
            this.Banner.Controls.Add(this.Image_ModeI);
            this.Banner.Controls.Add(this.label13);
            this.Banner.Controls.Add(this.Image_Stretch);
            this.Banner.Controls.Add(this.SaveDataTitle);
            this.Banner.Controls.Add(this.label12);
            this.Banner.Controls.Add(this.baseList_banner);
            this.Banner.Controls.Add(this.ImportBanner);
            this.Banner.Controls.Add(this.Image);
            this.Banner.Controls.Add(this.Players);
            this.Banner.Controls.Add(this.label11);
            this.Banner.Controls.Add(this.ReleaseYear);
            this.Banner.Controls.Add(this.label10);
            this.Banner.Controls.Add(this.BannerTitle);
            this.Banner.Controls.Add(this.label9);
            this.Banner.Controls.Add(this.label8);
            this.Banner.Controls.Add(this.ChannelTitle);
            resources.ApplyResources(this.Banner, "Banner");
            this.Banner.Name = "Banner";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // Image_ModeI
            // 
            this.Image_ModeI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Image_ModeI, "Image_ModeI");
            this.Image_ModeI.FormattingEnabled = true;
            this.Image_ModeI.Items.AddRange(new object[] {
            resources.GetString("Image_ModeI.Items")});
            this.Image_ModeI.Name = "Image_ModeI";
            this.Image_ModeI.SelectedIndexChanged += new System.EventHandler(this.Image_ModeIChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // Image_Stretch
            // 
            this.Image_Stretch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Image_Stretch, "Image_Stretch");
            this.Image_Stretch.FormattingEnabled = true;
            this.Image_Stretch.Items.AddRange(new object[] {
            resources.GetString("Image_Stretch.Items")});
            this.Image_Stretch.Name = "Image_Stretch";
            this.Image_Stretch.SelectedIndexChanged += new System.EventHandler(this.Image_StretchChanged);
            // 
            // SaveDataTitle
            // 
            resources.ApplyResources(this.SaveDataTitle, "SaveDataTitle");
            this.SaveDataTitle.Name = "SaveDataTitle";
            this.SaveDataTitle.TextChanged += new System.EventHandler(this.BannerText_Changed);
            this.SaveDataTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BannerText_KeyPress);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // baseList_banner
            // 
            this.baseList_banner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.baseList_banner, "baseList_banner");
            this.baseList_banner.FormattingEnabled = true;
            this.baseList_banner.Name = "baseList_banner";
            // 
            // ImportBanner
            // 
            resources.ApplyResources(this.ImportBanner, "ImportBanner");
            this.ImportBanner.Name = "ImportBanner";
            this.ImportBanner.UseVisualStyleBackColor = true;
            this.ImportBanner.CheckedChanged += new System.EventHandler(this.ImportBanner_CheckedChanged);
            // 
            // Image
            // 
            this.Image.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Image.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Image, "Image");
            this.Image.Name = "Image";
            this.Image.TabStop = false;
            this.Image.Click += new System.EventHandler(this.Image_Click);
            // 
            // Players
            // 
            resources.ApplyResources(this.Players, "Players");
            this.Players.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Players.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Players.Name = "Players";
            this.Players.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // ReleaseYear
            // 
            resources.ApplyResources(this.ReleaseYear, "ReleaseYear");
            this.ReleaseYear.Maximum = new decimal(new int[] {
            2029,
            0,
            0,
            0});
            this.ReleaseYear.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.ReleaseYear.Name = "ReleaseYear";
            this.ReleaseYear.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // BannerTitle
            // 
            resources.ApplyResources(this.BannerTitle, "BannerTitle");
            this.BannerTitle.Name = "BannerTitle";
            this.BannerTitle.TextChanged += new System.EventHandler(this.BannerText_Changed);
            this.BannerTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BannerText_KeyPress);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // ChannelTitle
            // 
            resources.ApplyResources(this.ChannelTitle, "ChannelTitle");
            this.ChannelTitle.Name = "ChannelTitle";
            this.ChannelTitle.TextChanged += new System.EventHandler(this.BannerText_Changed);
            // 
            // Customize
            // 
            resources.ApplyResources(this.Customize, "Customize");
            this.Customize.Name = "Customize";
            this.Customize.UseVisualStyleBackColor = true;
            this.Customize.CheckedChanged += new System.EventHandler(this.Customize_CheckedChanged);
            // 
            // page4
            // 
            this.page4.Controls.Add(this.RegionFree);
            this.page4.Controls.Add(this.TitleID);
            this.page4.Controls.Add(this.label7);
            this.page4.Controls.Add(this.DisableEmanual);
            this.page4.Controls.Add(this.label4);
            this.page4.Controls.Add(this.Options_Flash);
            this.page4.Controls.Add(this.Options_N64);
            this.page4.Controls.Add(this.Options_NES);
            resources.ApplyResources(this.page4, "page4");
            this.page4.Name = "page4";
            // 
            // RegionFree
            // 
            resources.ApplyResources(this.RegionFree, "RegionFree");
            this.RegionFree.Name = "RegionFree";
            this.RegionFree.UseVisualStyleBackColor = true;
            // 
            // TitleID
            // 
            this.TitleID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.TitleID, "TitleID");
            this.TitleID.Name = "TitleID";
            this.TitleID.TextChanged += new System.EventHandler(this.TitleID_Changed);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // DisableEmanual
            // 
            resources.ApplyResources(this.DisableEmanual, "DisableEmanual");
            this.DisableEmanual.Name = "DisableEmanual";
            this.DisableEmanual.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // Options_Flash
            // 
            this.Options_Flash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_Flash.Controls.Add(this.Flash_Total);
            this.Options_Flash.Controls.Add(this.Flash_TotalSaveDataSize);
            this.Options_Flash.Controls.Add(this.Flash_HBM_NoSave);
            this.Options_Flash.Controls.Add(this.Flash_SaveData);
            resources.ApplyResources(this.Options_Flash, "Options_Flash");
            this.Options_Flash.Name = "Options_Flash";
            // 
            // Flash_Total
            // 
            resources.ApplyResources(this.Flash_Total, "Flash_Total");
            this.Flash_Total.Name = "Flash_Total";
            // 
            // Flash_TotalSaveDataSize
            // 
            this.Flash_TotalSaveDataSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Flash_TotalSaveDataSize.FormattingEnabled = true;
            this.Flash_TotalSaveDataSize.Items.AddRange(new object[] {
            resources.GetString("Flash_TotalSaveDataSize.Items"),
            resources.GetString("Flash_TotalSaveDataSize.Items1"),
            resources.GetString("Flash_TotalSaveDataSize.Items2"),
            resources.GetString("Flash_TotalSaveDataSize.Items3"),
            resources.GetString("Flash_TotalSaveDataSize.Items4")});
            resources.ApplyResources(this.Flash_TotalSaveDataSize, "Flash_TotalSaveDataSize");
            this.Flash_TotalSaveDataSize.Name = "Flash_TotalSaveDataSize";
            // 
            // Flash_HBM_NoSave
            // 
            resources.ApplyResources(this.Flash_HBM_NoSave, "Flash_HBM_NoSave");
            this.Flash_HBM_NoSave.Name = "Flash_HBM_NoSave";
            this.Flash_HBM_NoSave.UseVisualStyleBackColor = true;
            // 
            // Flash_SaveData
            // 
            resources.ApplyResources(this.Flash_SaveData, "Flash_SaveData");
            this.Flash_SaveData.Name = "Flash_SaveData";
            this.Flash_SaveData.UseVisualStyleBackColor = true;
            // 
            // Options_N64
            // 
            this.Options_N64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_N64.Controls.Add(this.N64_AllocateROM);
            this.Options_N64.Controls.Add(this.N64_8MBRAM);
            this.Options_N64.Controls.Add(this.N64_FixBrightness);
            resources.ApplyResources(this.Options_N64, "Options_N64");
            this.Options_N64.Name = "Options_N64";
            // 
            // N64_AllocateROM
            // 
            resources.ApplyResources(this.N64_AllocateROM, "N64_AllocateROM");
            this.N64_AllocateROM.Name = "N64_AllocateROM";
            this.N64_AllocateROM.UseVisualStyleBackColor = true;
            // 
            // N64_8MBRAM
            // 
            resources.ApplyResources(this.N64_8MBRAM, "N64_8MBRAM");
            this.N64_8MBRAM.Name = "N64_8MBRAM";
            this.N64_8MBRAM.UseVisualStyleBackColor = true;
            // 
            // N64_FixBrightness
            // 
            resources.ApplyResources(this.N64_FixBrightness, "N64_FixBrightness");
            this.N64_FixBrightness.Name = "N64_FixBrightness";
            this.N64_FixBrightness.UseVisualStyleBackColor = true;
            // 
            // Options_NES
            // 
            this.Options_NES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_NES.Controls.Add(this.NES_PaletteList);
            this.Options_NES.Controls.Add(this.NES_Palette);
            resources.ApplyResources(this.Options_NES, "Options_NES");
            this.Options_NES.Name = "Options_NES";
            // 
            // NES_PaletteList
            // 
            this.NES_PaletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NES_PaletteList.FormattingEnabled = true;
            this.NES_PaletteList.Items.AddRange(new object[] {
            resources.GetString("NES_PaletteList.Items")});
            resources.ApplyResources(this.NES_PaletteList, "NES_PaletteList");
            this.NES_PaletteList.Name = "NES_PaletteList";
            this.NES_PaletteList.SelectedIndexChanged += new System.EventHandler(this.NES_PaletteChanged);
            // 
            // NES_Palette
            // 
            resources.ApplyResources(this.NES_Palette, "NES_Palette");
            this.NES_Palette.Name = "NES_Palette";
            // 
            // BrowsePatch
            // 
            resources.ApplyResources(this.BrowsePatch, "BrowsePatch");
            // 
            // BrowseImage
            // 
            resources.ApplyResources(this.BrowseImage, "BrowseImage");
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.Controls.Add(this.panel);
            this.Controls.Add(this.page3);
            this.Controls.Add(this.page2);
            this.Controls.Add(this.page1);
            this.Controls.Add(this.page4);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.panel.ResumeLayout(false);
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            this.page2.ResumeLayout(false);
            this.page2.PerformLayout();
            this.page3.ResumeLayout(false);
            this.page3.PerformLayout();
            this.Banner.ResumeLayout(false);
            this.Banner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseYear)).EndInit();
            this.page4.ResumeLayout(false);
            this.page4.PerformLayout();
            this.Options_Flash.ResumeLayout(false);
            this.Options_Flash.PerformLayout();
            this.Options_N64.ResumeLayout(false);
            this.Options_N64.PerformLayout();
            this.Options_NES.ResumeLayout(false);
            this.Options_NES.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ComboBox Console;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Panel page1;
        private System.Windows.Forms.Panel page2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OpenROM;
        private System.Windows.Forms.OpenFileDialog BrowseROM;
        private System.Windows.Forms.OpenFileDialog BrowseWAD;
        private System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.ComboBox baseList;
        private System.Windows.Forms.Button AddBase;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button DeleteBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel page3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel page4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox DisableEmanual;
        private System.Windows.Forms.CheckBox Customize;
        private System.Windows.Forms.Panel Options_NES;
        private System.Windows.Forms.Label NES_Palette;
        private System.Windows.Forms.ComboBox NES_PaletteList;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Panel Banner;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TitleID;
        private System.Windows.Forms.CheckBox Patch;
        private System.Windows.Forms.OpenFileDialog BrowsePatch;
        private System.Windows.Forms.CheckBox RegionFree;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ChannelTitle;
        private System.Windows.Forms.TextBox BannerTitle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown ReleaseYear;
        private System.Windows.Forms.NumericUpDown Players;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox Image;
        private System.Windows.Forms.CheckBox ImportBanner;
        private System.Windows.Forms.ComboBox baseList_banner;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox SaveDataTitle;
        private System.Windows.Forms.OpenFileDialog BrowseImage;
        private System.Windows.Forms.ComboBox Image_Stretch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox Image_ModeI;
        private System.Windows.Forms.Panel Options_N64;
        private System.Windows.Forms.CheckBox N64_FixBrightness;
        private System.Windows.Forms.CheckBox N64_8MBRAM;
        private System.Windows.Forms.CheckBox N64_AllocateROM;
        private System.Windows.Forms.Panel Options_Flash;
        private System.Windows.Forms.CheckBox Flash_HBM_NoSave;
        private System.Windows.Forms.CheckBox Flash_SaveData;
        private System.Windows.Forms.ComboBox Flash_TotalSaveDataSize;
        private System.Windows.Forms.Label Flash_Total;
    }
}

