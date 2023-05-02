using System;
using System.Diagnostics;
using System.IO;

namespace FriishProduce
{
    partial class Global
    {
        private static Lang x = Program.Language;
        public static string ApplyPatch(string ROM, string patch = null)
        {
            if (patch != null)
            {
                if (!File.Exists(Paths.Apps + "flips.exe"))
                {
                    MessageBox.Show(x.Get("m006"), x.Get("error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                    MessageBox.Show(x.Get("m005"), x.Get("error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                        throw new Exception(x.Get("m009"));
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
                    throw new Exception(x.Get("m009"));

                if (File.Exists(Paths.WorkingFolder + "00000001.app")) File.Delete(Paths.WorkingFolder + "00000001.app");
                if (File.Exists(Paths.WorkingFolder + "00000001.app.dec")) File.Delete(Paths.WorkingFolder + "00000001.app.dec");
                File.Move(Paths.WorkingFolder + "00000001.app.rec", Paths.WorkingFolder + "00000001.app");
            }
        }
        public static void ChangeVideoMode()
        {
            string content1_file = Global.DetermineContent1();
            var content1 = File.ReadAllBytes(content1_file);


            File.WriteAllBytes(content1_file, content1);
            Global.PrepareContent1();
        }

        public static void RemoveEmanual()
        {
            // There is a bug in Korean NES WADs (tested Kirby's Adventure only) where the following error is produced:
            // "Wii 본체 저장 메모리가 소삼되머 다. 자세한 내용은 Wii 본체 사용설명서를 읽어 주십시오."
            // This is related to the "Opera.arc" failing to be detected when it is overwritten

            U8.Unpack(Paths.WorkingFolder + "00000004.app", Paths.WorkingFolder_Content4);
            if (Directory.Exists(Paths.WorkingFolder_Content4 + "HomeButton2"))
            {
                string targetDir = Paths.WorkingFolder_Content4 + "HomeButton3";
                if (Directory.Exists(Paths.WorkingFolder_Content4 + "Homebutton3"))
                    targetDir = Paths.WorkingFolder_Content4 + "Homebutton3";

                foreach (var file in Directory.GetFiles(targetDir))
                    File.Copy($"{Paths.WorkingFolder_Content4}HomeButton2\\{Path.GetFileName(file)}", file, true);
            }
            U8.Pack(Paths.WorkingFolder_Content4, Paths.WorkingFolder + "00000004.app");

            string[] emanualFiles =
            {
                "Opera.arc.zlib",
                "emanual.arc",
                "man.arc",
                "man.arc.zlib",
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
