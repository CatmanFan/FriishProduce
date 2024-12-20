﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static FriishProduce.Options.ADOBEFLASH;

namespace FriishProduce
{
    public partial class Options_Flash : ContentOptions
    {
        private string DLSPath { get; set; }

        public Options_Flash() : base()
        {
            InitializeComponent();
            controllerForm = new Controller_Flash();

            Options = new Dictionary<string, string>
            {
                { "update_frame_rate", Default.update_frame_rate },
                { "mouse", Default.mouse },
                { "qwerty_keyboard", Default.qwerty_keyboard },
                { "quality", Default.quality },
                { "shared_object_capability", Default.shared_object_capability },
                { "vff_sync_on_write", Default.vff_sync_on_write },
                { "vff_cache_size", Default.vff_cache_size },
                { "persistent_storage_total", Default.persistent_storage_total },
                { "persistent_storage_per_movie", Default.persistent_storage_per_movie },
                { "hbm_no_save", Default.hbm_no_save },
                { "strap_reminder", Default.strap_reminder },
                { "midi", null }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
                groupBox1.Text = Program.Lang.String("save_data", "projectform");
                save_data_enable.Text = Program.Lang.String("save_data_enable", "projectform");

                controller_cb.Text = Program.Lang.String("controller", "projectform");
                b_controller.Text = Program.Lang.String("controller_mapping", "projectform");
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                Refill:
                try
                {
                    // Code logic in derived Form
                    update_frame_rate.Value = int.Parse(Options["update_frame_rate"]);
                    save_data_enable.Checked = Options["shared_object_capability"] == "on";
                    // vff_sync_on_write.Checked = Options["vff_sync_on_write"] == "on";
                    vff_cache_size.SelectedItem = vff_cache_size.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Options["vff_cache_size"]);
                    persistent_storage_total.SelectedItem = persistent_storage_total.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Options["persistent_storage_total"]);
                    persistent_storage_per_movie.SelectedItem = persistent_storage_per_movie.Items.Cast<string>().FirstOrDefault(n => n.ToString() == Options["persistent_storage_per_movie"]);
                    mouse.Checked = Options["mouse"] == "on";
                    qwerty_keyboard.Checked = Options["qwerty_keyboard"] == "on";
                    quality.SelectedIndex = Options["quality"] switch { "high" => 0, "medium" => 1, _ => 2 };
                    strap_reminder_list.SelectedIndex = Options["strap_reminder"] switch { "none" => 0, "normal" => 1, _ => 2 };
                }
                catch
                {
                    Options = new Dictionary<string, string>
                    {
                        { "update_frame_rate", Default.update_frame_rate },
                        { "mouse", Default.mouse },
                        { "qwerty_keyboard", Default.qwerty_keyboard },
                        { "quality", Default.quality },
                        { "shared_object_capability", Default.shared_object_capability },
                        { "vff_sync_on_write", Default.vff_sync_on_write },
                        { "vff_cache_size", Default.vff_cache_size },
                        { "persistent_storage_total", Default.persistent_storage_total },
                        { "persistent_storage_per_movie", Default.persistent_storage_per_movie },
                        { "hbm_no_save", Default.hbm_no_save },
                        { "strap_reminder", Default.strap_reminder },
                        { "midi", null }
                    };

                    goto Refill;
                }
            }

            toggleSave();

            if (File.Exists(Options["midi"]))
            {
                DLSPath = Options["midi"];
                midi.Checked = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(Options["midi"])) MessageBox.Show(string.Format(Program.Lang.Msg(10, true), Path.GetFileName(Options["midi"])));
                midi.Checked = false;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            // Code logic in derived Form
            Options["update_frame_rate"] = update_frame_rate.Value.ToString();
            Options["shared_object_capability"] = save_data_enable.Checked ? "on" : "off";
            Options["vff_sync_on_write"] = /* vff_sync_on_write.Checked */ Options["shared_object_capability"] == "on" ? "on" : "off";
            Options["vff_cache_size"] = vff_cache_size.SelectedItem.ToString();
            Options["persistent_storage_total"] = persistent_storage_total.SelectedItem.ToString();
            Options["persistent_storage_per_movie"] = persistent_storage_per_movie.SelectedItem.ToString();
            Options["mouse"] = mouse.Checked ? "on" : "off";
            Options["qwerty_keyboard"] = qwerty_keyboard.Checked ? "on" : "off";
            Options["midi"] = DLSPath;
            Options["quality"] = quality.SelectedIndex switch { 0 => "high", 1 => "medium", _ => "low" };
            Options["strap_reminder"] = strap_reminder_list.SelectedIndex switch { 0 => "none", 1 => "normal", _ => "no_ex" };
            Options["hbm_no_save"] = Options["shared_object_capability"] == "on" ? "no" : "yes";
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void toggleSave()
        {
            // vff_sync_on_write.Enabled = save_data_enable.Checked;
            vff_cache_size_l.Enabled = vff_cache_size.Enabled = save_data_enable.Checked;
            persistent_storage_total_l.Enabled = persistent_storage_total.Enabled = save_data_enable.Checked;
            persistent_storage_per_movie_l.Enabled = persistent_storage_per_movie.Enabled = save_data_enable.Checked;
        }

        private void checkBoxChanged(object sender, EventArgs e)
        {
            if (sender == save_data_enable)
            {
                toggleSave();
            }

            else if (sender == midi)
            {
                if (midi.Checked)
                {
                    if (!File.Exists(DLSPath) || string.IsNullOrWhiteSpace(DLSPath))
                    {
                        ImportDLS.Title = midi.Text;

                        if (ImportDLS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            DLSPath = ImportDLS.FileName;
                        else midi.Checked = false;
                    }
                }

                if (!midi.Checked) DLSPath = null;
            }
        }
        #endregion
    }
}
