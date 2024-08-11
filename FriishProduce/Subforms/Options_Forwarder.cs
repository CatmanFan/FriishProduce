using FriishProduce.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_Forwarder : ContentOptions
    {
        private Platform platform { get; set; }
        private string biosPath { get; set; }

        public Options_Forwarder(Platform platform)
        {
            InitializeComponent();
            this.platform = platform;

            Options = new Dictionary<string, string>
            {
                { "BIOSPath", null },
                { "BIOSScreen", FORWARDER.Default.show_bios_screen }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
                ImportBIOS.Filter += Program.Lang.String("filter");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                bool valid = Options["BIOSPath"] != null || File.Exists(Options["BIOSPath"]);

                if (!File.Exists(Options["BIOSPath"]) && !string.IsNullOrEmpty(Options["BIOSPath"]))
                {
                    MessageBox.Show(string.Format(Program.Lang.Msg(10, true), Path.GetFileName(Options["BIOSPath"])));
                    Options["BIOSPath"] = null;
                }

                toggleSwitch1.Checked = valid;
                toggleSwitch2.Checked = bool.Parse(Options["BIOSScreen"]);

                if (valid) biosPath = Options["BIOSPath"];

                show_bios_screen.Enabled = toggleSwitch2.Enabled = valid;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["BIOSPath"] = biosPath;
            Options["BIOSScreen"] = toggleSwitch2.Checked.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void BIOSChanged(object sender, EventArgs e)
        {
            if (toggleSwitch1.Checked)
            {
                if (biosPath == null)
                {
                    ImportBIOS.Title = toggleSwitch1.Text;

                    if (ImportBIOS.ShowDialog() == DialogResult.OK)
                    {
                        if (BIOS.Verify(ImportBIOS.FileName, platform))
                            biosPath = ImportBIOS.FileName;
                        else toggleSwitch1.Checked = false;
                    }
                    else toggleSwitch1.Checked = false;
                }
            }

            if (!toggleSwitch1.Checked) biosPath = null;

            show_bios_screen.Enabled = toggleSwitch2.Enabled = toggleSwitch1.Checked;
        }
        #endregion
    }
}
