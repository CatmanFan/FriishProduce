using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class Forwarder
    {
        public static readonly IDictionary<int, string> List = new Dictionary<int, string>
        {
            { 0, "FCE Ultra GX" },
            { 1, "FCE Ultra RX" },
            { 2, "FCEUX TX" },
            { 3, "Snes9x GX" },
            { 4, "Snes9x RX" },
            { 5, "Snes9x TX" },
            { 6, "Visual Boy Advance GX" },
            { 7, "Genesis Plus GX" },
            { 8, "Wii64 (GLideN64 GFX)" },
            { 9, "Wii64 (Rice GFX)" },
            { 10, "Not64" },
            { 11, "mupen64gc-fix94" },
        };

        private static readonly IDictionary<int, string> Files = new Dictionary<int, string>
        {
            { 0, "fceugx" },
            { 1, "fceurx" },
            { 2, "fceuxtx" },
            { 3, "snes9xgx" },
            { 4, "snes9xrx" },
            { 5, "snes9xtx" },
            { 6, "vbagx" },
            { 7, "genplusgx" },
            { 8, "wii64_gln64" },
            { 9, "wii64_rice" },
            { 10, "not64" },
            { 11, "mupen64gc-fix94" }
        };

        public static string Emulator { get; set; }
        private static int EmulatorIndex
        {
            get
            {
                foreach (KeyValuePair<int, string> item in List)
                {
                    if (item.Value == Emulator) return item.Key;
                }

                return -1;
            }
        }

        public enum Storages { SD, USB }
        public static Storages Storage = Storages.SD;

        public enum WADTypes { Comex, Waninkoko, Comex_v12, Waninkoko_v12 };
        public static WADTypes WADType { get; set; }

        private static readonly string AppFolder = "sd:/private/vc/";

        private static bool IsDisc { get => false; } // Emulator == List[12] || Emulator == List[13]

        public static byte[] ROM { get; set; }

        public static string ID { get; set; }

        public static void CreateZIP(string Out)
        {
            if (ROM == null) throw new FileNotFoundException();
            if (EmulatorIndex == -1) throw new NotSupportedException();
            if (!File.Exists(Paths.Emulators + Files[EmulatorIndex] + ".dol")) throw new Exception(Language.Get("Error.008"));

            // Create SD folder
            // *******
            var dir = Paths.SDUSBRoot + $"private\\vc\\{ID}\\";
            if (IsDisc) dir += "title\\";
            Directory.CreateDirectory(dir);

            // Copy game to SD folder
            // *******
            string romFile = (EmulatorIndex >= 7 ? "title" : "HOME Menu") + ".rom";
            File.WriteAllBytes(dir + romFile, ROM);

            // Clean file directory string
            // *******
            if (dir.EndsWith("title\\")) dir = dir.Substring(0, dir.Length - "title\\".Length);

            // Declare meta.xml list
            // *******
            string root = Storage == Storages.USB ? AppFolder.Replace("sd:/", "usb:/") : AppFolder;
            List<string> meta = new List<string>
            {
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>",
                "<app version=\"1\">",
                $"  <name>{ID}</name>",
                "  <coder></coder>",
                "  <version></version>",
                "  <release_date></release_date>",
                "  <short_description>Placeholder for emulator.dol</short_description>",
                "  <long_description>This is a sample placeholder for auto-running an emulator using a ROM path argument.</long_description>",
                "  <arguments>",
                IsDisc ? $"    <arg>{root}{ID}/title</arg>" : $"    <arg>{root}{ID}</arg>",
                $"    <arg>{romFile}</arg>"
            };
            switch (EmulatorIndex)
            {
                case 7:
                    meta[9]  = meta[9].Replace("</arg>", "/</arg>");
                    break;

                case 10:
                    meta[9]  = $"    <arg>rompath=\"{root}{ID}/{romFile}\"</arg>";
                    meta[10] = $"    <arg>SkipMenu=1</arg>";
                    meta.Add("    <arg>ScreenMode=0</arg>");
                    break;

                case 11:
                    meta.Add("    <arg>loader</arg>"); // Mupen64GC-FIX94 needs at least a third argument for be able to autoboot
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    break;
            }
            meta.Add("  </arguments>");
            meta.Add("  <ahb_access />");
            meta.Add("</app>");
            File.WriteAllLines(dir + "meta.xml", meta.ToArray());

            // Get ZIP directory path & compress to .ZIP archive
            // *******
            if (File.Exists(Out)) File.Delete(Out);
            ZipFile.CreateFromDirectory(Paths.SDUSBRoot, Out);

            // Clean
            // *******
            Directory.Delete(Paths.SDUSBRoot, true);
        }

        public static WAD CreateWAD(WAD WAD, string TitleID, bool vWii = false)
        {
            // > For GenPlus & all emulators based on Wii64 Team's code (e.g. Wii64, WiiSX and forks), use Comex NANDloader
            // > For WiiMednafen, use Waninkoko NANDloader
            // *******
            WADType = WADTypes.Waninkoko;
            if (EmulatorIndex == 8 && EmulatorIndex <= 11) WADType = WADTypes.Comex_v12;
            if (EmulatorIndex >= 7 && EmulatorIndex <= 11) WADType = WADTypes.Comex;

            WAD x = WADType == WADTypes.Comex || WADType == WADTypes.Comex_v12 ? WAD.Load(Properties.Resources.forwarder_comex) : WAD.Load(Properties.Resources.forwarder_waninkoko);

            x.Unpack(Paths.WAD);
            WAD.BannerApp.Save(Paths.WAD + "00000000.app");

            // Create forwarder .app
            // *******
            bool isV12 = WADType == WADTypes.Comex_v12 || WADType == WADTypes.Waninkoko_v12;
            byte[] Forwarder = isV12 ? Properties.Resources.forwarder_v12 : Properties.Resources.forwarder_v14;
            var TargetOffset = isV12 ? 488501 : 522628;
            Encoding.ASCII.GetBytes(TitleID).CopyTo(Forwarder, TargetOffset);

            File.WriteAllBytes(Paths.WAD + $"0000000{(WADType == WADTypes.Comex || WADType == WADTypes.Comex_v12 ? 2 : 1)}.app", Forwarder);
            if (vWii) File.WriteAllBytes(Paths.WAD + $"0000000{(WADType == WADTypes.Comex || WADType == WADTypes.Comex_v12 ? 1 : 2)}.app", Properties.Resources.nandloader_vwii);

            x.CreateNew(Paths.WAD);
            x.Region = WAD.Region;
            Directory.Delete(Paths.WAD, true);
            return x;
        }

        public static void Dispose()
        {
            ROM = null;
            Emulator = null;
            ID = null;
        }
    }
}
