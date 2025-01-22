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
        private Timer fader;

        public Wait(bool showProgress, int msg = 0)
        {
            InitializeComponent();

            Opacity = 0;
            fader = new() { Interval = 5, Enabled = true };
            fader.Tick += Fade;
            fader.Start();

            if (Program.MainForm != null)
                label1.Font = Program.MainForm.Font;
            label1.Text = Program.Lang.String($"busy{msg}");

            progress.Visible = showProgress;
            if (!showProgress)
            {
                tableLayoutPanel2.RowCount = 1;
                label1.TextAlign = ContentAlignment.MiddleLeft;
                label1.Padding = new(label1.Padding.Left, 0, 0, 0);
            }
            else
            {
                tableLayoutPanel1.Height += 2;
                Height += 2;
            }
        }

        private void Fade(object sender, EventArgs e)
        {
            Opacity += 2;
            if (Opacity >= 100 && fader != null)
            {
                fader.Stop();
                fader.Enabled = false;
                fader.Dispose();
            }
        }
    }
}
