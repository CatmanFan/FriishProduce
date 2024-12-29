using System;
using System.Collections.Generic;
using System.IO;

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
                Platform.PSX => Program.Config.paths.bios_psx,
                Platform.GB => Program.Config.paths.bios_gb,
                Platform.GBC => Program.Config.paths.bios_gbc,
                Platform.GBA => Program.Config.paths.bios_gba,
                _ => null
            };

            Options = new Dictionary<string, string>
            {
                { "use_bios", false.ToString() },
                { "show_bios_screen", Program.Config.forwarder.show_bios_screen.ToString() }
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
                        string.Format(string.Format(Program.Lang.Msg(11, true), Path.GetFileName(bios))),
                        MessageBox.Buttons.Ok,
                        MessageBox.Icons.Information
                    );

                    bios = null;
                    switch (platform)
                    {
                        case Platform.PSX:
                            Program.Config.paths.bios_psx = null;
                            break;
                        case Platform.GB:
                            Program.Config.paths.bios_gb = null;
                            break;
                        case Platform.GBC:
                            Program.Config.paths.bios_gbc = null;
                            break;
                        case Platform.GBA:
                            Program.Config.paths.bios_gba = null;
                            break;
                    }
                    Program.Config.Save();
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
            if (toggleSwitch1.Checked && string.IsNullOrWhiteSpace(bios))
            {
                MessageBox.Show(Program.Lang.Msg(14, true), MessageBox.Buttons.Ok, MessageBox.Icons.Error, false);
                toggleSwitch1.Checked = false;
            }

            show_bios_screen.Enabled = toggleSwitch2.Enabled = toggleSwitch1.Checked;
            if (!toggleSwitch2.Enabled) toggleSwitch2.Checked = false;
        }
        #endregion
    }
}
