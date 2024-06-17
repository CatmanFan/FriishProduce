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
    public class GameData
    {
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

        private string crc32 { get; set; }
        private string serial { get; set; }

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

        private readonly string db_base = "https://raw.githubusercontent.com/libretro/libretro-database/master/metadat/";

        private string db_name(Platform platform)
        {
            Dictionary<string, Platform> names = new Dictionary<string, Platform>
            {
                { "Nintendo - Nintendo Entertainment System", Platform.NES },
                { "Nintendo - Super Nintendo Entertainment System", Platform.SNES },
                { "Nintendo - Nintendo 64", Platform.N64 },
                { "Sega - Master System - Mark III", Platform.SMS },
                { "Sega - Mega Drive - Genesis", Platform.SMD },
                { "NEC - PC Engine - TurboGrafx 16", Platform.PCE },
                { "NEC - PC Engine SuperGrafx", Platform.PCE },
                { "NEC - PC Engine CD - TurboGrafx-CD", Platform.PCECD },
                { "MAME", Platform.NEO },
                { "Microsoft - MSX", Platform.MSX },
                { "Microsoft - MSX2", Platform.MSX },
                { "Microsoft - MSX 2", Platform.MSX },
                { "Nintendo - Game Boy", Platform.GB },
                { "Nintendo - Game Boy Color", Platform.GBC },
                { "Nintendo - Game Boy Advance", Platform.GBA },
                { "Nintendo - GameCube", Platform.GCN },
                { "Sega - 32X", Platform.S32X },
                { "Sega - Mega-CD - Sega CD", Platform.SMCD },
                { "Sony - PlayStation", Platform.PSX },
            };

            foreach (KeyValuePair<string, Platform> item in names)
            {
                if (item.Value == platform)
                {
                    return item.Key;
                }
            }

            return null;
        }

        private bool db_search(string path, Platform platform, int type)
        {
            #region Establish URL of database entry
            string URL = null;
            string db_name = this.db_name(platform);

            if (db_name != null)
            {
                switch (platform)
                {
                    case Platform.PCECD:
                    case Platform.GCN:
                    case Platform.SMCD:
                    case Platform.PSX:
                        URL = db_base + "redump/" + Uri.EscapeUriString(db_name) + ".dat";
                        break;

                    case Platform.NEO:
                        URL = db_base + "mame-split/" + Uri.EscapeUriString(db_name) + " 2016.dat";
                        break;

                    default:
                        URL = db_base + "releaseyear/" + Uri.EscapeUriString(db_name) + ".dat";
                        break;
                }

                switch (type)
                {
                    case 1: // developer
                        URL = db_base + "developer/" + Uri.EscapeUriString(db_name) + ".dat";
                        break;

                    case 2: // maxusers
                        URL = db_base + "maxusers/" + Uri.EscapeUriString(db_name) + ".dat";
                        break;

                    case 3: // serial
                        URL = db_base + "serial/" + Uri.EscapeUriString(db_name) + ".dat";
                        break;
                }
            }
            #endregion

            if (URL != null)
            {
                try
                {
                    using (WebClient c = new WebClient())
                    {
                        // Scan retrieved database
                        // ****************
                        var db_lines = Encoding.UTF8.GetString(Web.Get(URL)).Split(Environment.NewLine.ToCharArray());

                        for (int i = 5; i < db_lines.Length; i++)
                        {
                            int searchMethod = serial != null && db_lines[i].ToLower().Contains(serial?.ToLower()) ? 2
                                             : db_lines[i].ToLower().Contains(Path.GetFileName(path).ToLower()) || db_lines[i].ToLower().Contains(crc32) ? 1
                                             : 0;

                            if (searchMethod != 0)
                            {
                                int x = i;
                                while ((searchMethod == 1 && x > i - 10) || (searchMethod == 2 && x < i + 10))
                                {
                                    string line = db_lines[x];

                                    if (title == null && (db_lines[x].Contains("name \"") || db_lines[x].Contains("comment \"")) && !db_lines[x].Contains("rom ("))
                                    {
                                        title = db_lines[x].Replace("\t", "").Replace("name \"", "").Replace("comment \"", "").Replace("\"", "");
                                    }

                                    if (serial == null && db_lines[x].Contains("serial "))
                                    {
                                        serial = db_lines[x].Substring(db_lines[x].IndexOf("serial ")).Replace("serial ", "").TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                                    }

                                    if (db_lines[x].Contains("releaseyear"))
                                    {
                                        year = db_lines[x].Trim().Replace("releaseyear \"", "").TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                                    }

                                    if (db_lines[x].Contains("users "))
                                    {
                                        players = db_lines[x].Replace("users ", "").TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                                    }

                                    if (searchMethod == 1) x--; else x++;
                                }

                                return true;
                            }
                        }
                    }
                }
                catch { return false; }
            }

            return false;
        }

        /// <summary>
        /// Gets any game metadata that is available for the file based on its CRC32 reading hash, including the software title, year, players, and title image URL.
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public bool Get(Platform platform, string path)
        {
            title = null;
            year = null;
            players = null;
            serial = null;
            imgURL = null;

            bool isDisc = platform == Platform.PCECD || platform == Platform.GCN || platform == Platform.SMCD || platform == Platform.PSX;
            if (isDisc)
            {
                if (Path.GetExtension(path).ToLower() == ".cue")
                    foreach (var item in Directory.EnumerateFiles(Path.GetDirectoryName(path)))
                        if (Path.GetExtension(item).ToLower() == ".bin" && Path.GetFileNameWithoutExtension(path).ToLower() == Path.GetFileNameWithoutExtension(item).ToLower())
                        {
                            path = item;
                        }

                if (Path.GetExtension(path).ToLower() != ".bin") goto NotFound;
            }

            using (var file = File.OpenRead(path))
            {
                var crc = new Crc32();
                crc.Append(file);
                var hash_array = crc.GetCurrentHash();
                Array.Reverse(hash_array);
                crc32 = BitConverter.ToString(hash_array).Replace("-", "").ToLower();
            }

            if (!Web.InternetTest("https://gbatemp.net/")) goto NotFound;

            db_search(path, platform, 0);
            if (title == null || year == null) db_search(path, platform, 1);

            if (title != null)
            {
                db_search(path, platform, 2); 

                #region Get image
                imgURL = setImgURL
                (
                    db_name(platform),
                    title.Replace('/', '_'),
                    Properties.Settings.Default.gamedata_source_image == 2 ? true : Properties.Settings.Default.gamedata_source_image == 1 ? false : !Web.InternetTest("https://thumbnails.libretro.com/README.md")
                );

                try
                {
                    using (WebClient c = new WebClient())
                    using (Stream s = c.OpenRead(imgURL))
                    {
                        // Do something
                    }
                }
                catch { imgURL = null; }
                #endregion

                return true;
            }

            else goto NotFound;

            NotFound:
            System.Media.SystemSounds.Beep.Play();
            return false;
        }
    }
}
