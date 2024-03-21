using System;
using System.Collections.Generic;
using System.Drawing;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Options_VC_N64 : ContentOptions
    {
        public Options_VC_N64() : base()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>
            {
                { "brightness", Default.Default_N64_FixBrightness.ToString() },
                { "crash",  Default.Default_N64_FixCrashes.ToString() },
                { "expansion",  Default.Default_N64_ExtendedRAM.ToString() },
                { "rom_autosize",  Default.Default_N64_AllocateROM.ToString() },
                { "romc_0",  Default.Default_N64_ROMC0.ToString() }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Language.Localize(this);

                Height = EmuType == 3 ? 320 : 260;
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Settings != null)
            {
                n64003.Enabled = n64001.Enabled = EmuType <= 1;
                n64004.Visible = n64004.Enabled = EmuType == 3;
                n64000.Checked = bool.Parse(Settings["brightness"]);
                n64001.Checked = bool.Parse(Settings["crash"]);
                n64002.Checked = bool.Parse(Settings["expansion"]);
                n64003.Checked = bool.Parse(Settings["rom_autosize"]);
                ROMCType.SelectedIndex = bool.Parse(Settings["romc_0"]) ? 0 : 1;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Settings["brightness"] = n64000.Checked.ToString();
            Settings["crash"] = n64001.Checked.ToString();
            Settings["expansion"] = n64002.Checked.ToString();
            Settings["rom_autosize"] = n64003.Checked.ToString();
            Settings["romc_0"] = (ROMCType.SelectedIndex == 0).ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void Form_IsShown(object sender, EventArgs e)
        {
            Size = n64004.Visible ? new Size(475, 280) : new Size(475, 280 - 55);
            CenterToParent();
        }
    }
}
