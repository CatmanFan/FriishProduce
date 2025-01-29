using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Controller_SEGA : ControllerMapping
    {
        public Controller_SEGA(bool IsSMS) : base()
        {
            AllowedKeymaps = Allowed.Wiimote | Allowed.Classic | Allowed.GC;
            InitializeComponent();

            #region Modifiable values: Buttons
            available = new()
            {
                Wii = new Buttons[]
                {
                    Buttons.Classic_Up,
                    Buttons.Classic_Down,
                    Buttons.Classic_Left,
                    Buttons.Classic_Right,
                    Buttons.Classic_Plus,
                    Buttons.Classic_Y,
                    Buttons.Classic_B,
                    Buttons.Classic_A,
                    Buttons.Classic_L,
                    Buttons.Classic_R,
                    Buttons.Classic_ZL,
                    Buttons.Classic_X,
                    Buttons.Classic_ZR,

                    Buttons.WiiRemote_Right,
                    Buttons.WiiRemote_Left,
                    Buttons.WiiRemote_Up,
                    Buttons.WiiRemote_Down,
                    Buttons.WiiRemote_Plus,
                    Buttons.WiiRemote_A,
                    Buttons.WiiRemote_1,
                    Buttons.WiiRemote_2,
                    Buttons.WiiRemote_B,

                    Buttons.GC_Up,
                    Buttons.GC_Down,
                    Buttons.GC_Left,
                    Buttons.GC_Right,
                    Buttons.GC_Start,
                    Buttons.GC_B,
                    Buttons.GC_A,
                    Buttons.GC_X,
                    Buttons.GC_L,
                    Buttons.GC_Y,
                    Buttons.GC_R,
                    Buttons.GC_Z,
                    // Buttons.GC_C,
                },

                Local_Displayed = IsSMS ? new string[]
                {    
                    // Buttons used in the target console, as displayed on the form
                    // ************************************************************
                    Program.Lang.String("up", "controller"),
                    Program.Lang.String("left", "controller"),
                    Program.Lang.String("right", "controller"),
                    Program.Lang.String("down", "controller"),
                    "1",
                    "2",
                    "1 (Rapidfire)",
                    "2 (Rapidfire)",
                    "Pause",
                    "Rapidfire",
                } : new string[]
                {    
                    // Buttons used in the target console, as displayed on the form
                    // ************************************************************
                    Program.Lang.String("up", "controller"),
                    Program.Lang.String("left", "controller"),
                    Program.Lang.String("right", "controller"),
                    Program.Lang.String("down", "controller"),
                    "A",
                    "B",
                    "C",
                    "X",
                    "Y",
                    "Z",
                    "Start",
                    "Mode",
                },

                Local_Formatted = IsSMS ? new string[]
                {
                    // Buttons used in the target console, as formatted in configuration file on WAD
                    // *****************************************************************************
                    "U",
                    "L",
                    "R",
                    "D",
                    "B",
                    "C",
                    "Y",
                    "Z",
                    "S",
                    "M",
                } : new string[]
                {
                    // Buttons used in the target console, as formatted in configuration file on WAD
                    // *****************************************************************************
                    "U",
                    "L",
                    "R",
                    "D",
                    "A",
                    "B",
                    "C",
                    "X",
                    "Y",
                    "Z",
                    "S",
                    "M",
                }
            };
            #endregion

            #region Modifiable values: Available presets
            presets = new Dictionary<string, string[]>()
            {
                { Program.Lang.String("default"), new string[]
                    {
                        "U",
                        "D",
                        "L",
                        "R",
                        "S",
                        IsSMS ? "C" : "A",
                        "B",
                        "C",
                        IsSMS ? "Y" : "X",
                        IsSMS ? "Z" : "Y",
                        IsSMS ? "M" : "Z",
                        IsSMS ? null : "M",
                        null,

                        "U",
                        "D",
                        "L",
                        "R",
                        "S",
                        IsSMS ? null : "A",
                        IsSMS ? "B" : "B",
                        IsSMS ? "C" : "C",
                        null,

                        "U",
                        "D",
                        "L",
                        "R",
                        "S",
                        IsSMS ? "C" : "A",
                        "B",
                        "C",
                        IsSMS ? "Y" : "X",
                        IsSMS ? null : "Y",
                        "Z",
                        "M",
                        // null
                    }
                },
            };
            #endregion

            ResetLayout();

            // Localization
            // -----------------------------------------------------------------------------------------------------------
            Program.Lang.Control(this, "controller");
            Text = Program.Lang.String("controller", "controller").TrimEnd('.').Trim();
        }
    }
}