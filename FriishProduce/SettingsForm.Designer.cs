
namespace FriishProduce
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.b_cancel = new System.Windows.Forms.Button();
            this.GetBanners = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bypass_rom_size = new System.Windows.Forms.CheckBox();
            this.use_online_wad_enabled = new System.Windows.Forms.CheckBox();
            this.use_custom_database = new System.Windows.Forms.CheckBox();
            this.reset_all_dialogs = new System.Windows.Forms.CheckBox();
            this.forwarder = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.auto_prefill = new System.Windows.Forms.CheckBox();
            this.auto_fill_save_data = new System.Windows.Forms.CheckBox();
            this.default_injection_methods = new System.Windows.Forms.Panel();
            this.bios_files = new System.Windows.Forms.Panel();
            this.border = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.default_content_options_list = new System.Windows.Forms.ListBox();
            this.default_content_options_l = new System.Windows.Forms.Label();
            this.default_content_options_e = new System.Windows.Forms.Button();
            this.theme = new FriishProduce.GroupBoxEx();
            this.themes = new System.Windows.Forms.ComboBox();
            this.language = new FriishProduce.GroupBoxEx();
            this.languages = new System.Windows.Forms.ComboBox();
            this.forwarder_root_device = new FriishProduce.GroupBoxEx();
            this.forwarder_type = new System.Windows.Forms.ComboBox();
            this.bios_settings = new FriishProduce.GroupBoxEx();
            this.show_bios_screen = new System.Windows.Forms.Label();
            this.toggleSwitch2 = new JCS.ToggleSwitch();
            this.image_interpolation_mode = new FriishProduce.GroupBoxEx();
            this.image_interpolation_modes = new System.Windows.Forms.ComboBox();
            this.banner_region = new FriishProduce.GroupBoxEx();
            this.banner_regions = new System.Windows.Forms.ComboBox();
            this.default_target_filename = new FriishProduce.GroupBoxEx();
            this.default_target_project = new System.Windows.Forms.Label();
            this.default_target_project_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.default_target_wad = new System.Windows.Forms.Label();
            this.default_target_wad_tb = new System.Windows.Forms.TextBox();
            this.default_target_parameters = new System.Windows.Forms.Label();
            this.sega_default = new FriishProduce.GroupBoxEx();
            this.injection_methods_sega = new System.Windows.Forms.ComboBox();
            this.snes_default = new FriishProduce.GroupBoxEx();
            this.injection_methods_snes = new System.Windows.Forms.ComboBox();
            this.nes_default = new FriishProduce.GroupBoxEx();
            this.injection_methods_nes = new System.Windows.Forms.ComboBox();
            this.n64_default = new FriishProduce.GroupBoxEx();
            this.injection_methods_n64 = new System.Windows.Forms.ComboBox();
            this.bios_neo = new FriishProduce.GroupBoxEx();
            this.browse_bios_neo = new System.Windows.Forms.Button();
            this.bios_filename_neo = new System.Windows.Forms.TextBox();
            this.bios_psx = new FriishProduce.GroupBoxEx();
            this.browse_bios_psx = new System.Windows.Forms.Button();
            this.bios_filename_psx = new System.Windows.Forms.TextBox();
            this.htmlLabel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.forwarder.SuspendLayout();
            this.panel2.SuspendLayout();
            this.default_injection_methods.SuspendLayout();
            this.bios_files.SuspendLayout();
            this.panel3.SuspendLayout();
            this.theme.SuspendLayout();
            this.language.SuspendLayout();
            this.forwarder_root_device.SuspendLayout();
            this.bios_settings.SuspendLayout();
            this.image_interpolation_mode.SuspendLayout();
            this.banner_region.SuspendLayout();
            this.default_target_filename.SuspendLayout();
            this.sega_default.SuspendLayout();
            this.snes_default.SuspendLayout();
            this.nes_default.SuspendLayout();
            this.n64_default.SuspendLayout();
            this.bios_neo.SuspendLayout();
            this.bios_psx.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            resources.ApplyResources(this.bottomPanel2, "bottomPanel2");
            this.bottomPanel2.Name = "bottomPanel2";
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel1.Controls.Add(this.b_cancel);
            this.bottomPanel1.Controls.Add(this.GetBanners);
            this.bottomPanel1.Controls.Add(this.b_ok);
            resources.ApplyResources(this.bottomPanel1, "bottomPanel1");
            this.bottomPanel1.Name = "bottomPanel1";
            // 
            // b_cancel
            // 
            resources.ApplyResources(this.b_cancel, "b_cancel");
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Tag = "b_cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // GetBanners
            // 
            resources.ApplyResources(this.GetBanners, "GetBanners");
            this.GetBanners.Name = "GetBanners";
            this.GetBanners.UseVisualStyleBackColor = true;
            this.GetBanners.Click += new System.EventHandler(this.DownloadBanners_Click);
            // 
            // b_ok
            // 
            resources.ApplyResources(this.b_ok, "b_ok");
            this.b_ok.Name = "b_ok";
            this.b_ok.Tag = "b_ok";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.OK_Click);
            // 
            // TreeView
            // 
            this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView.FullRowSelect = true;
            this.TreeView.HotTracking = true;
            resources.ApplyResources(this.TreeView, "TreeView");
            this.TreeView.ItemHeight = 22;
            this.TreeView.Name = "TreeView";
            this.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes1"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes2"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes3")))});
            this.TreeView.ShowPlusMinus = false;
            this.TreeView.ShowRootLines = false;
            this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.theme);
            this.panel1.Controls.Add(this.bypass_rom_size);
            this.panel1.Controls.Add(this.use_online_wad_enabled);
            this.panel1.Controls.Add(this.use_custom_database);
            this.panel1.Controls.Add(this.reset_all_dialogs);
            this.panel1.Controls.Add(this.language);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // bypass_rom_size
            // 
            resources.ApplyResources(this.bypass_rom_size, "bypass_rom_size");
            this.bypass_rom_size.Name = "bypass_rom_size";
            this.bypass_rom_size.Tag = "bypass_rom_size";
            this.bypass_rom_size.UseVisualStyleBackColor = true;
            // 
            // use_online_wad_enabled
            // 
            resources.ApplyResources(this.use_online_wad_enabled, "use_online_wad_enabled");
            this.use_online_wad_enabled.Name = "use_online_wad_enabled";
            this.use_online_wad_enabled.Tag = "use_online_wad_enabled";
            this.use_online_wad_enabled.UseVisualStyleBackColor = true;
            // 
            // use_custom_database
            // 
            resources.ApplyResources(this.use_custom_database, "use_custom_database");
            this.use_custom_database.Name = "use_custom_database";
            this.use_custom_database.Tag = "use_custom_database";
            this.use_custom_database.UseVisualStyleBackColor = true;
            this.use_custom_database.CheckedChanged += new System.EventHandler(this.CustomDatabase_CheckedChanged);
            // 
            // reset_all_dialogs
            // 
            resources.ApplyResources(this.reset_all_dialogs, "reset_all_dialogs");
            this.reset_all_dialogs.Name = "reset_all_dialogs";
            this.reset_all_dialogs.Tag = "reset_all_dialogs";
            this.reset_all_dialogs.UseVisualStyleBackColor = true;
            // 
            // forwarder
            // 
            this.forwarder.Controls.Add(this.forwarder_root_device);
            this.forwarder.Controls.Add(this.bios_settings);
            resources.ApplyResources(this.forwarder, "forwarder");
            this.forwarder.Name = "forwarder";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.auto_prefill);
            this.panel2.Controls.Add(this.auto_fill_save_data);
            this.panel2.Controls.Add(this.image_interpolation_mode);
            this.panel2.Controls.Add(this.banner_region);
            this.panel2.Controls.Add(this.default_target_filename);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // auto_prefill
            // 
            resources.ApplyResources(this.auto_prefill, "auto_prefill");
            this.auto_prefill.Name = "auto_prefill";
            this.auto_prefill.Tag = "auto_prefill";
            this.auto_prefill.UseVisualStyleBackColor = true;
            // 
            // auto_fill_save_data
            // 
            resources.ApplyResources(this.auto_fill_save_data, "auto_fill_save_data");
            this.auto_fill_save_data.Name = "auto_fill_save_data";
            this.auto_fill_save_data.Tag = "auto_fill_save_data";
            this.auto_fill_save_data.UseVisualStyleBackColor = true;
            // 
            // default_injection_methods
            // 
            this.default_injection_methods.Controls.Add(this.sega_default);
            this.default_injection_methods.Controls.Add(this.snes_default);
            this.default_injection_methods.Controls.Add(this.nes_default);
            this.default_injection_methods.Controls.Add(this.n64_default);
            resources.ApplyResources(this.default_injection_methods, "default_injection_methods");
            this.default_injection_methods.Name = "default_injection_methods";
            // 
            // bios_files
            // 
            this.bios_files.Controls.Add(this.bios_neo);
            this.bios_files.Controls.Add(this.bios_psx);
            resources.ApplyResources(this.bios_files, "bios_files");
            this.bios_files.Name = "bios_files";
            // 
            // border
            // 
            this.border.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.border, "border");
            this.border.Name = "border";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.htmlLabel1);
            this.panel3.Controls.Add(this.default_content_options_list);
            this.panel3.Controls.Add(this.default_content_options_l);
            this.panel3.Controls.Add(this.default_content_options_e);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            this.panel3.Tag = "";
            // 
            // default_content_options_list
            // 
            this.default_content_options_list.FormattingEnabled = true;
            resources.ApplyResources(this.default_content_options_list, "default_content_options_list");
            this.default_content_options_list.Name = "default_content_options_list";
            // 
            // default_content_options_l
            // 
            resources.ApplyResources(this.default_content_options_l, "default_content_options_l");
            this.default_content_options_l.Name = "default_content_options_l";
            this.default_content_options_l.Tag = "default_content_options_l";
            // 
            // default_content_options_e
            // 
            resources.ApplyResources(this.default_content_options_e, "default_content_options_e");
            this.default_content_options_e.Name = "default_content_options_e";
            this.default_content_options_e.Tag = "default_content_options_e";
            this.default_content_options_e.UseVisualStyleBackColor = true;
            this.default_content_options_e.Click += new System.EventHandler(this.OpenContentOptions);
            // 
            // theme
            // 
            this.theme.Controls.Add(this.themes);
            this.theme.Flat = false;
            resources.ApplyResources(this.theme, "theme");
            this.theme.Name = "theme";
            this.theme.TabStop = false;
            this.theme.Tag = "theme";
            // 
            // themes
            // 
            this.themes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.themes.FormattingEnabled = true;
            this.themes.Items.AddRange(new object[] {
            resources.GetString("themes.Items")});
            resources.ApplyResources(this.themes, "themes");
            this.themes.Name = "themes";
            // 
            // language
            // 
            this.language.Controls.Add(this.languages);
            this.language.Flat = false;
            resources.ApplyResources(this.language, "language");
            this.language.Name = "language";
            this.language.TabStop = false;
            this.language.Tag = "language";
            // 
            // languages
            // 
            this.languages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languages.FormattingEnabled = true;
            this.languages.Items.AddRange(new object[] {
            resources.GetString("languages.Items")});
            resources.ApplyResources(this.languages, "languages");
            this.languages.Name = "languages";
            // 
            // forwarder_root_device
            // 
            this.forwarder_root_device.Controls.Add(this.forwarder_type);
            this.forwarder_root_device.Flat = false;
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
            // bios_settings
            // 
            this.bios_settings.Controls.Add(this.show_bios_screen);
            this.bios_settings.Controls.Add(this.toggleSwitch2);
            this.bios_settings.Flat = false;
            resources.ApplyResources(this.bios_settings, "bios_settings");
            this.bios_settings.Name = "bios_settings";
            this.bios_settings.TabStop = false;
            this.bios_settings.Tag = "bios_settings";
            // 
            // show_bios_screen
            // 
            resources.ApplyResources(this.show_bios_screen, "show_bios_screen");
            this.show_bios_screen.Name = "show_bios_screen";
            this.show_bios_screen.Tag = "show_bios_screen";
            // 
            // toggleSwitch2
            // 
            resources.ApplyResources(this.toggleSwitch2, "toggleSwitch2");
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // image_interpolation_mode
            // 
            this.image_interpolation_mode.Controls.Add(this.image_interpolation_modes);
            this.image_interpolation_mode.Flat = false;
            resources.ApplyResources(this.image_interpolation_mode, "image_interpolation_mode");
            this.image_interpolation_mode.Name = "image_interpolation_mode";
            this.image_interpolation_mode.TabStop = false;
            this.image_interpolation_mode.Tag = "image_interpolation_mode";
            // 
            // image_interpolation_modes
            // 
            this.image_interpolation_modes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.image_interpolation_modes.FormattingEnabled = true;
            resources.ApplyResources(this.image_interpolation_modes, "image_interpolation_modes");
            this.image_interpolation_modes.Name = "image_interpolation_modes";
            this.image_interpolation_modes.Tag = "image_interpolation_mode";
            // 
            // banner_region
            // 
            this.banner_region.Controls.Add(this.banner_regions);
            this.banner_region.Flat = false;
            resources.ApplyResources(this.banner_region, "banner_region");
            this.banner_region.Name = "banner_region";
            this.banner_region.TabStop = false;
            this.banner_region.Tag = "banner_region";
            // 
            // banner_regions
            // 
            this.banner_regions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.banner_regions.FormattingEnabled = true;
            resources.ApplyResources(this.banner_regions, "banner_regions");
            this.banner_regions.Name = "banner_regions";
            this.banner_regions.Tag = "";
            // 
            // default_target_filename
            // 
            this.default_target_filename.Controls.Add(this.default_target_project);
            this.default_target_filename.Controls.Add(this.default_target_project_tb);
            this.default_target_filename.Controls.Add(this.label2);
            this.default_target_filename.Controls.Add(this.default_target_wad);
            this.default_target_filename.Controls.Add(this.default_target_wad_tb);
            this.default_target_filename.Controls.Add(this.default_target_parameters);
            this.default_target_filename.Flat = false;
            resources.ApplyResources(this.default_target_filename, "default_target_filename");
            this.default_target_filename.Name = "default_target_filename";
            this.default_target_filename.TabStop = false;
            this.default_target_filename.Tag = "default_target_filename";
            // 
            // default_target_project
            // 
            resources.ApplyResources(this.default_target_project, "default_target_project");
            this.default_target_project.Name = "default_target_project";
            this.default_target_project.Tag = "default_target_project";
            // 
            // default_target_project_tb
            // 
            resources.ApplyResources(this.default_target_project_tb, "default_target_project_tb");
            this.default_target_project_tb.Name = "default_target_project_tb";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // default_target_wad
            // 
            resources.ApplyResources(this.default_target_wad, "default_target_wad");
            this.default_target_wad.Name = "default_target_wad";
            this.default_target_wad.Tag = "default_target_wad";
            // 
            // default_target_wad_tb
            // 
            resources.ApplyResources(this.default_target_wad_tb, "default_target_wad_tb");
            this.default_target_wad_tb.Name = "default_target_wad_tb";
            // 
            // default_target_parameters
            // 
            resources.ApplyResources(this.default_target_parameters, "default_target_parameters");
            this.default_target_parameters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.default_target_parameters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.default_target_parameters.ForeColor = System.Drawing.Color.Black;
            this.default_target_parameters.Name = "default_target_parameters";
            this.default_target_parameters.Tag = "default_target_parameters";
            // 
            // sega_default
            // 
            this.sega_default.Controls.Add(this.injection_methods_sega);
            this.sega_default.Flat = false;
            resources.ApplyResources(this.sega_default, "sega_default");
            this.sega_default.Name = "sega_default";
            this.sega_default.TabStop = false;
            this.sega_default.Tag = "sega";
            // 
            // injection_methods_sega
            // 
            this.injection_methods_sega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.injection_methods_sega.FormattingEnabled = true;
            resources.ApplyResources(this.injection_methods_sega, "injection_methods_sega");
            this.injection_methods_sega.Name = "injection_methods_sega";
            this.injection_methods_sega.Tag = "";
            // 
            // snes_default
            // 
            this.snes_default.Controls.Add(this.injection_methods_snes);
            this.snes_default.Flat = false;
            resources.ApplyResources(this.snes_default, "snes_default");
            this.snes_default.Name = "snes_default";
            this.snes_default.TabStop = false;
            this.snes_default.Tag = "snes";
            // 
            // injection_methods_snes
            // 
            this.injection_methods_snes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.injection_methods_snes.FormattingEnabled = true;
            resources.ApplyResources(this.injection_methods_snes, "injection_methods_snes");
            this.injection_methods_snes.Name = "injection_methods_snes";
            this.injection_methods_snes.Tag = "";
            // 
            // nes_default
            // 
            this.nes_default.Controls.Add(this.injection_methods_nes);
            this.nes_default.Flat = false;
            resources.ApplyResources(this.nes_default, "nes_default");
            this.nes_default.Name = "nes_default";
            this.nes_default.TabStop = false;
            this.nes_default.Tag = "nes";
            // 
            // injection_methods_nes
            // 
            this.injection_methods_nes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.injection_methods_nes.FormattingEnabled = true;
            resources.ApplyResources(this.injection_methods_nes, "injection_methods_nes");
            this.injection_methods_nes.Name = "injection_methods_nes";
            this.injection_methods_nes.Tag = "";
            // 
            // n64_default
            // 
            this.n64_default.Controls.Add(this.injection_methods_n64);
            this.n64_default.Flat = false;
            resources.ApplyResources(this.n64_default, "n64_default");
            this.n64_default.Name = "n64_default";
            this.n64_default.TabStop = false;
            this.n64_default.Tag = "n64";
            // 
            // injection_methods_n64
            // 
            this.injection_methods_n64.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.injection_methods_n64.FormattingEnabled = true;
            resources.ApplyResources(this.injection_methods_n64, "injection_methods_n64");
            this.injection_methods_n64.Name = "injection_methods_n64";
            this.injection_methods_n64.Tag = "";
            // 
            // bios_neo
            // 
            this.bios_neo.Controls.Add(this.browse_bios_neo);
            this.bios_neo.Controls.Add(this.bios_filename_neo);
            this.bios_neo.Flat = false;
            resources.ApplyResources(this.bios_neo, "bios_neo");
            this.bios_neo.Name = "bios_neo";
            this.bios_neo.TabStop = false;
            this.bios_neo.Tag = "neo";
            // 
            // browse_bios_neo
            // 
            resources.ApplyResources(this.browse_bios_neo, "browse_bios_neo");
            this.browse_bios_neo.Name = "browse_bios_neo";
            this.browse_bios_neo.UseVisualStyleBackColor = true;
            this.browse_bios_neo.Click += new System.EventHandler(this.BrowseBIOS);
            // 
            // bios_filename_neo
            // 
            resources.ApplyResources(this.bios_filename_neo, "bios_filename_neo");
            this.bios_filename_neo.Name = "bios_filename_neo";
            this.bios_filename_neo.ReadOnly = true;
            // 
            // bios_psx
            // 
            this.bios_psx.Controls.Add(this.browse_bios_psx);
            this.bios_psx.Controls.Add(this.bios_filename_psx);
            this.bios_psx.Flat = false;
            resources.ApplyResources(this.bios_psx, "bios_psx");
            this.bios_psx.Name = "bios_psx";
            this.bios_psx.TabStop = false;
            this.bios_psx.Tag = "psx";
            // 
            // browse_bios_psx
            // 
            resources.ApplyResources(this.browse_bios_psx, "browse_bios_psx");
            this.browse_bios_psx.Name = "browse_bios_psx";
            this.browse_bios_psx.UseVisualStyleBackColor = true;
            this.browse_bios_psx.Click += new System.EventHandler(this.BrowseBIOS);
            // 
            // bios_filename_psx
            // 
            resources.ApplyResources(this.bios_filename_psx, "bios_filename_psx");
            this.bios_filename_psx.Name = "bios_filename_psx";
            this.bios_filename_psx.ReadOnly = true;
            // 
            // htmlLabel1
            // 
            this.htmlLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.htmlLabel1.BaseStylesheet = "";
            resources.ApplyResources(this.htmlLabel1, "htmlLabel1");
            this.htmlLabel1.Name = "htmlLabel1";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.b_cancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.border);
            this.Controls.Add(this.TreeView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.forwarder);
            this.Controls.Add(this.default_injection_methods);
            this.Controls.Add(this.bios_files);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Tag = "settingsform";
            this.Load += new System.EventHandler(this.Loading);
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.forwarder.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.default_injection_methods.ResumeLayout(false);
            this.bios_files.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.theme.ResumeLayout(false);
            this.language.ResumeLayout(false);
            this.forwarder_root_device.ResumeLayout(false);
            this.bios_settings.ResumeLayout(false);
            this.bios_settings.PerformLayout();
            this.image_interpolation_mode.ResumeLayout(false);
            this.banner_region.ResumeLayout(false);
            this.default_target_filename.ResumeLayout(false);
            this.default_target_filename.PerformLayout();
            this.sega_default.ResumeLayout(false);
            this.snes_default.ResumeLayout(false);
            this.nes_default.ResumeLayout(false);
            this.n64_default.ResumeLayout(false);
            this.bios_neo.ResumeLayout(false);
            this.bios_neo.PerformLayout();
            this.bios_psx.ResumeLayout(false);
            this.bios_psx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel bottomPanel2;
        private System.Windows.Forms.ComboBox languages;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox reset_all_dialogs;
        private System.Windows.Forms.Panel forwarder;
        private GroupBoxEx forwarder_root_device;
        private System.Windows.Forms.CheckBox auto_prefill;
        private System.Windows.Forms.CheckBox auto_fill_save_data;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label show_bios_screen;
        private JCS.ToggleSwitch toggleSwitch2;
        private System.Windows.Forms.CheckBox use_custom_database;
        private GroupBoxEx bios_settings;
        private System.Windows.Forms.Panel default_injection_methods;
        private System.Windows.Forms.ComboBox injection_methods_nes;
        private System.Windows.Forms.ComboBox injection_methods_n64;
        private System.Windows.Forms.ComboBox injection_methods_snes;
        private System.Windows.Forms.ComboBox injection_methods_sega;
        private System.Windows.Forms.ComboBox image_interpolation_modes;
        private System.Windows.Forms.Panel bottomPanel1;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.ComboBox forwarder_type;
        private System.Windows.Forms.CheckBox use_online_wad_enabled;
        private System.Windows.Forms.ComboBox banner_regions;
        private GroupBoxEx nes_default;
        private GroupBoxEx snes_default;
        private GroupBoxEx sega_default;
        private GroupBoxEx n64_default;
        private GroupBoxEx banner_region;
        private System.Windows.Forms.Panel bios_files;
        private System.Windows.Forms.Button browse_bios_psx;
        private System.Windows.Forms.TextBox bios_filename_psx;
        private GroupBoxEx bios_psx;
        private GroupBoxEx bios_neo;
        private System.Windows.Forms.Button browse_bios_neo;
        private System.Windows.Forms.TextBox bios_filename_neo;
        private GroupBoxEx language;
        private GroupBoxEx image_interpolation_mode;
        private System.Windows.Forms.CheckBox bypass_rom_size;
        private System.Windows.Forms.Label default_target_project;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox default_target_wad_tb;
        private System.Windows.Forms.Label default_target_parameters;
        private System.Windows.Forms.Label default_target_wad;
        private System.Windows.Forms.TextBox default_target_project_tb;
        private GroupBoxEx default_target_filename;
        private System.Windows.Forms.Button GetBanners;
        private System.Windows.Forms.Panel border;
        private GroupBoxEx theme;
        private System.Windows.Forms.ComboBox themes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button default_content_options_e;
        private System.Windows.Forms.Label default_content_options_l;
        private System.Windows.Forms.ListBox default_content_options_list;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel htmlLabel1;
    }
}