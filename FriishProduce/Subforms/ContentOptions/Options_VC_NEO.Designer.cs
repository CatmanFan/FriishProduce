
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
            this.g1 = new System.Windows.Forms.GroupBox();
            this.bottomPanel1.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.g1.SuspendLayout();
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
            this.bios_list.Location = new System.Drawing.Point(10, 18);
            this.bios_list.Name = "bios_list";
            this.bios_list.Size = new System.Drawing.Size(380, 21);
            this.bios_list.TabIndex = 14;
            this.bios_list.Tag = "bios";
            this.bios_list.SelectedIndexChanged += new System.EventHandler(this.BIOSChanged);
            // 
            // g1
            // 
            this.g1.Controls.Add(this.bios_list);
            this.g1.Location = new System.Drawing.Point(12, 10);
            this.g1.Name = "g1";
            this.g1.Size = new System.Drawing.Size(400, 50);
            this.g1.TabIndex = 38;
            this.g1.TabStop = false;
            this.g1.Tag = "bios";
            this.g1.Text = "bios";
            // 
            // Options_VC_NEO
            // 
            this.ClientSize = new System.Drawing.Size(424, 116);
            this.Controls.Add(this.g1);
            this.Name = "Options_VC_NEO";
            this.Tag = "vc_neo";
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.Controls.SetChildIndex(this.g1, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.g1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog biosImport;
        private System.Windows.Forms.ComboBox bios_list;
        private System.Windows.Forms.GroupBox g1;
    }
}
