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
        Dictionary<string, string> langs = new Dictionary<string, string>();

        public Settings()
        {
            ComponentResourceManager mang = new ComponentResourceManager(typeof(Main));
            var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            foreach (Control child in Controls.OfType<Panel>())
                mang.ApplyResources(child, child.Name, new CultureInfo(lang));

            InitializeComponent();

            // Currently supported languages
            langs.Add("English",  "en");
            langs.Add("français", "fr");
            // -----------------------------
            foreach (var langName in langs.Keys)
                language.Items.Add(langName.ToString());

            if (lang == "en") language.SelectedIndex = 0;
            else foreach (KeyValuePair<string, string> listing in langs)
                    if (listing.Value == Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName) language.SelectedItem = listing.Key;

            enableWADtoHBC.Checked = Properties.Settings.Default.enable_wad_to_hbc;
            IP.Enabled = Properties.Settings.Default.enable_wad_to_hbc;
            IP.Text = Properties.Settings.Default.wii_ip_address;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            bool showRestart = false;

            foreach (KeyValuePair<string, string> listing in langs)
            { 
                if (listing.Key == language.SelectedItem.ToString())
                {
                    showRestart = listing.Value != Properties.Settings.Default.language;
                    Properties.Settings.Default.language = listing.Value;
                    break;
                }
            }
            Properties.Settings.Default.enable_wad_to_hbc = enableWADtoHBC.Checked;
            Properties.Settings.Default.wii_ip_address = IP.Text;

            if (showRestart) MessageBox.Show(strings.settings_Restart, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void EnableWADtoHBC_CheckedChanged(object sender, EventArgs e) => IP.Enabled = enableWADtoHBC.Checked;
    }
}
