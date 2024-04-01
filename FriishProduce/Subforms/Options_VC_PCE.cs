using System;
using System.Collections.Generic;
using FriishProduce.Options;

namespace FriishProduce
{
    public partial class Options_VC_PCE : ContentOptions
    {
        public bool IsCD { get; set; }

        public Options_VC_PCE()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "BACKUPRAM", VC_PCE.Default.BACKUPRAM },
                { "PADBUTTON", VC_PCE.Default.PADBUTTON },
                { "CHASEHQ", "0" },
                { "EUROPE", VC_PCE.Default.EUROPE },
                { "SGENABLE", VC_PCE.Default.SGENABLE },
                { "HIDEOVERSCAN", VC_PCE.Default.HIDEOVERSCAN },
                { "YOFFSET", VC_PCE.Default.YOFFSET },
                { "MULTITAP", "1" },
                { "HDS", "0" },
                { "RASTER", VC_PCE.Default.RASTER },
                { "POPULUS", "0" },
                { "SPRLINE", VC_PCE.Default.SPRLINE },
                { "NOFPA", "1" },
                { "PAD5", "1" },
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                ToggleSwitchText();
                checkBox4.Text = Program.Lang.String("save_data_enable", "projectform");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                numericUpDown1.Value = int.Parse(Options["YOFFSET"]);
                toggleSwitch1.Checked = Options["EUROPE"] == "1";
                toggleSwitch2.Checked = Options["SGENABLE"] == "1";
                toggleSwitch3.Checked = Options["PADBUTTON"] == "6";
                checkBox1.Checked = Options["HIDEOVERSCAN"] == "1";
                checkBox2.Checked = Options["RASTER"] == "1";
                checkBox3.Checked = Options["SPRLINE"] == "1";
                checkBox4.Checked = Options["BACKUPRAM"] == "1";
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["YOFFSET"] = numericUpDown1.Value.ToString();
            Options["EUROPE"] = toggleSwitch1.Checked ? "1" : "0";
            Options["SGENABLE"] = toggleSwitch2.Checked ? "1" : "0";
            Options["PADBUTTON"] = toggleSwitch3.Checked ? "6" : "2";
            Options["HIDEOVERSCAN"] = checkBox1.Checked ? "1" : "0";
            Options["RASTER"] = checkBox2.Checked ? "1" : "0";
            Options["SPRLINE"] = checkBox3.Checked ? "1" : "0";
            Options["BACKUPRAM"] = checkBox4.Checked ? "1" : "0";
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            ToggleSwitchText();
        }

        private void ToggleSwitchText()
        {
            toggleSwitchL1.Text = Program.Lang.Toggle(toggleSwitch1.Checked, "europe", "vc_pce");
            toggleSwitchL2.Text = Program.Lang.Toggle(toggleSwitch2.Checked, "sgenable", "vc_pce");
            toggleSwitchL3.Text = Program.Lang.Toggle(toggleSwitch3.Checked, "padbutton", "vc_pce");
        }
    }
}
