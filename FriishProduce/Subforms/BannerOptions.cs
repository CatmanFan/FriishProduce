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
            region.SelectedIndex = Program.Config.application.default_banner_region;

            // Remove Korea option for non-available platforms
            // ********
            if (region.Items.Count == 5 && (int)platform >= 3)
            {
                if (region.SelectedIndex == 4) region.SelectedIndex = 0;
                region.Items.RemoveAt(4);
            }

            // Remove Japan option for C64 & Flash
            // ********
            if (region.Items.Count == 4 && (platform is Platform.C64 or Platform.Flash))
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
            Tip.SetToolTip(title, Program.Lang.HTML(1, true, label1.Text));
            Tip.SetToolTip(region, Program.Lang.HTML(2, true, label4.Text));
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (sender as TextBox)?.Lines?.Length >= 2)
            {
                System.Media.SystemSounds.Beep.Play();
                e.SuppressKeyPress = true;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox)?.Lines?.Length >= 2)
                (sender as TextBox).Lines = new string[] { (sender as TextBox).Lines[0], (sender as TextBox).Lines[1] };
        }
    }
}
