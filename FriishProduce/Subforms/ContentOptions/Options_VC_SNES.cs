using FriishProduce.Options;
using System;
using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Options_VC_SNES : ContentOptions
    {
        public Options_VC_SNES() : base()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "patch_volume",       VC_SNES.Default.patch_volume },
                { "patch_nodark",       VC_SNES.Default.patch_nodark },
                { "patch_nocc",         VC_SNES.Default.patch_nocc },
                { "patch_nosuspend",    VC_SNES.Default.patch_nosuspend },
                { "patch_nosave",       VC_SNES.Default.patch_nosave },
                { "patch_widescreen",   VC_SNES.Default.patch_widescreen }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
                patch_nosave.Text = Program.Lang.String("save_data_enable", "projectform");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                patch_volume.Checked                = bool.Parse(Options["patch_volume"]);
                patch_nodark.Checked                = bool.Parse(Options["patch_nodark"]);
                patch_nocc.Checked                  = bool.Parse(Options["patch_nocc"]);
                patch_nosuspend.Checked             = bool.Parse(Options["patch_nosuspend"]);
                patch_nosave.Checked                = !bool.Parse(Options["patch_nosave"]);
                patch_widescreen.Checked            = bool.Parse(Options["patch_widescreen"]);
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["patch_volume"] = patch_volume.Checked.ToString();
            Options["patch_nodark"] = patch_nodark.Checked.ToString();
            Options["patch_nocc"] = patch_nocc.Checked.ToString();
            Options["patch_nosuspend"] = patch_nosuspend.Checked.ToString();
            Options["patch_nosave"] = (!patch_nosave.Checked).ToString();
            Options["patch_widescreen"] = patch_widescreen.Checked.ToString();
        }
    }
}
