
namespace FriishProduce
{
    partial class Options_VC_NEO
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
            this.biosImport = new System.Windows.Forms.OpenFileDialog();
            this.bios_list = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // biosImport
            // 
            this.biosImport.DefaultExt = "rom";
            this.biosImport.Filter = ".ROM (*.rom)|*.rom";
            this.biosImport.SupportMultiDottedExtensions = true;
            // 
            // bios_list
            // 
            this.bios_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bios_list.FormattingEnabled = true;
            this.bios_list.Items.AddRange(new object[] {
            "auto"});
            this.bios_list.Location = new System.Drawing.Point(12, 25);
            this.bios_list.Name = "bios_list";
            this.bios_list.Size = new System.Drawing.Size(536, 21);
            this.bios_list.TabIndex = 14;
            this.bios_list.Tag = "bios";
            this.bios_list.SelectedIndexChanged += new System.EventHandler(this.BIOSChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 16;
            this.label1.Tag = "bios";
            this.label1.Text = "bios";
            // 
            // Options_VC_NEO
            // 
            this.ClientSize = new System.Drawing.Size(560, 107);
            this.Controls.Add(this.bios_list);
            this.Controls.Add(this.label1);
            this.Name = "Options_VC_NEO";
            this.Tag = "vc_neo";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.bios_list, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog biosImport;
        private System.Windows.Forms.ComboBox bios_list;
        private System.Windows.Forms.Label label1;
    }
}
