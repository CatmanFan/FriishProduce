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
            if (region.Items.Count == 5 && (int)platform >= 3)
            {
                if (region.SelectedIndex == 4) region.SelectedIndex = 0;
                region.Items.RemoveAt(4);
            }

            Program.Lang.Control(this);
            Text = Program.Lang.String("banner", Tag.ToString());
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BannerOptions_FormClosing(object sender, FormClosingEventArgs e)
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
