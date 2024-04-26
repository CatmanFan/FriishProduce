
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
            this.palette = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.palette_use_on_banner = new System.Windows.Forms.CheckBox();
            this.PaletteList = new System.Windows.Forms.ComboBox();
            this.bottomPanel1.SuspendLayout();
            this.palette.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(226, 12);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(424, 47);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(322, 12);
            // 
            // palette
            // 
            this.palette.Controls.Add(this.pictureBox1);
            this.palette.Controls.Add(this.palette_use_on_banner);
            this.palette.Controls.Add(this.PaletteList);
            this.palette.Location = new System.Drawing.Point(12, 8);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(400, 270);
            this.palette.TabIndex = 13;
            this.palette.TabStop = false;
            this.palette.Tag = "palette";
            this.palette.Text = "palette";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.screen_nes;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(72, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // palette_use_on_banner
            // 
            this.palette_use_on_banner.AutoSize = true;
            this.palette_use_on_banner.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.palette_use_on_banner.Location = new System.Drawing.Point(10, 246);
            this.palette_use_on_banner.Name = "palette_use_on_banner";
            this.palette_use_on_banner.Size = new System.Drawing.Size(141, 17);
            this.palette_use_on_banner.TabIndex = 12;
            this.palette_use_on_banner.Tag = "palette_use_on_banner";
            this.palette_use_on_banner.Text = "palette_use_on_banner";
            this.palette_use_on_banner.UseVisualStyleBackColor = true;
            // 
            // PaletteList
            // 
            this.PaletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaletteList.FormattingEnabled = true;
            this.PaletteList.Location = new System.Drawing.Point(10, 219);
            this.PaletteList.Name = "PaletteList";
            this.PaletteList.Size = new System.Drawing.Size(380, 21);
            this.PaletteList.TabIndex = 9;
            this.PaletteList.Tag = "palette";
            this.PaletteList.SelectedIndexChanged += new System.EventHandler(this.PaletteChanged);
            // 
            // Options_VC_NES
            // 
            this.ClientSize = new System.Drawing.Size(424, 342);
            this.Controls.Add(this.palette);
            this.Name = "Options_VC_NES";
            this.Tag = "vc_nes";
            this.Controls.SetChildIndex(this.palette, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.palette.ResumeLayout(false);
            this.palette.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox palette;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox PaletteList;
        private System.Windows.Forms.CheckBox palette_use_on_banner;
    }
}
