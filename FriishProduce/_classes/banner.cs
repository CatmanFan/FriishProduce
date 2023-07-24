using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libWiiSharp;

namespace FriishProduce
{
    public enum BannerTypes
    {
        Normal = 0,
        PSX = Platforms.PSX,
        GB = Platforms.GB,
        GBC = Platforms.GBC,
        GBA = Platforms.GBA,
        S32X = Platforms.S32X,
        SMCD = Platforms.SMCD
    }

    public class Banner
    {
        internal bool UsingImage { get; set; }
        internal int Custom { get; set; }
        private readonly string VCbrlytPath = Paths.Apps + "vcbrlyt\\";

        /// <summary>
        /// Extracts files from banner & icon U8
        /// </summary>
        public void ExtractFiles(libWiiSharp.U8 Banner, libWiiSharp.U8 Icon)
        {
            File.WriteAllBytes(Paths.WorkingFolder + "banner.brlyt", Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

            if (UsingImage)
            {
                Directory.CreateDirectory(Paths.Images);
                File.WriteAllBytes(Paths.Images + "VCPic.tpl", Banner.Data[Banner.GetNodeIndex("VCPic.tpl")]);
                File.WriteAllBytes(Paths.Images + "IconVCPic.tpl", Icon.Data[Icon.GetNodeIndex("IconVCPic.tpl")]);
            }
        }

        /// <summary>
        /// Pack banner & icon U8, then packs banner.app with specified titles
        /// </summary>
        public void PackBanner(libWiiSharp.U8 Banner, libWiiSharp.U8 Icon, libWiiSharp.U8 BannerApp, string[] titles)
        {
            // ----------------------------------------
            // Replace banner & icon, add IMD5 headers
            // ----------------------------------------
            Banner.AddHeaderImd5();
            Icon.AddHeaderImd5();
            BannerApp.ReplaceFile(BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
            BannerApp.ReplaceFile(BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());

            // ----------------------------------------
            // Replace banner.app
            // ----------------------------------------
            BannerApp.AddHeaderImet(false, titles);
            BannerApp.Save(Paths.WorkingFolder + "00000000.app");
        }

        /// <summary>
        /// Copy VCbrlyt to working folder
        /// </summary>
        public void SetupVCBrlyt()
        {
            foreach (string dir in Directory.GetDirectories(VCbrlytPath))
            {
                try { Directory.CreateDirectory(dir.Replace(VCbrlytPath, Paths.WorkingFolder + "vcbrlyt\\")); }
                catch { }
            }
            foreach (string file in Directory.GetFiles(VCbrlytPath, "*.*", SearchOption.AllDirectories))
            {
                try { File.Copy(file, file.Replace(VCbrlytPath, Paths.WorkingFolder + "vcbrlyt\\"), true); }
                catch { }
            }
        }

        /// <summary>
        /// Runs VCbrlyt with specified arguments
        /// </summary>
        public void RunVCBrlyt(string arg)
        {
            var Original = File.ReadAllBytes(Paths.WorkingFolder + "banner.brlyt");

            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = Paths.WorkingFolder + "vcbrlyt\\vcbrlyt.exe",
                WorkingDirectory = Paths.WorkingFolder + "vcbrlyt\\",
                Arguments = $"{Paths.WorkingFolder}banner.brlyt " + arg,
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();

            if (File.ReadAllBytes(Paths.WorkingFolder + "banner.brlyt") == Original)
                throw new Exception(Program.Language.Get("m007"));
            return;
        }

        /// <summary>
        /// Checks for edited brlyt, then replaces the original and cleans leftover files.
        /// </summary>
        public void ReplaceBrlyt(libWiiSharp.U8 Banner)
        {
            // ---------------------------------------------------------------------------------------- //
            Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), Paths.WorkingFolder + "banner.brlyt");
            // ---------------------------------------------------------------------------------------- //

            // ----------------------------------------
            // Cleanup
            // ----------------------------------------
            Directory.Delete(Paths.WorkingFolder + "vcbrlyt\\", true);
            File.Delete(Paths.WorkingFolder + "banner.brlyt");
        }

        /// <summary>
        /// Replaces VC Pic TPL files
        /// </summary>
        /// <param name="Banner">Banner U8</param>
        /// <param name="Icon">Icon U8</param>
        /// <param name="VCPic">Path to extracted VCPic.tpl</param>
        /// <param name="IconVCPic">Path to extracted IconVCPic.tpl</param>
        /// <param name="tImg">The image to insert</param>
        public void ReplaceVCPic(libWiiSharp.U8 Banner, libWiiSharp.U8 Icon, string VCPic, string IconVCPic, TitleImage tImg)
        {
            TPL tpl = TPL.Load(VCPic);
            var tplTF = tpl.GetTextureFormat(0);
            var tplPF = tpl.GetPaletteFormat(0);
            tpl.RemoveTexture(0);
            tpl.AddTexture(tImg.VCPic, tplTF, tplPF);
            tpl.Save(VCPic);

            tpl = TPL.Load(IconVCPic);
            tplTF = tpl.GetTextureFormat(0);
            tplPF = tpl.GetPaletteFormat(0);
            tpl.RemoveTexture(0);
            tpl.AddTexture(tImg.IconVCPic, tplTF, tplPF);
            tpl.Save(IconVCPic);

            tpl.Dispose();

            Banner.ReplaceFile(Banner.GetNodeIndex("VCPic.tpl"), VCPic);
            Icon.ReplaceFile(Icon.GetNodeIndex("IconVCPic.tpl"), IconVCPic);
        }

        public void CustomizeConsole(libWiiSharp.U8 Banner, libWiiSharp.U8 Icon, BannerTypes type, Region region)
        {
            if (type == BannerTypes.Normal) return;

            // ----------------------------------------
            // Define target parameters & images
            // ----------------------------------------
            var targetBannerImage = new byte[0];
            var targetBannerX = new byte[] { 0xC3, 0xB4, 0x73, 0x33 };
            var targetIconImage = Properties.Resources.VC_PSX_Icon;
            var targetIconSize = new byte[] { 0x42, 0x70, 0x00, 0x00, 0x42, 0x70, 0x00, 0x00 };
            bool forceTransparent = false;
            var consoleName = "Undefined";
            var consoleCode = "SFC";
            switch (type)
            {
                default:
                case BannerTypes.PSX:
                    targetBannerImage = Properties.Resources.VC_PSX_Banner;
                    targetIconImage = Properties.Resources.VC_PSX_Icon;
                    targetIconSize = new byte[] { 0x42, 0x70, 0x00, 0x00, 0x42, 0x70, 0x00, 0x00 };
                    consoleName = "PlayStation";
                    consoleCode = "PSX";
                    break;
                case BannerTypes.GB:
                    targetBannerImage = Properties.Resources.VC_GB_Banner;
                    targetIconImage = Properties.Resources.VC_GB_Icon;
                    targetIconSize = new byte[] { 0x42, 0xEC, 0x00, 0x00, 0x41, 0xA8, 0x00, 0x00 };
                    consoleName = "Game Boy";
                    consoleCode = "GB";
                    forceTransparent = true;
                    break;
                case BannerTypes.GBC:
                    targetBannerImage = Properties.Resources.VC_GBC_Banner;
                    targetIconImage = Properties.Resources.VC_GBC_Icon;
                    targetIconSize = new byte[] { 0x42, 0xC0, 0x00, 0x00, 0x42, 0x1C, 0x00, 0x00 };
                    consoleName = "Game Boy Color";
                    consoleCode = "GBC";
                    forceTransparent = true;
                    break;
                case BannerTypes.GBA:
                    targetBannerImage = Properties.Resources.VC_GBA_Banner;
                    targetIconImage = Properties.Resources.VC_GBA_Icon;
                    targetIconSize = new byte[] { 0x42, 0xC0, 0x00, 0x00, 0x42, 0x08, 0x00, 0x00 };
                    consoleName = "Game Boy Advance";
                    consoleCode = "GBA";
                    forceTransparent = true;
                    break;
                case BannerTypes.S32X:
                    consoleName = region == Region.USA ? "Sega 32X" : "Mega 32X";
                    consoleCode = region == Region.Japan ? "MDJ" : "Gen";
                    forceTransparent = true;
                    break;
                case BannerTypes.SMCD:
                    consoleName = region == Region.USA ? "Sega CD" : "Mega CD";
                    consoleCode = region == Region.Japan ? "MDJ" : "Gen";
                    forceTransparent = true;
                    break;
            }

            // ----------------------------------------
            // Search for source logo in banner & icon
            // ----------------------------------------
            string LogoB = null;
            string LogoI = null;

            for (int i = 0; i < Banner.NumOfNodes; i++)
                if (Banner.StringTable[i].StartsWith("my_Back"))
                    LogoB = Banner.StringTable[i];

            for (int i = 0; i < Icon.NumOfNodes; i++)
                if (Icon.StringTable[i].StartsWith("Logo"))
                    LogoI = Icon.StringTable[i];

            if (LogoB == null || LogoI == null) throw new Exception(Program.Language.Get("m020"));

            // ----------------------------------------
            // Replace banner
            // ----------------------------------------
            if (targetBannerImage.Length > 0) Banner.ReplaceFile(Banner.GetNodeIndex(LogoB), targetBannerImage);
            RunVCBrlyt($"-H_T_PF {consoleName.ToUpper()} -Color {consoleCode}");
            RunVCBrlyt($"-H_PF {consoleName.ToUpper()} -Color {consoleCode}");

            // ----------------------------------------
            // Replace icon
            // ----------------------------------------

            // Replace logo image

            TPL tpl = TPL.Load(Icon.Data[Icon.GetNodeIndex(LogoI)]);
            var tplTF = !forceTransparent ? tpl.GetTextureFormat(0) : TPL_TextureFormat.RGB5A3;
            var tplPF = !forceTransparent ? tpl.GetPaletteFormat(0) : TPL_PaletteFormat.RGB5A3;
            tpl.RemoveTexture(0);
            tpl.AddTexture(targetIconImage, tplTF, tplPF);
            Icon.ReplaceFile(Icon.GetNodeIndex(LogoI), tpl.ToByteArray());
            tpl.Dispose();

            // Resize logo in icon.brlyt to proper pixel dimensions

            var BrlytI = Icon.Data[Icon.GetNodeIndex("icon.brlyt")];
            int index = Bytes.Search(BrlytI, "49 63 6F 6E 31 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3F 80 00 00 3F 80 00 00");
            if (index == -1) throw new Exception(Program.Language.Get("m020"));
            targetIconSize.CopyTo(BrlytI, index + 56);
            Icon.ReplaceFile(Icon.GetNodeIndex("icon.brlyt"), BrlytI);
            // NES: 120x32 / N64: 70x64

            // ----------------------------------------

            // ----------------------------------------
            // TO-DO: Resize top header for console name in banner.brlyt
            // ----------------------------------------
            /* var BrlytB = Banner.Data[Banner.GetNodeIndex("banner.brlyt")];
            index = Bytes.Search(BrlytB, "70 69 63 31 00 00 00 80 01 04 FF 00 50 46 4C 69 6E 65 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            if (index == -1) throw new Exception(Program.Language.Get("m020"));
            targetBannerX.CopyTo(BrlytB, index + 36);
            Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), BrlytB); */
            // SNES: -365,5027 / N64: -361,001
        }
    }
}
