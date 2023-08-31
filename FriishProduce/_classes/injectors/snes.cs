using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FriishProduce.Injectors
{
    public class SNES
    {
        public string ROM { get; set; }
        public string ROMcode { get; set; }
        public int type { get; set; }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. ROM type is automatically determined.
        /// </summary>
        public void ReplaceROM()
        {
            // Maximum ROM limit allowed: 4 MB
            if (File.ReadAllBytes(ROM).Length > 4194304)
                throw new Exception(Program.Language.Get("m018"));

            string rom = Paths.WorkingFolder_Content5 + $"{ROMcode}.rom";
            type = 0;

            // -----------------------
            // Check if raw ROM exists
            // -----------------------

            if (File.Exists(rom))
            {
                File.Copy(ROM, rom, true);

                foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                {
                    if (Path.GetExtension(file) == ".pcm" || Path.GetExtension(file) == ".var") File.WriteAllText(file, String.Empty);
                    // xxxx.pcm is the digital audio file. It is not usually needed in most cases
                }

                return;
            }

            else
            {
                // ------------------------
                // Check if LZH8 ROM exists
                // ------------------------

                rom = Paths.WorkingFolder_Content5 + $"LZH8{ROMcode}.rom";
                type = 1;

                if (File.Exists(rom))
                {
                    File.Delete(rom);

                    string pPath = Paths.WorkingFolder + "lzh8.exe";
                    File.WriteAllBytes(pPath, Properties.Resources.LZH8);
                    using (Process p = Process.Start(new ProcessStartInfo
                    {
                        FileName = pPath,
                        WorkingDirectory = Paths.WorkingFolder,
                        Arguments = $"\"{ROM}\" \"{rom}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }))
                        p.WaitForExit();
                    File.Delete(pPath);

                    // xxxx.pcm must NOT be replaced in this instance, because otherwise it will display a "Wii System Memory is damaged" error and halt
                    return;
                }

                else
                {
                    // -----------------------------
                    // Check if LZ77 ROM exists
                    // -----------------------------

                    rom = Paths.WorkingFolder_Content5 + $"LZ77{ROMcode}.rom";
                    type = 2;

                    if (File.Exists(rom))
                    {
                        string pPath = Paths.WorkingFolder + "wwcxtool.exe";
                        File.WriteAllBytes(pPath, Properties.Resources.WWCXTool);
                        using (Process p = Process.Start(new ProcessStartInfo
                        {
                            FileName = pPath,
                            WorkingDirectory = Paths.WorkingFolder,
                            Arguments = $"/cr \"{rom}\" \"{ROM}\" \"{rom}\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }))
                            p.WaitForExit();
                        File.Delete(pPath);

                        return;
                    }

                    else
                        throw new Exception(Program.Language.Get("m010"));
                }
            }
        }

        /// <summary>
        /// Sets the WAD title ID and replaces regional indicators where possible
        /// </summary>
        public string ProduceID(string WAD_ID)
        {
            StringWriter s = new StringWriter();
            s.Write(WAD_ID[0]);
            s.Write(WAD_ID[1]);
            s.Write(WAD_ID[2]);

            if (WAD_ID.EndsWith("N") || WAD_ID.EndsWith("L")) s.Write('J');
            else if (WAD_ID.EndsWith("M")) s.Write('E');
            else s.Write(WAD_ID[3]);

            return s.ToString();
        }

        /// <summary>
        /// This happens to increase the screen brightness, including in the HOME Menu, so it remains hidden
        /// </summary>
        /// <param name="c1">Content1 file</param>
        public void RemoveFilter(string c1)
        {
            byte[] content1 = File.ReadAllBytes(c1);
            int index = Bytes.Search(content1, "2C 05 00 00 38 80 00 53 39 60 00 00 90 09 80 00 38 00 00 54 39 80 00 00 50 8B C0 0E 99 49 80 00 50 0C C0 0E 90 69 80 00 99 49 80 00 90 E9 80 00 99 49 80 00 91 09 80 00 41 82 00 40 88 86 00 00 88 06 00 04 50 8B 06 BE 88 66 00 01 50 0C 06 BE 88 A6 00 02 50 6B 35 32 88 66 00 05 88 86 00 03 50 AB 63 A6 88 06 00 06 50 6C 35 32 50 8B 92 1A 50 0C 63 A6 48 00 00 20");
            if (index == -1) throw new Exception(Program.Language.Get("m020"));

            string antiFilter = "38 80 00 AA 38 00 00 EE 50 8B 06 BE 38 60 00 BB 50 0C 06 BE 38 A0 00 CC 50 6B 35 32 38 60 00 FF 38 80 00 DD 50 AB 63 A6 38 00 00 GG 50 6C 35 32 50 8B 92 1A 50 0C 63 A6 48 00 00 20";
                   antiFilter = "38 80 00 00 38 00 00 15 50 8B 06 BE 38 60 00 00 50 0C 06 BE 38 A0 00 15 50 6B 35 32 38 60 00 00 38 80 00 30 50 AB 63 A6 38 00 00 00 50 6C 35 32 50 8B 92 1A 50 0C 63 A6 48 00 00 20";
            var pArray = antiFilter.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            pBytes.CopyTo(content1, index + 60);
            File.WriteAllBytes(c1, content1);
        }

        internal void InsertSaveTitle(string[] lines)
        {
            string content1_file = Global.DetermineContent1(type == 2);
            byte[] content1 = File.ReadAllBytes(content1_file);
            int ROMcode_index = 0;

            for (int i = 0; i < content1.Length; i++)
                if (content1[i] == Convert.ToByte(ROMcode[0])
                 && content1[i + 1] == Convert.ToByte(ROMcode[1])
                 && content1[i + 2] == Convert.ToByte(ROMcode[2])
                 && content1[i + 3] == Convert.ToByte(ROMcode[3])
                 && content1[i + 4] == 0x00
                 && content1[i + 5] == 0x00
                 && content1[i + 6] == 0x00
                 && content1[i + 7] == 0x00
                 && content1[i + 8] == 0x00
                 && content1[i + 9] == 0x00
                 && content1[i + 10] == 0x00
                 && content1[i + 11] == 0x00)
                {
                    ROMcode_index = i;
                    break;
                }

            if (ROMcode_index == 0)
                goto End;

            // Text addition format: Shift-JIS
            var line1 = Encoding.GetEncoding(932).GetBytes(lines[0]);
            var line2 = lines.Length == 2 ? Encoding.GetEncoding(932).GetBytes(lines[1]) : new byte[] { 0x00 };
            var title = new byte[80];
            line1.CopyTo(title, 0);
            line2.CopyTo(title, 40);
            title.CopyTo(content1, ROMcode_index + 64);
            File.WriteAllBytes(content1_file, content1);

        End:
            Global.PrepareContent1(content1_file);
        }
    }
}
