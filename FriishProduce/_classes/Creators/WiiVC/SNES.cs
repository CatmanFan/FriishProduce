using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FriishProduce.Injectors
{
    public class SNES : InjectorWiiVC
    {
        private string ID { get; set; }
        private string target { get; set; }

        protected override void Load()
        {
            needsMainDol = true;
            mainContentIndex = 5;
            needsManualLoaded = true;
            SaveTextEncoding = WAD.Region == Region.Korea ? Encoding.BigEndianUnicode : Encoding.GetEncoding(932); // Shift-JIS

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
            #region -- LZ77 compression --
            if (target.ToUpper().StartsWith("LZ77"))
            {
                File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                Utils.Run
                (
                    FileDatas.Apps.gbalzss,
                    "gbalzss",
                    "e rom lz77.rom"
                );
                if (!File.Exists(Paths.WorkingFolder + "lz77.rom")) throw new Exception(Program.Lang.Msg(2, true));

                MainContent.ReplaceFile(MainContent.GetNodeIndex(target), Paths.WorkingFolder + "lz77.rom");
            }
            #endregion

            #region -- LZH8 compression --
            else if (target.ToUpper().StartsWith("LZH8"))
            {
                File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                Utils.Run
                (
                    FileDatas.Apps.lzh8_cmp_nonstrict,
                    "lzh8",
                    "rom lzh8.rom"
                );
                if (!File.Exists(Paths.WorkingFolder + "lzh8.rom")) throw new Exception(Program.Lang.Msg(2, true));

                MainContent.ReplaceFile(MainContent.GetNodeIndex(target), Paths.WorkingFolder + "lzh8.rom");
            }
            #endregion

            #region -- Normal --
            else if (target.Length == 8)
            {
                MainContent.ReplaceFile(MainContent.GetNodeIndex(target), ROM.Bytes);
            }
            #endregion

            else
            {
                throw new Exception(Program.Lang.Msg(12, true));
            }

            // Delete temporary files
            // ****************
            foreach (var file in new string[] { Paths.WorkingFolder + "rom", Paths.WorkingFolder + "lz77.rom", Paths.WorkingFolder + "lzh8.rom" })
                if (File.Exists(file)) File.Delete(file);

            // Dummify unused files in emulator
            // ****************
            foreach (string file in MainContent.StringTable)
            {
                if (file.ToLower().EndsWith(".pcm") || file.ToLower().EndsWith(".var"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(file), Byte.Dummy);
                // xxxx.pcm is the digital audio file. It is not usually needed in most cases
            }
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            // -----------------------
            // TEXT
            // -----------------------

            lines = ConvertSaveText(lines);

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
                foreach (var index in ID_indexes)
                {
                    var title = new byte[80];

                    var line1 = SaveTextEncoding.GetBytes(lines[0]);
                    var line2 = lines.Length == 2 ? SaveTextEncoding.GetBytes(lines[1]) : new byte[] { 0x00 };

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

            MainContent.ReplaceFile(MainContent.GetNodeIndex("banner.tpl"), Img.CreateSaveTPL(MainContent.Data[MainContent.GetNodeIndex("banner.tpl")]).ToByteArray());
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
                    target = file;
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
