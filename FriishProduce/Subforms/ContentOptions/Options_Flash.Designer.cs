
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
            this.quality = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vff_cache_size_l = new System.Windows.Forms.Label();
            this.vff_cache_size = new System.Windows.Forms.ComboBox();
            this.persistent_storage_per_movie_l = new System.Windows.Forms.Label();
            this.persistent_storage_per_movie = new System.Windows.Forms.ComboBox();
            this.persistent_storage_total_l = new System.Windows.Forms.Label();
            this.persistent_storage_total = new System.Windows.Forms.ComboBox();
            this.save_data_enable = new System.Windows.Forms.CheckBox();
            this.display = new System.Windows.Forms.GroupBox();
            this.stretch_to_4_3 = new System.Windows.Forms.CheckBox();
            this.update_frame_rate_l = new System.Windows.Forms.Label();
            this.update_frame_rate = new System.Windows.Forms.NumericUpDown();
            this.quality_l = new System.Windows.Forms.Label();
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
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.update_frame_rate)).BeginInit();
            this.controls.SuspendLayout();
            this.strap_reminder.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(622, 8);
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(516, 8);
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.Size = new System.Drawing.Size(734, 41);
            // 
            // b_controller
            // 
            this.b_controller.Size = new System.Drawing.Size(690, 24);
            // 
            // controller_box
            // 
            this.controller_box.Location = new System.Drawing.Point(12, 260);
            this.controller_box.Size = new System.Drawing.Size(710, 54);
            // 
            // quality
            // 
            this.quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quality.FormattingEnabled = true;
            this.quality.Items.AddRange(new object[] {
            "auto"});
            this.quality.Location = new System.Drawing.Point(15, 36);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(150, 21);
            this.quality.TabIndex = 15;
            this.quality.Tag = "quality";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vff_cache_size_l);
            this.groupBox1.Controls.Add(this.vff_cache_size);
            this.groupBox1.Controls.Add(this.persistent_storage_per_movie_l);
            this.groupBox1.Controls.Add(this.persistent_storage_per_movie);
            this.groupBox1.Controls.Add(this.persistent_storage_total_l);
            this.groupBox1.Controls.Add(this.persistent_storage_total);
            this.groupBox1.Controls.Add(this.save_data_enable);
            this.groupBox1.Location = new System.Drawing.Point(370, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 146);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "save_data";
            this.groupBox1.Text = "save_data";
            // 
            // vff_cache_size_l
            // 
            this.vff_cache_size_l.AutoSize = true;
            this.vff_cache_size_l.Location = new System.Drawing.Point(7, 43);
            this.vff_cache_size_l.Name = "vff_cache_size_l";
            this.vff_cache_size_l.Size = new System.Drawing.Size(79, 13);
            this.vff_cache_size_l.TabIndex = 23;
            this.vff_cache_size_l.Tag = "vff_cache_size";
            this.vff_cache_size_l.Text = "vff_cache_size";
            // 
            // vff_cache_size
            // 
            this.vff_cache_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vff_cache_size.FormattingEnabled = true;
            this.vff_cache_size.Items.AddRange(new object[] {
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
            "4096"});
            this.vff_cache_size.Location = new System.Drawing.Point(15, 60);
            this.vff_cache_size.Name = "vff_cache_size";
            this.vff_cache_size.Size = new System.Drawing.Size(150, 21);
            this.vff_cache_size.TabIndex = 22;
            // 
            // persistent_storage_per_movie_l
            // 
            this.persistent_storage_per_movie_l.Location = new System.Drawing.Point(183, 79);
            this.persistent_storage_per_movie_l.Name = "persistent_storage_per_movie_l";
            this.persistent_storage_per_movie_l.Size = new System.Drawing.Size(160, 30);
            this.persistent_storage_per_movie_l.TabIndex = 28;
            this.persistent_storage_per_movie_l.Tag = "persistent_storage_per_movie";
            this.persistent_storage_per_movie_l.Text = "persistent_storage_per_movie";
            this.persistent_storage_per_movie_l.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // persistent_storage_per_movie
            // 
            this.persistent_storage_per_movie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.persistent_storage_per_movie.FormattingEnabled = true;
            this.persistent_storage_per_movie.Items.AddRange(new object[] {
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
            "4096"});
            this.persistent_storage_per_movie.Location = new System.Drawing.Point(191, 113);
            this.persistent_storage_per_movie.Name = "persistent_storage_per_movie";
            this.persistent_storage_per_movie.Size = new System.Drawing.Size(150, 21);
            this.persistent_storage_per_movie.TabIndex = 27;
            // 
            // persistent_storage_total_l
            // 
            this.persistent_storage_total_l.Location = new System.Drawing.Point(7, 79);
            this.persistent_storage_total_l.Name = "persistent_storage_total_l";
            this.persistent_storage_total_l.Size = new System.Drawing.Size(160, 30);
            this.persistent_storage_total_l.TabIndex = 26;
            this.persistent_storage_total_l.Tag = "persistent_storage_total";
            this.persistent_storage_total_l.Text = "persistent_storage_total";
            this.persistent_storage_total_l.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // persistent_storage_total
            // 
            this.persistent_storage_total.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.persistent_storage_total.FormattingEnabled = true;
            this.persistent_storage_total.Items.AddRange(new object[] {
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
            "4096"});
            this.persistent_storage_total.Location = new System.Drawing.Point(15, 113);
            this.persistent_storage_total.Name = "persistent_storage_total";
            this.persistent_storage_total.Size = new System.Drawing.Size(150, 21);
            this.persistent_storage_total.TabIndex = 25;
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
            // display
            // 
            this.display.Controls.Add(this.stretch_to_4_3);
            this.display.Controls.Add(this.update_frame_rate_l);
            this.display.Controls.Add(this.update_frame_rate);
            this.display.Controls.Add(this.quality_l);
            this.display.Controls.Add(this.quality);
            this.display.Location = new System.Drawing.Point(12, 68);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(352, 90);
            this.display.TabIndex = 17;
            this.display.TabStop = false;
            this.display.Tag = "display";
            this.display.Text = "display";
            // 
            // stretch_to_4_3
            // 
            this.stretch_to_4_3.AutoSize = true;
            this.stretch_to_4_3.Location = new System.Drawing.Point(10, 64);
            this.stretch_to_4_3.Name = "stretch_to_4_3";
            this.stretch_to_4_3.Size = new System.Drawing.Size(100, 17);
            this.stretch_to_4_3.TabIndex = 18;
            this.stretch_to_4_3.Tag = "stretch_to_4_3";
            this.stretch_to_4_3.Text = "stretch_to_4_3";
            this.stretch_to_4_3.UseVisualStyleBackColor = true;
            // 
            // update_frame_rate_l
            // 
            this.update_frame_rate_l.AutoSize = true;
            this.update_frame_rate_l.Location = new System.Drawing.Point(183, 18);
            this.update_frame_rate_l.Name = "update_frame_rate_l";
            this.update_frame_rate_l.Size = new System.Drawing.Size(101, 13);
            this.update_frame_rate_l.TabIndex = 17;
            this.update_frame_rate_l.Tag = "update_frame_rate";
            this.update_frame_rate_l.Text = "update_frame_rate";
            // 
            // update_frame_rate
            // 
            this.update_frame_rate.Location = new System.Drawing.Point(191, 36);
            this.update_frame_rate.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.update_frame_rate.Name = "update_frame_rate";
            this.update_frame_rate.Size = new System.Drawing.Size(150, 21);
            this.update_frame_rate.TabIndex = 0;
            // 
            // quality_l
            // 
            this.quality_l.AutoSize = true;
            this.quality_l.Location = new System.Drawing.Point(7, 18);
            this.quality_l.Name = "quality_l";
            this.quality_l.Size = new System.Drawing.Size(39, 13);
            this.quality_l.TabIndex = 16;
            this.quality_l.Tag = "quality";
            this.quality_l.Text = "quality";
            // 
            // controls
            // 
            this.controls.Controls.Add(this.midi);
            this.controls.Controls.Add(this.qwerty_keyboard);
            this.controls.Controls.Add(this.mouse);
            this.controls.Location = new System.Drawing.Point(12, 168);
            this.controls.Name = "controls";
            this.controls.Size = new System.Drawing.Size(710, 86);
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
            this.strap_reminder.Location = new System.Drawing.Point(12, 12);
            this.strap_reminder.Name = "strap_reminder";
            this.strap_reminder.Size = new System.Drawing.Size(352, 50);
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
            this.strap_reminder_list.Size = new System.Drawing.Size(333, 21);
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
            this.ClientSize = new System.Drawing.Size(734, 371);
            this.Controls.Add(this.strap_reminder);
            this.Controls.Add(this.controls);
            this.Controls.Add(this.display);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options_Flash";
            this.Tag = "adobe_flash";
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.controls, 0);
            this.Controls.SetChildIndex(this.strap_reminder, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.display.ResumeLayout(false);
            this.display.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.update_frame_rate)).EndInit();
            this.controls.ResumeLayout(false);
            this.controls.PerformLayout();
            this.strap_reminder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox quality;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox vff_cache_size;
        private System.Windows.Forms.Label vff_cache_size_l;
        private System.Windows.Forms.CheckBox save_data_enable;
        private System.Windows.Forms.GroupBox display;
        private System.Windows.Forms.GroupBox controls;
        private System.Windows.Forms.CheckBox qwerty_keyboard;
        private System.Windows.Forms.CheckBox mouse;
        private System.Windows.Forms.GroupBox strap_reminder;
        private System.Windows.Forms.ComboBox strap_reminder_list;
        private System.Windows.Forms.CheckBox midi;
        private System.Windows.Forms.OpenFileDialog ImportDLS;
        private System.Windows.Forms.Label persistent_storage_total_l;
        private System.Windows.Forms.ComboBox persistent_storage_total;
        private System.Windows.Forms.Label persistent_storage_per_movie_l;
        private System.Windows.Forms.ComboBox persistent_storage_per_movie;
        private System.Windows.Forms.NumericUpDown update_frame_rate;
        private System.Windows.Forms.Label quality_l;
        private System.Windows.Forms.Label update_frame_rate_l;
        private System.Windows.Forms.CheckBox stretch_to_4_3;
    }
}
