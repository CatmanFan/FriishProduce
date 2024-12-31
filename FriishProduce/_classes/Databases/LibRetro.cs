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

        private static string db_name
        {
            get
            {
                Dictionary<string, Platform> names = new()
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
                    case Platform.GCN:
                    case Platform.SMCD:
                    case Platform.PSX:
                        folders = new string[] { "redump", "maxusers", "releaseyear" };
                        break;

                    case Platform.NEO:
                        folders = new string[] { "mame-split", "maxusers", "releaseyear" };
                        break;
                }

                return i < folders.Length ? db_base + folders[i] + $"/{db_name}.dat" : null;
            }

            else return null;
        }
        #endregion

        public static DataTable Parse(Platform In)
        {
            Top:
            DataTable dt = new DataTable(platform.ToString().ToLower());

            string path = Path.Combine(Paths.Databases, In.ToString().ToLower() + ".xml");

            if (File.Exists(path))
            {
                try { dt.ReadXml(path); }
                catch { File.Delete(path); goto Top; }
            }

            else
            {
                platform = In;
                string crc = "";
                string name = "";
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
                    if (!string.IsNullOrWhiteSpace(db_url(i)))
                    {
                        if (File.Exists(db_url(i)))
                            db_lines.Add(File.ReadAllLines(db_url(i)));

                        else if (Web.InternetTest())
                        {
                            using (WebClient c = new WebClient())
                            {
                                db_lines.Add(Encoding.UTF8.GetString(Web.Get(db_url(i))).Split('\n'));
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
                        crc = line.Substring(line.IndexOf("crc ") + 4, 8).Trim().ToUpper().Substring(0, 7);

                        if (dt.Select($"crc = '{crc}'")?.Length == 0)
                        {
                            dt.Rows.Add(crc, name, null, null, users, image);
                            image = users = name = crc = null;
                        }
                    }

                    if (name == null && (line.Contains("name \"") || line.Contains("comment \"")) && !line.Contains("rom ("))
                    {
                        name = line.Replace("\t", "").Replace("name \"", "").Replace("comment \"", "").Replace("\"", "");
                        image = "https://archive.org/download/No-Intro_Thumbnails_2016-04-10/" + Uri.EscapeUriString(db_name) + ".zip/" + Uri.EscapeUriString(db_name) + "/Named_Titles/" + Uri.EscapeUriString(name.Replace('/', '_')) + ".png";
                    }

                    if (line.Contains("year "))
                    {
                        var rows = dt.Select($"crc = '{crc}'");
                        if (rows?.Length > 0)
                        {
                            rows[0][3] = line.Substring(line.IndexOf("year ") + 5).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                        }
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
                            crc = line.Substring(line.IndexOf("crc ") + 4, 8).Trim().ToUpper().Substring(0, 7);
                        }

                        if (line.Contains("serial "))
                        {
                            var rows = dt.Select($"crc = '{crc}'");
                            if (rows?.Length > 0)
                            {
                                rows[0][2] = line.Substring(line.IndexOf("serial ") + 7).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                            }
                        }

                        if (line.Contains("year "))
                        {
                            var rows = dt.Select($"crc = '{crc}'");
                            if (rows?.Length > 0 && string.IsNullOrWhiteSpace(rows[0][3]?.ToString()))
                            {
                                rows[0][3] = line.Substring(line.IndexOf("year ") + 5).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                            }
                        }

                        if (line.Contains("users "))
                        {
                            var rows = dt.Select($"crc = '{crc}'");
                            if (rows?.Length > 0)
                            {
                                rows[0][4] = line.Substring(line.IndexOf("users ") + 6).TrimStart('\"', ' ', '\t', ')').TrimEnd('\"', ' ', '\t', ')');
                            }
                        }
                    }
                }

                dt.WriteXml(path);
            }

            return dt;
        }

        public static (string Name, string Serial, string Year, string Players, string Image) Read(string file, Platform platform)
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
                crc32 = BitConverter.ToString(hash_array).Replace("-", "").ToUpper();
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
                    catch { rows[0][5] = null; }

                    return (rows[0][1]?.ToString(), rows[0][2]?.ToString(), rows[0][3]?.ToString(), rows[0][4]?.ToString(), rows[0][5]?.ToString());
                }
            }

            return (null, null, null, null, null);
        }
    }
}