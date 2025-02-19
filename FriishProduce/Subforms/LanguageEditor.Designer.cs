
namespace FriishProduce
{
    partial class LanguageEditor
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageEditor));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.languages_list = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.TextBox();
            this.iso_code = new System.Windows.Forms.TextBox();
            this.author = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.strings = new System.Windows.Forms.DataGridView();
            this.Section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Original = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Translated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.filter_by_section = new System.Windows.Forms.ToolStripDropDownButton();
            this.search_for = new System.Windows.Forms.ToolStripDropDownButton();
            this.find_original_l = new System.Windows.Forms.ToolStripMenuItem();
            this.find_original = new System.Windows.Forms.ToolStripTextBox();
            this.find_translated_l = new System.Windows.Forms.ToolStripMenuItem();
            this.find_translated = new System.Windows.Forms.ToolStripTextBox();
            this.status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strings)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem8});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem2,
            this.menuItem6,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
            this.menuItem1.Text = "&File";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem7.Text = "&New";
            this.menuItem7.Click += new System.EventHandler(this.ClearAll);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem2.Text = "&Open...";
            this.menuItem2.Click += new System.EventHandler(this.Open_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 2;
            this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem6.Text = "&Save";
            this.menuItem6.Click += new System.EventHandler(this.Save_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.menuItem3.Text = "Save &as...";
            this.menuItem3.Click += new System.EventHandler(this.Save_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 5;
            this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuItem5.Text = "&Exit";
            this.menuItem5.Click += new System.EventHandler(this.Exit);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9});
            this.menuItem8.Text = "&Edit";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "&Clear all identical strings";
            this.menuItem9.Click += new System.EventHandler(this.ClearIdentical);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Metadata";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel2.Controls.Add(this.languages_list, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.title, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.iso_code, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.author, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(854, 85);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // languages_list
            // 
            this.languages_list.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.languages_list.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.languages_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.languages_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languages_list.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.languages_list.FormattingEnabled = true;
            this.languages_list.Items.AddRange(new object[] {
            "[Custom]"});
            this.languages_list.Location = new System.Drawing.Point(173, 3);
            this.languages_list.Name = "languages_list";
            this.languages_list.Size = new System.Drawing.Size(572, 21);
            this.languages_list.TabIndex = 0;
            this.languages_list.SelectedIndexChanged += new System.EventHandler(this.Languages_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Translated application title:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // title
            // 
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Location = new System.Drawing.Point(173, 57);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(572, 21);
            this.title.TabIndex = 6;
            this.title.TextChanged += new System.EventHandler(this.Text_Changed);
            // 
            // iso_code
            // 
            this.iso_code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iso_code.Location = new System.Drawing.Point(751, 3);
            this.iso_code.Name = "iso_code";
            this.iso_code.ReadOnly = true;
            this.iso_code.Size = new System.Drawing.Size(100, 21);
            this.iso_code.TabIndex = 2;
            this.iso_code.TextChanged += new System.EventHandler(this.Text_Changed);
            // 
            // author
            // 
            this.author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.author.Location = new System.Drawing.Point(173, 30);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(572, 21);
            this.author.TabIndex = 5;
            this.author.TextChanged += new System.EventHandler(this.Text_Changed);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Language:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Author:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "json";
            this.openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            this.openFileDialog.Title = "Open JSON file...";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "json";
            this.saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            this.saveFileDialog.Title = "Save JSON as...";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.strings, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 421);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // strings
            // 
            this.strings.AllowUserToAddRows = false;
            this.strings.AllowUserToDeleteRows = false;
            this.strings.AllowUserToResizeRows = false;
            this.strings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.strings.BackgroundColor = System.Drawing.SystemColors.Control;
            this.strings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.strings.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.strings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.strings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Section,
            this.ID,
            this.Original,
            this.Translated});
            this.strings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strings.GridColor = System.Drawing.SystemColors.Control;
            this.strings.Location = new System.Drawing.Point(11, 137);
            this.strings.Margin = new System.Windows.Forms.Padding(10, 10, 10, 31);
            this.strings.MultiSelect = false;
            this.strings.Name = "strings";
            this.strings.RowHeadersVisible = false;
            this.strings.Size = new System.Drawing.Size(864, 252);
            this.strings.TabIndex = 2;
            this.strings.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Strings_CellContentDoubleClick);
            this.strings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Strings_CellEndEdit);
            this.strings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Strings_KeyDown);
            // 
            // Section
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Section.DefaultCellStyle = dataGridViewCellStyle1;
            this.Section.HeaderText = "Section";
            this.Section.MinimumWidth = 100;
            this.Section.Name = "Section";
            this.Section.ReadOnly = true;
            this.Section.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Section.Visible = false;
            this.Section.Width = 150;
            // 
            // ID
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Original
            // 
            this.Original.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Original.DefaultCellStyle = dataGridViewCellStyle3;
            this.Original.HeaderText = "Original";
            this.Original.Name = "Original";
            this.Original.ReadOnly = true;
            this.Original.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Translated
            // 
            this.Translated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Translated.DefaultCellStyle = dataGridViewCellStyle4;
            this.Translated.HeaderText = "Translated";
            this.Translated.Name = "Translated";
            this.Translated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filter_by_section,
            this.search_for,
            this.status_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 399);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // filter_by_section
            // 
            this.filter_by_section.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filter_by_section.Name = "filter_by_section";
            this.filter_by_section.Size = new System.Drawing.Size(103, 20);
            this.filter_by_section.Text = "Filter by section";
            // 
            // search_for
            // 
            this.search_for.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.search_for.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.find_original_l,
            this.find_translated_l});
            this.search_for.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.search_for.Name = "search_for";
            this.search_for.Size = new System.Drawing.Size(104, 20);
            this.search_for.Text = "Search for text...";
            // 
            // find_original_l
            // 
            this.find_original_l.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.find_original});
            this.find_original_l.Name = "find_original_l";
            this.find_original_l.Size = new System.Drawing.Size(129, 22);
            this.find_original_l.Text = "Original";
            // 
            // find_original
            // 
            this.find_original.AcceptsReturn = true;
            this.find_original.AutoSize = false;
            this.find_original.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.find_original.Name = "find_original";
            this.find_original.Size = new System.Drawing.Size(200, 23);
            this.find_original.TextChanged += new System.EventHandler(this.Find);
            // 
            // find_translated_l
            // 
            this.find_translated_l.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.find_translated});
            this.find_translated_l.Name = "find_translated_l";
            this.find_translated_l.Size = new System.Drawing.Size(129, 22);
            this.find_translated_l.Text = "Translated";
            // 
            // find_translated
            // 
            this.find_translated.AcceptsReturn = true;
            this.find_translated.AutoSize = false;
            this.find_translated.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.find_translated.Name = "find_translated";
            this.find_translated.Size = new System.Drawing.Size(200, 23);
            this.find_translated.TextChanged += new System.EventHandler(this.Find);
            // 
            // status_label
            // 
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(662, 17);
            this.status_label.Spring = true;
            this.status_label.Text = "undefined";
            this.status_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LanguageEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(884, 421);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu;
            this.Name = "LanguageEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FriishProduce - Language JSON Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Exit_Form);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.strings)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox languages_list;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox iso_code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox author;
        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView strings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_label;
        private System.Windows.Forms.ToolStripDropDownButton search_for;
        private System.Windows.Forms.ToolStripMenuItem find_original_l;
        private System.Windows.Forms.ToolStripTextBox find_original;
        private System.Windows.Forms.ToolStripMenuItem find_translated_l;
        private System.Windows.Forms.ToolStripTextBox find_translated;
        private System.Windows.Forms.ToolStripDropDownButton filter_by_section;
        private System.Windows.Forms.DataGridViewTextBoxColumn Section;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Original;
        private System.Windows.Forms.DataGridViewTextBoxColumn Translated;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
    }
}