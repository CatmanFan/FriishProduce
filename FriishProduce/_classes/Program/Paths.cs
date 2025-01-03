﻿using System.IO;

namespace FriishProduce
{
    public static class Paths
    {
        public static readonly string EnvironmentFolder = System.Windows.Forms.Application.StartupPath;
        public static readonly string WorkingFolder = Path.Combine(EnvironmentFolder, "resources\\temp\\");
        public static readonly string Banners = Path.Combine(EnvironmentFolder, "resources\\tools\\banners\\");

        // Application paths
        public static readonly string Configuration = Path.Combine(EnvironmentFolder, "config.json");
        public static readonly string Databases = Path.Combine(EnvironmentFolder, "resources\\databases\\");
        public static readonly string Tools = Path.Combine(EnvironmentFolder, "resources\\tools\\");
        public static readonly string Languages = Path.Combine(EnvironmentFolder, "strings\\");
        public static readonly string Out = Path.Combine(EnvironmentFolder, "out\\");

        // Extracted paths
        public static readonly string WAD = WorkingFolder + "wad\\";
        public static readonly string Manual = WorkingFolder + "manual\\";
        public static readonly string DataCCF = WorkingFolder + "data_ccf\\";
        public static readonly string MiscCCF = DataCCF + "misc_ccf\\";

        // Flash paths
        public static readonly string FlashContents = WorkingFolder + "flash\\";
        public static readonly string FlashSWF = FlashContents + "content\\menu.swf";
        public static readonly string FlashConfig = FlashContents + "config\\config.common.pcf";

        // Forwarder paths
        public static readonly string SDUSBRoot = WorkingFolder + "root\\";
        public static readonly string BIOSFiles = Path.Combine(EnvironmentFolder, "resources\\bios\\");

        // Other
        public static readonly string PatchedSuffix = "-patched";
    }
}
