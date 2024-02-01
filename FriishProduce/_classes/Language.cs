using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static FriishProduce.Properties.Settings;
using System.Threading;

namespace FriishProduce
{
    public class Language
    {
        internal static Dictionary<string, string> English { get; set; }
        internal static Dictionary<string, string> Current { get; set; }

        private static string CurrentCode { get; set; }

        internal static Dictionary<string, string> Read(string jsonFile)
        {
            if (Path.GetExtension(jsonFile).ToLower() != ".json") return null;

            try
            {
                JObject jsonReader = new JObject();
                var jsonCode = "";

                try { jsonReader = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(jsonFile)); }
                catch { throw new Exception($"File \"{Path.GetFileName(jsonFile)}\" was not found."); }

                int index = 0;
                try { jsonCode = jsonReader["key"].ToString(); }
                catch { throw new Exception("Failed to get \"key\" value." + Environment.NewLine + $"Please check that the value \"key\" is included in langs\\{Path.GetFileName(jsonFile)}."); }

                try
                {
                    var jsonName = new CultureInfo(Path.GetFileNameWithoutExtension(jsonFile)).DisplayName;
                    var target = new Dictionary<string, string>();

                    foreach (JProperty element in jsonReader.Children<JToken>())
                        target.Add(element.Name, element.Value.ToString());

                    // Multi-array function
                    /* var target = new Dictionary<string, string>() { { "key", jsonReader["key"].ToString() }, { "author", jsonReader["author"].ToString() } };
                    
                    for (int i = 2; i < jsonReader.Children<JToken>().Count(); i++)
                        foreach (JObject category in jsonReader.Children<JToken>().ElementAt(i).Children<JToken>())
                            foreach (JProperty element in category.Properties())
                            {
                                index++;
                                target.Add(element.Name, element.Value.ToString());
                            } */

                    return target;
                }
                catch (Exception ex)
                {
                    string msg = "Failed to collect language data at index " + index.ToString() + "." + Environment.NewLine + Environment.NewLine + "Exception name: " + ex.GetType().Name + Environment.NewLine + "Exception message: " + ex.Message;
                    if (ex.GetType().Name != "InvalidCastException" && ex.GetType().Name != "ArgumentException") msg += Environment.NewLine + Environment.NewLine + $"Please check the value \"key\" in langs\\{Path.GetFileName(jsonFile)} and make sure it matches the filename.";
                    throw new Exception(msg);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "The application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.FailFast(ex.Message);
                return null;
            }
        }
        public Language()
        {
            CurrentCode = Default.UI_Language;
            string code = Default.UI_Language;
            string json_en = Paths.Languages + "en.json";
            string json = Paths.Languages + $"{code}.json";

            // --------------------------
            // Set up English
            // --------------------------
            if (!Directory.Exists(Paths.Languages))
                Directory.CreateDirectory(Paths.Languages);

            if (!File.Exists(json_en) || File.ReadAllBytes(json_en) != Properties.Resources.English)
                File.WriteAllBytes(json_en, Properties.Resources.English);

            try
            {
                English = Read(json_en);
            }
            catch
            {
                throw new Exception("Unable to setup language!");
            }

            // --------------------------
            // Localization process
            // --------------------------
            if (code != "sys" && (string.IsNullOrWhiteSpace(code) || ((Read(json) == null))))
            {
                code = "sys";
                Default.UI_Language = "sys";
                Default.Save();
            }

            var culture = new CultureInfo(code == "sys" ? "en" : code);
            if (code == "sys")
            {
                foreach (string fn in Directory.GetFiles(Paths.Languages))
                {
                    string fn_code = Path.GetFileNameWithoutExtension(fn);

                    if (fn_code == CultureInfo.InstalledUICulture.TwoLetterISOLanguageName || fn_code == CultureInfo.InstalledUICulture.Name)
                    {
                        CurrentCode = fn_code;
                        json = Paths.Languages + $"{fn_code}.json";
                        culture = new CultureInfo(fn_code);
                        Current = Read(json);
                        goto EndOfFunction;
                    }
                }

                Current = English;
                goto EndOfFunction;
            }
            else
            {
                CurrentCode = culture.Name;
                json = Paths.Languages + $"{culture.Name}.json";
                Current = Read(json);

                if (Current == null)
                {
                    CurrentCode = "en";
                    Current = English;
                }

                goto EndOfFunction;
            }

            EndOfFunction:
                culture.DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" };
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
        }

        public void Localize(Control main)
        {
            Get(main);
            foreach (Control c in main.Controls)
            {
                Get(c);
                foreach (Control d in c.Controls)
                {
                    Get(d);
                    foreach (Control e in d.Controls)
                    {
                        Get(e);
                        foreach (Control f in e.Controls)
                        {
                            Get(f);
                            foreach (Control g in f.Controls)
                                Get(g);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// First check in case-sensitive format, then use ToLower() if no result has been returned.
        /// Returns English by default if no corresponding localized string is found.
        /// </summary>
        public string Get(string id)
        {
            try
            {
                foreach (KeyValuePair<string, string> translation in Current)
                    if (translation.Key == id)
                        return translation.Value.Replace(@"\n", Environment.NewLine).Replace('\"', '"');
                    else if (translation.Key.ToLower() == id.ToLower())
                        return translation.Value.Replace(@"\n", Environment.NewLine).Replace('\"', '"');
            }
            catch (Exception)
            {
                goto End;
            }

            End:
                foreach (KeyValuePair<string, string> default_string in English)
                    if (default_string.Key == id)
                        return default_string.Value.Replace(@"\n", Environment.NewLine).Replace('\"', '"');
                    else if (default_string.Key.ToLower() == id.ToLower())
                        return default_string.Value.Replace(@"\n", Environment.NewLine).Replace('\"', '"');

                return "undefined";
        }

        public void Get(Control c)
        {
            c.Text = Get(c.Name.ToString()) != "undefined" ? Get(c.Name.ToString()) : c.Text;
            if (c.Tag != null) c.Text = Get(c.Tag.ToString()) != "undefined" ? Get(c.Tag.ToString()) : c.Text;

            if (c.GetType() == typeof(ComboBox) && ((ComboBox)c).Items.Contains("null"))
            {
                ((ComboBox)c).Items.Remove("null");

                int x = Get(c.Tag + $"_{0}") != "undefined" ? 0 : 1;
                if (x == 1) try { ((ComboBox)c).Items.Add(Get("g006")); } catch { } // "Default" item

                for (int i = x; i < 20; i++)
                    if (Get(c.Tag + $"_{i}") != "undefined")
                        try { ((ComboBox)c).Items.Add(Get(c.Tag + $"_{i}")); } catch { }
                    else if (Get(c.Name + $"_{i}") != "undefined")
                        try { ((ComboBox)c).Items.Add(Get(c.Name + $"_{i}")); } catch { }
            }

            if (Current.First().Value.StartsWith("ar") || Current.First().Value.StartsWith("fa") || Current.First().Value.StartsWith("he"))
                c.RightToLeft = RightToLeft.Yes;
        }

        public string[] LangInfo(string code)
        {
            var jsonReader = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Paths.Languages + $"{code}.json"));
            return new string[] { jsonReader["key"].ToString(), jsonReader["author"].ToString() };
        }

        public string[] LangInfo() => LangInfo(CurrentCode);
    }
}
