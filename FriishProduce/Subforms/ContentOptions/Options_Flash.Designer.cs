
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
            this.fullscreen = new System.Windows.Forms.CheckBox();
            this.update_frame_rate_l = new System.Windows.Forms.Label();
            this.update_frame_rate = new System.Windows.Forms.NumericUpDown();
            this.quality_l = new System.Windows.Forms.Label();
            this.controls = new System.Windows.Forms.GroupBox();
            this.midi = new System.Windows.Forms.CheckBox();
            this.qwerty_keyboard = new System.Windows.Forms.CheckBox();
            this.mouse = new System.Windows.Forms.CheckBox();
            this.background_color_img = new System.Windows.Forms.PictureBox();
            this.strap_reminder = new System.Windows.Forms.GroupBox();
            this.strap_reminder_list = new System.Windows.Forms.ComboBox();
            this.ImportDLS = new System.Windows.Forms.OpenFileDialog();
            this.BGColor = new System.Windows.Forms.ColorDialog();
            this.swf_metadata = new System.Windows.Forms.GroupBox();
            this.background_color = new System.Windows.Forms.TextBox();
            this.background_color_l = new System.Windows.Forms.Label();
            this.content_domain_l = new System.Windows.Forms.Label();
            this.content_domain = new FriishProduce.PlaceholderTextBox();
            this.bottomPanel1.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.update_frame_rate)).BeginInit();
            this.controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.background_color_img)).BeginInit();
            this.strap_reminder.SuspendLayout();
            this.swf_metadata.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_controller
            // 
            this.b_controller.Size = new System.Drawing.Size(319, 24);
            // 
            // controller_box
            // 
            this.controller_box.Location = new System.Drawing.Point(12, 254);
            this.controller_box.Size = new System.Drawing.Size(360, 54);
            // 
            // quality
            // 
            this.quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quality.FormattingEnabled = true;
            this.quality.Items.AddRange(new object[] {
            "auto"});
            this.quality.Location = new System.Drawing.Point(15, 36);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(154, 21);
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
            this.groupBox1.Location = new System.Drawing.Point(378, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 146);
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
            "64",
            "72",
            "96",
            "128",
            "160",
            "192",
            "224",
            "256",
            "384",
            "512",
            "768",
            "1024",
            "1536",
            "2048",
            "3072",
            "4096"});
            this.vff_cache_size.Location = new System.Drawing.Point(15, 60);
            this.vff_cache_size.Name = "vff_cache_size";
            this.vff_cache_size.Size = new System.Drawing.Size(152, 21);
            this.vff_cache_size.TabIndex = 22;
            // 
            // persistent_storage_per_movie_l
            // 
            this.persistent_storage_per_movie_l.Location = new System.Drawing.Point(185, 79);
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
            "64",
            "72",
            "96",
            "128",
            "160",
            "192",
            "224",
            "256",
            "384",
            "512",
            "768",
            "1024",
            "1536",
            "2048",
            "3072",
            "4096"});
            this.persistent_storage_per_movie.Location = new System.Drawing.Point(193, 113);
            this.persistent_storage_per_movie.Name = "persistent_storage_per_movie";
            this.persistent_storage_per_movie.Size = new System.Drawing.Size(152, 21);
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
            "64",
            "72",
            "96",
            "128",
            "160",
            "192",
            "224",
            "256",
            "384",
            "512",
            "768",
            "1024",
            "1536",
            "2048",
            "3072",
            "4096"});
            this.persistent_storage_total.Location = new System.Drawing.Point(15, 113);
            this.persistent_storage_total.Name = "persistent_storage_total";
            this.persistent_storage_total.Size = new System.Drawing.Size(152, 21);
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
            this.save_data_enable.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // display
            // 
            this.display.Controls.Add(this.fullscreen);
            this.display.Controls.Add(this.update_frame_rate_l);
            this.display.Controls.Add(this.update_frame_rate);
            this.display.Controls.Add(this.quality_l);
            this.display.Controls.Add(this.quality);
            this.display.Location = new System.Drawing.Point(12, 66);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(360, 90);
            this.display.TabIndex = 17;
            this.display.TabStop = false;
            this.display.Tag = "display";
            this.display.Text = "display";
            // 
            // fullscreen
            // 
            this.fullscreen.AutoSize = true;
            this.fullscreen.Location = new System.Drawing.Point(10, 64);
            this.fullscreen.Name = "fullscreen";
            this.fullscreen.Size = new System.Drawing.Size(72, 17);
            this.fullscreen.TabIndex = 18;
            this.fullscreen.Tag = "fullscreen";
            this.fullscreen.Text = "fullscreen";
            this.fullscreen.UseVisualStyleBackColor = true;
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
            this.update_frame_rate.Size = new System.Drawing.Size(154, 21);
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
            this.controls.Location = new System.Drawing.Point(12, 162);
            this.controls.Name = "controls";
            this.controls.Size = new System.Drawing.Size(360, 86);
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
            this.midi.CheckedChanged += new System.EventHandler(this.valueChanged);
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
            // background_color_img
            // 
            this.background_color_img.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.background_color_img.Cursor = System.Windows.Forms.Cursors.Hand;
            this.background_color_img.Location = new System.Drawing.Point(311, 79);
            this.background_color_img.Name = "background_color_img";
            this.background_color_img.Size = new System.Drawing.Size(34, 20);
            this.background_color_img.TabIndex = 17;
            this.background_color_img.TabStop = false;
            this.background_color_img.Click += new System.EventHandler(this.changeBackgroundColor);
            // 
            // strap_reminder
            // 
            this.strap_reminder.Controls.Add(this.strap_reminder_list);
            this.strap_reminder.Location = new System.Drawing.Point(12, 10);
            this.strap_reminder.Name = "strap_reminder";
            this.strap_reminder.Size = new System.Drawing.Size(360, 50);
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
            this.strap_reminder_list.Size = new System.Drawing.Size(340, 21);
            this.strap_reminder_list.TabIndex = 16;
            this.strap_reminder_list.Tag = "strap_reminder";
            // 
            // ImportDLS
            // 
            this.ImportDLS.DefaultExt = "dls";
            this.ImportDLS.Filter = ".dls (*.dls)|*.dls";
            // 
            // swf_metadata
            // 
            this.swf_metadata.Controls.Add(this.background_color);
            this.swf_metadata.Controls.Add(this.background_color_l);
            this.swf_metadata.Controls.Add(this.content_domain_l);
            this.swf_metadata.Controls.Add(this.background_color_img);
            this.swf_metadata.Controls.Add(this.content_domain);
            this.swf_metadata.Location = new System.Drawing.Point(378, 162);
            this.swf_metadata.Name = "swf_metadata";
            this.swf_metadata.Size = new System.Drawing.Size(360, 110);
            this.swf_metadata.TabIndex = 39;
            this.swf_metadata.TabStop = false;
            this.swf_metadata.Tag = "swf_metadata";
            this.swf_metadata.Text = "swf_metadata";
            // 
            // background_color
            // 
            this.background_color.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.background_color.Location = new System.Drawing.Point(15, 79);
            this.background_color.MaxLength = 6;
            this.background_color.Name = "background_color";
            this.background_color.Size = new System.Drawing.Size(290, 21);
            this.background_color.TabIndex = 22;
            this.background_color.TextChanged += new System.EventHandler(this.valueChanged);
            // 
            // background_color_l
            // 
            this.background_color_l.AutoSize = true;
            this.background_color_l.Location = new System.Drawing.Point(7, 61);
            this.background_color_l.Name = "background_color_l";
            this.background_color_l.Size = new System.Drawing.Size(92, 13);
            this.background_color_l.TabIndex = 20;
            this.background_color_l.Tag = "background_color";
            this.background_color_l.Text = "background_color";
            // 
            // content_domain_l
            // 
            this.content_domain_l.AutoSize = true;
            this.content_domain_l.Location = new System.Drawing.Point(7, 18);
            this.content_domain_l.Name = "content_domain_l";
            this.content_domain_l.Size = new System.Drawing.Size(84, 13);
            this.content_domain_l.TabIndex = 19;
            this.content_domain_l.Tag = "content_domain";
            this.content_domain_l.Text = "content_domain";
            // 
            // content_domain
            // 
            this.content_domain.Location = new System.Drawing.Point(15, 36);
            this.content_domain.Name = "content_domain";
            this.content_domain.PlaceholderText = "file:///trusted/";
            this.content_domain.Size = new System.Drawing.Size(330, 21);
            this.content_domain.TabIndex = 0;
            // 
            // Options_Flash
            // 
            this.ClientSize = new System.Drawing.Size(751, 366);
            this.Controls.Add(this.swf_metadata);
            this.Controls.Add(this.strap_reminder);
            this.Controls.Add(this.controls);
            this.Controls.Add(this.display);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options_Flash";
            this.Tag = "adobe_flash";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.controls, 0);
            this.Controls.SetChildIndex(this.strap_reminder, 0);
            this.Controls.SetChildIndex(this.swf_metadata, 0);
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel1.PerformLayout();
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.display.ResumeLayout(false);
            this.display.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.update_frame_rate)).EndInit();
            this.controls.ResumeLayout(false);
            this.controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.background_color_img)).EndInit();
            this.strap_reminder.ResumeLayout(false);
            this.swf_metadata.ResumeLayout(false);
            this.swf_metadata.PerformLayout();
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
        private System.Windows.Forms.CheckBox fullscreen;
        private System.Windows.Forms.ColorDialog BGColor;
        private System.Windows.Forms.PictureBox background_color_img;
        private System.Windows.Forms.GroupBox swf_metadata;
        private PlaceholderTextBox content_domain;
        private System.Windows.Forms.Label content_domain_l;
        private System.Windows.Forms.TextBox background_color;
        private System.Windows.Forms.Label background_color_l;
    }
}
