
namespace FriishProduce
{
    partial class Options_RPGM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rpgm_settings = new GroupBoxEx();
            this.rtp_folder = new System.Windows.Forms.Label();
            this.use_rtp = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.ImportRTP = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.bottomPanel1.SuspendLayout();
            this.rpgm_settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // rpgm_settings
            // 
            this.rpgm_settings.Controls.Add(this.rtp_folder);
            this.rpgm_settings.Controls.Add(this.use_rtp);
            this.rpgm_settings.Controls.Add(this.toggleSwitch1);
            this.rpgm_settings.Location = new System.Drawing.Point(12, 10);
            this.rpgm_settings.Name = "rpgm_settings";
            this.rpgm_settings.Size = new System.Drawing.Size(530, 60);
            this.rpgm_settings.TabIndex = 29;
            this.rpgm_settings.TabStop = false;
            this.rpgm_settings.Tag = "rpgm_settings";
            this.rpgm_settings.Text = "rpgm_settings";
            // 
            // rtp_folder
            // 
            this.rtp_folder.AutoSize = true;
            this.rtp_folder.Enabled = false;
            this.rtp_folder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rtp_folder.Location = new System.Drawing.Point(50, 38);
            this.rtp_folder.Name = "rtp_folder";
            this.rtp_folder.Size = new System.Drawing.Size(31, 13);
            this.rtp_folder.TabIndex = 28;
            this.rtp_folder.Text = "none";
            // 
            // use_rtp
            // 
            this.use_rtp.AutoSize = true;
            this.use_rtp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.use_rtp.Location = new System.Drawing.Point(50, 20);
            this.use_rtp.Name = "use_rtp";
            this.use_rtp.Size = new System.Drawing.Size(44, 13);
            this.use_rtp.TabIndex = 27;
            this.use_rtp.Tag = "use_rtp";
            this.use_rtp.Text = "use_rtp";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(10, 19);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Size = new System.Drawing.Size(35, 15);
            this.toggleSwitch1.TabIndex = 26;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.RTPChanged);
            // 
            // ImportRTP
            // 
            this.ImportRTP.ShowNewFolderButton = false;
            // 
            // Options_RPGM
            // 
            this.ClientSize = new System.Drawing.Size(554, 126);
            this.Controls.Add(this.rpgm_settings);
            this.Name = "Options_RPGM";
            this.Tag = "forwarder";
            this.Controls.SetChildIndex(this.rpgm_settings, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.rpgm_settings.ResumeLayout(false);
            this.rpgm_settings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBoxEx rpgm_settings;
        private System.Windows.Forms.Label rtp_folder;
        private System.Windows.Forms.Label use_rtp;
        private JCS.ToggleSwitch toggleSwitch1;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog ImportRTP;
    }
}
