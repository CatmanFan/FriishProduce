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

            NewProject.DropDownItems.Clear();
            NewProject.DropDownItems.AddRange(new List<RibbonItem>
            {
                new RibbonSeparator(Language.Get("PlatformGroup_0")),

                new RibbonButton(new Icon(Properties.Resources.nintendo_nes, 16, 16).ToBitmap())          { Tag = Console.NES.ToString() },
                new RibbonButton(new Icon(Properties.Resources.nintendo_super_nes, 16, 16).ToBitmap())    { Tag = Console.SNES.ToString() },
                new RibbonButton(new Icon(Properties.Resources.nintendo_nintendo64, 16, 16).ToBitmap())   { Tag = Console.N64.ToString() },

                new RibbonSeparator(Language.Get("PlatformGroup_1")),

                new RibbonButton(new Icon(Properties.Resources.sega_master_system, 16, 16).ToBitmap())    { Tag = Console.SMS.ToString() },
                new RibbonButton(new Icon(Properties.Resources.sega_genesis, 16, 16).ToBitmap())          { Tag = Console.SMDGEN.ToString() },

                new RibbonSeparator(Language.Get("PlatformGroup_2")),

                new RibbonButton(new Icon(Properties.Resources.snk_neo_geo_aes, 16, 16).ToBitmap())    { Tag = Console.NeoGeo.ToString() },
            });

            foreach (RibbonButton item in NewProject.DropDownItems.OfType<RibbonButton>())
            {
                item.Text = string.Format(Language.Get("ProjectType"), Language.Get($"Platform_{item.Tag}"));
                item.Click += AddProject;
            }

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
                        if (text != "undefined")
                        {
                            button.Text = text;
                            button.MinimumSize = new Size(20 + (button.Text.Length * 5), 0);
                            button.MaximumSize = button.MaximumSize;
                        }
                    }
                }
            }

            foreach (RibbonButton button in ribbon1.OrbDropDown.MenuItems.OfType<RibbonButton>())
            {
                text = Language.Get(button.Name, this);
                if (text != "undefined") button.Text = text;
            }

        }

        /// <summary>
        /// Changes language of this form and all tab pages
        /// </summary>
        private void RefreshForm()
        {
            AutoSetRibbon();
            Language.AutoSetForm(this);

            Text = Language.Get("_AppTitle");
            MenuItem_Settings.Text = Language.Get("Settings");
            BrowseROM.Title = BrowseImage.Title = ribbonPanel_Open.Text;
            SaveWAD.Title = Strip_ExportWAD.Text = ExportWAD.Text;

            Strip_UseLibRetro.Text = UseLibRetro.Text;
            Strip_OpenROM.Text = Language.Get(Strip_OpenROM.Name, this);
            Strip_OpenImage.Text = Language.Get(Strip_OpenImage.Name, this);
            CloseTab.Text = Language.Get(CloseTab.Name, this);

            Strip_OpenROM.Image = OpenROM.SmallImage;
            Strip_OpenImage.Image = OpenImage.SmallImage;
            Strip_UseLibRetro.Image = UseLibRetro.SmallImage;
            Strip_ExportWAD.Image = ExportWAD.SmallImage;

            BrowseImage.Filter = Language.Get("Filter_Img");
            SaveWAD.Filter = Language.Get("Filter_WAD");

            foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Form.GetType() == typeof(InjectorForm))
                    (tabPage.Form as InjectorForm).RefreshForm();
            }
        }

        public MainForm()
        {
            InitializeComponent();
            RefreshForm();

            Strip_OpenROM.Image = OpenROM.SmallImage;
            Strip_OpenImage.Image = OpenImage.SmallImage;

            // Automatically set defined initial directories for each browse/save file dialog
            // ********
            BrowseManual.SelectedPath = BrowseImage.InitialDirectory = BrowseROM.InitialDirectory = Paths.EnvironmentFolder;
            SaveWAD.InitialDirectory = Paths.Out;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.UI_Language;

            SettingsForm s = new SettingsForm();
            s.ShowDialog(this);

            if (lang != Properties.Settings.Default.UI_Language) RefreshForm();
        }

        public void TabChanged(object sender, EventArgs e)
        {
            // Toggle visibility of Open ROM/Image buttons
            // ********
            if (sender != tabControl.TabPages[0]) OpenROM.Enabled = tabControl.TabPages.Count > 1;
            else OpenROM.Enabled = true;
            tabControl.Visible = OpenManual.Enabled = Strip_OpenImage.Enabled = OpenImage.Enabled = Strip_OpenROM.Enabled = OpenROM.Enabled;

            // Toggle visibility of Export WAD button
            // Toggle visibility of Download LibRetro data button
            // ********
            if (!OpenROM.Enabled)
            {
                Strip_ExportWAD.Enabled = ExportWAD.Enabled = false;
                Strip_UseLibRetro.Enabled = UseLibRetro.Enabled = false;
            }

            else ExportCheck(sender, e);

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
        private void AddProject(object sender, EventArgs e)
        {
            if (!Enum.TryParse((sender as RibbonButton).Tag.ToString(), out Console console))
                return;

            tabControl.Visible = true;

            InjectorForm Tab = new InjectorForm(console) { Parent = this };
            Tab.FormClosed += TabChanged;
            Tab.ExportCheck += ExportCheck;
            tabControl.TabPages.Add(Tab);

            // BrowseROMDialog(console, Tab);
        }

        private void OpenROM_Click(object sender, EventArgs e) => BrowseROMDialog((tabControl.SelectedForm as InjectorForm).Console, tabControl.SelectedForm as InjectorForm);

        private void BrowseROMDialog(Console c, InjectorForm currentForm)
        {
            switch (c)
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
                    BrowseROM.Filter = Language.Get($"Filter_ROM_{c}");
                    break;

                case Console.NeoGeo:
                    BrowseROM.Filter = Language.Get("Filter_ZIP");
                    break;
            }

            if (BrowseROM.ShowDialog() == DialogResult.OK)
            {
                currentForm.LoadROM(Properties.Settings.Default.AutoLibRetro);
                Strip_UseLibRetro.Enabled = UseLibRetro.Enabled = true;
            }
        }

        private void UseLibRetro_Click(object sender, EventArgs e) => (tabControl.SelectedForm as InjectorForm).LoadLibRetroData();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            CleanTemp();
            var currentForm = tabControl.SelectedForm as InjectorForm;

            SaveWAD.FileName = currentForm.GetName();
            if (SaveWAD.ShowDialog() == DialogResult.OK)
                if (currentForm.CreateInject())
                    currentForm.Close();
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            if (BrowseImage.ShowDialog() == DialogResult.OK)
            {
                var currentForm = tabControl.SelectedForm as InjectorForm;
                currentForm.LoadImage(BrowseImage.FileName);
            }
        }

        private void OpenManual_Click(object sender, EventArgs e) => (tabControl.SelectedForm as InjectorForm).LoadManual(BrowseManual.ShowDialog() == DialogResult.OK ? BrowseManual.SelectedPath : null);

        public void CleanTemp()
        {
            try
            {
                foreach (var item in Directory.GetFiles(Paths.WorkingFolder, "*.*", SearchOption.AllDirectories))
                    if (!Path.GetFileName(item).ToLower().Contains("readme.md")) File.Delete(item);
                foreach (var item in Directory.GetDirectories(Paths.WorkingFolder))
                    Directory.Delete(item, true);
            }
            catch { }
        }

        private void CloseTab_Click(object sender, EventArgs e) => (tabControl.SelectedForm as Form).Close();

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();
    }
}
