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
    public partial class Msg : Form
    {
        public MessageBox.Result? Result { get; private set; }
        public bool DoNotShow_Clicked { get; private set; }

        public string MsgText { get; set; }
        public string Description { get; set; }

        private string[] buttons;
        public string[] Buttons
        {
            get => buttons;
            set
            {
                buttons = value;

                if (buttons == null) buttons = new string[] { "b_ok" };
                for (int i = 0; i < Math.Min(buttons.Length, 3); i++)
                {
                    if (buttons[i] is "b_ok" or "b_cancel" or "b_close" or "b_yes" or "b_no" or "b_save" or "b_dont_save")
                    {
                        try
                        {
                            if (i == 0) button1.Tag = buttons[i];
                            else if (i == 1) button2.Tag = buttons[i];
                            else if (i == 2) button3.Tag = buttons[i];
                        }
                        catch { }

                        buttons[i] = Program.Lang.String(Buttons[i]);
                    }
                }
            }
        }

        public MessageBox.Icons IconType { get; set; }
        public new Icon Icon { get; set; }

        private int doNotShow_Index;
        public int DoNotShow_Index
        {
            get => doNotShow_Index;
            set
            {
                doNotShow_Index = value;
                try { do_not_show.Visible = value >= 0; } catch { }
            }
        }

        public Msg()
        {
            InitializeComponent();
            do_not_show.Text = Program.Lang.String(do_not_show.Tag.ToString());
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button.Tag?.ToString()?.ToLower() == "b_ok")
                Result = MessageBox.Result.Ok;

            else if (button.Tag?.ToString()?.ToLower() == "b_cancel")
                Result = MessageBox.Result.Cancel;

            else if (button.Tag?.ToString()?.ToLower() == "b_close")
                Result = MessageBox.Result.Close;

            else if (button.Tag?.ToString()?.ToLower() == "b_yes")
                Result = MessageBox.Result.Yes;

            else if (button.Tag?.ToString()?.ToLower() == "b_no")
                Result = MessageBox.Result.No;

            else if (sender == button1)
                Result = MessageBox.Result.Button1;

            else if (sender == button2)
                Result = MessageBox.Result.Button2;

            else if (sender == button3)
                Result = MessageBox.Result.Button3;

            else
                Result = MessageBox.Result.Ok;

            DoNotShow_Clicked = do_not_show.Visible && do_not_show.Checked
                             && Result is not MessageBox.Result.No and not MessageBox.Result.Cancel;

            Close();
        }

        private void Msg_Load(object sender, EventArgs e)
        {
            // Set initial values
            // ****************
            Text = Program.Lang.ApplicationTitle;
            textLabel.Text = MsgText;
            descriptionLabel.Text = Description;
            RightToLeft = Program.Lang.GetScript(MsgText) == Language.ScriptType.RTL || Program.Lang.GetScript(Description) == Language.ScriptType.RTL ? RightToLeft.Yes : RightToLeft.No;

            Theme.ChangeColors(this, true);
            textLabel.ForeColor = Theme.Colors.Headline;

            // Set message box icon and play sound (if available)
            // ****************
            if (Icon != null) IconType = MessageBox.Icons.Custom;
            else switch (IconType)
            {
                default:
                case MessageBox.Icons.None:
                    Icon = null;
                    break;

                case MessageBox.Icons.Error:
                    System.Media.SystemSounds.Hand.Play();
                    Icon = SystemIcons.Hand;
                    break;

                case MessageBox.Icons.Information:
                    System.Media.SystemSounds.Asterisk.Play();
                    Icon = SystemIcons.Asterisk;
                    break;

                case MessageBox.Icons.Shield:
                    Icon = SystemIcons.Shield;
                    break;

                case MessageBox.Icons.Warning:
                    System.Media.SystemSounds.Exclamation.Play();
                    Icon = SystemIcons.Exclamation;
                    break;
            }
            if (Icon != null) image.Image = new Icon(Icon, 32, 32).ToBitmap();

            // Set button text
            // ****************
            try { button1.Text = Buttons[0]; button1.Visible = true; } catch { }
            try { button2.Text = Buttons[1]; button2.Visible = true; } catch { }
            try { button3.Text = Buttons[2]; button3.Visible = true; } catch { }

            // Set button sizes
            // ****************
            Theme.BtnSizes(button1, button2, button3);

            // Change visibility/position of certain elements
            // ****************
            (bool no_icon, bool no_text, bool no_desc) = (Icon == null, string.IsNullOrWhiteSpace(MsgText), string.IsNullOrWhiteSpace(Description));

            tableLayoutPanel1.Controls.Clear();

            if (no_icon)
                tableLayoutPanel1.ColumnCount = 1;

            if (no_text || no_desc)
            {
                descriptionLabel.Margin = new(descriptionLabel.Margin.Left + 1, 7, descriptionLabel.Margin.Right, descriptionLabel.Margin.Bottom);
                tableLayoutPanel1.RowCount = 1;
            }

            if (!no_icon) tableLayoutPanel1.Controls.Add(image, 0, 0);
            if (!no_text) tableLayoutPanel1.Controls.Add(textLabel, no_icon ? 0 : 1, 0);
            if (!no_desc) tableLayoutPanel1.Controls.Add(descriptionLabel, no_icon ? 0 : 1, no_text ? 0 : 1);

            // Set width
            // ****************
            var maxWidth = !no_desc && !no_text ? Math.Max(descriptionLabel.Width, textLabel.Width)
                         : !no_desc ? descriptionLabel.Width
                         : !no_text ? textLabel.Width
                         : 0;
            if (!no_icon) maxWidth += image.Width;

            maxWidth += (tableLayoutPanel1.Padding.Left * 2) + 20;
            ClientSize = new Size(Math.Min(ClientSize.Width, Math.Max(330, maxWidth)), ClientSize.Height);

            // Set height
            // ****************
            var maxHeight = !no_desc ? descriptionLabel.Location.Y + descriptionLabel.Height
                                     : textLabel.Location.Y + textLabel.Height;
            if (!no_icon) maxHeight = Math.Max(maxHeight, image.Location.Y + image.Height);
            maxHeight += (tableLayoutPanel1.Padding.Top * 2) + bottomPanel2.Height;

            ClientSize = new Size(ClientSize.Width, maxHeight);
            Size = SizeFromClientSize(ClientSize);

            // Set button locations and "do not show" checkbox
            // ****************
            Theme.BtnLayout(this, button1, button2, button3);
            const int dns_spacing = 14;

            if (RightToLeft == RightToLeft.Yes)
            {
                if (do_not_show.Visible && (button1.Location.X + button1.Width) > ClientSize.Width - dns_spacing - do_not_show.Width)
                {
                    Size = new Size(Width + do_not_show.Width + 30, Height);
                    if (button1.Visible) Size = new Size(Width - button1.Width, Height);
                    if (button2.Visible) Size = new Size(Width - button2.Width, Height);
                    if (button3.Visible) Size = new Size(Width - button3.Width, Height);
                }

                if (Theme.BtnLayout(this, button1, button2, button3))
                    Size = SizeFromClientSize(ClientSize);
                do_not_show.Location = new Point(ClientSize.Width - dns_spacing - do_not_show.Width, do_not_show.Location.Y);
                do_not_show.MaximumSize = new Size(ClientSize.Width - dns_spacing - button1.Width - button1.Location.X, 0);
            }

            else
            {
                do_not_show.Location = new Point(dns_spacing, do_not_show.Location.Y);
                if (do_not_show.Visible && do_not_show.Width > button1.Location.X)
                {
                    Size = new Size(Width + do_not_show.Width + 30, Height);
                    if (button1.Visible) Size = new Size(Width - button1.Width, Height);
                    if (button2.Visible) Size = new Size(Width - button2.Width, Height);
                    if (button3.Visible) Size = new Size(Width - button3.Width, Height);
                }

                if (Theme.BtnLayout(this, button1, button2, button3))
                    Size = SizeFromClientSize(ClientSize);
                do_not_show.MaximumSize = new Size(button1.Location.X - do_not_show.Location.X - dns_spacing, 0);
            }

            try { CenterToParent(); } catch { CenterToScreen(); }
        }

        private void Msg_Close(object sender, FormClosingEventArgs e)
        {
            if (!Result.HasValue)
            {
                DoNotShow_Clicked = false;
                Result = MessageBox.Result.Cancel;
            }
        }
    }
}
