namespace FriishProduce
{
    public partial class Controller_SEGA : ControllerMapping
    {
        public Controller_SEGA() : base()
        {
            UsesGC      = true;
            UsesNunchuk = false;
            InitializeComponent();

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
                    Buttons.WiiRemote_1,
                    Buttons.WiiRemote_2,
                    // Buttons.WiiRemote_AB,
                    Buttons.WiiRemote_A,
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
                    Buttons.GC_R,
                    Buttons.GC_Z,
                    // Buttons.GC_C,
                    Buttons.GC_Y,
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
                    "C",
                    "X",
                    "Y",
                    "Z",
                    "Start",
                    "Mode",
                },

                Local_Formatted = new string[]
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

            ResetLayout();

            // Localization
            // -----------------------------------------------------------------------------------------------------------
            Program.Lang.Control(this, "controller");
            Text = Program.Lang.String("controller_mapping", "projectform").TrimEnd('.').Trim();
        }
    }
}
