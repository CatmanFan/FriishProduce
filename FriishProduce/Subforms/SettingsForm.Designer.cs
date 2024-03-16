
namespace FriishProduce
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.DefaultImageInterpolation = new System.Windows.Forms.ComboBox();
            this.AutoOpenFolder = new System.Windows.Forms.CheckBox();
            this.LanguageList = new System.Windows.Forms.ComboBox();
            this.AutoLibRetro = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.n64003 = new System.Windows.Forms.CheckBox();
            this.n64002 = new System.Windows.Forms.CheckBox();
            this.n64001 = new System.Windows.Forms.CheckBox();
            this.n64000 = new System.Windows.Forms.CheckBox();
            this.n64004 = new System.Windows.Forms.GroupBox();
            this.ROMCType = new System.Windows.Forms.ComboBox();
            this.DownloadBanners = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.leftSeparator = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.FNANDLoader_vWii = new System.Windows.Forms.RadioButton();
            this.FNANDLoader_Wii = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.FStorage_SD = new System.Windows.Forms.RadioButton();
            this.FStorage_USB = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.n64004.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.bottomPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // DefaultImageInterpolation
            // 
            this.DefaultImageInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.DefaultImageInterpolation, "DefaultImageInterpolation");
            this.DefaultImageInterpolation.FormattingEnabled = true;
            this.DefaultImageInterpolation.Items.AddRange(new object[] {
            resources.GetString("DefaultImageInterpolation.Items")});
            this.DefaultImageInterpolation.Name = "DefaultImageInterpolation";
            // 
            // AutoOpenFolder
            // 
            resources.ApplyResources(this.AutoOpenFolder, "AutoOpenFolder");
            this.AutoOpenFolder.Name = "AutoOpenFolder";
            this.AutoOpenFolder.UseVisualStyleBackColor = true;
            // 
            // LanguageList
            // 
            this.LanguageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageList.FormattingEnabled = true;
            this.LanguageList.Items.AddRange(new object[] {
            resources.GetString("LanguageList.Items")});
            resources.ApplyResources(this.LanguageList, "LanguageList");
            this.LanguageList.Name = "LanguageList";
            this.LanguageList.SelectedIndexChanged += new System.EventHandler(this.LanguageList_SelectedIndexChanged);
            // 
            // AutoLibRetro
            // 
            resources.ApplyResources(this.AutoLibRetro, "AutoLibRetro");
            this.AutoLibRetro.Name = "AutoLibRetro";
            this.AutoLibRetro.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.n64003);
            this.groupBox3.Controls.Add(this.n64002);
            this.groupBox3.Controls.Add(this.n64001);
            this.groupBox3.Controls.Add(this.n64000);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
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
            // n64000
            // 
            resources.ApplyResources(this.n64000, "n64000");
            this.n64000.Name = "n64000";
            this.n64000.UseVisualStyleBackColor = true;
            // 
            // n64004
            // 
            this.n64004.Controls.Add(this.ROMCType);
            this.n64004.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.n64004, "n64004");
            this.n64004.Name = "n64004";
            this.n64004.TabStop = false;
            // 
            // ROMCType
            // 
            this.ROMCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ROMCType, "ROMCType");
            this.ROMCType.FormattingEnabled = true;
            this.ROMCType.Name = "ROMCType";
            // 
            // DownloadBanners
            // 
            resources.ApplyResources(this.DownloadBanners, "DownloadBanners");
            this.DownloadBanners.Name = "DownloadBanners";
            this.DownloadBanners.UseVisualStyleBackColor = true;
            this.DownloadBanners.Click += new System.EventHandler(this.DownloadBanners_Click);
            // 
            // OK
            // 
            resources.ApplyResources(this.OK, "OK");
            this.OK.Name = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.Cancel, "Cancel");
            this.Cancel.Name = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel1.Controls.Add(this.Cancel);
            this.bottomPanel1.Controls.Add(this.OK);
            this.bottomPanel1.Controls.Add(this.DownloadBanners);
            resources.ApplyResources(this.bottomPanel1, "bottomPanel1");
            this.bottomPanel1.Name = "bottomPanel1";
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            resources.ApplyResources(this.bottomPanel2, "bottomPanel2");
            this.bottomPanel2.Name = "bottomPanel2";
            // 
            // TreeView
            // 
            this.TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TreeView, "TreeView");
            this.TreeView.FullRowSelect = true;
            this.TreeView.ItemHeight = 19;
            this.TreeView.Name = "TreeView";
            this.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes1")))});
            this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.AutoOpenFolder);
            this.panel1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LanguageList);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.n64004);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.AutoLibRetro);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DefaultImageInterpolation);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // leftSeparator
            // 
            this.leftSeparator.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.leftSeparator, "leftSeparator");
            this.leftSeparator.Name = "leftSeparator";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox9);
            this.panel3.Controls.Add(this.groupBox8);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.FNANDLoader_vWii);
            this.groupBox9.Controls.Add(this.FNANDLoader_Wii);
            this.groupBox9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // FNANDLoader_vWii
            // 
            resources.ApplyResources(this.FNANDLoader_vWii, "FNANDLoader_vWii");
            this.FNANDLoader_vWii.Name = "FNANDLoader_vWii";
            this.FNANDLoader_vWii.TabStop = true;
            this.FNANDLoader_vWii.UseVisualStyleBackColor = true;
            // 
            // FNANDLoader_Wii
            // 
            resources.ApplyResources(this.FNANDLoader_Wii, "FNANDLoader_Wii");
            this.FNANDLoader_Wii.Name = "FNANDLoader_Wii";
            this.FNANDLoader_Wii.TabStop = true;
            this.FNANDLoader_Wii.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.FStorage_SD);
            this.groupBox8.Controls.Add(this.FStorage_USB);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // FStorage_SD
            // 
            resources.ApplyResources(this.FStorage_SD, "FStorage_SD");
            this.FStorage_SD.Name = "FStorage_SD";
            this.FStorage_SD.TabStop = true;
            this.FStorage_SD.UseVisualStyleBackColor = true;
            // 
            // FStorage_USB
            // 
            resources.ApplyResources(this.FStorage_USB, "FStorage_USB");
            this.FStorage_USB.Name = "FStorage_USB";
            this.FStorage_USB.TabStop = true;
            this.FStorage_USB.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.Cancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.leftSeparator);
            this.Controls.Add(this.TreeView);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Loading);
            this.Shown += new System.EventHandler(this.Loading);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.n64004.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Panel bottomPanel1;
        private System.Windows.Forms.Panel bottomPanel2;
        private System.Windows.Forms.ComboBox DefaultImageInterpolation;
        private System.Windows.Forms.CheckBox AutoLibRetro;
        private System.Windows.Forms.ComboBox LanguageList;
        private System.Windows.Forms.CheckBox AutoOpenFolder;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox n64003;
        private System.Windows.Forms.CheckBox n64002;
        private System.Windows.Forms.CheckBox n64001;
        private System.Windows.Forms.CheckBox n64000;
        private System.Windows.Forms.GroupBox n64004;
        private System.Windows.Forms.ComboBox ROMCType;
        private System.Windows.Forms.Button DownloadBanners;
        private System.Windows.Forms.TreeView TreeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel leftSeparator;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton FNANDLoader_vWii;
        private System.Windows.Forms.RadioButton FNANDLoader_Wii;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton FStorage_SD;
        private System.Windows.Forms.RadioButton FStorage_USB;
    }
}