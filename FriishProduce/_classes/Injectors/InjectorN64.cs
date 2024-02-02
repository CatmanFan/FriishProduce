using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using libWiiSharp;

namespace FriishProduce
{
    public class InjectorN64 : InjectorBase
    {
        public enum Type
        {
            Rev1,           // F-Zero X, Super Mario 64

            Rev1_Alt,       // Star Fox 64, Mario Kart 64, Zelda: Ocarina

            Rev2,           // Pokémon Snap, Sin & Punishment, Yoshi's Story

            Rev3            // Bomberman Hero, Custom Robo V2, Mario Golf, Mario Party 2, Ogre Battle 64, Paper Mario + KOREA: Star Fox 64, Mario Kart 64
                            // This is the one which uses the ROMC compressed format
        }

        public Type EmuType { get; set; }
        private bool isAllocated { get; set; }

        private byte[] ROM { get; set; }

        public InjectorN64(WAD w) : base(w)
        {
            UsesContent1 = true;
            UsesContent5 = true;
            isAllocated = false;
            Load();

            if (WAD.Region == Region.Korea) EmuType = Type.Rev3;
            else switch (WAD.UpperTitleID.Substring(0, 3).ToUpper())
            {
                default:
                case "NAA":
                case "NAF":
                    EmuType = Type.Rev1;
                    break;

                case "NAB":
                case "NAC":
                case "NAD":
                    EmuType = Type.Rev1_Alt;
                    break;

                case "NAK":
                case "NAJ":
                case "NAH":
                    EmuType = Type.Rev2;
                    break;

                case "NA3":
                case "NAE":
                case "NAU":
                case "NAY":
                case "NAZ":
                    EmuType = Type.Rev3;
                    break;
            }

        }

        /// <summary>
        /// Injection of ROM from base file
        /// </summary>
        /// <param name="ROM">Path to ROM</param>
        /// <param name="type">Type of ROMC compression (0 = none; 1 = ROMC type 0; 2 = ROMC type 1)</param>
        public void ReplaceROM(string ROM, int type = 0)
        {
            // -----------------------
            // Check if raw ROM exists
            // -----------------------
            if (!File.Exists(ROM))
                throw new FileNotFoundException(new FileNotFoundException().Message, ROM);

            ReplaceROM(File.ReadAllBytes(ROM), type);
        }

        /// <summary>
        /// Injection of ROM from byte array
        /// </summary>
        /// <param name="ROM">ROM in bytes</param>
        /// <param name="type">Type of ROMC compression (0 = none; 1 = ROMC type 0; 2 = ROMC type 1)</param>
        public void ReplaceROM(byte[] ROMbytes, int type = 0)
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
            if ((ROMbytes[56] == 0x4E && ROMbytes[57] == 0x00 && ROMbytes[58] == 0x00 && ROMbytes[59] == 0x00)
                || (ROMbytes[0] == 0x40 && ROMbytes[1] == 0x12 && ROMbytes[2] == 0x37 && ROMbytes[3] == 0x80))
            {
                for (int i = 0; i < ROMbytes.Length; i += 4)
                    (ROMbytes[i], ROMbytes[i + 1], ROMbytes[i + 2], ROMbytes[i + 3]) = (ROMbytes[i + 3], ROMbytes[i + 2], ROMbytes[i + 1], ROMbytes[i]);
            }

            // Little Endian to Big Endian
            // ****************
            else if ((ROMbytes[56] == 0x00 && ROMbytes[57] == 0x00 && ROMbytes[58] == 0x4E && ROMbytes[59] == 0x00)
                || (ROMbytes[0] == 0x37 && ROMbytes[1] == 0x80 && ROMbytes[2] == 0x40 && ROMbytes[3] == 0x12))
            {
                for (int i = 0; i < ROMbytes.Length; i += 2)
                    (ROMbytes[i], ROMbytes[i + 1])                                   = (ROMbytes[i + 1], ROMbytes[i]);
            }

            // -----------------------
            // Check filesize
            // Maximum ROM limit allowed: 32 MB unless allocated in main.dol (maximum possible: ~56 MB)
            // -----------------------
            double maxSize = isAllocated ? 56623104 : 33554432;
            if (ROMbytes.Length > maxSize)
                throw new Exception(string.Format(Language.Get("Error003"), Math.Round(maxSize / 1048576).ToString(), Language.Get("Abbreviation_Megabytes")));

            // -----------------------
            // Actually replace original ROM
            // -----------------------
            switch (EmuType)
            {
                default:
                    Content5.ReplaceFile(Content5.GetNodeIndex("rom"), ROMbytes);
                    break;

                case Type.Rev3:
                    // Set title ID to NBDx to prevent crashing on Bomberman Hero
                    // ****************
                    if (WAD.UpperTitleID.ToUpper().StartsWith("NBD"))
                    {
                        ROMbytes[0x3B] = 0x4E;
                        ROMbytes[0x3C] = 0x42;
                        ROMbytes[0x3D] = 0x44;
                    }

                    // Temporary ROM file at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "rom", ROMbytes);

                    // Compress using ROMC type
                    // ****************
                    if (type == 1) // Type 0
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
                    ROMbytes = File.ReadAllBytes(Paths.WorkingFolder + "romc");
                    File.Delete(Paths.WorkingFolder + "romc");

                    Content5.ReplaceFile(Content5.GetNodeIndex("romc"), ROMbytes);
                    break;
            }

            ROM = ROMbytes;
        }

        public void InsertSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // TEXT
            // -----------------------

            // The separator byte array for double-lines.
            // This varies depending on the revision of the emulator, and the second separator is often cut off by end-of-file in earlier releases
            // ****************
            byte[] separator = EmuType == Type.Rev1     ? new byte[] { 0x00, 0xBB, 0xBB, 0xBB }
                             : EmuType == Type.Rev1_Alt ? new byte[] { 0x00, 0xBB }
                             : new byte[] { 0x00, 0x00, 0xBB, 0xBB };

            // Text addition format: UTF-16 (Little Endian) for Rev1, UTF-16 (Big Endian) for newer revisions
            // ****************
            var encoding = EmuType == Type.Rev1 || EmuType == Type.Rev1_Alt ? Encoding.Unicode : Encoding.BigEndianUnicode;

            foreach (var item in Content5.StringTable)
            {
                if (item.Contains("saveComments_"))
                {
                    var byteArray = Content5.Data[Content5.GetNodeIndex(item)];

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
                    if (lines.Length == 1 && EmuType == Type.Rev1) lines = new string[2] { lines[0], " " };

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

                    Content5.ReplaceFile(Content5.GetNodeIndex(item), newSave.ToArray());
                }

                // -----------------------
                // IMAGE
                // -----------------------

                else if (item.ToLower().Contains("banner.tpl"))
                    Content5.ReplaceFile(Content5.GetNodeIndex(item), tImg.CreateSaveTPL(Console.N64, Content5.Data[Content5.GetNodeIndex(item)]).ToByteArray());
            }
        }
        
        // *****************************************************************************************************
        #region SETTINGS
        public void ModifyEmulator(bool BrightnessFix, bool CrashFix, bool RAMExpansion, bool AllocateROMSize)
        {
            List<string> failed = new List<string>();

            try
            {
                if (BrightnessFix)
                {
                    // Method originally reported by @NoobletCheese/@Maeson on GBAtemp.


                    // Check for offset
                    // ****************
                    int index = Byte.IndexOf(Content1, "80 04 00 04 2C 00 00 FF 40 82 00 10 80 04 00 08 2C 00 00 FF");

                    if (index == -1) failed.Add(Language.GetArray("List_N64Options")[0]);
                    else
                    {
                        for (int i = index; i > 200; i--)
                        {
                            if (Content1[i] == 0x94
                             && Content1[i + 1] == 0x21
                             && Content1[i + 2] == 0xFF
                             && Content1[i + 3] == 0xE0)
                            {
                                // Set brightness
                                // ****************
                                new byte[] { 0x4E, 0x80, 0x00, 0x20 }.CopyTo(Content1, i);
                            }
                        }
                    }
                }

                if (CrashFix && (EmuType == Type.Rev1 || EmuType == Type.Rev1_Alt))
                {
                    // Declare changed value
                    // ****************
                    byte[] insert = { 0x48, 0x00, 0xD2, 0xF0 };

                    // Search for offset and copy
                    // ****************
                    int index = Byte.IndexOf(Content1, "4E 80 00 20 94 21 FF F0 7C 08 02 A6 3C A0 80 18 90 01 00 14 93 E1 00 0C 7C 7F 1B 78 38 65 74 B8");

                    if (index == -1) failed.Add(Language.GetArray("List_N64Options")[1]);
                    else
                    {
                        insert.CopyTo(Content1, index);

                        // Do same with new values
                        // ****************
                        insert = new byte[] { 0x3C, 0x80, 0x81, 0x09, 0x38, 0xA0, 0x00, 0x7F, 0x90, 0xA4, 0x0D, 0x00 };

                        index = Byte.IndexOf(Content1, "38 00 00 01 38 63 B9 C0 98 03 00 0C 4E", 0xC0000, 0xCA000);

                        if (index == -1) failed.Add(Language.GetArray("List_N64Options")[1]);
                        else insert.CopyTo(Content1, index);
                    }
                }

                if (RAMExpansion)
                {
                    // Check for offset and set RAM memory if found
                    // ****************
                    int index = Byte.IndexOf(Content1, "41 82 00 08 3C 80 00 80", 0x2000, 0x9999);

                    if (index == -1)
                    {
                        index = Byte.IndexOf(Content1, "48 00 00 64 3C 80 00 80", 0x2000, 0x9999);

                        if (index == -1) failed.Add(Language.GetArray("List_N64Options")[2]);
                        else new byte[] { 0x60, 0x00, 0x00, 0x00 }.CopyTo(Content1, index);
                    }

                    else new byte[] { 0x60, 0x00, 0x00, 0x00 }.CopyTo(Content1, index);
                }

                if (AllocateROMSize && (EmuType == Type.Rev1 || EmuType == Type.Rev1_Alt))
                {
                    // Fix based on SM64Wii (aglab2)
                    // https://github.com/aglab2/sm64wii/blob/master/usamune.gzi
                    // https://github.com/aglab2/sm64wii/blob/master/kit/Main.cs
                    // ---------------------------------------------------------
                    // Check ROM size
                    // ****************
                    int size_ROM = 1 + ROM.Length / 1024 / 1024;
                    // if (size_ROM > 56) throw new Exception(string.Format(Language.Get("Error003"), "56", Language.Get("Abbreviation_Megabytes")));

                    // Check for offset
                    // ****************
                    int index = Byte.IndexOf(Content1, "44 38 7D 00 1C 3C 80", 0x5A000, 0x5E000);

                    if (index == -1) failed.Add(Language.GetArray("List_N64Options")[3]);
                    else
                    {
                        // Set size value in bytes
                        // ****************
                        var size = size_ROM.ToString("X2");
                        var size_array = new byte[]
                        {
                        Convert.ToByte($"7{size[0]}", 16),
                        Convert.ToByte($"{size[1]}0", 16),
                        Convert.ToByte($"0{size[0]}", 16),
                        Convert.ToByte($"{size[1]}0", 16),
                        };

                        // Copy
                        // ****************
                        Content1[index + 7] = size_array[0];
                        Content1[index + 8] = size_array[1];

                        isAllocated = true;
                    }
                }

                if (failed.Count > 0)
                {
                    string failedList = "";
                    foreach (var item in failed)
                        failedList += "- " + item + Environment.NewLine;

                    System.Windows.Forms.MessageBox.Show(string.Format(Language.Get("Error004"), failedList), Language.Get("ApplicationName"));
                }
            }
            catch (Exception ex)
            {
                // Dispose Content1
                throw ex;
            }
        }
        #endregion
    }
}
