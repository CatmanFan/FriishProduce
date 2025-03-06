using System;
using System.Collections.Generic;

namespace FriishProduce
{
    public partial class Options_VC_N64 : ContentOptions
    {
        public Options_VC_N64() : base()
        {
            InitializeComponent();
            if (DesignMode) return;

            controllerForm = new Controller_N64();
            ClearOptions();

            // Cosmetic
            // *******
            Program.Lang.Control(this);
            controller_box.Text = Program.Lang.String("controller", "projectform");
            b_controller.Text = Program.Lang.String("controller_mapping", "projectform");

            tip = HTML.CreateToolTip();

            Theme.ChangeColors(this, false);
            Theme.BtnSizes(b_ok, b_cancel);
            Theme.BtnLayout(this, b_ok, b_cancel);
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ClearOptions()
        {
            Options = new Dictionary<string, string>
            {
                { "brightness",     Program.Config.n64.patch_nodark.ToString() },
                { "crash",          Program.Config.n64.patch_crashfix.ToString() },
                { "expansion",      Program.Config.n64.patch_expandedram.ToString() },
                { "rom_autosize",   Program.Config.n64.patch_autoromsize.ToString() },
                { "clean_textures", Program.Config.n64.patch_cleantextures.ToString() },
                { "widescreen",     "False" },
                { "romc",           Program.Config.n64.romc_type.ToString() }
            };
        }
        
        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                patch_autosizerom.Enabled = patch_fixcrashes.Enabled = EmuType.HasValue && EmuType <= 1;

                patch_fixbrightness.Checked         = bool.Parse(Options["brightness"]);
                patch_fixcrashes.Checked            = bool.Parse(Options["crash"]);
                patch_expandedram.Checked           = bool.Parse(Options["expansion"]);
                patch_autosizerom.Checked           = bool.Parse(Options["rom_autosize"]);
                patch_cleantextures.Checked         = bool.Parse(Options["clean_textures"]);
                // patch_widescreen.Checked            = bool.Parse(Options["widescreen"]);
                romc_type_list.SelectedIndex        = int.Parse(Options["romc"]);
            }

            if (!patch_autosizerom.Enabled) patch_autosizerom.Checked = false;
            if (!patch_fixcrashes.Enabled) patch_fixcrashes.Checked = false;
            // *******

            // ROMC visibility control
            // *******
            bool isRomc = !EmuType.HasValue || EmuType == 3;

            g2.Enabled = g2.Visible = isRomc;

            if (MaximumSize.IsEmpty) MaximumSize = Size;
            if (MinimumSize.IsEmpty) MinimumSize = new(Size.Width, Size.Height - g2.Height - 4);
            Size = isRomc ? MaximumSize : MinimumSize;
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
            Options["brightness"]                   = patch_fixbrightness.Checked.ToString();
            Options["crash"]                        = patch_fixcrashes.Checked.ToString();
            Options["expansion"]                    = patch_expandedram.Checked.ToString();
            Options["rom_autosize"]                 = patch_autosizerom.Checked.ToString();
            Options["clean_textures"]               = patch_cleantextures.Checked.ToString();
            // Options["widescreen"]                   = patch_widescreen.Checked.ToString();
            Options["romc"]                         = romc_type_list.SelectedIndex.ToString();

            base.SaveOptions();
        }
    }
}