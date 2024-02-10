using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using FriishProduce.Properties;
using System.Collections;
using System.IO;

namespace FriishProduce
{
    public static class Language
    {
        public static CultureInfo English { get => new CultureInfo(9); }
        public static CultureInfo Current
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                bool needsChange = English_Dictionary != null && value.ToString() != Current.Name;
                CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(value.ToString()) { DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" } };
                if (needsChange) Current_Dictionary = GetDictionary(CultureInfo.CurrentCulture.Name);
            }
        }

        private static SortedDictionary<string, string> _list;
        public static SortedDictionary<string, string> List
        {
            get
            {
                _list = new SortedDictionary<string, string>() { { "en", "English" } };

                // Pass the class name of your resources as a parameter e.g. MyResources for MyResources.resx
                string[] installedLangs = Directory.GetDirectories(Paths.Languages);

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                foreach (CultureInfo culture in cultures)
                {
                    foreach (var item in installedLangs)
                    {
                        if (culture.Name == Path.GetFileName(item))
                        {
                            // Create neutral cultures to be displayed instead of local ones
                            // ****************
                            var displayCulture = culture;
                            if (culture.Name == "ja-JP") displayCulture = new CultureInfo("ja");

                            else if (culture.Name.Length == 5 && culture.Name.Substring(0, 2).ToLower() == culture.Name.Substring(3).ToLower()) // e.g. es-ES, fr-FR, pt-PT
                                displayCulture = new CultureInfo(culture.Name.Substring(0, 2).ToLower());

                            // Get native name & capitalize first letter
                            // ****************
                            string langName = displayCulture.NativeName;
                            langName = langName.Substring(0, 1).ToUpper() + langName.Substring(1, langName.Length - 1); ;

                            _list.Add(culture.Name, langName);
                        }
                    }
                }

                _list.Remove(""); // the "Invariant Language (Invariant Country)" item
                _list.Remove("en-001"); // "English (World)"

                return _list;
            }
        }

        public static CultureInfo GetSystemLanguage()
        {
            foreach (var item in List)
                if (item.Key.ToLower() == CultureInfo.InstalledUICulture.Name.ToLower()) return CultureInfo.InstalledUICulture;

            foreach (var item in List)
                if (item.Key.ToLower() == CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToLower()) return new CultureInfo(item.Key);

            return English.Parent;
        }

        private static IDictionary<string, string>[] English_Dictionary { get; set; }
        private static List<string> Sources { get; set; }

        private static IDictionary<string, string>[] GetEnglish()
        {
            int ResourceFileNumber = 0;
            string ResourceFileName = null;
            string KeyName = null;

            try
            {
                Sources = new List<string>();

                var A = Assembly.GetExecutingAssembly();
                var list = A.GetManifestResourceNames();

                foreach (var listEntry in list)
                    if (listEntry.StartsWith("FriishProduce.Strings."))
                        Sources.Add(listEntry);

                if (Sources == null) throw new Exception("No source string files were found.");

                IDictionary<string, string>[] Dict = new Dictionary<string, string>[Sources.Count];

                for (int i = 0; i < Sources.Count; i++)
                {
                    Dict[i] = new Dictionary<string, string>();
                    foreach (DictionaryEntry DictEntry in new ResourceManager(Sources[i].Replace(".resources", ""), Assembly.GetExecutingAssembly()).GetResourceSet(English, true, true))
                    {
                        ResourceFileNumber = i;
                        ResourceFileName = Sources[i].Replace(".resources", "").Replace("FriishProduce.Strings.", "");
                        KeyName = (string)DictEntry.Key;
                        if (!KeyName.EndsWith(".LargeImage") && !KeyName.EndsWith(".SmallImage") && !KeyName.EndsWith(".Image"))
                            Dict[i].Add(KeyName, (string)DictEntry.Value);
                    }
                }

                for (int i = 0; i < Sources.Count; i++)
                    Sources[i] = Sources[i].Replace(".resources", "").Replace("FriishProduce.Strings.", "");

                return Dict;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A fatal error occured while trying to read the English language resource files.\nKey name: {KeyName}\n{ResourceFileName} (index: {ResourceFileNumber}).\n\n" +
                    $"Exception: {ex.GetType()}\nMessage: {ex.Message}\n\nStack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine +
                    "The application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.FailFast(ex.Message);
                return null;
            }
        }

        private static IDictionary<string, string>[] Current_Dictionary { get; set; }

        private static IDictionary<string, string>[] GetDictionary(string code)
        {
            if (code == "en") return English_Dictionary;

            string currentPath = Path.GetDirectoryName(Paths.Languages + $"{code}\\").Replace("file:\\", "");

            IDictionary<string, string>[] res = new Dictionary<string, string>[Directory.GetFiles(currentPath).Length];
            try
            {
                if (!(Directory.GetFiles(currentPath).Contains("Strings.resx") || res.Length == English_Dictionary.Length)) return English_Dictionary;

                for (int i = 0; i < Directory.GetFiles(currentPath).Length; i++)
                {
                    res[i] = new Dictionary<string, string>();
                    using (ResXResourceReader resxReader = new ResXResourceReader(Directory.GetFiles(currentPath)[i]))
                    {
                        foreach (DictionaryEntry entry in resxReader)
                            if (!entry.Key.ToString().EndsWith(".LargeImage") && !entry.Key.ToString().EndsWith(".SmallImage") && !entry.Key.ToString().EndsWith(".Image"))
                                res[i].Add((string)entry.Key, (string)entry.Value);
                    }
                }

                return res;
            }
            catch { return English_Dictionary; }
        }

        public static void Load()
        {
            var code = Settings.Default.UI_Language;

            // --------------------------
            // Failsafe check
            // --------------------------
            bool Exists = false;
            foreach (var item in List)
            {
                if (item.Key == code) Exists = true;
                else if (item.Key.Contains(code)) Exists = true;
            }

            if (string.IsNullOrWhiteSpace(code) || !Exists)
            {
                code = "sys";
                Settings.Default.UI_Language = "sys";
                Settings.Default.Save();
            }
            
            // --------------------------
            // Localization process
            // --------------------------
            Set:
            if (code.ToLower() != "sys")
            {
                bool set = false;

                foreach (var item in List)
                {
                    if (item.Key.ToLower() == code.ToLower()) Current = new CultureInfo(item.Key);
                    set = true;
                }

                if (!set)
                {
                    code = "sys";
                    Settings.Default.UI_Language = "sys";
                    Settings.Default.Save();
                    goto Set; // Loop back
                }
            }

            else Current = GetSystemLanguage();

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = Current;

            English_Dictionary = GetEnglish();
            Current_Dictionary = null;

            if (Settings.Default.UI_Language == "en" || Current == English || Current.EnglishName == English.EnglishName) Current_Dictionary = English_Dictionary;
            else
            {
                Current_Dictionary = GetDictionary(Current.Name);
                if (Current_Dictionary == English_Dictionary)
                {
                    Current = English;
                    Settings.Default.UI_Language = "en";
                    Settings.Default.Save();
                }
            }
        }

        #region Get
        /// <summary>
        /// Gets a string corresponding to the CultureInfo given.
        /// </summary>
        /// <param name="name">Key name within the resource file</param>
        /// <param name="className">The name of the resource file</param>
        /// <param name="ci">CultureInfo name</param>
        /// <returns>English version by default if no corresponding localized string is found, and "undefined" if all methods fail.</returns>
        public static string Get(string name, string source)
        {
            int index = -1;
            for (int i = 0; i < Sources.Count; i++)
                if (Sources[i] == source) index = i;

            foreach (var item in Current_Dictionary[index])
                if (item.Key == name) return item.Value;

            foreach (var item in English_Dictionary[index])
                if (item.Key == name) return item.Value;

            return "undefined";
        }

        public static string Get(string name) => Get(name, "Strings");
        public static string Get(string name, Control x) => Get(name + ".Text", x.Name);
        #endregion Get

        #region GetArray
        /// <summary>
        /// Gets a string array from a Strings.resx, or another resource file, both corresponding to the CultureInfo given.
        /// </summary>
        /// <param name="name">Key name within the resource file</param>
        /// <param name="className">The name of the resource file</param>
        /// <param name="ci">CultureInfo name</param>
        /// <returns>English version by default if no corresponding localized string array is found, and "undefined" if all methods fail.</returns>
        public static string[] GetArray(string name, string source)
        {
            List<string> undefined = new List<string>() { "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined" };

            // Get original string containing list
            // ****************
            string orig = Get(name, source);

            // Split
            // ****************
            var Object = orig != "undefined" && !string.IsNullOrEmpty(orig) ? orig.Split(Environment.NewLine.ToCharArray()).ToList() : undefined;
            for (int i = 0; i < Object.Count; i++)
                if (string.IsNullOrEmpty(Object[i])) Object.RemoveAt(i);

            return Object.ToArray();
        }

        public static string[] GetArray(string name) => GetArray(name, "Strings");
        #endregion

        public static void AutoSetForm(Control c)
        {
            if (c.Name.ToLower().StartsWith("options_vc")) c.Text = Get("InjectionMethodOptions");

            foreach (Control d in c.Controls)
            {
                GetControl(d, c);
                foreach (Control e in d.Controls)
                {
                    GetControl(e, c);
                    foreach (Control f in e.Controls)
                    {
                        GetControl(f, c);
                        foreach (Control g in f.Controls)
                        {
                            GetControl(g, c);
                            foreach (Control h in g.Controls)
                            {
                                GetControl(h, c);
                                foreach (Control i in h.Controls)
                                    GetControl(i, c);
                            }
                        }
                    }
                }
            }
        }

        private static void GetControl(Control x, Control parent, bool customStrings = true)
        {
            if      (x.GetType() == typeof(Form) && x.Name != parent.Name)                                                            return;
            else if (x.GetType() == typeof(MdiTabControl.TabPage) && (x as MdiTabControl.TabPage).Form.GetType().Name != parent.Name) return;

            if (!string.IsNullOrWhiteSpace(x.Name) && Get(x.Name, parent) != "undefined")
                x.Text = Get(x.Name, parent);

            // Custom strings (e.g. for buttons)
            // ****************
            if (customStrings)
            {
                if (x.GetType() == typeof(Button))
                {
                    if (x.Name.ToLower() == "ok") x.Text = Get("Button_OK");
                    if (x.Name.ToLower() == "cancel") x.Text = Get("Button_Cancel");
                }
            }
        }
    }
}
