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
    public partial class Options_VCN64 : Form
    {
        // Options
        // *******
        public IDictionary<int, string> Settings = new Dictionary<int, string>
        {
            /* Brightness fix */        { 0, "False" },
            /* Crashes fix */           { 1, "False" },
            /* Extended RAM */          { 2, "False" },
            /* Allocate for ROM size */ { 3, "False" },
            /* Use ROMC type 0 */       { 4, "False" }
        };

        public int EmuType { get; set; }

        public Options_VCN64()
        {
            InitializeComponent();
            Language.AutoSetForm(this);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Options
            // *******
            n64003.Enabled = n64001.Enabled = EmuType <= 1;
            n64004.Visible = n64004.Enabled = EmuType == 3;
            n64000.Checked = bool.Parse(Settings[0]);
            n64001.Checked = bool.Parse(Settings[1]);
            n64002.Checked = bool.Parse(Settings[2]);
            n64003.Checked = bool.Parse(Settings[3]);
            ROMCType.SelectedIndex = bool.Parse(Settings[4]) ? 0 : 1;
            // *******

            panel3.Height = 6 + Math.Max(x009.Height, pictureBox1.Height) + 7;
            Height = EmuType == 3 ? 320 : 260;

            CenterToParent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // Options
            // *******
            Settings[0] = n64000.Checked.ToString();
            Settings[1] = n64001.Checked.ToString();
            Settings[2] = n64002.Checked.ToString();
            Settings[3] = n64003.Checked.ToString();
            Settings[4] = (ROMCType.SelectedIndex == 0).ToString();
            // *******

            DialogResult = DialogResult.OK;
        }
    }
}
