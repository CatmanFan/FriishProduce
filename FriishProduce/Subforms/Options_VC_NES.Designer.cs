
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
            this.palette_banner_usage = new System.Windows.Forms.CheckBox();
            this.PaletteList = new System.Windows.Forms.ComboBox();
            this.bottomPanel1.SuspendLayout();
            this.palette.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // palette
            // 
            this.palette.Controls.Add(this.pictureBox1);
            this.palette.Controls.Add(this.palette_banner_usage);
            this.palette.Controls.Add(this.PaletteList);
            this.palette.Location = new System.Drawing.Point(12, 10);
            this.palette.Name = "palette";
            this.palette.Size = new System.Drawing.Size(536, 270);
            this.palette.TabIndex = 13;
            this.palette.TabStop = false;
            this.palette.Tag = "palette";
            this.palette.Text = "palette";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.screen_nes;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(140, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // palette_banner_usage
            // 
            this.palette_banner_usage.AutoSize = true;
            this.palette_banner_usage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.palette_banner_usage.Location = new System.Drawing.Point(10, 246);
            this.palette_banner_usage.Name = "palette_banner_usage";
            this.palette_banner_usage.Size = new System.Drawing.Size(138, 17);
            this.palette_banner_usage.TabIndex = 12;
            this.palette_banner_usage.Tag = "palette_banner_usage";
            this.palette_banner_usage.Text = "palette_banner_usage";
            this.palette_banner_usage.UseVisualStyleBackColor = true;
            // 
            // PaletteList
            // 
            this.PaletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaletteList.FormattingEnabled = true;
            this.PaletteList.Location = new System.Drawing.Point(10, 219);
            this.PaletteList.Name = "PaletteList";
            this.PaletteList.Size = new System.Drawing.Size(516, 21);
            this.PaletteList.TabIndex = 9;
            this.PaletteList.Tag = "palette";
            this.PaletteList.SelectedIndexChanged += new System.EventHandler(this.PaletteChanged);
            // 
            // Options_VC_NES
            // 
            this.ClientSize = new System.Drawing.Size(560, 422);
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
        private System.Windows.Forms.CheckBox palette_banner_usage;
    }
}
