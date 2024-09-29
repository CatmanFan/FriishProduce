
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
            this.vc_options = new System.Windows.Forms.GroupBox();
            this.dev_mdpad_enable_6b = new System.Windows.Forms.CheckBox();
            this.save_sram = new System.Windows.Forms.CheckBox();
            this.console_disableresetbutton = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bottomPanel1.SuspendLayout();
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.vc_options.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(492, 12);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(710, 47);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(598, 12);
            // 
            // display
            // 
            this.display.Controls.Add(this.label1);
            this.display.Controls.Add(this.console_brightness);
            this.display.Controls.Add(this.pictureBox1);
            this.display.Location = new System.Drawing.Point(12, 10);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(330, 290);
            this.display.TabIndex = 14;
            this.display.TabStop = false;
            this.display.Tag = "display";
            this.display.Text = "display";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(290, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "100";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // console_brightness
            // 
            this.console_brightness.AutoSize = false;
            this.console_brightness.Location = new System.Drawing.Point(10, 252);
            this.console_brightness.Maximum = 100;
            this.console_brightness.Name = "console_brightness";
            this.console_brightness.Size = new System.Drawing.Size(282, 24);
            this.console_brightness.TabIndex = 15;
            this.console_brightness.Value = 100;
            this.console_brightness.Scroll += new System.EventHandler(this.BrightnessValue_Set);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.screen_smd;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(15, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 225);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // country
            // 
            this.country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.country.FormattingEnabled = true;
            this.country.Location = new System.Drawing.Point(13, 37);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(325, 21);
            this.country.TabIndex = 18;
            // 
            // vc_options
            // 
            this.vc_options.Controls.Add(this.dev_mdpad_enable_6b);
            this.vc_options.Controls.Add(this.save_sram);
            this.vc_options.Controls.Add(this.console_disableresetbutton);
            this.vc_options.Controls.Add(this.label2);
            this.vc_options.Controls.Add(this.country);
            this.vc_options.Location = new System.Drawing.Point(348, 10);
            this.vc_options.Name = "vc_options";
            this.vc_options.Size = new System.Drawing.Size(350, 290);
            this.vc_options.TabIndex = 20;
            this.vc_options.TabStop = false;
            this.vc_options.Tag = "vc_options";
            this.vc_options.Text = "vc_options";
            // 
            // dev_mdpad_enable_6b
            // 
            this.dev_mdpad_enable_6b.AutoSize = true;
            this.dev_mdpad_enable_6b.Location = new System.Drawing.Point(13, 69);
            this.dev_mdpad_enable_6b.Name = "dev_mdpad_enable_6b";
            this.dev_mdpad_enable_6b.Size = new System.Drawing.Size(138, 17);
            this.dev_mdpad_enable_6b.TabIndex = 22;
            this.dev_mdpad_enable_6b.Tag = "dev_mdpad_enable_6b";
            this.dev_mdpad_enable_6b.Text = "dev_mdpad_enable_6b";
            this.dev_mdpad_enable_6b.UseVisualStyleBackColor = true;
            // 
            // save_sram
            // 
            this.save_sram.AutoSize = true;
            this.save_sram.Location = new System.Drawing.Point(13, 111);
            this.save_sram.Name = "save_sram";
            this.save_sram.Size = new System.Drawing.Size(77, 17);
            this.save_sram.TabIndex = 20;
            this.save_sram.Text = "save_sram";
            this.save_sram.UseVisualStyleBackColor = true;
            // 
            // console_disableresetbutton
            // 
            this.console_disableresetbutton.AutoSize = true;
            this.console_disableresetbutton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_disableresetbutton.Location = new System.Drawing.Point(13, 90);
            this.console_disableresetbutton.Name = "console_disableresetbutton";
            this.console_disableresetbutton.Size = new System.Drawing.Size(155, 17);
            this.console_disableresetbutton.TabIndex = 21;
            this.console_disableresetbutton.Tag = "console_disableresetbutton";
            this.console_disableresetbutton.Text = "console_disableresetbutton";
            this.console_disableresetbutton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "region";
            // 
            // Options_VC_SEGA
            // 
            this.ClientSize = new System.Drawing.Size(710, 362);
            this.Controls.Add(this.vc_options);
            this.Controls.Add(this.display);
            this.Name = "Options_VC_SEGA";
            this.Tag = "vc_sega";
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.vc_options, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.display.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.vc_options.ResumeLayout(false);
            this.vc_options.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox display;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar console_brightness;
        private System.Windows.Forms.ComboBox country;
        private System.Windows.Forms.GroupBox vc_options;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox save_sram;
        private System.Windows.Forms.CheckBox console_disableresetbutton;
        private System.Windows.Forms.CheckBox dev_mdpad_enable_6b;
    }
}
