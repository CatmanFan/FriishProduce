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
                OK.Click += OK_Click;
                Cancel.Click += Cancel_Click;
                Load += Form_Load;

                // Language.Localize(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Settings != null)
            {
                // Code logic in derived Form
            }
            // *******
        }

        protected override void SaveOptions()
        {
            // Code logic in derived Form
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
