// using Config.Net;

namespace FriishProduce
{
    public class Settings
    {
        public Settings()
        {
        }

        public struct App
        {
            string Language { get; set; }
            bool AutoUpdate { get; set; }
            bool DoNotShow_000 { get; set; }
            bool DoNotShow_001 { get; set; }
            bool AutoGameScan { get; set; }
            bool AutoFillSaveData { get; set; }
            bool EnableUseOnlineWAD { get; set; }
            bool BypassROMSize { get; set; }
            int Image_Interpolation { get; set; }
            bool Image_FitAspectRatio { get; set; }
            string Default_TargetProjectFilename { get; set; }
            string Default_TargetExportFilename { get; set; }
            int Default_BannerRegion { get; set; }
            int Default_InjectionMethod_NES { get; set; }
            int Default_InjectionMethod_SNES { get; set; }
            int Default_InjectionMethod_N64 { get; set; }
            int Default_InjectionMethod_SEGA { get; set; }
        }

        public interface Paths
        {
            string Database { get; set; }
            string BIOS_NEO { get; set; }
            string BIOS_PSX { get; set; }
            string BIOS_GB { get; set; }
            string BIOS_GBC { get; set; }
            string BIOS_GBA { get; set; }
        }
    }
}
