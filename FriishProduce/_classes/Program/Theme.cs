using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public static class Theme
    {
        public static bool Active { get; set; } = false;

        public class colors
        {
            public Color ToolStrip_Top { get; set; } = Color.White;
            public Color ToolStrip_Bottom { get; set; } = Color.Gainsboro;
            public Color ToolStrip_Border { get; set; } = Color.FromArgb(200, 200, 200);
            public Color LogoBG_Top { get; set; } = Color.FromArgb(228, 228, 228);
            public Color LogoBG_Bottom { get; set; } = Color.FromArgb(250, 250, 250);


            public struct form
            {
                public Color BG;
                public Color Border;
                public Color Bottom;
            }

            public form? Controls { get; set; } = null;

            public form Form { get; set; } = new()
            {
                BG = Color.FromArgb(240, 240, 240),
                Border = Color.FromArgb(200, 200, 200),
                Bottom = Color.FromArgb(225, 225, 225)
            };

            public form Dialog { get; set; } = new()
            {
                BG = Color.White,
                Border = Color.FromArgb(223, 223, 223),
                Bottom = Color.FromArgb(240, 240, 240)
            };

            public Color Text { get; set; } = Color.Black;
            public Color Headline { get; set; } = Color.FromArgb(0, 51, 153);
            public Color TextBox { get; set; } = Color.FromArgb(237, 237, 237);

            public Color Highlight { get; set; } = Color.FromArgb(231, 238, 247); // Color.FromArgb(200, 225, 240);
            public Color HighlightBorder { get; set; } = Color.FromArgb(174, 207, 247); // Color.FromArgb(180, 214, 234);
        }

        public static colors Colors { get; set; }

        public enum Scheme
        {
            Default = 0,
            Dark
        }

        /// <summary>
        /// Changes the theme for the app. This is only used once, when the GUI starts.
        /// </summary>
        public static void ChangeScheme(int index = -1)
        {
            Active = index >= 0;
            Scheme theme = 0;

            if (index >= 0)
            {
                try { theme = (Scheme)index; } catch { theme = 0; }
                Logger.Log($"Current theme is set to {theme}.");
            }

            ChangeScheme(theme);
        }

        /// <summary>
        /// Changes the theme for the app. This is only used once, when the GUI starts.
        /// </summary>
        public static void ChangeScheme(Scheme target = 0)
        {
            switch (target)
            {
                default:
                case 0:
                    Colors = new();
                    break;

                case Scheme.Dark:
                    Colors = new()
                    {
                        ToolStrip_Top = Color.FromArgb(50, 50, 50),
                        ToolStrip_Bottom = Color.FromArgb(28, 28, 28),
                        ToolStrip_Border = Color.FromArgb(70, 70, 70),
                        LogoBG_Top = Color.FromArgb(26, 26, 26),
                        LogoBG_Bottom = Color.FromArgb(60, 60, 60),

                        Controls = new()
                        {
                            BG = Color.FromArgb(53, 53, 53),
                            Border = Color.FromArgb(112, 112, 112),
                        },
                        Form = new()
                        {
                            BG = Color.FromArgb(46, 46, 46),
                            Border = Color.FromArgb(70, 70, 70),
                            Bottom = Color.FromArgb(57, 57, 57)
                        },
                        Dialog = new()
                        {
                            BG = Color.FromArgb(47, 47, 47),
                            Border = Color.FromArgb(70, 70, 70),
                            Bottom = Color.FromArgb(57, 57, 57)
                        },

                        Text = Color.White,
                        Headline = Color.FromArgb(15, 220, 255),
                        TextBox = Color.FromArgb(57, 57, 57),

                        Highlight = Color.FromArgb(67, 110, 140),
                        HighlightBorder = Color.FromArgb(77, 139, 181),
                    };
                    break;
            }
        }

        /// <summary>
        /// Generates background for main form.
        /// </summary>
        public static Bitmap GenerateBG(Rectangle r)
        {
            Bitmap b = new(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Colors.LogoBG_Bottom);
                g.SmoothingMode = SmoothingMode.HighSpeed;

                using (SolidBrush brush = new(Colors.LogoBG_Top))
                    g.FillRectangle(brush, new Rectangle(Point.Empty, new(r.Width, 30)));

                using (LinearGradientBrush brush = new(new Rectangle(0, 20, r.Width, 70), Colors.LogoBG_Top, Colors.LogoBG_Bottom, LinearGradientMode.Vertical))
                    g.FillRectangle(brush, brush.Rectangle);
            }

            /* Bitmap b = new(20, 20);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.HighSpeed;

                using var b1 = new SolidBrush(Colors.LogoBG_Top);
                using var b2 = new SolidBrush(Colors.LogoBG_Bottom);

                g.FillRectangle(b2, 0, 0, b.Width, b.Height);
                g.FillPolygon(b1, new Point[] { new Point(10, 0), new Point(20, 10), new Point(10, 20), new Point(0, 10) });
            } */

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return b;
        }

        #region ## CHANGING COLORS OF FORMS AND CONTROLS ##

        /// <summary>
        /// Sets main colors of form, and if also needed, the styling of relevant subcontrols.
        /// </summary>
        /// <param name="isDialog">Determines whether to use the form or dialog color scheme.</param>
        public static bool ChangeColors(Form frm, bool isDialog)
        {
            if (!Active && !isDialog) return false;

            if (Active && Colors.Controls.HasValue)
            {
                foreach (Control item1 in frm.Controls)
                {
                    colorize(item1);
                    foreach (Control item2 in item1.Controls)
                    {
                        colorize(item2);
                        foreach (Control item3 in item2.Controls)
                        {
                            colorize(item3);
                            foreach (Control item4 in item3.Controls)
                            {
                                colorize(item4);
                                foreach (Control item5 in item4.Controls)
                                {
                                    colorize(item5);
                                }
                            }
                        }
                    }
                }
            }

            frm.BackColor = isDialog ? Colors.Dialog.BG : Colors.Form.BG;
            frm.ForeColor = Colors.Text;

            foreach (var bottomPanel2 in frm.Controls.OfType<Panel>().Where(x => x.Name.ToLower() == "bottompanel2"))
            {
                bottomPanel2.BackColor = isDialog ? Colors.Dialog.Border : Colors.Form.Border;

                foreach (var bottomPanel1 in bottomPanel2.Controls.OfType<Panel>().Where(x => x.Name.ToLower() == "bottompanel1"))
                    bottomPanel1.BackColor = isDialog ? Colors.Dialog.Bottom : Colors.Form.Bottom;
            }

            return true;
        }

        /// <summary>
        /// Function for subcontrols
        /// </summary>
        private static void colorize(Control c)
        {
            bool isEligible = false;
            bool useFormBG = false;
            bool useTextBox = false;

            if (c.GetType() == typeof(Button))
            {
                isEligible = true;
                (c as Button).FlatStyle = FlatStyle.Flat;
                (c as Button).FlatAppearance.BorderColor = Colors.Controls.Value.Border;
            }

            if (c.GetType() == typeof(ComboBox))
            {
                isEligible = true;
                (c as ComboBox).FlatStyle = FlatStyle.Flat;
            }

            if (c.GetType() == typeof(RadioButton))
            {
                useFormBG = true;
                isEligible = true;
                (c as RadioButton).FlatStyle = FlatStyle.Flat;
            }

            if (c.GetType() == typeof(CheckBox))
            {
                useFormBG = true;
                isEligible = true;
                (c as CheckBox).FlatStyle = FlatStyle.Flat;
            }

            if (c.GetType() == typeof(GroupBox))
            {
                useFormBG = true;
                isEligible = true;
                (c as GroupBox).FlatStyle = FlatStyle.Flat;
            }

            if (c.GetType() == typeof(TabPage))
            {
                useFormBG = true;
                isEligible = true;
            }

            if (c.GetType() == typeof(TextBox))
            {
                useTextBox = true;
                isEligible = true;
            }

            if (c.GetType() == typeof(TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel))
            {
                useFormBG = true;
                isEligible = true;
                (c as TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel).BorderStyle = BorderStyle.Fixed3D;
            }

            if (isEligible)
            {
                c.BackColor = useTextBox ? Colors.TextBox : useFormBG ? Colors.Form.BG : Colors.Controls.Value.BG;
                c.ForeColor = Colors.Text;
            }
        }

        /// <summary>
        /// Custom renderer for the main form's tool strip.
        /// </summary>
        public class CustomToolStrip : ToolStripSystemRenderer
        {
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip.GetType() != typeof(ToolStripDropDownMenu))
                {
                    Rectangle r = new(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width, e.AffectedBounds.Height + 5);

                    using (LinearGradientBrush b = new(new(0, 0), new(0, r.Height), Colors.ToolStrip_Top, Colors.ToolStrip_Bottom))
                        e.Graphics.FillRectangle(b, r);
                }

                else
                {
                    using (SolidBrush brush = new(Colors.Form.BG))
                        e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip.GetType() != typeof(ToolStripDropDownMenu))
                {
                    using (Pen pen = new(Colors.ToolStrip_Border, 2f))
                        e.Graphics.DrawLine(pen, new(0, e.AffectedBounds.Height), new(e.AffectedBounds.Width, e.AffectedBounds.Height));
                }

                else
                    base.OnRenderToolStripBorder(e);
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);

                if (e.Item.Selected)
                {
                    rc.X += 1;
                    rc.Width -= 1;

                    using (SolidBrush brush = new(Colors.Highlight))
                        e.Graphics.FillRectangle(brush, rc);

                    Rectangle r = new(0, 6, rc.Width, rc.Height * 3);
                    using (LinearGradientBrush brush = new(r, Colors.Highlight, Colors.HighlightBorder, LinearGradientMode.Vertical))
                        e.Graphics.FillRectangle(brush, new(r.X, r.Y + 1, r.Width, r.Height));

                    using (Pen pen = new(Colors.HighlightBorder, 2f))
                        e.Graphics.DrawRectangle(pen, rc);
                }
                else
                {
                    using (SolidBrush brush = new(Colors.Form.BG))
                        e.Graphics.FillRectangle(brush, rc);
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.Item.ForeColor = Colors.Text;

                base.OnRenderItemText(e);
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                if ((e.Item as ToolStripSeparator) == null)
                {
                    base.OnRenderSeparator(e);
                    return;
                }

                Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
                bounds.Y += 3;
                bounds.Height = Math.Max(0, bounds.Height - 6);
                if (bounds.Height >= 4)
                    bounds.Inflate(0, -2);

                using (Pen pen = new Pen(Colors.Form.Border, 1.5f))
                {
                    int x = bounds.Width / 2;
                    if (e.Vertical)
                        e.Graphics.DrawLine(pen, x, bounds.Top, x, bounds.Bottom);
                    else
                        e.Graphics.DrawLine(pen, bounds.Left + 3, bounds.Bottom, bounds.Right - 3, bounds.Bottom);
                }
            }
        }

        #endregion

        /// <summary>
        /// Automatically sets the sizes of each dialog button, and forces the font to Segoe UI.
        /// </summary>
        /// <param name="btns">The buttons from the dialog form. May only support a maximum of three.</param>
        public static void BtnSizes(params Button[] btns)
        {
            const int extra = 8;
            const int min = 66;

            var button1 = btns?.Length >= 1 ? btns[0] : null;
            var button2 = btns?.Length >= 2 ? btns[1] : null;
            var button3 = btns?.Length >= 3 ? btns[2] : null;

            foreach (var btn in btns)
            {
                btn.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                btn.AutoSize = true;
                btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                btn.Font = new Font("Segoe UI", 9f);
                btn.MaximumSize = btn.MinimumSize = new(0, 24);
            }

            if (button1 != null)
            {
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

        /// <summary>
        /// Automatically sets the positions of each dialog button.
        /// </summary>
        /// <param name="frm">The dialog form, to use as a reference for the client size.</param>
        /// <param name="btns">The buttons from the dialog form. May only support a maximum of three.</param>
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
