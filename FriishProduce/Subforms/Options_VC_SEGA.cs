using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_VC_SEGA : ContentOptions
    {
        public Options_VC_SEGA() : base()
        {
            ResetOptions();
            InitializeComponent();

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                ResetOptions(false);
                Language.AutoSetForm(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions(bool NoDesign = true)
        {
            if (Settings == null || Settings.Count == 0)
            {
                Settings = new SortedDictionary<string, string>
                {
                    { "console.brightness", "68" },
                   // { "console.disable_resetbutton", "1" },
                   // { "console.volume", "+6.0" },
                   // { "country", "us" },
                   // { "dev.mdpad.enable_6b", "1" },
                   // { "save_sram", "0" },
                };
            }

            // Form control
            // *******
            if (!NoDesign)
            {
                BrightnessValue.Value = int.Parse(Settings["console.brightness"]);

                dataGridView1.Columns.Add("Setting", "Setting");
                dataGridView1.Columns.Add("Value", "Value");
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[0].Width = dataGridView1.Columns[0].MinimumWidth = 175;
                dataGridView1.Columns[1].Width = dataGridView1.Columns[1].MinimumWidth = 125;
                foreach (KeyValuePair<string, string> item in Settings)
                {
                    dataGridView1.Rows.Add(item.Key, item.Value);
                }
            }
        }

        protected override bool SaveOptions()
        {
            /*Settings.Clear();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Settings.Add(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString());
            } */

            Settings["console.brightness"] = BrightnessValue.Value.ToString();
            return true;
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void BrightnessValue_Set(object sender, EventArgs e)
        {

        }
    }
}
