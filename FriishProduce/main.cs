using System;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (page1.Visible)
            {
                page2.Visible = true;
                page1.Visible = false;
            }
        }
    }
}
