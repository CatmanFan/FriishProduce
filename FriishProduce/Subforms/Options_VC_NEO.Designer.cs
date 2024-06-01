
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
            this.ImportBIOS = new System.Windows.Forms.OpenFileDialog();
            this.bios_list = new System.Windows.Forms.ComboBox();
            this.bios = new System.Windows.Forms.GroupBox();
            this.bottomPanel1.SuspendLayout();
            this.bios.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(206, 12);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(404, 47);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(302, 12);
            // 
            // ImportBIOS
            // 
            this.ImportBIOS.DefaultExt = "rom";
            this.ImportBIOS.Filter = ".ROM (*.rom)|*.rom";
            // 
            // bios_list
            // 
            this.bios_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bios_list.FormattingEnabled = true;
            this.bios_list.Items.AddRange(new object[] {
            "auto"});
            this.bios_list.Location = new System.Drawing.Point(10, 18);
            this.bios_list.Name = "bios_list";
            this.bios_list.Size = new System.Drawing.Size(360, 21);
            this.bios_list.TabIndex = 14;
            this.bios_list.Tag = "bios";
            this.bios_list.SelectedIndexChanged += new System.EventHandler(this.BIOSChanged);
            // 
            // bios
            // 
            this.bios.Controls.Add(this.bios_list);
            this.bios.Location = new System.Drawing.Point(12, 9);
            this.bios.Name = "bios";
            this.bios.Size = new System.Drawing.Size(380, 50);
            this.bios.TabIndex = 15;
            this.bios.TabStop = false;
            this.bios.Tag = "bios";
            this.bios.Text = "bios";
            // 
            // Options_VC_NEO
            // 
            this.ClientSize = new System.Drawing.Size(404, 122);
            this.Controls.Add(this.bios);
            this.Name = "Options_VC_NEO";
            this.Tag = "vc_neo";
            this.Controls.SetChildIndex(this.bios, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.bios.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ImportBIOS;
        private System.Windows.Forms.ComboBox bios_list;
        private System.Windows.Forms.GroupBox bios;
    }
}
