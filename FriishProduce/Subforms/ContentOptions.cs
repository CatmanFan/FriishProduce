using FriishProduce.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class ContentOptions : Form
    {
        public ContentOptions()
        {
            InitializeComponent();

            // Add options that can be changed in string format
            // ************************************************
            Options = new Dictionary<string, string>
            {
            };
            // ************************************************

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                // Remove this code when creating a new copy
                // *****************************************
                b_ok.Click += OK_Click;
                b_cancel.Click += Cancel_Click;
                b_controller.Click += OpenControllerMapping;
                Load += Form_Load;
                // *****************************************
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected virtual void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                // Code logic in derived Form
            }
            // *******
        }

        protected virtual void SaveOptions()
        {
            // Code logic in derived Form
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

        public IDictionary<string, string> Options { get; set; }
        public IDictionary<Buttons, string> Keymap
        {
            get
            {
                if (controllerForm != null) return controllerForm.Mapping;
                else return null;
            }
            set
            {
                if (controllerForm != null) controllerForm.Mapping = value;
            }
        }
        protected ControllerMapping controllerForm { get; set; }
        public int EmuType { get; set; }

        protected void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            SaveOptions();
            DialogResult = DialogResult.OK;
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            DialogResult = DialogResult.Cancel;
        }

        protected void Form_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            b_controller.Visible = b_controller.Enabled = controllerForm != null;
            ResetOptions();
            CenterToParent();
        }

        protected void OpenControllerMapping(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // Code logic in derived Form
            // ********
            controllerForm.ShowDialog();
        }
    }
}
