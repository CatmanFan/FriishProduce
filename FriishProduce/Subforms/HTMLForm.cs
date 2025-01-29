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
    public partial class HTMLForm : Form
    {
        public string FormText { get; private set; } = null;

        public HTMLForm(string text = null, string title = null)
        {
            InitializeComponent();
            Font = Program.MainForm.Font;
            FormText = text;
            Text = title;
            htmlPanel1.Text = string.IsNullOrWhiteSpace(text) ? "Not implemented" : text;
            b_ok.Text = Program.Lang.String("b_close");
        }
    }
}
