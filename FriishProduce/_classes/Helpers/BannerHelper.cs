using libWiiSharp;
using System;
using System.IO;
using System.Text;
using static FriishProduce.FileDatas.WADBanners;
using System.Collections.Generic;

namespace FriishProduce
{
    public static class BannerHelper
    {
        public static U8 BannerApp(Platform platform, Region region, string tID = null)
        {
            switch (platform)
            {
                #region VC & Other Official
                case Platform.NES:
                    return U8.Load(region switch { Region.Japan => jp_fc, Region.Korea => kr_fc, _ => nes });

                case Platform.SNES:
                    return U8.Load(region switch { Region.Japan => jp_sfc, Region.Korea => kr_sfc, _ => snes });

                case Platform.N64:
                    return U8.Load(region switch { Region.Japan => jp_n64, Region.Korea => kr_n64, _ => n64 });

                case Platform.SMS:
                    return U8.Load(region switch { Region.Japan => jp_sms, _ => sms });

                case Platform.SMD:
                    return U8.Load(region switch { Region.Japan => jp_smd, Region.USA => gen, _ => smd });

                case Platform.PCE:
                case Platform.PCECD:
                    return U8.Load(region switch { Region.Japan => jp_pce, _ => tg16 });

                case Platform.NEO:
                    return U8.Load(region switch { Region.Japan => jp_neogeo, _ => neogeo });

                case Platform.C64:
                    return U8.Load(region switch { Region.Europe => c64_eu, _ => c64_us });

                case Platform.MSX:
                    return U8.Load(tID != null && tID.ToUpper().StartsWith("XAP") ? jp_msx2 : jp_msx1); // Check if version 2 MSX WADs use a different color scheme?

                case Platform.Flash:
                    return U8.Load(flash);
                #endregion

                #region Forwarders
                case Platform.GB:
                    // return U8.Load(region switch { Region.Japan => jp_gb, _ => gb });
                    throw new NotImplementedException();

                case Platform.GBC:
                    // return U8.Load(region switch { Region.Japan => jp_gbc, _ => gbc });
                    throw new NotImplementedException();

                case Platform.GBA:
                    // return U8.Load(region switch { Region.Japan => jp_gba, _ => gba });
                    throw new NotImplementedException();

                case Platform.PSX:
                    return U8.Load(region switch { Region.Japan => jp_psx, _ => psx });

                case Platform.RPGM:
                    return U8.Load(region switch { Region.Japan => jp_rpgm, _ => rpgm });
                #endregion

                default:
                    throw new NotImplementedException();
            }
        }

        public static U8 Get(WAD w)
        {
            if (w != null)
            {
                return w.BannerApp;
            }

            return null;
        }

        public static U8[] Get(byte[] file)
        {
            U8 Banner = U8.Load(file);
            U8 Icon = U8.Load(Banner.Data[Banner.GetNodeIndex("icon.bin")]);

            Banner.LoadFile(Banner.Data[Banner.GetNodeIndex("banner.bin")]);

            return new U8[] { Banner, Icon };
        }

        /// <summary>
        /// Modifies banner directly, without use of VCBrlyt.
        /// </summary>
        /// <param name="w">WAD data to edit</param>
        /// <param name="platform">Console/platform of the original game</param>
        /// <param name="region">WAD region</param>
        /// <param name="title">Game title</param>
        /// <param name="year">Release year</param>
        /// <param name="players">No. of players</param>
        public static void Modify(WAD w, Platform platform, Region region, string title, int year, int players)
        {
            // NOTE:
            // Confirmed working:
            // [X] English/international banner
            // [X] Japanese banner (tested on 4.3J)
            // [X] Korean banner (tested on 4.3K)

            if (!w.HasBanner) return;

            U8 Banner = BannerApp(platform, region, w.UpperTitleID);

            using (var bannerBin = U8.Load(Banner.Data[Banner.GetNodeIndex("banner.bin")]))
            {
                byte[] BRLYT = bannerBin.Data[bannerBin.GetNodeIndex("banner.brlyt")];

                // Convert title to bytes
                // ****************
                byte[] Title = new byte[260];
                Encoding.BigEndianUnicode.GetBytes("N/A").CopyTo(Title, 0);
                title = title.Replace("\r\n", "\n");
                try { Encoding.BigEndianUnicode.GetBytes(title).CopyTo(Title, 0); } catch { }

                // Convert year & players to bytes
                // ****************
                byte[] Year = Encoding.BigEndianUnicode.GetBytes(year.ToString());
                string playersDisplay = players == 1 ? players.ToString() : "1-" + players.ToString();

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

                    // Players: Japanese
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

                    // Players: Korean
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

                // Players: Other languages
                // -----
                Dictionary<string, byte[]> Players = new()
                {
                    { "T_Play_ENG", Encoding.BigEndianUnicode.GetBytes($"Players: {playersDisplay}") },
                    { "T_Play_GER", Encoding.BigEndianUnicode.GetBytes($"{playersDisplay} Spieler") },
                    { "T_Play_FRA", Encoding.BigEndianUnicode.GetBytes($"Joueurs: {playersDisplay}") },
                    { "T_Play_ITA", Encoding.BigEndianUnicode.GetBytes($"Giocatori: {playersDisplay}") },
                    { "T_Play_SPA", Encoding.BigEndianUnicode.GetBytes($"Jugadores: {playersDisplay}") },
                    { "T_Play_NED", Encoding.BigEndianUnicode.GetBytes($"{playersDisplay} speler(s)") }
                };

                List<int> lengths = new();

                foreach (var line in Players)
                {
                    int index = Byte.IndexOf(BRLYT, line.Key, 5100, 6900);
                    if (index != -1)
                    {
                        int length = line.Key.ToUpper().EndsWith("NED") ? Encoding.BigEndianUnicode.GetBytes("1-2 speler(s)").Length : Byte.IndexOf(BRLYT, "txt1", index + 104) - (index + 104) - 2;
                        byte[] target = new byte[length];

                        line.Value.CopyTo(target, 0);
                        target.CopyTo(BRLYT, index + 104); 
                    }
                }

                // Replace
                // ****************
                bannerBin.ReplaceFile(bannerBin.GetNodeIndex("banner.brlyt"), BRLYT);
                w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), bannerBin.ToByteArray());
                w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("icon.bin"), Banner.Data[Banner.GetNodeIndex("icon.bin")]);
            }

            Banner.Dispose();
        }

        #region *** Using VCBrlyt ***

        public static void VCBRLYT(WAD w, string title, string year, string players)
        {
            if (!w.HasBanner) return;

            var Banner = U8.Load(w.BannerApp.Data[w.BannerApp.GetNodeIndex("banner.bin")]);

            // VCBrlyt and create temporary .brlyt file
            // ****************
            string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
            File.WriteAllBytes(BRLYTPath, Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

            Utils.Run
            (
                Paths.Tools + "vcbrlyt\\vcbrlyt.exe",
                $"..\\..\\temp\\banner.brlyt -Title \"{title}\" -YEAR {year} -Play {players}"
            );

            byte[] BRLYT = File.ReadAllBytes(BRLYTPath);
            File.Delete(BRLYTPath);

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
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
            Banner.Dispose();
        }

        /// <summary>
        /// Temporary debug function
        /// </summary>
        public static void ExportBanner(string tID, Platform c, string outFile = null)
        {
            try
            {
                if (File.Exists(Paths.Banners + tID.ToUpper() + ".app")) return;

                var d = new ChannelDatabase(c);
                foreach (var entry in d.Entries)
                {
                    for (int i = 0; i < entry.Regions.Count; i++)
                    {
                        if (entry.GetUpperID(i).ToUpper() == tID.ToUpper())
                        {
                            using WAD w = WAD.Load(Web.Get(entry.GetWAD(i)));
                            using var BannerApp = Get(w);

                            using (var Banner = U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("banner.bin")]))
                            {
                                // VCBrlyt and create temporary .brlyt file
                                // ****************
                                string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
                                File.WriteAllBytes(BRLYTPath, Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

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
                                if (BRLYT == Banner.Data[Banner.GetNodeIndex("banner.brlyt")])
                                {
                                    Banner.Dispose();
                                    return;
                                }

                                // ****************
                                #region VCPic.TPL & IconVCPic.TPL
                                var VCPic = new System.Drawing.Bitmap(256, 192);
                                var IconVCPic = new System.Drawing.Bitmap(128, 96);

                                using (var x = System.Drawing.Graphics.FromImage(VCPic))
                                    x.DrawImage(Properties.Resources.x, 0, 0, VCPic.Width, VCPic.Height);
                                using (var x = System.Drawing.Graphics.FromImage(IconVCPic))
                                    x.DrawImage(Properties.Resources.x, 0, 0, IconVCPic.Width, IconVCPic.Height);

                                using (var Icon = U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("icon.bin")]))
                                using (TPL tpl1 = TPL.Load(Banner.Data[Banner.GetNodeIndex("VCPic.tpl")]))
                                using (TPL tpl2 = TPL.Load(Icon.Data[Icon.GetNodeIndex("IconVCPic.tpl")]))
                                {
                                    ImageHelper.ReplaceTPL(tpl1, VCPic);
                                    ImageHelper.ReplaceTPL(tpl2, IconVCPic);
                                    Banner.ReplaceFile(Banner.GetNodeIndex("VCPic.tpl"), tpl1.ToByteArray());
                                    Icon.ReplaceFile(Icon.GetNodeIndex("IconVCPic.tpl"), tpl2.ToByteArray());
                                    BannerApp.ReplaceFile(BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());
                                }

                                VCPic.Dispose();
                                IconVCPic.Dispose();
                                #endregion
                                // ****************

                                // Replace
                                // ****************
                                Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), BRLYT);
                                BannerApp.ReplaceFile(BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
                            }

                            if (!Directory.Exists(Paths.Banners)) Directory.CreateDirectory(Paths.Banners);
                            File.WriteAllBytes(Paths.Banners + (string.IsNullOrWhiteSpace(outFile) ? tID.ToUpper() : outFile.Replace(".app", "")) + ".app", BannerApp.ToByteArray());
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                if (Program.DebugMode || !Program.GUI)
                    throw ex;
                else
                    MessageBox.Error(ex.Message);
                return;
            }
        }

        public static void ModifyBanner(string system, byte[] file, string outFile, int colorIndex, System.Drawing.Bitmap bImage, System.Drawing.Bitmap icon)
        {
            var (bg, bgLogo, bgBottom, lines, topBorder, topBG, topText) = BannerSchemes.List[colorIndex];

            outFile = outFile.ToLower();

            var leftTextColor = BannerSchemes.GetBrightness(colorIndex, 0) < 0.8 ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(50, 50, 50);

            string[] colorsFile = new string[]
            {
                $"{topBorder.R}             {topBorder.G}             {topBorder.B}             {topBG.R}             {topBG.G}             {topBG.B}",
                $"{topText.R}             {topText.G}             {topText.B}             {topText.R}             {topText.G}             {topText.B}",
                "116           108           109           67            105           101",
                $"0             0             0             {bg.R}             {bg.G}             {bg.B}",
                $"{bg.R}             {bg.G}             {bg.B}             {bgLogo.R}             {bgLogo.G}             {bgLogo.B}",
                $"{bg.R}             {bg.G}             {bg.B}             {bgLogo.R}             {bgLogo.G}             {bgLogo.B}",
                $"{bg.R}             {bg.G}             {bg.B}             {bgLogo.R}             {bgLogo.G}             {bgLogo.B}",
                $"{bg.R}             {bg.G}             {bg.B}             {bgLogo.R}             {bgLogo.G}             {bgLogo.B}",
                $"0             0             0             {lines.R}             {lines.G}             {lines.B}",
                $"0             0             0             {lines.R}             {lines.G}             {lines.B}",
                $"{BannerSchemes.TextColor(colorIndex).R}             {BannerSchemes.TextColor(colorIndex).G}             {BannerSchemes.TextColor(colorIndex).B}             {BannerSchemes.TextColor(colorIndex).R}             {BannerSchemes.TextColor(colorIndex).G}             {BannerSchemes.TextColor(colorIndex).B}",
                $"{leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}             {leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}",
                $"{leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}             {leftTextColor.R}             {leftTextColor.G}             {leftTextColor.B}",
                $"{BannerSchemes.TextColor(colorIndex).R}             {BannerSchemes.TextColor(colorIndex).G}             {BannerSchemes.TextColor(colorIndex).B}             {BannerSchemes.TextColor(colorIndex).R}             {BannerSchemes.TextColor(colorIndex).G}             {BannerSchemes.TextColor(colorIndex).B}",
                "0             0             0             255           255           255",
                "0             0             0             0             0             0",
                "60             60             60             255           255           255",
                $"0             0             0             {bgBottom.R}             {bgBottom.G}             {bgBottom.B}",
                $"0             0             0             {bgBottom.R}             {bgBottom.G}             {bgBottom.B}",
            };

            string VCCSPath = Path.Combine(Paths.Tools, "vcbrlyt\\Schemes\\banner.vccs");
            File.WriteAllLines(VCCSPath, colorsFile);

            try
            {
                using U8 BannerApp = U8.Load(file);

                using (U8 Banner = U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("banner.bin")]))
                {
                    foreach (var item in Banner.StringTable)
                    {
                        if (item.ToLower().StartsWith("my_back") && item.ToLower().Contains(".tpl"))
                        {
                            using TPL tpl = TPL.Load(Banner.Data[Banner.GetNodeIndex(item)]);

                            var y = new System.Drawing.Bitmap(tpl.ExtractTexture());
                            using (var x = System.Drawing.Graphics.FromImage(y))
                            {
                                x.Clear(System.Drawing.Color.Black);
                                x.DrawImage(bImage, 0, 0, y.Width, y.Height);
                            }

                            ImageHelper.ReplaceTPL(tpl, y);
                            Banner.ReplaceFile(Banner.GetNodeIndex(item), tpl.ToByteArray());

                            y.Dispose();
                        }
                    }

                    // VCBrlyt and create temporary .brlyt file
                    // ****************
                    string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
                    File.WriteAllBytes(BRLYTPath, Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

                    Utils.Run
                    (
                        "vcbrlyt\\vcbrlyt.exe",
                        $"..\\..\\temp\\banner.brlyt -System \"{system}\" -Color banner"
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
                    BannerApp.ReplaceFile(BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
                }

                using (var Icon = U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("icon.bin")]))
                {
                    foreach (var item in Icon.StringTable)
                    {
                        if (item.ToLower().StartsWith("logo") && item.ToLower().Contains(".tpl"))
                        {
                            using TPL tpl = TPL.Load(Icon.Data[Icon.GetNodeIndex(item)]);

                            var y = new System.Drawing.Bitmap(tpl.GetTextureSize(0).Width, tpl.GetTextureSize(0).Height);
                            using (var x = System.Drawing.Graphics.FromImage(y))
                            {
                                x.DrawImage(icon, 0, 0, y.Width, y.Height);
                            }

                            ImageHelper.ReplaceTPL(tpl, y, true);
                            Icon.ReplaceFile(Icon.GetNodeIndex(item), tpl.ToByteArray());
                            BannerApp.ReplaceFile(BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());

                            y.Dispose();
                        }
                    }
                }

                if (!Directory.Exists(Paths.Banners)) Directory.CreateDirectory(Paths.Banners);
                File.WriteAllBytes(Paths.Banners + outFile.Replace(".app", "") + ".app", BannerApp.ToByteArray());
            }

            catch (Exception ex)
            {
                if (Program.DebugMode || !Program.GUI)
                    throw ex;
                else
                    MessageBox.Error(ex.Message);
                return;
            }
        }

        #endregion
    }
}
