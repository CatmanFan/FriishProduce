using System;
using System.IO;
using System.IO.Compression;

namespace FriishProduce.Forwarders
{
    /// <summary>
    /// VBAGX Emulator forwarder
    /// </summary>
    public class Snes9xGX
    {
        public string ROM { get; set; }
        public string AppFolder = "sd:/apps/forwarders/";

        public string[] meta = new string[]
        {
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>",
            "<app version=\"1\">",
            "<name>Placeholder</name>",
            "<short_description>Placeholder for emulator.dol</short_description>",
            "<arguments>",
            "<arg></arg>",
            "<arg>rom</arg>",
            "</arguments>",
            "<ahb_access/>",
            "</app>"
        };

        public void Generate(string name, string outZIP)
        {
            meta[2] = $"<name>{name}</name>";
            name = name.Replace(" ", "").Replace(":", "-").Replace("?", "").ToLower();
            meta[5] = $"<name>{AppFolder}{name}/</name>";

            Directory.CreateDirectory(Paths.WorkingFolder + "sd\\apps\\forwarders\\" + name);
            File.Copy(ROM, Paths.WorkingFolder + "sd\\apps\\forwarders\\" + name + "\\rom");
            ZipFile.CreateFromDirectory(Paths.WorkingFolder + "sd\\", outZIP);
            Directory.Delete(Paths.WorkingFolder + "sd\\", true);
        }
    }
    /// <summary>
    /// VBAGX Emulator forwarder
    /// </summary>
    public class VBAGX
    {
        public string ROM { get; set; }
        public string AppFolder { get; set; }
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
