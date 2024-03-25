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
                Language.Localize(this);
                toggleSwitchL1.Text = Language.Get(toggleSwitch1, this);
                toggleSwitchL2.Text = Language.Get(toggleSwitch2, this);
                toggleSwitchL3.Text = Language.Get(toggleSwitch3, this);
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
            if (sender == toggleSwitch1) toggleSwitchL1.Text = Language.Get(toggleSwitch1, this);
            if (sender == toggleSwitch2) toggleSwitchL2.Text = Language.Get(toggleSwitch2, this);
            if (sender == toggleSwitch3) toggleSwitchL3.Text = Language.Get(toggleSwitch3, this);
        }
    }
}
