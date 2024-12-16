
namespace FriishProduce
{
    partial class Options_Flash
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
            this.quality_list = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vff_sync_on_write = new System.Windows.Forms.CheckBox();
            this.save_data_enable = new System.Windows.Forms.CheckBox();
            this.vff_cache_size = new System.Windows.Forms.Label();
            this.vff_cache_size_list = new System.Windows.Forms.ComboBox();
            this.quality = new System.Windows.Forms.GroupBox();
            this.controls = new System.Windows.Forms.GroupBox();
            this.midi = new System.Windows.Forms.CheckBox();
            this.qwerty_keyboard = new System.Windows.Forms.CheckBox();
            this.mouse = new System.Windows.Forms.CheckBox();
            this.strap_reminder = new System.Windows.Forms.GroupBox();
            this.strap_reminder_list = new System.Windows.Forms.ComboBox();
            this.ImportDLS = new System.Windows.Forms.OpenFileDialog();
            this.bottomPanel1.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.quality.SuspendLayout();
            this.controls.SuspendLayout();
            this.strap_reminder.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_controller
            // 
            this.b_controller.Size = new System.Drawing.Size(510, 24);
            // 
            // controller_box
            // 
            this.controller_box.Location = new System.Drawing.Point(12, 313);
            this.controller_box.Size = new System.Drawing.Size(530, 54);
            // 
            // quality_list
            // 
            this.quality_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quality_list.FormattingEnabled = true;
            this.quality_list.Items.AddRange(new object[] {
            "auto"});
            this.quality_list.Location = new System.Drawing.Point(10, 18);
            this.quality_list.Name = "quality_list";
            this.quality_list.Size = new System.Drawing.Size(510, 21);
            this.quality_list.TabIndex = 15;
            this.quality_list.Tag = "quality";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vff_sync_on_write);
            this.groupBox1.Controls.Add(this.save_data_enable);
            this.groupBox1.Controls.Add(this.vff_cache_size);
            this.groupBox1.Controls.Add(this.vff_cache_size_list);
            this.groupBox1.Location = new System.Drawing.Point(12, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 70);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "save_data";
            this.groupBox1.Text = "save_data";
            // 
            // vff_sync_on_write
            // 
            this.vff_sync_on_write.AutoSize = true;
            this.vff_sync_on_write.Location = new System.Drawing.Point(10, 42);
            this.vff_sync_on_write.Name = "vff_sync_on_write";
            this.vff_sync_on_write.Size = new System.Drawing.Size(116, 17);
            this.vff_sync_on_write.TabIndex = 25;
            this.vff_sync_on_write.Tag = "vff_sync_on_write";
            this.vff_sync_on_write.Text = "vff_sync_on_write";
            this.vff_sync_on_write.UseVisualStyleBackColor = true;
            // 
            // save_data_enable
            // 
            this.save_data_enable.AutoSize = true;
            this.save_data_enable.Location = new System.Drawing.Point(10, 20);
            this.save_data_enable.Name = "save_data_enable";
            this.save_data_enable.Size = new System.Drawing.Size(115, 17);
            this.save_data_enable.TabIndex = 24;
            this.save_data_enable.Tag = "save_data_enable";
            this.save_data_enable.Text = "save_data_enable";
            this.save_data_enable.UseVisualStyleBackColor = true;
            this.save_data_enable.CheckedChanged += new System.EventHandler(this.checkBoxChanged);
            // 
            // vff_cache_size
            // 
            this.vff_cache_size.AutoSize = true;
            this.vff_cache_size.Location = new System.Drawing.Point(321, 20);
            this.vff_cache_size.Name = "vff_cache_size";
            this.vff_cache_size.Size = new System.Drawing.Size(79, 13);
            this.vff_cache_size.TabIndex = 23;
            this.vff_cache_size.Tag = "vff_cache_size";
            this.vff_cache_size.Text = "vff_cache_size";
            // 
            // vff_cache_size_list
            // 
            this.vff_cache_size_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vff_cache_size_list.FormattingEnabled = true;
            this.vff_cache_size_list.Items.AddRange(new object[] {
            "32",
            "48",
            "72",
            "96",
            "128",
            "160",
            "192",
            "224",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192"});
            this.vff_cache_size_list.Location = new System.Drawing.Point(324, 37);
            this.vff_cache_size_list.Name = "vff_cache_size_list";
            this.vff_cache_size_list.Size = new System.Drawing.Size(75, 21);
            this.vff_cache_size_list.TabIndex = 22;
            // 
            // quality
            // 
            this.quality.Controls.Add(this.quality_list);
            this.quality.Location = new System.Drawing.Point(12, 12);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(530, 50);
            this.quality.TabIndex = 17;
            this.quality.TabStop = false;
            this.quality.Tag = "quality";
            this.quality.Text = "quality";
            // 
            // controls
            // 
            this.controls.Controls.Add(this.midi);
            this.controls.Controls.Add(this.qwerty_keyboard);
            this.controls.Controls.Add(this.mouse);
            this.controls.Location = new System.Drawing.Point(12, 68);
            this.controls.Name = "controls";
            this.controls.Size = new System.Drawing.Size(530, 86);
            this.controls.TabIndex = 18;
            this.controls.TabStop = false;
            this.controls.Tag = "controls";
            this.controls.Text = "controls";
            // 
            // midi
            // 
            this.midi.AutoSize = true;
            this.midi.Location = new System.Drawing.Point(10, 61);
            this.midi.Name = "midi";
            this.midi.Size = new System.Drawing.Size(44, 17);
            this.midi.TabIndex = 15;
            this.midi.Tag = "midi";
            this.midi.Text = "midi";
            this.midi.UseVisualStyleBackColor = true;
            this.midi.CheckedChanged += new System.EventHandler(this.checkBoxChanged);
            // 
            // qwerty_keyboard
            // 
            this.qwerty_keyboard.AutoSize = true;
            this.qwerty_keyboard.Location = new System.Drawing.Point(10, 40);
            this.qwerty_keyboard.Name = "qwerty_keyboard";
            this.qwerty_keyboard.Size = new System.Drawing.Size(111, 17);
            this.qwerty_keyboard.TabIndex = 14;
            this.qwerty_keyboard.Tag = "qwerty_keyboard";
            this.qwerty_keyboard.Text = "qwerty_keyboard";
            this.qwerty_keyboard.UseVisualStyleBackColor = true;
            // 
            // mouse
            // 
            this.mouse.AutoSize = true;
            this.mouse.Location = new System.Drawing.Point(10, 19);
            this.mouse.Name = "mouse";
            this.mouse.Size = new System.Drawing.Size(57, 17);
            this.mouse.TabIndex = 13;
            this.mouse.Tag = "mouse";
            this.mouse.Text = "mouse";
            this.mouse.UseVisualStyleBackColor = true;
            // 
            // strap_reminder
            // 
            this.strap_reminder.Controls.Add(this.strap_reminder_list);
            this.strap_reminder.Location = new System.Drawing.Point(12, 236);
            this.strap_reminder.Name = "strap_reminder";
            this.strap_reminder.Size = new System.Drawing.Size(530, 50);
            this.strap_reminder.TabIndex = 19;
            this.strap_reminder.TabStop = false;
            this.strap_reminder.Tag = "strap_reminder";
            this.strap_reminder.Text = "strap_reminder";
            // 
            // strap_reminder_list
            // 
            this.strap_reminder_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.strap_reminder_list.FormattingEnabled = true;
            this.strap_reminder_list.Items.AddRange(new object[] {
            "auto"});
            this.strap_reminder_list.Location = new System.Drawing.Point(10, 18);
            this.strap_reminder_list.Name = "strap_reminder_list";
            this.strap_reminder_list.Size = new System.Drawing.Size(510, 21);
            this.strap_reminder_list.TabIndex = 16;
            this.strap_reminder_list.Tag = "strap_reminder";
            // 
            // ImportDLS
            // 
            this.ImportDLS.DefaultExt = "dls";
            this.ImportDLS.Filter = ".dls (*.dls)|*.dls";
            // 
            // Options_Flash
            // 
            this.ClientSize = new System.Drawing.Size(554, 422);
            this.Controls.Add(this.strap_reminder);
            this.Controls.Add(this.controls);
            this.Controls.Add(this.quality);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options_Flash";
            this.Tag = "adobe_flash";
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.quality, 0);
            this.Controls.SetChildIndex(this.controls, 0);
            this.Controls.SetChildIndex(this.strap_reminder, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.quality.ResumeLayout(false);
            this.controls.ResumeLayout(false);
            this.controls.PerformLayout();
            this.strap_reminder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox quality_list;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox vff_cache_size_list;
        private System.Windows.Forms.Label vff_cache_size;
        private System.Windows.Forms.CheckBox save_data_enable;
        private System.Windows.Forms.GroupBox quality;
        private System.Windows.Forms.GroupBox controls;
        private System.Windows.Forms.CheckBox qwerty_keyboard;
        private System.Windows.Forms.CheckBox mouse;
        private System.Windows.Forms.CheckBox vff_sync_on_write;
        private System.Windows.Forms.GroupBox strap_reminder;
        private System.Windows.Forms.ComboBox strap_reminder_list;
        private System.Windows.Forms.CheckBox midi;
        private System.Windows.Forms.OpenFileDialog ImportDLS;
    }
}
