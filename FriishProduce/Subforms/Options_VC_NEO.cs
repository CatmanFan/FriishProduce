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
                { "BIOS", VC_NEO.Default.bios },
                { "BIOSPath", null }
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
                if (!File.Exists(Options["BIOSPath"]) && !string.IsNullOrEmpty(Options["BIOSPath"])) MessageBox.Show(string.Format(Program.Lang.Msg(10, true), Path.GetFileName(Options["BIOSPath"])), MessageBox.Buttons.Ok, MessageBox.Icons.Warning);

                if (Options["BIOSPath"] != null || File.Exists(Options["BIOSPath"]))
                {
                    Options["BIOS"] = "custom";
                    biosPath = Options["BIOSPath"];
                }

                else if (Options["BIOS"] == "custom") Options["BIOS"] = VC_NEO.Default.bios;

                bios_list.SelectedIndex = biosIndex;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["BIOSPath"] = biosPath;
            Options["BIOS"] = Options["BIOSPath"] != null || File.Exists(Options["BIOSPath"]) ? "custom" : biosType;
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Variables
        private string biosPath { get; set; }

        private string biosType
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
                string type = Options["BIOS"] == "custom" ? VC_NEO.Default.bios : Options["BIOS"];
                return type switch
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
            if (biosType == "custom")
            {
                if (biosPath == null)
                {
                    biosImport.Title = bios_list.SelectedItem.ToString();

                    if (biosImport.ShowDialog() == DialogResult.OK)
                        biosPath = biosImport.FileName;

                    else bios_list.SelectedIndex = biosIndex;
                }
            }

            else if (bios_list.SelectedIndex > 0) biosPath = null;
        }
        #endregion
    }
}
