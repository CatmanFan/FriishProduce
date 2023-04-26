using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SimpleTranslationSystem;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Localization
    {
        private Language Language { get; set; }
        private Language English { get; set; }
        internal char separator = '⎸';
        public Localization()
        {
            string code = Default.Language;
            string csv1 = Paths.Languages + "en.csv";
            string csv2 = Paths.Languages + $"{code}.csv";

            // Failsafe to prevent exceptions in case the English file is modified or the file/languages directory are missing
            if (!Directory.Exists(Paths.Languages))
                Directory.CreateDirectory(Paths.Languages);
            if (File.ReadAllText(Paths.Languages + "en.csv") != Properties.Resources.English)
                File.Delete(Paths.Languages + "en.csv");
            if (!File.Exists(Paths.Languages + "en.csv"))
                File.WriteAllText(Properties.Resources.English, Paths.Languages + "en.csv", System.Text.Encoding.Default);

            // Load English stringlist as backup
            English = TranslationFileReader.GetLanguagesFromCsvFile(csv1, separator, System.Text.Encoding.Default)[0];

            if (string.IsNullOrWhiteSpace(code) || (!IsConvertable(csv2) && code != "sys"))
                goto Reset;

            Main:
            var culture = new CultureInfo(code == "sys" ? "en" : code);
            if (code == "sys")
            {
                foreach (string fn in Directory.GetFiles(Paths.Languages))
                {
                    string fn_code = Path.GetFileNameWithoutExtension(fn);
                    if (fn_code == CultureInfo.InstalledUICulture.TwoLetterISOLanguageName
                     || fn_code == CultureInfo.InstalledUICulture.Name)
                    {
                        culture = new CultureInfo(fn_code);
                        csv2 = Paths.Languages + $"{fn_code}.csv";
                        Language = TranslationFileReader.GetLanguagesFromCsvFile(csv2, separator, System.Text.Encoding.Default)[0];
                        culture.DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" };
                        Thread.CurrentThread.CurrentUICulture = culture;
                        return;
                    }
                }
                culture.DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" };
                Thread.CurrentThread.CurrentUICulture = culture;
                Language = English;
                return;
            }
            else
            {
                csv2 = Paths.Languages + $"{culture.Name}.csv";
                Language = TranslationFileReader.GetLanguagesFromCsvFile(csv2, separator, System.Text.Encoding.Default)[0];
                culture.DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" };
                Thread.CurrentThread.CurrentUICulture = culture;
                return;
            }

            Reset:
            Default.Language = "sys";
            Default.Save();
            code = "sys";
            goto Main;
        }

        public bool IsConvertable(string csv)
        {
            try
            {
                TranslationFileReader.GetLanguagesFromCsvFile(csv, '›', System.Text.Encoding.Unicode);
                var culture = new CultureInfo(Path.GetFileNameWithoutExtension(csv));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the strings of the form and all child controls within it
        /// </summary>
        public void ChangeFormLanguage(Control f)
        {
            foreach (Control c in f.Controls)
            {
                Get(c);
            }
        }

        public string Get(string stringName)
        {
            for (int i = 0; i < Language.translations.Count; i++)
                if (stringName.ToLower() == Language.translations[i].identifier.Replace(" ", "").ToLower())
                    return TranslationSystem.GetText(Language.translations[i].identifier, Language).Replace(@"\n", Environment.NewLine);

            for (int i = 0; i < English.translations.Count; i++)
                if (stringName.ToLower() == English.translations[i].identifier.Replace(" ", "").ToLower())
                    return TranslationSystem.GetText(English.translations[i].identifier, English).Replace(@"\n", Environment.NewLine);

            return "undefined";
        }

        public string Get(string stringName, Language lang)
        {
            for (int i = 0; i < lang.translations.Count; i++)
                if (stringName.ToLower() == lang.translations[i].identifier.Replace(" ", "").ToLower())
                    return TranslationSystem.GetText(lang.translations[i].identifier, Language).Replace(@"\n", Environment.NewLine);

            return "undefined";
        }

        public void Get(Control c)
        {
            string stringName = c.Name;

            try
            {
                if (((ComboBox)c).Items.Contains("null"))
                {
                    ((ComboBox)c).Items.Clear();
                    ((ComboBox)c).Items.Add(Get(c.Name + ".Items"));
                    for (int i = 1; i < 20; i++)
                        try { if (Get(c.Name + $".Items{i}") != "undefined") ((ComboBox)c).Items.Add(Get(c.Name + $".Items{i}")); } catch { }
                }
            }
            catch
            {
                string translated = Get(c.Name + ".Text");
                c.Text = translated == "undefined" ? c.Text : translated;
            }
            if (c.GetType() == typeof(Form)) c.Text = Get(c.Name);

            foreach (Control d in c.Controls) Get(d);
        }
    }
}
