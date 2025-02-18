using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Controller_SNES : ControllerMapping
    {
        public Controller_SNES() : base()
        {
            AllowedKeymaps = Allowed.Wiimote;
            InitializeComponent();
            if (DesignMode) return;

            #region Modifiable values: Buttons
            available = new()
            {
                Wii = new Buttons[]
                {
                    Buttons.WiiRemote_1,
                    Buttons.WiiRemote_2,
                    Buttons.WiiRemote_A,
                    Buttons.WiiRemote_B,
                    Buttons.WiiRemote_Up,
                    Buttons.WiiRemote_Down,
                    Buttons.WiiRemote_Left,
                    Buttons.WiiRemote_Right,
                    Buttons.WiiRemote_Plus,
                    Buttons.WiiRemote_Minus
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
                    "X",
                    "Y",
                    "L",
                    "R",
                    "Start",
                    "Select"
                },

                Local_Formatted = new string[]
                {
                    // Buttons used in the target console, as formatted in configuration file on WAD
                    // *****************************************************************************
                    "Up",
                    "Left",
                    "Right",
                    "Down",
                    "A",
                    "B",
                    "X",
                    "Y",
                    "L",
                    "R",
                    "Start",
                    "Select"
                }
            };
            #endregion

            #region Modifiable values: Available presets
            presets = new Dictionary<string, string[]>()
            {
                { "Basic (Horizontal)", new string[]
                    {
                        "Y",
                        "B",
                        "A",
                        "X",
                        "Left",
                        "Right",
                        "Down",
                        "Up",
                        "Start",
                        "Select"
                    }
                },

                { "Basic (Vertical)", new string[]
                    {
                        "X",
                        "Y",
                        "A",
                        "B",
                        "Up",
                        "Down",
                        "Left",
                        "Right",
                        "Start",
                        "Select",
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