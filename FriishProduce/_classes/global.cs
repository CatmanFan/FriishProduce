using System;
using System.Diagnostics;
using System.IO;

namespace FriishProduce
{
    partial class Global
    {
        private static readonly Lang x = Program.Language;

        public static string ApplyPatch(string ROM, string patch = null)
        {
            if (patch != null)
            {
                if (!File.Exists(Paths.Apps + "flips\\flips.exe"))
                {
                    MessageBox.Show(x.Get("m006"), x.Get("error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return ROM;
                }

                string outROM = ROM + Paths.PatchedSuffix;
                string batchScript = $"\"{Paths.Apps}flips\\flips.exe\" --apply \"{patch}\" \"{Path.GetFileName(ROM)}\" \"{Path.GetFileName(outROM)}\"";
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

                if (!File.Exists(outROM) || File.Exists(outROM) && (File.ReadAllBytes(outROM).Length == File.ReadAllBytes(ROM).Length))
                {
                    if (File.Exists(outROM)) File.Delete(outROM);
                    MessageBox.Show(x.Get("m005"), x.Get("error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return ROM;
                }
                return outROM;
            }
            else return ROM;
        }

        public static string DetermineContent1()
        {
            // Check for LZSS compression
            long filesize = new FileInfo(Paths.WorkingFolder + "00000001.app").Length / 1024 / 1024;
            if (filesize == 0)
            {
                // Run process
                string pPath = Paths.WorkingFolder + "lzss.exe";
                File.WriteAllBytes(pPath, Properties.Resources.LZSS);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = Paths.WorkingFolder,
                    Arguments = $"-d 00000001.app",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(pPath);

                // Failsafe
                if (new FileInfo(Paths.WorkingFolder + "00000001.app").Length / 1024 / 1024 == filesize)
                    throw new Exception(x.Get("m009"));

                // Return decompressed file path
                File.Move(Paths.WorkingFolder + "00000001.app", Paths.WorkingFolder + "00000001.app.raw");
                return Paths.WorkingFolder + "00000001.app.raw";
            }

            // There is nothing to do
            return Paths.WorkingFolder + "00000001.app";
        }

        public static void PrepareContent1(string path)
        {
            if (path == Paths.WorkingFolder + "00000001.app.raw")
            {
                File.Move(Paths.WorkingFolder + "00000001.app.raw", Paths.WorkingFolder + "00000001.app");

                var filesize = new FileInfo(Paths.WorkingFolder + "00000001.app").Length / 1024 / 1024;

                // Run process
                string pPath = Paths.WorkingFolder + "lzss.exe";
                File.WriteAllBytes(pPath, Properties.Resources.LZSS);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = Paths.WorkingFolder,
                    Arguments = $"-evn 00000001.app",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(pPath);

                // Check for compressed filesize length
                if (new FileInfo(Paths.WorkingFolder + "00000001.app").Length / 1024 / 1024 == filesize)
                    throw new Exception(x.Get("m009"));
            }
        }

        public static void ChangeVideoMode(int mode)
        {
            string content1_file = DetermineContent1();
            var content1 = File.ReadAllBytes(content1_file);

            int index;
            // List of byte patterns and corresponding video modes:
            // NTSC (interlaced)   / 480i = 00 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // NTSC (non interlaced)      = 01 02 80 01 E0 01 E0 00 28 00 0B 02 80 01 E0
            // NTSC (progressive)  / 480p = 02 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // MPAL (interlaced)          = 08 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // MPAL (non interlaced)      = 09 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // MPAL (progressive)         = 0A 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // PAL60 (interlaced)  / 480i = 14 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // PAL60 (non interlaced)     = 15 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // PAL60 (progressive) / 480p = 16 02 80 01 E0 01 E0 00 28 00 00 02 80 01 E0
            // PAL: replace 01 at end with 02
            // PAL (interlaced)    / 576i = 04 02 80 02 10 02 10 00 28 00 17 02 80 02 10
            // PAL (non interlaced)       = 05 02 80 01 08 01 08 00 28 00 0B 02 80 02 10
            // PAL (progressive)          = 06 02 80 02 10 02 10 00 28 00 17 02 80 02 10
            //                                                            ^
            // ONLY PAL50 is different in byte composition !     //  This byte seems to vary
            // PAL (progressive/alt)          = 06 02 80 01 08 02 0C 00 28 00 17 02 80 02 0C

            int start = 0x14F000;
            int end = 0x1FFFFF;

            // 0-2: NTSC/PAL
            // 3-5: MPAL/PAL
            // 6: Backup for PAL (progressive)
            System.Collections.Generic.Dictionary<int, string> list = new System.Collections.Generic.Dictionary<int, string>
            {
                // NTSC
                { 0, "00 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                { 1, "01 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                { 2, "02 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                // MPAL
                { 3, "08 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                { 4, "09 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                { 5, "0A 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                // PAL60
                { 6, "14 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                { 7, "15 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                { 8, "16 02 80 01 E0 01 E0 00 28 00 xx 02 80 01 E0" },
                // PAL50
                { 9, "04 02 80 02 10 02 10 00 28 00 xx 02 80 02 10" },
                { 10, "05 02 80 01 08 01 08 00 28 00 xx 02 80 02 10" },
                { 11, "06 02 80 02 10 02 10 00 28 00 xx 02 80 02 10" },
            };

            // 0 = NTSC
            // 1 = PAL50
            // 2 = PAL60
            // 3 = NTSC/PAL60
            // 4 = NTSC/MPAL
            // 5 = PAL60/50
            if (mode >= 4)
            {
                for (int i = 6; i < list.Count; i++)
                {
                    while (Bytes.Search(content1, list[mode == 5 ? i - 6 : i].Substring(0, 23), start, end) != -1)
                    {
                        index = Bytes.Search(content1, list[mode == 5 ? i - 6 : i].Substring(0, 23), start, end);
                        var Array = list[mode == 5 ? i : i - 6].Split(' ');
                        for (int x = 0; x < 15; x++)
                            if (Array[x].ToLower() != "xx") content1[index + x] = Convert.ToByte(Array[x], 16);
                        break;
                    }
                }
            }
            else if (mode == 3)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i >= 9 || (i >= 3 && i <= 5))
                    {
                        while (Bytes.Search(content1, list[i].Substring(0, 23), start, end) != -1)
                        {
                            index = Bytes.Search(content1, list[i].Substring(0, 23), start, end);
                            var Array = i == 9 ? list[6].Split(' ') : i == 10 ? list[7].Split(' ') : i == 11 ? list[8].Split(' ') :
                                i == 3 ? list[0].Split(' ') : i == 4 ? list[1].Split(' ') : list[2].Split(' ');
                            for (int x = 0; x < 15; x++)
                                if (Array[x].ToLower() != "xx") content1[index + x] = Convert.ToByte(Array[x], 16);
                            break;
                        }
                    }
                }
            }
            else
            {
                int mode_offset = mode == 1 ? 9 : mode == 2 ? 6 : 0;
                for (int i = 0; i < list.Count; i++)
                {
                    while (Bytes.Search(content1, list[i].Substring(0, 23), start, end) != -1)
                    {
                        index = Bytes.Search(content1, list[i].Substring(0, 23), start, end);
                        var Array = i == 0 || i == 3 || i == 6 || i == 9 ? list[mode_offset + 0].Split(' ') :
                            i == 1 || i == 4 || i == 7 || i == 10 ? list[mode_offset + 1].Split(' ') :
                            list[mode_offset + 2].Split(' ');
                        for (int x = 0; x < 15; x++)
                            if (Array[x].ToLower() != "xx") content1[index + x] = Convert.ToByte(Array[x], 16);
                        break;
                    }
                }
            }

            File.WriteAllBytes(content1_file, content1);
            PrepareContent1(content1_file);
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
                "LZH8emanual.arc",
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

            if (Directory.Exists(Paths.WorkingFolder_Content6))
            {
                foreach (var file in Directory.GetFiles(Paths.WorkingFolder_Content6, "*.*", SearchOption.AllDirectories))
                {
                    foreach (var item in emanualFiles)
                        if (file.EndsWith(item)) File.WriteAllBytes(file, new byte[16]);
                }
            }
        }
    }
}
