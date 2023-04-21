using System;
using System.Diagnostics;
using System.IO;
using libWiiSharp;

namespace FriishProduce.Injectors
{
    public class N64
    {
        public string ROM { get; set; }
        public string emuVersion { get; set; }

        private readonly string byteswappedROM = $"{Paths.Apps}ucon64\\rom.z64";
        private readonly string compressedROM = $"{Paths.Apps}ucon64\\romc";

        public void ReplaceROM()
        {
            if (File.ReadAllBytes(ROM).Length % 4194304 != 0 && emuVersion.Contains("romc"))
                throw new Exception("The ROM is not a multiple of 4 MB (4194304 bytes).");

            File.Copy(ROM, $"{Paths.Apps}ucon64\\rom");
            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = $"{Paths.Apps}ucon64\\ucon64.exe",
                WorkingDirectory = $"{Paths.Apps}ucon64\\",
                Arguments = $"--z64 \"{Paths.Apps}ucon64\\rom\" \"{byteswappedROM}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();
            File.Delete($"{Paths.Apps}ucon64\\rom");
            if (File.Exists($"{Paths.Apps}ucon64\\rom.bak")) File.Delete($"{Paths.Apps}ucon64\\rom.bak");

            if (!File.Exists(byteswappedROM))
                throw new Exception("The ROM byteswapping process failed.");

            if (emuVersion.Contains("romc"))
            {
                if (emuVersion.Contains("hero"))
                {
                    // Set title ID to NBDx to prevent crashing
                    var ROMbytes = File.ReadAllBytes(byteswappedROM);
                    ROMbytes[0x3B] = 0x4E;
                    ROMbytes[0x3C] = 0x42;
                    ROMbytes[0x3D] = 0x44;
                    File.WriteAllBytes(byteswappedROM, ROMbytes);
                }

                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = $"{Paths.Apps}romc.exe",
                    WorkingDirectory = Paths.Apps,
                    Arguments = $"e ucon64\\rom.z64 ucon64\\romc",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(byteswappedROM);

                if (!File.Exists(compressedROM))
                    throw new Exception("The ROMC compression process failed.");

                // Check filesize
                /* if (File.ReadAllBytes(compressedROM).Length > File.ReadAllBytes($"{Paths.WorkingFolder_Content5}romc").Length)
                {
                    File.Delete(compressedROM);
                    throw new Exception("The ROM to be injected is larger in filesize than the target ROM.");
                } */

                // Copy
                File.Copy(compressedROM, $"{Paths.WorkingFolder_Content5}romc", true);
                File.Delete(compressedROM);
            }
            else
            {
                // Check filesize
                if (File.ReadAllBytes(byteswappedROM).Length > File.ReadAllBytes($"{Paths.WorkingFolder_Content5}rom").Length)
                {
                    File.Delete(byteswappedROM);
                    throw new Exception("The ROM to be injected is larger in filesize than the target ROM.");
                }

                // Copy
                File.Copy(byteswappedROM, $"{Paths.WorkingFolder_Content5}rom", true);
                File.Delete(byteswappedROM);
            }
        }
    }
}