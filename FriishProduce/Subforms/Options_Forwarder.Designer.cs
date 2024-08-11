
namespace FriishProduce
{
    partial class Options_Forwarder
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
            this.ImportBIOS = new System.Windows.Forms.OpenFileDialog();
            this.use_bios = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.show_bios_screen = new System.Windows.Forms.Label();
            this.toggleSwitch2 = new JCS.ToggleSwitch();
            this.bios_settings = new System.Windows.Forms.GroupBox();
            this.bottomPanel1.SuspendLayout();
            this.bios_settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(362, 12);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(560, 47);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(458, 12);
            // 
            // ImportBIOS
            // 
            this.ImportBIOS.DefaultExt = "bin";
            this.ImportBIOS.Filter = ".bin (*.bin)|*.bin";
            // 
            // use_bios
            // 
            this.use_bios.AutoSize = true;
            this.use_bios.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.use_bios.Location = new System.Drawing.Point(50, 20);
            this.use_bios.Name = "use_bios";
            this.use_bios.Size = new System.Drawing.Size(49, 13);
            this.use_bios.TabIndex = 23;
            this.use_bios.Tag = "use_bios";
            this.use_bios.Text = "use_bios";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(10, 19);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Size = new System.Drawing.Size(35, 15);
            this.toggleSwitch1.TabIndex = 22;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.BIOSChanged);
            // 
            // show_bios_screen
            // 
            this.show_bios_screen.AutoSize = true;
            this.show_bios_screen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.show_bios_screen.Location = new System.Drawing.Point(50, 42);
            this.show_bios_screen.Name = "show_bios_screen";
            this.show_bios_screen.Size = new System.Drawing.Size(95, 13);
            this.show_bios_screen.TabIndex = 29;
            this.show_bios_screen.Tag = "show_bios_screen";
            this.show_bios_screen.Text = "show_bios_screen";
            // 
            // toggleSwitch2
            // 
            this.toggleSwitch2.Location = new System.Drawing.Point(10, 41);
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.Size = new System.Drawing.Size(35, 15);
            this.toggleSwitch2.TabIndex = 28;
            // 
            // bios_settings
            // 
            this.bios_settings.Controls.Add(this.toggleSwitch1);
            this.bios_settings.Controls.Add(this.use_bios);
            this.bios_settings.Controls.Add(this.show_bios_screen);
            this.bios_settings.Controls.Add(this.toggleSwitch2);
            this.bios_settings.Location = new System.Drawing.Point(12, 10);
            this.bios_settings.Name = "bios_settings";
            this.bios_settings.Size = new System.Drawing.Size(536, 65);
            this.bios_settings.TabIndex = 30;
            this.bios_settings.TabStop = false;
            this.bios_settings.Tag = "bios_settings";
            this.bios_settings.Text = "bios_settings";
            // 
            // Options_Forwarder
            // 
            this.ClientSize = new System.Drawing.Size(560, 137);
            this.Controls.Add(this.bios_settings);
            this.Name = "Options_Forwarder";
            this.Tag = "forwarder";
            this.Controls.SetChildIndex(this.bios_settings, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.bios_settings.ResumeLayout(false);
            this.bios_settings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ImportBIOS;
        private System.Windows.Forms.Label use_bios;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Label show_bios_screen;
        private JCS.ToggleSwitch toggleSwitch2;
        private System.Windows.Forms.GroupBox bios_settings;
    }
}
