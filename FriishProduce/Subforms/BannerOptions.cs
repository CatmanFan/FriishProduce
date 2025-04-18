using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class BannerOptions : Form
    {
        internal string origTitle;
        internal string origSound;
        internal string sound;
        internal int origYear;
        internal int origPlayers;
        internal int origRegion;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip tip = HTML.CreateToolTip();

        public BannerOptions(Platform platform)
        {
            InitializeComponent();
            if (DesignMode) return;

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
            Program.Lang.ToolTip(tip, title, "banner_title", label1.Text);
            Program.Lang.ToolTip(tip, region, "banner_region", label4.Text);

            Theme.ChangeColors(this, false);
            Theme.BtnSizes(b_ok, b_cancel);
            Theme.BtnLayout(this, b_ok, b_cancel);
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
            isPlaying = false;

            if (DialogResult != DialogResult.OK)
            {
                title.Text = origTitle;
                released.Value = origYear;
                players.Value = origPlayers;
                region.SelectedIndex = origRegion;

                sound = origSound;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (sender as TextBox)?.Lines?.Length >= 2)
            {
                SystemSounds.Beep.Play();
                e.SuppressKeyPress = true;
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox)?.Lines?.Length >= 2)
                (sender as TextBox).Lines = new string[] { (sender as TextBox).Lines[0], (sender as TextBox).Lines[1] };
        }

        public void LoadSound(string file)
        {
            isPlaying = false;

            sound = file;
            banner_sound_restore.Enabled = File.Exists(file) && file != null;
            if (!banner_sound_restore.Enabled) sound = null;
        }

        private void isShown(object sender, EventArgs e)
        {
            banner_sound_restore.Enabled = File.Exists(sound) && sound != null;
        }

        private void banner_sound_replace_Click(object sender, EventArgs e)
        {
            browseSound.Filter = "WAV (*.wav)|*.wav" + Program.Lang.String("filter");
            browseSound.Title = banner_sound.Text.Replace("&", "");
            if (browseSound.ShowDialog() == DialogResult.OK || File.Exists(browseSound.FileName))
                LoadSound(browseSound.FileName);
        }

        private void banner_sound_restore_Click(object sender, EventArgs e) => LoadSound(null);

        private bool _isPlaying = false;
        private bool isPlaying
        {
            get => _isPlaying;
            set
            {
                bool isChanged = _isPlaying != value;
                _isPlaying = value;

                if (isChanged)
                {
                    if (value)
                    {
                        banner_sound_play.Image = Properties.Resources.control_stop;
                    }

                    else
                    {
                        if (player != null)
                        {
                            player.Stop();
                            player = null;
                        }

                        banner_sound_play.Image = Properties.Resources.control_play;
                        playerThread.Cancel();
                    }
                }
            }
        }

        private System.Threading.CancellationTokenSource playerThread = new();
        private SoundPlayer player = null;
        private void banner_sound_play_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                player = File.Exists(sound) && sound != null ? new(sound) : new(Properties.Resources.Sound_WiiVC);

                playerThread = new();
                Task.Run(() => PlaySound(playerThread.Token));
            }

            else isPlaying = false;
        }

        private void PlaySound(System.Threading.CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            try
            {
                isPlaying = true;
                player.PlaySync();
                playerThread.Cancel();
            }

            finally
            {
                isPlaying = false;
            }
        }
    }
}
