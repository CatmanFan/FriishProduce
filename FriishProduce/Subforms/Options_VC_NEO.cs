using FriishProduce.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_VC_NEO : ContentOptions
    {
        public Options_VC_NEO()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "BIOS", VC_NEO.Default.bios }
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
                bool valid = File.Exists(FriishProduce.Options.BIOS.Default.neogeo);

                // Clear list selection if not found
                if (!valid && biosIndex == 0)
                {
                    MessageBox.Show
                    (
                        string.Format(Program.Lang.Msg(10, true), Path.GetFileName(FriishProduce.Options.BIOS.Default.neogeo)),
                        MessageBox.Buttons.Ok,
                        MessageBox.Icons.Information
                    );

                    Options["BIOS"] = VC_NEO.Default.bios;
                    FriishProduce.Options.BIOS.Default.neogeo = null;
                    FriishProduce.Options.BIOS.Default.Save();
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
                    "vc1" => 1,
                    "vc2" => 2,
                    "vc3" => 3,
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
            if (biosName == "custom" && FriishProduce.Options.BIOS.Default.neogeo == null)
            {
                MessageBox.Show(Program.Lang.Msg(13, true), MessageBox.Buttons.Ok, MessageBox.Icons.Error, false);

                // Set list selection back to previous one
                if (biosIndex == 0) Options["BIOS"] = VC_NEO.Default.bios;
                bios_list.SelectedIndex = biosIndex;
            }
        }
        #endregion
    }
}
