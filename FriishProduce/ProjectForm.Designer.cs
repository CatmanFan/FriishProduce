
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
            this.imageintpl = new System.Windows.Forms.ComboBox();
            this.image_fit = new System.Windows.Forms.RadioButton();
            this.image_stretch = new System.Windows.Forms.RadioButton();
            this.browseInputWad = new System.Windows.Forms.OpenFileDialog();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.SaveIcon_Panel = new System.Windows.Forms.PictureBox();
            this.fill_save_data = new System.Windows.Forms.CheckBox();
            this.save_data_title = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.manual_type_list = new System.Windows.Forms.ComboBox();
            this.injection_methods = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.video_modes = new System.Windows.Forms.ComboBox();
            this.region_list = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.video_mode = new System.Windows.Forms.Label();
            this.channel_title = new System.Windows.Forms.TextBox();
            this.title_id_upper = new System.Windows.Forms.TextBox();
            this.title_id = new System.Windows.Forms.Label();
            this.title_id_random = new System.Windows.Forms.PictureBox();
            this.channel_name = new System.Windows.Forms.Label();
            this.region = new System.Windows.Forms.Label();
            this.hasImage = new System.Windows.Forms.PictureBox();
            this.import_image = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.players = new System.Windows.Forms.NumericUpDown();
            this.released = new System.Windows.Forms.NumericUpDown();
            this.banner_title = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bannerPreview = new System.Windows.Forms.PictureBox();
            this.wad_base = new System.Windows.Forms.GroupBox();
            this.import_wad = new System.Windows.Forms.Button();
            this.hasWad = new System.Windows.Forms.PictureBox();
            this.wad_filename = new System.Windows.Forms.TextBox();
            this.use_online_wad = new System.Windows.Forms.RadioButton();
            this.base_name = new System.Windows.Forms.Label();
            this.baseName = new System.Windows.Forms.Label();
            this.BaseRegion = new System.Windows.Forms.PictureBox();
            this.Base = new System.Windows.Forms.ComboBox();
            this.title_id_2 = new System.Windows.Forms.Label();
            this.baseID = new System.Windows.Forms.Label();
            this.use_offline_wad = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.browsePatch = new System.Windows.Forms.OpenFileDialog();
            this.browseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.editContentOptions = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.browseROM = new System.Windows.Forms.OpenFileDialog();
            this.browseImage = new System.Windows.Forms.OpenFileDialog();
            this.import_rom = new System.Windows.Forms.Button();
            this.rom_filename = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hasRom = new System.Windows.Forms.PictureBox();
            this.hasPatch = new System.Windows.Forms.PictureBox();
            this.import_patch = new System.Windows.Forms.Button();
            this.patch_filename = new System.Windows.Forms.TextBox();
            this.forwarder_root_device = new System.Windows.Forms.GroupBox();
            this.forwarder_type = new System.Windows.Forms.ComboBox();
            this.manual_type = new System.Windows.Forms.GroupBox();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveIcon_Panel)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hasImage)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.released)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPreview)).BeginInit();
            this.wad_base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hasWad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hasRom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hasPatch)).BeginInit();
            this.forwarder_root_device.SuspendLayout();
            this.manual_type.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseRegionList
            // 
            resources.ApplyResources(this.baseRegionList, "baseRegionList");
            this.baseRegionList.Name = "WADRegion";
            this.baseRegionList.ShowCheckMargin = true;
            this.baseRegionList.ShowImageMargin = false;
            // 
            // imageintpl
            // 
            this.imageintpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageintpl.FormattingEnabled = true;
            this.imageintpl.Items.AddRange(new object[] {
            resources.GetString("imageintpl.Items")});
            resources.ApplyResources(this.imageintpl, "imageintpl");
            this.imageintpl.Name = "imageintpl";
            this.imageintpl.Tag = "image_interpolation_mode";
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
            // browseInputWad
            // 
            resources.ApplyResources(this.browseInputWad, "browseInputWad");
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.SaveIcon_Panel);
            this.groupBox7.Controls.Add(this.fill_save_data);
            this.groupBox7.Controls.Add(this.save_data_title);
            this.groupBox7.Controls.Add(this.label16);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.groupBox7.Tag = "save_data";
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
            this.save_data_title.Tag = "24";
            this.save_data_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.save_data_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.label16.Tag = "none";
            // 
            // manual_type_list
            // 
            this.manual_type_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manual_type_list.FormattingEnabled = true;
            resources.ApplyResources(this.manual_type_list, "manual_type_list");
            this.manual_type_list.Name = "manual_type_list";
            this.manual_type_list.Tag = "manual_type";
            this.manual_type_list.SelectedIndexChanged += new System.EventHandler(this.CustomManual_CheckedChanged);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.video_modes);
            this.groupBox3.Controls.Add(this.region_list);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.video_mode);
            this.groupBox3.Controls.Add(this.channel_title);
            this.groupBox3.Controls.Add(this.title_id_upper);
            this.groupBox3.Controls.Add(this.title_id);
            this.groupBox3.Controls.Add(this.title_id_random);
            this.groupBox3.Controls.Add(this.channel_name);
            this.groupBox3.Controls.Add(this.region);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "channel";
            // 
            // video_modes
            // 
            this.video_modes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            // region_list
            // 
            this.region_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region_list.FormattingEnabled = true;
            resources.ApplyResources(this.region_list, "region_list");
            this.region_list.Name = "region_list";
            this.region_list.SelectedIndexChanged += new System.EventHandler(this.RegionsList_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // video_mode
            // 
            resources.ApplyResources(this.video_mode, "video_mode");
            this.video_mode.Name = "video_mode";
            this.video_mode.Tag = "video_mode";
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
            // title_id
            // 
            resources.ApplyResources(this.title_id, "title_id");
            this.title_id.Name = "title_id";
            this.title_id.Tag = "title_id";
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
            // channel_name
            // 
            resources.ApplyResources(this.channel_name, "channel_name");
            this.channel_name.Name = "channel_name";
            this.channel_name.Tag = "channel_name";
            // 
            // region
            // 
            resources.ApplyResources(this.region, "region");
            this.region.Name = "region";
            this.region.Tag = "region";
            // 
            // hasImage
            // 
            resources.ApplyResources(this.hasImage, "hasImage");
            this.hasImage.Name = "hasImage";
            this.hasImage.TabStop = false;
            // 
            // import_image
            // 
            resources.ApplyResources(this.import_image, "import_image");
            this.import_image.Name = "import_image";
            this.import_image.Tag = "import_image";
            this.import_image.UseVisualStyleBackColor = true;
            this.import_image.Click += new System.EventHandler(this.import_image_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.players);
            this.groupBox6.Controls.Add(this.released);
            this.groupBox6.Controls.Add(this.banner_title);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label8);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "banner";
            // 
            // players
            // 
            resources.ApplyResources(this.players, "players");
            this.players.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.players.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.players.Name = "players";
            this.players.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.players.ValueChanged += new System.EventHandler(this.Value_Changed);
            // 
            // released
            // 
            resources.ApplyResources(this.released, "released");
            this.released.Maximum = new decimal(new int[] {
            2029,
            0,
            0,
            0});
            this.released.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.released.Name = "released";
            this.released.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.released.ValueChanged += new System.EventHandler(this.Value_Changed);
            // 
            // banner_title
            // 
            resources.ApplyResources(this.banner_title, "banner_title");
            this.banner_title.Name = "banner_title";
            this.banner_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.banner_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
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
            // bannerPreview
            // 
            this.bannerPreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bannerPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.bannerPreview, "bannerPreview");
            this.bannerPreview.Name = "bannerPreview";
            this.bannerPreview.TabStop = false;
            // 
            // wad_base
            // 
            this.wad_base.Controls.Add(this.import_wad);
            this.wad_base.Controls.Add(this.hasWad);
            this.wad_base.Controls.Add(this.wad_filename);
            this.wad_base.Controls.Add(this.use_online_wad);
            this.wad_base.Controls.Add(this.base_name);
            this.wad_base.Controls.Add(this.baseName);
            this.wad_base.Controls.Add(this.BaseRegion);
            this.wad_base.Controls.Add(this.Base);
            this.wad_base.Controls.Add(this.title_id_2);
            this.wad_base.Controls.Add(this.baseID);
            this.wad_base.Controls.Add(this.use_offline_wad);
            this.wad_base.Controls.Add(this.pictureBox2);
            resources.ApplyResources(this.wad_base, "wad_base");
            this.wad_base.Name = "wad_base";
            this.wad_base.TabStop = false;
            this.wad_base.Tag = "wad_base";
            // 
            // import_wad
            // 
            resources.ApplyResources(this.import_wad, "import_wad");
            this.import_wad.Name = "import_wad";
            this.import_wad.Tag = "import_wad";
            this.import_wad.UseVisualStyleBackColor = true;
            this.import_wad.Click += new System.EventHandler(this.import_wad_Click);
            // 
            // hasWad
            // 
            resources.ApplyResources(this.hasWad, "hasWad");
            this.hasWad.Name = "hasWad";
            this.hasWad.TabStop = false;
            // 
            // wad_filename
            // 
            resources.ApplyResources(this.wad_filename, "wad_filename");
            this.wad_filename.Name = "wad_filename";
            this.wad_filename.ReadOnly = true;
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
            // title_id_2
            // 
            resources.ApplyResources(this.title_id_2, "title_id_2");
            this.title_id_2.Name = "title_id_2";
            this.title_id_2.Tag = "title_id";
            this.title_id_2.UseMnemonic = false;
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
            // browsePatch
            // 
            this.browsePatch.RestoreDirectory = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.image_stretch);
            this.groupBox5.Controls.Add(this.imageintpl);
            this.groupBox5.Controls.Add(this.image_fit);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "image_interpolation_mode";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.editContentOptions);
            this.groupBox4.Controls.Add(this.injection_methods);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "injection_method";
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
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // browseROM
            // 
            this.browseROM.RestoreDirectory = true;
            // 
            // browseImage
            // 
            resources.ApplyResources(this.browseImage, "browseImage");
            this.browseImage.RestoreDirectory = true;
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
            this.groupBox1.Controls.Add(this.hasRom);
            this.groupBox1.Controls.Add(this.hasPatch);
            this.groupBox1.Controls.Add(this.import_patch);
            this.groupBox1.Controls.Add(this.patch_filename);
            this.groupBox1.Controls.Add(this.rom_filename);
            this.groupBox1.Controls.Add(this.import_rom);
            this.groupBox1.Controls.Add(this.import_image);
            this.groupBox1.Controls.Add(this.hasImage);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "main";
            // 
            // hasRom
            // 
            resources.ApplyResources(this.hasRom, "hasRom");
            this.hasRom.Name = "hasRom";
            this.hasRom.TabStop = false;
            // 
            // hasPatch
            // 
            resources.ApplyResources(this.hasPatch, "hasPatch");
            this.hasPatch.Name = "hasPatch";
            this.hasPatch.TabStop = false;
            // 
            // import_patch
            // 
            resources.ApplyResources(this.import_patch, "import_patch");
            this.import_patch.Name = "import_patch";
            this.import_patch.Tag = "import_patch";
            this.import_patch.UseVisualStyleBackColor = true;
            this.import_patch.Click += new System.EventHandler(this.import_patch_CheckedChanged);
            // 
            // patch_filename
            // 
            resources.ApplyResources(this.patch_filename, "patch_filename");
            this.patch_filename.Name = "patch_filename";
            this.patch_filename.ReadOnly = true;
            // 
            // forwarder_root_device
            // 
            this.forwarder_root_device.Controls.Add(this.forwarder_type);
            resources.ApplyResources(this.forwarder_root_device, "forwarder_root_device");
            this.forwarder_root_device.Name = "forwarder_root_device";
            this.forwarder_root_device.TabStop = false;
            this.forwarder_root_device.Tag = "forwarder_root_device";
            // 
            // forwarder_type
            // 
            this.forwarder_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.forwarder_type.FormattingEnabled = true;
            this.forwarder_type.Items.AddRange(new object[] {
            resources.GetString("forwarder_type.Items"),
            resources.GetString("forwarder_type.Items1")});
            resources.ApplyResources(this.forwarder_type, "forwarder_type");
            this.forwarder_type.Name = "forwarder_type";
            this.forwarder_type.Tag = "";
            // 
            // manual_type
            // 
            this.manual_type.Controls.Add(this.manual_type_list);
            resources.ApplyResources(this.manual_type, "manual_type");
            this.manual_type.Name = "manual_type";
            this.manual_type.TabStop = false;
            this.manual_type.Tag = "manual_type";
            // 
            // ProjectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.wad_base);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.bannerPreview);
            this.Controls.Add(this.forwarder_root_device);
            this.Controls.Add(this.manual_type);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Load += new System.EventHandler(this.Form_Shown);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveIcon_Panel)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hasImage)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.released)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPreview)).EndInit();
            this.wad_base.ResumeLayout(false);
            this.wad_base.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hasWad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hasRom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hasPatch)).EndInit();
            this.forwarder_root_device.ResumeLayout(false);
            this.manual_type.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox save_data_title;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox banner_title;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label channel_name;
        private System.Windows.Forms.TextBox channel_title;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label title_id;
        private System.Windows.Forms.TextBox title_id_upper;
        private System.Windows.Forms.ComboBox imageintpl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox title_id_random;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.GroupBox wad_base;
        private System.Windows.Forms.PictureBox BaseRegion;
        private System.Windows.Forms.ContextMenuStrip baseRegionList;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.Label title_id_2;
        private System.Windows.Forms.Label base_name;
        private System.Windows.Forms.NumericUpDown released;
        private System.Windows.Forms.NumericUpDown players;
        internal System.Windows.Forms.OpenFileDialog browseInputWad;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton image_stretch;
        private System.Windows.Forms.RadioButton image_fit;
        private System.Windows.Forms.ComboBox injection_methods;
        internal System.Windows.Forms.OpenFileDialog browsePatch;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label region;
        private System.Windows.Forms.ComboBox region_list;
        private System.Windows.Forms.CheckBox fill_save_data;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog browseManual;
        private System.Windows.Forms.ComboBox manual_type_list;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox bannerPreview;
        private System.Windows.Forms.PictureBox SaveIcon_Panel;
        private System.Windows.Forms.Button editContentOptions;
        private System.Windows.Forms.ComboBox video_modes;
        private System.Windows.Forms.Label video_mode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog browseROM;
        private System.Windows.Forms.OpenFileDialog browseImage;
        private System.Windows.Forms.Button import_image;
        private System.Windows.Forms.PictureBox hasImage;
        private System.Windows.Forms.Button import_rom;
        private System.Windows.Forms.TextBox rom_filename;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox forwarder_root_device;
        private System.Windows.Forms.GroupBox manual_type;
        private System.Windows.Forms.TextBox patch_filename;
        private System.Windows.Forms.Button import_patch;
        private System.Windows.Forms.ComboBox forwarder_type;
        private System.Windows.Forms.PictureBox hasRom;
        private System.Windows.Forms.PictureBox hasPatch;
        private System.Windows.Forms.Button import_wad;
        private System.Windows.Forms.RadioButton use_online_wad;
        private System.Windows.Forms.RadioButton use_offline_wad;
        private System.Windows.Forms.PictureBox hasWad;
        private System.Windows.Forms.TextBox wad_filename;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}