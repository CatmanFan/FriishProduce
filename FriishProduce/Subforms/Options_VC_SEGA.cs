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
        public Options_VC_SEGA() : base()
        {
            InitializeComponent();

            Settings = new SortedDictionary<string, string>
            {
                { "console.brightness", "68" },
                // { "console.disable_resetbutton", "1" },
                // { "console.volume", "+6.0" },
                // { "country", "us" },
                { "dev.mdpad.enable_6b", "1" },
                // { "save_sram", "0" },
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
                BrightnessValue.Value = int.Parse(Settings["console.brightness"]);
                checkBox1.Checked = Settings["dev.mdpad.enable_6b"] == "1";
                ChangeBrightness();
            }
        }

        protected override void SaveOptions()
        {
            Settings["console.brightness"] = label1.Text;
            Settings["dev.mdpad.enable_6b"] = checkBox1.Checked ? "1" : "0";
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void BrightnessValue_Set(object sender, EventArgs e) => ChangeBrightness();
        private void ChangeBrightness()
        {
            /* float brightness = (BrightnessValue.Value / 100.0f);

            float[][] ptsArray =
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {brightness, brightness, brightness, 0, 1}
            };

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(orig, new Rectangle(0, 0, orig.Width, orig.Height)
                , 0, 0, orig.Width, orig.Height,
                GraphicsUnit.Pixel, imageAttributes); */

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
