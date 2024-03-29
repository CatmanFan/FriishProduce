﻿
namespace FriishProduce
{
    partial class Options_VC_N64
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.n64003 = new System.Windows.Forms.CheckBox();
            this.x009 = new System.Windows.Forms.Label();
            this.n64002 = new System.Windows.Forms.CheckBox();
            this.n64001 = new System.Windows.Forms.CheckBox();
            this.n64000 = new System.Windows.Forms.CheckBox();
            this.n64004 = new System.Windows.Forms.GroupBox();
            this.ROMCType = new System.Windows.Forms.ComboBox();
            this.bottomPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.n64004.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(261, 12);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(459, 47);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(357, 12);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.n64003);
            this.groupBox1.Controls.Add(this.x009);
            this.groupBox1.Controls.Add(this.n64002);
            this.groupBox1.Controls.Add(this.n64001);
            this.groupBox1.Controls.Add(this.n64000);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 110);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patches";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.information_large;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(242, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // n64003
            // 
            this.n64003.AutoSize = true;
            this.n64003.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64003.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.n64003.Location = new System.Drawing.Point(10, 63);
            this.n64003.MaximumSize = new System.Drawing.Size(203, 27);
            this.n64003.Name = "n64003";
            this.n64003.Size = new System.Drawing.Size(117, 18);
            this.n64003.TabIndex = 16;
            this.n64003.Text = "Allocate ROM size";
            this.n64003.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64003.UseVisualStyleBackColor = true;
            // 
            // x009
            // 
            this.x009.AutoSize = true;
            this.x009.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.x009.Location = new System.Drawing.Point(271, 17);
            this.x009.MaximumSize = new System.Drawing.Size(155, 80);
            this.x009.Name = "x009";
            this.x009.Size = new System.Drawing.Size(154, 39);
            this.x009.TabIndex = 18;
            this.x009.Text = "Please note that these options may not work for all base WADs.";
            // 
            // n64002
            // 
            this.n64002.AutoSize = true;
            this.n64002.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64002.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.n64002.Location = new System.Drawing.Point(10, 41);
            this.n64002.MaximumSize = new System.Drawing.Size(203, 27);
            this.n64002.Name = "n64002";
            this.n64002.Size = new System.Drawing.Size(103, 18);
            this.n64002.TabIndex = 15;
            this.n64002.Text = "Extended RAM";
            this.n64002.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64002.UseVisualStyleBackColor = true;
            // 
            // n64001
            // 
            this.n64001.AutoSize = true;
            this.n64001.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64001.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.n64001.Location = new System.Drawing.Point(10, 85);
            this.n64001.MaximumSize = new System.Drawing.Size(203, 27);
            this.n64001.Name = "n64001";
            this.n64001.Size = new System.Drawing.Size(86, 18);
            this.n64001.TabIndex = 14;
            this.n64001.Text = "Crashes fix";
            this.n64001.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64001.UseVisualStyleBackColor = true;
            // 
            // n64000
            // 
            this.n64000.AutoSize = true;
            this.n64000.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64000.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.n64000.Location = new System.Drawing.Point(10, 19);
            this.n64000.MaximumSize = new System.Drawing.Size(203, 27);
            this.n64000.Name = "n64000";
            this.n64000.Size = new System.Drawing.Size(133, 18);
            this.n64000.TabIndex = 13;
            this.n64000.Text = "Screen brightness fix";
            this.n64000.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.n64000.UseVisualStyleBackColor = true;
            // 
            // n64004
            // 
            this.n64004.Controls.Add(this.ROMCType);
            this.n64004.Location = new System.Drawing.Point(12, 128);
            this.n64004.Name = "n64004";
            this.n64004.Size = new System.Drawing.Size(435, 50);
            this.n64004.TabIndex = 17;
            this.n64004.TabStop = false;
            this.n64004.Text = "ROM compression type";
            // 
            // ROMCType
            // 
            this.ROMCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ROMCType.FormattingEnabled = true;
            this.ROMCType.Items.AddRange(new object[] {
            "ROMC Type 0",
            "ROMC Type 1"});
            this.ROMCType.Location = new System.Drawing.Point(10, 18);
            this.ROMCType.Name = "ROMCType";
            this.ROMCType.Size = new System.Drawing.Size(415, 21);
            this.ROMCType.TabIndex = 0;
            // 
            // Options_VC_N64
            // 
            this.ClientSize = new System.Drawing.Size(459, 242);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.n64004);
            this.Name = "Options_VC_N64";
            this.Shown += new System.EventHandler(this.Form_IsShown);
            this.Controls.SetChildIndex(this.n64004, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.n64004.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox n64003;
        private System.Windows.Forms.CheckBox n64002;
        private System.Windows.Forms.CheckBox n64001;
        private System.Windows.Forms.CheckBox n64000;
        private System.Windows.Forms.GroupBox n64004;
        private System.Windows.Forms.ComboBox ROMCType;
        private System.Windows.Forms.Label x009;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
