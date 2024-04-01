using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FriishProduce
{
    public class Web
    {
        public static void InternetTest()
        {
            string URL = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create
                (
                    System.Globalization.CultureInfo.InstalledUICulture.Name.StartsWith("fa") ? "https://www.aparat.com/" :
                    System.Globalization.CultureInfo.InstalledUICulture.Name.Contains("zh-CN") ? "http://www.baidu.com/" :
                    "https://www.google.com/"
                );

                URL = request.Address.Authority;
                request.Method = "HEAD";
                request.KeepAlive = false;
                request.Timeout = 30000;

                var response = request.GetResponse();

                for (int i = 0; i < 2; i++)
                {
                    char x = response.ResponseUri.ToString()[i];
                }
            }

            catch (WebException ex)
            {
                string message = (ex.Message.Contains(URL) ? ex.Message.Substring(0, ex.Message.IndexOf(':')) : ex.Message) + (ex.Message[ex.Message.Length - 1] != '.' ? "." : string.Empty);
                throw new Exception(string.Format(Program.Lang.Msg(0, true), message));
            }
        }

        public static byte[] Get(string URL)
        {
            // Actual web connection is done here
            using (MemoryStream ms = new MemoryStream())
            using (WebClient x = new WebClient())
            {
                if (Task.WaitAny
                    (new Task[]{Task.Run(() =>
                        {
                            Stream webS = x.OpenRead(URL);
                            try { webS.CopyTo(ms); return ms; } catch { return null; }
                        }
                    )}, 100 * 1000) == -1)
                    throw new TimeoutException();

                if (ms.ToArray().Length > 75) return ms.ToArray();

                throw new WebException();
            }
        }
    }

    [Serializable]
    public class LibRetroDB
    {
        public string SoftwarePath { get; set; }
        private string Title;
        private string Year;
        private string ImgURL;
        private string Players;

        public string GetYear() => Year;

        public string GetTitle() => Title;

        public string GetCleanTitle(bool isMultiline = false)
        {
            if (Title == null) return null;

            string title = Regex.Replace(Title?.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "");
            if (title.Contains(", The")) title = "The " + title.Replace(", The", string.Empty);
            return title.Trim();
        }

        public string GetImgURL() => ImgURL;

        public string GetPlayers() => Players;

        public bool Get(Console platform)
        {
            Title = null;
            Year = null;
            ImgURL = null;
            Players = null;

            Web.InternetTest();

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

            bool TitleIsSet = false;
            bool YearIsSet = false;

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
                                            Title = db_lines[x].Replace("\t", "").Replace("name \"", "").Replace("\"", "");
                                            ImgURL = "https://thumbnails.libretro.com/" + Uri.EscapeUriString(item.Key) + "/Named_Titles/" + Uri.EscapeUriString(Title.Replace('/', '_')) + ".png";
                                            TitleIsSet = true;
                                        }

                                        if (db_lines[x].Contains("year \""))
                                        {
                                            Year = db_lines[x].Replace("\t", "").Replace("year \"", "").Replace("\"", "");
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
                                            Title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                            ImgURL = "https://thumbnails.libretro.com/" + Uri.EscapeUriString(item.Key) + "/Named_Titles/" + Uri.EscapeUriString(Title) + ".png";
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
                                            Title = db_lines[x].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                                            TitleIsSet = true;
                                            ImgURL = "https://thumbnails.libretro.com/" + Uri.EscapeUriString(item.Key) + "/Named_Titles/" + Uri.EscapeUriString(Title) + ".png";
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
                                        Players = db_lines[x].Replace("\t", "").Replace("users ", "");
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

                    return true;
                }
            }

        NotFound:
            System.Media.SystemSounds.Beep.Play();
            return false;
        }
    }
}
