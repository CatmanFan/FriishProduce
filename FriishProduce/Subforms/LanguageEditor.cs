using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class LanguageEditor : Form
    {
        private List<string> iso_codes = new List<string>() { "custom" };
        Language.LanguageData source, target;

        public LanguageEditor()
        {
            InitializeComponent();
            MinimumSize = Size;
            languages_list.SelectedIndex = 0;
            openFileDialog.InitialDirectory = Paths.Languages;

            SortedDictionary<string, string> langsDict = new();

            foreach (var item in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try { langsDict.Add(item.EnglishName, item.Name); } catch { }
            }

            langsDict.Remove("English");

            string[] langs = new string[langsDict.Count];
            langsDict.Keys.CopyTo(langs, 0);
            languages_list.Items.AddRange(langs);
            langsDict.Values.CopyTo(langs, 0);
            iso_codes.AddRange(langs);

            Open(Properties.Resources.English);
        }

        private void Exit(object sender, EventArgs e) => Close();

        private void Exit_Form(object sender, FormClosingEventArgs e) => e.Cancel = MessageBox.Show(null, "This will discard all unsaved changes.", new string[] { "Exit", "Cancel" }, MessageBox.Icons.None) == MessageBox.Result.Button2;

        private void Open(byte[] file)
        {
            bool isEnglish = source == null;

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
                target = JsonSerializer.Deserialize<Language.LanguageData>(fileReader, new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });
                if (isEnglish)
                {
                    source = target;
                    target.author = "[Your username here]";
                    target.language = null;

                    foreach (var section in source.global)
                    {
                        // Dictionary<string, Dictionary<string, string>>
                        foreach (var item in section.Value)
                        {
                            strings.Rows.Add($"global > {section.Key}", item.Key, item.Value, "");
                        }
                    }

                    foreach (var section in source.strings)
                    {
                        foreach (var item in section.Value)
                        {
                            strings.Rows.Add($"strings > {section.Key}", item.Key, item.Value, "");
                        }
                    }
                }

                title.Text = target.application_title;
                author.Text = target.author;
                iso_code.Text = target.language;

                if (!isEnglish)
                {
                    try { languages_list.SelectedIndex = iso_codes.IndexOf(iso_code.Text); } catch { languages_list.SelectedIndex = 0; }

                    int index = 0;
                    if (target.global != null)
                    {
                        foreach (var row in strings.Rows)
                        {
                            ;
                        }
                        foreach (var section in target.global)
                        {
                            foreach (var item in section.Value)
                            {
                                if (strings.Rows[index].Cells[3].Value?.ToString() != item.Value)
                                    strings.Rows[index].Cells[3].Value = item.Value;
                                else
                                    strings.Rows[index].Cells[3].Value = null;
                                index++;
                            }
                        }
                    }

                    if (target.strings != null)
                    {
                        foreach (var section in target.strings)
                        {
                            foreach (var item in section.Value)
                            {
                                if (strings.Rows[index].Cells[3].Value?.ToString() != item.Value)
                                    strings.Rows[index].Cells[3].Value = item.Value;
                                else
                                    strings.Rows[index].Cells[3].Value = null;
                                index++;
                            }
                        }
                    }
                }

                sr.Dispose();
                ms.Dispose();
            }

            // var name = reader["language"].ToString();
            // var author = reader["author"].ToString();
            // var appName = reader["application_title"]?.ToString();
        }

        private void Save(string file)
        {

        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                Open(File.ReadAllBytes(openFileDialog.FileName));
        }

        private void Save_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = iso_code.Text + ".json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                Save(saveFileDialog.FileName);
        }

        private void Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            iso_code.ReadOnly = languages_list.SelectedIndex != 0;
            if (languages_list.SelectedIndex > 0) iso_code.Text = iso_codes[languages_list.SelectedIndex];
        }
    }
}
