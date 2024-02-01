using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class SettingsForm : Form
    {
        private readonly Language Strings = Program.Language;
        private readonly List<string> langs = new List<string>();
        bool isDirty = false;

        public SettingsForm()
        {
            InitializeComponent();
            Strings.Localize(this);

            // -----------------------------
            // Add system default
            langs.Add("sys");
            LanguageList.Items.Add($"<{Strings.Get("s002")}>");

            // Add all languages
            foreach (string file in Directory.GetFiles(Paths.Languages))
                if (Language.Read(file) != null)
                {
                    langs.Add(Path.GetFileNameWithoutExtension(file));
                    LanguageList.Items.Add(LanguageName(file));
                }
            LanguageList.Sorted = true;

            if (Default.UI_Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = LanguageList.Items.IndexOf(LanguageName(Default.UI_Language, false));
            // -----------------------------
            imageintpl.SelectedIndex = Default.ImageInterpolation;
            s006.Checked = Default.AutoLibRetro;
        }

        private string LanguageName(string fileName, bool isFilePath = true)
        {
            // For using name from .json file:
            //   x.LangInfo(Path.GetFileNameWithoutExtension(file))[0] (for certain file)    //
            //   x.LangInfo(Default.Language)[0]                       (for selected).       //

            // Should not be removed so as to not break the language functions, this is merely a cosmetic edit for now

            string var = isFilePath ? Path.GetFileNameWithoutExtension(fileName) : fileName;
            string langName = new System.Globalization.CultureInfo(var).NativeName;

            // Capitalize first letter
            langName = langName.Substring(0, 1).ToUpper() + langName.Substring(1, langName.Length - 1);

            return langName;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            if (!isDirty)
            {
                if (LanguageList.SelectedIndex == 0)
                {
                    isDirty = Default.UI_Language != "sys";
                    Default.UI_Language = "sys";
                }
                else
                {
                    foreach (string file in Directory.GetFiles(Paths.Languages))
                        if (Language.Read(file) != null)
                        {
                            if (LanguageName(file) == LanguageList.SelectedItem.ToString())
                            {
                                isDirty = Path.GetFileNameWithoutExtension(file) != Default.UI_Language;
                                Default.UI_Language = Path.GetFileNameWithoutExtension(file);
                                break;
                            }
                        }
                }
            }

            // -------------------------------------------
            // Other settings
            // -------------------------------------------
            Default.ImageInterpolation = imageintpl.SelectedIndex;
            Default.AutoLibRetro = s006.Checked;

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            if (isDirty)
            {
                if (MessageBox.Show(Strings.Get("m000"), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Default.Save();
                    Application.Restart();
                }
            }
            else
            {
                Default.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void Cancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
