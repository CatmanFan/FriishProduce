using System;
using System.Diagnostics;
using System.IO;

namespace FriishProduce
{
    partial class Injector
    {
        private static Localization lang = Program.lang;
        public static string ApplyPatch(string ROM, string patch = null)
        {
            if (patch != null)
            {
                if (!File.Exists(Paths.Apps + "flips.exe"))
                {
                    System.Windows.Forms.MessageBox.Show(lang.Get("Error_FlipsMissing"), lang.Get("Error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return ROM;
                }

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

                if (!File.Exists(outROM))
                {
                    System.Windows.Forms.MessageBox.Show(lang.Get("Error_PatchFailed"), lang.Get("Error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return ROM;
                }
                return outROM;
            }
            else return ROM;
        }

        public static string DetermineContent1()
        {
            // Check for LZ77 compression
            if (File.ReadAllBytes(Paths.WorkingFolder + "00000001.app").Length < 1000)
            {
                if (!File.Exists(Paths.WorkingFolder + "00000001.app.dec"))
                {
                    string pPath = Paths.WorkingFolder + "wwcxtool.exe";
                    File.WriteAllBytes(pPath, Properties.Resources.WWCXTool);
                    using (Process p = Process.Start(new ProcessStartInfo
                    {
                        FileName = pPath,
                        WorkingDirectory = Paths.WorkingFolder,
                        Arguments = $"/u 00000001.app 00000001.app.dec",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }))
                        p.WaitForExit();
                    File.Delete(pPath);

                    if (!File.Exists(Paths.WorkingFolder + "00000001.app.dec"))
                        throw new Exception(lang.Get("Error_EmulatorCompression"));
                }

                return Paths.WorkingFolder + "00000001.app.dec";
            }

            return Paths.WorkingFolder + "00000001.app";
        }

        public static void PrepareContent1()
        {
            if (File.Exists(Paths.WorkingFolder + "00000001.app.dec"))
            {
                string pPath = Paths.WorkingFolder + "wwcxtool.exe";
                File.WriteAllBytes(pPath, Properties.Resources.WWCXTool);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = Paths.WorkingFolder,
                    Arguments = $"/cr 00000001.app 00000001.app.dec 00000001.app.rec",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(pPath);

                if (!File.Exists(Paths.WorkingFolder + "00000001.app.rec"))
                    throw new Exception(lang.Get("Error_EmulatorCompression"));

                if (File.Exists(Paths.WorkingFolder + "00000001.app")) File.Delete(Paths.WorkingFolder + "00000001.app");
                if (File.Exists(Paths.WorkingFolder + "00000001.app.dec")) File.Delete(Paths.WorkingFolder + "00000001.app.dec");
                File.Move(Paths.WorkingFolder + "00000001.app.rec", Paths.WorkingFolder + "00000001.app");
            }
        }
        public static void ChangeVideoMode()
        {
            string content1_file = Injector.DetermineContent1();
            var content1 = File.ReadAllBytes(content1_file);


            File.WriteAllBytes(content1_file, content1);
            Injector.PrepareContent1();
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
                "manc.arc",
                "html.arc",
                "htmlc.arc"
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
