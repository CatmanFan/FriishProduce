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
    public class Forwarder
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
            { 12, "WiiStation" }
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
            { 11, "mupen64gc-fix94" },
            { 12, "wiistation" }
        };

        public string Emulator { get; set; }
        private int EmulatorIndex
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
        public Storages Storage = Storages.SD;

        public enum WADTypes { Comex, Waninkoko };
        public static WADTypes WADType { get; set; }

        private static string Path { get; set; }

        private static bool IsDisc { get => new Disc().CheckValidity(Path); }

        public byte[] ROM { get; set; }
        public string ROMExtension { get; set; }

        private string tID;
        public string ID { get => tID; set { tID = value; Path = $"%s:/private/VC/{value}/boot.dol"; } }

        public void CreateZIP(string Out)
        {
            // Declare main variables and failsafes
            // *******
            if (ROM == null) throw new FileNotFoundException();
            if (EmulatorIndex == -1) throw new NotSupportedException();

            string PackageFolder = Paths.SDUSBRoot + Path.Substring(4).Replace("/boot.dol", "").Replace('/', '\\') + '\\';
            string ROMFolder = IsDisc ? PackageFolder + "title\\" : PackageFolder;
            string ROMName = (EmulatorIndex >= 7 ? "title" : "HOME Menu") + ROMExtension;

            if (!File.Exists(Paths.Emulators + Files[EmulatorIndex] + ".dol")) throw new Exception(Language.Get("Error.008"));

            // Create SD folder and copy emulator
            // *******
            Directory.CreateDirectory(ROMFolder);
            File.Copy(Paths.Emulators + Files[EmulatorIndex] + ".dol", PackageFolder + "boot.dol");

            // Copy game to SD folder
            // *******
            File.WriteAllBytes(ROMFolder + ROMName, ROM);

            // Prepare for meta.xml creation
            // *******
            ROMFolder = Path.Replace("%s:/", Storage == Storages.USB ? "usb:/" : "sd:/").Replace("/boot.dol", "");
            if (IsDisc) ROMFolder += "/title";

            // Write main
            // *******
            List<string> meta = new List<string>
            {
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>",
                "<app version=\"1\">",
                $"  <name>{ID}</name>",
                "  <coder></coder>",
                "  <version></version>",
                "  <release_date></release_date>",
                "  <short_description>SRL Forwarder</short_description>",
                "  <long_description>This will attempt to load a ROM from the following path:" +
                ROMFolder + '/' + ROMName,
                $"using the emulator {Files[EmulatorIndex]}.</long_description>",
                "  <arguments>",
                $"    <arg>{ROMFolder}</arg>",
                $"    <arg>{ROMName}</arg>"
            };

            // Change or add parameters when needed
            // *******
            switch (EmulatorIndex)
            {
                case 7:
                    meta[9]  = meta[9].Replace("</arg>", "/</arg>");
                    break;

                case 10:
                    meta[9]  = $"    <arg>rompath=\"{ROMFolder}/{ROMName}\"</arg>";
                    meta[10] = $"    <arg>SkipMenu=1</arg>";
                    meta.Add("    <arg>ScreenMode=0</arg>");
                    break;

                case 11:
                    meta.Add("    <arg>loader</arg>"); // Mupen64GC-FIX94 needs at least a third argument for be able to autoboot
                    meta.Add("    <arg>ScreenMode = 0</arg>");
                    break;
            }

            // Write footer and create file
            // *******
            meta.Add("  </arguments>");
            meta.Add("  <ahb_access />");
            meta.Add("</app>");

            File.WriteAllLines(PackageFolder + "meta.xml", meta.ToArray());

            // Get ZIP directory path & compress to .ZIP archive
            // *******
            if (File.Exists(Out)) File.Delete(Out);
            ZipFile.CreateFromDirectory(Paths.SDUSBRoot, Out);

            // Clean
            // *******
            Directory.Delete(Paths.SDUSBRoot, true);
        }

        public WAD CreateWAD(WAD WAD, bool vWii = false)
        {
            // > For GenPlus & all emulators based on Wii64 Team's code (e.g. Wii64, WiiSX and forks), use Comex NANDloader
            // > For WiiMednafen, use Waninkoko NANDloader
            // *******
            WADType = EmulatorIndex >= 7 && EmulatorIndex <= 11 ? WADTypes.Comex : WADTypes.Waninkoko;

            // Load and unpack WAD
            // *******
            WAD x = WADType == WADTypes.Comex ? WAD.Load(Properties.Resources.Forwarder_Comex) : WAD.Load(Properties.Resources.Forwarder_Waninkoko);

            x.Unpack(Paths.WAD);
            WAD.BannerApp.Save(Paths.WAD + "00000000.app");

            // Define forwarder version
            // *******
            bool NeedsOldForwarder = EmulatorIndex == 7;
            byte[] Forwarder  = NeedsOldForwarder ? Properties.Resources.ForwarderV12 : Properties.Resources.ForwarderV14;
            int TargetOffset  = NeedsOldForwarder ? 0x77426 : 0x7F979;
            int SecondTargetOffset = NeedsOldForwarder ? 263 : 256;
            string TargetPath = NeedsOldForwarder ? Path : Path.Substring(4);

            // Create forwarder .app
            // *******
            Encoding.ASCII.GetBytes(TargetPath).CopyTo(Forwarder, TargetOffset);
            File.WriteAllBytes(Paths.WAD + (x.BootIndex == 1 ? "00000002.app" : "00000001.app"), Forwarder);

            // Write NANDloader & save
            // *******
            if (vWii) File.WriteAllBytes(Paths.WAD + $"0000000{x.BootIndex}.app", Properties.Resources.Forwarder_vWii);

            x.CreateNew(Paths.WAD);
            x.Region = WAD.Region;
            Directory.Delete(Paths.WAD, true);
            return x;
        }
    }
}
