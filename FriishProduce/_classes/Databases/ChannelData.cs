using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FriishProduce
{
    public class ChannelDatabase
    {
        public class ChannelEntry
        {
            public string ID { get; set; }
            public List<int> Regions = new();
            public List<string> Titles = new();
            public List<int> EmuRevs = new();
            public List<string> MarioCube = new();

            public string GetID(int index, bool raw = false)
            {
                string r = "41";

                switch (Regions[index])
                {
                    default:
                    case 0:
                        r = "4a";
                        break;

                    case 1:
                        r = "45";
                        break;

                    case 2:
                        r = "4e";
                        break;

                    case 3:
                        r = "50";
                        break;

                    case 4:
                        r = "4c";
                        break;

                    case 5:
                        r = "4d";
                        break;

                    case 6:
                        r = "51";
                        break;

                    case 7:
                        r = "54";
                        break;
                }

                return raw ? ID.Replace("__", r).Replace("-", "").ToLower() : ID.Replace("__", r).ToLower();
            }

            public string GetUpperID(int index)
            {
                if (index == -1) return null;

                var hex = GetID(index).Substring(ID.Length - 8);
                var ascii = string.Empty;

                for (int i = 0; i < hex.Length; i += 2)
                {
                    var hs = hex.Substring(i, 2);
                    char character = Convert.ToChar(Convert.ToInt64(hs, 16));
                    ascii += character;
                }

                return ascii.ToUpper();
            }

            /// <summary>
            /// Gets a WAD file from the entry corresponding to the index entered and loads it to memory.
            /// </summary>
            public WAD GetWAD(int index)
            {
                string tID = GetUpperID(index);

                // Load Static WAD by default
                // ****************
                if (tID == null || tID?.Length < 4 || tID == "STLB") return WAD.Load(Properties.Resources.StaticBase);

                string reg = " (Japan)";
                switch (Regions[index])
                {
                    case 1:
                    case 2:
                        reg = " (USA)";
                        break;

                    case 3:
                    case 4:
                    case 5:
                        reg = " (Europe)";
                        break;

                    case 6:
                        reg = " (Korea) (Ja,Ko)";
                        break;

                    case 7:
                        reg = " (Korea) (En,Ko)";
                        break;
                }

                string console = "";
                switch (tID[0])
                {
                    case 'F':
                        console = "NES";
                        break;

                    case 'J':
                        console = "SNES";
                        break;

                    case 'N':
                        console = "N64";
                        break;

                    case 'L':
                        console = "SMS";
                        break;

                    case 'M':
                        console = "SMD";
                        break;

                    case 'E':
                        console = "NG";
                        break;

                    case 'P':
                    case 'Q':
                        console = "TGX";
                        break;

                    case 'C':
                        console = "C64";
                        break;

                    case 'X':
                        console = "MSX";
                        break;
                }

                string name = MarioCube[index] + reg;
                if (!string.IsNullOrWhiteSpace(console)) name += " (" + console + ")";

                // ****************
                // Load WAD from MarioCube.
                // I have done a less copyright-friendly workaround solution for now.
                // ------------------------------------------------
                // Sadly, the NUS downloader cannot decrypt VC/Wii Shop titles on its own without needing the ticket file.
                // Trying to generate a ticket locally using the leaked title key algorithm (https://gbatemp.net/threads/3ds-wii-u-titlekey-generation-algorithm-leaked.566318/) fails to decrypt contents to a readable format anyway.
                // ------------------------------------------------
                // Direct link is not included, for obvious reasons!
                // ****************
                string URL = "https://repo.mariocube.com/WADs/_WiiWare,%20VC,%20DLC,%20Channels%20&%20IOS/" + name[0].ToString().ToUpper() + "/" + Uri.EscapeDataString(name + " (Virtual Console)") + ".wad";
                if (GetUpperID(index).StartsWith("WNA"))
                {
                    URL = "https://repo.mariocube.com/WADs/Flash%20Injects/Base/" + Uri.EscapeDataString(name) + ".wad";
                    tID = "WNAP";
                }

                Web.InternetTest();

                WAD w = WAD.Load(Web.Get(URL));

                // Title ID check
                // ****************
                if (w.UpperTitleID.ToUpper() == tID && w.HasBanner) return w;
                return null;
            }
        }

        public List<ChannelEntry> Entries { get; private set; }

        #region Standalone Database
        /// <summary>
        /// Loads the Static Base WAD.
        /// </summary>
        public ChannelDatabase()
        {
            Entries = new List<ChannelEntry>();

            if (!File.Exists(Properties.Settings.Default.custom_database))
            {
                Properties.Settings.Default.custom_database = null;
                Properties.Settings.Default.Save();
            }

            getStaticBase();
        }

        private readonly string error = "The database format or styling is not valid.";

        private void getStaticBase()
        {
            Entries = new List<ChannelEntry>();

            var y = new ChannelEntry() { ID = "00010001-53544c42" };
            y.Regions.Add(8);
            y.Titles.Add("Static Base");
            y.EmuRevs.Add(0);
            y.MarioCube.Add("");

            Entries.Add(y);

            if (Entries.Count == 0) throw new Exception(error);
        }
        #endregion

        #region Platform Databases
        /// <summary>
        /// Loads a database of WADs for a selected console/platform.
        /// </summary>
        public ChannelDatabase(Platform c, string externalFile = null)
        {
            string file = File.Exists(externalFile) ? externalFile : File.Exists(Properties.Settings.Default.custom_database) ? Properties.Settings.Default.custom_database : null;

            Entries = new List<ChannelEntry>();

            if (!File.Exists(Properties.Settings.Default.custom_database))
            {
                Properties.Settings.Default.custom_database = null;
                Properties.Settings.Default.Save();
            }

            try
            {
                GetEntries(c, File.ReadAllBytes(file));
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(externalFile)) throw;
                else GetEntries(c, Properties.Resources.Database);
            }
        }

        private void GetEntries(Platform c, byte[] file)
        {
            Entries = new List<ChannelEntry>();

            using (MemoryStream ms = new(file))
            using (StreamReader sr = new(ms, Encoding.Unicode))
            using (var doc = JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }))
            {
                var x = doc.Deserialize<JsonElement>(new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip }).GetProperty(c.ToString().ToLower());
                if (x.ValueKind != JsonValueKind.Array) throw new Exception(error);

                sr.Dispose();
                ms.Dispose();

                foreach (var item in x.EnumerateArray())
                {
                    ChannelEntry y = new() { ID = item.GetProperty("id").GetString() };
                    var reg = item.GetProperty("region");

                    for (int i = 0; i < reg.GetArrayLength(); i++)
                    {
                        y.Regions.Add(reg[i].GetInt32());
                        try { y.Titles.Add(item.GetProperty("titles")[i].GetString()); } catch { y.Titles.Add(item.GetProperty("titles")[Math.Max(0, item.GetProperty("titles").GetArrayLength() - 1)].GetString()); }
                        try { y.EmuRevs.Add(item.GetProperty("emu_ver")[i].GetInt32()); } catch { y.EmuRevs.Add(0); }
                    }

                    for (int i = 0; i < y.Regions.Count; i++)
                    {
                        try { y.MarioCube.Add(item.GetProperty("wad_titles")[i].GetString()); }
                        catch
                        {
                            try { y.MarioCube.Add(item.GetProperty("wad_titles")[Math.Max(0, item.GetProperty("wad_titles").GetArrayLength() - 1)].GetString()); }
                            catch
                            {
                                var title = y.Titles[y.Regions.Count > 1 ? 1 : 0];
                                if (title.StartsWith("The "))
                                {
                                    title = title.Substring(4);
                                    if (title.Contains(": ")) title = title.Replace(": ", ", The: ");
                                    else title += ", The";
                                }
                                y.MarioCube.Add(title.Replace(": ", " - ").Replace('é', 'e'));
                            }
                        }
                    }

                    Entries.Add(y);
                }
            }

            if (Entries.Count == 0) throw new Exception(error);
        }
        #endregion
    }
}
