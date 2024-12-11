
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
            this.g1 = new System.Windows.Forms.GroupBox();
            this.patch_autosizerom = new System.Windows.Forms.CheckBox();
            this.patch_expandedram = new System.Windows.Forms.CheckBox();
            this.patch_fixcrashes = new System.Windows.Forms.CheckBox();
            this.patch_fixbrightness = new System.Windows.Forms.CheckBox();
            this.romc_type_list = new System.Windows.Forms.ComboBox();
            this.g2 = new System.Windows.Forms.GroupBox();
            this.bottomPanel1.SuspendLayout();
            this.g1.SuspendLayout();
            this.g2.SuspendLayout();
            this.SuspendLayout();
            // 
            // g1
            // 
            this.g1.Controls.Add(this.patch_autosizerom);
            this.g1.Controls.Add(this.patch_expandedram);
            this.g1.Controls.Add(this.patch_fixcrashes);
            this.g1.Controls.Add(this.patch_fixbrightness);
            this.g1.Location = new System.Drawing.Point(12, 10);
            this.g1.Name = "g1";
            this.g1.Size = new System.Drawing.Size(530, 112);
            this.g1.TabIndex = 16;
            this.g1.TabStop = false;
            this.g1.Tag = "vc_options";
            this.g1.Text = "vc_options";
            // 
            // patch_autosizerom
            // 
            this.patch_autosizerom.AutoSize = true;
            this.patch_autosizerom.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_autosizerom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.patch_autosizerom.Location = new System.Drawing.Point(9, 63);
            this.patch_autosizerom.Name = "patch_autosizerom";
            this.patch_autosizerom.Size = new System.Drawing.Size(117, 17);
            this.patch_autosizerom.TabIndex = 16;
            this.patch_autosizerom.Tag = "patch_autosizerom";
            this.patch_autosizerom.Text = "patch_autosizerom";
            this.patch_autosizerom.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_autosizerom.UseVisualStyleBackColor = true;
            // 
            // patch_expandedram
            // 
            this.patch_expandedram.AutoSize = true;
            this.patch_expandedram.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_expandedram.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.patch_expandedram.Location = new System.Drawing.Point(9, 41);
            this.patch_expandedram.Name = "patch_expandedram";
            this.patch_expandedram.Size = new System.Drawing.Size(125, 17);
            this.patch_expandedram.TabIndex = 15;
            this.patch_expandedram.Tag = "patch_expandedram";
            this.patch_expandedram.Text = "patch_expandedram";
            this.patch_expandedram.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_expandedram.UseVisualStyleBackColor = true;
            // 
            // patch_fixcrashes
            // 
            this.patch_fixcrashes.AutoSize = true;
            this.patch_fixcrashes.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_fixcrashes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.patch_fixcrashes.Location = new System.Drawing.Point(9, 85);
            this.patch_fixcrashes.Name = "patch_fixcrashes";
            this.patch_fixcrashes.Size = new System.Drawing.Size(108, 17);
            this.patch_fixcrashes.TabIndex = 14;
            this.patch_fixcrashes.Tag = "patch_fixcrashes";
            this.patch_fixcrashes.Text = "patch_fixcrashes";
            this.patch_fixcrashes.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_fixcrashes.UseVisualStyleBackColor = true;
            // 
            // patch_fixbrightness
            // 
            this.patch_fixbrightness.AutoSize = true;
            this.patch_fixbrightness.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_fixbrightness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.patch_fixbrightness.Location = new System.Drawing.Point(9, 19);
            this.patch_fixbrightness.Name = "patch_fixbrightness";
            this.patch_fixbrightness.Size = new System.Drawing.Size(121, 17);
            this.patch_fixbrightness.TabIndex = 13;
            this.patch_fixbrightness.Tag = "patch_fixbrightness";
            this.patch_fixbrightness.Text = "patch_fixbrightness";
            this.patch_fixbrightness.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.patch_fixbrightness.UseVisualStyleBackColor = true;
            // 
            // romc_type_list
            // 
            this.romc_type_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.romc_type_list.FormattingEnabled = true;
            this.romc_type_list.Items.AddRange(new object[] {
            "ROMC Type 0",
            "ROMC Type 1"});
            this.romc_type_list.Location = new System.Drawing.Point(10, 18);
            this.romc_type_list.Name = "romc_type_list";
            this.romc_type_list.Size = new System.Drawing.Size(510, 21);
            this.romc_type_list.TabIndex = 0;
            this.romc_type_list.Tag = "romc_type";
            // 
            // g2
            // 
            this.g2.Controls.Add(this.romc_type_list);
            this.g2.Location = new System.Drawing.Point(12, 128);
            this.g2.Name = "g2";
            this.g2.Size = new System.Drawing.Size(530, 50);
            this.g2.TabIndex = 19;
            this.g2.TabStop = false;
            this.g2.Tag = "romc_type";
            this.g2.Text = "romc_type";
            // 
            // Options_VC_N64
            // 
            this.ClientSize = new System.Drawing.Size(554, 234);
            this.Controls.Add(this.g1);
            this.Controls.Add(this.g2);
            this.Name = "Options_VC_N64";
            this.Tag = "vc_n64";
            this.Load += new System.EventHandler(this.Form_IsShown);
            this.Controls.SetChildIndex(this.g2, 0);
            this.Controls.SetChildIndex(this.g1, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.g1.ResumeLayout(false);
            this.g1.PerformLayout();
            this.g2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g1;
        private System.Windows.Forms.CheckBox patch_autosizerom;
        private System.Windows.Forms.CheckBox patch_expandedram;
        private System.Windows.Forms.CheckBox patch_fixcrashes;
        private System.Windows.Forms.CheckBox patch_fixbrightness;
        private System.Windows.Forms.ComboBox romc_type_list;
        private System.Windows.Forms.GroupBox g2;
    }
}
