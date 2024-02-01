using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public class Web
    {
        public static void InternetTest()
        {
            using (Ping p = new Ping())
            {
                try
                {
                    PingReply r = p.Send("google.com", 3000);
                    if (r.Status != IPStatus.Success || r == null) throw new WebException(Program.Language.Get("error000"), WebExceptionStatus.Timeout);
                }
                catch
                {
                    throw new WebException(Program.Language.Get("error000"), WebExceptionStatus.ConnectFailure);
                }
            }
        }
    }

    public class LibRetroDB
    {
        public string SoftwarePath { get; set; }
        private string Title;
        private string Year;
        private string ImgURL;
        private string Players;
        private string Serial;

        public string GetYear() => Year;

        public string GetTitle() => Title;

        public string GetCleanTitle()
        {
            string title = System.Text.RegularExpressions.Regex.Replace(Title.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "");
            if (title.Contains(", The")) title = "The " + title.Replace(", The", string.Empty);
            return title.Trim();
        }

        public string GetImgURL() => ImgURL;

        public string GetPlayers() => Players;

        public string GetSerial() => Serial;

        public bool GetData(Console platform)
        {
            Title = null;
            Year = null;
            ImgURL = null;
            Players = null;
            Serial = null;

            Web.InternetTest();

            var crc = new Crc32();
            using (var file = File.OpenRead(SoftwarePath))
                crc.Append(file);

            var hash_array = crc.GetCurrentHash();
            Array.Reverse(hash_array);
            string hash = BitConverter.ToString(hash_array).Replace("-", "").ToLower();

            // Original: https://github.com/libretro/libretro-database/raw/master/metadat/
            string db_base = "https://raw.githubusercontent.com/libretro/libretro-database/master/metadat/";
            Dictionary<Console, string> db_platforms = new Dictionary<Console, string>
            {
                { Console.NES, "Nintendo - Nintendo Entertainment System" },
                { Console.SNES, "Nintendo - Super Nintendo Entertainment System" },
                { Console.N64, "Nintendo - Nintendo 64" },
                { Console.SMS, "Sega - Master System - Mark III" },
                { Console.SMDGEN, "Sega - Mega Drive - Genesis" }
            };
            bool TitleIsSet = false;
            bool YearIsSet = false;

            foreach (KeyValuePair<Console, string> item in db_platforms)
            {
                if (item.Key == platform)
                {
                    byte[] db_bytes = { 0x00 };

                    // --------------------------------------------------------------------- //

                    string[] db_lines = new string[1];

                    try
                    {
                        // Search in "releaseyear" repository
                        using (WebClient c = new WebClient())
                            db_bytes = c.DownloadData(db_base + "releaseyear/" + Uri.EscapeUriString(item.Value) + ".dat");

                        db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                        // Scan retrieved database
                        for (int i = 10; i < db_lines.Length; i++)
                            if (db_lines[i].ToLower().Contains(hash))
                            {
                                for (int x = i + 1; x > i - 11; x--)
                                {
                                    if (db_lines[x].Contains("comment \"") && !TitleIsSet)
                                    {
                                        Title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                        ImgURL = "https://thumbnails.libretro.com/" + Uri.EscapeUriString(item.Value) + "/Named_Titles/" + Uri.EscapeUriString(Title) + ".png";
                                        TitleIsSet = true;
                                    }
                                    if (db_lines[x].Contains("releaseyear") && !YearIsSet)
                                    {
                                        Year = db_lines[x].Trim().Replace("releaseyear \"", "").Replace("\"", "");
                                        YearIsSet = true;
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
                    db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

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
                    db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                    for (int i = 5; i < db_lines.Length; i++)
                        if (db_lines[i].Contains(Title))
                        {
                            for (int x = i; x < i + 5; x++)
                                if (db_lines[x].Contains("users "))
                                    Players = db_lines[x].Replace("\t", "").Replace("users ", "");
                        }

                    // --------------------------------------------------------------------- //

                    // Get original serial (title or content ID) of game
                    using (WebClient c = new WebClient())
                        db_bytes = c.DownloadData(db_base + "serial/" + Uri.EscapeUriString(item.Value) + ".dat");

                    // Scan retrieved database
                    db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                    for (int i = 5; i < db_lines.Length; i++)
                        if (db_lines[i].Contains(Title))
                        {
                            for (int x = i; x < i + 5; x++)
                                if (db_lines[x].Contains("serial "))
                                    Serial = db_lines[x].Replace("\t", "").Replace("serial ", "").Replace("\"", "");
                        }
                    return true;
                }
            }

        NotFound:
            System.Media.SystemSounds.Beep.Play();
            return false;
        }
    }
}
