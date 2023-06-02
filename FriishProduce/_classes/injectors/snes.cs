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
        public string type { get; set; }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. ROM type is automatically determined.
        /// </summary>
        public void ReplaceROM()
        {
            // Maximum ROM limit allowed: 4 MB
            if (File.ReadAllBytes(ROM).Length > 4194304)
                throw new Exception(Program.Language.Get("m018"));

            string rom = Paths.WorkingFolder_Content5 + $"{ROMcode}.rom";

            if (File.Exists(rom))
            {
                File.Copy(ROM, rom, true);

                foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                {
                    if (Path.GetExtension(file) == ".pcm" || Path.GetExtension(file) == ".var") File.WriteAllText(file, String.Empty);
                    // xxxx.pcm is the digital audio file. It is not usually needed in most cases
                }
            }
            else
            {
                rom = Paths.WorkingFolder_Content5 + $"LZH8{ROMcode}.rom";

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
                }
                else
                    throw new Exception(Program.Language.Get("m010"));
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

        internal void InsertSaveTitle(string[] lines)
        {
            string content1_file = Global.DetermineContent1();
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
