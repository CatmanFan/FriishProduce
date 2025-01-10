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
                extract_wad_banner.Visible = true;
                extract_wad_icon.Visible = true;
            }

            foreach (JacksiroKe.MdiTabCtrl.TabPage tabPage in tabControl.TabPages)
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

            // BrowseROMDialog();
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
            var index = tabControl.TabPages.get_IndexOf(page as JacksiroKe.MdiTabCtrl.TabPage);
            if (index != -1) tabControl.TabPages[index].Select();
        }

        private void CloseTab_Click(object sender, EventArgs e) { var tab = tabControl.SelectedForm as Form; tab?.Close(); }

        private void About_Click(object sender, EventArgs e) { using var about = new About() { Font = Font }; about.ShowDialog(); }

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedForm is not ProjectForm currentForm) return;

            SaveWAD.FileName = currentForm?.GetName(true);
            SaveWAD.Filter = (currentForm.IsForwarder ? Program.Lang.String("filter.zip") : Program.Lang.String("filter.wad")) + Program.Lang.String("filter");

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
            foreach (var item in Directory.EnumerateFiles(Paths.Databases))
                if (Path.GetExtension(item).ToLower() == ".xml")
                    File.Delete(item);
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
                Exception error;
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

                    bool isIMET = sender == extract_wad_banner && MessageBox.Show(Program.Lang.Msg(11), MessageBox.Buttons.YesNo) == MessageBox.Result.Yes;
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
                                            if (!File.Exists(Paths.WorkingFolder + "html.dec")) throw new Exception(Program.Lang.Msg(2, true));

                                            var bytes = File.ReadAllBytes(Paths.WorkingFolder + "html.dec");

                                            if (File.Exists(Paths.WorkingFolder + "html.dec")) File.Delete(Paths.WorkingFolder + "html.dec");
                                            if (File.Exists(Paths.WorkingFolder + "html.arc")) File.Delete(Paths.WorkingFolder + "html.arc");

                                            extManual = U8.Load(bytes);
                                            u8.Dispose();
                                            break;
                                        }

                                        else if (item.ToLower().Contains("emanual.arc") || item.ToLower().Contains("html.arc") || item.ToLower().Contains("man.arc"))
                                        {
                                            extManual = U8.Load(u8.Data[u8.GetNodeIndex(item)]);
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

                            goto End;
                        }

                        else throw new Exception(Program.Lang.Msg(16, true));
                    }

                    catch (Exception ex) { error = ex; goto Failed; }

                }

                goto End;

                Failed:
                if (error == null)
                    System.Media.SystemSounds.Hand.Play();
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
    }
}
