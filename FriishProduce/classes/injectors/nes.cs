using System;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce.Injectors
{
    public class NES
    {
        public string ROM { get; set; }
        public string content1_file { get; set; }
        public int[] saveTPL_offsets { get; set; }

        public void InsertROM()
        {
            int offset = 0;
            byte[] content1 = File.ReadAllBytes(content1_file);

            // Search for "NES" header
            for (int i = 0; i < content1.Length; i++)
            {
                if (content1[i] == 0x4E
                && content1[i + 1] == 0x45
                && content1[i + 2] == 0x53
                && content1[i + 3] == 0x1A)
                {
                    offset = i;
                    break;
                }
            }

            if (offset != 0)
            {
                int ROMsize = 16;
                ROMsize += 16 * 1024 * content1[offset + 4]; // PRG
                ROMsize += 8 * 1024 * content1[offset + 5]; // CHR

                var targetROM = new byte[ROMsize];
                var inputROM = File.ReadAllBytes(ROM);
                if (inputROM.Length > ROMsize)
                    throw new Exception(strings.error_ROMtoobig);
                File.ReadAllBytes(ROM).CopyTo(targetROM, 0);
                targetROM.CopyTo(content1, offset);

                File.WriteAllBytes(content1_file, content1);
            }
        }

        public void InsertPalette(int index)
        {
            int offset = 0;
            byte[] content1 = File.ReadAllBytes(content1_file);

            string pal = null;
            switch (index)
            {
                case 1: // Remove dark filter
                    pal = "B5 AD 80 13 84 11 9C 0F AC 0D B4 00 AC 00 9C 60 90 E0 81 02 81 00 80 E2 80 AC 80 00 80 00 80 00 D2 94 81 17 A0 1E B4 1A C4 16 CC 0A CC A0 C1 00 AD A0 92 00 89 E0 81 E9 81 B0 88 42 80 00 80 00 FF FF B6 9F C5 FF DD DF F5 DF FD B7 FE 2D EA 89 DA 87 C3 00 AB 28 A3 30 AB 39 A9 4A 80 00 80 00 FF FF E7 9F E7 3F EF 3F F7 1F FF 3C FF 59 EF 36 EF 94 EB B6 DF B6 DB 97 D3 59 EF 7B 80 00 80 00";
                    break;
            }

            if (pal != null)
            {
                // Search for palette header identifier
                for (int i = 0; i < content1.Length; i++)
                {
                    if (content1[i]      == 0x42
                     && content1[i + 1]  == 0x59
                     && content1[i + 2]  == 0x21
                     && content1[i + 3]  == 0xC8
                     && content1[i + 4]  == 0x0D
                     && content1[i + 5]  == 0x53
                     && content1[i + 6]  == 0x41
                     && content1[i + 7]  == 0x54
                     && content1[i + 8]  == 0x00
                     && content1[i + 9]  == 0x00
                     && content1[i + 10] == 0x00
                     && content1[i + 11] == 0x00
                     && content1[i + 12] == 0x00
                     && content1[i + 13] == 0x00
                     && content1[i + 14] == 0x00
                     && content1[i + 15] == 0x00)
                    {
                        offset = i + 16;
                        break;
                    }
                }

                if (offset != 0)
                {
                    // Convert palette to bytes
                    var palBytes = new byte[128];
                    var palStringArray = pal.Split(' ');
                    for (int i = 0; i < 128; i++)
                        palBytes[i] = Convert.ToByte(palStringArray[i], 16);

                    palBytes.CopyTo(content1, offset);

                    File.WriteAllBytes(content1_file, content1);
                }
            }
        }

        public int[] DetermineSaveTPLOffsets()
        {
            byte[] content1 = File.ReadAllBytes(content1_file);
            int[] offsets = new int[2];

            for (int i = content1.Length - 20; i > 0; i--)
            {
                if (content1[i] == 0xA2
                 && content1[i + 1] == 0xDB
                 && content1[i + 2] == 0xA2
                 && content1[i + 3] == 0xDB
                 && content1[i + 4] == 0xA2
                 && content1[i + 5] == 0xDB
                 && content1[i + 6] == 0xA2
                 && content1[i + 7] == 0xDB
                 && content1[i + 8] == 0xA2
                 && content1[i + 9] == 0xDB
                 && content1[i + 10] == 0xA2
                 && content1[i + 11] == 0xDB
                 && content1[i + 12] == 0xA2
                 && content1[i + 13] == 0xDB
                 && content1[i + 14] == 0xA2
                 && content1[i + 15] == 0xDB)
                {
                    offsets[1] = i + 16;
                    for (int x = offsets[1]; x > 0; x--)
                    {
                        if (content1[x] == 0x00
                         && content1[x + 1] == 0x20
                         && content1[x + 2] == 0xAF
                         && content1[x + 3] == 0x30
                         && content1[x + 4] == 0x00
                         && content1[x + 5] == 0x00
                         && content1[x + 6] == 0x00
                         && content1[x + 7] == 0x05
                         && content1[x + 8] == 0x00
                         && content1[x + 9] == 0x00
                         && content1[x + 10] == 0x00
                         && content1[x + 11] == 0x0C
                         && content1[x + 12] == 0x00
                         && content1[x + 13] == 0x00
                         && content1[x + 14] == 0x00
                         && content1[x + 15] == 0x34)
                            offsets[0] = x;
                    }
                    break;
                }
            }

            return offsets;
        }

        public void ExtractSaveTPL(string out_file)
        {
            byte[] content1 = File.ReadAllBytes(content1_file);
            var tpl = new List<byte>();

            if (saveTPL_offsets[0] != 0 && saveTPL_offsets[1] != 0)
            {
                for (int i = saveTPL_offsets[0]; i < saveTPL_offsets[1]; i++)
                    tpl.Add(content1[i]);
                File.WriteAllBytes(out_file, tpl.ToArray());
            }
        }

        public void InsertSaveData(string text, string inputTPL)
        {
            // In the two WADs I've tested (SMB3 & Kirby's Adventure), the savedata text is found near
            // the string "VirtualIF.c MEM1 heap allocation error" within the content1 file
            // This function will try to read for the index of the string by going backwards from end-of-file

            byte[] content1 = File.ReadAllBytes(content1_file);
            int start = 0;
            int end = 0;

            for (int i = content1.Length - 1; i > 0; i--)
            {
                if (content1[i] == 0x56
                 && content1[i + 1] == 0x69
                 && content1[i + 2] == 0x72
                 && content1[i + 3] == 0x74
                 && content1[i + 4] == 0x75
                 && content1[i + 5] == 0x61
                 && content1[i + 6] == 0x6C
                 && content1[i + 7] == 0x49
                 && content1[i + 8] == 0x46)
                    end = i;
            }

            if (end == 0) return;

            for (int i = end; i > 0; i--)
                if (content1[i - 1] == 0x00 && content1[i - 2] == 0xDB)
                {
                    start = i;
                    break;
                }

            if (start != 0 && end != 0)
            {
                for (int i = start; i < end; i += 2)
                {
                    try { content1[i] = Convert.ToByte(text[(i - start) / 2]); }
                    catch { content1[i] = 0x00; }
                }
            }

            if (saveTPL_offsets[0] != 0 && File.Exists(inputTPL))
            {
                var inputTPL_bytes = File.ReadAllBytes(inputTPL);
                for (int i = saveTPL_offsets[0]; i < saveTPL_offsets[1]; i++)
                    content1[i] = inputTPL_bytes[i - saveTPL_offsets[0]];
                File.Delete(inputTPL);
            }

            File.WriteAllBytes(content1_file, content1);
        }
    }
}
