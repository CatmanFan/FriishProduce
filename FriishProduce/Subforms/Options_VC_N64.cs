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
                Program.AutoSizeControl(romc_type_list, romc_type);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                patch_autosizerom.Enabled           = patch_fixcrashes.Enabled = EmuType <= 1;
                patch_fixbrightness.Checked         = bool.Parse(Options["brightness"]);
                patch_fixcrashes.Checked            = bool.Parse(Options["crash"]);
                patch_expandedram.Checked           = bool.Parse(Options["expansion"]);
                patch_autosizerom.Checked           = bool.Parse(Options["rom_autosize"]);
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
            Options["romc"]                         = romc_type_list.SelectedIndex.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void Form_IsShown(object sender, EventArgs e)
        {
            romc_type_list.Visible = romc_type.Visible = romc_type.Enabled = EmuType == 3;
            g1.Height = romc_type.Visible ? 140 : 110;
            Height = romc_type.Visible ? 250 : 220;
            if (!patch_autosizerom.Enabled) patch_autosizerom.Checked = false;
            if (!patch_fixcrashes.Enabled) patch_fixcrashes.Checked = false;
            CenterToParent();
        }
    }
}
