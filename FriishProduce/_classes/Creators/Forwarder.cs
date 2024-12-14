using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static FriishProduce.FileDatas.Emulators;

namespace FriishProduce
{
    public class Forwarder
    {
        public static readonly (int Index, byte[] File, string Name)[] List = new (int Index, byte[] File, string Name)[]
        {
            (0,     fceugx,             "FCE Ultra GX"),
            (1,     fceurx,             "FCE Ultra RX"),
            (2,     fceuxtx,            "FCEUX TX"),
            (3,     snes9xgx,           "Snes9x GX"),
            (4,     snes9xrx,           "Snes9x RX"),
            (5,     snes9xtx,           "Snes9x TX"),
            (6,     vbagx,              "Visual Boy Advance GX"),
            (7,     genplusgx,          "Genesis Plus GX"),
            (8,     wii64_gln64,        "Wii64 (GLideN64 GFX)"),
            (9,     wii64_rice,         "Wii64 (Rice GFX)"),
            (10,    not64,              "Not64"),
            (11,    mupen64gc_fix94,    "mupen64gc-fix94"),
            (12,    wiistation,         "WiiStation"),
            (13,    easyrpg,            "EasyRPG Player"),
            (14,    mgba,               "mGBA"),
        };

        public string Emulator { get; set; }
        private int EmulatorIndex
        {
            get
            {
                foreach (var item in List)
                {
                    if (item.Name == Emulator) return item.Index;
                }

                return -1;
            }
        }

        public IDictionary<string, string> Settings { get; set; }

        public enum Storages { SD, USB }
        public Storages Storage = Storages.SD;

        public enum WADTypes { Comex, Waninkoko };
        public static WADTypes WADType { get; set; }

        private static string loadPath { get; set; }


        public string ROM { get; set; }
        private string romExtension { get => Path.GetExtension(ROM).ToLower(); }
        private bool isDisc { get => new Disc().CheckValidity(ROM); }

        public string Name { get; set; }
        private string tID;
        public string ID { get => tID; set { tID = value; loadPath = $"%s:/private/vc/{value}/boot.dol"; } }

        public void CreateZIP(string outFile)
        {
            // Declare main variables and failsafes
            // *******
            if (ROM == null) throw new FileNotFoundException();
            if (string.IsNullOrWhiteSpace(outFile)) throw new ArgumentNullException();
            if (EmulatorIndex == -1) throw new NotSupportedException();

            bool hasBIOS = false;
            string PackageFolder = Paths.SDUSBRoot + loadPath.Substring(4).Replace("/boot.dol", "").Replace('/', '\\') + '\\';
            string ROMFolder = isDisc ? PackageFolder + "title\\" : PackageFolder;
            string ROMName = isDisc ? Path.GetFileName(ROM) : (EmulatorIndex >= 7 ? "title" : "HOME Menu") + romExtension;

            // Create SD folder and copy emulator
            // *******
            Directory.CreateDirectory(ROMFolder);
            File.WriteAllBytes(PackageFolder + "boot.dol", List[EmulatorIndex].File);

            // If RPG Maker game, copy all files within the ROM folder (RTP not included as part of this, yet)
            // *******
            if (EmulatorIndex == 13)
            {
                if (RTP.IsValid(Settings["rtp_folder"]))
                {
                    foreach (var folder in Directory.EnumerateDirectories(Settings["rtp_folder"], "*.*", SearchOption.AllDirectories))
                    {
                        string target = Path.Combine(ROMFolder, folder.Replace(Settings["rtp_folder"], "").TrimStart('\\'));
                        if (!Directory.Exists(target)) Directory.CreateDirectory(target);

                        foreach (var file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                        {
                            string rawFile = file.Replace(folder, "");
                            File.Copy(file, target + rawFile, true);
                        }
                    }
                }
                    
                string origPath = Path.GetDirectoryName(ROM);
                foreach (var folder in Directory.EnumerateDirectories(origPath, "*.*", SearchOption.AllDirectories))
                {
                    string target = Path.Combine(ROMFolder, folder.Replace(origPath, "").TrimStart('\\'));
                    if (!Directory.Exists(target)) Directory.CreateDirectory(target);
                }

                foreach (var file in Directory.EnumerateFiles(origPath, "*.*", SearchOption.AllDirectories))
                {
                    string rawFile = file.Replace(origPath, "");
                    if (!Path.GetExtension(file).ToLower().EndsWith("exe")) File.Copy(file, ROMFolder + rawFile.TrimStart('\\'), true);
                }
            }

            else
            {
                // Saves folders
                // *******
                switch (EmulatorIndex)
                {
                    case 0:
                    case 1:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "fceugx\\saves\\");
                        break;
                    case 2:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "fceuxtx\\saves\\");
                        break;
                    case 3:
                    case 4:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "snes9xgx\\saves\\");
                        break;
                    case 5:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "snes9xtx\\saves\\");
                        break;
                    case 6:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "vbagx\\saves\\");
                        break;
                    case 8:
                    case 9:
                    case 11:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "wii64\\roms\\");
                        Directory.CreateDirectory(Paths.SDUSBRoot + "wii64\\saves\\");
                        break;
                    case 10:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "not64\\roms\\");
                        Directory.CreateDirectory(Paths.SDUSBRoot + "not64\\saves\\");
                        break;
                    case 12:
                        Directory.CreateDirectory(Paths.SDUSBRoot + "wiisxrx\\bios\\");
                        Directory.CreateDirectory(Paths.SDUSBRoot + "wiisxrx\\isos\\");
                        Directory.CreateDirectory(Paths.SDUSBRoot + "wiisxrx\\saves\\");
                        Directory.CreateDirectory(Paths.SDUSBRoot + "wiisxrx\\savestates\\");
                        break;
                }

                // Copy game to SD folder
                // *******
                if (!File.Exists(ROMFolder + ROMName))
                {
                    File.Copy(ROM, ROMFolder + ROMName);
                    if (romExtension == ".cue")
                    {
                        foreach (var item in Directory.EnumerateFiles(Path.GetDirectoryName(ROM)))
                        {
                            if ((Path.GetExtension(item).ToLower() == ".bin" || Path.GetExtension(item).ToLower() == ".iso")
                                && (Path.GetFileNameWithoutExtension(item).ToLower() == Path.GetFileNameWithoutExtension(ROM).ToLower()))
                                File.Copy(item, ROMFolder + Path.GetFileName(item));
                        }
                    }
                }

                // Copy BIOS if available
                // *******
                if (Settings != null && Settings.ContainsKey("use_bios") && bool.Parse(Settings["use_bios"]))
                {
                    try
                    {
                        var validList = new List<(Platform Platform, int Index, string Target)>()
                        {
                            (Platform.PSX,   12, Paths.SDUSBRoot + "wiisxrx\\bios\\SCPH1001.BIN"),
                        };

                        foreach (var item in validList)
                        {
                            string source = item.Platform switch {
                                Platform.PSX => Options.BIOSFILES.Default.psx,
                                Platform.GBA => Options.BIOSFILES.Default.gba,
                                _ => null
                            };

                            if (EmulatorIndex == item.Index && File.Exists(source) && BIOS.GetConsole(File.ReadAllBytes(source)) == item.Platform)
                            {
                                File.Copy(source, item.Target);
                                hasBIOS = true;
                            }
                        }
                    }

                    catch
                    {
                        hasBIOS = false;
                    }
                }
            }

            // Prepare for meta.xml creation
            // *******
            ROMFolder = loadPath.Replace("%s:/", Storage == Storages.USB ? "usb:/" : "sd:/").Replace("/boot.dol", "");
            if (isDisc) ROMFolder += "/title";

            // Write main
            // *******
            List<string> meta = new()
            {
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>",
                "<app version=\"1\">",
                $"  <name>{(string.IsNullOrWhiteSpace(Name) ? ID : Name + " - " + ID)}</name>",
                "  <coder></coder>",
                "  <version></version>",
                "  <release_date></release_date>",
                $"  <short_description>SRL Forwarder</short_description>",
                "  <long_description>Loads a ROM from the following path:",
                ROMFolder + '/' + ROMName,
                $"using the {List[EmulatorIndex].Name} emulator.</long_description>",
                "  <arguments>",

            };

            // Change or add parameters when needed
            // *******
            switch (EmulatorIndex)
            {
                case 7:
                    meta.Add($"    <arg>{ROMFolder}/</arg>");
                    meta.Add($"    <arg>{ROMName}</arg>");
                    break;

                case 10:
                    meta.Add($"    <arg>rompath=\"{ROMFolder}/{ROMName}\"</arg>");
                    meta.Add("    <arg>SkipMenu=1</arg>");
                    meta.Add("    <arg>ScreenMode=0</arg>");
                    break;

                case 11:
                    meta.Add($"    <arg>{ROMFolder}</arg>");
                    meta.Add($"    <arg>{ROMName}</arg>");
                    meta.Add("    <arg>loader</arg>"); // Mupen64GC-FIX94 needs at least a third argument for be able to autoboot
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    break;

                case 12:
                    meta.Add($"    <arg>{ROMFolder}</arg>");
                    meta.Add($"    <arg>{ROMName}</arg>");
                    meta.Add($"    <arg>BiosDevice = {(hasBIOS ? "1" : "0")}</arg>");
                    meta.Add($"    <arg>BootThruBios = {(hasBIOS && bool.Parse(Settings["show_bios_screen"]) ? "1" : "0")}</arg>");
                    meta.Add("    <arg>FPS = 0</arg>");
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    meta.Add("    <arg>VideoMode = 0</arg>");
                    meta.Add("    <arg>Interlaced = 1</arg>");
                    break;

                case 14:
                    meta.Add($"    <arg>{ROMFolder}/{ROMName}</arg>");
                    break;

                default:
                    meta.Add($"    <arg>{ROMFolder}</arg>");
                    meta.Add($"    <arg>{ROMName}</arg>");
                    break;
            }

            // Write footer and create meta.xml file
            // *******
            meta.Add("  </arguments>");
            meta.Add("  <ahb_access />");
            meta.Add("</app>");

            File.WriteAllLines(PackageFolder + "meta.xml", meta.ToArray());

            // Do readme
            // *******
            File.WriteAllText(Paths.SDUSBRoot + $"(Extract to your {(Storage == Storages.USB ? "USB" : "SD")} root).txt", "");
        }

        public WAD CreateWAD(WAD WAD)
        {
            // > For GenPlus & all emulators based on Wii64 Team's code (e.g. Wii64, WiiSX and forks), use Comex NANDloader
            // > For WiiMednafen, use Waninkoko NANDloader
            // *******
            WADType = (EmulatorIndex >= 7 && EmulatorIndex <= 11) || EmulatorIndex == 13 ? WADTypes.Comex : WADTypes.Waninkoko;

            // Load and unpack WAD
            // *******
            WAD x = WADType == WADTypes.Comex ? WAD.Load(FileDatas.Forwarder.Base_Comex) : WAD.Load(FileDatas.Forwarder.Base_Waninkoko);

            x.Unpack(Paths.WAD);
            WAD.BannerApp.Save(Paths.WAD + "00000000.app");

            #region -- Define forwarder version --
            bool v12 = EmulatorIndex == 7 || EmulatorIndex == 13;
            byte[] forwarder = v12 ? FileDatas.Forwarder.DOL_V12 : FileDatas.Forwarder.DOL_V14;
            int targetOffset = v12 ? 0x77426 : 0x7F979;
            string targetPath = v12 ? loadPath : loadPath.Substring(4);
            #endregion

            // Create forwarder .app
            // *******
            Encoding.ASCII.GetBytes(targetPath).CopyTo(forwarder, targetOffset);

            File.WriteAllBytes(Paths.WorkingFolder + "forwarder.dol", forwarder);
            Utils.Run(FileDatas.Apps.OpenDolBoot, "OpenDolBoot", "forwarder.dol forwarder.app");
            if (!File.Exists(Paths.WorkingFolder + "forwarder.app")) throw new Exception(Program.Lang.Msg(2, true));

            var forwarderApp = File.ReadAllBytes(Paths.WorkingFolder + "forwarder.app");
            if (File.Exists(Paths.WorkingFolder + "forwarder.app")) File.Delete(Paths.WorkingFolder + "forwarder.app");

            File.WriteAllBytes(Paths.WAD + (x.BootIndex == 1 ? "00000002.app" : "00000001.app"), forwarder);

            // Write OpenDolBoot loader & save
            // *******
            File.WriteAllBytes(Paths.WAD + $"0000000{x.BootIndex}.app", forwarderApp);

            x.CreateNew(Paths.WAD);
            x.Region = WAD.Region;
            Directory.Delete(Paths.WAD, true);
            return x;
        }
    }
}
