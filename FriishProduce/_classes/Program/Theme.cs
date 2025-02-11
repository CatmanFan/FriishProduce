using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public static class Theme
    {
        public struct colors
        {
            public Color BG { get; set; }
            public Color Bottom { get; set; }
            public Color BottomBorder { get; set; }
            public Color Text { get; set; }
        }

        public static colors Colors { get; set; }

        public enum Scheme
        {
            Light,
            Dark
        }

        public static void ChangeScheme(Scheme target = 0)
        {

            switch (target)
            {
                default:
                case Scheme.Light:
                    Colors = new()
                    {
                        BG = Color.FromArgb(255, 255, 255),
                        Bottom = Color.FromArgb(240, 240, 240),
                        BottomBorder = Color.FromArgb(223, 223, 223),
                        Text = Color.FromArgb(0, 0, 0)
                    };
                    break;

                case Scheme.Dark:
                    break;
            }
        }

        public static void BtnSizes(params Button[] btns)
        {
            const int extra = 8;
            const int min = 66;

            var button1 = btns?.Length >= 1 ? btns[0] : null;
            var button2 = btns?.Length >= 2 ? btns[1] : null;
            var button3 = btns?.Length >= 3 ? btns[2] : null;

            if (button1 != null)
            {
                button1.AutoSize = true;
                button1.AutoSizeMode = AutoSizeMode.GrowOnly;
                button1.Size = button1.MinimumSize = new Size(Math.Max(min, button1.Width + extra), button1.Height);
            }

            if (button2 != null)
            {
                button2.AutoSize = true;
                button2.AutoSizeMode = AutoSizeMode.GrowOnly;
                button2.Size = button2.MinimumSize = new Size(Math.Max(min, button2.Width + extra), button2.Height);
            }

            if (button3 != null)
            {
                button3.AutoSize = true;
                button3.AutoSizeMode = AutoSizeMode.GrowOnly;
                button3.Size = button3.MinimumSize = new Size(Math.Max(min, button3.Width + extra), button3.Height);
            }
        }

        public static void BtnLayout(Form frm, params Button[] btns)
        {
            const int spacing = 8;

            var button1 = btns?.Length >= 1 ? btns[0] : null;
            var button2 = btns?.Length >= 2 ? btns[1] : null;
            var button3 = btns?.Length >= 3 ? btns[2] : null;

            bool has1 = button1?.Visible ?? false;
            bool has2 = button2?.Visible ?? false;
            bool has3 = button3?.Visible ?? false;

            if (!has1 && !has2 && !has3)
            {
                has1 = button1 != null;
                has2 = button2 != null;
                has3 = button3 != null;
            }

            if (frm.RightToLeft == RightToLeft.Yes)
            {
                if (button1 != null) button1.Location = new Point(spacing, button1.Location.Y);
                if (button2 != null) button2.Location = new Point(spacing, button1.Location.Y);
                if (button3 != null) button3.Location = new Point(spacing, button1.Location.Y);

                if (has2 && has3)
                {
                    button2.Location = new Point(button3.Location.X + button2.Width + spacing, button1.Location.Y);
                    button1.Location = new Point(button2.Location.X + button1.Width + spacing, button1.Location.Y);
                }

                if (!has2 && has3)
                {
                    button1.Location = new Point(button3.Location.X + button1.Width + spacing, button1.Location.Y);
                }

                if (has2 && !has3)
                {
                    button1.Location = new Point(button2.Location.X + button1.Width + spacing, button1.Location.Y);
                }
            }

            else
            {
                if (button1 != null) button1.Location = new Point(frm.ClientSize.Width - spacing - button1.Width, button1.Location.Y);
                if (button2 != null) button2.Location = new Point(frm.ClientSize.Width - spacing - button2.Width, button1.Location.Y);
                if (button3 != null) button3.Location = new Point(frm.ClientSize.Width - spacing - button3.Width, button1.Location.Y);

                if (has2 && has3)
                {
                    button2.Location = new Point(button3.Location.X - spacing - button2.Width, button1.Location.Y);
                    button1.Location = new Point(button2.Location.X - spacing - button1.Width, button1.Location.Y);
                }

                if (!has2 && has3)
                {
                    button1.Location = new Point(button3.Location.X - spacing - button1.Width, button1.Location.Y);
                }

                if (has2 && !has3)
                {
                    button1.Location = new Point(button2.Location.X - spacing - button1.Width, button1.Location.Y);
                }
            }
        }
    }
}
