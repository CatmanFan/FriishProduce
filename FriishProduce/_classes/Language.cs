using FriishProduce.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

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
                string[] XMLList = Directory.EnumerateFiles(Paths.Languages, "*.xml").ToArray();

                if (_list?.Count == XMLList.Length + 1) return _list;

                _list = new SortedDictionary<string, string>() { { "en", "English" } };

                foreach (var item in XMLList)
                {
                    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => c.Name.StartsWith(Path.GetFileNameWithoutExtension(item)));

                    foreach (CultureInfo culture in cultures)
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

            EnglishXML = Load();
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

        private static XmlNode Load(string lang = "en")
        {
            bool isEnglish = lang == "en";

            try
            {
                XmlNode target = null;

                if (lang == "en" && EnglishXML != null) throw new Exception("Lol");

                XmlDocument doc = new XmlDocument();
                XmlReaderSettings opts = new XmlReaderSettings() { IgnoreComments = true, IgnoreWhitespace = true };
                XmlReader reader = isEnglish ? XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("FriishProduce.Strings.en.xml"), opts) : XmlReader.Create(Paths.Languages + lang + ".xml", opts);

                while (reader.Read())
                    if (reader.Name.ToLower().Equals("language") && (reader.NodeType == XmlNodeType.Element))
                        target = doc.ReadNode(reader);

                if (target == null) throw new Exception("The file was not found or is of invalid type.");

                return target;
            }

            catch (Exception ex)
            {
                if (EnglishXML != null) return EnglishXML;

                else
                {
                    System.Windows.Forms.MessageBox.Show($"A fatal error occurred while trying to read the English language strings.\n" +
                        $"Exception: {ex.GetType()}\nMessage: {ex.Message}\n\n" +
                        "The application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Environment.FailFast(ex.Message);
                    return null;
                }
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
                bool isFound = false;

                if (isForm && !name.EndsWith("Text") && !name.Contains(".Items")) name += ".Text";

                foreach (XmlNode section in targetXML.SelectNodes(type))
                    if (section.Attributes[0].InnerText == sectionName)
                    {
                        isFound = true;
                        searchTarget = section.SelectNodes(".");
                    }

                if (isForm && !isFound) goto Failed;
            }

            foreach (XmlNode section in searchTarget)
                foreach (XmlNode item in section.ChildNodes)
                    if (item.Name == name && !string.IsNullOrEmpty(item.InnerText))
                    {
                        string returned = Regex.Replace(item.InnerText, "[\r\t]", "").TrimEnd('\n').Replace(@"\n", Environment.NewLine).Replace(@"\\", "\\");
                        return returned.StartsWith("\n") ? returned.Substring(1) : returned;
                    }

                Failed:
            if (!useEnglish)
            {
                useEnglish = true;
                goto Search;
            }

            else return "undefined";
        }

        public static string AppTitle()
        {
            if (XML.Attributes?[1].Name.ToLower() == "apptitle")
                return XML.Attributes?[1].InnerText;

            return Application.ProductName;
        }

        public static string Author()
        {
            if (XML.Attributes?[0].Name.ToLower() == "author")
                return XML.Attributes?[0].InnerText;

            return "?";
        }

        public static string Get(string name, Form f) => Get(name, f.Name, true);
        public static string Get(Control c, Form f) => Get(c, f.Name);
        public static string Get(Control c, string section)
        {
            if (c == null) return Get("undefined", "undefined");
            else if (c.GetType() == typeof(JCS.ToggleSwitch)) return GetToggleSwitch(c as JCS.ToggleSwitch, null, section);
            else return Get(c.Name, section, true);
        }

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

        public static string GetToggleSwitch(JCS.ToggleSwitch x, string name = null, string section = null)
        {
            if (name == null) name = x.Name;
            return Get(name + (x.Checked ? ".On" : ".Off") + "Text", section, true);
        }

        public static void GetComboBox(ComboBox x, string name = null, string section = null)
        {
            if (name == null) name = x.Name;

            Begin:
            try
            {
                if (x.Items[0].ToString() == "static")
                {
                    x.Items.RemoveAt(0);
                    return;
                }

                else if (x.Items[0].ToString() == "auto")
                {
                    x.Items.Clear();

                    for (int y = 0; y < 100; y++)
                        if (Get(name + $".Items" + (y == 0 ? null : y.ToString()), section, true) != "undefined")
                            x.Items.Add(Get(name + $".Items" + (y == 0 ? null : y.ToString()), section, true));
                }

                else if (x.Items[0].ToString() == "def")
                {
                    x.Items.Clear();
                    x.Items.Add(Get("ByDefault"));

                    for (int y = 1; y < 100; y++)
                        if (Get(name + $".Items{y}", section, true) != "undefined")
                            x.Items.Add(Get(name + $".Items{y}", section, true));
                }

                else throw new InvalidDataException();
            }

            catch
            {
                if (Get(name + $".Items", section, true) != "undefined" || Get(name + $".Items1", section, true) != "undefined")
                {
                    x.Items.Clear();
                    x.Items.Add(Get(name + $".Items", section, true) != "undefined" ? "auto" : Get(name + $".Items1", section, true) != "undefined" ? "def" : "static");
                    goto Begin;
                }
            }
        }

        public static void Localize(Form c)
        {
            if (c.Name.ToLower().StartsWith("options_")) c.Text = Get("InjectionMethodOptions");

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

        private static void GetControl(Control x, Form parent)
        {
            var origText = x.Text;

            if (x.GetType() == typeof(Form) && x.Name != parent.Name) return;
            else if (x.GetType() == typeof(MdiTabControl.TabPage) && (x as MdiTabControl.TabPage).Form.GetType().Name != parent.Name) return;
            else if (x.GetType() == typeof(ComboBox) && (x as ComboBox).Items.Count >= 1) GetComboBox(x as ComboBox, x.Name, parent.Name);
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

            if (!string.IsNullOrWhiteSpace(x.Name))
            {
                if (Get(x.Name, parent) != "undefined") x.Text = Get(x.Name, parent);
                else if (Get(x.Name + ".OnText", parent) != "undefined") x.Text = Get(x.Name + ".OnText", parent);
                else if (Get(x.Name + ".OffText", parent) != "undefined") x.Text = Get(x.Name + ".OffText", parent);
            }

            // Custom strings (e.g. for buttons)
            // ****************
            if (x.GetType() == typeof(Button))
            {
                if (x.Name.ToLower() == "ok") { x.Text = Get("B.OK"); (x as Button).UseMnemonic = true; }
                if (x.Name.ToLower() == "cancel") { x.Text = Get("B.Cancel"); (x as Button).UseMnemonic = true; }
                if (x.Name.ToLower() == "close") { x.Text = Get("B.Close"); (x as Button).UseMnemonic = true; }
            }

            if (origText.ToLower() == "system") x.Text = Get("System");
            if (origText.ToLower() == "display") x.Text = Get("Display");
        }
    }
}
