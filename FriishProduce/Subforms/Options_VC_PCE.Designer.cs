
namespace FriishProduce
{
    partial class Options_VC_PCE
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
            this.toggleSwitch2 = new JCS.ToggleSwitch();
            this.toggleSwitchL2 = new System.Windows.Forms.Label();
            this.toggleSwitch3 = new JCS.ToggleSwitch();
            this.toggleSwitchL3 = new System.Windows.Forms.Label();
            this.toggleSwitchL1 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bottomPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toggleSwitch2
            // 
            this.toggleSwitch2.Location = new System.Drawing.Point(10, 41);
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch2.Size = new System.Drawing.Size(30, 15);
            this.toggleSwitch2.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch2.TabIndex = 21;
            // 
            // toggleSwitchL2
            // 
            this.toggleSwitchL2.AutoSize = true;
            this.toggleSwitchL2.Location = new System.Drawing.Point(46, 41);
            this.toggleSwitchL2.Name = "toggleSwitchL2";
            this.toggleSwitchL2.Size = new System.Drawing.Size(72, 13);
            this.toggleSwitchL2.TabIndex = 20;
            this.toggleSwitchL2.Text = "Using HuCard";
            // 
            // toggleSwitch3
            // 
            this.toggleSwitch3.Location = new System.Drawing.Point(10, 62);
            this.toggleSwitch3.Name = "toggleSwitch3";
            this.toggleSwitch3.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch3.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch3.Size = new System.Drawing.Size(30, 15);
            this.toggleSwitch3.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch3.TabIndex = 23;
            // 
            // toggleSwitchL3
            // 
            this.toggleSwitchL3.AutoSize = true;
            this.toggleSwitchL3.Location = new System.Drawing.Point(46, 62);
            this.toggleSwitchL3.Name = "toggleSwitchL3";
            this.toggleSwitchL3.Size = new System.Drawing.Size(99, 13);
            this.toggleSwitchL3.TabIndex = 22;
            this.toggleSwitchL3.Text = "Using 2-button pad";
            // 
            // toggleSwitchL1
            // 
            this.toggleSwitchL1.AutoSize = true;
            this.toggleSwitchL1.Location = new System.Drawing.Point(46, 20);
            this.toggleSwitchL1.Name = "toggleSwitchL1";
            this.toggleSwitchL1.Size = new System.Drawing.Size(89, 13);
            this.toggleSwitchL1.TabIndex = 15;
            this.toggleSwitchL1.Text = "Japanese Region";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(10, 20);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Size = new System.Drawing.Size(30, 15);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.TabIndex = 19;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toggleSwitch1);
            this.groupBox1.Controls.Add(this.toggleSwitchL1);
            this.groupBox1.Controls.Add(this.toggleSwitch3);
            this.groupBox1.Controls.Add(this.toggleSwitchL2);
            this.groupBox1.Controls.Add(this.toggleSwitchL3);
            this.groupBox1.Controls.Add(this.toggleSwitch2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 87);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System";
            // 
            // Options_VC_PCE
            // 
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options_VC_PCE";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private JCS.ToggleSwitch toggleSwitch2;
        private System.Windows.Forms.Label toggleSwitchL2;
        private JCS.ToggleSwitch toggleSwitch3;
        private System.Windows.Forms.Label toggleSwitchL3;
        private System.Windows.Forms.Label toggleSwitchL1;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
