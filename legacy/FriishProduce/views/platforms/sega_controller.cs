using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce.Views
{
    public partial class SEGA_Controller : Form
    {
        internal readonly string[] WiiBtns =
        {
            "cl_up",
            "cl_down",
            "cl_left",
            "cl_right",
            "cl_+",
            "cl_-",
            "cl_y",
            "cl_b",
            "cl_a",
            "cl_l",
            "cl_x",
            "cl_r",
            "cl_zr",
            "cl_zl",
            "core_up",
            "core_down",
            "core_left",
            "core_right",
            "core_+",
            "core_a",
            "core_1",
            "core_2",
            "core_b",
            "gc_up",
            "gc_down",
            "gc_left",
            "gc_right",
            "gc_start",
            "gc_b",
            "gc_a",
            "gc_x",
            "gc_l",
            "gc_y",
            "gc_r",
            "gc_z",
            "gc_c"
        };
        internal string[] SrcBtns
        {
            get
            {
                List<string> List = new List<string>
                {
                    "—",
                    "U",
                    "D",
                    "L",
                    "R",
                    "Start",
                    "A",
                    "B",
                    "C",
                    "X",
                    "Y",
                    "Z",
                    "Mode"
                };
                return List.ToArray();
            }
        }
        internal Dictionary<string, string> Config { get; set; }

        public SEGA_Controller(Dictionary<string, string> srcConfig)
        {
            InitializeComponent();
            Program.Language.Localize(this);
            Config = srcConfig ?? new Dictionary<string, string>();
        }

        private void Page_Load(object sender, EventArgs e)
        {
            foreach (ComboBox c in WiiRemote.Controls.OfType<ComboBox>())
            {
                c.Items.AddRange(SrcBtns);
                c.SelectedIndex = 0;

                foreach (KeyValuePair<string, string> item in Config)
                    if (c.Tag.ToString() == item.Key)
                        c.SelectedItem = item.Value;
            }
            foreach (ComboBox c in ClassicController.Controls.OfType<ComboBox>())
            {
                c.Items.AddRange(SrcBtns);
                c.SelectedIndex = 0;

                foreach (KeyValuePair<string, string> item in Config)
                    if (c.Tag.ToString() == item.Key)
                        c.SelectedItem = item.Value;
            }
            foreach (ComboBox c in GameCube.Controls.OfType<ComboBox>())
            {
                c.Items.AddRange(SrcBtns);
                c.SelectedIndex = 0;

                foreach (KeyValuePair<string, string> item in Config)
                    if (c.Tag.ToString() == item.Key)
                        c.SelectedItem = item.Value;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Config = new Dictionary<string, string>();

            foreach (var button in WiiRemote.Controls.OfType<ComboBox>())
            {
                foreach (string btnName in WiiBtns)
                {
                    if (button.Tag.ToString() == btnName && button.SelectedIndex > 0)
                        Config.Add(button.Tag.ToString(), SrcBtns[button.SelectedIndex]);
                }
            }

            foreach (var button in ClassicController.Controls.OfType<ComboBox>())
            {
                foreach (string btnName in WiiBtns)
                {
                    if (button.Tag.ToString() == btnName && button.SelectedIndex > 0)
                        Config.Add(button.Tag.ToString(), SrcBtns[button.SelectedIndex]);
                }
            }

            foreach (var button in GameCube.Controls.OfType<ComboBox>())
            {
                foreach (string btnName in WiiBtns)
                {
                    if (button.Tag.ToString() == btnName && button.SelectedIndex > 0)
                        Config.Add(button.Tag.ToString(), SrcBtns[button.SelectedIndex]);
                }
            }

            DialogResult = DialogResult.OK;
        }
    }
}
