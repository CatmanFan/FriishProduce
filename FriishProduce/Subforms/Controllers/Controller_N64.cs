using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Controller_N64 : ControllerMapping
    {
        public Controller_N64() : base()
        {
            AllowedKeymaps = Allowed.Classic | Allowed.ContSticks;
            InitializeComponent();
            if (DesignMode) return;

            #region Modifiable values: Buttons
            available = new()
            {
                Wii = new Buttons[]
                {
                    Buttons.Classic_A,
                    Buttons.Classic_B,
                    Buttons.Classic_X,
                    Buttons.Classic_Y,
                    Buttons.Classic_L,
                    Buttons.Classic_R,
                    Buttons.Classic_ZR,
                    Buttons.Classic_Plus,

                    Buttons.Classic_Up_L,
                    Buttons.Classic_Down_L,
                    Buttons.Classic_Left_L,
                    Buttons.Classic_Right_L,
                    Buttons.Classic_Up,
                    Buttons.Classic_Down,
                    Buttons.Classic_Right,
                    Buttons.Classic_Left,
                    Buttons.Classic_Up_R,
                    Buttons.Classic_Down_R,
                    Buttons.Classic_Left_R,
                    Buttons.Classic_Right_R
                },

                Local_Displayed = new string[]
                {    
                    // Buttons used in the target console, as displayed on the form
                    // ************************************************************
                    Program.Lang.String("up", "controller"),
                    Program.Lang.String("left", "controller"),
                    Program.Lang.String("right", "controller"),
                    Program.Lang.String("down", "controller"),
                    "A",
                    "B",
                    "Z",
                    "C-" + Program.Lang.String("up", "controller"),
                    "C-" + Program.Lang.String("left", "controller"),
                    "C-" + Program.Lang.String("right", "controller"),
                    "C-" + Program.Lang.String("down", "controller"),
                    "L",
                    "R",
                    "Start"
                },

                Local_Formatted = new string[]
                {
                    // Buttons used in the target console, as formatted in configuration file on WAD
                    // *****************************************************************************
                    "08 00", // u
                    "02 00", // l
                    "01 00", // r
                    "04 00", // d
                    "80 00", // A
                    "40 00", // B
                    "20 00", // Z
                    "00 08", // c-u
                    "00 02", // c-l
                    "00 01", // c-r
                    "00 04", // c-d
                    "00 20", // L
                    "00 10", // R
                    "10 00" // S
                }
            };
            #endregion

            #region Modifiable values: Available presets
            presets = new Dictionary<string, string[]>()
            {
                { Program.Lang.String("preset_basic", "controller"), new string[]
                    {
                        "80 00",
                        "40 00",
                        null,
                        null,
                        "00 20",
                        "00 10",
                        "20 00",
                        "10 00",

                        "08 00",
                        "04 00",
                        "02 00",
                        "01 00",
                        "08 00",
                        "04 00",
                        "02 00",
                        "01 00",
                        "00 08",
                        "00 04",
                        "00 02",
                        "00 01"
                    }
                }
            };
            #endregion

            ResetLayout();

            // Localization
            // -----------------------------------------------------------------------------------------------------------
            Program.Lang.Control(this, "controller");
            Text = Program.Lang.String("controller", "controller").TrimEnd('.').Trim();

            Theme.ChangeColors(this, false);
            Theme.BtnSizes(b_save, b_close);
            Theme.BtnLayout(this, b_save, b_close);
        }
    }
}