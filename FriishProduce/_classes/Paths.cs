using System.IO;

namespace FriishProduce
{
    public class Paths
    {
        public static readonly string EnvironmentFolder = System.Windows.Forms.Application.StartupPath;
        public static readonly string WorkingFolder = Path.Combine(EnvironmentFolder, "resources\\temp\\");
        public static readonly string Banners = Path.Combine(EnvironmentFolder, "resources\\tools\\banners\\");

        // Application paths
        public static readonly string Tools = Path.Combine(EnvironmentFolder, "resources\\tools\\");
        public static readonly string Languages = Path.Combine(EnvironmentFolder, "strings\\");
        public static readonly string Out = Path.Combine(EnvironmentFolder, "out\\");

        // Extracted paths
        public static readonly string WAD = WorkingFolder + "wad\\";
        public static readonly string Manual = WorkingFolder + "manual\\";
        public static readonly string DataCCF = WorkingFolder + "data_ccf\\";
        public static readonly string MiscCCF = DataCCF + "misc_ccf\\";

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
