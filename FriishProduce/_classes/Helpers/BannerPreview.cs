using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FriishProduce
{
    public class Preview
    {
        public enum Language
        {
            America,
            Japanese,
            Korean,
            Europe,
            Auto
        }

        private Bitmap RoundCorners(Image StartImage, int CornerRadius, bool Smooth = false)
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

        #region Font Functions

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts;

        private FontFamily Font()
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

        /// <summary>
        /// Creates a banner preview bitmap using VCPic.
        /// </summary>
        /// <param name="img">Image to be used</param>
        /// <param name="text">Full title of the game</param>
        /// <param name="year">Year the game was released</param>
        /// <param name="players">No. of players supported</param>
        /// <param name="console">Platform/console</param>
        /// <param name="lang">Banner region/language: Japanese, Korean, Europe or America</param>
        /// <returns></returns>
        public Bitmap Banner(Bitmap img, string text, int year, int players, Console console, Language lang)
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

                case Console.PSX:
                    target = 15;
                    break;

                case Console.RPGM:
                    target = 16;
                    break;
            }

            var leftTextColor = target == 2 ? Color.Black : target == 8 ? Color.FromArgb(90, 90, 90) : BannerSchemes.List[target].bg.GetBrightness() < 0.8 ? Color.White : Color.FromArgb(50, 50, 50);

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
            using (LinearGradientBrush b1 = new LinearGradientBrush(new Point(0, 130), new Point(0, (int)Math.Round(bmp.Height * 0.9)), BannerSchemes.List[target].bg, BannerSchemes.List[target].bgBottom))
            using (LinearGradientBrush b2 = new LinearGradientBrush(new Point(0, 50), new Point(0, (int)Math.Round(bmp.Height * 1.25)), BannerSchemes.List[target].bg, BannerSchemes.List[target].bgBottom))
            using (LinearGradientBrush c = new LinearGradientBrush(new Point(0, 0), new Point(125, 0), BannerSchemes.List[target].lines, BannerSchemes.List[target].bg))
            {
                g.CompositingQuality = CompositingQuality.AssumeLinear;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                g.Clear(BannerSchemes.List[target].bg);
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
                        cName = lang switch { Language.Japanese => "プレイステーション", _ => "PLAYSTATION" };
                        break;

                    case Console.RPGM:
                        cName = lang switch { Language.Japanese => "ＲＰＧツクール", _ => "RPG MAKER" };
                        break;
                }

                var f = new Font(Font(), 9);
                var brush = new SolidBrush(BannerSchemes.List[target].topBG);

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

                g.DrawPath(new Pen(BannerSchemes.List[target].topBorder, 2), p);
                g.FillPath(brush, p);
                g.DrawString(cName, f, new SolidBrush(BannerSchemes.List[target].topText),
                    bmp.Width - 10,
                    24,
                    new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                #endregion
            }

            return bmp;
        }

        /// <summary>
        /// Creates an icon preview bitmap using IconVCPic. If a PictureBox control is specified, this can also be used to create an animation similar to what will appear on the Wii Menu.
        /// </summary>
        /// <param name="img">Image to be used</param>
        /// <param name="console">Platform/console</param>
        /// <param name="lang">Banner region/language: Japanese, Korean, Europe or America</param>
        /// <param name="target">PictureBox control</param>
        /// <returns></returns>
        public Bitmap Icon(Bitmap img, Console console, Language lang, PictureBox target = null)
        {
            // 0s - 5s = Title
            // 5s - 6s = Fadein
            // 6s - 8s = Logo
            // 8s - 9s = Fadeout

            if (iconData.duration.second >= iconData.opacities.Count - 1) iconData.duration = (0, 0);
            bool reset = iconData.type != ((int)console, (int)lang) || iconData.origImg != img;

            // Console/platform logo
            // ****************
            if (reset)
            {
                iconData.type = ((int)console, (int)lang);
                using (var U8 = BannerHelper.BannerApp(console, lang switch { Language.Japanese => libWiiSharp.Region.Japan, Language.Korean => libWiiSharp.Region.Korea, Language.Europe => libWiiSharp.Region.Europe, _ => libWiiSharp.Region.USA }))
                {
                    if (U8 != null)
                    {
                        using (var Icon = libWiiSharp.U8.Load(U8.Data[U8.GetNodeIndex("icon.bin")]))
                        {
                            foreach (var item in Icon.StringTable)
                            {
                                if (item.ToLower().StartsWith("log"))
                                {
                                    var tpl = libWiiSharp.TPL.Load(Icon.Data[Icon.GetNodeIndex(item)]);

                                    iconData.consoleImg = new Bitmap(128, 96);
                                    using (Graphics g = Graphics.FromImage(iconData.consoleImg))
                                    {
                                        g.Clear(Color.White);
                                        Bitmap logo = (Bitmap)tpl.ExtractTexture();

                                        if ((int)tpl.GetTextureFormat(0) <= 1)
                                        {
                                            using (Bitmap transLogo = new Bitmap(logo.Width, logo.Height, PixelFormat.Format32bppArgb))
                                            {
                                                unsafe
                                                {
                                                    BitmapData data = transLogo.LockBits(new Rectangle(Point.Empty, transLogo.Size), ImageLockMode.ReadWrite, transLogo.PixelFormat);

                                                    byte* line = (byte*)data.Scan0;
                                                    for (int y = 0; y < data.Height; y++)
                                                    {
                                                        for (int x = 0; x < data.Width; x++)
                                                        {
                                                            var c = console == Console.NES ? Color.FromArgb(176, 26, 26) : Color.Black;
                                                            *((int*)line + x) = Color.FromArgb(logo.GetPixel(x, y).R, c.R, c.G, c.B).ToArgb();
                                                        }

                                                        line += data.Stride;
                                                    }

                                                    transLogo.UnlockBits(data);
                                                    g.DrawImage(transLogo, (128 / 2) - (logo.Width / 2), (96 / 2) - (logo.Height / 2));
                                                }
                                            }

                                        }
                                        else g.DrawImage(logo, (128 / 2) - (logo.Width / 2), (96 / 2) - (logo.Height / 2));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (iconData.consoleImg == null)
            {
                iconData.consoleImg = new Bitmap(128, 96);
                using (Graphics g = Graphics.FromImage(iconData.consoleImg))
                    g.Clear(Color.White);
            }

            // Title image
            // ****************
            if (img != null && reset)
            {
                iconData.origImg = img;
                iconData.duration = (0, 0);

                try { iconData.durationTimer.Tick -= iconDurationTick; } catch { }
                iconData.durationTimer.Stop();

                // Set icon image
                // ****************
                iconData.iconImg = new Bitmap(128, 96);

                using (Graphics g = Graphics.FromImage(iconData.iconImg))
                {
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.DrawImage(img, -4, -4, iconData.iconImg.Width + 11, iconData.iconImg.Height + 11);
                }

                // Restart timer
                // ****************
                iconData.durationTimer.Tick += iconDurationTick;
                iconData.durationTimer.Start();
            }

            var bmp = new Bitmap(128, 96);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(iconData.consoleImg, 0, 0);
                var opacity1 = iconData.opacities[iconData.duration.second];
                var opacity2 = iconData.opacities[iconData.duration.second + 1];
                var percentage = iconData.duration.frame / (1000F / iconData.durationTimer.Interval);
                float opacityT = (opacity1 * (1F - percentage)) + (opacity2 * percentage);

                if (img != null)
                {
                    if (opacityT < 1 && opacityT > 0)
                    {
                        using (var a = new ImageAttributes())
                        {
                            a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacityT });
                            g.DrawImage(iconData.iconImg, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, iconData.iconImg.Width, iconData.iconImg.Height, GraphicsUnit.Pixel, a);
                        }
                    }
                    else if (opacity1 >= 1) g.DrawImage(iconData.iconImg, 0, 0);
                }
            }

            var bmp2 = new Bitmap(120, 87);
            using (Graphics g = Graphics.FromImage(bmp2))
            {
                using (Bitmap border = new Bitmap(bmp2.Width, bmp2.Height))
                {
                    using (Graphics gBorder = Graphics.FromImage(border))
                        gBorder.Clear(Color.LightSlateGray);
                    g.DrawImage(RoundCorners(border, 10, true), 0, 0, bmp2.Width, bmp2.Height);
                }

                g.DrawImage(RoundCorners(bmp, 8, true), 1, 1, bmp2.Width - 2, bmp2.Height - 2);
            }

            iconData.duration.frame++;
            if (img != null && iconData.duration.frame >= 1000 / iconData.durationTimer.Interval) { iconData.duration.frame = 0; iconData.duration.second++; }

            if (target != null) iconData.target = target;
            if (iconData.target != null) iconData.target.Image = bmp2;
            return bmp2;
        }

        #region Icon (private functions)
        private struct icon
        {
            public Bitmap origImg;
            public Bitmap iconImg;
            public Bitmap consoleImg;
            public (int, int) type;
            public (int second, float frame) duration;
            public List<float> opacities;
            public Timer durationTimer;
            public PictureBox target;
        }

        private icon iconData = new icon() { type = (-1, -1), opacities = new List<float>() { 1, 1, 1, 1, 1, 1, 0, 0, 0, 1 }, durationTimer = new Timer() { Interval = 25 } };

        private void iconDurationTick(object sender, EventArgs e) => Icon(iconData.origImg, (Console)Math.Max(iconData.type.Item1, 0), (Language)Math.Max(iconData.type.Item2, 0), iconData.target);
        #endregion
    }
}
