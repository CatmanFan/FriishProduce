﻿
namespace FriishProduce
{
    partial class Options_VC_NES
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
            this.palette = new GroupBoxEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.palette_banner_usage = new System.Windows.Forms.CheckBox();
            this.PaletteList = new System.Windows.Forms.ComboBox();
            this.bottomPanel1.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.palette.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // b_controller
            // 
            this.b_controller.Location = new System.Drawing.Point(-264, 7);
            // 
            // controller_box
            // 
            this.controller_box.Location = new System.Drawing.Point(348, 258);
            // 
            // palette
            // 
            this.palette.Controls.Add(this.pictureBox1);
            this.palette.Controls.Add(this.palette_banner_usage);
            this.palette.Controls.Add(this.PaletteList);
            this.palette.Location = new System.Drawing.Point(12, 10);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(330, 302);
            this.palette.TabIndex = 13;
            this.palette.TabStop = false;
            this.palette.Tag = "palette";
            this.palette.Text = "palette";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.screen_nes;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(10, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(311, 225);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // palette_banner_usage
            // 
            this.palette_banner_usage.AutoSize = true;
            this.palette_banner_usage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.palette_banner_usage.Location = new System.Drawing.Point(10, 277);
            this.palette_banner_usage.Name = "palette_banner_usage";
            this.palette_banner_usage.Size = new System.Drawing.Size(135, 17);
            this.palette_banner_usage.TabIndex = 12;
            this.palette_banner_usage.Tag = "palette_banner_usage";
            this.palette_banner_usage.Text = "palette_banner_usage";
            this.palette_banner_usage.UseVisualStyleBackColor = true;
            // 
            // PaletteList
            // 
            this.PaletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaletteList.FormattingEnabled = true;
            this.PaletteList.Location = new System.Drawing.Point(10, 250);
            this.PaletteList.Name = "PaletteList";
            this.PaletteList.Size = new System.Drawing.Size(311, 21);
            this.PaletteList.TabIndex = 9;
            this.PaletteList.Tag = "palette";
            this.PaletteList.SelectedIndexChanged += new System.EventHandler(this.PaletteChanged);
            // 
            // Options_VC_NES
            // 
            this.ClientSize = new System.Drawing.Size(354, 370);
            this.Controls.Add(this.palette);
            this.Name = "Options_VC_NES";
            this.Tag = "vc_nes";
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.Controls.SetChildIndex(this.palette, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.palette.ResumeLayout(false);
            this.palette.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBoxEx palette;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox PaletteList;
        private System.Windows.Forms.CheckBox palette_banner_usage;
    }
}
