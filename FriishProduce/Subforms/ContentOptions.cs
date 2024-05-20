using FriishProduce.Options;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class ContentOptions : Form
    {
        #region Variables to be changed go here. They should not be copied!
        public IDictionary<string, string> Options { get; set; }
        public int EmuType { get; set; }
        #endregion

        public ContentOptions()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                b_ok.Click += OK_Click;
                b_cancel.Click += Cancel_Click;
                Load += Form_Load;
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

            ResetOptions();
            CenterToParent();
        }
    }
}
