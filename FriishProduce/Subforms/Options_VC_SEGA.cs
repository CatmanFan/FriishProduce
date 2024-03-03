using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
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
                { "console.brightness", null },
                // { "console.disable_resetbutton", "1" },
                // { "console.volume", "+6.0" },
                { "country", Language.Current.Name.StartsWith("ja") ? "jp" : "us" },
                { "dev.mdpad.enable_6b", null },
                { "save_sram", "0" },
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Language.Localize(this);
                comboBox1.Items.Add(Language.Get("Region.U"));
                comboBox1.Items.Add(Language.Get("Region.E"));
                comboBox1.Items.Add(Language.Get("Region.J"));
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Settings != null)
            {
                if (Settings["console.brightness"] == null) Settings["console.brightness"] = IsSMS ? "68" : "100";
                if (Settings["dev.mdpad.enable_6b"] == null) Settings["dev.mdpad.enable_6b"] = IsSMS ? "0" : "1";

                BrightnessValue.Value = int.Parse(Settings["console.brightness"]);
                checkBox1.Checked = Settings["dev.mdpad.enable_6b"] == "1";
                checkBox2.Checked = Settings["save_sram"] == "1";
                comboBox1.SelectedIndex = Settings["country"] == "jp" ? 2 : Settings["country"] == "eu" ? 1 : 0;
                ChangeBrightness();
            }
        }

        protected override void SaveOptions()
        {
            Settings["console.brightness"] = label1.Text;
            Settings["dev.mdpad.enable_6b"] = checkBox1.Checked ? "1" : "0";
            Settings["save_sram"] = checkBox2.Checked ? "1" : "0";
            Settings["country"] = comboBox1.SelectedIndex == 2 ? "jp" : comboBox1.SelectedIndex == 1 ? "eu" : "us";
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
    }
}
