using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
            console_disable_resetbutton.Text = Program.Lang.String("console_disable_resetbutton", "vc_sega");
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
                { "console.disable_resetbutton", Program.Config.sega.console_disable_resetbutton },
                { "country", Program.Config.sega.country },
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
            else if ((!EmuType.HasValue || EmuType < 3) && controllerForm != null)
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
                
                console_brightness.Value                = int.Parse(Options["console.brightness"]);
                country.SelectedIndex                   = Options["country"] switch { "jp" => 0, "us" => 1, _ => 2 };
                console_disable_resetbutton.CheckState  = Options["console.disable_resetbutton"] switch { "1" => CheckState.Checked, "0" => CheckState.Indeterminate, _ => CheckState.Unchecked };
                dev_mdpad_enable_6b.CheckState          = Options["dev.mdpad.enable_6b"] switch { "1" => CheckState.Checked, "0" => CheckState.Indeterminate, _ => CheckState.Unchecked };
                save_sram.CheckState                    = Options["save_sram"] switch { "1" => CheckState.Checked, "0" => CheckState.Indeterminate, _ => CheckState.Unchecked };
                changeBrightness();
            }
            // *******

            // Disable certain options if configuring application settings
            // *******
            if (Binding != null)
            {
                controller_box.Enabled = false;
            }
        }

        protected override void SaveOptions()
        {
            Options["console.brightness"]           = console_brightness.Enabled ? label1.Text : null;
            Options["country"]                      = country.SelectedIndex switch { 0 => "jp", 1 => "us", _ => "eu" };
            Options["console.disable_resetbutton"]  = console_disable_resetbutton.CheckState switch { CheckState.Checked => "1", CheckState.Indeterminate => "0", _ => null };
            Options["dev.mdpad.enable_6b"]          = dev_mdpad_enable_6b.CheckState switch { CheckState.Checked => "1", CheckState.Indeterminate => "0", _ => null };
            Options["save_sram"]                    = save_sram.CheckState switch { CheckState.Checked => "1", CheckState.Indeterminate => "0", _ => null };

            base.SaveOptions();
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
