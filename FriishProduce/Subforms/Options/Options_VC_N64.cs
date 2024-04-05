using System;
using System.Collections.Generic;
using System.Drawing;
using FriishProduce.Options;

namespace FriishProduce
{
    public partial class Options_VC_N64 : ContentOptions
    {
        public Options_VC_N64() : base()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "brightness", VC_N64.Default.patch_fixbrightness },
                { "crash",  VC_N64.Default.patch_fixcrashes },
                { "expansion",  VC_N64.Default.patch_expandedram },
                { "rom_autosize",  VC_N64.Default.patch_autosizerom },
                { "romc",  VC_N64.Default.romc_type }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
                Height = EmuType == 3 ? 320 : 260;
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                patch_autosizerom.Enabled = patch_fixcrashes.Enabled = EmuType <= 1;
                g2.Visible = g2.Enabled = EmuType == 3;
                patch_fixbrightness.Checked = bool.Parse(Options["brightness"]);
                patch_fixcrashes.Checked = bool.Parse(Options["crash"]);
                patch_expandedram.Checked = bool.Parse(Options["expansion"]);
                patch_autosizerom.Checked = bool.Parse(Options["rom_autosize"]);
                romc_type.SelectedIndex = int.Parse(Options["romc"]);
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["brightness"] = patch_fixbrightness.Checked.ToString();
            Options["crash"] = patch_fixcrashes.Checked.ToString();
            Options["expansion"] = patch_expandedram.Checked.ToString();
            Options["rom_autosize"] = patch_autosizerom.Checked.ToString();
            Options["romc"] = romc_type.SelectedIndex.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void Form_IsShown(object sender, EventArgs e)
        {
            Size = g2.Visible ? new Size(475, 280) : new Size(475, 280 - 55);
            CenterToParent();
        }
    }
}
