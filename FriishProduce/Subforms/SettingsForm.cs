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

            // Add all languages
            foreach (var item in Language.List)
                LanguageList.Items.Add(item.Value);
            LanguageList.Sorted = true;

            if (Default.UI_Language == "sys") LanguageList.SelectedIndex = 0;
            else LanguageList.SelectedIndex = LanguageList.Items.IndexOf(LanguageName(Default.UI_Language));
            // -----------------------------
            imageintpl.Items[0] = Properties.Strings.ByDefault;
            imageintpl.Items.AddRange(Language.GetArray("List_ImageInterpolation"));
            imageintpl.SelectedIndex = Default.ImageInterpolation;
            s006.Checked = Default.AutoLibRetro;
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
            if (!isDirty)
            {
                if (LanguageList.SelectedIndex == 0)
                {
                    isDirty = Default.UI_Language != "sys";
                    Default.UI_Language = "sys";
                }
                else
                {
                    foreach (var item in Language.List)
                        if (item.Value == LanguageList.SelectedItem.ToString())
                        {
                            isDirty = item.Value != Default.UI_Language;
                            Default.UI_Language = item.Key;
                            break;
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
                if (MessageBox.Show(Language.Get("Message000"), ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
