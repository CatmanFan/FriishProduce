
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.s000 = new System.Windows.Forms.TabPage();
            this.LanguageList = new System.Windows.Forms.ComboBox();
            this.s006 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.s001 = new System.Windows.Forms.Label();
            this.s004 = new System.Windows.Forms.TabPage();
            this.imageintpl = new System.Windows.Forms.ComboBox();
            this.s005 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.s000.SuspendLayout();
            this.s004.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.s000);
            this.tabControl1.Controls.Add(this.s004);
            this.tabControl1.Location = new System.Drawing.Point(17, 14);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(400, 345);
            this.tabControl1.TabIndex = 0;
            // 
            // s000
            // 
            this.s000.BackColor = System.Drawing.SystemColors.Window;
            this.s000.Controls.Add(this.LanguageList);
            this.s000.Controls.Add(this.s006);
            this.s000.Controls.Add(this.panel2);
            this.s000.Controls.Add(this.s001);
            this.s000.Location = new System.Drawing.Point(4, 22);
            this.s000.Name = "s000";
            this.s000.Padding = new System.Windows.Forms.Padding(3);
            this.s000.Size = new System.Drawing.Size(392, 319);
            this.s000.TabIndex = 0;
            this.s000.Tag = "s000";
            this.s000.Text = "Application";
            // 
            // LanguageList
            // 
            this.LanguageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageList.FormattingEnabled = true;
            this.LanguageList.Location = new System.Drawing.Point(81, 11);
            this.LanguageList.Name = "LanguageList";
            this.LanguageList.Size = new System.Drawing.Size(300, 21);
            this.LanguageList.TabIndex = 10;
            // 
            // s006
            // 
            this.s006.AutoSize = true;
            this.s006.Location = new System.Drawing.Point(14, 53);
            this.s006.Name = "s006";
            this.s006.Size = new System.Drawing.Size(250, 17);
            this.s006.TabIndex = 9;
            this.s006.Text = "Automatically download from LibRetro database";
            this.s006.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Location = new System.Drawing.Point(14, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(367, 1);
            this.panel2.TabIndex = 8;
            // 
            // s001
            // 
            this.s001.AutoSize = true;
            this.s001.Location = new System.Drawing.Point(10, 15);
            this.s001.Name = "s001";
            this.s001.Size = new System.Drawing.Size(58, 13);
            this.s001.TabIndex = 0;
            this.s001.Tag = "";
            this.s001.Text = "Language:";
            this.s001.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // s004
            // 
            this.s004.BackColor = System.Drawing.SystemColors.Window;
            this.s004.Controls.Add(this.imageintpl);
            this.s004.Controls.Add(this.s005);
            this.s004.Location = new System.Drawing.Point(4, 22);
            this.s004.Name = "s004";
            this.s004.Size = new System.Drawing.Size(392, 319);
            this.s004.TabIndex = 1;
            this.s004.Text = "Default settings";
            // 
            // imageintpl
            // 
            this.imageintpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageintpl.FormattingEnabled = true;
            this.imageintpl.Items.AddRange(new object[] {
            "null"});
            this.imageintpl.Location = new System.Drawing.Point(13, 34);
            this.imageintpl.Name = "imageintpl";
            this.imageintpl.Size = new System.Drawing.Size(367, 21);
            this.imageintpl.TabIndex = 5;
            // 
            // s005
            // 
            this.s005.AutoSize = true;
            this.s005.Location = new System.Drawing.Point(10, 15);
            this.s005.Name = "s005";
            this.s005.Size = new System.Drawing.Size(99, 13);
            this.s005.TabIndex = 4;
            this.s005.Tag = "";
            this.s005.Text = "Image interpolation:";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(212, 12);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(100, 23);
            this.OK.TabIndex = 1;
            this.OK.Tag = "";
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(322, 12);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(100, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Tag = "";
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.Cancel);
            this.panel1.Controls.Add(this.OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 47);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 373);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(434, 48);
            this.panel3.TabIndex = 3;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(434, 421);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "g001";
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.s000.ResumeLayout(false);
            this.s000.PerformLayout();
            this.s004.ResumeLayout(false);
            this.s004.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage s000;
        private System.Windows.Forms.Label s001;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabPage s004;
        private System.Windows.Forms.ComboBox imageintpl;
        private System.Windows.Forms.Label s005;
        private System.Windows.Forms.CheckBox s006;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox LanguageList;
    }
}