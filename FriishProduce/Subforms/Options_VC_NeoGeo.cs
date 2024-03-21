using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_VC_NeoGeo : ContentOptions
    {
        public bool IsCD { get; set; }

        public Options_VC_NeoGeo()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>
            {
                { "BiosPath", null },
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
                checkBox1.Checked = Settings["BiosPath"] != null;
            }
            // *******
        }

        protected override void SaveOptions()
        {
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            if (sender == checkBox1)
            {
                if (checkBox1.Checked)
                {
                    if (ImportBIOS.ShowDialog() == DialogResult.OK)
                        Settings["BiosPath"] = ImportBIOS.FileName;
                    else checkBox1.Checked = false;
                }
                else Settings["BiosPath"] = null;
            }
        }
    }
}
