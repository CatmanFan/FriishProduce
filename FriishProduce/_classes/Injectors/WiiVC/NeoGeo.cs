using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libWiiSharp;

namespace FriishProduce.WiiVC
{
    public class NeoGeo : InjectorWiiVC
    {
        public string ZIP { get; set; }
        public string BIOSPath { get; set; }
        public bool IsZLIB { get; set; }

        private byte[] P { get; set; }
        private List<byte> M { get; set; }
        private List<byte> V { get; set; }
        private List<byte> S { get; set; }
        private List<byte> C { get; set; }
        private List<byte> BIOS { get; set; }

        protected override void Load()
        {
            NeedsManualLoaded = false;
            base.Load();

            if (Contents.Count >= 8)
            {
                if (Contents[5].Length > Contents[6].Length && Contents[5].Length > Contents[7].Length)         MainContent = U8.Load(Contents[5]);
                else if (Contents[6].Length > Contents[5].Length && Contents[6].Length > Contents[7].Length)    MainContent = U8.Load(Contents[6]);
                else MainContent = U8.Load(Contents[7]);
            }
            else if (Contents.Count == 7)
                MainContent = Contents[6].Length > Contents[5].Length ? U8.Load(Contents[6]) : U8.Load(Contents[5]);
            else MainContent = U8.Load(Contents[5]);
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. (compressed formats not supported yet)
        /// </summary>
        protected override void ReplaceROM()
        {
            if (IsZLIB) throw new Exception("ZLIB WADs not supported yet!");

            // Extract to ZIP archive
            // ****************
            Directory.CreateDirectory(Paths.WorkingFolder_ROM);
            ZipFile.ExtractToDirectory(ZIP, Paths.WorkingFolder_ROM);

            // First do a check to see if valid
            // ****************
            {
                bool IsNG = false;

                foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
                    if (Path.GetFileName(file).EndsWith("c1.bin")
                     || Path.GetFileName(file).EndsWith("c2.bin")
                     || Path.GetFileName(file).EndsWith("m1.bin")
                     || Path.GetFileName(file).EndsWith("p1.bin")
                     || Path.GetFileName(file).EndsWith("s1.bin")
                     || Path.GetFileName(file).EndsWith("v1.bin"))
                        IsNG = true;

                if (!IsNG) throw new InvalidDataException();
            }

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
                var newBIOS = new List<byte>();

                if (Path.GetExtension(BIOSPath).ToLower() == ".rom")
                {
                    newBIOS.AddRange(File.ReadAllBytes(BIOSPath));
                }

                else if (Path.GetExtension(BIOSPath).ToLower() == ".zip")
                {
                    Directory.CreateDirectory(Paths.WorkingFolder_ROM + "bios\\");
                    ZipFile.ExtractToDirectory(BIOSPath, Paths.WorkingFolder_ROM + "bios\\");

                    foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM + "bios\\"))
                    {
                        if (Path.GetExtension(file).ToLower() == ".rom")
                        {
                            newBIOS.AddRange(File.ReadAllBytes(file));
                        }
                    }

                    if (newBIOS.Count < 10) goto RetrieveOrigBIOS;
                }

                else goto RetrieveOrigBIOS;

                for (int i = 1; i < newBIOS.Count; i += 2)
                {
                    (newBIOS[i - 1], newBIOS[i]) = (newBIOS[i], newBIOS[i - 1]);
                }

                BIOS.AddRange(newBIOS);
                goto Next;
            }

        RetrieveOrigBIOS:
            BIOS.Clear();

            // Get BIOS directly from game.bin
            // ****************
            if (!IsZLIB)
            {
                var orig = MainContent.Data[MainContent.GetNodeIndex("game.bin")];
                var origBIOS = orig.Skip(orig.Length - 131072).Take(131072);
                BIOS.AddRange(origBIOS);
            }
            else throw new FileNotFoundException();

            goto Next;

        Next:
            // ------------------------- //

            var P1 = new List<byte>();
            P = new byte[1];

            foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
            {
                if (char.ToLower(file[file.Length - 6]) == 'p')
                {
                    var b = File.ReadAllBytes(file);
                    bool P1BinIs2MB = b.Length == 2097152 && Path.GetFileNameWithoutExtension(file).ToLower().Contains("p1");

                    P1.AddRange(b);
                    P = P1.ToArray();

                    if (P1BinIs2MB)
                        for (int i = 0; i < P.Length / 2; i++) (P[i + (P.Length / 2)], P[i]) = (P[i], P[i + (P.Length / 2)]);
                }
            }

            for (int i = 1; i < P.Length; i += 2) (P[i - 1], P[i]) = (P[i], P[i - 1]);

            // ------------------------- //

            M = new List<byte>();

            foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
            {
                if (char.ToLower(file[file.Length - 6]) == 'm')
                    M.AddRange(File.ReadAllBytes(file));
            }

            // ------------------------- //

            V = new List<byte>();

            foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
            {
                if (char.ToLower(file[file.Length - 6]) == 'v')
                    V.AddRange(File.ReadAllBytes(file));
            }

            // ------------------------- //

            S = new List<byte>();

            foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
            {
                if (char.ToLower(file[file.Length - 6]) == 's')
                    S.AddRange(File.ReadAllBytes(file));
            }

            // ------------------------- //

            C = new List<byte>();
            int C_count = 0;

            foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
                if (char.ToLower(file[file.Length - 6]) == 'c') C_count++;

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

                foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
                    if (Path.GetFileName(file).ToLower().Contains($"c{x + 1}.bin"))
                    {
                        C1 = File.ReadAllBytes(file);

                        // Byteswap
                        for (int i = 1; i < C1.Length; i += 2)
                            (C1[i - 1], C1[i]) = (C1[i], C1[i - 1]);
                    }

                foreach (var file in Directory.EnumerateFiles(Paths.WorkingFolder_ROM))
                    if (Path.GetFileName(file).ToLower().Contains($"c{x + 2}.bin"))
                    {
                        C2 = File.ReadAllBytes(file);

                        // Byteswap
                        for (int i = 1; i < C2.Length; i += 2)
                            (C2[i - 1], C2[i]) = (C2[i], C2[i - 1]);
                    }

                if (C1.Length > 5 && C2.Length > 5)
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
            }

            var C_swap = C.ToArray();
            for (int i = 3; i < C_swap.Length; i += 4)
            {
                (C_swap[i - 3], C_swap[i - 2], C_swap[i - 1], C_swap[i]) = (C_swap[i - 2], C_swap[i], C_swap[i - 3], C_swap[i - 1]);
            }

            // ------------------------- //

            // Header
            // ****************
            string h = "00000040"
                + P.Length.ToString("X8")
                + (P.Length + 64).ToString("X8")
                + M.Count.ToString("X8")
                + (P.Length + M.Count + 64).ToString("X8")
                + V.Count.ToString("X8") + "00000000000000000000000000000000"
                + (P.Length + M.Count + V.Count + 64).ToString("X8")
                + S.Count.ToString("X8")
                + (P.Length + M.Count + V.Count + S.Count + 64).ToString("X8")
                + C.Count.ToString("X8")
                + (P.Length + M.Count + V.Count + S.Count + C.Count + 64).ToString("X8")
                + BIOS.Count.ToString("X8");
            var Header = new List<byte>();

            for (int i = 0; i < h.Length; i += 2)
                Header.Add(Convert.ToByte(h.Substring(i, 2), 16));

            var GameBin = new List<byte>();

            if (!IsZLIB)
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
            const string header = "0020AF30000000090000000C000000540000000000000078000000000000009C00000000000000C000000000000000E40000000000000108000000000000012C0000000000000150000000000000017400000000004000C000000005000001A00000000000000000000000010000000100000000000000000030003000000005000061A00000000000000000000000010000000100000000000000000030003000000005000073A00000000000000000000000010000000100000000000000000030003000000005000085A00000000000000000000000010000000100000000000000000030003000000005000097A000000000000000000000000100000001000000000000000000300030000000050000A9A000000000000000000000000100000001000000000000000000300030000000050000BBA000000000000000000000000100000001000000000000000000300030000000050000CDA000000000000000000000000100000001000000000000000000300030000000050000DFA00000000000000000000000010000000100000000000000000000000000000000";

            var tpl = new List<byte>();

            // Extract TPL
            // ****************
            for (int i = 0; i < header.Length; i += 2)
                tpl.Add(Convert.ToByte(header.Substring(i, 2), 16));
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
