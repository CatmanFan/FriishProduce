using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Hashing;
using System.Net;
using System.Text;

namespace FriishProduce.Databases
{
    public static class LibRetro
    {
        #region -- PRIVATE VARIABLES --
        private static Platform platform = Platform.NES;

        private static Dictionary<string, Platform> list = new()
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
            { "Commodore - 64", Platform.C64 },
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

        private static string db_name
        {
            get
            {
                foreach (KeyValuePair<string, Platform> item in list)
                {
                    if (item.Value == platform)
                    {
                        return item.Key;
                    }
                }

                return null;
            }
        }

        private static string db_url(int i)
        {
            const string db_base = "https://raw.githubusercontent.com/libretro/libretro-database/refs/heads/master/metadat/";
            string[] folders = new string[] { "maxusers", "releaseyear" };

            if (db_name != null)
            {
                switch (platform)
                {
                    case Platform.PCECD:
                    case Platform.SMCD:
                    case Platform.PSX:
                        folders = new string[] { "developer", "redump" };
                        break;

                    case Platform.GCN:
                        folders = new string[0];
                        break;

                    case Platform.C64:
                        folders = new string[] { "no-intro" };
                        break;

                    case Platform.NEO:
                        folders = new string[] { "mame-split", "maxusers", "releaseyear" };
                        break;
                }

                return folders.Length == 0 ? db_base.Replace("metadat", "dat") + $"{db_name}.dat" : i < folders.Length ? db_base + folders[i] + $"/{db_name}.dat" : null;
            }

            else return null;
        }

        private static string db_img(string name, int source = 0)
        {
            return source == 1
                ? "https://archive.org/download/No-Intro_Thumbnails_2016-04-10/" + Uri.EscapeUriString(db_name) + ".zip/" + Uri.EscapeUriString(db_name) + "/Named_Titles/" + Uri.EscapeUriString(name.Replace('/', '_').Replace('&', '_')) + ".png"
                : "https://thumbnails.libretro.com/" + Uri.EscapeUriString(db_name) + "/Named_Titles/" + Uri.EscapeUriString(name.Replace('/', '_').Replace('&', '_') + ".png");
        }

        private static string db_crc(string input) => input.Replace("-", "").Trim().ToUpper().Substring(0, 8);
        #endregion

        public static bool Exists(Platform In)
        {
            foreach (KeyValuePair<string, Platform> item in list)
            {
                if (item.Value == platform)
                {
                    return true;
                }
            }

            return false;
        }

        public static DataTable Parse(Platform In)
        {
            Top:
            platform = In;
            DataTable dt = new DataTable(platform.ToString().ToLower());

            string path = Path.Combine(Paths.Databases, In.ToString().ToLower() + ".xml");

            if (File.Exists(path))
            {
                try { dt.ReadXml(path); }
                catch { try { File.Delete(path); } catch { } goto Top; }
            }

            else
            {
                if (!Directory.Exists(Paths.Databases)) Directory.CreateDirectory(Paths.Databases);

                string crc = "";
                string name = "";
                string serial = "";
                string releaseyear = "";
                string users = "";
                string image = "";

                dt.Columns.Add("crc", typeof(string));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("serial", typeof(string));
                dt.Columns.Add("releaseyear", typeof(string));
                dt.Columns.Add("users", typeof(string));
                dt.Columns.Add("image", typeof(string));

                // Retrieve database from URL or file
                // ****************
                List<string[]> db_lines = new();

                for (int i = 0; i < 2; i++)
                {
                    string url = db_url(i);

                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        if (File.Exists(url))
                            db_lines.Add(File.ReadAllLines(url));

                        else if (Web.InternetTest())
                        {
                            using (WebClient c = new WebClient())
                            {
                                try { db_lines.Add(Encoding.UTF8.GetString(Web.Get(url)).Split('\n')); }
                                catch { }
                            }
                        }
                    }
                }

                if (db_lines.Count == 0) return null;

                // Scan retrieved database for CRC32 hashes, and add to data table
                // ****************
                for (int i = db_lines[0].Length - 1; i > 1; i--)
                {
                    string line = db_lines[0][i];

                    if (line.Contains("crc "))
                    {
                        if (dt.Select($"crc = '{crc}'")?.Length == 0 && !string.IsNullOrWhiteSpace(name))
                        {
                            dt.Rows.Add(crc, name, null, releaseyear, users, image);
                            image = users = releaseyear = name = crc = null;
                        }

                        crc = db_crc(line.Substring(line.IndexOf("crc ") + 4, 8));
                    }

                    if (string.IsNullOrEmpty(name) && (line.Contains("name \"") || line.Contains("comment \"")) && !line.Contains("rom ("))
                    {
                        name = line.Replace("\t", "").Replace("name \"", "").Replace("comment \"", "").Replace("\"", "");
                        image = db_img(name);
                    }

                    if (line.Contains("year "))
                    {
                        releaseyear = line.Substring(line.IndexOf("year ") + 5).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                    }

                    if (line.Contains("users "))
                    {
                        users = line.Substring(line.IndexOf("users ") + 6).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                    }

                    if (line.Contains("serial "))
                    {
                        serial = line.Substring(line.IndexOf("serial ") + 7).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                    }
                }

                // Add release year, players and others
                // ****************
                for (int x = 1; x < db_lines.Count; x++)
                {
                    for (int y = db_lines[x].Length - 1; y > 1; y--)
                    {
                        string line = db_lines[x][y];

                        if (line.Contains("crc "))
                        {
                            crc = db_crc(line.Substring(line.IndexOf("crc ") + 4, 8));
                        }

                        if (line.Contains("serial "))
                        {
                            serial = line.Substring(line.IndexOf("serial ") + 7).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');

                            var rows = dt.Select($"crc = '{crc}' or serial = '{serial}'");
                            if (rows?.Length > 0 && string.IsNullOrWhiteSpace(rows[0][2]?.ToString()))
                            {
                                rows[0][2] = serial;
                            }
                        }

                        if (line.Contains("year "))
                        {
                            var rows = dt.Select($"crc = '{crc}' or serial = '{serial}'");
                            if (rows?.Length > 0 && string.IsNullOrWhiteSpace(rows[0][3]?.ToString()))
                            {
                                rows[0][3] = line.Substring(line.IndexOf("year ") + 5).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                            }
                        }

                        if (line.Contains("users "))
                        {
                            var rows = dt.Select($"crc = '{crc}' or serial = '{serial}'");
                            if (rows?.Length > 0 && string.IsNullOrWhiteSpace(rows[0][4]?.ToString()))
                            {
                                rows[0][4] = line.Substring(line.IndexOf("users ") + 6).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                            }
                        }
                    }
                }

                using DataView dv = dt.DefaultView;
                dv.Sort = "name";
                dt = dv.ToTable();
                dt.WriteXml(path, XmlWriteMode.WriteSchema);
            }

            return dt;
        }

        public static (string Name, string Serial, string Year, string Players, string Image, bool Complete) Read(string file, Platform platform)
        {
            // Get current CRC32 hash of file and append to query
            // **********************
            string crc32 = null;
            using (var fileStream = File.OpenRead(file))
            {
                var crc = new Crc32();
                crc.Append(fileStream);
                var hash_array = crc.GetCurrentHash();
                Array.Reverse(hash_array);
                crc32 = db_crc(BitConverter.ToString(hash_array));
            }

            DataTable dt = Parse(platform);

            if (dt != null)
            {
                var rows = dt.Select($"crc = '{crc32}'");
                if (rows?.Length > 0)
                {
                    try
                    {
                        using (WebClient c = new WebClient())
                        using (Stream s = c.OpenRead(rows[0][5]?.ToString()))
                        {
                            // Do something
                        }
                    }
                    catch
                    {
                        rows[0][5] = db_img(rows[0][1]?.ToString(), 1);

                        try
                        {
                            using (WebClient c = new WebClient())
                            using (Stream s = c.OpenRead(rows[0][5]?.ToString()))
                            {
                                // Do something
                            }
                        }
                        catch { rows[0][5] = null; }
                    }

                    (string name, string serial, string year, string players, string image) result = (rows[0][1]?.ToString(), rows[0][2]?.ToString(), rows[0][3]?.ToString(), rows[0][4]?.ToString(), rows[0][5]?.ToString());

                    bool complete = !string.IsNullOrEmpty(result.name) && !string.IsNullOrEmpty(result.players) && !string.IsNullOrEmpty(result.year) && !string.IsNullOrEmpty(result.image);
                    if (platform == Platform.C64) complete = !string.IsNullOrEmpty(result.name) && !string.IsNullOrEmpty(result.image);

                    return (result.name, result.serial, result.year, result.players, result.image, complete);
                }
            }

            return (null, null, null, null, null, false);
        }
    }
}