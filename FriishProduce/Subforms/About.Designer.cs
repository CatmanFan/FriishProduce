
namespace FriishProduce
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.b_ok = new System.Windows.Forms.Button();
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelDescription, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 0, 4);
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.65174F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.018147F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.89084F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.16692F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.27235F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(610, 240);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Arial", 6F);
            this.labelCopyright.Location = new System.Drawing.Point(6, 81);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(473, 17);
            this.labelCopyright.TabIndex = 27;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::FriishProduce.Properties.Resources.logo;
            this.logoPictureBox.Location = new System.Drawing.Point(485, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 4);
            this.logoPictureBox.Size = new System.Drawing.Size(122, 99);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.labelProductName.Location = new System.Drawing.Point(6, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(473, 27);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDescription.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.labelDescription.Location = new System.Drawing.Point(6, 46);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(473, 35);
            this.labelDescription.TabIndex = 21;
            this.labelDescription.Text = "{0}\r\nCurrent language: {1} ({2})";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelVersion.Location = new System.Drawing.Point(6, 27);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(473, 17);
            this.labelVersion.TabIndex = 22;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            this.tableLayoutPanel.SetColumnSpan(this.textBoxDescription, 2);
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.textBoxDescription.ForeColor = System.Drawing.Color.White;
            this.textBoxDescription.Location = new System.Drawing.Point(6, 108);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(601, 129);
            this.textBoxDescription.TabIndex = 23;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = resources.GetString("textBoxDescription.Text");
            // 
            // b_ok
            // 
            this.b_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ok.AutoSize = true;
            this.b_ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.b_ok.Location = new System.Drawing.Point(500, 9);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(122, 23);
            this.b_ok.TabIndex = 24;
            this.b_ok.Tag = "b_ok";
            this.b_ok.Text = "&OK";
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            this.bottomPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel2.Location = new System.Drawing.Point(0, 268);
            this.bottomPanel2.Name = "bottomPanel2";
            this.bottomPanel2.Size = new System.Drawing.Size(634, 42);
            this.bottomPanel2.TabIndex = 25;
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bottomPanel1.Controls.Add(this.b_ok);
            this.bottomPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel1.Location = new System.Drawing.Point(0, 1);
            this.bottomPanel1.Name = "bottomPanel1";
            this.bottomPanel1.Size = new System.Drawing.Size(634, 41);
            this.bottomPanel1.TabIndex = 3;
            // 
            // About
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(634, 310);
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Panel bottomPanel2;
        private System.Windows.Forms.Panel bottomPanel1;
    }
}
