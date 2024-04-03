using System;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();
            Text = Program.Lang.String("tutorial", "mainform");
            b_close.Text = Program.Lang.String("b_close");
            richTextBox.Rtf = Properties.Resources.Tutorial;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
