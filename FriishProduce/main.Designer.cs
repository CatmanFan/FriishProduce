
namespace FriishProduce
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.settings = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.finish = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.ComboBox();
            this.page1 = new System.Windows.Forms.Panel();
            this.page2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.DeleteBase = new System.Windows.Forms.Button();
            this.AddBase = new System.Windows.Forms.Button();
            this.baseList = new System.Windows.Forms.ComboBox();
            this.openROM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseROM = new System.Windows.Forms.OpenFileDialog();
            this.BrowseWAD = new System.Windows.Forms.OpenFileDialog();
            this.SaveWAD = new System.Windows.Forms.SaveFileDialog();
            this.page3 = new System.Windows.Forms.Panel();
            this.customize = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.page4 = new System.Windows.Forms.Panel();
            this.disableEmanual = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Options_NES = new System.Windows.Forms.Panel();
            this.NES_Palette = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Banner = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.TitleID = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.page1.SuspendLayout();
            this.page2.SuspendLayout();
            this.page3.SuspendLayout();
            this.page4.SuspendLayout();
            this.Options_NES.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel1.Controls.Add(this.settings);
            this.panel1.Controls.Add(this.back);
            this.panel1.Controls.Add(this.finish);
            this.panel1.Controls.Add(this.next);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // settings
            // 
            this.settings.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.settings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.settings, "settings");
            this.settings.Name = "settings";
            this.settings.UseVisualStyleBackColor = true;
            // 
            // back
            // 
            resources.ApplyResources(this.back, "back");
            this.back.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.back.Name = "back";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.Back_Click);
            // 
            // finish
            // 
            resources.ApplyResources(this.finish, "finish");
            this.finish.Name = "finish";
            this.finish.UseVisualStyleBackColor = true;
            this.finish.Click += new System.EventHandler(this.Finish_Click);
            // 
            // next
            // 
            resources.ApplyResources(this.next, "next");
            this.next.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.next.Name = "next";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.Next_Click);
            // 
            // console
            // 
            this.console.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.console, "console");
            this.console.FormattingEnabled = true;
            this.console.Items.AddRange(new object[] {
            resources.GetString("console.Items"),
            resources.GetString("console.Items1"),
            resources.GetString("console.Items2")});
            this.console.Name = "console";
            this.console.SelectedIndexChanged += new System.EventHandler(this.Console_Changed);
            // 
            // page1
            // 
            this.page1.Controls.Add(this.console);
            this.page1.Controls.Add(this.label1);
            resources.ApplyResources(this.page1, "page1");
            this.page1.Name = "page1";
            // 
            // page2
            // 
            this.page2.Controls.Add(this.label3);
            this.page2.Controls.Add(this.DeleteBase);
            this.page2.Controls.Add(this.AddBase);
            this.page2.Controls.Add(this.baseList);
            this.page2.Controls.Add(this.openROM);
            this.page2.Controls.Add(this.label2);
            resources.ApplyResources(this.page2, "page2");
            this.page2.Name = "page2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // DeleteBase
            // 
            resources.ApplyResources(this.DeleteBase, "DeleteBase");
            this.DeleteBase.Name = "DeleteBase";
            this.DeleteBase.UseVisualStyleBackColor = true;
            this.DeleteBase.Click += new System.EventHandler(this.DeleteWAD);
            // 
            // AddBase
            // 
            resources.ApplyResources(this.AddBase, "AddBase");
            this.AddBase.Name = "AddBase";
            this.AddBase.UseVisualStyleBackColor = true;
            this.AddBase.Click += new System.EventHandler(this.AddWAD);
            // 
            // baseList
            // 
            this.baseList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baseList.FormattingEnabled = true;
            resources.ApplyResources(this.baseList, "baseList");
            this.baseList.Name = "baseList";
            this.baseList.SelectedIndexChanged += new System.EventHandler(this.BaseList_Changed);
            // 
            // openROM
            // 
            resources.ApplyResources(this.openROM, "openROM");
            this.openROM.Name = "openROM";
            this.openROM.UseVisualStyleBackColor = true;
            this.openROM.Click += new System.EventHandler(this.OpenROM_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // SaveWAD
            // 
            this.SaveWAD.DefaultExt = "wad";
            resources.ApplyResources(this.SaveWAD, "SaveWAD");
            // 
            // page3
            // 
            this.page3.Controls.Add(this.Banner);
            this.page3.Controls.Add(this.customize);
            this.page3.Controls.Add(this.label5);
            resources.ApplyResources(this.page3, "page3");
            this.page3.Name = "page3";
            // 
            // customize
            // 
            resources.ApplyResources(this.customize, "customize");
            this.customize.Name = "customize";
            this.customize.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // page4
            // 
            this.page4.Controls.Add(this.TitleID);
            this.page4.Controls.Add(this.label7);
            this.page4.Controls.Add(this.disableEmanual);
            this.page4.Controls.Add(this.label4);
            this.page4.Controls.Add(this.Options_NES);
            resources.ApplyResources(this.page4, "page4");
            this.page4.Name = "page4";
            // 
            // disableEmanual
            // 
            resources.ApplyResources(this.disableEmanual, "disableEmanual");
            this.disableEmanual.Name = "disableEmanual";
            this.disableEmanual.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // Options_NES
            // 
            this.Options_NES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Options_NES.Controls.Add(this.NES_Palette);
            this.Options_NES.Controls.Add(this.label6);
            resources.ApplyResources(this.Options_NES, "Options_NES");
            this.Options_NES.Name = "Options_NES";
            // 
            // NES_Palette
            // 
            this.NES_Palette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NES_Palette.FormattingEnabled = true;
            this.NES_Palette.Items.AddRange(new object[] {
            resources.GetString("NES_Palette.Items"),
            resources.GetString("NES_Palette.Items1")});
            resources.ApplyResources(this.NES_Palette, "NES_Palette");
            this.NES_Palette.Name = "NES_Palette";
            this.NES_Palette.SelectedIndexChanged += new System.EventHandler(this.NES_PaletteChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // Banner
            // 
            this.Banner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Banner, "Banner");
            this.Banner.Name = "Banner";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // TitleID
            // 
            this.TitleID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.TitleID, "TitleID");
            this.TitleID.Name = "TitleID";
            this.TitleID.TextChanged += new System.EventHandler(this.TitleID_Changed);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.page4);
            this.Controls.Add(this.page3);
            this.Controls.Add(this.page2);
            this.Controls.Add(this.page1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.panel1.ResumeLayout(false);
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            this.page2.ResumeLayout(false);
            this.page2.PerformLayout();
            this.page3.ResumeLayout(false);
            this.page3.PerformLayout();
            this.page4.ResumeLayout(false);
            this.page4.PerformLayout();
            this.Options_NES.ResumeLayout(false);
            this.Options_NES.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox console;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button settings;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Panel page1;
        private System.Windows.Forms.Panel page2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button openROM;
        private System.Windows.Forms.OpenFileDialog BrowseROM;
        private System.Windows.Forms.OpenFileDialog BrowseWAD;
        private System.Windows.Forms.SaveFileDialog SaveWAD;
        private System.Windows.Forms.ComboBox baseList;
        private System.Windows.Forms.Button AddBase;
        private System.Windows.Forms.Button finish;
        private System.Windows.Forms.Button DeleteBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel page3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel page4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox disableEmanual;
        private System.Windows.Forms.CheckBox customize;
        private System.Windows.Forms.Panel Options_NES;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox NES_Palette;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Panel Banner;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TitleID;
    }
}

