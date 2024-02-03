using System;
using System.IO;

namespace FriishProduce
{
    public class Paths
    {
        public static readonly string EnvironmentFolder = Environment.CurrentDirectory;
        public static readonly string WorkingFolder = Path.Combine(EnvironmentFolder, "resources\\temp\\");
        public static readonly string Banners = Path.Combine(EnvironmentFolder, "resources\\tools\\banners\\");

        // Application paths
        public static readonly string Tools = Path.Combine(EnvironmentFolder, "resources\\tools\\");
        public static readonly string Languages = Path.Combine(EnvironmentFolder, "strings\\");

        // SEGA VC paths
        public static readonly string CCF = WorkingFolder + "CCF\\";
        public static readonly string DataCCF = CCF + "data\\";
        public static readonly string MiscCCF = DataCCF + "misc\\";
        public static readonly string Images = WorkingFolder + "images\\";

        // Flash paths
        public static readonly string WorkingFolder_Content2 = WorkingFolder + "content2\\";
        public static readonly string WorkingFolder_FlashSWF = WorkingFolder_Content2 + "content\\menu.swf";
        public static readonly string WorkingFolder_FlashConfig = WorkingFolder_Content2 + "config\\config.common.pcf";

        // Forwarder paths
        public static readonly string WorkingFolder_ROM = WorkingFolder + "rom\\";
        public static readonly string WorkingFolder_SD = WorkingFolder + "SD\\";
        public static readonly string WorkingFolder_Forwarder = WorkingFolder + "forwarder\\";
        public static readonly string DOL = Path.Combine(EnvironmentFolder, "resources\\forwarders\\");
        public static readonly string BIOS = Path.Combine(DOL, "bios\\");

        // Other
        public static readonly string PatchedSuffix = "-patched";
    }
}
