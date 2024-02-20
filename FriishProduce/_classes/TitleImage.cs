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

        public Bitmap Source { get; protected set; }
        private string SourcePath { get; set; }
        public Bitmap VCPic { get; protected set; }
        public Bitmap IconVCPic { get; protected set; }
        private Bitmap SaveIconPic { get; set; }
        
        internal InterpolationMode Interpolation { get; set; }
        private int[] SaveIconL_xywh { get; set; }
        private int[] SaveIconS_xywh { get; set; }

        public TitleImage(Console console, string path)
        {
            SourcePath = null;
            Create(console, path);
        }

        public void Create(Console console, string path)
        {
            platform = console;
            if (SourcePath == null) Source = null;
            if (path != null) LoadToSource(path);

            VCPic = null;
            IconVCPic = null;
            SaveIconPic = null;
        }

        private PixelOffsetMode PixelOffset;
        private SmoothingMode Smoothing;
        private CompositingQuality CompositingQ;

        /// <summary>
        /// Gets source based on path given to URL or disk file
        /// </summary>
        /// <returns>Image file if successful, blank if otherwise</returns>
        public Bitmap LoadToSource(string path)
        {
            if (path == SourcePath && Source != null) return Source;

            try
            {
                if (path.ToLower().StartsWith("http"))
                {
                    // Prevent certificate error (LibRetro thumbnails website certificate is currently expired and will throw an exception by default)
                    // ****************
                    ServicePointManager.ServerCertificateValidationCallback = delegate
                        (object sender,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    { return true; };

                    using (WebClient c = new WebClient())
                    using (Stream s = c.OpenRead(path))
                    {
                        Source = new Bitmap(s);
                        SourcePath = path;
                        return Source;
                    }
                }
                else
                {
                    Source = (Bitmap)Image.FromFile(path);
                    SourcePath = path;
                    return Source;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Language.Get("Error"), System.Windows.Forms.MessageBoxButtons.OK, Ookii.Dialogs.WinForms.TaskDialogIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Generates VCPic, IconVCPic and saveicon bitmaps for use in injection.
        /// </summary>
        public void Generate(Bitmap src)
        {
            if (src == null) src = Source;

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

            PixelOffset = PixelOffsetMode.Half;
            Smoothing = SmoothingMode.HighQuality;
            CompositingQ = CompositingQuality.AssumeLinear;

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
            SaveIconPic = new Bitmap(SaveIconL_xywh[2], SaveIconL_xywh[3]);

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
                    g.DrawImage(src, 0, 0, 128, 96);

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

                Working.Dispose();
            }

            using (Graphics g = Graphics.FromImage(SaveIconPic))
            {
                g.InterpolationMode = Interpolation;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.Clear(src.GetPixel(0, 0));
                g.DrawImage(src, 0, 0, SaveIconPic.Width, SaveIconPic.Height);
                g.Dispose();
            }

            SaveIcon();
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

        public Bitmap SaveIcon()
        {
            Bitmap bmp = new Bitmap(SaveIconPlaceholder.Width, SaveIconPlaceholder.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(platform == Console.SMS || platform == Console.SMDGEN ? SaveIconPlaceholder_SEGA : SaveIconPlaceholder, 0, 0, bmp.Width, bmp.Height);

                g.InterpolationMode = InterpolationMode.Bilinear;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.HighSpeed;

                g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                g.Dispose();
            }

            return bmp;
        }

        public void ReplaceBanner(WAD w)
        {
            if (!w.HasBanner) return;

            U8[] BannerSet = Banner.GetBanner(w);

            // VCPic.tpl
            // ****************
            TPL tpl = TPL.Load(BannerSet[0].Data[BannerSet[0].GetNodeIndex("VCPic.tpl")]);
            ReplaceTPL(tpl, VCPic);
            BannerSet[0].ReplaceFile(BannerSet[0].GetNodeIndex("VCPic.tpl"), tpl.ToByteArray());

            // IconVCPic.tpl
            // ****************
            tpl = TPL.Load(BannerSet[1].Data[BannerSet[1].GetNodeIndex("IconVCPic.tpl")]);
            ReplaceTPL(tpl, IconVCPic);
            BannerSet[1].ReplaceFile(BannerSet[1].GetNodeIndex("IconVCPic.tpl"), tpl.ToByteArray());
            tpl.Dispose();

            // Replace banner.bin
            // ****************
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), BannerSet[0].ToByteArray());
            BannerSet[0].Dispose();

            // Replace icon.bin
            // ****************
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
            Image sIconBegin = tpl.ExtractTexture(1);
            Image sIconEnd = tpl.ExtractTexture(numTextures - 1);

            // Clean TPL textures
            while (tpl.NumOfTextures > 0) tpl.RemoveTexture(0);

            // -------------------------
            // Savedata banner
            // -------------------------
            using (Graphics g = Graphics.FromImage(sBanner))
            {
                g.DrawImage(SaveIconPic, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);

                tpl.AddTexture(sBanner, formatsT[0], formatsP[0]);

                g.Dispose();
                sBanner.Dispose();
            }

            // -------------------------
            // Savedata icon
            // -------------------------
            // 1ST FRAME
            // ****************
            using (Graphics g = Graphics.FromImage(sIconBegin))
            {
                g.DrawImage(SaveIconPlaceholder, 0, 0, sIconBegin.Width, sIconBegin.Height);

                g.InterpolationMode = InterpolationMode.Bilinear;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.HighSpeed;

                g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                g.Dispose();

                tpl.AddTexture(sIconBegin, formatsT[1], formatsP[1]);

                g.Dispose();
            }

            // ANIMATION AND END FRAMES
            // ****************
            using (Image sIcon = (Image)sIconBegin.Clone())
            using (Graphics g = Graphics.FromImage(sIcon))
            using (var a = new ImageAttributes())
            {
                var w = sIcon.Width; var h = sIcon.Height;

                if (numTextures == 5)
                {
                    // 2ND FRAME
                    // ****************
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon, formatsT[2], formatsP[2]);

                    // 3RD FRAME
                    // ****************
                    g.DrawImage(sIconBegin, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon, formatsT[3], formatsP[3]);
                }

                else if (platform == Console.NeoGeo || platform == Console.MSX)
                {
                    // 2ND/3RD FRAMES
                    // ****************
                    tpl.AddTexture(sIconBegin, formatsT[2], formatsP[2]);
                    tpl.AddTexture(sIconBegin, formatsT[3], formatsP[3]);

                    // 4TH FRAME
                    // ****************
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon, formatsT[4], formatsP[4]);

                    // 5TH FRAME
                    // ****************
                    g.DrawImage(sIconBegin, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(sIconEnd, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon, formatsT[5], formatsP[5]);
                }

                // END FRAME
                // ****************
                tpl.AddTexture(sIconEnd, formatsT[numTextures - 1], formatsP[numTextures - 1]);

                g.Dispose();
                sIconBegin.Dispose();
                sIcon.Dispose();
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
        public void ReplaceSaveWTE()
        {
            string ImagesPath = Paths.MiscCCF + "images\\";
            if (!Directory.Exists(ImagesPath)) Directory.CreateDirectory(ImagesPath);

            foreach (var item in Directory.EnumerateFiles(Paths.MiscCCF))
            {
                if (Path.GetExtension(item) == ".wte")
                {
                    // Convert first to tex0, then to PNG
                    // ****************
                    Process.Run
                    (
                        Paths.Tools + "sega\\wteconvert.exe",
                        $"\"{item}\" \"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.tex0\""
                    );
                    Process.Run
                    (
                        Paths.Tools + "sega\\texextract.exe",
                        $"\"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.tex0\" \"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.png\""
                    );
                }
            }

            // 01 is icon
            // 06 is end
            // banner_[xx] is savebanner
            // ****************
            string sBannerPath = File.Exists(ImagesPath + "banner_eu.png") ? ImagesPath + "banner_eu.png"
                               : File.Exists(ImagesPath + "banner_us.png") ? ImagesPath + "banner_us.png"
                               : File.Exists(ImagesPath + "banner_jp.png") ? ImagesPath + "banner_jp.png"
                               : ImagesPath + "banner.png";

            try
            {
                // -------------------------
                // Savedata banner
                // -------------------------
                using (Image img = Image.FromFile(sBannerPath))
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(SaveIconPic, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);
                    img.Save(ImagesPath + Path.GetFileNameWithoutExtension(sBannerPath) + ".new.png");

                    img.Dispose();
                    g.Dispose();
                }

                // -------------------------
                // Savedata icon
                // -------------------------
                // 1ST FRAME
                // ****************

                using (Image sIconBegin = Image.FromFile(ImagesPath + "01.png"))
                using (Graphics g = Graphics.FromImage(sIconBegin))
                {
                    g.DrawImage(SaveIconPlaceholder_SEGA, 0, 0, sIconBegin.Width, sIconBegin.Height);

                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.SmoothingMode = SmoothingMode.HighSpeed;

                    g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);

                    sIconBegin.Save(ImagesPath + "01.new.png");

                    g.Dispose();
                }

                // OTHER FRAMES
                // ****************
                using (Image img1 = Image.FromFile(ImagesPath + "01.new.png"))
                using (Image img2 = Image.FromFile(ImagesPath + "06.png"))
                using (Graphics g = Graphics.FromImage(img1))
                using (var a = new ImageAttributes())
                {
                    var w = img1.Width; var h = img1.Height;

                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[1] });
                    g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    img1.Save(ImagesPath + "02.new.png");

                    g.DrawImage(img1, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[2] });
                    g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    img1.Save(ImagesPath + "03.new.png");

                    g.DrawImage(img1, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[3] });
                    g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    img1.Save(ImagesPath + "04.new.png");

                    g.DrawImage(img1, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity6[4] });
                    g.DrawImage(img2, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    img1.Save(ImagesPath + "05.new.png");

                    g.Dispose();
                    img1.Dispose();
                    img2.Dispose();
                }
            }
            catch
            {
                throw new Exception(Language.Get("Error002"));
            }

            // Cleanup
            // ****************
            if (File.Exists(ImagesPath + "06.png")) File.Delete(ImagesPath + "06.png");
            if (File.Exists(ImagesPath + "06.tex0")) File.Delete(ImagesPath + "06.tex0");

            foreach (var item in Directory.EnumerateFiles(ImagesPath))
            {
                if (Path.GetExtension(item) == ".tex0" && File.Exists(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".new.png"))
                {
                    if (File.Exists(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".png")) File.Delete(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".png");
                    File.Move(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".new.png", ImagesPath + Path.GetFileNameWithoutExtension(item) + ".png");
                }
            }

            foreach (var item in Directory.EnumerateFiles(ImagesPath))
            {
                if (Path.GetExtension(item) == ".tex0" && File.Exists(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".png"))
                {
                    // Extracts original image to use in failsafe
                    // ****************
                    Process.Run
                    (
                        Paths.Tools + "sega\\texextract.exe",
                        $"\"{item}\" \"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.ext1.png\""
                    );

                    // --------------------------------------------
                    // TEX0 conversion
                    // --------------------------------------------
                    Process.Run
                    (
                        Paths.Tools + "sega\\texreplace.exe",
                        $"\"{item}\" \"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.png\"",
                        true
                    );

                    // --------------------------------------------
                    // Check if operation has been cancelled
                    // --------------------------------------------
                    // First failsafe, checks directly for process cancellation
                    // ****************
                    if (!File.Exists($"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.png")) throw new OperationCanceledException();

                    // Second failsafe, checks extracted image for similarities if Cancel was clicked on the GUI
                    // ****************
                    Process.Run
                    (
                        Paths.Tools + "sega\\texextract.exe",
                        $"\"{item}\" \"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.ext2.png\""
                    );
                    using (Bitmap ext = (Bitmap)Image.FromFile($"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.ext1.png"))
                    using (Bitmap src = (Bitmap)Image.FromFile($"{ImagesPath}{Path.GetFileNameWithoutExtension(item)}.ext2.png"))
                    {
                        bool same = true;
                        for (int x = 0; x < ext.Width; ++x)
                            for (int y = 0; y < src.Height; ++y)
                                if (ext.GetPixel(x, y) != src.GetPixel(x, y))
                                {
                                    same = false;
                                    break;
                                }

                        ext.Dispose();
                        src.Dispose();

                        if (same) throw new OperationCanceledException();
                    }

                    try { File.Delete(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".ext1.png"); } catch { }
                    try { File.Delete(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".ext2.png"); } catch { }
                    try { File.Delete(ImagesPath + Path.GetFileNameWithoutExtension(item) + ".png"); } catch { }

                    // --------------------------------------------
                    // WTE conversion
                    // --------------------------------------------
                    try { File.Delete(Paths.MiscCCF + Path.GetFileNameWithoutExtension(item) + ".wte"); } catch { }
                    Process.Run
                    (
                        Paths.Tools + "sega\\wteconvert.exe",
                        $"\"{item}\" \"{Paths.MiscCCF}{Path.GetFileNameWithoutExtension(item)}.wte\""
                    );
                }
            }

            if (Directory.Exists(ImagesPath)) Directory.Delete(ImagesPath, true);
        }

        /* TO-DO!!!!!!!!!!!!!! */
        public void CreateSave(Console platform)
        {
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
            }

            // ------------------------------------------------------------------------------------------
            // Save image generation for Flash TPLs (uses Properties.Resources.Save{Icon/Banner}Flash)
            // ------------------------------------------------------------------------------------------
            else
            {/*
                var bannerPath = Paths.WorkingFolder_Content2 + "banner\\US\\banner.tpl";
                var iconPath = Paths.WorkingFolder_Content2 + "banner\\US\\icons.tpl";

                // Declaration of Graphics/Imaging variables
                var sFiles = new System.Collections.Generic.List<string>();
                for (int i = 0; i < 5; i++)
                    sFiles.Add(// ImagesPath + // $"Texture_0{i}.png");

                Bitmap sBanner = new Bitmap(SaveBannerFlash.Width, SaveBannerFlash.Height);
                Bitmap sIcon = new Bitmap(48, 48);

                using (Image img = (Image)sBanner.Clone())
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(SaveBannerFlash, new Point(0, 0));
                    g.DrawImage(SaveIconPic, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);
                    img.Save(sFiles[0]);

                    img.Dispose();
                    g.Dispose();
                }

                using (Image img = (Image)SaveIcon.Clone())
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(SaveIconPlaceholder, new Point(0, 0));

                    g.InterpolationMode = Interpolation;
                    g.PixelOffsetMode = PixelOffset;
                    g.SmoothingMode = Smoothing;
                    g.CompositingQuality = CompositingQ;
                    g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                    img.Save(sFiles[1]);

                    img.Dispose();
                    g.Dispose();
                }

                // Update SaveIcon to modified version
                SaveIcon.Dispose();
                SaveIcon = (Bitmap)Image.FromFile(sFiles[1]);

                using (Image img = (Image)SaveIcon.Clone())
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
                SaveIcon.Dispose();
                sIcon.Dispose();
                tpl.Dispose();*/
            }

            // Delete images directory
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SwitchToThisWindow(IntPtr point, bool on);

        public void Dispose()
        {
            VCPic.Dispose();
            IconVCPic.Dispose();
            SaveIconPic.Dispose();
        }
    }
}
