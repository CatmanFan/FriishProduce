using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class SettingsForm : Form
    {
        private bool isDirty { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
            isDirty = false;
        }

        public void RefreshForm()
        {
            Language.Localize(this);
            Text = Language.Get("Settings");
            TreeView.Nodes[1].Nodes[0].Text = Language.Get(Console.NES.ToString());
            TreeView.Nodes[1].Nodes[1].Text = Language.Get(Console.N64.ToString(), "Platforms");
            TreeView.Nodes[1].Nodes[2].Text = Language.Get("Group1", "Platforms");
            TreeView.SelectedNode = TreeView.Nodes[0];

            // -----------------------------

            // -------------------------------------------
            // Add all languages
            // -------------------------------------------
            if (LanguageList.Items.Count != Language.List.Count + 1)
            {
                LanguageList.Items.Clear();
                LanguageList.Items.Add("<" + Language.Get("LanguageList.Items", this) + ">");
                foreach (var item in Language.List)
                    LanguageList.Items.Add(item.Value);
            }

            if (Default.UI_Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = Language.List.Keys.ToList().IndexOf(Default.UI_Language) + 1;

            DefaultImageInterpolation.Items.Clear();
            DefaultImageInterpolation.Items.Add(Language.Get("ByDefault"));
            DefaultImageInterpolation.Items.AddRange(Language.GetArray("List.ImageInterpolation"));
            DefaultImageInterpolation.SelectedIndex = Default.ImageInterpolation;

            AutoLibRetro.Checked = Default.AutoLibRetro;
            AutoOpenFolder.Checked = Default.AutoOpenFolder;
            checkBox1.Checked = false;

            // -----------------------------

            const string Name_N64 = "Options_VC_N64";
            n64000.Text = Language.Get(n64000, Name_N64);
            n64001.Text = Language.Get(n64001, Name_N64);
            n64002.Text = Language.Get(n64002, Name_N64);
            n64003.Text = Language.Get(n64003, Name_N64);
            n64004.Text = Language.Get(n64004, Name_N64);
            groupBox1.Text = Language.Get(groupBox1, Name_N64);
            ROMCType.Items.Clear();
            ROMCType.Items.AddRange(new string[] { Language.Get($"{ROMCType.Name}.Items", Name_N64), Language.Get($"{ROMCType.Name}.Items1", Name_N64) });

            // -----------------------------

            n64000.Checked = Default.Default_N64_FixBrightness;
            n64001.Checked = Default.Default_N64_FixCrashes;
            n64002.Checked = Default.Default_N64_ExtendedRAM;
            n64003.Checked = Default.Default_N64_AllocateROM;
            ROMCType.SelectedIndex = Default.Default_N64_ROMC0 ? 0 : 1;
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
            Language.Current = LanguageList.SelectedIndex == 0 ? Language.GetSystemLanguage() : new System.Globalization.CultureInfo(Default.UI_Language);

            // -------------------------------------------
            // Other settings
            // -------------------------------------------
            Default.ImageInterpolation = DefaultImageInterpolation.SelectedIndex;
            Default.AutoLibRetro = AutoLibRetro.Checked;
            Default.AutoOpenFolder = AutoOpenFolder.Checked;

            Default.Default_N64_FixBrightness = n64000.Checked;
            Default.Default_N64_FixCrashes = n64001.Checked;
            Default.Default_N64_ExtendedRAM = n64002.Checked;
            Default.Default_N64_AllocateROM = n64003.Checked;
            Default.Default_N64_ROMC0 = ROMCType.SelectedIndex == 0;

            // -------------------------------------------

            if (checkBox1.Checked)
            {
                Default.DoNotShow_Welcome = false;
                Default.DoNotShow_000 = false;
            }

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            if (isDirty)
            {
                if (MessageBox.Show(Language.Get("Message.000"), ProductName, MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                { "NABT", Console.N64 }, // MK64
                { "EAJP", Console.NeoGeo },
                { "EAJJ", Console.NeoGeo },
                { "C9YE", Console.C64 },
                { "C9YP", Console.C64 },
                { "WNAP", Console.Flash } */
            };

            foreach (var item in WADs)
                Banner.ExportBanner(item.Key, item.Value);

            System.Media.SystemSounds.Beep.Play();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel4.Hide();

            switch (e.Node.Name.Substring(4))
            {
                default:
                case "0":
                    panel1.Show();
                    break;

                case "1":
                    panel2.Show();
                    break;

                case "NES":
                    break;

                case "N64":
                    panel4.Show();
                    break;

                case "SEGA":
                    break;
            };
        }

        private void LanguageList_SelectedIndexChanged(object sender, EventArgs e)
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

            RefreshForm();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(Language.Get("N64", "Platforms"));
            MessageBox.Show(Language.Get("AppTitle"));
            MessageBox.Show(Language.Get("Error.004", "Strings"));
            MessageBox.Show(Language.Get("FuckingShit", "Strings"));
        }
    }
}
