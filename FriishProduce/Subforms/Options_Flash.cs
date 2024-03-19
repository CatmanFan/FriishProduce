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
                { "mouse", "on" },
                { "qwerty_keyboard", "on" },
                { "quality", "high" },
                { "shared_object_capability", "off" },
                { "vff_cache_size", "96" },
                { "vff_sync_on_write", "off" },
                { "hbm_no_save", "yes" },
                { "strap_reminder", "none" }
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
                // Code logic in derived Form
            }
            // *******
        }

        protected override void SaveOptions()
        {
            // Code logic in derived Form
        }
    }
}
