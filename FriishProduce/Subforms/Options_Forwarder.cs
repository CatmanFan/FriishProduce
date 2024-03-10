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
    public partial class Options_Forwarder : ContentOptions
    {
        public Options_Forwarder()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>
            {
                { "filesStorage", Default.Default_Forwarders_FilesStorage },
                { "mode", Default.Default_Forwarders_Mode }
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
                comboBox1.SelectedItem = Settings["filesStorage"];
                comboBox2.SelectedIndex = Settings["mode"].ToLower() == "wii" ? 0 : 1;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Settings["filesStorage"] = comboBox1.SelectedItem.ToString();
            Settings["mode"] = comboBox2.SelectedIndex == 0 ? "Wii" : "vWii";
        }

        // ---------------------------------------------------------------------------------------------------------------
    }
}
