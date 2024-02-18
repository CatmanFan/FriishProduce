using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class ContentOptions : Form
    {
        #region Variables to be changed go here
        public IDictionary<string, string> Settings { get; protected set; }
        public int EmuType { get; set; }
        #endregion

        public ContentOptions()
        {
            InitializeComponent();

            Settings = new Dictionary<string, string>
            {
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                OK.Click += OK_Click;
                Cancel.Click += Cancel_Click;
                Load += Form_Load;

                // Language.AutoSetForm(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected virtual void ResetOptions()
        {
            // Form control
            // *******
            if (Settings != null)
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
