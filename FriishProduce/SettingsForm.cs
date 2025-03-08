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

        private TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip tip = HTML.CreateToolTip();
        private int dirtyOption1;
        private int dirtyOption2;
        private bool dirtyOption3;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public void RefreshForm()
        {
            #region --- Localization / Appearance ---

            Text = Program.Lang.String("preferences");
            Program.Lang.Control(this);

            Theme.BtnSizes(GetBanners, b_ok, b_cancel);
            Theme.BtnLayout(this, GetBanners, b_ok, b_cancel);
            if (Theme.ChangeColors(this, false))
            {
                border.BackColor = bottomPanel2.BackColor = Theme.Colors.Form.Border;
                TreeView.ForeColor = ForeColor = Theme.Colors.Text;
                TreeView.BackColor = Theme.Colors.Dialog.BG;
            }

            TreeView.Nodes[0].Text = Program.Lang.String(TreeView.Nodes[0].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[1].Text = Program.Lang.String(TreeView.Nodes[1].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[2].Text = Program.Lang.String(TreeView.Nodes[2].Tag.ToString(), Tag.ToString());
            TreeView.Nodes[3].Text = Program.Lang.String(TreeView.Nodes[3].Tag.ToString(), Tag.ToString());

            var default_node = TreeView.Nodes[1];
            default_node.Text = Program.Lang.String(default_node.Tag.ToString(), Tag.ToString());
            default_node.Expand();
            default_node.Nodes[0].Text = Program.Lang.String(default_node.Nodes[0].Tag.ToString(), Tag.ToString());
            default_node.Nodes[1].Text = Program.Lang.String(default_node.Nodes[1].Tag.ToString(), Tag.ToString());
            default_node.Nodes[2].Text = Program.Lang.String("forwarders", "platforms");

            Program.Lang.ToolTip(tip, use_online_wad_enabled, null, use_online_wad_enabled.Text);
            Program.Lang.ToolTip(tip, bypass_rom_size, null, bypass_rom_size.Text);

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

            themes.Items.Clear();
            themes.Items.AddRange(Program.Lang.StringArray("theme", Name));
            themes.SelectedIndex = Program.Config.application.theme;

            image_interpolation_mode.Text = Program.Lang.String("image_interpolation_mode", "projectform");
            image_interpolation_modes.Items.Clear();
            image_interpolation_modes.Items.AddRange(Program.Lang.StringArray("image_interpolation_mode", "projectform"));
            image_interpolation_modes.SelectedIndex = Program.Config.application.image_interpolation;
            
            int maxX = Math.Max(default_target_project_tb.Location.X, default_target_wad_tb.Location.X), maxWidth = Math.Min(default_target_project_tb.Width, default_target_wad_tb.Width);
            default_target_project_tb.Location = new Point(maxX, default_target_project_tb.Location.Y);
            default_target_wad_tb.Location = new Point(maxX, default_target_wad_tb.Location.Y);
            default_target_project_tb.Width = default_target_wad_tb.Width = maxWidth;

            banner_region.Text = Program.Lang.String(banner_region.Name, "banner").TrimEnd(':').Trim();

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

            default_content_options_list.Items.Clear();
            default_content_options_list.Items.Add(Program.Lang.Console(Platform.NES));
            default_content_options_list.Items.Add(Program.Lang.Console(Platform.SNES));
            default_content_options_list.Items.Add(Program.Lang.Console(Platform.N64));
            default_content_options_list.Items.Add(Program.Lang.String("group1", "platforms"));
            default_content_options_list.Items.Add(Program.Lang.Console(Platform.PCE));
            default_content_options_list.Items.Add(Program.Lang.Console(Platform.NEO));
            default_content_options_list.Items.Add(Program.Lang.Console(Platform.Flash));
            // default_content_options_l.Items.Add(Program.Lang.Console(Platform.RPGM));

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

            // Debug-only options
#if DEBUG
            GetBanners.Visible = true;
#else
            GetBanners.Visible = false;
#endif
            theme.Enabled = Program.DebugMode;

#endregion

            // -----------------------------

            dirtyOption1 = languages.SelectedIndex;
            dirtyOption2 = themes.SelectedIndex;
            dirtyOption3 = reset_all_dialogs.Checked;
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
            // Language and theme settings
            // -------------------------------------------
            var lng = languages.SelectedIndex == 0 ? "sys" : "en";
            if (lng != "sys")
                foreach (var item in Program.Lang.List)
                    if (item.Value == languages.SelectedItem.ToString())
                        lng = item.Key;

            Program.Config.application.language = lng;
            // Program.Lang = new Language(lng);
            Program.Config.application.theme = themes.SelectedIndex;

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
                 || dirtyOption2 != themes.SelectedIndex
                 || dirtyOption3 != reset_all_dialogs.Checked);
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
                    v_selected == "1a",
                    v_selected == "1b",
                    v_selected == "1c",
                    v_selected == "2",
                    v_selected == "3",
                };

            foreach (var item in isVisible)
            {
                if (item == true)
                {
                    panel1.Visible = isVisible[0];
                    panel2.Visible = isVisible[1];
                    panel3.Visible = isVisible[2];
                    forwarder.Visible = isVisible[3];
                    default_injection_methods.Visible = isVisible[4];
                    bios_files.Visible = isVisible[5];
                }
            }
        }

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

            if (dialog == DialogResult.OK)
                if (BIOS.Verify(ImportBIOS.FileName, platform))
                    textbox.Text = ImportBIOS.FileName;

            else if (dialog == DialogResult.Cancel)
                textbox.Clear();
        }

        private void OpenContentOptions(object sender, EventArgs e)
        {
            ContentOptions options = null;

            if (default_content_options_list.SelectedIndex < 0) return;

            else if (default_content_options_list.SelectedIndex == 0)
                options = new Options_VC_NES() { Binding = Program.Config.nes };

            else if (default_content_options_list.SelectedIndex == 1)
                options = new Options_VC_SNES() { Binding = Program.Config.snes };

            else if (default_content_options_list.SelectedIndex == 2)
                options = new Options_VC_N64() { Binding = Program.Config.n64 };

            else if (default_content_options_list.SelectedIndex == 3)
                options = new Options_VC_SEGA() { Binding = Program.Config.sega };

            else if (default_content_options_list.SelectedIndex == 4)
                options = new Options_VC_PCE() { Binding = Program.Config.pce };

            else if (default_content_options_list.SelectedIndex == 5)
                options = new Options_VC_NEO() { Binding = Program.Config.neo };

            else if (default_content_options_list.SelectedIndex == 6)
                options = new Options_Flash() { Binding = Program.Config.flash };

            if (options != null)
            {
                options.Text = string.Format("{0} - {1}", Program.Lang.String("default_content_options", Name), default_content_options_list.SelectedItem.ToString());
                options.ShowDialog(this);
            }
        }
    }
}
