
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
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.use_bios.Location = new System.Drawing.Point(59, 13);
            this.use_bios.Name = "use_bios";
            this.use_bios.Size = new System.Drawing.Size(49, 13);
            this.use_bios.TabIndex = 23;
            this.use_bios.Tag = "use_bios";
            this.use_bios.Text = "use_bios";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(12, 12);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Size = new System.Drawing.Size(41, 16);
            this.toggleSwitch1.TabIndex = 22;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.BIOSChanged);
            // 
            // show_bios_screen
            // 
            this.show_bios_screen.AutoSize = true;
            this.show_bios_screen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.show_bios_screen.Location = new System.Drawing.Point(47, 36);
            this.show_bios_screen.Name = "show_bios_screen";
            this.show_bios_screen.Size = new System.Drawing.Size(95, 13);
            this.show_bios_screen.TabIndex = 29;
            this.show_bios_screen.Tag = "show_bios_screen";
            this.show_bios_screen.Text = "show_bios_screen";
            // 
            // toggleSwitch2
            // 
            this.toggleSwitch2.Location = new System.Drawing.Point(12, 35);
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.Size = new System.Drawing.Size(30, 15);
            this.toggleSwitch2.TabIndex = 28;
            // 
            // Options_Forwarder
            // 
            this.ClientSize = new System.Drawing.Size(384, 112);
            this.Controls.Add(this.show_bios_screen);
            this.Controls.Add(this.toggleSwitch2);
            this.Controls.Add(this.use_bios);
            this.Controls.Add(this.toggleSwitch1);
            this.Name = "Options_Forwarder";
            this.Tag = "forwarder";
            this.Controls.SetChildIndex(this.toggleSwitch1, 0);
            this.Controls.SetChildIndex(this.use_bios, 0);
            this.Controls.SetChildIndex(this.toggleSwitch2, 0);
            this.Controls.SetChildIndex(this.show_bios_screen, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ImportBIOS;
        private System.Windows.Forms.Label use_bios;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Label show_bios_screen;
        private JCS.ToggleSwitch toggleSwitch2;
    }
}
