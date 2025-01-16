using ICSharpCode.SharpZipLib.Zip;
using libWiiSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class ProjectForm : Form
    {
        protected Platform targetPlatform { get; set; }
        private readonly BannerOptions banner_form;
        private readonly Savedata savedata;
        private Wait wait;
        private HTMLForm htmlForm;

        protected string TIDCode;
        protected string Untitled;

        protected bool isVirtualConsole
        {
            get
            {
                bool value = false;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = injection_methods.SelectedItem?.ToString().ToLower() == Program.Lang.String("vc").ToLower(); }));
                else
                    value = injection_methods.SelectedItem?.ToString().ToLower() == Program.Lang.String("vc").ToLower();

                return value;
            }
        }
        public bool IsForwarder { get => !isVirtualConsole && targetPlatform != Platform.Flash; }

        protected bool showPatch = false;
        private bool _showSaveData;
        protected bool showSaveData
        {
            get => _showSaveData;
            set => edit_save_data.Enabled = _showSaveData = value;
        }
        private readonly bool _isShown;
        private bool _isMint;

        #region Public bools (for main form)
        public bool IsModified
        {
            get => _isModified;

            set
            {
                _isModified = value;
                if (value) _isMint = false;
                Program.MainForm.toolbarSave.Enabled = value;
                Program.MainForm.save_project.Enabled = value;
                Program.MainForm.toolbarSaveAs.Enabled = value;
                Program.MainForm.save_project_as.Enabled = value;
                Program.MainForm.toolbarExport.Enabled = IsExportable;
                Program.MainForm.export.Enabled = IsExportable;
            }
        }
        private bool _isModified;

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                groupBox1.Visible =
                groupBox2.Visible =
                groupBox3.Visible =
                groupBox4.Visible =
                groupBox5.Visible =
                groupBox6.Visible =
                _isVisible = value;
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
                    checkImg1.Visible =
                    title_id_random.Visible =
                    Enabled = !value;

                    include_patch.Enabled = !value && showPatch;
                    injection_method_help.Visible = !value && htmlForm != null;
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;

                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        if (!value) ParentForm.Select();

                        if (value)
                        {
                            wait = new();
                            wait.Show(this);
                        }
                        else
                            wait.Hide();

                        (ParentForm as MainForm).Enabled = Enabled = !value;
                    }));
                }

                else
                {
                    if (!value) ParentForm.Select();

                    if (value)
                    {
                        wait = new();
                        wait.Show(this);
                    }
                    else
                        wait.Hide();

                    (ParentForm as MainForm).Enabled = Enabled = !value;
                }
            }
        }

        public bool IsExportable
        {
            get
            {
                bool yes = !string.IsNullOrEmpty(_tID) && _tID.Length == 4
                            && !string.IsNullOrWhiteSpace(channel_name.Text)
                            && !string.IsNullOrEmpty(_bannerTitle)
                            && (img != null)
                            && rom?.FilePath != null
                            && ((use_online_wad.Checked) || (!use_online_wad.Checked && File.Exists(inWadFile)));

                if (!File.Exists(inWadFile) && !string.IsNullOrWhiteSpace(inWadFile))
                {
                    inWadFile = null;
                    refreshData();
                }

                return showSaveData ? yes && !string.IsNullOrEmpty(savedata.Lines[0]) : yes;
            }
        }

        public string ProjectPath { get; set; }
        #endregion

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
                ChannelDatabase.ChannelEntry channel = null;
                Region value = Region.America;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { channel = channels?.Entries[Base.SelectedIndex]; }));
                else channel = channels?.Entries[Base.SelectedIndex];

                for (int index = 0; index < channel.Regions.Count; index++)
                    if (channel.GetUpperID(index)[3] == baseID.Text[3])
                        value = channel.Regions[index] == 0 ? Region.Japan
                              : channel.Regions[index] == 6 || channel.Regions[index] == 7 ? Region.Korea
                              : channel.Regions[index] >= 3 && channel.Regions[index] <= 5 ? Region.Europe
                              : Region.America;

                return value;
            }
        }

        private Project project;

        private libWiiSharp.Region outWadRegion
        {
            get
            {
                string index = "";
                int indexNum = 0;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { index = regions.SelectedItem?.ToString(); indexNum = regions.SelectedIndex; }));
                else
                { index = regions.SelectedItem?.ToString(); indexNum = regions.SelectedIndex; }

                return index == Program.Lang.String("region_j") ? libWiiSharp.Region.Japan
                     : index == Program.Lang.String("region_u") ? libWiiSharp.Region.USA
                     : index == Program.Lang.String("region_e") ? libWiiSharp.Region.Europe
                     : index == Program.Lang.String("region_k") ? libWiiSharp.Region.Korea
                     : indexNum == 0 ? inWadRegion switch { Region.Japan => libWiiSharp.Region.Japan, Region.Korea => libWiiSharp.Region.Korea, Region.Europe => libWiiSharp.Region.Europe, Region.America => libWiiSharp.Region.USA, _ => libWiiSharp.Region.Free }
                     : libWiiSharp.Region.Free;
            }
        }

        protected ROM rom { get; set; }
        protected string patch { get; set; }
        protected string manual { get; set; }
        protected string sound { get; set; }
        protected ImageHelper img { get; set; }

        internal Preview preview = new();

        protected ContentOptions contentOptionsForm { get; set; }
        protected IDictionary<string, string> contentOptions { get => contentOptionsForm?.Options; }
        protected (bool Enabled, IDictionary<Buttons, string> List) keymap { get => contentOptionsForm != null ? (contentOptionsForm.UsesKeymap, contentOptionsForm.Keymap) : (false, null); }

        #region Channel/banner parameters
        private string _tID
        {
            get
            {
                string value = "";

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = title_id_upper.Text.ToUpper(); }));
                else
                    value = title_id_upper.Text.ToUpper();

                return value;
            }
        }
        private string[] _channelTitles
        {
            get
            {
                string[] value = new string[8];

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = new string[8] { channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text }; }));
                else
                    value = new string[8] { channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text, channel_name.Text };

                // DEFAULT: "無題", "Untitled", "Ohne Titel", "Sans titre", "Sin título", "Senza titolo", "Onbekend", "제목 없음"

                return value;
            }
        }
        private string _bannerTitle
        {
            get
            {
                string value = "";

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = banner_form.title.Text; }));
                else
                    value = banner_form.title.Text;

                return value;
            }
        }
        private int _bannerYear
        {
            get
            {
                int value = 0;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = (int)banner_form.released.Value; }));
                else
                    value = (int)banner_form.released.Value;

                return value;
            }
        }
        private int _bannerPlayers
        {
            get
            {
                int value = 0;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = (int)banner_form.players.Value; }));
                else
                    value = (int)banner_form.players.Value;

                return value;
            }
        }
        private string[] _saveDataTitle
        {
            get
            {
                string[] value = new string[2];

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = savedata.IsMultiline == false ? new string[] { savedata.Lines[0] } : savedata.Lines.Length == 0 ? new string[] { "" } : savedata.Lines; }));
                else
                    value = savedata.IsMultiline == false ? new string[] { savedata.Lines[0] } : savedata.Lines.Length == 0 ? new string[] { "" } : savedata.Lines;

                return value;
            }
        }

        private libWiiSharp.Region _bannerRegion
        {
            get
            {
                int value = 0;
                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { value = banner_form.region.SelectedIndex - 1; }));
                else value = banner_form.region.SelectedIndex - 1;

                if (value == -1)
                {
                    value = channels != null ? inWadRegion switch { Region.Japan => 0, Region.Europe => 2, Region.Korea => 3, _ => 1 } : 1;

                    if (!isVirtualConsole && Program.Lang.Current.StartsWith("ja"))
                        value = 0;

                    if (!isVirtualConsole && Program.Lang.Current.StartsWith("ko"))
                        value = 3;
                }

                // Japan/Korea: Use USA banner for C64 & Flash
                if (value != 1 && value != 2 && (targetPlatform == Platform.C64 || targetPlatform == Platform.Flash))
                    return libWiiSharp.Region.USA;

                // International: Use Japan banner for MSX
                else if (value != 0 && targetPlatform == Platform.MSX)
                    return libWiiSharp.Region.Japan;

                // Korea: Use Europe banner for SMD
                else if (value == 3 && targetPlatform == Platform.SMD)
                    return libWiiSharp.Region.Europe;

                // Korea: Use USA banner for non-available platforms
                else if (value == 3 && (int)targetPlatform >= 3)
                    return libWiiSharp.Region.USA;

                return value switch { 0 => libWiiSharp.Region.Japan, 2 => libWiiSharp.Region.Europe, 3 => libWiiSharp.Region.Korea, _ => libWiiSharp.Region.USA };
            }
        }
        #endregion


        // -----------------------------------

        public void SaveProject(string path)
        {
            ProjectPath = path;

            var p = new Project()
            {
                ProjectPath = path,

                Platform = targetPlatform,

                ROM = rom?.FilePath,
                Patch = patch,
                Manual = (manual_type.SelectedIndex, manual),
                Img = (img?.FilePath ?? null, img?.Source ?? null),
                ImageOptions = (image_interpolation_mode.SelectedIndex, image_resize1.Checked),
                Sound = sound,

                ContentOptions = contentOptions ?? null,
                Keymap = (keymap.Enabled, keymap.List ?? null),
                InjectionMethod = injection_methods.SelectedIndex,
                ForwarderStorageDevice = forwarder_root_device.SelectedIndex,
                IsMultifile = multifile_software.Checked,

                LinkSaveDataTitle = savedata.Fill.Checked,
                VideoMode = video_modes.SelectedIndex,

                TitleID = _tID,
                ChannelTitles = _channelTitles,
                BannerTitle = _bannerTitle,
                BannerYear = _bannerYear,
                BannerPlayers = _bannerPlayers,
                SaveDataTitle = savedata.Lines,

                WADRegion = regions.SelectedIndex,
            };

            p.OfflineWAD = inWadFile;
            p.OnlineWAD = (Base.SelectedIndex, 0);

            for (int i = 0; i < baseRegionList.Items.Count; i++)
                if (baseRegionList.Items[i].GetType() == typeof(ToolStripMenuItem) && (baseRegionList.Items[i] as ToolStripMenuItem).Checked) p.OnlineWAD = (Base.SelectedIndex, i);

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

            bool isMint = _isMint || !Program.MainForm.save_project_as.Enabled;

            #region ------------------------------------------ Localization: Controls ------------------------------------------
            Program.Lang.Control(this, "projectform");

            // File filters
            browsePatch.Filter = Program.Lang.String("filter.patch");
            // BrowseManualZIP.Filter = Program.Lang.String("filter.zip");

            // Banner menu
            banner_details.Text = Program.Lang.String(banner_details.Name, Name);
            banner_sound.Text = Program.Lang.String(banner_sound.Name, Name);
            play_banner_sound.Text = Program.Lang.String(play_banner_sound.Name, Name);
            replace_banner_sound.Text = Program.Lang.String(replace_banner_sound.Name, Name);
            restore_banner_sound.Text = Program.Lang.String(restore_banner_sound.Name, Name);

            // Change title text to untitled string
            Untitled = string.Format(Program.Lang.String("untitled_project", "mainform"), Program.Lang.String(Enum.GetName(typeof(Platform), targetPlatform).ToLower(), "platforms"));
            Text = string.IsNullOrWhiteSpace(channel_name.Text) ? Untitled : channel_name.Text;

            checkImg1.Location = new Point(import_wad.Location.X + import_wad.Width + 4, checkImg1.Location.Y);
            baseID.Location = new Point(current_wad.Location.X + current_wad.Width + 2, current_wad.Location.Y + 1);

            setFilesText();

            // Selected index properties
            Program.Lang.Control(image_interpolation_mode, Name);
            image_interpolation_mode.SelectedIndex = Program.Config.application.image_interpolation;

            // Manual
            manual_type.SelectedIndex = 0;
            manual = null;

            // Regions lists
            regions.Items.Clear();
            regions.Items.Add(Program.Lang.String("original"));
            regions.Items.Add(Program.Lang.String("region_rf"));
            regions.SelectedIndex = 0;

            // Video modes
            video_modes.Items[0] = Program.Lang.String("original");
            video_modes.SelectedIndex = 0;

            switch (Program.Lang.Current.ToLower())
            {
                default:
                    regions.Items.Add(Program.Lang.String("region_u"));
                    regions.Items.Add(Program.Lang.String("region_e"));
                    regions.Items.Add(Program.Lang.String("region_j"));
                    regions.Items.Add(Program.Lang.String("region_k"));
                    break;

                case "ja":
                    regions.Items.Add(Program.Lang.String("region_j"));
                    regions.Items.Add(Program.Lang.String("region_u"));
                    regions.Items.Add(Program.Lang.String("region_e"));
                    regions.Items.Add(Program.Lang.String("region_k"));
                    break;

                case "ko":
                    regions.Items.Add(Program.Lang.String("region_k"));
                    regions.Items.Add(Program.Lang.String("region_u"));
                    regions.Items.Add(Program.Lang.String("region_e"));
                    regions.Items.Add(Program.Lang.String("region_j"));
                    break;
            }
            #endregion

            #region ------------------------------------------ Localization: Tooltips ------------------------------------------
            (ParentForm as MainForm).Tip.SetToolTip(channel_name, Program.Lang.HTML(0, true, label1.Text));
            (ParentForm as MainForm).Tip.SetToolTip(injection_method_options, Program.Lang.HTML(3, true, injection_method_options.Text));
            (ParentForm as MainForm).Tip.SetToolTip(multifile_software, Program.Lang.HTML(4, true, multifile_software.Text));
            #endregion

            if (Base.SelectedIndex >= 0)
                for (int i = 0; i < channels.Entries[Base.SelectedIndex].Regions.Count; i++)
                {
                    baseRegionList.Items[i].Text = channels.Entries[Base.SelectedIndex].Regions[i] switch
                    {
                        1 or 2 => Program.Lang.String("region_u"),
                        3 or 4 or 5 => Program.Lang.String("region_e"),
                        6 or 7 => Program.Lang.String("region_k"),
                        _ => Program.Lang.String("region_j"),
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
                Platform.NES => Program.Config.application.default_injection_method_nes,
                Platform.SNES => Program.Config.application.default_injection_method_snes,
                Platform.N64 => Program.Config.application.default_injection_method_n64,
                Platform.SMS or Platform.SMD => Program.Config.application.default_injection_method_sega,
                _ => 0
            };
            injection_methods.Enabled = injection_methods.Items.Count > 1;
            banner_form.released.Maximum = DateTime.Now.Year;

            image_resize1.Checked = Program.Config.application.image_fit_aspect_ratio;
            image_resize0.Checked = !image_resize1.Checked;
            resetImages();
            if (isMint && IsModified) IsModified = false;
        }

        private void LoadChannelDatabase()
        {
            try { channels = new ChannelDatabase(targetPlatform); }
            catch (Exception ex)
            {
                if ((int)targetPlatform < 10 || targetPlatform == Platform.Flash)
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
            banner_form = new BannerOptions(platform);
            savedata = new Savedata(platform);
            LoadChannelDatabase();

            InitializeComponent();
            Setup();
            _isShown = true;

            if (project != null && ROMpath == null)
                this.project = project;

            if (ROMpath != null && project == null)
                rom.FilePath = ROMpath;
        }

        private void Setup()
        {
            AddBases();

            // Declare ROM and WAD metadata modifier
            // ********
            TIDCode = null;
            switch (targetPlatform)
            {
                case Platform.NES:
                    TIDCode = "F";
                    rom = new ROM_NES();
                    showPatch = true;
                    break;

                case Platform.SNES:
                    TIDCode = "J";
                    rom = new ROM_SNES();
                    showPatch = true;
                    break;

                case Platform.N64:
                    TIDCode = "N";
                    rom = new ROM_N64();
                    showPatch = true;
                    break;

                case Platform.SMS:
                    TIDCode = "L";
                    rom = new ROM_SEGA() { IsSMS = true };
                    showPatch = true;
                    break;

                case Platform.SMD:
                    TIDCode = "M";
                    rom = new ROM_SEGA() { IsSMS = false };
                    showPatch = true;
                    break;

                case Platform.PCE:
                    TIDCode = "P";
                    rom = new ROM_PCE();
                    showPatch = true;
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
                    showPatch = true;
                    break;

                case Platform.Flash:
                    rom = new SWF();
                    break;

                case Platform.RPGM:
                    rom = new RPGM();
                    banner_form.players.Enabled = false;
                    break;

                default:
                    rom = new Disc();
                    break;
            }

            switch (targetPlatform)
            {
                // ROM formats
                default:
                    try
                    {
                        var extensions = Filters.ROM.Where(x => x.Platform == (targetPlatform == Platform.S32X ? Platform.SMD : targetPlatform)).ToArray()[0];
                        for (int i = 0; i < extensions.Extensions.Length; i++)
                            if (!extensions.Extensions[i].StartsWith("*")) extensions.Extensions[i] = "*" + extensions.Extensions[i];

                        browseROM.Filter = string.Format(Program.Lang.String($"filter.rom"), Program.Lang.Console(targetPlatform), string.Join(", ", extensions.Extensions), string.Join(";", extensions.Extensions));
                    }
                    catch
                    {
                        browseROM.Filter = Program.Lang.String("filter").TrimStart('|');
                    }
                    break;

                // CD images
                case Platform.PCECD:
                case Platform.PSX:
                case Platform.SMCD:
                case Platform.GCN:
                    browseROM.Filter = Program.Lang.String("filter.disc") + "|" + Program.Lang.String("filter.zip") + Program.Lang.String("filter");
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
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            // Set icon
            // ********
            if (Program.MainForm != null)
            {
                using var icon = new Bitmap(Program.MainForm.Icons[targetPlatform]);
                icon.MakeTransparent(Color.White);
                Icon = Icon.FromHandle(icon.GetHicon());
            }

            // Cosmetic
            // ********
            RefreshForm();

            bool removeManual = true;
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
                if (targetPlatform == manualConsole) removeManual = false;
            if (removeManual) manual_type.Items.RemoveAt(2);

            // *****************************************************
            // LOADING PROJECT
            // *****************************************************
            bool loadProject = project != null;
            if (loadProject)
            {
                ProjectPath = project.ProjectPath;

                video_modes.SelectedIndex = project.VideoMode;

                img = new ImageHelper(project.Platform, null);
                img.LoadToSource(project.Img.Bmp);
                LoadROM(project.ROM, false);

                if (File.Exists(project.OfflineWAD))
                {
                    use_online_wad.Enabled = Program.Config.application.use_online_wad_enabled;
                    use_offline_wad.Checked = true;
                    LoadWAD(project.OfflineWAD);
                }
                else
                {
                    use_online_wad.Enabled = use_online_wad.Checked = true;
                    try { Base.SelectedIndex = project.OnlineWAD.BaseNumber; UpdateBaseForm(project.OnlineWAD.Region); }
                    catch { Base.SelectedIndex = 0; UpdateBaseForm(); }
                }

                patch = File.Exists(project.Patch) ? project.Patch : null;

                channel_name.Text = project.ChannelTitles[1];
                banner_form.title.Text = project.BannerTitle;
                banner_form.released.Value = project.BannerYear;
                banner_form.players.Value = project.BannerPlayers;
                savedata.title.Text = project.SaveDataTitle[0];
                savedata.subtitle.Text = project.SaveDataTitle.Length > 1 ? project.SaveDataTitle[1] : null;
                title_id_upper.Text = project.TitleID;

                regions.SelectedIndex = project.WADRegion;
                injection_methods.SelectedIndex = project.InjectionMethod;
                multifile_software.Checked = project.IsMultifile;
                image_interpolation_mode.SelectedIndex = project.ImageOptions.Item1;
                image_resize0.Checked = !project.ImageOptions.Item2;
                image_resize1.Checked = project.ImageOptions.Item2;

                if (contentOptionsForm != null)
                {
                    contentOptionsForm.Options = project.ContentOptions;
                    contentOptionsForm.UsesKeymap = project.Keymap.Enabled;
                    contentOptionsForm.Keymap = project.Keymap.List;
                }

                LoadImage();
                LoadManual(project.Manual.Type, project.Manual.File);
                LoadSound(project.Sound);
                setFilesText();
            }
            else
            {
                use_online_wad.Enabled = Program.Config.application.use_online_wad_enabled;
                if (use_online_wad.Checked && !use_online_wad.Enabled) use_online_wad.Checked = false;
                if (!use_offline_wad.Checked && !use_online_wad.Checked) use_offline_wad.Checked = true;
            }

            savedata.Fill.Checked = project != null ? project.LinkSaveDataTitle : Program.Config.application.auto_fill_save_data;
            if (savedata.Fill.Checked) linkSaveDataTitle();
            forwarder_root_device.SelectedIndex = loadProject ? project.ForwarderStorageDevice : Program.Config.forwarder.root_storage_device;

            IsVisible = true;

            IsEmpty = project == null;
            IsModified = false;
            _isMint = true;

            // Error messages for not found files
            // ********
            if (loadProject)
                foreach (var item in new string[] { project.ROM, project.Patch, project.OfflineWAD, project.Sound })
                    if (!File.Exists(item) && !string.IsNullOrWhiteSpace(item)) MessageBox.Show(string.Format(Program.Lang.Msg(11, true), Path.GetFileName(item)));
            project = null;

            if (File.Exists(rom?.FilePath) && IsEmpty)
                LoadROM(rom.FilePath, Program.Config.application.auto_prefill);
        }

        // -----------------------------------

        public void BrowseROMDialog(string text)
        {
            browseROM.Title = text;

            if (browseROM.ShowDialog() == DialogResult.OK)
            {
                LoadROM(browseROM.FileName, Program.Config.application.auto_prefill);
            }
        }

        public void BrowseImageDialog()
        {
            browseImage.Title = import_image.Text;
            browseImage.Filter = Program.Lang.String("filter.img");

            if (browseImage.ShowDialog() == DialogResult.OK) LoadImage(browseImage.FileName);
        }

        private void import_image_Click(object sender, EventArgs e) => BrowseImageDialog();
        private void download_image_Click(object sender, EventArgs e) => GameScan(true);

        private void refreshData()
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (isVirtualConsole && (inWadFile == null && !use_online_wad.Checked))
                injection_method_options.Enabled = false;
            else
                injection_method_options.Enabled = contentOptionsForm != null;

            if (channels.Entries?[0].ID == "00010001-53544c42")
            {
                use_online_wad.Checked = true;
                import_wad.Enabled = use_offline_wad.Enabled = use_online_wad.Enabled = false;
                using_default_wad.Visible = true;
                using_default_wad.BringToFront();
            }

            if (!IsEmpty)
                IsModified = true;

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
                && isVirtualConsole, // Browse manual
            };
        }

        public Bitmap FileTypeImage
        {
            get
            {
                return targetPlatform switch
                {
                    Platform.NEO => Properties.Resources.page_white_zip,
                    Platform.Flash => Properties.Resources.page_white_flash,
                    _ => Properties.Resources.page_white_cd
                };
            }
        }

        public string FileTypeName
        {
            get
            {
                return targetPlatform switch
                {
                    Platform.PSX
                    or Platform.PCECD
                    or Platform.SMCD
                    or Platform.GCN => Program.Lang.String(rom_label.Name + "2", Name),
                    Platform.RPGM => Program.Lang.String(rom_label.Name + "1", Name),
                    Platform.NEO => "ZIP",
                    Platform.Flash => "SWF",
                    _ => "ROM",
                };
            }
        }

        private void setFilesText()
        {
            // ROM/ISO
            // ********
            bool hasRom = !string.IsNullOrWhiteSpace(rom?.FilePath);
            bool hasWad = !string.IsNullOrWhiteSpace(inWadFile);

            rom_label.Text = string.Format(Program.Lang.String(rom_label.Name, Name), FileTypeName);
            rom_label_filename.Text = hasRom ? rom?.FilePath : Program.Lang.String("none");
            if (rom_label_filename.Text.Length > 80) rom_label_filename.Text = rom_label_filename.Text.Substring(0, 77) + "...";

            // WAD
            // ********
            if (!hasWad && !use_online_wad.Checked)
            {
                baseID.Visible = false;
                baseName.Location = baseID.Location;
                baseName.Text = Program.Lang.String("none");
            }
            else
            {
                baseID.Visible = true;
                baseName.Location = new Point(baseID.Location.X + baseID.Width, baseID.Location.Y);
            }

            checkImg1.Image = hasWad ? Program.Lang.Current.ToLower().StartsWith("ja") || Program.Lang.Current.ToUpper().EndsWith("-JP") ? Properties.Resources.tick_circle : Properties.Resources.tick : Properties.Resources.cross;
        }

        private void randomTID()
        {
            title_id_upper.Text = TIDCode != null ? TIDCode + GenerateTitleID().Substring(0, 3) : GenerateTitleID();
            refreshData();
        }

        public string GetName(bool full)
        {
            string FILENAME = File.Exists(patch) ? Path.GetFileNameWithoutExtension(patch) : Path.GetFileNameWithoutExtension(rom?.FilePath);
            string CHANNELNAME = channel_name.Text;
            string FULLNAME = System.Text.RegularExpressions.Regex.Replace(_bannerTitle, @"\((.*?)\)", "").Replace("\r\n", "\n").Replace("\n", " - ");
            string TITLEID = title_id_upper.Text.ToUpper();
            string PLATFORM = targetPlatform.ToString();

            string REGION = regions.SelectedItem.ToString() == Program.Lang.String("region_j") ? "Japan"
                          : regions.SelectedItem.ToString() == Program.Lang.String("region_u") ? "USA"
                          : regions.SelectedItem.ToString() == Program.Lang.String("region_e") ? "Europe"
                          : regions.SelectedItem.ToString() == Program.Lang.String("region_k") ? "Korea"
                          : regions.SelectedIndex == 1 ? "Region-Free"
                          : null;
            if (REGION == null)
            {
                for (int i = 0; i < baseRegionList.Items.Count; i++)
                {
                    if ((baseRegionList.Items[i] as ToolStripMenuItem).Checked)
                    {
                        REGION = channels.Entries[Base.SelectedIndex].Regions[i] switch
                        {
                            0 => "Japan",
                            1 or 2 => "USA",
                            3 or 4 or 5 => "Europe",
                            6 or 7 => "Korea",
                            8 => "Region-Free",
                            _ => "Original"
                        };
                    }
                }
            }

            string target = full ? Program.Config.application.default_export_filename : Program.Config.application.default_target_filename;
            target = target.Replace("FILENAME", FILENAME).Replace("CHANNELNAME", CHANNELNAME).Replace("FULLNAME", FULLNAME).Replace("TITLEID", TITLEID).Replace("PLATFORM", PLATFORM).Replace("REGION", REGION);

            return string.Join("_", target.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
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
                        if (File.Exists(ProjectPath))
                        {
                            SaveProject(ProjectPath);
                            return true;
                        }
                        else return Program.MainForm.SaveAs_Trigger();
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

        private void Value_Changed(object sender, EventArgs e) => refreshData();

        private void linkSaveDataTitle()
        {
            savedata.SourcedLines = new string[] { channel_name.Text, banner_form.title.Lines.Length > 1 ? banner_form.title.Lines[1] : "" };

            if (savedata.Fill.Checked)
            {
                savedata.SyncTitles();
                refreshData();
            }
        }

        private void TextBox_Changed(object sender, EventArgs e)
        {
            if (sender == channel_name)
            {
                Text = string.IsNullOrWhiteSpace(channel_name.Text) ? Untitled : channel_name.Text;
                linkSaveDataTitle();
            }

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
            SystemSounds.Beep.Play();
            e.Handled = true;
        }

        private void OpenWAD_CheckedChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            use_offline_wad.Checked = !use_online_wad.Checked;
            BaseRegion.Enabled = use_online_wad.Checked;
            Base.Enabled = use_online_wad.Checked && Base.Items.Count > 1;
            checkImg1.Visible = import_wad.Enabled = use_offline_wad.Checked;

            if (use_online_wad.Checked)
            {
                inWadFile = null;
                AddBases();
            }
            else
            {
                if (Base.Items.Count > 0) Base.SelectedIndex = 0;
            }

            if (!BaseRegion.Enabled)
                BaseRegion.Image = null;

            refreshData();
        }

        private void import_wad_Click(object sender, EventArgs e)
        {
            browseInputWad.Title = import_wad.Text;
            browseInputWad.Filter = Program.Lang.String("filter.wad");
            var result = browseInputWad.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadWAD(browseInputWad.FileName);
                refreshData();
            }
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (image_interpolation_mode.SelectedIndex != Program.Config.application.image_interpolation) refreshData();
            LoadImage();
        }

        private void SwitchAspectRatio(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (sender == image_resize0 || sender == image_resize1)
            {
                LoadImage();
            }

            if (sender == forwarder_root_device)
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
            WAD Reader = new();

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
                {
                    if (channels.Entries[h].GetUpperID(i) == Reader.UpperTitleID.ToUpper())
                    {
                        inWadFile = path;

                        // Fix Flash Placeholder (USA) bug
                        // ****************
                        if ((int)Reader.Region == 1 && Reader.UpperTitleID.ToUpper().StartsWith("WNA"))
                            i = 0;

                        Base.SelectedIndex = h;
                        UpdateBaseForm(i);
                        Reader.Dispose();
                        return true;
                    }
                }

            Failed:
            SystemSounds.Beep.Play();
            try
            {
                MessageBox.Show(string.Format(Program.Lang.Msg(5), Reader.UpperTitleID));
                Reader.Dispose();
            }
            catch { Reader = null; }
            inWadFile = null;
            return false;
        }

        protected void LoadManual(int index, string path = null, bool isFolder = true)
        {
            bool failed = false;

            #region Load manual as ZIP file (if exists)
            if (File.Exists(path) && !isFolder)
            {
                int applicable = 0;
                // bool hasFolder = false;

                using ZipFile ZIP = new(path);
                foreach (ZipEntry entry in ZIP)
                {
                    if (entry.IsFile)
                    {
                        // Check if is a valid emanual contents folder
                        // ****************
                        // if ((item.FileName == "emanual" || item.FileName == "html") && item.IsDirectory)
                        //    hasFolder = true;

                        // Check key files
                        // ****************
                        if ((entry.Name.StartsWith("startup") && Path.GetExtension(entry.Name) == ".html")
                          || entry.Name == "standard.css"
                          || entry.Name == "contents.css"
                          || entry.Name == "vsscript.css")
                            applicable++;
                    }
                }

                if (applicable >= 2 /* && hasFolder */)
                {
                    manual = path;
                    goto End;
                }

                failed = true;
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

            manual_type.SelectedIndex = manual == null && index >= 2 ? 0 : manual != null && index < 2 ? 2 : index;
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
                        var palette = contentOptionsNES.ImgPalette(src);

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
            banner.Image = preview.Banner
                (
                    img?.VCPic,
                    banner_form.title.Text,
                    (int)banner_form.released.Value,
                    (int)banner_form.players.Value,
                    targetPlatform,
                    _bannerRegion
                );

            if (!bannerOnly)
            {
                savedata.Picture.Image = img?.SaveIcon();
            }
        }

        protected bool LoadImage(Bitmap src)
        {
            if (src == null) return false;

            try
            {
                img.InterpMode = (InterpolationMode)image_interpolation_mode.SelectedIndex;
                img.FitAspectRatio = image_resize1.Checked;

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

        public void LoadROM(string ROMpath, bool AutoScan = true, bool filter = false)
        {
            if (ROMpath == null || rom == null || !File.Exists(ROMpath) || (filter && !browseROM.Filter.ToLower().Contains(Path.GetExtension(ROMpath).ToLower())))
            {
                SystemSounds.Beep.Play();
                return;
            }

            IsEmpty = false;

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
                        banner_form.title.Text = (rom as RPGM).GetTitle(ROMpath);
                        if (_bannerTitle.Length <= channel_name.MaxLength) channel_name.Text = banner_form.title.Text;
                        resetImages(true);
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

            Program.MainForm.toolbarGameScan.Enabled = Program.MainForm.game_scan.Enabled = ToolbarButtons[0];
            if (rom != null && AutoScan && ToolbarButtons[0]) GameScan(false);
            setFilesText();
        }


        /// <summary>
        /// Gets any game metadata that is available for the file based on its CRC32 reading hash, including the software title, year, players, and title image URL.
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        protected (string Name, string Serial, string Year, string Players, string Image) GetGameData(Platform platform, string path)
        {
            bool isDisc = platform == Platform.PCECD || platform == Platform.GCN || platform == Platform.SMCD || platform == Platform.PSX;
            if (isDisc)
            {
                if (Path.GetExtension(path).ToLower() == ".cue")
                    foreach (var item in Directory.EnumerateFiles(Path.GetDirectoryName(path)))
                        if (Path.GetExtension(item).ToLower() == ".bin" && Path.GetFileNameWithoutExtension(path).ToLower() == Path.GetFileNameWithoutExtension(item).ToLower())
                            path = item;

                if (Path.GetExtension(path).ToLower() != ".bin")
                    return (null, null, null, null, null);
            }

            var result = Databases.LibRetro.Read(path, platform);

            if (!string.IsNullOrEmpty(result.Name))
            {
                result.Name = System.Text.RegularExpressions.Regex.Replace(result.Name?.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "").Trim();
                if (result.Name.Contains(", The")) result.Name = "The " + result.Name.Replace(", The", string.Empty);
            }

            return result;
        }

        public async void GameScan(bool imageOnly)
        {
            if (rom == null || rom.FilePath == null) return;

            try
            {
                var gameData = await Task.FromResult(GetGameData(targetPlatform, rom.FilePath));
                bool retrieved = imageOnly ? !string.IsNullOrEmpty(gameData.Image) : gameData != (null, null, null, null, null);

                if (retrieved)
                {
                    if (!imageOnly)
                    {
                        // Set banner title
                        banner_form.title.Text = gameData.Name ?? banner_form.title.Text;

                        // Set channel title text
                        if (!string.IsNullOrEmpty(gameData.Name))
                        {
                            var text = gameData.Name.Replace("\r", "").Split('\n');
                            if (text[0].Length <= channel_name.MaxLength) { channel_name.Text = text[0]; }
                        }

                        // Set year and players
                        banner_form.released.Value = !string.IsNullOrEmpty(gameData.Year) ? int.Parse(gameData.Year) : banner_form.released.Value;
                        banner_form.players.Value = !string.IsNullOrEmpty(gameData.Players) ? int.Parse(gameData.Players) : banner_form.players.Value;

                        linkSaveDataTitle();
                    }

                    // Set image
                    if (!string.IsNullOrEmpty(gameData.Image))
                    {
                        LoadImage(gameData.Image);
                    }

                    resetImages(true);
                }

                // Show message if partially failed to retrieve data
                if (retrieved && (string.IsNullOrEmpty(gameData.Name) || string.IsNullOrEmpty(gameData.Players) || string.IsNullOrEmpty(gameData.Year) || string.IsNullOrEmpty(gameData.Image)) && !imageOnly)
                    MessageBox.Show(Program.Lang.Msg(4));
                else if (!retrieved) SystemSounds.Beep.Play();
            }

            catch (Exception ex)
            {
                MessageBox.Error(ex.Message);
            }
        }

        public void SaveToWAD(string targetFile = null) => backgroundWorker.RunWorkerAsync(targetFile);
        private void saveToWAD_UpdateProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e) => wait.progress.Value = e.ProgressPercentage;
        private void saveToWAD(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Exception error = null;
            IsBusy = true;

            string targetFile = e.Argument.ToString();
            if (targetFile == null) targetFile = Paths.WorkingFolder + "out.wad";

            Program.CleanTemp();

            try
            {
                Method m = new(targetPlatform)
                {
                    ROM = rom,
                    Patch = patch,
                    Img = img,

                    ChannelTitles = _channelTitles,
                    BannerTitle = _bannerTitle,
                    BannerYear = _bannerYear,
                    BannerPlayers = _bannerPlayers,
                    BannerSound = sound,

                    IsMultifile = multifile_software.Checked,
                    BannerRegion = _bannerRegion,
                    SaveDataTitle = _saveDataTitle,
                    Settings = (contentOptions, keymap.List),

                    WAD = WAD.Load(Properties.Resources.StaticBase),
                    WadRegion = (int)outWadRegion,
                    WadVideoMode = video_modes.SelectedIndex,

                    EmuVersion = emuVer,

                    Manual = manual,
                    TitleID = _tID,

                    Out = targetFile,
                };

                // Get WAD data
                // *******
                if (inWadFile != null) m.GetWAD(inWadFile, baseID.Text);
                else
                {
                    var entry = channels.Entries.Where(x => x.GetUpperIDs().Contains(baseID.Text)).ToArray()[0];
                    var index = Array.IndexOf(entry.GetUpperIDs(), baseID.Text);
                    m.GetWAD(entry.GetWAD(index), entry.GetUpperID(index));
                }

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
                        if (isVirtualConsole)
                            m.Inject();
                        else
                            m.CreateForwarder(injection_methods.SelectedItem.ToString(), forwarder_root_device.SelectedIndex == 1 ? Forwarder.Storages.USB : Forwarder.Storages.SD);
                        break;

                    case Platform.GB:
                    case Platform.GBC:
                    case Platform.GBA:
                    case Platform.S32X:
                    case Platform.SMCD:
                    case Platform.PSX:
                    case Platform.RPGM:
                        m.CreateForwarder(injection_methods.SelectedItem.ToString(), forwarder_root_device.SelectedIndex == 1 ? Forwarder.Storages.USB : Forwarder.Storages.SD);
                        break;

                    case Platform.Flash:
                        m.Inject();
                        break;

                    default:
                        throw new NotImplementedException();
                }

                // Change WAD region & internal main.dol things
                // *******

                // Other WAD settings to be changed done by WAD creator helper, which will save to a new file
                // *******
                m.EditMetadata();
                m.EditBanner();
                m.Save();

                // Check new WAD file
                // *******
                if (File.Exists(targetFile) && File.ReadAllBytes(targetFile).Length > 10) error = null;
                else throw new Exception(Program.Lang.Msg(7, true));
            }

            catch (Exception ex)
            {
                error = ex;
            }

            finally
            {
                Program.CleanTemp();

                Invoke(new MethodInvoker(delegate
                {
                    if (error == null)
                    {
                        SystemSounds.Beep.Play();

                        switch (MessageBox.Show(Program.Lang.Msg(3), null, MessageBox.Buttons.YesNo, MessageBox.Icons.Information))
                        {
                            case MessageBox.Result.Yes:
                                System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{targetFile}\"");
                                break;
                        }
                    }

                    else
                    {
                        MessageBox.Error(error.Message);
                    }
                }));

                IsBusy = false;
            }
        }
        #endregion

        #region **Console-Specific Functions**
        // ******************
        // CONSOLE-SPECIFIC
        // ******************
        private void openInjectorOptions(object sender, EventArgs e)
        {
            contentOptionsForm.Text = Program.Lang.String(injection_method_options.Name, Name).TrimEnd('.').Trim();
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
                { "-JP", 0 },
                { "-KR", 1 },
                { "-GB", 2 },
                { "-IE", 2 },
                { "-BE", 2 },
                { "-FR", 2 },
                { "-ES", 2 },
                { "-PT", 2 },
                { "-IN", 2 },
                { "-ZA", 2 },
                { "-AU", 2 },
                { "-NZ", 2 },
                { "-CA", 3 },
                { "-US", 3 },
                { "-419", 3 },
                { "-MX", 3 },
                { "-CO", 3 },
                { "-EC", 3 },
                { "-VN", 3 },
                { "-CL", 3 },
                { "-AR", 3 },
                { "-BR", 3 },
                { "ja", 0 },
                { "ko", 1 },
                { "fr", 2 },
                { "es", 2 },
                { "de", 2 },
                { "nl", 2 },
                { "it", 2 },
                { "pl", 2 },
                { "ru", 2 },
                { "uk", 2 },
                { "tr", 2 },
                { "hu", 2 },
                { "ro", 2 },
                { "ca", 2 },
                { "eu", 2 },
                { "gl", 2 },
                { "ast", 2 },
                { "dk", 2 },
                { "no", 2 },
                { "sv", 2 },
                { "fi", 2 },
            };

            foreach (var item in altRegions) if (Program.Lang.Current.ToLower().StartsWith(item.Key) || Program.Lang.Current.ToUpper().EndsWith(item.Key))
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
            BaseRegion.Image = channels.Entries[Base.SelectedIndex].Regions[index] switch
            {
                0 => Properties.Resources.flag_jp,
                1 or 2 => Properties.Resources.flag_us,
                3 => (int)targetPlatform <= 2 ? Properties.Resources.flag_eu50 : Properties.Resources.flag_eu,
                4 or 5 => (int)targetPlatform <= 2 ? Properties.Resources.flag_eu60 : Properties.Resources.flag_eu,
                6 or 7 => Properties.Resources.flag_kr,
                _ => null,
            };

            savedata.Reset(targetPlatform, (int)inWadRegion);
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
            if (targetPlatform == Platform.Flash && contentOptionsForm != null) return;

            manual_type.Visible = false;
            forwarder_root_device.Visible = false;
            multifile_software.Visible = false;
            extra.Visible = false;
            contentOptionsForm = null;
            htmlForm = null;

            if (isVirtualConsole)
            {
                manual_type.Visible = true;
                extra.Visible = true;
                extra.Text = Program.Lang.String(manual_type.Name, Name);

                switch (targetPlatform)
                {
                    case Platform.NES:
                        contentOptionsForm = new Options_VC_NES() { EmuType = emuVer };
                        break;

                    case Platform.SNES:
                        contentOptionsForm = new Options_VC_SNES();
                        htmlForm = new(Program.Lang.HTML(0, false), injection_methods.SelectedItem.ToString());
                        break;

                    case Platform.N64:
                        contentOptionsForm = new Options_VC_N64() { EmuType = inWadRegion == Region.Korea ? 3 : emuVer };
                        htmlForm = new(Program.Lang.HTML(1, false), injection_methods.SelectedItem.ToString());
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
                multifile_software.Visible = true;
            }

            else
            {
                forwarder_root_device.Visible = true;
                extra.Visible = true;
                extra.Text = Program.Lang.String(forwarder_root_device.Name, Name);

                switch (targetPlatform)
                {
                    case Platform.GB:
                    case Platform.GBC:
                    case Platform.GBA:
                    case Platform.S32X:
                    case Platform.SMCD:
                    case Platform.PSX:
                        contentOptionsForm = new Options_Forwarder(targetPlatform);
                        if (targetPlatform == Platform.PSX) multifile_software.Visible = true;
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
                // contentOptionsForm.Text = Program.Lang.String("injection_method_options", "projectform").TrimEnd('.').Trim();
                contentOptionsForm.Icon = Icon.FromHandle((injection_method_options.Image as Bitmap).GetHicon());
            }

            if (!isVirtualConsole && manual != null)
            {
                manual = null;
                manual_type.SelectedIndex = 0;
            }

            showSaveData = isVirtualConsole || targetPlatform == Platform.Flash;
            download_image.Enabled = targetPlatform != Platform.C64
                                  && targetPlatform != Platform.Flash
                                  && targetPlatform != Platform.RPGM;

            bool hasHelp = !string.IsNullOrWhiteSpace(htmlForm?.FormText);
            injection_method_help.Visible = hasHelp && !IsEmpty;
            injection_methods.Size = hasHelp ? injection_methods.MinimumSize : injection_methods.MaximumSize;
        }
        #endregion

        private void CustomManual_CheckedChanged(object sender, EventArgs e)
        {
            if (manual_type.Enabled && manual_type.SelectedIndex == 2 && manual == null)
            {
                if (!Program.Config.application.donotshow_000) MessageBox.Show((sender as Control).Text, Program.Lang.Msg(6), 0);

                if (browseManual.ShowDialog() == DialogResult.OK) LoadManual(manual_type.SelectedIndex, browseManual.SelectedPath, true);
                else manual_type.SelectedIndex = 0;
            }

            if (manual_type.Enabled && manual_type.SelectedIndex < 2) LoadManual(manual_type.SelectedIndex);

            refreshData();
        }

        private void include_patch_CheckedChanged(object sender, EventArgs e)
        {
            string oldPatch = patch;

            if (include_patch.Checked)
            {
                if (browsePatch.ShowDialog() == DialogResult.OK)
                    patch = browsePatch.FileName;
                else
                {
                    patch = null;
                    include_patch.Checked = false;
                }
            }
            else
                patch = null;

            if (oldPatch != patch) refreshData();
        }

        private void InjectorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetImages();
            resetContentOptions();
            LoadImage();
            refreshData();
        }

        /* private void banner_preview_Click(object sender, EventArgs e)
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
                    banner_form.title.Text,
                    (int)banner_form.released.Value,
                    (int)banner_form.players.Value,
                    targetPlatform,
                    _bannerRegion
                );

                f.ClientSize = p.Image.Size;
                f.StartPosition = FormStartPosition.CenterParent;
                f.Controls.Add(p);
                f.ShowDialog();
            }
        } */

        private void banner_sound_Click(object sender, EventArgs e) => bannerMenu.Show(Cursor.Position);

        private void banner_customize_Click(object sender, EventArgs e)
        {
            // banner_form.Text = Program.Lang.String(banner_details.Name, Name);
            banner_form.origTitle = banner_form.title.Text;
            banner_form.origYear = (int)banner_form.released.Value;
            banner_form.origPlayers = (int)banner_form.players.Value;
            banner_form.origRegion = banner_form.region.SelectedIndex;

            if (banner_form.ShowDialog() == DialogResult.OK)
            {
                bool hasBanner = banner_form.origTitle != banner_form.title.Text;
                bool hasYear = banner_form.origYear != (int)banner_form.released.Value;
                bool hasPlayers = banner_form.origPlayers != (int)banner_form.players.Value;
                bool hasRegion = banner_form.origRegion != banner_form.region.SelectedIndex;
                linkSaveDataTitle();

                if (hasBanner || hasYear || hasPlayers || hasRegion)
                {
                    resetImages(true);
                    refreshData();
                }
            }
        }

        protected void LoadSound(string file)
        {
            sound = file;
            restore_banner_sound.Enabled = File.Exists(file) && file != null;
            if (!restore_banner_sound.Enabled) sound = null;
            refreshData();
        }

        private void play_banner_sound_Click(object sender, EventArgs e)
        {
            SoundPlayer snd = File.Exists(sound) && sound != null ? new(sound) : new(Properties.Resources.Sound_WiiVC);
            snd.PlaySync();
        }

        private void replace_banner_sound_Click(object sender, EventArgs e)
        {
            browseSound.Filter = "WAV (*.wav)|*.wav" + Program.Lang.String("filter");
            browseSound.Title = replace_banner_sound.Text;
            if (browseSound.ShowDialog() == DialogResult.OK || File.Exists(browseSound.FileName))
                LoadSound(browseSound.FileName);
        }
        private void restore_banner_sound_Click(object sender, EventArgs e) => LoadSound(null);

        private void edit_save_data_Click(object sender, EventArgs e)
        {
            // savedata.Text = Program.Lang.String(edit_save_data.Name, Name);

            if (savedata.ShowDialog() == DialogResult.OK) refreshData();
        }

        private void injection_method_help_Click(object sender, EventArgs e) => htmlForm.ShowDialog(this);
    }
}
