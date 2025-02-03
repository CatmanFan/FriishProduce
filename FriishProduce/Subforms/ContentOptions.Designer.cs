
namespace FriishProduce
{
    partial class ContentOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentOptions));
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.b_controller = new System.Windows.Forms.Button();
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.controller_box = new System.Windows.Forms.GroupBox();
            this.controller_cb = new System.Windows.Forms.CheckBox();
            this.Tip = new TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip();
            this.bottomPanel1.SuspendLayout();
            this.bottomPanel2.SuspendLayout();
            this.controller_box.SuspendLayout();
            this.SuspendLayout();
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
            // 
            // b_ok
            // 
            resources.ApplyResources(this.b_ok, "b_ok");
            this.b_ok.Name = "b_ok";
            this.b_ok.Tag = "b_ok";
            this.b_ok.UseVisualStyleBackColor = true;
            // 
            // b_controller
            // 
            resources.ApplyResources(this.b_controller, "b_controller");
            this.b_controller.Name = "b_controller";
            this.b_controller.Tag = "controller_mapping";
            this.b_controller.UseVisualStyleBackColor = true;
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            resources.ApplyResources(this.bottomPanel2, "bottomPanel2");
            this.bottomPanel2.Name = "bottomPanel2";
            // 
            // controller_box
            // 
            this.controller_box.Controls.Add(this.controller_cb);
            this.controller_box.Controls.Add(this.b_controller);
            resources.ApplyResources(this.controller_box, "controller_box");
            this.controller_box.Name = "controller_box";
            this.controller_box.TabStop = false;
            this.controller_box.Tag = "controller";
            // 
            // controller_cb
            // 
            resources.ApplyResources(this.controller_cb, "controller_cb");
            this.controller_cb.Name = "controller_cb";
            this.controller_cb.UseVisualStyleBackColor = true;
            // 
            // Tip
            // 
            this.Tip.AllowLinksHandling = true;
            this.Tip.AutoPopDelay = 10000;
            this.Tip.BackColor = System.Drawing.Color.White;
            this.Tip.BaseStylesheet = "div { font-size: 11px !important; }";
            this.Tip.ForeColor = System.Drawing.Color.Black;
            this.Tip.InitialDelay = 500;
            this.Tip.MaximumSize = new System.Drawing.Size(350, 0);
            this.Tip.OwnerDraw = true;
            this.Tip.ReshowDelay = 100;
            this.Tip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.Tip.TooltipCssClass = "htmltooltip";
            this.Tip.UseGdiPlusTextRendering = true;
            // 
            // ContentOptions
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.b_cancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.controller_box);
            this.Controls.Add(this.bottomPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContentOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel2.ResumeLayout(false);
            this.controller_box.ResumeLayout(false);
            this.controller_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button b_cancel;
        protected System.Windows.Forms.Button b_ok;
        protected System.Windows.Forms.Panel bottomPanel1;
        protected System.Windows.Forms.Button b_controller;
        private System.Windows.Forms.Panel bottomPanel2;
        protected System.Windows.Forms.GroupBox controller_box;
        protected System.Windows.Forms.CheckBox controller_cb;
        protected TheArtOfDev.HtmlRenderer.WinForms.HtmlToolTip Tip;
    }
}