using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_VC_PCE : ContentOptions
    {
        public bool IsCD { get; set; }

        public Options_VC_PCE()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>
            {
                { "BACKUPRAM", "1" },
                { "PADBUTTON", "2" },
                { "CHASEHQ", "0" },
                { "EUROPE", "0" },
                { "SGENABLE", "0" },
                { "HIDEOVERSCAN", "0" },
                { "YOFFSET", "0" },
                { "MULTITAP", "1" },
                { "HDS", "0" },
                { "RASTER", "0" },
                { "POPULUS", "0" },
                { "SPRLINE", "0" },
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
            if (Settings != null)
            {
                numericUpDown1.Value = int.Parse(Settings["YOFFSET"]);
                toggleSwitch1.Checked = Settings["EUROPE"] == "1";
                toggleSwitch2.Checked = Settings["SGENABLE"] == "1";
                toggleSwitch3.Checked = Settings["PADBUTTON"] == "6";
                checkBox1.Checked = Settings["HIDEOVERSCAN"] == "1";
                checkBox2.Checked = Settings["RASTER"] == "1";
                checkBox3.Checked = Settings["SPRLINE"] == "1";
                checkBox4.Checked = Settings["BACKUPRAM"] == "1";
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Settings["YOFFSET"] = numericUpDown1.Value.ToString();
            Settings["EUROPE"] = toggleSwitch1.Checked ? "1" : "0";
            Settings["SGENABLE"] = toggleSwitch2.Checked ? "1" : "0";
            Settings["PADBUTTON"] = toggleSwitch3.Checked ? "6" : "2";
            Settings["HIDEOVERSCAN"] = checkBox1.Checked ? "1" : "0";
            Settings["RASTER"] = checkBox2.Checked ? "1" : "0";
            Settings["SPRLINE"] = checkBox3.Checked ? "1" : "0";
            Settings["BACKUPRAM"] = checkBox4.Checked ? "1" : "0";
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
