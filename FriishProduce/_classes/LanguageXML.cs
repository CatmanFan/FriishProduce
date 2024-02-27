using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using FriishProduce.Properties;
using System.IO;
using System.Xml;

namespace FriishProduce
{
    public static class LanguageXML
    {
        public static CultureInfo English { get => new CultureInfo(9); }
        public static CultureInfo Current
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                bool needsChange = EnglishXML != null && value.ToString() != Current.Name;
                CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(value.ToString()) { DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" } };
                if (needsChange) XML = Load(CultureInfo.CurrentCulture.Name);
            }
        }

        private static XmlNode EnglishXML { get; set; }
        private static XmlNode XML { get; set; }


        private static SortedDictionary<string, string> _list;
        public static SortedDictionary<string, string> List
        {
            get
            {
                _list = new SortedDictionary<string, string>() { { "en", "English" } };

                string[] XMLFiles = Directory.GetFiles(Paths.Languages, "*.xml");

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                foreach (CultureInfo culture in cultures)
                {
                    foreach (var item in XMLFiles)
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

        public static void Run()
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

            LoadEnglish();
            XML = null;

            if (Settings.Default.UI_Language == "en" || Current == English || Current.EnglishName == English.EnglishName)
                XML = EnglishXML;
            else
            {
                XML = Load(Current.Name);
                if (XML == EnglishXML)
                {
                    Current = English;
                    Settings.Default.UI_Language = "en";
                    Settings.Default.Save();
                }
            }
        }

        private static void LoadEnglish()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                doc.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("FriishProduce.Strings.en.xml"));

                EnglishXML = null;
                foreach (XmlNode item in doc.ChildNodes)
                    if (item.Name.ToLower() == "language") EnglishXML = item;

                if (EnglishXML == null) throw new Exception("The file was not found or is of invalid type.");
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"A fatal error occurred while trying to read the English language strings.\n" +
                    $"Exception: {ex.GetType()}\nMessage: {ex.Message}\n\nStack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine +
                    "The application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.FailFast(ex.Message);
            }
        }

        private static XmlNode Load(string lang)
        {
            try
            {
                if (lang == "en") throw new Exception("Lol");

                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.PreserveWhitespace = false;
                    doc.Load(Paths.Languages + lang + ".xml");

                    foreach (XmlNode item in doc.ChildNodes)
                        if (item.Name.ToLower() == "language") return item;

                    throw new FileNotFoundException();
                }
            }

            catch
            {
                return EnglishXML;
            }
        }

        public static string Get(string name, string target = null, string type = "global")
        {
            bool hasTarget = target != null;
            bool useEnglish = false;

        Search:
            var targetXML = useEnglish ? EnglishXML : XML;

            // Get global strings, if target is form select its relevant form name and use that
            // ****************
            var searchTarget = targetXML.SelectNodes("global");
            if (hasTarget)
            {
                if (type.ToLower() == "form" && !name.EndsWith(".Text")) name += ".Text";
                foreach (XmlNode section in targetXML.SelectNodes(type))
                    if (section.Attributes[0].InnerText == target) searchTarget = section.SelectNodes(".");
            }

            foreach (XmlNode section in searchTarget)
                foreach (XmlNode item in section.ChildNodes)
                    if (item.Name == name)
                        return item.InnerText;

            if (!useEnglish)
            {
                useEnglish = true;
                goto Search;
            }

            else return "undefined";
        }

        public static string Get(string name, string target) => Get (name, target);
        public static string Get(string name, Form f) => Get(name, f.Name, "form");
        public static string Get(Control c, string target) => Get(c.Name, target, "form");
    }
}
