using System;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce
{
    public partial class Options_VC_NEO : ContentOptions
    {
        public Options_VC_NEO()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "BIOS", Program.Config.neo.bios }
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
                bool valid = File.Exists(Program.Config.paths.bios_neo);

                // Clear list selection if not found
                if (!valid && biosIndex == 0)
                {
                    MessageBox.Show
                    (
                        string.Format(Program.Lang.Msg(11, true), Path.GetFileName(Program.Config.paths.bios_neo)),
                        MessageBox.Buttons.Ok,
                        MessageBox.Icons.Information
                    );

                    Options["BIOS"] = Program.Config.neo.bios;
                    Program.Config.paths.bios_neo = null;
                    Program.Config.Save();
                }

                bios_list.SelectedIndex = biosIndex;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["BIOS"] = biosName;
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Variables
        private string biosName
        {
            get
            {
                return bios_list.SelectedIndex switch
                {
                    0 => "custom",
                    1 => "VC1",
                    2 => "VC2",
                    3 => "VC3",
                    _ => "",
                };
            }
        }

        private int biosIndex
        {
            get
            {
                return Options["BIOS"] switch
                {
                    "custom" => 0,
                    "VC1" => 1,
                    "VC2" => 2,
                    "VC3" => 3,
                    _ => -1,
                };
            }
        }

        public bool IsCD { get; set; }
        #endregion

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void BIOSChanged(object sender, EventArgs e)
        {
            if (biosName == "custom" && string.IsNullOrWhiteSpace(Program.Config.paths.bios_neo))
            {
                MessageBox.Show(Program.Lang.Msg(14, true), MessageBox.Buttons.Ok, MessageBox.Icons.Error, false);

                // Set list selection back to previous one
                if (biosIndex == 0) Options["BIOS"] = Program.Config.neo.bios;
                bios_list.SelectedIndex = biosIndex;
            }
        }
        #endregion
    }
}
