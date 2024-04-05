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
    public class ImageHelper
    {
        private Console platform { get; set; }

        public Bitmap Source { get; protected set; }
        private string SourcePath { get; set; }
        public Bitmap VCPic { get; protected set; }
        public Bitmap IconVCPic { get; protected set; }
        private Bitmap SaveIconPic { get; set; }

        internal InterpolationMode Interpolation { get; set; }
        internal bool FitAspectRatio { get; set; }
        private int[] SaveIconL_xywh { get; set; }
        private int[] SaveIconS_xywh { get; set; }

        public ImageHelper(Console console, string path)
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
                MessageBox.Show(Program.Lang.String("error", "messages"), ex.Message, MessageBox.Buttons.Ok, brick);
                return null;
            }
        }

        public Bitmap LoadToSource(Bitmap b) => Source = b;

        /// <summary>
        /// Generates VCPic, IconVCPic and saveicon bitmaps for use in injection.
        /// </summary>
        public void Generate(Bitmap src)
        {
            if (src == null) src = Source;

            bool ShrinkToFit = platform == Console.NES || platform == Console.SNES || platform == Console.N64 || platform == Console.Flash;

            // --------------------------------------------------
            // SAVEICON : DEFINE POSITION AND SIZE VARIABLES
            // --------------------------------------------------
            SaveIconL_xywh = new int[] { 10, 10, 58, 44 };
            SaveIconS_xywh = new int[] { 4, 9, 40, 30 };
            if (platform == Console.SMS || platform == Console.SMD)
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

            if (FitAspectRatio)
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

                    if (FitAspectRatio)
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
                g.DrawImage(platform == Console.SMS || platform == Console.SMD ? SaveIconPlaceholder_SEGA : SaveIconPlaceholder, 0, 0, bmp.Width, bmp.Height);

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

            (U8, U8) BannerSet = BannerHelper.Get(w);

            // VCPic.tpl
            // ****************
            TPL tpl = TPL.Load(BannerSet.Item1.Data[BannerSet.Item1.GetNodeIndex("VCPic.tpl")]);
            ReplaceTPL(tpl, VCPic);
            BannerSet.Item1.ReplaceFile(BannerSet.Item1.GetNodeIndex("VCPic.tpl"), tpl.ToByteArray());

            // IconVCPic.tpl
            // ****************
            tpl = TPL.Load(BannerSet.Item2.Data[BannerSet.Item2.GetNodeIndex("IconVCPic.tpl")]);
            ReplaceTPL(tpl, IconVCPic);
            BannerSet.Item2.ReplaceFile(BannerSet.Item2.GetNodeIndex("IconVCPic.tpl"), tpl.ToByteArray());
            tpl.Dispose();

            // Replace banner.bin
            // ****************
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), BannerSet.Item1.ToByteArray());
            BannerSet.Item1.Dispose();

            // Replace icon.bin
            // ****************
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("icon.bin"), BannerSet.Item2.ToByteArray());
            BannerSet.Item2.Dispose();
        }

        /// <summary>
        /// Saveicon TPL generator for NES/SNES/N64/PCE/NeoGeo/MSX/Flash
        /// </summary>
        /// <param name="type">0 = both banner and icon animations; 1 = banner only: 2 = animation only. This is only valid if creating a new TPL from scratch.</param>
        /// <returns>Modified TPL</returns>
        public TPL CreateSaveTPL(int type)
        {
            TPL tpl = new TPL();

            Image sBanner = new Bitmap(SaveBannerFlash.Width, SaveBannerFlash.Height);
            Image sIconLogo = new Bitmap(SaveIconFlash);
            Image sIcon = new Bitmap(sIconLogo.Width, sIconLogo.Height);

            var TextureFormat = TPL_TextureFormat.RGB5A3;
            var PaletteFormat = TPL_PaletteFormat.RGB5A3;

            // -------------------------
            // Savedata banner
            // -------------------------
            if (type != 2)
                using (Graphics g = Graphics.FromImage(sBanner))
                {
                    g.DrawImage(SaveBannerFlash, 0, 0);
                    g.DrawImage(SaveIconPic, SaveIconL_xywh[0], SaveIconL_xywh[1], SaveIconL_xywh[2], SaveIconL_xywh[3]);

                    tpl.AddTexture(sBanner, TextureFormat, PaletteFormat);

                    g.Dispose();
                }

            sBanner.Dispose();

            // -------------------------
            // Savedata icon
            // -------------------------
            if (type != 1)
            {
                // LOGO
                // ****************
                using (Graphics g = Graphics.FromImage(sIconLogo))
                {
                    g.DrawImage(SaveIconFlash, 0, 0);
                    g.Dispose();
                }

                // 1ST FRAME
                // ****************
                using (Graphics g = Graphics.FromImage(sIcon))
                {
                    g.DrawImage(SaveIconPlaceholder, 0, 0, sIcon.Width, sIcon.Height);

                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.SmoothingMode = SmoothingMode.HighSpeed;

                    g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                    g.Dispose();

                    tpl.AddTexture(sIcon, TextureFormat, PaletteFormat);

                    g.Dispose();
                }

                // ANIMATION AND END FRAMES
                // ****************
                using (Image sIcon2 = (Image)sIcon.Clone())
                using (Graphics g = Graphics.FromImage(sIcon2))
                using (var a = new ImageAttributes())
                {
                    var w = sIcon.Width; var h = sIcon.Height;

                    // 2ND FRAME
                    // ****************
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(sIconLogo, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, TextureFormat, PaletteFormat);

                    // 3RD FRAME
                    // ****************
                    g.DrawImage(sIcon, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(sIconLogo, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, TextureFormat, PaletteFormat);

                    // END FRAME
                    // ****************
                    tpl.AddTexture(sIconLogo, TextureFormat, PaletteFormat);

                    g.Dispose();
                }

                tpl.AddTexture(sIconLogo, TextureFormat, PaletteFormat);
                tpl.AddTexture(tpl.ExtractTexture(2), TextureFormat, PaletteFormat);
                tpl.AddTexture(tpl.ExtractTexture(1), TextureFormat, PaletteFormat);
                tpl.AddTexture(sIcon, TextureFormat, PaletteFormat);
            }

            sIconLogo.Dispose();
            if (sIcon != null) sIcon.Dispose();

            return tpl;
        }

        /// <summary>
        /// Saveicon TPL generator for NES/SNES/N64/PCE/NeoGeo/MSX/Flash
        /// </summary>
        /// <param name="tplArray">The byte array which contains the TPL data (i.e. can be a file read in bytes)</param>
        /// <param name="type">0 = both banner and icon animations; 1 = banner only: 2 = animation only. This is only valid if creating a new TPL from scratch.</param>
        /// <returns>Modified TPL</returns>
        public TPL CreateSaveTPL(byte[] tplBytes)
        {
            /* DIRECTORY PATHS FOR TPLs:
             ******************************
               NES:     embedded in 01.app
               SNES:    05.app/banner.tpl
               N64:     05.app/save_banner.tpl
               PCE:     05.app/savedata.tpl
               NeoGeo:  05.app/banner.bin
               MSX:     05.app/banner.bin
               Flash:   banner/XX/banner.tpl
            */

            TPL tpl;
            try { tpl = TPL.Load(tplBytes); } catch { return CreateSaveTPL(0); }

            int numTextures = tpl.NumOfTextures;
            TPL_TextureFormat[] formatsT = new TPL_TextureFormat[numTextures];
            TPL_PaletteFormat[] formatsP = new TPL_PaletteFormat[numTextures];
            for (int i = 0; i < numTextures; i++)
            {
                formatsT[i] = tpl.GetTextureFormat(i);
                formatsP[i] = tpl.GetPaletteFormat(i);
            }

            Image sBanner = tpl.ExtractTexture(0);
            Image sIcon = new Bitmap(SaveIconPlaceholder.Width, SaveIconPlaceholder.Height);
            Image sIconLogo = tpl.ExtractTexture(numTextures - 1);

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
            using (Graphics g = Graphics.FromImage(sIcon))
            {
                g.DrawImage(SaveIconPlaceholder, 0, 0, sIcon.Width, sIcon.Height);

                g.InterpolationMode = InterpolationMode.Bilinear;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.HighSpeed;

                g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);
                g.Dispose();

                tpl.AddTexture(sIcon, formatsT[1], formatsP[1]);

                g.Dispose();
            }

            // ANIMATION AND END FRAMES
            // ****************
            using (Image sIcon2 = (Image)sIcon.Clone())
            using (Graphics g = Graphics.FromImage(sIcon2))
            using (var a = new ImageAttributes())
            {
                var w = sIcon.Width; var h = sIcon.Height;

                if (numTextures == 5)
                {
                    // 2ND FRAME
                    // ****************
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[1] });
                    g.DrawImage(sIconLogo, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[2], formatsP[2]);

                    // 3RD FRAME
                    // ****************
                    g.DrawImage(sIcon, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = opacity4[2] });
                    g.DrawImage(sIconLogo, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[3], formatsP[3]);
                }

                else if (platform == Console.NEO || platform == Console.MSX)
                {
                    // 2ND/3RD FRAMES
                    // ****************
                    tpl.AddTexture(sIcon2, formatsT[2], formatsP[2]);
                    tpl.AddTexture(sIcon2, formatsT[3], formatsP[3]);

                    // 4TH FRAME
                    // ****************
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = 0.47F });
                    g.DrawImage(sIconLogo, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[4], formatsP[4]);

                    // 5TH FRAME
                    // ****************
                    g.DrawImage(sIcon, 0, 0);
                    a.SetColorMatrix(new ColorMatrix() { Matrix33 = 0.82F });
                    g.DrawImage(sIconLogo, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, a);

                    tpl.AddTexture(sIcon2, formatsT[5], formatsP[5]);
                }

                // END FRAME
                // ****************
                tpl.AddTexture(sIconLogo, formatsT[numTextures - 1], formatsP[numTextures - 1]);

                g.Dispose();
                sIcon.Dispose();
                sIcon.Dispose();
                sIconLogo.Dispose();
            }

            return tpl;
        }

        /// <summary>
        /// Saveicon WTE generator for SEGA
        /// </summary>
        /// <param name="platform">Target console</param>
        /// <param name="tplArray">misc.wte in bytes</param>
        /// <returns>Modified WTE files in byte array format</returns>
        public void CreateSaveWTE(CCF CCF)
        {
            string ImagesPath = Paths.WorkingFolder + "images\\";
            if (!Directory.Exists(ImagesPath)) Directory.CreateDirectory(ImagesPath);

            foreach (var item in CCF.Nodes)
            {
                if (Path.GetExtension(item.Name) == ".wte")
                {
                    File.WriteAllBytes(ImagesPath + item.Name, CCF.Data[CCF.GetNodeIndex(item.Name)]);

                    // Convert first to tex0, then to PNG
                    // ****************
                    Utils.Run
                    (
                        "sega\\wteconvert.exe",
                        ImagesPath,
                        $"\"{item.Name}\" \"{Path.GetFileNameWithoutExtension(item.Name)}.tex0\""
                    );
                    Utils.Run
                    (
                        "sega\\texextract.exe",
                        ImagesPath,
                        $"\"{Path.GetFileNameWithoutExtension(item.Name)}.tex0\" \"{Path.GetFileNameWithoutExtension(item.Name)}.png\""
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

                using (Image sIcon = Image.FromFile(ImagesPath + "01.png"))
                using (Graphics g = Graphics.FromImage(sIcon))
                {
                    g.DrawImage(SaveIconPlaceholder_SEGA, 0, 0, sIcon.Width, sIcon.Height);

                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.SmoothingMode = SmoothingMode.HighSpeed;

                    g.DrawImage(SaveIconPic, SaveIconS_xywh[0], SaveIconS_xywh[1], SaveIconS_xywh[2], SaveIconS_xywh[3]);

                    sIcon.Save(ImagesPath + "01.new.png");

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
                throw new Exception(Program.Lang.Msg(2, true));
            }

            // Cleanup
            // ****************
            if (File.Exists(ImagesPath + "06.png")) File.Delete(ImagesPath + "06.png");
            if (File.Exists(ImagesPath + "06.tex0")) File.Delete(ImagesPath + "06.tex0");

            foreach (var file in Directory.EnumerateFiles(ImagesPath))
            {
                if (Path.GetExtension(file) == ".tex0" && File.Exists(ImagesPath + Path.GetFileNameWithoutExtension(file) + ".new.png"))
                {
                    string item = Path.GetFileNameWithoutExtension(file);

                    // Backups original image to use in failsafe
                    // ****************
                    File.Move(ImagesPath + item + ".png", ImagesPath + item + ".ext1.png");
                    File.Move(ImagesPath + item + ".new.png", ImagesPath + item + ".png");

                    // --------------------------------------------
                    // TEX0 conversion
                    // --------------------------------------------
                    using (Process p = Process.Start(new ProcessStartInfo
                    {
                        FileName = Paths.Tools + "sega\\texreplace.exe",
                        WorkingDirectory = ImagesPath,
                        Arguments = $"\"{item}.tex0\" \"{item}.png\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }))
                    {
                        while (!p.HasExited)
                        {
                            System.Threading.Thread.Sleep(200);

                            IntPtr zero = IntPtr.Zero;

                            for (int i = 0; (i < 60) && (zero == IntPtr.Zero); i++)
                            {
                                zero = FindWindow(null, "Advanced Texture Converter");
                            }

                            if (zero != IntPtr.Zero)
                            {
                                SetForegroundWindow(zero);
                                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                                System.Windows.Forms.SendKeys.Flush();
                            }
                        }
                    }

                    // --------------------------------------------
                    // Check if operation has been cancelled
                    // --------------------------------------------
                    // First failsafe, checks directly for process cancellation
                    // ****************
                    if (!File.Exists($"{ImagesPath}{item}.png")) throw new OperationCanceledException();

                    // Second failsafe, checks extracted image for similarities if Cancel was clicked on the GUI
                    // ****************
                    Utils.Run
                    (
                        "sega\\texextract.exe",
                        ImagesPath,
                        $"\"{item}.tex0\" \"{item}.ext2.png\""
                    );

                    using (Bitmap ext = (Bitmap)Image.FromFile($"{ImagesPath}{item}.ext1.png"))
                    using (Bitmap src = (Bitmap)Image.FromFile($"{ImagesPath}{item}.ext2.png"))
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

                    try { File.Delete(ImagesPath + item + ".ext1.png"); } catch { }
                    try { File.Delete(ImagesPath + item + ".ext2.png"); } catch { }
                    try { File.Delete(ImagesPath + item + ".png"); } catch { }

                    // --------------------------------------------
                    // WTE conversion
                    // --------------------------------------------
                    Utils.Run
                    (
                        "sega\\wteconvert.exe",
                        ImagesPath,
                        $"\"{item}.tex0\" \"{item}.new.wte\""
                    );

                    if (File.Exists(ImagesPath + item + ".new.wte")) CCF.ReplaceFile(CCF.GetNodeIndex($"{item}.wte"), File.ReadAllBytes(ImagesPath + item + ".new.wte"));

                    try { File.Delete(ImagesPath + item + ".new.wte"); } catch { }
                    try { File.Delete(ImagesPath + item + ".wte"); } catch { }
                }
            }

            if (Directory.Exists(ImagesPath)) try { Directory.Delete(ImagesPath, true); } catch { }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public void Dispose()
        {
            VCPic.Dispose();
            IconVCPic.Dispose();
            SaveIconPic.Dispose();
        }
    }
}
