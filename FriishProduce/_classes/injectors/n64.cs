using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using libWiiSharp;

namespace FriishProduce.Injectors
{
    public class N64
    {
        public string ROM { get; set; }
        public string emuVersion { get; set; }
        private byte[] content1 { get; set; }

        private readonly string byteswappedROM = $"{Paths.Apps}ucon64\\rom.z64";
        private readonly string compressedROM = $"{Paths.WorkingFolder}romc";

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

        // ------------------------- EMULATOR FUNCTIONS -------------------------- //

        private string content1_file;

        public void LoadContent1()
        {
            content1_file = Global.DetermineContent1();
            content1 = File.ReadAllBytes(content1_file);
        }

        public void SaveContent1()
        {
            File.WriteAllBytes(content1_file, content1);
            Global.PrepareContent1();
        }

        public void Op_FixCrashes()
        {
            for (int i = 0; i < content1.Length - 24; i++)
            {
                {
                    /* 
                    if (content1[i] == 0x38
                     && content1[i + 1] == 0x21
                     && content1[i + 2] == 0x00
                     && content1[i + 3] == 0x10
                     && content1[i + 4] == 0x4E
                     && content1[i + 5] == 0x80
                     && content1[i + 6] == 0x00
                     && content1[i + 7] == 0x20
                     && content1[i + 8] == 0x94
                     && content1[i + 9] == 0x21
                     && content1[i + 10] == 0xFF
                     && content1[i + 11] == 0xF0)*/
                }

                // 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23

                if (content1[i] == 0x4E
                 && content1[i + 1] == 0x80
                 && content1[i + 2] == 0x00
                 && content1[i + 3] == 0x20
                 && content1[i + 4] == 0x94
                 && content1[i + 5] == 0x21
                 && content1[i + 6] == 0xFF
                 && content1[i + 7] == 0xF0
                 && content1[i + 20] == 0x93
                 && content1[i + 21] == 0xE1
                 && content1[i + 22] == 0x00
                 && content1[i + 23] == 0x0C)
                {
                    content1[i] = 0x48;
                    content1[i + 1] = 0x00;
                    content1[i + 2] = 0xD2;
                    content1[i + 3] = 0xF0;
                }
            }

            for (int i = 0; i < content1.Length - 14; i++)
            {
                if (content1[i - 1] == 0x21
                 && content1[i] == 0x38
                 && content1[i + 1] == 0x00
                 && content1[i + 2] == 0x00
                 && content1[i + 3] == 0x01
                 && content1[i + 4] == 0x38
                 && content1[i + 5] == 0x63
                 && content1[i + 6] == 0xB9
                 && content1[i + 7] == 0xC0
                 && content1[i + 8] == 0x98
                 && content1[i + 9] == 0x03
                 && content1[i + 10] == 0x00
                 && content1[i + 11] == 0x0C
                 && content1[i + 12] == 0x4E)
                {
                    content1[i] = 0x3C;
                    content1[i + 1] = 0x80;
                    content1[i + 2] = 0x81;
                    content1[i + 3] = 0x09;
                    content1[i + 4] = 0x38;
                    content1[i + 5] = 0xA0;
                    content1[i + 6] = 0x00;
                    content1[i + 7] = 0x7F;
                    content1[i + 8] = 0x90;
                    content1[i + 9] = 0xA4;
                    content1[i + 10] = 0x0D;
                    content1[i + 11] = 0x00;
                }
            }
        }

        public void Op_FixBrightness()
        {
            // Method originally reported by @NoobletCheese/@Maeson on GBAtemp.
            for (int i = 0; i < content1.Length - 20; i++)
            {
                if (content1[i] == 0x80
                 && content1[i + 1] == 0x04
                 && content1[i + 2] == 0x00
                 && content1[i + 3] == 0x04
                 && content1[i + 4] == 0x2C
                 && content1[i + 5] == 0x00
                 && content1[i + 6] == 0x00
                 && content1[i + 7] == 0xFF
                 && content1[i + 8] == 0x40
                 && content1[i + 9] == 0x82
                 && content1[i + 10] == 0x00
                 && content1[i + 11] == 0x10
                 && content1[i + 12] == 0x80
                 && content1[i + 13] == 0x04
                 && content1[i + 14] == 0x00
                 && content1[i + 15] == 0x08
                 && content1[i + 16] == 0x2C
                 && content1[i + 17] == 0x00
                 && content1[i + 18] == 0x00
                 && content1[i + 19] == 0xFF)
                {
                    for (int x = i; x > 0; x--)
                    {
                        if (content1[x] == 0x94
                         && content1[x + 1] == 0x21
                         && content1[x + 2] == 0xFF
                         && content1[x + 3] == 0xE0)
                        {
                            content1[x] = 0x4E;
                            content1[x + 1] = 0x80;
                            content1[x + 2] = 0x00;
                            content1[x + 3] = 0x20;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Alternative method of fixing brightness, may not work with some WADs or result in excess brightness
        /// </summary>
        public void Op_FixBrightness_Alt()
        {
            for (int i = 0; i < content1.Length - 20; i++)
            {
                if (content1[i] == 0x00
                 && content1[i + 1] == 0x00
                 && content1[i + 2] == 0x00
                 && content1[i + 3] == 0x07
                 && content1[i + 4] == 0x00
                 && content1[i + 5] == 0x00
                 && content1[i + 6] == 0x00
                 && content1[i + 7] == 0xBE
                 && content1[i + 8] == 0x00
                 && content1[i + 9] == 0x00
                 && content1[i + 10] == 0x00
                 && content1[i + 11] == 0xBE
                 && content1[i + 12] == 0x00
                 && content1[i + 13] == 0x00
                 && content1[i + 14] == 0x00
                 && content1[i + 15] == 0xBE)
                {
                    content1[i + 7] = 0xFF;
                    content1[i + 11] = 0xFF;
                    content1[i + 15] = 0xFF;
                }
            }
        }

        public void Op_ExpansionRAM()
        {
            for (int i = 0; i < content1.Length - 8; i++)
            {
                if ((content1[i] == 0x41 && content1[i + 1] == 0x82 && content1[i + 2] == 0x00 && content1[i + 3] == 0x08)
                 || (content1[i] == 0x48 && content1[i + 1] == 0x00 && content1[i + 2] == 0x00 && content1[i + 3] == 0x64)
                 && content1[i + 4] == 0x3C
                 && content1[i + 5] == 0x80
                 && content1[i + 6] == 0x00
                 && content1[i + 7] == 0x80)
                {
                    content1[i] = 0x60;
                    content1[i + 1] = 0x00;
                    content1[i + 2] = 0x00;
                    content1[i + 3] = 0x00;
                }
            }
            throw new Exception(Program.Language.Get("m012"));
        }

        public void Op_AllocateROM()
        {
            for (int i = 0; i < content1.Length - 10; i++)
            {
                if (content1[i] == 0x44
                 && content1[i + 1] == 0x38
                 && content1[i + 2] == 0x7D
                 && content1[i + 3] == 0x00
                 && content1[i + 4] == 0x1C
                 && content1[i + 5] == 0x3C
                 && content1[i + 6] == 0x80)
                {
                    int offset = i + 5;
                    content1[offset + 2] = 0x73;
                    content1[offset + 3] = 0x10;
                    content1[offset + 6] = 0x03;
                    content1[offset + 7] = 0x10;
                }
            }
            throw new Exception(Program.Language.Get("m015"));
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

            // In Custom Robo V2 & Mario Kart 64 (KOR), a null character follows instead of the regular separator, before a supposed second line string.

            foreach (var item in Directory.GetFiles(Paths.WorkingFolder_Content5))
            {
                if (Path.GetFileName(item).Contains("saveComments_"))
                {
                    var byteArray = File.ReadAllBytes(item);
                    List<byte> newSave = new List<byte>();

                    // Add 64-byte header
                    for (int i = 0; i < 64; i++)
                        newSave.Add(byteArray[i]);

                    for (int i = 0; i < encoding.GetBytes(lines[0]).Length; i++)
                        try { newSave.Add(encoding.GetBytes(lines[0])[i]); } catch { newSave.Add(0x00); }
                    if (emuVersion == "romc-alt")
                        { newSave.Add(0x00); }
                    else foreach (var Byte in separator) newSave.Add(Byte);

                    // Set first-line offset
                    newSave[55] = Convert.ToByte(newSave.Count);
                    newSave[59] = Convert.ToByte(newSave.Count);

                    // Also varying on revision, how the second line field is itself handled.
                    // Where it is empty: In earlier revisions such as Star Fox 64 and Super Mario 64, it is a white space character, otherwise it is null.
                    if (lines.Length == 1 && emuVersion == "rev1") lines = new string[2] { lines[0], " " };

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

        public void ReplaceROM()
        {
            if (File.ReadAllBytes(ROM).Length % 4194304 != 0 && emuVersion.Contains("romc"))
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

            if (emuVersion.Contains("romc"))
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
                if (File.ReadAllBytes(compressedROM).Length > File.ReadAllBytes($"{Paths.WorkingFolder_Content5}romc").Length)
                {
                    File.Delete(compressedROM);
                    throw new Exception(Program.Language.Get("m004"));
                }

                // Copy
                File.Copy(compressedROM, $"{Paths.WorkingFolder_Content5}romc", true);
                File.Delete(compressedROM);
            }
            else
            {
                // Check filesize
                if (File.ReadAllBytes(byteswappedROM).Length > File.ReadAllBytes($"{Paths.WorkingFolder_Content5}rom").Length)
                {
                    File.Delete(byteswappedROM);
                    throw new Exception(Program.Language.Get("m004"));
                }

                // Copy
                File.Copy(byteswappedROM, $"{Paths.WorkingFolder_Content5}rom", true);
                File.Delete(byteswappedROM);
            }
        }
    }
}
