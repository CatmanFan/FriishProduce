using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace FriishProduce
{
    class PlaceholderTextBox : TextBox
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
        [Description("")]
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

    class CustomToolTip : ToolTip
    {
        private const int GCL_STYLE = -26;
        private const int CS_DROPSHADOW = 0x20000;

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        private static extern int GetClassLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        private static extern int SetClassLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public CustomToolTip()
        {
            OwnerDraw = true;
            UseAnimation = false;
            UseFading = true;
            Popup += new PopupEventHandler(OnPopup);
            Draw += new DrawToolTipEventHandler(OnDraw);
        }

        private void OnPopup(object sender, PopupEventArgs e) // use this event to set the size of the tool tip
        {
            e.ToolTipSize = new Size(300, 100);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) // use this event to customise the tool tip
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            const int spacing_X = 8;
            const int spacing_Y = 6;

            string[] text = e.ToolTipText.Split('\n');
            bool isMultiline = text.Length > 1;

            Rectangle bounds = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

            using LinearGradientBrush b = new LinearGradientBrush(bounds, Color.White, Color.FromArgb(239, 240, 246), 90f);
            g.FillRectangle(b, bounds);

            e.DrawBorder();

            int Y = e.Bounds.Y + spacing_X;
            g.DrawString(text[0], new Font(e.Font, isMultiline ? FontStyle.Bold : FontStyle.Regular), Brushes.Black,
                new PointF(bounds.X + spacing_X, Y));

            if (isMultiline)
            {
                string description = e.ToolTipText.Replace(text[0] + "\r", null).Replace(text[0] + "\n", null).TrimStart('\r', '\n');

                Y += e.Font.Height + spacing_Y;
                g.DrawLine(new Pen(Brushes.Black, 1),
                    new PointF(bounds.X + spacing_X, Y),
                    new PointF(bounds.Width - spacing_X, Y));

                Y += spacing_Y;
                g.DrawString(description, new Font(e.Font, FontStyle.Regular), Brushes.Black,
                    new RectangleF(bounds.X + spacing_X, Y, bounds.Width - spacing_X * 2, bounds.Height - spacing_Y * 2));
            }

            b.Dispose();
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
}
