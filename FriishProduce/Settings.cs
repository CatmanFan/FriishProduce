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
            HBC.Checked = Properties.Settings.Default.hbc;
            HBC_IP.Enabled = Properties.Settings.Default.hbc;
            HBC_IP.Text = Properties.Settings.Default.hbc_ip;
            HBC_Protocol.Enabled = Properties.Settings.Default.hbc;
            HBC_Protocol.SelectedIndex = Properties.Settings.Default.hbc_protocol;
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

            Properties.Settings.Default.hbc = HBC.Checked;
            Properties.Settings.Default.hbc_ip = HBC_IP.Text;
            Properties.Settings.Default.hbc_protocol = HBC_Protocol.SelectedIndex;

            if (showRestart) MessageBox.Show(strings.settings_Restart, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void EnableWADtoHBC_CheckedChanged(object sender, EventArgs e) { HBC_IP.Enabled = HBC.Checked; HBC_Protocol.Enabled = HBC.Checked; }
    }
}
