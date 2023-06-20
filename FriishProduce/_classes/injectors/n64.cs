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
        private bool IsAllocated = false;

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
            var size_ROM = 1 + new FileInfo(ROM).Length / 1024 / 1024;
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

                IsAllocated = true;
                return;
            }

            // Not found
            throw new Exception(Program.Language.Get("m015"));
        }

        // ----------------------------------------------------------------------- //

        private readonly string byteswappedROM = $"{Paths.Apps}ucon64\\rom.z64";
        private readonly string compressedROM = $"{Paths.WorkingFolder}romc";

        public bool CheckForROMC() => emuVersion.Contains("romc");

        public void ByteswapROM()
        {
            if (File.ReadAllBytes(ROM).Length % 4194304 != 0 && CheckForROMC())
                throw new Exception(Program.Language.Get("m013"));

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
        }

        public void ReplaceROM(bool romc)
        {
            if (romc)
            {
                if (emuVersion.Contains("hero"))
                {
                    // Set title ID to NBDx to prevent crashing
                    var ROMbytes = File.ReadAllBytes(byteswappedROM);
                    ROMbytes[0x3B] = 0x4E;
                    ROMbytes[0x3C] = 0x42;
                    ROMbytes[0x3D] = 0x44;
                    File.WriteAllBytes(byteswappedROM, ROMbytes);
                }

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

                if (!File.Exists(compressedROM))
                    throw new Exception(Program.Language.Get("m014"));

                // Check filesize
                // Maximum ROM limit allowed: 32 MB
                if (File.ReadAllBytes(compressedROM).Length > 4194304 * 8)
                {
                    File.Delete(compressedROM);
                    throw new Exception(Program.Language.Get("m018"));
                }

                // Copy
                File.Copy(compressedROM, $"{Paths.WorkingFolder_Content5}romc", true);
                File.Delete(compressedROM);
            }
            else
            {
                // Check filesize
                // Maximum ROM limit allowed: 32 MB unless allocated in main.dol (maximum possible: ~56 MB)
                if (File.ReadAllBytes(byteswappedROM).Length > 4194304 * 8 && !IsAllocated)
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
