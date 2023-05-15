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
        readonly Lang x = Program.Language;
        readonly List<string> langs = new List<string>();

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

            if (Default.Language == "sys") Language.SelectedIndex = 0;
            else Language.SelectedIndex = Language.Items.IndexOf(x.LangInfo(Default.Language)[0]);
            // -----------------------------

            OpenWhenDone.Checked = Default.OpenWhenDone;
            Theme.SelectedIndex = Default.LightTheme ? 1 : 0;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            bool showRestart = false;

            if (Language.SelectedIndex == 0)
            {
                showRestart = Default.Language != "sys";
                Default.Language = "sys";
            }
            else
            {
                foreach (string file in Directory.GetFiles(Paths.Languages))
                    if (Lang.Read(file) != null)
                    {
                        if (x.LangInfo(Path.GetFileNameWithoutExtension(file))[0] == Language.SelectedItem.ToString())
                        {
                            showRestart = Path.GetFileNameWithoutExtension(file) != Default.Language;
                            Default.Language = Path.GetFileNameWithoutExtension(file);
                            break;
                        }
                    }
            }

            if (showRestart) MessageBox.Show(x.Get("m001"), Text);

            Default.OpenWhenDone = OpenWhenDone.Checked;
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
