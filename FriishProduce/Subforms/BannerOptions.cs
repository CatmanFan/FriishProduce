using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class BannerOptions : Form
    {
        internal string origTitle;
        internal int origYear;
        internal int origPlayers;
        internal int origRegion;

        public BannerOptions(Platform platform)
        {
            InitializeComponent();

            region.Items.AddRange(new string[] { Program.Lang.String("automatic"), Program.Lang.String("region_j"), Program.Lang.String("region_u"), Program.Lang.String("region_e"), Program.Lang.String("region_k") });
            region.SelectedIndex = Properties.Settings.Default.default_banner_region;

            // Remove Korea option for non-available platforms
            // ********
            if (region.Items.Count == 5 && (int)platform >= 3)
            {
                if (region.SelectedIndex == 4) region.SelectedIndex = 0;
                region.Items.RemoveAt(4);
            }

            // Remove Japan option for C64 & Flash
            // ********
            if (region.Items.Count == 4 && platform == Platform.C64 || platform == Platform.Flash)
            {
                if (region.SelectedIndex == 1) region.SelectedIndex = 0;
                region.Items.RemoveAt(1);
            }

            // Remove USA/Europe options for MSX
            // ********
            if (region.Items.Count == 4 && platform == Platform.MSX)
            {
                region.Items.Clear();
                region.Items.Add(Program.Lang.String("region_j"));
                region.SelectedIndex = 0;
            }

            region.Enabled = region.Items.Count > 1;

            Program.Lang.Control(this);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Hide();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Hide();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void isClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                title.Text = origTitle;
                released.Value = origYear;
                players.Value = origPlayers;
                region.SelectedIndex = origRegion;
            }
        }
    }
}
