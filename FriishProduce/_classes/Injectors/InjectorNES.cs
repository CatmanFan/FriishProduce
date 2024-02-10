using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using libWiiSharp;

namespace FriishProduce
{
    public class InjectorNES : WiiVCInjector
    {
        public int[] saveTPL_offsets { get; set; }

        public InjectorNES(WAD w, string ROM) : base(w, ROM)
        {
            NeedsMainDOL = true;
            Load();
            ReplaceManual();
        }

        /// <summary>
        /// Inserts ROM into main.dol.
        /// </summary>
        public override void ReplaceROM()
        {
            // -----------------------
            // Check for "NES" header
            // -----------------------
            int offset = 0;
            for (int i = 0; i < Contents[1].Length; i++)
            {
                if (Contents[1][i] == 0x4E
                && Contents[1][i + 1] == 0x45
                && Contents[1][i + 2] == 0x53
                && Contents[1][i + 3] == 0x1A)
                {
                    offset = i;
                    break;
                }
            }

            if (offset == 0) throw new InvalidDataException();

            // -----------------------
            // Check filesize of original ROM and set to variable
            // -----------------------
            int PRG = 16384 * Contents[1][offset + 4];
            int CHR = 8192 * Contents[1][offset + 5];
            int ROMsize = PRG + CHR + 16;

            // -----------------------
            // Check filesize of input ROM
            // Maximum ROM limit allowed: 4 MB
            // -----------------------
            if (ROM.Length > ROMsize)
                throw new Exception(string.Format(Language.Get("Error003"), Math.Round((double)ROMsize / 1024).ToString(), Language.Get("Abbreviation_Kilobytes")));

            // -----------------------
            // Replace original ROM
            // -----------------------
            var targetROM = new byte[ROMsize];
            ROM.CopyTo(targetROM, 0);
            targetROM.CopyTo(Contents[1], offset);
        }

        /// <summary>
        /// Inserts palette into main.dol.
        /// </summary>
        public void InsertPalette(int index)
        {
            int offset = 0;

            string pal = null;
            switch (index)
            {
                case 0:
                    return;
                case 1:
                    pal = "B5 AD 80 13 84 11 9C 0F AC 0D B4 00 AC 00 9C 60 90 E0 81 02 81 00 80 E2 80 AC 80 00 80 00 80 00 D2 94 81 17 A0 1E B4 1A C4 16 CC 0A CC A0 C1 00 AD A0 92 00 89 E0 81 E9 81 B0 88 42 80 00 80 00 FF FF B6 9F C5 FF DD DF F5 DF FD B7 FE 2D EA 89 DA 87 C3 00 AB 28 A3 30 AB 39 A9 4A 80 00 80 00 FF FF E7 9F E7 3F EF 3F F7 1F FF 3C FF 59 EF 36 EF 94 EB B6 DF B6 DB 97 D3 59 EF 7B 80 00 80 00";
                    break;
                case 2:
                    pal = "B9 CE 90 71 80 15 A0 13 C4 0E D4 02 D0 00 BC 20 A0 A0 81 00 81 40 80 E2 8C EB 80 00 80 00 80 00 DE F7 81 DD 90 FD C0 1E DC 17 F0 0B EC A0 E5 21 C5 C0 82 40 82 A0 82 47 82 11 88 42 80 00 80 00 FF FF 9E FF AE 5F D2 3F F9 FF FD D6 FD CC FE 67 FA E7 C3 42 A7 69 AF F3 83 BB 9C E7 80 00 80 00 FF FF D7 9F E3 5F EB 3F FF 1F FF 1B FE F6 FF 75 FF 94 F3 F4 D7 D7 DB F9 CF FE C6 31 80 00 80 00";
                    break;
                case 3:
                    pal = "B1 8C 80 11 8C 33 98 4F A8 4C AC 02 A8 20 9C 81 90 C1 85 01 89 02 80 E3 80 AA 80 00 80 00 80 00 D6 B5 85 38 A4 9B B4 59 C8 55 CC 69 C8 C0 B9 40 AD A2 89 E2 8A 01 89 C9 8D 92 80 00 80 00 80 00 FF FF B2 7F C5 FF D9 BF ED BE F1 D5 F2 0B E6 64 D6 C0 BB 00 AF 29 9B 11 A6 F9 A1 08 80 00 80 00 FF FF DF 5F E7 3F EF 1F F7 1F FF 1C FB 38 F3 34 EF 73 E7 93 DF 97 DB B9 DB 9D D6 B5 80 00 80 00";
                    break;
                case 4:
                    pal = "B5 AD 94 53 94 35 AC 72 C4 8E CC 66 C8 C1 B9 00 A1 40 9D 81 9D 81 99 68 99 2E 80 00 80 00 80 00 D2 94 9D 5A 98 DB BC D9 DC D4 E0 CA E1 00 D1 60 BD E0 A2 20 A2 41 A2 2B 99 F2 B5 AD 80 00 80 00 EF 7B AE 3B A1 DB C5 9B E5 5B ED 52 ED A6 EE 20 E2 C3 C7 00 B3 27 AF 10 AA D7 D2 94 80 00 80 00 EF 7B C2 FB C6 9B D6 7B E2 7B EE 78 EE B3 EF 10 EF 2F DB 4E C7 51 C7 76 C7 79 EF 7B 80 00 80 00";
                    break;
                case 5:
                    pal = "B1 8C 80 B1 88 54 9C 14 AC 0F B4 08 B4 00 A8 60 98 C0 85 20 81 40 81 21 81 09 80 00 80 00 80 00 D6 B5 89 7B A1 1F B8 9F D0 79 D8 6F D8 C4 CD 20 B5 A0 9E 00 86 40 82 26 81 F1 80 00 80 00 80 00 FF FF B2 DF CA 5F E1 DF F9 BF FD B9 FE 0E F6 64 DE E0 C7 60 AF 86 A3 90 A7 3B A5 29 80 00 80 00 FF FF E3 7F EB 5F F7 3F FF 1F FF 1D FF 38 FB 74 F3 92 E7 B2 DF D5 DB D9 DB BE DE F7 80 00 80 00";
                    break;
                case 6:
                    pal = "B5 AD 80 92 80 1B B5 3B C8 0D D8 0D D8 80 C9 20 B5 20 91 20 81 A4 82 40 81 29 80 00 80 00 80 00 DA D6 81 BB 81 3F C8 1F D8 1F FC 12 FC 00 ED A0 C9 A0 92 40 82 40 82 CD 82 52 90 84 80 00 80 00 FF FF B6 DF CA 5F ED BF FC 1F FD BF FE 40 FE C0 EF 60 B7 60 83 E0 A7 FB 83 FF A5 29 80 00 80 00 FF FF DB 7F EE DF FE DF FE 5F FE D6 FF 72 FF E9 FF ED DB E9 CB ED A7 FB CB 7F CA 52 80 00 80 00";
                    break;
                case 7:
                    pal = "B9 CE 90 71 80 15 A0 13 C4 0E D4 02 D0 00 BC 20 A0 A0 81 00 81 40 80 E2 8C EB 80 00 80 00 80 00 DE F7 81 DD 90 FD C0 1E DC 17 F0 0B EC A0 E5 21 C5 C0 82 40 82 A0 82 47 82 11 80 00 80 00 80 00 FF FF 9E FF AE 5F E6 3F F9 FF FD D6 FD CC FE 67 FA E7 C3 42 A7 69 AF F3 83 BB BD EF 80 00 80 00 FF FF D7 9F E3 5F EB 3F FF 1F FF 1B FE F6 FF 75 FF 94 F3 F4 D7 D7 DB F9 CF FE E3 18 80 00 80 00";
                    break;
                case 8:
                    pal = "B5 AD 80 71 90 13 A0 11 B0 0C B0 03 AC 20 A4 40 94 C0 81 00 81 21 81 03 80 CB 80 00 80 00 80 00 DA D6 89 5A A0 DD B8 9B CC 77 D4 2C D4 A0 C5 20 B1 A0 92 00 82 20 82 08 81 D2 80 00 80 00 80 00 FF FF B2 DF BE 7F E1 FF F5 DF F9 D9 FA 2D EE 85 DE E1 C7 41 AF 67 A7 70 A7 3A A9 4A 80 00 80 00 FF FF DF 7F EB 5F F3 3F F7 1F FF 1C FF 38 FB 75 F7 94 EB B4 DF D6 DB D9 DB BE DE F7 80 00 80 00";
                    break;
                case 9:
                    pal = "B1 8C 80 4F 8C 11 98 10 A8 0B AC 03 A4 00 9C 60 8C C0 80 E0 81 00 80 E2 80 AA 80 00 80 00 80 00 D6 B5 8D 39 A0 BC B4 7A C8 75 CC 6B CC C0 BD 20 AD 80 91 E0 82 00 81 E7 81 B1 80 00 80 00 80 00 FF FF B2 BF C6 3F D9 DF F1 BF F5 B8 FA 0D EE 65 DE C1 C3 21 AF 47 A7 4F A7 19 A5 29 80 00 80 00 FF FF E3 9F EF 7F F7 5F FF 3F FF 3E FF 59 FF 76 F7 B4 EB D4 E3 F7 DF DA DF DE DE F7 80 00 80 00";
                    break;
            }

            if (pal != null)
            {
                // Search for palette header identifier
                for (int i = 0; i < Contents[1].Length; i++)
                {
                    if (Contents[1][i] == 0x42
                     && Contents[1][i + 1] == 0x59
                     && Contents[1][i + 2] == 0x21
                     && Contents[1][i + 3] == 0xC8
                     && Contents[1][i + 4] == 0x0D
                     && Contents[1][i + 5] == 0x53
                     && Contents[1][i + 6] == 0x41
                     && Contents[1][i + 7] == 0x54
                     && Contents[1][i + 8] == 0x00
                     && Contents[1][i + 9] == 0x00
                     && Contents[1][i + 10] == 0x00
                     && Contents[1][i + 11] == 0x00
                     && Contents[1][i + 12] == 0x00
                     && Contents[1][i + 13] == 0x00
                     && Contents[1][i + 14] == 0x00
                     && Contents[1][i + 15] == 0x00)
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

                    palBytes.CopyTo(Contents[1], offset);
                }
            }
        }

        // ---------- SAVEDATA-RELATED FUNCTIONS ---------- //

        /// <summary>
        /// Searches for offsets of the TPL embedded within main.dol so it is able to be extracted properly.
        /// </summary>
        public int[] DetermineSaveTPLOffsets()
        {
            int[] offsets = new int[2];

            for (int i = Contents[1].Length - 20; i > 0; i--)
            {
                if (Contents[1][i] == 0xA2
                    && Contents[1][i + 1] == 0xDB
                    && Contents[1][i + 2] == 0xA2
                    && Contents[1][i + 3] == 0xDB
                    && Contents[1][i + 4] == 0xA2
                    && Contents[1][i + 5] == 0xDB
                    && Contents[1][i + 6] == 0xA2
                    && Contents[1][i + 7] == 0xDB
                    && Contents[1][i + 8] == 0xA2
                    && Contents[1][i + 9] == 0xDB
                    && Contents[1][i + 10] == 0xA2
                    && Contents[1][i + 11] == 0xDB
                    && Contents[1][i + 12] == 0xA2
                    && Contents[1][i + 13] == 0xDB
                    && Contents[1][i + 14] == 0xA2
                    && Contents[1][i + 15] == 0xDB)
                {
                    offsets[1] = i + 16;
                    for (int x = offsets[1]; x > 0; x--)
                    {
                        if (Contents[1][x] == 0x00
                            && Contents[1][x + 1] == 0x20
                            && Contents[1][x + 2] == 0xAF
                            && Contents[1][x + 3] == 0x30
                            && Contents[1][x + 4] == 0x00
                            && Contents[1][x + 5] == 0x00
                            && Contents[1][x + 6] == 0x00
                            && Contents[1][x + 7] == 0x05
                            && Contents[1][x + 8] == 0x00
                            && Contents[1][x + 9] == 0x00
                            && Contents[1][x + 10] == 0x00
                            && Contents[1][x + 11] == 0x0C
                            && Contents[1][x + 12] == 0x00
                            && Contents[1][x + 13] == 0x00
                            && Contents[1][x + 14] == 0x00
                            && Contents[1][x + 15] == 0x34)
                            offsets[0] = x;
                    }
                    break;
                }
            }

            return offsets;
        }

        /// <summary>
        /// Inserts custom savedata text string & TPL file into main.dol. The function skips TPL replacement if the file doesn't exist or the offsets are not set properly.
        /// </summary>
        /// <param name="lines">Text string array</param>
        /// <param name="tImg">Input title image</param>
        public override void ReplaceSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // TEXT
            // -----------------------

            saveTPL_offsets = DetermineSaveTPLOffsets();
            string text = lines.Length > 1 ? string.Join("\n", lines) : lines[0];

            // In the two WADs I've tested (SMB3 & Kirby's Adventure), the savedata text is found near
            // the string "VirtualIF.c MEM1 heap allocation error" within the content1 file
            // This function will try to read for the index of the string by going backwards from end-of-file
            int start;
            int end = 0;

            for (int i = Contents[1].Length - 1; i > 0; i--)
            {
                if (Contents[1][i] == 0x56
                 && Contents[1][i + 1] == 0x69
                 && Contents[1][i + 2] == 0x72
                 && Contents[1][i + 3] == 0x74
                 && Contents[1][i + 4] == 0x75
                 && Contents[1][i + 5] == 0x61
                 && Contents[1][i + 6] == 0x6C
                 && Contents[1][i + 7] == 0x49
                 && Contents[1][i + 8] == 0x46)
                    end = i;
            }

            // In both aforementioned WADs the savetitle text must not be bigger than what the content1 can contain.
            // If trying to increase or decrease the filesize it breaks the WAD

            // Text addition format: UTF-16 (Big Endian)
            if (end != 0)
            {
                start = saveTPL_offsets[1] > 100 ? saveTPL_offsets[1] : end - 40;
                for (int i = start; i < end; i++)
                {
                    try { Contents[1][i] = Encoding.BigEndianUnicode.GetBytes(text)[i - start]; }
                    catch { Contents[1][i] = 0x00; }
                }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            if (saveTPL_offsets[0] != 0)
            {
                var TPL = new byte[1];

                // ----------------------------------------------------------------
                // Extracts savedata TPL from main.dol if offsets have been found.
                // ----------------------------------------------------------------
                if (saveTPL_offsets[0] != 0 && saveTPL_offsets[1] != 0)
                {
                    var tplList = new List<byte>();
                    for (int i = saveTPL_offsets[0]; i < saveTPL_offsets[1]; i++)
                        tplList.Add(Contents[1][i]);
                    TPL = tplList.ToArray();
                }
                if (TPL.Length < 5) return;

                // ----------------------------------------------------------------
                // Replace TPL
                // ----------------------------------------------------------------
                var TPLnew = tImg.CreateSaveTPL(Console.NES, TPL).ToByteArray();

                for (int i = saveTPL_offsets[0]; i < saveTPL_offsets[1]; i++)
                    Contents[1][i] = TPLnew[i - saveTPL_offsets[0]];
            }
        }
    }
}