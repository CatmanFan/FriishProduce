using libWiiSharp;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FriishProduce.Injectors
{
    public class Forwarders
    {
        public string[] List = new string[]
        {
            /* 00 */ "FCE Ultra GX",
            /* 01 */ "FCE Ultra RX",
            /* 02 */ "FCEUX TX",
            /* 03 */ "Snes9x GX",
            /* 04 */ "Snes9x RX",
            /* 05 */ "Snes9x TX",
            /* 06 */ "Visual Boy Advance GX",
            /* 07 */ "Genesis Plus GX",
            /* 08 */ "Wii64 (GLideN64 GFX)",
            /* 09 */ "Wii64 (Rice GFX)",
            /* 10 */ "Not64",
            /* 11 */ "Mupen64GC-FIX94",
            /* 12 */ "WiiSX",
            /* 13 */ "WiiStation"
        };

        public int DolIndex { get; set; }

        public string ROM { get; set; }

        public bool IsDisc { get; set; }

        private readonly string AppFolder = "sd:/private/vc/";

        public bool UseUSBStorage { get; set; }

        public void SetDOLIndex(string emuName)
        {
            // Set dolIndex to current selected emulator
            DolIndex = -1;
            for (int i = 0; i<List.Length; i++)
                if (List[i] == emuName)
                    DolIndex = i;
            if (DolIndex == -1) throw new System.InvalidOperationException();
        }

        public void Generate(string name, string outZip, bool UseBios = false, bool BootBios = false)
        {
            // Set filename
            string romFile = (DolIndex >= 7 ? "title" : "HOME Menu") + Path.GetExtension(ROM).Replace(Paths.PatchedSuffix, string.Empty);
            if (IsDisc) romFile = Path.GetFileName(ROM);

            // Create SD folder
            var dir = Paths.WorkingFolder_SD + $"private\\vc\\{name}\\";
            if (IsDisc) dir += "title\\";
            Directory.CreateDirectory(dir);

            // Copy ROM/ISO/CUE to SD folder
            File.Copy(ROM, dir + romFile);

            // ISO functions
            if (IsDisc)
            {
                // Check for BIOS
                string[] BIOSfiles = new string[] { "skip" };
                string target = "example\\";

                switch (DolIndex)
                {
                    case 7:
                        BIOSfiles = new string[] { "bios_CD_E.bin", "bios_CD_U.bin", "bios_CD_J.bin" };
                        target = "genplus\\bios\\";
                        break;
                    case 12:
                    case 13:
                        BIOSfiles = new string[] { "SCPH1000.BIN", "SCPH1001.BIN", "SCPH1002.BIN" };
                        string def = List[DolIndex].ToLower();
                        if (def.ToLower() == "wiistation") def = "wiisxrx";
                        target = $"{def}\\bios\\";
                        break;
                }

                if (BIOSfiles[0] != "skip" && target != "example\\")
                {
                    try
                    {
                        int missedFiles = 0;

                        Directory.CreateDirectory(Paths.WorkingFolder_SD + target);
                        foreach (var item in BIOSfiles)
                        {
                            string file = $"{Paths.BIOS}{item}";
                            string fileName = Path.GetFileNameWithoutExtension(item);
                            string fileExtension = Path.GetExtension(item);

                            if (File.Exists(file))
                                File.Copy(file, Paths.WorkingFolder_SD + target + item, true);

                            // Alternate filename checks
                            else if (File.Exists($"{Paths.BIOS}{fileName}{fileExtension.ToLower()}"))
                                       File.Copy($"{Paths.BIOS}{fileName}{fileExtension.ToLower()}", Paths.WorkingFolder_SD + target + item, true);
                            else if (File.Exists($"{Paths.BIOS}{fileName.ToLower()}{fileExtension}"))
                                       File.Copy($"{Paths.BIOS}{fileName.ToLower()}{fileExtension}", Paths.WorkingFolder_SD + target + item, true);
                            else if (File.Exists($"{Paths.BIOS}{fileName.ToLower()}{fileExtension.ToLower()}"))
                                       File.Copy($"{Paths.BIOS}{fileName.ToLower()}{fileExtension.ToLower()}", Paths.WorkingFolder_SD + target + item, true);

                            if (!File.Exists(Paths.WorkingFolder_SD + target + item)) missedFiles++;
                        }

                        if (missedFiles > 0) throw new FileNotFoundException();
                    }
                    catch
                    {
                        string msg = Program.Language.Get("m021");
                        foreach (var item in BIOSfiles)
                            msg += System.Environment.NewLine + item;

                        throw new System.Exception(msg);
                    }
                }

                //  Copy BIN/ISO if it is paired with CUE
                foreach (var item in Directory.EnumerateFiles(Path.GetDirectoryName(ROM)))
                    if ((Path.GetExtension(item).ToLower() == ".bin" && Path.GetFileNameWithoutExtension(item).Contains(Path.GetFileNameWithoutExtension(ROM)))
                     || (Path.GetExtension(item).ToLower() == ".iso" && Path.GetFileNameWithoutExtension(item).Contains(Path.GetFileNameWithoutExtension(ROM))))
                        File.Copy(item, dir + $"{Path.GetFileName(item)}", true);
            }

            // Clean file directory string
            if (dir.EndsWith("title\\")) dir = dir.Substring(0, dir.Length - "title\\".Length);

            // Declare meta.xml list
            string root = UseUSBStorage ? AppFolder.Replace("sd:/", "usb:/") : AppFolder;
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
                IsDisc ? $"    <arg>{root}{name}/title</arg>" : $"    <arg>{root}{name}</arg>",
                $"    <arg>{romFile}</arg>"
            };

            // Write relevant emulator .dol
            switch (DolIndex)
            {
                case 0:
                    File.Copy(Paths.DOL + "fceugx.dol", dir + "boot.dol");
                    break;
                case 1:
                    File.Copy(Paths.DOL + "fceurx.dol", dir + "boot.dol");
                    break;
                case 2:
                    File.Copy(Paths.DOL + "fceuxtx.dol", dir + "boot.dol");
                    break;
                case 3:
                    File.Copy(Paths.DOL + "snes9xgx.dol", dir + "boot.dol");
                    break;
                case 4:
                    File.Copy(Paths.DOL + "snes9xrx.dol", dir + "boot.dol");
                    break;
                case 5:
                    File.Copy(Paths.DOL + "snes9xtx.dol", dir + "boot.dol");
                    break;
                case 6:
                    File.Copy(Paths.DOL + "vbagx.dol", dir + "boot.dol");
                    break;
                case 7:
                    File.Copy(Paths.DOL + "genplusgx.dol", dir + "boot.dol");
                    meta[9] = meta[9].Replace("</arg>", "/</arg>");
                    break;
                case 8:
                    File.Copy(Paths.DOL + "wii64_gln64_wf.dol", dir + "boot.dol");
                    break;
                case 9:
                    File.Copy(Paths.DOL + "wii64_rice_wf.dol", dir + "boot.dol");
                    break;
                case 10:
                    File.Copy(Paths.DOL + "not64.dol", dir + "boot.dol");
                    meta[9] =  $"    <arg>rompath=\"{root}{name}/{romFile}\"</arg>";
                    meta[10] = $"    <arg>SkipMenu=1</arg>";
                    meta.Add("    <arg>ScreenMode=0</arg>");
                    break;
                case 11:
                    File.Copy(Paths.DOL + "mupen64gc-fix94.dol", dir + "boot.dol");
                    meta.Add("    <arg>loader</arg>"); // Mupen64GC-FIX94 needs at least a third argument for be able to autoboot
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    break;
                case 12:
                case 13:
                    File.Copy(Paths.DOL + $"{List[DolIndex].ToLower()}.dol", dir + "boot.dol");
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    meta.Add("    <arg>VideoMode = 0</arg>");
                    meta.Add($"    <arg>BiosDevice = {(UseUSBStorage ? '2' : '1')}</arg>");
                    if (BootBios) meta.Add("    <arg>BootThruBios = 1</arg>");
                    if (DolIndex == 13) meta.Add("    <arg>FPS = 0</arg>");
                    break;
            }
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
            bool COMX = type <= 1;
            bool v12  = type == 1 || type >= 3;
            
            var forwarderTarget = COMX ? "00000002.app"   : "00000001.app";
            var NANDloader      = COMX ? "00000001.app"   : "00000002.app";
            var x               = COMX ? WAD.Load(Properties.Resources.Forwarder_COMX) : WAD.Load(Properties.Resources.Forwarder_WNKO);
            x.Unpack(Paths.WorkingFolder_Forwarder);
            x.Dispose();

            // Copy banner from original WAD
            File.Copy(Paths.WorkingFolder + "00000000.app", Paths.WorkingFolder_Forwarder + "00000000.app", true);

            // Create forwarder .app
            var forwarder = v12 ? Properties.Resources.Forwarder_v12 : Properties.Resources.Forwarder_v14;
            var offset    = v12 ? 488501                             : 522628;
            var id        = System.Text.Encoding.ASCII.GetBytes(tid);
            id.CopyTo(forwarder, offset);
            File.WriteAllBytes(Paths.WorkingFolder_Forwarder + forwarderTarget, forwarder);

            // vWii
            if (vWii) File.WriteAllBytes(Paths.WorkingFolder_Forwarder + NANDloader, Properties.Resources.NANDLoader_vWii);

            // Replace original WAD
            foreach (var item in Directory.EnumerateFiles(Paths.WorkingFolder, "*.*", SearchOption.TopDirectoryOnly))
                File.Delete(item);
            foreach (var item in Directory.EnumerateFiles(Paths.WorkingFolder_Forwarder, "*.*", SearchOption.TopDirectoryOnly))
                File.Copy(item, item.Replace(Paths.WorkingFolder_Forwarder, Paths.WorkingFolder), true);

            Directory.Delete(Paths.WorkingFolder_Forwarder, true);
        }
    }
}
