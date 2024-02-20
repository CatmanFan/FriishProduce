using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using libWiiSharp;

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
            // Byteswap ROM first
            // ****************
            // Comparison of byte formats:
            // Big Endian:    SUPER MARIO 64
            // Byte Swapped:  USEP RAMIR O46 (each 2 bytes in reverse)
            // Little Endian: EPUSAM R OIR46 (each 4 bytes in reverse)
            // -----------------------

            // Byte Swapped to Big Endian
            // ****************
            if ((ROM.Bytes[56] == 0x4E && ROM.Bytes[57] == 0x00 && ROM.Bytes[58] == 0x00 && ROM.Bytes[59] == 0x00)
                || (ROM.Bytes[0] == 0x40 && ROM.Bytes[1] == 0x12 && ROM.Bytes[2] == 0x37 && ROM.Bytes[3] == 0x80))
            {
                for (int i = 0; i < ROM.Bytes.Length; i += 4)
                    (ROM.Bytes[i], ROM.Bytes[i + 1], ROM.Bytes[i + 2], ROM.Bytes[i + 3]) = (ROM.Bytes[i + 3], ROM.Bytes[i + 2], ROM.Bytes[i + 1], ROM.Bytes[i]);
            }

            // Little Endian to Big Endian
            // ****************
            else if ((ROM.Bytes[56] == 0x00 && ROM.Bytes[57] == 0x00 && ROM.Bytes[58] == 0x4E && ROM.Bytes[59] == 0x00)
                || (ROM.Bytes[0] == 0x37 && ROM.Bytes[1] == 0x80 && ROM.Bytes[2] == 0x40 && ROM.Bytes[3] == 0x12))
            {
                for (int i = 0; i < ROM.Bytes.Length; i += 2)
                    (ROM.Bytes[i], ROM.Bytes[i + 1]) = (ROM.Bytes[i + 1], ROM.Bytes[i]);
            }

            // -----------------------
            // Check filesize
            // Maximum ROM limit allowed: 32 MB unless allocated in main.dol (maximum possible: ~56 MB)
            // -----------------------
            double maxSize = Allocate ? 56623104 : 33554432;
            if (ROM.Bytes.Length > maxSize)
                throw new Exception(string.Format(Language.Get("Error003"), Math.Round(maxSize / 1048576).ToString(), Language.Get("Abbreviation_Megabytes")));

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
                    if (WAD.UpperTitleID.ToUpper().StartsWith("NBD"))
                    {
                        ROM.Bytes[0x3B] = 0x4E;
                        ROM.Bytes[0x3C] = 0x42;
                        ROM.Bytes[0x3D] = 0x44;
                    }

                    // Temporary ROM file at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                    // Compress using ROMC type
                    // ****************
                    if (CompressionType == 1) // Type 0
                        Process.Run
                        (
                            Paths.Tools + "romc0.exe",
                            Paths.WorkingFolder,
                            "rom romc"
                        );

                    else // Type 1
                        Process.Run
                        (
                            Paths.Tools + "romc.exe",
                            Paths.WorkingFolder,
                            "e rom romc"
                        );

                    // Check if converted file exists
                    // ****************
                    File.Delete(Paths.WorkingFolder + "rom");
                    if (!File.Exists(Paths.WorkingFolder + "romc")) throw new Exception(Language.Get("Error002"));

                    // Convert to bytes and replace at "romc"
                    // ****************
                    MainContent.ReplaceFile(MainContent.GetNodeIndex("romc"), File.ReadAllBytes(Paths.WorkingFolder + "romc"));
                    File.Delete(Paths.WorkingFolder + "romc");
                    break;
            }
        }

        protected override void ReplaceSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // TEXT
            // -----------------------

            // The separator byte array for double-lines.
            // This varies depending on the revision of the emulator, and the second separator is often cut off by end-of-file in earlier releases
            // ****************
            byte[] separator = EmuType == 0 ? new byte[] { 0x00, 0xBB, 0xBB, 0xBB }
                             : EmuType == 1 ? new byte[] { 0x00, 0xBB }
                             : new byte[] { 0x00, 0x00, 0xBB, 0xBB };

            // Text addition format: UTF-16 (Little Endian) for Rev1, UTF-16 (Big Endian) for newer revisions
            // ****************
            var encoding = EmuType == 0 || EmuType == 1 ? Encoding.Unicode : Encoding.BigEndianUnicode;

            foreach (var item in MainContent.StringTable)
            {
                if (item.Contains("saveComments_"))
                {
                    var byteArray = MainContent.Data[MainContent.GetNodeIndex(item)];

                    List<byte> newSave = new List<byte>();

                    // Add 64-byte header
                    // ****************
                    for (int i = 0; i < 64; i++)
                        newSave.Add(byteArray[i]);

                    // First savetext line
                    // ****************
                    for (int i = 0; i < encoding.GetBytes(lines[0]).Length; i++)
                        try { newSave.Add(encoding.GetBytes(lines[0])[i]); } catch { newSave.Add(0x00); }
                    if (byteArray[28] == 0x6A || (byteArray[28] == 0x6B && byteArray[29] == 0x72)) // JP/KO
                    { newSave.Add(0x00); }
                    else foreach (var Byte in separator) newSave.Add(Byte);

                    // Set first-line offset
                    // ****************
                    newSave[55] = Convert.ToByte(newSave.Count);
                    newSave[59] = Convert.ToByte(newSave.Count);

                    // Also varying on revision, how the second line field is itself handled.
                    // Where it is empty: In earlier revisions such as F-Zero X and Super Mario 64, it is a white space character, otherwise it is null.
                    // ****************
                    if (lines.Length == 1 && EmuType == 0) lines = new string[2] { lines[0], " " };

                    // Second savetext line (optional)
                    // ****************
                    if (lines.Length == 2)
                        for (int i = 0; i < encoding.GetBytes(lines[1]).Length; i++)
                            try { newSave.Add(encoding.GetBytes(lines[1])[i]); } catch { newSave.Add(0x00); }
                    foreach (var Byte in separator) newSave.Add(Byte);

                    // Character count determiner within savedata file
                    // ****************
                    newSave[45] = Convert.ToByte(encoding.GetBytes(lines[0]).Length);
                    newSave[47] = Convert.ToByte(encoding.GetBytes(lines[0]).Length);
                    newSave[61] = lines.Length == 2 ? Convert.ToByte(encoding.GetBytes(lines[1]).Length) : (byte)0x00;
                    newSave[63] = lines.Length == 2 ? Convert.ToByte(encoding.GetBytes(lines[1]).Length) : (byte)0x00;

                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), newSave.ToArray());
                }

                // -----------------------
                // IMAGE
                // -----------------------

                else if (item.ToLower().Contains("banner.tpl"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), tImg.CreateSaveTPL(Console.N64, MainContent.Data[MainContent.GetNodeIndex(item)]).ToByteArray());
            }
        }

        // *****************************************************************************************************
        #region SETTINGS
        protected override void ModifyEmulatorSettings()
        {
            List<string> failed = new List<string>();

            try
            {
                if (SettingParse(0))
                {
                    if (!ShadingFix()) failed.Add(Language.GetArray("List_N64Options")[0]);
                }

                if (SettingParse(1) && (EmuType <= 1))
                {
                    if (!CrashesFix()) failed.Add(Language.GetArray("List_N64Options")[1]);
                }

                if (SettingParse(2))
                {
                    if (!ExtendedRAM()) failed.Add(Language.GetArray("List_N64Options")[2]);
                }

                if (SettingParse(3) && (EmuType <= 1))
                {
                    if (!AllocateROM()) { failed.Add(Language.GetArray("List_N64Options")[3]); Allocate = false; }
                }

                if (failed.Count > 0)
                {
                    string failedList = "";
                    foreach (var item in failed)
                        failedList += "- " + item + Environment.NewLine;

                    System.Windows.Forms.MessageBox.Show(string.Format(Language.Get("Error004"), failedList));
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
            int NewSize = (1 + (ROM.Bytes.Length / 1048576)) * 1048576;
            ROM.CheckSize(NewSize);

            // Check for offset
            // ****************
            int index = Byte.IndexOf(Contents[1], "44 38 7D 00 1C 3C 80", 0x5A000, 0x5E000);

            if (index == -1) return false;
            else
            {
                index += 7;

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
                Contents[1][index] = size_array[0];
                Contents[1][index + 1] = size_array[1];

                var second = BitConverter.ToString(new byte[] { Contents[1][index + 36], Contents[1][index + 37] }).Replace("-", "");
                if (second[0] == '0' && second[3] == '0')
                {
                    Contents[1][index + 36] = size_array[2];
                    Contents[1][index + 37] = size_array[3];
                }

                return true;
            }
        }
        #endregion
    }
}
