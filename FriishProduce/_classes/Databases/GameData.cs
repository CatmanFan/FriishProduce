using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace FriishProduce
{
    [Serializable]
    public class GameDatabase
    {
        public string SoftwarePath { get; set; }

        private string title { get; set; }
        public string Title { get => title; }
        public string CleanTitle
        {
            get
            {
                if (Title == null) return null;

                string title = Regex.Replace(Title?.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "");
                if (title.Contains(", The")) title = "The " + title.Replace(", The", string.Empty);
                return title.Trim();
            }
        }

        private string year { get; set; }
        public string Year { get => year; }

        private string players { get; set; }
        public string Players { get => players; }

        private string setImgURL(string console, string title, bool useGitHub)
        {
            return !useGitHub ? "https://thumbnails.libretro.com/" + Uri.EscapeUriString(console) + "/Named_Titles/" + Uri.EscapeUriString(title) + ".png"
                                : "https://github.com/libretro/libretro-thumbnails/blob/master/" + Uri.EscapeUriString(console) + "/Named_Titles/" + Uri.EscapeUriString(title) + ".png?raw=true";
        }
        private string imgURL { get; set; }
        public string ImgURL { get => imgURL; }

        /// <summary>
        /// Gets any game metadata that is available for the file based on its CRC32 reading hash, including the software title, year, players, and title image URL.
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public bool Get(Console platform)
        {
            title = null;
            year = null;
            players = null;
            imgURL = null;

            bool TitleIsSet = false;
            bool YearIsSet = false;

            if (!Web.InternetTest("https://gbatemp.net/", 6)) goto NotFound;
            bool useGitHub = Properties.Settings.Default.gamedata_source_image == 2 ? true : Properties.Settings.Default.gamedata_source_image == 1 ? false : Web.InternetTest("https://thumbnails.libretro.com/", 6);

            // Original: https://github.com/libretro/libretro-database/raw/master/metadat/
            // ****************
            string db_base = "https://raw.githubusercontent.com/libretro/libretro-database/master/metadat/";
            Dictionary<string, Console> db_platforms = new Dictionary<string, Console>
            {
                { "Nintendo - Nintendo Entertainment System", Console.NES },
                { "Nintendo - Super Nintendo Entertainment System", Console.SNES },
                { "Nintendo - Nintendo 64", Console.N64 },
                { "Sega - Master System - Mark III", Console.SMS },
                { "Sega - Mega Drive - Genesis", Console.SMD },
                { "NEC - PC Engine - TurboGrafx 16", Console.PCE },
                { "NEC - PC Engine SuperGrafx", Console.PCE },
                { "MAME", Console.NEO },
                { "Microsoft - MSX", Console.MSX },
                { "Microsoft - MSX2", Console.MSX },
                { "Microsoft - MSX 2", Console.MSX },
            };

            string hash;
            using (var file = File.OpenRead(SoftwarePath))
            {
                var crc = new Crc32();
                crc.Append(file);
                var hash_array = crc.GetCurrentHash();
                Array.Reverse(hash_array);
                hash = BitConverter.ToString(hash_array).Replace("-", "").ToLower();
            }

            foreach (KeyValuePair<string, Console> item in db_platforms)
            {
                if (item.Value == platform)
                {
                    byte[] db_bytes = { 0x00 };

                    // --------------------------------------------------------------------- //

                    string[] db_lines = new string[1];

                    if (platform == Console.NEO)
                    {
                        var size = File.ReadAllBytes(SoftwarePath).Length.ToString();

                        try
                        {
                            using (WebClient c = new WebClient())
                                db_bytes = Web.Get(db_base + "mame-split/" + Uri.EscapeUriString(item.Key) + " 2016.dat");

                            // Scan retrieved database
                            // ****************
                            db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                            for (int i = 5; i < db_lines.Length; i++)
                                if (db_lines[i].ToLower().Contains(Path.GetFileName(SoftwarePath).ToLower()) || db_lines[i].ToLower().Contains(hash))
                                {
                                    for (int x = i; x > i - 10; x--)
                                    {
                                        string test = db_lines[x];
                                        if (db_lines[x].Contains("name \""))
                                        {
                                            title = db_lines[x].Replace("\t", "").Replace("name \"", "").Replace("\"", "");
                                            imgURL = setImgURL(item.Key, title.Replace('/', '_'), useGitHub);
                                            TitleIsSet = true;
                                        }

                                        if (db_lines[x].Contains("year \""))
                                        {
                                            year = db_lines[x].Replace("\t", "").Replace("year \"", "").Replace("\"", "");
                                            YearIsSet = true;
                                        }
                                    }

                                    return TitleIsSet || YearIsSet;
                                }

                        }
                        catch
                        {

                        }

                        goto NotFound;
                    }

                    else
                    {
                        try
                        {
                            // Search in "releaseyear" repository
                            // ****************
                            using (WebClient c = new WebClient())
                                db_bytes = Web.Get(db_base + "releaseyear/" + Uri.EscapeUriString(item.Key) + ".dat");

                            db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                            // Scan retrieved database
                            // ****************
                            for (int i = 10; i < db_lines.Length; i++)
                                if (db_lines[i].ToLower().Contains(hash))
                                {
                                    for (int x = i + 1; x > i - 11; x--)
                                    {
                                        if (db_lines[x].Contains("comment \"") && !TitleIsSet)
                                        {
                                            title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                            imgURL = setImgURL(item.Key, title.Replace('/', '_'), useGitHub);
                                            TitleIsSet = true;
                                        }
                                        if (db_lines[x].Contains("releaseyear") && !YearIsSet)
                                        {
                                            year = db_lines[x].Trim().Replace("releaseyear \"", "").Replace("\"", "");
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
                        try
                        {
                            // If not found, search in "developer" repository, which happens to be more complete
                            // ****************
                            using (WebClient c = new WebClient())
                                db_bytes = Web.Get(db_base + "developer/" + Uri.EscapeUriString(item.Key) + ".dat");

                            // Scan retrieved database
                            // ****************
                            db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                            for (int i = 5; i < db_lines.Length; i++)
                                if (db_lines[i].ToLower().Contains(hash))
                                {
                                    for (int x = i; x > i - 10; x--)
                                        if (db_lines[x].Contains("comment \"") && !TitleIsSet)
                                        {
                                            title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                            imgURL = setImgURL(item.Key, title.Replace('/', '_'), useGitHub);
                                            TitleIsSet = true;

                                            goto GetPlayers;
                                        }
                                }

                        }
                        catch
                        {
                        }

                        goto NotFound;
                    }

                // --------------------------------------------------------------------- //

                GetPlayers:
                    try
                    {
                        // "maxusers" contains maximum number of players supported
                        // ****************
                        using (WebClient c = new WebClient())
                            db_bytes = Web.Get(db_base + "maxusers/" + Uri.EscapeUriString(item.Key) + ".dat");

                        // Scan retrieved database
                        // ****************
                        db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                        for (int i = 5; i < db_lines.Length; i++)
                            if (db_lines[i].Contains(Title))
                            {
                                for (int x = i; x < i + 5; x++)
                                    if (db_lines[x].Contains("users "))
                                        players = db_lines[x].Replace("\t", "").Replace("users ", "");
                            }
                    }
                    catch
                    {

                    }

                    // --------------------------------------------------------------------- //

                    // Get original serial (title or content ID) of game
                    // ****************
                    /* using (WebClient c = new WebClient())
                        db_bytes = Web.Get(db_base + "serial/" + Uri.EscapeUriString(item.Key) + ".dat");

                    // Scan retrieved database
                    // ****************
                    db_lines = Encoding.UTF8.GetString(db_bytes).Split(Environment.NewLine.ToCharArray());

                    for (int i = 5; i < db_lines.Length; i++)
                        if (db_lines[i].Contains(Title))
                        {
                            for (int x = i; x < i + 5; x++)
                                if (db_lines[x].Contains("serial "))
                                    Serial = db_lines[x].Replace("\t", "").Replace("serial ", "").Replace("\"", "");
                        } */

                    try
                    {
                        using (WebClient c = new WebClient())
                        using (Stream s = c.OpenRead(imgURL))
                        {
                            // Do something
                        }
                    }
                    catch { imgURL = null; }

                    return true;
                }
            }

        NotFound:
            System.Media.SystemSounds.Beep.Play();
            return false;
        }
    }
}
