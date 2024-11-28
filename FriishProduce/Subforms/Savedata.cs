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
        internal string origTitle;
        internal bool origFill;

        public Savedata(Platform platform)
        {
            InitializeComponent();
            Reset(platform, 0);
            Program.Lang.Control(this);
            Fill.Text = Program.Lang.String("fill_save_data", "projectform");
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
            int orig = Title.MaxLength;
            Title.MaxLength = platform switch
            {
                Platform.NES => region == 3 ? 30 : 20,
                Platform.SNES => 80,
                Platform.N64 => 100,
                Platform.NEO or Platform.MSX => 64,
                _ => 80
            };

            // Singleline platforms
            // ********
            bool singleline = region == 3 // Korea
                             || platform == Platform.NES
                             || platform == Platform.SMS
                             || platform == Platform.SMD
                             || platform == Platform.PCE
                             || platform == Platform.PCECD;

            // Set textbox to use singleline when needed
            // ********
            if (Title.Multiline == singleline)
            {
                Title.Multiline = !singleline;
                Title.Location = singleline ? new Point(Title.Location.X, int.Parse(Title.Tag.ToString()) + 8) : new Point(Title.Location.X, int.Parse(Title.Tag.ToString()));
                Title.Clear();
                return;
            }

            // The following applies to both NES/FC & SNES/SFC
            // ********
            if (region == 4 && Title.Multiline) Title.MaxLength /= 2;

            // Clear text field if at least one line is longer than the maximum limit allowed
            // ********
            double max = Title.Multiline ? Math.Round((double)Title.MaxLength / 2) : Title.MaxLength;
            foreach (var line in Title.Lines)
                if (line.Length > max && Title.MaxLength != orig)
                    Title.Clear();
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

        private void isShown(object sender, EventArgs e)
        {
            origTitle = Title.Text;
            origFill = Fill.Checked;
        }

        private void isClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                Title.Text = origTitle;
                Fill.Checked = origFill;
            }
        }
    }
}
