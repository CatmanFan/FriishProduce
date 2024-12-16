
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
            this.bannerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.play_banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.replace_banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.restore_banner_sound = new System.Windows.Forms.ToolStripMenuItem();
            this.browseSound = new System.Windows.Forms.OpenFileDialog();
            this.channel_name = new System.Windows.Forms.TextBox();
            this.regions = new System.Windows.Forms.ComboBox();
            this.title_id_random = new System.Windows.Forms.PictureBox();
            this.title_id_upper = new System.Windows.Forms.TextBox();
            this.video_modes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.video_mode = new System.Windows.Forms.Label();
            this.region = new System.Windows.Forms.Label();
            this.banner = new System.Windows.Forms.PictureBox();
            this.banner_details = new System.Windows.Forms.Button();
            this.banner_sound = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.edit_save_data = new System.Windows.Forms.Button();
            this.forwarder_root_device = new System.Windows.Forms.ComboBox();
            this.extra = new System.Windows.Forms.Label();
            this.manual_type = new System.Windows.Forms.ComboBox();
            this.injection_methods = new System.Windows.Forms.ComboBox();
            this.injection_method_options = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.multifile_software = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.use_offline_wad = new System.Windows.Forms.RadioButton();
            this.baseID = new System.Windows.Forms.Label();
            this.Base = new System.Windows.Forms.ComboBox();
            this.BaseRegion = new System.Windows.Forms.PictureBox();
            this.baseName = new System.Windows.Forms.Label();
            this.current_base = new System.Windows.Forms.Label();
            this.use_online_wad = new System.Windows.Forms.RadioButton();
            this.checkImg1 = new System.Windows.Forms.PictureBox();
            this.import_wad = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.import_image = new System.Windows.Forms.Button();
            this.image_interpolation_mode = new System.Windows.Forms.ComboBox();
            this.image_resize0 = new System.Windows.Forms.RadioButton();
            this.image_resize1 = new System.Windows.Forms.RadioButton();
            this.download_image = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.include_patch = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rom_label_filename = new System.Windows.Forms.Label();
            this.rom_label = new System.Windows.Forms.Label();
            this.bannerMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // bannerMenu
            // 
            this.bannerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.play_banner_sound,
            this.replace_banner_sound,
            this.restore_banner_sound});
            this.bannerMenu.Name = "bannerMenu";
            resources.ApplyResources(this.bannerMenu, "bannerMenu");
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
            // channel_name
            // 
            resources.ApplyResources(this.channel_name, "channel_name");
            this.channel_name.Name = "channel_name";
            this.channel_name.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // regions
            // 
            this.regions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regions.FormattingEnabled = true;
            resources.ApplyResources(this.regions, "regions");
            this.regions.Name = "regions";
            this.regions.SelectedIndexChanged += new System.EventHandler(this.RegionsList_SelectedIndexChanged);
            // 
            // title_id_random
            // 
            this.title_id_random.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.title_id_random, "title_id_random");
            this.title_id_random.Name = "title_id_random";
            this.title_id_random.TabStop = false;
            this.title_id_random.Click += new System.EventHandler(this.Random_Click);
            // 
            // title_id_upper
            // 
            this.title_id_upper.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.title_id_upper, "title_id_upper");
            this.title_id_upper.Name = "title_id_upper";
            this.title_id_upper.TextChanged += new System.EventHandler(this.TextBox_Changed);
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
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Tag = "channel_name";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Tag = "title_id";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.video_modes);
            this.groupBox8.Controls.Add(this.regions);
            this.groupBox8.Controls.Add(this.video_mode);
            this.groupBox8.Controls.Add(this.region);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.title_id_upper);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.channel_name);
            this.groupBox8.Controls.Add(this.title_id_random);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.groupBox8.Tag = "channel_metadata";
            // 
            // video_mode
            // 
            resources.ApplyResources(this.video_mode, "video_mode");
            this.video_mode.Name = "video_mode";
            this.video_mode.Tag = "video_mode";
            // 
            // region
            // 
            resources.ApplyResources(this.region, "region");
            this.region.Name = "region";
            this.region.Tag = "region";
            // 
            // banner
            // 
            this.banner.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.banner, "banner");
            this.banner.Name = "banner";
            this.banner.TabStop = false;
            // 
            // banner_details
            // 
            resources.ApplyResources(this.banner_details, "banner_details");
            this.banner_details.Name = "banner_details";
            this.banner_details.Tag = "banner_details";
            this.banner_details.UseVisualStyleBackColor = true;
            this.banner_details.Click += new System.EventHandler(this.banner_customize_Click);
            // 
            // banner_sound
            // 
            resources.ApplyResources(this.banner_sound, "banner_sound");
            this.banner_sound.Name = "banner_sound";
            this.banner_sound.Tag = "banner_sound";
            this.banner_sound.UseVisualStyleBackColor = true;
            this.banner_sound.Click += new System.EventHandler(this.banner_sound_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.banner);
            this.groupBox7.Controls.Add(this.banner_details);
            this.groupBox7.Controls.Add(this.edit_save_data);
            this.groupBox7.Controls.Add(this.banner_sound);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.groupBox7.Tag = "banner";
            // 
            // edit_save_data
            // 
            resources.ApplyResources(this.edit_save_data, "edit_save_data");
            this.edit_save_data.Name = "edit_save_data";
            this.edit_save_data.Tag = "edit_save_data";
            this.edit_save_data.UseVisualStyleBackColor = true;
            this.edit_save_data.Click += new System.EventHandler(this.edit_save_data_Click);
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
            // injection_method_options
            // 
            resources.ApplyResources(this.injection_method_options, "injection_method_options");
            this.injection_method_options.Name = "injection_method_options";
            this.injection_method_options.Tag = "injection_method_options";
            this.injection_method_options.UseVisualStyleBackColor = true;
            this.injection_method_options.Click += new System.EventHandler(this.openInjectorOptions);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.injection_method_options);
            this.groupBox3.Controls.Add(this.injection_methods);
            this.groupBox3.Controls.Add(this.forwarder_root_device);
            this.groupBox3.Controls.Add(this.manual_type);
            this.groupBox3.Controls.Add(this.multifile_software);
            this.groupBox3.Controls.Add(this.extra);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "injection_method";
            // 
            // multifile_software
            // 
            resources.ApplyResources(this.multifile_software, "multifile_software");
            this.multifile_software.Name = "multifile_software";
            this.multifile_software.Tag = "multifile_software";
            this.multifile_software.UseVisualStyleBackColor = true;
            this.multifile_software.CheckedChanged += new System.EventHandler(this.multifile_software_CheckedChanged);
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
            this.Base.BackColor = System.Drawing.SystemColors.Control;
            this.Base.DropDownHeight = 150;
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
            // current_base
            // 
            resources.ApplyResources(this.current_base, "current_base");
            this.current_base.Name = "current_base";
            this.current_base.Tag = "current_base";
            this.current_base.UseMnemonic = false;
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
            // import_wad
            // 
            resources.ApplyResources(this.import_wad, "import_wad");
            this.import_wad.Name = "import_wad";
            this.import_wad.Tag = "import_wad";
            this.import_wad.UseVisualStyleBackColor = true;
            this.import_wad.Click += new System.EventHandler(this.import_wad_Click);
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
            this.groupBox2.Controls.Add(this.current_base);
            this.groupBox2.Controls.Add(this.baseName);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "wad_base";
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
            // 
            // rom_label
            // 
            resources.ApplyResources(this.rom_label, "rom_label");
            this.rom_label.Name = "rom_label";
            this.rom_label.Tag = "rom_label";
            // 
            // ProjectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Load += new System.EventHandler(this.Form_Shown);
            this.bannerMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.title_id_random)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.banner)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip bannerMenu;
        private System.Windows.Forms.OpenFileDialog browseSound;
        private System.Windows.Forms.ToolStripMenuItem play_banner_sound;
        private System.Windows.Forms.ToolStripMenuItem replace_banner_sound;
        private System.Windows.Forms.ToolStripMenuItem restore_banner_sound;
        private System.Windows.Forms.TextBox channel_name;
        private System.Windows.Forms.ComboBox regions;
        private System.Windows.Forms.PictureBox title_id_random;
        private System.Windows.Forms.TextBox title_id_upper;
        private System.Windows.Forms.ComboBox video_modes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox banner;
        private System.Windows.Forms.Button banner_details;
        private System.Windows.Forms.Button banner_sound;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox forwarder_root_device;
        private System.Windows.Forms.Label extra;
        private System.Windows.Forms.ComboBox manual_type;
        private System.Windows.Forms.ComboBox injection_methods;
        private System.Windows.Forms.Button injection_method_options;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton use_offline_wad;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.PictureBox BaseRegion;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label current_base;
        private System.Windows.Forms.RadioButton use_online_wad;
        private System.Windows.Forms.PictureBox checkImg1;
        private System.Windows.Forms.Button import_wad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button import_image;
        private System.Windows.Forms.ComboBox image_interpolation_mode;
        private System.Windows.Forms.RadioButton image_resize0;
        private System.Windows.Forms.RadioButton image_resize1;
        private System.Windows.Forms.Button download_image;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox include_patch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label video_mode;
        private System.Windows.Forms.Label region;
        private System.Windows.Forms.Button edit_save_data;
        private System.Windows.Forms.Label rom_label_filename;
        private System.Windows.Forms.Label rom_label;
        private System.Windows.Forms.CheckBox multifile_software;
    }
}