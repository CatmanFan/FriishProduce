
namespace FriishProduce
{
    partial class ProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            this.BaseRegionList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.imageintpl = new System.Windows.Forms.ComboBox();
            this.image_fit = new System.Windows.Forms.RadioButton();
            this.image_stretch = new System.Windows.Forms.RadioButton();
            this.BrowseWAD = new System.Windows.Forms.OpenFileDialog();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.LinkSaveData = new System.Windows.Forms.CheckBox();
            this.SaveIcon_Panel = new System.Windows.Forms.Panel();
            this.SaveDataTitle = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InjectorsList = new System.Windows.Forms.ComboBox();
            this.MethodOptions = new System.Windows.Forms.Button();
            this.COPanel_VC = new System.Windows.Forms.Panel();
            this.CustomManual = new System.Windows.Forms.CheckBox();
            this.COPanel_Forwarder = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.FStorage_SD = new System.Windows.Forms.RadioButton();
            this.FStorage_USB = new System.Windows.Forms.RadioButton();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.toggleSwitchL1 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.label11 = new System.Windows.Forms.Label();
            this.Patch = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.title_id_2 = new System.Windows.Forms.Label();
            this.ChannelTitle_Locale = new System.Windows.Forms.CheckBox();
            this.TargetRegion = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.TitleID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Random = new System.Windows.Forms.PictureBox();
            this.ChannelTitle = new System.Windows.Forms.TextBox();
            this.short_channel_name = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.software_name = new System.Windows.Forms.Label();
            this.filename = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Players = new System.Windows.Forms.NumericUpDown();
            this.ReleaseYear = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BannerTitle = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ImportWAD = new System.Windows.Forms.RadioButton();
            this.DownloadWAD = new System.Windows.Forms.RadioButton();
            this.base_name = new System.Windows.Forms.Label();
            this.title_id = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baseID = new System.Windows.Forms.Label();
            this.BaseRegion = new System.Windows.Forms.PictureBox();
            this.Base = new System.Windows.Forms.ComboBox();
            this.baseName = new System.Windows.Forms.Label();
            this.BrowsePatch = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.BrowseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.COPanel_VC.SuspendLayout();
            this.COPanel_Forwarder.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseYear)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // BaseRegionList
            // 
            resources.ApplyResources(this.BaseRegionList, "BaseRegionList");
            this.BaseRegionList.Name = "WADRegion";
            this.BaseRegionList.ShowCheckMargin = true;
            this.BaseRegionList.ShowImageMargin = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.imageintpl);
            this.groupBox6.Controls.Add(this.image_fit);
            this.groupBox6.Controls.Add(this.image_stretch);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "image";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.label12.Tag = "image_interpolation_mode";
            // 
            // imageintpl
            // 
            this.imageintpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageintpl.FormattingEnabled = true;
            this.imageintpl.Items.AddRange(new object[] {
            resources.GetString("imageintpl.Items")});
            resources.ApplyResources(this.imageintpl, "imageintpl");
            this.imageintpl.Name = "imageintpl";
            this.imageintpl.SelectedIndexChanged += new System.EventHandler(this.InterpolationChanged);
            // 
            // image_fit
            // 
            resources.ApplyResources(this.image_fit, "image_fit");
            this.image_fit.Name = "image_fit";
            this.image_fit.Tag = "image_fit";
            this.image_fit.UseVisualStyleBackColor = true;
            this.image_fit.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // image_stretch
            // 
            resources.ApplyResources(this.image_stretch, "image_stretch");
            this.image_stretch.Name = "image_stretch";
            this.image_stretch.Tag = "image_stretch";
            this.image_stretch.UseVisualStyleBackColor = true;
            this.image_stretch.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // BrowseWAD
            // 
            resources.ApplyResources(this.BrowseWAD, "BrowseWAD");
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.LinkSaveData);
            this.groupBox7.Controls.Add(this.SaveIcon_Panel);
            this.groupBox7.Controls.Add(this.SaveDataTitle);
            this.groupBox7.Controls.Add(this.label16);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.groupBox7.Tag = "save_data";
            // 
            // LinkSaveData
            // 
            resources.ApplyResources(this.LinkSaveData, "LinkSaveData");
            this.LinkSaveData.Name = "LinkSaveData";
            this.LinkSaveData.Tag = "autolink_save_data";
            this.LinkSaveData.UseVisualStyleBackColor = true;
            this.LinkSaveData.CheckedChanged += new System.EventHandler(this.LinkSaveData_Changed);
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
            this.SaveDataTitle.Tag = "33";
            this.SaveDataTitle.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.SaveDataTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.label16.Tag = "none";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.InjectorsList);
            this.groupBox3.Controls.Add(this.MethodOptions);
            this.groupBox3.Controls.Add(this.COPanel_VC);
            this.groupBox3.Controls.Add(this.COPanel_Forwarder);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "content_options";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Tag = "injection_method";
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
            // MethodOptions
            // 
            resources.ApplyResources(this.MethodOptions, "MethodOptions");
            this.MethodOptions.Image = global::FriishProduce.Properties.Resources.wrench;
            this.MethodOptions.Name = "MethodOptions";
            this.MethodOptions.Tag = "";
            this.MethodOptions.UseVisualStyleBackColor = true;
            this.MethodOptions.Click += new System.EventHandler(this.OpenInjectorOptions);
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
            this.CustomManual.Tag = "import_manual";
            this.CustomManual.UseVisualStyleBackColor = true;
            this.CustomManual.CheckedChanged += new System.EventHandler(this.CustomManual_CheckedChanged);
            // 
            // COPanel_Forwarder
            // 
            this.COPanel_Forwarder.Controls.Add(this.groupBox8);
            this.COPanel_Forwarder.Controls.Add(this.groupBox9);
            resources.ApplyResources(this.COPanel_Forwarder, "COPanel_Forwarder");
            this.COPanel_Forwarder.Name = "COPanel_Forwarder";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.FStorage_SD);
            this.groupBox8.Controls.Add(this.FStorage_USB);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.groupBox8.Tag = "forwarder_root_device";
            // 
            // FStorage_SD
            // 
            resources.ApplyResources(this.FStorage_SD, "FStorage_SD");
            this.FStorage_SD.Name = "FStorage_SD";
            this.FStorage_SD.TabStop = true;
            this.FStorage_SD.UseVisualStyleBackColor = true;
            this.FStorage_SD.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // FStorage_USB
            // 
            resources.ApplyResources(this.FStorage_USB, "FStorage_USB");
            this.FStorage_USB.Name = "FStorage_USB";
            this.FStorage_USB.TabStop = true;
            this.FStorage_USB.UseVisualStyleBackColor = true;
            this.FStorage_USB.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.toggleSwitchL1);
            this.groupBox9.Controls.Add(this.toggleSwitch1);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            this.groupBox9.Tag = "forwarder_console";
            // 
            // toggleSwitchL1
            // 
            resources.ApplyResources(this.toggleSwitchL1, "toggleSwitchL1");
            this.toggleSwitchL1.Name = "toggleSwitchL1";
            // 
            // toggleSwitch1
            // 
            resources.ApplyResources(this.toggleSwitch1, "toggleSwitch1");
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // Patch
            // 
            resources.ApplyResources(this.Patch, "Patch");
            this.Patch.Name = "Patch";
            this.Patch.Tag = "import_patch";
            this.Patch.UseVisualStyleBackColor = true;
            this.Patch.CheckedChanged += new System.EventHandler(this.Patch_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.title_id_2);
            this.groupBox4.Controls.Add(this.ChannelTitle_Locale);
            this.groupBox4.Controls.Add(this.TargetRegion);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.TitleID);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.Random);
            this.groupBox4.Controls.Add(this.ChannelTitle);
            this.groupBox4.Controls.Add(this.short_channel_name);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "channel";
            // 
            // title_id_2
            // 
            resources.ApplyResources(this.title_id_2, "title_id_2");
            this.title_id_2.Name = "title_id_2";
            this.title_id_2.Tag = "title_id";
            // 
            // ChannelTitle_Locale
            // 
            resources.ApplyResources(this.ChannelTitle_Locale, "ChannelTitle_Locale");
            this.ChannelTitle_Locale.Image = global::FriishProduce.Properties.Resources.locale;
            this.ChannelTitle_Locale.Name = "ChannelTitle_Locale";
            this.ChannelTitle_Locale.UseVisualStyleBackColor = true;
            this.ChannelTitle_Locale.CheckedChanged += new System.EventHandler(this.ChannelTitle_Locale_CheckedChanged);
            // 
            // TargetRegion
            // 
            this.TargetRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetRegion.FormattingEnabled = true;
            resources.ApplyResources(this.TargetRegion, "TargetRegion");
            this.TargetRegion.Name = "TargetRegion";
            this.TargetRegion.SelectedIndexChanged += new System.EventHandler(this.RegionsList_SelectedIndexChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.label13.Tag = "region";
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
            this.Random.Image = global::FriishProduce.Properties.Resources.arrow_circle_double;
            resources.ApplyResources(this.Random, "Random");
            this.Random.Name = "Random";
            this.Random.TabStop = false;
            this.Random.Click += new System.EventHandler(this.Random_Click);
            // 
            // ChannelTitle
            // 
            resources.ApplyResources(this.ChannelTitle, "ChannelTitle");
            this.ChannelTitle.Name = "ChannelTitle";
            this.ChannelTitle.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // short_channel_name
            // 
            resources.ApplyResources(this.short_channel_name, "short_channel_name");
            this.short_channel_name.Name = "short_channel_name";
            this.short_channel_name.Tag = "short_channel_name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.software_name);
            this.groupBox1.Controls.Add(this.filename);
            this.groupBox1.Controls.Add(this.Patch);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "rom_info";
            // 
            // software_name
            // 
            resources.ApplyResources(this.software_name, "software_name");
            this.software_name.Name = "software_name";
            this.software_name.Tag = "software_name";
            // 
            // filename
            // 
            resources.ApplyResources(this.filename, "filename");
            this.filename.Name = "filename";
            this.filename.Tag = "filename";
            this.filename.UseMnemonic = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Players);
            this.groupBox5.Controls.Add(this.ReleaseYear);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.BannerTitle);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "banner";
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.label10.Tag = "players";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.label9.Tag = "year";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Tag = "full_banner_name";
            // 
            // BannerTitle
            // 
            resources.ApplyResources(this.BannerTitle, "BannerTitle");
            this.BannerTitle.Name = "BannerTitle";
            this.BannerTitle.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.BannerTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.base_name);
            this.groupBox2.Controls.Add(this.ImportWAD);
            this.groupBox2.Controls.Add(this.DownloadWAD);
            this.groupBox2.Controls.Add(this.title_id);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.baseID);
            this.groupBox2.Controls.Add(this.BaseRegion);
            this.groupBox2.Controls.Add(this.Base);
            this.groupBox2.Controls.Add(this.baseName);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "base";
            // 
            // ImportWAD
            // 
            resources.ApplyResources(this.ImportWAD, "ImportWAD");
            this.ImportWAD.Name = "ImportWAD";
            this.ImportWAD.Tag = "import_wad_from_file";
            this.ImportWAD.UseVisualStyleBackColor = true;
            this.ImportWAD.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // DownloadWAD
            // 
            resources.ApplyResources(this.DownloadWAD, "DownloadWAD");
            this.DownloadWAD.Checked = true;
            this.DownloadWAD.Name = "DownloadWAD";
            this.DownloadWAD.TabStop = true;
            this.DownloadWAD.Tag = "use_online_wad";
            this.DownloadWAD.UseVisualStyleBackColor = true;
            this.DownloadWAD.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // base_name
            // 
            resources.ApplyResources(this.base_name, "base_name");
            this.base_name.Name = "base_name";
            this.base_name.Tag = "base_name";
            this.base_name.UseMnemonic = false;
            // 
            // title_id
            // 
            resources.ApplyResources(this.title_id, "title_id");
            this.title_id.Name = "title_id";
            this.title_id.Tag = "title_id";
            this.title_id.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // baseID
            // 
            resources.ApplyResources(this.baseID, "baseID");
            this.baseID.Name = "baseID";
            this.baseID.UseMnemonic = false;
            // 
            // BaseRegion
            // 
            this.BaseRegion.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BaseRegion.BackgroundImage = global::FriishProduce.Properties.Resources.x;
            resources.ApplyResources(this.BaseRegion, "BaseRegion");
            this.BaseRegion.ContextMenuStrip = this.BaseRegionList;
            this.BaseRegion.Name = "BaseRegion";
            this.BaseRegion.TabStop = false;
            this.BaseRegion.Click += new System.EventHandler(this.WADRegion_Click);
            // 
            // Base
            // 
            this.Base.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Base.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Base.DropDownHeight = 150;
            this.Base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Base.FormattingEnabled = true;
            resources.ApplyResources(this.Base, "Base");
            this.Base.Name = "Base";
            this.Base.SelectedIndexChanged += new System.EventHandler(this.Base_SelectedIndexChanged);
            // 
            // baseName
            // 
            resources.ApplyResources(this.baseName, "baseName");
            this.baseName.Name = "baseName";
            this.baseName.UseMnemonic = false;
            // 
            // BrowsePatch
            // 
            this.BrowsePatch.RestoreDirectory = true;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // ProjectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.COPanel_VC.ResumeLayout(false);
            this.COPanel_VC.PerformLayout();
            this.COPanel_Forwarder.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseYear)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox SaveDataTitle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox BannerTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label short_channel_name;
        private System.Windows.Forms.TextBox ChannelTitle;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label title_id_2;
        private System.Windows.Forms.TextBox TitleID;
        private System.Windows.Forms.Panel SaveIcon_Panel;
        private System.Windows.Forms.ComboBox imageintpl;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox Random;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.Button MethodOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox BaseRegion;
        private System.Windows.Forms.ContextMenuStrip BaseRegionList;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.Label title_id;
        private System.Windows.Forms.Label base_name;
        private System.Windows.Forms.NumericUpDown ReleaseYear;
        private System.Windows.Forms.NumericUpDown Players;
        private System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.OpenFileDialog BrowseWAD;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox ChannelTitle_Locale;
        private System.Windows.Forms.Label software_name;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton image_stretch;
        private System.Windows.Forms.RadioButton image_fit;
        private System.Windows.Forms.ComboBox InjectorsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Patch;
        private System.Windows.Forms.CheckBox CustomManual;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.OpenFileDialog BrowsePatch;
        private System.Windows.Forms.Panel COPanel_VC;
        private System.Windows.Forms.Panel COPanel_Forwarder;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton FStorage_SD;
        private System.Windows.Forms.RadioButton FStorage_USB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox TargetRegion;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label toggleSwitchL1;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.CheckBox LinkSaveData;
        private System.Windows.Forms.RadioButton DownloadWAD;
        private System.Windows.Forms.RadioButton ImportWAD;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog BrowseManual;
    }
}