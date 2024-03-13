using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Options_Flash : ContentOptions
    {
        public Options_Flash() : base()
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
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Settings != null)
            {

            }
            // *******
        }

        protected override void SaveOptions()
        {

        }
    }
}
