﻿
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
            this.padbutton_switch = new JCS.ToggleSwitch();
            this.padbutton = new System.Windows.Forms.Label();
            this.vc_options = new GroupBoxEx();
            this.sgenable = new System.Windows.Forms.CheckBox();
            this.backupram = new System.Windows.Forms.CheckBox();
            this.display = new GroupBoxEx();
            this.y_offset = new System.Windows.Forms.Label();
            this.y_offset_toggle = new System.Windows.Forms.NumericUpDown();
            this.sprline = new System.Windows.Forms.CheckBox();
            this.raster = new System.Windows.Forms.CheckBox();
            this.hide_overscan = new System.Windows.Forms.CheckBox();
            this.region_l = new GroupBoxEx();
            this.region = new System.Windows.Forms.ComboBox();
            this.bottomPanel1.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.vc_options.SuspendLayout();
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.y_offset_toggle)).BeginInit();
            this.region_l.SuspendLayout();
            this.SuspendLayout();
            // 
            // controller_box
            // 
            this.controller_box.Location = new System.Drawing.Point(564, 224);
            // 
            // padbutton_switch
            // 
            this.padbutton_switch.Location = new System.Drawing.Point(10, 20);
            this.padbutton_switch.Name = "padbutton_switch";
            this.padbutton_switch.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padbutton_switch.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padbutton_switch.Size = new System.Drawing.Size(35, 15);
            this.padbutton_switch.TabIndex = 23;
            this.padbutton_switch.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
            // 
            // padbutton
            // 
            this.padbutton.AutoSize = true;
            this.padbutton.Location = new System.Drawing.Point(51, 20);
            this.padbutton.Name = "padbutton";
            this.padbutton.Size = new System.Drawing.Size(57, 13);
            this.padbutton.TabIndex = 22;
            this.padbutton.Text = "padbutton";
            // 
            // vc_options
            // 
            this.vc_options.Controls.Add(this.sgenable);
            this.vc_options.Controls.Add(this.backupram);
            this.vc_options.Controls.Add(this.padbutton);
            this.vc_options.Controls.Add(this.padbutton_switch);
            this.vc_options.Location = new System.Drawing.Point(12, 66);
            this.vc_options.Name = "vc_options";
            this.vc_options.Size = new System.Drawing.Size(530, 90);
            this.vc_options.TabIndex = 24;
            this.vc_options.TabStop = false;
            this.vc_options.Tag = "vc_options";
            this.vc_options.Text = "vc_options";
            // 
            // sgenable
            // 
            this.sgenable.AutoSize = true;
            this.sgenable.Location = new System.Drawing.Point(10, 43);
            this.sgenable.Name = "sgenable";
            this.sgenable.Size = new System.Drawing.Size(69, 17);
            this.sgenable.TabIndex = 24;
            this.sgenable.Tag = "sgenable";
            this.sgenable.Text = "sgenable";
            this.sgenable.UseVisualStyleBackColor = true;
            // 
            // backupram
            // 
            this.backupram.AutoSize = true;
            this.backupram.Location = new System.Drawing.Point(10, 65);
            this.backupram.Name = "backupram";
            this.backupram.Size = new System.Drawing.Size(115, 17);
            this.backupram.TabIndex = 5;
            this.backupram.Tag = "save_data_enable";
            this.backupram.Text = "save_data_enable";
            this.backupram.UseVisualStyleBackColor = true;
            // 
            // display
            // 
            this.display.Controls.Add(this.y_offset);
            this.display.Controls.Add(this.y_offset_toggle);
            this.display.Controls.Add(this.sprline);
            this.display.Controls.Add(this.raster);
            this.display.Controls.Add(this.hide_overscan);
            this.display.Location = new System.Drawing.Point(12, 162);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(530, 116);
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
            this.y_offset.Size = new System.Drawing.Size(48, 13);
            this.y_offset.TabIndex = 4;
            this.y_offset.Tag = "y_offset";
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
            this.y_offset_toggle.Size = new System.Drawing.Size(46, 21);
            this.y_offset_toggle.TabIndex = 3;
            // 
            // sprline
            // 
            this.sprline.AutoSize = true;
            this.sprline.Location = new System.Drawing.Point(10, 90);
            this.sprline.Name = "sprline";
            this.sprline.Size = new System.Drawing.Size(57, 17);
            this.sprline.TabIndex = 2;
            this.sprline.Tag = "sprline";
            this.sprline.Text = "sprline";
            this.sprline.UseVisualStyleBackColor = true;
            // 
            // raster
            // 
            this.raster.AutoSize = true;
            this.raster.Location = new System.Drawing.Point(10, 68);
            this.raster.Name = "raster";
            this.raster.Size = new System.Drawing.Size(55, 17);
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
            // region_l
            // 
            this.region_l.Controls.Add(this.region);
            this.region_l.Location = new System.Drawing.Point(12, 10);
            this.region_l.Name = "region_l";
            this.region_l.Size = new System.Drawing.Size(530, 50);
            this.region_l.TabIndex = 39;
            this.region_l.TabStop = false;
            this.region_l.Tag = "region";
            this.region_l.Text = "region";
            // 
            // region
            // 
            this.region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region.FormattingEnabled = true;
            this.region.Location = new System.Drawing.Point(10, 18);
            this.region.Name = "region";
            this.region.Size = new System.Drawing.Size(510, 21);
            this.region.TabIndex = 18;
            this.region.Tag = "region";
            // 
            // Options_VC_PCE
            // 
            this.ClientSize = new System.Drawing.Size(554, 338);
            this.Controls.Add(this.region_l);
            this.Controls.Add(this.vc_options);
            this.Controls.Add(this.display);
            this.Name = "Options_VC_PCE";
            this.Tag = "vc_pce";
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.vc_options, 0);
            this.Controls.SetChildIndex(this.region_l, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.vc_options.ResumeLayout(false);
            this.vc_options.PerformLayout();
            this.display.ResumeLayout(false);
            this.display.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.y_offset_toggle)).EndInit();
            this.region_l.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private JCS.ToggleSwitch padbutton_switch;
        private System.Windows.Forms.Label padbutton;
        private GroupBoxEx vc_options;
        private GroupBoxEx display;
        private System.Windows.Forms.CheckBox hide_overscan;
        private System.Windows.Forms.CheckBox raster;
        private System.Windows.Forms.CheckBox sprline;
        private System.Windows.Forms.Label y_offset;
        private System.Windows.Forms.NumericUpDown y_offset_toggle;
        private System.Windows.Forms.CheckBox backupram;
        private System.Windows.Forms.CheckBox sgenable;
        private GroupBoxEx region_l;
        private System.Windows.Forms.ComboBox region;
    }
}
