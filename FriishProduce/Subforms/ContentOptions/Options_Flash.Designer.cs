
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
            this.groupBox1 = new FriishProduce.GroupBoxEx();
            this.vff_sync_on_write = new System.Windows.Forms.CheckBox();
            this.vff_cache_size_l = new System.Windows.Forms.Label();
            this.vff_cache_size = new System.Windows.Forms.ComboBox();
            this.persistent_storage_per_movie_l = new System.Windows.Forms.Label();
            this.persistent_storage_per_movie = new System.Windows.Forms.ComboBox();
            this.persistent_storage_total_l = new System.Windows.Forms.Label();
            this.persistent_storage_total = new System.Windows.Forms.ComboBox();
            this.save_data_enable = new System.Windows.Forms.CheckBox();
            this.display = new FriishProduce.GroupBoxEx();
            this.anti_aliasing = new System.Windows.Forms.CheckBox();
            this.fullscreen = new System.Windows.Forms.CheckBox();
            this.zoom_vl = new System.Windows.Forms.Label();
            this.quality_l = new System.Windows.Forms.Label();
            this.zoom_hl = new System.Windows.Forms.Label();
            this.zoom_v = new FriishProduce.NumericUpDownEx();
            this.zoom_h = new FriishProduce.NumericUpDownEx();
            this.controls = new FriishProduce.GroupBoxEx();
            this.midi = new System.Windows.Forms.CheckBox();
            this.qwerty_keyboard = new System.Windows.Forms.CheckBox();
            this.mouse = new System.Windows.Forms.CheckBox();
            this.background_color_img = new System.Windows.Forms.PictureBox();
            this.strap_reminder = new FriishProduce.GroupBoxEx();
            this.strap_reminder_list = new System.Windows.Forms.ComboBox();
            this.ImportDLS = new System.Windows.Forms.OpenFileDialog();
            this.BGColor = new System.Windows.Forms.ColorDialog();
            this.swf_metadata = new FriishProduce.GroupBoxEx();
            this.background_color = new System.Windows.Forms.TextBox();
            this.background_color_l = new System.Windows.Forms.Label();
            this.content_domain_l = new System.Windows.Forms.Label();
            this.content_domain = new FriishProduce.PlaceholderTextBox();
            this.zoom = new FriishProduce.GroupBoxEx();
            this.zoom_list = new System.Windows.Forms.ComboBox();
            this.bottomPanel1.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoom_v)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom_h)).BeginInit();
            this.controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.background_color_img)).BeginInit();
            this.strap_reminder.SuspendLayout();
            this.swf_metadata.SuspendLayout();
            this.zoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_controller
            // 
            this.b_controller.Size = new System.Drawing.Size(319, 24);
            // 
            // controller_box
            // 
            this.controller_box.Location = new System.Drawing.Point(12, 203);
            this.controller_box.Size = new System.Drawing.Size(360, 54);
            // 
            // quality
            // 
            this.quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quality.FormattingEnabled = true;
            this.quality.Items.AddRange(new object[] {
            "auto"});
            this.quality.Location = new System.Drawing.Point(10, 37);
            this.quality.Name = "quality";
            this.quality.Size = new System.Drawing.Size(340, 21);
            this.quality.TabIndex = 15;
            this.quality.Tag = "quality";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vff_sync_on_write);
            this.groupBox1.Controls.Add(this.vff_cache_size_l);
            this.groupBox1.Controls.Add(this.vff_cache_size);
            this.groupBox1.Controls.Add(this.persistent_storage_per_movie_l);
            this.groupBox1.Controls.Add(this.persistent_storage_per_movie);
            this.groupBox1.Controls.Add(this.persistent_storage_total_l);
            this.groupBox1.Controls.Add(this.persistent_storage_total);
            this.groupBox1.Controls.Add(this.save_data_enable);
            this.groupBox1.Flat = false;
            this.groupBox1.Location = new System.Drawing.Point(378, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 170);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "save_data";
            this.groupBox1.Text = "save_data";
            // 
            // vff_sync_on_write
            // 
            this.vff_sync_on_write.AutoSize = true;
            this.vff_sync_on_write.Location = new System.Drawing.Point(10, 41);
            this.vff_sync_on_write.Name = "vff_sync_on_write";
            this.vff_sync_on_write.Size = new System.Drawing.Size(116, 17);
            this.vff_sync_on_write.TabIndex = 29;
            this.vff_sync_on_write.Tag = "vff_sync_on_write";
            this.vff_sync_on_write.Text = "vff_sync_on_write";
            this.vff_sync_on_write.UseVisualStyleBackColor = true;
            // 
            // vff_cache_size_l
            // 
            this.vff_cache_size_l.AutoSize = true;
            this.vff_cache_size_l.Location = new System.Drawing.Point(12, 67);
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
            this.vff_cache_size.Location = new System.Drawing.Point(15, 83);
            this.vff_cache_size.Name = "vff_cache_size";
            this.vff_cache_size.Size = new System.Drawing.Size(152, 21);
            this.vff_cache_size.TabIndex = 22;
            // 
            // persistent_storage_per_movie_l
            // 
            this.persistent_storage_per_movie_l.Location = new System.Drawing.Point(185, 102);
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
            this.persistent_storage_per_movie.Location = new System.Drawing.Point(193, 136);
            this.persistent_storage_per_movie.Name = "persistent_storage_per_movie";
            this.persistent_storage_per_movie.Size = new System.Drawing.Size(152, 21);
            this.persistent_storage_per_movie.TabIndex = 27;
            // 
            // persistent_storage_total_l
            // 
            this.persistent_storage_total_l.Location = new System.Drawing.Point(12, 103);
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
            this.persistent_storage_total.Location = new System.Drawing.Point(15, 136);
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
            this.display.Controls.Add(this.anti_aliasing);
            this.display.Controls.Add(this.quality_l);
            this.display.Controls.Add(this.quality);
            this.display.Flat = false;
            this.display.Location = new System.Drawing.Point(12, 12);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(360, 90);
            this.display.TabIndex = 17;
            this.display.TabStop = false;
            this.display.Tag = "display";
            this.display.Text = "display";
            // 
            // anti_aliasing
            // 
            this.anti_aliasing.AutoSize = true;
            this.anti_aliasing.Location = new System.Drawing.Point(10, 64);
            this.anti_aliasing.Name = "anti_aliasing";
            this.anti_aliasing.Size = new System.Drawing.Size(85, 17);
            this.anti_aliasing.TabIndex = 19;
            this.anti_aliasing.Tag = "anti_aliasing";
            this.anti_aliasing.Text = "anti_aliasing";
            this.anti_aliasing.UseVisualStyleBackColor = true;
            // 
            // fullscreen
            // 
            this.fullscreen.AutoSize = true;
            this.fullscreen.Location = new System.Drawing.Point(10, 47);
            this.fullscreen.Name = "fullscreen";
            this.fullscreen.Size = new System.Drawing.Size(72, 17);
            this.fullscreen.TabIndex = 18;
            this.fullscreen.Tag = "fullscreen";
            this.fullscreen.Text = "fullscreen";
            this.fullscreen.UseVisualStyleBackColor = true;
            // 
            // zoom_vl
            // 
            this.zoom_vl.AutoSize = true;
            this.zoom_vl.Location = new System.Drawing.Point(280, 47);
            this.zoom_vl.Name = "zoom_vl";
            this.zoom_vl.Size = new System.Drawing.Size(13, 13);
            this.zoom_vl.TabIndex = 44;
            this.zoom_vl.Text = "V";
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
            // zoom_hl
            // 
            this.zoom_hl.AutoSize = true;
            this.zoom_hl.Location = new System.Drawing.Point(201, 47);
            this.zoom_hl.Name = "zoom_hl";
            this.zoom_hl.Size = new System.Drawing.Size(14, 13);
            this.zoom_hl.TabIndex = 43;
            this.zoom_hl.Text = "H";
            // 
            // zoom_v
            // 
            this.zoom_v.Location = new System.Drawing.Point(295, 45);
            this.zoom_v.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.zoom_v.Name = "zoom_v";
            this.zoom_v.Size = new System.Drawing.Size(55, 21);
            this.zoom_v.Suffix = "%";
            this.zoom_v.TabIndex = 42;
            this.zoom_v.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.zoom_v.ValueChanged += new System.EventHandler(this.valueChanged);
            // 
            // zoom_h
            // 
            this.zoom_h.Location = new System.Drawing.Point(216, 45);
            this.zoom_h.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.zoom_h.Name = "zoom_h";
            this.zoom_h.Size = new System.Drawing.Size(55, 21);
            this.zoom_h.Suffix = "%";
            this.zoom_h.TabIndex = 40;
            this.zoom_h.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.zoom_h.ValueChanged += new System.EventHandler(this.valueChanged);
            // 
            // controls
            // 
            this.controls.Controls.Add(this.midi);
            this.controls.Controls.Add(this.qwerty_keyboard);
            this.controls.Controls.Add(this.mouse);
            this.controls.Flat = false;
            this.controls.Location = new System.Drawing.Point(12, 108);
            this.controls.Name = "controls";
            this.controls.Size = new System.Drawing.Size(360, 89);
            this.controls.TabIndex = 18;
            this.controls.TabStop = false;
            this.controls.Tag = "controls";
            this.controls.Text = "controls";
            // 
            // midi
            // 
            this.midi.AutoSize = true;
            this.midi.Location = new System.Drawing.Point(10, 64);
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
            this.qwerty_keyboard.Location = new System.Drawing.Point(10, 42);
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
            this.mouse.Location = new System.Drawing.Point(10, 20);
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
            this.strap_reminder.Flat = false;
            this.strap_reminder.Location = new System.Drawing.Point(378, 302);
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
            this.swf_metadata.Flat = false;
            this.swf_metadata.Location = new System.Drawing.Point(378, 186);
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
            // zoom
            // 
            this.zoom.Controls.Add(this.zoom_list);
            this.zoom.Controls.Add(this.fullscreen);
            this.zoom.Controls.Add(this.zoom_h);
            this.zoom.Controls.Add(this.zoom_v);
            this.zoom.Controls.Add(this.zoom_hl);
            this.zoom.Controls.Add(this.zoom_vl);
            this.zoom.Location = new System.Drawing.Point(12, 263);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(360, 75);
            this.zoom.TabIndex = 45;
            this.zoom.TabStop = false;
            this.zoom.Tag = "zoom";
            this.zoom.Text = "zoom";
            // 
            // zoom_list
            // 
            this.zoom_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoom_list.FormattingEnabled = true;
            this.zoom_list.Items.AddRange(new object[] {
            "auto"});
            this.zoom_list.Location = new System.Drawing.Point(10, 18);
            this.zoom_list.Name = "zoom_list";
            this.zoom_list.Size = new System.Drawing.Size(340, 21);
            this.zoom_list.TabIndex = 17;
            this.zoom_list.Tag = "zoom";
            this.zoom_list.SelectedIndexChanged += new System.EventHandler(this.valueChanged);
            // 
            // Options_Flash
            // 
            this.ClientSize = new System.Drawing.Size(747, 407);
            this.Controls.Add(this.swf_metadata);
            this.Controls.Add(this.strap_reminder);
            this.Controls.Add(this.controls);
            this.Controls.Add(this.display);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.zoom);
            this.Name = "Options_Flash";
            this.Tag = "adobe_flash";
            this.Controls.SetChildIndex(this.zoom, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.display, 0);
            this.Controls.SetChildIndex(this.controls, 0);
            this.Controls.SetChildIndex(this.strap_reminder, 0);
            this.Controls.SetChildIndex(this.swf_metadata, 0);
            this.Controls.SetChildIndex(this.controller_box, 0);
            this.bottomPanel1.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.display.ResumeLayout(false);
            this.display.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoom_v)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoom_h)).EndInit();
            this.controls.ResumeLayout(false);
            this.controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.background_color_img)).EndInit();
            this.strap_reminder.ResumeLayout(false);
            this.swf_metadata.ResumeLayout(false);
            this.swf_metadata.PerformLayout();
            this.zoom.ResumeLayout(false);
            this.zoom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox quality;
        private GroupBoxEx groupBox1;
        private System.Windows.Forms.ComboBox vff_cache_size;
        private System.Windows.Forms.Label vff_cache_size_l;
        private System.Windows.Forms.CheckBox save_data_enable;
        private GroupBoxEx display;
        private GroupBoxEx controls;
        private System.Windows.Forms.CheckBox qwerty_keyboard;
        private System.Windows.Forms.CheckBox mouse;
        private GroupBoxEx strap_reminder;
        private System.Windows.Forms.ComboBox strap_reminder_list;
        private System.Windows.Forms.CheckBox midi;
        private System.Windows.Forms.OpenFileDialog ImportDLS;
        private System.Windows.Forms.Label persistent_storage_total_l;
        private System.Windows.Forms.ComboBox persistent_storage_total;
        private System.Windows.Forms.Label persistent_storage_per_movie_l;
        private System.Windows.Forms.ComboBox persistent_storage_per_movie;
        private System.Windows.Forms.Label quality_l;
        private System.Windows.Forms.CheckBox fullscreen;
        private System.Windows.Forms.ColorDialog BGColor;
        private System.Windows.Forms.PictureBox background_color_img;
        private GroupBoxEx swf_metadata;
        private PlaceholderTextBox content_domain;
        private System.Windows.Forms.Label content_domain_l;
        private System.Windows.Forms.TextBox background_color;
        private System.Windows.Forms.Label background_color_l;
        private System.Windows.Forms.CheckBox vff_sync_on_write;
        private System.Windows.Forms.CheckBox anti_aliasing;
        private NumericUpDownEx zoom_h;
        private NumericUpDownEx zoom_v;
        private System.Windows.Forms.Label zoom_hl;
        private System.Windows.Forms.Label zoom_vl;
        private FriishProduce.GroupBoxEx zoom;
        private System.Windows.Forms.ComboBox zoom_list;
    }
}
