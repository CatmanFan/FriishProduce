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
        Lang x = Program.Language;
        List<string> langs = new List<string>();

        public Settings()
        {
            InitializeComponent();
            x.Localize(this);

            // -----------------------------
            // Add system default
            langs.Add("sys");
            Language.Items.Add($"<{x.Get("s002")}>");

            // Add all languages
            foreach (string file in Directory.GetFiles(Paths.Languages))
                if (Lang.Read(file) != null)
                {
                    Language.Items.Add(x.LangInfo(Path.GetFileNameWithoutExtension(file))[0]);
                    langs.Add(Path.GetFileNameWithoutExtension(file));
                }

            foreach (var name in langs)
                if (name == Default.Language) Language.SelectedIndex = langs.IndexOf(name);
            if (Default.Language == "sys") Language.SelectedIndex = 0;
            // -----------------------------

            Theme.SelectedIndex = Default.LightTheme ? 1 : 0;
        }

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

            if (showRestart) MessageBox.Show(x.Get("m001"), Text);

            Default.LightTheme = Theme.SelectedIndex == 1;
            Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Language.SelectedIndex > 0)
                ToolTip.SetToolTip(Language, string.Format(x.Get("g005"), x.LangInfo(langs[Language.SelectedIndex])[1]));
            else
                ToolTip.SetToolTip(Language, null);
        }
    }
}
