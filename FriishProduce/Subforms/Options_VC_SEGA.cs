﻿using System;
using System.Collections.Generic;
using System.Drawing;
using FriishProduce.Options;

namespace FriishProduce
{
    public partial class Options_VC_SEGA : ContentOptions
    {
        public bool IsSMS { get; set; }

        public Options_VC_SEGA() : base()
        {
            InitializeComponent();

            Options = new SortedDictionary<string, string>
            {
                { "console.brightness", VC_SEGA.Default.console_brightness },
                { "console.disable_resetbutton", VC_SEGA.Default.console_disableresetbutton },
                { "country", Language.Current.Name.StartsWith("ja") ? "jp" : VC_SEGA.Default.country },
                { "dev.mdpad.enable_6b", VC_SEGA.Default.dev_mdpad_enable_6b },
                { "save_sram", VC_SEGA.Default.save_sram },
                { "machine_md.use_4ptap", null },
                { "nplayers", null }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Language.Localize(this);
                toggleSwitchL1.Text = Language.Get(toggleSwitch1, this);
                checkBox1.Text = Language.Get("EnableSaving");

                comboBox1.Items.Clear();
                comboBox1.Items.Add(Language.Get("Region.U"));
                comboBox1.Items.Add(Language.Get("Region.E"));
                comboBox1.Items.Add(Language.Get("Region.J"));
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Console/emulator-specific tools
            // *******
            toggleSwitchL1.Enabled = toggleSwitch1.Enabled = !IsSMS;
            // label1.Enabled = BrightnessValue.Enabled = EmuType >= 2 || IsSMS;

            // Form control
            // *******
            if (Options != null)
            {
                if (IsSMS) Options["dev.mdpad.enable_6b"] = null;

                if (Options["console.brightness"] == null || int.Parse(Options["console.brightness"]) < 0) Options["console.brightness"] = VC_SEGA.Default.console_brightness;

                BrightnessValue.Value = int.Parse(Options["console.brightness"]);
                comboBox1.SelectedIndex = Options["country"] == "jp" ? 2 : Options["country"] == "eu" ? 1 : 0;
                toggleSwitch1.Checked = Options["dev.mdpad.enable_6b"] == "1";
                checkBox1.Checked = Options["save_sram"] == "1";
                checkBox2.Checked = Options["console.disable_resetbutton"] == "1";
                ChangeBrightness();
            }
        }

        protected override void SaveOptions()
        {
            Options["console.brightness"] = BrightnessValue.Enabled ? label1.Text : null;
            Options["save_sram"] = checkBox1.Checked ? "1" : null;
            Options["country"] = comboBox1.SelectedIndex == 2 ? "jp" : comboBox1.SelectedIndex == 1 ? "eu" : "us";
            Options["dev.mdpad.enable_6b"] = toggleSwitch1.Checked ? "1" : null;
            Options["console.disable_resetbutton"] = checkBox2.Checked ? "1" : null;
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void BrightnessValue_Set(object sender, EventArgs e) => ChangeBrightness();
        private void ChangeBrightness()
        {
            double factor = BrightnessValue.Value * 0.01;
            var orig = Properties.Resources.screen_smd;
            Bitmap changed = new Bitmap(orig.Width, orig.Height);

            using (Graphics g = Graphics.FromImage(changed))
            using (Brush darkness = new SolidBrush(Color.FromArgb(255 - (int)Math.Round(factor * 255), Color.Black)))
            {
                g.DrawImage(orig, 0, 0);
                g.FillRectangle(darkness, new Rectangle(0, 0, orig.Width, orig.Height));
                g.Dispose();
            }
            pictureBox1.Image = changed;

            label1.Text = BrightnessValue.Value.ToString();

            if (!BrightnessValue.Enabled) Options["console.brightness"] = null;
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            if (sender == toggleSwitch1) toggleSwitchL1.Text = Language.Get(toggleSwitch1, this);
        }
    }
}
