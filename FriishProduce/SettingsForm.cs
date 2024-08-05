using static FriishProduce.Properties.Settings;
using FriishProduce.Options;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class SettingsForm : Form
    {
        private bool isShown = false;
        private bool isDirty = false;
        private bool nodeLocked = false;
        private string nodeName;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SaveAll()
        {
            FORWARDER.Default.Save();
            VC_NES.Default.Save();
            VC_N64.Default.Save();
            VC_SEGA.Default.Save();
            VC_PCE.Default.Save();
            VC_NEO.Default.Save();
            ADOBEFLASH.Default.Save();
        }

        public void RefreshForm()
        {
            Program.Lang.Control(this);
            Program.Lang.Control(vc_nes);
            Program.Lang.Control(vc_n64);
            Program.Lang.Control(vc_sega);
            Program.Lang.Control(vc_pce);
            Program.Lang.Control(vc_neo);
            Program.Lang.Control(adobe_flash);

            Text = Program.Lang.String("settings");
            TreeView.Nodes[0].Text = Program.Lang.String(TreeView.Nodes[0].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[1].Text = Program.Lang.String(TreeView.Nodes[1].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[2].Text = Program.Lang.String(TreeView.Nodes[2].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[1].Expand();
            TreeView.Nodes[1].Nodes[0].Text = Program.Lang.String("vc");
            TreeView.Nodes[1].Nodes[0].Nodes[0].Text = Program.Lang.Console(Platform.NES);
            TreeView.Nodes[1].Nodes[0].Nodes[1].Text = Program.Lang.Console(Platform.N64);
            TreeView.Nodes[1].Nodes[0].Nodes[2].Text = label8.Text = Program.Lang.String("group1", "platforms");
            TreeView.Nodes[1].Nodes[0].Nodes[3].Text = Program.Lang.Console(Platform.PCE);
            TreeView.Nodes[1].Nodes[0].Nodes[4].Text = Program.Lang.Console(Platform.NEO);
            TreeView.Nodes[1].Nodes[1].Text = Program.Lang.Console(Platform.Flash);
            TreeView.Nodes[1].Nodes[2].Text = Program.Lang.String("forwarders", "platforms");

            // -----------------------------

            // -------------------------------------------
            // Add all languages
            // -------------------------------------------
            lngList.Items.Clear();
            lngList.Items.Add("<" + Program.Lang.String("system_default", Name) + ">");
            foreach (var item in Program.Lang.List)
                lngList.Items.Add(item.Value);

            if (Default.language == "sys") lngList.SelectedIndex = 0;
            else lngList.SelectedIndex = Program.Lang.List.Keys.ToList().IndexOf(Default.language) + 1;

            #region Localization
            image_interpolation_mode.Text = Program.Lang.String(image_interpolation_mode.Name, "projectform");
            image_interpolation_mode.Items.Clear();
            image_interpolation_mode.Items.AddRange(Program.Lang.StringArray("image_interpolation_mode", "projectform"));
            image_interpolation_mode.SelectedIndex = Default.image_interpolation;

            gamedata_source_image_list.Items.Clear();
            gamedata_source_image_list.Items.AddRange(new string[] { Program.Lang.String("automatic"), "https://thumbnails.libretro.com/", "https://github.com/libretro/libretro-thumbnails/" });
            gamedata_source_image_list.SelectedIndex = Default.gamedata_source_image;

            retrieve_gamedata_online.Text = Program.Lang.String(retrieve_gamedata_online.Name, "mainform") != "undefined" ? Program.Lang.String(retrieve_gamedata_online.Name, "mainform") : Program.Lang.String(retrieve_gamedata_online.Name, Name);
            
            default_save_as_parameters.Font = new Font(default_save_as_parameters.Font, FontStyle.Bold);
            Program.AutoSizeControl(default_save_as_filename_tb, default_save_as_project);
            Program.AutoSizeControl(default_export_filename_tb, default_save_as_wad);
            int maxX = Math.Max(default_save_as_filename_tb.Location.X, default_export_filename_tb.Location.X), maxWidth = Math.Min(default_save_as_filename_tb.Width, default_export_filename_tb.Width);
            default_save_as_filename_tb.Location = new Point(maxX, default_save_as_filename_tb.Location.Y);
            default_export_filename_tb.Location = new Point(maxX, default_export_filename_tb.Location.Y);
            default_save_as_filename_tb.Width = default_export_filename_tb.Width = maxWidth;

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

            injection_methods_nes.SelectedIndex = Default.default_injection_method_nes;
            injection_methods_snes.SelectedIndex = Default.default_injection_method_snes;
            injection_methods_n64.SelectedIndex = Default.default_injection_method_n64;
            injection_methods_sega.SelectedIndex = Default.default_injection_method_sega;

            // -----------------------------

            Program.Lang.String(forwarder_root_device, "projectform");
            Program.Lang.String(bios_settings, "forwarder");
            Program.Lang.String(show_bios_screen, "forwarder");

            // -----------------------------

            Program.Lang.String(vc_nes_palette, "vc_nes");
            Program.Lang.String(vc_nes_palette_use_on_banner, "vc_nes");
            Program.Lang.String(vc_nes_palettelist, "vc_nes");

            // -----------------------------

            Program.Lang.String(vc_n64_patch_fixbrightness, "vc_n64");
            Program.Lang.String(vc_n64_patch_fixcrashes, "vc_n64");
            Program.Lang.String(vc_n64_patch_expandedram, "vc_n64");
            Program.Lang.String(vc_n64_patch_autosizerom, "vc_n64");
            // Program.Lang.String(vc_n64_patch_widescreen, "vc_n64");
            Program.Lang.String(vc_n64_romc_type, "vc_n64");
            Program.Lang.String(vc_n64_romc_type_list, "vc_n64");
            Program.AutoSizeControl(vc_n64_romc_type_list, vc_n64_romc_type);

            // -----------------------------

            Program.Lang.String(vc_sega_country_l);
            vc_sega_dev_mdpad_enable_6b.Text = string.Format(Program.Lang.String("dev_mdpad_enable_6b", "vc_sega"), Program.Lang.Console(Platform.SMD));
            Program.Lang.String(vc_sega_console_disableresetbutton, "vc_sega");

            vc_sega_country.Items.Clear();
            vc_sega_country.Items.AddRange(new string[] { Program.Lang.String("region_j"), Program.Lang.String("region_u"), Program.Lang.String("region_e") });

            // -----------------------------

            Program.Lang.String(vc_pce_y_offset_l, "vc_pce");
            Program.Lang.String(vc_pce_hide_overscan, "vc_pce");
            Program.Lang.String(vc_pce_raster, "vc_pce");
            Program.Lang.String(vc_pce_sprline, "vc_pce");

            // -----------------------------

            Program.Lang.String(vc_neo_bios_list, "vc_neo");
            vc_neo_bios_list.Items.RemoveAt(0);

            // -----------------------------

            Program.Lang.String(flash_save_data, "projectform");
            Program.Lang.String(flash_save_data_enable, "projectform");
            Program.Lang.String(flash_vff_sync_on_write, "adobe_flash");
            Program.Lang.String(flash_vff_cache_size, "adobe_flash");
            Program.Lang.String(flash_controls, "adobe_flash");
            Program.Lang.String(flash_mouse, "adobe_flash");
            Program.Lang.String(flash_qwerty_keyboard, "adobe_flash");
            Program.Lang.String(flash_quality, "adobe_flash");
            Program.Lang.String(flash_quality_list, "adobe_flash");
            Program.Lang.String(flash_strap_reminder, "adobe_flash");
            Program.Lang.String(flash_strap_reminder_list, "adobe_flash");
            #endregion

            // -----------------------------

            // Update checker button
            check_for_updates.Enabled = !Default.auto_update_check || !Program.IsUpdated;

            // Defaults & forwarders
            auto_update_check.Checked = Default.auto_update_check;
            auto_retrieve_gamedata_online.Checked = Default.auto_retrieve_game_data;
            autolink_save_data.Checked = Default.link_save_data;
            reset_all_dialogs.Checked = false;
            toggleSwitch2.Checked = bool.Parse(FORWARDER.Default.show_bios_screen);
            FStorage_SD.Checked = FORWARDER.Default.root_storage_device == 0;
            FStorage_USB.Checked = FORWARDER.Default.root_storage_device == 1;
            default_export_filename_tb.Text = Default.default_export_filename;
            default_save_as_filename_tb.Text = Default.default_save_as_filename;

            #region use_custom_database
            bool clearCustomDatabase = !File.Exists(Default.custom_database);
            use_custom_database.Checked = File.Exists(Default.custom_database);

#if DEBUG
            GetBanners.Visible = true;
#else
            GetBanners.Visible = false;
#endif

            if (clearCustomDatabase && use_custom_database.Checked)
            {
                use_custom_database.Checked = false;
                Default.custom_database = null;
                Default.Save();
            }
            #endregion

            // NES
            vc_nes_palettelist.SelectedIndex = int.Parse(VC_NES.Default.palette);
            vc_nes_palette_use_on_banner.Checked = bool.Parse(VC_NES.Default.palette_use_on_banner);

            // N64
            vc_n64_patch_fixbrightness.Checked = bool.Parse(VC_N64.Default.patch_fixbrightness);
            vc_n64_patch_fixcrashes.Checked = bool.Parse(VC_N64.Default.patch_fixcrashes);
            vc_n64_patch_expandedram.Checked = bool.Parse(VC_N64.Default.patch_expandedram);
            vc_n64_patch_autosizerom.Checked = bool.Parse(VC_N64.Default.patch_autosizerom);
            // vc_n64_patch_widescreen.Checked = bool.Parse(VC_N64.Default.patch_widescreen);
            vc_n64_romc_type_list.SelectedIndex = int.Parse(VC_N64.Default.romc_type);

            // SEGA
            label1.Text = VC_SEGA.Default.console_brightness;
            SEGA_console_brightness.Value = int.Parse(label1.Text);
            vc_sega_save_sram.Checked = VC_SEGA.Default.save_sram == "1";
            vc_sega_dev_mdpad_enable_6b.Checked = VC_SEGA.Default.dev_mdpad_enable_6b == "1";
            vc_sega_country.SelectedIndex = VC_SEGA.Default.country switch { "jp" => 0, "us" => 1, _ => 2 };
            vc_sega_console_disableresetbutton.Checked = VC_SEGA.Default.console_disableresetbutton == "1";

            // PCE
            vc_pce_backupram.Checked = VC_PCE.Default.BACKUPRAM == "1";
            vc_pce_europe_switch.Checked = VC_PCE.Default.EUROPE == "1";
            vc_pce_sgenable_switch.Checked = VC_PCE.Default.SGENABLE == "1";
            vc_pce_padbutton_switch.Checked = VC_PCE.Default.PADBUTTON == "6";
            vc_pce_y_offset.Value = int.Parse(VC_PCE.Default.YOFFSET);
            vc_pce_hide_overscan.Checked = VC_PCE.Default.HIDEOVERSCAN == "1";
            vc_pce_raster.Checked = VC_PCE.Default.RASTER == "1";
            vc_pce_sprline.Checked = VC_PCE.Default.SPRLINE == "1";

            // NEO-GEO
            vc_neo_bios_list.SelectedIndex = VC_NEO.Default.bios.ToLower() switch { "vc1" => 0, "vc3" => 2, _ => 1 };

            // FLASH
            flash_save_data_enable.Checked = ADOBEFLASH.Default.shared_object_capability == "on";
            flash_vff_sync_on_write.Checked = ADOBEFLASH.Default.vff_sync_on_write == "on";
            flash_vff_cache_size_list.SelectedItem = flash_vff_cache_size_list.Items.Cast<string>().FirstOrDefault(n => n.ToString() == ADOBEFLASH.Default.vff_cache_size);
            flash_quality_list.SelectedIndex = ADOBEFLASH.Default.quality switch { "high" => 0, "medium" => 1, _ => 2 };
            flash_mouse.Checked = ADOBEFLASH.Default.mouse == "on";
            flash_qwerty_keyboard.Checked = ADOBEFLASH.Default.qwerty_keyboard == "on";
            flash_strap_reminder_list.SelectedIndex = ADOBEFLASH.Default.strap_reminder switch { "none" => 0, "normal" => 1, _ => 2 };

            flash_vff_cache_size.Enabled = flash_vff_sync_on_write.Enabled = flash_vff_cache_size_list.Enabled = flash_save_data_enable.Checked;

            ToggleSwitchText();
            Program.AutoSizeControl(gamedata_source_image_list, gamedata_source_image);
            Program.AutoSizeControl(vc_sega_country, vc_sega_country_l);

            // -----------------------------
        }

        private void Loading(object sender, EventArgs e)
        {
            TreeView.Select();
            RefreshForm();
            isShown = true;
        }

        private void CustomDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (use_custom_database.Checked && (!File.Exists(Default.custom_database) || string.IsNullOrWhiteSpace(Default.custom_database)))
            {
                using (var dialog = new OpenFileDialog() { DefaultExt = ".json", CheckFileExists = true, AddExtension = true, Filter = "*.json|*.json", Title = use_custom_database.Text })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var database = new ChannelDatabase(Platform.NES, dialog.FileName);
                            Default.custom_database = dialog.FileName;
                        }
                        catch
                        {
                            MessageBox.Show(Program.Lang.Msg(2), 0, MessageBox.Icons.Warning);
                            Default.custom_database = null;
                            use_custom_database.Checked = false;
                        }
                    }
                    else
                    {
                        Default.custom_database = null;
                        use_custom_database.Checked = false;
                    }
                }

            }

            else if (!use_custom_database.Checked) Default.custom_database = null;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            var lng = lngList.SelectedIndex == 0 ? "sys" : "en";
            if (lng != "sys")
                foreach (var item in Program.Lang.List)
                    if (item.Value == lngList.SelectedItem.ToString())
                        lng = item.Key;

            Default.language = lng;
            // Program.Lang = new Language(lng);

            // -------------------------------------------
            // Other settings
            // -------------------------------------------

            Default.auto_update_check = auto_update_check.Checked;
            Default.link_save_data = autolink_save_data.Checked;
            Default.gamedata_source_image = gamedata_source_image_list.SelectedIndex;
            Default.image_interpolation = image_interpolation_mode.SelectedIndex;
            Default.auto_retrieve_game_data = auto_retrieve_gamedata_online.Checked;
            Default.default_export_filename = default_export_filename_tb.Text;
            Default.default_save_as_filename = default_save_as_filename_tb.Text;

            Default.default_injection_method_nes = injection_methods_nes.SelectedIndex;
            Default.default_injection_method_snes = injection_methods_snes.SelectedIndex;
            Default.default_injection_method_n64 = injection_methods_n64.SelectedIndex;
            Default.default_injection_method_sega = injection_methods_sega.SelectedIndex;
            Default.Save();

            FORWARDER.Default.root_storage_device = FStorage_USB.Checked ? 1 : 0;
            FORWARDER.Default.show_bios_screen = toggleSwitch2.Checked.ToString();

            VC_NES.Default.palette = vc_nes_palettelist.SelectedIndex.ToString();
            VC_NES.Default.palette_use_on_banner = vc_nes_palette_use_on_banner.Checked.ToString();

            VC_N64.Default.patch_fixbrightness = vc_n64_patch_fixbrightness.Checked.ToString();
            VC_N64.Default.patch_fixcrashes = vc_n64_patch_fixcrashes.Checked.ToString();
            VC_N64.Default.patch_expandedram = vc_n64_patch_expandedram.Checked.ToString();
            VC_N64.Default.patch_autosizerom = vc_n64_patch_autosizerom.Checked.ToString();
            // VC_N64.Default.patch_widescreen = vc_n64_patch_widescreen.Checked.ToString();
            VC_N64.Default.romc_type = vc_n64_romc_type_list.SelectedIndex.ToString();

            VC_SEGA.Default.console_brightness = label1.Text;
            VC_SEGA.Default.save_sram = vc_sega_save_sram.Checked ? "1" : "0";
            VC_SEGA.Default.dev_mdpad_enable_6b = vc_sega_dev_mdpad_enable_6b.Checked ? "1" : "0";
            VC_SEGA.Default.country = vc_sega_country.SelectedIndex switch { 0 => "jp", 1 => "us", _ => "eu" };
            VC_SEGA.Default.console_disableresetbutton = vc_sega_console_disableresetbutton.Checked ? "1" : null;

            VC_PCE.Default.BACKUPRAM = vc_pce_backupram.Checked ? "1" : "0";
            VC_PCE.Default.EUROPE = vc_pce_europe_switch.Checked ? "1" : "0";
            VC_PCE.Default.SGENABLE = vc_pce_sgenable_switch.Checked ? "1" : "0";
            VC_PCE.Default.PADBUTTON = vc_pce_padbutton_switch.Checked ? "6" : "2";
            VC_PCE.Default.YOFFSET = vc_pce_y_offset.Value.ToString();
            VC_PCE.Default.HIDEOVERSCAN = vc_pce_hide_overscan.Checked ? "1" : "0";
            VC_PCE.Default.RASTER = vc_pce_raster.Checked ? "1" : "0";
            VC_PCE.Default.SPRLINE = vc_pce_sprline.Checked ? "1" : "0";

            ADOBEFLASH.Default.shared_object_capability = flash_save_data_enable.Checked ? "on" : "off";
            ADOBEFLASH.Default.vff_sync_on_write = flash_vff_sync_on_write.Checked ? "on" : "off";
            ADOBEFLASH.Default.vff_cache_size = flash_vff_cache_size_list.SelectedItem.ToString();
            ADOBEFLASH.Default.quality = flash_quality_list.SelectedIndex switch { 0 => "high", 1 => "medium", _ => "low" };
            ADOBEFLASH.Default.mouse = flash_mouse.Checked ? "on" : "off";
            ADOBEFLASH.Default.qwerty_keyboard = flash_qwerty_keyboard.Checked ? "on" : "off";
            ADOBEFLASH.Default.strap_reminder = flash_strap_reminder_list.SelectedIndex switch { 0 => "none", 1 => "normal", _ => "no_ex" };
            ADOBEFLASH.Default.hbm_no_save = ADOBEFLASH.Default.shared_object_capability == "on" ? "no" : "yes";

            switch (vc_neo_bios_list.SelectedIndex)
            {
                case 0:
                    VC_NEO.Default.bios = "VC1";
                    break;

                case 1:
                    VC_NEO.Default.bios = "VC2";
                    break;

                case 2:
                    VC_NEO.Default.bios = "VC3";
                    break;
            }

            // -------------------------------------------

            if (reset_all_dialogs.Checked)
            {
                Default.donotshow_000 = false;
            }

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            SaveAll();

            if (isDirty)
                MessageBox.Show(Program.Lang.Msg(0));

            isShown = false;
            isDirty = false;
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Default.Reload();
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
            if (!nodeLocked) nodeName = e.Node.Name;

            string v_selected = e.Node.Name.Substring(4).ToLower();

            bool[] isVisible = new bool[]
                {
                    v_selected == "0",
                    v_selected == "1",
                    v_selected == "2",
                    v_selected == "forwarders",
                    v_selected == "nes",
                    v_selected == "n64",
                    v_selected == "sega",
                    v_selected == "pce",
                    v_selected == "neo",
                    v_selected == "flash"
                };

            foreach (var item in isVisible)
            {
                if (item == true)
                {
                    panel1.Visible = isVisible[0];
                    panel2.Visible = isVisible[1];
                    panel3.Visible = isVisible[2];
                    forwarder.Visible = isVisible[3];
                    vc_nes.Visible = isVisible[4];
                    vc_n64.Visible = isVisible[5];
                    vc_sega.Visible = isVisible[6];
                    vc_pce.Visible = isVisible[7];
                    vc_neo.Visible = isVisible[8];
                    adobe_flash.Visible = isVisible[9];
                }
            }
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            ToggleSwitchText();
            flash_vff_cache_size.Enabled = flash_vff_sync_on_write.Enabled = flash_vff_cache_size_list.Enabled = flash_save_data_enable.Checked;
        }

        private void ToggleSwitchText()
        {
            vc_pce_europe.Text = Program.Lang.Toggle(vc_pce_europe_switch.Checked, "europe", vc_pce.Tag.ToString());
            vc_pce_sgenable.Text = Program.Lang.Toggle(vc_pce_sgenable_switch.Checked, "sgenable", vc_pce.Tag.ToString());
            vc_pce_padbutton.Text = Program.Lang.Toggle(vc_pce_padbutton_switch.Checked, "padbutton", vc_pce.Tag.ToString());
        }

        private void BrightnessValue_Scroll(object sender, EventArgs e) => label1.Text = SEGA_console_brightness.Value.ToString();

        private async void CheckUpdates_Click(object sender, EventArgs e)
        {
            if ((sender as Control).Name.ToLower() == auto_update_check.Name.ToLower())
            {
                if (!auto_update_check.Checked) check_for_updates.Enabled = true;
                else check_for_updates.Enabled = !Program.IsUpdated;
            }

            else
            {
                var isUpdated = await Updater.GetLatest();
                check_for_updates.Enabled = !isUpdated || !auto_update_check.Checked;
                if (isUpdated) MessageBox.Show(Program.Lang.Msg(9), MessageBox.Buttons.Ok, MessageBox.Icons.Information);
            }
        }

        private void MakeDirty(object sender, EventArgs e) { if (isShown) isDirty = true; }
    }
}
