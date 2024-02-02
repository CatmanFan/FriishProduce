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

namespace FriishProduce
{
    public partial class MainForm : RibbonForm
    {
        internal LibRetroDB LibRetro { get; set; }

        private void AutoSetRibbon()
        {
            ribbon1.OrbText = Language.Get("File");
            string text = null;

            foreach (RibbonTab tab in ribbon1.Tabs)
            {
                text = Language.Get(tab.Name, this);
                if (text != "undefined") tab.Text = text;

                foreach (RibbonPanel panel in tab.Panels)
                {
                    text = Language.Get(panel.Name, this);
                    if (text != "undefined") panel.Text = text;

                    foreach (RibbonButton button in panel.Items.OfType<RibbonButton>())
                    {
                        text = Language.Get(button.Name, this);
                        if (text != "undefined") button.Text = text;
                    }
                }
            }

            foreach (RibbonButton button in ribbon1.OrbDropDown.MenuItems.OfType<RibbonButton>())
            {
                text = Language.Get(button.Name, this);
                if (text != "undefined") button.Text = text;
            }

            foreach (RibbonButton item in NewProject.DropDownItems.OfType<RibbonButton>())
            {
                item.Text = string.Format(Language.Get("ProjectType"), Language.Get($"Platform_{item.Text.Replace("CreateProject", "")}"));
                item.Click += CreateProject_Click;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            AutoSetRibbon();
            Language.AutoSetForm(this);

            Text = Language.Get("_AppTitle");
            MenuItem_Settings.Text = Language.Get("Settings");

            Strip_OpenROM.Image = OpenROM.SmallImage;
            Strip_OpenImage.Image = OpenImage.SmallImage;
            Strip_OpenROM.Text = OpenROM.Text;
            Strip_UseLibRetro.Text = UseLibRetro.Text;
            BrowseImage.Title = Strip_OpenImage.Text = OpenImage.Text;
            SaveWAD.Title = Strip_ExportWAD.Text = ExportWAD.Text;

            BrowseImage.Filter = Language.Get("Filter_Img");
            SaveWAD.Filter = Language.Get("Filter_WAD");
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            SettingsForm s = new SettingsForm();
            s.ShowDialog(this);
        }

        public void TabChanged(object sender, EventArgs e)
        {
            // Toggle visibility of Open ROM/Image buttons
            // ********
            if (sender != tabControl.TabPages[0]) OpenROM.Enabled = tabControl.TabPages.Count > 1;
            else OpenROM.Enabled = true;
            Strip_OpenROM.Enabled = OpenROM.Enabled;
            Strip_OpenImage.Enabled = OpenImage.Enabled = OpenROM.Enabled;
            tabControl.Visible = OpenROM.Enabled;

            // Toggle visibility of Export WAD button
            // ********
            if (!OpenROM.Enabled) { Strip_ExportWAD.Enabled = ExportWAD.Enabled = false; }
            else ExportCheck(sender, e);

            // Toggle visibility of Download LibRetro data button
            // ********
            if (!OpenROM.Enabled) { Strip_UseLibRetro.Enabled = UseLibRetro.Enabled = false; }

            // Context menu
            // ********
            if (tabControl.TabPages.Count >= 1)
            {
                foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
                    tabPage.ContextMenuStrip = null;
                tabControl.TabPages.SelectedTab().ContextMenuStrip = TabContextMenu;

                if (sender == tabControl.TabPages[0]) Strip_UseLibRetro.Enabled = UseLibRetro.Enabled = (tabControl.SelectedForm as InjectorForm).ROMLoaded;
            }
        }

        private void ExportCheck(object sender, EventArgs e) => Strip_ExportWAD.Enabled = ExportWAD.Enabled = (tabControl.SelectedForm as InjectorForm).ReadyToExport;

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            bool isUnsaved = false;
            foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
            {
                var x = tabPage.Form as Form;
                if (x.Tag != null && x.Tag.ToString() == "dirty") isUnsaved = true;
            }

            if (isUnsaved)
            {
                if (MessageBox.Show(Language.Get("Message002"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    e.Cancel = true;
                else
                    foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
                        (tabPage.Form as Form).Tag = null;
            }
        }

        /// <summary>
        /// Adds a new project to the Main Form.
        /// </summary>
        private void CreateProject_Click(object sender, EventArgs e)
        {
            Console console;
            if (!Enum.TryParse((sender as RibbonButton).Name.Replace("CreateProject_", ""), out console))
                return;

            tabControl.Visible = true;

            InjectorForm Tab = new InjectorForm(console) { Parent = this };
            Tab.FormClosed += TabChanged;
            Tab.ExportCheck += ExportCheck;
            tabControl.TabPages.Add(Tab);
        }

        private void OpenROM_Click(object sender, EventArgs e)
        {
            switch ((tabControl.SelectedForm as InjectorForm).Console)
            {
                default:
                    BrowseROM.Filter = Language.Get("Filter_Disc") + "|" + Language.Get("Filter_ZIP") + Language.Get("Filter_All");
                    break;

                case Console.NES:
                case Console.SNES:
                case Console.N64:
                case Console.SMS:
                case Console.SMDGEN:
                case Console.PCE:
                    BrowseROM.Filter = Language.Get("Filter_ROM_" + Enum.GetName(typeof(Console), (int)(tabControl.SelectedForm as InjectorForm).Console));
                    break;
            }

            if (BrowseROM.ShowDialog() == DialogResult.OK)
            {
                (tabControl.SelectedForm as InjectorForm).LoadROM(Properties.Settings.Default.AutoLibRetro);
                Strip_UseLibRetro.Enabled = UseLibRetro.Enabled = true;
            }
        }
        private void UseLibRetro_Click(object sender, EventArgs e) => (tabControl.SelectedForm as InjectorForm).LoadLibRetroData();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in Directory.GetFiles(Paths.WorkingFolder, "*.*", SearchOption.AllDirectories))
                    if (!Path.GetFileName(item).ToLower().Contains("readme.md")) File.Delete(item);
                foreach (var item in Directory.GetDirectories(Paths.WorkingFolder))
                    Directory.Delete(item, true);
            }
            catch { }

            var currentForm = tabControl.SelectedForm as InjectorForm;

            SaveWAD.FileName = currentForm.GetName();
            if (SaveWAD.ShowDialog() == DialogResult.OK) currentForm.CreateInject(SaveWAD.FileName);
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            if (BrowseImage.ShowDialog() == DialogResult.OK)
            {
                var currentForm = tabControl.SelectedForm as InjectorForm;
                currentForm.LoadImage(BrowseImage.FileName);
            }
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();
    }
}
