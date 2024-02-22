using Ookii.Dialogs.WinForms;
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
    public partial class MainForm : Form
    {
        private readonly SettingsForm s = new SettingsForm();

        internal LibRetroDB LibRetro { get; set; }

        private void AutoSetStrip()
        {
            NewProject.DropDownItems.Clear();

            var items = new ToolStripItem[][]
            {
                new ToolStripItem[]
                {
                    new ToolStripMenuItem(null, new Icon(Properties.Resources.nintendo_nes, 16, 16).ToBitmap(), AddProject, Console.NES.ToString()),
                    new ToolStripMenuItem(null, new Icon(Properties.Resources.nintendo_super_nes, 16, 16).ToBitmap(), AddProject, Console.SNES.ToString()),
                    new ToolStripMenuItem(null, new Icon(Properties.Resources.nintendo_nintendo64, 16, 16).ToBitmap(), AddProject, Console.N64.ToString()),
                    new ToolStripSeparator(),
                },

                new ToolStripItem[]
                {
                    new ToolStripMenuItem(null, new Icon(Properties.Resources.sega_master_system, 16, 16).ToBitmap(), AddProject, Console.SMS.ToString()),
                    new ToolStripMenuItem(null, new Icon(Properties.Resources.sega_genesis__model_2_, 16, 16).ToBitmap(), AddProject, Console.SMDGEN.ToString()),
                    new ToolStripSeparator(),
                },

                new ToolStripItem[]
                {
                    new ToolStripMenuItem(null, new Icon(Properties.Resources.snk_neo_geo_aes, 16, 16).ToBitmap(), AddProject, Console.NeoGeo.ToString()),
                    new ToolStripSeparator(),
                }
            };

            foreach (var section in items)
                foreach (ToolStripItem item in section)
                    item.Text = string.Format(Language.Get("ProjectType"), Language.Get($"Platform_{item.Name}"));

            NewProject.DropDownItems.AddRange(new ToolStripItem[]
                {
                    new ToolStripMenuItem(Language.Get("PlatformGroup_0"), null, items[0]),
                    new ToolStripMenuItem(Language.Get("PlatformGroup_1"), null, items[1]),
                    new ToolStripMenuItem(Language.Get("PlatformGroup_2"), null, items[2])
                });

            MenuItem_Settings.Text = Language.Get("Settings");
            foreach (ToolStripMenuItem section in MenuStrip.Items.OfType<ToolStripMenuItem>())
                foreach (ToolStripMenuItem item in section.DropDownItems.OfType<ToolStripMenuItem>())
                    item.Text = Language.Get(item.Name, this) != "undefined" ? Language.Get(item.Name, this) : item.Text;

            MenuItem_File.Text = Language.Get(MenuItem_File.Name, this);
            MenuItem_Project.Text = Language.Get(MenuItem_Project.Name, this);
            // MenuItem_Help.Text = Language.Get(MenuItem_Help.Name, this);
            OpenROM.Text = Language.Get(OpenROM.Name, this);
            OpenImage.Text = Language.Get(OpenImage.Name, this);
            OpenManual.Text = Language.Get(OpenManual.Name, this);
            UseLibRetro.Text = Language.Get(UseLibRetro.Name, this);
            ExportWAD.Text = Language.Get(ExportWAD.Name, this);
            CloseTab.Text = Language.Get(CloseTab.Name, this);
        }

        /// <summary>
        /// Changes language of this form and all tab pages
        /// </summary>
        private void RefreshForm()
        {
            AutoSetStrip();
            Language.AutoSetForm(this);

            SetTitle();
            BrowseROM.Title = OpenROM.Text;
            BrowseImage.Title = OpenImage.Text;
            SaveWAD.Title = ExportWAD.Text;

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

            Program.Handle = Handle;

            // Automatically set defined initial directory for save file dialog
            // ********
            SaveWAD.InitialDirectory = Paths.Out;

            // FriishProduce.Update.GetLatest();
        }

        public void SetTitle(bool isNull = false)
        {
            if (tabControl.SelectedForm == null || tabControl.TabPages.Count == 0 || isNull) Text = Language.Get("_AppTitle");
            else if (!string.IsNullOrWhiteSpace((tabControl.SelectedForm as InjectorForm).Text))
                Text = Language.Get("_AppTitle") + " - " + (tabControl.SelectedForm as InjectorForm).Text;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.UI_Language;

            s.ShowDialog(this);

            if (lang != Properties.Settings.Default.UI_Language) RefreshForm();
        }

        public void TabChanged(object sender, EventArgs e)
        {
            // Toggle visibility of Open ROM/Image buttons
            // ********
            if (sender != tabControl.TabPages[0]) OpenROM.Enabled = tabControl.TabPages.Count > 1;
            else OpenROM.Enabled = true;

            // Toggle visibility of Export WAD button
            // Toggle visibility of Download LibRetro data button
            // ********
            if (!OpenROM.Enabled)
            {
                ExportWAD.Enabled = false;
                UseLibRetro.Enabled = false;
                tabControl.Visible = false;
            }

            else ExportCheck(sender, e);

            CloseTab.Enabled = OpenManual.Enabled = OpenImage.Enabled = OpenROM.Enabled;

            SetTitle(!OpenROM.Enabled);

            // Context menu
            // ********
            if (tabControl.TabPages.Count >= 1)
            {
                if (sender == tabControl.TabPages[0]) UseLibRetro.Enabled = (tabControl.SelectedForm as InjectorForm).ROMLoaded;
            }
        }

        private void ExportCheck(object sender, EventArgs e) => ExportWAD.Enabled = ExportWAD.Enabled = (tabControl.SelectedForm as InjectorForm).ReadyToExport;

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            var collection = tabControl.TabPages;

            for (int i = 0; i < collection.Count; i++)
            {
                var tabPage = tabControl.TabPages[i];

                InjectorForm x = tabPage?.Form as InjectorForm;
                if (x?.Tag?.ToString() == "dirty")
                {
                    tabControl.TabPages[tabControl.TabPages.get_IndexOf(tabPage)].Select();
                    bool Cancelled = !x.CheckUnsaved();

                    if (Cancelled)
                    { e.Cancel = true; return; }
                }
            }

            foreach (MdiTabControl.TabPage tabPage in collection)
            {
                try
                {
                    (tabPage.Form as Form).Tag = null;
                }
                catch { }
            }
        }

        /// <summary>
        /// Adds a new project to the Main Form.
        /// </summary>
        private void AddProject(object sender, EventArgs e)
        {
            if (!Enum.TryParse((sender as ToolStripMenuItem).Name.ToString(), out Console console))
                return;

            InjectorForm Tab = new InjectorForm(console) { Parent = this };
            Tab.FormClosed += TabChanged;
            Tab.ExportCheck += ExportCheck;
            tabControl.TabBackHighColor = Tab.BackColor;
            tabControl.TabPages.Add(Tab);

            tabControl.Visible = true;

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
                UseLibRetro.Enabled = true;
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

        private void OpenManual_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.DoNotShow_000) MessageBox.Show(Language.Get("Message006"), 0);
            (tabControl.SelectedForm as InjectorForm).LoadManual(BrowseManual.ShowDialog() == DialogResult.OK ? BrowseManual.SelectedPath : null);
        }

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

        private void TabContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var page = (sender as ContextMenuStrip).SourceControl;
            var index = tabControl.TabPages.get_IndexOf(page as MdiTabControl.TabPage);
            if (index != -1) tabControl.TabPages[index].Select();
        }

        private void CloseTab_Click(object sender, EventArgs e) => (tabControl.SelectedForm as Form).Close();

        private void About_Click(object sender, EventArgs e) => new About().ShowDialog();

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();
    }
}
