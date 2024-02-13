using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                // Language.AutoSetForm(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected virtual void ResetOptions()
        {
            if (Settings == null || Settings.Count == 0)
            {
                Settings = new Dictionary<string, string>
                {
                };
            }

            // Default options
            // *******
            // Code logic in derived Form
            // *******
        }

        protected virtual bool SaveOptions()
        {
            // Code logic in derived Form
            return true;
        }

        // ---------------------------------------------------------------------------------------------------------------

        private void OK_Click(object sender, EventArgs e)
        {
            if (DesignMode) return;

            SaveOptions();

            DialogResult = DialogResult.OK;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (DesignMode) return;

            if (!e.Cancel && (DialogResult == DialogResult.OK))
            e.Cancel = !SaveOptions();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            ResetOptions();
            CenterToParent();
        }
    }
}
