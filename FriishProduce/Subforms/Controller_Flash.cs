using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Controller_Flash : ControllerMapping
    {
        public Controller_Flash() : base()
        {
            InitializeComponent();
            ;
        }

        // ---------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------------------------------------------
        // /!\ MODIFIABLE FUNCTIONS AND VALUES NEEDED! /!\
        // ---------------------------------------------------------------------------------------------------------------

        // Buttons used in the target console, as displayed on the form
        // ************************************************************
        protected new string[] TargetButtons_List = new string[]
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
        protected new string[] TargetButtons_Keys = new string[]
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
    }
}
