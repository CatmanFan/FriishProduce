
namespace FriishProduce
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.ComboBox();
            this.page1 = new System.Windows.Forms.Panel();
            this.page2 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.page1.SuspendLayout();
            this.page2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Back);
            this.panel1.Controls.Add(this.Next);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Back
            // 
            resources.ApplyResources(this.Back, "Back");
            this.Back.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.Back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Back.ForeColor = System.Drawing.Color.White;
            this.Back.Name = "Back";
            this.Back.UseVisualStyleBackColor = true;
            // 
            // Next
            // 
            this.Next.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.Next, "Next");
            this.Next.ForeColor = System.Drawing.Color.White;
            this.Next.Name = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.Color.DimGray;
            this.console.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.console, "console");
            this.console.ForeColor = System.Drawing.Color.White;
            this.console.FormattingEnabled = true;
            this.console.Items.AddRange(new object[] {
            resources.GetString("console.Items"),
            resources.GetString("console.Items1"),
            resources.GetString("console.Items2")});
            this.console.Name = "console";
            // 
            // page1
            // 
            this.page1.Controls.Add(this.console);
            this.page1.Controls.Add(this.label1);
            resources.ApplyResources(this.page1, "page1");
            this.page1.Name = "page1";
            // 
            // page2
            // 
            this.page2.Controls.Add(this.comboBox2);
            this.page2.Controls.Add(this.label2);
            resources.ApplyResources(this.page2, "page2");
            this.page2.Name = "page2";
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.Color.DimGray;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox2, "comboBox2");
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            resources.GetString("comboBox2.Items"),
            resources.GetString("comboBox2.Items1"),
            resources.GetString("comboBox2.Items2")});
            this.comboBox2.Name = "comboBox2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.page1);
            this.Controls.Add(this.page2);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.panel1.ResumeLayout(false);
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            this.page2.ResumeLayout(false);
            this.page2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox console;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Panel page1;
        private System.Windows.Forms.Panel page2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
    }
}

