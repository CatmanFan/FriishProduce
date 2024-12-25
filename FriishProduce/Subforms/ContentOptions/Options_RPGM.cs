using FriishProduce.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Options_RPGM : ContentOptions
    {
        private string rtp { get; set; }

        public Options_RPGM()
        {
            InitializeComponent();

            Options = new Dictionary<string, string>
            {
                { "rtp_folder", null },
            };

            // Cosmetic
            // *******
            if (!DesignMode)
            {
                Program.Lang.Control(this);
            }
        }

        // ---------------------------------------------------------------------------------------------------------------

        protected override void ResetOptions()
        {
            // Form control
            // *******
            if (Options != null)
            {
                bool valid = RTP.IsValid(Options["rtp_folder"]);

                if (!valid && !string.IsNullOrEmpty(Options["rtp_folder"]))
                {
                    MessageBox.Show(string.Format(Program.Lang.Msg(12, true), Options["rtp_folder"]));
                    Options["rtp_folder"] = null;
                }

                toggleSwitch1.Checked = valid;

                if (valid) rtp = Options["rtp_folder"];

                rtp_folder.Text = valid ? Options["rtp_folder"] : Program.Lang.String("none");
                rtp_folder.Enabled = valid;
            }
            // *******
        }

        protected override void SaveOptions()
        {
            Options["rtp_folder"] = rtp;
        }

        // ---------------------------------------------------------------------------------------------------------------

        #region Functions
        private void RTPChanged(object sender, EventArgs e)
        {
            if (toggleSwitch1.Checked)
            {
                if (rtp == null)
                {
                    ImportRTP.Description = toggleSwitch1.Text;

                    if (ImportRTP.ShowDialog() == DialogResult.OK)
                    {
                        if (RTP.IsValid(ImportRTP.SelectedPath))
                            rtp = ImportRTP.SelectedPath;
                        else toggleSwitch1.Checked = false;
                    }
                    else toggleSwitch1.Checked = false;
                }
            }

            if (!toggleSwitch1.Checked) rtp = null;

            rtp_folder.Text = toggleSwitch1.Checked ? rtp : Program.Lang.String("none");
            rtp_folder.Enabled = toggleSwitch1.Checked;
        }
        #endregion
    }
}
