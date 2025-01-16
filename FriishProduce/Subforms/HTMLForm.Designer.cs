
namespace FriishProduce
{
    partial class HTMLForm
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
            this.b_ok = new System.Windows.Forms.Button();
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            this.bottomPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel2.Location = new System.Drawing.Point(0, 330);
            this.bottomPanel2.Name = "bottomPanel2";
            this.bottomPanel2.Size = new System.Drawing.Size(444, 42);
            this.bottomPanel2.TabIndex = 26;
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel1.Controls.Add(this.b_ok);
            this.bottomPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel1.Location = new System.Drawing.Point(0, 1);
            this.bottomPanel1.Name = "bottomPanel1";
            this.bottomPanel1.Size = new System.Drawing.Size(444, 41);
            this.bottomPanel1.TabIndex = 3;
            // 
            // b_ok
            // 
            this.b_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ok.AutoSize = true;
            this.b_ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_ok.Location = new System.Drawing.Point(310, 9);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(122, 23);
            this.b_ok.TabIndex = 24;
            this.b_ok.Tag = "b_ok";
            this.b_ok.Text = "&OK";
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.AutoScrollMinSize = new System.Drawing.Size(420, 17);
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.htmlPanel1.BaseStylesheet = "div { font-size: 95% !important; }";
            this.htmlPanel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanel1.IsSelectionEnabled = false;
            this.htmlPanel1.Location = new System.Drawing.Point(12, 12);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(420, 306);
            this.htmlPanel1.TabIndex = 27;
            this.htmlPanel1.Text = "<div>Not implemented</div>";
            this.htmlPanel1.UseSystemCursors = true;
            // 
            // HTMLForm
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(444, 372);
            this.ControlBox = false;
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.bottomPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTMLForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel2;
        private System.Windows.Forms.Panel bottomPanel1;
        private System.Windows.Forms.Button b_ok;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
    }
}