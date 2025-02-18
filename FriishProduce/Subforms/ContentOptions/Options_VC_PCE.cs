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
            if (DesignMode) return;

            ClearOptions();

            // Cosmetic
            // *******
            Program.Lang.Control(this);
            ToggleSwitchText();
            backupram.Text = Program.Lang.String("save_data_enable", "projectform");

            tip = HTML.CreateToolTip();

            Theme.ChangeColors(this, false);
            Theme.BtnSizes(b_ok, b_cancel);
            Theme.BtnLayout(this, b_ok, b_cancel);
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ClearOptions()
        {
            Options = new Dictionary<string, string>
            {
                { "BACKUPRAM", Program.Config.pce.BACKUPRAM },
                { "PADBUTTON", Program.Config.pce.PADBUTTON },
                { "CHASEHQ", "0" },
                { "EUROPE", Program.Config.pce.EUROPE },
                { "SGENABLE", Program.Config.pce.SGENABLE },
                { "HIDEOVERSCAN", Program.Config.pce.HIDEOVERSCAN },
                { "YOFFSET", Program.Config.pce.YOFFSET },
                { "MULTITAP", "1" },
                { "HDS", "0" },
                { "RASTER", Program.Config.pce.RASTER },
                { "POPULUS", "0" },
                { "SPRLINE", Program.Config.pce.SPRLINE },
                { "NOFPA", "1" },
                { "PAD5", "0" },
            };
        }

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                y_offset_toggle.Value               = int.Parse(Options["YOFFSET"]);
                region.SelectedIndex                = int.Parse(Options["EUROPE"]);
                sgenable.Checked                    = Options["SGENABLE"] == "1";
                padbutton_switch.Checked            = Options["PADBUTTON"] == "6";
                hide_overscan.Checked               = Options["HIDEOVERSCAN"] == "1";
                raster.Checked                      = Options["RASTER"] == "1";
                sprline.Checked                     = Options["SPRLINE"] == "1";
                backupram.Checked                   = Options["BACKUPRAM"] == "1";
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["YOFFSET"]                      = y_offset_toggle.Value.ToString();
            Options["EUROPE"]                       = region.SelectedIndex.ToString();
            Options["SGENABLE"]                     = sgenable.Checked ? "1" : "0";
            Options["PADBUTTON"]                    = padbutton_switch.Checked ? "6" : "2";
            Options["HIDEOVERSCAN"]                 = hide_overscan.Checked ? "1" : "0";
            Options["RASTER"]                       = raster.Checked ? "1" : "0";
            Options["SPRLINE"]                      = sprline.Checked ? "1" : "0";
            Options["BACKUPRAM"]                    = backupram.Checked ? "1" : "0";
            Options["NOFPA"]                        = "1";
            Options["PAD5"]                         = "0";
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            ToggleSwitchText();
        }

        private void ToggleSwitchText()
        {
            padbutton.Text = Program.Lang.Toggle(padbutton_switch.Checked, padbutton.Name, Tag.ToString());
        }
        #endregion
    }
}
