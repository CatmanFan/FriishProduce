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

        private readonly string byteswappedROM = $"{Paths.Apps}ucon64\\rom.z64";
        private readonly string compressedROM = $"{Paths.WorkingFolder}romc";


        public void FixBrightness(string content1_file)
        {
            // Method originally reported by @NoobletCheese/@Maeson on GBAtemp:
            // Here is a magical string that should work for all N64 games.
            // 80 04 00 04 2C 00 00 FF 40 82 00 10 80 04 00 08 2C 00 00 FF
            // Then walk backwards until you find the
            // 94 21 FF E0
            // and replace with
            // 4E 80 00 20

            byte[] content1 = File.ReadAllBytes(content1_file);

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
                    int offset = i;
                    for (int x = offset; x > 0; x--)
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

                            File.WriteAllBytes(content1_file, content1);
                        }
                    }
                }
            }
        }

        public void InsertSaveComments(string[] lines)
        {
            // The separator byte array for double-lines.
            // This varies depending on the revision of the emulator, and the second separator is often cut off by end-of-file in earlier releases
            byte[] separator =
                emuVersion == "rev1" ? new byte[] { 0x00, 0xBB, 0xBB, 0xBB } :
                emuVersion == "rev1-alt" ? new byte[] { 0x00, 0xBB } :
                new byte[] { 0x00, 0xBB, 0xBB };

            // In Custom Robo v2, there is no difference between the present saveComments_jp and saveComments_en, aside from the language code.
            // These are formatted differently from other ROMC channels, mainly the title string starts at 0x40, and two null characters follow
            // instead of the regular separator, before a supposed second line string.

            // Text addition format: UTF-16 (Little Endian)
            foreach (var item in Directory.GetFiles(Paths.WorkingFolder_Content5))
            {
                if (Path.GetFileName(item).Contains("saveComments_"))
                {
                    var byteArray = File.ReadAllBytes(item);
                    List<byte> newSave = new List<byte>();
                    int headerEnd = (emuVersion.StartsWith("rev1") || emuVersion == "romc-crv2") ? 64 : 65;

                    for (int i = 0; i < headerEnd; i++)
                        newSave.Add(byteArray[i]);

                    for (int i = 0; i < lines[0].Length; i++)
                        try { newSave.Add(Encoding.Unicode.GetBytes(lines[0])[i]); } catch { newSave.Add(0x00); }

                    if (emuVersion == "romc-crv2")
                        { newSave.Add(0x00); newSave.Add(0x00); }
                    else foreach (var Byte in separator) newSave.Add(Byte);

                    newSave[55] = Convert.ToByte(newSave.Count);
                    newSave[59] = Convert.ToByte(newSave.Count);

                    // Also varying on revision, how the second line field is itself handled.
                    // Where it is empty: In Star Fox 64 and Super Mario 64, it is a white space character, in others it is set to null.
                    // In later revisions (e.g. S&P), a null byte also follows the first separator within the file, before the second line text's offset.
                    if (lines.Length == 1 && emuVersion == "rev1") lines = new string[2] { lines[0], " " };
                    if (emuVersion != "rev1") newSave.Add(0x00);

                    if (lines.Length == 2)
                        for (int i = 0; i < lines[1].Length; i++)
                            try { newSave.Add(Encoding.Unicode.GetBytes(lines[1])[i]); } catch { newSave.Add(0x00); }

                    foreach (var Byte in separator) newSave.Add(Byte);

                    // Character count determiner within savedata file
                    newSave[45] = Convert.ToByte(Encoding.Unicode.GetBytes(lines[0]).Length);
                    newSave[47] = Convert.ToByte(Encoding.Unicode.GetBytes(lines[0]).Length);
                    newSave[61] = lines.Length == 2 ? Convert.ToByte(Encoding.Unicode.GetBytes(lines[1]).Length) : (byte)0x00;
                    newSave[63] = lines.Length == 2 ? Convert.ToByte(Encoding.Unicode.GetBytes(lines[1]).Length) : (byte)0x00;

                    File.WriteAllBytes(item, newSave.ToArray());
                }
            }
        }

        public void ReplaceROM()
        {
            if (File.ReadAllBytes(ROM).Length % 4194304 != 0 && emuVersion.Contains("romc"))
                throw new Exception("The ROM cannot be compressed because it is not a multiple of 4 MB (4194304 bytes).");

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
                throw new Exception("The ROM byteswapping process failed.");

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
                    throw new Exception("The ROMC compression process failed.");

                // Check filesize
                if (File.ReadAllBytes(compressedROM).Length > File.ReadAllBytes($"{Paths.WorkingFolder_Content5}romc").Length)
                {
                    File.Delete(compressedROM);
                    throw new Exception(strings.error_ROMtoobig);
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
                    throw new Exception(strings.error_ROMtoobig);
                }

                // Copy
                File.Copy(byteswappedROM, $"{Paths.WorkingFolder_Content5}rom", true);
                File.Delete(byteswappedROM);
            }
        }
    }
}
