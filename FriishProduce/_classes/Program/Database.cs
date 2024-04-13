using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using libWiiSharp;

namespace FriishProduce
{
    public class Database
    {

        public class DatabaseEntry
        {
            public string ID { get; set; }
            public List<int> Regions = new List<int>();
            public List<string> Titles = new List<string>();
            public List<int> EmuRevs = new List<int>();

            public string GetID(int index)
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

                return ID.Replace("__", r).ToLower();
            }

            public string GetUpperID(int index)
            {
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
                throw new NotImplementedException();
                var placeholder = WAD.Load(Properties.Resources.StaticBase);
                placeholder.ChangeTitleID(LowerTitleID.Channel, GetUpperID(index));
            }
        }

        public List<DatabaseEntry> Entries { get; private set; }

        /// <summary>
        /// Loads a database of WADs for a selected console/platform.
        /// </summary>
        public Database(Console c)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Properties.Resources.Database))
                using (StreamReader sr = new StreamReader(ms, Encoding.Unicode))
                using (var doc = JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }))
                {
                    var x = doc.Deserialize<JsonElement>(new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip }).GetProperty(c.ToString().ToLower());
                    if (x.ValueKind != JsonValueKind.Array) throw new Exception("The database format or styling is not valid.");

                    sr.Dispose();
                    ms.Dispose();

                    Entries = new List<DatabaseEntry>();
                    foreach (var item in x.EnumerateArray())
                    {
                        var y = new DatabaseEntry() { ID = item.GetProperty("id").GetString() };
                        var reg = item.GetProperty("region");

                        for (int i = 0; i < reg.GetArrayLength(); i++)
                        {
                            y.Regions.Add(reg[i].GetInt32());
                            try { y.Titles.Add(item.GetProperty("titles")[i].GetString()); } catch { y.Titles.Add(item.GetProperty("titles")[Math.Max(0, item.GetProperty("titles").GetArrayLength() - 1)].GetString()); }
                            try { y.EmuRevs.Add(item.GetProperty("emu_ver")[i].GetInt32()); } catch { y.EmuRevs.Add(0); }
                        }

                        Entries.Add(y);
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"A fatal error occurred retrieving the {c} WADs database.\n\nException: {ex.GetType().FullName}\nMessage: {ex.Message}\n\nThe application will now shut down.", "Halt", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                Environment.FailFast("Database initialization failed.");
            }
        }
    }
}
