using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libWiiSharp;
using Ionic.Zip;

namespace FriishProduce.WiiVC
{
    public class NeoGeo : InjectorWiiVC
    {
        public string BIOSPath { get; set; }

        private List<byte> P { get; set; }
        private List<byte> M { get; set; }
        private List<byte> V { get; set; }
        private List<byte> S { get; set; }
        private List<byte> C { get; set; }
        private List<byte> BIOS { get; set; }

        private ZipFile ZIP { get; set; }

        private U8 ManualIndex { get; set; }

        protected override void Load()
        {
            NeedsManualLoaded = false;
            base.Load();

            // Game.bin may be stored on either 00000006.app or 00000005.app.
            // In all cases the manual files are stored also on 00000005.app.
            // ****************
            if (Contents.Count >= 7)
                MainContentIndex = WAD.Contents[6].Length > WAD.Contents[5].Length ? 6 : 5;
            else MainContentIndex = 5;

            MainContent = U8.Load(WAD.Contents[MainContentIndex]);

            if (Contents.Count >= 7) ManualIndex = U8.Load(WAD.Contents[5]);
            ReplaceManual(Contents.Count >= 7 ? ManualIndex : MainContent);

            ZIP = ZipFile.Read(new MemoryStream(ROM.Bytes));
        }

        /// <summary>
        /// Gets a specific file from the archive and extracts it to a byte array via a memory stream.
        /// </summary>
        private byte[] ExtractToByteArray(string file)
        {
            MemoryStream data = new MemoryStream();
            ZIP[file].Extract(data);
            data.Seek(0, SeekOrigin.Begin);
            return data.ToArray();
        }

        private byte[] ExtractToByteArray(ZipEntry item) => ExtractToByteArray(item.FileName);

        /// <summary>
        /// Replaces ROM within extracted content5 directory. (compressed formats not supported yet)
        /// </summary>
        protected override void ReplaceROM()
        {
            if (EmuType == 1) throw new Exception("ZLIB WADs not supported yet!");

            // ------------------------- //

            // TO-DO:
            // "game.bin.z" (found in KOF '95 WAD) is the game compressed in ZLIB format.
            // This should be decrypted to "game.bin" to avoid any IO exception then recompressed afterwards

            // ------------------------- //

            // TO-DO: Do byteswap for each byte with BIOS
            // ****************
            BIOS = new List<byte>();

            if (string.IsNullOrWhiteSpace(BIOSPath) || !File.Exists(BIOSPath))
            {
                goto RetrieveOrigBIOS;
            }

            else
            {
                if (Path.GetExtension(BIOSPath).ToLower() == ".rom")
                {
                    BIOS.AddRange(File.ReadAllBytes(BIOSPath));
                }

                else if (Path.GetExtension(BIOSPath).ToLower() == ".zip")
                {
                    foreach (ZipEntry item in ZIP.Entries)
                        if (Path.GetExtension(item.FileName).ToLower() == ".rom")
                            BIOS.AddRange(ExtractToByteArray(item));

                    if (BIOS.Count < 10) goto RetrieveOrigBIOS;
                }

                else goto RetrieveOrigBIOS;

                for (int i = 1; i < BIOS.Count; i += 2)
                {
                    (BIOS[i - 1], BIOS[i]) = (BIOS[i], BIOS[i - 1]);
                }

                goto Next;
            }

        RetrieveOrigBIOS:
            BIOS.Clear();

            // Get BIOS directly from game.bin
            // ****************
            if (EmuType < 1) // i.e. not Zlib
            {
                var orig = MainContent.Data[MainContent.GetNodeIndex("game.bin")];
                BIOS.AddRange(orig.Skip(orig.Length - 131072).Take(131072));
            }
            else throw new FileNotFoundException();

            goto Next;

        Next:
            // ------------------------- //

            P = new List<byte>();
            M = new List<byte>();
            V = new List<byte>();
            S = new List<byte>();
            C = new List<byte>();

            List<string> FileList = new List<string>();
            
            foreach (ZipEntry item in ZIP.Entries)
            {
                string filename1 = Path.GetFileNameWithoutExtension(item.FileName).ToLower();
                string filename2 = item.FileName.ToLower();

                if (filename1[filename1.Length - 2] == 'p')
                    FileList.Add(filename1);

                if (filename2[filename2.Length - 2] == 'p')
                    FileList.Add(filename2);
            }

            FileList = FileList.Distinct().ToList();

            foreach (var item in FileList)
            {
                var b = ExtractToByteArray(item);

                bool P1_2MB = b.Length == 2097152 && item.Contains("p1");
                if (P1_2MB)
                    for (int i = 0; i < b.Length / 2; i++) (b[i + (b.Length / 2)], b[i]) = (b[i], b[i + (b.Length / 2)]);

                P.AddRange(b);
            }

            for (int i = 1; i < P.Count; i += 2) (P[i - 1], P[i]) = (P[i], P[i - 1]);

            // ------------------------- //

            foreach (ZipEntry item in ZIP.Entries)
            {
                string filename = item.FileName.ToLower();
                if (filename[filename.Length - 2] == 'm')
                    M.AddRange(ExtractToByteArray(item));
            }

            if (M.Count == 0)
                foreach (ZipEntry item in ZIP.Entries)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.FileName).ToLower();
                    if (filename[filename.Length - 2] == 'm')
                        M.AddRange(ExtractToByteArray(item));
                }

            // ------------------------- //

            foreach (ZipEntry item in ZIP.Entries)
            {
                string filename = item.FileName.ToLower();
                if (filename[filename.Length - 2] == 'v')
                    V.AddRange(ExtractToByteArray(item));
            }

            if (V.Count == 0)
                foreach (ZipEntry item in ZIP.Entries)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.FileName).ToLower();
                    if (filename[filename.Length - 2] == 'v')
                        V.AddRange(ExtractToByteArray(item));
                }

            // ------------------------- //

            foreach (ZipEntry item in ZIP.Entries)
            {
                string filename = item.FileName.ToLower();
                if (filename[filename.Length - 2] == 's')
                    S.AddRange(ExtractToByteArray(item));
            }

            if (S.Count == 0)
                foreach (ZipEntry item in ZIP.Entries)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.FileName).ToLower();
                    if (filename[filename.Length - 2] == 's')
                        S.AddRange(ExtractToByteArray(item));
                }

            // ------------------------- //

            int C_count = 0;

            foreach (ZipEntry item in ZIP.Entries)
            {
                string filename = item.FileName.ToLower();
                if (filename[filename.Length - 2] == 'c')
                    C_count++;
            }

            if (C_count == 0)
                foreach (ZipEntry item in ZIP.Entries)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.FileName).ToLower();
                    if (filename[filename.Length - 2] == 'c')
                        C_count++;
                }

            // Documentation from Corsario
            // "There are some games with an non-standard C file order, and after reorganizing the bytes it's necessary to do a unknown puzzle
            // with that files and find the suitable order, each strange game have a custom combination."
            // Ex.: Fatal Fury 2, Kizuna Encounter, KOF '96"

            // Loop
            // ****************
            for (int x = 0; x <= C_count; x += 2)
            {
                var C1 = new byte[1];
                var C2 = new byte[1];

                foreach (ZipEntry item in ZIP.Entries)
                    if (item.FileName.ToLower().EndsWith($"c{x + 1}") || Path.GetFileNameWithoutExtension(item.FileName).ToLower().EndsWith($"c{x + 1}"))
                    {
                        C1 = ExtractToByteArray(item);

                        // Byteswap
                        for (int i = 1; i < C1.Length; i += 2)
                            (C1[i - 1], C1[i]) = (C1[i], C1[i - 1]);
                    }

                foreach (ZipEntry item in ZIP.Entries)
                    if (item.FileName.ToLower().EndsWith($"c{x + 2}") || Path.GetFileNameWithoutExtension(item.FileName).ToLower().EndsWith($"c{x + 2}"))
                    {
                        C2 = ExtractToByteArray(item);

                        // Byteswap
                        for (int i = 1; i < C2.Length; i += 2)
                            (C2[i - 1], C2[i]) = (C2[i], C2[i - 1]);
                    }

                if (C1.Length > 5 && C2.Length > 5)
                {
                    try
                    {
                        var C_temp = new byte[C1.Length + C2.Length];

                        for (int i = 0; i < C_temp.Length / 2; i += 2)
                        {
                            C_temp[2 * i] = C1[i];
                            C_temp[2 * i + 1] = C1[i + 1];
                            C_temp[2 * i + 2] = C2[i];
                            C_temp[2 * i + 3] = C2[i + 1];
                        }

                        C.AddRange(C_temp);
                    }
                    catch { }
                }
            }


            // ------------------------- //

            var C_swap = C.ToArray();
            for (int i = 3; i < C_swap.Length; i += 4)
                (C_swap[i - 3], C_swap[i - 2], C_swap[i - 1], C_swap[i]) = (C_swap[i - 2], C_swap[i], C_swap[i - 3], C_swap[i - 1]);

            // ------------------------- //

            // Header
            // ****************
            var Header = new List<byte>();
            string h = "00000040"
                + P.Count.ToString("X8")
                + (P.Count + 64).ToString("X8")
                + M.Count.ToString("X8")
                + (P.Count + M.Count + 64).ToString("X8")
                + V.Count.ToString("X8") + "00000000000000000000000000000000"
                + (P.Count + M.Count + V.Count + 64).ToString("X8")
                + S.Count.ToString("X8")
                + (P.Count + M.Count + V.Count + S.Count + 64).ToString("X8")
                + C.Count.ToString("X8")
                + (P.Count + M.Count + V.Count + S.Count + C.Count + 64).ToString("X8")
                + BIOS.Count.ToString("X8");

            for (int i = 0; i < h.Length; i += 2)
                Header.Add(Convert.ToByte(h.Substring(i, 2), 16));

            var GameBin = new List<byte>();

            if (EmuType < 1) // i.e. not Zlib
            {
                GameBin.AddRange(Header);
                GameBin.AddRange(P);
                GameBin.AddRange(M);
                GameBin.AddRange(V);
                GameBin.AddRange(S);
                GameBin.AddRange(C_swap);
                GameBin.AddRange(BIOS);
            }

            else
            {
                GameBin.AddRange(Header);
                GameBin.AddRange(S);
                GameBin.AddRange(C_swap);
                GameBin.AddRange(V);
                GameBin.AddRange(BIOS);
                GameBin.AddRange(P);
                GameBin.AddRange(M);
            }

            MainContent.ReplaceFile(MainContent.GetNodeIndex("game.bin"), GameBin.ToArray());

            ZIP.Dispose();

            // ------------------------- //

            // TO-DO:
            // Compress "game.bin.z"

            // ------------------------- //
        }

        protected override void ReplaceSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // TEXT
            // -----------------------

            byte[] contents = null;

            try { contents = MainContent.Data[MainContent.GetNodeIndex("banner.bin")]; } catch { }
            if (contents == null) return;

            // Text addition format: UTF-16 (Big Endian)
            // ****************
            for (int i = 32; i < 96; i++)
            {
                try { contents[i] = Encoding.BigEndianUnicode.GetBytes(lines[0])[i - 32]; }
                catch { contents[i] = 0x00; }
            }

            for (int i = 96; i < 160; i++)
            {
                try { contents[i] = Encoding.BigEndianUnicode.GetBytes(lines[1])[i - 96]; }
                catch { contents[i] = 0x00; }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            // TPL contents in banner.bin does not have TPL header, so it has to be manually added
            // ****************
            var header = new byte[] { 0x00, 0x20, 0xAF, 0x30, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x54, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00,
                                      0x00, 0x00, 0x00, 0x00, 0x00, 0x9C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE4, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x00, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                      0x01, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0xC0, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x01, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00,
                                      0x61, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x30, 0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x73, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00,
                                      0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x85, 0xA0, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x30, 0x00, 0x00,
                                      0x00, 0x05, 0x00, 0x00, 0x97, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0xA9, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0xBB, 0xA0,
                                      0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30,
                                      0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0xCD, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
                                      0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x30, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0xDF, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                      0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var tpl = new List<byte>();

            // Create TPL byte array
            // ****************
            tpl.AddRange(header);
            tpl.AddRange(contents.Skip(160).Take(contents.Length - 160));

            // Inject new TPL
            // ****************
            var newTPL = tImg.CreateSaveTPL(Console.NeoGeo, MainContent.Data[MainContent.GetNodeIndex("banner.tpl")]).ToByteArray();

            for (int i = 0; i < contents.Length - 160; i++)
                contents[i + 160] = newTPL[i + 416];

            // Replace original savebanner
            // ****************
            MainContent.ReplaceFile(MainContent.GetNodeIndex("banner.bin"), contents);
        }

        protected override void ModifyEmulatorSettings()
        {
            // Not exists
            return;
        }
    }
}
