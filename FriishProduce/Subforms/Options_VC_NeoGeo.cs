using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Options_VC_NeoGeo : ContentOptions
    {
        public bool IsCD { get; set; }
        private string BIOSPath { get; set; }

        public Options_VC_NeoGeo()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>
            {
                { "BIOS", Default.Default_NeoGeo_BIOS },
                { "BIOSPath", null }
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
                if (Settings["BIOSPath"] != null || File.Exists(Settings["BIOSPath"]))
                {
                    Settings["BIOS"] = "custom";
                    BIOSPath = Settings["BIOSPath"];
                }

                else if (Settings["BIOS"] == "custom") Settings["BIOS"] = Default.Default_NeoGeo_BIOS;

                comboBox1.SelectedIndex = GetBIOSIndex(Settings["BIOS"]);
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Settings["BIOSPath"] = BIOSPath;
            Settings["BIOS"] = Settings["BIOSPath"] != null || File.Exists(Settings["BIOSPath"]) ? "custom" : GetBIOSName(comboBox1.SelectedIndex);
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
            if (GetBIOSName(comboBox1.SelectedIndex) == "custom")
            {
                if (BIOSPath == null)
                {
                    if (ImportBIOS.ShowDialog() == DialogResult.OK)
                        BIOSPath = ImportBIOS.FileName;

                    else comboBox1.SelectedIndex = GetBIOSIndex(Settings["BIOS"] == "custom" ? Default.Default_NeoGeo_BIOS : Settings["BIOS"]);
                }
            }

            else if (comboBox1.SelectedIndex > 0) BIOSPath = null;
        }
    }
}
