using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libWiiSharp;

namespace FriishProduce
{
    public class InjectorSNES : InjectorBase
    {
        private string ID { get; set; }
        private string Target { get; set; }

        public InjectorSNES(WAD w) : base(w)
        {
            UsesContent1 = true;
            UsesContent5 = true;
            Load();

            GetID();
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. (compressed formats not supported yet)
        /// </summary>
        public void ReplaceROM(string ROM)
        {
            // -----------------------
            // Check if raw ROM exists
            // -----------------------
            if (!File.Exists(ROM))
                throw new FileNotFoundException(new FileNotFoundException().Message, ROM);

            // -----------------------
            // Check filesize
            // Maximum ROM limit allowed: 4 MB
            // -----------------------
            if (File.ReadAllBytes(ROM).Length > 4194304)
                throw new Exception(string.Format(Language.Get("Error003"), "4", Language.Get("Abbreviation_Megabytes")));

            // -----------------------
            // Replace original ROM
            // -----------------------

            // Normal
            // ****************
            if (Target.Length == 8) Content5.ReplaceFile(Content5.GetNodeIndex(Target), ROM);

            // LZ77 compression
            // ****************
            else if (Target.ToUpper().StartsWith("LZ77"))
            {
                File.Copy(ROM, Paths.WorkingFolder + "rom");
                File.WriteAllBytes(Paths.WorkingFolder + "LZ77orig.rom", Content5.Data[Content5.GetNodeIndex(Target)]);

                Process.Run
                (
                    Paths.Tools + "wwcxtool.exe",
                    Paths.WorkingFolder,
                    "/cr LZ77orig.rom rom LZ77out.rom"
                );
                if (!File.Exists(Paths.WorkingFolder + "LZ77out.rom")) throw new Exception(Language.Get("Error002"));

                Content5.ReplaceFile(Content5.GetNodeIndex(Target), Paths.WorkingFolder + "LZ77out.rom");

                File.Delete(Paths.WorkingFolder + "LZ77out.rom");
                if (File.Exists(Paths.WorkingFolder + "rom")) File.Delete(Paths.WorkingFolder + "rom");
                if (File.Exists(Paths.WorkingFolder + "LZ77orig.rom")) File.Delete(Paths.WorkingFolder + "LZ77orig.rom");
            }

            // Dummify unused files
            // ****************
            foreach (string file in Content5.StringTable)
            {
                if (file.ToLower().EndsWith(".pcm") || file.ToLower().EndsWith(".var"))
                    Content5.ReplaceFile(Content5.GetNodeIndex(file), Byte.Dummy);
                // xxxx.pcm is the digital audio file. It is not usually needed in most cases
            }
        }

        public void InsertSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // TEXT
            // -----------------------

            List<string> convertedLines = new List<string>();
            foreach (var line in lines)
                if (!string.IsNullOrWhiteSpace(line)) convertedLines.Add(line.Replace("\r\n", "\n"));
            lines = convertedLines.ToArray();

            List<int> ID_indexes = new List<int>();
            for (int i = 0; i < Content1.Length; i++)
                if (Content1[i] == Convert.ToByte(ID[0])
                 && Content1[i + 1] == Convert.ToByte(ID[1])
                 && Content1[i + 2] == Convert.ToByte(ID[2])
                 && Content1[i + 3] == Convert.ToByte(ID[3])
                 && Content1[i + 4] == 0x00
                 && Content1[i + 5] == 0x00
                 && Content1[i + 6] == 0x00
                 && Content1[i + 7] == 0x00
                 && Content1[i + 8] == 0x00
                 && Content1[i + 9] == 0x00
                 && Content1[i + 10] == 0x00
                 && Content1[i + 11] == 0x00)
                    ID_indexes.Add(i);

            if (ID_indexes.Count > 0)
            {
                // Text addition format: Big Endian (Korea region) / Shift-JIS (others)
                // ****************
                var encoder = WAD.Region == Region.Korea ? Encoding.BigEndianUnicode : Encoding.GetEncoding(932);

                foreach (var index in ID_indexes)
                {
                    var title = new byte[80];

                    var line1 = encoder.GetBytes(lines[0]);
                    var line2 = lines.Length == 2 ? encoder.GetBytes(lines[1]) : new byte[] { 0x00 };

                    for (int i = 0; i < 40; i++)
                        try { title[i] = line1[i]; } catch { title[i] = 0x00; }

                    for (int i = 0; i < 40; i++)
                        try { title[i + 40] = line2[i]; } catch { title[i + 40] = 0x00; }

                    title.CopyTo(Content1, index + 64);
                }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            Content5.ReplaceFile(Content5.GetNodeIndex("banner.tpl"), tImg.CreateSaveTPL(Console.SNES, Content5.Data[Content5.GetNodeIndex("banner.tpl")]).ToByteArray());
        }

        /// <summary>
        /// Searches for the ID used by the ROM and target pathfile of the ROM to replace
        /// </summary>
        private void GetID()
        {
            foreach (string file in Content5.StringTable)
            {
                if (file.ToLower().EndsWith(".rom"))
                {
                    Target = file;
                    ID = file.Substring(file.Length - 8, 4);
                }
            }
        }
    }
}
