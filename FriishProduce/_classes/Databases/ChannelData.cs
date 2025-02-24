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
            public int Count { get => (Regions?.Count == Titles?.Count) && (Titles?.Count == MarioCube?.Count) ? Titles?.Count ?? 0 : -1; }
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

            public string[] GetUpperIDs()
            {
                List<string> list = new();

                for (int i = 0; i < Regions.Count; i++)
                {
                    list.Add(GetUpperID(i));
                }

                return list.ToArray();
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
            /// Gets a WAD file from the entry corresponding to the index entered.
            /// </summary>
            public string GetWAD(int index)
            {
                string tID = GetUpperID(index);

                // Load Static WAD by default
                // ****************
                if (tID == null || tID?.Length < 4 || tID == "STLB") return null;

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
                string folder = int.TryParse(name[0].ToString(), out int result) ? "0-9" : name[0].ToString().ToUpper();
                string URL = "https://repo.mariocube.com/WADs/_WiiWare,%20VC,%20DLC,%20Channels%20&%20IOS/" + folder + "/" + Uri.EscapeDataString(name + " (Virtual Console)") + ".wad";
                if (GetUpperID(index).StartsWith("WNA"))
                {
                    URL = "https://repo.mariocube.com/WADs/Flash%20Injects/Base/" + Uri.EscapeDataString(name) + ".wad";
                    tID = "WNAP";
                }
                else if (GetUpperID(index).StartsWith("HCJ"))
                {
                    int ver = 768;
                    URL = "https://repo.mariocube.com/WADs/_WiiWare,%20VC,%20DLC,%20Channels%20&%20IOS/" + folder + "/" + Uri.EscapeDataString(name + $" (v{ver}) (Channel)") + ".wad";
                }
                else if (GetUpperID(index).StartsWith("HCX"))
                {
                    int ver = Regions[index] == 0 ? 768 : Regions[index] == 3 ? 1537 : 1536;
                    URL = "https://repo.mariocube.com/WADs/_WiiWare,%20VC,%20DLC,%20Channels%20&%20IOS/" + folder + "/" + Uri.EscapeDataString(name + $" (v{ver}) (Channel)") + ".wad";
                }

                return URL;
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

            if (!File.Exists(Program.Config.paths.database))
            {
                Program.Config.paths.database = null;
                Program.Config.Save();
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
            string file = File.Exists(externalFile) && !string.IsNullOrWhiteSpace(externalFile) ? externalFile : File.Exists(Program.Config.paths.database) ? Program.Config.paths.database : null;

            Entries = new List<ChannelEntry>();

            if (!File.Exists(Program.Config.paths.database) && !string.IsNullOrWhiteSpace(Program.Config.paths.database))
            {
                Program.Config.paths.database = null;
                Program.Config.Save();
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

                        try { y.Titles.Add(item.GetProperty("titles")[i].GetString()); }
                        catch { y.Titles.Add(item.GetProperty("titles")[Math.Max(0, item.GetProperty("titles").GetArrayLength() - 1)].GetString()); }

                        try { y.EmuRevs.Add(item.GetProperty("emu_ver")[i].GetInt32()); }
                        catch
                        {
                            try { y.EmuRevs.Add(item.GetProperty("emu_ver")[0].GetInt32()); }
                            catch { y.EmuRevs.Add(0); }
                        }

                        // ************************************************************************************************************-

                        if (Program.Lang.GetRegion() is not Language.Region.Korea and not Language.Region.Japan)
                        {
                            // Change Korean title to English if language is not CJK
                            if (y.Regions.Count == 4 && (y.Regions[3] is 6 or 7) && item.GetProperty("titles").GetArrayLength() == 4)
                                y.Titles[3] = item.GetProperty("titles")[1].GetString();
                        }

                        if (Program.Lang.GetRegion() is not Language.Region.Japan)
                        {
                            // Change Japanese title to English if language is not Japanese
                            if (((y.Regions.Count == 1 && y.Regions[0] == 0)
                                || (y.Regions.Count > 1 && !y.Regions.Contains(0)))
                                && item.GetProperty("titles").GetArrayLength() > 1)
                                y.Titles[0] = item.GetProperty("titles")[1].GetString();
                        }

                        else
                        {
                            // Change Korean title of Japanese-derived Korean WADs to original
                            if (y.Regions.Contains(6) && y.Regions.Contains(0) && item.GetProperty("titles").GetArrayLength() > 1)
                                y.Titles[y.Regions.IndexOf(6)] = item.GetProperty("titles")[0].GetString();
                        }
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

                    if (!(Program.Lang.GetRegion() is Language.Region.Japan && !y.Regions.Contains(0)) || c == Platform.Flash || c == Platform.C64 || c == Platform.MSX)
                        Entries.Add(y);
                }
            }

            if (Entries.Count == 0) throw new Exception(error);
        }
        #endregion
    }
}
