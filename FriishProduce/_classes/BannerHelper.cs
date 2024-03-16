using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using libWiiSharp;

namespace FriishProduce
{
    public static class BannerHelper
    {
        public static U8[] Get(WAD w)
        {
            if (w != null)
            {
                U8 Banner = new U8();
                U8 Icon = new U8();

                Banner.LoadFile(w.BannerApp.Data[w.BannerApp.GetNodeIndex("banner.bin")]);
                Icon.LoadFile(w.BannerApp.Data[w.BannerApp.GetNodeIndex("icon.bin")]);

                return new U8[] { Banner, Icon };
            }

            return null;
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

            string bannerPath = Paths.Banners;
            switch (c)
            {
                case Console.NES:
                    bannerPath += region == Region.Japan ? "jp_fc.bnr" : region == Region.Korea ? "kr_fc.bnr" : "nes.bnr";
                    break;

                case Console.SNES:
                    bannerPath += region == Region.Japan ? "jp_sfc.bnr" : region == Region.Korea ? "kr_sfc.bnr" : "snes.bnr";
                    break;

                case Console.N64:
                    bannerPath += region == Region.Japan ? "jp_n64.bnr" : region == Region.Korea ? "kr_n64.bnr" : "n64.bnr";
                    break;

                case Console.SMS:
                    bannerPath += region == Region.Japan ? "jp_sms.bnr" : "sms.bnr";
                    break;

                case Console.SMDGEN:
                    bannerPath += region == Region.Japan ? "jp_smd.bnr" : region == Region.Europe ? "smd.bnr" : "gen.bnr";
                    break;

                case Console.PCE:
                    bannerPath += region == Region.Japan ? "jp_pce.bnr" : "tg16.bnr";
                    break;

                case Console.NeoGeo:
                    bannerPath += region == Region.Japan ? "jp_neogeo.bnr" : "neogeo.bnr";
                    break;

                case Console.C64:
                    bannerPath += region == Region.Europe ? "c64_eu.bnr" : "c64_us.bnr";
                    break;

                case Console.MSX:
                     bannerPath += w.UpperTitleID.StartsWith("XAP") ? "jp_msx2.bnr" : "jp_msx1.bnr"; // Check if version 2 MSX WADs use a different color scheme?
                     break;

                 case Console.Flash:
                     bannerPath += "flash.bnr";
                     break;

                default:
                    throw new NotImplementedException();
            }

            if (!File.Exists(bannerPath)) throw new FileNotFoundException(new FileNotFoundException().Message, bannerPath);

            U8 Banner = U8.Load(bannerPath);

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

            U8[] Banner = Get(w);
            Banner[1].Dispose();

            // VCBrlyt and create temporary .brlyt file
            // ****************
            string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
            File.WriteAllBytes(BRLYTPath, Banner[0].Data[Banner[0].GetNodeIndex("banner.brlyt")]);

            ProcessHelper.Run
            (
                Paths.Tools + "vcbrlyt\\vcbrlyt.exe",
                $"..\\..\\temp\\banner.brlyt -Title \"{title}\" -YEAR {year} -Play {players}"
            );

            byte[] BRLYT = File.ReadAllBytes(BRLYTPath);
            File.Delete(BRLYTPath);

            // Check if modified
            // ****************
            if (BRLYT == Banner[0].Data[Banner[0].GetNodeIndex("banner.brlyt")])
            {
                Banner[0].Dispose();
                return;
            }

            // Replace
            // ****************
            Banner[0].ReplaceFile(Banner[0].GetNodeIndex("banner.brlyt"), BRLYT);
            w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex("banner.bin"), Banner[0].ToByteArray());
            Banner[0].Dispose();
        }

        /// <summary>
        /// Temporary debug function
        /// </summary>
        public static void ExportBanner(string tID, Console c)
        {
            try
            {
                if (File.Exists(Paths.Banners + tID.ToUpper() + ".bnr")) return;

                WAD w = DatabaseHelper.Get(tID).Load();
                U8[] Banner = Get(w);
                Banner[1].Dispose();

                // VCBrlyt and create temporary .brlyt file
                // ****************
                string BRLYTPath = Path.Combine(Paths.WorkingFolder, "banner.brlyt");
                File.WriteAllBytes(BRLYTPath, Banner[0].Data[Banner[0].GetNodeIndex("banner.brlyt")]);

                if (tID.ToUpper().EndsWith("Q") || tID.ToUpper().EndsWith("T"))
                {
                    ProcessHelper.Run
                    (
                        "vcbrlyt\\vcbrlyt.exe",
                        $"..\\..\\temp\\banner.brlyt -H_T_VCTitle_KOR \"VC................................................................................................................................\""
                    );
                }
                else
                {
                    ProcessHelper.Run
                    (
                        "vcbrlyt\\vcbrlyt.exe",
                        $"..\\..\\temp\\banner.brlyt -Title \"VC................................................................................................................................\" -YEAR VCVC -Play 4"
                    );
                }

                byte[] BRLYT = File.ReadAllBytes(BRLYTPath);
                File.Delete(BRLYTPath);

                // Check if modified
                // ****************
                if (BRLYT == Banner[0].Data[Banner[0].GetNodeIndex("banner.brlyt")])
                {
                    Banner[0].Dispose();
                    return;
                }

                // Replace
                // ****************
                Banner[0].ReplaceFile(Banner[0].GetNodeIndex("banner.brlyt"), BRLYT);

                if (!Directory.Exists(Paths.Banners)) Directory.CreateDirectory(Paths.Banners);
                File.WriteAllBytes(Paths.Banners + tID.ToUpper() + ".bnr", Banner[0].ToByteArray());
                Banner[0].Dispose();
                w.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Get("Error"), ex.Message, System.Windows.Forms.MessageBoxButtons.OK, Ookii.Dialogs.WinForms.TaskDialogIcon.Error);
            }
        }

        #endregion
    }
}
