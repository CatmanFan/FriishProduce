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
        public Options_VC_SEGA()
        {
            InitializeComponent();

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Language.AutoSetForm(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            if (Settings == null || Settings.Count == 0)
            {
                Settings = new Dictionary<string, string>
                {
                    { "console.brightness", "68" },
                    { "console.machine_country", "us" },
                    { "console.volume", "+6.0" },
                    { "dev.mdpad.enable_6b", "1" },
                    { "save_sram", "0" },
                };
            }

            // Default options
            // *******
            // Code logic in derived Form
            // *******

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

        protected override bool SaveOptions()
        {
            // Code logic in derived Form
            Settings.Clear();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Settings.Add(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString());
            }
            return true;
        }

        // ---------------------------------------------------------------------------------------------------------------
    }
}
