
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
            this.label12 = new System.Windows.Forms.Label();
            this.imageintpl = new System.Windows.Forms.ComboBox();
            this.image_fit = new System.Windows.Forms.RadioButton();
            this.image_stretch = new System.Windows.Forms.RadioButton();
            this.browseInputWad = new System.Windows.Forms.OpenFileDialog();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.LinkSaveData = new System.Windows.Forms.CheckBox();
            this.SaveIcon_Panel = new System.Windows.Forms.Panel();
            this.save_data_title = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.FStorage_SD = new System.Windows.Forms.RadioButton();
            this.FStorage_USB = new System.Windows.Forms.RadioButton();
            this.injection_method = new System.Windows.Forms.Label();
            this.toggleSwitchL1 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.manual_type_list = new System.Windows.Forms.ComboBox();
            this.injection_methods = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Patch = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tid = new System.Windows.Forms.TextBox();
            this.title_id_2 = new System.Windows.Forms.Label();
            this.Random = new System.Windows.Forms.PictureBox();
            this.channel_title = new System.Windows.Forms.TextBox();
            this.channel_name = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TargetRegion = new System.Windows.Forms.ComboBox();
            this.region = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.software_name = new System.Windows.Forms.Label();
            this.filename = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ShowBannerPreview = new System.Windows.Forms.Button();
            this.players = new System.Windows.Forms.NumericUpDown();
            this.released = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.banner_title = new System.Windows.Forms.TextBox();
            this.wad_base = new System.Windows.Forms.GroupBox();
            this.base_name = new System.Windows.Forms.Label();
            this.ImportWAD = new System.Windows.Forms.RadioButton();
            this.DownloadWAD = new System.Windows.Forms.RadioButton();
            this.title_id = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baseID = new System.Windows.Forms.Label();
            this.BaseRegion = new System.Windows.Forms.PictureBox();
            this.Base = new System.Windows.Forms.ComboBox();
            this.baseName = new System.Windows.Forms.Label();
            this.browsePatch = new System.Windows.Forms.OpenFileDialog();
            this.browseManual = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.bannerPreview = new System.Windows.Forms.PictureBox();
            this.editContentOptions = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.manual_type = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.forwarder_console = new System.Windows.Forms.Label();
            this.forwarder_root_device = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.released)).BeginInit();
            this.wad_base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPreview)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseRegionList
            // 
            resources.ApplyResources(this.baseRegionList, "baseRegionList");
            this.baseRegionList.Name = "WADRegion";
            this.baseRegionList.ShowCheckMargin = true;
            this.baseRegionList.ShowImageMargin = false;
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
            this.groupBox7.Controls.Add(this.LinkSaveData);
            this.groupBox7.Controls.Add(this.SaveIcon_Panel);
            this.groupBox7.Controls.Add(this.save_data_title);
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
            this.SaveIcon_Panel.Name = "SaveIcon_Panel";
            // 
            // save_data_title
            // 
            resources.ApplyResources(this.save_data_title, "save_data_title");
            this.save_data_title.Name = "save_data_title";
            this.save_data_title.Tag = "25";
            this.save_data_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.save_data_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.label16.Tag = "none";
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
            // injection_method
            // 
            resources.ApplyResources(this.injection_method, "injection_method");
            this.injection_method.Name = "injection_method";
            this.injection_method.Tag = "injection_method";
            // 
            // toggleSwitchL1
            // 
            resources.ApplyResources(this.toggleSwitchL1, "toggleSwitchL1");
            this.toggleSwitchL1.Name = "toggleSwitchL1";
            this.toggleSwitchL1.Click += new System.EventHandler(this.toggleSwitchL1_Click);
            // 
            // toggleSwitch1
            // 
            resources.ApplyResources(this.toggleSwitch1, "toggleSwitch1");
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
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
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.label11.UseMnemonic = false;
            // 
            // Patch
            // 
            resources.ApplyResources(this.Patch, "Patch");
            this.Patch.Name = "Patch";
            this.Patch.Tag = "import_patch";
            this.Patch.UseVisualStyleBackColor = true;
            this.Patch.CheckedChanged += new System.EventHandler(this.Patch_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tid);
            this.groupBox3.Controls.Add(this.title_id_2);
            this.groupBox3.Controls.Add(this.Random);
            this.groupBox3.Controls.Add(this.channel_title);
            this.groupBox3.Controls.Add(this.channel_name);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.TargetRegion);
            this.groupBox3.Controls.Add(this.region);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "channel";
            // 
            // tid
            // 
            this.tid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.tid, "tid");
            this.tid.Name = "tid";
            this.tid.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // title_id_2
            // 
            resources.ApplyResources(this.title_id_2, "title_id_2");
            this.title_id_2.Name = "title_id_2";
            this.title_id_2.Tag = "title_id";
            // 
            // Random
            // 
            this.Random.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Random.Image = global::FriishProduce.Properties.Resources.arrow_switch;
            resources.ApplyResources(this.Random, "Random");
            this.Random.Name = "Random";
            this.Random.TabStop = false;
            this.Random.Click += new System.EventHandler(this.Random_Click);
            // 
            // channel_title
            // 
            resources.ApplyResources(this.channel_title, "channel_title");
            this.channel_title.Name = "channel_title";
            this.channel_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            // 
            // channel_name
            // 
            resources.ApplyResources(this.channel_name, "channel_name");
            this.channel_name.Name = "channel_name";
            this.channel_name.Tag = "channel_name";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // TargetRegion
            // 
            this.TargetRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetRegion.FormattingEnabled = true;
            resources.ApplyResources(this.TargetRegion, "TargetRegion");
            this.TargetRegion.Name = "TargetRegion";
            this.TargetRegion.SelectedIndexChanged += new System.EventHandler(this.RegionsList_SelectedIndexChanged);
            // 
            // region
            // 
            resources.ApplyResources(this.region, "region");
            this.region.Name = "region";
            this.region.Tag = "region";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.software_name);
            this.groupBox1.Controls.Add(this.filename);
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
            this.software_name.UseMnemonic = false;
            // 
            // filename
            // 
            resources.ApplyResources(this.filename, "filename");
            this.filename.Name = "filename";
            this.filename.Tag = "filename";
            this.filename.UseMnemonic = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ShowBannerPreview);
            this.groupBox6.Controls.Add(this.players);
            this.groupBox6.Controls.Add(this.released);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.banner_title);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "banner";
            // 
            // ShowBannerPreview
            // 
            resources.ApplyResources(this.ShowBannerPreview, "ShowBannerPreview");
            this.ShowBannerPreview.Name = "ShowBannerPreview";
            this.ShowBannerPreview.Tag = "banner_preview";
            this.ShowBannerPreview.UseVisualStyleBackColor = true;
            this.ShowBannerPreview.Click += new System.EventHandler(this.ShowBannerPreview_Click);
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
            // banner_title
            // 
            resources.ApplyResources(this.banner_title, "banner_title");
            this.banner_title.Name = "banner_title";
            this.banner_title.TextChanged += new System.EventHandler(this.TextBox_Changed);
            this.banner_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Handle);
            // 
            // wad_base
            // 
            this.wad_base.Controls.Add(this.base_name);
            this.wad_base.Controls.Add(this.ImportWAD);
            this.wad_base.Controls.Add(this.DownloadWAD);
            this.wad_base.Controls.Add(this.title_id);
            this.wad_base.Controls.Add(this.panel1);
            this.wad_base.Controls.Add(this.baseID);
            this.wad_base.Controls.Add(this.BaseRegion);
            this.wad_base.Controls.Add(this.Base);
            this.wad_base.Controls.Add(this.baseName);
            resources.ApplyResources(this.wad_base, "wad_base");
            this.wad_base.Name = "wad_base";
            this.wad_base.TabStop = false;
            this.wad_base.Tag = "wad_base";
            // 
            // base_name
            // 
            resources.ApplyResources(this.base_name, "base_name");
            this.base_name.Name = "base_name";
            this.base_name.Tag = "base_name";
            this.base_name.UseMnemonic = false;
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
            // title_id
            // 
            resources.ApplyResources(this.title_id, "title_id");
            this.title_id.Name = "title_id";
            this.title_id.Tag = "title_id";
            this.title_id.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
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
            this.BaseRegion.ContextMenuStrip = this.baseRegionList;
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
            // browsePatch
            // 
            this.browsePatch.RestoreDirectory = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.image_stretch);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.bannerPreview);
            this.groupBox5.Controls.Add(this.imageintpl);
            this.groupBox5.Controls.Add(this.image_fit);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "image";
            // 
            // bannerPreview
            // 
            resources.ApplyResources(this.bannerPreview, "bannerPreview");
            this.bannerPreview.Name = "bannerPreview";
            this.bannerPreview.TabStop = false;
            // 
            // editContentOptions
            // 
            this.editContentOptions.Image = global::FriishProduce.Properties.Resources.cog;
            resources.ApplyResources(this.editContentOptions, "editContentOptions");
            this.editContentOptions.Name = "editContentOptions";
            this.editContentOptions.Tag = "";
            this.editContentOptions.UseCompatibleTextRendering = true;
            this.editContentOptions.UseVisualStyleBackColor = true;
            this.editContentOptions.Click += new System.EventHandler(this.openInjectorOptions);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.forwarder_console);
            this.groupBox4.Controls.Add(this.editContentOptions);
            this.groupBox4.Controls.Add(this.toggleSwitchL1);
            this.groupBox4.Controls.Add(this.injection_methods);
            this.groupBox4.Controls.Add(this.injection_method);
            this.groupBox4.Controls.Add(this.toggleSwitch1);
            this.groupBox4.Controls.Add(this.FStorage_SD);
            this.groupBox4.Controls.Add(this.FStorage_USB);
            this.groupBox4.Controls.Add(this.forwarder_root_device);
            this.groupBox4.Controls.Add(this.manual_type_list);
            this.groupBox4.Controls.Add(this.manual_type);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "content_options";
            // 
            // manual_type
            // 
            resources.ApplyResources(this.manual_type, "manual_type");
            this.manual_type.Name = "manual_type";
            this.manual_type.Tag = "manual_type";
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // forwarder_console
            // 
            resources.ApplyResources(this.forwarder_console, "forwarder_console");
            this.forwarder_console.Name = "forwarder_console";
            this.forwarder_console.Tag = "forwarder_console";
            // 
            // forwarder_root_device
            // 
            resources.ApplyResources(this.forwarder_root_device, "forwarder_root_device");
            this.forwarder_root_device.Name = "forwarder_root_device";
            this.forwarder_root_device.Tag = "forwarder_root_device";
            // 
            // ProjectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.Patch);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.wad_base);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ProjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.released)).EndInit();
            this.wad_base.ResumeLayout(false);
            this.wad_base.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BaseRegion)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPreview)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.Label title_id_2;
        private System.Windows.Forms.TextBox tid;
        private System.Windows.Forms.Panel SaveIcon_Panel;
        private System.Windows.Forms.ComboBox imageintpl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox Random;
        private System.Windows.Forms.ComboBox Base;
        private System.Windows.Forms.Button editContentOptions;
        private System.Windows.Forms.GroupBox wad_base;
        private System.Windows.Forms.PictureBox BaseRegion;
        private System.Windows.Forms.ContextMenuStrip baseRegionList;
        private System.Windows.Forms.Label baseName;
        private System.Windows.Forms.Label baseID;
        private System.Windows.Forms.Label title_id;
        private System.Windows.Forms.Label base_name;
        private System.Windows.Forms.NumericUpDown released;
        private System.Windows.Forms.NumericUpDown players;
        internal System.Windows.Forms.OpenFileDialog browseInputWad;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label software_name;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton image_stretch;
        private System.Windows.Forms.RadioButton image_fit;
        private System.Windows.Forms.ComboBox injection_methods;
        private System.Windows.Forms.Label injection_method;
        private System.Windows.Forms.CheckBox Patch;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.OpenFileDialog browsePatch;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label region;
        private System.Windows.Forms.ComboBox TargetRegion;
        private System.Windows.Forms.CheckBox LinkSaveData;
        private System.Windows.Forms.RadioButton DownloadWAD;
        private System.Windows.Forms.RadioButton ImportWAD;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog browseManual;
        private System.Windows.Forms.ComboBox manual_type_list;
        private System.Windows.Forms.Button ShowBannerPreview;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label toggleSwitchL1;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.RadioButton FStorage_SD;
        private System.Windows.Forms.RadioButton FStorage_USB;
        private System.Windows.Forms.PictureBox bannerPreview;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label manual_type;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label forwarder_console;
        private System.Windows.Forms.Label forwarder_root_device;
    }
}