using libWiiSharp;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FriishProduce.Forwarders
{
    public class Generic
    {
        public string[] List = new string[]
        {
            "FCE Ultra GX",
            "FCEUX TX",
            "Snes9x GX",
            "Snes9x TX",
            "Visual Boy Advance GX",
            "Genesis Plus GX",
            "Mupen64GC",
            "WiiSXRX"
        };

        public string ROM { get; set; }

        public bool IsISO { get; set; }

        private readonly string AppFolder = "sd:/private/vc/";

        public bool UseUSBStorage { get; set; }

        public void Generate(string name, string outZip, string emuName)
        {
            // Set dolIndex to current selected emulator
            int dolIndex = -1;
            for (int i = 0; i < List.Length; i++)
                if (List[i] == emuName)
                    dolIndex = i;
            if (dolIndex == -1) throw new FileNotFoundException();

            string romFile = (dolIndex >= 5 ? "rom" : "HOME Menu") + Path.GetExtension(ROM).Replace(Paths.PatchedSuffix, "");

            List<string> meta = new List<string>
            {
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>",
                "<app version=\"1\">",
                $"  <name>{name}</name>",
                "  <coder></coder>",
                "  <version></version>",
                "  <release_date></release_date>",
                "  <short_description>Placeholder for emulator.dol</short_description>",
                "  <long_description>This is a sample placeholder for auto-running an emulator using a ROM path argument.</long_description>",
                "  <arguments>",
                $"    <arg>{AppFolder}{name}</arg>",
                $"    <arg>{romFile}</arg>"
            };

            // Create SD folder
            var dir = Paths.WorkingFolder_SD + $"private\\vc\\{name}\\";
            Directory.CreateDirectory(dir);

            // Copy ROM/ISO/CUE to SD folder
            File.Copy(ROM, dir + romFile);

            // Copy BIN if it is paired with CUE
            if (IsISO)
                foreach (var item in Directory.EnumerateFiles(Path.GetDirectoryName(ROM)))
                    if (Path.GetExtension(item) == ".bin" && Path.GetFileNameWithoutExtension(item) == Path.GetFileNameWithoutExtension(ROM))
                        File.Copy(item, dir + $"{Path.GetFileNameWithoutExtension(romFile)}.bin", true);

            // Write relevant emulator .dol
            switch (dolIndex)
            {
                case 0:
                    File.Copy(Paths.Database + "dol\\fceugx.dol", dir + "boot.dol");
                    break;
                case 1:
                    File.Copy(Paths.Database + "dol\\fceuxtx.dol", dir + "boot.dol");
                    break;
                case 2:
                    File.Copy(Paths.Database + "dol\\snes9xgx.dol", dir + "boot.dol");
                    break;
                case 3:
                    File.Copy(Paths.Database + "dol\\snes9xtx.dol", dir + "boot.dol");
                    break;
                case 4:
                    File.Copy(Paths.Database + "dol\\vbagx.dol", dir + "boot.dol");
                    break;
                case 5:
                    File.Copy(Paths.Database + "dol\\genplusgx.dol", dir + "boot.dol");
                    meta[9] = meta[9].Replace("</arg>", "/</arg>");
                    break;
                case 6:
                    File.Copy(Paths.Database + "dol\\mupen64gc.dol", dir + "boot.dol");
                    meta.Add("    <arg>SkipMenu = 1</arg>");
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    break;
                case 7:
                    File.Copy(Paths.Database + "dol\\wiisxrx.dol", dir + "boot.dol");
                    meta.Add("    <arg>SkipMenu = 1</arg>");
                    meta.Add("    <arg>VideoMode = 0</arg>");
                    break;
            }
            if (UseUSBStorage) meta[9] = meta[9].Replace("sd:/", "usb:/");
            meta.Add("  </arguments>");
            meta.Add("  <ahb_access />");
            meta.Add("</app>");
            File.WriteAllLines(dir + "meta.xml", meta.ToArray());

            // Get ZIP directory path & compress to .ZIP archive
            if (File.Exists(outZip)) File.Delete(outZip);
            ZipFile.CreateFromDirectory(Paths.WorkingFolder_SD, outZip);

            // Clean
            Directory.Delete(Paths.WorkingFolder_SD, true);
        }

        public WAD ConvertWAD(WAD w, int NANDloader_type, string tid)
        {
            // Determine app index & reload contents
            w.BootIndex = 1;
            if (w.NumOfContents > 0) w.RemoveAllContents();
            w.BannerApp = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000000.app");

            // Add bootloader
            w.ChangeStartupIOS(58);
            byte[] NANDloader = Properties.Resources.NANDLoader_vWii;
            switch (NANDloader_type)
            {
                case 0:
                    NANDloader = Properties.Resources.NANDLoader_Comex;
                    break;
                case 1:
                    NANDloader = Properties.Resources.NANDLoader_Waninkoko;
                    break;
            };
            w.AddContent(NANDloader, w.BootIndex, w.BootIndex);

            // Create forwarder .app
            var forwarder = Properties.Resources.Forwarder;
            var id = System.Text.Encoding.ASCII.GetBytes(tid);
            id.CopyTo(forwarder, 522628);
            File.WriteAllBytes(Paths.WorkingFolder + "00000001.app", forwarder);

            w.AddContent(forwarder, w.BootIndex == 2 ? 1 : 2, w.BootIndex == 2 ? 1 : 2);

            return w;
        }
    }

    /// <summary>
    /// 0RANGECHiCKEN revision of VBAGX Emulator (retrievable from MarioCube)
    /// </summary>
    public class VBAGX_0C
    {
        public string ROM { get; set; }

        public void ReplaceROM() => File.Copy(ROM, Paths.WorkingFolder + "00000002.app", true);
    }

    /// <summary>
    /// RetroArch fork by SuperrSonic
    /// </summary>
    public class RA_SS
    {
        public string ROM { get; set; }
        public string Core { get; set; }
        public string AppFolder { get; set; }
    }
}
