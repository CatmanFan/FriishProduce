
namespace FriishProduce
{
    partial class Options_VC_SEGA
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
            this.display = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.console_brightness = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.country = new System.Windows.Forms.ComboBox();
            this.dev_mdpad_enable_6b = new JCS.ToggleSwitch();
            this.toggleSwitchL1 = new System.Windows.Forms.Label();
            this.system = new System.Windows.Forms.GroupBox();
            this.console_disableresetbutton = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.save_sram = new System.Windows.Forms.CheckBox();
            this.bottomPanel1.SuspendLayout();
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.system.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(382, 12);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(580, 47);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(478, 12);
            // 
            // display
            // 
            this.display.Controls.Add(this.label1);
            this.display.Controls.Add(this.console_brightness);
            this.display.Controls.Add(this.pictureBox1);
            this.display.Location = new System.Drawing.Point(12, 32);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(275, 245);
            this.display.TabIndex = 14;
            this.display.TabStop = false;
            this.display.Tag = "display";
            this.display.Text = "display";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label1.Location = new System.Drawing.Point(241, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "100";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // console_brightness
            // 
            this.console_brightness.AutoSize = false;
            this.console_brightness.Location = new System.Drawing.Point(7, 215);
            this.console_brightness.Maximum = 100;
            this.console_brightness.Name = "console_brightness";
            this.console_brightness.Size = new System.Drawing.Size(235, 20);
            this.console_brightness.TabIndex = 15;
            this.console_brightness.Value = 100;
            this.console_brightness.Scroll += new System.EventHandler(this.BrightnessValue_Set);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.screen_smd;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(9, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // country
            // 
            this.country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.country.FormattingEnabled = true;
            this.country.Location = new System.Drawing.Point(9, 36);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(257, 21);
            this.country.TabIndex = 18;
            // 
            // dev_mdpad_enable_6b
            // 
            this.dev_mdpad_enable_6b.Location = new System.Drawing.Point(9, 64);
            this.dev_mdpad_enable_6b.Name = "dev_mdpad_enable_6b";
            this.dev_mdpad_enable_6b.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dev_mdpad_enable_6b.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dev_mdpad_enable_6b.Size = new System.Drawing.Size(30, 15);
            this.dev_mdpad_enable_6b.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.dev_mdpad_enable_6b.TabIndex = 18;
            this.dev_mdpad_enable_6b.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // toggleSwitchL1
            // 
            this.toggleSwitchL1.AutoSize = true;
            this.toggleSwitchL1.Location = new System.Drawing.Point(45, 64);
            this.toggleSwitchL1.Name = "toggleSwitchL1";
            this.toggleSwitchL1.Size = new System.Drawing.Size(119, 13);
            this.toggleSwitchL1.TabIndex = 19;
            this.toggleSwitchL1.Text = "dev_mdpad_enable_6b";
            // 
            // system
            // 
            this.system.Controls.Add(this.console_disableresetbutton);
            this.system.Controls.Add(this.label2);
            this.system.Controls.Add(this.toggleSwitchL1);
            this.system.Controls.Add(this.country);
            this.system.Controls.Add(this.dev_mdpad_enable_6b);
            this.system.Location = new System.Drawing.Point(293, 32);
            this.system.Name = "system";
            this.system.Size = new System.Drawing.Size(275, 110);
            this.system.TabIndex = 20;
            this.system.TabStop = false;
            this.system.Tag = "system";
            this.system.Text = "system";
            // 
            // console_disableresetbutton
            // 
            this.console_disableresetbutton.AutoSize = true;
            this.console_disableresetbutton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_disableresetbutton.Location = new System.Drawing.Point(9, 85);
            this.console_disableresetbutton.Name = "console_disableresetbutton";
            this.console_disableresetbutton.Size = new System.Drawing.Size(158, 17);
            this.console_disableresetbutton.TabIndex = 21;
            this.console_disableresetbutton.Tag = "console_disableresetbutton";
            this.console_disableresetbutton.Text = "console_disableresetbutton";
            this.console_disableresetbutton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "region";
            // 
            // save_sram
            // 
            this.save_sram.AutoSize = true;
            this.save_sram.Location = new System.Drawing.Point(12, 10);
            this.save_sram.Name = "save_sram";
            this.save_sram.Size = new System.Drawing.Size(78, 17);
            this.save_sram.TabIndex = 20;
            this.save_sram.Text = "save_sram";
            this.save_sram.UseVisualStyleBackColor = true;
            // 
            // Options_VC_SEGA
            // 
            this.ClientSize = new System.Drawing.Size(580, 337);
            this.Controls.Add(this.save_sram);
            this.Controls.Add(this.system);
            this.Controls.Add(this.display);
            this.Name = "Options_VC_SEGA";
            this.Tag = "vc_sega";
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.system, 0);
            this.Controls.SetChildIndex(this.save_sram, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.display.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.system.ResumeLayout(false);
            this.system.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox display;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar console_brightness;
        private System.Windows.Forms.ComboBox country;
        private JCS.ToggleSwitch dev_mdpad_enable_6b;
        private System.Windows.Forms.Label toggleSwitchL1;
        private System.Windows.Forms.GroupBox system;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox save_sram;
        private System.Windows.Forms.CheckBox console_disableresetbutton;
    }
}
