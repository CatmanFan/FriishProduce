using Ionic.Zip;
using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
{
    public class NEO : InjectorWiiVC
    {
        private List<byte> P { get; set; }
        private List<byte> M { get; set; }
        private List<byte> V { get; set; }
        private List<byte> S { get; set; }
        private List<byte> C { get; set; }
        private List<byte> BIOS { get; set; }

        private ZipFile ZIP { get; set; }

        protected override void Load()
        {
            NeedsMainDOL = false;
            NeedsManualLoaded = true;
            SaveTextEncoding = Encoding.BigEndianUnicode;

            // Game.bin may be stored on either 6.app, 5.app or 7.app.
            // In all cases the manual files are stored also on 00000005.app.
            // ****************
            if (WAD.Contents.Length >= 8)
                MainContentIndex = WAD.Contents[7].Length > WAD.Contents[6].Length && WAD.Contents[7].Length > WAD.Contents[5].Length ? 7
                    : WAD.Contents[6].Length > WAD.Contents[7].Length && WAD.Contents[6].Length > WAD.Contents[5].Length ? 6
                    : 5;
            else if (WAD.Contents.Length == 7)
                MainContentIndex = WAD.Contents[6].Length > WAD.Contents[5].Length ? 6 : 5;
            else MainContentIndex = 5;

            ZIP = ZipFile.Read(new MemoryStream(File.ReadAllBytes(ROM.Path)));
            base.Load();
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
            int target = 0;
            bool isZlib = EmuType >= 1;

            foreach (var item in MainContent.StringTable)
            {
                if (item.ToLower() == "game.bin.z") { target = MainContent.GetNodeIndex(item); isZlib = true; }
                if (item.ToLower() == "game.bin") target = MainContent.GetNodeIndex(item);
                // "game.bin.z" (found in KOF '95 WAD) is the game compressed in ZLIB format.
            }

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

                if (filename1[filename1.Length - 2] == 'p' || filename2[filename2.Length - 2] == 'p')
                    FileList.Add(item.FileName);
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

            int C_count = 0;

            foreach (ZipEntry item in ZIP.Entries)
            {
                string filename1 = item.FileName.ToLower();
                string filename2 = Path.GetFileNameWithoutExtension(item.FileName).ToLower();

                if (filename1[filename1.Length - 2] == 'm' || filename2[filename2.Length - 2] == 'm')
                    M.AddRange(ExtractToByteArray(item));

                if (filename1[filename1.Length - 2] == 'v' || filename2[filename2.Length - 2] == 'v')
                    V.AddRange(ExtractToByteArray(item));

                if (filename1[filename1.Length - 2] == 's' || filename2[filename2.Length - 2] == 's')
                    S.AddRange(ExtractToByteArray(item));

                if (filename1[filename1.Length - 2] == 'c' || filename2[filename2.Length - 2] == 'c')
                    C_count++;
            }

            // ------------------------- //

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

            // TO-DO: Do byteswap for each byte with BIOS
            // ****************
            BIOS = new List<byte>();
            var BIOSPath = "";
            bool noBIOS = false;

            try { var test = Settings["BIOS"]; BIOSPath = Settings["BIOSPath"]; } catch { noBIOS = true; goto AutoBIOS; }

            if (Settings["BIOS"].ToLower() != "custom" || string.IsNullOrWhiteSpace(BIOSPath) || !File.Exists(BIOSPath))
            {
                goto AutoBIOS;
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

                    if (BIOS.Count < 10) goto AutoBIOS;
                }

                else goto AutoBIOS;

                goto Next;
            }

            AutoBIOS:
            var targetBIOS = Options.VC_NEO.Default.bios;
            if (!noBIOS && Settings["BIOS"].ToLower() != "custom") targetBIOS = Settings["BIOS"];

            BIOS.Clear();
            switch (targetBIOS.ToLower())
            {
                default:
                case "vc1":
                    BIOS.AddRange(Properties.Resources.NeoGeo_VC1);
                    break;

                case "vc2":
                    BIOS.AddRange(Properties.Resources.NeoGeo_VC2);
                    break;

                case "vc3":
                    BIOS.AddRange(Properties.Resources.NeoGeo_VC3);
                    break;
            }

            goto Next;

            Next:
            for (int i = 1; i < BIOS.Count; i += 2)
            {
                (BIOS[i - 1], BIOS[i]) = (BIOS[i], BIOS[i - 1]);
            }

            // Header
            // ****************
            var Header = new List<byte>();

            string h = isZlib ? "524F4D300000000000000000"
                + S.Count.ToString("X8")
                + C.Count.ToString("X8")
                + V.Count.ToString("X8")
                + "00000000"
                + BIOS.Count.ToString("X8")
                + P.Count.ToString("X8")
                + M.Count.ToString("X8")
                + "000000000000000000000000000000000000000000000000"

                : "00000040"
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

            if (!isZlib) // i.e. not Zlib
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

            MainContent.ReplaceFile(target, isZlib ? Ionic.Zlib.ZlibStream.CompressBuffer(GameBin.ToArray()) : GameBin.ToArray());

            ZIP.Dispose();
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            int[] target = new int[] { 0, MainContent.GetNodeIndex("banner.bin") };
            if (target[1] == -1) target = new int[] { 1, ManualContent.GetNodeIndex("banner.bin") };
            if (target[1] == -1) return;

            // -----------------------
            // TEXT
            // -----------------------

            lines = ConvertSaveText(lines);

            byte[] contents = target[0] == 0 ? MainContent.Data[target[1]] : ManualContent.Data[target[1]];

            for (int i = 32; i < 96; i++)
            {
                try { contents[i] = SaveTextEncoding.GetBytes(lines[0])[i - 32]; }
                catch { contents[i] = 0x00; }
            }

            for (int i = 96; i < 160; i++)
            {
                try { contents[i] = SaveTextEncoding.GetBytes(lines[1])[i - 96]; }
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

            var placeholder = new List<byte>();

            // Create TPL byte array
            // ****************
            placeholder.AddRange(header);
            placeholder.AddRange(contents.Skip(160).Take(contents.Length - 160).ToArray());

            // Inject new TPL
            // ___________________
            // There seems to be a bug where the savedata icon is saved as a TPL, but does not display correctly after being injected into the banner.bin (i.e. glitched on Wii Menu Save Data Management)
            // ****************
            Img.CreateSaveTPL(placeholder.ToArray()).ToByteArray().Skip(header.Length).ToArray().CopyTo(contents, 160);

            // Replace original savebanner
            // ****************
            if (target[0] == 0) MainContent.ReplaceFile(target[1], contents);
            else ManualContent.ReplaceFile(target[1], contents);
        }

        protected override void ModifyEmulatorSettings()
        {
            // Not exists
            return;
        }
    }
}
