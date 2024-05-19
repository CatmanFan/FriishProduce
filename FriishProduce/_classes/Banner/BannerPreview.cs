using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FriishProduce
{
    public static class Preview
    {
        public enum Language
        {
            America,
            Japanese,
            Korean,
            Europe,
            Auto
        }

        private static readonly Color[][] ColorSchemes = BannerSchemes.List;

        #region Font Functions

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private static PrivateFontCollection fonts;

        private static FontFamily Font()
        {
            if (fonts != null && fonts.Families.Length > 0) return fonts.Families[0];

            fonts = new PrivateFontCollection();

            byte[] fontData = Properties.Resources.Font;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Font.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Font.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            return fonts.Families[0];
        }

        #endregion

        public static Bitmap Banner(Console console, string text, int year, int players, Bitmap img, Language lang)
        {
            Bitmap bmp = new Bitmap(650, 260);
            if (img == null)
            {
                img = new Bitmap(256, 192);
                using (Graphics g = Graphics.FromImage(img))
                    g.Clear(Color.Gainsboro);
            }

            int target = 0;
            switch (console)
            {
                case Console.NES:
                    target = lang switch { Language.Japanese or Language.Korean => 1, _ => 0 };
                    break;

                case Console.SNES:
                    target = lang switch { Language.Japanese or Language.Korean => 3, _ => 2 };
                    break;

                case Console.N64:
                    target = 4;
                    break;

                case Console.SMS:
                    target = 5;
                    break;

                case Console.SMD:
                    target = 6;
                    break;

                case Console.PCE:
                case Console.PCECD:
                    target = lang switch { Language.Japanese or Language.Korean => 8, _ => 7 };
                    break;

                case Console.NEO:
                    target = 9;
                    break;

                case Console.C64:
                    target = 11;
                    break;

                case Console.MSX:
                    target = 12;
                    break;

                case Console.Flash:
                    target = 14;
                    break;

                case Console.RPGM:
                    target = 15;
                    break;
            }

            var leftTextColor = target == 2 ? Color.Black : target == 8 ? Color.FromArgb(90, 90, 90) : ColorSchemes[target][0].GetBrightness() < 0.8 ? Color.White : Color.FromArgb(50, 50, 50);

            string released = lang == Language.Japanese ? "{0}年発売"
                             : lang == Language.Korean ? "일본판 발매년도\r\n{0}년"
                             : Program.Lang.Current == "nl" ? "Release: {0}"
                             : Program.Lang.Current == "es" ? "Año: {0}"
                             : Program.Lang.Current == "it" ? "Pubblicato: {0}"
                             : Program.Lang.Current == "fr" ? "Publié en {0}"
                             : Program.Lang.Current == "de" ? "Erschienen: {0}"
                             : "Released: {0}";

            string numPlayers = lang == Language.Japanese ? "プレイ人数\r\n{0}人"
                              : lang == Language.Korean ? "플레이 인원수\r\n{0}명"
                              : Program.Lang.Current == "nl" ? "{0} speler(s)"
                              : Program.Lang.Current == "es" ? "Jugadores: {0}"
                              : Program.Lang.Current == "it" ? "Giocatori: {0}"
                              : Program.Lang.Current == "fr" ? "Joueurs: {0}"
                              : Program.Lang.Current == "de" ? "{0} Spieler"
                              : "Players: {0}";

            using (Graphics g = Graphics.FromImage(bmp))
            using (LinearGradientBrush b1 = new LinearGradientBrush(new Point(0, 130), new Point(0, (int)Math.Round(bmp.Height * 0.9)), ColorSchemes[target][0], ColorSchemes[target][2]))
            using (LinearGradientBrush b2 = new LinearGradientBrush(new Point(0, 50), new Point(0, (int)Math.Round(bmp.Height * 1.25)), ColorSchemes[target][0], ColorSchemes[target][2]))
            using (LinearGradientBrush c = new LinearGradientBrush(new Point(0, 0), new Point(125, 0), ColorSchemes[target][3], ColorSchemes[target][0]))
            {
                g.CompositingQuality = CompositingQuality.AssumeLinear;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                g.Clear(ColorSchemes[target][0]);
                g.FillRectangle(b1, -5, (bmp.Height / 2) + 10, bmp.Width + 10, 40);
                g.FillRectangle(b2, -5, (bmp.Height / 2) + 49, bmp.Width + 10, bmp.Height);

                #region Text
                // Title
                // ********
                g.DrawString(
                    text,
                    new Font(Font(), 15),
                    new SolidBrush(BannerSchemes.TextColor(target)),
                    bmp.Width / 2,
                    210,
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                // Released
                // ********
                g.DrawString(
                    string.Format(released, year),
                    new Font(Font(), (float)9.25),
                    new SolidBrush(leftTextColor),
                    10,
                    45,
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far });

                // Players
                // ********
                g.DrawString(
                    string.Format(numPlayers, $"{1}{(players <= 1 ? null : "-" + players)}").Replace("-", lang == Language.Japanese ? "～" : "-"),
                    new Font(Font(), (float)9.25),
                    new SolidBrush(leftTextColor),
                    10,
                    87,
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far });
                #endregion

                // Gradient lines
                // ********
                g.FillRectangle(c, -1, 45, 125, 2);
                g.FillRectangle(c, -1, 87, 125, 2);

                #region Image
                double[] point = new double[] { (bmp.Width / 2) - Math.Round((img.Width * 0.72) / 2), 40 };
                double[] size = new double[] { Math.Round(img.Width * 0.72), Math.Round(img.Height * 0.72) };

                using (Bitmap border = new Bitmap(256, 192))
                {
                    using (Graphics gBorder = Graphics.FromImage(border))
                        gBorder.Clear(Color.Black);
                    g.DrawImage(RoundCorners(border, 9), (int)point[0] - 1, (int)point[1] - 1, (int)size[0] + 2, (int)size[1] + 2);
                }
                g.DrawImage(RoundCorners(img, 8), (int)point[0], (int)point[1], (int)size[0], (int)size[1]);
                #endregion

                #region Top Console Header
                var cName = "FriishProduce";
                switch (console)
                {
                    case Console.NES:
                        cName = lang switch { Language.Japanese => "ファミリーコンピュータ", Language.Korean => "패밀리컴퓨터", _ => "NINTENDO ENTERTAINMENT SYSTEM" };
                        break;

                    case Console.SNES:
                        cName = lang switch { Language.Japanese => "スーパーファミコン", Language.Korean => "슈퍼 패미컴", _ => "SUPER NINTENDO ENTERTAINMENT SYSTEM" };
                        break;

                    case Console.N64:
                        cName = lang switch { Language.Korean => "닌텐도 64", _ => "NINTENDO64" };
                        break;

                    case Console.SMS:
                        cName = "MASTER SYSTEM";
                        break;

                    case Console.SMD:
                        cName = lang > 0 ? "MEGA DRIVE" : "GENESIS";
                        break;

                    case Console.PCE:
                    case Console.PCECD:
                        cName = lang switch { Language.Japanese or Language.Korean => "PC ENGINE", _ => "TURBO GRAFX16" };
                        break;

                    case Console.NEO:
                        cName = "NEO-GEO";
                        break;

                    case Console.C64:
                        cName = "COMMODORE 64";
                        break;

                    case Console.MSX:
                        cName = "MSX";
                        break;

                    case Console.Flash:
                        cName = "    Flash";
                        break;

                    case Console.GB:
                        cName = "GAME BOY";
                        break;

                    case Console.GBC:
                        cName = "GAME BOY COLOR";
                        break;

                    case Console.GBA:
                        cName = "GAME BOY ADVANCE";
                        break;

                    case Console.GCN:
                        cName = "GAMECUBE";
                        break;

                    case Console.PSX:
                        cName = "PLAYSTATION";
                        break;

                    case Console.RPGM:
                        cName = "RPG MAKER";
                        break;
                }

                var f = new Font(Font(), 9);
                var brush = new SolidBrush(ColorSchemes[target][5]);

                var p = new GraphicsPath();
                p.AddLine(-5, -5, -5, 5);
                p.AddLine(-5, 5, bmp.Width, 5);
                p.AddLine(bmp.Width - TextRenderer.MeasureText(cName, f).Width - 58, 5,
                          bmp.Width - TextRenderer.MeasureText(cName, f).Width - 50, 7);
                p.AddLine(bmp.Width - TextRenderer.MeasureText(cName, f).Width - 50, 7,
                          bmp.Width - TextRenderer.MeasureText(cName, f).Width - 46, 9);
                p.AddLine(bmp.Width - TextRenderer.MeasureText(cName, f).Width - 46, 9,
                          bmp.Width - TextRenderer.MeasureText(cName, f).Width - 26, 28);
                p.AddLine(bmp.Width - TextRenderer.MeasureText(cName, f).Width - 26, 28,
                          bmp.Width - TextRenderer.MeasureText(cName, f).Width - 22, 30);
                p.AddLine(bmp.Width - TextRenderer.MeasureText(cName, f).Width - 22, 30,
                          bmp.Width - TextRenderer.MeasureText(cName, f).Width - 14, 32);
                p.AddLine(bmp.Width - TextRenderer.MeasureText(cName, f).Width - 14, 32, bmp.Width + 5, 32);
                p.AddLine(bmp.Width + 5, 32, bmp.Width + 5, -5);

                g.DrawPath(new Pen(ColorSchemes[target][4], 2), p);
                g.FillPath(brush, p);
                g.DrawString(cName, f, new SolidBrush(ColorSchemes[target][6]),
                    bmp.Width - 10,
                    24,
                    new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                #endregion
            }

            return bmp;
        }

        public static Bitmap Icon(Bitmap img)
        {
            // 0s - 1s = Fadein
            // 1s - 3s = Logo
            // 3s - 4s = Fadeout
            // 4s - 9s = Title

            if (img == null)
            {
                img = new Bitmap(128, 96);
                using (Graphics g = Graphics.FromImage(img))
                    g.Clear(Color.Gainsboro);
            }

            Bitmap bmp = new Bitmap(img.Width, 92);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.Bicubic;
                g.DrawImage(img, -5, -4, bmp.Width + 11, bmp.Height + 11);
            }

            Bitmap bmp2 = new Bitmap(bmp.Width + 2, bmp.Height + 2);

            using (Graphics g = Graphics.FromImage(bmp2))
            {
                using (Bitmap border = new Bitmap(bmp2.Width, bmp2.Height))
                {
                    using (Graphics gBorder = Graphics.FromImage(border))
                        gBorder.Clear(SystemColors.GrayText);
                    g.DrawImage(RoundCorners(border, 10, true), 0, 0, bmp2.Width, bmp2.Height);
                }

                g.DrawImage(RoundCorners(bmp, 10, true), 1, 1, bmp.Width, bmp.Height);
            }

            bmp.Dispose();
            return bmp2;
        }

        private static Bitmap RoundCorners(Image StartImage, int CornerRadius, bool Smooth = false)
        {
            CornerRadius *= 2;
            Bitmap x = new Bitmap(StartImage.Width, StartImage.Height);
            using (Graphics g = Graphics.FromImage(x))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Brush brush = new TextureBrush(StartImage);
                GraphicsPath gp1 = new GraphicsPath();

                int offset = Smooth ? 1 : 0;

                gp1.AddArc(offset, offset, CornerRadius, CornerRadius, 180, 90);
                gp1.AddArc(offset + x.Width - CornerRadius, offset, CornerRadius - (offset * 2), CornerRadius - (offset * 2), 270, 90);
                gp1.AddArc(offset + x.Width - CornerRadius, offset + x.Height - CornerRadius, CornerRadius - (offset * 2), CornerRadius - (offset * 2), 0, 90);
                gp1.AddArc(offset, offset + x.Height - CornerRadius, CornerRadius - (offset * 2), CornerRadius - (offset * 2), 90, 90);
                g.FillPath(brush, gp1);

                if (Smooth)
                {
                    GraphicsPath gp2 = new GraphicsPath();
                    gp2.AddCurve(new Point[] { new Point(offset, CornerRadius / 2), new Point(0, x.Height / 2), new Point(offset, x.Height - CornerRadius / 2) });
                    gp2.AddCurve(new Point[] { new Point(x.Width - offset, CornerRadius / 2), new Point(x.Width, x.Height / 2), new Point(x.Width - offset, x.Height - CornerRadius / 2) });
                    gp2.AddCurve(new Point[] { new Point(CornerRadius / 2, offset), new Point(x.Width / 2, 0), new Point(x.Width - CornerRadius / 2, offset) });
                    gp2.AddCurve(new Point[] { new Point(CornerRadius / 2, x.Height - offset), new Point(x.Width / 2, x.Height), new Point(x.Width - CornerRadius / 2, x.Height - offset) });
                    g.FillPath(brush, gp2);
                }

                return x;
            }
        }

        #region Icon
        #endregion
    }
}
