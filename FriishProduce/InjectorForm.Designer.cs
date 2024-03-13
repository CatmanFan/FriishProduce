
namespace FriishProduce
{
    partial class InjectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InjectorForm));
            this.WADRegionList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.imageintpl = new System.Windows.Forms.ComboBox();
            this.BrowseWAD = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SaveIcon_Panel = new System.Windows.Forms.Panel();
            this.SaveDataTitle = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.COPanel_Forwarder = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.Forwarder_Mode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Forwarder_Device = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InjectorsList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.COPanel_VC = new System.Windows.Forms.Panel();
            this.CustomManual = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ImportPatch = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChannelTitle_Locale = new System.Windows.Forms.CheckBox();
            this.TitleID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Random = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RegionFree = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ChannelTitle = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenWAD = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Players = new System.Windows.Forms.NumericUpDown();
            this.ReleaseYear = new System.Windows.Forms.NumericUpDown();
            this.BannerTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.baseID = new System.Windows.Forms.Label();
            this.baseName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WADRegion = new System.Windows.Forms.PictureBox();
            this.Base = new System.Windows.Forms.ComboBox();
            this.BrowseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.BrowsePatch = new System.Windows.Forms.OpenFileDialog();
            this.bannerPreview1 = new FriishProduce.BannerPreview();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.COPanel_Forwarder.SuspendLayout();
            this.COPanel_VC.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseYear)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WADRegion)).BeginInit();
            this.SuspendLayout();
            // 
            // WADRegionList
            // 
            resources.ApplyResources(this.WADRegionList, "WADRegionList");
            this.WADRegionList.Name = "WADRegion";
            this.WADRegionList.ShowCheckMargin = true;
            this.WADRegionList.ShowImageMargin = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButton2);
            this.groupBox8.Controls.Add(this.radioButton1);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.imageintpl);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // imageintpl
            // 
            this.imageintpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.imageintpl, "imageintpl");
            this.imageintpl.FormattingEnabled = true;
            this.imageintpl.Items.AddRange(new object[] {
            resources.GetString("imageintpl.Items")});
            this.imageintpl.Name = "imageintpl";
            this.imageintpl.SelectedIndexChanged += new System.EventHandler(this.InterpolationChanged);
            // 
            // BrowseWAD
            // 
            resources.ApplyResources(this.BrowseWAD, "BrowseWAD");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.SaveIcon_Panel);
            this.groupBox5.Controls.Add(this.SaveDataTitle);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // SaveIcon_Panel
            // 
            this.SaveIcon_Panel.BackColor = System.Drawing.Color.Black;
            this.SaveIcon_Panel.BackgroundImage = global::FriishProduce.Properties.Resources.SaveIconPlaceholder;
            resources.ApplyResources(this.SaveIcon_Panel, "SaveIcon_Panel");
            this.SaveIcon_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SaveIcon_Panel.Name = "SaveIcon_Panel";
            // 
            // SaveDataTitle
            // 
            resources.ApplyResources(this.SaveDataTitle, "SaveDataTitle");
            this.SaveDataTitle.Name = "SaveDataTitle";
            this.SaveDataTitle.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.SaveDataTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.COPanel_Forwarder);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.InjectorsList);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.COPanel_VC);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // COPanel_Forwarder
            // 
            this.COPanel_Forwarder.Controls.Add(this.label14);
            this.COPanel_Forwarder.Controls.Add(this.Forwarder_Mode);
            this.COPanel_Forwarder.Controls.Add(this.label13);
            this.COPanel_Forwarder.Controls.Add(this.Forwarder_Device);
            resources.ApplyResources(this.COPanel_Forwarder, "COPanel_Forwarder");
            this.COPanel_Forwarder.Name = "COPanel_Forwarder";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // Forwarder_Mode
            // 
            this.Forwarder_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Forwarder_Mode.FormattingEnabled = true;
            this.Forwarder_Mode.Items.AddRange(new object[] {
            resources.GetString("Forwarder_Mode.Items"),
            resources.GetString("Forwarder_Mode.Items1")});
            resources.ApplyResources(this.Forwarder_Mode, "Forwarder_Mode");
            this.Forwarder_Mode.Name = "Forwarder_Mode";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // Forwarder_Device
            // 
            this.Forwarder_Device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Forwarder_Device.FormattingEnabled = true;
            this.Forwarder_Device.Items.AddRange(new object[] {
            resources.GetString("Forwarder_Device.Items"),
            resources.GetString("Forwarder_Device.Items1")});
            resources.ApplyResources(this.Forwarder_Device, "Forwarder_Device");
            this.Forwarder_Device.Name = "Forwarder_Device";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // InjectorsList
            // 
            this.InjectorsList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.InjectorsList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.InjectorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InjectorsList.FormattingEnabled = true;
            resources.ApplyResources(this.InjectorsList, "InjectorsList");
            this.InjectorsList.Name = "InjectorsList";
            this.InjectorsList.SelectedIndexChanged += new System.EventHandler(this.InjectorsList_SelectedIndexChanged);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::FriishProduce.Properties.Resources.wrench;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenInjectorOptions);
            // 
            // COPanel_VC
            // 
            this.COPanel_VC.Controls.Add(this.CustomManual);
            resources.ApplyResources(this.COPanel_VC, "COPanel_VC");
            this.COPanel_VC.Name = "COPanel_VC";
            // 
            // CustomManual
            // 
            resources.ApplyResources(this.CustomManual, "CustomManual");
            this.CustomManual.Name = "CustomManual";
            this.CustomManual.UseVisualStyleBackColor = true;
            this.CustomManual.CheckedChanged += new System.EventHandler(this.CustomManual_CheckedChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Name = "label11";
            // 
            // ImportPatch
            // 
            resources.ApplyResources(this.ImportPatch, "ImportPatch");
            this.ImportPatch.Name = "ImportPatch";
            this.ImportPatch.UseVisualStyleBackColor = true;
            this.ImportPatch.CheckedChanged += new System.EventHandler(this.Patch_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ChannelTitle_Locale);
            this.groupBox3.Controls.Add(this.TitleID);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.Random);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.RegionFree);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.ChannelTitle);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // ChannelTitle_Locale
            // 
            resources.ApplyResources(this.ChannelTitle_Locale, "ChannelTitle_Locale");
            this.ChannelTitle_Locale.Image = global::FriishProduce.Properties.Resources.locale;
            this.ChannelTitle_Locale.Name = "ChannelTitle_Locale";
            this.ChannelTitle_Locale.UseVisualStyleBackColor = true;
            this.ChannelTitle_Locale.CheckedChanged += new System.EventHandler(this.ChannelTitle_Locale_CheckedChanged);
            // 
            // TitleID
            // 
            this.TitleID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.TitleID, "TitleID");
            this.TitleID.Name = "TitleID";
            this.TitleID.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // Random
            // 
            this.Random.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Random.Image = global::FriishProduce.Properties.Resources.arrow_circle_double;
            resources.ApplyResources(this.Random, "Random");
            this.Random.Name = "Random";
            this.Random.TabStop = false;
            this.Random.Click += new System.EventHandler(this.Random_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // RegionFree
            // 
            resources.ApplyResources(this.RegionFree, "RegionFree");
            this.RegionFree.Name = "RegionFree";
            this.RegionFree.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // ChannelTitle
            // 
            resources.ApplyResources(this.ChannelTitle, "ChannelTitle");
            this.ChannelTitle.Name = "ChannelTitle";
            this.ChannelTitle.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ImportPatch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.OpenWAD);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.UseMnemonic = false;
            // 
            // OpenWAD
            // 
            resources.ApplyResources(this.OpenWAD, "OpenWAD");
            this.OpenWAD.Name = "OpenWAD";
            this.OpenWAD.UseVisualStyleBackColor = true;
            this.OpenWAD.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Players);
            this.groupBox6.Controls.Add(this.ReleaseYear);
            this.groupBox6.Controls.Add(this.BannerTitle);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label10);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // Players
            // 
            resources.ApplyResources(this.Players, "Players");
            this.Players.Maximum = new decimal(new int[] {
            9,
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
            this.Players.ValueChanged += new System.EventHandler(this.Value_Changed);
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
            this.ReleaseYear.ValueChanged += new System.EventHandler(this.Value_Changed);
            // 
            // BannerTitle
            // 
            resources.ApplyResources(this.BannerTitle, "BannerTitle");
            this.BannerTitle.Name = "BannerTitle";
            this.BannerTitle.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.BannerTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.baseID);
            this.groupBox2.Controls.Add(this.baseName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.WADRegion);
            this.groupBox2.Controls.Add(this.Base);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // baseID
            // 
            resources.ApplyResources(this.baseID, "baseID");
            this.baseID.Name = "baseID";
            this.baseID.UseMnemonic = false;
            // 
            // baseName
            // 
            resources.ApplyResources(this.baseName, "baseName");
            this.baseName.Name = "baseName";
            this.baseName.UseMnemonic = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.label5.UseMnemonic = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.UseMnemonic = false;
            // 
            // WADRegion
            // 
            this.WADRegion.BackColor = System.Drawing.SystemColors.ControlLight;
            this.WADRegion.BackgroundImage = global::FriishProduce.Properties.Resources.x;
            resources.ApplyResources(this.WADRegion, "WADRegion");
            this.WADRegion.ContextMenuStrip = this.WADRegionList;
            this.WADRegion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WADRegion.Name = "WADRegion";
            this.WADRegion.TabStop = false;
            this.WADRegion.Click += new System.EventHandler(this.WADRegion_Click);
            // 
            // Base
            // 
            this.Base.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Base.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Base.DropDownHeight = 150;
            this.Base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Base, "Base");
            this.Base.FormattingEnabled = true;
            this.Base.Name = "Base";
            this.Base.SelectedIndexChanged += new System.EventHandler(this.Base_SelectedIndexChanged);
            // 
            // BrowseManual
            // 
            this.BrowseManual.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.BrowseManual.ShowNewFolderButton = false;
            // 
            // BrowsePatch
            // 
            this.BrowsePatch.RestoreDirectory = true;
            // 
            // bannerPreview1
            // 
            this.bannerPreview1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.bannerPreview1, "bannerPreview1");
            this.bannerPreview1.Name = "bannerPreview1";
            // 
            // InjectorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.bannerPreview1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InjectorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.COPanel_Forwarder.ResumeLayout(false);
            this.COPanel_Forwarder.PerformLayout();
            this.COPanel_VC.ResumeLayout(false);
            this.COPanel_VC.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseYear)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WADRegion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox SaveDataTitle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox BannerTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ChannelTitle;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TitleID;
        private System.Windows.Forms.Panel SaveIcon_Panel;
        private System.Windows.Forms.ComboBox imageintpl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox RegionFree;
        private System.Windows.Forms.PictureBox Random;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox WADRegion;
        private System.Windows.Forms.ContextMenuStrip WADRegionList;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown ReleaseYear;
        private System.Windows.Forms.NumericUpDown Players;
        private System.Windows.Forms.GroupBox groupBox8;
        internal System.Windows.Forms.OpenFileDialog BrowseWAD;
        private System.Windows.Forms.CheckBox OpenWAD;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox ChannelTitle_Locale;
        private System.Windows.Forms.Label label2;
        private BannerPreview bannerPreview1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ComboBox InjectorsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ImportPatch;
        private System.Windows.Forms.CheckBox CustomManual;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog BrowseManual;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.OpenFileDialog BrowsePatch;
        private System.Windows.Forms.Panel COPanel_VC;
        private System.Windows.Forms.Panel COPanel_Forwarder;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox Forwarder_Mode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox Forwarder_Device;
        private System.Windows.Forms.Label label15;
    }
}