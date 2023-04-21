using System;
using System.IO;

namespace FriishProduce.Injectors
{
    public class NES
    {
        public string ROM { get; set; }
        public string content1_file { get; set; }


        public void InsertROM()
        {
            int offset = 0;
            byte[] content1 = File.ReadAllBytes(Paths.WorkingFolder + "00000001.app");

            // Search for "NES" header
            for (int i = 0; i < content1.Length; i++)
            {
                if (content1[i] == 0x4E && content1[i + 1] == 0x45 && content1[i + 2] == 0x53 && content1[i + 3] == 0x1A)
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
                    throw new Exception("The ROM to be injected is larger in filesize than the target ROM.");
                File.ReadAllBytes(ROM).CopyTo(targetROM, 0);
                targetROM.CopyTo(content1, offset);
            }

            File.WriteAllBytes(Paths.WorkingFolder + "00000001.app", content1);
        }

        public void InsertPalette(int index)
        {
            int offset = 0;
            byte[] content1 = File.ReadAllBytes(Paths.WorkingFolder + "00000001.app");

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
                }

                File.WriteAllBytes(Paths.WorkingFolder + "00000001.app", content1);
            }
        }
    }
}
