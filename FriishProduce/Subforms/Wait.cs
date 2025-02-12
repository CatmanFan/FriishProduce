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
        public string Msg
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public Wait(bool showProgress, string msg = null)
        {
            InitializeComponent();

            Theme.ChangeColors(this, true);
            label1.ForeColor = Theme.Colors.Headline;

            Text = Program.Lang.ApplicationTitle;
            // if (Program.MainForm != null) label1.Font = Program.MainForm.Font;
            label1.Text = msg ?? Program.Lang.String("busy0");

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
    }
}