using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Savedata : Form
    {
        internal string[] origLines;
        internal bool origFill;

        public string[] SourcedLines { get; set; }
        public string[] Lines { get => new string[] { title.Text, subtitle.Text }; }
        public int MaxLength { get; private set; }
        public bool IsMultiline { get; private set; }

        public Savedata(Platform platform)
        {
            InitializeComponent();
            Reset(platform, 0);

            #region Localization
            Program.Lang.Control(this);
            Fill.Text = Program.Lang.String("fill_save_data", "projectform");
            label1.Text = Program.Lang.String(label1.Tag.ToString(), "projectform");
            label2.Text = Program.Lang.String(label2.Tag.ToString(), "projectform");
            label3.Text = string.Format(Program.Lang.String(label3.Tag.ToString(), "projectform"), MaxLength);
            #endregion
        }

        /// <summary>
        /// Changes maximum text length available and/or switches between single/multiline format. When needed, it also clears the text field.
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="region"></param>
        public void Reset(Platform platform, int region)
        {
            // Determine initial max character limit
            // ********
            int orig = MaxLength = platform switch
            {
                Platform.NES => region == 3 ? 30 : 20,
                Platform.SNES => 80,
                Platform.N64 => 100,
                Platform.NEO or Platform.MSX => 64,
                _ => 80
            };

            // Singleline platforms
            // ********
            IsMultiline = !(region == 3 // Korea
                         || platform == Platform.NES
                         || platform == Platform.SMS
                         || platform == Platform.SMD
                         || platform == Platform.PCE
                         || platform == Platform.PCECD);

            // Set textbox to use singleline when needed
            // ********
            label2.Enabled = subtitle.Enabled = IsMultiline;
            if (!subtitle.Enabled)
            {
                subtitle.Clear();
                return;
            }

            // The following applies to both NES/FC & SNES/SFC
            // ********
            if (region == 4 && IsMultiline) MaxLength /= 2;

            // Clear text field if at least one line is longer than the maximum limit allowed
            // ********
            MaxLength = IsMultiline ? (int)Math.Round((double)MaxLength / 2) : MaxLength;
            foreach (var line in Controls.OfType<TextBox>())
            {
                if (line.Text.Length > MaxLength && MaxLength != orig)
                    line.Clear();
                line.MaxLength = MaxLength;
            }

            // Write length to label
            // ********
            label3.Text = string.Format(Program.Lang.String(label3.Tag.ToString(), "projectform"), MaxLength);
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Hide();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Hide();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void isClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                title.Text = origLines[0];
                subtitle.Text = origLines[1];
                Fill.Checked = origFill;
            }
        }

        private void isLoading(object sender, EventArgs e)
        {
            origLines = new string[] { title.Text, subtitle.Text };
            origFill = Fill.Checked;
            SyncTitles();
        }

        public void SyncTitles()
        {
            title.ReadOnly = subtitle.ReadOnly = Fill.Checked;

            if (Fill.Checked)
            {
                ReplaceTitles(SourcedLines[0], SourcedLines[1]);
            }
        }

        public void ReplaceTitles(string line1, string line2)
        {
            if (string.IsNullOrWhiteSpace(line1)) line1 = null;
            if (string.IsNullOrWhiteSpace(line2)) line2 = null;

            if (title == null && subtitle == null) return;

            title.Text = line1?.Length <= MaxLength ? line1 : null;
            subtitle.Text = line2?.Length <= MaxLength ? line2 : null;
        }

        private void Fill_CheckedChanged(object sender, EventArgs e)
        {
            SyncTitles();
        }
    }
}
