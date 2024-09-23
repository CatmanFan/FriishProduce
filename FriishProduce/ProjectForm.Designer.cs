
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
            this.baseRegionList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.image_fit = new System.Windows.Forms.RadioButton();
            this.image_stretch = new System.Windows.Forms.RadioButton();
            this.browseInputWad = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SaveIcon_Panel = new System.Windows.Forms.PictureBox();
            this.fill_save_data = new System.Windows.Forms.CheckBox();
            this.save_data_title = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.manual_type = new System.Windows.Forms.ComboBox();
            this.injection_methods = new System.Windows.Forms.ComboBox();
            this.region_list = new System.Windows.Forms.ComboBox();
            this.channel_title = new System.Windows.Forms.TextBox();
            this.title_id_upper = new System.Windows.Forms.TextBox();
            this.title_id_random = new System.Windows.Forms.PictureBox();
            this.checkImg2 = new System.Windows.Forms.PictureBox();
            this.import_image = new System.Windows.Forms.Button();
            this.banner = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.import_wad = new System.Windows.Forms.Button();
            this.checkImg3 = new System.Windows.Forms.PictureBox();
            this.use_online_wad = new System.Windows.Forms.RadioButton();
            this.base_name = new System.Windows.Forms.Label();
            this.baseName = new System.Windows.Forms.Label();
            this.BaseRegion = new System.Windows.Forms.PictureBox();
            this.Base = new System.Windows.Forms.ComboBox();
            this.title_id = new System.Windows.Forms.Label();
            this.baseID = new System.Windows.Forms.Label();
            this.use_offline_wad = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.wad_filename = new System.Windows.Forms.TextBox();
            this.browsePatch = new System.Windows.Forms.OpenFileDialog();
            this.browseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.editContentOptions = new System.Windows.Forms.Button();
            this.forwarder_root_device = new System.Windows.Forms.ComboBox();
            this.extra = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.browseROM = new System.Windows.Forms.OpenFileDialog();
            this.browseImage = new System.Windows.Forms.OpenFileDialog();
            this.import_rom = new System.Windows.Forms.Button();
            this.rom_filename = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.import_patch = new System.Windows.Forms.Button();
            this.checkImg1 = new System.Windows.Forms.PictureBox();
            this.patch_filename = new System.Windows.Forms.TextBox();
            this.image_filename = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.video_modes = new System.Windows.Forms.ListBox();
            this.tab_channel = new System.Windows.Forms.TabPage();
            this.tab_main = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.bannerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.banner_customize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.play_banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.replace_banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.restore_banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.browseSound = new System.Windows.Forms.OpenFileDialog();
            this.imageintpl = new System.Windows.Forms.ComboBox();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveIcon_Panel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg1)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tab_channel.SuspendLayout();
            this.tab_main.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.bannerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseRegionList
            // 
            resources.ApplyResources(this.baseRegionList, "baseRegionList");
            this.baseRegionList.Name = "WADRegion";
            this.baseRegionList.ShowCheckMargin = true;
            this.baseRegionList.ShowImageMargin = false;
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
            // browseInputWad
            // 
            resources.ApplyResources(this.browseInputWad, "browseInputWad");
            this.browseInputWad.SupportMultiDottedExtensions = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.SaveIcon_Panel);
            this.groupBox5.Controls.Add(this.fill_save_data);
            this.groupBox5.Controls.Add(this.save_data_title);
            this.groupBox5.Controls.Add(this.label16);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "save_data";
            // 
            // SaveIcon_Panel
            // 
            this.SaveIcon_Panel.BackgroundImage = global::FriishProduce.Properties.Resources.SaveIconPlaceholder;
            resources.ApplyResources(this.SaveIcon_Panel, "SaveIcon_Panel");
            this.SaveIcon_Panel.Name = "SaveIcon_Panel";
            this.SaveIcon_Panel.TabStop = false;
            // 
            // fill_save_data
            // 
            resources.ApplyResources(this.fill_save_data, "fill_save_data");
            this.fill_save_data.Name = "fill_save_data";
            this.fill_save_data.Tag = "fill_save_data";
            this.fill_save_data.UseVisualStyleBackColor = true;
            this.fill_save_data.CheckedChanged += new System.EventHandler(this.LinkSaveData_Changed);
            // 
            // save_data_title
            // 
            resources.ApplyResources(this.save_data_title, "save_data_title");
            this.save_data_title.Name = "save_data_title";
            this.save_data_title.Tag = "19";
            this.save_data_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.save_data_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.label16.Tag = "none";
            // 
            // manual_type
            // 
            this.manual_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manual_type.FormattingEnabled = true;
            resources.ApplyResources(this.manual_type, "manual_type");
            this.manual_type.Name = "manual_type";
            this.manual_type.Tag = "manual_type";
            this.manual_type.SelectedIndexChanged += new System.EventHandler(this.CustomManual_CheckedChanged);
            // 
            // injection_methods
            // 
            this.injection_methods.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.injection_methods.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.injection_methods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.injection_methods.FormattingEnabled = true;
            resources.ApplyResources(this.injection_methods, "injection_methods");
            this.injection_methods.Name = "injection_methods";
            this.injection_methods.SelectedIndexChanged += new System.EventHandler(this.InjectorsList_SelectedIndexChanged);
            // 
            // region_list
            // 
            this.region_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region_list.FormattingEnabled = true;
            resources.ApplyResources(this.region_list, "region_list");
            this.region_list.Name = "region_list";
            this.region_list.SelectedIndexChanged += new System.EventHandler(this.RegionsList_SelectedIndexChanged);
            // 
            // channel_title
            // 
            resources.ApplyResources(this.channel_title, "channel_title");
            this.channel_title.Name = "channel_title";
            this.channel_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // title_id_upper
            // 
            this.title_id_upper.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.title_id_upper, "title_id_upper");
            this.title_id_upper.Name = "title_id_upper";
            this.title_id_upper.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // title_id_random
            // 
            this.title_id_random.Cursor = System.Windows.Forms.Cursors.Hand;
            this.title_id_random.Image = global::FriishProduce.Properties.Resources.arrow_switch;
            resources.ApplyResources(this.title_id_random, "title_id_random");
            this.title_id_random.Name = "title_id_random";
            this.title_id_random.TabStop = false;
            this.title_id_random.Click += new System.EventHandler(this.Random_Click);
            // 
            // checkImg2
            // 
            resources.ApplyResources(this.checkImg2, "checkImg2");
            this.checkImg2.Name = "checkImg2";
            this.checkImg2.TabStop = false;
            // 
            // import_image
            // 
            resources.ApplyResources(this.import_image, "import_image");
            this.import_image.Name = "import_image";
            this.import_image.Tag = "import_image";
            this.import_image.UseVisualStyleBackColor = true;
            this.import_image.Click += new System.EventHandler(this.import_image_Click);
            // 
            // banner
            // 
            this.banner.BackColor = System.Drawing.SystemColors.ControlLight;
            this.banner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.banner.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.banner, "banner");
            this.banner.Name = "banner";
            this.banner.TabStop = false;
            this.banner.Click += new System.EventHandler(this.banner_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.import_wad);
            this.groupBox2.Controls.Add(this.checkImg3);
            this.groupBox2.Controls.Add(this.use_online_wad);
            this.groupBox2.Controls.Add(this.base_name);
            this.groupBox2.Controls.Add(this.baseName);
            this.groupBox2.Controls.Add(this.BaseRegion);
            this.groupBox2.Controls.Add(this.Base);
            this.groupBox2.Controls.Add(this.title_id);
            this.groupBox2.Controls.Add(this.baseID);
            this.groupBox2.Controls.Add(this.use_offline_wad);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.wad_filename);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "wad_base";
            // 
            // import_wad
            // 
            resources.ApplyResources(this.import_wad, "import_wad");
            this.import_wad.Name = "import_wad";
            this.import_wad.Tag = "import_wad";
            this.import_wad.UseVisualStyleBackColor = true;
            this.import_wad.Click += new System.EventHandler(this.import_wad_Click);
            // 
            // checkImg3
            // 
            resources.ApplyResources(this.checkImg3, "checkImg3");
            this.checkImg3.Name = "checkImg3";
            this.checkImg3.TabStop = false;
            // 
            // use_online_wad
            // 
            resources.ApplyResources(this.use_online_wad, "use_online_wad");
            this.use_online_wad.Name = "use_online_wad";
            this.use_online_wad.Tag = "use_online_wad";
            this.use_online_wad.UseVisualStyleBackColor = true;
            this.use_online_wad.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // base_name
            // 
            resources.ApplyResources(this.base_name, "base_name");
            this.base_name.Name = "base_name";
            this.base_name.Tag = "base_name";
            this.base_name.UseMnemonic = false;
            // 
            // baseName
            // 
            resources.ApplyResources(this.baseName, "baseName");
            this.baseName.Name = "baseName";
            this.baseName.UseMnemonic = false;
            // 
            // BaseRegion
            // 
            this.BaseRegion.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BaseRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BaseRegion.ContextMenuStrip = this.baseRegionList;
            resources.ApplyResources(this.BaseRegion, "BaseRegion");
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
            resources.ApplyResources(this.Base, "Base");
            this.Base.FormattingEnabled = true;
            this.Base.Name = "Base";
            this.Base.SelectedIndexChanged += new System.EventHandler(this.Base_SelectedIndexChanged);
            // 
            // title_id
            // 
            resources.ApplyResources(this.title_id, "title_id");
            this.title_id.Name = "title_id";
            this.title_id.Tag = "title_id";
            this.title_id.UseMnemonic = false;
            // 
            // baseID
            // 
            resources.ApplyResources(this.baseID, "baseID");
            this.baseID.Name = "baseID";
            this.baseID.UseMnemonic = false;
            // 
            // use_offline_wad
            // 
            resources.ApplyResources(this.use_offline_wad, "use_offline_wad");
            this.use_offline_wad.Name = "use_offline_wad";
            this.use_offline_wad.Tag = "";
            this.use_offline_wad.UseVisualStyleBackColor = true;
            this.use_offline_wad.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // wad_filename
            // 
            resources.ApplyResources(this.wad_filename, "wad_filename");
            this.wad_filename.Name = "wad_filename";
            this.wad_filename.ReadOnly = true;
            // 
            // browsePatch
            // 
            this.browsePatch.RestoreDirectory = true;
            this.browsePatch.SupportMultiDottedExtensions = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.imageintpl);
            this.groupBox6.Controls.Add(this.image_stretch);
            this.groupBox6.Controls.Add(this.image_fit);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "image";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.editContentOptions);
            this.groupBox3.Controls.Add(this.injection_methods);
            this.groupBox3.Controls.Add(this.forwarder_root_device);
            this.groupBox3.Controls.Add(this.manual_type);
            this.groupBox3.Controls.Add(this.extra);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "injection_method";
            // 
            // editContentOptions
            // 
            this.editContentOptions.Image = global::FriishProduce.Properties.Resources.wrench;
            resources.ApplyResources(this.editContentOptions, "editContentOptions");
            this.editContentOptions.Name = "editContentOptions";
            this.editContentOptions.Tag = "";
            this.editContentOptions.UseCompatibleTextRendering = true;
            this.editContentOptions.UseVisualStyleBackColor = true;
            this.editContentOptions.Click += new System.EventHandler(this.openInjectorOptions);
            // 
            // forwarder_root_device
            // 
            this.forwarder_root_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.forwarder_root_device.FormattingEnabled = true;
            this.forwarder_root_device.Items.AddRange(new object[] {
            resources.GetString("forwarder_root_device.Items"),
            resources.GetString("forwarder_root_device.Items1")});
            resources.ApplyResources(this.forwarder_root_device, "forwarder_root_device");
            this.forwarder_root_device.Name = "forwarder_root_device";
            this.forwarder_root_device.Tag = "";
            // 
            // extra
            // 
            resources.ApplyResources(this.extra, "extra");
            this.extra.Name = "extra";
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // browseROM
            // 
            this.browseROM.RestoreDirectory = true;
            this.browseROM.SupportMultiDottedExtensions = true;
            // 
            // browseImage
            // 
            resources.ApplyResources(this.browseImage, "browseImage");
            this.browseImage.RestoreDirectory = true;
            this.browseImage.SupportMultiDottedExtensions = true;
            // 
            // import_rom
            // 
            resources.ApplyResources(this.import_rom, "import_rom");
            this.import_rom.Name = "import_rom";
            this.import_rom.Tag = "import_rom";
            this.import_rom.UseVisualStyleBackColor = true;
            this.import_rom.Click += new System.EventHandler(this.import_rom_Click);
            // 
            // rom_filename
            // 
            resources.ApplyResources(this.rom_filename, "rom_filename");
            this.rom_filename.Name = "rom_filename";
            this.rom_filename.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.import_patch);
            this.groupBox1.Controls.Add(this.import_rom);
            this.groupBox1.Controls.Add(this.checkImg1);
            this.groupBox1.Controls.Add(this.patch_filename);
            this.groupBox1.Controls.Add(this.rom_filename);
            this.groupBox1.Controls.Add(this.import_image);
            this.groupBox1.Controls.Add(this.checkImg2);
            this.groupBox1.Controls.Add(this.image_filename);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "main";
            // 
            // import_patch
            // 
            resources.ApplyResources(this.import_patch, "import_patch");
            this.import_patch.Name = "import_patch";
            this.import_patch.Tag = "import_patch";
            this.import_patch.UseVisualStyleBackColor = true;
            this.import_patch.Click += new System.EventHandler(this.import_patch_CheckedChanged);
            // 
            // checkImg1
            // 
            resources.ApplyResources(this.checkImg1, "checkImg1");
            this.checkImg1.Name = "checkImg1";
            this.checkImg1.TabStop = false;
            // 
            // patch_filename
            // 
            resources.ApplyResources(this.patch_filename, "patch_filename");
            this.patch_filename.Name = "patch_filename";
            this.patch_filename.ReadOnly = true;
            // 
            // image_filename
            // 
            resources.ApplyResources(this.image_filename, "image_filename");
            this.image_filename.Name = "image_filename";
            this.image_filename.ReadOnly = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.title_id_upper);
            this.groupBox9.Controls.Add(this.title_id_random);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            this.groupBox9.Tag = "title_id";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.banner);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.channel_title);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.groupBox8.Tag = "channel_name";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.region_list);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "region";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.video_modes);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            this.groupBox10.Tag = "video_mode";
            // 
            // video_modes
            // 
            this.video_modes.FormattingEnabled = true;
            this.video_modes.Items.AddRange(new object[] {
            resources.GetString("video_modes.Items"),
            resources.GetString("video_modes.Items1"),
            resources.GetString("video_modes.Items2"),
            resources.GetString("video_modes.Items3"),
            resources.GetString("video_modes.Items4"),
            resources.GetString("video_modes.Items5"),
            resources.GetString("video_modes.Items6"),
            resources.GetString("video_modes.Items7"),
            resources.GetString("video_modes.Items8")});
            resources.ApplyResources(this.video_modes, "video_modes");
            this.video_modes.Name = "video_modes";
            this.video_modes.SelectedIndexChanged += new System.EventHandler(this.Value_Changed);
            // 
            // tab_channel
            // 
            this.tab_channel.Controls.Add(this.groupBox9);
            this.tab_channel.Controls.Add(this.groupBox7);
            this.tab_channel.Controls.Add(this.groupBox8);
            this.tab_channel.Controls.Add(this.groupBox4);
            this.tab_channel.Controls.Add(this.groupBox10);
            resources.ApplyResources(this.tab_channel, "tab_channel");
            this.tab_channel.Name = "tab_channel";
            this.tab_channel.Tag = "tab_channel";
            this.tab_channel.UseVisualStyleBackColor = true;
            // 
            // tab_main
            // 
            this.tab_main.Controls.Add(this.groupBox3);
            this.tab_main.Controls.Add(this.groupBox1);
            this.tab_main.Controls.Add(this.groupBox2);
            this.tab_main.Controls.Add(this.groupBox6);
            this.tab_main.Controls.Add(this.groupBox5);
            resources.ApplyResources(this.tab_main, "tab_main");
            this.tab_main.Name = "tab_main";
            this.tab_main.Tag = "tab_main";
            this.tab_main.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_main);
            this.tabControl1.Controls.Add(this.tab_channel);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // bannerMenu
            // 
            this.bannerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.banner_customize,
            this.toolStripSeparator1,
            this.banner_sound});
            this.bannerMenu.Name = "bannerMenu";
            resources.ApplyResources(this.bannerMenu, "bannerMenu");
            // 
            // banner_customize
            // 
            this.banner_customize.Name = "banner_customize";
            resources.ApplyResources(this.banner_customize, "banner_customize");
            this.banner_customize.Tag = "banner_customize";
            this.banner_customize.Click += new System.EventHandler(this.banner_customize_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // banner_sound
            // 
            this.banner_sound.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.play_banner_sound,
            this.replace_banner_sound,
            this.restore_banner_sound});
            this.banner_sound.Name = "banner_sound";
            resources.ApplyResources(this.banner_sound, "banner_sound");
            this.banner_sound.Tag = "banner_sound";
            // 
            // play_banner_sound
            // 
            this.play_banner_sound.Name = "play_banner_sound";
            resources.ApplyResources(this.play_banner_sound, "play_banner_sound");
            this.play_banner_sound.Tag = "play_banner_sound";
            this.play_banner_sound.Click += new System.EventHandler(this.play_banner_sound_Click);
            // 
            // replace_banner_sound
            // 
            this.replace_banner_sound.Name = "replace_banner_sound";
            resources.ApplyResources(this.replace_banner_sound, "replace_banner_sound");
            this.replace_banner_sound.Tag = "replace_banner_sound";
            this.replace_banner_sound.Click += new System.EventHandler(this.replace_banner_sound_Click);
            // 
            // restore_banner_sound
            // 
            resources.ApplyResources(this.restore_banner_sound, "restore_banner_sound");
            this.restore_banner_sound.Name = "restore_banner_sound";
            this.restore_banner_sound.Tag = "restore_banner_sound";
            this.restore_banner_sound.Click += new System.EventHandler(this.restore_banner_sound_Click);
            // 
            // browseSound
            // 
            resources.ApplyResources(this.browseSound, "browseSound");
            this.browseSound.RestoreDirectory = true;
            this.browseSound.SupportMultiDottedExtensions = true;
            // 
            // imageintpl
            // 
            this.imageintpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.imageintpl, "imageintpl");
            this.imageintpl.FormattingEnabled = true;
            this.imageintpl.Items.AddRange(new object[] {
            resources.GetString("imageintpl.Items")});
            this.imageintpl.Name = "imageintpl";
            this.imageintpl.Tag = "image_interpolation_mode";
            // 
            // ProjectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Load += new System.EventHandler(this.Form_Shown);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveIcon_Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg1)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.tab_channel.ResumeLayout(false);
            this.tab_main.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.bannerMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox save_data_title;
        private System.Windows.Forms.TextBox channel_title;
        private System.Windows.Forms.TextBox title_id_upper;
        private System.Windows.Forms.PictureBox title_id_random;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox BaseRegion;
        private System.Windows.Forms.ContextMenuStrip baseRegionList;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.Label title_id;
        private System.Windows.Forms.Label base_name;
        internal System.Windows.Forms.OpenFileDialog browseInputWad;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton image_stretch;
        private System.Windows.Forms.RadioButton image_fit;
        private System.Windows.Forms.ComboBox injection_methods;
        internal System.Windows.Forms.OpenFileDialog browsePatch;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox region_list;
        private System.Windows.Forms.CheckBox fill_save_data;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog browseManual;
        private System.Windows.Forms.ComboBox manual_type;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox banner;
        private System.Windows.Forms.PictureBox SaveIcon_Panel;
        private System.Windows.Forms.Button editContentOptions;
        private System.Windows.Forms.OpenFileDialog browseROM;
        private System.Windows.Forms.OpenFileDialog browseImage;
        private System.Windows.Forms.Button import_image;
        private System.Windows.Forms.PictureBox checkImg2;
        private System.Windows.Forms.Button import_rom;
        private System.Windows.Forms.TextBox rom_filename;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox patch_filename;
        private System.Windows.Forms.Button import_patch;
        private System.Windows.Forms.ComboBox forwarder_root_device;
        private System.Windows.Forms.PictureBox checkImg1;
        private System.Windows.Forms.Button import_wad;
        private System.Windows.Forms.RadioButton use_online_wad;
        private System.Windows.Forms.RadioButton use_offline_wad;
        private System.Windows.Forms.PictureBox checkImg3;
        private System.Windows.Forms.TextBox wad_filename;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox image_filename;
        private System.Windows.Forms.Label extra;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ListBox video_modes;
        private System.Windows.Forms.TabPage tab_channel;
        private System.Windows.Forms.TabPage tab_main;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip bannerMenu;
        private System.Windows.Forms.ToolStripMenuItem banner_customize;
        private System.Windows.Forms.ToolStripMenuItem banner_sound;
        private System.Windows.Forms.ToolStripMenuItem play_banner_sound;
        private System.Windows.Forms.ToolStripMenuItem replace_banner_sound;
        private System.Windows.Forms.ToolStripMenuItem restore_banner_sound;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.OpenFileDialog browseSound;
        private System.Windows.Forms.ComboBox imageintpl;
    }
}