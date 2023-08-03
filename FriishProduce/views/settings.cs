using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    public partial class Settings : Form
    {
        readonly Lang x = Program.Language;
        readonly List<string> langs = new List<string>();
        bool showRestart = false;

        public Settings()
        {
            InitializeComponent();
            x.Localize(this);

            // -----------------------------
            // Add system default
            langs.Add("sys");
            Language.Items.Add($"<{x.Get("s002")}>");

            // Add all languages
            foreach (string file in Directory.GetFiles(Paths.Languages))
                if (Lang.Read(file) != null)
                {
                    Language.Items.Add(LanguageName(file));
                    langs.Add(Path.GetFileNameWithoutExtension(file));
                }

            if (Default.Language == "sys") Language.SelectedIndex = 0;
            else Language.SelectedIndex = Language.Items.IndexOf(LanguageName(Default.Language, false));
            // -----------------------------

            OpenWhenDone.Checked = Default.OpenWhenDone;
            AutoRetrieve.Checked = Default.D_Custom_AutoRetrieveROMData;
            ImgInterp.SelectedIndex = Default.D_Custom_InterpolationMode;
            Theme.SelectedIndex = Default.LightTheme ? 1 : 0;
            FileName.Text = Default.WadName;
            FileNameZIP.Text = Default.ZipName;
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
            if (!showRestart)
            {
                if (Language.SelectedIndex == 0)
                {
                    showRestart = Default.Language != "sys";
                    Default.Language = "sys";
                }
                else
                {
                    foreach (string file in Directory.GetFiles(Paths.Languages))
                        if (Lang.Read(file) != null)
                        {
                            if (LanguageName(file) == Language.SelectedItem.ToString())
                            {
                                showRestart = Path.GetFileNameWithoutExtension(file) != Default.Language;
                                Default.Language = Path.GetFileNameWithoutExtension(file);
                                break;
                            }
                        }
                }
            }

            if (showRestart) MessageBox.Show(x.Get("m001"), Text);

            Default.OpenWhenDone = OpenWhenDone.Checked;
            Default.D_Custom_AutoRetrieveROMData = AutoRetrieve.Checked;
            Default.D_Custom_InterpolationMode = ImgInterp.SelectedIndex;
            Default.LightTheme = Theme.SelectedIndex == 1;
            Default.WadName = FileName.Text;
            Default.ZipName = FileNameZIP.Text;
            Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Language.SelectedIndex > 0)
                ToolTip.SetToolTip(Language, string.Format(x.Get("g005"), x.LangInfo(langs[Language.SelectedIndex])[1]));
            else
                ToolTip.SetToolTip(Language, null);
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = OK.Enabled == false && Cancel.Enabled == false;
    }
}
