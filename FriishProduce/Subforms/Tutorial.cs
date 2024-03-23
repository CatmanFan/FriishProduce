using System;
using System.IO;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();
            Language.Localize(this);
            CloseButton.Text = Language.Get("B.Close");
            richTextBox.Rtf = Properties.Resources.Tutorial;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
