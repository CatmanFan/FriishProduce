
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
            this.ResetAllDialogs = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.leftSeparator = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.toggleSwitchL1 = new System.Windows.Forms.Label();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.FStorage_SD = new System.Windows.Forms.RadioButton();
            this.FStorage_USB = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PaletteBanner = new System.Windows.Forms.CheckBox();
            this.PaletteList = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.NGBios = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.Sega6ButtonPad = new System.Windows.Forms.CheckBox();
            this.SegaSRAM = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrightnessValue = new System.Windows.Forms.TrackBar();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.n64004.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.bottomPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessValue)).BeginInit();
            this.groupBox7.SuspendLayout();
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
            this.panel1.Controls.Add(this.ResetAllDialogs);
            this.panel1.Controls.Add(this.AutoOpenFolder);
            this.panel1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ResetAllDialogs
            // 
            resources.ApplyResources(this.ResetAllDialogs, "ResetAllDialogs");
            this.ResetAllDialogs.Name = "ResetAllDialogs";
            this.ResetAllDialogs.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LanguageList);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Controls.Add(this.n64004);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
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
            this.groupBox9.Controls.Add(this.toggleSwitchL1);
            this.groupBox9.Controls.Add(this.toggleSwitch1);
            this.groupBox9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // toggleSwitchL1
            // 
            resources.ApplyResources(this.toggleSwitchL1, "toggleSwitchL1");
            this.toggleSwitchL1.Name = "toggleSwitchL1";
            // 
            // toggleSwitch1
            // 
            resources.ApplyResources(this.toggleSwitch1, "toggleSwitch1");
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.ToggleSwitchChanged);
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
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PaletteBanner);
            this.groupBox4.Controls.Add(this.PaletteList);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // PaletteBanner
            // 
            resources.ApplyResources(this.PaletteBanner, "PaletteBanner");
            this.PaletteBanner.Name = "PaletteBanner";
            this.PaletteBanner.UseVisualStyleBackColor = true;
            // 
            // PaletteList
            // 
            this.PaletteList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.PaletteList, "PaletteList");
            this.PaletteList.FormattingEnabled = true;
            this.PaletteList.Name = "PaletteList";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox5);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.NGBios);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // NGBios
            // 
            this.NGBios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.NGBios, "NGBios");
            this.NGBios.FormattingEnabled = true;
            this.NGBios.Name = "NGBios";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox7);
            this.panel6.Controls.Add(this.groupBox6);
            this.panel6.Controls.Add(this.SegaSRAM);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // Sega6ButtonPad
            // 
            resources.ApplyResources(this.Sega6ButtonPad, "Sega6ButtonPad");
            this.Sega6ButtonPad.Name = "Sega6ButtonPad";
            this.Sega6ButtonPad.UseVisualStyleBackColor = true;
            // 
            // SegaSRAM
            // 
            resources.ApplyResources(this.SegaSRAM, "SegaSRAM");
            this.SegaSRAM.Name = "SegaSRAM";
            this.SegaSRAM.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.BrightnessValue);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BrightnessValue
            // 
            resources.ApplyResources(this.BrightnessValue, "BrightnessValue");
            this.BrightnessValue.Maximum = 100;
            this.BrightnessValue.Name = "BrightnessValue";
            this.BrightnessValue.Value = 100;
            this.BrightnessValue.Scroll += new System.EventHandler(this.BrightnessValue_Scroll);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.Sega6ButtonPad);
            this.groupBox7.Controls.Add(this.comboBox1);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
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
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
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
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessValue)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
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
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel leftSeparator;
        private System.Windows.Forms.CheckBox ResetAllDialogs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton FStorage_SD;
        private System.Windows.Forms.RadioButton FStorage_USB;
        private System.Windows.Forms.Label toggleSwitchL1;
        private JCS.ToggleSwitch toggleSwitch1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox PaletteBanner;
        private System.Windows.Forms.ComboBox PaletteList;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox NGBios;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox Sega6ButtonPad;
        private System.Windows.Forms.CheckBox SegaSRAM;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar BrightnessValue;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}