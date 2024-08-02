using Ionic.Zip;
using libWiiSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class ProjectForm : Form
    {
        protected Platform targetPlatform { get; set; }

        protected string TIDCode;
        protected string Untitled;
        protected string WADPath = null;

        private bool _showSaveData;
        protected bool showSaveData
        {
            get => _showSaveData;
            set
            {
                _showSaveData = value;

                autolink_save_data.Visible = SaveIcon_Panel.Visible = save_data_title.Visible = value;
                label16.Visible = !value;
            }
        }

        private bool _isShown;
        private bool _isMint;

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;

            set
            {
                _isModified = value;
                if (value) _isMint = false;
                Program.MainForm.toolbarSaveAs.Enabled = value;
                Program.MainForm.menuItem6.Enabled = value;
                Program.MainForm.toolbarExport.Enabled = IsExportable;
                Program.MainForm.menuItem11.Enabled = IsExportable;
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;

            set
            {
                _isEmpty = value;

                if (_isShown)
                {
                    title_id_random.Visible =
                    import_image.Enabled =
                    import_patch.Enabled =
                    wad_base.Enabled =
                    forwarder_root_device.Enabled =
                    manual_type.Enabled =
                    groupBox3.Enabled =
                    groupBox4.Enabled =
                    groupBox5.Enabled =
                    groupBox6.Enabled =
                    groupBox7.Enabled = !value;
                }
            }
        }

        public bool IsExportable
        {
            get
            {
                bool yes = !string.IsNullOrEmpty(_tID) && _tID.Length == 4
                            && !string.IsNullOrWhiteSpace(channel_title.Text)
                            && !string.IsNullOrEmpty(_bannerTitle)
                            && (img != null)
                            && rom?.FilePath != null;

                return save_data_title.Visible ? yes && !string.IsNullOrEmpty(_saveDataTitle[0]) : yes;
            }
        }

        protected bool IsVC { get => injection_methods.SelectedItem?.ToString().ToLower() == Program.Lang.String("vc").ToLower(); }
        public bool IsForwarder { get => !IsVC && targetPlatform != Platform.Flash; }

        private new enum Region
        {
            America,
            Europe,
            Japan,
            Korea,
            Free,
            Orig
        };

        // -----------------------------------
        // Public variables
        // -----------------------------------
        protected ChannelDatabase channels { get; set; }
        protected (int baseNumber, int region) inWad { get; set; }
        protected string inWadFile { get; set; }
        private Region inWadRegion
        {
            get
            {
                for (int index = 0; index < channels.Entries[Base.SelectedIndex].Regions.Count; index++)
                    if (channels?.Entries[Base.SelectedIndex].GetUpperID(index)[3] == baseID.Text[3])
                        return channels?.Entries[Base.SelectedIndex].Regions[index] == 0 ? Region.Japan
                              : channels?.Entries[Base.SelectedIndex].Regions[index] == 6 || channels.Entries[Base.SelectedIndex].Regions[index] == 7 ? Region.Korea
                              : channels?.Entries[Base.SelectedIndex].Regions[index] >= 3 && channels.Entries[Base.SelectedIndex].Regions[index] <= 5 ? Region.Europe
                              : Region.America;

                throw new InvalidOperationException();
            }
        }

        private Project project;

        private WAD outWad;
        private libWiiSharp.Region outWadRegion
        {
            get => region_list.SelectedItem?.ToString() == Program.Lang.String("region_j") ? libWiiSharp.Region.Japan
                 : region_list.SelectedItem?.ToString() == Program.Lang.String("region_u") ? libWiiSharp.Region.USA
                 : region_list.SelectedItem?.ToString() == Program.Lang.String("region_e") ? libWiiSharp.Region.Europe
                 : region_list.SelectedItem?.ToString() == Program.Lang.String("region_k") ? libWiiSharp.Region.Korea
                 : region_list.SelectedIndex == 0 ? inWadRegion switch { Region.Japan => libWiiSharp.Region.Japan, Region.Korea => libWiiSharp.Region.Korea, Region.Europe => libWiiSharp.Region.Europe, Region.America => libWiiSharp.Region.USA, _ => libWiiSharp.Region.Free }
                 : libWiiSharp.Region.Free;
        }

        protected ROM rom { get; set; }
        protected string patch { get; set; }
        protected string manual { get; set; }
        protected ImageHelper img { get; set; }

        private Preview preview = new Preview();

        protected ContentOptions contentOptionsForm { get; set; }
        protected IDictionary<string, string> contentOptions { get => contentOptionsForm?.Options; }

        #region Channel/banner parameters
        private string _tID { get => title_id_upper.Text.ToUpper(); }
        private string[] _channelTitles { get => new string[8] { channel_title.Text, channel_title.Text, channel_title.Text, channel_title.Text, channel_title.Text, channel_title.Text, channel_title.Text, channel_title.Text }; } // "無題", "Untitled", "Ohne Titel", "Sans titre", "Sin título", "Senza titolo", "Onbekend", "제목 없음"
        private string _bannerTitle { get => banner_title.Text; }
        private int _bannerYear { get => (int)released.Value; }
        private int _bannerPlayers { get => (int)players.Value; }
        private string[] _saveDataTitle { get => save_data_title.Lines.Length == 1 ? new string[] { save_data_title.Text } : save_data_title.Lines.Length == 0 ? new string[] { "" } : save_data_title.Lines; }
        private int _bannerRegion
        {
            get
            {
                // -1 = Auto
                // 0  = USA
                // 1  = Japan
                // 2  = Korea
                // 3  = Europe

                int lang = IsVC ? -1 : outWadRegion switch
                {
                    libWiiSharp.Region.USA => 0,
                    libWiiSharp.Region.Japan => 1,
                    libWiiSharp.Region.Korea => 2,
                    libWiiSharp.Region.Europe => 3,
                    _ => -1
                };

                if (lang < 0 || lang > 3) lang = channels != null ? inWadRegion switch { Region.Japan => 1, Region.Korea => 2, Region.Europe => 3, _ => 0 } : 0;
                if (!IsVC && Program.Lang.Current.StartsWith("ja")) lang = 1;
                if (!IsVC && Program.Lang.Current.StartsWith("ko")) lang = 2;

                // Japan/Korea: Use USA banner for C64
                if (lang != 0 && lang != 3 && targetPlatform == Platform.C64)
                    lang = 0;

                // International: Use Japan banner for MSX
                else if (lang != 1 && targetPlatform == Platform.MSX)
                    lang = 1;

                // Korea: Use Europe banner for SMD
                else if (lang == 2 && targetPlatform == Platform.SMD)
                    lang = 3;

                // Korea: Use USA banner for non-available platforms
                else if (lang == 2 && (int)targetPlatform >= 3)
                    lang = 0;

                return lang;
            }
        }
        #endregion


        // -----------------------------------

        public void SaveProject(string path)
        {
            var p = new Project()
            {
                Platform = targetPlatform,

                ROM = rom?.FilePath,
                Patch = patch,
                Manual = (manual_type_list.SelectedIndex, manual),
                Img = img?.Source ?? null,
                InjectionMethod = injection_methods.SelectedIndex,
                ForwarderStorageDevice = forwarder_usb.Checked ? 1 : 0,
                ContentOptions = contentOptions ?? null,
                WADRegion = region_list.SelectedIndex,
                LinkSaveDataTitle = autolink_save_data.Checked,
                ImageOptions = (imageintpl.SelectedIndex, image_fit.Checked),
                VideoMode = video_modes.SelectedIndex,

                TitleID = _tID,
                ChannelTitles = _channelTitles,
                SaveDataTitle = _saveDataTitle,
                BannerTitle = _bannerTitle,
                BannerYear = _bannerYear,
                BannerPlayers = _bannerPlayers,
            };

            if (!string.IsNullOrWhiteSpace(WADPath)) p.BaseFile = WADPath;
            else p.Base = (Base.SelectedIndex, 0);

            for (int i = 0; i < baseRegionList.Items.Count; i++)
                if (baseRegionList.Items[i].GetType() == typeof(ToolStripMenuItem) && (baseRegionList.Items[i] as ToolStripMenuItem).Checked) p.Base = (Base.SelectedIndex, i);

            using (Stream stream = File.Open(path, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, p);
            }

            IsModified = false;
            _isMint = true;
        }

        public void RefreshForm()
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            bool isMint = _isMint || !Program.MainForm.menuItem6.Enabled;

            #region Localization
            Program.Lang.Control(this, "projectform");
            title_id.Text = title_id_2.Text;
            import_rom.Text = string.Format(Program.Lang.String(import_rom.Name, Name), targetPlatform switch
            {
                Platform.PSX => Program.Lang.String("disc_image"),
                Platform.GCN => Program.Lang.String("disc_image"),
                Platform.PCECD => Program.Lang.String("disc_image"),
                Platform.SMCD => Program.Lang.String("disc_image"),
                Platform.RPGM => Program.Lang.String("game_file"),
                Platform.NEO => "ZIP",
                Platform.Flash => "SWF",
                _ => "ROM",
            });
            browsePatch.Filter = Program.Lang.String("filter.patch");
            // BrowseManualZIP.Filter = Program.Lang.String("filter.zip");

            // Change title text to untitled string
            Untitled = string.Format(Program.Lang.String("untitled_project", "mainform"), Program.Lang.String(Enum.GetName(typeof(Platform), targetPlatform).ToLower(), "platforms"));
            Text = string.IsNullOrWhiteSpace(channel_title.Text) ? Untitled : channel_title.Text;

            setFilesText();

            var baseMax = Math.Max(base_name.Location.X + base_name.Width - 4, title_id_2.Location.X + title_id_2.Width - 4) + 2;
            baseName.Location = new Point(baseMax, base_name.Location.Y);
            baseID.Location = new Point(baseMax, title_id_2.Location.Y);

            // Selected index properties
            Program.Lang.Control(imageintpl, Name);
            imageintpl.SelectedIndex = Properties.Settings.Default.image_interpolation;

            // Manual
            manual_type_list.SelectedIndex = 0;
            manual = null;

            // Regions lists
            region_list.Items.Clear();
            region_list.Items.Add(Program.Lang.String("keep_original"));
            region_list.Items.Add(Program.Lang.String("region_rf"));
            region_list.SelectedIndex = 0;

            // Video modes
            video_modes.Items[0] = Program.Lang.String("keep_original");
            video_modes.SelectedIndex = 0;

            switch (Program.Lang.Current.ToLower())
            {
                default:
                    region_list.Items.Add(Program.Lang.String("region_u"));
                    region_list.Items.Add(Program.Lang.String("region_e"));
                    region_list.Items.Add(Program.Lang.String("region_j"));
                    region_list.Items.Add(Program.Lang.String("region_k"));
                    break;

                case "ja":
                    region_list.Items.Add(Program.Lang.String("region_j"));
                    region_list.Items.Add(Program.Lang.String("region_u"));
                    region_list.Items.Add(Program.Lang.String("region_e"));
                    region_list.Items.Add(Program.Lang.String("region_k"));
                    break;

                case "ko":
                    region_list.Items.Add(Program.Lang.String("region_k"));
                    region_list.Items.Add(Program.Lang.String("region_u"));
                    region_list.Items.Add(Program.Lang.String("region_e"));
                    region_list.Items.Add(Program.Lang.String("region_j"));
                    break;
            }

            toolTip = new CustomToolTip();
            toolTip.SetToolTip(channel_title, Program.Lang.ToolTip(0));
            toolTip.SetToolTip(title_id_upper, Program.Lang.ToolTip(1));
            toolTip.SetToolTip(region_list, Program.Lang.ToolTip(2));
            toolTip.SetToolTip(save_data_title, Program.Lang.ToolTip(3));
            toolTip.SetToolTip(autolink_save_data, Program.Lang.ToolTip(4));
            toolTip.SetToolTip(video_modes, Program.Lang.ToolTip(5));
            toolTip.SetToolTip(banner_title, Program.Lang.ToolTip(6));
            toolTip.SetToolTip(released, Program.Lang.ToolTip(7));
            toolTip.SetToolTip(players, Program.Lang.ToolTip(8));
            toolTip.SetToolTip(injection_methods, Program.Lang.ToolTip(9));
            toolTip.SetToolTip(import_wad_from_file, Program.Lang.ToolTip(10));

            #endregion

            baseName.Font = new Font(baseName.Font, FontStyle.Bold);

            if (Base.SelectedIndex >= 0)
                for (int i = 0; i < channels.Entries[Base.SelectedIndex].Regions.Count; i++)
                {
                    baseRegionList.Items[i].Text = channels.Entries[Base.SelectedIndex].Regions[i] switch
                    {
                        1 or 2      => Program.Lang.String("region_u"),
                        3 or 4 or 5 => Program.Lang.String("region_e"),
                        6 or 7      => Program.Lang.String("region_k"),
                        _           => Program.Lang.String("region_j"),
                    };
                }


            for (int i = 0; i < Base.Items.Count; i++)
            {
                var title = channels.Entries[i].Regions.Contains(0) && Program.Lang.Current.ToLower().StartsWith("ja") ? channels.Entries[i].Titles[0]
                          : channels.Entries[i].Regions.Contains(0) && Program.Lang.Current.ToLower().StartsWith("ko") ? channels.Entries[i].Titles[channels.Entries[i].Titles.Count - 1]
                          : channels.Entries[i].Regions.Contains(0) && channels.Entries[i].Regions.Count > 1 ? channels.Entries[i].Titles[1]
                          : channels.Entries[i].Titles[0];

                Base.Items[i] = title;
            }

            // Injection methods list
            injection_methods.Items.Clear();

            switch (targetPlatform)
            {
                case Platform.NES:
                    injection_methods.Items.Add(Program.Lang.String("vc"));
                    injection_methods.Items.Add(Forwarder.List[0].Name);
                    injection_methods.Items.Add(Forwarder.List[1].Name);
                    injection_methods.Items.Add(Forwarder.List[2].Name);
                    break;

                case Platform.SNES:
                    injection_methods.Items.Add(Program.Lang.String("vc"));
                    injection_methods.Items.Add(Forwarder.List[3].Name);
                    injection_methods.Items.Add(Forwarder.List[4].Name);
                    injection_methods.Items.Add(Forwarder.List[5].Name);
                    break;

                case Platform.N64:
                    injection_methods.Items.Add(Program.Lang.String("vc"));
                    injection_methods.Items.Add(Forwarder.List[8].Name);
                    injection_methods.Items.Add(Forwarder.List[9].Name);
                    injection_methods.Items.Add(Forwarder.List[10].Name);
                    injection_methods.Items.Add(Forwarder.List[11].Name);
                    break;

                case Platform.SMS:
                case Platform.SMD:
                    injection_methods.Items.Add(Program.Lang.String("vc"));
                    injection_methods.Items.Add(Forwarder.List[7].Name);
                    break;

                case Platform.PCE:
                case Platform.NEO:
                case Platform.MSX:
                case Platform.C64:
                    injection_methods.Items.Add(Program.Lang.String("vc"));
                    break;

                case Platform.Flash:
                    injection_methods.Items.Add(Program.Lang.String("by_default"));
                    break;

                case Platform.GBA:
                    injection_methods.Items.Add(Forwarder.List[6].Name);
                    break;

                case Platform.PSX:
                    injection_methods.Items.Add(Forwarder.List[12].Name);
                    break;

                case Platform.RPGM:
                    injection_methods.Items.Add(Forwarder.List[13].Name);
                    break;

                default:
                    break;
            }

            injection_methods.SelectedIndex = targetPlatform switch
            {
                Platform.NES                 => Properties.Settings.Default.default_injection_method_nes,
                Platform.SNES                => Properties.Settings.Default.default_injection_method_snes,
                Platform.N64                 => Properties.Settings.Default.default_injection_method_n64,
                Platform.SMS or Platform.SMD => Properties.Settings.Default.default_injection_method_sega,
                _ => 0
            };
            injection_methods.Enabled = injection_methods.Items.Count > 1;
            groupBox4.Enabled = injection_methods.Enabled && !IsEmpty;
            released.Maximum = DateTime.Now.Year;

            if (Properties.Settings.Default.image_fit_aspect_ratio) image_fit.Checked = true; else image_stretch.Checked = true;
            resetImages();
            if (isMint && IsModified) IsModified = false;
        }

        private void LoadChannelDatabase()
        {
            try { channels = new ChannelDatabase(targetPlatform); }
            catch (Exception ex)
            {
                if ((int)targetPlatform < 10)
                {
                    System.Windows.Forms.MessageBox.Show($"A fatal error occurred retrieving the {targetPlatform} WADs database.\n\nException: {ex.GetType().FullName}\nMessage: {ex.Message}\n\nThe application will now shut down.", "Halt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Environment.FailFast("Database initialization failed.");
                }
                else { channels = new ChannelDatabase(); }
            }
        }

        public ProjectForm(Platform platform, string ROMpath = null, Project project = null)
        {
            targetPlatform = platform;
            IsEmpty = true;
            LoadChannelDatabase();

            InitializeComponent();
            AddBases();
            _isShown = true;

            if (project != null && ROMpath == null)
                this.project = project;

            if (ROMpath != null && project == null)
            {
                rom.FilePath = ROMpath;
                LoadROM(rom.FilePath, Properties.Settings.Default.auto_retrieve_game_data);
            }
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            // Declare WAD metadata modifier
            // ********
            TIDCode = null;

            using (var icon = new Bitmap(Program.MainForm.Icons[targetPlatform]))
            {
                icon.MakeTransparent(Color.White);
                Icon = Icon.FromHandle(icon.GetHicon());
            }

            switch (targetPlatform)
            {
                case Platform.NES:
                    TIDCode = "F";
                    rom = new ROM_NES();
                    break;

                case Platform.SNES:
                    TIDCode = "J";
                    rom = new ROM_SNES();
                    break;

                case Platform.N64:
                    TIDCode = "N";
                    rom = new ROM_N64();
                    break;

                case Platform.SMS:
                    TIDCode = "L";
                    rom = new ROM_SEGA() { IsSMS = true };
                    break;

                case Platform.SMD:
                    TIDCode = "M";
                    rom = new ROM_SEGA() { IsSMS = false };
                    break;

                case Platform.PCE:
                    TIDCode = "P";
                    rom = new ROM_PCE();
                    break;

                case Platform.PCECD:
                    TIDCode = "Q";
                    rom = new Disc();
                    break;

                case Platform.NEO:
                    TIDCode = "E";
                    rom = new ROM_NEO();
                    break;

                case Platform.MSX:
                    TIDCode = "X";
                    rom = new ROM_MSX();
                    break;

                case Platform.Flash:
                    rom = new SWF();
                    import_patch.Enabled = false;
                    break;

                case Platform.RPGM:
                    rom = new RPGM();
                    import_patch.Enabled = false;
                    players.Enabled = false;
                    break;

                default:
                    rom = new Disc();
                    import_patch.Enabled = false;
                    break;
            }

            // Cosmetic
            // ********
            RefreshForm();

            manual_type_list.Enabled = false;
            foreach (var manualConsole in new List<Platform>() // Confirmed to have an algorithm exist for NES, SNES, N64, SEGA, PCE, NEO
            {
                Platform.NES,
                Platform.SNES,
                Platform.N64,
                Platform.SMS,
                Platform.SMD,
                // Platform.PCE,
                // Platform.NEO
            })
                if (targetPlatform == manualConsole) manual_type_list.Enabled = true;

            if (project != null)
            {
                video_modes.SelectedIndex = project.VideoMode;

                // Error messages for not found files
                // ********
                foreach (var item in new string[] { project.ROM, project.Patch, project.BaseFile })
                    if (!File.Exists(item) && !string.IsNullOrWhiteSpace(item)) MessageBox.Show(string.Format(Program.Lang.Msg(10, true), Path.GetFileName(item)));

                img = new ImageHelper(project.Platform, null);
                img.LoadToSource(project.Img);
                LoadROM(project.ROM, false);

                if (File.Exists(project.BaseFile))
                {
                    WADPath = project.BaseFile;
                    import_wad_from_file.Checked = true;
                    LoadWAD(project.BaseFile);
                }
                else
                {
                    Base.SelectedIndex = project.Base.Item1;
                    for (int i = 0; i < baseRegionList.Items.Count; i++)
                        if (baseRegionList.Items[i].GetType() == typeof(ToolStripMenuItem)) (baseRegionList.Items[i] as ToolStripMenuItem).Checked = false;
                    UpdateBaseForm(project.Base.Item2);
                    (baseRegionList.Items[project.Base.Item2] as ToolStripMenuItem).Checked = true;
                }

                patch = File.Exists(project.Patch) ? project.Patch : null;
                setFilesText();

                channel_title.Text = project.ChannelTitles[1];
                banner_title.Text = project.BannerTitle;
                released.Value = project.BannerYear;
                players.Value = project.BannerPlayers;
                save_data_title.Lines = project.SaveDataTitle;
                title_id_upper.Text = project.TitleID;

                region_list.SelectedIndex = project.WADRegion;
                injection_methods.SelectedIndex = project.InjectionMethod;
                imageintpl.SelectedIndex = project.ImageOptions.Item1;
                image_fit.Checked = project.ImageOptions.Item2;

                if (contentOptionsForm != null) contentOptionsForm.Options = project.ContentOptions;
                LoadImage();
                LoadManual(project.Manual.Type, project.Manual.File);
            }

            autolink_save_data.Checked = project == null ? Properties.Settings.Default.link_save_data : project.LinkSaveDataTitle;

            int forwarderStorageDevice = project == null ? Options.FORWARDER.Default.root_storage_device : project.ForwarderStorageDevice;
            forwarder_sd.Checked  = forwarderStorageDevice == 0;
            forwarder_usb.Checked = forwarderStorageDevice == 1;

            IsEmpty = project == null;
            IsModified = false;
            _isMint = true;
            project = null;
        }

        // -----------------------------------

        public void BrowseROMDialog()
        {
            browseROM.Title = import_rom.Text;

            switch (targetPlatform)
            {
                default:
                    browseROM.Filter = Program.Lang.String("filter.disc") + "|" + Program.Lang.String("filter.zip") + Program.Lang.String("filter");
                    break;

                case Platform.NES:
                case Platform.SNES:
                case Platform.N64:
                case Platform.SMS:
                case Platform.SMD:
                case Platform.PCE:
                case Platform.C64:
                case Platform.MSX:
                    browseROM.Filter = Program.Lang.String($"filter.rom_{targetPlatform.ToString().ToLower()}");
                    break;

                case Platform.NEO:
                    browseROM.Filter = Program.Lang.String("filter.zip");
                    break;

                case Platform.Flash:
                    browseROM.Filter = Program.Lang.String("filter.swf");
                    break;

                case Platform.RPGM:
                    browseROM.Filter = Program.Lang.String("filter.rpgm");
                    break;
            }

            if (browseROM.ShowDialog() == DialogResult.OK)
            {
                IsEmpty = false;
                LoadROM(browseROM.FileName, Properties.Settings.Default.auto_retrieve_game_data);
            }
        }

        private void import_rom_Click(object sender, EventArgs e) => BrowseROMDialog();

        public void BrowseImageDialog()
        {
            browseImage.Title = import_image.Text;
            browseImage.Filter = Program.Lang.String("filter.img");

            if (browseImage.ShowDialog() == DialogResult.OK)
            {
                LoadImage(browseImage.FileName);
            }
        }

        private void import_image_Click(object sender, EventArgs e) => BrowseImageDialog();

        private void refreshData()
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (!IsEmpty)
            {
                IsModified = true;
            }

            setFilesText();
        }

        public bool[] ToolbarButtons
        {
            get => new bool[]
            {
                targetPlatform != Platform.Flash
                && targetPlatform != Platform.RPGM
                && rom?.FilePath != null, // LibRetro / game data

                targetPlatform != Platform.Flash
                && targetPlatform != Platform.RPGM
                && IsVC, // Browse manual
            };
        }

        private void setFilesText()
        {
            int maxLength = 75;

            rom_filename.Text = !string.IsNullOrWhiteSpace(rom?.FilePath) ? Path.GetFileName(rom.FilePath) : Program.Lang.String("none");
            patch_filename.Text = !string.IsNullOrWhiteSpace(patch) ? Path.GetFileName(patch) : Program.Lang.String("none");
            if (rom_filename.Text.Length > maxLength) rom_filename.Text = rom_filename.Text.Substring(0, maxLength - 3) + "...";
            if (patch_filename.Text.Length > maxLength) patch_filename.Text = patch_filename.Text.Substring(0, maxLength - 3) + "...";

            rom_filename.Enabled = !string.IsNullOrWhiteSpace(rom?.FilePath);
            patch_filename.Enabled = !string.IsNullOrWhiteSpace(patch);

            hasImage.Image = (img?.Source != null) ? Properties.Resources.yes : Properties.Resources.no;
        }

        private void randomTID()
        {
            title_id_upper.Text = TIDCode != null ? TIDCode + GenerateTitleID().Substring(0, 3) : GenerateTitleID();
            refreshData();
        }

        public string GetName(bool full)
        {
            string FILENAME = File.Exists(patch) ? Path.GetFileNameWithoutExtension(patch) : Path.GetFileNameWithoutExtension(rom?.FilePath);
            string CHANNELNAME = channel_title.Text;
            string FULLNAME = System.Text.RegularExpressions.Regex.Replace(_bannerTitle.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "").Replace("\r\n", "\n").Replace("\n", " - ");
            string TITLEID = title_id_upper.Text.ToUpper();
            string PLATFORM = targetPlatform.ToString();

            string target = full ? Properties.Settings.Default.default_export_filename : Properties.Settings.Default.default_save_as_filename;

            return target.Replace("FILENAME", FILENAME).Replace("CHANNELNAME", CHANNELNAME).Replace("FULLNAME", FULLNAME).Replace("TITLEID", TITLEID).Replace("PLATFORM", PLATFORM);
        }

        private void isClosing(object sender, FormClosingEventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            e.Cancel = !CheckUnsaved();

            if (!e.Cancel)
            {
                rom = null;
                channels = null;

                if (img != null) img.Dispose();
                img = null;

                if (contentOptionsForm != null) contentOptionsForm.Dispose();
                contentOptionsForm = null;

                preview.Dispose();
                preview = null;
            }
        }

        public bool CheckUnsaved()
        {
            if (IsModified)
            {
                var result = MessageBox.Show(string.Format(Program.Lang.Msg(1), Text), null, new string[] { Program.Lang.String("b_save"), Program.Lang.String("b_dont_save"), Program.Lang.String("b_cancel") });
                {
                    if (result == MessageBox.Result.Button1)
                    {
                        return Program.MainForm.SaveAs_Trigger();
                    }

                    else if (result == MessageBox.Result.Button2)
                    {
                        IsModified = false;
                        return true;
                    }

                    else if (result == MessageBox.Result.Cancel || result == MessageBox.Result.Button3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Random_Click(object sender, EventArgs e) => randomTID();

        private void Value_Changed(object sender, EventArgs e)
        {
            if (sender == released || sender == players) resetImages(true);
            refreshData();
        }

        private void linkSaveDataTitle()
        {
            if (autolink_save_data.Checked && autolink_save_data.Enabled && autolink_save_data.Visible)
            {
                string[] lines = new string[2];
                int limit = save_data_title.Multiline ? save_data_title.MaxLength / 2 : save_data_title.MaxLength;
                if (channel_title.TextLength <= limit) lines[0] = channel_title.Text;
                if (banner_title.Lines.Length > 1 && banner_title.Lines[1].Length <= limit) lines[1] = banner_title.Lines[1];

                if (string.Join("\n", lines).Length > save_data_title.MaxLength) return;

                save_data_title.Text
                    = string.IsNullOrWhiteSpace(lines[1]) ? lines[0]
                    : string.IsNullOrWhiteSpace(lines[0]) ? lines[1]
                    : string.IsNullOrWhiteSpace(lines[0]) && string.IsNullOrWhiteSpace(lines[1]) ? null
                    : save_data_title.Multiline ? string.Join(Environment.NewLine, lines) : lines[0];

                refreshData();
            }
        }

        private void LinkSaveData_Changed(object sender, EventArgs e)
        {
            if (sender == autolink_save_data)
            {
                save_data_title.Enabled = !autolink_save_data.Checked;
                linkSaveDataTitle();
            }
        }

        private void TextBox_Changed(object sender, EventArgs e)
        {
            if (sender == channel_title)
                Text = string.IsNullOrWhiteSpace(channel_title.Text) ? Untitled : channel_title.Text;

            if (sender == banner_title || sender == channel_title) linkSaveDataTitle();
            if (sender == banner_title) resetImages(true);

            var currentSender = sender as TextBox;
            if (currentSender.Multiline && currentSender.Lines.Length > 2) currentSender.Lines = new string[] { currentSender.Lines[0], currentSender.Lines[1] };

            refreshData();
        }

        private void TextBox_Handle(object sender, KeyPressEventArgs e)
        {
            var currentSender = sender as TextBox;
            var currentIndex = currentSender.GetLineFromCharIndex(currentSender.SelectionStart);
            var lineMaxLength = currentSender.Multiline ? Math.Round((double)currentSender.MaxLength / 2) : currentSender.MaxLength;

            if (!string.IsNullOrEmpty(currentSender.Text)
                && currentSender.Lines[currentIndex].Length >= lineMaxLength
                && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)8 && e.KeyChar != (char)Keys.Enter)
                goto Handled;

            if (currentSender.Multiline && currentSender.Lines.Length == 2 && e.KeyChar == (char)Keys.Enter) goto Handled;

            return;

            Handled:
            System.Media.SystemSounds.Beep.Play();
            e.Handled = true;
        }

        private void OpenWAD_CheckedChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            Base.Enabled = BaseRegion.Enabled = !import_wad_from_file.Checked;
            if (Base.Enabled)
            {
                AddBases();
            }
            else
            {
                BaseRegion.Image = null;
            }

            if (import_wad_from_file.Checked && WADPath == null)
            {
                browseInputWad.Title = import_wad_from_file.Text;
                browseInputWad.Filter = Program.Lang.String("filter.wad");
                var result = browseInputWad.ShowDialog();

                if (result == DialogResult.OK && !LoadWAD(browseInputWad.FileName)) import_wad_from_file.Checked = false;
                else if (result == DialogResult.Cancel) import_wad_from_file.Checked = false;
            }

            if (!import_wad_from_file.Checked)
            {
                WADPath = null;
            }

            refreshData();
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (imageintpl.SelectedIndex != Properties.Settings.Default.image_interpolation) refreshData();
            LoadImage();
        }

        private void SwitchAspectRatio(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (sender == image_stretch || sender == image_fit)
            {
                if (sender == image_stretch && image_fit.Checked)
                {
                    image_fit.Checked = !image_stretch.Checked;
                }

                else if (sender == image_fit && image_stretch.Checked)
                {
                    image_stretch.Checked = !image_fit.Checked;
                }

                LoadImage();
            }

            if (sender == forwarder_sd || sender == forwarder_usb)
            {
                refreshData();
            }
        }

        #region Load Data Functions
        private string GenerateTitleID()
        {
            var r = new Random();
            string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(allowed, 4).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        public bool LoadWAD(string path)
        {
            WAD Reader = new WAD();

            try
            {
                if (Directory.Exists(Paths.WAD)) Directory.Delete(Paths.WAD, true);
                Reader = WAD.Load(path);
            }
            catch
            {
                goto Failed;
            }

            for (int h = 0; h < channels.Entries.Count; h++)
                for (int i = 0; i < channels.Entries[h].Regions.Count; i++)
                    if (channels.Entries[h].GetUpperID(i) == Reader.UpperTitleID.ToUpper())
                    {
                        WADPath = path;

                        Base.SelectedIndex = h;
                        UpdateBaseForm(i);
                        Reader.Dispose();
                        return true;
                    }

            Failed:
            Reader.Dispose();
            System.Media.SystemSounds.Beep.Play();
            MessageBox.Show(string.Format(Program.Lang.Msg(5), Reader.UpperTitleID));
            return false;
        }

        protected void LoadManual(int index, string path = null, bool isFolder = true)
        {
            bool failed = false;

            #region Load manual as ZIP file (if exists)
            if (File.Exists(path) && !isFolder)
            {
                using (ZipFile ZIP = ZipFile.Read(path))
                {
                    int applicable = 0;
                    // bool hasFolder = false;

                    foreach (var item in ZIP.Entries)
                    {
                        // Check if is a valid emanual contents folder
                        // ****************
                        // if ((item.FileName == "emanual" || item.FileName == "html") && item.IsDirectory)
                        //    hasFolder = true;

                        // Check key files
                        // ****************
                        /* else */
                        if ((item.FileName.StartsWith("startup") && Path.GetExtension(item.FileName) == ".html")
                          || item.FileName == "standard.css"
                          || item.FileName == "contents.css"
                          || item.FileName == "vsscript.css")
                            applicable++;
                    }

                    if (applicable >= 2 /* && hasFolder */)
                    {
                        manual = path;
                        goto End;
                    }

                    failed = true;
                }
            }
            #endregion

            #region Load manual as folder/directory (if exists)
            else if (Directory.Exists(path) && isFolder)
            {
                // Check if is a valid emanual contents folder
                // ****************
                string folder = path;
                if (Directory.Exists(Path.Combine(path, "emanual")))
                    folder = Path.Combine(path, "emanual");
                else if (Directory.Exists(Path.Combine(path, "html")))
                    folder = Path.Combine(path, "html");

                int validFiles = 0;
                if (folder != null)
                    foreach (var item in Directory.EnumerateFiles(folder))
                    {
                        if ((Path.GetFileNameWithoutExtension(item).StartsWith("startup") && Path.GetExtension(item) == ".html")
                         || Path.GetFileName(item) == "standard.css"
                         || Path.GetFileName(item) == "contents.css"
                         || Path.GetFileName(item) == "vsscript.css") validFiles++;
                    }

                if (validFiles >= 2)
                {
                    manual = path;
                    goto End;
                }

                failed = true;
            }
            #endregion

            else
            {
                manual = null;
                goto End;
            }

            End:
            if (failed)
            {
                MessageBox.Show(Program.Lang.Msg(7), MessageBox.Buttons.Ok, MessageBox.Icons.Warning);
                manual = null;
            }

            manual_type_list.SelectedIndex = manual == null && index >= 2 ? 0 : manual != null && index < 2 ? 2 : index;
        }

        protected void LoadImage()
        {
            if (img != null) LoadImage(img.Source);
            else refreshData();
        }

        protected void LoadImage(string path)
        {
            img = new ImageHelper(targetPlatform, path);
            LoadImage(img.Source);
        }

        #region /////////////////////////////////////////////// Inheritable functions ///////////////////////////////////////////////
        /// <summary>
        /// Additionally edit image before generating files, e.g. with modification of image palette/brightness, used only for images with exact resolution of original screen size
        /// </summary>
        // protected abstract void platformImageFunction(Bitmap src);

        protected void platformImageFunction(Bitmap src)
        {
            Bitmap bmp = null;

            switch (targetPlatform)
            {
                case Platform.NES:
                    bmp = cloneImage(src);
                    if (bmp == null) return;

                    if (contentOptions != null && bool.Parse(contentOptions.ElementAt(1).Value))
                    {
                        var contentOptionsNES = contentOptionsForm as Options_VC_NES;
                        var palette = contentOptionsNES.CheckPalette(src);

                        if (palette != -1 && src.Width == 256 && (src.Height == 224 || src.Height == 240))
                            bmp = contentOptionsNES.SwapColors(bmp, contentOptionsNES.Palettes[palette], contentOptionsNES.Palettes[int.Parse(contentOptions.ElementAt(0).Value)]);
                    }
                    else bmp = src;
                    break;

                case Platform.SMS:
                case Platform.SMD:
                    break;
            }

            img.Generate(bmp ?? src);
        }

        private Bitmap cloneImage(Bitmap src)
        {
            try { return (Bitmap)src.Clone(); } catch { try { return (Bitmap)img?.Source.Clone(); } catch { return null; } }
        }
        #endregion

        private void resetImages(bool bannerOnly = false)
        {
            bannerPreview.Image = preview.Banner
                (
                    img?.VCPic,
                    banner_title.Text,
                    (int)released.Value,
                    (int)players.Value,
                    targetPlatform,
                    _bannerRegion
                );

            if (!bannerOnly)
            {
                SaveIcon_Panel.Image = img?.SaveIcon();
            }
        }

        protected bool LoadImage(Bitmap src)
        {
            if (src == null) return false;

            try
            {
                img.InterpMode = (InterpolationMode)imageintpl.SelectedIndex;
                img.FitAspectRatio = image_fit.Checked;

                platformImageFunction(src);

                if (img.Source != null)
                {
                    resetImages();
                    refreshData();
                }

                return true;
            }

            catch
            {
                MessageBox.Show(Program.Lang.Msg(1, true));
                return false;
            }
        }

        protected void LoadROM(string ROMpath, bool LoadGameData = true)
        {
            if (ROMpath == null || rom == null || !File.Exists(ROMpath)) return;

            switch (targetPlatform)
            {
                // ROM file formats
                // ****************
                default:
                    if (!rom.CheckValidity(ROMpath))
                    {
                        MessageBox.Show(Program.Lang.Msg(2), 0, MessageBox.Icons.Warning);
                        return;
                    }
                    break;

                // ZIP format
                // ****************
                case Platform.NEO:
                    if (!rom.CheckZIPValidity(new string[] { "c1", "c2", "m1", "p1", "s1", "v1" }, true, true, ROMpath))
                    {
                        MessageBox.Show(Program.Lang.Msg(2), 0, MessageBox.Icons.Warning);
                        return;
                    }
                    break;

                // Disc format
                // ****************
                case Platform.PSX:
                    break;

                // RPG Maker format
                // ****************
                case Platform.RPGM:
                    if ((rom as RPGM).GetTitle(ROMpath) != null)
                    {
                        banner_title.Text = (rom as RPGM).GetTitle(ROMpath);
                        if (_bannerTitle.Length <= channel_title.MaxLength) channel_title.Text = banner_title.Text;
                    }
                    break;

                // Other, no verification needed
                // ****************
                case Platform.Flash:
                    break;
            }

            rom.FilePath = ROMpath;

            randomTID();
            patch = null;

            Program.MainForm.toolbarRetrieveGameData.Enabled = Program.MainForm.menuItem10.Enabled = ToolbarButtons[0];
            if (rom != null && LoadGameData && ToolbarButtons[0]) this.LoadGameData();
            setFilesText();
        }

        public async void LoadGameData()
        {
            if (rom == null || rom.FilePath == null) return;

            try
            {
                var gameData = await Task.FromResult(rom.GetData(targetPlatform, rom.FilePath));
                bool retrieved = gameData != (null, null, null, null, null);

                if (retrieved)
                {
                    // Set banner title
                    banner_title.Text = rom.CleanTitle ?? banner_title.Text;

                    // Set channel title text
                    if (rom.CleanTitle != null)
                    {
                        var text = rom.CleanTitle.Replace("\r", "").Split('\n');
                        if (text[0].Length <= channel_title.MaxLength) { channel_title.Text = text[0]; }
                    }

                    // Set image
                    if (gameData.Image != null)
                    {
                        LoadImage(gameData.Image);
                    }

                    // Set year and players
                    released.Value = !string.IsNullOrEmpty(gameData.Year) ? int.Parse(gameData.Year) : released.Value;
                    players.Value = !string.IsNullOrEmpty(gameData.Players) ? int.Parse(gameData.Players) : players.Value;
                }

                if (retrieved && autolink_save_data.Checked) linkSaveDataTitle();
                else if (rom.CleanTitle != null && channel_title.TextLength <= save_data_title.MaxLength) save_data_title.Text = channel_title.Text;

                // Show message if partially failed to retrieve data
                if (retrieved && (gameData.Title == null || gameData.Players == null || gameData.Year == null || gameData.Image == null))
                    MessageBox.Show(Program.Lang.Msg(4));
                else if (!retrieved) System.Media.SystemSounds.Beep.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Error(ex.Message);
            }
        }

        public bool SaveToWAD(string targetFile = null)
        {
            if (targetFile == null) targetFile = Paths.WorkingFolder + "out.wad";

            try
            {
                Program.MainForm.CleanTemp();

                // Get WAD data
                // *******
                outWad = new WAD();
                if (WADPath != null) outWad = WAD.Load(WADPath);
                else foreach (var entry in channels.Entries)
                        for (int i = 0; i < entry.Regions.Count; i++)
                            if (entry.GetUpperID(i) == baseID.Text.ToUpper()) outWad = entry.GetWAD(i);
                if (outWad == null || outWad?.NumOfContents <= 1) throw new Exception(Program.Lang.Msg(8, true));

                if (File.Exists(patch)) rom.Patch(patch);

                switch (targetPlatform)
                {
                    case Platform.NES:
                    case Platform.SNES:
                    case Platform.N64:
                    case Platform.SMS:
                    case Platform.SMD:
                    case Platform.PCE:
                    case Platform.PCECD:
                    case Platform.NEO:
                    case Platform.MSX:
                        if (IsVC)
                            WiiVCInject();
                        else
                            ForwarderCreator(targetFile);
                        break;

                    case Platform.GB:
                    case Platform.GBC:
                    case Platform.GBA:
                    case Platform.S32X:
                    case Platform.SMCD:
                    case Platform.PSX:
                    case Platform.RPGM:
                        ForwarderCreator(targetFile);
                        break;

                    case Platform.Flash:
                        FlashInject();
                        break;

                    default:
                        throw new NotImplementedException();
                }

                // Banner
                // *******
                BannerHelper.Modify
                (
                    outWad,
                    targetPlatform,
                    IsVC ? outWad.Region : _bannerRegion switch { 1 => libWiiSharp.Region.Japan, 2 => libWiiSharp.Region.Korea, 3 => libWiiSharp.Region.Europe, _ => libWiiSharp.Region.USA },
                    _bannerTitle,
                    _bannerYear,
                    _bannerPlayers
                );
                SoundHelper.ReplaceSound(outWad, Properties.Resources.Sound_WiiVC);
                if (img.VCPic != null) img.ReplaceBanner(outWad);

                // Change WAD region & internal main.dol things
                // *******
                if (region_list.SelectedIndex > 0)
                    outWad.Region = outWadRegion;
                Utils.ChangeVideoMode(outWad, video_modes.SelectedIndex);

                // Other WAD settings to be changed done by WAD creator helper, which will save to a new file
                // *******
                outWad.ChangeChannelTitles(_channelTitles);
                outWad.ChangeTitleID(LowerTitleID.Channel, _tID);
                outWad.FakeSign = true;

                if (Directory.Exists(Paths.SDUSBRoot))
                {
                    Directory.CreateDirectory(Paths.SDUSBRoot + "wad\\");
                    outWad.Save(Paths.SDUSBRoot + "wad\\" + Path.GetFileNameWithoutExtension(targetFile) + ".wad");

                    // Get ZIP directory path & compress to .ZIP archive
                    // *******
                    if (File.Exists(targetFile)) File.Delete(targetFile);

                    using (ZipFile z = new ZipFile(targetFile))
                    {
                        z.AddDirectory(Paths.SDUSBRoot, "");
                        z.Save();
                    }

                    // Clean
                    // *******
                    Directory.Delete(Paths.SDUSBRoot, true);
                }
                else outWad.Save(targetFile);

                outWad.Dispose();

                // Check new WAD file
                // *******
                if (File.Exists(targetFile) && File.ReadAllBytes(targetFile).Length > 10)
                {
                    System.Media.SystemSounds.Beep.Play();

                    switch (MessageBox.Show(Program.Lang.Msg(3), null, MessageBox.Buttons.Custom, MessageBox.Icons.Information))
                    {
                        case MessageBox.Result.Button1:
                            System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{targetFile}\"");
                            break;
                    }

                    return true;
                }
                else throw new Exception(Program.Lang.Msg(6, true));
            }

            catch (Exception ex)
            {
                MessageBox.Error(ex.Message);
                return false;
            }

            finally
            {
                Program.MainForm.CleanTemp();
            }
        }

        public void ForwarderCreator(string path)
        {
            Forwarder f = new Forwarder()
            {
                ROM = rom.FilePath,
                ID = _tID,
                Emulator = injection_methods.SelectedItem.ToString(),
                Storage = forwarder_usb.Checked ? Forwarder.Storages.USB : Forwarder.Storages.SD,
                Name = _channelTitles[1]
            };

            // Get settings from relevant form
            // *******
            f.Settings = contentOptions;

            // Actually inject everything
            // *******
            f.CreateZIP(Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + $" ({f.Storage}).zip"));
            outWad = f.CreateWAD(outWad);
        }
        #endregion

        #region /////////////////////////////////////////////// To inherit ///////////////////////////////////////////////
        public void FlashInject()
        {
            Injectors.Flash.Settings = contentOptions;
            outWad = Injectors.Flash.Inject(outWad, rom.FilePath, _saveDataTitle, img);
        }

        public void WiiVCInject()
        {
            // Create Wii VC injector to use
            // *******
            InjectorWiiVC VC = null;

            switch (targetPlatform)
            {
                default:
                    throw new NotImplementedException();

                // NES
                // *******
                case Platform.NES:
                    VC = new Injectors.NES();
                    break;

                // SNES
                // *******
                case Platform.SNES:
                    VC = new Injectors.SNES();
                    break;

                // N64
                // *******
                case Platform.N64:
                    VC = new Injectors.N64()
                    {
                        Settings = contentOptions,

                        CompressionType = emuVer == 3 ? (contentOptions["romc"] == "0" ? 1 : 2) : 0,
                        Allocate = contentOptions["rom_autosize"] == "True" && (emuVer <= 1),
                    };
                    break;

                // SEGA
                // *******
                case Platform.SMS:
                case Platform.SMD:
                    VC = new Injectors.SEGA()
                    {
                        IsSMS = targetPlatform == Platform.SMS
                    };
                    break;

                // PCE
                // *******
                case Platform.PCE:
                    VC = new Injectors.PCE();
                    break;

                // PCECD
                // *******
                case Platform.PCECD:
                    // VC = new Injectors.PCECD();
                    break;

                // NEOGEO
                // *******
                case Platform.NEO:
                    VC = new Injectors.NEO();
                    break;

                // MSX
                // *******
                case Platform.MSX:
                    VC = new Injectors.MSX();
                    break;
            }

            // Get settings from relevant form
            // *******
            VC.Settings = contentOptions;

            // Set path to manual (if it exists) and load WAD
            // *******
            VC.RetainOriginalManual = manual_type_list.SelectedIndex == 1;
            VC.ManualFile = manual != null ? new Ionic.Zip.ZipFile("manual") : null;
            if (VC.ManualFile != null) VC.ManualFile.AddDirectory(manual);

            // Actually inject everything
            // *******
            outWad = VC.Inject(outWad, rom, _saveDataTitle, img);
        }
        #endregion

        #region **Console-Specific Functions**
        // ******************
        // CONSOLE-SPECIFIC
        // ******************
        private void openInjectorOptions(object sender, EventArgs e)
        {
            var result = contentOptionsForm.ShowDialog(this) == DialogResult.OK;

            switch (targetPlatform)
            {
                default:
                    if (result) { refreshData(); }
                    break;

                case Platform.NES:
                    if (result) { LoadImage(); }
                    break;
            }
        }
        #endregion

        #region Base WAD Management/Visual
        private void AddBases()
        {
            Base.Items.Clear();

            foreach (var entry in channels.Entries)
            {
                var title = entry.Regions.Contains(0) && Program.Lang.Current.StartsWith("ja") ? entry.Titles[0]
                          : entry.Regions.Contains(0) && Program.Lang.Current.StartsWith("ko") ? entry.Titles[entry.Titles.Count - 1]
                          : entry.Regions.Contains(0) && entry.Regions.Count > 1 ? entry.Titles[1]
                          : entry.Titles[0];

                Base.Items.Add(title);
            }

            if (Base.Items.Count > 0) { Base.SelectedIndex = 0; }

            Base.Enabled = Base.Items.Count > 1;
            UpdateBaseForm();
        }


        // -----------------------------------

        private void Base_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode || Base.SelectedIndex < 0) return;
            // ----------------------------

            var regions = new List<string>();
            for (int i = 0; i < channels.Entries[Base.SelectedIndex].Regions.Count; i++)
            {
                switch (channels.Entries[Base.SelectedIndex].Regions[i])
                {
                    case 0:
                        regions.Add(Program.Lang.String("region_j"));
                        break;

                    case 1:
                    case 2:
                        regions.Add(Program.Lang.String("region_u"));
                        break;

                    case 3:
                    case 4:
                    case 5:
                        regions.Add(Program.Lang.String("region_e"));
                        break;

                    case 6:
                    case 7:
                        regions.Add(Program.Lang.String("region_k"));
                        break;

                    default:
                    case 8:
                        regions.Add(Program.Lang.String("region_rf"));
                        break;
                }
            }

            // Check if language is set to Japanese or Korean
            // If so, make Japan/Korea region item the first in the WAD region context list
            // ********
            var selected = regions.IndexOf(Program.Lang.String("region_u"));

            var altRegions = new Dictionary<string, int>()
            {
                { "ja", 0 },
                { "ko", 1 },
                { "fr", 2 },
                { "de", 2 },
                { "nl", 2 },
                { "it", 2 },
                { "pl", 2 },
                { "ru", 2 },
                { "uk", 2 },
                { "tr", 2 },
                { "hu", 2 },
                { "ca", 2 },
                { "eu", 2 },
                { "gl", 2 },
                { "ast", 2 },
                { "no", 2 },
                { "sv", 2 },
                { "fi", 2 },
                { "-GB", 2 },
                { "-UK", 2 },
                { "-ES", 2 },
                { "-PT", 2 },
                { "-RU", 2 },
                { "-IN", 2 },
                { "-ZA", 2 },
                { "-CA", 3 },
                { "-US", 3 },
                { "-MX", 3 },
                { "-BR", 3 },
            };

            foreach (var item in altRegions) if (Program.Lang.Current.ToLower().StartsWith(item.Key) || (item.Key.Contains("-") && Program.Lang.Current.ToLower().EndsWith(item.Key)))
            {
                selected = regions.IndexOf(item.Value == 0 ? Program.Lang.String("region_j")
                         : item.Value == 1 ? Program.Lang.String("region_k")
                         : item.Value == 2 ? Program.Lang.String("region_e")
                         : Program.Lang.String("region_u"));

                if (selected == -1 && item.Value == 1) selected = regions.IndexOf(Program.Lang.String("region_u"));
                break;
            }

            if (selected == -1) selected = 0;

            // Reset currently-selected base info
            // ********
            baseRegionList.Items.Clear();

            // Add regions to WAD region context list
            // ********
            for (int i = 0; i < channels.Entries[Base.SelectedIndex].Regions.Count; i++)
            {
                switch (channels.Entries[Base.SelectedIndex].Regions[i])
                {
                    case 0:
                        baseRegionList.Items.Add(Program.Lang.String("region_j"), null, WADRegionList_Click);
                        break;

                    case 1:
                    case 2:
                        baseRegionList.Items.Add(Program.Lang.String("region_u"), null, WADRegionList_Click);
                        break;

                    case 3:
                    case 4:
                    case 5:
                        baseRegionList.Items.Add(Program.Lang.String("region_e"), null, WADRegionList_Click);
                        break;

                    case 6:
                    case 7:
                        baseRegionList.Items.Add(Program.Lang.String("region_k"), null, WADRegionList_Click);
                        break;

                    default:
                    case 8:
                        baseRegionList.Items.Add(Program.Lang.String("region_rf"), null, WADRegionList_Click);
                        break;
                }
            }

            // Final visual updates
            // ********
            UpdateBaseForm(selected);
            BaseRegion.Cursor = baseRegionList.Items.Count == 1 ? Cursors.Default : Cursors.Hand;
        }

        private void WADRegion_Click(object sender, EventArgs e)
        {
            if (baseRegionList.Items.Count > 1)
                baseRegionList.Show(this, PointToClient(Cursor.Position));
        }

        private void WADRegionList_Click(object sender, EventArgs e)
        {
            string targetRegion = (sender as ToolStripMenuItem).Text;

            for (int i = 0; i < baseRegionList.Items.Count; i++)
            {
                if ((baseRegionList.Items[i] as ToolStripMenuItem).Text == targetRegion)
                {
                    UpdateBaseForm(i);
                    refreshData();
                    return;
                }
            }
        }

        private void UpdateBaseForm(int index = -1)
        {
            if (index == -1)
            {
                for (index = 0; index < channels.Entries[Base.SelectedIndex].Regions.Count; index++)
                    if (channels.Entries[Base.SelectedIndex].GetUpperID(index)[3] == baseID.Text[3])
                        goto Set;

                return;
            }

            Set:
            // Native name & Title ID
            // ********
            baseName.Text = channels.Entries[Base.SelectedIndex].Titles[index];
            baseID.Text = channels.Entries[Base.SelectedIndex].GetUpperID(index);

            if (baseRegionList.Items.Count > 0)
            {
                foreach (ToolStripMenuItem item in baseRegionList.Items.OfType<ToolStripMenuItem>())
                    item.Checked = false;
                (baseRegionList.Items[index] as ToolStripMenuItem).Checked = true;
            }

            // Flag
            // ********
            switch (channels.Entries[Base.SelectedIndex].Regions[index])
            {
                case 0:
                    BaseRegion.Image = Properties.Resources.flag_jp;
                    break;

                case 1:
                case 2:
                    BaseRegion.Image = Properties.Resources.flag_us;
                    break;

                case 3:
                    BaseRegion.Image = (int)targetPlatform <= 2 ? Properties.Resources.flag_eu50 : Properties.Resources.flag_eu;
                    break;

                case 4:
                case 5:
                    BaseRegion.Image = (int)targetPlatform <= 2 ? Properties.Resources.flag_eu60 : Properties.Resources.flag_eu;
                    break;

                case 6:
                case 7:
                    BaseRegion.Image = Properties.Resources.flag_kr;
                    break;

                default:
                    BaseRegion.Image = null;
                    break;
            }

            #region Save data text handling
            int oldSaveLength = save_data_title.MaxLength;

            // Changing SaveDataTitle max length & clearing text field when needed
            // ----------------------
            if (targetPlatform == Platform.NES) save_data_title.MaxLength = inWadRegion == Region.Korea ? 30 : 20;
            else if (targetPlatform == Platform.SNES) save_data_title.MaxLength = 80;
            else if (targetPlatform == Platform.N64) save_data_title.MaxLength = 100;
            else if (targetPlatform == Platform.NEO
                  || targetPlatform == Platform.MSX) save_data_title.MaxLength = 64;
            else save_data_title.MaxLength = 80;

            // Also, some consoles only support a single line anyway
            // ********
            bool isSingleLine = inWadRegion == Region.Korea
                             || targetPlatform == Platform.NES
                             || targetPlatform == Platform.SMS
                             || targetPlatform == Platform.SMD
                             || targetPlatform == Platform.PCE
                             || targetPlatform == Platform.PCECD;

            // Set textbox to use single line when needed
            // ********
            if (save_data_title.Multiline == isSingleLine)
            {
                save_data_title.Multiline = !isSingleLine;
                save_data_title.Location = isSingleLine ? new Point(save_data_title.Location.X, int.Parse(save_data_title.Tag.ToString()) + 8) : new Point(save_data_title.Location.X, int.Parse(save_data_title.Tag.ToString()));
                save_data_title.Clear();
                goto End;
            }
            if (inWadRegion == Region.Korea && save_data_title.Multiline) save_data_title.MaxLength /= 2; // Applies to both NES/FC & SNES/SFC

            // Clear text field if at least one line is longer than the maximum limit allowed
            // ********
            double max = save_data_title.Multiline ? Math.Round((double)save_data_title.MaxLength / 2) : save_data_title.MaxLength;
            foreach (var line in save_data_title.Lines)
                if (line.Length > max && save_data_title.MaxLength != oldSaveLength)
                    save_data_title.Clear();
            #endregion

            End:
            resetImages();
            linkSaveDataTitle();
            resetContentOptions();
            refreshData();
        }

        private int emuVer
        {
            get
            {
                if (channels != null)
                    foreach (var entry in channels.Entries)
                        for (int i = 0; i < entry.Regions.Count; i++)
                            if (entry.GetUpperID(i) == baseID.Text.ToUpper())
                                return entry.EmuRevs[i];

                return 0;
            }
        }

        /// <summary>
        /// Changes injector settings based on selected base/console
        /// </summary>
        private void resetContentOptions()
        {
            manual_type.Visible = false;
            forwarder_root_device.Visible = false;
            contentOptionsForm = null;

            if (IsVC)
            {
                manual_type.Visible = true;

                switch (targetPlatform)
                {
                    case Platform.NES:
                        contentOptionsForm = new Options_VC_NES() { EmuType = emuVer };
                        break;

                    case Platform.SNES:
                        break;

                    case Platform.N64:
                        contentOptionsForm = new Options_VC_N64() { EmuType = inWadRegion == Region.Korea ? 3 : emuVer };
                        break;

                    case Platform.SMS:
                    case Platform.SMD:
                        contentOptionsForm = new Options_VC_SEGA() { EmuType = emuVer, IsSMS = targetPlatform == Platform.SMS };
                        break;

                    case Platform.PCE:
                    case Platform.PCECD:
                        contentOptionsForm = new Options_VC_PCE();
                        break;

                    case Platform.NEO:
                        contentOptionsForm = new Options_VC_NEO() { EmuType = emuVer };
                        break;

                    case Platform.MSX:
                        break;

                    case Platform.C64:
                        break;
                }
            }

            else if (targetPlatform == Platform.Flash)
            {
                contentOptionsForm = new Options_Flash();
            }

            else
            {
                forwarder_root_device.Visible = true;

                switch (targetPlatform)
                {
                    case Platform.GB:
                    case Platform.GBC:
                    case Platform.GBA:
                    case Platform.S32X:
                    case Platform.SMCD:
                    case Platform.PSX:
                        contentOptionsForm = new Options_Forwarder(targetPlatform);
                        break;
                    case Platform.NES:
                        break;
                    case Platform.SNES:
                        break;
                    case Platform.N64:
                        break;
                    case Platform.SMS:
                        break;
                    case Platform.SMD:
                        break;
                    case Platform.PCE:
                        break;
                    case Platform.PCECD:
                        break;
                    case Platform.NEO:
                        break;
                    case Platform.MSX:
                        break;
                    case Platform.C64:
                        break;
                    case Platform.Flash:
                        break;
                    case Platform.RPGM:
                        contentOptionsForm = new Options_RPGM();
                        break;
                    default:
                        break;
                }
            }

            if (contentOptionsForm != null)
            {
                contentOptionsForm.Font = Font;
                contentOptionsForm.Text = Program.Lang.String("injection_method_options", "projectform");
                contentOptionsForm.Icon = Icon.FromHandle(Properties.Resources.wrench.GetHicon());
            }

            if (!IsVC && manual != null)
            {
                manual = null;
                manual_type_list.SelectedIndex = 0;
            }

            #region -- Content options panel --
            editContentOptions.Enabled = contentOptionsForm != null;
            #endregion

            showSaveData = IsVC || targetPlatform == Platform.Flash;
        }
        #endregion

        private void CustomManual_CheckedChanged(object sender, EventArgs e)
        {
            if (manual_type_list.Enabled && manual_type_list.SelectedIndex == 2 && manual == null)
            {
                if (!Properties.Settings.Default.donotshow_000) MessageBox.Show((sender as Control).Text, Program.Lang.Msg(6), 0);

                if (browseManual.ShowDialog() == DialogResult.OK) LoadManual(manual_type_list.SelectedIndex, browseManual.SelectedPath, true);
                else manual_type_list.SelectedIndex = 0;
            }

            if (manual_type_list.Enabled && manual_type_list.SelectedIndex < 2) LoadManual(manual_type_list.SelectedIndex);

            refreshData();
        }

        private void import_patch_CheckedChanged(object sender, EventArgs e)
        {
            if (browsePatch.ShowDialog() == DialogResult.OK)
            {
                patch = browsePatch.FileName;
                refreshData();
            }

            else if (patch != null)
            {
                patch = null;
                refreshData();
            }
        }

        private void InjectorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetImages();
            resetContentOptions();
            LoadImage();
            refreshData();
        }

        private void RegionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetImages();
            LoadImage();
            refreshData();
        }

        private void banner_preview_Click(object sender, EventArgs e)
        {
            using (Form f = new Form())
            {
                f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                f.ShowInTaskbar = false;
                f.Text = Program.Lang.String("banner_preview", Name);
                f.Icon = Icon;

                var p = new PictureBox() { Name = "picture" };
                p.SizeMode = PictureBoxSizeMode.AutoSize;
                p.Location = new Point(0, 0);

                p.Image = preview.Banner
                (
                    img?.VCPic,
                    banner_title.Text,
                    (int)released.Value,
                    (int)players.Value,
                    targetPlatform,
                    _bannerRegion
                );

                f.ClientSize = p.Image.Size;
                f.StartPosition = FormStartPosition.CenterParent;
                f.Controls.Add(p);
                f.ShowDialog();
            }
        }
    }
}
