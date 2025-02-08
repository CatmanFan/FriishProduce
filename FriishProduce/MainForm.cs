using libWiiSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class MainForm : Form
    {
        private readonly string[] files = null;
        private readonly SettingsForm settings = new();
        private Wait wait = new(false);

        #region //////////////////// Platforms ////////////////////
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
            Platform.PCECD.ToString(),
            null,
            Platform.NEO.ToString(),
            null,
            Platform.C64.ToString(),
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
                if (!string.IsNullOrWhiteSpace(platform))
                {
                    Platform converted = (Platform)Enum.Parse(typeof(Platform), platform);

                    list.Add(new MenuItem(platform, addProject)
                    {
                        Text = Program.Lang.Format(("project_type", Name), Program.Lang.Console(converted)),
                        Name = platform
                    });
                }

                else list.Add(new MenuItem("-"));
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
                    Platform converted = (Platform)Enum.Parse(typeof(Platform), platform);

                    list.Add(new ToolStripMenuItem
                    (
                        Program.Lang.Format(("project_type", Name), Program.Lang.Console(converted)),
                        Platforms.Icons[converted],
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
            about.Text = Program.Lang.Format(("about_app", null), Program.Lang.ApplicationTitle);

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
            import_game_file.Text = Program.Lang.Format((import_game_file.Tag.ToString(), Name), Program.Lang.String("rom_label1", "projectform"));

            toolbarNewProject.Text = new Regex(@"\(.*\)").Replace(new_project.Text, "");
            toolbarOpenProject.Text = open_project.Text;
            toolbarSave.Text = save_project.Text;
            toolbarSaveAs.Text = save_project_as.Text;
            toolbarExport.Text = export.Text;
            toolbarCloseProject.Text = close_project.Text;
            toolbarGameScan.Text = game_scan.Text;
            toolbarPreferences.Text = preferences.Text = Program.Lang.String("preferences");

            vistaMenu.SetImage(new_project, toolbarNewProject.Image);
            vistaMenu.SetImage(open_project, toolbarOpenProject.Image);
            vistaMenu.SetImage(save_project, toolbarSave.Image);
            vistaMenu.SetImage(save_project_as, toolbarSaveAs.Image);
            vistaMenu.SetImage(export, toolbarExport.Image);
            vistaMenu.SetImage(close_project, toolbarCloseProject.Image);
            vistaMenu.SetImage(import_game_file, toolbarImportGameFile.Image);
            vistaMenu.SetImage(game_scan, toolbarGameScan.Image);
            vistaMenu.SetImage(preferences, toolbarPreferences.Image);
            foreach (MenuItem item in new_project.MenuItems.OfType<MenuItem>())
                if (Enum.TryParse(item.Name, out Platform converted))
                    vistaMenu.SetImage(item, Platforms.Icons[converted]);

            BrowseProject.Title = new Regex(@"\(.*\)").Replace(open_project.Text, "").Replace("&", "");
            SaveProject.Title = new Regex(@"\(.*\)").Replace(save_project_as.Text, "").Replace("&", "");
            SaveWAD.Title = new Regex(@"\(.*\)").Replace(export.Text, "").Replace("&", "");

            try { BrowseProject.Filter = SaveProject.Filter = Program.Lang.String("filter.project"); }
            catch { MessageBox.Show("Warning!\nThe language strings have not been loaded correctly.\n\nSeveral items may show up as 'undefined'.\n\nOther exceptions related to strings or filters can also occur!", MessageBox.Buttons.Ok, MessageBox.Icons.Warning, false); }
            #endregion

            if (Program.DebugMode)
            {
                Text += " [Running in debug mode]";
                // Debug mode-only features are activated here. //
                test_database.Visible = true;
            }

            RefreshRecent();

            foreach (JacksiroKe.MdiTabCtrl.TabPage tabPage in tabControl.TabPages)
                if (tabPage.Form.GetType() == typeof(ProjectForm))
                    (tabPage.Form as ProjectForm).RefreshForm();
        }

        public void UpdateConfig()
        {
            foreach (JacksiroKe.MdiTabCtrl.TabPage tabPage in tabControl.TabPages)
                if (tabPage.Form.GetType() == typeof(ProjectForm))
                {
                    (tabPage.Form as ProjectForm).use_online_wad.Enabled = Program.Config.application.use_online_wad_enabled;
                    if (!(tabPage.Form as ProjectForm).use_online_wad.Enabled) (tabPage.Form as ProjectForm).use_online_wad.Checked = false;
                }
        }

        public MainForm(string[] files = null)
        {
            InitializeComponent();
            Program.Handle = Handle;
            this.files = files;
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

            // Open project(s) if defined as argument(s)
            // ********
            if (files?.Length > 0 && File.Exists(files[0]))
                OpenProject(files);

            // Automatically set defined initial directory for save file dialog
            // ********
            // SaveWAD.InitialDirectory = Paths.Out;

            if (Program.Config.application.auto_update) { _ = Updater.GetLatest(); }
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

        /// <summary>
        /// Displays wait dialog.
        /// </summary>
        /// <param name="reset">Resets the dialog.</param>
        /// <param name="show">Shows or hides the dialog.</param>
        /// <param name="msg">The message to display.</param>
        /// <param name="progress">Progress value</param>
        /// <param name="showProgress">Whether to display the progress bar.</param>
        public void Wait(bool show, bool reset, bool showProgress, int progress = 0, int msg = 0) => Wait(show, reset, showProgress, progress, Program.Lang.Msg(msg, 2));

        /// <summary>
        /// Displays wait dialog.
        /// </summary>
        /// <param name="reset">Resets the dialog.</param>
        /// <param name="show">Shows or hides the dialog.</param>
        /// <param name="msg">The message to display.</param>
        /// <param name="progress">Progress value</param>
        /// <param name="showProgress">Whether to display the progress bar.</param>
        public void Wait(bool show, bool reset, bool showProgress, int progress, string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    Enabled = !show;

                    if (show)
                    {
                        if (reset || wait == null)
                        {
                            if (wait != null) wait.Visible = false;
                            wait = new(showProgress, msg) { Visible = false };
                        }

                        if (!wait.Visible)
                            wait.Show(this);
                    }

                    else
                    {
                        if (wait != null) wait.Visible = false;
                        Select();
                    }

                    wait.progress.Value = progress;
                }));
            }

            else
            {
                Enabled = !show;

                if (show)
                {
                    if (reset || wait == null)
                    {
                        if (wait != null) wait.Visible = false;
                        wait = new(showProgress, msg) { Visible = false };
                    }

                    if (!wait.Visible)
                        wait.Show(this);
                }

                else
                {
                    if (wait != null) wait.Visible = false;
                    Select();
                }

                wait.progress.Value = progress;
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
                save_project_as.Enabled = save_project.Enabled = false;
                export.Enabled = false;
                toolbarImportGameFile.Image = Properties.Resources.page_white_cd;
                import_game_file.Text = Program.Lang.Format((import_game_file.Tag.ToString(), Name), Program.Lang.String("rom_label1", "projectform"));

                tabControl.Visible = false;
            }

            else
            {
                game_scan.Enabled = (tabControl.SelectedForm as ProjectForm).ToolbarButtons[0];
                save_project_as.Enabled = save_project.Enabled = (tabControl.SelectedForm as ProjectForm).IsModified;
                export.Enabled = (tabControl.SelectedForm as ProjectForm).IsExportable;
                toolbarImportGameFile.Image = (tabControl.SelectedForm as ProjectForm).FileTypeImage;
                import_game_file.Text = Program.Lang.Format((import_game_file.Tag.ToString(), Name), (tabControl.SelectedForm as ProjectForm).FileTypeName);
            }

            import_game_file.Enabled = close_all.Enabled = close_project.Enabled = hasTabs;
            vistaMenu.SetImage(import_game_file, toolbarImportGameFile.Image);

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

        private void addTab(Platform platform, Project x = null, string file = null)
        {
            ProjectForm p = new(platform, file, x);
            p.FormClosed += TabChanged;
            tabControl.TabPages.Add(p);

            tabControl.BringToFront();
            tabControl.Visible = true;

            // if (file == null || !File.Exists(file)) BrowseROMDialog();
        }

        private void OpenROM_Click(object sender, EventArgs e) => BrowseROMDialog();

        private void BrowseROMDialog(ProjectForm _form = null)
        {
            if (_form == null && tabControl.SelectedForm != null)
            {
                _form = tabControl.SelectedForm as ProjectForm;
                _form.BrowseROMDialog(new Regex(@"\(.*\)").Replace(import_game_file.Text, "").Replace("&", ""));
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

        private void TabContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var page = (sender as ContextMenuStrip).SourceControl;
            var index = tabControl.TabPages.get_IndexOf(page as JacksiroKe.MdiTabCtrl.TabPage);
            if (index != -1) tabControl.TabPages[index].Select();
        }

        private void CloseTab_Click(object sender, EventArgs e) { var tab = tabControl.SelectedForm as Form; tab?.Close(); }

        private void CloseAll_Click(object sender, EventArgs e)
        {
            List<Form> list = new();

            for (int i = 0; i < tabControl.TabPages.Count; i++)
                list.Add(tabControl.TabPages[i].Form as Form);

            foreach (var tab in list)
                tab.Close();
        }

        private void About_Click(object sender, EventArgs e) { using var about = new About(); about.ShowDialog(); }

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedForm is not ProjectForm currentForm) return;

            SaveWAD.FileName = currentForm?.GetName(true);
            SaveWAD.Filter = (currentForm.IsForwarder ? Program.Lang.String("filter.zip") : Program.Lang.String("filter.wad")) + Program.Lang.String("filter");

            if (SaveWAD.ShowDialog() == DialogResult.OK)
                currentForm?.SaveToWAD(SaveWAD.FileName);
        }

        private void SaveAs_Click(object sender, EventArgs e) => SaveAs_Trigger(tabControl.SelectedForm as Form);

        public bool SaveAs_Trigger(Form form)
        {
            try
            {
                if (form is not ProjectForm) return false;

                SaveProject.FileName =
                    Path.GetFileNameWithoutExtension((form as ProjectForm).ProjectPath) ??
                    (form as ProjectForm)?.GetName(false) ??
                    (form as ProjectForm).Text;

                foreach (var item in new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' })
                    SaveProject.FileName = SaveProject.FileName.Replace(item, '_');

                if (SaveProject.ShowDialog() == DialogResult.OK)
                {
                    (form as ProjectForm).SaveProject(SaveProject.FileName);
                    return true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Program.Lang.Msg(18, 1), form.Text), ex.Message, MessageBox.Buttons.Ok);
            }

            return false;
        }

        private void SaveAll_Click(object sender, EventArgs e)
        {
            List<ProjectForm> list = new();

            for (int i = 0; i < tabControl.TabPages.Count; i++)
                list.Add(tabControl.TabPages[i].Form as ProjectForm);

            foreach (var tab in list)
            {
                try { if (File.Exists(tab.ProjectPath)) tab.SaveProject(tab.ProjectPath); else SaveAs_Trigger(tab); }
                catch (Exception ex) { MessageBox.Show(string.Format(Program.Lang.Msg(18, 1), tab.Text), ex.Message, MessageBox.Buttons.Ok); }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedForm is not ProjectForm currentForm) return;

            if (File.Exists(currentForm.ProjectPath))
            {
                try { currentForm.SaveProject(currentForm.ProjectPath); }
                catch (Exception ex) { MessageBox.Show(string.Format(Program.Lang.Msg(18, 1), currentForm.Text), ex.Message, MessageBox.Buttons.Ok); }
            }

            else SaveAs_Trigger(currentForm);
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            if (BrowseProject.ShowDialog() == DialogResult.OK)
            {
                OpenProject(BrowseProject.FileNames);
            }
        }

        public void RefreshRecent()
        {
            open_recent.MenuItems.Clear();

            if (File.Exists(Program.Config.paths.recent_00)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_00).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_01)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_01).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_02)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_02).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_03)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_03).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_04)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_04).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_05)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_05).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_06)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_06).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_07)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_07).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_08)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_08).Replace("&", "&&"), OpenRecent));
            if (File.Exists(Program.Config.paths.recent_09)) open_recent.MenuItems.Add(new MenuItem(Path.GetFileName(Program.Config.paths.recent_09).Replace("&", "&&"), OpenRecent));

            if (open_recent.MenuItems?.Count > 0)
            {
                open_recent.MenuItems.Add(new MenuItem("-", OpenRecent));
                open_recent.MenuItems.Add(new MenuItem(Program.Lang.String("clear_recent", Name), ClearRecent));
            }

            open_recent.Enabled = open_recent.MenuItems?.Count > 0;
        }

        private void ClearRecent(object sender, EventArgs e)
        {
            Program.Config.paths.recent_00 = null;
            Program.Config.paths.recent_01 = null;
            Program.Config.paths.recent_02 = null;
            Program.Config.paths.recent_03 = null;
            Program.Config.paths.recent_04 = null;
            Program.Config.paths.recent_05 = null;
            Program.Config.paths.recent_06 = null;
            Program.Config.paths.recent_07 = null;
            Program.Config.paths.recent_08 = null;
            Program.Config.paths.recent_09 = null;

            RefreshRecent();
        }

        private void OpenRecent(object sender, EventArgs e)
        {
            var control = sender as MenuItem;
            var list = open_recent.MenuItems.OfType<MenuItem>().Where(x => x.Text == control?.Text).ToArray();

            if (list?.Length > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    string file = i switch
                    {
                        9 => Program.Config.paths.recent_09,
                        8 => Program.Config.paths.recent_08,
                        7 => Program.Config.paths.recent_07,
                        6 => Program.Config.paths.recent_06,
                        5 => Program.Config.paths.recent_05,
                        4 => Program.Config.paths.recent_04,
                        3 => Program.Config.paths.recent_03,
                        2 => Program.Config.paths.recent_02,
                        1 => Program.Config.paths.recent_01,
                        0 or _ => Program.Config.paths.recent_00
                    };

                    string title1 = Path.GetFileName(file)?.Replace("&", "&&");
                    string title2 = list[0].Text;

                    if (title1 == title2)
                    {
                        OpenProject(new string[] { file });
                        return;
                    }
                }
            }
        }

        private void OpenProject(string[] files)
        {
            foreach (var file in files)
            {
                var project = new Project();

                try
                {
                    using Stream stream = File.Open(file, FileMode.Open);
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    project = (Project)binaryFormatter.Deserialize(stream);

                    if (project.ProjectPath != file) project.ProjectPath = file;

                    addTab(project.Platform, project);
                }

                catch
                {
                    MessageBox.Show(string.Format(Program.Lang.Msg(17, 1), Path.GetFileName(file)), MessageBox.Buttons.Ok, MessageBox.Icons.Error);
                }
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
            {
                if (tabControl.SelectedForm == null)
                {
                    foreach (string file in files)
                    {
                        var value = Path.GetExtension(file).ToLower();
                        switch (value)
                        {
                            default:
                                foreach (var tuple in Platforms.Filters)
                                {
                                    foreach (var extension in tuple.Value)
                                    {
                                        if (extension.ToLower().Contains(value))
                                        {
                                            addTab(tuple.Key ?? Platform.NES, null, file);
                                            return;
                                        }
                                    };
                                }

                                System.Media.SystemSounds.Beep.Play();
                                break;

                            case ".swf":
                                addTab(Platform.Flash, null, file);
                                break;

                            case ".ldb":
                                addTab(Platform.RPGM, null, file);
                                break;

                            case ".cue":
                                addTab(Platform.PSX, null, file);
                                break;

                            case ".fppj":
                                OpenProject(new string[] { file });
                                break;
                        }
                    }
                }

                else
                {
                    foreach (string file in files)
                    {
                        if (Path.GetExtension(file).ToLower() == ".fppj")
                        {
                            OpenProject(new string[] { file });
                        }

                        else
                        {
                            (tabControl.SelectedForm as ProjectForm).LoadROM(files[0], Program.Config.application.auto_prefill, true);
                        }
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

                Program.Config.application.auto_update = auto_update.Checked;
            }

            /* 
            check_for_updates.Enabled = !Default.auto_update_check || !Program.IsUpdated; */
        }

        private void LanguageFileEditor(object sender, EventArgs e)
        {
            new LanguageEditor().ShowDialog();
        }

        private void ClearAllDatabases(object sender, EventArgs e)
        {
            if (Directory.Exists(Paths.Databases))
                foreach (var item in Directory.EnumerateFiles(Paths.Databases))
                    if (Path.GetExtension(item).ToLower() == ".xml")
                        File.Delete(item);
        }

        private void ResetConfig(object sender, EventArgs e)
        {
            if (MessageBox.Show(Program.Lang.Msg(11), MessageBox.Buttons.YesNo, MessageBox.Icons.Warning) == MessageBox.Result.Yes)
            {
                if (Directory.Exists(Paths.Databases))
                    foreach (var item in Directory.EnumerateFiles(Paths.Databases))
                        if (Path.GetExtension(item).ToLower() == ".xml")
                            File.Delete(item);

                Program.Config.Reset(false);
                try { File.Delete(Paths.Config); } catch { }

                Environment.Exit(0);
            }
        }

        private void ExtractWAD_Click(object sender, EventArgs e)
        {
            using OpenFileDialog wadDialog = new()
            {
                Title = new Regex(@"\(.*\)").Replace((sender as MenuItem).Text, "").Replace("&", ""),
                Filter = Program.Lang.String("filter.wad") + Program.Lang.String("filter"),

                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "wad",
                SupportMultiDottedExtensions = true
            };

            if (wadDialog.ShowDialog() == DialogResult.OK)
            {
                Exception error = null;
                using WAD w = new();

                try { w.LoadFile(wadDialog.FileName); }
                catch (Exception ex) { error = ex; goto Failed; }

                if (sender == extract_wad_banner || sender == extract_wad_icon)
                {
                    using SaveFileDialog saveDialog = new()
                    {
                        FileName = sender == extract_wad_icon ? "icon.bin" : "banner.bin",
                        Title = wadDialog.Title,
                        Filter = "BIN (*.bin)|*.bin" + Program.Lang.String("filter"),

                        AddExtension = true,
                        DefaultExt = "bin",
                        SupportMultiDottedExtensions = true
                    };

                    bool isIMET = sender == extract_wad_banner && MessageBox.Show(Program.Lang.Msg(10), MessageBox.Buttons.YesNo) == MessageBox.Result.Yes;
                    if (isIMET)
                    {
                        saveDialog.FileName = "opening.bnr";
                        saveDialog.Filter = "BNR (*.bnr)|*.bnr|BIN (*.bin)|*.bin" + Program.Lang.String("filter");
                        saveDialog.DefaultExt = "bnr";
                    }

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using var x = isIMET ? w.BannerApp : U8.Load(w.BannerApp.Data[w.BannerApp.GetNodeIndex(sender == extract_wad_icon ? "icon.bin" : "banner.bin")]);
                            File.WriteAllBytes(saveDialog.FileName, x.ToByteArray());

                            goto Succeeded;
                        }

                        catch (Exception ex) { error = ex; goto Failed; }
                    }
                }

                else if (sender == extract_wad_sound)
                {
                    #region ##### Extract banner sound #####
                    using SaveFileDialog saveDialog = new()
                    {
                        FileName = "sound",
                        Title = wadDialog.Title,
                        Filter = "WAV (*.wav)|*.wav|BNS (*.bns)|*.bns" + Program.Lang.String("filter"),

                        AddExtension = true,
                        SupportMultiDottedExtensions = true
                    };

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            var wav = SoundHelper.ExtractSound(w, Path.GetExtension(saveDialog.FileName).ToLower() == ".wav");
                            if (wav == null) throw new FileNotFoundException();

                            File.WriteAllBytes(saveDialog.FileName, wav);

                            goto Succeeded;
                        }

                        catch (Exception ex) { error = ex; goto Failed; }
                    }
                    #endregion
                }

                else if (sender == extract_wad_manual)
                {
                    U8 u8, extManual = null;

                    try
                    {
                        for (int i = Math.Min(w.Contents.Length, 5); i < w.Contents.Length; i++)
                        {
                            Loop:
                            if (i != w.Contents.Length)
                            {
                                try { u8 = U8.Load(w.Contents[i]); } catch { i++; goto Loop; }

                                #region ##### Extract original manual #####
                                if (u8.StringTable.Contains("main.ccf"))
                                {
                                    using var target = CCF.Load(u8.Data[u8.GetNodeIndex("main.ccf")]);

                                    // Get and read emanual
                                    // ****************
                                    foreach (var item in target.Nodes)
                                        if (item.Name.ToLower().Contains("man.arc"))
                                        {
                                            extManual = U8.Load(target.Data[target.GetNodeIndex(item.Name)]);
                                            u8.Dispose();
                                            break;
                                        }
                                }

                                else
                                {
                                    foreach (var item in u8.StringTable)
                                        if (item.ToLower().Contains("htmlc.arc") || item.ToLower().Contains("lz77_html.arc") || item.ToLower().Contains("lz77emanual.arc"))
                                        {
                                            File.WriteAllBytes(Paths.WorkingFolder + "html.arc", u8.Data[u8.GetNodeIndex(item)]);
                                            Utils.Run
                                            (
                                                FileDatas.Apps.wwcxtool,
                                                "wwcxtool.exe",
                                                "/u html.arc html.dec"
                                            );
                                            if (!File.Exists(Paths.WorkingFolder + "html.dec")) throw new Exception(Program.Lang.Msg(2, 1));

                                            var bytes = File.ReadAllBytes(Paths.WorkingFolder + "html.dec");

                                            try { File.Delete(Paths.WorkingFolder + "html.dec"); } catch { }
                                            try { File.Delete(Paths.WorkingFolder + "html.arc"); } catch { }

                                            extManual = U8.Load(bytes);
                                            u8.Dispose();
                                            break;
                                        }

                                        else if (item.ToLower().Contains("emanual.arc") || item.ToLower().Contains("html.arc") || item.ToLower().Contains("man.arc"))
                                        {
                                            var file = u8.Data[u8.GetNodeIndex(item)];

                                            try
                                            {
                                                extManual = U8.Load(file);
                                            }
                                            catch // File is compressed
                                            {
                                                int type = item.ToLower().Contains("lzh8") ? 1 : 0;
                                                // 0 = LZ77, 1 = LZH8

                                                Decompress:
                                                // Create temporary files at working folder
                                                // ****************
                                                File.WriteAllBytes(Paths.WorkingFolder + "emanual.arc", file);

                                                // Decompress
                                                // ****************
                                                Utils.Run
                                                (
                                                    type == 0 ? FileDatas.Apps.wwcxtool : FileDatas.Apps.lzh8_dec,
                                                    type == 0 ? "wwcxtool" : "lzh8_dec",
                                                    (type == 0 ? "/u " : null) + "emanual.arc emanual.dec"
                                                );

                                                if (File.Exists(Paths.WorkingFolder + "emanual.dec"))
                                                    file = File.ReadAllBytes(Paths.WorkingFolder + "emanual.dec");

                                                try { File.Delete(Paths.WorkingFolder + "emanual.arc"); } catch { }
                                                try { File.Delete(Paths.WorkingFolder + "emanual.dec"); } catch { }

                                                // Load manual
                                                // ****************
                                                try { extManual = U8.Load(file); }
                                                catch
                                                {
                                                    if (type == 0)
                                                    {
                                                        type++;
                                                        goto Decompress;
                                                    }

                                                    else throw;
                                                }
                                            }

                                            u8.Dispose();
                                            break;
                                        }
                                }
                                #endregion
                            }
                        }

                        if (extManual != null)
                        {
                            using FolderBrowserDialog saveDialog = new()
                            {
                                ShowNewFolderButton = true,
                                Description = wadDialog.Title.TrimEnd('.').Trim(),
                            };

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                extManual.Unpack(saveDialog.SelectedPath);
                                extManual.Dispose();
                            }

                            goto Succeeded;
                        }

                        goto Failed;
                    }

                    catch (Exception ex) { error = ex; goto Failed; }

                }

                goto End;

                Failed:
                if (error == null)
                    MessageBox.Show(Program.Lang.Msg(16, 1));
                else
                    MessageBox.Error(error.Message);
                goto End;

                Succeeded:
                System.Media.SystemSounds.Beep.Play();
                goto End;

                End:
                if (w != null) w.Dispose();
            }
        }

        private void ToolStrip_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height + 5);
            using var b = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(0, r.Height), Color.White, Color.Gainsboro);
            e.Graphics.FillRectangle(b, r);
        }

        private void TabControl_Paint(object sender, JacksiroKe.MdiTabCtrl.TabControl.TabPaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

        private void Website_Click(object sender, EventArgs e)
        {
            string lang = Program.Lang.Current.StartsWith("fr") ? "fr/"
                        : Program.Lang.Current.StartsWith("es") ? "es/"
                        : Program.Lang.Current.StartsWith("ja") ? "ja/"
                        : null;
            System.Diagnostics.Process.Start("https://catmanfan.github.io/FriishProduce/" + lang);
        }

        private async void TestDatabase(object sender, EventArgs e)
        {
            if (MessageBox.Show(Program.Lang.Msg(12), MessageBox.Buttons.YesNo) != MessageBox.Result.Yes) return;

            Dictionary<string, bool> tested = new();

            // Open each platform's database.
            // ****************
            foreach (Platform p in new Platform[] { Platform.NES, Platform.SNES, Platform.N64, Platform.SMS, Platform.SMD, Platform.PCE, Platform.PCECD, Platform.NEO, Platform.MSX, Platform.Flash })
            {
                string msg = string.Format(Program.Lang.Msg(3, 2), Program.Lang.Console(p));

                Wait(true, true, true, 0, msg);

                ChannelDatabase c = new(p);
                int index = 0;
                double total = 0, verified = 0;

                for (int x = index; x < c.Entries.Count; x++)
                    total += c.Entries[x].Count;

                // Download each entry's WAD files, then check to see if contents are valid.
                // ****************
                for (int x = index; x < c.Entries.Count; x++)
                {
                    var entry = c.Entries[x];

                    for (int y = 0; y < entry.Count; y++)
                    {
                        wait.Msg = msg + $" ({verified + 1}/{total})";

                        int regIndex = entry.Regions[y];
                        string title = string.Format
                        (
                            "{0} ({1}) ({2})",
                            entry.Titles[y],
                            Program.Lang.Console(p),
                            regIndex == 0 ? "J" : regIndex is 1 or 2 ? "U" : regIndex is 3 or 4 or 5 ? "E" : regIndex is 6 or 7 ? "K" : Program.Lang.String("region_rf")
                        );

                        await System.Threading.Tasks.Task.Run(() =>
                        {
                            WAD w = WAD.Load(Web.Get(entry.GetWAD(y)));
                            bool valid = true;
                            try { if (w.Contents?.Length == 0) throw new NotSupportedException(); }
                            catch { valid = false; }
                            w.Dispose();

                            tested.Add(title, valid);
                        });

                        verified += 1;
                        int value = (int)Math.Round(verified / total * 100.0);

                        Wait(true, false, true, value);
                    }
                }

                Wait(false, false, false);
            }

            // Final report
            // ****************
            var failed = tested.Where(x => !x.Value).ToArray();
            if (failed?.Length > 0)
            {
                List<string> failedNames = new();

                foreach (var item in failed)
                    failedNames.Add("- " + item.Key);

                MessageBox.Show(string.Format(Program.Lang.Msg(14), string.Join(Environment.NewLine, failedNames)));
            }
            else
                MessageBox.Show(Program.Lang.Msg(13));
        }
    }
}
