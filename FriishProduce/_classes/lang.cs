using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Lang
    {
        private static Dictionary<string, string> English { get; set; }
        internal static Dictionary<string, string> Current { get; set; }
        public Lang()
        {
            // Set up English

            if (!Directory.Exists(Paths.Languages))
                Directory.CreateDirectory(Paths.Languages);

            if (!File.Exists(Paths.Languages + "en.json"))
                File.WriteAllText(Paths.Languages + "en.json", Properties.Resources.English);

            try
            {
                var jsonReader = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Paths.Languages + "en.json"));

                English = new Dictionary<string, string>();
                foreach (JObject category in jsonReader.Children<JToken>().Children<JToken>())
                    foreach (JProperty key in category.Properties())
                        English.Add(key.Name, key.Value.ToString());
            }
            catch
            {
                throw new Exception("Unable to setup language!");
            }

        }

        internal static Dictionary<string, string> Read(string jsonFile)
        {
            try
            {
                var jsonReader = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(jsonFile));
                var jsonCode = jsonReader["meta"]["code"].ToString();
                var jsonName = jsonReader["meta"]["name"].ToString();

                var target = new Dictionary<string, string>();

                foreach (JObject category in jsonReader.Children<JToken>().Children<JToken>())
                    foreach (JProperty key in category.Properties())
                        target.Add(key.Name, key.Value.ToString());

                return target;
            }
            catch
            {
                return null;
            }
        }

        public void Set(string code)
        {
            string json_en = Paths.Languages + "en.json";
            string json = Paths.Languages + $"{code}.json";

            if (string.IsNullOrWhiteSpace(code) || ((Read(json) == null) && code != "sys"))
            {
                code = "sys";
                Default.Language = "sys";
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
                        json = Paths.Languages + $"{fn_code}.json";
                        culture = new CultureInfo(fn_code);
                        Current = Read(json);
                        goto EndOfFunction;
                    }
                }

                Current = English;
            }
            else
            {
                json = Paths.Languages + $"{culture.Name}.json";
                Current = Read(json);
            }

            EndOfFunction:
            culture.DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" };
            Thread.CurrentThread.CurrentUICulture = culture;
            Get("a000");
            Get("m011");
        }
        public void Localize(Control f)
        {
            foreach (Control c in f.Controls)
            {
                c.Text = Get(c.Name) == "undefined" ? c.Text : Get(c.Name);
                if (c.GetType() == typeof(ComboBox) && ((ComboBox)c).Items.Contains("null"))
                {
                    ((ComboBox)c).Items.Clear();
                    for (int i = 0; i < 20; i++)
                        try { if (Get(c.Name + $"__l{i}") != "undefined") ((ComboBox)c).Items.Add(Get(c.Name + $"__l{i}")); } catch { }
                }

                foreach (Control d in c.Controls) Localize(d);
            }
        }
        public static string Get(string id)
        {
            foreach (KeyValuePair<string, string> translation in Current)
                if (translation.Key == id)
                    return translation.Value;

            foreach (KeyValuePair<string, string> default_string in English)
                if (default_string.Key == id)
                    return default_string.Value;

            return "undefined";
        }
    }
}
