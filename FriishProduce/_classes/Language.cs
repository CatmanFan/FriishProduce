using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriishProduce.Properties;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

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
                ResourceManager rm = new ResourceManager(typeof(Strings));

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
                return _list;
            }
        }
        
        public static void Load()
        {
            var code = Settings.Default.UI_Language;
            if (string.IsNullOrWhiteSpace(code))
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

        /// <summary>
        /// Gets a string from Strings.resx corresponding to the CultureInfo given.
        /// </summary>
        /// <param name="name">Key name within Strings.resx</param>
        /// <param name="ci">CultureInfo name</param>
        /// <returns>English version by default if no corresponding localized string is found, and "undefined" if all methods fail.</returns>
        public static string Get(string name, CultureInfo ci)
        {
            const string undefined = "undefined";
            var Object = undefined;

            try { Object = Strings.ResourceManager.GetObject(name, ci).ToString(); } catch { }
            if (Object == undefined) try { Object = Strings.ResourceManager.GetObject(name, English).ToString(); } catch { }

            Object = Object.Replace("\r\n", "[[NL]]").Replace("\n", "[[NL]]").Replace("[[NL]]", Environment.NewLine);

            return Object;
        }

        /// <summary>
        /// Gets a localized string from Strings.resx.
        /// </summary>
        /// <param name="name">Key name within Strings.resx</param>
        /// <returns>English version by default if no corresponding localized string is found, and "undefined" if all methods fail.</returns>
        public static string Get(string name) => Get(name, Current);

        /// <summary>
        /// Gets a string array from Strings.resx corresponding to the CultureInfo given.
        /// </summary>
        /// <param name="name">Key name within Strings.resx</param>
        /// <param name="ci">CultureInfo name</param>
        /// <returns>English version by default if no corresponding localized string array is found, and "undefined" if all methods fail.</returns>
        public static string[] GetArray(string name, CultureInfo ci)
        {
            List<string> undefined = new List<string>() { "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined", "undefined" };
            var Object = undefined;

            try { Object = Strings.ResourceManager.GetObject(name, ci).ToString().Split(Environment.NewLine.ToCharArray()).ToList(); } catch { }
            if (Object == undefined) try { Object = Strings.ResourceManager.GetObject(name, English).ToString().Split(Environment.NewLine.ToCharArray()).ToList(); } catch { }

            for (int i = 0; i < Object.Count; i++)
                if (string.IsNullOrEmpty(Object[i])) Object.RemoveAt(i);

            return Object.ToArray();
        }

        /// <summary>
        /// Gets a localized string array from Strings.resx.
        /// </summary>
        /// <param name="name">Key name within Strings.resx</param>
        /// <returns>English version by default if no corresponding localized string array is found, and "undefined" if all methods fail.</returns>
        public static string[] GetArray(string name) => GetArray(name, Current);

        public static void AutoSetForm(Control c)
        {
            if (c.GetType() == typeof(Form) && c.Name.ToLower().StartsWith("options_vc")) c.Text = Get("InjectionMethodOptions");

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

        private static void Get(Control x)
        {
            if (x.GetType() == typeof(Button))
            {
                if (x.Name.ToLower() == "ok") x.Text = Get("Button_OK");
                if (x.Name.ToLower() == "cancel") x.Text = Get("Button_Cancel");
            }
        }
    }
}
