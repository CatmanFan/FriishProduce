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
    public partial class MessageBoxHTML : Form
    {
        public string FormText { get; private set; } = null;

        public MessageBoxHTML(string text = null, string title = null)
        {
            InitializeComponent();
            Text = title ?? Program.Lang.ApplicationTitle;
            FormText = text;
            b_close.Text = Program.Lang.String("b_close");

            Theme.ChangeColors(this, true);
            Theme.BtnSizes(b_close);
            Theme.BtnLayout(this, b_close);

            htmlPanel1.BackColor = BackColor;
            htmlPanel1.BaseStylesheet = HTML.BaseStylesheet + "\n" + "div { padding: 6px !important; }";
            htmlPanel1.Text = string.IsNullOrWhiteSpace(text) ? "<div>Not implemented</div>" : text;

            const int spacing = 16;
            htmlPanel1.Location = new(spacing, spacing);
            htmlPanel1.Size = new(ClientSize.Width - htmlPanel1.Location.X * 2, ClientSize.Height - bottomPanel2.Height - htmlPanel1.Location.Y * 2);
        }

        private void IsShown(object sender, EventArgs e)
        {
            Activate();
            try { CenterToParent(); } catch { CenterToScreen(); }
        }
    }
}
