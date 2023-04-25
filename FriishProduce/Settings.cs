using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<string> langFiles = new List<string>();
        // readonly Dictionary<string, string> langs = new Languages().Get();

        public Settings()
        {
            InitializeComponent();
            lang.ChangeFormLanguage(this);

            // -----------------------------
            /* foreach (var langName in langs.Keys)
                language.Items.Add(langName.ToString()); */

            // Add system default
            langFiles.Add("sys");
            language.Items.Add("[" + lang.Get("SystemDefault") + "]");

            // Add all languages
            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory + $"\\langs\\"))
                if (lang.IsConvertable(file))
                {
                    language.Items.Add(new CultureInfo(Path.GetFileNameWithoutExtension(file)).NativeName);
                    langFiles.Add(Path.GetFileNameWithoutExtension(file));
                }

            foreach (var name in langFiles)
                if (name == Default.language) language.SelectedIndex = langFiles.IndexOf(name);
            if (Default.language == "sys") language.SelectedIndex = 0;
            // -----------------------------
        }

        private void OK_Click(object sender, EventArgs e)
        {
            bool showRestart = false;

            for (int i = 0; i < langFiles.Count; i++)
            {
                if (i == language.SelectedIndex)
                {
                    showRestart = langFiles[i] != Default.language;
                    Default.language = langFiles[i];
                    break;
                }
            }

            if (showRestart) MessageBox.Show(lang.Get("Message_Restart"), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Default.Save();
            DialogResult = DialogResult.OK;
        }
    }
}
