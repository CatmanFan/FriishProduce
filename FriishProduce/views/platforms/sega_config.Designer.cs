
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
            this.console_brightness_value = new System.Windows.Forms.TrackBar();
            this.console_brightness = new System.Windows.Forms.CheckBox();
            this.console_country = new System.Windows.Forms.ComboBox();
            this.SEGA_Country = new System.Windows.Forms.Label();
            this.mdpad_6b = new System.Windows.Forms.CheckBox();
            this.console_savesram = new System.Windows.Forms.CheckBox();
            this.controller = new System.Windows.Forms.CheckBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel.SuspendLayout();
            this.p_console.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console_brightness_value)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.panel.Controls.Add(this.OK);
            this.panel.Controls.Add(this.Cancel);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 371);
            this.panel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(639, 50);
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
            this.OK.Location = new System.Drawing.Point(415, 12);
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
            this.Cancel.Location = new System.Drawing.Point(520, 12);
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
            this.console.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.console.Location = new System.Drawing.Point(12, 12);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(100, 25);
            this.console.TabIndex = 5;
            this.console.Tag = "section";
            this.console.Text = "console";
            this.console.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.console.UseVisualStyleBackColor = true;
            // 
            // p_console
            // 
            this.p_console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.p_console.Controls.Add(this.console_disable_resetbutton);
            this.p_console.Controls.Add(this.console_brightness_value);
            this.p_console.Controls.Add(this.console_brightness);
            this.p_console.Controls.Add(this.console_country);
            this.p_console.Controls.Add(this.SEGA_Country);
            this.p_console.Controls.Add(this.mdpad_6b);
            this.p_console.Controls.Add(this.console_savesram);
            this.p_console.Controls.Add(this.controller);
            this.p_console.Dock = System.Windows.Forms.DockStyle.Right;
            this.p_console.Location = new System.Drawing.Point(122, 0);
            this.p_console.Name = "p_console";
            this.p_console.Size = new System.Drawing.Size(517, 371);
            this.p_console.TabIndex = 6;
            this.p_console.Tag = "page";
            // 
            // console_disable_resetbutton
            // 
            this.console_disable_resetbutton.AutoSize = true;
            this.console_disable_resetbutton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_disable_resetbutton.Location = new System.Drawing.Point(21, 91);
            this.console_disable_resetbutton.Name = "console_disable_resetbutton";
            this.console_disable_resetbutton.Size = new System.Drawing.Size(129, 19);
            this.console_disable_resetbutton.TabIndex = 19;
            this.console_disable_resetbutton.Tag = "";
            this.console_disable_resetbutton.Text = "disable_resetbutton";
            this.ToolTip.SetToolTip(this.console_disable_resetbutton, "☐ = Disabled | ☑ = Enabled");
            this.console_disable_resetbutton.UseVisualStyleBackColor = true;
            // 
            // console_brightness_value
            // 
            this.console_brightness_value.AutoSize = false;
            this.console_brightness_value.Enabled = false;
            this.console_brightness_value.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_brightness_value.Location = new System.Drawing.Point(35, 137);
            this.console_brightness_value.Maximum = 100;
            this.console_brightness_value.Name = "console_brightness_value";
            this.console_brightness_value.Size = new System.Drawing.Size(248, 24);
            this.console_brightness_value.TabIndex = 18;
            this.console_brightness_value.TickFrequency = 5;
            this.console_brightness_value.Value = 87;
            // 
            // console_brightness
            // 
            this.console_brightness.AutoSize = true;
            this.console_brightness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_brightness.Location = new System.Drawing.Point(21, 116);
            this.console_brightness.Name = "console_brightness";
            this.console_brightness.Size = new System.Drawing.Size(125, 19);
            this.console_brightness.TabIndex = 17;
            this.console_brightness.Tag = "";
            this.console_brightness.Text = "console.brightness";
            this.ToolTip.SetToolTip(this.console_brightness, "☐ = Disabled | ☑ = Enabled\r\nSets screen brightness (HOME Menu is not affected).");
            this.console_brightness.UseVisualStyleBackColor = true;
            this.console_brightness.CheckedChanged += new System.EventHandler(this.BrightnessToggled);
            // 
            // console_country
            // 
            this.console_country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.console_country.FormattingEnabled = true;
            this.console_country.Items.AddRange(new object[] {
            "us",
            "eu",
            "jp"});
            this.console_country.Location = new System.Drawing.Point(75, 14);
            this.console_country.Name = "console_country";
            this.console_country.Size = new System.Drawing.Size(81, 23);
            this.console_country.TabIndex = 16;
            this.ToolTip.SetToolTip(this.console_country, "[us] = USA/North America\r\n[eu] = Europe\r\n[jp] = Japan\r\nSets region of emulated co" +
        "nsole.");
            // 
            // SEGA_Country
            // 
            this.SEGA_Country.AutoSize = true;
            this.SEGA_Country.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SEGA_Country.Location = new System.Drawing.Point(18, 17);
            this.SEGA_Country.Name = "SEGA_Country";
            this.SEGA_Country.Size = new System.Drawing.Size(51, 15);
            this.SEGA_Country.TabIndex = 15;
            this.SEGA_Country.Text = "country:";
            // 
            // mdpad_6b
            // 
            this.mdpad_6b.AutoSize = true;
            this.mdpad_6b.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mdpad_6b.Location = new System.Drawing.Point(21, 66);
            this.mdpad_6b.Name = "mdpad_6b";
            this.mdpad_6b.Size = new System.Drawing.Size(142, 19);
            this.mdpad_6b.TabIndex = 14;
            this.mdpad_6b.Tag = "";
            this.mdpad_6b.Text = "dev.mdpad.enable_6b";
            this.ToolTip.SetToolTip(this.mdpad_6b, "☐ = Disabled | ☑ = Enabled for Comix Zone\r\nToggles 6 button use in Mega Drive/Gen" +
        "esis.\r\n");
            this.mdpad_6b.UseVisualStyleBackColor = true;
            // 
            // console_savesram
            // 
            this.console_savesram.AutoSize = true;
            this.console_savesram.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.console_savesram.Location = new System.Drawing.Point(21, 42);
            this.console_savesram.Name = "console_savesram";
            this.console_savesram.Size = new System.Drawing.Size(80, 19);
            this.console_savesram.TabIndex = 13;
            this.console_savesram.Tag = "";
            this.console_savesram.Text = "save_sram";
            this.ToolTip.SetToolTip(this.console_savesram, "☐ = Disabled | ☑ = Enabled for Phantasy Star, Sonic 3\r\nToggles SRAM.");
            this.console_savesram.UseVisualStyleBackColor = true;
            // 
            // controller
            // 
            this.controller.AutoSize = true;
            this.controller.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.controller.Location = new System.Drawing.Point(21, 305);
            this.controller.Name = "controller";
            this.controller.Size = new System.Drawing.Size(100, 49);
            this.controller.TabIndex = 0;
            this.controller.Text = "core_bindings\r\ncl_bindings\r\ngc_bindings";
            this.controller.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ToolTip.SetToolTip(this.controller, "Change controller bindings.");
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
            // SEGA_Config
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(639, 421);
            this.Controls.Add(this.p_console);
            this.Controls.Add(this.console);
            this.Controls.Add(this.panel);
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
        private System.Windows.Forms.ComboBox console_country;
        private System.Windows.Forms.Label SEGA_Country;
        private System.Windows.Forms.CheckBox mdpad_6b;
        private System.Windows.Forms.CheckBox console_savesram;
        private System.Windows.Forms.CheckBox console_disable_resetbutton;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}