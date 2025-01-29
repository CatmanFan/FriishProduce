using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Controller_Flash : ControllerMapping
    {
        public Controller_Flash() : base()
        {
            AllowedKeymaps = Allowed.Wiimote | Allowed.Nunchuk | Allowed.Classic;
            InitializeComponent();

            #region Modifiable values: Buttons
            available = new()
            {
                Wii = new Buttons[]
                {
                    Buttons.WiiRemote_Left,
                    Buttons.WiiRemote_Right,
                    Buttons.WiiRemote_Down,
                    Buttons.WiiRemote_Up,
                    Buttons.WiiRemote_A,
                    Buttons.WiiRemote_B,
                    Buttons.WiiRemote_Home,
                    Buttons.WiiRemote_Plus,
                    Buttons.WiiRemote_Minus,
                    Buttons.WiiRemote_1,
                    Buttons.WiiRemote_2,
                    Buttons.Nunchuck_Z,
                    Buttons.Nunchuck_C,

                    Buttons.Classic_Up,
                    Buttons.Classic_Left,
                    Buttons.Classic_ZR,
                    Buttons.Classic_X,
                    Buttons.Classic_A,
                    Buttons.Classic_Y,
                    Buttons.Classic_B,
                    Buttons.Classic_ZL,
                    Buttons.Classic_R,
                    Buttons.Classic_Plus,
                    Buttons.Classic_Home,
                    Buttons.Classic_Minus,
                    Buttons.Classic_L,
                    Buttons.Classic_Down,
                    Buttons.Classic_Right
                },

                Local_Displayed = new string[]
                {    
                    // Buttons used in the target console, as displayed on the form
                    // ************************************************************
                    Program.Lang.String("up", "controller"),
                    Program.Lang.String("left", "controller"),
                    Program.Lang.String("right", "controller"),
                    Program.Lang.String("down", "controller"),
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
                    ";　　:",
                    "=　　+",
                    ",　　<",
                    "-　　_",
                    ".　　>",
                    "/　　?",
                    "`　　~",
                    "[　　{",
                    "\\　　|",
                    "]　　}",
                    "'　　\"",

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
                },

                Local_Formatted = new string[]
                {
                    // Buttons used in the target console, as formatted in configuration file on WAD
                    // *****************************************************************************
                    "KEY_UP",
                    "KEY_LEFT",
                    "KEY_RIGHT",
                    "KEY_DOWN",
                    "KEY_BACKSPACE",
                    "KEY_ENTER",
                    "KEY_SHIFT",
                    "KEY_CTRL",
                    "18",
                    "KEY_CAPS",
                    "27",
                    "32",

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
                    "KEY_TAB"
                }
            };
            #endregion

            #region Modifiable values: Available presets
            presets = new Dictionary<string, string[]>()
            {
                { "Basic (Horizontal)", new string[]
                    {
                        "KEY_DOWN",
                        "KEY_UP",
                        "KEY_RIGHT",
                        "KEY_LEFT",
                        "KEY_SELECT",
                        null,
                        null,
                        "KEY_ENTER",
                        null,
                        "32",
                        null,
                        null,
                        null,

                        "KEY_UP",
                        "KEY_LEFT",
                        null,
                        null,
                        "32",
                        null,
                        null,
                        null,
                        null,
                        "KEY_ENTER",
                        null,
                        null,
                        null,
                        "KEY_DOWN",
                        "KEY_RIGHT"
                    }
                },

                { "Basic (Vertical)", new string[]
                    {
                        "KEY_LEFT",
                        "KEY_RIGHT",
                        "KEY_DOWN",
                        "KEY_UP",
                        "KEY_SELECT",
                        null,
                        null,
                        "KEY_ENTER",
                        null,
                        "32",
                        null,
                        null,
                        null,

                        "KEY_UP",
                        "KEY_LEFT",
                        null,
                        null,
                        "32",
                        null,
                        null,
                        null,
                        null,
                        "KEY_ENTER",
                        null,
                        null,
                        null,
                        "KEY_DOWN",
                        "KEY_RIGHT"
                    }
                },

                { "Mouse-Only", new string[]
                    {
                        null,
                        null,
                        null,
                        null,
                        "KEY_SELECT",
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,

                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
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