using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FriishProduce.Options;

namespace FriishProduce
{
    public partial class Options_VC_NEO : ContentOptions
    {
        public bool IsCD { get; set; }
        private string BIOSPath { get; set; }

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
                if (Options["BIOSPath"] != null || File.Exists(Options["BIOSPath"]))
                {
                    Options["BIOS"] = "custom";
                    BIOSPath = Options["BIOSPath"];
                }

                else if (Options["BIOS"] == "custom") Options["BIOS"] = VC_NEO.Default.bios;

                bios_list.SelectedIndex = GetBIOSIndex(Options["BIOS"]);
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["BIOSPath"] = BIOSPath;
            Options["BIOS"] = Options["BIOSPath"] != null || File.Exists(Options["BIOSPath"]) ? "custom" : GetBIOSName(bios_list.SelectedIndex);
        }

        // ---------------------------------------------------------------------------------------------------------------

        private string GetBIOSName(int i)
        {
            switch (i)
            {
                default:
                    return "";
                case 0:
                    return "custom";
                case 1:
                    return "VC1";
                case 2:
                    return "VC2";
                case 3:
                    return "VC3";
            }
        }

        private int GetBIOSIndex(string name)
        {
            switch (name.ToLower())
            {
                default:
                    return -1;
                case "custom":
                    return 0;
                case "vc1":
                    return 1;
                case "vc2":
                    return 2;
                case "vc3":
                    return 3;
            }
        }

        private void BIOSChanged(object sender, EventArgs e)
        {
            if (GetBIOSName(bios_list.SelectedIndex) == "custom")
            {
                if (BIOSPath == null)
                {
                    if (ImportBIOS.ShowDialog() == DialogResult.OK)
                        BIOSPath = ImportBIOS.FileName;

                    else bios_list.SelectedIndex = GetBIOSIndex(Options["BIOS"] == "custom" ? VC_NEO.Default.bios : Options["BIOS"]);
                }
            }

            else if (bios_list.SelectedIndex > 0) BIOSPath = null;
        }
    }
}
