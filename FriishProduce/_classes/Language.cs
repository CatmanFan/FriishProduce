using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using FriishProduce.Properties;
using System.IO;
using System.Xml;
using System.Linq;

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
                bool needsChange = EnglishXML != null && value.ToString() != Current.Name;
                CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(value.ToString()) { DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" } };
                if (needsChange) XML = Load(CultureInfo.CurrentCulture.Name);
            }
        }

        public static XmlNode EnglishXML { get; private set; }
        public static XmlNode XML { get; private set; }


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
                        if (culture.Name == Path.GetFileNameWithoutExtension(item))
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
                    $"Exception: {ex.GetType()}\nMessage: {ex.Message}\n\n" +
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

        /// <summary>
        /// Gets a string from the loaded XML file.
        /// </summary>
        /// <returns>English version by default if no corresponding localized string array is found, and "undefined" if all methods fail.</returns>
        public static string Get(string name, string sectionName = null, bool isForm = false)
        {
            bool isSectionSet = sectionName != null;
            bool useEnglish = false;

        Search:
            var targetXML = useEnglish ? EnglishXML : XML;

            // Get global strings, if section belongs to a Form type search only in form-type node lists
            // ****************
            string type = isForm ? "form" : "global";
            var searchTarget = targetXML.SelectNodes(type);
            if (isSectionSet)
            {
                if (isForm && !name.EndsWith(".Text")) name += ".Text";
                foreach (XmlNode section in targetXML.SelectNodes(type))
                    if (section.Attributes[0].InnerText == sectionName) searchTarget = section.SelectNodes(".");
            }

            foreach (XmlNode section in searchTarget)
                foreach (XmlNode item in section.ChildNodes)
                    if (item.Name == name)
                    {
                        string returned = item.InnerText.Replace("\t", "");

                        if (item.InnerText.StartsWith("\r\n")) return returned.Substring(2).Trim();
                        else if (item.InnerText.StartsWith("\n")) return returned.Substring(1).Trim();
                        else return returned.Trim();
                    }

            if (!useEnglish)
            {
                useEnglish = true;
                goto Search;
            }

            else return "undefined";
        }

        public static string Get(string name, Form f) => Get(name, f.Name, true);
        public static string Get(Control c, Form f) => Get(c.Name, f.Name, true);
        public static string Get(Control c, string section) => Get(c.Name, section, true);

        /// <summary>
        /// Same as above, except it converts the resulting string to an array.
        /// </summary>
        public static string[] GetArray(string name, string section = null)
        {
            List<string> undefined = new List<string>() { "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined" };

            // Get original string containing list
            // ****************
            string orig = Get(name, section);

            // Split
            // ****************
            var Object = orig == "undefined" || string.IsNullOrEmpty(orig) ? undefined : orig.Split(Environment.NewLine.ToCharArray()).ToList();
            for (int i = 0; i < Object.Count; i++)
                if (string.IsNullOrEmpty(Object[i])) Object.RemoveAt(i);

            return Object.ToArray();
        }

        public static void Localize(Form c)
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
                                {
                                    GetControl(i, c);
                                    foreach (Control j in i.Controls)
                                        GetControl(j, c);
                                }
                            }
                        }
                    }
                }
            }

        }

        private static void GetControl(Control x, Form parent, bool customStrings = true)
        {
            if (x.GetType() == typeof(Form) && x.Name != parent.Name) return;
            else if (x.GetType() == typeof(MdiTabControl.TabPage) && (x as MdiTabControl.TabPage).Form.GetType().Name != parent.Name) return;
            else if (x.GetType() == typeof(TreeView))
            {
                foreach (TreeNode d in (x as TreeView).Nodes)
                {
                    if (!string.IsNullOrWhiteSpace(d.Name) && Get(d.Name, parent) != "undefined")
                        d.Text = Get(d.Name, parent);

                    foreach (TreeNode e in d.Nodes)
                    {
                        if (!string.IsNullOrWhiteSpace(e.Name) && Get(e.Name, parent) != "undefined")
                            e.Text = Get(e.Name, parent);

                        foreach (TreeNode f in e.Nodes)
                        {
                            if (!string.IsNullOrWhiteSpace(f.Name) && Get(f.Name, parent) != "undefined")
                                f.Text = Get(f.Name, parent);
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(x.Name) && Get(x.Name, parent) != "undefined")
                x.Text = Get(x.Name, parent);

            // Custom strings (e.g. for buttons)
            // ****************
            if (customStrings)
            {
                if (x.GetType() == typeof(Button))
                {
                    if (x.Name.ToLower() == "ok") { x.Text = @"&" + Get("B.OK"); (x as Button).UseMnemonic = true; }
                    if (x.Name.ToLower() == "cancel") { x.Text = @"&" + Get("B.Cancel"); (x as Button).UseMnemonic = true; }
                }
            }
        }
    }
}
