﻿
namespace FriishProduce
{
    partial class BannerOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BannerOptions));
            this.players = new System.Windows.Forms.NumericUpDown();
            this.released = new System.Windows.Forms.NumericUpDown();
            this.title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.region = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.banner_sound = new System.Windows.Forms.Button();
            this.banner_sound_play = new System.Windows.Forms.Button();
            this.banner_sound_restore = new System.Windows.Forms.Button();
            this.browseSound = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.released)).BeginInit();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // players
            // 
            resources.ApplyResources(this.players, "players");
            this.players.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.players.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.players.Name = "players";
            this.players.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // released
            // 
            resources.ApplyResources(this.released, "released");
            this.released.Maximum = new decimal(new int[] {
            2029,
            0,
            0,
            0});
            this.released.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.released.Name = "released";
            this.released.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            // 
            // title
            // 
            resources.ApplyResources(this.title, "title");
            this.title.Name = "title";
            this.title.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.title.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Tag = "players";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Tag = "year";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Tag = "full_name";
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            resources.ApplyResources(this.bottomPanel2, "bottomPanel2");
            this.bottomPanel2.Name = "bottomPanel2";
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel1.Controls.Add(this.b_cancel);
            this.bottomPanel1.Controls.Add(this.b_ok);
            resources.ApplyResources(this.bottomPanel1, "bottomPanel1");
            this.bottomPanel1.Name = "bottomPanel1";
            // 
            // b_cancel
            // 
            resources.ApplyResources(this.b_cancel, "b_cancel");
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Tag = "b_cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // b_ok
            // 
            resources.ApplyResources(this.b_ok, "b_ok");
            this.b_ok.Name = "b_ok";
            this.b_ok.Tag = "b_ok";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.OK_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.Tag = "banner_region";
            // 
            // region
            // 
            this.region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.region.FormattingEnabled = true;
            resources.ApplyResources(this.region, "region");
            this.region.Name = "region";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // banner_sound
            // 
            resources.ApplyResources(this.banner_sound, "banner_sound");
            this.banner_sound.Name = "banner_sound";
            this.banner_sound.Tag = "banner_sound";
            this.banner_sound.UseVisualStyleBackColor = true;
            this.banner_sound.Click += new System.EventHandler(this.banner_sound_replace_Click);
            // 
            // banner_sound_play
            // 
            this.banner_sound_play.Image = global::FriishProduce.Properties.Resources.control_play;
            resources.ApplyResources(this.banner_sound_play, "banner_sound_play");
            this.banner_sound_play.Name = "banner_sound_play";
            this.banner_sound_play.UseVisualStyleBackColor = true;
            this.banner_sound_play.Click += new System.EventHandler(this.banner_sound_play_Click);
            // 
            // banner_sound_restore
            // 
            resources.ApplyResources(this.banner_sound_restore, "banner_sound_restore");
            this.banner_sound_restore.Name = "banner_sound_restore";
            this.banner_sound_restore.UseVisualStyleBackColor = true;
            this.banner_sound_restore.Click += new System.EventHandler(this.banner_sound_restore_Click);
            // 
            // browseSound
            // 
            resources.ApplyResources(this.browseSound, "browseSound");
            this.browseSound.RestoreDirectory = true;
            this.browseSound.SupportMultiDottedExtensions = true;
            // 
            // BannerOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.b_cancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.banner_sound_restore);
            this.Controls.Add(this.banner_sound_play);
            this.Controls.Add(this.banner_sound);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.region);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.players);
            this.Controls.Add(this.released);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BannerOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "banner";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Shown += new System.EventHandler(this.isShown);
            ((System.ComponentModel.ISupportInitialize)(this.players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.released)).EndInit();
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel bottomPanel2;
        protected System.Windows.Forms.Panel bottomPanel1;
        protected System.Windows.Forms.Button b_ok;
        internal System.Windows.Forms.NumericUpDown players;
        internal System.Windows.Forms.NumericUpDown released;
        internal System.Windows.Forms.TextBox title;
        protected System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox region;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button banner_sound;
        private System.Windows.Forms.Button banner_sound_play;
        private System.Windows.Forms.Button banner_sound_restore;
        private System.Windows.Forms.OpenFileDialog browseSound;
    }
}