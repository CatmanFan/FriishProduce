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
        public ControllerMapping()
        {
            InitializeComponent();

            // Remove this code when creating a new copy
            // *****************************************
            if (!DesignMode)
            {
                // Cosmetic
                // ---------------
                b_ok.Click += OK_Click;
                b_cancel.Click += Cancel_Click;
                Load += Form_Load;
            }
            // *****************************************
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            foreach (Buttons orig in Mapping.Keys.ToList())
            {
                ComboBox target = Controls.Find(orig.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                if (target != null)
                    Mapping[orig] = target.SelectedIndex == 0 ? "" : available.Local_Formatted[target.SelectedIndex - 1];
            }

            OldMapping = new Dictionary<Buttons, string>(Mapping);
            DialogResult = DialogResult.OK;
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // Map button values to previous settings
            // ---------------
            Mapping = new Dictionary<Buttons, string>(OldMapping);
            DialogResult = DialogResult.Cancel;
        }

        protected void Form_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // Map button values to null
            // ---------------
            if (Mapping == null)
            {
                const string empty = "—";

                foreach (ComboBox c in page1.Controls.OfType<ComboBox>())
                {
                    c.Items.Add(empty);
                    c.Items.AddRange(available.Local_Displayed);
                    c.SelectedIndex = 0;
                }

                foreach (ComboBox c in page2.Controls.OfType<ComboBox>())
                {
                    c.Items.Add(empty);
                    c.Items.AddRange(available.Local_Displayed);
                    c.SelectedIndex = 0;
                }

                foreach (ComboBox c in page3.Controls.OfType<ComboBox>())
                {
                    c.Items.Add(empty);
                    c.Items.AddRange(available.Local_Displayed);
                    c.SelectedIndex = 0;
                }

                Mapping = new Dictionary<Buttons, string>();
                foreach (Buttons orig in available.Wii) Mapping.Add(orig, "");
                OldMapping = new Dictionary<Buttons, string>(Mapping);

                foreach (Buttons orig in Mapping.Keys.ToList())
                {
                    ComboBox target = Controls.Find(orig.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                    if (target != null) target.Enabled = true;
                }
            }
            else
            {
                foreach (Buttons orig in Mapping.Keys.ToList())
                {
                    int index = string.IsNullOrWhiteSpace(Mapping[orig]) ? 0 : Array.IndexOf(available.Local_Formatted, Mapping[orig]) + 1;

                    ComboBox target = Controls.Find(orig.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                    if (target != null)
                    {
                        target.Enabled = true;
                        target.SelectedIndex = index;
                    }
                }

                if (OldMapping == null) OldMapping = new Dictionary<Buttons, string>(Mapping);
            }

            if (!UsesGC && tabControl1.TabPages.Contains(page3)) tabControl1.TabPages.Remove(page3);
            CenterToParent();
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected struct Available
        {
            public Buttons[] Wii;
            public string[] Local_Displayed;
            public string[] Local_Formatted;
        }

        public IDictionary<Buttons, string> Mapping;
        protected IDictionary<Buttons, string> OldMapping;

        protected bool UsesGC { get; set; }
        protected bool UsesNunchuk { get; set; }

        // ---------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------------------------------------------
        // /!\ MODIFIABLE FUNCTIONS AND VALUES NEEDED! /!\
        // ---------------------------------------------------------------------------------------------------------------

        protected Available available = new()
        {
            Wii = new Buttons[]
            {
                Buttons.WiiRemote_Up,
                Buttons.WiiRemote_Left,
                Buttons.WiiRemote_Right,
                Buttons.WiiRemote_Down,
                Buttons.WiiRemote_1,
                Buttons.WiiRemote_2,
                Buttons.WiiRemote_Plus,
                Buttons.WiiRemote_Minus,
                Buttons.WiiRemote_A,
                Buttons.WiiRemote_B,

                Buttons.Classic_Up,
                Buttons.Classic_Left,
                Buttons.Classic_Right,
                Buttons.Classic_Down,
                Buttons.Classic_A,
                Buttons.Classic_B,
                Buttons.Classic_X,
                Buttons.Classic_Y,
                Buttons.Classic_Plus,
                Buttons.Classic_Minus,
                Buttons.Classic_L,
                Buttons.Classic_R,
                Buttons.Classic_ZL,
                Buttons.Classic_ZR,

                Buttons.GC_Up,
                Buttons.GC_Right,
                Buttons.GC_Left,
                Buttons.GC_Down,
                Buttons.GC_A,
                Buttons.GC_B,
                Buttons.GC_X,
                Buttons.GC_Y,
                Buttons.GC_Start,
                Buttons.GC_L,
                Buttons.GC_R,
                Buttons.GC_Z
            },

            Local_Displayed = new string[]
            {    
                // Buttons used in the target console, as displayed on the form
                // ************************************************************
                "null"
            },

            Local_Formatted = new string[]
            {
                // Buttons used in the target console, as formatted in configuration file on WAD
                // *****************************************************************************
                "null"
            }
        };
    }
}
