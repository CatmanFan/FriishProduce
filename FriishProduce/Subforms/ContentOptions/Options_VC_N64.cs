using System;
using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Options_VC_N64 : ContentOptions
    {
        public Options_VC_N64() : base()
        {
            InitializeComponent();
            ClearOptions();

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ClearOptions()
        {
            Options = new Dictionary<string, string>
            {
                { "brightness",     Program.Config.n64.patch_nodark.ToString() },
                { "crash",          Program.Config.n64.patch_crashfix.ToString() },
                { "expansion",      Program.Config.n64.patch_expandedram.ToString() },
                { "rom_autosize",   Program.Config.n64.patch_autoromsize.ToString() },
                { "clean_textures", Program.Config.n64.patch_cleantextures.ToString() },
                { "widescreen",     "False" },
                { "romc",           Program.Config.n64.romc_type.ToString() }
            };
        }
        
        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                patch_autosizerom.Enabled = patch_fixcrashes.Enabled = EmuType <= 1;

                patch_fixbrightness.Checked         = bool.Parse(Options["brightness"]);
                patch_fixcrashes.Checked            = bool.Parse(Options["crash"]);
                patch_expandedram.Checked           = bool.Parse(Options["expansion"]);
                patch_autosizerom.Checked           = bool.Parse(Options["rom_autosize"]);
                patch_cleantextures.Checked         = bool.Parse(Options["clean_textures"]);
                // patch_widescreen.Checked            = bool.Parse(Options["widescreen"]);
                romc_type_list.SelectedIndex        = int.Parse(Options["romc"]);
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["brightness"]                   = patch_fixbrightness.Checked.ToString();
            Options["crash"]                        = patch_fixcrashes.Checked.ToString();
            Options["expansion"]                    = patch_expandedram.Checked.ToString();
            Options["rom_autosize"]                 = patch_autosizerom.Checked.ToString();
            Options["clean_textures"]               = patch_cleantextures.Checked.ToString();
            // Options["widescreen"]                   = patch_widescreen.Checked.ToString();
            Options["romc"]                         = romc_type_list.SelectedIndex.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void Form_IsShown(object sender, EventArgs e)
        {
            bool isRomc = EmuType == 3;

            if (!patch_autosizerom.Enabled) patch_autosizerom.Checked = false;
            if (!patch_fixcrashes.Enabled) patch_fixcrashes.Checked = false;

            // g1.Height = patch_autosizerom.Enabled && patch_fixcrashes.Enabled ? 160 : 110;
            g2.Enabled = isRomc;
        }
        #endregion
    }
}
