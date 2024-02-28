
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
            this.line2 = new System.Windows.Forms.Panel();
            this.DefaultImageInterpolation = new System.Windows.Forms.ComboBox();
            this.AutoOpenFolder = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LanguageList = new System.Windows.Forms.ComboBox();
            this.AutoLibRetro = new System.Windows.Forms.CheckBox();
            this.line1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.leftSeparator = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.n64004.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.bottomPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // line2
            // 
            this.line2.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.line2, "line2");
            this.line2.Name = "line2";
            // 
            // DefaultImageInterpolation
            // 
            this.DefaultImageInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultImageInterpolation.FormattingEnabled = true;
            this.DefaultImageInterpolation.Items.AddRange(new object[] {
            resources.GetString("DefaultImageInterpolation.Items")});
            resources.ApplyResources(this.DefaultImageInterpolation, "DefaultImageInterpolation");
            this.DefaultImageInterpolation.Name = "DefaultImageInterpolation";
            // 
            // AutoOpenFolder
            // 
            resources.ApplyResources(this.AutoOpenFolder, "AutoOpenFolder");
            this.AutoOpenFolder.Name = "AutoOpenFolder";
            this.AutoOpenFolder.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // line1
            // 
            this.line1.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.line1, "line1");
            this.line1.Name = "line1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.n64003);
            this.groupBox1.Controls.Add(this.n64002);
            this.groupBox1.Controls.Add(this.n64001);
            this.groupBox1.Controls.Add(this.n64000);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            resources.ApplyResources(this.n64004, "n64004");
            this.n64004.Name = "n64004";
            this.n64004.TabStop = false;
            // 
            // ROMCType
            // 
            this.ROMCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ROMCType.FormattingEnabled = true;
            resources.ApplyResources(this.ROMCType, "ROMCType");
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
            this.TreeView.FullRowSelect = true;
            this.TreeView.ItemHeight = 20;
            resources.ApplyResources(this.TreeView, "TreeView");
            this.TreeView.Name = "TreeView";
            this.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("TreeView.Nodes1")))});
            this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DownloadBanners);
            this.panel1.Controls.Add(this.line1);
            this.panel1.Controls.Add(this.AutoOpenFolder);
            this.panel1.Controls.Add(this.LanguageList);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.n64004);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.DefaultImageInterpolation);
            this.panel2.Controls.Add(this.line2);
            this.panel2.Controls.Add(this.AutoLibRetro);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // leftSeparator
            // 
            this.leftSeparator.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.leftSeparator, "leftSeparator");
            this.leftSeparator.Name = "leftSeparator";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.Cancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.leftSeparator);
            this.Controls.Add(this.TreeView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Loading);
            this.Shown += new System.EventHandler(this.Loading);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.n64004.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Panel bottomPanel1;
        private System.Windows.Forms.Panel bottomPanel2;
        private System.Windows.Forms.ComboBox DefaultImageInterpolation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox AutoLibRetro;
        private System.Windows.Forms.Panel line1;
        private System.Windows.Forms.ComboBox LanguageList;
        private System.Windows.Forms.CheckBox AutoOpenFolder;
        private System.Windows.Forms.Panel line2;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Button button1;
    }
}