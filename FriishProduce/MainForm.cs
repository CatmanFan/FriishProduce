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
            { Console.RPGM, new Icon(Properties.Resources.rpg2003, 16, 16).ToBitmap() }
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
                         // new ToolStripMenuItem(null, new Icon(Properties.Resources.sony_playstation, 16, 16).ToBitmap(), AddProject, Console.PSX.ToString()),
                         // new ToolStripSeparator(),
                         // new ToolStripMenuItem(null, Icons[Console.RPGM], AddProject, Console.RPGM.ToString())
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

            menu_new_project.DropDownItems.Clear();
            menu_new_project.DropDownItems.AddRange(ConsolesList());
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
            Program.Lang.Control(this);
            menu_file.Text = Program.Lang.String("file", Name);
            menu_project.Text = Program.Lang.String("project", Name);
            menu_help.Text = Program.Lang.String("help", Name);
            menu_about_app.Text = string.Format(Program.Lang.String("about_app"), Program.Lang.ApplicationTitle);
            Text = Program.Lang.ApplicationTitle;

            ToolStrip_NewProject.Text = menu_new_project.Text;
            ToolStrip_OpenProject.Text = menu_open_project.Text;
            ToolStrip_SaveAs.Text = menu_save_project_as.Text;
            ToolStrip_ExportWAD.Text = menu_save_as_wad.Text;
            ToolStrip_OpenROM.Text = menu_open_gamefile.Text;
            ToolStrip_OpenImage.Text = menu_open_image.Text;
            ToolStrip_CloseTab.Text = menu_close_project.Text;
            ToolStrip_UseLibRetro.Text = menu_retrieve_gamedata_online.Text;
            ToolStrip_Settings.Text = menu_settings.Text;
            BrowseROM.Title = menu_open_gamefile.Text;
            BrowseImage.Title = menu_open_image.Text;
            SaveProject.Title = menu_save_project_as.Text;
            SaveWAD.Title = menu_save_as_wad.Text;

            try
            {
                BrowseImage.Filter = Program.Lang.String("filter.img");
                BrowseProject.Filter = SaveProject.Filter = Program.Lang.String("filter.project");
                SaveWAD.Filter = Program.Lang.String("filter.wad");
            }
            catch
            {
                MessageBox.Show("Warning!\nThe language strings have not been loaded correctly.\n\nSeveral items may show up as 'undefined'.\n\nOther exceptions related to strings or filters can also occur!", MessageBox.Buttons.Ok, TaskDialogIcon.Warning, false);
            }
            #endregion

            foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Form.GetType() == typeof(ProjectForm))
                    (tabPage.Form as ProjectForm).RefreshForm();
            }

            MenuStrip.Font = ToolStrip.Font; // = Font;
            using (var pj = new ProjectForm(0))
                Size = new Size(pj.Width + 16, pj.Height + MenuStrip.Height + ToolStrip.Height + tabControl.TabHeight + 40);
            pictureBox1.Location = new Point((MainPanel.Width / 2) - (pictureBox1.Width / 2), (MainPanel.Height / 2) - (pictureBox1.Height / 2));

            if (MaximumSize.IsEmpty)
            {
                MaximumSize = new Size
                (
                    MainPanel.Width + (ToolStrip.Dock == DockStyle.Right || ToolStrip.Dock == DockStyle.Left ? ToolStrip.Width - 8 : 16),
                    MainPanel.Location.Y + MainPanel.Height + excessHeight
                );
                MinimumSize = Size = MaximumSize;
            }

            tabControl.Size = new Size(MainPanel.Width, 1000);
        }

        private int excessHeight = 40;

        public MainForm()
        {
            InitializeComponent();
            Program.Handle = Handle;

            MaximumSize = new Size(0,0);
            tabControl.Location = MainPanel.Location;
            MainPanel.Height += excessHeight + 5;
            RefreshForm();
            CenterToScreen();

            // Automatically set defined initial directory for save file dialog
            // ********
            // SaveWAD.InitialDirectory = Paths.Out;

            Updater.GetLatest();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.language;

            s.Font = Font;
            s.ShowDialog(this);

            if (lang != Properties.Settings.Default.language) RefreshForm();
        }

        public void TabChanged(object sender, EventArgs e)
        {
            // Toggle visibility of Open ROM/Image buttons
            // ********
            if (sender != tabControl.TabPages[0]) menu_open_gamefile.Enabled = tabControl.TabPages.Count > 1;
            else menu_open_gamefile.Enabled = true;

            // Toggle visibility of Export WAD button
            // Toggle visibility of Download LibRetro data button
            // ********
            if (!menu_open_gamefile.Enabled)
            {
                menu_save_project_as.Enabled = false;
                ToolStrip_ExportWAD.Enabled = menu_save_as_wad.Enabled = false;
                menu_retrieve_gamedata_online.Enabled = false;
                tabControl.Visible = false;
                MainPanel.Visible = true;
            }

            else ExportCheck(sender, e);

            ToolStrip_CloseTab.Enabled = menu_close_project.Enabled
            = ToolStrip_OpenImage.Enabled = menu_open_image.Enabled
            = ToolStrip_OpenROM.Enabled = menu_open_gamefile.Enabled;

            // Context menu
            // ********
            if (tabControl.TabPages.Count >= 1)
                if (sender == tabControl.TabPages[0])
                {
                    menu_retrieve_gamedata_online.Enabled = (tabControl.SelectedForm as ProjectForm).CheckToolStripButtons()[0];
                    menu_save_project_as.Enabled = (tabControl.SelectedForm as ProjectForm).Tag?.ToString().ToLower() == "dirty";
                }
            
            ToolStrip_SaveAs.Enabled = menu_save_project_as.Enabled;
            ToolStrip_UseLibRetro.Enabled = menu_retrieve_gamedata_online.Enabled;
        }

        private void ExportCheck(object sender, EventArgs e)
        {
            ToolStrip_ExportWAD.Enabled = menu_save_as_wad.Enabled = (tabControl.SelectedForm as ProjectForm).ReadyToExport;
            ToolStrip_UseLibRetro.Enabled = menu_retrieve_gamedata_online.Enabled = (tabControl.SelectedForm as ProjectForm).CheckToolStripButtons()[0];
            ToolStrip_SaveAs.Enabled = menu_save_project_as.Enabled = (tabControl.SelectedForm as ProjectForm).Tag?.ToString().ToLower() == "dirty";
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

        private ProjectForm AddTab(Console console, Project x = null)
        {
            ProjectForm Tab = x == null ? new ProjectForm(console) : new ProjectForm(x);
            Tab.Font = Font;
            Tab.Parent = this;
            Tab.FormClosed += TabChanged;
            Tab.ExportCheck += ExportCheck;
            tabControl.TabBackHighColor = Tab.BackColor;
            tabControl.TabPages.Add(Tab);

            tabControl.Visible = true;
            MainPanel.Visible = false;

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

                case Console.RPGM:
                    BrowseROM.Filter = Program.Lang.String("filter.rpgm");
                    break;
            }

            if (BrowseROM.ShowDialog() == DialogResult.OK)
            {
                ToolStrip_UseLibRetro.Enabled = menu_retrieve_gamedata_online.Enabled = currentForm.CheckToolStripButtons()[0];

                currentForm.LoadROM(Properties.Settings.Default.auto_retrieve_game_data);
            }
        }

        private void UseLibRetro_Click(object sender, EventArgs e) => (tabControl.SelectedForm as ProjectForm).LoadGameData();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            var currentForm = tabControl.SelectedForm as ProjectForm;

            SaveWAD.FileName = currentForm.GetName();
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

        private void About_Click(object sender, EventArgs e) { using (var about = new About() { Font = Font }) about.ShowDialog(); }

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
                        var project = (Project)binaryFormatter.Deserialize(stream);
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
