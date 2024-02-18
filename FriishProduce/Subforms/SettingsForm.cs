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

            Language.AutoSetForm(this);
            Text = Language.Get("Settings");
            tabPage4.Text = string.Format(tabPage2.Text, Language.Get("PlatformGroup_1"));
            tabPage3.Text = string.Format(tabPage2.Text, Language.Get("Platform_N64"));
            tabPage2.Text = string.Format(tabPage2.Text, Language.Get("Platform_NES"));

            const string Name_N64 = "Options_VC_N64";
            n64000.Text = Language.Get(n64000, Name_N64);
            n64001.Text = Language.Get(n64001, Name_N64);
            n64002.Text = Language.Get(n64002, Name_N64);
            n64003.Text = Language.Get(n64003, Name_N64);
            n64004.Text = Language.Get(n64004, Name_N64);
            groupBox1.Text = Language.Get(groupBox1, Name_N64);
            ROMCType.Items.AddRange(new string[] { Language.Get($"{ROMCType.Name}.Items", Name_N64), Language.Get($"{ROMCType.Name}.Items1", Name_N64) });

            n64000.Checked = Default.Default_N64_FixBrightness;
            n64001.Checked = Default.Default_N64_FixCrashes;
            n64002.Checked = Default.Default_N64_ExtendedRAM;
            n64003.Checked = Default.Default_N64_AllocateROM;
            ROMCType.SelectedIndex = Default.Default_N64_ROMC0 ? 0 : 1;

            // -----------------------------

            // -------------------------------------------
            // Add all languages
            // -------------------------------------------
            LanguageList.Items[0] = "<" + LanguageList.Items[0].ToString() + ">";
            foreach (var item in Language.List)
                LanguageList.Items.Add(item.Value);
            var x = Default.UI_Language;

            if (Default.UI_Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = Language.List.Keys.ToList().IndexOf(Default.UI_Language) + 1;

            // -----------------------------

            DefaultImageInterpolation.Items[0] = Language.Get("ByDefault");
            DefaultImageInterpolation.Items.AddRange(Language.GetArray("List_ImageInterpolation"));
            DefaultImageInterpolation.SelectedIndex = Default.ImageInterpolation;
            AutoLibRetro.Checked = Default.AutoLibRetro;
            AutoOpenFolder.Checked = Default.AutoOpenFolder;
        }

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
            Default.AutoOpenFolder = AutoOpenFolder.Checked;

            Default.Default_N64_FixBrightness = n64000.Checked;
            Default.Default_N64_FixCrashes = n64001.Checked;
            Default.Default_N64_ExtendedRAM = n64002.Checked;
            Default.Default_N64_AllocateROM = n64003.Checked;
            Default.Default_N64_ROMC0 = ROMCType.SelectedIndex == 0;

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            if (isDirty)
            {
                if (MessageBox.Show(Language.Get("Message000"), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void Cancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
