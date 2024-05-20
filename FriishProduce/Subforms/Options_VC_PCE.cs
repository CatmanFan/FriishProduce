using FriishProduce.Options;
using System;
using System.Collections.Generic;

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
                Program.Lang.Control(this);
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
                y_offset_toggle.Value               = int.Parse(Options["YOFFSET"]);
                europe_switch.Checked               = Options["EUROPE"] == "1";
                sgenable_switch.Checked             = Options["SGENABLE"] == "1";
                padbutton_switch.Checked            = Options["PADBUTTON"] == "6";
                hide_overscan.Checked               = Options["HIDEOVERSCAN"] == "1";
                raster.Checked                      = Options["RASTER"] == "1";
                sprline.Checked                     = Options["SPRLINE"] == "1";
                checkBox4.Checked                   = Options["BACKUPRAM"] == "1";
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["YOFFSET"]                      = y_offset_toggle.Value.ToString();
            Options["EUROPE"]                       = europe_switch.Checked ? "1" : "0";
            Options["SGENABLE"]                     = sgenable_switch.Checked ? "1" : "0";
            Options["PADBUTTON"]                    = padbutton_switch.Checked ? "6" : "2";
            Options["HIDEOVERSCAN"]                 = hide_overscan.Checked ? "1" : "0";
            Options["RASTER"]                       = raster.Checked ? "1" : "0";
            Options["SPRLINE"]                      = sprline.Checked ? "1" : "0";
            Options["BACKUPRAM"]                    = checkBox4.Checked ? "1" : "0";
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            ToggleSwitchText();
        }

        private void ToggleSwitchText()
        {
            europe.Text = Program.Lang.Toggle(europe_switch.Checked, europe.Name, Tag.ToString());
            sgenable.Text = Program.Lang.Toggle(sgenable_switch.Checked, sgenable.Name, Tag.ToString());
            padbutton.Text = Program.Lang.Toggle(padbutton_switch.Checked, padbutton.Name, Tag.ToString());
        }
    }
}
