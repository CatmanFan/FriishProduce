using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using libWiiSharp;
using static FriishProduce.Properties.Resources;

namespace FriishProduce
{
    public class TitleImage
    {
        public string path { get; set; }
        internal InterpolationMode InterpolationMode { get; set; }
        internal Resize ResizeMode { get; set; }
        public Bitmap VCPic { get; set; }
        public Bitmap IconVCPic { get; set; }
        public bool shrinkToFit { get; set; }
        public Bitmap SaveIcon { get; set; }
        public int[] SaveIconL_xywh { get; set; }
        public int[] SaveIconS_xywh { get; set; }

        public enum Resize
        { 
            Stretch,
            Fit
        }

        public TitleImage(Platforms platform = Platforms.NES)
        {
            path = null;
            VCPic = null;
            IconVCPic = null;
            SaveIcon = null;
            Generate(platform);
        }

        public Image Generate(Platforms platform)
        {
            // --------------------------------------------------
            // Console defined options
            // --------------------------------------------------
            shrinkToFit = (int)platform <= 2 || platform == Platforms.Flash;
            SaveIconL_xywh = new int[] { 10, 10, 58, 44 };
            SaveIconS_xywh = new int[] { 4, 9, 40, 30 };

            if (path == null) return null;

            if (ResizeMode == Resize.Fit)
            {
                Image pathImg = Image.FromFile(path);

                float maxWidth = SaveIconS_xywh[2];
                float ratio = Math.Min(maxWidth / pathImg.Width, maxWidth / pathImg.Height);

                SaveIconS_xywh[2] = Convert.ToInt32(pathImg.Width * ratio);
                SaveIconS_xywh[3] = Convert.ToInt32(pathImg.Height * ratio);
                SaveIconS_xywh[0] = Convert.ToInt32((maxWidth - SaveIconS_xywh[2]) / 2) + Convert.ToInt32((48 - maxWidth) / 2);
                SaveIconS_xywh[1] = Convert.ToInt32((maxWidth - SaveIconS_xywh[3]) / 2) + Convert.ToInt32((48 - maxWidth) / 2);

                pathImg.Dispose();
            }

            // --------------------------------------------------
            // Actual image generation
            // --------------------------------------------------
            VCPic = new Bitmap(256, 192);
            IconVCPic = new Bitmap(128, 96);
            SaveIcon = new Bitmap(SaveIconL_xywh[2], SaveIconL_xywh[3]);
            Bitmap tempBmp = new Bitmap(256, 192);

            var PixelOffset = PixelOffsetMode.HighQuality;
            var Smoothing = SmoothingMode.HighQuality;
            var CompositingQ = CompositingQuality.HighQuality;

            using (Image pathImg = Image.FromFile(path))
            using (Graphics g = Graphics.FromImage(tempBmp))
            {
                g.Clear(Color.Black);
                tempBmp.SetResolution(pathImg.HorizontalResolution, pathImg.VerticalResolution);
                VCPic.SetResolution(tempBmp.HorizontalResolution, tempBmp.VerticalResolution);
                IconVCPic.SetResolution(tempBmp.HorizontalResolution, tempBmp.VerticalResolution);

                g.InterpolationMode = InterpolationMode;
                g.PixelOffsetMode = PixelOffset;
                g.SmoothingMode = Smoothing;
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQ;

                if (ResizeMode == Resize.Stretch)
                    g.DrawImage(pathImg, 0, 0, 256, 192);
                else if (ResizeMode == Resize.Fit)
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
                g.InterpolationMode = InterpolationMode;
                g.PixelOffsetMode = PixelOffset;
                g.SmoothingMode = Smoothing;
                g.CompositingQuality = CompositingQ;

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

            using (Graphics g = Graphics.FromImage(SaveIcon))
            {
                g.InterpolationMode = InterpolationMode;
                g.PixelOffsetMode = PixelOffset;
                g.SmoothingMode = Smoothing;
                g.CompositingQuality = CompositingQ;

                g.DrawImage(Image.FromFile(path), 0, 0, SaveIcon.Width, SaveIcon.Height);

                g.Dispose();
            }

            tempBmp.Dispose();
            return IconVCPic;
        }

        private float[] opacity4 = { 0F, 0.32F, 0.64F, 1F };

        public void CreateSave(Platforms platform)
        {
            Directory.CreateDirectory(Paths.Images);

            if (platform != Platforms.Flash)
            {
                bool embedded = false;
                string tplPath = null;
                bool usesWte = false;

                switch (platform)
                {
                    case Platforms.NES:
                        tplPath = Paths.WorkingFolder + "out.tpl";
                        embedded = true;
                        break;
                    case Platforms.SNES:
                        tplPath = Paths.WorkingFolder_Content5 + "banner.tpl";
                        break;
                    case Platforms.N64:
                        tplPath = Paths.WorkingFolder_Content5 + "save_banner.tpl";
                        break;
                    case Platforms.SMS:
                    case Platforms.SMD:
                        usesWte = true;
                        break;
                }
                if (embedded && !File.Exists(tplPath)) return;
                else if (tplPath == null && !usesWte) return;

                // -----------------------------------------------------------------------------------------
                // Save image generation for non-Flash TPLs (uses the source TPL embedded within the WAD)
                // -----------------------------------------------------------------------------------------
                if (!usesWte)
                {
                    TPL tpl = TPL.Load(tplPath);
                    int numTextures = tpl.NumOfTextures;
                    TPL_TextureFormat[] formatsT = new TPL_TextureFormat[numTextures];
                    TPL_PaletteFormat[] formatsP = new TPL_PaletteFormat[numTextures];
                    for (int i = 0; i < numTextures; i++)
                    {
                        formatsT[i] = tpl.GetTextureFormat(i);
                        formatsP[i] = tpl.GetPaletteFormat(i);
                    }
                    tpl.ExtractAllTextures(Paths.Images);

                    // Declaration of Graphics/Imaging variables
                    var sFiles = new System.Collections.Generic.List<string>();
                    for (int i = 0; i < numTextures; i++)
                        sFiles.Add(Paths.Images + $"Texture_0{i}.png");

                    Bitmap sBanner = new Bitmap(192, 64);
                    Bitmap sIcon1 = new Bitmap(48, 48);
                    Image sIcon2 = Image.FromFile(sFiles[numTextures - 1]);

                    using (Image img = (Image)sBanner.Clone())
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(Image.FromFile(sFiles[0]), new Point(0, 0));
                        g.DrawImage(SaveIcon, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);
                        sFiles[0] = Paths.Images + Path.GetFileNameWithoutExtension(sFiles[0]) + "_new.png";
                        img.Save(sFiles[0]);

                        img.Dispose();
                        g.Dispose();
                    }

                    using (Image img = (Image)sIcon1.Clone())
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(SaveIconPlaceholder, new Point(0, 0));
                        g.InterpolationMode = InterpolationMode;
                        g.DrawImage(SaveIcon, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                        sFiles[1] = Paths.Images + Path.GetFileNameWithoutExtension(sFiles[1]) + "_new.png";
                        img.Save(sFiles[1]);

                        img.Dispose();
                        g.Dispose();
                    }

                    // Update sIcon1 to modified version
                    sIcon1.Dispose();
                    sIcon1 = (Bitmap)Image.FromFile(sFiles[1]);

                    using (Image img1 = (Image)sIcon1.Clone())
                    using (Image img2 = (Image)sIcon2.Clone())
                    using (Graphics g = Graphics.FromImage(img1))
                    using (var a = new ImageAttributes())
                    {
                        var w = img1.Width; var h = img1.Height;

                        if (numTextures == 5)
                        {
                            a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                            g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                            sFiles[2] = Paths.Images + Path.GetFileNameWithoutExtension(sFiles[2]) + "_new.png";
                            img1.Save(sFiles[2]);

                            g.DrawImage(img1, 0, 0);
                            a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                            g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                            sFiles[3] = Paths.Images + Path.GetFileNameWithoutExtension(sFiles[3]) + "_new.png";
                            img1.Save(sFiles[3]);

                        }

                        g.Dispose();
                        img1.Dispose();
                        img2.Dispose();
                    }

                    // -------------------------------------------------------------- //

                    while (tpl.NumOfTextures > 0)
                        tpl.RemoveTexture(0);

                    for (int i = 0; i < numTextures; i++)
                        tpl.AddTexture(sFiles[i], formatsT[i], formatsP[i]);

                    tpl.Save(Paths.Images + "out.tpl");
                    if (File.Exists(tplPath))
                    {
                        File.Delete(tplPath);
                        File.Move(Paths.Images + "out.tpl", tplPath);
                    }

                    sBanner.Dispose();
                    sIcon1.Dispose();
                    sIcon2.Dispose();
                    tpl.Dispose();
                }

                // -------------------------------------------------------------------------------------
                // Save image generation for WTEs (uses the source WTE files embedded within the WAD)
                // -------------------------------------------------------------------------------------
                else
                {
                }
            }

            // ------------------------------------------------------------------------------------------
            // Save image generation for Flash TPLs (uses Properties.Resources.Save{Icon/Banner}Flash)
            // ------------------------------------------------------------------------------------------
            else
            {
                var bannerPath = Paths.WorkingFolder_Content2 + "banner\\US\\banner.tpl";
                var iconPath = Paths.WorkingFolder_Content2 + "banner\\US\\icons.tpl";

                // Declaration of Graphics/Imaging variables
                var sFiles = new System.Collections.Generic.List<string>();
                for (int i = 0; i < 5; i++)
                    sFiles.Add(Paths.Images + $"Texture_0{i}.png");

                Bitmap sBanner = new Bitmap(SaveBannerFlash.Width, SaveBannerFlash.Height);
                Bitmap sIcon1 = new Bitmap(48, 48);
                Bitmap sIcon2 = new Bitmap(48, 48);

                using (Image img = (Image)sBanner.Clone())
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(SaveBannerFlash, new Point(0,0));
                    g.DrawImage(SaveIcon, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);
                    img.Save(sFiles[0]);

                    img.Dispose();
                    g.Dispose();
                }

                using (Image img = (Image)sIcon1.Clone())
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(SaveIconPlaceholder, new Point(0, 0));
                    g.InterpolationMode = InterpolationMode;
                    g.DrawImage(SaveIcon, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                    img.Save(sFiles[1]);

                    img.Dispose();
                    g.Dispose();
                }

                // Update sIcon1 to modified version
                sIcon1.Dispose();
                sIcon1 = (Bitmap)Image.FromFile(sFiles[1]);

                using (Image img = (Image)sIcon1.Clone())
                using (Graphics g = Graphics.FromImage(img))
                using (var a = new ImageAttributes())
                {
                    var w = img.Width; var h = img.Height;

                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(SaveIconFlash, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    img.Save(sFiles[2]);

                    g.DrawImage(img, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(SaveIconFlash, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    img.Save(sFiles[3]);

                    g.Dispose();
                    img.Dispose();
                }

                SaveIconFlash.Save(sFiles[4]);

                // -------------------------------------------------------------- //

                TPL tpl = TPL.FromImage(sFiles[0], TPL.Load(bannerPath).GetTextureFormat(0), TPL.Load(bannerPath).GetPaletteFormat(0));
                tpl.Save(bannerPath);
                tpl.Save(bannerPath.Replace("banner\\US\\", "banner\\EU\\"));
                tpl.Save(bannerPath.Replace("banner\\US\\", "banner\\JP\\"));

                tpl = TPL.FromImages(new string[8]
                    {
                        sFiles[1],
                        sFiles[1],
                        sFiles[1],
                        sFiles[2],
                        sFiles[3],
                        sFiles[4],
                        sFiles[4],
                        sFiles[4]
                    }, new TPL_TextureFormat[8]
                    {
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0),
                        TPL.Load(iconPath).GetTextureFormat(0)
                    }, new TPL_PaletteFormat[8]
                    {
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0),
                        TPL.Load(iconPath).GetPaletteFormat(0)
                    });
                tpl.Save(iconPath);
                tpl.Save(iconPath.Replace("banner\\US\\", "banner\\EU\\"));
                tpl.Save(iconPath.Replace("banner\\US\\", "banner\\JP\\"));

                sBanner.Dispose();
                sIcon1.Dispose();
                sIcon2.Dispose();
                tpl.Dispose();
            }

            foreach (var file in Directory.GetFiles(Paths.Images)) try { File.Delete(file); } catch { }
            try { Directory.Delete(Paths.Images, true); } catch { }
        }

        public void Dispose()
        {
            path = null;
            VCPic.Dispose();
            IconVCPic.Dispose();
            SaveIcon.Dispose();
        }
    }
}
