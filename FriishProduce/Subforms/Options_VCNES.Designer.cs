
namespace FriishProduce
{
    partial class Options_VCNES
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.nespl = new System.Windows.Forms.ComboBox();
            this.nes000 = new System.Windows.Forms.Label();
            this.gbox007 = new System.Windows.Forms.GroupBox();
            this.nes001 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nes002 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.gbox007.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.Cancel);
            this.panel1.Controls.Add(this.OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 47);
            this.panel1.TabIndex = 4;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(312, 12);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(100, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Tag = "";
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(202, 12);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(100, 23);
            this.OK.TabIndex = 3;
            this.OK.Tag = "";
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // nespl
            // 
            this.nespl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nespl.FormattingEnabled = true;
            this.nespl.Items.AddRange(new object[] {
            "null"});
            this.nespl.Location = new System.Drawing.Point(96, 256);
            this.nespl.Name = "nespl";
            this.nespl.Size = new System.Drawing.Size(266, 21);
            this.nespl.TabIndex = 9;
            this.nespl.SelectedIndexChanged += new System.EventHandler(this.PaletteChanged);
            // 
            // nes000
            // 
            this.nes000.Location = new System.Drawing.Point(6, 260);
            this.nes000.Name = "nes000";
            this.nes000.Size = new System.Drawing.Size(88, 18);
            this.nes000.TabIndex = 8;
            this.nes000.Text = "palette:";
            this.nes000.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbox007
            // 
            this.gbox007.Controls.Add(this.nes001);
            this.gbox007.Controls.Add(this.pictureBox1);
            this.gbox007.Controls.Add(this.nespl);
            this.gbox007.Controls.Add(this.nes000);
            this.gbox007.Controls.Add(this.nes002);
            this.gbox007.Location = new System.Drawing.Point(19, 17);
            this.gbox007.Name = "gbox007";
            this.gbox007.Size = new System.Drawing.Size(390, 335);
            this.gbox007.TabIndex = 10;
            this.gbox007.TabStop = false;
            this.gbox007.Text = "screen";
            // 
            // nes001
            // 
            this.nes001.AutoSize = true;
            this.nes001.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.nes001.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.nes001.Location = new System.Drawing.Point(93, 307);
            this.nes001.Name = "nes001";
            this.nes001.Size = new System.Drawing.Size(47, 13);
            this.nes001.TabIndex = 11;
            this.nes001.Text = "author:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.screen_nes;
            this.pictureBox1.Location = new System.Drawing.Point(61, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(268, 226);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // nes002
            // 
            this.nes002.AutoSize = true;
            this.nes002.Location = new System.Drawing.Point(96, 283);
            this.nes002.Name = "nes002";
            this.nes002.Size = new System.Drawing.Size(80, 17);
            this.nes002.TabIndex = 12;
            this.nes002.Text = "checkBox1";
            this.nes002.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 373);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(429, 48);
            this.panel2.TabIndex = 11;
            // 
            // Options_VCNES
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(429, 421);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbox007);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options_VCNES";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "x006";
            this.Text = "options";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.gbox007.ResumeLayout(false);
            this.gbox007.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox nespl;
        private System.Windows.Forms.Label nes000;
        private System.Windows.Forms.GroupBox gbox007;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label nes001;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.CheckBox nes002;
    }
}