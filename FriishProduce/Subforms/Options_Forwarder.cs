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
        private string bios { get; set; }

        public Options_Forwarder(Platform platform)
        {
            InitializeComponent();
            this.platform = platform;
            bios = platform switch
            {
                Platform.PSX => FriishProduce.Options.BIOS.Default.psx,
                Platform.GBA => FriishProduce.Options.BIOS.Default.gba,
                _ => null
            };

            Options = new Dictionary<string, string>
            {
                { "use_bios", false.ToString() },
                { "show_bios_screen", FORWARDER.Default.show_bios_screen }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                bool valid = File.Exists(bios);

                // Clear BIOS file option if not found
                if (!valid && !string.IsNullOrEmpty(bios))
                {
                    MessageBox.Show
                    (
                        string.Format(string.Format(Program.Lang.Msg(10, true), Path.GetFileName(bios))),
                        MessageBox.Buttons.Ok,
                        MessageBox.Icons.Information
                    );

                    bios = null;
                    switch (platform)
                    {
                        case Platform.PSX:
                            FriishProduce.Options.BIOS.Default.psx = null;
                            break;
                        case Platform.GBA:
                            FriishProduce.Options.BIOS.Default.gba = null;
                            break;
                    }
                    FriishProduce.Options.BIOS.Default.Save();
                    Options["show_bios_screen"] = Options["use_bios"] = false.ToString();
                }

                toggleSwitch1.Checked = bool.Parse(Options["use_bios"]);
                toggleSwitch2.Checked = bool.Parse(Options["show_bios_screen"]);

                show_bios_screen.Enabled = toggleSwitch2.Enabled = valid;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["use_bios"] = toggleSwitch1.Checked.ToString();
            Options["show_bios_screen"] = toggleSwitch2.Checked.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void BIOSChanged(object sender, EventArgs e)
        {
            if (toggleSwitch1.Checked && bios == null)
            {
                MessageBox.Show(Program.Lang.Msg(13, true), MessageBox.Buttons.Ok, MessageBox.Icons.Error, false);
                toggleSwitch1.Checked = false;
            }

            show_bios_screen.Enabled = toggleSwitch2.Enabled = toggleSwitch1.Checked;
            if (!toggleSwitch2.Enabled) toggleSwitch2.Checked = false;
        }
        #endregion
    }
}
