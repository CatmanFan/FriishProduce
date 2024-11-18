using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce.Subforms
{
    public partial class ControllerOptions : Form
    {
        public enum Buttons
        {
            WiiRemote_Up,
            WiiRemote_Left,
            WiiRemote_Right,
            WiiRemote_Down,
            WiiRemote_A,
            WiiRemote_B,
            WiiRemote_1,
            WiiRemote_2,
            WiiRemote_Plus,
            WiiRemote_Minus,
            WiiRemote_Home,
            Nunchuck_Z,
            Nunchuck_C,
            Classic_Up,
            Classic_Left,
            Classic_Right,
            Classic_Down,
            Classic_A,
            Classic_B,
            Classic_X,
            Classic_Y,
            Classic_L,
            Classic_R,
            Classic_ZL,
            Classic_ZR,
            Classic_Plus,
            Classic_Minus,
            Classic_Home,
            GC_Up,
            GC_Left,
            GC_Right,
            GC_Down,
            GC_A,
            GC_B,
            GC_X,
            GC_Y,
            GC_L,
            GC_R,
            GC_Z,
            GC_Start,
            GC_Select
        };

        public ControllerOptions()
        {
            InitializeComponent();
        }

        public IDictionary<Buttons, string> Mapping = new Dictionary<Buttons, string>()
        {
            { Buttons.WiiRemote_Left, null },
            { Buttons.WiiRemote_Right, null },
            { Buttons.WiiRemote_Down, null },
            { Buttons.WiiRemote_Up, null },
            { Buttons.WiiRemote_A, null },
            { Buttons.WiiRemote_B, null },
            { Buttons.WiiRemote_Home, null },
            { Buttons.WiiRemote_Plus, null },
            { Buttons.WiiRemote_Minus, null },
            { Buttons.WiiRemote_1, null },
            { Buttons.WiiRemote_2, null },
            { Buttons.Nunchuck_Z, null },
            { Buttons.Nunchuck_C, null },
            { Buttons.Classic_Up, null },
            { Buttons.Classic_Left, null },
            { Buttons.Classic_ZR, null },
            { Buttons.Classic_X, null },
            { Buttons.Classic_A, null },
            { Buttons.Classic_Y, null },
            { Buttons.Classic_B, null },
            { Buttons.Classic_ZL, null },
            { Buttons.Classic_R, null },
            { Buttons.Classic_Plus, null },
            { Buttons.Classic_Home, null },
            { Buttons.Classic_Minus, null },
            { Buttons.Classic_L, null },
            { Buttons.Classic_Down, null },
            { Buttons.Classic_Right, null }
        };

        protected void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            DialogResult = DialogResult.OK;
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            DialogResult = DialogResult.Cancel;
        }

        protected void Form_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            CenterToParent();
        }
    }
}
