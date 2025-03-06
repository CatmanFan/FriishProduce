using FriishProduce.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class ContentOptions : Form
    {
        protected TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip tip;

        public ContentOptions()
        {
            InitializeComponent();
            if (DesignMode) return;

            // controllerForm = new Controller_...();
            ClearOptions();

            // Cosmetic
            // *******
            // Remove this code when creating a new copy
            // *****************************************
            b_ok.Click += OK_Click;
            b_cancel.Click += Cancel_Click;
            controller_cb.Click += Controller_Toggle;
            b_controller.Click += OpenControllerMapping;
            Load += Form_Load;
            // *****************************************

            // Program.Lang.Control(this);
            // [Any additional strings]
            // controller_box.Text = Program.Lang.String("controller", "projectform");
            // b_controller.Text = Program.Lang.String("controller_mapping", "projectform");

            // tip = HTML.CreateToolTip();
            // [Any additional tooltips]

            // Theme.ChangeColors(this, false);
            // Theme.BtnSizes(b_ok, b_cancel);
            // Theme.BtnLayout(this, b_ok, b_cancel);
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected virtual void ClearOptions()
        {
            // Add options that can be changed in string format
            // ************************************************
            Options = new Dictionary<string, string>
            {
            };
            // ************************************************
        }

        protected virtual void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                // Code logic in derived Form
            }
            // *******

            // Disable certain options if configuring application settings
            // *******
            if (Binding != null)
            {
                controller_box.Enabled = false;
            }
        }

        protected virtual void SaveOptions()
        {
            // Code logic in derived Form

            if (Binding != null)
            {
                foreach (var key in Options.Keys)
                {
                    var prop = Binding.GetType().GetProperty(key.ToString().Replace('.', '_'));

                    if (prop != null)
                    {
                        var val = prop.GetValue(Binding, null);

                        if (val != null)
                        {
                            if (val.GetType() == typeof(string))
                                prop.SetValue(Binding, Options[key]);

                            else if (val.GetType() == typeof(int))
                            {
                                int x = 0;
                                int.TryParse(Options[key], out x);
                                prop.SetValue(Binding, x);
                            }

                            else if (val.GetType() == typeof(bool))
                            {
                                bool x = false;
                                bool.TryParse(Options[key], out x);
                                prop.SetValue(Binding, x);
                            }
                        }

                        else if (prop.GetMethod.ReturnType.FullName == "System.String")
                        {
                            prop.SetValue(Binding, Options[key]);
                        }
                    }
                }
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Variables

        // Variables to be changed go here. They should not be copied!

        #endregion

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions

        // Additional functions for Form go here.

        #endregion

        // ---------------------------------------------------------------------------------------------------------------
        // /!\ REMAINING FUNCTIONS AND VALUES NEEDED FOR THE FORM BELOW. THERE IS NO NEED TO COPY THEM. /!\
        // ---------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Determines whether the content options form is binded to the config.json or to the project file.
        /// </summary>
        public dynamic Binding { get; set; } = null;

        public IDictionary<string, string> Options { get; set; }
        public int? EmuType { get; set; } = null;

        protected ControllerMapping controllerForm { get; set; }

        public bool UsesKeymap { get; set; }

        private IDictionary<Buttons, string> _Keymap;
        private IDictionary<Buttons, string> _oldKeymap;
        public IDictionary<Buttons, string> Keymap
        {
            get
            {
                if (controllerForm != null)
                {
                    controllerForm.Keymap = _Keymap;
                    return _Keymap;
                }

                else return null;
            }
            set
            {
                _Keymap = value;

                if (controllerForm != null)
                {
                    controllerForm.Keymap = value;
                }
            }
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (controllerForm?.Keymap != null)
            {
                UsesKeymap = controller_cb.Checked;

                if (UsesKeymap)
                    _Keymap = controllerForm.Keymap;
                else
                    Keymap = null;

                if (_oldKeymap != _Keymap)
                    _oldKeymap = _Keymap == null ? null : new Dictionary<Buttons, string>(_Keymap);
            }

            ClearOptions(); SaveOptions();
            DialogResult = DialogResult.OK;
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (_oldKeymap != null)
                controllerForm.Keymap = _Keymap = new Dictionary<Buttons, string>(_oldKeymap);
            else if (_oldKeymap == null && controllerForm?.Keymap?.Count > 0)
            {
                var blank = new Dictionary<Buttons, string>();

                for (int i = 0; i < controllerForm.Keymap.Count; i++)
                    blank.Add(controllerForm.Keymap.ElementAt(i).Key, string.Empty);

                controllerForm.Keymap = _oldKeymap = _Keymap = new Dictionary<Buttons, string>(blank);
            }

            DialogResult = DialogResult.Cancel;
        }

        protected void Form_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (_oldKeymap == null && _Keymap != null)
                _oldKeymap = controllerForm.Keymap = new Dictionary<Buttons, string>(_Keymap);

            try { ResetOptions(); }
            catch { ClearOptions(); ResetOptions(); MessageBox.Show(Program.Lang.Msg(14), MessageBox.Buttons.Ok, MessageBox.Icons.Warning); }
            CenterToParent();

            controller_box.Visible = controllerForm != null;
            b_controller.Enabled = controller_cb.Checked = UsesKeymap && controllerForm != null;
        }

        protected void OpenControllerMapping(object sender, EventArgs e)
        {
            if (DesignMode) return;

            controllerForm.Font = Program.MainForm.Font;
            controllerForm.Text = b_controller.Text.Replace("&", null).TrimEnd('.', '。').Trim();
            controllerForm.ShowDialog();
        }

        private void Controller_Toggle(object sender, EventArgs e)
        {
            b_controller.Enabled = controller_cb.Checked;
        }
    }
}
