using System;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class ChannelTitles : Form
    {
        public ChannelTitles(string text)
        {
            InitializeComponent();

            OK.Text = Program.Lang.String("b_ok");
            foreach (TextBox TextBox in Controls.OfType<TextBox>())
                TextBox.Text = text;
        }

        private void OK_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        private void TextIsChanged(object sender, EventArgs e)
        {
            int isFilled = 0;
            int total = 0;

            foreach (TextBox TextBox in Controls.OfType<TextBox>())
            {
                if (!string.IsNullOrWhiteSpace(TextBox.Text)) isFilled++;
                total++;
            }

            OK.Enabled = isFilled == total;
        }
    }
}
