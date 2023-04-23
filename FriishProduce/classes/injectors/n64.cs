using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using libWiiSharp;

namespace FriishProduce.Injectors
{
    public class N64
    {
        public string ROM { get; set; }
        public string emuVersion { get; set; }

        private readonly string byteswappedROM = $"{Paths.Apps}ucon64\\rom.z64";
        private readonly string compressedROM = $"{Paths.Apps}ucon64\\romc";

        public void RemoveDarkFilter()
        {
            // Here is a magical string that should work for all N64 games.
            // 80 04 00 04 2C 00 00 FF 40 82 00 10 80 04 00 08 2C 00 00 FF
            // Then walk backwards until you find the
            // 94 21 FF E0
            // and replace with
            // 4E 80 00 20
        }

        public void InsertSaveComments(string[] lines)
        {
            if (emuVersion == "romc-crv2") return; // NOT IMPLEMENTED yet for CRobov2

            // The separator byte array for double-lines.
            // This varies depending on the revision of the emulator, and the second separator is often cut off by end-of-file in earlier releases
            byte[] separator = emuVersion == "rev1" ? new byte[] { 0x00, 0xBB, 0xBB, 0xBB } : new byte[] { 0x00, 0xBB, 0xBB };

            // In Custom Robo v2, there is no difference between the present saveComments_jp and saveComments_en, aside from the language code.
            // These are formatted differently from other ROMC channels, mainly the title string starts at 0x40, and two null characters follow
            // instead of the regular separator, before a supposed second line string.

            // Text addition is based on UTF-16 (Big Endian), with null spaces in between each character
            foreach (var item in Directory.GetFiles(Paths.WorkingFolder_Content5))
            {
                if (Path.GetFileName(item).Contains("saveComments_"))
                {
                    var byteArray = File.ReadAllBytes(item);
                    List<byte> newSave = new List<byte>();
                    int headerEnd = emuVersion == "rev1" ? 64 : 65;

                    for (int i = 0; i < headerEnd; i++)
                        newSave.Add(byteArray[i]);

                    for (int i = 0; i < lines[0].Length; i++)
                    {
                        try { newSave.Add(Convert.ToByte(lines[0][i])); }
                        catch { newSave.Add(0x00); }
                        finally { if (emuVersion != "romc-crv2") newSave.Add(0x00); }
                    }

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
                        {
                            try { newSave.Add(Convert.ToByte(lines[1][i])); }
                            catch { newSave.Add(0x00); }
                            finally { if (emuVersion != "romc-crv2") newSave.Add(0x00); }
                        }

                    foreach (var Byte in separator) newSave.Add(Byte);

                    // Character count determiner within savedata file
                    var spacing = emuVersion != "romc-crv2" ? 2 : 1;
                    newSave[45] = Convert.ToByte(lines[0].Length * spacing);
                    newSave[47] = Convert.ToByte(lines[0].Length * spacing);
                    newSave[61] = lines.Length == 2 ? Convert.ToByte(lines[1].Length * spacing) : (byte)0x00;
                    newSave[63] = lines.Length == 2 ? Convert.ToByte(lines[1].Length * spacing) : (byte)0x00;

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

                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = $"{Paths.Apps}romc.exe",
                    WorkingDirectory = Paths.Apps,
                    Arguments = $"e ucon64\\rom.z64 ucon64\\romc",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(byteswappedROM);

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
