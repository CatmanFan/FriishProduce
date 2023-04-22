using System;
using System.Diagnostics;
using System.IO;

namespace FriishProduce
{
    public class Injector
    {
        public static string ApplyPatch(string ROM, string patch = null)
        {
            if (patch != null)
            {
                string outROM = ROM + Paths.PatchedSuffix;
                string batchScript = $"\"{Paths.Apps}flips.exe\" --apply \"{patch}\" \"{Path.GetFileName(ROM)}\" \"{Path.GetFileName(outROM)}\"";
                string batchPath = Path.Combine(Path.GetDirectoryName(ROM), "patch.bat");

                File.WriteAllText(batchPath, batchScript);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = batchPath,
                    WorkingDirectory = Path.GetDirectoryName(ROM),
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(batchPath);

                if (!File.Exists(outROM)) throw new Exception(strings.error_patchFailed);
                return outROM;
            }
            else return ROM;
        }

        public static string DetermineContent1()
        {
            // Check for LZ77 compression
            if (File.ReadAllBytes(Paths.WorkingFolder + "00000001.app").Length < 1024)
            {
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = $"{Paths.Apps}wwcxtool.exe",
                    Arguments = $"/u \"{Paths.WorkingFolder}00000001.app\" \"{Paths.WorkingFolder}00000001.app.dec\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();

                if (!File.Exists(Paths.WorkingFolder + "00000001.app.dec"))
                    throw new Exception("WWCXTool failed to decompress the .dol base.");
                return Paths.WorkingFolder + "00000001.app.dec";
            }

            return Paths.WorkingFolder + "00000001.app";
        }

        public static void PrepareContent1(string file)
        {
            if (file.EndsWith(".dec"))
            {
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = $"{Paths.Apps}wwcxtool.exe",
                    Arguments = $"/cr \"{Paths.WorkingFolder}00000001.app\" \"{Paths.WorkingFolder}00000001.app.dec\" \"{Paths.WorkingFolder}00000001.app.rec\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();

                if (!File.Exists(Paths.WorkingFolder + "00000001.app.rec"))
                    throw new Exception("WWCXTool failed to recompress the .dol base.");

                if (File.Exists(Paths.WorkingFolder + "00000001.app")) File.Delete(Paths.WorkingFolder + "00000001.app");
                if (File.Exists(Paths.WorkingFolder + "00000001.app.dec")) File.Delete(Paths.WorkingFolder + "00000001.app.dec");
                File.Move(Paths.WorkingFolder + "00000001.app.rec", Paths.WorkingFolder + "00000001.app");
            }
        }

        public static void RemoveEmanual()
        {
            U8.Unpack(Paths.WorkingFolder + "00000004.app", Paths.WorkingFolder_Content4);
            if (Directory.Exists(Paths.WorkingFolder_Content4 + "HomeButton2") && Directory.Exists(Paths.WorkingFolder_Content4 + "HomeButton3"))
            {
                foreach (var file in Directory.GetFiles(Paths.WorkingFolder_Content4 + "HomeButton3"))
                    File.Copy($"{Paths.WorkingFolder_Content4}HomeButton2\\{Path.GetFileName(file)}", file, true);
            }
            U8.Pack(Paths.WorkingFolder_Content4, Paths.WorkingFolder + "00000004.app");

            string[] emanualFiles =
            {
                "Opera.arc",
                "Opera.ccf",
                "Opera.ccf.zlib",
                "emanual.arc",
                "man.arc",
                "manc.arc"
            };

            foreach (var file in Directory.GetFiles(Paths.WorkingFolder_Content5, "*.*", SearchOption.AllDirectories))
            {
                foreach (var item in emanualFiles)
                    if (file.EndsWith(item)) File.WriteAllText(file, string.Empty);
                // It is important to empty the file instead of simply deleting it, because otherwise it will throw a "data corruption" error in some WADs
            }
        }
    }
}
