
namespace FriishProduce
{
    partial class UpdaterForm
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
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.b_no = new System.Windows.Forms.Button();
            this.b_yes = new System.Windows.Forms.Button();
            this.desc2 = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.wait = new System.Windows.Forms.PictureBox();
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.desc1 = new System.Windows.Forms.Label();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wait)).BeginInit();
            this.SuspendLayout();
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            this.bottomPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel2.Location = new System.Drawing.Point(0, 281);
            this.bottomPanel2.Name = "bottomPanel2";
            this.bottomPanel2.Size = new System.Drawing.Size(468, 41);
            this.bottomPanel2.TabIndex = 38;
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel1.Controls.Add(this.b_no);
            this.bottomPanel1.Controls.Add(this.b_yes);
            this.bottomPanel1.Controls.Add(this.desc2);
            this.bottomPanel1.Controls.Add(this.Progress);
            this.bottomPanel1.Controls.Add(this.wait);
            this.bottomPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel1.Location = new System.Drawing.Point(0, 1);
            this.bottomPanel1.Name = "bottomPanel1";
            this.bottomPanel1.Size = new System.Drawing.Size(468, 40);
            this.bottomPanel1.TabIndex = 12;
            // 
            // b_no
            // 
            this.b_no.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_no.AutoSize = true;
            this.b_no.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_no.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_no.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.b_no.Location = new System.Drawing.Point(419, 8);
            this.b_no.MaximumSize = new System.Drawing.Size(0, 24);
            this.b_no.MinimumSize = new System.Drawing.Size(0, 24);
            this.b_no.Name = "b_no";
            this.b_no.Size = new System.Drawing.Size(37, 24);
            this.b_no.TabIndex = 4;
            this.b_no.Tag = "b_no";
            this.b_no.Text = "&No";
            this.b_no.UseVisualStyleBackColor = true;
            // 
            // b_yes
            // 
            this.b_yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_yes.AutoSize = true;
            this.b_yes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_yes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.b_yes.Location = new System.Drawing.Point(378, 8);
            this.b_yes.MaximumSize = new System.Drawing.Size(0, 24);
            this.b_yes.MinimumSize = new System.Drawing.Size(0, 24);
            this.b_yes.Name = "b_yes";
            this.b_yes.Size = new System.Drawing.Size(39, 24);
            this.b_yes.TabIndex = 3;
            this.b_yes.Tag = "b_yes";
            this.b_yes.Text = "&Yes";
            this.b_yes.UseVisualStyleBackColor = true;
            this.b_yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // desc2
            // 
            this.desc2.AutoSize = true;
            this.desc2.Location = new System.Drawing.Point(9, 13);
            this.desc2.Name = "desc2";
            this.desc2.Size = new System.Drawing.Size(37, 15);
            this.desc2.TabIndex = 40;
            this.desc2.Tag = "desc2";
            this.desc2.Text = "desc2";
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(12, 13);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(444, 15);
            this.Progress.TabIndex = 5;
            this.Progress.Visible = false;
            // 
            // wait
            // 
            this.wait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.wait.Image = global::FriishProduce.Properties.Resources.loading;
            this.wait.Location = new System.Drawing.Point(428, 0);
            this.wait.Margin = new System.Windows.Forms.Padding(1);
            this.wait.Name = "wait";
            this.wait.Size = new System.Drawing.Size(39, 40);
            this.wait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.wait.TabIndex = 41;
            this.wait.TabStop = false;
            this.wait.Visible = false;
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.htmlPanel1.BaseStylesheet = "";
            this.htmlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.htmlPanel1.Location = new System.Drawing.Point(14, 52);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(440, 214);
            this.htmlPanel1.TabIndex = 39;
            this.htmlPanel1.Text = null;
            // 
            // desc1
            // 
            this.desc1.AutoSize = true;
            this.desc1.Location = new System.Drawing.Point(10, 12);
            this.desc1.Name = "desc1";
            this.desc1.Size = new System.Drawing.Size(21, 30);
            this.desc1.TabIndex = 40;
            this.desc1.Tag = "desc1";
            this.desc1.Text = "{0}\r\n{1}";
            // 
            // UpdaterForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(468, 322);
            this.Controls.Add(this.desc1);
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.bottomPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "updaterform";
            this.Text = "title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Updater_FormClosing);
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel bottomPanel2;
        protected System.Windows.Forms.Panel bottomPanel1;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label desc2;
        private System.Windows.Forms.Label desc1;
        private System.Windows.Forms.PictureBox wait;
        private System.Windows.Forms.Button b_no;
        private System.Windows.Forms.Button b_yes;
    }
}