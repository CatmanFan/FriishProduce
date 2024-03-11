
namespace FriishProduce
{
    partial class BannerPreview
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Animation1 = new System.Windows.Forms.Timer(this.components);
            this.BannerPreview_Main = new System.Windows.Forms.Panel();
            this.BannerPreview_Text = new System.Windows.Forms.Label();
            this.BannerPreview_Line2 = new System.Windows.Forms.PictureBox();
            this.BannerPreview_Line1 = new System.Windows.Forms.PictureBox();
            this.Image = new System.Windows.Forms.PictureBox();
            this.BannerPreview_Players = new System.Windows.Forms.Label();
            this.BannerPreview_Year = new System.Windows.Forms.Label();
            this.BannerPreview_Gradient = new System.Windows.Forms.PictureBox();
            this.BannerPreview_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BannerPreview_Line2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BannerPreview_Line1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BannerPreview_Gradient)).BeginInit();
            this.SuspendLayout();
            // 
            // Animation1
            // 
            this.Animation1.Interval = 10;
            this.Animation1.Tick += new System.EventHandler(this.Animation1_Tick);
            // 
            // BannerPreview_Main
            // 
            this.BannerPreview_Main.BackColor = System.Drawing.SystemColors.Control;
            this.BannerPreview_Main.Controls.Add(this.BannerPreview_Text);
            this.BannerPreview_Main.Controls.Add(this.BannerPreview_Line2);
            this.BannerPreview_Main.Controls.Add(this.BannerPreview_Line1);
            this.BannerPreview_Main.Controls.Add(this.Image);
            this.BannerPreview_Main.Controls.Add(this.BannerPreview_Players);
            this.BannerPreview_Main.Controls.Add(this.BannerPreview_Year);
            this.BannerPreview_Main.Controls.Add(this.BannerPreview_Gradient);
            this.BannerPreview_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BannerPreview_Main.Location = new System.Drawing.Point(0, 0);
            this.BannerPreview_Main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BannerPreview_Main.Name = "BannerPreview_Main";
            this.BannerPreview_Main.Size = new System.Drawing.Size(680, 277);
            this.BannerPreview_Main.TabIndex = 44;
            // 
            // BannerPreview_Text
            // 
            this.BannerPreview_Text.BackColor = System.Drawing.Color.Transparent;
            this.BannerPreview_Text.Font = new System.Drawing.Font("Arial", 16.5F);
            this.BannerPreview_Text.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BannerPreview_Text.Location = new System.Drawing.Point(35, 199);
            this.BannerPreview_Text.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BannerPreview_Text.Name = "BannerPreview_Text";
            this.BannerPreview_Text.Size = new System.Drawing.Size(610, 50);
            this.BannerPreview_Text.TabIndex = 43;
            this.BannerPreview_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BannerPreview_Text.UseMnemonic = false;
            this.BannerPreview_Text.Visible = false;
            // 
            // BannerPreview_Line2
            // 
            this.BannerPreview_Line2.BackColor = System.Drawing.Color.Silver;
            this.BannerPreview_Line2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BannerPreview_Line2.Location = new System.Drawing.Point(-2, 99);
            this.BannerPreview_Line2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BannerPreview_Line2.Name = "BannerPreview_Line2";
            this.BannerPreview_Line2.Size = new System.Drawing.Size(117, 2);
            this.BannerPreview_Line2.TabIndex = 45;
            this.BannerPreview_Line2.TabStop = false;
            this.BannerPreview_Line2.Paint += new System.Windows.Forms.PaintEventHandler(this.BannerPreview_Paint);
            // 
            // BannerPreview_Line1
            // 
            this.BannerPreview_Line1.BackColor = System.Drawing.Color.Silver;
            this.BannerPreview_Line1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BannerPreview_Line1.Location = new System.Drawing.Point(-2, 54);
            this.BannerPreview_Line1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BannerPreview_Line1.Name = "BannerPreview_Line1";
            this.BannerPreview_Line1.Size = new System.Drawing.Size(117, 2);
            this.BannerPreview_Line1.TabIndex = 44;
            this.BannerPreview_Line1.TabStop = false;
            this.BannerPreview_Line1.Paint += new System.Windows.Forms.PaintEventHandler(this.BannerPreview_Paint);
            // 
            // Image
            // 
            this.Image.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Image.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Image.Location = new System.Drawing.Point(258, 52);
            this.Image.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(165, 132);
            this.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Image.TabIndex = 32;
            this.Image.TabStop = false;
            // 
            // BannerPreview_Players
            // 
            this.BannerPreview_Players.Font = new System.Drawing.Font("Tahoma", 11F);
            this.BannerPreview_Players.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BannerPreview_Players.Location = new System.Drawing.Point(11, 62);
            this.BannerPreview_Players.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BannerPreview_Players.Name = "BannerPreview_Players";
            this.BannerPreview_Players.Size = new System.Drawing.Size(153, 35);
            this.BannerPreview_Players.TabIndex = 47;
            this.BannerPreview_Players.Text = "Players:";
            this.BannerPreview_Players.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BannerPreview_Players.UseMnemonic = false;
            // 
            // BannerPreview_Year
            // 
            this.BannerPreview_Year.Font = new System.Drawing.Font("Tahoma", 11F);
            this.BannerPreview_Year.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BannerPreview_Year.Location = new System.Drawing.Point(11, 16);
            this.BannerPreview_Year.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BannerPreview_Year.Name = "BannerPreview_Year";
            this.BannerPreview_Year.Size = new System.Drawing.Size(153, 35);
            this.BannerPreview_Year.TabIndex = 46;
            this.BannerPreview_Year.Text = "Released:";
            this.BannerPreview_Year.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BannerPreview_Year.UseMnemonic = false;
            // 
            // BannerPreview_Gradient
            // 
            this.BannerPreview_Gradient.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BannerPreview_Gradient.Location = new System.Drawing.Point(0, 146);
            this.BannerPreview_Gradient.Name = "BannerPreview_Gradient";
            this.BannerPreview_Gradient.Size = new System.Drawing.Size(680, 140);
            this.BannerPreview_Gradient.TabIndex = 48;
            this.BannerPreview_Gradient.TabStop = false;
            // 
            // BannerPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(4F, 10F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BannerPreview_Main);
            this.Font = new System.Drawing.Font("Tahoma", 6F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "BannerPreview";
            this.Size = new System.Drawing.Size(680, 277);
            this.BannerPreview_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BannerPreview_Line2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BannerPreview_Line1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BannerPreview_Gradient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Animation1;
        private System.Windows.Forms.Panel BannerPreview_Main;
        private System.Windows.Forms.Label BannerPreview_Text;
        private System.Windows.Forms.PictureBox BannerPreview_Line2;
        private System.Windows.Forms.PictureBox BannerPreview_Line1;
        private System.Windows.Forms.PictureBox Image;
        private System.Windows.Forms.Label BannerPreview_Players;
        private System.Windows.Forms.Label BannerPreview_Year;
        private System.Windows.Forms.PictureBox BannerPreview_Gradient;
    }
}
