
namespace FriishProduce
{
    partial class ControllerMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllerMapping));
            this.bottomPanel2 = new System.Windows.Forms.Panel();
            this.bottomPanel1 = new System.Windows.Forms.Panel();
            this.b_cancel = new System.Windows.Forms.Button();
            this.b_ok = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.page1 = new System.Windows.Forms.TabPage();
            this.vertical_layout = new System.Windows.Forms.CheckBox();
            this.WiiRemote_Minus = new System.Windows.Forms.ComboBox();
            this.WiiRemote_Plus = new System.Windows.Forms.ComboBox();
            this.WiiRemote_B = new System.Windows.Forms.ComboBox();
            this.WiiRemote_A = new System.Windows.Forms.ComboBox();
            this.WiiRemote_2 = new System.Windows.Forms.ComboBox();
            this.WiiRemote_1 = new System.Windows.Forms.ComboBox();
            this.WiiRemote_Left = new System.Windows.Forms.ComboBox();
            this.WiiRemote_Down = new System.Windows.Forms.ComboBox();
            this.WiiRemote_Up = new System.Windows.Forms.ComboBox();
            this.WiiRemote_Right = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.page2 = new System.Windows.Forms.TabPage();
            this.Classic_Plus = new System.Windows.Forms.ComboBox();
            this.Classic_Minus = new System.Windows.Forms.ComboBox();
            this.Classic_B = new System.Windows.Forms.ComboBox();
            this.Classic_A = new System.Windows.Forms.ComboBox();
            this.Classic_Y = new System.Windows.Forms.ComboBox();
            this.Classic_X = new System.Windows.Forms.ComboBox();
            this.Classic_R = new System.Windows.Forms.ComboBox();
            this.Classic_ZR = new System.Windows.Forms.ComboBox();
            this.Classic_Down = new System.Windows.Forms.ComboBox();
            this.Classic_Right = new System.Windows.Forms.ComboBox();
            this.Classic_Left = new System.Windows.Forms.ComboBox();
            this.Classic_Up = new System.Windows.Forms.ComboBox();
            this.Classic_L = new System.Windows.Forms.ComboBox();
            this.Classic_ZL = new System.Windows.Forms.ComboBox();
            this.image_cc = new System.Windows.Forms.PictureBox();
            this.page3 = new System.Windows.Forms.TabPage();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.GC_Z = new System.Windows.Forms.ComboBox();
            this.GC_R = new System.Windows.Forms.ComboBox();
            this.GC_B = new System.Windows.Forms.ComboBox();
            this.GC_A = new System.Windows.Forms.ComboBox();
            this.GC_X = new System.Windows.Forms.ComboBox();
            this.GC_Y = new System.Windows.Forms.ComboBox();
            this.GC_Down = new System.Windows.Forms.ComboBox();
            this.GC_Right = new System.Windows.Forms.ComboBox();
            this.GC_Left = new System.Windows.Forms.ComboBox();
            this.GC_Up = new System.Windows.Forms.ComboBox();
            this.GC_L = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bottomPanel2.SuspendLayout();
            this.bottomPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.page1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.page2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_cc)).BeginInit();
            this.page3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bottomPanel2
            // 
            this.bottomPanel2.BackColor = System.Drawing.SystemColors.ControlDark;
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
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.b_cancel, "b_cancel");
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.page1);
            this.tabControl1.Controls.Add(this.page2);
            this.tabControl1.Controls.Add(this.page3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // page1
            // 
            this.page1.Controls.Add(this.vertical_layout);
            this.page1.Controls.Add(this.WiiRemote_Minus);
            this.page1.Controls.Add(this.WiiRemote_Plus);
            this.page1.Controls.Add(this.WiiRemote_B);
            this.page1.Controls.Add(this.WiiRemote_A);
            this.page1.Controls.Add(this.WiiRemote_2);
            this.page1.Controls.Add(this.WiiRemote_1);
            this.page1.Controls.Add(this.WiiRemote_Left);
            this.page1.Controls.Add(this.WiiRemote_Down);
            this.page1.Controls.Add(this.WiiRemote_Up);
            this.page1.Controls.Add(this.WiiRemote_Right);
            this.page1.Controls.Add(this.pictureBox2);
            this.page1.Controls.Add(this.pictureBox3);
            resources.ApplyResources(this.page1, "page1");
            this.page1.Name = "page1";
            this.page1.Tag = "page1";
            this.page1.UseVisualStyleBackColor = true;
            // 
            // vertical_layout
            // 
            resources.ApplyResources(this.vertical_layout, "vertical_layout");
            this.vertical_layout.Name = "vertical_layout";
            this.vertical_layout.Tag = "vertical_layout";
            this.vertical_layout.UseVisualStyleBackColor = true;
            // 
            // WiiRemote_Minus
            // 
            this.WiiRemote_Minus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_Minus, "WiiRemote_Minus");
            this.WiiRemote_Minus.FormattingEnabled = true;
            this.WiiRemote_Minus.Name = "WiiRemote_Minus";
            // 
            // WiiRemote_Plus
            // 
            this.WiiRemote_Plus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_Plus, "WiiRemote_Plus");
            this.WiiRemote_Plus.FormattingEnabled = true;
            this.WiiRemote_Plus.Name = "WiiRemote_Plus";
            // 
            // WiiRemote_B
            // 
            this.WiiRemote_B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_B, "WiiRemote_B");
            this.WiiRemote_B.FormattingEnabled = true;
            this.WiiRemote_B.Name = "WiiRemote_B";
            // 
            // WiiRemote_A
            // 
            this.WiiRemote_A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_A, "WiiRemote_A");
            this.WiiRemote_A.FormattingEnabled = true;
            this.WiiRemote_A.Name = "WiiRemote_A";
            // 
            // WiiRemote_2
            // 
            this.WiiRemote_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_2, "WiiRemote_2");
            this.WiiRemote_2.FormattingEnabled = true;
            this.WiiRemote_2.Name = "WiiRemote_2";
            // 
            // WiiRemote_1
            // 
            this.WiiRemote_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_1, "WiiRemote_1");
            this.WiiRemote_1.FormattingEnabled = true;
            this.WiiRemote_1.Name = "WiiRemote_1";
            // 
            // WiiRemote_Left
            // 
            this.WiiRemote_Left.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_Left, "WiiRemote_Left");
            this.WiiRemote_Left.FormattingEnabled = true;
            this.WiiRemote_Left.Name = "WiiRemote_Left";
            // 
            // WiiRemote_Down
            // 
            this.WiiRemote_Down.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_Down, "WiiRemote_Down");
            this.WiiRemote_Down.FormattingEnabled = true;
            this.WiiRemote_Down.Name = "WiiRemote_Down";
            // 
            // WiiRemote_Up
            // 
            this.WiiRemote_Up.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_Up, "WiiRemote_Up");
            this.WiiRemote_Up.FormattingEnabled = true;
            this.WiiRemote_Up.Name = "WiiRemote_Up";
            // 
            // WiiRemote_Right
            // 
            this.WiiRemote_Right.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.WiiRemote_Right, "WiiRemote_Right");
            this.WiiRemote_Right.FormattingEnabled = true;
            this.WiiRemote_Right.Name = "WiiRemote_Right";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::FriishProduce.Properties.Resources.mapping_wiiremh;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::FriishProduce.Properties.Resources.mapping_wiiremh;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // page2
            // 
            this.page2.Controls.Add(this.Classic_Plus);
            this.page2.Controls.Add(this.Classic_Minus);
            this.page2.Controls.Add(this.Classic_B);
            this.page2.Controls.Add(this.Classic_A);
            this.page2.Controls.Add(this.Classic_Y);
            this.page2.Controls.Add(this.Classic_X);
            this.page2.Controls.Add(this.Classic_R);
            this.page2.Controls.Add(this.Classic_ZR);
            this.page2.Controls.Add(this.Classic_Down);
            this.page2.Controls.Add(this.Classic_Right);
            this.page2.Controls.Add(this.Classic_Left);
            this.page2.Controls.Add(this.Classic_Up);
            this.page2.Controls.Add(this.Classic_L);
            this.page2.Controls.Add(this.Classic_ZL);
            this.page2.Controls.Add(this.image_cc);
            resources.ApplyResources(this.page2, "page2");
            this.page2.Name = "page2";
            this.page2.Tag = "page2";
            this.page2.UseVisualStyleBackColor = true;
            // 
            // Classic_Plus
            // 
            this.Classic_Plus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Plus, "Classic_Plus");
            this.Classic_Plus.FormattingEnabled = true;
            this.Classic_Plus.Name = "Classic_Plus";
            // 
            // Classic_Minus
            // 
            this.Classic_Minus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Minus, "Classic_Minus");
            this.Classic_Minus.FormattingEnabled = true;
            this.Classic_Minus.Name = "Classic_Minus";
            // 
            // Classic_B
            // 
            this.Classic_B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_B, "Classic_B");
            this.Classic_B.FormattingEnabled = true;
            this.Classic_B.Name = "Classic_B";
            // 
            // Classic_A
            // 
            this.Classic_A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_A, "Classic_A");
            this.Classic_A.FormattingEnabled = true;
            this.Classic_A.Name = "Classic_A";
            // 
            // Classic_Y
            // 
            this.Classic_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Y, "Classic_Y");
            this.Classic_Y.FormattingEnabled = true;
            this.Classic_Y.Name = "Classic_Y";
            // 
            // Classic_X
            // 
            this.Classic_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_X, "Classic_X");
            this.Classic_X.FormattingEnabled = true;
            this.Classic_X.Name = "Classic_X";
            // 
            // Classic_R
            // 
            this.Classic_R.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_R, "Classic_R");
            this.Classic_R.FormattingEnabled = true;
            this.Classic_R.Name = "Classic_R";
            // 
            // Classic_ZR
            // 
            this.Classic_ZR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_ZR, "Classic_ZR");
            this.Classic_ZR.FormattingEnabled = true;
            this.Classic_ZR.Name = "Classic_ZR";
            // 
            // Classic_Down
            // 
            this.Classic_Down.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Down, "Classic_Down");
            this.Classic_Down.FormattingEnabled = true;
            this.Classic_Down.Name = "Classic_Down";
            // 
            // Classic_Right
            // 
            this.Classic_Right.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Right, "Classic_Right");
            this.Classic_Right.FormattingEnabled = true;
            this.Classic_Right.Name = "Classic_Right";
            // 
            // Classic_Left
            // 
            this.Classic_Left.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Left, "Classic_Left");
            this.Classic_Left.FormattingEnabled = true;
            this.Classic_Left.Name = "Classic_Left";
            // 
            // Classic_Up
            // 
            this.Classic_Up.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_Up, "Classic_Up");
            this.Classic_Up.FormattingEnabled = true;
            this.Classic_Up.Name = "Classic_Up";
            // 
            // Classic_L
            // 
            this.Classic_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_L, "Classic_L");
            this.Classic_L.FormattingEnabled = true;
            this.Classic_L.Name = "Classic_L";
            // 
            // Classic_ZL
            // 
            this.Classic_ZL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.Classic_ZL, "Classic_ZL");
            this.Classic_ZL.FormattingEnabled = true;
            this.Classic_ZL.Name = "Classic_ZL";
            // 
            // image_cc
            // 
            this.image_cc.Image = global::FriishProduce.Properties.Resources.mapping_cc;
            resources.ApplyResources(this.image_cc, "image_cc");
            this.image_cc.Name = "image_cc";
            this.image_cc.TabStop = false;
            // 
            // page3
            // 
            this.page3.Controls.Add(this.comboBox8);
            this.page3.Controls.Add(this.GC_Z);
            this.page3.Controls.Add(this.GC_R);
            this.page3.Controls.Add(this.GC_B);
            this.page3.Controls.Add(this.GC_A);
            this.page3.Controls.Add(this.GC_X);
            this.page3.Controls.Add(this.GC_Y);
            this.page3.Controls.Add(this.GC_Down);
            this.page3.Controls.Add(this.GC_Right);
            this.page3.Controls.Add(this.GC_Left);
            this.page3.Controls.Add(this.GC_Up);
            this.page3.Controls.Add(this.GC_L);
            this.page3.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.page3, "page3");
            this.page3.Name = "page3";
            this.page3.Tag = "page3";
            this.page3.UseVisualStyleBackColor = true;
            // 
            // comboBox8
            // 
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox8, "comboBox8");
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Name = "comboBox8";
            // 
            // GC_Z
            // 
            this.GC_Z.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_Z, "GC_Z");
            this.GC_Z.FormattingEnabled = true;
            this.GC_Z.Name = "GC_Z";
            // 
            // GC_R
            // 
            this.GC_R.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_R, "GC_R");
            this.GC_R.FormattingEnabled = true;
            this.GC_R.Name = "GC_R";
            // 
            // GC_B
            // 
            this.GC_B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_B, "GC_B");
            this.GC_B.FormattingEnabled = true;
            this.GC_B.Name = "GC_B";
            // 
            // GC_A
            // 
            this.GC_A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_A, "GC_A");
            this.GC_A.FormattingEnabled = true;
            this.GC_A.Name = "GC_A";
            // 
            // GC_X
            // 
            this.GC_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_X, "GC_X");
            this.GC_X.FormattingEnabled = true;
            this.GC_X.Name = "GC_X";
            // 
            // GC_Y
            // 
            this.GC_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_Y, "GC_Y");
            this.GC_Y.FormattingEnabled = true;
            this.GC_Y.Name = "GC_Y";
            // 
            // GC_Down
            // 
            this.GC_Down.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_Down, "GC_Down");
            this.GC_Down.FormattingEnabled = true;
            this.GC_Down.Name = "GC_Down";
            // 
            // GC_Right
            // 
            this.GC_Right.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_Right, "GC_Right");
            this.GC_Right.FormattingEnabled = true;
            this.GC_Right.Name = "GC_Right";
            // 
            // GC_Left
            // 
            this.GC_Left.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_Left, "GC_Left");
            this.GC_Left.FormattingEnabled = true;
            this.GC_Left.Name = "GC_Left";
            // 
            // GC_Up
            // 
            this.GC_Up.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_Up, "GC_Up");
            this.GC_Up.FormattingEnabled = true;
            this.GC_Up.Name = "GC_Up";
            // 
            // GC_L
            // 
            this.GC_L.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.GC_L, "GC_L");
            this.GC_L.FormattingEnabled = true;
            this.GC_L.Name = "GC_L";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FriishProduce.Properties.Resources.mapping_gc;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // ControllerMapping
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.b_cancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.bottomPanel2);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControllerMapping";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "controller";
            this.Load += new System.EventHandler(this.Form_Load);
            this.bottomPanel2.ResumeLayout(false);
            this.bottomPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.page1.ResumeLayout(false);
            this.page1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.page2.ResumeLayout(false);
            this.page2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_cc)).EndInit();
            this.page3.ResumeLayout(false);
            this.page3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel2;
        protected System.Windows.Forms.Panel bottomPanel1;
        protected System.Windows.Forms.Button b_cancel;
        protected System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage page1;
        private System.Windows.Forms.TabPage page2;
        private System.Windows.Forms.PictureBox image_cc;
        private System.Windows.Forms.TabPage page3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox WiiRemote_Minus;
        private System.Windows.Forms.ComboBox WiiRemote_Plus;
        private System.Windows.Forms.ComboBox WiiRemote_B;
        private System.Windows.Forms.ComboBox WiiRemote_A;
        private System.Windows.Forms.ComboBox WiiRemote_2;
        private System.Windows.Forms.ComboBox WiiRemote_1;
        private System.Windows.Forms.ComboBox WiiRemote_Left;
        private System.Windows.Forms.ComboBox WiiRemote_Down;
        private System.Windows.Forms.ComboBox WiiRemote_Up;
        private System.Windows.Forms.ComboBox WiiRemote_Right;
        private System.Windows.Forms.ComboBox Classic_Plus;
        private System.Windows.Forms.ComboBox Classic_Minus;
        private System.Windows.Forms.ComboBox Classic_B;
        private System.Windows.Forms.ComboBox Classic_A;
        private System.Windows.Forms.ComboBox Classic_Y;
        private System.Windows.Forms.ComboBox Classic_X;
        private System.Windows.Forms.ComboBox Classic_R;
        private System.Windows.Forms.ComboBox Classic_ZR;
        private System.Windows.Forms.ComboBox Classic_Down;
        private System.Windows.Forms.ComboBox Classic_Right;
        private System.Windows.Forms.ComboBox Classic_Left;
        private System.Windows.Forms.ComboBox Classic_Up;
        private System.Windows.Forms.ComboBox Classic_L;
        private System.Windows.Forms.ComboBox Classic_ZL;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.ComboBox GC_Z;
        private System.Windows.Forms.ComboBox GC_R;
        private System.Windows.Forms.ComboBox GC_B;
        private System.Windows.Forms.ComboBox GC_A;
        private System.Windows.Forms.ComboBox GC_X;
        private System.Windows.Forms.ComboBox GC_Y;
        private System.Windows.Forms.ComboBox GC_Down;
        private System.Windows.Forms.ComboBox GC_Right;
        private System.Windows.Forms.ComboBox GC_Left;
        private System.Windows.Forms.ComboBox GC_Up;
        private System.Windows.Forms.ComboBox GC_L;
        private System.Windows.Forms.CheckBox vertical_layout;
    }
}