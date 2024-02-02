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

namespace FriishProduce
{
    public static class Language
    {
        public static CultureInfo English { get => new CultureInfo(9); }
        public static CultureInfo Current
        {
            get => CultureInfo.CurrentCulture;
            set => CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(value.ToString()) { DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" } };
        }

        private static Dictionary<string, string> _list;
        public static Dictionary<string, string> List
        {
            get
            {
                _list = new Dictionary<string, string>() { { "en", "English" } };

                // Pass the class name of your resources as a parameter e.g. MyResources for MyResources.resx
                ResourceManager rm = new ResourceManager("FriishProduce.Strings.Strings", Assembly.GetExecutingAssembly());

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                foreach (CultureInfo culture in cultures)
                {
                    try
                    {
                        ResourceSet rs = rm.GetResourceSet(culture, true, false);
                        if (rs != null)
                        {
                            string langName = culture.NativeName;

                            // Capitalize first letter
                            langName = langName.Substring(0, 1).ToUpper() + langName.Substring(1, langName.Length - 1); ;
                            _list.Add(culture.Name, langName);
                        }
                    }
                    catch /* (CultureNotFoundException ex) */ { }
                }

                _list.Remove(""); // the "Invariant Language (Invariant Country)" item
                _list.Remove("en-001"); // "English (World)"
                return _list;
            }
        }

        private static ResourceManager RManager { get; set; }
        
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

            else
            {
                bool set = false;

                foreach (var item in List)
                {
                    if (item.Key.ToLower() == CultureInfo.InstalledUICulture.Name.ToLower()) Current = CultureInfo.InstalledUICulture;
                    set = true;
                }

                if (!set) foreach (var item in List)
                {
                    if (item.Key.ToLower() == CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToLower()) Current = new CultureInfo(item.Key);
                    set = true;
                }

                if (!set) Current = English.Parent;
            }

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = Current;
        }

        private static ResourceManager LoadResources(string pathName)
        {
            try
            {
                string found = null;

                var A = Assembly.GetExecutingAssembly();
                var list = A.GetManifestResourceNames();
                foreach (var listEntry in list)
                {
                    if (listEntry.StartsWith("FriishProduce.Strings." + pathName))
                    {
                        found = listEntry;
                        goto End;
                    }
                }

                if (found == null) throw new Exception("The file cannot be found.");

                End:
                found = found.Replace(".resources", "");
                return new ResourceManager(found, Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A fatal error occured while trying to read the language resource file '{pathName}.resx'." + Environment.NewLine + ex.GetType().ToString() + ": " + ex.Message + Environment.NewLine + Environment.NewLine
                    + "Stack trace:" + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine + "The application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.FailFast(ex.Message);
                return null;
            }
        }

        #region Get
        /// <summary>
        /// Gets a string from Strings.resx corresponding to the CultureInfo given.
        /// </summary>
        /// <param name="name">Key name within the resource file</param>
        /// <param name="className">The name of the resource file</param>
        /// <param name="ci">CultureInfo name</param>
        /// <returns>English version by default if no corresponding localized string is found, and "undefined" if all methods fail.</returns>
        public static string Get(string name, string source, CultureInfo ci)
        {
            RManager = LoadResources(source);
            if (source.ToLower() == "strings")
            {
                string value = RManager.GetString(name, ci)
                            ?? RManager.GetString(name, English)
                            ?? "undefined";
                return value;
            }
            else
            {
                foreach (DictionaryEntry item in RManager.GetResourceSet(ci, true, true))
                    if (item.Key.ToString().ToLower() == name.ToLower()) return item.Value.ToString();

                foreach (DictionaryEntry item in RManager.GetResourceSet(English, true, true))
                    if (item.Key.ToString().ToLower() == name.ToLower()) return item.Value.ToString();

                return "undefined";
            }
        }

        public static string Get(string name, string source) => Get(name, source, Current != null ? Current : English);
        public static string Get(string name) => Get(name, "Strings", Current);
        public static string Get(string name, Control source) => Get(name + ".Text", source.Name);
        #endregion Get

        #region GetArray
        /// <summary>
        /// Gets a string array from a Strings.resx, or another resource file, both corresponding to the CultureInfo given.
        /// </summary>
        /// <param name="name">Key name within the resource file</param>
        /// <param name="className">The name of the resource file</param>
        /// <param name="ci">CultureInfo name</param>
        /// <returns>English version by default if no corresponding localized string array is found, and "undefined" if all methods fail.</returns>
        public static string[] GetArray(string name, string source, CultureInfo ci)
        {
            List<string> undefined = new List<string>() { "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined" };

            // Get original string containing list
            // ****************
            string orig = Get(name, source, ci);

            // Split
            // ****************
            var Object = orig != "undefined" && !string.IsNullOrEmpty(orig) ? orig.Split(Environment.NewLine.ToCharArray()).ToList() : undefined;
            for (int i = 0; i < Object.Count; i++)
                if (string.IsNullOrEmpty(Object[i])) Object.RemoveAt(i);

            return Object.ToArray();
        }

        public static string[] GetArray(string name) => GetArray(name, "Strings", Current);
        #endregion

        public static void AutoSetForm(Control c)
        {
            if (c.Name.ToLower().StartsWith("options_vc")) c.Text = Get("InjectionMethodOptions");

            foreach (Control d in c.Controls)
            {
                GetControl(d, c.Name);
                foreach (Control e in d.Controls)
                {
                    GetControl(e, c.Name);
                    foreach (Control f in e.Controls)
                    {
                        GetControl(f, c.Name);
                        foreach (Control g in f.Controls)
                        {
                            GetControl(g, c.Name);
                            foreach (Control h in g.Controls)
                            {
                                GetControl(h, c.Name);
                                foreach (Control i in h.Controls)
                                    GetControl(i, c.Name);
                            }
                        }
                    }
                }
            }
        }

        private static void GetControl(Control x, string parent, bool customStrings = true)
        {
            if (!string.IsNullOrWhiteSpace(x.Name) && !string.IsNullOrWhiteSpace(x.Text))
            {
                x.Text = Get(x.Name + ".Text", parent);
            }

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
