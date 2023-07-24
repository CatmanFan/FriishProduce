using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Net;
using System.Net.NetworkInformation;

namespace FriishProduce
{
    public class DBEntry
    {
        private string Title;
        private string Year;
        private string ImgURL;
        private string Players;

        public DBEntry()
        {
            Title = null;
            Year = null;
            ImgURL = null;
            Players = null;
        }

        public string GetYear() => Year;

        public string GetTitle() => Title;

        public string GetImgURL() => ImgURL;

        public string GetPlayers() => Players;

        public void Get(string gamePath, Platforms platform)
        {
            var crc = new Crc32();
            using (var file = File.OpenRead(gamePath))
                crc.Append(file);

            var hash_array = crc.GetCurrentHash();
            Array.Reverse(hash_array);
            string hash = BitConverter.ToString(hash_array).Replace("-", "").ToLower();

            string db_base = "https://github.com/libretro/libretro-database/raw/master/metadat/";
            Dictionary<Platforms, string> db_platforms = new Dictionary<Platforms, string>
            {
                { Platforms.NES, "Nintendo - Nintendo Entertainment System" },
                { Platforms.SNES, "Nintendo - Super Nintendo Entertainment System" },
                { Platforms.N64, "Nintendo - Nintendo 64" },
                { Platforms.GB, "Nintendo - Game Boy" },
                { Platforms.GBC, "Nintendo - Game Boy Color" },
                { Platforms.GBA, "Nintendo - Game Boy Advance" },
                { Platforms.SMS, "Sega - Master System - Mark III" },
                { Platforms.SMD, "Sega - Mega Drive - Genesis" },
                { Platforms.S32X, "Sega - 32X" },
                { Platforms.PCE, "NEC - PC Engine - TurboGrafx 16" },
                { Platforms.NeoGeo, "SNK - Neo Geo" },
                { Platforms.MSX, "Microsoft - MSX" },
                // { Platforms.MSX, "Microsoft - MSX2" },
            };
            bool TitleIsSet = false;

            foreach (KeyValuePair<Platforms, string> item in db_platforms)
            {
                if (item.Key == platform)
                {
                    byte[] db_bytes = { 0x00 };

                    using (Ping p = new Ping())
                    {
                        try
                        {
                            PingReply r = p.Send("google.com", 3000);
                            if (r.Status != IPStatus.Success || r == null) throw new WebException(Program.Language.Get("m017"), WebExceptionStatus.Timeout);
                        }
                        catch
                        {
                            throw new WebException(Program.Language.Get("m017"), WebExceptionStatus.ConnectFailure);
                        }
                    }

                    // --------------------------------------------------------------------- //

                    string[] db_lines = new string[1];

                    try
                    {
                        // Search in "releaseyear" repository
                        using (WebClient c = new WebClient())
                            db_bytes = c.DownloadData(db_base + "releaseyear/" + Uri.EscapeUriString(item.Value) + ".dat");

                        db_lines = System.Text.Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                        // Scan retrieved database
                        for (int i = 10; i < db_lines.Length; i++)
                            if (db_lines[i].ToLower().Contains(hash))
                            {
                                for (int x = i; x > i - 10; x--)
                                {
                                    if (db_lines[x].Contains("comment \"") && !TitleIsSet)
                                    {
                                        Title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                        ImgURL = "https://thumbnails.libretro.com/" + Uri.EscapeUriString(item.Value) + "/Named_Titles/" + Uri.EscapeUriString(Title) + ".png";
                                        TitleIsSet = true;
                                    }
                                    if (db_lines[x].Contains("releaseyear"))
                                    {
                                        Year = db_lines[x].Trim().Replace("releaseyear \"", "").Replace("\"", "");
                                    }
                                }

                                goto GetPlayers;
                            }
                    }
                    catch
                    {
                        goto Dev;
                    }

                    // --------------------------------------------------------------------- //

                    Dev:
                    // If not found, search in "developer" repository, which happens to be more complete
                    using (WebClient c = new WebClient())
                        db_bytes = c.DownloadData(db_base + "developer/" + Uri.EscapeUriString(item.Value) + ".dat");

                    // Scan retrieved database
                    db_lines = System.Text.Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                    for (int i = 5; i < db_lines.Length; i++)
                        if (db_lines[i].ToLower().Contains(hash))
                        {
                            for (int x = i; x > i - 10; x--)
                                if (db_lines[x].Contains("comment \""))
                                {
                                    Title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                    ImgURL = "https://thumbnails.libretro.com/" + Uri.EscapeUriString(item.Value) + "/Named_Titles/" + Uri.EscapeUriString(Title) + ".png";
                                    goto GetPlayers;
                                }
                        }

                    goto NotFound;

                // --------------------------------------------------------------------- //

                GetPlayers:
                    // "maxusers" contains maximum number of players supported
                    using (WebClient c = new WebClient())
                        db_bytes = c.DownloadData(db_base + "maxusers/" + Uri.EscapeUriString(item.Value) + ".dat");

                    // Scan retrieved database
                    db_lines = System.Text.Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                    for (int i = 5; i < db_lines.Length; i++)
                        if (db_lines[i].Contains(Title))
                        {
                            for (int x = i; x < i + 5; x++)
                                if (db_lines[x].Contains("users "))
                                    Players = db_lines[x].Replace("\t", "").Replace("users ", "");
                        }
                    return;
                }
            }

        NotFound:
            System.Media.SystemSounds.Beep.Play();
            return;
        }
    }
}
