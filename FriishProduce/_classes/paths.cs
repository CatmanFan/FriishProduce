using System;
using System.IO;

namespace FriishProduce
{
    public class Paths
    {
        public static readonly string EnvironmentFolder = Environment.CurrentDirectory;
        public static readonly string Apps = Path.Combine(Environment.CurrentDirectory, "apps\\");
        public static readonly string Database = Path.Combine(Environment.CurrentDirectory, "bases\\");
        public static readonly string Languages = Path.Combine(Environment.CurrentDirectory, "locales\\");

        // VC paths
        public static readonly string WorkingFolder = "C:\\FriishProduce\\";
        public static readonly string WorkingFolder_WAD = WorkingFolder + "out.wad";
        public static readonly string WorkingFolder_Contents = WorkingFolder + "contents\\";
        public static readonly string WorkingFolder_Content4 = WorkingFolder + "content4\\";
        public static readonly string WorkingFolder_Content5 = WorkingFolder + "content5\\";
        public static readonly string WorkingFolder_DataCCF = WorkingFolder_Content5 + "data\\";
        public static readonly string WorkingFolder_MiscCCF = WorkingFolder_Content5 + "data\\misc\\";
        public static readonly string WorkingFolder_Banner = WorkingFolder + "banner\\";
        public static readonly string WorkingFolder_Icon = WorkingFolder + "icon\\";

        // Flash paths
        public static readonly string WorkingFolder_Content2 = WorkingFolder + "content2\\";
        public static readonly string WorkingFolder_FlashSWF = WorkingFolder_Content2 + "content\\menu.swf";
        public static readonly string WorkingFolder_FlashConfig = WorkingFolder_Content2 + "config\\config.common.pcf";

        // Forwarder paths
        public static readonly string WorkingFolder_ROM = WorkingFolder + "rom\\";
        public static readonly string WorkingFolder_SD = WorkingFolder + "SD\\";
        public static readonly string WorkingFolder_Forwarder = WorkingFolder + "forwarder\\";
        public static readonly string DOL = Database + "fwdr_dol\\";
        public static readonly string BIOS = Database + "fwdr_bios\\";

        // Other
        public static readonly string PatchedSuffix = "-patched";
        public static readonly string Images = WorkingFolder + "images\\";
    }
}
