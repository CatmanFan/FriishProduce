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
            if (DesignMode) return;

            ClearOptions();

            // Cosmetic
            // *******
            Program.Lang.Control(this);
            tip = HTML.CreateToolTip();

            Theme.ChangeColors(this, false);
            Theme.BtnSizes(b_ok, b_cancel);
            Theme.BtnLayout(this, b_ok, b_cancel);

            #region Localization
            controller_box.Text = Program.Lang.String("controller", "projectform");
            b_controller.Text = Program.Lang.String("controller_mapping", "projectform");

            region.Text = Program.Lang.String("region");
            save_sram.Text = Program.Lang.String("save_data_enable", "projectform");
            console_disableresetbutton.Text = Program.Lang.String("console_disableresetbutton", "vc_sega");
            dev_mdpad_enable_6b.Text = Program.Lang.Format(("dev_mdpad_enable_6b", "vc_sega"), Program.Lang.Console(Platform.SMD));

            country.Items.Clear();
            country.Items.AddRange(new string[] { Program.Lang.String("region_j"), Program.Lang.String("region_u"), Program.Lang.String("region_e") });
            #endregion
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ClearOptions()
        {
            Options = new SortedDictionary<string, string>
            {
                { "console.brightness", Program.Config.sega.console_brightness },
                { "console.disable_resetbutton", Program.Config.sega.console_disableresetbutton },
                { "country", Program.Lang.GetRegion() is Language.Region.Japan ? "jp" : Program.Config.sega.country },
                { "dev.mdpad.enable_6b", Program.Config.sega.dev_mdpad_enable_6b },
                { "save_sram", Program.Config.sega.save_sram },
                { "machine_md.use_4ptap", null },
                { "nplayers", null }
            };
        }

        protected override void ResetOptions()
        {
            // Console/emulator-specific tools
            // *******
            dev_mdpad_enable_6b.Enabled = !IsSMS;
            // label1.Enabled = BrightnessValue.Enabled = EmuType >= 2 || IsSMS;

            // Controller options availability
            // *******
            if (EmuType == 3 && controllerForm == null)
            {
                controllerForm = new Controller_SEGA(IsSMS);
                vc_options.Size = vc_options.MinimumSize;
            }
            else if (EmuType < 3 && controllerForm != null)
            {
                controllerForm = null;
                vc_options.Size = vc_options.MaximumSize;
            }

            // Form control
            // *******
            if (Options != null)
            {
                if (IsSMS) Options["dev.mdpad.enable_6b"] = null;

                if (Options["console.brightness"] == null || int.Parse(Options["console.brightness"]) < 0) Options["console.brightness"] = Program.Config.sega.console_brightness;
                
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
            var orig = IsSMS ? Properties.Resources.screen_sms : Properties.Resources.screen_smd;
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
