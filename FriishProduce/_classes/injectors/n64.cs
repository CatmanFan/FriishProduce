using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FriishProduce.Injectors
{
    public class N64
    {
        public string ROM { get; set; }
        public string emuVersion { get; set; }
        private byte[] content1 { get; set; }
        private string content1_file = Paths.WorkingFolder + "00000001.app";
        public bool AllocateSize { get; set; }

        private enum Buttons
        {
            A = 0x8000,
            B = 0x4000,
            Z = 0x2000,
            Start = 0x1000,
            DUp = 0x0800,
            DDown = 0x0400,
            DLeft = 0x0200,
            DRight = 0x0100,
            X = 0x0080, //unused
            Y = 0x0040, //unused
            L = 0x0020,
            R = 0x0010,
            CUp = 0x0008,
            CDown = 0x0004,
            CLeft = 0x0002,
            CRight = 0x0001,
        }

        // --------------------------------------------------------------------------------------- //
        // Global functions & patches
        // --------------------------------------------------------------------------------------- //

        public void LoadContent1()
        {
            content1_file = Global.DetermineContent1();
            content1 = File.ReadAllBytes(content1_file);
        }

        public void SaveContent1()
        {
            File.WriteAllBytes(content1_file, content1);
            Global.PrepareContent1(content1_file);
        }

        public void Op_FixBrightness()
        {
            // Method originally reported by @NoobletCheese/@Maeson on GBAtemp.
            int index = Bytes.Search(content1, "80 04 00 04 2C 00 00 FF 40 82 00 10 80 04 00 08 2C 00 00 FF");
            if (index != -1)
            {
                for (int i = index; i > 200; i--)
                {
                    if (content1[i] == 0x94
                     && content1[i + 1] == 0x21
                     && content1[i + 2] == 0xFF
                     && content1[i + 3] == 0xE0)
                    {
                        new byte[] { 0x4E, 0x80, 0x00, 0x20 }.CopyTo(content1, i);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Alternative method of fixing dark filter, may not work with some WADs or result in excess brightness
        /// </summary>
        public void Op_FixBrightness_Alt()
        {
            int index = Bytes.Search(content1, "00 00 00 07 00 00 00 BE 00 00 00 BE 00 00 00 BE");
            if (index != -1)
            {
                content1[index + 7] = 0xFF;
                content1[index + 11] = 0xFF;
                content1[index + 15] = 0xFF;
            }
        }

        public void Op_ExpansionRAM()
        {
            int index = Bytes.Search(content1, "41 82 00 08 3C 80 00 80", 0x2000, 0x9999);
            if (index == -1)
            {
                index = Bytes.Search(content1, "48 00 00 64 3C 80 00 80", 0x2000, 0x9999);
                if (index == -1) throw new Exception(Program.Language.Get("m012"));
                else goto Set;
            }
            else goto Set;

            Set:
            new byte[] { 0x60, 0x00, 0x00, 0x00 }.CopyTo(content1, index);
        }

        // --------------------------------------------------------------------------------------- //
        // The following patches seems to be supported by only ver.1 revisions (i.e. not ROMC)
        // --------------------------------------------------------------------------------------- //

        public void Op_FixCrashes()
        {
            byte[] insert = { 0x48, 0x00, 0xD2, 0xF0 };
            int index = Bytes.Search(content1, "4E 80 00 20 94 21 FF F0 7C 08 02 A6 3C A0 80 18 90 01 00 14 93 E1 00 0C 7C 7F 1B 78 38 65 74 B8");
            if (index != -1)
            {
                insert.CopyTo(content1, index);

                insert = new byte[] { 0x3C, 0x80, 0x81, 0x09, 0x38, 0xA0, 0x00, 0x7F, 0x90, 0xA4, 0x0D, 0x00 };
                index = Bytes.Search(content1, "38 00 00 01 38 63 B9 C0 98 03 00 0C 4E", 0xC0000, 0xCA000);
                if (index != -1) insert.CopyTo(content1, index);

                return;
            }
        }

        public void Op_AllocateROM()
        {
            // Fix based on SM64Wii (aglab2)
            // https://github.com/aglab2/sm64wii/blob/master/usamune.gzi
            // https://github.com/aglab2/sm64wii/blob/master/kit/Main.cs
            // ---------------------------------------------------------
            var size_ROM = 1 + new FileInfo(File.Exists($"{Paths.WorkingFolder_Content5}romc") ? $"{Paths.WorkingFolder_Content5}romc" : $"{Paths.WorkingFolder_Content5}rom").Length
                / 1024 / 1024;
            if (size_ROM > 56) throw new Exception(Program.Language.Get("m015"));

            int index = Bytes.Search(content1, "44 38 7D 00 1C 3C 80", 0x5A000, 0x5E000);
            if (index != -1)
            {
                var size = size_ROM.ToString("X2");
                var size_array = new byte[4];

                size_array[0] = Convert.ToByte($"7{size[0]}", 16);
                size_array[1] = Convert.ToByte($"{size[1]}0", 16);
                size_array[2] = Convert.ToByte($"0{size[0]}", 16);
                size_array[3] = Convert.ToByte($"{size[1]}0", 16);

                int offset = index + 7;
                content1[offset] = size_array[0];
                content1[offset + 1] = size_array[1];

                return;
            }

            // Not found
            throw new Exception(Program.Language.Get("m015"));
        }

        // ----------------------------------------------------------------------- //

        private readonly string byteswappedROM = $"{Paths.WorkingFolder}rom";
        private readonly string compressedROM = $"{Paths.WorkingFolder}rom_comp";
        private readonly string ROMC = $"{Paths.WorkingFolder}romc";

        public bool CheckForROMC() => emuVersion.Contains("romc");

        public void ByteswapROM()
        {
            File.Copy(ROM, $"{Paths.Apps}ucon64\\rom");
            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = $"{Paths.Apps}ucon64\\ucon64.exe",
                WorkingDirectory = $"{Paths.Apps}ucon64\\",
                Arguments = $"--z64 \"{Paths.Apps}ucon64\\rom\" \"{byteswappedROM}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();
            File.Delete($"{Paths.Apps}ucon64\\rom");
            if (File.Exists($"{Paths.Apps}ucon64\\rom.bak")) File.Delete($"{Paths.Apps}ucon64\\rom.bak");

            if (!File.Exists(byteswappedROM))
                throw new Exception(Program.Language.Get("m011"));

            if (emuVersion.Contains("hero")) FixBHeroCrash(byteswappedROM);
        }

        public void FixROM(bool Byteswap = false)
        {
            if (Byteswap)
            {
                string pPath = Paths.WorkingFolder + "romfix.exe";
                File.WriteAllBytes(pPath, Properties.Resources.N64WiiRomfixer);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = Path.GetDirectoryName(ROM),
                }))
                    p.WaitForExit();
                File.Delete(pPath);

               if (!File.Exists(ROM + ".new"))
                    throw new Exception(Program.Language.Get("m011"));

            }

            if (emuVersion.Contains("hero"))
            {
                if (!Byteswap) File.WriteAllBytes(ROM + ".new", File.ReadAllBytes(ROM));
                FixBHeroCrash(ROM + ".new");
                
            }

            if (File.Exists(ROM + ".new")) File.Move(ROM + ".new", byteswappedROM);
            else File.Copy(ROM, byteswappedROM);
        }

        private void FixBHeroCrash(string ROMpath)
        {
            // Set title ID to NBDx to prevent crashing
            var ROMbytes = File.ReadAllBytes(ROMpath);
            ROMbytes[0x3B] = 0x4E;
            ROMbytes[0x3C] = 0x42;
            ROMbytes[0x3D] = 0x44;
            File.WriteAllBytes(ROMpath, ROMbytes);
        }

        public void CompressROM(int type)
        {
            if (type <= 0) return;

            switch (type)
            {
                case 1: // SM64compress
                {
                    // Checksum fix (ASM)
                    /* var ROMbytes = File.ReadAllBytes(byteswappedROM);
                    new byte[] { 0x14, 0xE8, 0x00, 0x06 }.CopyTo(ROMbytes, 0x66C);
                    new byte[] { 0x16, 0x08, 0x00, 0x03 }.CopyTo(ROMbytes, 0x678);
                    File.WriteAllBytes(byteswappedROM, ROMbytes); */

                    // Run process
                    string pPath = $"{Paths.Apps}ucon64\\sm64compress.exe";
                    File.WriteAllBytes(pPath, Properties.Resources.SM64Compress);
                    using (Process p = Process.Start(new ProcessStartInfo
                    {
                        FileName = pPath,
                        WorkingDirectory = $"{Paths.Apps}ucon64\\",
                        Arguments = $"{Paths.WorkingFolder + "rom"} rom_comp",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }))
                        p.WaitForExit();
                    File.Delete(pPath);
                    break;
                }

                case 2: // Zelda64compress
                {
                    string arg = $"--in {Paths.WorkingFolder + "rom"} --out rom_comp --mb 32 --codec yaz";
                    /* if      (type == 2) arg += " --dma \"0x12F70,1548\" --compress \"9-14,28-END\"";
                    else if (type == 3) arg += " --dma \"0x7430,1526\" --compress \"10-14,27-END\"";
                    else if (type == 4) arg += " --dma \"0x1A500,1568\" --compress \"10-14,23,24,31-END\" --skip \"1127\" --repack \"15-20,22\""; */

                    // Run process
                    string pPath = $"{Paths.Apps}ucon64\\z64compress.exe";
                    File.WriteAllBytes(pPath, Properties.Resources.Z64Compress);
                    using (Process p = Process.Start(new ProcessStartInfo
                    {
                        FileName = pPath,
                        WorkingDirectory = $"{Paths.Apps}ucon64\\",
                        Arguments = arg,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }))
                        p.WaitForExit();
                    File.Delete(pPath);
                    break;
                }
            }

            File.Delete(byteswappedROM);
            if (!File.Exists(compressedROM))
                throw new Exception(Program.Language.Get("m011"));
            else File.Move(compressedROM, byteswappedROM);
        }

        public void ReplaceROM(bool romc)
        {
            if (romc)
            {
                // Check filesize
                // Maximum ROM limit allowed: 32 MB (uncompressed)
                // Crashes on black screen if tested with an originally 64 MB .z64 ROM (not compressed beforehand)
                if (File.ReadAllBytes(byteswappedROM).Length > 4194304 * 8)
                {
                    File.Delete(byteswappedROM);
                    throw new Exception(Program.Language.Get("m018"));
                }

                // ROMs not in multiple of 4MB are not supported by the ROMC.exe
                else if (File.ReadAllBytes(ROM).Length % 4194304 != 0)
                {
                    File.Delete(byteswappedROM);
                    throw new Exception(Program.Language.Get("m013"));
                }

                else
                {
                    string pPath = Paths.WorkingFolder + "romc.exe";
                    File.WriteAllBytes(pPath, Properties.Resources.ROMC);
                    using (Process p = Process.Start(new ProcessStartInfo
                    {
                        FileName = pPath,
                        WorkingDirectory = Paths.WorkingFolder,
                        Arguments = $"e \"{byteswappedROM}\" romc",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }))
                        p.WaitForExit();
                    File.Delete(byteswappedROM);
                    File.Delete(pPath);

                    if (!File.Exists(ROMC))
                        throw new Exception(Program.Language.Get("m014"));

                    // Copy
                    File.Copy(ROMC, $"{Paths.WorkingFolder_Content5}romc", true);
                    File.Delete(ROMC);
                }
            }
            else
            {
                // Check filesize
                // Maximum ROM limit allowed: 32 MB unless allocated in main.dol (maximum possible: ~56 MB)
                if (File.ReadAllBytes(byteswappedROM).Length > 4194304 * 8 && !AllocateSize)
                {
                    File.Delete(byteswappedROM);
                    throw new Exception(Program.Language.Get("m018"));
                }

                // Copy
                File.WriteAllBytes($"{Paths.WorkingFolder_Content5}rom", File.ReadAllBytes(byteswappedROM));
                File.Delete(byteswappedROM);
            }
        }

        // ----------------------------------------------------------------------- //

        public void InsertSaveComments(string[] lines)
        {
            // The separator byte array for double-lines.
            // This varies depending on the revision of the emulator, and the second separator is often cut off by end-of-file in earlier releases
            byte[] separator =
                emuVersion == "rev1" ? new byte[] { 0x00, 0xBB, 0xBB, 0xBB } :
                emuVersion == "rev1-alt" ? new byte[] { 0x00, 0xBB } :
                new byte[] { 0x00, 0x00, 0xBB, 0xBB };

            // Text addition format: UTF-16 (Little Endian) for Rev1, UTF-16 (Big Endian) for newer revisions
            var encoding = emuVersion.Contains("rev1") ? Encoding.Unicode : Encoding.BigEndianUnicode;

            foreach (var item in Directory.GetFiles(Paths.WorkingFolder_Content5))
            {
                if (Path.GetFileName(item).Contains("saveComments_"))
                {
                    var byteArray = File.ReadAllBytes(item);
                    List<byte> newSave = new List<byte>();

                    // Add 64-byte header
                    for (int i = 0; i < 64; i++)
                        newSave.Add(byteArray[i]);

                    // First savetext line
                    for (int i = 0; i < encoding.GetBytes(lines[0]).Length; i++)
                        try { newSave.Add(encoding.GetBytes(lines[0])[i]); } catch { newSave.Add(0x00); }
                    if (byteArray[28] == 0x6A || (byteArray[28] == 0x6B && byteArray[29] == 0x72)) // JP/KO
                    { newSave.Add(0x00); }
                    else foreach (var Byte in separator) newSave.Add(Byte);

                    // Set first-line offset
                    newSave[55] = Convert.ToByte(newSave.Count);
                    newSave[59] = Convert.ToByte(newSave.Count);

                    // Also varying on revision, how the second line field is itself handled.
                    // Where it is empty: In earlier revisions such as F-Zero X and Super Mario 64, it is a white space character, otherwise it is null.
                    if (lines.Length == 1 && emuVersion == "rev1") lines = new string[2] { lines[0], " " };

                    // Second savetext line (optional)
                    if (lines.Length == 2)
                        for (int i = 0; i < encoding.GetBytes(lines[1]).Length; i++)
                            try { newSave.Add(encoding.GetBytes(lines[1])[i]); } catch { newSave.Add(0x00); }
                    foreach (var Byte in separator) newSave.Add(Byte);

                    // Character count determiner within savedata file
                    newSave[45] = Convert.ToByte(encoding.GetBytes(lines[0]).Length);
                    newSave[47] = Convert.ToByte(encoding.GetBytes(lines[0]).Length);
                    newSave[61] = lines.Length == 2 ? Convert.ToByte(encoding.GetBytes(lines[1]).Length) : (byte)0x00;
                    newSave[63] = lines.Length == 2 ? Convert.ToByte(encoding.GetBytes(lines[1]).Length) : (byte)0x00;

                    File.WriteAllBytes(item, newSave.ToArray());
                }
            }
        }

        public void CleanT64(bool delete = true)
        {
            foreach (var item in Directory.GetFiles(Paths.WorkingFolder_Content5))
                if (Path.GetExtension(item).ToLower() == ".t64")
                    if (delete) File.Delete(item); else File.WriteAllText(item, string.Empty);
        }
    }
}
