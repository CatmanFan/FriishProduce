using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
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
            needsMainDol = true;
            mainContentIndex = 5;
            needsManualLoaded = true;
            SaveTextEncoding = Encoding.Unicode;

            base.Load();

            if (MainContent.GetNodeIndex("romc") != -1)
                EmuType = 3;
            else EmuType = WAD.UpperTitleID.Substring(0, 3).ToUpper() switch
            {
                   "NAB"
                or "NAC"
                or "NAD" => 1,

                   "NAH"
                or "NAI"
                or "NAJ"
                or "NAK"
                or "NAO" => 2,

                   "NA3"
                or "NAE"
                or "NAL"
                or "NAP"
                or "NAU"
                or "NAY"
                or "NAZ" => 3,

                _ => 0,
            };
        }

        /// <summary>
        /// Injection of ROM from base file
        /// </summary>
        /// <param name="ROM">Path to ROM</param>
        protected override void ReplaceROM()
        {
            // -----------------------
            // Check filesize
            // Maximum ROM limit allowed: 64 MB for ROMC, otherwise 32 MB unless allocated in main.dol (maximum possible: ~56 MB)
            // -----------------------
            ROM.MaxSize = EmuType == 3 ? 67108864 : Allocate ? 56623104 : 33554432;
            ROM.CheckSize();
            var data = (ROM as ROM_N64).ToBigEndian();

            // -----------------------
            // Actually replace original ROM
            // -----------------------
            switch (EmuType)
            {
                default:
                    MainContent.ReplaceFile(MainContent.GetNodeIndex("rom"), data);
                    break;

                case 3:
                    // Set title ID to NBDx to prevent crashing on Bomberman Hero
                    // ****************
                    if (WAD.UpperTitleID.ToUpper().StartsWith("NA3"))
                        Encoding.ASCII.GetBytes("NBD").CopyTo(data, 0x3B);

                    // Temporary ROM file at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "rom", data);

                    // Compress using ROMC type
                    // ****************
                    if (CompressionType == 1) // Type 0
                        Utils.Run(FileDatas.Apps.romc0, "romc0", "rom romc", false, false);

                    else // Type 1
                        Utils.Run(FileDatas.Apps.romc, "romc", "e rom romc", false, false);

                    // Check if converted file exists
                    // ****************
                    File.Delete(Paths.WorkingFolder + "rom");
                    if (!File.Exists(Paths.WorkingFolder + "romc")) throw new Exception(Program.Lang.Msg(2, 1));

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

                    List<byte> newSave = new();

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

                else if (item.ToLower().Contains("banner.tpl") && Img != null)
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), Img.CreateSaveTPL(MainContent.Data[MainContent.GetNodeIndex(item)]).ToByteArray());
            }
        }

        private bool HasChars(string text, int min, int max) => text.Where(e => e >= min && e <= max).Any();

        // *****************************************************************************************************
        #region SETTINGS
        protected override void ModifyEmulatorSettings()
        {
            List<string> failed = new();

            try
            {
                if (SettingParse(0))
                {
                    if (!ShadingFix()) failed.Add(Program.Lang.String("patch_fixbrightness", "vc_n64"));
                }

                if (SettingParse(1) && (EmuType <= 1))
                {
                    if (!CrashesFix()) failed.Add(Program.Lang.String("patch_fixcrashes", "vc_n64"));
                }

                if (SettingParse(2))
                {
                    if (!ExtendedRAM()) failed.Add(Program.Lang.String("patch_expandedram", "vc_n64"));
                }

                if (SettingParse(3) && (EmuType <= 1))
                {
                    if (!AllocateROM()) { failed.Add(Program.Lang.String("patch_autosizerom", "vc_n64")); Allocate = false; }
                }

                if (Keymap?.Count > 0)
                {
                    if (!RemapControls()) failed.Add(Program.Lang.String("patch_remap", "vc_n64"));
                }

                if (Settings["clean_textures"].ToLower() == "true")
                {
                    CleanTextures();
                }

                /* if (Settings["widescreen"].ToLower() == "true")
                {
                    if (!Widescreen()) { failed.Add(Program.Lang.String("patch_widescreen", "vc_n64")); }
                } */

                if (failed.Count > 0)
                {
                    string failedList = "";
                    foreach (var item in failed)
                        failedList += "- " + item + Environment.NewLine;

                    MessageBox.Show(string.Format(Program.Lang.Msg(5, 1), failedList));
                }
            }

            catch (Exception ex)
            {
                // Dispose Contents[1]
                throw ex;
            }
        }

        private void CleanTextures()
        {
            try { Directory.CreateDirectory(Paths.WorkingFolder + "content5\\"); } catch { }
            MainContent.Extract(Paths.WorkingFolder + "content5\\");

            string[] deletable = new string[] { ".t64", ".tif", ".usm" };

            foreach (var extension in deletable)
            {
                var list = Directory.EnumerateFiles(Paths.WorkingFolder + "content5\\", "*.*", SearchOption.AllDirectories).Where(x => x.ToLower().EndsWith(extension));

                foreach (var item in list)
                    try { File.Delete(item); } catch { }
            }

            MainContent.CreateFromDirectory(Paths.WorkingFolder + "content5\\");
            try { Directory.Delete(Paths.WorkingFolder + "content5\\", true); } catch { }
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
                    if (Byte.IsSame(Contents[1].Skip(i).Take(4).ToArray(), new byte[] { 0x94, 0x21, 0xFF, 0xE0 }))
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
            // Search for first offset and copy if found
            // ****************
            var offsets = new List<(string hex, int start, int end)>()
            {
                ("41 82 00 08 3C 80 00 80", 0x2000, 0x9999),
                ("48 00 00 64 3C 80 00 80", 0x2000, 0x9999)
            };

            foreach (var (hex, start, end) in offsets)
            {
                int index = Byte.IndexOf(Contents[1], hex, start, end);
                if (index != -1)
                {
                    new byte[] { 0x60, 0x00, 0x00, 0x00 }.CopyTo(Contents[1], index);
                    return true;
                }
            }

            return false;
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

        private bool RemapControls()
        {
            // Search for controller button offsets
            // ****************
            Dictionary<Buttons, int> offsets = new();
            int start = 0;

            switch (WAD.UpperTitleID.Substring(0, 3).ToUpper())
            {
                #region Manual search offsets (unused)
                // Manual search for known controller button offsets:
                // These offsets are for the first button found ("A"), as retrieved from Patcher64+ Tool source code.
                // ****************
                /* case "NAA": // Super Mario 64
                    start = 0x168618;
                    break;

                case "NAB": // Mario Kart 64
                    start = 0x169ED8;
                    break;

                case "NAE": // Paper Mario
                    start = 0x175A20;
                    break;

                case "NAL": // Smash Bros.
                    start = 0x150F00;
                    break;

                case "NAC": // Zelda: Ocarina
                    start = 0x16BAC0;
                    break; */
                #endregion

                case "NAR": // Zelda: Majora
                    start = 0x148430; // 0x1484E0; third set
                    break;

                // Do automatic search for first available controller button offset
                // ****************
                default:
                    // 
                    if (Byte.IndexOf(Contents[1], "30 30 27 29 0A 0A") != -1)
                    {
                        start = Byte.IndexOf(Contents[1], "30 30 27 29 0A 0A") + 6;

                        while (Contents[1][start] == 0)
                            start++;
                    }
                    else
                        // No controller button offsets were found (perhaps not supported or the algorithm is different).
                        return false;
                    break;
            }

            // Some WADs have DPad-Left's offset coming before Right, instead of the other way around
            bool flippedOrder = WAD.UpperTitleID.Substring(0, 3).ToUpper() is "NAC" or "NAR";

            // Declare arrays to fill remapped buttons
            // ****************
            Buttons[] main_b = new[]
            {
                Buttons.Classic_A,
                Buttons.Classic_B,
                Buttons.Classic_X,
                Buttons.Classic_Y,
                Buttons.Classic_L,
                Buttons.Classic_R,
                Buttons.Classic_ZR,
                Buttons.Classic_Plus
            };

            Buttons[] directional_b = new[]
            {
                Buttons.Classic_Up_L,
                Buttons.Classic_Down_L,
                Buttons.Classic_Left_L,
                Buttons.Classic_Right_L,
                Buttons.Classic_Up,
                Buttons.Classic_Down,
                flippedOrder ? Buttons.Classic_Left : Buttons.Classic_Right,
                flippedOrder ? Buttons.Classic_Right : Buttons.Classic_Left,
                Buttons.Classic_Up_R,
                Buttons.Classic_Down_R,
                Buttons.Classic_Left_R,
                Buttons.Classic_Right_R
            };

            // 

            byte[] main = new byte[32];
            byte[] directional = new byte[48];
            int dSpacing = 4, eSpacing = 4;

            // Both the main and directional groups may or may not be separated by four empty bytes
            // ****************
            if (BitConverter.ToInt32(Contents[1], start + main.Length) != 0)
                dSpacing = 0;
            if (BitConverter.ToInt32(Contents[1], start + main.Length + dSpacing + directional.Length) != 0)
                eSpacing = 0;

            // Add all offsets needed for each button relative to the starting path
            // ****************
            if (start > 0)
            {
                for (int i = 0; i < main_b.Length; i++)
                    offsets.Add(main_b[i], start + (4 * i));

                for (int i = 0; i < directional_b.Length; i++)
                    offsets.Add(directional_b[i], start + main.Length + dSpacing + (4 * i));
            }

            // File.WriteAllBytes(Paths.WorkingFolder + "content1.app", Contents[1]);

            // Copy
            // ****************
            for (int i = 0; i < main_b.Length; i++)
                try { Byte.FromHex(Keymap[main_b[i]]).CopyTo(main, i * 4); } catch { }

            for (int i = 0; i < directional_b.Length; i++)
                try { Byte.FromHex(Keymap[directional_b[i]]).CopyTo(directional, i * 4); } catch { }

            List<byte> remapped = new();

            for (int i = 0; i < 3; i++)
            {
                if (BitConverter.ToInt32(Contents[1], start + (remapped.Count * i)) == 128)
                {
                    remapped.AddRange(main);
                    remapped.AddRange(new byte[dSpacing]);
                    remapped.AddRange(directional);
                    remapped.AddRange(new byte[eSpacing]);
                }
            }

            remapped.CopyTo(Contents[1], start);

            return true;
        }

        /// NOT WORKING
        private bool Widescreen()
        {
            // Check for offset, patch and profit
            return false;
        }
        #endregion
    }
}
