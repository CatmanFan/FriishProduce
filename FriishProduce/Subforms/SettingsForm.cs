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
        bool isDirty = false;

        public SettingsForm()
        {
            InitializeComponent();

            Language.AutoSetForm(this);
            Text = Language.Get("Settings");
            LanguageList.Items[0] = "<" + LanguageList.Items[0].ToString() + ">";

            // Add all languages
            foreach (var item in Language.List)
                LanguageList.Items.Add(item.Value);
            var x = Default.UI_Language;

            if (Default.UI_Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = Language.List.Keys.ToList().IndexOf(Default.UI_Language) + 1;
            // -----------------------------
            DefaultImageInterpolation.Items[0] = Language.Get("ByDefault");
            DefaultImageInterpolation.Items.AddRange(Language.GetArray("List_ImageInterpolation"));
            DefaultImageInterpolation.SelectedIndex = Default.ImageInterpolation;
            AutoLibRetro.Checked = Default.AutoLibRetro;
        }

        private string LanguageName(string code)
        {
            foreach (var item in Language.List)
                if (item.Key.ToLower() == code.ToLower()) return item.Value;
            return null;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // -------------------------------------------
            // Language setting
            // -------------------------------------------
            if (LanguageList.SelectedIndex == 0)
                Default.UI_Language = "sys";
            else
                foreach (var item in Language.List)
                    if (item.Value == LanguageList.SelectedItem.ToString())
                        Default.UI_Language = item.Key;

            Language.Current = LanguageList.SelectedIndex == 0 ? Language.GetSystemLanguage() : new System.Globalization.CultureInfo(Default.UI_Language);

            // -------------------------------------------
            // Other settings
            // -------------------------------------------
            Default.ImageInterpolation = DefaultImageInterpolation.SelectedIndex;
            Default.AutoLibRetro = AutoLibRetro.Checked;

            // -------------------------------------------
            // Restart message box & save changes
            // -------------------------------------------
            /*if (isDirty)
            {
                if (MessageBox.Show(Language.Get("Message000"), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Default.Save();
                    Application.Restart();
                }
            }
            else*/

            Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
