using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Settings : Form
    {
        readonly Dictionary<string, string> langs = new Languages().Get();

        public Settings()
        {
            ComponentResourceManager mang = new ComponentResourceManager(typeof(Main));
            var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            foreach (Control child in Controls.OfType<Panel>())
                mang.ApplyResources(child, child.Name, new CultureInfo(lang));

            InitializeComponent();

            // -----------------------------
            foreach (var langName in langs.Keys)
                language.Items.Add(langName.ToString());
            foreach (KeyValuePair<string, string> listing in langs)
                if (listing.Value == Properties.Settings.Default.language) language.SelectedItem = listing.Key;
            // -----------------------------
        }

        private void OK_Click(object sender, EventArgs e)
        {
            bool showRestart = false;

            foreach (KeyValuePair<string, string> listing in langs)
                if (listing.Key == language.SelectedItem.ToString())
                {
                    showRestart = listing.Value != Properties.Settings.Default.language;
                    Properties.Settings.Default.language = listing.Value;
                    break;
                }

            if (showRestart) MessageBox.Show(Strings.restart, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }
    }
}
