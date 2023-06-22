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
            "FCE Ultra RX",
            "FCEUX TX",
            "Snes9x GX",
            "Snes9x RX",
            "Snes9x TX",
            "Visual Boy Advance GX",
            "Genesis Plus GX",
            "Wii64 (WiiFlow)",
            "Not64",
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
                    File.Copy(Paths.Database + "dol\\fceurx.dol", dir + "boot.dol");
                    break;
                case 2:
                    File.Copy(Paths.Database + "dol\\fceuxtx.dol", dir + "boot.dol");
                    break;
                case 3:
                    File.Copy(Paths.Database + "dol\\snes9xgx.dol", dir + "boot.dol");
                    break;
                case 4:
                    File.Copy(Paths.Database + "dol\\snes9xrx.dol", dir + "boot.dol");
                    break;
                case 5:
                    File.Copy(Paths.Database + "dol\\snes9xtx.dol", dir + "boot.dol");
                    break;
                case 6:
                    File.Copy(Paths.Database + "dol\\vbagx.dol", dir + "boot.dol");
                    break;
                case 7:
                    File.Copy(Paths.Database + "dol\\genplusgx.dol", dir + "boot.dol");
                    meta[9] = meta[9].Replace("</arg>", "/</arg>");
                    break;
                case 8:
                    File.Copy(Paths.Database + "dol\\wii64_wf.dol", dir + "boot.dol");
                    break;
                case 9:
                    File.Copy(Paths.Database + "dol\\not64.dol", dir + "boot.dol");
                    meta[9] =  $"    <arg>rompath=\"{AppFolder}{name}/{romFile}\"</arg>";
                    meta[10] = $"    <arg>SkipMenu=1</arg>";
                    meta.Add("    <arg>ScreenMode=0</arg>");
                    break;
                case 10:
                    File.Copy(Paths.Database + "dol\\mupen64gc.dol", dir + "boot.dol");
                    meta.Add("    <arg>SkipMenu = 1</arg>");
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    break;
                case 11:
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

        public void ConvertWAD(int type, string tid, bool vWii = false)
        {
            string forwarderTarget = type <= 1 ? "00000002.app" : "00000001.app";
            WAD x = type <= 1 ? WAD.Load(Paths.Database + "dol\\COMX.wad") : WAD.Load(Paths.Database + "dol\\WNKO.wad");
            x.Unpack(Paths.WorkingFolder_Forwarder);
            x.Dispose();

            // Copy banner from original WAD
            File.Copy(Paths.WorkingFolder + "00000000.app", Paths.WorkingFolder_Forwarder + "00000000.app", true);

            // Create forwarder .app
            bool v12 = type == 1 || type >= 3;
            var forwarder = v12 ? Properties.Resources.Forwarder_v12 : Properties.Resources.Forwarder_v14;
            var offset = v12 ? 488501 : 522628;
            var id = System.Text.Encoding.ASCII.GetBytes(tid);
            id.CopyTo(forwarder, offset);
            File.WriteAllBytes(Paths.WorkingFolder_Forwarder + forwarderTarget, forwarder);

            // vWii
            if (vWii)
            {
                string NANDloader = type <= 1 ? "00000001.app" : "00000002.app";
                File.WriteAllBytes(Paths.WorkingFolder_Forwarder + NANDloader, Properties.Resources.NANDLoader_vWii);
            }

            // Replace original WAD
            foreach (var item in Directory.EnumerateFiles(Paths.WorkingFolder, "*.*", SearchOption.TopDirectoryOnly))
                File.Delete(item);
            foreach (var item in Directory.EnumerateFiles(Paths.WorkingFolder_Forwarder, "*.*", SearchOption.TopDirectoryOnly))
                File.Copy(item, item.Replace(Paths.WorkingFolder_Forwarder, Paths.WorkingFolder), true);

            Directory.Delete(Paths.WorkingFolder_Forwarder, true);
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
