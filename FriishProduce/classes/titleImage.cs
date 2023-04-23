using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using libWiiSharp;

namespace FriishProduce
{
    public class TitleImage
    {
        public string path { get; set; }

        public Bitmap VCPic { get; set; }
        public Bitmap IconVCPic { get; set; }
        public bool shrinkToFit { get; set; }
        public Bitmap SaveBanner { get; set; }
        public int[] SaveBannerL_xywh { get; set; }
        public int[] SaveBannerS_xywh { get; set; }

        public enum Resize
        { 
            Stretch,
            Fit
        }


        public TitleImage()
        {
            path = null;
            VCPic = null;
            IconVCPic = null;
            SaveBanner = null;
            SaveBannerL_xywh = null;
            SaveBannerS_xywh = null;
            shrinkToFit = false;
        }

        public void Generate(InterpolationMode modeI, Resize modeR)
        {
            if (path == null) return;

            VCPic = new Bitmap(256, 192);
            IconVCPic = new Bitmap(128, 96);
            Bitmap tempBmp = new Bitmap(256, 192);

            using (Image pathImg = Image.FromFile(path))
            using (Graphics g = Graphics.FromImage(tempBmp))
            {
                g.Clear(Color.Black);
                tempBmp.SetResolution(pathImg.HorizontalResolution, pathImg.VerticalResolution);
                VCPic.SetResolution(tempBmp.HorizontalResolution, tempBmp.VerticalResolution);
                IconVCPic.SetResolution(tempBmp.HorizontalResolution, tempBmp.VerticalResolution);

                g.InterpolationMode = modeI;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;

                if (modeR == Resize.Stretch)
                    g.DrawImage(pathImg, 0, 0, 256, 192);
                else if (modeR == Resize.Fit)
                {
                    float ratio = Math.Min(256F / pathImg.Width, 192F / pathImg.Height);
                    int W = Convert.ToInt32(pathImg.Width * ratio);
                    int H = Convert.ToInt32(pathImg.Height * ratio);

                    g.DrawImage(pathImg, (256 - W) / 2, (192 - H) / 2, W, H);
                }

                g.Dispose();
            };

            using (Graphics g = Graphics.FromImage(VCPic))
            {
                g.DrawImage(tempBmp, 0, 0, 256, 192);
                g.Dispose();
            }

            using (Graphics g = Graphics.FromImage(IconVCPic))
            {
                g.InterpolationMode = modeI;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.Clear(Color.White);
                g.DrawImage(tempBmp, 0, 0, 128, 96);

                if (shrinkToFit)
                {
                    for (int a = 0; a <= 4; a++)
                        for (int b = 0; b <= 4; b++)
                            g.DrawImage(tempBmp, a, b, 128 - (a * 2), 96 - (b * 2));
                    for (int i = 1; i <= 4; i++)
                        g.DrawImage(tempBmp, 4, i, 120, 96 - (i * 2));
                }
                
                g.Dispose();
            }
            
            tempBmp.Dispose();
        }

        public void InsertSaveBanner(Platforms platform)
        {
            return;

            // TO-DO
            string path = Paths.WorkingFolder_Content5 + "save_banner.tpl";
            switch (platform)
            {
                case Platforms.nes:
                case Platforms.snes:
                    break;
                case Platforms.n64:
                    break;
            }
            TPL tpl = TPL.Load(path);
            ;
        }

        public void Dispose()
        {
            path = null;
            VCPic.Dispose();
            IconVCPic.Dispose();
            if (SaveBanner != null) SaveBanner.Dispose();
            SaveBannerL_xywh = null;
            SaveBannerS_xywh = null;
            shrinkToFit = false;
        }
    }
}
