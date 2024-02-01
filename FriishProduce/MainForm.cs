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
        private readonly Language Strings = Program.Language;
        internal LibRetroDB LibRetro { get; set; }

        private void LocalizeMe(RibbonButton c)
        {
            if (c.Tag != null) c.Text = Strings.Get(c.Tag.ToString());
            else c.Text = Strings.Get(c.Name);
        }

        private void LocalizeMe(RibbonPanel c)
        {
            if (c.Tag != null) c.Text = Strings.Get(c.Tag.ToString());
            else c.Text = Strings.Get(c.Name);
        }

        private void LocalizeMe(RibbonTab c)
        {
            if (c.Tag != null) c.Text = Strings.Get(c.Tag.ToString());
            else c.Text = Strings.Get(c.Name);
        }


        public MainForm()
        {
            InitializeComponent();
            Strings.Localize(this);

            LocalizeMe(ribbonTab_Home);
            LocalizeMe(NewProject);
            LocalizeMe(MenuItem_Settings);
            LocalizeMe(MenuItem_About);
            LocalizeMe(MenuItem_Exit);
            LocalizeMe(ribbonPanel_Import);
            LocalizeMe(OpenROM);
            LocalizeMe(OpenImage);
            LocalizeMe(UseLibRetro);
            LocalizeMe(ribbonPanel_Export);
            LocalizeMe(ExportWAD);

            ribbon1.OrbText = Strings.Get("r000");
            foreach (RibbonButton item in NewProject.DropDownItems.OfType<RibbonButton>())
            {
                item.Text = string.Format(Strings.Get("r013"), Strings.Get(item.Tag.ToString()));
                item.Click += CreateProject_Click;
            }

            Strip_OpenROM.Image = OpenROM.SmallImage;
            Strip_OpenImage.Image = OpenImage.SmallImage;
            Strip_OpenROM.Text = OpenROM.Text;
            BrowseImage.Title = Strip_OpenImage.Text = OpenImage.Text;
            SaveWAD.Title = Strip_ExportWAD.Text = ExportWAD.Text;

            BrowseImage.Filter = Strings.Get("f_img");
            SaveWAD.Filter = Strings.Get("f_wad");
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
                if (MessageBox.Show(string.Format(Strings.Get("m002"), Text), Strings.Get("g000"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
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
            if (!Enum.TryParse((sender as RibbonButton).Tag.ToString(), out console)) return;

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
                    BrowseROM.Filter = Strings.Get("f_iso") + "|" + Strings.Get("f_zip") + Strings.Get("f_all");
                    break;
                case Console.NES:
                    BrowseROM.Filter = Strings.Get("f_nes");
                    break;
                case Console.SNES:
                    BrowseROM.Filter = Strings.Get("f_sfc");
                    break;
                case Console.N64:
                    BrowseROM.Filter = Strings.Get("f_n64");
                    break;
                case Console.SMS:
                    BrowseROM.Filter = Strings.Get("f_sms");
                    break;
                case Console.SMDGEN:
                    BrowseROM.Filter = Strings.Get("f_smd");
                    break;
                case Console.PCE:
                    BrowseROM.Filter = Strings.Get("f_pce");
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
