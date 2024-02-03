
namespace FriishProduce
{
    partial class Options_VCN64
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options_VCN64));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.n64000 = new System.Windows.Forms.CheckBox();
            this.gbox008 = new System.Windows.Forms.GroupBox();
            this.n64003 = new System.Windows.Forms.CheckBox();
            this.n64002 = new System.Windows.Forms.CheckBox();
            this.n64001 = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.x009 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.n64004 = new System.Windows.Forms.GroupBox();
            this.ROMCType = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbox008.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.n64004.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.panel1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.Cancel);
            this.panel1.Controls.Add(this.OK);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.Name = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            resources.ApplyResources(this.OK, "OK");
            this.OK.Name = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // n64000
            // 
            resources.ApplyResources(this.n64000, "n64000");
            this.n64000.Name = "n64000";
            this.n64000.UseVisualStyleBackColor = true;
            // 
            // gbox008
            // 
            this.gbox008.Controls.Add(this.n64003);
            this.gbox008.Controls.Add(this.n64002);
            this.gbox008.Controls.Add(this.n64001);
            this.gbox008.Controls.Add(this.n64000);
            this.gbox008.Controls.Add(this.panel3);
            resources.ApplyResources(this.gbox008, "gbox008");
            this.gbox008.Name = "gbox008";
            this.gbox008.TabStop = false;
            // 
            // n64003
            // 
            resources.ApplyResources(this.n64003, "n64003");
            this.n64003.Name = "n64003";
            this.n64003.UseVisualStyleBackColor = true;
            // 
            // n64002
            // 
            resources.ApplyResources(this.n64002, "n64002");
            this.n64002.Name = "n64002";
            this.n64002.UseVisualStyleBackColor = true;
            // 
            // n64001
            // 
            resources.ApplyResources(this.n64001, "n64001");
            this.n64001.Name = "n64001";
            this.n64001.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(224)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.x009);
            this.panel3.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // x009
            // 
            resources.ApplyResources(this.x009, "x009");
            this.x009.Name = "x009";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.info_rhombus_large;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // n64004
            // 
            this.n64004.Controls.Add(this.ROMCType);
            resources.ApplyResources(this.n64004, "n64004");
            this.n64004.Name = "n64004";
            this.n64004.TabStop = false;
            // 
            // ROMCType
            // 
            this.ROMCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ROMCType.FormattingEnabled = true;
            this.ROMCType.Items.AddRange(new object[] {
            resources.GetString("ROMCType.Items"),
            resources.GetString("ROMCType.Items1")});
            resources.ApplyResources(this.ROMCType, "ROMCType");
            this.ROMCType.Name = "ROMCType";
            // 
            // Options_VCN64
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.Cancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.gbox008);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.n64004);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options_VCN64";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbox008.ResumeLayout(false);
            this.gbox008.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.n64004.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.CheckBox n64000;
        private System.Windows.Forms.GroupBox gbox008;
        private System.Windows.Forms.CheckBox n64003;
        private System.Windows.Forms.CheckBox n64002;
        private System.Windows.Forms.CheckBox n64001;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label x009;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox n64004;
        private System.Windows.Forms.ComboBox ROMCType;
    }
}