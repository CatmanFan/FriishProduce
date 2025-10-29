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
    public partial class Wait : Form
    {
        private int? _downloadTries;
        public int? downloadTries
        {
            get => _downloadTries;
            set
            {
                if (_downloadTries.HasValue)
                    downloadLabel.Text = string.Format(Program.Lang.String("b_005_tries", "messages"), value);
                _downloadTries = value;
            }
        }

        public string Msg
        {
            get => label1.Text;
            set
            {
                label1.Text = value;

                // Set width if too short
                // ****************
                var maxWidth = label1.Width + (label1.Padding.Left * 2) + 20;
                if (maxWidth > ClientSize.Width)
                {
                    ClientSize = new Size(maxWidth, ClientSize.Height);
                    Size = SizeFromClientSize(ClientSize);
                }
            }
        }

        public Wait(bool showProgress, string msg = null, bool download = false)
        {
            InitializeComponent();

            Theme.ChangeColors(this, true);
            label1.ForeColor = Theme.Colors.Headline;

            Text = Program.Lang.ApplicationTitle;
            // if (Program.GUI) label1.Font = Program.MainForm.Font;
            label1.Text = msg ?? Program.Lang.String("b_000", "messages");

            if (download == true)
            {
                downloadTries = 10;
                downloadLabel.Text = string.Format(Program.Lang.String("tries"), downloadTries);
                downloadLabel.Visible = true;
                ClientSize = new Size(ClientSize.Width, ClientSize.Height + 12);
                Size = SizeFromClientSize(ClientSize);
            }

            progress.Visible = showProgress;

            // Set width
            // ****************
            var maxWidth = label1.Width + (label1.Padding.Left * 2) + 20;
            ClientSize = new Size(Math.Min(ClientSize.Width, Math.Max(330, maxWidth)), ClientSize.Height);
            Size = SizeFromClientSize(ClientSize);

            // Set RTL if needed
            // ****************
            if (Program.Lang.GetScript(label1.Text) == Language.ScriptType.RTL)
                RightToLeft = RightToLeft.Yes;
        }

        private void IsClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason is CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }
        }

        private void IsShown(object sender, EventArgs e)
        {
            // Activate();
            try { CenterToParent(); } catch { CenterToScreen(); }
        }
    }
}