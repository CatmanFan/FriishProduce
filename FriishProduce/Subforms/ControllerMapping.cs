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
    public partial class ControllerMapping : Form
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
            GC_Start
        };

        private Buttons[] mapping = new Buttons[]
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
            // Buttons.Classic_Home,
            Buttons.Classic_Minus,
            Buttons.Classic_L,
            Buttons.Classic_Down,
            Buttons.Classic_Right
        };

        public ControllerMapping()
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

        public List<string> Mapping = new();
        public bool UsesGC { get; set; }

        // ---------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------------------------------------------
        // /!\ MODIFIABLE FUNCTIONS AND VALUES NEEDED! /!\
        // ---------------------------------------------------------------------------------------------------------------

        // Buttons used in the target console, as displayed on the form
        // ************************************************************
        protected string[] TargetButtons_List = new string[]
        {
        };

        // Buttons used in the target console, as formatted in configuration file on WAD
        // *****************************************************************************
        protected string[] TargetButtons_Keys = new string[]
        {
        };

        // ---------------------------------------------------------------------------------------------------------------

        protected void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            foreach (Buttons instance in mapping)
            {
                ComboBox setting = Controls.Find(instance.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                if (setting != null)
                    Mapping.Add(setting.SelectedIndex == 0 ? null : TargetButtons_Keys[setting.SelectedIndex - 1]);
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
            foreach (ComboBox c in page1.Controls.OfType<ComboBox>()) c.SelectedIndex = 0;
            foreach (ComboBox c in page2.Controls.OfType<ComboBox>()) c.SelectedIndex = 0;
            foreach (ComboBox c in page3.Controls.OfType<ComboBox>()) c.SelectedIndex = 0;

            if (!UsesGC) tabControl1.TabPages.Remove(page3);


            CenterToParent();
        }
    }
}
