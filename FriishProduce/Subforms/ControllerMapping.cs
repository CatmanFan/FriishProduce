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
            Font = Program.MainForm.Font;
        }

        protected void ResetLayout()
        {
            if (DesignMode) return;

            // Clean and reload combo boxes
            // ********
            const string empty = "—";

            foreach (ComboBox c in page1.Controls.OfType<ComboBox>())
            {
                c.Enabled = false;
                c.Items.Clear();
                c.Items.Add(empty);
                c.Items.AddRange(available.Local_Displayed);
                c.SelectedIndex = 0;
            }

            foreach (ComboBox c in page2.Controls.OfType<ComboBox>())
            {
                c.Enabled = false;
                c.Items.Clear();
                c.Items.Add(empty);
                c.Items.AddRange(available.Local_Displayed);
                c.SelectedIndex = 0;
            }

            foreach (ComboBox c in page3.Controls.OfType<ComboBox>())
            {
                c.Enabled = false;
                c.Items.Clear();
                c.Items.Add(empty);
                c.Items.AddRange(available.Local_Displayed);
                c.SelectedIndex = 0;
            }

            // Reset mapping layout and enable available buttons
            // ********
            Mapping = new Dictionary<Buttons, string>();
            foreach (Buttons orig in available.Wii) Mapping.Add(orig, "");
            OldMapping = new Dictionary<Buttons, string>(Mapping);

            foreach (Buttons orig in Mapping.Keys.ToList())
            {
                ComboBox target = Controls.Find(orig.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                if (target != null) target.Enabled = true;
            }

            // -----------------------------------------------------------------------------------------------------------

            presets_list.Items.Clear();
            presets_list.Items.Add(Program.Lang.String("preset_blank", "controller"));
            presets_list.Items.AddRange(presets.Keys.ToArray());
            if (presets_list.Items?.Count > 0)
            {
                preset_load.Enabled = presets_list.Enabled = true;
            }
            else
            {
                presets_list.Items.Add(empty);
                preset_load.Enabled = presets_list.Enabled = false;
            }
            presets_list.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets all current buttons to a preset or the current mapped values.
        /// </summary>
        /// <param name="input">A typical dictionary containing the lists of Wii buttons and their mapped values.</param>
        protected void SetKeymap(IDictionary<Buttons, string> input)
        {
            foreach (Buttons orig in input.Keys.ToList())
            {
                int index = string.IsNullOrWhiteSpace(input[orig]) || !available.Local_Formatted.Contains(input[orig]) ? 0 : Array.IndexOf(available.Local_Formatted, input[orig]) + 1;

                ComboBox target = Controls.Find(orig.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                if (target != null)
                {
                    target.Enabled = true;
                    try { target.SelectedIndex = index; } catch { }
                }
            }
        }

        /// <summary>
        /// Sets all current buttons to a preset or the current mapped values.
        /// </summary>
        /// <param name="input">A string array containing the internal names of mapped buttons.</param>
        protected void SetKeymap(string[] input)
        {
            for (int i = 0; i < Math.Min(available.Wii.Length, input.Length); i++)
            {
                int index = string.IsNullOrWhiteSpace(input[i]) || !available.Local_Formatted.Contains(input[i]) ? 0 : Array.IndexOf(available.Local_Formatted, input[i]) + 1;

                ComboBox target = Controls.Find(available.Wii[i].ToString(), true).OfType<ComboBox>().FirstOrDefault();
                if (target != null)
                {
                    target.Enabled = true;
                    try { target.SelectedIndex = index; } catch { }
                }
            }
        }

        protected void SetKeymap(string name)
        {
            if (presets?.ContainsKey(name) == true) SetKeymap(presets[name]);
            else if (presets_list.SelectedIndex == 0) SetKeymap(new string[available.Wii.Length]);
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
                ResetLayout();
            }
            else
            {
                SetKeymap(Mapping.Values.ToArray());

                if (OldMapping == null) OldMapping = new Dictionary<Buttons, string>(Mapping);
            }

            if (!UsesGC && tabControl1.TabPages.Contains(page3)) tabControl1.TabPages.Remove(page3);
            if (!UsesNunchuk) vertical_layout.Checked = false;
            vertical_layout.Enabled = vertical_layout.Visible = UsesNunchuk;
            CenterToParent();
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected struct Available
        {
            public Buttons[] Wii;
            public string[] Wii_Formatted;
            public string[] Local_Displayed;
            public string[] Local_Formatted;
        }

        public IDictionary<Buttons, string> Keymap
        {
            get
            {
                try
                {
                    Mapping = new Dictionary<Buttons, string>();
                    foreach (Buttons orig in available.Wii) Mapping.Add(orig, "");

                    foreach (Buttons orig in Mapping.Keys.ToList())
                    {
                        ComboBox target = Controls.Find(orig.ToString(), true).OfType<ComboBox>().FirstOrDefault();
                        if (target != null)
                            Mapping[orig] = target.SelectedIndex == 0 ? "" : available.Local_Formatted[target.SelectedIndex - 1];
                    }

                    return Mapping;
                }
                catch
                {
                    return null;
                }
            }

            set
            {
                if (value != null)
                {
                    Mapping = new Dictionary<Buttons, string>(value);

                    try { SetKeymap(value.Values.ToArray()); } catch { }
                }
                else
                {
                    Mapping = null;

                    try { SetKeymap(new string[available.Wii.Length]); } catch { }
                }
            }
        }
        protected IDictionary<Buttons, string> Mapping;
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

        protected IDictionary<string, string[]> presets { get; set; }

        private void Preset_Click(object sender, EventArgs e)
        {
            if (sender == preset_load) SetKeymap(presets_list.SelectedItem.ToString());
            // else if (sender == preset_clear) SetKeymap(new string[available.Wii.Count()]);
        }
    }
}