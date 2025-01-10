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
