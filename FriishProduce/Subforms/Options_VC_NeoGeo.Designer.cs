
namespace FriishProduce
{
    partial class Options_VC_NeoGeo
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ImportBIOS = new System.Windows.Forms.OpenFileDialog();
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(282, 12);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(12, 12);
            this.Cancel.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(134, 18);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Import external BIOS";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.ToggleSwitchChanged);
            // 
            // ImportBIOS
            // 
            this.ImportBIOS.DefaultExt = "rom";
            this.ImportBIOS.Filter = ".ROM (*.rom)|*.rom";
            // 
            // Options_VC_NeoGeo
            // 
            this.ClientSize = new System.Drawing.Size(384, 92);
            this.Controls.Add(this.checkBox1);
            this.Name = "Options_VC_NeoGeo";
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.OpenFileDialog ImportBIOS;
    }
}
