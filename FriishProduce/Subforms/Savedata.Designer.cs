namespace FriishProduce
{
    partial class Savedata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Savedata));
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.Fill = new System.Windows.Forms.CheckBox();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.TextBox();
            this.subtitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.htmlLabel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
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
            // Fill
            // 
            resources.ApplyResources(this.Fill, "Fill");
            this.Fill.Name = "Fill";
            this.Fill.Tag = "fill_save_data";
            this.Fill.UseVisualStyleBackColor = true;
            this.Fill.CheckedChanged += new System.EventHandler(this.Fill_CheckedChanged);
            // 
            // Picture
            // 
            this.Picture.BackgroundImage = global::FriishProduce.FileDatas.SaveImages.Icon_Nintendo;
            resources.ApplyResources(this.Picture, "Picture");
            this.Picture.Name = "Picture";
            this.Picture.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Tag = "edit_save_data_1";
            // 
            // title
            // 
            resources.ApplyResources(this.title, "title");
            this.title.Name = "title";
            // 
            // subtitle
            // 
            resources.ApplyResources(this.subtitle, "subtitle");
            this.subtitle.Name = "subtitle";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Tag = "edit_save_data_2";
            // 
            // htmlLabel1
            // 
            this.htmlLabel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel1.BaseStylesheet = "div { font-size: 11px !important; }";
            resources.ApplyResources(this.htmlLabel1, "htmlLabel1");
            this.htmlLabel1.Name = "htmlLabel1";
            this.htmlLabel1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // Savedata
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.b_cancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.htmlLabel1);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Fill);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.bottomPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Savedata";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.isClosing);
            this.Load += new System.EventHandler(this.isLoading);
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel2;
        protected System.Windows.Forms.Panel bottomPanel1;
        protected System.Windows.Forms.Button b_cancel;
        protected System.Windows.Forms.Button b_ok;
        public System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox title;
        internal System.Windows.Forms.TextBox subtitle;
        internal System.Windows.Forms.CheckBox Fill;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel htmlLabel1;
    }
}