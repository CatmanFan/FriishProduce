using libWiiSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using static FriishProduce.Properties.Resources;

namespace FriishProduce
{
    public class TitleImage
    {
        private Console platform { get; set; }

        private Bitmap Source { get; set; }
        public string SourcePath { get; set; }
        public Bitmap VCPic { get; set; }
        public Bitmap IconVCPic { get; set; }
        public Bitmap SaveIcon { get; set; }
        
        internal InterpolationMode Interpolation { get; set; }
        private int[] SaveIconL_xywh { get; set; }
        private int[] SaveIconS_xywh { get; set; }

        public TitleImage(Console console, string path)
        {
            platform = console;
            Source = null;
            SourcePath = path;

            VCPic = null;
            IconVCPic = null;
            SaveIcon = null;
        }

        private PixelOffsetMode PixelOffset;
        private SmoothingMode Smoothing;
        private CompositingQuality CompositingQ;

        /// <summary>
        /// Gets source based on path given to URL or disk file
        /// </summary>
        /// <returns>Image file if successful, blank if otherwise</returns>
        public Bitmap GetSource()
        {
            try
            {
                if (SourcePath.ToLower().StartsWith("http"))
                {
                    // Prevent certificate error (LibRetro thumbnails website certificate is currently expired and will throw an exception by default)
                    ServicePointManager.ServerCertificateValidationCallback = delegate
                        (object sender,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    { return true; };

                    using (WebClient c = new WebClient())
                    using (Stream s = c.OpenRead(SourcePath))
                    {
                        return new Bitmap(s);
                    }
                }
                else
                {
                    return (Bitmap)Image.FromFile(SourcePath);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, Language.Get("Error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return null;
            }
        }

        /// <summary>
        /// Generates VCPic, IconVCPic and saveicon bitmaps for use in injection.
        /// </summary>
        public void Generate(Bitmap src)
        {
            if (src == null) return;

            bool ShrinkToFit  = platform == Console.NES || platform == Console.SNES || platform == Console.N64 || platform == Console.Flash;
            bool DoNotStretch = platform == Console.Flash;

            // --------------------------------------------------
            // SAVEICON : DEFINE POSITION AND SIZE VARIABLES
            // --------------------------------------------------
            SaveIconL_xywh = new int[] { 10, 10, 58, 44 };
            SaveIconS_xywh = new int[] { 4, 9, 40, 30 };
            if (platform == Console.SMS || platform == Console.SMDGEN)
            {
                SaveIconL_xywh = new int[] { 8, 8, 69, 48 };
                SaveIconS_xywh = new int[] { 2, new Random().Next(8, 9), 44, 31 };
            }
            if (platform == Console.PCE)
            {
                SaveIconS_xywh = new int[] { 6, 9, 36, 30 };
            }
            // --------------------------------------------------

            PixelOffset = PixelOffsetMode.HighQuality;
            Smoothing = SmoothingMode.HighQuality;
            CompositingQ = CompositingQuality.Default;

            // --------------------------------------------------
            // SAVEICON : Fit by width/height variables
            // --------------------------------------------------

            if (DoNotStretch)
            {
                float maxWidth = SaveIconS_xywh[2];
                float ratio = Math.Min(maxWidth / src.Width, maxWidth / src.Height);

                SaveIconS_xywh[2] = Convert.ToInt32(src.Width * ratio);
                SaveIconS_xywh[3] = Convert.ToInt32(src.Height * ratio);
                SaveIconS_xywh[0] = Convert.ToInt32((maxWidth - SaveIconS_xywh[2]) / 2) + Convert.ToInt32((48 - maxWidth) / 2);
                SaveIconS_xywh[1] = Convert.ToInt32((maxWidth - SaveIconS_xywh[3]) / 2) + Convert.ToInt32((48 - maxWidth) / 2);
            }

            // --------------------------------------------------
            // Actual image generation
            // --------------------------------------------------
            VCPic = new Bitmap(256, 192);
            IconVCPic = new Bitmap(128, 96);
            SaveIcon = new Bitmap(SaveIconL_xywh[2], SaveIconL_xywh[3]);

            using (Bitmap Working = new Bitmap(256, 192, PixelFormat.Format32bppRgb))
            {
                using (Graphics g = Graphics.FromImage(Working))
                {
                    g.Clear(Color.Black);
                    Working.SetResolution(src.HorizontalResolution, src.VerticalResolution);
                    VCPic.SetResolution(Working.HorizontalResolution, src.VerticalResolution);
                    IconVCPic.SetResolution(Working.HorizontalResolution, src.VerticalResolution);

                    g.InterpolationMode = Interpolation;
                    g.PixelOffsetMode = PixelOffset;
                    g.SmoothingMode = Smoothing;
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.CompositingQuality = CompositingQ;

                    if (DoNotStretch)
                    {
                        // --------------------------------------------------
                        // Fit by width/height
                        // --------------------------------------------------
                        float ratio = Math.Min(256F / src.Width, 192F / src.Height);
                        int W = Convert.ToInt32(src.Width * ratio);
                        int H = Convert.ToInt32(src.Height * ratio);

                        g.DrawImage(src, (256 - W) / 2, (192 - H) / 2, W, H);
                    }
                    else
                    {
                        // --------------------------------------------------
                        // Stretch to fill
                        // --------------------------------------------------
                        g.DrawImage(src, 0, 0, 256, 192);
                    }

                };

                using (Graphics g = Graphics.FromImage(VCPic))
                {
                    g.DrawImage(Working, 0, 0, 256, 192);
                    g.Dispose();
                }

                using (Graphics g = Graphics.FromImage(IconVCPic))
                {
                    g.InterpolationMode = Interpolation;
                    g.PixelOffsetMode = PixelOffset;
                    g.SmoothingMode = Smoothing;
                    g.CompositingQuality = CompositingQ;

                    g.Clear(Color.White);
                    g.DrawImage(Working, 0, 0, 128, 96);

                    if (ShrinkToFit)
                    {
                        for (int a = 0; a <= 4; a++)
                            for (int b = 0; b <= 4; b++)
                                g.DrawImage(Working, a, b, 128 - (a * 2), 96 - (b * 2));
                        for (int i = 1; i <= 4; i++)
                            g.DrawImage(Working, 4, i, 120, 96 - (i * 2));
                    }

                    g.Dispose();
                }

                using (Graphics g = Graphics.FromImage(SaveIcon))
                {
                    g.InterpolationMode = Interpolation;
                    g.PixelOffsetMode = PixelOffset;
                    g.SmoothingMode = Smoothing;
                    g.CompositingQuality = CompositingQ;

                    g.DrawImage(src, 0, 0, SaveIcon.Width, SaveIcon.Height);

                    g.Dispose();
                }

                Working.Dispose();
            }
        }
        public void Generate() => Generate(Source);

        private readonly float[] opacity4 = { 0F, 0.32F, 0.64F, 1F };
        private readonly float[] opacity6 = { 0F, 0.20F, 0.40F, 0.60F, 0.80F, 1F };
        private readonly float[] opacity8 = { 0F, 0F, 0F, 0.32F, 0.64F, 1F, 1F, 1F };

        public void ReplaceTPL(TPL t, Bitmap bmp)
        {
            var tplTF = t.GetTextureFormat(0);
            var tplPF = t.GetPaletteFormat(0);
            t.RemoveTexture(0);
            t.AddTexture(bmp, tplTF, tplPF);
        }

        public void ReplaceBanner(WAD w)
        {
            if (!w.HasBanner) return;

            U8[] BannerSet = Banner.GetBanner(w);
            
            // VCPic.tpl
            TPL tpl = TPL.Load(BannerSet[0].Data[BannerSet[0].GetNodeIndex("VCPic.tpl")]);
            ReplaceTPL(tpl, VCPic);
            BannerSet[0].ReplaceFile(BannerSet[0].GetNodeIndex("VCPic.tpl"), tpl.ToByteArray());

            // IconVCPic.tpl
            tpl = TPL.Load(BannerSet[1].Data[BannerSet[1].GetNodeIndex("IconVCPic.tpl")]);
            ReplaceTPL(tpl, IconVCPic);
            BannerSet[1].ReplaceFile(BannerSet[1].GetNodeIndex("IconVCPic.tpl"), tpl.ToByteArray());
            tpl.Dispose();

            // Replace banner.bin
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), BannerSet[0].ToByteArray());
            BannerSet[0].Dispose();

            // Replace icon.bin
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("icon.bin"), BannerSet[1].ToByteArray());
            BannerSet[1].Dispose();
        }

        /// <summary>
        /// Saveicon TPL generator for NES/SNES/N64/PCE/NeoGeo/MSX
        /// </summary>
        /// <param name="platform">Target console</param>
        /// <param name="tplArray">The byte array which contains the TPL data (i.e. can be a file read in bytes)</param>
        /// <returns>Modified TPL</returns>
        public TPL CreateSaveTPL(Console platform, byte[] tplBytes)
        {
            /* DIRECTORY PATHS FOR TPLs:
             ******************************
               NES:     embedded in 01.app
               SNES:    05.app/banner.tpl
               N64:     05.app/save_banner.tpl
               PCE:     05.app/savedata.tpl
               NeoGeo:  embedded in 01.app
               MSX:     embedded in 01.app
            */

            TPL tpl = TPL.Load(tplBytes);
            int numTextures = tpl.NumOfTextures;
            TPL_TextureFormat[] formatsT = new TPL_TextureFormat[numTextures];
            TPL_PaletteFormat[] formatsP = new TPL_PaletteFormat[numTextures];
            for (int i = 0; i < numTextures; i++)
            {
                formatsT[i] = tpl.GetTextureFormat(i);
                formatsP[i] = tpl.GetPaletteFormat(i);
            }

            Image sBanner = tpl.ExtractTexture(0);
            Image sIcon1 = tpl.ExtractTexture(1);
            Image sIconEnd = tpl.ExtractTexture(numTextures - 1);

            // Clean TPL textures
            while (tpl.NumOfTextures > 0) tpl.RemoveTexture(0);

            // -------------------------
            // Savedata banner
            // -------------------------
            using (Graphics g = Graphics.FromImage(sBanner))
            {
                g.DrawImage(SaveIcon, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);

                tpl.AddTexture(sBanner, formatsT[0], formatsP[0]);

                g.Dispose();
                sBanner.Dispose();
            }

            // -------------------------
            // Savedata icon
            // -------------------------
            // 1ST FRAME
            using (Graphics g = Graphics.FromImage(sIcon1))
            {
                g.DrawImage(SaveIconPlaceholder, new Point(0, 0));
                g.InterpolationMode = Interpolation;
                g.PixelOffsetMode = PixelOffset;
                g.SmoothingMode = Smoothing;
                g.CompositingQuality = CompositingQ;
                g.DrawImage(SaveIcon, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);

                tpl.AddTexture(sIcon1, formatsT[1], formatsP[1]);

                g.Dispose();
            }


            // ANIMATION AND END FRAMES
            using (Image sIcon2 = (Image)sIcon1.Clone())
            using (Graphics g = Graphics.FromImage(sIcon2))
            using (var a = new ImageAttributes())
            {
                var w = sIcon2.Width; var h = sIcon2.Height;

                if (numTextures == 5)
                {
                    // 2ND FRAME
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[2], formatsP[2]);

                    // 3RD FRAME
                    g.DrawImage(sIcon1, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[3], formatsP[3]);
                }

                else if (platform == Console.NeoGeo || platform == Console.MSX)
                {
                    // 2ND/3RD FRAMES
                    tpl.AddTexture(sIcon1, formatsT[2], formatsP[2]);
                    tpl.AddTexture(sIcon1, formatsT[3], formatsP[3]);

                    // 4TH FRAME
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[4], formatsP[4]);

                    // 5TH FRAME
                    g.DrawImage(sIcon1, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[5], formatsP[5]);
                }

                // END FRAME
                tpl.AddTexture(sIconEnd, formatsT[numTextures - 1], formatsP[numTextures - 1]);

                g.Dispose();
                sIcon1.Dispose();
                sIcon2.Dispose();
                sIconEnd.Dispose();
            }

            return tpl;
        }

        /// <summary>
        /// Saveicon WTE generator for SEGA
        /// </summary>
        /// <param name="platform">Target console</param>
        /// <param name="tplArray">misc.wte in bytes</param>
        /// <returns>Modified WTE files in byte array format</returns>
        public byte[][] CreateSaveWTE(Console platform, byte[] miscWTE)
        {
            // INCOMPLETE
            return new byte[1][];
        }

        /* TO-DO!!!!!!!!!!!!!! */
        public void CreateSave(Console platform)
        {
            Directory.CreateDirectory(Paths.Images);

            if (platform != Console.Flash)
            {
                bool embedded = false;
                string tplPath = null;
                bool usesWte = false;

                switch (platform)
                {
                    case Console.SMS:
                    case Console.SMDGEN:
                        usesWte = true;
                        break;
                    case Console.PCE:
                        tplPath = "content5\\savedata.tpl";
                        break;
                    case Console.NeoGeo:
                    case Console.MSX:
                        embedded = true;
                        break;
                }
                if (embedded && !File.Exists(tplPath) && !usesWte) return;
                else if (tplPath == null && !usesWte) return;

                // -------------------------------------------------------------------------------------
                // Save image generation for WTEs (uses the source WTE files embedded within the WAD)
                // -------------------------------------------------------------------------------------
                {
                    foreach (var item in Directory.EnumerateFiles(Paths.MiscCCF))
                    {
                        if (System.IO.Path.GetExtension(item) == ".wte")
                        {
                            Process.Run
                            (
                                Paths.Tools + "brawllib\\wteconvert.exe",
                                $"\"{item}\" \"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}.tex0\""
                            );
                            Process.Run
                            (
                                Paths.Tools + "brawllib\\texextract.exe",
                                $"\"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}.tex0\" \"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}.png\""
                            );
                        }
                    }

                    // 01 is icon
                    // 06 is end
                    // banner_[xx] is savebanner
                    Bitmap sBanner = new Bitmap(192, 64);
                    Bitmap sIcon1 = new Bitmap(48, 48);
                    Image sIcon2 = Image.FromFile(Paths.Images + "06.png");
                    string bannerImage = File.Exists(Paths.Images + "banner_eu.png") ? Paths.Images + "banner_eu.png"
                                       : File.Exists(Paths.Images + "banner_us.png") ? Paths.Images + "banner_us.png"
                                       : File.Exists(Paths.Images + "banner_jp.png") ? Paths.Images + "banner_jp.png"
                                       : Paths.Images + "banner.png";

                    using (Image img = (Image)sBanner.Clone())
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(Image.FromFile(bannerImage), new Point(0, 0));
                        g.DrawImage(SaveIcon, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);
                        img.Save(Paths.Images + System.IO.Path.GetFileNameWithoutExtension(bannerImage) + "_new.png");

                        img.Dispose();
                        g.Dispose();
                    }

                    using (Image img = (Image)sIcon1.Clone())
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(SaveIconPlaceholder_SEGA, 0, 0, SaveIconPlaceholder_SEGA.Width, SaveIconPlaceholder_SEGA.Height);
                        g.InterpolationMode = Interpolation;
                        g.PixelOffsetMode = PixelOffset;
                        g.SmoothingMode = Smoothing;
                        g.CompositingQuality = CompositingQ;
                        g.DrawImage(SaveIcon, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                        img.Save(Paths.Images + "01_new.png");

                        img.Dispose();
                        g.Dispose();
                    }

                    // Update sIcon1 to modified version
                    sIcon1.Dispose();
                    sIcon1 = (Bitmap)Image.FromFile(Paths.Images + "01_new.png");

                    using (Image img1 = (Image)sIcon1.Clone())
                    using (Image img2 = (Image)sIcon2.Clone())
                    using (Graphics g = Graphics.FromImage(img1))
                    using (var a = new ImageAttributes())
                    {
                        var w = img1.Width; var h = img1.Height;

                        a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[1] });
                        g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                        img1.Save(Paths.Images + "02_new.png");

                        g.DrawImage(img1, 0, 0);
                        a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[2] });
                        g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                        img1.Save(Paths.Images + "03_new.png");

                        g.DrawImage(img1, 0, 0);
                        a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[3] });
                        g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                        img1.Save(Paths.Images + "04_new.png");

                        g.DrawImage(img1, 0, 0);
                        a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[4] });
                        g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                        img1.Save(Paths.Images + "05_new.png");

                        g.Dispose();
                        img1.Dispose();
                        img2.Dispose();
                        sIcon1.Dispose();
                    }

                    foreach (var item in Directory.EnumerateFiles(Paths.Images))
                    {
                        if (Path.GetExtension(item) == ".tex0" && File.Exists(Paths.Images + Path.GetFileNameWithoutExtension(item) + "_new.png"))
                        {
                            // --------------------------------------------
                            // TEX0 conversion
                            // --------------------------------------------
                            Process.Run
                            (
                                Paths.Tools + "brawllib\\texreplace.exe",
                                $"\"{item}\" \"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}_new.png\""
                            );

                            // --------------------------------------------
                            // Check if operation has been cancelled
                            // --------------------------------------------
                            if (!File.Exists($"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}_new.png")) throw new OperationCanceledException();

                            Process.Run
                            (
                                Paths.Tools + "brawllib\\texextract.exe",
                                $"\"{item}\" \"{Paths.Images}{System.IO.Path.GetFileNameWithoutExtension(item)}_ext.png\""
                            );

                            // --------------------------------------------
                            // TEX0 conversion
                            // --------------------------------------------
                            using (Bitmap ext = (Bitmap)Image.FromFile($"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}_ext.png"))
                            using (Bitmap src = (Bitmap)Image.FromFile($"{Paths.Images}{Path.GetFileNameWithoutExtension(item)}.png"))
                            {
                                bool same = true;
                                for (int x = 0; x < ext.Width; ++x)
                                    for (int y = 0; y < src.Height; ++y)
                                        if (ext.GetPixel(x, y) != src.GetPixel(x, y))
                                            same = false;

                                if (same && !Path.GetFileNameWithoutExtension(item).Contains("06"))
                                {
                                    ext.Dispose();
                                    src.Dispose();
                                    sBanner.Dispose();
                                    sIcon1.Dispose();
                                    sIcon2.Dispose();
                                    throw new OperationCanceledException();
                                }

                                ext.Dispose();
                                src.Dispose();
                            }
                            File.Delete($"{Paths.Images}{System.IO.Path.GetFileNameWithoutExtension(item)}_ext.png");

                            // --------------------------------------------
                            // WTE conversion
                            // --------------------------------------------
                            File.Delete(Paths.MiscCCF + System.IO.Path.GetFileNameWithoutExtension(item) + ".wte");
                            Process.Run
                            (
                                Paths.Tools + "brawllib\\wteconvert.exe",
                                $"\"{item}\" \"{Paths.MiscCCF}{System.IO.Path.GetFileNameWithoutExtension(item)}.wte\""
                            );
                        }
                    }

                    sBanner.Dispose();
                    sIcon1.Dispose();
                    sIcon2.Dispose();
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
                    g.DrawImage(SaveBannerFlash, new Point(0, 0));
                    g.DrawImage(SaveIcon, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);
                    img.Save(sFiles[0]);

                    img.Dispose();
                    g.Dispose();
                }

                using (Image img = (Image)sIcon1.Clone())
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(SaveIconPlaceholder, new Point(0, 0));
                    g.InterpolationMode = Interpolation;
                    g.PixelOffsetMode = PixelOffset;
                    g.SmoothingMode = Smoothing;
                    g.CompositingQuality = CompositingQ;
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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SwitchToThisWindow(IntPtr point, bool on);

        public void Dispose()
        {
            VCPic.Dispose();
            IconVCPic.Dispose();
            SaveIcon.Dispose();
        }
    }
}
