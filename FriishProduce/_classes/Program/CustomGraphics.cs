using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FriishProduce
{
    public class PlaceholderTextBox : TextBox
    {
        private string placeholderText = string.Empty;
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);
        public PlaceholderTextBox() : base()
        {

        }

        [Localizable(true)]
        [DefaultValue("")]
        [Description("Placeholder text to display if there is no value.")]
        public virtual string PlaceholderText
        {
            get
            {
                return placeholderText;
            }
            set
            {
                if (value is null)
                {
                    value = string.Empty;
                }

                if (placeholderText != value)
                {
                    placeholderText = value;
                    if (IsHandleCreated)
                    {
                        Invalidate();
                        SetPlaceholderText();
                    }
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetPlaceholderText();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage
      (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        public void SetPlaceholderText()
        {
            SendMessage(Handle, EM_SETCUEBANNER, 0, placeholderText);
        }
    }

    class ImageLabel : Label
    {
        const int spacing = 4;

        public ImageLabel()
        {
            ImageAlign = ContentAlignment.TopLeft;
            TextAlign = ContentAlignment.TopLeft;
            Padding = new Padding(spacing);
        }

        private Image _image;
        public new Image Image
        {
            get { return _image; }

            set
            {
                if (_image != null)
                    Padding = new Padding(spacing, Padding.Top, Padding.Right, Padding.Bottom);

                if (value != null)
                    Padding = new Padding(value.Width + spacing + 2, Padding.Top, Padding.Right, Padding.Bottom);

                _image = value;

                int height = _image != null ? _image.Height + Padding.Top + Padding.Bottom : Padding.Top + Padding.Bottom;
                if (AutoSize) base.SetBoundsCore(Left, Top, Width, height, BoundsSpecified.Size);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image != null)
            {
                Rectangle r = CalcImageRenderBounds(Image, new Rectangle(ClientRectangle.X + 2, ClientRectangle.Y - 3 + Padding.Top, ClientRectangle.Width, ClientRectangle.Height), ImageAlign);
                e.Graphics.DrawImage(Image, r);
            }

            base.OnPaint(e); // Paint text
        }
    }

    public class GroupBoxEx : GroupBox
    {
        public GroupBoxEx() : base()
        {

        }

        public bool Flat { get; set; } = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Flat)
            {
                BackColor = Theme.Colors.Form.BG;
                ForeColor = Theme.Colors.Text;

                (int x, int y) spacing = (2, 5);

                var rect = new Rectangle(ClientRectangle.X, ClientRectangle.Y + 5, ClientRectangle.Width - 1, ClientRectangle.Height - 6);

                GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var pen = new Pen(Theme.Colors.Form.Border, 1))
                    e.Graphics.DrawRectangle(pen, rect);

                using (var brush = new SolidBrush(BackColor))
                    e.Graphics.FillRectangle(brush, spacing.x, 0, TextRenderer.MeasureText(Text, Program.MainForm.Font).Width, TextRenderer.MeasureText(Text, Font).Height + spacing.y);
                TextRenderer.DrawText(e.Graphics, Text, Font, new Point(spacing.x * 2, 0), Enabled ? ForeColor : SystemColors.GrayText);

                var clip = e.Graphics.ClipBounds;
                e.Graphics.SetClip(clip);
            }
        }
    }

    public class NumericUpDownEx : NumericUpDown
    {
        public NumericUpDownEx() : base()
        {

        }

        private string prefix = string.Empty;
        private string suffix = string.Empty;

        [Localizable(true)]
        [DefaultValue("")]
        [Description("The string to be displayed before the value.")]
        public virtual string Prefix
        {
            get => prefix;
            set
            {
                prefix = value;
                UpdateEditText();
            }
        }


        [Localizable(true)]
        [DefaultValue("")]
        [Description("The string to be displayed after the value.")]
        public string Suffix
        {
            get => suffix;
            set
            {
                suffix = value;
                UpdateEditText();
            }
        }

        protected override void UpdateEditText()
        {
            base.UpdateEditText();

            ChangingText = true;
            Text = prefix + Text + suffix;
        }
    }
}
