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
    public partial class BannerPreview : Form
    {
        public BannerPreview(string title, int year, int players, Image img, int altRegion = 0)
        {
            InitializeComponent();

            BannerPreview_Panel.BackColor = BannerPreview_BG.BackColor;
            BannerPreview_Buffer.BackColor = BannerPreview_Label.BackColor;
            BannerPreview_Line1.Refresh();
            BannerPreview_Line2.Refresh();
            BannerPreview_BG.Refresh();

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
            BannerPreview_Label.Text = title;
            BannerPreview_Year.Text = string.Format(BannerPreview_Year.Tag.ToString(), year);
            BannerPreview_Players.Text = string.Format(BannerPreview_Players.Tag.ToString(), $"{1}{(players <= 1 ? null : "-" + players)}");
            if (Language.Current.TwoLetterISOLanguageName == "ja" || altRegion == 1) BannerPreview_Players.Text = BannerPreview_Players.Text.Replace("-", "～");

            var dif = 50;
            Image.Location = new Point(Image.Location.X, Image.Location.Y - dif);
            BannerPreview_Label.Location = new Point(BannerPreview_Label.Location.X, BannerPreview_Label.Location.Y + dif);
            BannerPreview_Year.Location = new Point(BannerPreview_Year.Location.X - (dif * 6), BannerPreview_Year.Location.Y);
            BannerPreview_Players.Location = new Point(BannerPreview_Year.Location.X, BannerPreview_Players.Location.Y);
            BannerPreview_Line1.Location = new Point(BannerPreview_Line1.Location.X - (dif * 3), BannerPreview_Line1.Location.Y);
            BannerPreview_Line2.Location = new Point(BannerPreview_Line1.Location.X, BannerPreview_Line2.Location.Y);
            Animation1.Enabled = true;
            Animation1.Start();
        }

        private void BannerPreview_Paint(object sender, PaintEventArgs e)
        {
            if (BannerPreview_Panel.Enabled)
            {
                if (sender == BannerPreview_BG)
                    using (LinearGradientBrush b = new LinearGradientBrush(new PointF(0, 0), new PointF(0, (sender as Control).Height),
                        BannerPreview_Panel.BackColor,
                        BannerPreview_Label.BackColor))
                    {
                        e.Graphics.FillRectangle(b, (sender as Control).ClientRectangle);
                    }

                else
                    using (LinearGradientBrush b = new LinearGradientBrush(new PointF((sender as Control).Width, 0), new PointF(0, 0),
                        BannerPreview_Panel.BackColor,
                        (sender as Control).BackColor))
                    {
                        e.Graphics.FillRectangle(b, (sender as Control).ClientRectangle);
                    }
            }
        }

        private void ExportBanner_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();

            var WADs = new Dictionary<string, Console>()
            {
                /* { "FCWP", Console.NES }, // SMB3
                { "FCWJ", Console.NES },
                { "FCWQ", Console.NES },
                { "JBDP", Console.SNES }, // DKC2
                { "JBDJ", Console.SNES },
                { "JBDT", Console.SNES },
                { "NAAP", Console.N64 }, // SM64
                { "NAAJ", Console.N64 },
                { "NABT", Console.N64 }, // MK64 */
            };

            foreach (var item in WADs)
                Banner.ExportBanner(item.Key, item.Value);

            System.Media.SystemSounds.Beep.Play();
        }

        private void Animation1_Tick(object sender, EventArgs e)
        {
            if (BannerPreview_Label.Location.Y > 260)
                BannerPreview_Label.Location = new Point(BannerPreview_Label.Location.X, BannerPreview_Label.Location.Y - 1);

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
