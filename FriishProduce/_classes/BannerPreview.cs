using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class BannerPreviewX
    {

        private static Color[][] ColorSchemes = new Color[][]
        {
            // ****************

            /* NES */           new Color[]
                                {
                                    Color.FromArgb(250, 250, 250),  // Main Background Color
                                    Color.FromArgb(255, 230, 230),  // Background Logo(s)
                                    Color.FromArgb(100, 100, 100),  // Bottom Section

                                    Color.FromArgb(155, 155, 155),  // Left Lines

                                    Color.FromArgb(144, 144, 144),  // PFLine Border
                                    Color.FromArgb(210, 210, 210),  // PFLine BG Color
                                    Color.FromArgb(230, 40, 40)     // Text_PF
                                },

            /* FC */            new Color[]
                                {
                                    Color.FromArgb(250, 250, 250),
                                    Color.FromArgb(240, 240, 240),
                                    Color.FromArgb(200, 200, 200),

                                    Color.FromArgb(155, 155, 155),

                                    Color.FromArgb(122, 48, 48),
                                    Color.FromArgb(217, 31, 31),
                                    Color.FromArgb(230, 230, 40)
                                },

            /* SNES */          new Color[]
                                {
                                    Color.FromArgb(209, 209, 209),
                                    Color.FromArgb(200, 200, 200),
                                    Color.FromArgb(60, 40, 70),

                                    Color.FromArgb(120, 120, 120),

                                    Color.FromArgb(153, 153, 153),
                                    Color.FromArgb(255, 255, 255),
                                    Color.FromArgb(145, 145, 145)
                                },

            /* SFC */           new Color[]
                                {
                                    Color.FromArgb(209, 209, 209),
                                    Color.FromArgb(200, 200, 200),
                                    Color.FromArgb(60, 40, 70),

                                    Color.FromArgb(50, 50, 50),

                                    Color.FromArgb(127, 88, 149),
                                    Color.FromArgb(190, 146, 255),
                                    Color.FromArgb(100, 60, 120)
                                },

            /* N64 */           new Color[]
                                {
                                    Color.FromArgb(254, 254, 254),
                                    Color.FromArgb(240, 242, 255),
                                    Color.FromArgb(0, 0, 120),

                                    Color.FromArgb(80, 80, 255),

                                    Color.FromArgb(30, 30, 120),
                                    Color.FromArgb(80, 80, 255),
                                    Color.FromArgb(255, 255, 255)
                                },

            /* SMS */           new Color[]
                                {
                                    Color.FromArgb(32, 32, 32),
                                    Color.FromArgb(44, 44, 44),
                                    Color.FromArgb(191, 191, 191),

                                    Color.FromArgb(185, 28, 34),

                                    Color.FromArgb(0, 0, 0),
                                    Color.FromArgb(241, 84, 90),
                                    Color.FromArgb(255, 255, 255)
                                },

            /* SMD */           new Color[]
                                {
                                    Color.FromArgb(40, 40, 40),
                                    Color.FromArgb(47, 47, 47),
                                    Color.FromArgb(180, 180, 180),

                                    Color.FromArgb(157, 63, 105),

                                    Color.FromArgb(0, 0, 0),
                                    Color.FromArgb(157, 63, 105),
                                    Color.FromArgb(255, 255, 255)
                                },

            /* TG-16 */         new Color[]
                                {
                                    Color.FromArgb(78, 78, 78),
                                    Color.FromArgb(95, 95, 95),
                                    Color.FromArgb(0, 0, 0),

                                    Color.FromArgb(180, 180, 180),

                                    Color.FromArgb(255, 80, 0),
                                    Color.FromArgb(64, 64, 64),
                                    Color.FromArgb(255, 80, 0)
                                },

            /* PCE */           new Color[]
                                {
                                    Color.FromArgb(255, 255, 255),
                                    Color.FromArgb(255, 232, 218),
                                    Color.FromArgb(40, 40, 50),

                                    Color.FromArgb(138, 138, 138),

                                    Color.FromArgb(255, 80, 0),
                                    Color.FromArgb(255, 255, 255),
                                    Color.FromArgb(255, 80, 0)
                                },

            /* NEO-GEO */       new Color[]
                                {
                                    Color.FromArgb(223, 223, 223),
                                    Color.FromArgb(220, 212, 198),
                                    Color.FromArgb(255, 248, 152),

                                    Color.FromArgb(191, 139, 0),

                                    Color.FromArgb(184, 31, 24),
                                    Color.FromArgb(255, 227, 29),
                                    Color.FromArgb(160, 107, 0)
                                },

            /* NEO-GEO MVS */   new Color[]
                                {
                                    Color.FromArgb(223, 223, 223),
                                    Color.FromArgb(198, 198, 198),
                                    Color.FromArgb(100, 100, 100),

                                    Color.FromArgb(191, 191, 191),

                                    Color.FromArgb(0, 0, 0),
                                    Color.FromArgb(255, 51, 51),
                                    Color.FromArgb(255, 255, 255)
                                },

            /* C64 */           new Color[]
                                {
                                    Color.FromArgb(209, 209, 209),
                                    Color.FromArgb(201, 201, 201),
                                    Color.FromArgb(37, 72, 186),

                                    Color.FromArgb(79, 114, 228),

                                    Color.FromArgb(63, 98, 212),
                                    Color.FromArgb(166, 196, 255),
                                    Color.FromArgb(28, 63, 186)
                                },

            /* MSX */           new Color[]
                                {
                                    Color.FromArgb(55, 55, 255),
                                    Color.FromArgb(3, 0, 120),
                                    Color.FromArgb(0, 0, 120),

                                    Color.FromArgb(255, 255, 255),

                                    Color.FromArgb(60, 60, 60),
                                    Color.FromArgb(255, 255, 255),
                                    Color.FromArgb(90, 90, 90)
                                },

            /* MSX MOD */       new Color[]
                                {
                                    Color.FromArgb(55, 55, 255),
                                    Color.FromArgb(0, 0, 247),
                                    Color.FromArgb(0, 0, 120),

                                    Color.FromArgb(255, 255, 255),

                                    Color.FromArgb(60, 60, 60),
                                    Color.FromArgb(255, 255, 255),
                                    Color.FromArgb(90, 90, 90)
                                },

            /* Flash */         new Color[]
                                {
                                    Color.FromArgb(40, 40, 40),
                                    Color.FromArgb(47, 47, 47),
                                    Color.FromArgb(180, 180, 180),

                                    Color.FromArgb(94, 49, 52),

                                    Color.FromArgb(0, 0, 0),
                                    Color.FromArgb(174, 30, 37),
                                    Color.FromArgb(255, 255, 255)
                                },
        };

        public static Bitmap Generate(Console console, string text, int year, int players, Bitmap img, int lang)
        {
            Bitmap bmp = new Bitmap(660, 260);
            if (img == null)
            {
                img = new Bitmap(256, 192);
                using (Graphics g = Graphics.FromImage(img))
                    g.Clear(Color.Transparent);
            }

            int target = 0;
            switch (console)
            {
                case Console.NES:
                    target = lang > 0 ? 1 : 0;
                    break;

                case Console.SNES:
                    target = lang > 0 ? 3 : 2;
                    break;

                case Console.N64:
                    target = 4;
                    break;

                case Console.SMS:
                    target = 5;
                    break;

                case Console.SMDGEN:
                    target = 6;
                    break;

                case Console.PCE:
                    target = lang > 0 ? 8 : 7;
                    break;

                case Console.NeoGeo:
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
            }

            var textColor = ColorSchemes[target][2].GetBrightness() < 0.8 ? Color.White : Color.Black;
            var leftTextColor = target == 2 ? Color.Black : target == 8 ? Color.FromArgb(90, 90, 90) : ColorSchemes[target][0].GetBrightness() < 0.8 ? Color.White : Color.Black;
            if (target == 4 || target == 2) textColor = Color.White;


            string released  = Language.Current.TwoLetterISOLanguageName == "ja" || lang == 1 ? "{0}年発売"
                                      : Language.Current.TwoLetterISOLanguageName == "ko" || lang == 2 ? "일본판 발매년도\r\n{0}년"
                                      : Language.Current.TwoLetterISOLanguageName == "nl" ? "Release: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "es" ? "Año: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "it" ? "Pubblicato: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "fr" ? "Publié en {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "de" ? "Erschienen: {0}"
                                      : "Released: {0}";

            string numPlayers = Language.Current.TwoLetterISOLanguageName == "ja" || lang == 1 ? "プレイ人数\r\n{0}人"
                                      : Language.Current.TwoLetterISOLanguageName == "ko" || lang == 2 ? "플레이 인원수\r\n{0}명"
                                      : Language.Current.TwoLetterISOLanguageName == "nl" ? "{0} speler(s)"
                                      : Language.Current.TwoLetterISOLanguageName == "es" ? "Jugadores: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "it" ? "Giocatori: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "fr" ? "Joueurs: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "de" ? "{0} Spieler"
                                      : "Players: {0}";

            using (Graphics g = Graphics.FromImage(bmp))
            using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, 112), new Point(0, (int)Math.Round(bmp.Height * 1.25)), ColorSchemes[target][0], ColorSchemes[target][2]))
            {
                g.Clear(ColorSchemes[target][0]);
                g.FillRectangle(b, 0, (bmp.Height / 2) + 10, bmp.Width, bmp.Height);

                g.DrawString(
                    text,
                    new Font(FontFamily.GenericSansSerif, 15, FontStyle.Regular),
                    new SolidBrush(textColor),
                    bmp.Width / 2,
                    215,
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                g.DrawString(
                    string.Format(released, year),
                    new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular),
                    new SolidBrush(leftTextColor),
                    15,
                    45,
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far });

                g.DrawString(
                    string.Format(numPlayers, players),
                    new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular),
                    new SolidBrush(leftTextColor),
                    15,
                    90,
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far });

                double[] point = new double[] { (bmp.Width / 2) - Math.Round((img.Width * 0.72) / 2), 40 };
                double[] size = new double[] { Math.Round(img.Width * 0.72), Math.Round(img.Height * 0.72) };

                using (Bitmap black = new Bitmap(256, 192))
                {
                    using (Graphics gBlack = Graphics.FromImage(black))
                        gBlack.Clear(Color.Gray);
                    g.DrawImage(RoundCorners(black, 7), (int)point[0] - 1, (int)point[1] - 1, (int)size[0] + 2, (int)size[1] + 2);
                }
                g.DrawImage(RoundCorners(img, 7), (int)point[0], (int)point[1], (int)size[0], (int)size[1]);
            }

            return bmp;
        }

        private static Bitmap RoundCorners(Image StartImage, int CornerRadius)
        {
            CornerRadius *= 2;
            Bitmap x = new Bitmap(StartImage.Width, StartImage.Height);
            using (Graphics g = Graphics.FromImage(x))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Brush brush = new TextureBrush(StartImage);
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
                gp.AddArc(0 + x.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
                gp.AddArc(0 + x.Width - CornerRadius, 0 + x.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gp.AddArc(0, 0 + x.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                g.FillPath(brush, gp);
                return x;
            }
        }
    }
}
