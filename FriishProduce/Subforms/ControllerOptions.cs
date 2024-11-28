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

            // Cosmetic
            // ---------------
            // Remove this code when creating a new copy
            // *****************************************
            if (!DesignMode)
            {
                b_ok.Click += OK_Click;
                b_cancel.Click += Cancel_Click;
                Load += Form_Load;
            }
            // *****************************************
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected IDictionary<Buttons, string> mapping = new Dictionary<Buttons, string>()
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

        // ^^^ MODIFY THIS ONLY

        // ---------------------------------------------------------------------------------------------------------------
        // /!\ REMAINING FUNCTIONS AND VALUES NEEDED FOR THE FORM BELOW. THERE IS NO NEED TO COPY THEM. /!\
        // ---------------------------------------------------------------------------------------------------------------

        // Buttons used in the target console, as displayed on the form
        // ************************************************************
        protected string[] TargetButtons_List = new string[]
        {
            "Left",
            "Right",
            "Down",
            "Up",
            "Backspace",
            "Enter",
            "Shift",
            "Ctrl",
            "Alt",
            "CapsLock",
            "Esc",
            "Space",

            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            ";   :",
            "=   +",
            ",   <",
            "-   _",
            ".   >",
            "/   ?",
            "`   ~",
            "[   {",
            "\\   |",
            "]   }",
            "'   \"",

            "PageUp",
            "PageDown",
            "Insert",
            "Home",
            "End",
            "Delete",
            "Select",
            "Forward",
            "Backward",
            "Tab",
        };

        // Buttons used in the target console, as formatted in configuration file on WAD
        // *****************************************************************************
        protected string[] TargetButtons_Keys = new string[]
        {
            "KEY_LEFT",
            "KEY_RIGHT",
            "KEY_DOWN",
            "KEY_UP",
            "KEY_BACKSPACE",
            "KEY_ENTER",
            "KEY_SHIFT",
            "KEY_CTRL",
            "18",
            "KEY_CAPS",
            "27",
            "KEY_ESCAPE",

            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "219",
            "220",
            "221",
            "222",

            "KEY_PAGEUP",
            "KEY_PAGEDOWN",
            "KEY_INSERT",
            "KEY_HOME",
            "KEY_END",
            "KEY_DELETE",
            "KEY_SELECT",
            "KEY_FORWARD",
            "KEY_BACKWARD",
            "KEY_TAB",


            "KEY_BUTTON_LEFT",
            "KEY_BUTTON_RIGHT",
            "KEY_BUTTON_DOWN",
            "KEY_BUTTON_UP",
            "KEY_BUTTON_A",
            "KEY_BUTTON_B",
            "KEY_BUTTON_HOME",
            "KEY_BUTTON_PLUS",
            "KEY_BUTTON_MINUS",
            "KEY_BUTTON_1",
            "KEY_BUTTON_2",
            "KEY_BUTTON_Z",
            "KEY_BUTTON_C",
            "KEY_CL_BUTTON_UP",
            "KEY_CL_BUTTON_LEFT",
            "KEY_CL_TRIGGER_ZR",
            "KEY_CL_BUTTON_X",
            "KEY_CL_BUTTON_A",
            "KEY_CL_BUTTON_Y",
            "KEY_CL_BUTTON_B",
            "KEY_CL_TRIGGER_ZL",
            "KEY_CL_RESERVED",
            "KEY_CL_TRIGGER_R",
            "KEY_CL_BUTTON_PLUS",
            "KEY_CL_BUTTON_HOME",
            "KEY_CL_BUTTON_MINUS",
            "KEY_CL_TRIGGER_L",
            "KEY_CL_BUTTON_DOWN",
            "KEY_CL_BUTTON_RIGHT",
        };

        public List<string> Mapping = new();

        // ---------------------------------------------------------------------------------------------------------------

        protected void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            foreach (var value in mapping.Values)
            {
                Mapping.Add(value);
            }
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
