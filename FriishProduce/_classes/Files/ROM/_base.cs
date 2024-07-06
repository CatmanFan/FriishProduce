using Ionic.Zip;
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
    public abstract class ROM
    {
        private string _rom;
        public string FilePath
        {
            get => _rom;

            set
            {
                if (value != null)
                {
                    try
                    {
                        // -----------------------
                        // Check if raw ROM exists
                        // -----------------------
                        if (!File.Exists(value))
                            throw new FileNotFoundException(new FileNotFoundException().Message, value);

                        _rom = value;

                        if (System.IO.Path.GetExtension(value).ToLower() == ".zip") try { ZIP = new ZipFile(value); } catch { ZIP = null; }
                        if (ZIP == null) origData = File.ReadAllBytes(value);
                        Load();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                        _rom = null;
                        origData = null;
                        ZIP = null;
                    }
                }
            }
        }

        protected byte[] origData = new byte[0];
        protected byte[] patched = new byte[0];

        public byte[] Bytes { get => patched?.Length > 0 ? patched : origData; }

        public int MaxSize { get; set; }

        protected ZipFile ZIP { get; set; }

        public ROM()
        {
            _rom = null;
            origData = null;
            patched = null;
            ZIP = null;
            MaxSize = -1;
        }

        protected virtual void Load() { }

        public virtual bool CheckValidity(string path)
        {
            return true;
        }

        public virtual bool CheckZIPValidity(string[] strings, bool searchEndingOnly, bool forceLowercase, string path = null)
        {
            if (path == null && FilePath != null) path = FilePath;
            if (!File.Exists(path)) return true;

            using (ZipFile ZIP = ZipFile.Read(path))
            {
                int applicable = 0;

                foreach (var item in ZIP.Entries)
                    foreach (string line in strings)
                    {
                        string name = forceLowercase ? item.FileName.ToLower() : item.FileName;
                        if ((searchEndingOnly && (name.EndsWith(line) || System.IO.Path.GetFileNameWithoutExtension(name).EndsWith(line)))
                            || (!searchEndingOnly && name.Contains(line)))
                            applicable++;
                    }

                if (applicable < strings.Length)
                {
                    return false;
                }

                return true;
            }
        }

        public bool CheckSize(int length = 0)
        {
            if (length == 0) length = MaxSize;

            if (Bytes.Length > length && MaxSize > 0)
            {
                bool isMB = length >= 1048576;
                throw new Exception(string.Format(Program.Lang.Msg(3, true),
                    Math.Round((double)length / (isMB ? 1048576 : 1024), 2).ToString(),
                    isMB ? Program.Lang.String("megabytes") : Program.Lang.String("kilobytes")));
            }

            return true;
        }

        public void Patch(string filePath)
        {
            if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(filePath)) return;

            File.WriteAllBytes(Paths.WorkingFolder + "rom", origData);
            File.WriteAllBytes(Paths.WorkingFolder + "patch", File.ReadAllBytes(filePath));

            Utils.Run(FileDatas.Apps.xdelta3, "xdelta3", "-d -s rom patch rom_p_xdelta");
            Utils.Run(FileDatas.Apps.flips, "flips", "--apply patch rom rom_p_bps");

            // -----------------------
            // Check if patch applied successfully
            // -----------------------
            if (File.Exists(Paths.WorkingFolder + "rom")) File.Delete(Paths.WorkingFolder + "rom");
            if (File.Exists(Paths.WorkingFolder + "patch")) File.Delete(Paths.WorkingFolder + "patch");

            var Out = File.Exists(Paths.WorkingFolder + "rom_p_bps") ? File.ReadAllBytes(Paths.WorkingFolder + "rom_p_bps")
                : File.Exists(Paths.WorkingFolder + "rom_p_xdelta") ? File.ReadAllBytes(Paths.WorkingFolder + "rom_p_xdelta")
                : null;

            // -----------------------
            // Delete files
            // -----------------------
            if (File.Exists(Paths.WorkingFolder + "rom_p_xdelta")) File.Delete(Paths.WorkingFolder + "rom_p_xdelta");
            if (File.Exists(Paths.WorkingFolder + "rom_p_bps")) File.Delete(Paths.WorkingFolder + "rom_p_bps");

            if (Out == null) throw new Exception(Program.Lang.Msg(7, true));

            patched = Out;
        }

        /// <summary>
        /// Gets any game metadata that is available for the file based on its CRC32 reading hash, including the software title, year, players, and title image URL.
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public (string Title, string Year, string Players, string Serial, string Image) GetData(Platform platform, string path)
        {
            string crc32 = null;
            string title = null;
            string year = null;
            string players = null;
            string serial = null;
            string imgURL = null;

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

            if (!Web.InternetTest()) goto NotFound;

            var values = db_search(path, crc32, platform, 0);
            (serial, title, year, players) = (values.serial ?? serial, values.title ?? title, values.year ?? year, values.players ?? players);

            if (title == null || year == null)
            {
                values = db_search(path, crc32, platform, 1);
                (serial, title, year, players) = (values.serial ?? serial, values.title ?? title, values.year ?? year, values.players ?? players);
            }

            if (title != null)
            {
                CleanTitle = Regex.Replace(title?.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "");
                if (CleanTitle.Contains(", The")) CleanTitle = "The " + CleanTitle.Replace(", The", string.Empty);
                CleanTitle = CleanTitle.Trim();

                values = db_search(path, crc32, platform, 2);
                (serial, title, year, players) = (values.serial ?? serial, values.title ?? title, values.year ?? year, values.players ?? players);

                #region Get image
                imgURL = setImgURL
                (
                    db_name(platform),
                    title.Replace('/', '_'),
                    Properties.Settings.Default.gamedata_source_image == 2 ? true : Properties.Settings.Default.gamedata_source_image == 1 ? false : !Web.InternetTest()
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

                CleanTitle = CleanTitle.Trim();
                return (title, year, players, serial, imgURL);
            }

            else goto NotFound;

            NotFound:
            System.Media.SystemSounds.Beep.Play();
            return (null, null, null, null, null);
        }

        public string CleanTitle { get; protected set; }

        #region -- Private gamedata variables and functions --
        private readonly string db_base = "https://raw.githubusercontent.com/libretro/libretro-database/master/metadat/";

        private string setImgURL(string console, string title, bool useGitHub)
        {
            return !useGitHub ? "https://thumbnails.libretro.com/" + Uri.EscapeUriString(console) + "/Named_Titles/" + Uri.EscapeUriString(title) + ".png"
                                : "https://github.com/libretro/libretro-thumbnails/blob/master/" + Uri.EscapeUriString(console) + "/Named_Titles/" + Uri.EscapeUriString(title) + ".png?raw=true";
        }

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

        private (string serial, string title, string year, string players) db_search(string path, string crc32, Platform platform, int type)
        {
            string serial = null;
            string title = null;
            string year = null;
            string players = null;

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

                                return (serial, title, year, players);
                            }
                        }
                    }
                }
                catch { return (null, null, null, null); }
            }

            return (null, null, null, null);
        }
        #endregion
    }
}
