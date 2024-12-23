using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class MainForm : Form
    {
        private readonly SettingsForm settings = new();

        #region //////////////////// Platforms ////////////////////
        public readonly IDictionary<Platform, Bitmap> Icons = new Dictionary<Platform, Bitmap>
        {
            { Platform.NES, new Icon(Properties.Resources.nintendo_nes, 16, 16).ToBitmap() },
            { Platform.SNES, new Icon(Properties.Resources.nintendo_super_nes, 16, 16).ToBitmap() },
            { Platform.N64, new Icon(Properties.Resources.nintendo_nintendo64, 16, 16).ToBitmap() },
            { Platform.SMS, new Icon(Properties.Resources.sega_master_system, 16, 16).ToBitmap() },
            { Platform.SMD, new Icon(Properties.Resources.sega_genesis, 16, 16).ToBitmap() },
            { Platform.PCE, new Icon(Properties.Resources.nec_turbografx_16, 16, 16).ToBitmap() },
            { Platform.NEO, new Icon(Properties.Resources.snk_neo_geo_aes, 16, 16).ToBitmap() },
            { Platform.MSX, Properties.Resources.msx },
            { Platform.PSX, new Icon(Properties.Resources.sony_playstation, 16, 16).ToBitmap() },
            { Platform.Flash, Properties.Resources.flash },
            { Platform.RPGM, new Icon(Properties.Resources.rpg2003, 16, 16).ToBitmap() }
        };

        private static readonly string[] platformsList = new string[]
        {
            Platform.NES.ToString(),
            Platform.SNES.ToString(),
            Platform.N64.ToString(),
            null,
            Platform.SMS.ToString(),
            Platform.SMD.ToString(),
            null,
            Platform.PCE.ToString(),
            null,
            Platform.NEO.ToString(),
            null,
            Platform.MSX.ToString(),
            null,
            Platform.Flash.ToString(),
            null,
            Platform.PSX.ToString(),
            null,
            Platform.RPGM.ToString(),
        };

        private MenuItem[] platformsMenuItemList()
        {
            var list = new List<MenuItem>();
            foreach (var platform in platformsList)
            {
                var item = !string.IsNullOrWhiteSpace(platform) ? new MenuItem(platform, addProject) : new MenuItem("-");
                if (!string.IsNullOrWhiteSpace(platform))
                {
                    item.Name = platform;
                    item.Text = string.Format(Program.Lang.String("project_type", Name), Program.Lang.Console((Platform)Enum.Parse(typeof(Platform), platform)));
                }
                list.Add(item);
            }

            return list.ToArray();
        }

        private ToolStripItem[] platformsStripItemList()
        {
            var list = new List<ToolStripItem>();
            foreach (var platform in platformsList)
            {
                if (!string.IsNullOrWhiteSpace(platform))
                {
                    Platform convertedPlatform = (Platform)Enum.Parse(typeof(Platform), platform);

                    list.Add(new ToolStripMenuItem
                    (
                        string.Format(Program.Lang.String("project_type", Name), Program.Lang.Console(convertedPlatform)),
                        Icons[convertedPlatform],
                        addProject,
                        platform + "0"
                    ));
                }

                else list.Add(new ToolStripSeparator());
            }

            return list.ToArray();
        }
        #endregion

        /// <summary>
        /// Changes language of this form and all tab pages
        /// </summary>
        private void RefreshForm()
        {
            new_project.MenuItems.Clear();
            new_project.MenuItems.AddRange(platformsMenuItemList());
            toolbarNewProject.DropDownItems.Clear();
            toolbarNewProject.DropDownItems.AddRange(platformsStripItemList());

            #region Localization
            Program.Lang.Control(this);
            Text = Program.Lang.ApplicationTitle;
            about.Text = string.Format(Program.Lang.String("about_app"), Program.Lang.ApplicationTitle);

            foreach (MenuItem section in mainMenu.MenuItems)
                foreach (MenuItem item in section.MenuItems)
                {
                    if (Program.Lang.StringCheck(item.Tag?.ToString().ToLower(), Name)) item.Text = Program.Lang.String(item.Tag?.ToString().ToLower(), Name);
                    foreach (MenuItem subitem in item.MenuItems)
                        if (Program.Lang.StringCheck(subitem.Tag?.ToString().ToLower(), Name)) subitem.Text = Program.Lang.String(subitem.Tag?.ToString().ToLower(), Name);
                }

            menuItem1.Text = Program.Lang.String(menuItem1.Tag.ToString(), Name);
            menuItem2.Text = Program.Lang.String(menuItem2.Tag.ToString(), Name);
            menuItem3.Text = Program.Lang.String(menuItem3.Tag.ToString(), Name);
            menuItem4.Text = Program.Lang.String(menuItem4.Tag.ToString(), Name);
            if (import_game_file.Tag == null) import_game_file.Tag = import_game_file.Text;
            import_game_file.Text = string.Format(Program.Lang.String(import_game_file.Tag.ToString(), Name), Program.Lang.String("rom_label1", "projectform"));

            toolbarNewProject.Text = new Regex(@"\(.*\)").Replace(new_project.Text, "");
            toolbarOpenProject.Text = open_project.Text;
            toolbarSave.Text = save_project.Text;
            toolbarSaveAs.Text = save_project_as.Text;
            toolbarExport.Text = export.Text;
            toolbarCloseProject.Text = close_project.Text;
            toolbarGameScan.Text = game_scan.Text;
            toolbarPreferences.Text = preferences.Text = Program.Lang.String("preferences");

            SaveProject.Title = save_project_as.Text.Replace("&", "");
            SaveWAD.Title = export.Text.Replace("&", "");

            try { BrowseProject.Filter = SaveProject.Filter = Program.Lang.String("filter.project"); }
            catch { MessageBox.Show("Warning!\nThe language strings have not been loaded correctly.\n\nSeveral items may show up as 'undefined'.\n\nOther exceptions related to strings or filters can also occur!", MessageBox.Buttons.Ok, MessageBox.Icons.Warning, false); }
            #endregion

            if (Program.DebugMode)
            {
                Text += " [Running in debug mode]";
                language_file_editor.Visible = true;
            }

            foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
                if (tabPage.Form.GetType() == typeof(ProjectForm))
                    (tabPage.Form as ProjectForm).RefreshForm();
        }

        public MainForm()
        {
            InitializeComponent();
            Program.Handle = Handle;
        }

        private void MainForm_Loading(object sender, EventArgs e)
        {
            RefreshForm();

            #region Set size of window
            mainPanel.Dock = DockStyle.None;

            int w = 16;
            int h = mainPanel.Location.Y + tabControl.TabHeight + tabControl.TabTop;
            using (var pF = new ProjectForm(0))
            {
                ClientSize = new Size(pF.Width, pF.Height + h);
                MinimumSize = MaximumSize = Size;
                tabControl.TabBackLowColor = pF.BackColor;
            }

            mainPanel.Size = tabControl.Size = new Size(Width - w, Height - h);
            CenterToScreen();

            // Set logo position (425, 163)
            // ********
            if (Logo.Location.X == 0 || Logo.Location.Y == 0) Logo.Location = new Point(mainPanel.Bounds.Width / 2 - (Logo.Width / 2), mainPanel.Bounds.Height / 2 - (Logo.Height / 2) - mainPanel.Location.Y);
            #endregion

            // Automatically set defined initial directory for save file dialog
            // ********
            // SaveWAD.InitialDirectory = Paths.Out;

            if (Properties.Settings.Default.auto_update) { _ = Updater.GetLatest(); }
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (!Enabled)
            {
                System.Media.SystemSounds.Beep.Play();
                e.Cancel = true;
                return;
            }

            var collection = tabControl.TabPages;

            for (int i = 0; i < collection.Count; i++)
            {
                var p = tabControl.TabPages[i];
                var f = p.Form as ProjectForm;

                if (f.IsModified)
                {
                    tabControl.TabPages[tabControl.TabPages.get_IndexOf(p)].Select();

                    if (!f.CheckUnsaved())
                        e.Cancel = true;
                }
            }
        }

        private void Settings_Click(object sender, EventArgs e) => settings.ShowDialog(this);

        public void TabChanged(object sender, EventArgs e)
        {
            // Check if any tabs exist
            // ********
            bool hasTabs = (tabControl.TabPages.Count >= 1 && sender == tabControl.TabPages[0]) || (tabControl.TabPages.Count > 1 || e.GetType() != typeof(FormClosedEventArgs));

            // Toggle visibility of Export WAD button
            // Toggle visibility of Download LibRetro data button
            // ********
            if (!hasTabs)
            {
                game_scan.Enabled = false;
                save_project.Enabled = save_project_as.Enabled = false;
                export.Enabled = false;
                toolbarImportGameFile.Image = Properties.Resources.page_white_cd;
                import_game_file.Text = string.Format(Program.Lang.String(import_game_file.Tag.ToString(), Name), Program.Lang.String("rom_label1", "projectform"));

                tabControl.Visible = false;
            }

            else
            {
                game_scan.Enabled = (tabControl.SelectedForm as ProjectForm).ToolbarButtons[0];
                save_project.Enabled = save_project_as.Enabled = (tabControl.SelectedForm as ProjectForm).IsModified;
                export.Enabled = (tabControl.SelectedForm as ProjectForm).IsExportable;
                toolbarImportGameFile.Image = (tabControl.SelectedForm as ProjectForm).FileTypeImage;
                import_game_file.Text = string.Format(Program.Lang.String(import_game_file.Tag.ToString(), Name), (tabControl.SelectedForm as ProjectForm).FileTypeName);
            }

            import_game_file.Enabled = close_project.Enabled = hasTabs;

            // Sync toolbar buttons
            // ********
            toolbarImportGameFile.Text      = import_game_file.Text;
            toolbarImportGameFile.Enabled   = import_game_file.Enabled;
            toolbarSave.Enabled             = save_project.Enabled;
            toolbarSaveAs.Enabled           = save_project_as.Enabled;
            toolbarCloseProject.Enabled     = close_project.Enabled;
            toolbarGameScan.Enabled = game_scan.Enabled;
            toolbarExport.Enabled           = export.Enabled;
        }

        /// <summary>
        /// Adds a new project to the Main Form.
        /// </summary>
        private void addProject(object sender, EventArgs e)
        {
            string name = sender.GetType() == typeof(MenuItem) ? (sender as MenuItem).Name.ToString() : (sender as ToolStripItem).Name.ToString();

            try
            {
                addTab((Platform)Enum.Parse(typeof(Platform), name));
            }

            catch
            {
                name = name.Substring(0, name.Length - 1);

                if (Enum.TryParse(name, out Platform console))
                    addTab(console);
            }
        }

        private void addTab(Platform platform, Project x = null)
        {
            ProjectForm p = new(platform, null, x) { Font = Font };
            p.FormClosed += TabChanged;
            tabControl.TabPages.Add(p);

            tabControl.BringToFront();
            tabControl.Visible = true;

            // BrowseROMDialog(p);
        }

        private void OpenROM_Click(object sender, EventArgs e) => BrowseROMDialog();

        private void BrowseROMDialog()
        {
            if (tabControl.SelectedForm != null)
            {
                var p = tabControl.SelectedForm as ProjectForm;
                p.BrowseROMDialog(import_game_file.Text);
            }
        }

        private void GameScan_Click(object sender, EventArgs e) => (tabControl.SelectedForm as ProjectForm)?.GameScan(false);

        private void OpenImage_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedForm != null)
            {
                var p = tabControl.SelectedForm as ProjectForm;
                p.BrowseImageDialog();
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

        private void CloseTab_Click(object sender, EventArgs e) { var tab = tabControl.SelectedForm as Form; tab?.Close(); }

        private void About_Click(object sender, EventArgs e) { using var about = new About() { Font = Font }; about.ShowDialog(); }

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedForm is not ProjectForm currentForm) return;

            SaveWAD.FileName = currentForm?.GetName(true);
            SaveWAD.Filter = currentForm.IsForwarder ? Program.Lang.String("filter.zip") : Program.Lang.String("filter.wad");

            if (SaveWAD.ShowDialog() == DialogResult.OK)
                currentForm?.SaveToWAD(SaveWAD.FileName);
        }

        private void SaveAs_Click(object sender, EventArgs e) => SaveAs_Trigger();

        public bool SaveAs_Trigger()
        {
            try
            {
                if (tabControl.SelectedForm is not ProjectForm currentForm) return false;

                SaveProject.FileName = Path.GetFileNameWithoutExtension(currentForm.ProjectPath) ?? currentForm?.GetName(false) ?? currentForm.Text;
                foreach (var item in new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' })
                    SaveProject.FileName = SaveProject.FileName.Replace(item, '_');

                if (SaveProject.ShowDialog() == DialogResult.OK)
                {
                    currentForm.SaveProject(SaveProject.FileName);
                    return true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Could not save!", ex.Message, MessageBox.Buttons.Ok, MessageBox.Icons.Error);
            }

            return false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedForm is not ProjectForm currentForm) return;

            if (File.Exists(currentForm.ProjectPath))
            {
                try { currentForm.SaveProject(currentForm.ProjectPath); }
                catch (Exception ex) { MessageBox.Show("Could not save!", ex.Message, MessageBox.Buttons.Ok, MessageBox.Icons.Error); }
            }

            else SaveAs_Trigger();
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (BrowseProject.ShowDialog() == DialogResult.OK)
            {
                foreach (var projectFile in BrowseProject.FileNames)
                {
                    var project = new Project();

                    try
                    {
                        using Stream stream = File.Open(projectFile, FileMode.Open);
                        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        project = (Project)binaryFormatter.Deserialize(stream);

                        addTab(project.Platform, project);
                    }

                    catch
                    {
                        MessageBox.Show("Not a valid project file!", MessageBox.Buttons.Ok, MessageBox.Icons.Error);
                    }
                }
            }
        }

        private async void updateCheck()
        {
            bool updated = await Updater.GetLatest();
            if (updated) MessageBox.Show(Program.Lang.Msg(9), MessageBox.Buttons.Ok, MessageBox.Icons.Information);
            check_for_updates.Enabled = !auto_update.Checked || !updated;
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (sender == check_for_updates)
            {
                updateCheck();
            }

            else if (sender == auto_update)
            {
                auto_update.Checked = !auto_update.Checked;
                check_for_updates.Enabled = !Updater.IsLatest;

                Properties.Settings.Default.auto_update = auto_update.Checked;
                Properties.Settings.Default.Save();
            }

            /* 
            check_for_updates.Enabled = !Default.auto_update_check || !Program.IsUpdated; */
        }

        private void LanguageFileEditor(object sender, EventArgs e)
        {
            new LanguageEditor().ShowDialog();
        }
    }
}
