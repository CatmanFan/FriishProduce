
namespace FriishProduce
{
    partial class Msg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Msg));
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.do_not_show = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.image = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.bottomPanel2.Controls.Add(this.bottomPanel1);
            resources.ApplyResources(this.bottomPanel2, "bottomPanel2");
            this.bottomPanel2.Name = "bottomPanel2";
            // 
            // bottomPanel1
            // 
            this.bottomPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bottomPanel1.Controls.Add(this.button1);
            this.bottomPanel1.Controls.Add(this.button2);
            this.bottomPanel1.Controls.Add(this.do_not_show);
            this.bottomPanel1.Controls.Add(this.button3);
            resources.ApplyResources(this.bottomPanel1, "bottomPanel1");
            this.bottomPanel1.Name = "bottomPanel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Name = "button1";
            this.button1.Tag = "";
            this.button1.Click += new System.EventHandler(this.Button_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Name = "button2";
            this.button2.Tag = "";
            this.button2.Click += new System.EventHandler(this.Button_Click);
            // 
            // do_not_show
            // 
            resources.ApplyResources(this.do_not_show, "do_not_show");
            this.do_not_show.Name = "do_not_show";
            this.do_not_show.Tag = "do_not_show";
            this.do_not_show.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Name = "button3";
            this.button3.Tag = "";
            this.button3.Click += new System.EventHandler(this.Button_Click);
            // 
            // textLabel
            // 
            resources.ApplyResources(this.textLabel, "textLabel");
            this.textLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.textLabel.Name = "textLabel";
            this.textLabel.UseMnemonic = false;
            // 
            // descriptionLabel
            // 
            resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.UseMnemonic = false;
            // 
            // image
            // 
            resources.ApplyResources(this.image, "image");
            this.image.Name = "image";
            this.tableLayoutPanel1.SetRowSpan(this.image, 2);
            this.image.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.textLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.descriptionLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.image, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // Msg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Msg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Msg_Close);
            this.Load += new System.EventHandler(this.Msg_Load);
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.bottomPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel2;
        private System.Windows.Forms.Panel bottomPanel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.CheckBox do_not_show;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}