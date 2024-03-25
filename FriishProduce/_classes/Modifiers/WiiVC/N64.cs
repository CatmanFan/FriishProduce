using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.WiiVC
{
    public class N64 : InjectorWiiVC
    {
        public enum Type
        {
            Rev1 = 0,       // F-Zero X, Super Mario 64

            Rev1_Alt = 1,   // Star Fox 64, Mario Kart 64, Zelda: Ocarina

            Rev2 = 2,       // Pokémon Snap, Sin & Punishment, Yoshi's Story

            Rev3 = 3        // Bomberman Hero, Custom Robo V2, Mario Golf, Mario Party 2, Ogre Battle 64, Paper Mario + KOREA: Star Fox 64, Mario Kart 64
                            // This is the one which uses the ROMC compressed format
        }

        public int CompressionType { get; set; }    // Type of ROMC compression (0 = none; 1 = ROMC type 0; 2 = ROMC type 1)
        public bool Allocate { get; set; }          // Determines whether to allocate ROM size (rev. 1 WADs only)

        protected override void Load()
        {
            NeedsMainDOL = true;
            MainContentIndex = 5;
            NeedsManualLoaded = true;
            SaveTextEncoding = Encoding.Unicode;

            base.Load();

            if (WAD.Region == Region.Korea) EmuType = 3;
            else switch (WAD.UpperTitleID.Substring(0, 3).ToUpper())
                {
                    default:
                    case "NAA":
                    case "NAF":
                        EmuType = 0;
                        break;

                    case "NAB":
                    case "NAC":
                    case "NAD":
                        EmuType = 1;
                        break;

                    case "NAK":
                    case "NAJ":
                    case "NAH":
                        EmuType = 2;
                        break;

                    case "NA3":
                    case "NAE":
                    case "NAP":
                    case "NAU":
                    case "NAY":
                    case "NAZ":
                        EmuType = 3;
                        break;
                }
        }

        /// <summary>
        /// Injection of ROM from base file
        /// </summary>
        /// <param name="ROM">Path to ROM</param>
        protected override void ReplaceROM()
        {
            // -----------------------
            // Check filesize
            // Maximum ROM limit allowed: 32 MB unless allocated in main.dol (maximum possible: ~56 MB)
            // -----------------------
            ROM.MaxSize = Allocate ? 56623104 : 33554432;
            ROM.CheckSize();

            if (MainContent.GetNodeIndex("romc") != -1) EmuType = 3;

            // -----------------------
            // Actually replace original ROM
            // -----------------------
            switch (EmuType)
            {
                default:
                    MainContent.ReplaceFile(MainContent.GetNodeIndex("rom"), ROM.Bytes);
                    break;

                case 3:
                    // Set title ID to NBDx to prevent crashing on Bomberman Hero
                    // ****************
                    if (WAD.UpperTitleID.ToUpper().StartsWith("NA3"))
                    {
                        Encoding.ASCII.GetBytes("NBD").CopyTo(ROM.Bytes, 0x3B);
                    }

                    // Temporary ROM file at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                    // Compress using ROMC type
                    // ****************
                    if (CompressionType == 1) // Type 0
                        ProcessHelper.Run
                        (
                            Paths.Tools + "romc0.exe",
                            Paths.WorkingFolder,
                            "rom romc"
                        );

                    else // Type 1
                        ProcessHelper.Run
                        (
                            Paths.Tools + "romc.exe",
                            Paths.WorkingFolder,
                            "e rom romc"
                        );

                    // Check if converted file exists
                    // ****************
                    File.Delete(Paths.WorkingFolder + "rom");
                    if (!File.Exists(Paths.WorkingFolder + "romc")) throw new Exception(Language.Get("Error.002"));

                    // Convert to bytes and replace at "romc"
                    // ****************
                    MainContent.ReplaceFile(MainContent.GetNodeIndex("romc"), File.ReadAllBytes(Paths.WorkingFolder + "romc"));
                    File.Delete(Paths.WorkingFolder + "romc");
                    break;
            }
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            // -----------------------
            // TEXT
            // -----------------------

            if (EmuType >= 2) SaveTextEncoding = Encoding.BigEndianUnicode;
            lines = ConvertSaveText(lines);

            // The separator byte array for double-lines.
            // This varies depending on the revision of the emulator, and the second separator is often cut off by end-of-file in earlier releases
            // ****************
            byte[][] separators = new byte[][]
            {
                new byte[] { 0x00, 0xBB, 0xBB, 0xBB },
                new byte[] { 0x00, 0xBB },
                new byte[] { 0x00, 0x00, 0xBB, 0xBB }
            };

            foreach (var item in MainContent.StringTable)
            {
                if (item.Contains("saveComments_"))
                {
                    var byteArray = MainContent.Data[MainContent.GetNodeIndex(item)];

                    List<byte> newSave = new List<byte>();

                    // Also varying on revision, how the second line field is itself handled.
                    // Where it is empty: In earlier revisions such as F-Zero X and Super Mario 64, it is a white space character, otherwise it is null.
                    // ****************
                    if (lines.Length == 1 && EmuType == 0) lines = new string[2] { lines[0], " " };

                    // Add 64-byte header
                    // ****************
                    // 30 69 0A FB 00 00 00 AA 00 00 00 AA 00 BB 00 BB
                    // 30 69 0A FD 00 00 00 CC 00 00 00 CC 00 DD 00 DD
                    // ****************
                    // AA = Offset beginning the first line
                    // BB = Length in bytes
                    // CC = Offset beginning the second line
                    // DD = Length in bytes
                    // ****************
                    newSave.AddRange(byteArray.Take(32));

                    var isLatin = !(byteArray[28] == 0x6A || (byteArray[28] == 0x6B && byteArray[29] == 0x72)); // Lang is not JP/KO
                    var isCJK
                       = HasChars(lines[0], 0x1100, 0x11FF) // Hangul Jamo
                      || HasChars(lines[0], 0x3040, 0x309F) // Hiragana
                      || HasChars(lines[0], 0x30A0, 0x30FF) // Katakana
                      || HasChars(lines[0], 0x3130, 0x318F) // Hangul Jamo
                      || HasChars(lines[0], 0x3300, 0x9FFF) // CJK
                      || HasChars(lines[0], 0xAC00, 0xD7AF); // Hangul Syllables

                    var line1 = SaveTextEncoding.GetBytes(lines[0]);
                    var line2 = lines.Length > 1 ? SaveTextEncoding.GetBytes(lines[1]) : new byte[0];

                    var separator1 = isCJK ? separators[1] : EmuType > 1 ? separators[2] : EmuType == 0 && isLatin ? separators[0] : separators[1];
                    var separator2 = EmuType < 2 ? (!isLatin ? separators[1] : separators[0]) : separators[2];

                    newSave.AddRange(new byte[]
                        {
                            0x30, 0x69, 0x0A, 0xFB, 0x00, 0x00, 0x00,
                            0x40,
                            0x00, 0x00, 0x00,
                            0x40,
                            0x00, Convert.ToByte(line1.Length), 0x00, Convert.ToByte(line1.Length),

                            0x30, 0x69, 0x0A, 0xFD, 0x00, 0x00, 0x00,
                            Convert.ToByte(64 + line1.Length + separator1.Length),
                            0x00, 0x00, 0x00,
                            Convert.ToByte(64 + line1.Length + separator1.Length),
                            0x00, Convert.ToByte(line2.Length), 0x00, Convert.ToByte(line2.Length)
                        });

                    // First savetext line
                    // ****************
                    newSave.AddRange(line1);
                    newSave.AddRange(separator1);

                    // Second savetext line (optional)
                    // ****************
                    if (lines.Length > 1) newSave.AddRange(line2);
                    newSave.AddRange(separator2);

                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), newSave.ToArray());
                }

                // -----------------------
                // IMAGE
                // -----------------------

                else if (item.ToLower().Contains("banner.tpl"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), Img.CreateSaveTPL(MainContent.Data[MainContent.GetNodeIndex(item)]).ToByteArray());
            }
        }

        private bool HasChars(string text, int min, int max) => text.Where(e => e >= min && e <= max).Any();

        // *****************************************************************************************************
        #region SETTINGS
        protected override void ModifyEmulatorSettings()
        {
            List<string> failed = new List<string>();

            try
            {
                if (SettingParse(0))
                {
                    if (!ShadingFix()) failed.Add(Language.Get("n64000.Text", typeof(Options_VC_N64).Name, true));
                }

                if (SettingParse(1) && (EmuType <= 1))
                {
                    if (!CrashesFix()) failed.Add(Language.Get("n64001.Text", typeof(Options_VC_N64).Name, true));
                }

                if (SettingParse(2))
                {
                    if (!ExtendedRAM()) failed.Add(Language.Get("n64002.Text", typeof(Options_VC_N64).Name, true));
                }

                if (SettingParse(3) && (EmuType <= 1))
                {
                    if (!AllocateROM()) { failed.Add(Language.Get("n64003.Text", typeof(Options_VC_N64).Name, true)); Allocate = false; }
                }

                if (failed.Count > 0)
                {
                    string failedList = "";
                    foreach (var item in failed)
                        failedList += "- " + item + Environment.NewLine;

                    MessageBox.Show(string.Format(Language.Get("Error.004"), failedList));
                }
            }
            catch (Exception ex)
            {
                // Dispose Contents[1]
                throw ex;
            }
        }

        private bool ShadingFix()
        {
            // Method originally reported by @NoobletCheese/@Maeson on GBAtemp.

            // Check for offset
            // ****************
            int index = Byte.IndexOf(Contents[1], "80 04 00 04 2C 00 00 FF 40 82 00 10 80 04 00 08 2C 00 00 FF");

            if (index == -1) return false;
            else
            {
                for (int i = index; i > 200; i--)
                {
                    if (Contents[1][i] == 0x94
                     && Contents[1][i + 1] == 0x21
                     && Contents[1][i + 2] == 0xFF
                     && Contents[1][i + 3] == 0xE0)
                    {
                        // Set brightness
                        // ****************
                        new byte[] { 0x4E, 0x80, 0x00, 0x20 }.CopyTo(Contents[1], i);
                        return true;
                    }
                }

                return false;
            }
        }

        private bool CrashesFix()
        {
            // Search for offset and copy
            // ****************
            int index = Byte.IndexOf(Contents[1], "4E 80 00 20 94 21 FF F0 7C 08 02 A6 3C A0 80 18 90 01 00 14 93 E1 00 0C 7C 7F 1B 78 38 65 74 B8");

            if (index == -1) return false;
            else
            {
                // Declare changed value
                // ****************
                new byte[] { 0x48, 0x00, 0xD2, 0xF0 }.CopyTo(Contents[1], index);

                // Do same with second set of values
                // ****************
                index = Byte.IndexOf(Contents[1], "38 00 00 01 38 63 B9 C0 98 03 00 0C 4E", 0xC0000, 0xCA000);

                if (index == -1) return false;
                else new byte[] { 0x3C, 0x80, 0x81, 0x09, 0x38, 0xA0, 0x00, 0x7F, 0x90, 0xA4, 0x0D, 0x00 }.CopyTo(Contents[1], index);
            }

            return true;
        }

        private bool ExtendedRAM()
        {
            // Check for offset and set RAM memory if found
            // ****************
            int index = Byte.IndexOf(Contents[1], "41 82 00 08 3C 80 00 80", 0x2000, 0x9999);

            if (index == -1)
            {
                index = Byte.IndexOf(Contents[1], "48 00 00 64 3C 80 00 80", 0x2000, 0x9999);

                if (index == -1) return false;
                else new byte[] { 0x60, 0x00, 0x00, 0x00 }.CopyTo(Contents[1], index);
            }
            else new byte[] { 0x60, 0x00, 0x00, 0x00 }.CopyTo(Contents[1], index);

            return true;
        }

        private bool AllocateROM()
        {
            // Fix based on SM64Wii (aglab2)
            // https://github.com/aglab2/sm64wii/blob/master/usamune.gzi
            // https://github.com/aglab2/sm64wii/blob/master/kit/Main.cs
            // ---------------------------------------------------------
            // Check ROM size
            // ****************
            int NewSize = 1 + ROM.Bytes.Length / 1024 / 1024;
            ROM.CheckSize();

            // Check for offset
            // ****************
            int index = Byte.IndexOf(Contents[1], "7D 00 1C 3C 80", 0x5A000, 0x5E000);

            if (index == -1) return false;
            else
            {
                var origSize = BitConverter.ToString(Contents[1].Skip(index + 5).Take(2).ToArray()).Replace("-", "");

                if (origSize[0] != '7' || origSize[3] != '0' || Byte.IndexOf(Contents[1], "1D 00 1C 3C 60", index, index + 50) == -1) return false;

                // Set size value in bytes
                // ****************
                var size = NewSize.ToString("X2");
                var size_array = new byte[]
                {
                        Convert.ToByte($"7{size[0]}", 16),
                        Convert.ToByte($"{size[1]}0", 16),
                        Convert.ToByte($"0{size[0]}", 16),
                        Convert.ToByte($"{size[1]}0", 16),
                };

                // Copy
                // ****************
                (Contents[1][index + 5], Contents[1][index + 6]) = (size_array[0], size_array[1]);

                index = Byte.IndexOf(Contents[1], "1D 00 1C 3C 60", index, index + 50);
                (Contents[1][index + 5], Contents[1][index + 6]) = (size_array[2], size_array[3]);

                return true;
            }
        }
        #endregion
    }
}
