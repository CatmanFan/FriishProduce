using System;
using System.IO;

namespace FriishProduce.Forwarders
{
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
