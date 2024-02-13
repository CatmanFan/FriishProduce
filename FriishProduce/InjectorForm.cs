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
        protected string                        ROM         { get; set; }
        protected Database                      Database    { get; set; }
        protected IDictionary<string, string>   CurrentBase { get; set; }
        protected TitleImage                    tImg        { get; set; }
        protected Injector                      i           { get; set; }
        protected InjectorWiiVC                 WiiVC       { get; set; }

        // -----------------------------------
        // Options
        // -----------------------------------
        protected ContentOptions                CO { get; set; }

        // -----------------------------------
        // Connection with parent form
        // -----------------------------------
        public new MainForm Parent { get; set; }

        public event EventHandler ExportCheck;


        // -----------------------------------

        public void RefreshForm()
        {
            Language.AutoSetForm(this);

            // Change title text to untitled string
            Untitled = string.Format(Language.Get("Untitled"), Language.Get($"Platform_{Enum.GetName(typeof(Console), Console)}"));
            Text = string.IsNullOrWhiteSpace(i.ChannelTitle) ? Untitled : i.ChannelTitle;
            SoftwareName.Location = new Point(label2.Location.X + label2.Width - 4, label2.Location.Y);
            baseName.Location = new Point(label4.Location.X + label4.Width - 4, label4.Location.Y);
            baseID.Location = new Point(label5.Location.X + label5.Width - 4, label5.Location.Y);

            // Selected index properties
            imageintpl.Items.Clear();
            imageintpl.Items.Add(Language.Get("ByDefault"));
            imageintpl.Items.AddRange(Language.GetArray("List_ImageInterpolation"));
            imageintpl.SelectedIndex = Properties.Settings.Default.ImageInterpolation;
        }

        public InjectorForm(Console c, string ROM)
        {
            Console = c;
            InitializeComponent();

            // Declare WAD metadata modifier
            // ********
            i = new Injector(Console);

            switch (Console)
            {
                case Console.NES:
                    Icon = Properties.Resources.nintendo_nes;
                    TIDCode = "F";
                    CO = new Options_VC_NES();
                    break;

                case Console.SNES:
                    Icon = Properties.Resources.nintendo_super_nes;
                    TIDCode = "J";
                    break;

                case Console.N64:
                    Icon = Properties.Resources.nintendo_nintendo64;
                    TIDCode = "N";
                    CO = new Options_VC_N64();
                    break;

                case Console.SMS:
                    Icon = Properties.Resources.sega_master_system;
                    TIDCode = "L";
                    CO = new Options_VC_SEGA();
                    break;

                case Console.SMDGEN:
                    Icon = Properties.Resources.sega_genesis;
                    TIDCode = "M";
                    CO = new Options_VC_SEGA();
                    break;

                case Console.PCE:
                    Icon = Properties.Resources.nec_turbografx_16;
                    TIDCode = "P"; // Q for CD games
                    break;

                case Console.NeoGeo:
                    Icon = Properties.Resources.snk_neo_geo_aes;
                    TIDCode = "E";
                    break;

                case Console.MSX:
                    TIDCode = "X";
                    break;

                default:
                case Console.Flash:
                    TIDCode = null;
                    break;
            }

            // Cosmetic
            // ********
            if (Console == Console.SMS || Console == Console.SMDGEN) SaveIcon_Panel.BackgroundImage = Properties.Resources.SaveIconPlaceholder_SEGA;
            button1.Enabled = CO != null;
            RefreshForm();

            i.BannerYear = (int)ReleaseYear.Value;
            i.BannerPlayers = (int)Players.Value;
            AddBases();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (ROM != null) LoadROM(ROM, Properties.Settings.Default.AutoLibRetro);
        }

        // -----------------------------------

        protected virtual void CheckExport()
        {
            i.TitleID = TitleID.Text;
            i.ChannelTitle = ChannelTitle.Text;
            i.BannerTitle = BannerTitle.Text;
            i.BannerYear = (int)ReleaseYear.Value;
            i.BannerPlayers = (int)Players.Value;
            i.SaveDataTitle =
                SaveDataTitle.Lines.Length == 1 ? new string[] { SaveDataTitle.Text } :
                SaveDataTitle.Lines.Length == 0 ? new string[] { "" } :
                SaveDataTitle.Lines;

            button2.Enabled = tImg != null && !string.IsNullOrEmpty(i.BannerTitle);

            ReadyToExport =    !string.IsNullOrEmpty(i.TitleID) && i.TitleID.Length == 4
                            && !string.IsNullOrWhiteSpace(i.ChannelTitle)
                            && !string.IsNullOrEmpty(i.BannerTitle)
                            && !string.IsNullOrEmpty(i.SaveDataTitle[0])
                            && (tImg != null)
                            && ROM != null;
            Tag = "dirty";
            ExportCheck.Invoke(this, EventArgs.Empty);
        }

        private void RandomTID() => TitleID.Text = i.TitleID = TIDCode != null ? TIDCode + GenerateTitleID().Substring(0, 3) : GenerateTitleID();

        public string GetName() => $"{ChannelTitle.Text} [{TitleID.Text.ToUpper()}]";

        private void isClosing(object sender, FormClosingEventArgs e)
        {
            if (Tag != null && Tag.ToString() == "dirty")
                if (MessageBox.Show(string.Format(Language.Get("Message001"), Text), Parent.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    e.Cancel = true;
        }

        private void Random_Click(object sender, EventArgs e) => RandomTID();

        private void Value_Changed(object sender, EventArgs e) => CheckExport();

        private void TextBox_Changed(object sender, EventArgs e)
        {
            if (sender == ChannelTitle)
            {
                Text = string.IsNullOrWhiteSpace(ChannelTitle.Text) ? Untitled : ChannelTitle.Text;
                if (ChannelTitle.TextLength <= SaveDataTitle.MaxLength) SaveDataTitle.Text = ChannelTitle.Text;
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

        #region Load Data Functions
        private void ShowBanner_Click(object sender, EventArgs e)
        {
            BannerPreview preview = new BannerPreview(i.BannerTitle, i.BannerYear, i.BannerPlayers, tImg.VCPic, i.isJapan ? 1 : i.isKorea ? 2 : 0);
            preview.Text = string.Join(" - ", BannerTitle.Lines);
            preview.ShowDialog(this);
            preview.Dispose();
        }

        private string GenerateTitleID()
        {
            var r = new Random();
            string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(allowed, 4).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        private void OpenWAD_CheckedChanged(object sender, EventArgs e)
        {
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
                BrowseWAD.Filter = Language.Get("Filter_WAD");
                var result = BrowseWAD.ShowDialog();

                if (result == DialogResult.OK && !LoadWAD(BrowseWAD.FileName)) OpenWAD.Checked = false;
                else if (result == DialogResult.Cancel) OpenWAD.Checked = false;
            }
            else
            {
                WADPath = null;
            }
        }

        public bool LoadWAD(string path)
        {
            WAD Reader = new WAD();
            try { Reader = WAD.Load(path); } catch { goto Failed; }

            for (int x = 0; x < Database.List.Length; x++)
                if (Database.List[x].TitleID.ToUpper() == Reader.UpperTitleID.ToUpper())
                {
                    WADPath = path;

                    CurrentBase.Clear();
                    CurrentBase.Add(Database.List[x].TitleID, Database.List[x].NativeName);
                    baseName.Text = Database.List[x].NativeName;
                    baseID.Text = Database.List[x].TitleID;
                    UpdateBaseGeneral(0);
                    Reader.Dispose();
                    return true;
                }

            Reader.Dispose();

            Failed:
            System.Media.SystemSounds.Beep.Play();
            MessageBox.Show(string.Format(Language.Get("Message005"), Reader.UpperTitleID), Parent.Text);
            return false;
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            if (imageintpl.SelectedIndex != Properties.Settings.Default.ImageInterpolation) Tag = "dirty";
            if (i != null && tImg != null) LoadImage();
        }

        public void LoadImage()
        {
            if (tImg != null) LoadImage(tImg.Source);
            else CheckExport();
        }

        public void LoadImage(string path)
        {
            if (tImg != null) oldImgPath = newImgPath;
            newImgPath = path;

            if (tImg == null) tImg = new TitleImage(Console, path);
            else tImg.Create(Console, path);

            LoadImage(tImg.Source);
        }

        public bool LoadImage(Bitmap src)
        {
            try
            {
                Bitmap img = (Bitmap)src.Clone();

                tImg.Interpolation = (InterpolationMode)imageintpl.SelectedIndex;

                // Additionally edit image before generating files, e.g. with modification of image palette/brightness, used only for images with exact resolution of original screen size
                // ********
                switch (Console)
                {
                    default:
                        break;

                    case Console.NES:
                        if (src.Width == 256 && (src.Height == 224 || src.Height == 240) && CO.Settings != null && CO.Settings["use_tImg"] == "1")
                        {
                            var CO_NES = CO as Options_VC_NES;
                            if (CO_NES.ImgPaletteIndex == -1 || oldImgPath != newImgPath) CO_NES.ImgPaletteIndex = CO_NES.CheckPalette(img);
                                img = CO_NES.SwapColors(img, CO_NES.Palettes[CO_NES.ImgPaletteIndex], CO_NES.Palettes[int.Parse(CO_NES.Settings["palette"])]);
                        }
                        break;

                    case Console.SMS:
                    case Console.SMDGEN:
                        break;
                }

                tImg.Generate(img);
                img.Dispose();

                if (tImg.Source != null)
                {
                    Preview.Image = tImg.IconVCPic;
                    SaveIcon_Panel.BackgroundImage = tImg.SaveIcon();
                }

                CheckExport();
                return true;
            }
            catch
            {
                MessageBox.Show(Language.Get("Error001"), Parent.Text);
                return false;
            }
        }

        public void LoadROM(bool UseLibRetro = true) => LoadROM(Parent.BrowseROM.FileName, UseLibRetro);

        public void LoadROM(string ROMpath, bool UseLibRetro = true)
        {
            ROM = ROMpath;
            ROMLoaded = true;

            // Set ROM name & serial text
            label1.Text = Language.Get("label1", this);
            label1.Text += Path.GetFileName(ROM);

            if (!UseLibRetro)
            {
                SoftwareName.Text = Language.Get("Unknown");
                SoftwareName.Font = new Font(SoftwareName.Font, FontStyle.Regular);
                label3.Text = Language.Get("label3", this) + Language.Get("Unknown");
            }

            Random.Visible =
            groupBox1.Enabled =
            groupBox2.Enabled =
            groupBox3.Enabled =
            groupBox4.Enabled =
            groupBox5.Enabled =
            groupBox6.Enabled =
            groupBox8.Enabled = true;

            UpdateBaseForm();

            RandomTID();
            CheckExport();

            Parent.tabControl.Visible = true;

            if (ROM != null && UseLibRetro) LoadLibRetroData();
        }

        public void LoadLibRetroData()
        {
            try
            {
                var LibRetro = Parent.LibRetro;
                LibRetro = new LibRetroDB { SoftwarePath = ROM };

                bool Retrieved = LibRetro.GetData(Console);
                if (Retrieved)
                {
                    // Set banner title
                    BannerTitle.Text = i.BannerTitle = LibRetro.GetCleanTitle() ?? i.BannerTitle;

                    // Set channel title text
                    if (LibRetro.GetCleanTitle() != null)
                    {
                        var text = LibRetro.GetCleanTitle().Replace("\r", "").Split('\n');
                        if (text[0].Length <= ChannelTitle.MaxLength) ChannelTitle.Text = i.ChannelTitle = text[0];
                        if (ChannelTitle.TextLength <= SaveDataTitle.MaxLength) SaveDataTitle.Text = ChannelTitle.Text;
                    }

                    // Set image
                    if (LibRetro.GetImgURL() != null) { LoadImage(LibRetro.GetImgURL()); }

                    // Set year and players
                    ReleaseYear.Value = i.BannerYear    = !string.IsNullOrEmpty(LibRetro.GetYear())    ? int.Parse(LibRetro.GetYear())    : i.BannerYear;
                    Players.Value     = i.BannerPlayers = !string.IsNullOrEmpty(LibRetro.GetPlayers()) ? int.Parse(LibRetro.GetPlayers()) : i.BannerPlayers;
                }

                // Set ROM name & serial text
                SoftwareName.Text = LibRetro.GetTitle() ?? Language.Get("Unknown");
                SoftwareName.Font = SoftwareName.Text != Language.Get("Unknown") ? new Font(SoftwareName.Font, FontStyle.Bold) : new Font(SoftwareName.Font, FontStyle.Regular);
                label3.Text = Language.Get("label3", this);
                label3.Text += LibRetro.GetSerial() ?? Language.Get("Unknown");

                // Show message if partially failed to retrieve data
                if (Retrieved && (LibRetro.GetTitle() == null || LibRetro.GetPlayers() == null || LibRetro.GetYear() == null || LibRetro.GetImgURL() == null))
                    MessageBox.Show(Language.Get("Message004"), Parent.Text);
                else if (!Retrieved) System.Media.SystemSounds.Beep.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Language.Get("Error"), MessageBoxButtons.OK);
            }
        }

        public bool CreateInject()
        {
            i.Out = Parent.SaveWAD.FileName;

            try
            {
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
                        WiiVCInject(); // To-Do: Consider forwarder function
                        break;

                    default:
                    case Console.Flash:
                        throw new NotImplementedException();
                }

                // Check new WAD file
                // *******
                if (File.Exists(i.Out) && File.ReadAllBytes(i.Out).Length > 10)
                {
                    System.Media.SystemSounds.Beep.Play();
                    Tag = null;

                    if (Properties.Settings.Default.AutoOpenFolder)
                        System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{i.Out}\"");
                    else
                        MessageBox.Show(string.Format(Language.Get("Message003"), i.Out), Language.Get("_AppTitle"), MessageBoxButtons.OK);

                    return true;
                }
                else throw new Exception(Language.Get("Error006"));
            }

            catch (Exception ex)
            {
                i.ShowErrorMessage(ex);
                return false;
            }

            finally
            {
                Parent.CleanTemp();
                if (WiiVC != null) WiiVC.Dispose();
            }
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
                    WiiVC = new WiiVC.NES();
                    break;


                // SNES
                // *******
                case Console.SNES:
                    WiiVC = new WiiVC.SNES();
                    break;


                // N64
                // *******
                case Console.N64:
                    WiiVC = new WiiVC.N64()
                    {
                        CompressionType = CO.EmuType == 3 ? (CO.Settings.ElementAt(3).Value == "True" ? 1 : 2) : 0,
                        Allocate = CO.Settings.ElementAt(3).Value == "True" && (CO.EmuType <= 1),
                    };
                    break;


                // SEGA
                // *******
                case Console.SMS:
                case Console.SMDGEN:
                    WiiVC = new WiiVC.SEGA()
                    {
                        IsSMS = Console == Console.SMS
                    };
                    break;
            }

            // Set path to manual folder (if it exists) and load WAD
            // *******
            WiiVC.Settings = CO.Settings;
            WiiVC.ManualPath = null;
            if (WADPath != null) WiiVC.WAD = WAD.Load(WADPath);
            else for (int x = 0; x < Database.List.Length; x++)
                if (Database.List[x].TitleID.ToUpper() == baseID.Text.ToUpper()) WiiVC.WAD = Database.Load(x);

            // Actually inject everything
            // *******
            i.Create(WiiVC.Inject(ROM, i.SaveDataTitle, tImg), tImg);
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
            Database = new Database(Console);
            string ID = null;

            for (int x = 0; x < Database.List.Length; x++)
            {
                if (Database.List[x].TitleID.Substring(0, 3) != ID)
                {
                    Base.Items.Add(Database.List[x].NativeName);
                    ID = Database.List[x].TitleID.Substring(0, 3);
                }
            }

            if (Base.Items.Count > 0) { Base.SelectedIndex = 0; }
        }


        // -----------------------------------

        private void Base_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset currently-selected base info
            // ********
            CurrentBase = new Dictionary<string, string>();
            WADRegionList.Items.Clear();

            // Add base native names to temporary list
            // ********
            var tempList = new List<string>();
            var tempIDs = new List<string>();
            for (int x = 0; x < Database.List.Length; x++) tempList.Add(Database.List[x].NativeName);
            for (int x = 0; x < Database.List.Length; x++) tempIDs.Add(Database.List[x].TitleID.Substring(0, 3));

            string oldID = null;

            // Add regions to WAD region context list
            // ********
            for (int x = tempList.IndexOf(Base.SelectedItem.ToString()); x < Database.List.Length; x++)
            {
                // If base Title ID code is no longer identical, break loop
                // Update oldID variable
                // ********
                if (oldID == null) oldID = Database.List[x].TitleID.Substring(0, 3);
                else if (oldID != Database.List[x].TitleID.Substring(0, 3))
                    break;

                // Add region of entry to context list
                // ********
                switch (Database.GetRegion(x))
                {
                    case Database.Region.USA:
                        WADRegionList.Items.Add(Language.Get("Region_U"), null, WADRegionList_Click);
                        break;

                    case Database.Region.PAL50:
                    case Database.Region.PAL60:
                        WADRegionList.Items.Add(Language.Get("Region_E"), null, WADRegionList_Click);
                        break;

                    case Database.Region.JPN:
                        WADRegionList.Items.Add(Language.Get("Region_J"), null, WADRegionList_Click);
                        break;

                    case Database.Region.KOR_Ja:
                    case Database.Region.KOR_En:
                        WADRegionList.Items.Add(Language.Get("Region_K"), null, WADRegionList_Click);
                        break;

                    default:
                        break;
                }

                CurrentBase.Add(Database.List[x].TitleID, Database.List[x].NativeName);
            }

            // Check if language is set to Japanese or Korean
            // If so, make Japan/Korea region item the first in the WAD region context list
            // ********
            string langCode = Language.Current.TwoLetterISOLanguageName;
            if (langCode == "ja" || langCode == "ko")
            {
                string target = langCode == "ja" ? Language.Get("Region_J") : Language.Get("Region_K");

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
                    break;

                case 'P':
                    WADRegion.Image = (int)Console <= 2 ? Properties.Resources.flag_eu50 : Properties.Resources.flag_eu;
                    break;

                case 'L':
                case 'M':
                    WADRegion.Image = (int)Console <= 2 ? Properties.Resources.flag_eu60 : Properties.Resources.flag_eu;
                    break;

                case 'J':
                    WADRegion.Image = Properties.Resources.flag_jp;
                    break;

                case 'Q':
                case 'T':
                    WADRegion.Image = Properties.Resources.flag_kr;
                    break;
            }

            UpdateBaseGeneral(index);
        }

        private void UpdateBaseGeneral(int index)
        {
            int oldSaveLength = SaveDataTitle.MaxLength;

            // Changing SaveDataTitle max length & clearing text field when needed
            // ----------------------
            if (Console == Console.NES) SaveDataTitle.MaxLength = i.isKorea ? 30 : 20;
            else if (Console == Console.SNES) SaveDataTitle.MaxLength = 80;
            else if (Console == Console.N64) SaveDataTitle.MaxLength = 100;
            else if (Console == Console.NeoGeo
                  || Console == Console.MSX) SaveDataTitle.MaxLength = 64;
            else SaveDataTitle.MaxLength = 80;

            // Korean WADs use different encoding format & using two lines or going over max limit cause visual bugs
            // ********
            i.isKorea = CurrentBase.ElementAt(index).Key[3] == 'Q'
                     || CurrentBase.ElementAt(index).Key[3] == 'T'
                     || CurrentBase.ElementAt(index).Key[3] == 'K';
            i.isJapan = CurrentBase.ElementAt(index).Key[3] == 'J';

            // Also, some consoles only support a single line anyway
            // ********
            bool isSingleLine = i.isKorea
                             || Console == Console.NES
                             || Console == Console.SMS
                             || Console == Console.SMDGEN;

            // Set textbox to use single line when needed
            // ********
            if (SaveDataTitle.Multiline == isSingleLine)
            {
                SaveDataTitle.Multiline = !isSingleLine;
                SaveDataTitle.Location = SaveDataTitle.Multiline ? new Point(SaveDataTitle.Location.X, 26) : new Point(SaveDataTitle.Location.X, 26 + 7);
                SaveDataTitle.Clear();
                goto End;
            }
            if (i.isKorea && SaveDataTitle.Multiline) SaveDataTitle.MaxLength /= 2; // Applies to both NES/FC & SNES/SFC

            // Clear text field if at least one line is longer than the maximum limit allowed
            // ********
            double max = SaveDataTitle.Multiline ? Math.Round((double)SaveDataTitle.MaxLength / 2) : SaveDataTitle.MaxLength;
            foreach (var line in SaveDataTitle.Lines)
                if (line.Length > max && SaveDataTitle.MaxLength != oldSaveLength)
                    SaveDataTitle.Clear();

            End:
            foreach (var item in Database.List)
                foreach (var key in CurrentBase.Keys)
                    if (item.TitleID.ToUpper() == key.ToUpper()) UpdateBaseConsole(item.emuRev);
        }

        /// <summary>
        /// Changes injector settings based on selected base/console
        /// </summary>
        private void UpdateBaseConsole(int emuVer)
        {
            // ******************
            // CONSOLE-SPECIFIC
            // ******************
            // -------------------------------------------------------------------------------------
            switch (Console)
            {
                case Console.SNES:
                    break;

                case Console.N64:
                    CO.EmuType = i.isKorea ? 3 : emuVer;
                    break;

                case Console.SMS:
                case Console.SMDGEN:
                    // o4.EmuType = emuVer;
                    break;

                case Console.PCE:
                    break;

                case Console.NeoGeo:
                    break;

                case Console.MSX:
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
