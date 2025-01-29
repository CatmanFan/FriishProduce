using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class SettingsForm : Form
    {
        private bool isShown = false;

        private int dirtyOption1;
        private bool dirtyOption2;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public void RefreshForm()
        {
            #region --- Localization / Appearance ---

            Text = Program.Lang.String("preferences");
            Program.Lang.Control(this);
            Program.Lang.Control(vc_nes);
            Program.Lang.Control(vc_snes);
            Program.Lang.Control(vc_n64);
            Program.Lang.Control(vc_sega);
            Program.Lang.Control(vc_pce);
            Program.Lang.Control(vc_neo);
            Program.Lang.Control(adobe_flash);

            TreeView.Nodes[0].Text = Program.Lang.String(TreeView.Nodes[0].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[1].Text = Program.Lang.String(TreeView.Nodes[1].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[2].Text = Program.Lang.String(TreeView.Nodes[2].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[3].Text = Program.Lang.String(TreeView.Nodes[3].Tag.ToString(), Tag.ToString());

            var default_node = TreeView.Nodes[1];
            default_node.Text = Program.Lang.String(default_node.Tag.ToString(), Tag.ToString());
            default_node.Expand();
            default_node.Nodes[0].Text = Program.Lang.Console(Platform.NES);
            default_node.Nodes[1].Text = Program.Lang.Console(Platform.SNES);
            default_node.Nodes[2].Text = Program.Lang.Console(Platform.N64);
            default_node.Nodes[3].Text = sega_default.Text = Program.Lang.String("group1", "platforms");
            default_node.Nodes[4].Text = Program.Lang.Console(Platform.PCE);
            default_node.Nodes[5].Text = Program.Lang.Console(Platform.NEO);
            default_node.Nodes[6].Text = Program.Lang.Console(Platform.Flash);
            default_node.Nodes[7].Text = Program.Lang.String("forwarders", "platforms");

            #endregion

            // -----------------------------

            // -------------------------------------------
            // Add all languages
            // -------------------------------------------
            languages.Items.Clear();
            languages.Items.Add("<" + Program.Lang.String("system_default", Name) + ">");
            foreach (var item in Program.Lang.List) languages.Items.Add(item.Value);
            languages.SelectedIndex = Program.Config.application.language == "sys" ? 0 : Program.Lang.List.Keys.ToList().IndexOf(Program.Config.application.language) + 1;

            #region --- Localization of All Controls ---

            image_interpolation_mode.Text = Program.Lang.String("image_interpolation_mode", "projectform");
            image_interpolation_modes.Items.Clear();
            image_interpolation_modes.Items.AddRange(Program.Lang.StringArray("image_interpolation_mode", "projectform"));
            image_interpolation_modes.SelectedIndex = Program.Config.application.image_interpolation;
            
            int maxX = Math.Max(default_target_project_tb.Location.X, default_target_wad_tb.Location.X), maxWidth = Math.Min(default_target_project_tb.Width, default_target_wad_tb.Width);
            default_target_project_tb.Location = new Point(maxX, default_target_project_tb.Location.Y);
            default_target_wad_tb.Location = new Point(maxX, default_target_wad_tb.Location.Y);
            default_target_project_tb.Width = default_target_wad_tb.Width = maxWidth;

            banner_region.Text = Program.Lang.String(banner_region.Name, "banner").TrimEnd(':').Trim();

            flash_save_data_enable.Text = vc_pce_backupram.Text = vc_sega_save_sram.Text = Program.Lang.String("save_data_enable", "projectform");

            // -----------------------------

            injection_methods_nes.Items.Clear();
            injection_methods_nes.Items.Add(Program.Lang.String("vc"));
            injection_methods_nes.Items.Add(Forwarder.List[0].Name);
            injection_methods_nes.Items.Add(Forwarder.List[1].Name);
            injection_methods_nes.Items.Add(Forwarder.List[2].Name);

            injection_methods_snes.Items.Clear();
            injection_methods_snes.Items.Add(Program.Lang.String("vc"));
            injection_methods_snes.Items.Add(Forwarder.List[3].Name);
            injection_methods_snes.Items.Add(Forwarder.List[4].Name);
            injection_methods_snes.Items.Add(Forwarder.List[5].Name);

            injection_methods_n64.Items.Clear();
            injection_methods_n64.Items.Add(Program.Lang.String("vc"));
            injection_methods_n64.Items.Add(Forwarder.List[8].Name);
            injection_methods_n64.Items.Add(Forwarder.List[9].Name);
            injection_methods_n64.Items.Add(Forwarder.List[10].Name);
            injection_methods_n64.Items.Add(Forwarder.List[11].Name);

            injection_methods_sega.Items.Clear();
            injection_methods_sega.Items.Add(Program.Lang.String("vc"));
            injection_methods_sega.Items.Add(Forwarder.List[7].Name);

            injection_methods_nes.SelectedIndex = Program.Config.application.default_injection_method_nes;
            injection_methods_snes.SelectedIndex = Program.Config.application.default_injection_method_snes;
            injection_methods_n64.SelectedIndex = Program.Config.application.default_injection_method_n64;
            injection_methods_sega.SelectedIndex = Program.Config.application.default_injection_method_sega;

            // -----------------------------

            Program.Lang.String(forwarder_root_device, "projectform");
            forwarder_root_device.Text = forwarder_root_device.Text.TrimEnd(':').Trim();
            Program.Lang.String(bios_settings, "forwarder");
            Program.Lang.String(show_bios_screen, "forwarder");

            // -----------------------------

            Program.Lang.String(vc_nes_palette, "vc_nes");
            Program.Lang.String(vc_nes_palette_banner_usage, "vc_nes");
            Program.Lang.String(vc_nes_palettelist, "vc_nes");

            // -----------------------------

            Program.Lang.String(vc_snes_patch_volume, "vc_snes");
            Program.Lang.String(vc_snes_patch_nodark, "vc_snes");
            Program.Lang.String(vc_snes_patch_nocc, "vc_snes");
            Program.Lang.String(vc_snes_patch_nosave, "vc_snes");
            Program.Lang.String(vc_snes_patch_nosuspend, "vc_snes");
            Program.Lang.String(vc_snes_patch_widescreen, "vc_snes");
            Program.Lang.String(vc_snes_patch_nocheck, "vc_snes");

            // -----------------------------

            Program.Lang.String(vc_n64_patch_fixbrightness, "vc_n64");
            Program.Lang.String(vc_n64_patch_fixcrashes, "vc_n64");
            Program.Lang.String(vc_n64_patch_expandedram, "vc_n64");
            Program.Lang.String(vc_n64_patch_autosizerom, "vc_n64");
            Program.Lang.String(vc_n64_patch_cleantextures, "vc_n64");
            // Program.Lang.String(vc_n64_patch_widescreen, "vc_n64");
            Program.Lang.String(vc_n64_romc_type, "vc_n64");
            Program.Lang.String(vc_n64_romc_type_list, "vc_n64");

            // -----------------------------

            vc_sega_country.Text = Program.Lang.String("region");
            vc_sega_dev_mdpad_enable_6b.Text = string.Format(Program.Lang.String("dev_mdpad_enable_6b", "vc_sega"), Program.Lang.Console(Platform.SMD));
            Program.Lang.String(vc_sega_console_disableresetbutton, "vc_sega");

            vc_sega_countries.Items.Clear();
            vc_sega_countries.Items.AddRange(new string[] { Program.Lang.String("region_j"), Program.Lang.String("region_u"), Program.Lang.String("region_e") });

            // -----------------------------

            Program.Lang.Control(vc_pce_region, "vc_pce");
            Program.Lang.String(vc_pce_y_offset_l, "vc_pce");
            Program.Lang.String(vc_pce_hide_overscan, "vc_pce");
            Program.Lang.String(vc_pce_raster, "vc_pce");
            Program.Lang.String(vc_pce_sprline, "vc_pce");

            // -----------------------------

            Program.Lang.String(vc_neo_bios, "vc_neo");
            Program.Lang.String(vc_neo_bios_list, "vc_neo");
            vc_neo_bios_list.Items.RemoveAt(0);

            // -----------------------------

            Program.Lang.String(flash_update_frame_rate_l, "adobe_flash");
            Program.Lang.String(flash_save_data, "projectform");
            Program.Lang.String(flash_save_data_enable, "projectform");
            // Program.Lang.String(flash_vff_sync_on_write, "adobe_flash");
            Program.Lang.String(flash_vff_cache_size_l, "adobe_flash");
            Program.Lang.String(flash_persistent_storage_total_l, "adobe_flash");
            Program.Lang.String(flash_persistent_storage_per_movie_l, "adobe_flash");
            Program.Lang.String(flash_controls, "adobe_flash");
            Program.Lang.String(flash_mouse, "adobe_flash");
            Program.Lang.String(flash_qwerty_keyboard, "adobe_flash");
            Program.Lang.String(flash_quality_l, "adobe_flash");
            Program.Lang.String(flash_quality_list, "adobe_flash");
            Program.Lang.String(flash_strap_reminder, "adobe_flash");
            Program.Lang.String(flash_strap_reminder_list, "adobe_flash");
            Program.Lang.String(flash_fullscreen, "adobe_flash");

            #endregion

            // -----------------------------

            #region --- Set All Settings to Defaults ---

            // Defaults & forwarders
            reset_all_dialogs.Checked = false;
            toggleSwitch2.Checked = Program.Config.forwarder.show_bios_screen;
            forwarder_type.SelectedIndex = Program.Config.forwarder.root_storage_device;
            use_online_wad_enabled.Checked = Program.Config.application.use_online_wad_enabled;
            bypass_rom_size.Checked = Program.Config.application.bypass_rom_size;
            auto_fill_save_data.Checked = Program.Config.application.auto_fill_save_data;
            auto_prefill.Checked = Program.Config.application.auto_prefill;
            default_target_project_tb.Text = Program.Config.application.default_target_filename;
            default_target_wad_tb.Text = Program.Config.application.default_export_filename;

            // BIOS files
            bios_filename_neo.Text = Program.Config.paths.bios_neo;
            bios_filename_psx.Text = Program.Config.paths.bios_psx;

            // Banner region
            banner_regions.Items.Clear();
            banner_regions.Items.AddRange(new string[] { Program.Lang.String("automatic"), Program.Lang.String("region_j"), Program.Lang.String("region_u"), Program.Lang.String("region_e"), Program.Lang.String("region_k") });
            banner_regions.SelectedIndex = Program.Config.application.default_banner_region;

            // Use custom database
            use_custom_database.Checked = File.Exists(Program.Config.paths.database);
            if (!File.Exists(Program.Config.paths.database) && use_custom_database.Checked)
            {
                use_custom_database.Checked = false;
                Program.Config.paths.database = null;
                Program.Config.Save();
            }

            GetBanners.Visible = Program.DebugMode;

            // NES
            vc_nes_palettelist.SelectedIndex = Program.Config.nes.palette;
            vc_nes_palette_banner_usage.Checked = Program.Config.nes.palette_banner_usage;

            // SNES
            vc_snes_patch_volume.Checked = Program.Config.snes.patch_volume;
            vc_snes_patch_nodark.Checked = Program.Config.snes.patch_nodark;
            vc_snes_patch_nocc.Checked = Program.Config.snes.patch_nocc;
            vc_snes_patch_nosuspend.Checked = Program.Config.snes.patch_nosuspend;
            vc_snes_patch_nosave.Checked = Program.Config.snes.patch_nosave;
            vc_snes_patch_widescreen.Checked = Program.Config.snes.patch_widescreen;
            vc_snes_patch_nocheck.Checked = Program.Config.snes.patch_nocheck;
            vc_snes_patch_wiimote.Checked = Program.Config.snes.patch_wiimote;

            // N64
            vc_n64_patch_fixbrightness.Checked = Program.Config.n64.patch_nodark;
            vc_n64_patch_fixcrashes.Checked = Program.Config.n64.patch_crashfix;
            vc_n64_patch_expandedram.Checked = Program.Config.n64.patch_expandedram;
            vc_n64_patch_autosizerom.Checked = Program.Config.n64.patch_autoromsize;
            vc_n64_patch_cleantextures.Checked = Program.Config.n64.patch_cleantextures;
            // vc_n64_patch_widescreen.Checked = Program.Config.n64.patch_widescreen;
            vc_n64_romc_type_list.SelectedIndex = Program.Config.n64.romc_type;

            // SEGA
            label1.Text = Program.Config.sega.console_brightness;
            SEGA_console_brightness.Value = int.Parse(label1.Text);
            vc_sega_save_sram.Checked = Program.Config.sega.save_sram == "1";
            vc_sega_dev_mdpad_enable_6b.Checked = Program.Config.sega.dev_mdpad_enable_6b == "1";
            vc_sega_countries.SelectedIndex = Program.Config.sega.country switch { "jp" => 0, "us" => 1, _ => 2 };
            vc_sega_console_disableresetbutton.Checked = Program.Config.sega.console_disableresetbutton == "1";

            // PCE
            vc_pce_region.SelectedIndex = int.Parse(Program.Config.pce.EUROPE);
            vc_pce_padbutton_switch.Checked = Program.Config.pce.PADBUTTON == "6";
            vc_pce_backupram.Checked = Program.Config.pce.BACKUPRAM == "1";
            vc_pce_sgenable.Checked = Program.Config.pce.SGENABLE == "1";
            vc_pce_y_offset.Value = int.Parse(Program.Config.pce.YOFFSET);
            vc_pce_hide_overscan.Checked = Program.Config.pce.HIDEOVERSCAN == "1";
            vc_pce_raster.Checked = Program.Config.pce.RASTER == "1";
            vc_pce_sprline.Checked = Program.Config.pce.SPRLINE == "1";

            // NEO-GEO
            vc_neo_bios_list.SelectedIndex = Program.Config.neo.bios switch { "VC2" => 1, "VC3" => 2, _ => 0 };

            // FLASH
            flash_update_frame_rate.Value = int.Parse(Program.Config.flash.update_frame_rate);
            flash_save_data_enable.Checked = Program.Config.flash.shared_object_capability == "on";
            // flash_vff_sync_on_write.Checked = Program.Config.flash.vff_sync_on_write == "on";
            flash_vff_cache_size.SelectedItem = flash_vff_cache_size.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Program.Config.flash.vff_cache_size);
            flash_persistent_storage_total.SelectedItem = flash_persistent_storage_total.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Program.Config.flash.persistent_storage_total);
            flash_persistent_storage_per_movie.SelectedItem = flash_persistent_storage_per_movie.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Program.Config.flash.persistent_storage_per_movie);
            flash_quality_list.SelectedIndex = Program.Config.flash.quality switch { "high" => 0, "medium" => 1, _ => 2 };
            flash_mouse.Checked = Program.Config.flash.mouse == "on";
            flash_qwerty_keyboard.Checked = Program.Config.flash.qwerty_keyboard == "on";
            flash_strap_reminder_list.SelectedIndex = Program.Config.flash.strap_reminder switch { "none" => 0, "normal" => 1, _ => 2 };
            flash_fullscreen.Checked = Program.Config.flash.fullscreen == "yes";

            // flash_vff_sync_on_write.Enabled = flash_save_data_enable.Checked;
            flash_vff_cache_size_l.Enabled = flash_vff_cache_size.Enabled = flash_save_data_enable.Checked;
            flash_persistent_storage_total_l.Enabled = flash_persistent_storage_total.Enabled = flash_save_data_enable.Checked;
            flash_persistent_storage_per_movie_l.Enabled = flash_persistent_storage_per_movie.Enabled = flash_save_data_enable.Checked;

            ToggleSwitchText();

#endregion

            // -----------------------------

            dirtyOption1 = languages.SelectedIndex;
            dirtyOption2 = reset_all_dialogs.Checked;
        }

        private void Loading(object sender, EventArgs e)
        {
            TreeView.Select();
            RefreshForm();
            isShown = true;
        }

        private void CustomDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (use_custom_database.Checked && (!File.Exists(Program.Config.paths.database) || string.IsNullOrWhiteSpace(Program.Config.paths.database)))
            {
                using OpenFileDialog dialog = new() { DefaultExt = ".json", CheckFileExists = true, AddExtension = true, Filter = "*.json|*.json", Title = use_custom_database.Text.Replace("&", "") };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var database = new ChannelDatabase(Platform.NES, dialog.FileName);
                        Program.Config.paths.database = dialog.FileName;
                    }
                    catch
                    {
                        MessageBox.Show(Program.Lang.Msg(2), 0, MessageBox.Icons.Warning);
                        Program.Config.paths.database = null;
                        use_custom_database.Checked = false;
                    }
                }
                else
                {
                    Program.Config.paths.database = null;
                    use_custom_database.Checked = false;
                }
            }

            else if (!use_custom_database.Checked) Program.Config.paths.database = null;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            var lng = languages.SelectedIndex == 0 ? "sys" : "en";
            if (lng != "sys")
                foreach (var item in Program.Lang.List)
                    if (item.Value == languages.SelectedItem.ToString())
                        lng = item.Key;

            Program.Config.application.language = lng;
            // Program.Lang = new Language(lng);

            // -------------------------------------------
            // Other settings
            // -------------------------------------------

            bool toggledOnline = Program.Config.application.use_online_wad_enabled != use_online_wad_enabled.Checked;
            Program.Config.application.image_interpolation = image_interpolation_modes.SelectedIndex;
            Program.Config.application.use_online_wad_enabled = use_online_wad_enabled.Checked;
            Program.Config.application.bypass_rom_size = bypass_rom_size.Checked;
            Program.Config.application.auto_fill_save_data = auto_fill_save_data.Checked;
            Program.Config.application.auto_prefill = auto_prefill.Checked;
            Program.Config.application.default_target_filename = default_target_project_tb.Text;
            Program.Config.application.default_export_filename = default_target_wad_tb.Text;

            // -------------------------------------------
            // BIOS files
            // -------------------------------------------

            Program.Config.paths.bios_neo = bios_filename_neo.Text;
            Program.Config.paths.bios_psx = bios_filename_psx.Text;

            // -------------------------------------------
            // Platform-specific settings
            // -------------------------------------------

            Program.Config.application.default_banner_region = banner_regions.SelectedIndex;
            Program.Config.application.default_injection_method_nes = injection_methods_nes.SelectedIndex;
            Program.Config.application.default_injection_method_snes = injection_methods_snes.SelectedIndex;
            Program.Config.application.default_injection_method_n64 = injection_methods_n64.SelectedIndex;
            Program.Config.application.default_injection_method_sega = injection_methods_sega.SelectedIndex;

            Program.Config.forwarder.root_storage_device = forwarder_type.SelectedIndex;
            Program.Config.forwarder.show_bios_screen = toggleSwitch2.Checked;

            Program.Config.nes.palette = vc_nes_palettelist.SelectedIndex;
            Program.Config.nes.palette_banner_usage = vc_nes_palette_banner_usage.Checked;

            Program.Config.snes.patch_volume = vc_snes_patch_volume.Checked;
            Program.Config.snes.patch_nodark = vc_snes_patch_nodark.Checked;
            Program.Config.snes.patch_nocc = vc_snes_patch_nocc.Checked;
            Program.Config.snes.patch_nosuspend = vc_snes_patch_nosuspend.Checked;
            Program.Config.snes.patch_nosave = vc_snes_patch_nosave.Checked;
            Program.Config.snes.patch_widescreen = vc_snes_patch_widescreen.Checked;
            Program.Config.snes.patch_nocheck = vc_snes_patch_nocheck.Checked;
            Program.Config.snes.patch_wiimote = vc_snes_patch_wiimote.Checked;

            Program.Config.n64.patch_nodark = vc_n64_patch_fixbrightness.Checked;
            Program.Config.n64.patch_crashfix = vc_n64_patch_fixcrashes.Checked;
            Program.Config.n64.patch_expandedram = vc_n64_patch_expandedram.Checked;
            Program.Config.n64.patch_autoromsize = vc_n64_patch_autosizerom.Checked;
            Program.Config.n64.patch_cleantextures = vc_n64_patch_cleantextures.Checked;
            // Program.Config.n64.patch_widescreen = vc_n64_patch_widescreen.Checked;
            Program.Config.n64.romc_type = vc_n64_romc_type_list.SelectedIndex;

            Program.Config.sega.console_brightness = label1.Text;
            Program.Config.sega.save_sram = vc_sega_save_sram.Checked ? "1" : "0";
            Program.Config.sega.dev_mdpad_enable_6b = vc_sega_dev_mdpad_enable_6b.Checked ? "1" : "0";
            Program.Config.sega.country = vc_sega_countries.SelectedIndex switch { 0 => "jp", 1 => "us", _ => "eu" };
            Program.Config.sega.console_disableresetbutton = vc_sega_console_disableresetbutton.Checked ? "1" : null;

            Program.Config.pce.EUROPE = vc_pce_region.SelectedIndex.ToString();
            Program.Config.pce.PADBUTTON = vc_pce_padbutton_switch.Checked ? "6" : "2";
            Program.Config.pce.SGENABLE = vc_pce_sgenable.Checked ? "1" : "0";
            Program.Config.pce.BACKUPRAM = vc_pce_backupram.Checked ? "1" : "0";
            Program.Config.pce.YOFFSET = vc_pce_y_offset.Value.ToString();
            Program.Config.pce.HIDEOVERSCAN = vc_pce_hide_overscan.Checked ? "1" : "0";
            Program.Config.pce.RASTER = vc_pce_raster.Checked ? "1" : "0";
            Program.Config.pce.SPRLINE = vc_pce_sprline.Checked ? "1" : "0";

            Program.Config.flash.update_frame_rate = flash_update_frame_rate.Value.ToString();
            Program.Config.flash.shared_object_capability = flash_save_data_enable.Checked ? "on" : "off";
            // Program.Config.flash.vff_sync_on_write = flash_vff_sync_on_write.Checked ? "on" : "off";
            Program.Config.flash.vff_cache_size = flash_vff_cache_size.SelectedItem.ToString();
            Program.Config.flash.persistent_storage_total = flash_persistent_storage_total.SelectedItem.ToString();
            Program.Config.flash.persistent_storage_per_movie = flash_persistent_storage_per_movie.SelectedItem.ToString();
            Program.Config.flash.quality = flash_quality_list.SelectedIndex switch { 0 => "high", 1 => "medium", _ => "low" };
            Program.Config.flash.mouse = flash_mouse.Checked ? "on" : "off";
            Program.Config.flash.qwerty_keyboard = flash_qwerty_keyboard.Checked ? "on" : "off";
            Program.Config.flash.strap_reminder = flash_strap_reminder_list.SelectedIndex switch { 0 => "none", 1 => "normal", _ => "no_ex" };
            Program.Config.flash.hbm_no_save = Program.Config.flash.shared_object_capability == "on" ? "no" : "yes";
            Program.Config.flash.fullscreen = flash_fullscreen.Checked ? "yes" : "no";

            switch (vc_neo_bios_list.SelectedIndex)
            {
                case 0:
                    Program.Config.neo.bios = "VC1";
                    break;

                case 1:
                    Program.Config.neo.bios = "VC2";
                    break;

                case 2:
                    Program.Config.neo.bios = "VC3";
                    break;
            }

            // -------------------------------------------

            if (reset_all_dialogs.Checked)
            {
                Program.Config.application.donotshow_000 = false;
                Program.Config.application.donotshow_001 = false;
            }

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            Program.Config.Save();

            bool isDirty = isShown
                && (dirtyOption1 != languages.SelectedIndex
                 || dirtyOption2 != reset_all_dialogs.Checked);
            if (isDirty) MessageBox.Show(Program.Lang.Msg(0), MessageBox.Buttons.Ok, MessageBox.Icons.Information);

            if (toggledOnline)
                Program.MainForm.UpdateConfig();

            isShown = false;
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Program.Config = new(Paths.Config);
            DialogResult = DialogResult.Cancel;
        }

        private void DownloadBanners_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();

            /* var WADs = new List<(string ID, Platform c, string file)>()
            {
                ("FCWP", Platform.NES, "nes"),
                ("FCWJ", Platform.NES, "jp_fc"),
                ("FCWQ", Platform.NES, "kr_fc"),
                ("JBDP", Platform.SNES, "snes"),
                ("JBDJ", Platform.SNES, "jp_sfc"),
                ("JBDT", Platform.SNES, "kr_sfc"),
                ("NAAP", Platform.N64, "n64"),
                ("NAAJ", Platform.N64, "jp_n64"),
                ("NABT", Platform.N64, "kr_n64"),
                ("LAGP", Platform.SMS, "sms"),
                ("LAGJ", Platform.SMS, "jp_sms"),
                ("MAPE", Platform.SMD, "gen"),
                ("MAPP", Platform.SMD, "smd"),
                ("MAPJ", Platform.SMD, "jp_smd"),
                ("PAAP", Platform.PCE, "tg16"),
                ("PAGJ", Platform.PCE, "jp_pce"),
                ("EAJP", Platform.NEO, "neogeo"),
                ("EAJJ", Platform.NEO, "jp_neogeo"),
                ("C9YE", Platform.C64, "us_c64"),
                ("C9YP", Platform.C64, "eu_c64"),
                ("XAGJ", Platform.MSX, "jp_msx1"),
                ("XAPJ", Platform.MSX, "jp_msx2"),
                ("WNAP", Platform.Flash, "flash")
            };

            foreach (var item in WADs)
            {
                BannerHelper.ExportBanner(item.ID, item.c, item.file);
                System.Media.SystemSounds.Beep.Play();
            }

            System.Media.SystemSounds.Asterisk.Play(); */

            // BannerHelper.ModifyBanner("RPG MAKER",        FileDatas.WADBanners.n64,     "rpgm.app",     16,     FileDatas.WADBanners.rpgm_banner,       FileDatas.WADBanners.rpgm_icon);
            // BannerHelper.ModifyBanner("ＲＰＧツクール",    FileDatas.WADBanners.jp_n64,  "jp_rpgm.app",  16,     FileDatas.WADBanners.jp_rpgm_banner,    FileDatas.WADBanners.rpgm_icon);
            // BannerHelper.ModifyBanner("PLAYSTATION", FileDatas.WADBanners.n64, "psx.app", 15, FileDatas.WADBanners.psx_banner, FileDatas.WADBanners.psx_icon);
            // BannerHelper.ModifyBanner("プレイステーション", FileDatas.WADBanners.jp_n64, "jp_psx.app", 15, FileDatas.WADBanners.psx_banner, FileDatas.WADBanners.psx_icon);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string v_selected = e.Node.Name.Substring(4).ToLower();

            bool[] isVisible = new bool[]
                {
                    v_selected == "0",
                    v_selected == "1",
                    v_selected == "2",
                    v_selected == "3",
                    v_selected == "nes",
                    v_selected == "snes",
                    v_selected == "n64",
                    v_selected == "sega",
                    v_selected == "pce",
                    v_selected == "neo",
                    v_selected == "flash",
                    v_selected == "forwarders"
                };

            foreach (var item in isVisible)
            {
                if (item == true)
                {
                    panel1.Visible = isVisible[0];
                    panel2.Visible = isVisible[1];
                    default_injection_methods.Visible = isVisible[2];
                    bios_files.Visible = isVisible[3];
                    vc_nes.Visible = isVisible[4];
                    vc_snes.Visible = isVisible[5];
                    vc_n64.Visible = isVisible[6];
                    vc_sega.Visible = isVisible[7];
                    vc_pce.Visible = isVisible[8];
                    vc_neo.Visible = isVisible[9];
                    adobe_flash.Visible = isVisible[10];
                    forwarder.Visible = isVisible[11];
                }
            }
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            ToggleSwitchText();

            // flash_vff_sync_on_write.Enabled = flash_save_data_enable.Checked;
            flash_vff_cache_size_l.Enabled = flash_vff_cache_size.Enabled = flash_save_data_enable.Checked;
            flash_persistent_storage_total_l.Enabled = flash_persistent_storage_total.Enabled = flash_save_data_enable.Checked;
            flash_persistent_storage_per_movie_l.Enabled = flash_persistent_storage_per_movie.Enabled = flash_save_data_enable.Checked;
        }

        private void ToggleSwitchText()
        {
            vc_pce_padbutton.Text = Program.Lang.Toggle(vc_pce_padbutton_switch.Checked, "padbutton", vc_pce.Tag.ToString());
        }

        private void BrightnessValue_Scroll(object sender, EventArgs e) => label1.Text = SEGA_console_brightness.Value.ToString();

        private void BrowseBIOS(object sender, EventArgs e)
        {
            Platform platform = (Platform)Enum.Parse(typeof(Platform), (sender as Button).Name.Replace("browse_bios_", null).ToUpper());
            
            TextBox textbox = platform switch {
                Platform.PSX => bios_filename_psx,
                Platform.NEO => bios_filename_neo,
                _ => null
            };

            using OpenFileDialog ImportBIOS = new()
            {
                Title = Program.Lang.String("browse_bios", Name).Replace("&", ""),
                Filter = ".bin (*.bin)|*.bin",
                DefaultExt = "bin",
                CheckFileExists = true,
                CheckPathExists = true,
                SupportMultiDottedExtensions = true,
                AddExtension = true,
                Multiselect = false,
            };

            if (platform == Platform.NEO)
            {
                ImportBIOS.Filter = ".ROM (*.rom)|*.rom";
                ImportBIOS.DefaultExt = "rom";
            }

            ImportBIOS.Filter += "|" + Program.Lang.String("filter.zip") + Program.Lang.String("filter");

            var dialog = ImportBIOS.ShowDialog();
            if (dialog == DialogResult.OK && BIOS.Verify(ImportBIOS.FileName, platform))
            {
                textbox.Text = ImportBIOS.FileName;
            }
            else if (dialog == DialogResult.Cancel)
            {
                textbox.Clear();
            }
        }
    }
}
