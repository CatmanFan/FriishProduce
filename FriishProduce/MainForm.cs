using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class MainForm : Form
    {
        private readonly SettingsForm s = new SettingsForm();

        internal LibRetroDB LibRetro { get; set; }

        public readonly IDictionary<Console, Bitmap> Icons = new Dictionary<Console, Bitmap>
        {
            { Console.NES, new Icon(Properties.Resources.nintendo_nes, 16, 16).ToBitmap() },
            { Console.SNES, new Icon(Properties.Resources.nintendo_super_nes, 16, 16).ToBitmap() },
            { Console.N64, new Icon(Properties.Resources.nintendo_nintendo64, 16, 16).ToBitmap() },
            { Console.SMS, new Icon(Properties.Resources.sega_master_system, 16, 16).ToBitmap() },
            { Console.SMD, new Icon(Properties.Resources.sega_genesis, 16, 16).ToBitmap() },
            { Console.PCE, new Icon(Properties.Resources.nec_turbografx_16, 16, 16).ToBitmap() },
            { Console.NEO, new Icon(Properties.Resources.snk_neo_geo_aes, 16, 16).ToBitmap() },
            { Console.MSX, Properties.Resources.msx },
            { Console.Flash, Properties.Resources.flash },
        };

        private ToolStripItem[] ConsolesList()
        {
            var list = new ToolStripItem[]
                {
                    /*new ToolStripMenuItem(
                        Language.Get("Group0", "Platforms"), null,
                        new ToolStripItem[]
                        {*/
                            new ToolStripMenuItem(null, Icons[Console.NES], AddProject, Console.NES.ToString()),
                            new ToolStripMenuItem(null, Icons[Console.SNES], AddProject, Console.SNES.ToString()),
                            new ToolStripMenuItem(null, Icons[Console.N64], AddProject, Console.N64.ToString()),
                            new ToolStripSeparator(),
                        /*}),

                    new ToolStripMenuItem(
                        Language.Get("Group1", "Platforms"), null,
                        new ToolStripItem[]
                        {*/
                            new ToolStripMenuItem(null, Icons[Console.SMS], AddProject, Console.SMS.ToString()),
                            new ToolStripMenuItem(null, Icons[Console.SMD], AddProject, Console.SMD.ToString()),
                            new ToolStripSeparator(),
                        /*}),

                    new ToolStripMenuItem(
                        Language.Get("Other"), null,
                        new ToolStripItem[]
                        {*/
                            new ToolStripMenuItem(null, Icons[Console.PCE], AddProject, Console.PCE.ToString()),
                            new ToolStripSeparator(),
                            new ToolStripMenuItem(null, Icons[Console.NEO], AddProject, Console.NEO.ToString()),
                            new ToolStripSeparator(),
                            new ToolStripMenuItem(null, Icons[Console.MSX], AddProject, Console.MSX.ToString()),
                            new ToolStripSeparator(),
                            new ToolStripMenuItem(null, Icons[Console.Flash], AddProject, Console.Flash.ToString()),
                         // new ToolStripSeparator(),
                         // new ToolStripMenuItem(null, new Icon(Properties.Resources.sony_playstation, 16, 16).ToBitmap(), AddProject, Console.PSX.ToString())
                        //})
                };

            /* foreach (ToolStripMenuItem section in Consoles.OfType<ToolStripMenuItem>())
                foreach (ToolStripMenuItem item in section.DropDownItems.OfType<ToolStripMenuItem>())
                    item.Text = string.Format(Language.Get("ProjectType"), Language.Get(item.Name, "Platforms")); */

            foreach (ToolStripMenuItem item in list.OfType<ToolStripMenuItem>())
                item.Text = string.Format(Program.Lang.String("project_type", Name), Program.Lang.Console((Console)Enum.Parse(typeof(Console), item.Name)));

            return list;
        }

        private void AutoSetStrip()
        {
            foreach (ToolStripMenuItem section in MenuStrip.Items.OfType<ToolStripMenuItem>())
                foreach (ToolStripItem item in section.DropDownItems.OfType<ToolStripItem>())
                    if (Program.Lang.StringCheck(item.Tag?.ToString().ToLower(), Name)) item.Text = Program.Lang.String(item.Tag?.ToString().ToLower(), Name);

            NewProject.DropDownItems.Clear();
            NewProject.DropDownItems.AddRange(ConsolesList());
            ToolStrip_NewProject.DropDownItems.Clear();
            ToolStrip_NewProject.DropDownItems.AddRange(ConsolesList());
        }

        /// <summary>
        /// Changes language of this form and all tab pages
        /// </summary>
        private void RefreshForm()
        {
            AutoSetStrip();

            #region Localization
            Text = Program.Lang.ApplicationTitle;

            MenuItem_File.Text = Program.Lang.String("file", Name);
            MenuItem_Project.Text = Program.Lang.String("project", Name);
            MenuItem_Help.Text = Program.Lang.String("help", Name);
            ToolStrip_NewProject.Text = NewProject.Text = Program.Lang.String("new_project", Name);
            ToolStrip_Tutorial.Text = MenuItem_Tutorial.Text = Program.Lang.String("tutorial", Name);
            ToolStrip_Tutorial.Image = MenuItem_Tutorial.Image;
            ToolStrip_Settings.Text = MenuItem_Settings.Text = Program.Lang.String("settings");
            ToolStrip_OpenROM.Text = OpenROM.Text = Program.Lang.String("open_gamefile", Name);
            ToolStrip_OpenImage.Text = OpenImage.Text = Program.Lang.String("open_image", Name);
            ToolStrip_UseLibRetro.Text = UseLibRetro.Text = Program.Lang.String("retrieve_gamedata_online", Name);
            ToolStrip_ExportWAD.Text = ExportWAD.Text = Program.Lang.String("save_as_wad", Name);
            ToolStrip_CloseTab.Text = CloseTab.Text = Program.Lang.String("b_close");
            ToolStrip_SaveAs.Text = SaveAs.Text;

            BrowseROM.Title = OpenROM.Text;
            BrowseImage.Title = OpenImage.Text;
            SaveProject.Title = SaveAs.Text;
            SaveWAD.Title = ExportWAD.Text;

            var test = Program.Lang.String("filter.img");
            BrowseImage.Filter = Program.Lang.String("filter.img");
            BrowseProject.Filter = SaveProject.Filter = Program.Lang.String("filter.project");
            SaveWAD.Filter = Program.Lang.String("filter.wad");

            Welcome_DoNotShow.Text = Program.Lang.String("do_not_show");
            #endregion


            foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Form.GetType() == typeof(ProjectForm))
                    (tabPage.Form as ProjectForm).RefreshForm();
            }
        }

        public MainForm()
        {
            InitializeComponent();
            RefreshForm();

            Welcome_DoNotShow.Visible = PointToTutorial.Visible = Welcome.Visible = !Properties.Settings.Default.DoNotShow_Welcome;

            Program.Handle = Handle;
            tabControl.Location = MainPanel.Location;
            tabControl.Size = new Size(MainPanel.Width, 1000);
            Size = new Size
            (
                MainPanel.Width + (ToolStrip.Dock == DockStyle.Right || ToolStrip.Dock == DockStyle.Left ? ToolStrip.Width - 8 : 16),
                MainPanel.Location.Y + MainPanel.Height + 38
            );
            CenterToScreen();

            // Automatically set defined initial directory for save file dialog
            // ********
            // SaveWAD.InitialDirectory = Paths.Out;

            Updater.GetLatest();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Language;

            s.ShowDialog(this);

            if (lang != Properties.Settings.Default.Language) RefreshForm();
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
                SaveAs.Enabled = false;
                ToolStrip_ExportWAD.Enabled = ExportWAD.Enabled = false;
                UseLibRetro.Enabled = false;
                tabControl.Visible = false;
                MainPanel.Visible = true;
            }

            else ExportCheck(sender, e);

            ToolStrip_CloseTab.Enabled = CloseTab.Enabled
            = ToolStrip_OpenImage.Enabled = OpenImage.Enabled
            = ToolStrip_OpenROM.Enabled = OpenROM.Enabled;

            // Context menu
            // ********
            if (tabControl.TabPages.Count >= 1)
                if (sender == tabControl.TabPages[0])
                {
                    UseLibRetro.Enabled = (tabControl.SelectedForm as ProjectForm).CheckToolStripButtons()[0];
                    SaveAs.Enabled = (tabControl.SelectedForm as ProjectForm).Tag?.ToString().ToLower() == "dirty";
                }
            
            ToolStrip_SaveAs.Enabled = SaveAs.Enabled;
            ToolStrip_UseLibRetro.Enabled = UseLibRetro.Enabled;
        }

        private void ExportCheck(object sender, EventArgs e)
        {
            ToolStrip_ExportWAD.Enabled = ExportWAD.Enabled = (tabControl.SelectedForm as ProjectForm).ReadyToExport;
            ToolStrip_UseLibRetro.Enabled = UseLibRetro.Enabled = (tabControl.SelectedForm as ProjectForm).CheckToolStripButtons()[0];
            ToolStrip_SaveAs.Enabled = SaveAs.Enabled = (tabControl.SelectedForm as ProjectForm).Tag?.ToString().ToLower() == "dirty";
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            var collection = tabControl.TabPages;

            for (int i = 0; i < collection.Count; i++)
            {
                var tabPage = tabControl.TabPages[i];

                ProjectForm x = tabPage?.Form as ProjectForm;
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
            var source = sender as ToolStripMenuItem;

            if (Enum.TryParse(source.Name.ToString(), out Console console))
            {
                var Tab = AddTab(console);
                // BrowseROMDialog(console, Tab);
            }
        }

        private ProjectForm AddTab(Console console, ProjectType x = null)
        {
            ProjectForm Tab = x == null ? new ProjectForm(console) : new ProjectForm(x);
            Tab.Parent = this;
            Tab.FormClosed += TabChanged;
            Tab.ExportCheck += ExportCheck;
            tabControl.TabBackHighColor = Tab.BackColor;
            tabControl.TabPages.Add(Tab);

            tabControl.Visible = true;
            Welcome_DoNotShow.Visible = PointToTutorial.Visible = Welcome.Visible = MainPanel.Visible = false;

            return Tab;
        }

        private void OpenROM_Click(object sender, EventArgs e) => BrowseROMDialog((tabControl.SelectedForm as ProjectForm).Console, tabControl.SelectedForm as ProjectForm);

        private void BrowseROMDialog(Console c, ProjectForm currentForm)
        {
            switch (c)
            {
                default:
                    BrowseROM.Filter = Program.Lang.String("filter.disc") + "|" + Program.Lang.String("filter.zip") + Program.Lang.String("filter");
                    break;

                case Console.NES:
                case Console.SNES:
                case Console.N64:
                case Console.SMS:
                case Console.SMD:
                case Console.PCE:
                case Console.C64:
                case Console.MSX:
                    BrowseROM.Filter = Program.Lang.String($"filter.rom_{c.ToString().ToLower()}");
                    break;

                case Console.NEO:
                    BrowseROM.Filter = Program.Lang.String("filter.zip");
                    break;

                case Console.Flash:
                    BrowseROM.Filter = Program.Lang.String("filter.swf");
                    break;
            }

            if (BrowseROM.ShowDialog() == DialogResult.OK)
            {
                ToolStrip_UseLibRetro.Enabled = UseLibRetro.Enabled = currentForm.CheckToolStripButtons()[0];

                currentForm.LoadROM(Properties.Settings.Default.AutoLibRetro);
            }
        }

        private void UseLibRetro_Click(object sender, EventArgs e) => (tabControl.SelectedForm as ProjectForm).LoadLibRetroData();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            var currentForm = tabControl.SelectedForm as ProjectForm;

            SaveWAD.FileName = string.IsNullOrWhiteSpace(SaveWAD.FileName) ? currentForm.GetName() : Path.GetFileNameWithoutExtension(SaveWAD.FileName);
            if (SaveWAD.ShowDialog() == DialogResult.OK) currentForm.SaveToWAD();
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            if (BrowseImage.ShowDialog() == DialogResult.OK)
            {
                var currentForm = tabControl.SelectedForm as ProjectForm;
                currentForm.LoadImage(BrowseImage.FileName);
            }
        }

        private void Tutorial_Click(object sender, EventArgs e)
        {
            var tut = new Tutorial() { Text = MenuItem_Tutorial.Text };
            tut.ShowDialog();
            tut.Dispose();
        }

        private void Welcome_DoNotShow_Click(object sender, EventArgs e)
        {
            Welcome_DoNotShow.Visible = PointToTutorial.Visible = Welcome.Visible = false;
            Properties.Settings.Default.DoNotShow_Welcome = true;
            Properties.Settings.Default.Save();
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

        private void SaveAs_Click(object sender, EventArgs e) => SaveAs_Trigger();

        public bool SaveAs_Trigger()
        {
            try
            {
                var currentForm = tabControl.SelectedForm as ProjectForm;
                if (currentForm == null) throw new Exception("No project found!");

                SaveProject.FileName = currentForm.Text;
                foreach (var item in new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' })
                    SaveProject.FileName = SaveProject.FileName.Replace(item, '_');

                if (SaveProject.ShowDialog() == DialogResult.OK)
                {
                    currentForm.Save();
                    return true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Could not save!", ex.Message, MessageBox.Buttons.Ok, TaskDialogIcon.Error);
            }

            return false;
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (BrowseProject.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (Stream stream = File.Open(BrowseProject.FileName, FileMode.Open))
                    {
                        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        var project = (ProjectType)binaryFormatter.Deserialize(stream);
                        AddTab(project.Console, project);
                    }
                }

                catch
                {
                    MessageBox.Show("Not a valid project file!", MessageBox.Buttons.Ok, TaskDialogIcon.Error);
                }
            }
        }
    }
}
