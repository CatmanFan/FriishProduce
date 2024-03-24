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
        }

        public void RefreshForm()
        {
            TreeView.SelectedNode = !string.IsNullOrWhiteSpace(NodeName) ? TreeView.Nodes.Cast<TreeNode>().SingleOrDefault(n => n.Text == NodeName) : TreeView.Nodes[0];

            Language.Localize(this);
            Text = Language.Get("Settings");
            TreeView.Nodes[1].Nodes[0].Text = Language.Get(Console.NES.ToString());
            TreeView.Nodes[1].Nodes[1].Text = Language.Get(Console.N64.ToString(), "Platforms");
            TreeView.Nodes[1].Nodes[2].Text = Language.Get("Group1", "Platforms");
            TreeView.Nodes[1].Nodes[3].Text = Language.Get(Console.PCE.ToString(), "Platforms");
            TreeView.Nodes[1].Nodes[4].Text = Language.Get(Console.NEO.ToString(), "Platforms");
            TreeView.Nodes[1].Nodes[5].Text = Language.Get("Forwarders");

            // -----------------------------

            // -------------------------------------------
            // Add all languages
            // -------------------------------------------
            LanguageList.Items.Clear();
            LanguageList.Items.Add("<" + Language.Get("LanguageList.Items", this) + ">");
            foreach (var item in Language.List)
                LanguageList.Items.Add(item.Value);

            if (Default.UI_Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = Language.List.Keys.ToList().IndexOf(Default.UI_Language) + 1;

            DefaultImageInterpolation.Items.Clear();
            DefaultImageInterpolation.Items.Add(Language.Get("ByDefault"));
            DefaultImageInterpolation.Items.AddRange(Language.GetArray("List.ImageInterpolation"));
            DefaultImageInterpolation.SelectedIndex = Default.ImageInterpolation;

            AutoLibRetro.Checked = Default.AutoLibRetro;
            ResetAllDialogs.Checked = false;

            // -----------------------------

            groupBox8.Text = Language.Get("groupBox8", "InjectorForm", true);
            groupBox9.Text = Language.Get("groupBox9", "InjectorForm", true);

            // -----------------------------

            groupBox4.Text = Language.Get("groupBox1", typeof(Options_VC_NES).Name, true);
            PaletteBanner.Text = Language.Get("checkBox1", typeof(Options_VC_NES).Name, true);

            Language.GetComboBox(PaletteList, "PaletteList", typeof(Options_VC_NES).Name);

            // -----------------------------

            n64000.Text = Language.Get(n64000, typeof(Options_VC_N64).Name);
            n64001.Text = Language.Get(n64001, typeof(Options_VC_N64).Name);
            n64002.Text = Language.Get(n64002, typeof(Options_VC_N64).Name);
            n64003.Text = Language.Get(n64003, typeof(Options_VC_N64).Name);
            n64004.Text = Language.Get(n64004, typeof(Options_VC_N64).Name);
            groupBox3.Text = Language.Get("groupBox1", typeof(Options_VC_N64).Name, true);

            Language.GetComboBox(ROMCType, "ROMCType", typeof(Options_VC_N64).Name);

            // -----------------------------

            label2.Text = Language.Get("label2", typeof(Options_VC_SEGA).Name, true);
            SegaSRAM.Text = Language.Get("checkBox1", typeof(Options_VC_SEGA).Name, true);
            Sega6ButtonPad.Text = string.Format(Language.Get(Sega6ButtonPad, this), Language.Get(Console.SMD.ToString()));

            SegaRegion.Items.Clear();
            SegaRegion.Items.Add(Language.Get("Region.U"));
            SegaRegion.Items.Add(Language.Get("Region.E"));
            SegaRegion.Items.Add(Language.Get("Region.J"));

            // -----------------------------

            label3.Text = Language.Get("label1", typeof(Options_VC_PCE).Name, true);
            PCEHideOverscan.Text = Language.Get("checkBox1", typeof(Options_VC_PCE).Name, true);
            PCEBgRaster.Text = Language.Get("checkBox2", typeof(Options_VC_PCE).Name, true);
            PCESpriteLimit.Text = Language.Get("checkBox3", typeof(Options_VC_PCE).Name, true);
            PCEUseSRAM.Text = Language.Get("checkBox4", typeof(Options_VC_PCE).Name, true);
            toggleSwitchL2.Text = Language.GetToggleSwitch(toggleSwitch2, "toggleSwitch1", typeof(Options_VC_PCE).Name);
            toggleSwitchL3.Text = Language.GetToggleSwitch(toggleSwitch3, "toggleSwitch2", typeof(Options_VC_PCE).Name);
            toggleSwitchL4.Text = Language.GetToggleSwitch(toggleSwitch4, "toggleSwitch3", typeof(Options_VC_PCE).Name);

            // -----------------------------

            Language.GetComboBox(NGBios, "comboBox1", typeof(Options_VC_NEO).Name);
            NGBios.Items.RemoveAt(0);

            // -----------------------------

            FStorage_SD.Checked = FORWARDER.Default.root_storage_device.ToLower() == "sd";
            toggleSwitch1.Checked = FORWARDER.Default.nand_loader.ToLower() == "vwii";
            FStorage_USB.Checked = !FStorage_SD.Checked;

            PaletteList.SelectedIndex = int.Parse(VC_NES.Default.palette);
            PaletteBanner.Checked = bool.Parse(VC_NES.Default.palette_use_on_banner);

            n64000.Checked = bool.Parse(VC_N64.Default.patch_fixbrightness);
            n64001.Checked = bool.Parse(VC_N64.Default.patch_fixcrashes);
            n64002.Checked = bool.Parse(VC_N64.Default.patch_expandedram);
            n64003.Checked = bool.Parse(VC_N64.Default.patch_autosizerom);
            ROMCType.SelectedIndex = bool.Parse(VC_N64.Default.romc_type0) ? 0 : 1;

            label1.Text = VC_SEGA.Default.console_brightness;
            SegaBrightnessValue.Value = int.Parse(label1.Text);
            SegaSRAM.Checked = VC_SEGA.Default.save_sram == "1";
            Sega6ButtonPad.Checked = VC_SEGA.Default.dev_mdpad_enable_6b == "1";
            SegaRegion.SelectedIndex = VC_SEGA.Default.country == "jp" ? 0 : VC_SEGA.Default.country == "eu" ? 2 : 1;

            PCEUseSRAM.Checked = VC_PCE.Default.BACKUPRAM == "1";
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

            // -----------------------------
        }

        private void Loading(object sender, EventArgs e) { TreeView.Select(); RefreshForm(); }

        private void OK_Click(object sender, EventArgs e)
        {
            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            if (LanguageList.SelectedIndex == 0)
                Default.UI_Language = "sys";
            else
                foreach (var item in Language.List)
                    if (item.Value == LanguageList.SelectedItem.ToString())
                        Default.UI_Language = item.Key;

            Language.Current = LanguageList.SelectedIndex == 0 ? Language.GetSystemLanguage() : new System.Globalization.CultureInfo(Default.UI_Language);

            // -------------------------------------------
            // Other settings
            // -------------------------------------------
            Default.ImageInterpolation = DefaultImageInterpolation.SelectedIndex;
            Default.AutoLibRetro = AutoLibRetro.Checked;

            FORWARDER.Default.root_storage_device = FStorage_SD.Checked ? "SD" : "USB";
            FORWARDER.Default.nand_loader = toggleSwitch1.Checked ? "vWii" : "Wii";

            VC_NES.Default.palette = PaletteList.SelectedIndex.ToString();
            VC_NES.Default.palette_use_on_banner = PaletteBanner.Checked.ToString();

            VC_N64.Default.patch_fixbrightness = n64000.Checked.ToString();
            VC_N64.Default.patch_fixcrashes = n64001.Checked.ToString();
            VC_N64.Default.patch_expandedram = n64002.Checked.ToString();
            VC_N64.Default.patch_autosizerom = n64003.Checked.ToString();
            VC_N64.Default.romc_type0 = (ROMCType.SelectedIndex == 0).ToString();

            VC_SEGA.Default.console_brightness = label1.Text;
            VC_SEGA.Default.save_sram = SegaSRAM.Checked ? "1" : "0";
            VC_SEGA.Default.dev_mdpad_enable_6b = Sega6ButtonPad.Checked ? "1" : "0";
            VC_SEGA.Default.country = SegaRegion.SelectedIndex == 0 ? "jp" : SegaRegion.SelectedIndex == 2 ? "eu" : "us";

            VC_PCE.Default.BACKUPRAM = PCEUseSRAM.Checked ? "1" : "0";
            VC_PCE.Default.EUROPE = toggleSwitch2.Checked ? "1" : "0";
            VC_PCE.Default.SGENABLE = toggleSwitch3.Checked ? "1" : "0";
            VC_PCE.Default.PADBUTTON = toggleSwitch4.Checked ? "6" : "2";
            VC_PCE.Default.YOFFSET = PCEYOffset.Value.ToString();
            VC_PCE.Default.HIDEOVERSCAN = PCEHideOverscan.Checked ? "1" : "0";
            VC_PCE.Default.RASTER = PCEBgRaster.Checked ? "1" : "0";
            VC_PCE.Default.SPRLINE = PCESpriteLimit.Checked ? "1" : "0";

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
                if (MessageBox.Show(Language.Get("Message.000"), ProductName, MessageBoxButtons.YesNo) == MessageBox.Result.Yes)
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
            Language.Current = Default.UI_Language.ToLower() == "sys" ? Language.GetSystemLanguage() : new System.Globalization.CultureInfo(Default.UI_Language);
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
            // panel9.Hide();

            NodeName = e.Node.Name;

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
                    // panel9.Show();
                    break;

                case "Forwarders":
                    panel3.Show();
                    break;
            };
        }

        private void LanguageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool refresh = false;

            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            if (LanguageList.SelectedIndex == 0)
            {
                refresh = Default.UI_Language != "sys";
                Default.UI_Language = "sys";
            }

            else
                foreach (var item in Language.List)
                    if (item.Value == LanguageList.SelectedItem.ToString())
                    {
                        refresh = Default.UI_Language != item.Key;
                        Default.UI_Language = item.Key;
                    }


            Language.Current = LanguageList.SelectedIndex == 0 ? Language.GetSystemLanguage() : new System.Globalization.CultureInfo(Default.UI_Language);

            // if (refresh) RefreshForm();
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            toggleSwitchL1.Text = toggleSwitch1.Checked ? "vWii (Wii U)" : "Wii";
            toggleSwitchL2.Text = Language.GetToggleSwitch(toggleSwitch2, "toggleSwitch1", typeof(Options_VC_PCE).Name);
            toggleSwitchL3.Text = Language.GetToggleSwitch(toggleSwitch3, "toggleSwitch2", typeof(Options_VC_PCE).Name);
            toggleSwitchL4.Text = Language.GetToggleSwitch(toggleSwitch4, "toggleSwitch3", typeof(Options_VC_PCE).Name);
        }

        private void BrightnessValue_Scroll(object sender, EventArgs e) => label1.Text = SegaBrightnessValue.Value.ToString();
    }
}
