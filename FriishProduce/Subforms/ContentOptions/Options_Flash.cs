﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                { "update_frame_rate", Program.Config.flash.update_frame_rate },
                { "mouse", Program.Config.flash.mouse },
                { "qwerty_keyboard", Program.Config.flash.qwerty_keyboard },
                { "quality", Program.Config.flash.quality },
                { "shared_object_capability", Program.Config.flash.shared_object_capability },
                { "vff_sync_on_write", Program.Config.flash.vff_sync_on_write },
                { "vff_cache_size", Program.Config.flash.vff_cache_size },
                { "persistent_storage_total", Program.Config.flash.persistent_storage_total },
                { "persistent_storage_per_movie", Program.Config.flash.persistent_storage_per_movie },
                { "hbm_no_save", Program.Config.flash.hbm_no_save },
                { "strap_reminder", Program.Config.flash.strap_reminder },
                { "midi", null },
                { "fullscreen", Program.Config.flash.fullscreen },
                { "background_color", "0 0 0 0" }
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
                    // MIDI is counted separately
                    fullscreen.Checked = Options["fullscreen"] == "yes";

                    // Background color
                    // ****************
                    background_color_img.BackColor = BGColor.Color = System.Drawing.Color.FromArgb(255, 0, 0, 0);
                    int index = 0;
                    string R = "0", G = "0", B = "0";
                    for (int i = 0; i < 4; i++)
                    {
                        string color = Options["background_color"];
                        int prevIndex = index;
                        index = color.IndexOf(' ', prevIndex);

                        if (i == 0) R = color.Substring(prevIndex, index - prevIndex);
                        if (i == 1) G = color.Substring(prevIndex, index - prevIndex);
                        if (i == 2) B = color.Substring(prevIndex, index - prevIndex);

                        if (index < color.Length) index++;
                    }
                    background_color_img.BackColor = BGColor.Color = System.Drawing.Color.FromArgb(255, int.Parse(R), int.Parse(G), int.Parse(B));
                }
                catch
                {
                    Options = new Dictionary<string, string>
                    {
                        { "update_frame_rate", Program.Config.flash.update_frame_rate },
                        { "mouse", Program.Config.flash.mouse },
                        { "qwerty_keyboard", Program.Config.flash.qwerty_keyboard },
                        { "quality", Program.Config.flash.quality },
                        { "shared_object_capability", Program.Config.flash.shared_object_capability },
                        { "vff_sync_on_write", Program.Config.flash.vff_sync_on_write },
                        { "vff_cache_size", Program.Config.flash.vff_cache_size },
                        { "persistent_storage_total", Program.Config.flash.persistent_storage_total },
                        { "persistent_storage_per_movie", Program.Config.flash.persistent_storage_per_movie },
                        { "hbm_no_save", Program.Config.flash.hbm_no_save },
                        { "strap_reminder", Program.Config.flash.strap_reminder },
                        { "midi", null },
                        { "fullscreen", Program.Config.flash.fullscreen },
                        { "background_color", "0 0 0 0" }
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
                if (!string.IsNullOrEmpty(Options["midi"])) MessageBox.Show(string.Format(Program.Lang.Msg(11, true), Path.GetFileName(Options["midi"])));
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
            Options["fullscreen"] = fullscreen.Checked ? "yes" : "no";
            Options["background_color"] = BGColor.Color.R + BGColor.Color.G + BGColor.Color.B > 0 ? $"{BGColor.Color.R} {BGColor.Color.G} {BGColor.Color.B} 255" : "0 0 0 0";
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

        private void changeBackgroundColor(object sender, EventArgs e)
        {
            BGColor.ShowDialog();
            background_color_img.BackColor = BGColor.Color;
        }
        #endregion
    }
}
