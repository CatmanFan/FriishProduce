
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
            this.sgenable_switch = new JCS.ToggleSwitch();
            this.sgenable = new System.Windows.Forms.Label();
            this.padbutton_switch = new JCS.ToggleSwitch();
            this.padbutton = new System.Windows.Forms.Label();
            this.europe = new System.Windows.Forms.Label();
            this.europe_switch = new JCS.ToggleSwitch();
            this.vc_options = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.display = new System.Windows.Forms.GroupBox();
            this.y_offset = new System.Windows.Forms.Label();
            this.y_offset_toggle = new System.Windows.Forms.NumericUpDown();
            this.sprline = new System.Windows.Forms.CheckBox();
            this.raster = new System.Windows.Forms.CheckBox();
            this.hide_overscan = new System.Windows.Forms.CheckBox();
            this.bottomPanel1.SuspendLayout();
            this.vc_options.SuspendLayout();
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.y_offset_toggle)).BeginInit();
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
            // sgenable_switch
            // 
            this.sgenable_switch.Location = new System.Drawing.Point(10, 41);
            this.sgenable_switch.Name = "sgenable_switch";
            this.sgenable_switch.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgenable_switch.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgenable_switch.Size = new System.Drawing.Size(35, 15);
            this.sgenable_switch.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Iphone;
            this.sgenable_switch.TabIndex = 21;
            this.sgenable_switch.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // sgenable
            // 
            this.sgenable.AutoSize = true;
            this.sgenable.Location = new System.Drawing.Point(51, 41);
            this.sgenable.Name = "sgenable";
            this.sgenable.Size = new System.Drawing.Size(50, 13);
            this.sgenable.TabIndex = 20;
            this.sgenable.Text = "sgenable";
            // 
            // padbutton_switch
            // 
            this.padbutton_switch.Location = new System.Drawing.Point(10, 62);
            this.padbutton_switch.Name = "padbutton_switch";
            this.padbutton_switch.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padbutton_switch.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padbutton_switch.Size = new System.Drawing.Size(35, 15);
            this.padbutton_switch.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Iphone;
            this.padbutton_switch.TabIndex = 23;
            this.padbutton_switch.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // padbutton
            // 
            this.padbutton.AutoSize = true;
            this.padbutton.Location = new System.Drawing.Point(51, 62);
            this.padbutton.Name = "padbutton";
            this.padbutton.Size = new System.Drawing.Size(55, 13);
            this.padbutton.TabIndex = 22;
            this.padbutton.Text = "padbutton";
            // 
            // europe
            // 
            this.europe.AutoSize = true;
            this.europe.Location = new System.Drawing.Point(51, 20);
            this.europe.Name = "europe";
            this.europe.Size = new System.Drawing.Size(40, 13);
            this.europe.TabIndex = 15;
            this.europe.Text = "europe";
            // 
            // europe_switch
            // 
            this.europe_switch.Location = new System.Drawing.Point(10, 20);
            this.europe_switch.Name = "europe_switch";
            this.europe_switch.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.europe_switch.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.europe_switch.Size = new System.Drawing.Size(35, 15);
            this.europe_switch.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Iphone;
            this.europe_switch.TabIndex = 19;
            this.europe_switch.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // vc_options
            // 
            this.vc_options.Controls.Add(this.checkBox4);
            this.vc_options.Controls.Add(this.europe_switch);
            this.vc_options.Controls.Add(this.europe);
            this.vc_options.Controls.Add(this.padbutton_switch);
            this.vc_options.Controls.Add(this.sgenable);
            this.vc_options.Controls.Add(this.padbutton);
            this.vc_options.Controls.Add(this.sgenable_switch);
            this.vc_options.Location = new System.Drawing.Point(12, 10);
            this.vc_options.Name = "vc_options";
            this.vc_options.Size = new System.Drawing.Size(536, 110);
            this.vc_options.TabIndex = 24;
            this.vc_options.TabStop = false;
            this.vc_options.Tag = "vc_options";
            this.vc_options.Text = "vc_options";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(10, 84);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(114, 17);
            this.checkBox4.TabIndex = 5;
            this.checkBox4.Tag = "save_data_enable";
            this.checkBox4.Text = "save_data_enable";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // display
            // 
            this.display.Controls.Add(this.y_offset);
            this.display.Controls.Add(this.y_offset_toggle);
            this.display.Controls.Add(this.sprline);
            this.display.Controls.Add(this.raster);
            this.display.Controls.Add(this.hide_overscan);
            this.display.Location = new System.Drawing.Point(12, 126);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(536, 115);
            this.display.TabIndex = 25;
            this.display.TabStop = false;
            this.display.Tag = "display";
            this.display.Text = "display";
            // 
            // y_offset
            // 
            this.y_offset.AutoSize = true;
            this.y_offset.Location = new System.Drawing.Point(62, 21);
            this.y_offset.Name = "y_offset";
            this.y_offset.Size = new System.Drawing.Size(44, 13);
            this.y_offset.TabIndex = 4;
            this.y_offset.Text = "y_offset";
            // 
            // y_offset_toggle
            // 
            this.y_offset_toggle.Location = new System.Drawing.Point(10, 19);
            this.y_offset_toggle.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.y_offset_toggle.Name = "y_offset_toggle";
            this.y_offset_toggle.Size = new System.Drawing.Size(46, 20);
            this.y_offset_toggle.TabIndex = 3;
            // 
            // sprline
            // 
            this.sprline.AutoSize = true;
            this.sprline.Location = new System.Drawing.Point(10, 88);
            this.sprline.Name = "sprline";
            this.sprline.Size = new System.Drawing.Size(56, 17);
            this.sprline.TabIndex = 2;
            this.sprline.Tag = "sprline";
            this.sprline.Text = "sprline";
            this.sprline.UseVisualStyleBackColor = true;
            // 
            // raster
            // 
            this.raster.AutoSize = true;
            this.raster.Location = new System.Drawing.Point(10, 67);
            this.raster.Name = "raster";
            this.raster.Size = new System.Drawing.Size(52, 17);
            this.raster.TabIndex = 1;
            this.raster.Tag = "raster";
            this.raster.Text = "raster";
            this.raster.UseVisualStyleBackColor = true;
            // 
            // hide_overscan
            // 
            this.hide_overscan.AutoSize = true;
            this.hide_overscan.Location = new System.Drawing.Point(10, 46);
            this.hide_overscan.Name = "hide_overscan";
            this.hide_overscan.Size = new System.Drawing.Size(96, 17);
            this.hide_overscan.TabIndex = 0;
            this.hide_overscan.Tag = "hide_overscan";
            this.hide_overscan.Text = "hide_overscan";
            this.hide_overscan.UseVisualStyleBackColor = true;
            // 
            // Options_VC_PCE
            // 
            this.ClientSize = new System.Drawing.Size(560, 422);
            this.Controls.Add(this.vc_options);
            this.Controls.Add(this.display);
            this.Name = "Options_VC_PCE";
            this.Tag = "vc_pce";
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.vc_options, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.vc_options.ResumeLayout(false);
            this.vc_options.PerformLayout();
            this.display.ResumeLayout(false);
            this.display.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.y_offset_toggle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private JCS.ToggleSwitch sgenable_switch;
        private System.Windows.Forms.Label sgenable;
        private JCS.ToggleSwitch padbutton_switch;
        private System.Windows.Forms.Label padbutton;
        private System.Windows.Forms.Label europe;
        private JCS.ToggleSwitch europe_switch;
        private System.Windows.Forms.GroupBox vc_options;
        private System.Windows.Forms.GroupBox display;
        private System.Windows.Forms.CheckBox hide_overscan;
        private System.Windows.Forms.CheckBox raster;
        private System.Windows.Forms.CheckBox sprline;
        private System.Windows.Forms.Label y_offset;
        private System.Windows.Forms.NumericUpDown y_offset_toggle;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}
