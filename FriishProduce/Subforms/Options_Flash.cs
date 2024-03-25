using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static FriishProduce.Options.ADOBEFLASH;

namespace FriishProduce
{
    public partial class Options_Flash : ContentOptions
    {
        public Options_Flash() : base()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "mouse", Default.mouse },
                { "qwerty_keyboard", Default.qwerty_keyboard },
                { "quality", Default.quality },
                { "shared_object_capability", Default.shared_object_capability },
                { "vff_cache_size", Default.vff_cache_size },
                { "vff_sync_on_write", Default.vff_sync_on_write },
                { "hbm_no_save", Default.hbm_no_save },
                { "strap_reminder", Default.strap_reminder },
                { "midi", Default.midi }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Language.Localize(this);
                groupBox1.Text = Language.Get("SaveData");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                // Code logic in derived Form
                checkBox1.Checked = Options["shared_object_capability"] == "on";
                checkBox2.Checked = Options["vff_sync_on_write"] == "on";
                checkBox3.Checked = Options["mouse"] == "on";
                checkBox4.Checked = Options["qwerty_keyboard"] == "on";
                comboBox1.SelectedIndex = Options["quality"] == "high" ? 0 : Options["quality"] == "medium" ? 1 : 2;
                comboBox2.SelectedItem = comboBox2.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Options["vff_cache_size"]);
            }

            label1.Enabled = comboBox2.Enabled = checkBox2.Enabled = checkBox1.Checked;
            // *******
        }

        protected override void SaveOptions()
        {
            // Code logic in derived Form
            Options["shared_object_capability"] = checkBox1.Checked ? "on" : "off";
            Options["vff_sync_on_write"] = checkBox2.Checked ? "on" : "off";
            Options["mouse"] = checkBox3.Checked ? "on" : "off";
            Options["qwerty_keyboard"] = checkBox4.Checked ? "on" : "off";
            Options["quality"] = comboBox1.SelectedIndex == 0 ? "high" : comboBox1.SelectedIndex == 1 ? "medium" : "low";
            Options["vff_cache_size"] = comboBox2.SelectedItem.ToString();

            Options["hbm_no_save"] = Options["shared_object_capability"] == "on" ? "no" : "yes";
        }

        private void checkBoxChanged(object sender, EventArgs e)
        {
            if (sender == checkBox1) label1.Enabled = comboBox2.Enabled = checkBox2.Enabled = checkBox1.Checked;
        }
    }
}
