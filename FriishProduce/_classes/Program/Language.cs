using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace FriishProduce
{
    public class Language
    {
        private readonly string extension = ".json";
        private readonly byte[] englishFile = Properties.Resources.English;

        public class LanguageData
        {
            public string language { get; set; }
            public string author { get; set; }
            public string application_title { get; set; }
            public Dictionary<string, Dictionary<string, string>> global { get; set; }
            public Dictionary<string, Dictionary<string, string>> strings { get; set; }
        }

        #region List of currently existing languages
        private SortedDictionary<string, string> _list;
        public SortedDictionary<string, string> List
        {
            get
            {
                string[] fileList = Directory.EnumerateFiles(Paths.Languages, "*.*").Where(x => x.ToLower().EndsWith(extension)).ToArray();

                if (_list?.Count == fileList.Length + 1) return _list;

                _list = new SortedDictionary<string, string>() { { "en", "English" } };

                foreach (var item in fileList)
                {
                    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => c.Name.StartsWith(Path.GetFileNameWithoutExtension(item)));

                    foreach (CultureInfo culture in cultures)
                    {
                        if (culture.Name == Path.GetFileNameWithoutExtension(item))
                        {
                            // Get native name & capitalize first letter
                            // ****************
                            string langName = culture.NativeName;
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
        #endregion

        private LanguageData _english { get; set; }
        private LanguageData _current { get; set; }
        private LanguageData _current_parent { get; set; }
        public string Current { get => _current.language; }
        public string Author { get => _current.author; }
        public string ApplicationTitle { get => _current.application_title ?? _english.application_title; }

        public Language(string code = null)
        {
            if (string.IsNullOrWhiteSpace(code)) code = Program.Config?.application.language ?? "sys";

            bool valid = false;
            foreach (var item in List)
            {
                if (item.Key == code) valid = true;
                else if (item.Key.Contains(code)) valid = true;
            }

            if (string.IsNullOrWhiteSpace(code) || (!valid && code != "sys"))
            {
                code = "sys";
                Program.Config.application.language = "sys";
                Program.Config.Save();
            }

            if (_english == null || _english?.language != "en")
            {
                try
                {
                    _english = parseFile(englishFile);
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"A fatal error occurred initializing the program, as the English string resource file was not found or is invalid.\n\n{ex.GetType().FullName}\n{ex.Message}\n\nThe application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Environment.FailFast("Language initialization failed.");
                }
            }

            Set:
            if (code.ToLower() != "sys")
            {
                bool set = false;

                foreach (var item in List.Keys)
                {
                    if (item.ToLower() == code.ToLower())
                    {
                        _current = parseFile(code);
                        set = _current != null;
                    }
                }

                if (!set)
                {
                    code = "sys";
                    Program.Config.application.language = "sys";
                    Program.Config.Save();

                    goto Set; // Loop back
                }
            }

            else _current = parseFile(GetSystemLanguage());

            if (string.IsNullOrEmpty(_current.language)) _current = _english;
            else if (_current.language != code && code != "sys") _current.language = code;

            if (_current == null)
            {
                _current = _english;
                Program.Config.application.language = "en";
                Program.Config.Save();
            }

            if (Program.Config.application.language == "en") _current = _english;

            string parent = returnParent(_current.language, false);
            if (_current.language.Contains('-') && _current.language != parent)
            {
                if (File.Exists(returnParent(_current.language, true)))
                {
                    if (_current.language != parent)
                        _current_parent = parseFile(parent);
                    else
                        _current_parent = _current;

                    _current_parent.language = new CultureInfo(parent).TwoLetterISOLanguageName;
                }
            }

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(code == "sys" ? GetSystemLanguage() : code);
        }

        #region Public methods and variables
        /// <summary>
        /// Returns the current OS language.
        /// </summary>
        public string GetSystemLanguage()
        {
            foreach (var item in List.Keys)
                if (item.ToLower() == CultureInfo.InstalledUICulture.Name.ToLower()) return CultureInfo.InstalledUICulture.Name;

            foreach (var item in List.Keys)
                if (item.ToLower() == CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToLower()) return item;

            return "en";
        }

        public enum ScriptType
        {
            Normal,
            CJK,
            RTL
        }

        public ScriptType GetScript(string text)
        {
            if (text == null) return ScriptType.Normal;

            else return new System.Text.RegularExpressions.Regex(
                @"\p{IsHangulJamo}|" +
                @"\p{IsCJKRadicalsSupplement}|" +
                @"\p{IsCJKSymbolsandPunctuation}|" +
                @"\p{IsEnclosedCJKLettersandMonths}|" +
                @"\p{IsCJKCompatibility}|" +
                @"\p{IsCJKUnifiedIdeographsExtensionA}|" +
                @"\p{IsCJKUnifiedIdeographs}|" +
                @"\p{IsHangulSyllables}|" +
                @"\p{IsHiragana}|" +
                @"\p{IsKatakana}" +
                @"\p{IsKatakana}" +
                @"\p{IsCJKCompatibilityForms}").IsMatch(text) ? ScriptType.CJK
                 : Current.StartsWith("ar") || Current.StartsWith("he") || new System.Text.RegularExpressions.Regex(
                @"\p{IsArabic}|" +
                @"\p{IsHebrew}|" +
                @"\p{IsArabicPresentationForms-A}|" +
                @"\p{IsArabicPresentationForms-B}").IsMatch(text) ? ScriptType.RTL
                : ScriptType.Normal;
        }

        /// <summary>
        /// Localizes a control or form using an auto-determined or manually-inputted tag name.
        /// </summary>
        public void Control(Control c, string tag = null)
        {
            if (tag == null) tag = c.Tag != null ? c.Tag?.ToString() : c.Name;

            ScriptType script = GetScript(Program.Lang.String("preferences"));

            foreach (Control item1 in c.Controls)
            {
                localize(item1, tag, script);
                foreach (Control item2 in item1.Controls)
                {
                    localize(item2, tag, script);
                    foreach (Control item3 in item2.Controls)
                    {
                        localize(item3, tag, script);
                        foreach (Control item4 in item3.Controls)
                        {
                            localize(item4, tag, script);
                            foreach (Control item5 in item4.Controls)
                            {
                                localize(item5, tag, script);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the localized name of a console/platform.
        /// </summary>
        public string Console(Platform c) => String(c.ToString().ToLower(), "platforms");

        /// <summary>
        /// Returns a localized message string from the corresponding ID.
        /// </summary>
        /// <param name="isError">Determines if the message should be drawn from the errors category instead.</param>
        public string Msg(int number, bool isError = false) => String((isError ? "e_" : null) + number.ToString("000"), "messages");

        public string HTML(int number, bool isTooltip, string title = null)
        {
            string text = !string.IsNullOrWhiteSpace(title) ? "<b>" + title.TrimEnd(':', '：', '.', '。', '…').Trim() + "</b><br /><br />" : "";
            string input = String((isTooltip ? "t_" : "l_") + number.ToString("000"), "html");
            return "<div>" + text + input + "</div>";
        }

        /// <summary>
        /// Returns a localized string which changes depending on a boolean condition. This is the name of the string suffixed with "0" if false, or "1" if true.
        /// </summary>
        public string Toggle(bool activated, string name, string sectionName = "")
        {
            if (activated && StringCheck(name + "1", sectionName)) return String(name + "1", sectionName);
            if (!activated && StringCheck(name + "0", sectionName)) return String(name + "0", sectionName);
            return String(name, sectionName);
        }

        /// <summary>
        /// Returns an array of localized strings corresponding to the name input plus a number affixed to the end for each entry. A maximum of 100 entries is allowed.
        /// </summary>
        public string[] StringArray(string name, string sectionName = "")
        {
            List<string> result = new();

            for (int i = 0; i < 100; i++)
            {
                if (StringCheck(name + i.ToString(), sectionName)) result.Add(String(name + i.ToString(), sectionName));
            }

            return result?.Count > 0 ? result.ToArray() : new string[] { "undefined" };
        }

        /// <summary>
        /// Checks if the input localized string exists in the language file.
        /// </summary>
        public bool StringCheck(string name, string sectionName = "") => String(name, sectionName) != "undefined";

        /// <summary>
        /// Returns a localized string using the identification name, and the name of the section containing said string within the file.
        /// </summary>
        public void String(Control control, string sectionName = "")
        {
            string target = String(control.Text, sectionName) != "undefined" ? control.Text : control.Tag != null ? control.Tag.ToString() : control.Name;
            if (control.GetType() == typeof(ComboBox)) { (control as ComboBox).Items.Clear(); (control as ComboBox).Items.AddRange(StringArray(target, sectionName)); }
            else control.Text = String(target, sectionName);
        }

        /// <summary>
        /// Returns a localized string using the identification name, and the name of the section containing said string within the file.
        /// </summary>
        public string String(string name, string sectionName = "")
        {
            if (string.IsNullOrWhiteSpace(name)) return "undefined";

            int mode = 0;

            Top:
            var target = mode switch { 2 => _english, 1 => _current_parent, _ => _current };

            try
            {
                var path = sectionName.ToLower() == "main"
                              || sectionName.ToLower() == "filters"
                              || sectionName.ToLower() == "platforms"
                              || sectionName.ToLower() == "messages"
                              || sectionName.ToLower() == "html"
                              || sectionName.ToLower() == "tooltips" ? target.global : target.strings;

                string result = null;

                if (path != null && !string.IsNullOrEmpty(sectionName))
                {
                    if (!path.ContainsKey(sectionName.ToLower())) goto NotFound;

                    foreach (KeyValuePair<string, string> item in path[sectionName.ToLower()])
                    {
                        if (item.Key?.ToLower() == name?.ToLower())
                        {
                            result = item.Value;
                            goto Found;
                        }
                    }
                }

                else if (target.global != null)
                {
                    foreach (KeyValuePair<string, Dictionary<string, string>> section in target.global)
                    {
                        foreach (KeyValuePair<string, string> item in section.Value)
                        {
                            if (item.Key?.ToLower() == name?.ToLower())
                            {
                                result = item.Value;
                                goto Found;
                            }
                        }
                    }
                }

                if (result == null) goto NotFound;
                else goto Found;

                Found:
                var brackets = new List<Tuple<int, int>>();

                for (int i = 0; i < result.Length - 2; i++)
                {
                    if (result[i] == '{')
                        if (result.IndexOf('}', i) != -1)
                            brackets.Add(Tuple.Create(i, result.IndexOf('}', i)));
                }

                if (brackets.Count > 0)
                {
                    foreach (var (open, close, inside) in from pair in brackets
                                                          let open = pair.Item1
                                                          let close = pair.Item2
                                                          let inside = result.Substring(open + 1, close - open - 1)
                                                          select (open, close, inside))
                    {
                        result = !int.TryParse(inside, out int x) ? result.Replace(result.Substring(open, close - open + 1), String(inside)) : result;
                    }
                }

                return result;
            }

            catch
            {
                goto NotFound;
            }

            NotFound:
            if (mode < 2)
            {
                if (_current_parent == null) mode = 2;
                else mode++;

                goto Top;
            }

            else
            {
                return "undefined";
            }
        }
        #endregion

        #region Private methods and variables
        private string returnParent(string input, bool isFile)
        {
            if (input.ToLower() == "en") return input;

            string code = new CultureInfo(input).TwoLetterISOLanguageName;
            int tries = 0;
            
            Top:
            string file = File.Exists(Paths.Languages + code + extension) ? Paths.Languages + code + extension : null;

            string region = code.ToLower() switch
            {
                "de" => "-DE",
                "en" => "-US",
                "es" => "-ES",
                "fr" => "-FR",
                "it" => "-IT",
                "pt" => "-PT",
                "zh" => "-CN",
                _ => null
            };

            if (file == null)
            {
                tries++;

                if (region != null && tries == 1)
                    code += region;
                else if (tries == 2)
                    code = input;

                goto Top;
            }

            return isFile ? file : code;
        }

        private dynamic parseFile(string code)
        {
            if (code.ToLower() != "en")
            {
                try
                {
                    string path = Paths.Languages + code;

                    if (File.Exists(path + extension))
                    {
                        return parseFile(File.ReadAllBytes(path + extension));
                    }

                    else
                    {
                        System.Windows.Forms.MessageBox.Show($"Could not initialize the language string file \"{code}.json\" as it was not found.\n\nThe language will now be reset to system default.");
                        return null;
                    }
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Could not initialize the language string file \"{code}.json\" because of an error.\n\n{ex.GetType().FullName}\n{ex.Message}\n\nThe language will now be reset to system default.");
                    return null;
                }
            }

            else return parseFile(englishFile);
        }

        private dynamic parseFile(byte[] file)
        {
            dynamic reader = null;
            var encoding = Encoding.Unicode;

            using (MemoryStream ms = new(file))
            using (StreamReader sr = new(ms, encoding))
            {
                sr.ReadToEnd();
                encoding = sr.CurrentEncoding;

                try { JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }); }
                catch { encoding = Encoding.UTF8; }
            }

            using (MemoryStream ms = new(file))
            using (StreamReader sr = new(ms, encoding))
            using (var fileReader = JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }))
            {
                reader = JsonSerializer.Deserialize<LanguageData>(fileReader, new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
                sr.Dispose();
                ms.Dispose();
            }

            // var name = reader["language"].ToString();
            // var author = reader["author"].ToString();
            // var appName = reader["application_title"]?.ToString();

            return reader;
        }

        private void localize(Control item, string form, ScriptType script)
        {
            if (item.Tag == null) return;

            if (Current.StartsWith("ja"))
            {
                float size = item.Font.Size;
                if (size == 8.25) size = 9;
                if (size == 11) size = 9;

                item.Font = new System.Drawing.Font("MS Gothic", size, item.Font.Style);
            }

            // ----------------------------------------------------------------------------------------------------

            string name = item.Tag != null ? item.Tag?.ToString() : item.Name;

            if (item.GetType() == typeof(ComboBox))
            {
                string[] targetA = StringArray(name.ToLower(), form.ToLower());

                if (targetA[0] != "undefined")
                {
                    (item as ComboBox).Items.Clear();
                    (item as ComboBox).Items.AddRange(targetA);
                }
            }

            else
            {
                string target = item.GetType() == typeof(JCS.ToggleSwitch)
                                                ? Toggle((item as JCS.ToggleSwitch).Checked, name.ToLower(), form.ToLower())
                                                : String(name.ToLower(), form.ToLower());
                if (target == "undefined") target = String(name.ToLower());

                if (target != "undefined")
                {
                    if (item.GetType() == typeof(PlaceholderTextBox))
                        (item as PlaceholderTextBox).PlaceholderText = target;
                    else
                        item.Text = target;
                    // if (script == ScriptType.RTL) item.RightToLeft = RightToLeft.Yes;
                }
            }
        }
        #endregion
    }
}
