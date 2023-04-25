using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Settings : Form
    {
        Localization lang = Program.lang;
        List<string> langs = new List<string>();

        public Settings()
        {
            InitializeComponent();
            lang.ChangeFormLanguage(this);

            // -----------------------------
            // Add system default
            langs.Add("sys");
            Language.Items.Add("[" + lang.Get("SystemDefault") + "]");

            // Add all languages
            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory + $"\\langs\\"))
                if (lang.IsConvertable(file))
                {
                    string name = new CultureInfo(Path.GetFileNameWithoutExtension(file)).NativeName;
                    Language.Items.Add(name[0].ToString().ToUpper() + name.Substring(1));
                    langs.Add(Path.GetFileNameWithoutExtension(file));
                }

            foreach (var name in langs)
                if (name == Default.Language) Language.SelectedIndex = langs.IndexOf(name);
            if (Default.Language == "sys") Language.SelectedIndex = 0;
            // -----------------------------

            Theme.SelectedIndex = Default.LightTheme ? 1 : 0;
        }
        private void Settings_Load(object sender, EventArgs e) => ChangeTheme();

        private void OK_Click(object sender, EventArgs e)
        {
            bool showRestart = false;

            for (int i = 0; i < langs.Count; i++)
                if (i == Language.SelectedIndex)
                {
                    showRestart = langs[i] != Default.Language;
                    Default.Language = langs[i];
                    break;
                }
            Default.LightTheme = Theme.SelectedIndex == 1;

            if (showRestart) MessageBox.Show(lang.Get("Message_Restart"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Default.Save();
            ChangeTheme();
            DialogResult = DialogResult.OK;
        }

        private void ChangeTheme()
        {
            BackColor = Owner.BackColor;
            ForeColor = Owner.ForeColor;
            panel.BackColor = ((Main)Owner).panel.BackColor;

            if (Default.LightTheme)
            {
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.FlatAppearance.BorderColor = Color.LightGray;
                        button.FlatAppearance.MouseDownBackColor = Color.Silver;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
            else
            {
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var c2 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c2.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
                        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50);
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
        }
    }
}
