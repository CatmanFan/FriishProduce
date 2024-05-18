using libWiiSharp;
using System;
using System.IO;
using System.Text;
using static FriishProduce.FileDatas.WADBanners;

namespace FriishProduce
{
    public static class BannerHelper
    {
        public static (U8, U8) Get(WAD w)
        {
            if (w != null)
            {
                U8 Banner = new U8();
                U8 Icon = new U8();

                Banner.LoadFile(w.BannerApp.Data[w.BannerApp.GetNodeIndex("banner.bin")]);
                Icon.LoadFile(w.BannerApp.Data[w.BannerApp.GetNodeIndex("icon.bin")]);

                return (Banner, Icon);
            }

            return (null, null);
        }

        public static U8[] Get(string file)
        {
            U8 Banner = new U8();
            U8 Icon = new U8();

            Banner.LoadFile(file);

            Icon.LoadFile(Banner.Data[Banner.GetNodeIndex("icon.bin")]);
            Banner.LoadFile(Banner.Data[Banner.GetNodeIndex("banner.bin")]);

            return new U8[] { Banner, Icon };
        }

        /// <summary>
        /// EXPERIMENTAL: Modifies banner directly, without use of VCBrlyt.
        /// </summary>
        /// <param name="w">WAD data to edit</param>
        /// <param name="c">Console type of VC WAD</param>
        /// <param name="region">WAD region</param>
        /// <param name="title">Game title</param>
        /// <param name="year">Release year</param>
        /// <param name="players">No. of players</param>
        public static void Modify(WAD w, Console c, Region region, string title, int year, int players)
        {
            // NOTE:
            // Confirmed working:
            // [X] English/international banner
            // [X] Japanese banner (tested on 4.3J)
            // [X] Korean banner (tested on 4.3K)

            if (!w.HasBanner) return;

            U8 Banner = new U8();
            U8 Icon = null;

            switch (c)
            {
            #region VC & Other Official
                case Console.NES:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_fc, Region.Korea => BNR_kr_fc, _ => BNR_nes });
                    break;

                case Console.SNES:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_sfc, Region.Korea => BNR_kr_sfc, _ => BNR_snes });
                    break;

                case Console.N64:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_n64, Region.Korea => BNR_kr_n64, _ => BNR_n64 });
                    break;

                case Console.SMS:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_sms, _ => BNR_sms });
                    break;

                case Console.SMD:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_smd, Region.Europe => BNR_smd, _ => BNR_gen });
                    break;

                case Console.PCE:
                case Console.PCECD:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_pce, _ => BNR_tg16 });
                    break;

                case Console.NEO:
                    Banner = U8.Load(region switch { Region.Japan => BNR_jp_neogeo, _ => BNR_neogeo });
                    break;

                case Console.C64:
                    Banner = U8.Load(region switch { Region.Europe => BNR_c64_eu, _ => BNR_c64_us });
                    break;

                case Console.MSX:
                    Banner = U8.Load(w.UpperTitleID.StartsWith("XAP") ? BNR_jp_msx2 : BNR_jp_msx1); // Check if version 2 MSX WADs use a different color scheme?
                    break;

                case Console.Flash:
                    Banner = U8.Load(BNR_flash);
                    break;
            #endregion

            #region Forwarders
                case Console.GB:
                    // Banner = U8.Load(region switch { Region.Japan => BNR_jp_gb, _ => BNR_gb });
                    // Icon = U8.Load(ICN_gb);
                    break;

                case Console.GBC:
                    // Banner = U8.Load(region switch { Region.Japan => BNR_jp_gbc, _ => BNR_gbc });
                    // Icon = U8.Load(ICN_gbc);
                    break;

                case Console.GBA:
                    // Banner = U8.Load(region switch { Region.Japan => BNR_jp_gba, _ => BNR_gba });
                    // Icon = U8.Load(ICN_gba);
                    break;

                case Console.PSX:
                    // Banner = U8.Load(region switch { Region.Japan => BNR_jp_psx, _ => BNR_psx });
                    // Icon = U8.Load(ICN_psx);
                    break;

                case Console.RPGM:
                    // Banner = U8.Load(region switch { Region.Japan => BNR_jp_rpgm, _ => BNR_rpgm });
                    Banner = U8.Load(BNR_rpgm);
                    Icon = U8.Load(ICN_rpgm);
                    break;
            #endregion

                default:
                    throw new NotImplementedException();
            }

            if (Icon != null) w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());

            byte[] BRLYT = Banner.Data[Banner.GetNodeIndex("banner.brlyt")];

            // Convert title to bytes
            // ****************
            byte[] Title = new byte[260];
            Encoding.BigEndianUnicode.GetBytes("N/A").CopyTo(Title, 0);
            title = title.Replace("\r\n", "\n");
            try { Encoding.BigEndianUnicode.GetBytes(title).CopyTo(Title, 0); } catch { }

            // Convert year & players to bytes
            // ****************
            byte[] Year = Encoding.BigEndianUnicode.GetBytes(year.ToString());
            string playersDisplay = players >= 1 ? players.ToString() : "1-" + players.ToString();

            var Players_EN = new byte[24];
            Encoding.BigEndianUnicode.GetBytes($"Players: {playersDisplay}").CopyTo(Players_EN, 0);

            var Players_DE = new byte[22];
            Encoding.BigEndianUnicode.GetBytes($"{playersDisplay} Spieler").CopyTo(Players_DE, 0);

            var Players_FR = new byte[24];
            Encoding.BigEndianUnicode.GetBytes($"Joueurs: {playersDisplay}").CopyTo(Players_FR, 0);

            var Players_IT = new byte[28];
            Encoding.BigEndianUnicode.GetBytes($"Giocatori: {playersDisplay}").CopyTo(Players_IT, 0);

            var Players_ES = new byte[28];
            Encoding.BigEndianUnicode.GetBytes($"Jugadores: {playersDisplay}").CopyTo(Players_ES, 0);

            var Players_NL = new byte[26];
            Encoding.BigEndianUnicode.GetBytes($"{playersDisplay} speler(s)").CopyTo(Players_NL, 0);

            var Players_JP = players >= 1 ? new byte[] { 0x31, 0x4E, 0xBA, 0x00, 0x00, 0x00, 0x00 } : new byte[] { 0x31, 0xFF, 0x5E, 0x00, Encoding.BigEndianUnicode.GetBytes(players.ToString())[1], 0x4E, 0xBA };

            var Players_KO = players >= 1 ? new byte[] { 0x31, 0xBA, 0x85, 0x00, 0x00, 0x00, 0x00 } : new byte[] { 0x31, 0x00, 0x2D, 0x00, Encoding.BigEndianUnicode.GetBytes(players.ToString())[1], 0xBA, 0x85 };

            for (int i = 0; i < BRLYT.Length - 13; i++)
            {
                // Copy title and year
                // ****************
                if (BRLYT[i] == 0x00
                 && BRLYT[i + 1] == 0x56
                 && BRLYT[i + 2] == 0x00
                 && BRLYT[i + 3] == 0x43
                 && BRLYT[i + 4] == 0x00
                 && BRLYT[i + 5] == 0x2E)
                    Title.CopyTo(BRLYT, i);

                if (region == Region.Korea &&
                    BRLYT[i + 8] == 0xB1
                 && BRLYT[i + 9] == 0x44
                 && BRLYT[i + 10] == 0x00)
                    Year.CopyTo(BRLYT, i);

                else if (BRLYT[i] == 0x00
                 && BRLYT[i + 1] == 0x56
                 && BRLYT[i + 2] == 0x00
                 && BRLYT[i + 3] == 0x43
                 && BRLYT[i + 4] == 0x00
                 && BRLYT[i + 5] == 0x56
                 && BRLYT[i + 6] == 0x00
                 && BRLYT[i + 7] == 0x43)
                    Year.CopyTo(BRLYT, i);

                // Copy players
                // ****************

                // English
                // -----
                if (BRLYT[i] == 0x00
                 && BRLYT[i + 1] == 0x50
                 && BRLYT[i + 2] == 0x00
                 && BRLYT[i + 3] == 0x6C
                 && BRLYT[i + 4] == 0x00
                 && BRLYT[i + 5] == 0x61)
                    Players_EN.CopyTo(BRLYT, i);

                // German
                // -----
                else if (BRLYT[i + 7] == 0x20
                 && BRLYT[i + 8] == 0x00
                 && BRLYT[i + 9] == 0x53
                 && BRLYT[i + 10] == 0x00
                 && BRLYT[i + 11] == 0x70)
                    Players_DE.CopyTo(BRLYT, i);

                // French
                // -----
                else if (BRLYT[i] == 0x00
                 && BRLYT[i + 1] == 0x4A
                 && BRLYT[i + 2] == 0x00
                 && BRLYT[i + 3] == 0x6F
                 && BRLYT[i + 4] == 0x00
                 && BRLYT[i + 5] == 0x75)
                    Players_FR.CopyTo(BRLYT, i);

                // Italian
                // -----
                else if (BRLYT[i] == 0x00
                 && BRLYT[i + 1] == 0x47
                 && BRLYT[i + 2] == 0x00
                 && BRLYT[i + 3] == 0x69
                 && BRLYT[i + 4] == 0x00
                 && BRLYT[i + 5] == 0x6F)
                    Players_IT.CopyTo(BRLYT, i);

                // Spanish
                // -----
                else if (BRLYT[i] == 0x00
                 && BRLYT[i + 1] == 0x4A
                 && BRLYT[i + 2] == 0x00
                 && BRLYT[i + 3] == 0x75
                 && BRLYT[i + 4] == 0x00
                 && BRLYT[i + 5] == 0x67)
                    Players_ES.CopyTo(BRLYT, i);

                // Dutch
                // -----
                else if (BRLYT[i + 9] == 0x73
                 && BRLYT[i + 10] == 0x00
                 && BRLYT[i + 11] == 0x70
                 && BRLYT[i + 12] == 0x00
                 && BRLYT[i + 13] == 0x65)
                    Players_NL.CopyTo(BRLYT, i);

                // Japanese
                // -----
                else if (region == Region.Japan
                 && BRLYT[i] == 0x31
                 && BRLYT[i + 1] == 0xFF
                 && BRLYT[i + 2] == 0x5E
                 && BRLYT[i + 3] == 0x00
                 && BRLYT[i + 4] == 0x34
                 && BRLYT[i + 5] == 0x4E
                 && BRLYT[i + 6] == 0xBA)
                    Players_JP.CopyTo(BRLYT, i);

                // Korean
                // -----
                else if (region == Region.Korea
                 && BRLYT[i] == 0x31
                 && BRLYT[i + 1] == 0x00
                 && BRLYT[i + 2] == 0x2D
                 && BRLYT[i + 3] == 0x00
                 && BRLYT[i + 5] == 0xBA
                 && BRLYT[i + 6] == 0x85)
                    Players_KO.CopyTo(BRLYT, i);
            }

            // Replace
            // ****************
            Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), BRLYT);
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
            Banner.Dispose();
        }

        #region *** Using VCBrlyt ***

        public static void VCBRLYT(WAD w, string title, string year, string players)
        {
            if (!w.HasBanner) return;

            (U8, U8) Banner = Get(w);
            Banner.Item2.Dispose();

            // VCBrlyt and create temporary .brlyt file
            // ****************
            string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
            File.WriteAllBytes(BRLYTPath, Banner.Item1.Data[Banner.Item1.GetNodeIndex("banner.brlyt")]);

            Utils.Run
            (
                Paths.Tools + "vcbrlyt\\vcbrlyt.exe",
                $"..\\..\\temp\\banner.brlyt -Title \"{title}\" -YEAR {year} -Play {players}"
            );

            byte[] BRLYT = File.ReadAllBytes(BRLYTPath);
            File.Delete(BRLYTPath);

            // Check if modified
            // ****************
            if (BRLYT == Banner.Item1.Data[Banner.Item1.GetNodeIndex("banner.brlyt")])
            {
                Banner.Item1.Dispose();
                return;
            }

            // Replace
            // ****************
            Banner.Item1.ReplaceFile(Banner.Item1.GetNodeIndex("banner.brlyt"), BRLYT);
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), Banner.Item1.ToByteArray());
            Banner.Item1.Dispose();
        }

        /// <summary>
        /// Temporary debug function
        /// </summary>
        public static void ExportBanner(string tID, Console c)
        {
            try
            {
                if (File.Exists(Paths.Banners + tID.ToUpper() + ".bnr")) return;

                var d = new ChannelDatabase(c);
                foreach (var entry in d.Entries)
                {
                    for (int i = 0; i < entry.Regions.Count; i++)
                    {
                        if (entry.GetUpperID(i).ToUpper() == tID.ToUpper())
                        {
                            using (WAD w = entry.GetWAD(i))
                            {
                                (U8, U8) Banner = Get(w);
                                Banner.Item2.Dispose();

                                // VCBrlyt and create temporary .brlyt file
                                // ****************
                                string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
                                File.WriteAllBytes(BRLYTPath, Banner.Item1.Data[Banner.Item1.GetNodeIndex("banner.brlyt")]);

                                if (tID.ToUpper().EndsWith("Q") || tID.ToUpper().EndsWith("T"))
                                {
                                    Utils.Run
                                    (
                                        "vcbrlyt\\vcbrlyt.exe",
                                        $"..\\..\\temp\\banner.brlyt -H_T_VCTitle_KOR \"VC................................................................................................................................\""
                                    );
                                }
                                else
                                {
                                    Utils.Run
                                    (
                                        "vcbrlyt\\vcbrlyt.exe",
                                        $"..\\..\\temp\\banner.brlyt -Title \"VC................................................................................................................................\" -YEAR VCVC -Play 4"
                                    );
                                }

                                byte[] BRLYT = File.ReadAllBytes(BRLYTPath);
                                File.Delete(BRLYTPath);

                                // Check if modified
                                // ****************
                                if (BRLYT == Banner.Item1.Data[Banner.Item1.GetNodeIndex("banner.brlyt")])
                                {
                                    Banner.Item1.Dispose();
                                    return;
                                }

                                // Replace
                                // ****************
                                Banner.Item1.ReplaceFile(Banner.Item1.GetNodeIndex("banner.brlyt"), BRLYT);

                                if (!Directory.Exists(Paths.Banners)) Directory.CreateDirectory(Paths.Banners);
                                File.WriteAllBytes(Paths.Banners + tID.ToUpper() + ".bnr", Banner.Item1.ToByteArray());
                                Banner.Item1.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Error(ex.Message);
            }
        }

        public static void ModifyBanner(string system, string file, string outFile, int target)
        {
            var colors = BannerSchemes.List[target];

            file = file.ToLower();
            outFile = outFile.ToLower();

            var leftTextColor = colors[0].GetBrightness() < 0.8 ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(50, 50, 50);

            string[] colorsFile = new string[]
            {
                $"{colors[4].R}             {colors[4].G}             {colors[4].B}             {colors[5].R}             {colors[5].G}             {colors[5].B}",
                $"{colors[6].R}             {colors[6].G}             {colors[6].B}             {colors[6].R}             {colors[6].G}             {colors[6].B}",
                "116           108           109           67            105           101",
                "0             0             0             255           255           255",
                $"{colors[0].R}             {colors[0].G}             {colors[0].B}             {colors[1].R}             {colors[1].G}             {colors[1].B}",
                $"{colors[0].R}             {colors[0].G}             {colors[0].B}             {colors[1].R}             {colors[1].G}             {colors[1].B}",
                $"{colors[0].R}             {colors[0].G}             {colors[0].B}             {colors[1].R}             {colors[1].G}             {colors[1].B}",
                $"{colors[0].R}             {colors[0].G}             {colors[0].B}             {colors[1].R}             {colors[1].G}             {colors[1].B}",
                $"0             0             0             {colors[3].R}             {colors[3].G}             {colors[3].B}",
                $"0             0             0             {colors[3].R}             {colors[3].G}             {colors[3].B}",
                $"{BannerSchemes.TextColor(target).R}             {BannerSchemes.TextColor(target).G}             {BannerSchemes.TextColor(target).B}             {BannerSchemes.TextColor(target).R}             {BannerSchemes.TextColor(target).G}             {BannerSchemes.TextColor(target).B}",
                $"{leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}             {leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}",
                $"{leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}             {leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}",
                $"{BannerSchemes.TextColor(target).R}             {BannerSchemes.TextColor(target).G}             {BannerSchemes.TextColor(target).B}             {BannerSchemes.TextColor(target).R}             {BannerSchemes.TextColor(target).G}             {BannerSchemes.TextColor(target).B}",
                "0             0             0             255           255           255",
                "0             0             0             0             0             0",
                "60             60             60             255           255           255",
                $"0             0             0             {colors[2].R}             {colors[2].G}             {colors[2].B}",
                $"0             0             0             {colors[2].R}             {colors[2].G}             {colors[2].B}",
            };

            string VCCSPath = Path.Combine(Paths.Tools, "vcbrlyt\\Schemes\\banner.vccs");
            File.WriteAllLines(VCCSPath, colorsFile);

            try
            {
                if (!File.Exists(Paths.Banners + file.Replace(".bnr", "") + ".bnr")) return;

                U8 Banner = U8.Load(Paths.Banners + file.Replace(".bnr", "") + ".bnr");

                // VCBrlyt and create temporary .brlyt file
                // ****************
                string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
                File.WriteAllBytes(BRLYTPath, Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

                if (file.ToLower().Contains("kr_"))
                {
                    Utils.Run
                    (
                        "vcbrlyt\\vcbrlyt.exe",
                        $"..\\..\\temp\\banner.brlyt -Color banner -H_T_VCTitle_KOR \"VC................................................................................................................................\""
                    );
                }
                else
                {
                    Utils.Run
                    (
                        "vcbrlyt\\vcbrlyt.exe",
                        $"..\\..\\temp\\banner.brlyt -Color banner -Title \"VC................................................................................................................................\" -YEAR VCVC -Play 4"
                    );
                }

                Utils.Run
                (
                    "vcbrlyt\\vcbrlyt.exe",
                    $"..\\..\\temp\\banner.brlyt -System \"{system}\""
                );

                byte[] BRLYT = File.ReadAllBytes(BRLYTPath);
                File.Delete(BRLYTPath);
                File.Delete(VCCSPath);

                // Check if modified
                // ****************
                if (BRLYT == Banner.Data[Banner.GetNodeIndex("banner.brlyt")])
                {
                    Banner.Dispose();
                    return;
                }

                // Replace
                // ****************
                Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), BRLYT);

                if (!Directory.Exists(Paths.Banners)) Directory.CreateDirectory(Paths.Banners);
                File.WriteAllBytes(Paths.Banners + outFile.Replace(".bnr", "") + ".bnr", Banner.ToByteArray());
                Banner.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Error(ex.Message);
            }
        }

        #endregion
    }
}
