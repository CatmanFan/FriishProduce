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
            this.browseInputWad = new System.Windows.Forms.OpenFileDialog();
            this.browsePatch = new System.Windows.Forms.OpenFileDialog();
            this.browseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.browseROM = new System.Windows.Forms.OpenFileDialog();
            this.browseImage = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox5 = new FriishProduce.GroupBoxEx();
            this.banner = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new FriishProduce.GroupBoxEx();
            this.injection_method_options = new System.Windows.Forms.Button();
            this.wiiu_display_l = new System.Windows.Forms.Label();
            this.edit_save_data = new System.Windows.Forms.Button();
            this.wiiu_display = new System.Windows.Forms.ComboBox();
            this.injection_method_help = new System.Windows.Forms.PictureBox();
            this.injection_methods = new System.Windows.Forms.ComboBox();
            this.multifile_software = new System.Windows.Forms.CheckBox();
            this.extra = new System.Windows.Forms.Label();
            this.manual_type = new System.Windows.Forms.ComboBox();
            this.forwarder_root_device = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new FriishProduce.GroupBoxEx();
            this.title_id = new System.Windows.Forms.TextBox();
            this.video_mode = new System.Windows.Forms.ComboBox();
            this.region = new System.Windows.Forms.ComboBox();
            this.video_mode_l = new System.Windows.Forms.Label();
            this.region_l = new System.Windows.Forms.Label();
            this.title_id_l = new System.Windows.Forms.Label();
            this.channel_name_l = new System.Windows.Forms.Label();
            this.channel_name = new System.Windows.Forms.TextBox();
            this.title_id_random = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.use_offline_wad = new System.Windows.Forms.RadioButton();
            this.baseID = new System.Windows.Forms.Label();
            this.Base = new System.Windows.Forms.ComboBox();
            this.BaseRegion = new System.Windows.Forms.PictureBox();
            this.baseName = new System.Windows.Forms.Label();
            this.current_wad = new System.Windows.Forms.Label();
            this.use_online_wad = new System.Windows.Forms.RadioButton();
            this.checkImg1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new FriishProduce.GroupBoxEx();
            this.import_wad = new System.Windows.Forms.Button();
            this.using_default_wad = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.import_image = new System.Windows.Forms.Button();
            this.image_interpolation_mode = new System.Windows.Forms.ComboBox();
            this.image_resize0 = new System.Windows.Forms.RadioButton();
            this.image_resize1 = new System.Windows.Forms.RadioButton();
            this.download_image = new System.Windows.Forms.Button();
            this.groupBox6 = new FriishProduce.GroupBoxEx();
            this.include_patch = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new FriishProduce.GroupBoxEx();
            this.rom_label_filename = new System.Windows.Forms.Label();
            this.rom_label = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.injection_method_help)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseRegionList
            // 
            resources.ApplyResources(this.baseRegionList, "baseRegionList");
            this.baseRegionList.Name = "WADRegion";
            this.baseRegionList.ShowCheckMargin = true;
            this.baseRegionList.ShowImageMargin = false;
            // 
            // browseInputWad
            // 
            resources.ApplyResources(this.browseInputWad, "browseInputWad");
            this.browseInputWad.SupportMultiDottedExtensions = true;
            // 
            // browsePatch
            // 
            this.browsePatch.RestoreDirectory = true;
            this.browsePatch.SupportMultiDottedExtensions = true;
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
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.saveToWAD);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.saveToWAD_UpdateProgress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.banner);
            this.groupBox5.Flat = false;
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "banner";
            // 
            // banner
            // 
            this.banner.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.banner.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.banner, "banner");
            this.banner.Name = "banner";
            this.banner.TabStop = false;
            this.banner.Click += new System.EventHandler(this.banner_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.injection_method_options);
            this.groupBox3.Controls.Add(this.wiiu_display_l);
            this.groupBox3.Controls.Add(this.edit_save_data);
            this.groupBox3.Controls.Add(this.wiiu_display);
            this.groupBox3.Controls.Add(this.injection_method_help);
            this.groupBox3.Controls.Add(this.injection_methods);
            this.groupBox3.Controls.Add(this.multifile_software);
            this.groupBox3.Controls.Add(this.extra);
            this.groupBox3.Controls.Add(this.manual_type);
            this.groupBox3.Controls.Add(this.forwarder_root_device);
            this.groupBox3.Flat = false;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "injection_method";
            // 
            // injection_method_options
            // 
            resources.ApplyResources(this.injection_method_options, "injection_method_options");
            this.injection_method_options.Name = "injection_method_options";
            this.injection_method_options.Tag = "injection_method_options";
            this.injection_method_options.UseVisualStyleBackColor = true;
            this.injection_method_options.Click += new System.EventHandler(this.openInjectorOptions);
            // 
            // wiiu_display_l
            // 
            resources.ApplyResources(this.wiiu_display_l, "wiiu_display_l");
            this.wiiu_display_l.Name = "wiiu_display_l";
            this.wiiu_display_l.Tag = "wiiu_display";
            // 
            // edit_save_data
            // 
            resources.ApplyResources(this.edit_save_data, "edit_save_data");
            this.edit_save_data.Name = "edit_save_data";
            this.edit_save_data.Tag = "edit_save_data";
            this.edit_save_data.UseVisualStyleBackColor = true;
            this.edit_save_data.Click += new System.EventHandler(this.edit_save_data_Click);
            // 
            // wiiu_display
            // 
            this.wiiu_display.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wiiu_display.FormattingEnabled = true;
            resources.ApplyResources(this.wiiu_display, "wiiu_display");
            this.wiiu_display.Name = "wiiu_display";
            this.wiiu_display.Tag = "wiiu_display";
            this.wiiu_display.SelectedIndexChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // injection_method_help
            // 
            this.injection_method_help.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.injection_method_help, "injection_method_help");
            this.injection_method_help.Name = "injection_method_help";
            this.injection_method_help.TabStop = false;
            this.injection_method_help.Click += new System.EventHandler(this.injection_method_help_Click);
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
            // multifile_software
            // 
            resources.ApplyResources(this.multifile_software, "multifile_software");
            this.multifile_software.Name = "multifile_software";
            this.multifile_software.Tag = "multifile_software";
            this.multifile_software.UseVisualStyleBackColor = true;
            this.multifile_software.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // extra
            // 
            resources.ApplyResources(this.extra, "extra");
            this.extra.Name = "extra";
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
            this.forwarder_root_device.SelectedIndexChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.title_id);
            this.groupBox4.Controls.Add(this.video_mode);
            this.groupBox4.Controls.Add(this.region);
            this.groupBox4.Controls.Add(this.video_mode_l);
            this.groupBox4.Controls.Add(this.region_l);
            this.groupBox4.Controls.Add(this.title_id_l);
            this.groupBox4.Controls.Add(this.channel_name_l);
            this.groupBox4.Controls.Add(this.channel_name);
            this.groupBox4.Controls.Add(this.title_id_random);
            this.groupBox4.Flat = false;
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "channel_metadata";
            // 
            // title_id
            // 
            this.title_id.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.title_id, "title_id");
            this.title_id.Name = "title_id";
            this.title_id.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // video_mode
            // 
            this.video_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.video_mode.FormattingEnabled = true;
            this.video_mode.Items.AddRange(new object[] {
            resources.GetString("video_mode.Items"),
            resources.GetString("video_mode.Items1"),
            resources.GetString("video_mode.Items2"),
            resources.GetString("video_mode.Items3"),
            resources.GetString("video_mode.Items4"),
            resources.GetString("video_mode.Items5"),
            resources.GetString("video_mode.Items6"),
            resources.GetString("video_mode.Items7"),
            resources.GetString("video_mode.Items8")});
            resources.ApplyResources(this.video_mode, "video_mode");
            this.video_mode.Name = "video_mode";
            // 
            // region
            // 
            this.region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region.FormattingEnabled = true;
            resources.ApplyResources(this.region, "region");
            this.region.Name = "region";
            this.region.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // video_mode_l
            // 
            resources.ApplyResources(this.video_mode_l, "video_mode_l");
            this.video_mode_l.Name = "video_mode_l";
            this.video_mode_l.Tag = "video_mode";
            // 
            // region_l
            // 
            resources.ApplyResources(this.region_l, "region_l");
            this.region_l.Name = "region_l";
            this.region_l.Tag = "region";
            // 
            // title_id_l
            // 
            resources.ApplyResources(this.title_id_l, "title_id_l");
            this.title_id_l.Name = "title_id_l";
            this.title_id_l.Tag = "title_id";
            // 
            // channel_name_l
            // 
            resources.ApplyResources(this.channel_name_l, "channel_name_l");
            this.channel_name_l.Name = "channel_name_l";
            this.channel_name_l.Tag = "channel_name";
            // 
            // channel_name
            // 
            resources.ApplyResources(this.channel_name, "channel_name");
            this.channel_name.Name = "channel_name";
            this.channel_name.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // title_id_random
            // 
            this.title_id_random.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.title_id_random, "title_id_random");
            this.title_id_random.Name = "title_id_random";
            this.title_id_random.TabStop = false;
            this.title_id_random.Click += new System.EventHandler(this.Random_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // use_offline_wad
            // 
            resources.ApplyResources(this.use_offline_wad, "use_offline_wad");
            this.use_offline_wad.Name = "use_offline_wad";
            this.use_offline_wad.Tag = "";
            this.use_offline_wad.UseVisualStyleBackColor = true;
            this.use_offline_wad.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // baseID
            // 
            resources.ApplyResources(this.baseID, "baseID");
            this.baseID.Name = "baseID";
            this.baseID.UseMnemonic = false;
            // 
            // Base
            // 
            this.Base.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Base.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Base, "Base");
            this.Base.FormattingEnabled = true;
            this.Base.Name = "Base";
            this.Base.SelectedIndexChanged += new System.EventHandler(this.Base_SelectedIndexChanged);
            // 
            // BaseRegion
            // 
            this.BaseRegion.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BaseRegion.ContextMenuStrip = this.baseRegionList;
            resources.ApplyResources(this.BaseRegion, "BaseRegion");
            this.BaseRegion.Name = "BaseRegion";
            this.BaseRegion.TabStop = false;
            this.BaseRegion.Click += new System.EventHandler(this.WADRegion_Click);
            // 
            // baseName
            // 
            resources.ApplyResources(this.baseName, "baseName");
            this.baseName.Name = "baseName";
            this.baseName.UseMnemonic = false;
            // 
            // current_wad
            // 
            resources.ApplyResources(this.current_wad, "current_wad");
            this.current_wad.Name = "current_wad";
            this.current_wad.Tag = "current_wad";
            this.current_wad.UseMnemonic = false;
            // 
            // use_online_wad
            // 
            resources.ApplyResources(this.use_online_wad, "use_online_wad");
            this.use_online_wad.Name = "use_online_wad";
            this.use_online_wad.Tag = "use_online_wad";
            this.use_online_wad.UseVisualStyleBackColor = true;
            this.use_online_wad.CheckedChanged += new System.EventHandler(this.OpenWAD_CheckedChanged);
            // 
            // checkImg1
            // 
            this.checkImg1.Image = global::FriishProduce.Properties.Resources.cross;
            resources.ApplyResources(this.checkImg1, "checkImg1");
            this.checkImg1.Name = "checkImg1";
            this.checkImg1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.import_wad);
            this.groupBox2.Controls.Add(this.checkImg1);
            this.groupBox2.Controls.Add(this.use_online_wad);
            this.groupBox2.Controls.Add(this.BaseRegion);
            this.groupBox2.Controls.Add(this.Base);
            this.groupBox2.Controls.Add(this.use_offline_wad);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.baseID);
            this.groupBox2.Controls.Add(this.current_wad);
            this.groupBox2.Controls.Add(this.baseName);
            this.groupBox2.Controls.Add(this.using_default_wad);
            this.groupBox2.Flat = false;
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
            // using_default_wad
            // 
            resources.ApplyResources(this.using_default_wad, "using_default_wad");
            this.using_default_wad.Name = "using_default_wad";
            this.using_default_wad.Tag = "using_default_wad";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // import_image
            // 
            resources.ApplyResources(this.import_image, "import_image");
            this.import_image.Name = "import_image";
            this.import_image.Tag = "import_image";
            this.import_image.UseVisualStyleBackColor = true;
            this.import_image.Click += new System.EventHandler(this.import_image_Click);
            // 
            // image_interpolation_mode
            // 
            this.image_interpolation_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.image_interpolation_mode, "image_interpolation_mode");
            this.image_interpolation_mode.FormattingEnabled = true;
            this.image_interpolation_mode.Items.AddRange(new object[] {
            resources.GetString("image_interpolation_mode.Items")});
            this.image_interpolation_mode.Name = "image_interpolation_mode";
            this.image_interpolation_mode.Tag = "image_interpolation_mode";
            // 
            // image_resize0
            // 
            resources.ApplyResources(this.image_resize0, "image_resize0");
            this.image_resize0.Name = "image_resize0";
            this.image_resize0.TabStop = true;
            this.image_resize0.Tag = "image_resize0";
            this.image_resize0.UseVisualStyleBackColor = true;
            this.image_resize0.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // image_resize1
            // 
            resources.ApplyResources(this.image_resize1, "image_resize1");
            this.image_resize1.Name = "image_resize1";
            this.image_resize1.TabStop = true;
            this.image_resize1.Tag = "image_resize1";
            this.image_resize1.UseVisualStyleBackColor = true;
            this.image_resize1.CheckedChanged += new System.EventHandler(this.SwitchAspectRatio);
            // 
            // download_image
            // 
            resources.ApplyResources(this.download_image, "download_image");
            this.download_image.Name = "download_image";
            this.download_image.Tag = "download_image";
            this.download_image.UseVisualStyleBackColor = true;
            this.download_image.Click += new System.EventHandler(this.download_image_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.download_image);
            this.groupBox6.Controls.Add(this.image_resize1);
            this.groupBox6.Controls.Add(this.image_resize0);
            this.groupBox6.Controls.Add(this.image_interpolation_mode);
            this.groupBox6.Controls.Add(this.import_image);
            this.groupBox6.Controls.Add(this.pictureBox1);
            this.groupBox6.Flat = false;
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "image";
            // 
            // include_patch
            // 
            resources.ApplyResources(this.include_patch, "include_patch");
            this.include_patch.Name = "include_patch";
            this.include_patch.Tag = "include_patch";
            this.include_patch.UseVisualStyleBackColor = true;
            this.include_patch.CheckedChanged += new System.EventHandler(this.include_patch_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rom_label_filename);
            this.groupBox1.Controls.Add(this.rom_label);
            this.groupBox1.Controls.Add(this.include_patch);
            this.groupBox1.Flat = false;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "main";
            // 
            // rom_label_filename
            // 
            resources.ApplyResources(this.rom_label_filename, "rom_label_filename");
            this.rom_label_filename.Name = "rom_label_filename";
            this.rom_label_filename.Tag = "";
            this.rom_label_filename.UseMnemonic = false;
            // 
            // rom_label
            // 
            resources.ApplyResources(this.rom_label, "rom_label");
            this.rom_label.Name = "rom_label";
            this.rom_label.Tag = "rom_label";
            // 
            // ProjectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Load += new System.EventHandler(this.Form_Shown);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.banner)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.injection_method_help)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkImg1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip baseRegionList;
        internal System.Windows.Forms.OpenFileDialog browseInputWad;
        internal System.Windows.Forms.OpenFileDialog browsePatch;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog browseManual;
        private System.Windows.Forms.OpenFileDialog browseROM;
        private System.Windows.Forms.OpenFileDialog browseImage;
        private System.Windows.Forms.TextBox channel_name;
        private System.Windows.Forms.ComboBox region;
        private System.Windows.Forms.PictureBox title_id_random;
        private System.Windows.Forms.TextBox title_id;
        private System.Windows.Forms.ComboBox video_mode;
        private System.Windows.Forms.Label channel_name_l;
        private System.Windows.Forms.Label title_id_l;
        private GroupBoxEx groupBox4;
        private System.Windows.Forms.PictureBox banner;
        private GroupBoxEx groupBox5;
        private System.Windows.Forms.ComboBox forwarder_root_device;
        private System.Windows.Forms.Label extra;
        private System.Windows.Forms.ComboBox manual_type;
        private System.Windows.Forms.ComboBox injection_methods;
        private System.Windows.Forms.Button injection_method_options;
        private GroupBoxEx groupBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton use_offline_wad;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.PictureBox BaseRegion;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label current_wad;
        private System.Windows.Forms.PictureBox checkImg1;
        private GroupBoxEx groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button import_image;
        private System.Windows.Forms.ComboBox image_interpolation_mode;
        private System.Windows.Forms.RadioButton image_resize0;
        private System.Windows.Forms.RadioButton image_resize1;
        private System.Windows.Forms.Button download_image;
        private GroupBoxEx groupBox6;
        private System.Windows.Forms.CheckBox include_patch;
        private GroupBoxEx groupBox1;
        private System.Windows.Forms.Label video_mode_l;
        private System.Windows.Forms.Label region_l;
        private System.Windows.Forms.Button edit_save_data;
        private System.Windows.Forms.Label rom_label_filename;
        private System.Windows.Forms.Label rom_label;
        private System.Windows.Forms.CheckBox multifile_software;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button import_wad;
        private System.Windows.Forms.PictureBox injection_method_help;
        private System.Windows.Forms.Label using_default_wad;
        internal System.Windows.Forms.RadioButton use_online_wad;
        private System.Windows.Forms.ComboBox wiiu_display;
        private System.Windows.Forms.Label wiiu_display_l;
    }
}
