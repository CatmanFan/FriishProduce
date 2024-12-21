using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FriishProduce
{
    class CustomToolTip : ToolTip
    {
        public CustomToolTip()
        {
            OwnerDraw = true;
            Popup += new PopupEventHandler(OnPopup);
            Draw += new DrawToolTipEventHandler(OnDraw);
        }

        private void OnPopup(object sender, PopupEventArgs e) // use this event to set the size of the tool tip
        {
            e.ToolTipSize = new Size(350, 100);
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e) // use this event to customise the tool tip
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            const int spacing_X = 8;
            const int spacing_Y = 6;
            Rectangle bounds = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

            LinearGradientBrush b = new LinearGradientBrush(bounds, Color.WhiteSmoke, Color.Gainsboro, 0f);

            g.FillRectangle(b, bounds);

            g.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(e.Bounds.X, e.Bounds.Y,
                bounds.Width - 1, bounds.Height - 1));

            string[] text = e.ToolTipText.Split('\n');
            bool isMultiline = text.Length > 1;

            int Y = e.Bounds.Y + spacing_X;
            g.DrawString(text[0], new Font(e.Font, isMultiline ? FontStyle.Bold : FontStyle.Regular), Brushes.Black,
                new PointF(bounds.X + spacing_X, Y));

            if (isMultiline)
            {
                string description = e.ToolTipText.Replace(text[0] + "\r", null).Replace(text[0] + "\n", null).TrimStart('\r', '\n');

                Y += e.Font.Height + spacing_Y;
                g.DrawLine(new Pen(Brushes.Black, 1),
                    new PointF(bounds.X + spacing_X, Y),
                    new PointF(bounds.Width - spacing_Y, Y));

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
