using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class BannerPreview : UserControl
    {
        private Color[][] ColorSchemes = new Color[][]
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

        Bitmap bmp = new Bitmap(1, 1);

        public BannerPreview() => InitializeComponent();

        public BannerPreview(Console console, string title, int year, int players, Image img, int altRegion = 0, bool animation = false)
        {
            InitializeComponent();

            BannerPreview_Text.Parent = BannerPreview_Gradient;
            BannerPreview_Text.BackColor = Color.Transparent;

            Update(console, title, year, players, img, altRegion);

            if (animation)
            {
                var dif = 50;
                Image.Location = new Point(Image.Location.X, Image.Location.Y - dif);
                BannerPreview_Text.Location = new Point(BannerPreview_Text.Location.X, BannerPreview_Text.Location.Y + dif);
                BannerPreview_Year.Location = new Point(BannerPreview_Year.Location.X - (dif * 6), BannerPreview_Year.Location.Y);
                BannerPreview_Players.Location = new Point(BannerPreview_Year.Location.X, BannerPreview_Players.Location.Y);
                BannerPreview_Line1.Location = new Point(BannerPreview_Line1.Location.X - (dif * 3), BannerPreview_Line1.Location.Y);
                BannerPreview_Line2.Location = new Point(BannerPreview_Line1.Location.X, BannerPreview_Line2.Location.Y);
                Animation1.Enabled = true;
                Animation1.Start();
            }
        }

        public void Update(Console console, string title, int year, int players, Image img, int altRegion = 0)
        {
            int target = 0;
            switch (console)
            {
                case Console.NES:
                    target = altRegion > 0 ? 1 : 0;
                    break;

                case Console.SNES:
                    target = altRegion > 0 ? 3 : 2;
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
                    target = altRegion > 0 ? 8 : 7;
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

            BannerPreview_Line1.BackColor = ColorSchemes[target][3];
            BannerPreview_Line2.BackColor = ColorSchemes[target][3];
            BannerPreview_Main.BackColor = ColorSchemes[target][0];

            BannerPreview_Text.ForeColor = BannerPreview_Main.BackColor.GetBrightness() < 0.8 ? Color.White : Color.Black;

            if (target == 2) BannerPreview_Year.ForeColor = BannerPreview_Players.ForeColor = Color.Black;
            if (target == 8) BannerPreview_Year.ForeColor = BannerPreview_Players.ForeColor = Color.FromArgb(90, 90, 90);
            else BannerPreview_Year.ForeColor = BannerPreview_Players.ForeColor = BannerPreview_Text.ForeColor;

            bmp = new Bitmap(BannerPreview_Gradient.Width, BannerPreview_Gradient.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            using (LinearGradientBrush b = new LinearGradientBrush(new Point(0, -10), new Point(0, (int)Math.Round(bmp.Height * 1.5)), ColorSchemes[target][0], ColorSchemes[target][2]))
            {
                g.FillRectangle(b, 0, 0, bmp.Width, bmp.Height * 2);
                BannerPreview_Gradient.Image = bmp;
                g.DrawString(title, BannerPreview_Text.Font, new SolidBrush(BannerPreview_Text.ForeColor), bmp.Width / 2, 73, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }

            BannerPreview_Line1.Refresh();
            BannerPreview_Line2.Refresh();

            BannerPreview_Year.Tag = Language.Current.TwoLetterISOLanguageName == "ja" || altRegion == 1 ? "{0}年発売"
                                      : Language.Current.TwoLetterISOLanguageName == "ko" || altRegion == 2 ? "일본판 발매년도\r\n{0}년"
                                      : Language.Current.TwoLetterISOLanguageName == "nl" ? "Release: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "es" ? "Año: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "it" ? "Pubblicato: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "fr" ? "Publié en {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "de" ? "Erschienen: {0}"
                                      : "Released: {0}";

            BannerPreview_Players.Tag = Language.Current.TwoLetterISOLanguageName == "ja" || altRegion == 1 ? "プレイ人数\r\n{0}人"
                                      : Language.Current.TwoLetterISOLanguageName == "ko" || altRegion == 2 ? "플레이 인원수\r\n{0}명"
                                      : Language.Current.TwoLetterISOLanguageName == "nl" ? "{0} speler(s)"
                                      : Language.Current.TwoLetterISOLanguageName == "es" ? "Jugadores: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "it" ? "Giocatori: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "fr" ? "Joueurs: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "de" ? "{0} Spieler"
                                      : "Players: {0}";

            Image.Image = img;
            BannerPreview_Text.Text = title;
            BannerPreview_Year.Text = string.Format(BannerPreview_Year.Tag.ToString(), year);
            BannerPreview_Players.Text = string.Format(BannerPreview_Players.Tag.ToString(), $"{1}{(players <= 1 ? null : "-" + players)}");
            if (Language.Current.TwoLetterISOLanguageName == "ja" || altRegion == 1) BannerPreview_Players.Text = BannerPreview_Players.Text.Replace("-", "～");
        }

        private void BannerPreview_Paint(object sender, PaintEventArgs e)
        {
            if (sender == BannerPreview_Line1 || sender == BannerPreview_Line2)
                using (LinearGradientBrush b = new LinearGradientBrush(new PointF((sender as Control).Width, 0), new PointF(0, 0),
                    BannerPreview_Main.BackColor,
                    (sender as Control).BackColor))
                {
                    e.Graphics.FillRectangle(b, (sender as Control).ClientRectangle);
                }
        }

        private void Animation1_Tick(object sender, EventArgs e)
        {
            if (BannerPreview_Text.Location.Y > 260)
                BannerPreview_Text.Location = new Point(BannerPreview_Text.Location.X, BannerPreview_Text.Location.Y - 1);

            if (BannerPreview_Year.Location.X < 20)
            {
                BannerPreview_Year.Location = new Point(BannerPreview_Year.Location.X + 4, BannerPreview_Year.Location.Y);
                BannerPreview_Players.Location = new Point(BannerPreview_Year.Location.X, BannerPreview_Players.Location.Y);

                BannerPreview_Line1.Location = new Point(BannerPreview_Line1.Location.X + 2, BannerPreview_Line1.Location.Y);
                BannerPreview_Line2.Location = new Point(BannerPreview_Line1.Location.X, BannerPreview_Line2.Location.Y);
            }
            else { Animation1.Stop(); Animation1.Enabled = false; }

            if (Image.Location.Y < 65)
                Image.Location = new Point(Image.Location.X, Image.Location.Y + 1);
        }
    }
}
