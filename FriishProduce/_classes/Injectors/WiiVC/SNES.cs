using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libWiiSharp;

namespace FriishProduce.WiiVC
{
    public class SNES : InjectorWiiVC
    {
        private string ID { get; set; }
        private string Target { get; set; }

        protected override void Load()
        {
            // -----------------------
            // Maximum ROM limit allowed: 4 MB
            // -----------------------

            NeedsMainDOL = true;
            MainContentIndex = 5;
            NeedsManualLoaded = true;

            base.Load();

            GetID();
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. (compressed formats not supported yet)
        /// </summary>
        protected override void ReplaceROM()
        {
            ROM.CheckSize();

            // -----------------------
            // Replace original ROM
            // -----------------------

            // Normal
            // ****************
            if (Target.Length == 8) MainContent.ReplaceFile(MainContent.GetNodeIndex(Target), ROM.Bytes);

            // LZ77 compression
            // ****************
            else if (Target.ToUpper().StartsWith("LZ77"))
            {
                File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);
                File.WriteAllBytes(Paths.WorkingFolder + "LZ77orig.rom", MainContent.Data[MainContent.GetNodeIndex(Target)]);

                ProcessHelper.Run
                (
                    Paths.Tools + "wwcxtool.exe",
                    Paths.WorkingFolder,
                    "/cr LZ77orig.rom rom LZ77out.rom"
                );
                if (!File.Exists(Paths.WorkingFolder + "LZ77out.rom")) throw new Exception(Language.Get("Error002"));

                MainContent.ReplaceFile(MainContent.GetNodeIndex(Target), Paths.WorkingFolder + "LZ77out.rom");

                File.Delete(Paths.WorkingFolder + "LZ77out.rom");
                if (File.Exists(Paths.WorkingFolder + "rom")) File.Delete(Paths.WorkingFolder + "rom");
                if (File.Exists(Paths.WorkingFolder + "LZ77orig.rom")) File.Delete(Paths.WorkingFolder + "LZ77orig.rom");
            }

            // Dummify unused files
            // ****************
            foreach (string file in MainContent.StringTable)
            {
                if (file.ToLower().EndsWith(".pcm") || file.ToLower().EndsWith(".var"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(file), Byte.Dummy);
                // xxxx.pcm is the digital audio file. It is not usually needed in most cases
            }
        }

        protected override void ReplaceSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // TEXT
            // -----------------------

            // Search for entry points
            // ****************
            List<int> ID_indexes = new List<int>();
            for (int i = 0; i < Contents[1].Length; i++)
                if (Contents[1][i] == Convert.ToByte(ID[0])
                 && Contents[1][i + 1] == Convert.ToByte(ID[1])
                 && Contents[1][i + 2] == Convert.ToByte(ID[2])
                 && Contents[1][i + 3] == Convert.ToByte(ID[3])
                 && Contents[1][i + 4] == 0x00
                 && Contents[1][i + 5] == 0x00
                 && Contents[1][i + 6] == 0x00
                 && Contents[1][i + 7] == 0x00
                 && Contents[1][i + 8] == 0x00
                 && Contents[1][i + 9] == 0x00
                 && Contents[1][i + 10] == 0x00
                 && Contents[1][i + 11] == 0x00)
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

                    title.CopyTo(Contents[1], index + 64);
                }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            MainContent.ReplaceFile(MainContent.GetNodeIndex("banner.tpl"), tImg.CreateSaveTPL(Console.SNES, MainContent.Data[MainContent.GetNodeIndex("banner.tpl")]).ToByteArray());
        }

        /// <summary>
        /// Searches for the ID used by the ROM and target pathfile of the ROM to replace
        /// </summary>
        private void GetID()
        {
            foreach (string file in MainContent.StringTable)
            {
                if (file.ToLower().EndsWith(".rom"))
                {
                    Target = file;
                    ID = file.Substring(file.Length - 8, 4);
                }
            }
        }

        protected override void ModifyEmulatorSettings()
        {
            // Not exists
            return;
        }
    }
}
