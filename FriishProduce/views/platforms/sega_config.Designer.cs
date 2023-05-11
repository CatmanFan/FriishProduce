
namespace FriishProduce.Views
{
    partial class SEGA_Config
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
            this.panel = new System.Windows.Forms.Panel();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.CheckBox();
            this.p_console = new System.Windows.Forms.Panel();
            this.console_disable_resetbutton = new System.Windows.Forms.CheckBox();
            this.console_brightness = new System.Windows.Forms.CheckBox();
            this.country_l = new System.Windows.Forms.ComboBox();
            this.country = new System.Windows.Forms.Label();
            this.console_brightness_value = new System.Windows.Forms.TrackBar();
            this.mdpad_6b = new System.Windows.Forms.CheckBox();
            this.console_savesram = new System.Windows.Forms.CheckBox();
            this.controller = new System.Windows.Forms.CheckBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.nplayers = new System.Windows.Forms.CheckBox();
            this.use_4ptap = new System.Windows.Forms.CheckBox();
            this.disable_selectmenu = new System.Windows.Forms.CheckBox();
            this.smsui_opll = new System.Windows.Forms.CheckBox();
            this.control = new System.Windows.Forms.CheckBox();
            this.p_control = new System.Windows.Forms.Panel();
            this.use_4ptap_l = new System.Windows.Forms.ComboBox();
            this.nplayers_l = new System.Windows.Forms.ComboBox();
            this.etc = new System.Windows.Forms.CheckBox();
            this.p_etc = new System.Windows.Forms.Panel();
            this.warning = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.p_console.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness_value)).BeginInit();
            this.p_control.SuspendLayout();
            this.p_etc.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.panel.Controls.Add(this.OK);
            this.panel.Controls.Add(this.Cancel);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 361);
            this.panel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(554, 45);
            this.panel.TabIndex = 4;
            this.panel.Tag = "panel";
            // 
            // OK
            // 
            this.OK.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.OK.FlatAppearance.BorderSize = 0;
            this.OK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.OK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OK.Location = new System.Drawing.Point(339, 10);
            this.OK.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(98, 25);
            this.OK.TabIndex = 1;
            this.OK.Text = "ok";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.Cancel.FlatAppearance.BorderSize = 0;
            this.Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Cancel.Location = new System.Drawing.Point(444, 10);
            this.Cancel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(98, 25);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // console
            // 
            this.console.Appearance = System.Windows.Forms.Appearance.Button;
            this.console.AutoCheck = false;
            this.console.Checked = true;
            this.console.CheckState = System.Windows.Forms.CheckState.Checked;
            this.console.FlatAppearance.BorderSize = 0;
            this.console.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(115)))), ((int)(((byte)(175)))));
            this.console.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.console.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.console.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.console.Location = new System.Drawing.Point(12, 12);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(100, 25);
            this.console.TabIndex = 5;
            this.console.Tag = "section";
            this.console.Text = "console";
            this.console.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.console.UseVisualStyleBackColor = true;
            this.console.Click += new System.EventHandler(this.SwitchPanel);
            // 
            // p_console
            // 
            this.p_console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.p_console.Controls.Add(this.console_disable_resetbutton);
            this.p_console.Controls.Add(this.console_brightness);
            this.p_console.Controls.Add(this.country_l);
            this.p_console.Controls.Add(this.country);
            this.p_console.Controls.Add(this.console_brightness_value);
            this.p_console.Location = new System.Drawing.Point(122, 0);
            this.p_console.Name = "p_console";
            this.p_console.Size = new System.Drawing.Size(517, 360);
            this.p_console.TabIndex = 6;
            this.p_console.Tag = "page";
            // 
            // console_disable_resetbutton
            // 
            this.console_disable_resetbutton.AutoSize = true;
            this.console_disable_resetbutton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_disable_resetbutton.Location = new System.Drawing.Point(20, 45);
            this.console_disable_resetbutton.Name = "console_disable_resetbutton";
            this.console_disable_resetbutton.Size = new System.Drawing.Size(173, 19);
            this.console_disable_resetbutton.TabIndex = 19;
            this.console_disable_resetbutton.Tag = "";
            this.console_disable_resetbutton.Text = "console.disable_resetbutton";
            this.ToolTip.SetToolTip(this.console_disable_resetbutton, "☐ = Disabled | ☑ = Enabled");
            this.console_disable_resetbutton.UseVisualStyleBackColor = true;
            // 
            // console_brightness
            // 
            this.console_brightness.AutoSize = true;
            this.console_brightness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_brightness.Location = new System.Drawing.Point(20, 70);
            this.console_brightness.Name = "console_brightness";
            this.console_brightness.Size = new System.Drawing.Size(125, 19);
            this.console_brightness.TabIndex = 17;
            this.console_brightness.Tag = "";
            this.console_brightness.Text = "console.brightness";
            this.ToolTip.SetToolTip(this.console_brightness, "☐ = Disabled | ☑ = Enabled\r\nSets screen brightness (HOME Menu is not affected).");
            this.console_brightness.UseVisualStyleBackColor = true;
            this.console_brightness.CheckedChanged += new System.EventHandler(this.BrightnessToggled);
            // 
            // country_l
            // 
            this.country_l.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.country_l.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.country_l.FormattingEnabled = true;
            this.country_l.Items.AddRange(new object[] {
            "us",
            "eu",
            "jp"});
            this.country_l.Location = new System.Drawing.Point(74, 15);
            this.country_l.Name = "country_l";
            this.country_l.Size = new System.Drawing.Size(75, 21);
            this.country_l.TabIndex = 16;
            this.ToolTip.SetToolTip(this.country_l, "[us] = USA/North America\r\n[eu] = Europe\r\n[jp] = Japan\r\nSets region of emulated co" +
        "nsole.");
            // 
            // country
            // 
            this.country.AutoSize = true;
            this.country.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.country.Location = new System.Drawing.Point(17, 17);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(51, 15);
            this.country.TabIndex = 15;
            this.country.Text = "country:";
            // 
            // console_brightness_value
            // 
            this.console_brightness_value.AutoSize = false;
            this.console_brightness_value.Enabled = false;
            this.console_brightness_value.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_brightness_value.Location = new System.Drawing.Point(34, 91);
            this.console_brightness_value.Maximum = 100;
            this.console_brightness_value.Name = "console_brightness_value";
            this.console_brightness_value.Size = new System.Drawing.Size(248, 24);
            this.console_brightness_value.TabIndex = 18;
            this.console_brightness_value.TickFrequency = 5;
            this.console_brightness_value.Value = 87;
            // 
            // mdpad_6b
            // 
            this.mdpad_6b.AutoSize = true;
            this.mdpad_6b.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mdpad_6b.Location = new System.Drawing.Point(14, 45);
            this.mdpad_6b.Name = "mdpad_6b";
            this.mdpad_6b.Size = new System.Drawing.Size(142, 19);
            this.mdpad_6b.TabIndex = 14;
            this.mdpad_6b.Tag = "";
            this.mdpad_6b.Text = "dev.mdpad.enable_6b";
            this.ToolTip.SetToolTip(this.mdpad_6b, "☐ = Disabled | ☑ = Enabled for Comix Zone\r\nToggles 6 button use in Mega Drive/Gen" +
        "esis.");
            this.mdpad_6b.UseVisualStyleBackColor = true;
            // 
            // console_savesram
            // 
            this.console_savesram.AutoSize = true;
            this.console_savesram.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_savesram.Location = new System.Drawing.Point(20, 16);
            this.console_savesram.Name = "console_savesram";
            this.console_savesram.Size = new System.Drawing.Size(80, 19);
            this.console_savesram.TabIndex = 13;
            this.console_savesram.Tag = "";
            this.console_savesram.Text = "save_sram";
            this.ToolTip.SetToolTip(this.console_savesram, "☐ = Disabled | ☑ = Enabled\r\nToggles SRAM.\r\nUsed in Phantasy Star, Sonic 3");
            this.console_savesram.UseVisualStyleBackColor = true;
            // 
            // controller
            // 
            this.controller.Appearance = System.Windows.Forms.Appearance.Button;
            this.controller.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.controller.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.controller.Location = new System.Drawing.Point(14, 12);
            this.controller.Name = "controller";
            this.controller.Size = new System.Drawing.Size(406, 25);
            this.controller.TabIndex = 0;
            this.controller.Tag = "Flash__004";
            this.controller.Text = "controllers";
            this.controller.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.controller.UseVisualStyleBackColor = true;
            this.controller.CheckedChanged += new System.EventHandler(this.ControllerChecked);
            // 
            // ToolTip
            // 
            this.ToolTip.AutoPopDelay = 5000;
            this.ToolTip.BackColor = System.Drawing.Color.LemonChiffon;
            this.ToolTip.ForeColor = System.Drawing.Color.Black;
            this.ToolTip.InitialDelay = 300;
            this.ToolTip.ReshowDelay = 100;
            // 
            // nplayers
            // 
            this.nplayers.AutoSize = true;
            this.nplayers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.nplayers.Location = new System.Drawing.Point(14, 95);
            this.nplayers.Name = "nplayers";
            this.nplayers.Size = new System.Drawing.Size(70, 19);
            this.nplayers.TabIndex = 20;
            this.nplayers.Tag = "";
            this.nplayers.Text = "nplayers";
            this.ToolTip.SetToolTip(this.nplayers, "☐ = Disabled | ☑ = Enabled\r\nSets num of controllers connected to emulated machine" +
        ".\r\nUsed in Columns III/Pengo");
            this.nplayers.UseVisualStyleBackColor = true;
            this.nplayers.CheckedChanged += new System.EventHandler(this.NPlayersToggled);
            // 
            // use_4ptap
            // 
            this.use_4ptap.AutoSize = true;
            this.use_4ptap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.use_4ptap.Location = new System.Drawing.Point(14, 70);
            this.use_4ptap.Name = "use_4ptap";
            this.use_4ptap.Size = new System.Drawing.Size(151, 19);
            this.use_4ptap.TabIndex = 21;
            this.use_4ptap.Tag = "";
            this.use_4ptap.Text = "machine_md.use_4ptap";
            this.ToolTip.SetToolTip(this.use_4ptap, "☐ = Disabled | ☑ = Enabled\r\nEnables 4player Multitap.\r\n\"1\" = Connected to Control" +
        "ler Port 1\r\n\"2\" = Connected to Controller Port 2\r\n\"3\" = Connected to Controller " +
        "Port 1 & 2\r\nUsed in Columns III/Pengo");
            this.use_4ptap.UseVisualStyleBackColor = true;
            this.use_4ptap.CheckedChanged += new System.EventHandler(this.Use4PTapToggled);
            // 
            // disable_selectmenu
            // 
            this.disable_selectmenu.AutoSize = true;
            this.disable_selectmenu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.disable_selectmenu.Location = new System.Drawing.Point(20, 41);
            this.disable_selectmenu.Name = "disable_selectmenu";
            this.disable_selectmenu.Size = new System.Drawing.Size(129, 19);
            this.disable_selectmenu.TabIndex = 14;
            this.disable_selectmenu.Tag = "";
            this.disable_selectmenu.Text = "disable_selectmenu";
            this.ToolTip.SetToolTip(this.disable_selectmenu, "☐ = Disabled | ☑ = Enabled\r\nToggles \"Select Menu\" (only has options for FM\r\nSound" +
        " Unit & switching functions of buttons 1 & 2).");
            this.disable_selectmenu.UseVisualStyleBackColor = true;
            // 
            // smsui_opll
            // 
            this.smsui_opll.AutoSize = true;
            this.smsui_opll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smsui_opll.Location = new System.Drawing.Point(20, 66);
            this.smsui_opll.Name = "smsui_opll";
            this.smsui_opll.Size = new System.Drawing.Size(103, 19);
            this.smsui_opll.TabIndex = 15;
            this.smsui_opll.Tag = "";
            this.smsui_opll.Text = "smsui.has_opll";
            this.ToolTip.SetToolTip(this.smsui_opll, "☐ = Disabled | ☑ = Enabled\r\nToggles \"Select Menu\" (only has options for FM Sound " +
        "Unit).");
            this.smsui_opll.UseVisualStyleBackColor = true;
            // 
            // control
            // 
            this.control.Appearance = System.Windows.Forms.Appearance.Button;
            this.control.AutoCheck = false;
            this.control.FlatAppearance.BorderSize = 0;
            this.control.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(115)))), ((int)(((byte)(175)))));
            this.control.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.control.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.control.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.control.Location = new System.Drawing.Point(12, 43);
            this.control.Name = "control";
            this.control.Size = new System.Drawing.Size(100, 25);
            this.control.TabIndex = 7;
            this.control.Tag = "section";
            this.control.Text = "control";
            this.control.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.control.UseVisualStyleBackColor = true;
            this.control.Click += new System.EventHandler(this.SwitchPanel);
            // 
            // p_control
            // 
            this.p_control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.p_control.Controls.Add(this.use_4ptap_l);
            this.p_control.Controls.Add(this.nplayers_l);
            this.p_control.Controls.Add(this.use_4ptap);
            this.p_control.Controls.Add(this.nplayers);
            this.p_control.Controls.Add(this.controller);
            this.p_control.Controls.Add(this.mdpad_6b);
            this.p_control.Location = new System.Drawing.Point(122, 0);
            this.p_control.Name = "p_control";
            this.p_control.Size = new System.Drawing.Size(517, 362);
            this.p_control.TabIndex = 20;
            this.p_control.Tag = "page";
            this.p_control.Visible = false;
            // 
            // use_4ptap_l
            // 
            this.use_4ptap_l.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.use_4ptap_l.Enabled = false;
            this.use_4ptap_l.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.use_4ptap_l.FormattingEnabled = true;
            this.use_4ptap_l.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.use_4ptap_l.Location = new System.Drawing.Point(183, 69);
            this.use_4ptap_l.Name = "use_4ptap_l";
            this.use_4ptap_l.Size = new System.Drawing.Size(75, 21);
            this.use_4ptap_l.TabIndex = 23;
            // 
            // nplayers_l
            // 
            this.nplayers_l.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nplayers_l.Enabled = false;
            this.nplayers_l.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nplayers_l.FormattingEnabled = true;
            this.nplayers_l.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.nplayers_l.Location = new System.Drawing.Point(183, 94);
            this.nplayers_l.Name = "nplayers_l";
            this.nplayers_l.Size = new System.Drawing.Size(75, 21);
            this.nplayers_l.TabIndex = 22;
            // 
            // etc
            // 
            this.etc.Appearance = System.Windows.Forms.Appearance.Button;
            this.etc.AutoCheck = false;
            this.etc.FlatAppearance.BorderSize = 0;
            this.etc.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(115)))), ((int)(((byte)(175)))));
            this.etc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.etc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.etc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.etc.Location = new System.Drawing.Point(12, 74);
            this.etc.Name = "etc";
            this.etc.Size = new System.Drawing.Size(100, 25);
            this.etc.TabIndex = 21;
            this.etc.Tag = "section";
            this.etc.Text = "etc";
            this.etc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.etc.UseVisualStyleBackColor = true;
            this.etc.Click += new System.EventHandler(this.SwitchPanel);
            // 
            // p_etc
            // 
            this.p_etc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.p_etc.Controls.Add(this.smsui_opll);
            this.p_etc.Controls.Add(this.disable_selectmenu);
            this.p_etc.Controls.Add(this.console_savesram);
            this.p_etc.Location = new System.Drawing.Point(122, 0);
            this.p_etc.Name = "p_etc";
            this.p_etc.Size = new System.Drawing.Size(517, 360);
            this.p_etc.TabIndex = 21;
            this.p_etc.Tag = "page";
            this.p_etc.Visible = false;
            // 
            // warning
            // 
            this.warning.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.warning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(65)))), ((int)(((byte)(45)))));
            this.warning.Location = new System.Drawing.Point(9, 173);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(107, 181);
            this.warning.TabIndex = 22;
            this.warning.Tag = "SEGA__ConWarn";
            this.warning.Text = "label1";
            this.warning.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SEGA_Config
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(554, 406);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.etc);
            this.Controls.Add(this.control);
            this.Controls.Add(this.console);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.p_control);
            this.Controls.Add(this.p_etc);
            this.Controls.Add(this.p_console);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SEGA_Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel.ResumeLayout(false);
            this.p_console.ResumeLayout(false);
            this.p_console.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness_value)).EndInit();
            this.p_control.ResumeLayout(false);
            this.p_control.PerformLayout();
            this.p_etc.ResumeLayout(false);
            this.p_etc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox console;
        private System.Windows.Forms.Panel p_console;
        private System.Windows.Forms.CheckBox controller;
        private System.Windows.Forms.TrackBar console_brightness_value;
        private System.Windows.Forms.CheckBox console_brightness;
        private System.Windows.Forms.ComboBox country_l;
        private System.Windows.Forms.Label country;
        private System.Windows.Forms.CheckBox mdpad_6b;
        private System.Windows.Forms.CheckBox console_savesram;
        private System.Windows.Forms.CheckBox console_disable_resetbutton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.CheckBox control;
        private System.Windows.Forms.Panel p_control;
        private System.Windows.Forms.CheckBox etc;
        private System.Windows.Forms.CheckBox nplayers;
        private System.Windows.Forms.Panel p_etc;
        private System.Windows.Forms.Label warning;
        private System.Windows.Forms.CheckBox use_4ptap;
        private System.Windows.Forms.ComboBox nplayers_l;
        private System.Windows.Forms.ComboBox use_4ptap_l;
        private System.Windows.Forms.CheckBox disable_selectmenu;
        private System.Windows.Forms.CheckBox smsui_opll;
    }
}