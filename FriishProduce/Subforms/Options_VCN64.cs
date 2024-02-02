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
        public IDictionary<int, bool> Settings = new Dictionary<int, bool>
        {
            /* Brightness fix */        { 0, true },
            /* Crashes fix */           { 1, true },
            /* Extended RAM */          { 2, false },
            /* Allocate for ROM size */ { 3, false },
            /* Use ROMC type 0 */       { 4, false }
        };

        public InjectorN64.Type EmuType { get; set; }

        public Options_VCN64()
        {
            InitializeComponent();
            Language.AutoSetForm(this);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Options
            // *******
            n64003.Enabled = n64001.Enabled = EmuType == InjectorN64.Type.Rev1 || EmuType == InjectorN64.Type.Rev1_Alt;
            n64004.Visible = n64004.Enabled = EmuType == InjectorN64.Type.Rev3;
            n64000.Checked = Settings[0];
            n64001.Checked = Settings[1];
            n64002.Checked = Settings[2];
            n64003.Checked = Settings[3];
            ROMCType.SelectedIndex = Settings[4] ? 0 : 1;
            // *******

            panel3.Height = 6 + Math.Max(x009.Height, pictureBox1.Height) + 7;
            Height = EmuType == InjectorN64.Type.Rev3 ? 320 : 260;

            CenterToParent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // Options
            // *******
            Settings[0] = n64000.Checked;
            Settings[1] = n64001.Checked;
            Settings[2] = n64002.Checked;
            Settings[3] = n64003.Checked;
            Settings[4] = ROMCType.SelectedIndex == 0;
            // *******

            DialogResult = DialogResult.OK;
        }
    }
}
