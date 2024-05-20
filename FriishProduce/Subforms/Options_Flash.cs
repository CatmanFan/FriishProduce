using System;
using System.Collections.Generic;
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
                Program.Lang.Control(this);
                groupBox1.Text = Program.Lang.String("save_data", "projectform");
                save_data_enable.Text = Program.Lang.String("save_data_enable", "projectform");
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
                save_data_enable.Checked = Options["shared_object_capability"] == "on";
                vff_sync_on_write.Checked = Options["vff_sync_on_write"] == "on";
                vff_cache_size_list.SelectedItem = vff_cache_size_list.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Options["vff_cache_size"]);
                mouse.Checked = Options["mouse"] == "on";
                qwerty_keyboard.Checked = Options["qwerty_keyboard"] == "on";
                quality_list.SelectedIndex = Options["quality"] switch { "high" => 0, "medium" => 1, _ => 2 };
                strap_reminder_list.SelectedIndex = Options["strap_reminder"] switch { "none" => 0, "normal" => 1, _ => 2 };
            }

            vff_cache_size.Enabled = vff_cache_size_list.Enabled = vff_sync_on_write.Enabled = save_data_enable.Checked;
            // *******
        }

        protected override void SaveOptions()
        {
            // Code logic in derived Form
            Options["shared_object_capability"] = save_data_enable.Checked ? "on" : "off";
            Options["vff_sync_on_write"] = vff_sync_on_write.Checked ? "on" : "off";
            Options["vff_cache_size"] = vff_cache_size_list.SelectedItem.ToString();
            Options["mouse"] = mouse.Checked ? "on" : "off";
            Options["qwerty_keyboard"] = qwerty_keyboard.Checked ? "on" : "off";
            Options["quality"] = quality_list.SelectedIndex switch { 0 => "high", 1 => "medium", _ => "low" };
            Options["strap_reminder"] = strap_reminder_list.SelectedIndex switch { 0 => "none", 1 => "normal", _ => "no_ex" };
            Options["hbm_no_save"] = Options["shared_object_capability"] == "on" ? "no" : "yes";
        }

        private void checkBoxChanged(object sender, EventArgs e)
        {
            if (sender == save_data_enable) vff_cache_size.Enabled = vff_cache_size_list.Enabled = vff_sync_on_write.Enabled = save_data_enable.Checked;
        }
    }
}
