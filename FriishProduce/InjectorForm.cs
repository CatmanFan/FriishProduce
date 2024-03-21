using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libWiiSharp;

namespace FriishProduce
{
    public partial class InjectorForm : Form
    {
        public Console Console;
        protected string TIDCode;
        protected string Untitled;
        protected string oldImgPath = "null";
        protected string newImgPath = "null";
        protected string WADPath = null;

        public bool ReadyToExport = false;
        public bool ROMLoaded = false;

        // -----------------------------------
        // Public variables
        // -----------------------------------
        protected ROM ROM { get; set; }
        protected WAD OutWAD { get; set; }
        protected string PatchFile { get; set; }
        protected string Manual { get; set; }
        protected LibRetroDB LibRetro { get; set; }
        protected DatabaseEntry[] Database { get; set; }
        protected IDictionary<string, string> CurrentBase { get; set; }
        protected ImageHelper Img { get; set; }
        protected Creator Creator { get; set; }

        protected bool isVC { get; set; }
        protected InjectorWiiVC VC { get; set; }

        // -----------------------------------
        // Options
        // -----------------------------------
        protected ContentOptions CO { get; set; }

        // -----------------------------------
        // Connection with parent form
        // -----------------------------------
        public new MainForm Parent { get; set; }

        public event EventHandler ExportCheck;


        // -----------------------------------

        public void RefreshForm()
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------
            Language.Localize(this);
            label7.Text = label5.Text;
            CustomManual.Text = Language.Get("CustomManual");
            BrowsePatch.Filter = Language.Get("Filter.Patch");
            label16.Text = Language.Get("NotSupported");

            // Change title text to untitled string
            Untitled = string.Format(Language.Get("Untitled"), Language.Get(Enum.GetName(typeof(Console), Console), "Platforms"));
            Text = string.IsNullOrWhiteSpace(ChannelTitle.Text) ? Untitled : ChannelTitle.Text;

            SetROMDataText();

            var baseMax = Math.Max(label4.Location.X + label4.Width - 4, label5.Location.X + label5.Width - 4);
            baseName.Location = new Point(baseMax, label4.Location.Y);
            baseID.Location = new Point(baseMax, label5.Location.Y);

            // Selected index properties
            imageintpl.Items.Clear();
            imageintpl.Items.Add(Language.Get("ByDefault"));
            imageintpl.Items.AddRange(Language.GetArray("List.ImageInterpolation"));
            imageintpl.SelectedIndex = Properties.Settings.Default.ImageInterpolation;
            FStorage_USB.Checked = Properties.Settings.Default.Default_Forwarders_FilesStorage.ToLower().Contains("usb");
            FStorage_SD.Checked = !FStorage_USB.Checked;
            toggleSwitch1.Checked = Properties.Settings.Default.Default_Forwarders_Mode.ToLower().Contains("vwii");

            // Regions lists
            RegionsList.Items.Clear();
            RegionsList.Items.Add(Language.Get("Region.F"));
            RegionsList.SelectedIndex = 0;
            if (Language.Current.TwoLetterISOLanguageName.ToLower() == "ja")
            {
                RegionsList.Items.Add(Language.Get("Region.J"));
                RegionsList.Items.Add(Language.Get("Region.U"));
                RegionsList.Items.Add(Language.Get("Region.E"));
                RegionsList.Items.Add(Language.Get("Region.K"));
            }
            else if (Language.Current.TwoLetterISOLanguageName.ToLower() == "ko")
            {
                RegionsList.Items.Add(Language.Get("Region.K"));
                RegionsList.Items.Add(Language.Get("Region.U"));
                RegionsList.Items.Add(Language.Get("Region.E"));
                RegionsList.Items.Add(Language.Get("Region.J"));
            }
            else
            {
                RegionsList.Items.Add(Language.Get("Region.U"));
                RegionsList.Items.Add(Language.Get("Region.E"));
                RegionsList.Items.Add(Language.Get("Region.J"));
                RegionsList.Items.Add(Language.Get("Region.K"));
            }

            if (CurrentBase != null)
            {
                for (int i = 0; i < CurrentBase.Count; i++)
                {
                    switch (CurrentBase.ElementAt(i).Key.ToString().ToUpper()[3])
                    {
                        default:
                        case 'E':
                        case 'N':
                            WADRegionList.Items[i].Text = Language.Get("Region.U");
                            break;

                        case 'P':
                        case 'L':
                        case 'M':
                            WADRegionList.Items[i].Text = Language.Get("Region.E");
                            break;

                        case 'J':
                            WADRegionList.Items[i].Text = Language.Get("Region.J");
                            break;

                        case 'Q':
                        case 'T':
                            WADRegionList.Items[i].Text = Language.Get("Region.K");
                            break;
                    }
                }
            }

            // Injection methods list
            InjectorsList.Items.Clear();

            switch (Console)
            {
                case Console.NES:
                    InjectorsList.Items.Add(Language.Get("VC"));
                    InjectorsList.Items.Add(Forwarder.List[0]);
                    InjectorsList.Items.Add(Forwarder.List[1]);
                    InjectorsList.Items.Add(Forwarder.List[2]);
                    break;

                case Console.SNES:
                    InjectorsList.Items.Add(Language.Get("VC"));
                    InjectorsList.Items.Add(Forwarder.List[3]);
                    InjectorsList.Items.Add(Forwarder.List[4]);
                    InjectorsList.Items.Add(Forwarder.List[5]);
                    break;

                case Console.N64:
                    InjectorsList.Items.Add(Language.Get("VC"));
                    InjectorsList.Items.Add(Forwarder.List[8]);
                    InjectorsList.Items.Add(Forwarder.List[9]);
                    InjectorsList.Items.Add(Forwarder.List[10]);
                    InjectorsList.Items.Add(Forwarder.List[11]);
                    break;

                case Console.SMS:
                case Console.SMDGEN:
                    InjectorsList.Items.Add(Language.Get("VC"));
                    InjectorsList.Items.Add(Forwarder.List[7]);
                    break;

                case Console.PCE:
                case Console.NeoGeo:
                case Console.MSX:
                case Console.C64:
                    InjectorsList.Items.Add(Language.Get("VC"));
                    break;

                case Console.Flash:
                    InjectorsList.Items.Add(Language.Get("ByDefault"));
                    break;

                case Console.GBA:
                    InjectorsList.Items.Add(Forwarder.List[6]);
                    break;

                case Console.PSX:
                    InjectorsList.Items.Add(Forwarder.List[12]);
                    break;

                default:
                    break;
            }

            InjectorsList.SelectedIndex = 0;
            label3.Enabled = InjectorsList.Enabled = InjectorsList.Items.Count > 1;

            if (Properties.Settings.Default.ImageFitAspectRatio) radioButton2.Checked = true; else radioButton1.Checked = true;
        }

        public InjectorForm(Console c, string ROMpath = null)
        {
            Console = c;
            InitializeComponent();
            if (ROMpath != null) ROM.Path = ROMpath;
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            // Declare WAD metadata modifier
            // ********
            Creator = new Creator();
            TIDCode = null;

            switch (Console)
            {
                case Console.NES:
                    TIDCode = "F";
                    ROM = new ROM_NES();
                    break;

                case Console.SNES:
                    TIDCode = "J";
                    ROM = new ROM_SNES();
                    break;

                case Console.N64:
                    TIDCode = "N";
                    ROM = new ROM_N64();
                    break;

                case Console.SMS:
                    TIDCode = "L";
                    ROM = new ROM_SEGA() { IsSMS = true };
                    break;

                case Console.SMDGEN:
                    TIDCode = "M";
                    ROM = new ROM_SEGA() { IsSMS = false };
                    break;

                case Console.PCE:
                    TIDCode = "P"; // Q for CD games
                    ROM = new ROM_PCE();
                    break;

                case Console.NeoGeo:
                    TIDCode = "E";
                    ROM = new ROM_NeoGeo();
                    break;

                case Console.MSX:
                    TIDCode = "X";
                    ROM = new ROM_MSX();
                    break;

                case Console.Flash:
                    ROM = new SWF();
                    ImportPatch.Enabled = false;
                    break;

                default:
                    ROM = new Disc();
                    break;
            }

            LibRetro = Parent.LibRetro;

            // Cosmetic
            // ********
            if (Console == Console.SMS || Console == Console.SMDGEN) SaveIcon_Panel.BackgroundImage = Properties.Resources.SaveIconPlaceholder_SEGA;
            RefreshForm();

            Creator.BannerYear = (int)ReleaseYear.Value;
            Creator.BannerPlayers = (int)Players.Value;
            AddBases();

            Tag = null;

            if (ROM?.Path != null) LoadROM(ROM.Path, Properties.Settings.Default.AutoLibRetro);
        }

        // -----------------------------------

        protected virtual void CheckExport()
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            Creator.TitleID = TitleID.Text;
            Creator.BannerTitle = BannerTitle.Text;
            Creator.BannerYear = (int)ReleaseYear.Value;
            Creator.BannerPlayers = (int)Players.Value;
            Creator.SaveDataTitle =
                SaveDataTitle.Lines.Length == 1 ? new string[] { SaveDataTitle.Text } :
                SaveDataTitle.Lines.Length == 0 ? new string[] { "" } :
                SaveDataTitle.Lines;

            SetROMDataText();
            button1.Enabled = CO != null;

            ReadyToExport = !string.IsNullOrEmpty(Creator.TitleID) && Creator.TitleID.Length == 4
                            && !string.IsNullOrWhiteSpace(ChannelTitle.Text)
                            && !string.IsNullOrEmpty(Creator.BannerTitle)
                            && !string.IsNullOrEmpty(Creator.SaveDataTitle[0])
                            && (Img != null)
                            && (ROM != null && ROM?.Path != null);
            Tag = "dirty";
            ExportCheck.Invoke(this, EventArgs.Empty);
        }

        public bool[] CheckToolStripButtons() => new bool[]
            {
                Console != Console.Flash && (ROM?.Bytes != null || !string.IsNullOrWhiteSpace(ROM?.Path)), // LibRetro
                Console != Console.Flash, // Browse manual
            };

        protected virtual void SetROMDataText()
        {
            label1.Text = string.Format(Language.Get(label1.Name, this), ROM != null ? Path.GetFileName(ROM.Path) : Language.Get("Unknown"));

            if (LibRetro == null)
                label2.Text = string.Format(Language.Get(label2.Name, this), Language.Get("Unknown"));
            else
                label2.Text = string.Format(Language.Get(label2.Name, this), LibRetro.GetCleanTitle()?.Replace(Environment.NewLine, " - ") ?? Language.Get("Unknown"));

            label11.Text = PatchFile != null ? Path.GetFileName(PatchFile) : Language.Get("None");
            label11.Enabled = PatchFile != null;

            pictureBox1.Image = Preview.Banner(Console, BannerTitle.Text, (int)ReleaseYear.Value, (int)Players.Value, Img?.VCPic, (int)Creator.OrigRegion);
            pictureBox2.Image = Preview.Icon(Img?.IconVCPic);
        }

        private void RandomTID() => TitleID.Text = Creator.TitleID = TIDCode != null ? TIDCode + GenerateTitleID().Substring(0, 3) : GenerateTitleID();

        public string GetName() => ROM?.Path != null ? Path.GetFileNameWithoutExtension(ROM?.Path) + $" [{TitleID.Text.ToUpper()}]" : $"{Console} - {ChannelTitle.Text} [{TitleID.Text.ToUpper()}]";

        private void isClosing(object sender, FormClosingEventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            e.Cancel = !CheckUnsaved();
        }

        public bool CheckUnsaved()
        {
            if (Tag?.ToString() == "dirty")
                if (MessageBox.Show(Text, Language.Get("Message.001"), MessageBoxButtons.YesNo, 0, false) == DialogResult.No)
                    return false;
            return true;
        }

        private void Random_Click(object sender, EventArgs e) => RandomTID();

        private void Value_Changed(object sender, EventArgs e) => CheckExport();

        private void TextBox_Changed(object sender, EventArgs e)
        {
            if (sender == ChannelTitle)
            {
                Text = string.IsNullOrWhiteSpace(ChannelTitle.Text) ? Untitled : ChannelTitle.Text;
                if (ChannelTitle.TextLength <= SaveDataTitle.MaxLength) SaveDataTitle.Text = ChannelTitle.Text;

                if (!ChannelTitle_Locale.Checked)
                {
                    ChannelTitle_Locale.Enabled = !string.IsNullOrWhiteSpace(ChannelTitle.Text);
                    Creator.ChannelTitles = new string[8] { ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text };
                }
            }

            var currentSender = sender as TextBox;
            if (currentSender.Multiline && currentSender.Lines.Length > 2) currentSender.Lines = new string[] { currentSender.Lines[0], currentSender.Lines[1] };

            CheckExport();
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

            Base.Enabled = WADRegion.Enabled = !OpenWAD.Checked;
            if (Base.Enabled)
            {
                AddBases();
            }
            else
            {
                Base.Items.Clear();
                WADRegion.Image = null;
            }

            if (OpenWAD.Checked && WADPath == null)
            {
                BrowseWAD.Title = Language.Get("ribbonPanel_Open", Parent);
                BrowseWAD.Filter = Language.Get("Filter.WAD");
                var result = BrowseWAD.ShowDialog();

                if (result == DialogResult.OK && !LoadWAD(BrowseWAD.FileName)) OpenWAD.Checked = false;
                else if (result == DialogResult.Cancel) OpenWAD.Checked = false;
            }
            else
            {
                WADPath = null;
            }

            if (groupBox2.Enabled) CheckExport();
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (imageintpl.SelectedIndex != Properties.Settings.Default.ImageInterpolation) Tag = "dirty";
            LoadImage();
        }

        private void SwitchAspectRatio(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            if (sender == radioButton1 || sender == radioButton2)
            {
                if (sender == radioButton1 && radioButton2.Checked)
                {
                    Tag = "dirty";
                    radioButton2.Checked = !radioButton1.Checked;
                }

                else if (sender == radioButton2 && radioButton1.Checked)
                {
                    Tag = "dirty";
                    radioButton1.Checked = !radioButton2.Checked;
                }

                LoadImage();
            }

            else if (groupBox3.Enabled) CheckExport();
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
                Reader = WAD.Load(path);
            }
            catch
            {
                MessageBox.Show(Language.Get("Message.002"), 0, Ookii.Dialogs.WinForms.TaskDialogIcon.Warning);
                return false;
            }

            for (int x = 0; x < Database.Length; x++)
                if (Database[x].TitleID.ToUpper() == Reader.UpperTitleID.ToUpper())
                {
                    WADPath = path;

                    CurrentBase.Clear();
                    CurrentBase.Add(Database[x].TitleID, Database[x].DisplayName);
                    baseName.Text = Database[x].DisplayName;
                    baseID.Text = Database[x].TitleID;
                    UpdateBaseGeneral(0);
                    Reader.Dispose();
                    return true;
                }

            Reader.Dispose();
            System.Media.SystemSounds.Beep.Play();
            MessageBox.Show(string.Format(Language.Get("Message.005"), Reader.UpperTitleID));
            return false;
        }

        public void LoadManual(string path)
        {
            if (path != null)
            {
                // Check if is a valid emanual contents folder
                // ****************
                string folder = null;
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

                if (validFiles < 2)
                {
                    MessageBox.Show(Language.Get("Message.007"), MessageBoxButtons.OK, Ookii.Dialogs.WinForms.TaskDialogIcon.Warning);
                    Manual = null;
                    return;
                }
            }

            Manual = path;
            CustomManual.Checked = Manual != null;
        }

        public void LoadImage()
        {
            if (Creator != null && Img != null) LoadImage(Img.Source);
            else CheckExport();
        }

        public void LoadImage(string path)
        {
            if (Img != null) oldImgPath = newImgPath;
            newImgPath = path;

            if (Img == null) Img = new ImageHelper(Console, path);
            else Img.Create(Console, path);

            LoadImage(Img.Source);
        }

        public bool LoadImage(Bitmap src)
        {
            if (src == null) return false;

            try
            {
                Bitmap img = (Bitmap)src.Clone();

                Img.Interpolation = (InterpolationMode)imageintpl.SelectedIndex;
                Img.FitAspectRatio = radioButton2.Checked;

                // Additionally edit image before generating files, e.g. with modification of image palette/brightness, used only for images with exact resolution of original screen size
                // ********
                switch (Console)
                {
                    default:
                        break;

                    case Console.NES:
                        if (InjectorsList.SelectedIndex == 0 && src.Width == 256 && (src.Height == 224 || src.Height == 240) && CO.Settings != null && CO.Settings["use_tImg"] == "1")
                        {
                            var CO_NES = CO as Options_VC_NES;

                            if (CO_NES.ImgPaletteIndex == -1 || oldImgPath != newImgPath)
                                CO_NES.ImgPaletteIndex = CO_NES.CheckPalette(img);

                            img = CO_NES.SwapColors(img, CO_NES.Palettes[CO_NES.ImgPaletteIndex], CO_NES.Palettes[int.Parse(CO_NES.Settings["palette"])]);
                        }
                        break;

                    case Console.SMS:
                    case Console.SMDGEN:
                        break;
                }

                Img.Generate(img);
                img.Dispose();

                if (Img.Source != null)
                {
                    SaveIcon_Panel.BackgroundImage = Img.SaveIcon();
                    pictureBox2.Image = Preview.Icon(Img.IconVCPic);
                }

                CheckExport();
                return true;
            }
            catch
            {
                MessageBox.Show(Language.Get("Error.001"));
                return false;
            }
        }

        public void LoadROM(bool UseLibRetro = true) => LoadROM(Parent.BrowseROM.FileName, UseLibRetro);

        public void LoadROM(string ROMpath, bool UseLibRetro = true)
        {
            switch (Console)
            {
                default:
                case Console.NES:
                case Console.SNES:
                case Console.N64:
                case Console.SMS:
                case Console.SMDGEN:
                case Console.PCE:
                case Console.C64:
                case Console.MSX:
                    if (ROM == null || !ROM.CheckValidity(ROMpath))
                    {
                        MessageBox.Show(Language.Get("Message.002"), 0, Ookii.Dialogs.WinForms.TaskDialogIcon.Warning);
                        return;
                    }
                    break;

                case Console.NeoGeo:
                    // Check if ZIP archive is of valid format
                    // ****************
                    if (!ROM.CheckZIPValidity(ROMpath, new string[] { "c1", "c2", "m1", "p1", "s1", "v1" }, true, true))
                    {
                        MessageBox.Show(Language.Get("Message.002"), 0, Ookii.Dialogs.WinForms.TaskDialogIcon.Warning);
                        return;
                    }
                    break;

                case Console.Flash:
                    break;
            }

            if (ROM != null) ROM.Path = ROMpath;
            ROMLoaded = true;

            PatchFile = null;
            ImportPatch.Checked = false;

            Random.Visible =
            groupBox1.Enabled =
            groupBox2.Enabled =
            groupBox2.Enabled =
            groupBox3.Enabled =
            groupBox4.Enabled =
            groupBox5.Enabled =
            groupBox6.Enabled =
            groupBox7.Enabled = true;

            RandomTID();
            UpdateBaseForm();
            CheckExport();

            Parent.tabControl.Visible = true;

            if (ROM != null && UseLibRetro && CheckToolStripButtons()[0]) LoadLibRetroData();
        }

        public async void LoadLibRetroData()
        {
            if (ROM == null || ROM.Path == null) return;

            try
            {
                LibRetro = new LibRetroDB { SoftwarePath = ROM.Path };
                var Retrieved = await Task.FromResult(LibRetro.Get(Console));
                if (Retrieved)
                {
                    // Set banner title
                    BannerTitle.Text = Creator.BannerTitle = LibRetro.GetCleanTitle() ?? Creator.BannerTitle;

                    // Set channel title text
                    if (LibRetro.GetCleanTitle() != null)
                    {
                        var text = LibRetro.GetCleanTitle().Replace("\r", "").Split('\n');
                        if (text[0].Length <= ChannelTitle.MaxLength) { ChannelTitle_Locale.Checked = false; ChannelTitle.Text = text[0]; }
                        if (ChannelTitle.TextLength <= SaveDataTitle.MaxLength) SaveDataTitle.Text = ChannelTitle.Text;
                    }

                    // Set image
                    if (LibRetro.GetImgURL() != null) { LoadImage(LibRetro.GetImgURL()); }

                    // Set year and players
                    ReleaseYear.Value = Creator.BannerYear    = !string.IsNullOrEmpty(LibRetro.GetYear())    ? int.Parse(LibRetro.GetYear())    : Creator.BannerYear;
                    Players.Value     = Creator.BannerPlayers = !string.IsNullOrEmpty(LibRetro.GetPlayers()) ? int.Parse(LibRetro.GetPlayers()) : Creator.BannerPlayers;
                }

                if (Retrieved) CheckExport();

                // Show message if partially failed to retrieve data
                if (Retrieved && (LibRetro.GetTitle() == null || LibRetro.GetPlayers() == null || LibRetro.GetYear() == null || LibRetro.GetImgURL() == null))
                    MessageBox.Show(Language.Get("Message.004"));
                else if (!Retrieved) System.Media.SystemSounds.Beep.Play();

            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Get("Error"), ex.Message, MessageBoxButtons.OK, Ookii.Dialogs.WinForms.TaskDialogIcon.Error);
            }
        }

        public bool CreateInject()
        {
            try
            {
                Parent.CleanTemp();

                Creator.Out = Parent.SaveWAD.FileName;
                if (PatchFile != null) ROM.Patch(PatchFile, false);

                OutWAD = new WAD();

                // Get WAD data
                // *******
                if (WADPath != null) OutWAD = WAD.Load(WADPath);
                else for (int x = 0; x < Database.Length; x++)
                        if (Database[x].TitleID.ToUpper() == baseID.Text.ToUpper()) OutWAD = Database[x].Load();

                switch (Console)
                {
                    case Console.NES:
                    case Console.SNES:
                    case Console.N64:
                    case Console.SMS:
                    case Console.SMDGEN:
                    case Console.PCE:
                    case Console.NeoGeo:
                    case Console.MSX:
                        if (IsVCMode())
                            WiiVCInject();
                        else
                            ForwarderCreator();
                        break;

                    case Console.GBA:
                        ForwarderCreator();
                        break;

                    case Console.Flash:
                        FlashInject();
                        break;

                    default:
                        throw new NotImplementedException();
                }

                // Banner
                // *******
                BannerHelper.Modify(OutWAD, Console, OutWAD.Region, Creator.BannerTitle, Creator.BannerYear, Creator.BannerPlayers);
                if (Img.VCPic != null) Img.ReplaceBanner(OutWAD);

                // Other WAD settings to be changed
                // *******
                OutWAD.Region = RegionsList.SelectedItem.ToString() == Language.Get("Region.J") ? libWiiSharp.Region.Japan
                    : RegionsList.SelectedItem.ToString() == Language.Get("Region.U") ? libWiiSharp.Region.USA
                    : RegionsList.SelectedItem.ToString() == Language.Get("Region.E") ? libWiiSharp.Region.Europe
                    : RegionsList.SelectedItem.ToString() == Language.Get("Region.K") ? libWiiSharp.Region.Korea
                    : libWiiSharp.Region.Free;

                // Remaining ones done by WAD creator helper, which will save to a new file
                // *******
                Creator.MakeWAD(OutWAD);

                // Check new WAD file
                // *******
                if (File.Exists(Creator.Out) && File.ReadAllBytes(Creator.Out).Length > 10)
                {
                    System.Media.SystemSounds.Beep.Play();
                    Tag = null;

                    if (Properties.Settings.Default.AutoOpenFolder)
                    {
                        string args = string.Format("/e, /select, \"{0}\"", Creator.Out);

                        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                        info.FileName = "explorer";
                        info.Arguments = args;
                        System.Diagnostics.Process.Start(info);
                    }
                    else
                        MessageBox.Show(string.Format(Language.Get("Message.003"), Creator.Out), MessageBoxButtons.OK, Ookii.Dialogs.WinForms.TaskDialogIcon.Information);

                    return true;
                }
                else throw new Exception(Language.Get("Error.006"));
            }

            catch (Exception ex)
            {
                MessageBox.Show(Language.Get("Error"), ex.Message, MessageBoxButtons.OK, Ookii.Dialogs.WinForms.TaskDialogIcon.Error);
                return false;
            }

            finally
            {
                Creator.Out = null;
                OutWAD = null;
                Parent.CleanTemp();
                if (VC != null) VC.Dispose();
            }
        }

        public void ForwarderCreator()
        {
            Forwarder f = new Forwarder()
            {
                ROM = ROM.Bytes,
                ROMExtension = Path.GetExtension(ROM.Path),
                ID = Creator.TitleID,
                Emulator = InjectorsList.SelectedItem.ToString(),
                Storage = FStorage_USB.Checked ? Forwarder.Storages.USB : Forwarder.Storages.SD
            };

            // Actually inject everything
            // *******
            f.CreateZIP(Path.Combine(Path.GetDirectoryName(Creator.Out), Path.GetFileNameWithoutExtension(Creator.Out) + $" ({f.Storage}).zip"));
            OutWAD = f.CreateWAD(OutWAD, toggleSwitch1.Checked);
        }

        public void FlashInject()
        {
            OutWAD = Injectors.Flash.Inject(OutWAD, ROM.Path, Creator.SaveDataTitle, Img);
        }

        public void WiiVCInject()
        {
            // Create Wii VC injector to use
            // *******
            switch (Console)
            {
                default:
                    throw new NotImplementedException();

                // NES
                // *******
                case Console.NES:
                    VC = new WiiVC.NES();
                    break;

                // SNES
                // *******
                case Console.SNES:
                    VC = new WiiVC.SNES();
                    break;

                // N64
                // *******
                case Console.N64:
                    VC = new WiiVC.N64()
                    {
                        Settings = CO.Settings,

                        CompressionType = CO.EmuType == 3 ? (CO.Settings.ElementAt(3).Value == "True" ? 1 : 2) : 0,
                        Allocate = CO.Settings.ElementAt(3).Value == "True" && (CO.EmuType <= 1),
                    };
                    break;

                // SEGA
                // *******
                case Console.SMS:
                case Console.SMDGEN:
                    VC = new WiiVC.SEGA()
                    {
                        IsSMS = Console == Console.SMS
                    };
                    break;

                // PCE
                // *******
                case Console.PCE:
                    VC = new WiiVC.PCE()
                    {
                    };
                    break;

                // NEOGEO
                // *******
                case Console.NeoGeo:
                    VC = new WiiVC.NeoGeo()
                    {
                        BIOSPath = CO?.Settings?.Count > 0 ? CO?.Settings?.ElementAt(0).Value : null
                    };
                    break;

                // MSX
                // *******
                case Console.MSX:
                    VC = new WiiVC.MSX();
                    break;
            }

            VC.WAD = OutWAD;

            // Get settings from relevant form
            // *******
            if (CO != null) { VC.Settings = CO.Settings; } else { VC.Settings = new Dictionary<string, string> { { "N/A", "N/A" } }; }

            // Set path to manual folder (if it exists) and load WAD
            // *******
            VC.ManualPath = Manual;

            // Actually inject everything
            // *******
            OutWAD = VC.Inject(ROM, Creator.SaveDataTitle, Img);
        }
        #endregion

        #region **Console-Specific Functions**
        // ******************
        // CONSOLE-SPECIFIC
        // ******************
        private void OpenInjectorOptions(object sender, EventArgs e)
        {
            var result = CO.ShowDialog(this) == DialogResult.OK;

            switch (Console)
            {
                case Console.NES:
                    if (result) { LoadImage(); }
                    break;
                case Console.SNES:
                    break;
                case Console.N64:
                    if (result) { CheckExport(); }
                    break;
                case Console.SMS:
                    break;
                case Console.SMDGEN:
                    break;
                case Console.PCE:
                    break;
                case Console.NeoGeo:
                    break;
                case Console.MSX:
                    break;
                case Console.Flash:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Base WAD Management/Visual
        private void AddBases()
        {
            Database = DatabaseHelper.Get(Console) ?? DatabaseHelper.Get(Console.Flash);
            string ID = null;

            for (int x = 0; x < Database.Length; x++)
            {
                if (Database[x].TitleID.Substring(0, 3) != ID)
                {
                    Base.Items.Add(Database[x].DisplayName);
                    ID = Database[x].TitleID.Substring(0, 3);
                }
            }

            if (Base.Items.Count > 0) { Base.SelectedIndex = 0; }

            Base.Enabled = Base.Items.Count > 1;
        }


        // -----------------------------------

        private void Base_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ----------------------------
            if (DesignMode) return;
            // ----------------------------

            // Reset currently-selected base info
            // ********
            CurrentBase = new Dictionary<string, string>();
            WADRegionList.Items.Clear();

            // Add base native names to temporary list
            // ********
            var tempList = new List<string>();
            var tempIDs = new List<string>();
            for (int x = 0; x < Database.Length; x++) tempList.Add(Database[x].DisplayName);
            for (int x = 0; x < Database.Length; x++) tempIDs.Add(Database[x].TitleID.Substring(0, 3));

            int start = tempList.IndexOf(Base.SelectedItem.ToString());
            int end = start == Database.Length - 1 ? Database.Length : -1;

            for (int x = start; x < Database.Length; x++)
            {
                if (Database[x].TitleID.Substring(0, 3) != Database[start].TitleID.Substring(0, 3) && end == -1)
                    end = x;
            }

            if (end == -1) end = Database.Length;

            // Add regions to WAD region context list
            // ********
            for (int x = start; x < end; x++)
            {
                // Add region of entry to context list
                // ********
                switch (Database[x].Region())
                {
                    case 0:
                        WADRegionList.Items.Add(Language.Get("Region.U"), null, WADRegionList_Click);
                        break;

                    case 1:
                    case 2:
                        WADRegionList.Items.Add(Language.Get("Region.E"), null, WADRegionList_Click);
                        break;

                    case 3:
                        WADRegionList.Items.Add(Language.Get("Region.J"), null, WADRegionList_Click);
                        break;

                    case 4:
                    case 5:
                        WADRegionList.Items.Add(Language.Get("Region.K"), null, WADRegionList_Click);
                        break;

                    default:
                        break;
                }

                CurrentBase.Add(Database[x].TitleID, Database[x].DisplayName);
            }

            // Check if language is set to Japanese or Korean
            // If so, make Japan/Korea region item the first in the WAD region context list
            // ********
            string langCode = Language.Current.TwoLetterISOLanguageName;
            if (langCode == "ja" || langCode == "ko")
            {
                string target = langCode == "ja" ? Language.Get("Region.J") : Language.Get("Region.K");

                for (int i = 0; i < WADRegionList.Items.Count; i++)
                    if ((WADRegionList.Items[i] as ToolStripMenuItem).Text == target)
                    {
                        // Swap first element of context list with Japan/Korea
                        // ********
                        var tempDict = new Dictionary<string, string> { { WADRegionList.Items[i].Text, null } };

                        for (int j = 0; j < CurrentBase.Count; j++)
                            try { tempDict.Add(WADRegionList.Items[j].Text, null); } catch { }

                        for (int x = 0; x < WADRegionList.Items.Count; x++)
                        {
                            var item = WADRegionList.Items[x] as ToolStripMenuItem;
                            item.Text = tempDict.ElementAt(x).Key;
                        }

                        // Likewise do the same for the CurrentBase dictionary
                        // ********
                        tempDict = new Dictionary<string, string> { { CurrentBase.ElementAt(i).Key, CurrentBase.ElementAt(i).Value } };

                        for (int j = 0; j < CurrentBase.Count; j++)
                            if (CurrentBase.ElementAt(j).Key != tempDict.ElementAt(0).Key)
                                tempDict.Add(CurrentBase.ElementAt(j).Key, CurrentBase.ElementAt(j).Value);
                        CurrentBase = tempDict;
                    }
            }

            // Final visual updates
            // ********
            (WADRegionList.Items[0] as ToolStripMenuItem).Checked = true;
            UpdateBaseForm(0);
            WADRegion.Cursor = WADRegionList.Items.Count == 1 ? Cursors.Default : Cursors.Hand;
        }

        private void WADRegion_Click(object sender, EventArgs e)
        {
            if (WADRegionList.Items.Count > 1)
                WADRegionList.Show(this, PointToClient(Cursor.Position));
        }

        private void WADRegionList_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in WADRegionList.Items.OfType<ToolStripMenuItem>())
                item.Checked = false;

            string targetRegion = (sender as ToolStripMenuItem).Text;

            for (int i = 0; i < WADRegionList.Items.Count; i++)
            {
                var item = WADRegionList.Items[i] as ToolStripMenuItem;
                if (item.Text == targetRegion)
                {
                    baseID.Text = CurrentBase.ElementAt(i).Key;
                    item.Checked = true;
                    UpdateBaseForm();
                    CheckExport();
                    return;
                }
            }

        }

        private void UpdateBaseForm(int index = -1)
        {
            if (index == -1)
            {
                for (index = 0; index < CurrentBase.Count; index++)
                    if (CurrentBase.ElementAt(index).Key[3] == baseID.Text[3])
                        goto Set;

                return;
            }

            Set:
            // Native name & Title ID
            // ********
            baseName.Text = CurrentBase.ElementAt(index).Value;
            baseID.Text = CurrentBase.ElementAt(index).Key;

            // Flag
            // ********
            if (!WADRegion.Enabled) WADRegion.Image = null;
            else switch (CurrentBase.ElementAt(index).Key[3])
            {
                default:
                case 'E':
                case 'N':
                    WADRegion.Image = Properties.Resources.flag_us;
                    RegionsList.SelectedIndex = 1;
                    break;

                case 'P':
                    WADRegion.Image = (int)Console <= 2 ? Properties.Resources.flag_eu50 : Properties.Resources.flag_eu;
                    RegionsList.SelectedIndex = 2;
                    break;

                case 'L':
                case 'M':
                    WADRegion.Image = (int)Console <= 2 ? Properties.Resources.flag_eu60 : Properties.Resources.flag_eu;
                    RegionsList.SelectedIndex = 2;
                    break;

                case 'J':
                    WADRegion.Image = Properties.Resources.flag_jp;
                    RegionsList.SelectedIndex = 3;
                    break;

                case 'Q':
                case 'T':
                    WADRegion.Image = Properties.Resources.flag_kr;
                    RegionsList.SelectedIndex = 4;
                    break;
            }

            UpdateBaseGeneral(index);
        }

        private void UpdateBaseGeneral(int index)
        {
            int oldSaveLength = SaveDataTitle.MaxLength;

            // Korean WADs use different encoding format & using two lines or going over max limit cause visual bugs
            // ********
            Creator.OrigRegion = CurrentBase.ElementAt(index).Key[3] == 'Q'
                     || CurrentBase.ElementAt(index).Key[3] == 'T'
                     || CurrentBase.ElementAt(index).Key[3] == 'K' ? Creator.RegionType.Korea
                     : CurrentBase.ElementAt(index).Key[3] == 'J' ? Creator.RegionType.Japan
                     : CurrentBase.ElementAt(index).Key[3] == 'P'
                     || CurrentBase.ElementAt(index).Key[3] == 'L'
                     || CurrentBase.ElementAt(index).Key[3] == 'M' ? Creator.RegionType.Europe
                     : Creator.RegionType.Universal;

            // Changing SaveDataTitle max length & clearing text field when needed
            // ----------------------
            if (Console == Console.NES) SaveDataTitle.MaxLength = Creator.OrigRegion == Creator.RegionType.Korea ? 30 : 20;
            else if (Console == Console.SNES) SaveDataTitle.MaxLength = 80;
            else if (Console == Console.N64) SaveDataTitle.MaxLength = 100;
            else if (Console == Console.NeoGeo
                  || Console == Console.MSX) SaveDataTitle.MaxLength = 64;
            else SaveDataTitle.MaxLength = 80;

            // Also, some consoles only support a single line anyway
            // ********
            bool isSingleLine = Creator.OrigRegion == Creator.RegionType.Korea
                             || Console == Console.NES
                             || Console == Console.SMS
                             || Console == Console.SMDGEN
                             || Console == Console.PCE;

            // Set textbox to use single line when needed
            // ********
            if (SaveDataTitle.Multiline == isSingleLine)
            {
                SaveDataTitle.Multiline = !isSingleLine;
                SaveDataTitle.Location = SaveDataTitle.Multiline ? new Point(SaveDataTitle.Location.X, 29) : new Point(SaveDataTitle.Location.X, 35);
                SaveDataTitle.Clear();
                goto End;
            }
            if (Creator.OrigRegion == Creator.RegionType.Korea && SaveDataTitle.Multiline) SaveDataTitle.MaxLength /= 2; // Applies to both NES/FC & SNES/SFC

            // Clear text field if at least one line is longer than the maximum limit allowed
            // ********
            double max = SaveDataTitle.Multiline ? Math.Round((double)SaveDataTitle.MaxLength / 2) : SaveDataTitle.MaxLength;
            foreach (var line in SaveDataTitle.Lines)
                if (line.Length > max && SaveDataTitle.MaxLength != oldSaveLength)
                    SaveDataTitle.Clear();

            End:
            UpdateBaseConsole();
            pictureBox1.Image = Preview.Banner(Console, BannerTitle.Text, (int)ReleaseYear.Value, (int)Players.Value, Img?.VCPic, (int)Creator.OrigRegion);
        }

        /// <summary>
        /// Changes injector settings based on selected base/console
        /// </summary>
        private void UpdateBaseConsole()
        {
            int emuVer = 0;

            if (Database != null)
                foreach (var Entry in Database)
                    if (Entry.TitleID.ToUpper() == baseID.Text.ToUpper())
                        emuVer = Entry.Emulator;

            // ******************
            // CONSOLE-SPECIFIC
            // ******************
            // -------------------------------------------------------------------------------------
            switch (Console)
            {
                case Console.SNES:
                    break;

                case Console.N64:
                    if (IsVCMode()) CO.EmuType = Creator.OrigRegion == Creator.RegionType.Korea ? 3 : emuVer;
                    break;

                case Console.SMS:
                case Console.SMDGEN:
                    CO.EmuType = emuVer;
                    break;

                case Console.PCE:
                    break;

                case Console.NeoGeo:
                    if (emuVer >= 1) CO = null;
                    break;

                case Console.MSX:
                    break;

                default:
                    break;
            }
        }
        #endregion

        private void ChannelTitle_Locale_CheckedChanged(object sender, EventArgs e)
        {
            if (ChannelTitle_Locale.Checked)
            {
                ChannelTitles titles = new ChannelTitles(ChannelTitle.Text);
                if (titles.ShowDialog() == DialogResult.OK)
                {
                    Creator.ChannelTitles = new string[8]
                        {
                        titles.Japanese.Text,
                        titles.English.Text,
                        titles.German.Text,
                        titles.French.Text,
                        titles.Spanish.Text,
                        titles.Italian.Text,
                        titles.Dutch.Text,
                        titles.Korean.Text,
                        };
                    ChannelTitle.Text = Language.Current.TwoLetterISOLanguageName == "ja" ? titles.Japanese.Text
                                          : Language.Current.TwoLetterISOLanguageName == "ko" ? titles.Korean.Text
                                          : Language.Current.TwoLetterISOLanguageName == "nl" ? titles.Dutch.Text
                                          : Language.Current.TwoLetterISOLanguageName == "es" ? titles.Spanish.Text
                                          : Language.Current.TwoLetterISOLanguageName == "it" ? titles.Italian.Text
                                          : Language.Current.TwoLetterISOLanguageName == "fr" ? titles.French.Text
                                          : Language.Current.TwoLetterISOLanguageName == "de" ? titles.English.Text
                                          : titles.English.Text;
                }
            }

            else Creator.ChannelTitles = new string[8] { ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text, ChannelTitle.Text };

            ChannelTitle.Enabled = !ChannelTitle_Locale.Checked;
        }

        private void CustomManual_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomManual.Checked)
            {
                if (!Properties.Settings.Default.DoNotShow_000) MessageBox.Show((sender as Control).Text, Language.Get("Message.006"), 0);

                if (BrowseManual.ShowDialog() == DialogResult.OK)
                {
                    LoadManual(BrowseManual.SelectedPath);
                    CheckExport();
                }

                else
                {
                    if (Manual != null)
                    {
                        LoadManual(null);
                        CheckExport();
                    }

                    CustomManual.Checked = false;
                }
            }

            else if (!CustomManual.Checked && Manual != null)
            {
                LoadManual(null);
                CheckExport();
            }
        }

        private void Patch_CheckedChanged(object sender, EventArgs e)
        {
            if (ImportPatch.Checked)
            {
                if (BrowsePatch.ShowDialog() == DialogResult.OK)
                {
                    if (ROM.Patch(BrowsePatch.FileName, true))
                    {
                        PatchFile = BrowsePatch.FileName;
                        CheckExport();
                    }
                    else
                    {
                        PatchFile = null;
                        ImportPatch.Checked = false;
                    }
                }

                else
                {
                    if (!ImportPatch.Checked && PatchFile != null)
                    {
                        PatchFile = null;
                        CheckExport();
                    }

                    ImportPatch.Checked = false;
                }
            }

            else if (!ImportPatch.Checked && PatchFile != null)
            {
                PatchFile = null;
                CheckExport();
            }
        }

        private bool IsVCMode() => InjectorsList.SelectedItem.ToString().ToLower() == Language.Get("VC").ToLower();

        private void InjectorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            COPanel_VC.Hide();
            COPanel_Forwarder.Hide();
            SaveIcon_Panel.Visible = SaveDataTitle.Visible = false;

            CO = null;
            if (IsVCMode())
            {
                COPanel_VC.Show();
                SaveIcon_Panel.Visible = SaveDataTitle.Visible = true;

                switch (Console)
                {
                    case Console.NES:
                        CO = new Options_VC_NES();
                        break;

                    case Console.SNES:
                        break;

                    case Console.N64:
                        CO = new Options_VC_N64();
                        break;

                    case Console.SMS:
                    case Console.SMDGEN:
                        CO = new Options_VC_SEGA() { IsSMS = Console == Console.SMS };
                        break;

                    case Console.PCE:
                        CO = new Options_VC_PCE();
                        break;

                    case Console.NeoGeo:
                        // CO = new Options_VC_NeoGeo();
                        break;

                    case Console.MSX:
                        break;

                    case Console.C64:
                        break;
                }
                
                UpdateBaseConsole();
            }

            else if (Console == Console.Flash)
            {
                SaveIcon_Panel.Visible = SaveDataTitle.Visible = true;
                CO = new Options_Flash();
            }

            else
                COPanel_Forwarder.Show();

            label16.Visible = !SaveDataTitle.Visible;
            var selected = COPanel_Forwarder.Visible ? COPanel_Forwarder : COPanel_VC.Visible ? COPanel_VC : null;
            int height = selected == null ? (groupBox7.Visible ? groupBox7.Location.Y + groupBox7.Height : InjectorsList.Location.Y + InjectorsList.Height) + 10 : selected.Location.Y + selected.Height + 10;
            groupBox3.Size = new Size(groupBox3.Width, height);

            if (groupBox3.Enabled) CheckExport();
        }

        private void RegionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupBox4.Enabled) CheckExport();
        }

        private void ToggleSwitchChanged(object sender, EventArgs e)
        {
            if (sender == toggleSwitch1) toggleSwitchL1.Text = toggleSwitch1.Checked ? "vWii (Wii U)" : "Wii";
        }
    }
}
