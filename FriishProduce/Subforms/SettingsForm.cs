using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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

        public void RefreshForm()
        {
            TreeView.SelectedNode = !string.IsNullOrWhiteSpace(NodeName) ? TreeView.Nodes.Cast<TreeNode>().SingleOrDefault(n => n.Text == NodeName) : TreeView.Nodes[0];

            Language.Localize(this);
            Text = Language.Get("Settings");
            TreeView.Nodes[1].Nodes[0].Text = Language.Get(Console.NES.ToString());
            TreeView.Nodes[1].Nodes[1].Text = Language.Get(Console.N64.ToString(), "Platforms");
            TreeView.Nodes[1].Nodes[2].Text = Language.Get("Group1", "Platforms");
            TreeView.Nodes[1].Nodes[3].Text = Language.Get(Console.PCE.ToString(), "Platforms");
            TreeView.Nodes[1].Nodes[4].Text = Language.Get(Console.NeoGeo.ToString(), "Platforms");
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
            Sega6ButtonPad.Text = string.Format(Language.Get(Sega6ButtonPad, this), Language.Get(Console.SMDGEN.ToString()));

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

            Language.GetComboBox(NGBios, "comboBox1", typeof(Options_VC_NeoGeo).Name);
            NGBios.Items.RemoveAt(0);

            // -----------------------------

            FStorage_SD.Checked = Default.Default_Forwarders_FilesStorage.ToLower() == "sd";
            toggleSwitch1.Checked = Default.Default_Forwarders_Mode.ToLower() == "vwii";
            FStorage_USB.Checked = !FStorage_SD.Checked;

            PaletteList.SelectedIndex = Default.Default_NES_Palette;
            PaletteBanner.Checked = Default.Default_NES_UsePaletteForBanner;

            n64000.Checked = Default.Default_N64_FixBrightness;
            n64001.Checked = Default.Default_N64_FixCrashes;
            n64002.Checked = Default.Default_N64_ExtendedRAM;
            n64003.Checked = Default.Default_N64_AllocateROM;
            ROMCType.SelectedIndex = Default.Default_N64_ROMC0 ? 0 : 1;

            label1.Text = Default.Default_SEGA_Brightness;
            SegaBrightnessValue.Value = int.Parse(Default.Default_SEGA_Brightness);
            SegaSRAM.Checked = Default.Default_SEGA_SRAM == "1";
            Sega6ButtonPad.Checked = Default.Default_SEGA_6B == "1";
            SegaRegion.SelectedIndex = Default.Default_SEGA_Region.ToLower() == "jp" ? 0 : Default.Default_SEGA_Region.ToLower() == "eu" ? 2 : 1;

            PCEUseSRAM.Checked = Default.Default_PCE_BackupRAM == "1";
            toggleSwitch2.Checked = Default.Default_PCE_Europe == "1";
            toggleSwitch3.Checked = Default.Default_PCE_SuperGrafx == "1";
            toggleSwitch4.Checked = Default.Default_PCE_Pad == "6";
            PCEYOffset.Value = int.Parse(Default.Default_PCE_YOffset);
            PCEHideOverscan.Checked = Default.Default_PCE_HideOverscan == "1";
            PCEBgRaster.Checked = Default.Default_PCE_BGRaster == "1";
            PCESpriteLimit.Checked = Default.Default_PCE_SpriteLimit == "1";

            switch (Default.Default_NeoGeo_BIOS.ToLower())
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

            Default.Default_Forwarders_FilesStorage = FStorage_SD.Checked ? "SD" : "USB";
            Default.Default_Forwarders_Mode = toggleSwitch1.Checked ? "vWii" : "Wii";

            Default.Default_NES_Palette = PaletteList.SelectedIndex;
            Default.Default_NES_UsePaletteForBanner = PaletteBanner.Checked;

            Default.Default_N64_FixBrightness = n64000.Checked;
            Default.Default_N64_FixCrashes = n64001.Checked;
            Default.Default_N64_ExtendedRAM = n64002.Checked;
            Default.Default_N64_AllocateROM = n64003.Checked;
            Default.Default_N64_ROMC0 = ROMCType.SelectedIndex == 0;

            Default.Default_SEGA_Brightness = label1.Text;
            Default.Default_SEGA_SRAM = SegaSRAM.Checked ? "1" : null;
            Default.Default_SEGA_6B = Sega6ButtonPad.Checked ? "1" : null;
            Default.Default_SEGA_Region = SegaRegion.SelectedIndex == 0 ? "jp" : SegaRegion.SelectedIndex == 2 ? "eu" : "us";

            Default.Default_PCE_BackupRAM = PCEUseSRAM.Checked ? "1" : "0";
            Default.Default_PCE_Europe = toggleSwitch2.Checked ? "1" : "0";
            Default.Default_PCE_SuperGrafx = toggleSwitch3.Checked ? "1" : "0";
            Default.Default_PCE_Pad = toggleSwitch4.Checked ? "6" : "2";
            Default.Default_PCE_YOffset = PCEYOffset.Value.ToString();
            Default.Default_PCE_HideOverscan = PCEHideOverscan.Checked ? "1" : "0";
            Default.Default_PCE_BGRaster = PCEBgRaster.Checked ? "1" : "0";
            Default.Default_PCE_SpriteLimit = PCESpriteLimit.Checked ? "1" : "0";

            switch (NGBios.SelectedIndex)
            {
                case 0:
                    Default.Default_NeoGeo_BIOS = "VC1";
                    break;

                case 1:
                    Default.Default_NeoGeo_BIOS = "VC2";
                    break;

                case 2:
                    Default.Default_NeoGeo_BIOS = "VC3";
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
                    Default.Save();
                    Application.Restart();
                }
            }
            else
            {
                Default.Save();
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
             /* { "EAJP", Console.NeoGeo },
                { "EAJJ", Console.NeoGeo },
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

                case "NeoGeo":
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
