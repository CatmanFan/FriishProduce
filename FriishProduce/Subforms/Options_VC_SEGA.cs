using System;
using System.Collections.Generic;
using System.Drawing;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Options_VC_SEGA : ContentOptions
    {
        public bool IsSMS { get; set; }

        public Options_VC_SEGA() : base()
        {
            InitializeComponent();

            Settings = new SortedDictionary<string, string>
            {
                { "console.brightness", "87" },
                // { "console.disable_resetbutton", "1" },
                { "country", Language.Current.Name.StartsWith("ja") ? "jp" : "us" },
                { "dev.mdpad.enable_6b", null },
                { "save_sram", "0" },
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Language.Localize(this);
                toggleSwitchL1.Text = Language.Get(toggleSwitch1, this);

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
            if (Settings != null)
            {
                if (Settings["console.brightness"] == null || int.Parse(Settings["console.brightness"]) < 0 || !BrightnessValue.Enabled) Settings["console.brightness"] = IsSMS ? "87" : "100";

                if (!IsSMS)
                {
                    if (Settings["dev.mdpad.enable_6b"] == null) Settings["dev.mdpad.enable_6b"] = IsSMS ? "0" : "1";
                    toggleSwitch1.Checked = Settings["dev.mdpad.enable_6b"] == "1";
                }

                BrightnessValue.Value = int.Parse(Settings["console.brightness"]);
                checkBox1.Checked = Settings["save_sram"] == "1";
                comboBox1.SelectedIndex = Settings["country"] == "jp" ? 2 : Settings["country"] == "eu" ? 1 : 0;
                ChangeBrightness();
            }
        }

        protected override void SaveOptions()
        {
            Settings["console.brightness"] = label1.Text;
            Settings["save_sram"] = checkBox1.Checked ? "1" : "0";
            Settings["country"] = comboBox1.SelectedIndex == 2 ? "jp" : comboBox1.SelectedIndex == 1 ? "eu" : "us";

            if (!IsSMS)
            {
                Settings["dev.mdpad.enable_6b"] = toggleSwitch1.Checked ? "1" : "0";
            }
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
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            if (sender == toggleSwitch1) toggleSwitchL1.Text = Language.Get(toggleSwitch1, this);
        }
    }
}
