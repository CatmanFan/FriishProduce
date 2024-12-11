using FriishProduce.Options;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FriishProduce
{
    public partial class Options_VC_SEGA : ContentOptions
    {
        public Options_VC_SEGA() : base()
        {
            InitializeComponent();

            Options = new SortedDictionary<string, string>
            {
                { "console.brightness", VC_SEGA.Default.console_brightness },
                { "console.disable_resetbutton", VC_SEGA.Default.console_disableresetbutton },
                { "country", Program.Lang.Current.StartsWith("ja") ? "jp" : VC_SEGA.Default.country },
                { "dev.mdpad.enable_6b", VC_SEGA.Default.dev_mdpad_enable_6b },
                { "save_sram", VC_SEGA.Default.save_sram },
                { "machine_md.use_4ptap", null },
                { "nplayers", null }
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);

                #region Localization
                region.Text = Program.Lang.String("region");
                save_sram.Text = Program.Lang.String("save_data_enable", "projectform");
                console_disableresetbutton.Text = Program.Lang.String("console_disableresetbutton", "vc_sega");
                dev_mdpad_enable_6b.Text = string.Format(Program.Lang.String("dev_mdpad_enable_6b", "vc_sega"), Program.Lang.Console(Platform.SMD));

                country.Items.Clear();
                country.Items.AddRange(new string[] { Program.Lang.String("region_j"), Program.Lang.String("region_u"), Program.Lang.String("region_e") });
                #endregion
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Console/emulator-specific tools
            // *******
            dev_mdpad_enable_6b.Enabled = !IsSMS;
            // label1.Enabled = BrightnessValue.Enabled = EmuType >= 2 || IsSMS;

            // Controller options availability
            // *******
            if (EmuType >= 2 && Controller == null) Controller = new Controller_SEGA();
            else if (EmuType < 2 && Controller != null) Controller = null;

            // Form control
            // *******
            if (Options != null)
            {
                if (IsSMS) Options["dev.mdpad.enable_6b"] = null;

                if (Options["console.brightness"] == null || int.Parse(Options["console.brightness"]) < 0) Options["console.brightness"] = VC_SEGA.Default.console_brightness;
                
                console_brightness.Value            = int.Parse(Options["console.brightness"]);
                country.SelectedIndex               = Options["country"] switch { "jp" => 0, "us" => 1, _ => 2 };
                dev_mdpad_enable_6b.Checked         = Options["dev.mdpad.enable_6b"] == "1";
                save_sram.Checked                   = Options["save_sram"] == "1";
                console_disableresetbutton.Checked  = Options["console.disable_resetbutton"] == "1";
                changeBrightness();
            }
        }

        protected override void SaveOptions()
        {
            Options["console.brightness"]           = console_brightness.Enabled ? label1.Text : null;
            Options["save_sram"]                    = save_sram.Checked ? "1" : null;
            Options["country"]                      = country.SelectedIndex switch { 0 => "jp", 1 => "us", _ => "eu" };
            Options["dev.mdpad.enable_6b"]          = dev_mdpad_enable_6b.Checked ? "1" : null;
            Options["console.disable_resetbutton"]  = console_disableresetbutton.Checked ? "1" : null;
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Variables
        public bool IsSMS { get; set; }
        #endregion

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void BrightnessValue_Set(object sender, EventArgs e) => changeBrightness();
        private void changeBrightness()
        {
            double factor = console_brightness.Value * 0.01;
            var orig = Properties.Resources.screen_smd;
            Bitmap changed = new(orig.Width, orig.Height);

            using (Graphics g = Graphics.FromImage(changed))
            using (Brush darkness = new SolidBrush(Color.FromArgb(255 - (int)Math.Round(factor * 255), Color.Black)))
            {
                g.DrawImage(orig, 0, 0);
                g.FillRectangle(darkness, new Rectangle(0, 0, orig.Width, orig.Height));
                g.Dispose();
            }
            pictureBox1.Image = changed;

            label1.Text = console_brightness.Value.ToString();

            if (!console_brightness.Enabled) Options["console.brightness"] = null;
        }
        #endregion
    }
}
