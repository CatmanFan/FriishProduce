using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce.Views
{
    public partial class SEGA_Config : Form
    {
        readonly Lang x = Program.Language;
        private Dictionary<string, string> btns = new Dictionary<string, string>();
        public List<string> config { get; set; }

        public SEGA_Config(bool SMS)
        {
            InitializeComponent();
            x.Localize(this);

            if (country_l.SelectedIndex < 0) country_l.SelectedIndex = 0;
            if (nplayers_l.SelectedIndex < 0) nplayers_l.SelectedIndex = 0;
            if (use_4ptap_l.SelectedIndex < 0) use_4ptap_l.SelectedIndex = 0;
            mdpad_6b.Enabled = !SMS;
            use_4ptap.Enabled = !SMS;
            nplayers.Enabled = !SMS;
            smsui_opll.Enabled = SMS;

            if (Properties.Settings.Default.LightTheme)
            {
                foreach (var cb in Controls.OfType<CheckBox>())
                {
                    cb.ForeColor = ForeColor;
                    cb.FlatAppearance.CheckedBackColor = Color.FromArgb(122, 190, 245);
                    cb.FlatAppearance.MouseDownBackColor = Themes.Light.ButtonDown;
                    cb.FlatAppearance.MouseOverBackColor = Themes.Light.ButtonBorder;
                }
            }
            else
            {
                foreach (var cb in Controls.OfType<CheckBox>())
                {
                    cb.ForeColor = ForeColor;
                    cb.FlatAppearance.CheckedBackColor = Color.FromArgb(52, 115, 175);
                    cb.FlatAppearance.MouseDownBackColor = Themes.Dark.ButtonDown;
                    cb.FlatAppearance.MouseOverBackColor = Themes.Dark.ButtonBorder;
                }
            }

            controller.FlatStyle = FlatStyle.System;
            controller.ForeColor = Color.Black;
        }

        private void SwitchPanel(object sender, EventArgs e)
        {
            console.Checked = ((CheckBox)sender).Text == console.Text;
            control.Checked = ((CheckBox)sender).Text == control.Text;
            etc.Checked = ((CheckBox)sender).Text == etc.Text;
            p_console.Visible = console.Checked;
            p_control.Visible = control.Checked;
            p_etc.Visible = etc.Checked;
        }

        private void ChangeTheme(Form f)
        {
            f.BackColor = BackColor;
            f.ForeColor = ForeColor;
            foreach (var item in f.Controls.OfType<Panel>())
                if (item.Tag.ToString() == "panel") item.BackColor = panel.BackColor;
            foreach (var item in f.Controls.OfType<ComboBox>())
                if (item.Name.StartsWith("btns")) item.BackColor = use_4ptap_l.BackColor;
            foreach (var panel in f.Controls.OfType<Panel>())
            {
                foreach (var cb in panel.Controls.OfType<CheckBox>())
                    cb.ForeColor = ForeColor;
                foreach (var c1 in panel.Controls.OfType<Panel>())
                    foreach (var cb in c1.Controls.OfType<CheckBox>()) cb.ForeColor = ForeColor;
            }

            if (Properties.Settings.Default.LightTheme)
            {
                foreach (var panel in f.Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.BackColor = Themes.Light.Button;
                        button.FlatAppearance.BorderColor = Themes.Light.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Light.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
            else
            {
                foreach (var panel in f.Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var c2 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c2.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.BackColor = Themes.Dark.Button;
                        button.FlatAppearance.BorderColor = Themes.Dark.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Dark.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
        }

        private void ControllerChecked(object sender, EventArgs e)
        {
            if (controller.Checked)
            {
                SEGA_Controller ControllerForm = new SEGA_Controller(btns) { Text = x.Get("g006"), };
                ChangeTheme(ControllerForm);

                if (ControllerForm.ShowDialog(this) == DialogResult.OK)
                    btns = ControllerForm.Config;
                else
                    controller.Checked = false;
            }
            else
            {
                btns = new Dictionary<string, string>();
                controller.Checked = false;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            config = new List<string> { $"console.machine_country=\"{country_l.SelectedItem}\"" };
            if (console_disable_resetbutton.Checked) config.Add("console.disable_resetbutton=\"1\"");
            if (console_savesram.Checked) config.Add("save_sram=\"1\"");
            if (mdpad_6b.Checked && mdpad_6b.Enabled) config.Add("dev.mdpad.enable_6b=\"1\"");
            if (console_brightness.Checked) config.Add($"console.brightness=\"{console_brightness_value.Value}\"");
            if (controller.Checked) SetController();
            if (use_4ptap.Checked) config.Add($"machine_md.use_4ptap=\"{use_4ptap_l.SelectedItem}\"");
            if (nplayers.Checked) config.Add($"nplayers=\"{nplayers_l.SelectedItem}\"");
            if (disable_selectmenu.Checked) config.Add("disable_selectmenu=\"1\"");
            if (smsui_opll.Checked) config.Add("smsui.has_opll=\"1\"");
            config.Sort();

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Version 3 functions
        /// </summary>
        private void SetController()
        {
            string coreBtns = "console.core_bindings=\"up=:down=:left=:right=:+=:-=:a=:1=:2=:b=\"";
            string clBtns = "console.cl_bindings=\"up=:down=:left=:right=:+=:-=:y=:b=:a=:l=:x=:r=:zr=:zl=\"";
            string gcBtns = "console.gc_bindings=\"up=:down=:left=:right=:start=:b=:a=:x=:l=:y=:r=:z=:c=\"";

            foreach (KeyValuePair<string, string> item in btns)
            {
                // --------------------------------------------------
                // Wii Remote Configurations
                // --------------------------------------------------
                if (item.Key == "core_up" && item.Value != "—")
                    coreBtns = coreBtns.Replace("up=", $"up={item.Value[0]}");
                if (item.Key == "core_down" && item.Value != "—")
                    coreBtns = coreBtns.Replace("down=", $"down={item.Value[0]}");
                if (item.Key == "core_left" && item.Value != "—")
                    coreBtns = coreBtns.Replace("left=", $"left={item.Value[0]}");
                if (item.Key == "core_right" && item.Value != "—")
                    coreBtns = coreBtns.Replace("right=", $"right={item.Value[0]}");
                if (item.Key == "core_+" && item.Value != "—")
                    coreBtns = coreBtns.Replace("+=", $"+={item.Value[0]}");
                if (item.Key == "core_-" && item.Value != "—")
                    coreBtns = coreBtns.Replace("-=", $"-={item.Value[0]}");
                if (item.Key == "core_a" && item.Value != "—")
                    coreBtns = coreBtns.Replace("a=", $"a={item.Value[0]}");
                if (item.Key == "core_1" && item.Value != "—")
                    coreBtns = coreBtns.Replace("1=", $"1={item.Value[0]}");
                if (item.Key == "core_2" && item.Value != "—")
                    coreBtns = coreBtns.Replace("2=", $"2={item.Value[0]}");
                if (item.Key == "core_b" && item.Value != "—")
                    coreBtns = coreBtns.Replace("b=", $"b={item.Value[0]}");

                // --------------------------------------------------
                // Wii Classic Controller Configurations
                // --------------------------------------------------
                if (item.Key == "cl_up" && item.Value != "—")
                    clBtns = clBtns.Replace("up=", $"up={item.Value[0]}");
                if (item.Key == "cl_down" && item.Value != "—")
                    clBtns = clBtns.Replace("down=", $"down={item.Value[0]}");
                if (item.Key == "cl_left" && item.Value != "—")
                    clBtns = clBtns.Replace("left=", $"left={item.Value[0]}");
                if (item.Key == "cl_right" && item.Value != "—")
                    clBtns = clBtns.Replace("right=", $"right={item.Value[0]}");
                if (item.Key == "cl_+" && item.Value != "—")
                    clBtns = clBtns.Replace("+=", $"+={item.Value[0]}");
                if (item.Key == "cl_-" && item.Value != "—")
                    clBtns = clBtns.Replace("-=", $"-={item.Value[0]}");
                if (item.Key == "cl_y" && item.Value != "—")
                    clBtns = clBtns.Replace("y=", $"y={item.Value[0]}");
                if (item.Key == "cl_b" && item.Value != "—")
                    clBtns = clBtns.Replace("b=", $"b={item.Value[0]}");
                if (item.Key == "cl_a" && item.Value != "—")
                    clBtns = clBtns.Replace("a=", $"a={item.Value[0]}");
                if (item.Key == "cl_l" && item.Value != "—")
                    clBtns = clBtns.Replace("l=", $"l={item.Value[0]}");
                if (item.Key == "cl_x" && item.Value != "—")
                    clBtns = clBtns.Replace("x=", $"x={item.Value[0]}");
                if (item.Key == "cl_r" && item.Value != "—")
                    clBtns = clBtns.Replace("r=", $"r={item.Value[0]}");
                if (item.Key == "cl_zl" && item.Value != "—")
                    clBtns = clBtns.Replace("zl=", $"zl={item.Value[0]}");
                if (item.Key == "cl_zr" && item.Value != "—")
                    clBtns = clBtns.Replace("zr=", $"zr={item.Value[0]}");

                // --------------------------------------------------
                // Nintendo GameCube Controller Configurations
                // --------------------------------------------------
                if (item.Key == "gc_up" && item.Value != "—")
                    gcBtns = gcBtns.Replace("up=", $"up={item.Value[0]}");
                if (item.Key == "gc_down" && item.Value != "—")
                    gcBtns = gcBtns.Replace("down=", $"down={item.Value[0]}");
                if (item.Key == "gc_left" && item.Value != "—")
                    gcBtns = gcBtns.Replace("left=", $"left={item.Value[0]}");
                if (item.Key == "gc_right" && item.Value != "—")
                    gcBtns = gcBtns.Replace("right=", $"right={item.Value[0]}");
                if (item.Key == "gc_start" && item.Value != "—")
                    gcBtns = gcBtns.Replace("start=", $"start={item.Value[0]}");
                if (item.Key == "gc_b" && item.Value != "—")
                    gcBtns = gcBtns.Replace("b=", $"b={item.Value[0]}");
                if (item.Key == "gc_a" && item.Value != "—")
                    gcBtns = gcBtns.Replace("a=", $"a={item.Value[0]}");
                if (item.Key == "gc_x" && item.Value != "—")
                    gcBtns = gcBtns.Replace("x=", $"x={item.Value[0]}");
                if (item.Key == "gc_l" && item.Value != "—")
                    gcBtns = gcBtns.Replace("l=", $"l={item.Value[0]}");
                if (item.Key == "gc_y" && item.Value != "—")
                    gcBtns = gcBtns.Replace("y=", $"y={item.Value[0]}");
                if (item.Key == "gc_r" && item.Value != "—")
                    gcBtns = gcBtns.Replace("r=", $"r={item.Value[0]}");
                if (item.Key == "gc_z" && item.Value != "—")
                    gcBtns = gcBtns.Replace("z=", $"z={item.Value[0]}");
                if (item.Key == "gc_c" && item.Value != "—")
                    gcBtns = gcBtns.Replace("c=", $"c={item.Value[0]}");
            }

            config.Add(coreBtns);
            config.Add(clBtns);
            config.Add(gcBtns);
        }

        private void BrightnessToggled(object sender, EventArgs e) => console_brightness_value.Enabled = console_brightness.Checked;

        private void NPlayersToggled(object sender, EventArgs e) => nplayers_l.Enabled = nplayers.Checked;

        private void Use4PTapToggled(object sender, EventArgs e) => use_4ptap_l.Enabled = use_4ptap.Checked;
    }
}
