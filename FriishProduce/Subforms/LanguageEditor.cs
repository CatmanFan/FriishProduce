﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class LanguageEditor : Form
    {
        private List<string> iso_codes = new List<string>() { "custom" };
        Language.LanguageData source, target;

        string currentFile, windowTitle = null;
        string fullWindowTitle { get => windowTitle + (!string.IsNullOrWhiteSpace(currentFile) ? " - " + Path.GetFileName(currentFile) : " - Untitled"); }

        bool _unsaved, loaded = false;
        bool Unsaved
        {
            get => _unsaved; set
            {
                if (windowTitle != null && !value)
                    Text = fullWindowTitle;

                if (!_unsaved && value && loaded)
                    Text += "*";

                _unsaved = value && loaded;
            }
        }

        public LanguageEditor()
        {
            InitializeComponent();
            MinimumSize = Size;
            languages_list.SelectedIndex = 0;
            openFileDialog.InitialDirectory = Paths.Languages;

            windowTitle = Text;
            Text = fullWindowTitle;

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

            Open();
        }

        private bool Discard()
        {
            if (Unsaved)
            {
                MessageBox.Result message = MessageBox.Show("The current file has unsaved changes.", "What would you like to do?", new string[] { "Save", "Don't save", "Cancel" }, MessageBox.Icons.None);
                switch (message)
                {
                    case MessageBox.Result.Button1:
                        return Save();
                    case MessageBox.Result.Button2:
                        return true;
                    default:
                        return false;
                }
            }

            else return true;
        }

        private void Exit(object sender, EventArgs e) => Close();

        private void Exit_Form(object sender, FormClosingEventArgs e) => e.Cancel = !Discard();

        private void Open(string file = null)
        {
            bool invalid = !File.Exists(file) || string.IsNullOrWhiteSpace(file);

            currentFile = invalid ? null : file;
            Text = fullWindowTitle;

            if (invalid) source = null;
            Open(!invalid ? File.ReadAllBytes(file) : Properties.Resources.English);
            status_label.Text = invalid ? "Loaded empty file." : $"Loaded {Path.GetFileName(file)}. ({percentage:F0}% translated)";
        }

        private readonly string prefix_global = "global: ";
        private readonly string prefix_strings = "strings: ";
        private double percentage
        {
            get
            {
                (double count, double filled) = (0.0, 0.0);

                foreach (DataGridViewRow row in strings.Rows)
                {
                    count += 1.0;
                    if (!string.IsNullOrEmpty(row.Cells[3].Value?.ToString()))
                        filled += 1.0;
                }

                return filled / count * 100.0;
            }
        }

        private void Open(byte[] file)
        {
            loaded = Unsaved = false;
            bool isEnglish = source == null;
            string txt = Language.Convert(file);

            if (txt == null)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            using (var fileReader = JsonDocument.Parse(txt, new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }))
            {
                target = JsonSerializer.Deserialize<Language.LanguageData>(fileReader, new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip });

                // -------------------------
                // Fill in all English strings
                // -------------------------
                if (isEnglish)
                {
                    source = target;
                    target.author = "[Your username here]";
                    target.language = null;

                    // Add rows
                    // ****************
                    strings.Rows.Clear();
                    filter_by_section.DropDownItems.Clear();
                    filter_by_section.DropDownItems.Add("All", null, FilterBySection);

                    var brackets_filter = new System.Text.RegularExpressions.Regex(@"{(.*?)}");

                    foreach (var section in source.global)
                    {
                        filter_by_section.DropDownItems.Add(prefix_global + section.Key, null, FilterBySection);

                        // Dictionary<string, Dictionary<string, string>>
                        foreach (var item in section.Value)
                        {
                            var match = brackets_filter.Match(item.Value);
                            if (match.Value?.ToLower() != item.Value.ToLower())
                                strings.Rows.Add(prefix_global + section.Key, item.Key, item.Value.Replace("\n", "\r\n"), "");
                        }
                    }

                    foreach (var section in source.strings)
                    {
                        filter_by_section.DropDownItems.Add(prefix_strings + section.Key, null, FilterBySection);

                        foreach (var item in section.Value)
                        {
                            var match = brackets_filter.Match(item.Value);
                            if (match.Value?.ToLower() != item.Value.ToLower())
                                strings.Rows.Add(prefix_strings + section.Key, item.Key, item.Value.Replace("\n", "\r\n"), "");
                        }
                    }

                    foreach (DataGridViewRow row in strings.Rows)
                    {
                        UpdateCell(row.Cells[3], false);
                    }
                }

                title.Text = target.application_title;
                author.Text = target.author;
                iso_code.Text = target.language;

                // -------------------------
                // Clear old data
                // -------------------------
                for (int i = 0; i < strings.Rows.Count; i++)
                {
                    strings[3, i].Value = null;
                    UpdateCell(strings[3, i]);
                }
                languages_list.SelectedIndex = 0;

                // -------------------------
                // Fill in all translated strings
                // -------------------------
                if (!isEnglish)
                {
                    // Search for ISO code
                    // ****************
                    try
                    {
                        languages_list.SelectedIndex = iso_codes.IndexOf(iso_code.Text);
                        if (languages_list.SelectedIndex < 0)
                            languages_list.SelectedIndex = 0;
                    }
                    catch { languages_list.SelectedIndex = 0; }

                    // Go
                    // ****************
                    if (target.global != null)
                    {
                        foreach (var section in target.global)
                        {
                            foreach (DataGridViewRow row in strings.Rows)
                            {
                                string sectionName = row.Cells[0].Value.ToString();
                                string id = row.Cells[1].Value.ToString();

                                if (section.Value.ContainsKey(id) && sectionName.Contains(section.Key))
                                    row.Cells[3].Value = section.Value[id].Replace("\n", "\r\n");

                                UpdateCell(row.Cells[3], false);
                            }
                        }
                    }

                    if (target.strings != null)
                    {
                        foreach (var section in target.strings)
                        {
                            foreach (DataGridViewRow row in strings.Rows)
                            {
                                string sectionName = row.Cells[0].Value.ToString();
                                string id = row.Cells[1].Value.ToString();

                                if (section.Value.ContainsKey(id) && sectionName.Contains(section.Key))
                                    row.Cells[3].Value = section.Value[id].Replace("\n", "\r\n");

                                UpdateCell(row.Cells[3], false);
                            }
                        }
                    }
                }
            }

            // var name = reader["language"].ToString();
            // var author = reader["author"].ToString();
            // var appName = reader["application_title"]?.ToString();

            loaded = true;
        }

        private bool Save(string file = null)
        {
            // -------------------------
            // Set currentFile string
            // -------------------------
            if (!string.IsNullOrWhiteSpace(file)) currentFile = file;

            if (string.IsNullOrWhiteSpace(currentFile))
            {
                currentFile = saveFileDialog.FileName;

                if (string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    saveFileDialog.FileName = iso_code.Text + ".json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        currentFile = saveFileDialog.FileName;
                    else return false;
                }
            }

            // -------------------------
            // Replace sections with new ones
            // -------------------------
            target.language = iso_code.Text;
            target.author = author.Text;
            target.application_title = title.Text;
            target.global = new();
            target.strings = new();

            foreach (DataGridViewRow row in strings.Rows)
            {
                bool isGlobal = row.Cells[0].Value.ToString().ToLower().StartsWith(prefix_global);
                string sectionName = isGlobal ? row.Cells[0].Value.ToString().Replace(prefix_global, null) : row.Cells[0].Value.ToString().Replace(prefix_strings, null);
                var dest = isGlobal ? target.global : target.strings;

                string id = row.Cells[1].Value.ToString();

                if (!string.IsNullOrEmpty(row.Cells[3].Value?.ToString()))
                {
                    if (!dest.ContainsKey(sectionName)) dest.Add(sectionName, new());
                    dest[sectionName].Add(id, row.Cells[3].Value.ToString().Replace("\r\n", "\n"));
                }
            }

            var outFile = JsonSerializer.Serialize(target, new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                WriteIndented = true,
                IndentCharacter = Convert.ToChar(9),
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }).Replace("\n\n", "\n").Replace("\t\t", "\t");

            File.WriteAllText(currentFile, outFile);

            // -------------------------
            // Finalized
            // -------------------------
            status_label.Text = $"Saved to {Path.GetFileName(currentFile)}. ({percentage:F0}% translated)";
            Unsaved = false;
            return true;
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (Discard() && openFileDialog.ShowDialog() == DialogResult.OK)
                Open(openFileDialog.FileName);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (sender == menuItem3)
            {
                saveFileDialog.FileName = iso_code.Text + ".json";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    Save(saveFileDialog.FileName);
            }

            else Save();
        }

        private void Strings_CellEndEdit(object sender, DataGridViewCellEventArgs e) => UpdateCell(strings.CurrentCell);

        private void Strings_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode is Keys.Back or Keys.Delete) && strings.CurrentCell.ColumnIndex == 3)
            {
                strings.CurrentCell.Value = null;
                UpdateCell(strings.CurrentCell);
            }
        }

        private void Strings_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (strings.CurrentCell.ColumnIndex == 2)
            {
                Clipboard.SetText(strings.CurrentCell.Value.ToString());
                status_label.Text = $"Copied original string at {strings.CurrentRow.Cells[0].Value} > {strings.CurrentRow.Cells[1].Value}.";
            }
        }

        private void ClearAll(object sender, EventArgs e)
        {
            if (Discard()) Open();
        }

        private void Find(object sender, EventArgs e)
        {
            var target = sender as ToolStripTextBox;
            int targetColumn = target == find_translated ? 3 : 2;
            string text = target.Text.ToLower();

            foreach (DataGridViewRow row in strings.Rows)
                row.Visible = string.IsNullOrEmpty(text) || row.Cells[targetColumn].Value?.ToString().ToLower().Contains(text) == true;
        }

        private void FilterBySection(object sender, EventArgs e)
        {
            string text = null;
            int index = 0;
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                text = (sender as ToolStripMenuItem).Text;
                index = (sender as ToolStripMenuItem).GetCurrentParent().Items.IndexOf((sender as ToolStripMenuItem));
            }

            foreach (DataGridViewRow row in strings.Rows)
                row.Visible = index == 0 || row.Cells[0].Value?.ToString().ToLower().Contains(text) == true;
        }

        private void ClearIdentical(object sender, EventArgs e)
        {
            (double count, double filled) = (0.0, 0.0);

            foreach (DataGridViewRow row in strings.Rows)
            {
                count += 1.0;

                if (string.Compare(row.Cells[3].Value?.ToString(), row.Cells[2].Value?.ToString()) == 0)
                {
                    row.Cells[3].Value = null;
                    UpdateCell(row.Cells[3], false);
                    filled += 1.0;
                }
            }

            if (filled > 0)
                status_label.Text = $"Cleared identical strings ({filled / count * 100.0:F0}% of all strings).";
            else
                status_label.Text = "No identical strings were found.";
        }

        private void About(object sender, EventArgs e) { using var about = new About(true); about.ShowDialog(); }

        private void Text_Changed(object sender, EventArgs e) => Unsaved = true;

        private void Languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            iso_code.ReadOnly = languages_list.SelectedIndex != 0;
            if (languages_list.SelectedIndex > 0) iso_code.Text = iso_codes[languages_list.SelectedIndex];

            Unsaved = true;
        }

        private void UpdateCell(DataGridViewCell cell, bool makeUnsaved = true)
        {
            cell.Style.BackColor = string.IsNullOrEmpty(cell.Value?.ToString()) ? Color.MistyRose : strings.DefaultCellStyle.BackColor;

            if (makeUnsaved) Unsaved = true;
        }
    }
}
