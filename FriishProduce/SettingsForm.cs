using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FriishProduce.Options;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class SettingsForm : Form
    {
        private bool isDirty { get; set; }
        private string NodeName { get; set; }
        private bool NodeLocked { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
            isDirty = false;
        }

        private void SaveAll()
        {
            Default.Save();
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
            NodeLocked = true;
            try
            {
                foreach (TreeNode node in TreeView.Nodes)
                {
                    TreeView.SelectedNode = node;
                    if (node.Name == NodeName) break;
                    if (TreeView.SelectedNode.Nodes.Count > 1) TreeView.SelectedNode = node.Nodes.Cast<TreeNode>().First(n => n.Name == NodeName);
                }
            }
            catch { TreeView.SelectedNode = TreeView.Nodes[0]; }
            NodeLocked = false;

            Text = Program.Lang.String("settings");
            TreeView.Nodes[1].Nodes[0].Text = Program.Lang.Console(Console.NES);
            TreeView.Nodes[1].Nodes[1].Text = Program.Lang.Console(Console.N64);
            TreeView.Nodes[1].Nodes[2].Text = Program.Lang.String("group1", "platforms");
            TreeView.Nodes[1].Nodes[3].Text = Program.Lang.Console(Console.PCE);
            TreeView.Nodes[1].Nodes[4].Text = Program.Lang.Console(Console.NEO);
            TreeView.Nodes[1].Nodes[5].Text = Program.Lang.Console(Console.Flash);
            TreeView.Nodes[1].Nodes[6].Text = Program.Lang.String("forwarders", "platforms");

            // -----------------------------

            // -------------------------------------------
            // Add all languages
            // -------------------------------------------
            LanguageList.Items.Clear();
            LanguageList.Items.Add("<" + Program.Lang.String("system_default", Name) + ">");
            foreach (var item in Program.Lang.List)
                LanguageList.Items.Add(item.Value);

            if (Default.Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = Program.Lang.List.Keys.ToList().IndexOf(Default.Language) + 1;

            DefaultImageInterpolation.Items.Clear();
            DefaultImageInterpolation.Items.AddRange(Program.Lang.StringArray("image_interpolation_mode", "projectform"));
            DefaultImageInterpolation.SelectedIndex = Default.ImageInterpolation;

            AutoLibRetro.Checked = Default.AutoLibRetro;
            ResetAllDialogs.Checked = false;

            // -----------------------------

            forwarder_root_device.Text = Program.Lang.String(forwarder_root_device.Name, "projectform");
            forwarder_console.Text = Program.Lang.String(forwarder_console.Name, "projectform");

            // -----------------------------

            palette.Text = Program.Lang.String("palette", "vc_nes");
            palette_use_on_banner.Text = Program.Lang.String("palette_use_on_banner", "vc_nes");

            PaletteList.Items.Clear();
            PaletteList.Items.AddRange(Program.Lang.StringArray("palette", "vc_nes"));

            // -----------------------------

            patch_fixbrightness.Text = Program.Lang.String("patch_fixbrightness", "vc_n64");
            patch_fixcrashes.Text = Program.Lang.String("patch_fixcrashes", "vc_n64");
            patch_expandedram.Text = Program.Lang.String("patch_expandedram", "vc_n64");
            patch_autosizerom.Text = Program.Lang.String("patch_autosizerom", "vc_n64");
            romc_type.Text = Program.Lang.String("romc_type", "vc_n64");
            groupBox3.Text = Program.Lang.String("patches", "vc_n64");

            ROMCType.Items.Clear();
            ROMCType.Items.AddRange(Program.Lang.StringArray("romc_type", "vc_n64"));

            // -----------------------------

            label2.Text = Program.Lang.String("country", "vc_sega");
            SEGA_save_sram.Text = Program.Lang.String("save_data_enable", "projectform");
            SEGA_dev_mdpad_enable_6b.Text = string.Format(Program.Lang.String("dev_mdpad_enable_6b", "vc_sega"), Program.Lang.Console(Console.SMD));
            SEGA_console_disableresetbutton.Text = Program.Lang.String("console_disableresetbutton", "vc_sega");

            SEGA_country.Items.Clear();
            SEGA_country.Items.Add(Program.Lang.String("region_u"));
            SEGA_country.Items.Add(Program.Lang.String("region_e"));
            SEGA_country.Items.Add(Program.Lang.String("region_j"));

            // -----------------------------

            label3.Text = Program.Lang.String("y_offset", "vc_pce");
            PCEHideOverscan.Text = Program.Lang.String("hide_overscan", "vc_pce");
            PCEBgRaster.Text = Program.Lang.String("raster", "vc_pce");
            PCESpriteLimit.Text = Program.Lang.String("sprline", "vc_pce");
            PCESavedata.Text = Program.Lang.String("save_data_enable", "projectform");

            // -----------------------------

            NGBios.Items.Clear();
            NGBios.Items.AddRange(Program.Lang.StringArray("bios", "vc_neo"));
            NGBios.Items.RemoveAt(0);

            // -----------------------------

            groupBox14.Text = Program.Lang.String("save_data", "projectform");

            // -----------------------------

            FStorage_SD.Checked = FORWARDER.Default.root_storage_device.ToLower() == "sd";
            toggleSwitch1.Checked = FORWARDER.Default.nand_loader.ToLower() == "vwii";
            FStorage_USB.Checked = !FStorage_SD.Checked;

            PaletteList.SelectedIndex = int.Parse(VC_NES.Default.palette);
            palette_use_on_banner.Checked = bool.Parse(VC_NES.Default.palette_use_on_banner);

            patch_fixbrightness.Checked = bool.Parse(VC_N64.Default.patch_fixbrightness);
            patch_fixcrashes.Checked = bool.Parse(VC_N64.Default.patch_fixcrashes);
            patch_expandedram.Checked = bool.Parse(VC_N64.Default.patch_expandedram);
            patch_autosizerom.Checked = bool.Parse(VC_N64.Default.patch_autosizerom);
            ROMCType.SelectedIndex = int.Parse(VC_N64.Default.romc_type);

            label1.Text = VC_SEGA.Default.console_brightness;
            SEGA_console_brightness.Value = int.Parse(label1.Text);
            SEGA_save_sram.Checked = VC_SEGA.Default.save_sram == "1";
            SEGA_dev_mdpad_enable_6b.Checked = VC_SEGA.Default.dev_mdpad_enable_6b == "1";
            SEGA_country.SelectedIndex = VC_SEGA.Default.country == "jp" ? 0 : VC_SEGA.Default.country == "eu" ? 2 : 1;
            SEGA_console_disableresetbutton.Checked = VC_SEGA.Default.console_disableresetbutton == "1";

            PCESavedata.Checked = VC_PCE.Default.BACKUPRAM == "1";
            toggleSwitch2.Checked = VC_PCE.Default.EUROPE == "1";
            toggleSwitch3.Checked = VC_PCE.Default.SGENABLE == "1";
            toggleSwitch4.Checked = VC_PCE.Default.PADBUTTON == "6";
            PCEYOffset.Value = int.Parse(VC_PCE.Default.YOFFSET);
            PCEHideOverscan.Checked = VC_PCE.Default.HIDEOVERSCAN == "1";
            PCEBgRaster.Checked = VC_PCE.Default.RASTER == "1";
            PCESpriteLimit.Checked = VC_PCE.Default.SPRLINE == "1";

            switch (VC_NEO.Default.bios.ToLower())
            {
                case "vc1":
                    NGBios.SelectedIndex = 0;
                    break;

                default:
                case "vc2":
                    NGBios.SelectedIndex = 1;
                    break;

                case "vc3":
                    NGBios.SelectedIndex = 2;
                    break;
            }

            FLASH_savedata.Checked = ADOBEFLASH.Default.shared_object_capability == "on";
            FLASH_vff_sync_on_write.Checked = ADOBEFLASH.Default.vff_sync_on_write == "on";
            FLASH_vff_cache_size.SelectedItem = FLASH_vff_cache_size.Items.Cast<string>().FirstOrDefault(n => n.ToString() == ADOBEFLASH.Default.vff_cache_size);
            FLASH_quality.SelectedIndex = ADOBEFLASH.Default.quality == "high" ? 0 : ADOBEFLASH.Default.quality == "medium" ? 1 : 2;
            FLASH_mouse.Checked = ADOBEFLASH.Default.mouse == "on";
            FLASH_qwerty_keyboard.Checked = ADOBEFLASH.Default.qwerty_keyboard == "on";
            label4.Enabled = FLASH_vff_sync_on_write.Enabled = FLASH_vff_cache_size.Enabled = FLASH_savedata.Checked;

            // -----------------------------
        }

        private void Loading(object sender, EventArgs e) { TreeView.Select(); RefreshForm(); }

        private void OK_Click(object sender, EventArgs e)
        {
            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            if (LanguageList.SelectedIndex == 0)
                Default.Language = "sys";
            else
                foreach (var item in Program.Lang.List)
                    if (item.Value == LanguageList.SelectedItem.ToString())
                        Default.Language = item.Key;

            // -------------------------------------------
            // Other settings
            // -------------------------------------------
            Default.ImageInterpolation = DefaultImageInterpolation.SelectedIndex;
            Default.AutoLibRetro = AutoLibRetro.Checked;

            FORWARDER.Default.root_storage_device = FStorage_SD.Checked ? "SD" : "USB";
            FORWARDER.Default.nand_loader = toggleSwitch1.Checked ? "vWii" : "Wii";

            VC_NES.Default.palette = PaletteList.SelectedIndex.ToString();
            VC_NES.Default.palette_use_on_banner = palette_use_on_banner.Checked.ToString();

            VC_N64.Default.patch_fixbrightness = patch_fixbrightness.Checked.ToString();
            VC_N64.Default.patch_fixcrashes = patch_fixcrashes.Checked.ToString();
            VC_N64.Default.patch_expandedram = patch_expandedram.Checked.ToString();
            VC_N64.Default.patch_autosizerom = patch_autosizerom.Checked.ToString();
            VC_N64.Default.romc_type = ROMCType.SelectedIndex.ToString();

            VC_SEGA.Default.console_brightness = label1.Text;
            VC_SEGA.Default.save_sram = SEGA_save_sram.Checked ? "1" : "0";
            VC_SEGA.Default.dev_mdpad_enable_6b = SEGA_dev_mdpad_enable_6b.Checked ? "1" : "0";
            VC_SEGA.Default.country = SEGA_country.SelectedIndex == 0 ? "jp" : SEGA_country.SelectedIndex == 2 ? "eu" : "us";
            VC_SEGA.Default.console_disableresetbutton = SEGA_console_disableresetbutton.Checked ? "1" : null;

            VC_PCE.Default.BACKUPRAM = PCESavedata.Checked ? "1" : "0";
            VC_PCE.Default.EUROPE = toggleSwitch2.Checked ? "1" : "0";
            VC_PCE.Default.SGENABLE = toggleSwitch3.Checked ? "1" : "0";
            VC_PCE.Default.PADBUTTON = toggleSwitch4.Checked ? "6" : "2";
            VC_PCE.Default.YOFFSET = PCEYOffset.Value.ToString();
            VC_PCE.Default.HIDEOVERSCAN = PCEHideOverscan.Checked ? "1" : "0";
            VC_PCE.Default.RASTER = PCEBgRaster.Checked ? "1" : "0";
            VC_PCE.Default.SPRLINE = PCESpriteLimit.Checked ? "1" : "0";

            ADOBEFLASH.Default.shared_object_capability = FLASH_savedata.Checked ? "on" : "off";
            ADOBEFLASH.Default.vff_sync_on_write = FLASH_vff_sync_on_write.Checked ? "on" : "off";
            ADOBEFLASH.Default.vff_cache_size = FLASH_vff_cache_size.SelectedItem.ToString();
            ADOBEFLASH.Default.quality = FLASH_quality.SelectedIndex == 0 ? "high" : FLASH_quality.SelectedIndex == 1 ? "medium" : "low";
            ADOBEFLASH.Default.mouse = FLASH_mouse.Checked ? "on" : "off";
            ADOBEFLASH.Default.qwerty_keyboard = FLASH_qwerty_keyboard.Checked ? "on" : "off";
            ADOBEFLASH.Default.hbm_no_save = ADOBEFLASH.Default.shared_object_capability == "on" ? "no" : "yes";

            switch (NGBios.SelectedIndex)
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

            if (ResetAllDialogs.Checked)
            {
                Default.DoNotShow_Welcome = false;
                Default.DoNotShow_000 = false;
            }

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            if (isDirty)
            {
                if (MessageBox.Show(Program.Lang.Msg(0), ProductName, MessageBox.Buttons.YesNo) == MessageBox.Result.Yes)
                {
                    SaveAll();
                    Application.Restart();
                }
            }
            else
            {
                SaveAll();
                DialogResult = DialogResult.OK;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Default.Reload();
            DialogResult = DialogResult.Cancel;
        }

        private void DownloadBanners_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();

            var WADs = new Dictionary<string, Console>()
            {
                /* { "FCWP", Console.NES }, // SMB3
                { "FCWJ", Console.NES },
                { "FCWQ", Console.NES },
                { "JBDP", Console.SNES }, // DKC2
                { "JBDJ", Console.SNES },
                { "JBDT", Console.SNES },
                { "NAAP", Console.N64 }, // SM64
                { "NAAJ", Console.N64 },
                { "NABT", Console.N64 }, // MK64 */
                { "PAAP", Console.PCE },
                { "PAGJ", Console.PCE },
             /* { "EAJP", Console.NEO },
                { "EAJJ", Console.NEO },
                { "C9YE", Console.C64 },
                { "C9YP", Console.C64 },
                { "XAGJ", Console.MSX },
                { "XAPJ", Console.MSX },
                { "WNAP", Console.Flash } */
            };

            foreach (var item in WADs)
                BannerHelper.ExportBanner(item.Key, item.Value);

            System.Media.SystemSounds.Beep.Play();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();

            if (!NodeLocked) NodeName = e.Node.Name;

            switch (e.Node.Name.Substring(4))
            {
                default:
                    break;

                case "0":
                    panel1.Show();
                    break;

                case "1":
                    panel2.Show();
                    break;

                case "NES":
                    panel4.Show();
                    break;

                case "N64":
                    panel5.Show();
                    break;

                case "SEGA":
                    panel6.Show();
                    break;

                case "PCE":
                    panel7.Show();
                    break;

                case "NEO":
                    panel8.Show();
                    break;

                case "Flash":
                    panel9.Show();
                    break;

                case "Forwarders":
                    panel3.Show();
                    break;
            };
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            ToggleSwitchText();
            label4.Enabled = FLASH_vff_sync_on_write.Enabled = FLASH_vff_cache_size.Enabled = FLASH_savedata.Checked;
        }

        private void ToggleSwitchText()
        {
            toggleSwitchL1.Text = toggleSwitch1.Checked ? "vWii (Wii U)" : "Wii";
            toggleSwitchL2.Text = Program.Lang.Toggle(toggleSwitch2.Checked, "europe", "vc_pce");
            toggleSwitchL3.Text = Program.Lang.Toggle(toggleSwitch3.Checked, "sgenable", "vc_pce");
            toggleSwitchL4.Text = Program.Lang.Toggle(toggleSwitch4.Checked, "padbutton", "vc_pce");
        }

        private void BrightnessValue_Scroll(object sender, EventArgs e) => label1.Text = SEGA_console_brightness.Value.ToString();
    }
}
