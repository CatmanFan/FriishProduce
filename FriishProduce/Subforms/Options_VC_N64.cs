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
                ;

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
                n64003.Enabled = n64001.Enabled = EmuType <= 1;
                n64004.Visible = n64004.Enabled = EmuType == 3;
                n64000.Checked = bool.Parse(Options["brightness"]);
                n64001.Checked = bool.Parse(Options["crash"]);
                n64002.Checked = bool.Parse(Options["expansion"]);
                n64003.Checked = bool.Parse(Options["rom_autosize"]);
                ROMCType.SelectedIndex = int.Parse(Options["romc"]);
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["brightness"] = n64000.Checked.ToString();
            Options["crash"] = n64001.Checked.ToString();
            Options["expansion"] = n64002.Checked.ToString();
            Options["rom_autosize"] = n64003.Checked.ToString();
            Options["romc"] = ROMCType.SelectedIndex.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void Form_IsShown(object sender, EventArgs e)
        {
            Size = n64004.Visible ? new Size(475, 280) : new Size(475, 280 - 55);
            CenterToParent();
        }
    }
}
