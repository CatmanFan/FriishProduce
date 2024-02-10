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
        protected bool isJapanRegion = false;

        // -----------------------------------
        // Public variables
        // -----------------------------------
        protected Database                      Database    { get; set; }
        protected IDictionary<string, string>   CurrentBase { get; set; }
        protected WiiVCModifier                 i           { get; set; }
        protected Options_VCNES                 o1          { get; set; }
        protected Options_VCN64                 o3          { get; set; }

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
            Text = i.ChannelTitle ?? Untitled;

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
            i = new WiiVCModifier() { Console = Console, ROM = ROM };

            switch (Console)
            {
                case Console.NES:
                    Icon = Properties.Resources.nintendo_nes;
                    TIDCode = "F";
                    o1 = new Options_VCNES();
                    break;

                case Console.SNES:
                    button1.Enabled = false;
                    Icon = Properties.Resources.nintendo_super_nes;
                    TIDCode = "J";
                    break;

                case Console.N64:
                    Icon = Properties.Resources.nintendo_nintendo64;
                    TIDCode = "N";
                    o3 = new Options_VCN64();
                    break;

                case Console.SMS:
                    Icon = Properties.Resources.sega_master_system;
                    TIDCode = "L";
                    break;

                case Console.SMDGEN:
                    Icon = Properties.Resources.sega_genesis;
                    TIDCode = "M";
                    break;

                case Console.PCE:
                    button1.Enabled = false;
                    Icon = Properties.Resources.nec_turbografx_16;
                    TIDCode = "P"; // Q for CD games
                    break;

                case Console.NeoGeo:
                    Icon = Properties.Resources.snk_neo_geo_aes;
                    TIDCode = "E";
                    break;

                case Console.MSX:
                    button1.Enabled = false;
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
            UpdateBannerPreview();
            RefreshForm();

            i.BannerYear = (int)ReleaseYear.Value;
            i.BannerPlayers = (int)Players.Value;
            AddBases();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (i.ROM != null) LoadROM(i.ROM, Properties.Settings.Default.AutoLibRetro);
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

            ReadyToExport =    !string.IsNullOrEmpty(i.TitleID) && i.TitleID.Length == 4
                            && !string.IsNullOrEmpty(i.ChannelTitle)
                            && !string.IsNullOrEmpty(i.BannerTitle)
                            && !string.IsNullOrEmpty(i.SaveDataTitle[0])
                            && (i.tImg != null)
                            && i.ROM != null;
            Tag = "dirty";
            ExportCheck.Invoke(this, EventArgs.Empty);

            UpdateBannerPreview();
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

            CheckExport();
        }

        private void TextBox_Handle(object sender, KeyPressEventArgs e)
        {
            var currentSender = sender as TextBox;
            var currentIndex = currentSender.GetLineFromCharIndex(currentSender.SelectionStart);
            var lineMaxLength = Math.Round((double)currentSender.MaxLength / 2);

            if (!string.IsNullOrEmpty(currentSender.Text)
                && currentSender.Lines[currentIndex].Length >= lineMaxLength
                && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)8 && e.KeyChar != (char)Keys.Enter) { System.Media.SystemSounds.Beep.Play(); e.Handled = true; }
        }

        private void BannerPreview_Paint(object sender, PaintEventArgs e)
        {
            if (BannerPreview_Panel.Enabled)
            {
                if (sender == BannerPreview_BG)
                    using (LinearGradientBrush b = new LinearGradientBrush(new PointF(0, 0), new PointF(0, (sender as Control).Height),
                        BannerPreview_Panel.BackColor,
                        BannerPreview_Label.BackColor))
                    {
                        e.Graphics.FillRectangle(b, (sender as Control).ClientRectangle);
                    }

                else
                    using (LinearGradientBrush b = new LinearGradientBrush(new PointF((sender as Control).Width, 0), new PointF(0, 0),
                        BannerPreview_Panel.BackColor,
                        (sender as Control).BackColor))
                    {
                        e.Graphics.FillRectangle(b, (sender as Control).ClientRectangle);
                    }
            }
        }

        private void UpdateBannerPreview()
        {
            BannerPreview_Year.Tag    = Language.Current.TwoLetterISOLanguageName == "ja" || isJapanRegion ? "{0}年発売"
                                      : Language.Current.TwoLetterISOLanguageName == "ko" || i.isKorea ? "일본판 발매년도\r\n{0}년"
                                      : Language.Current.TwoLetterISOLanguageName == "nl" ? "Release: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "es" ? "Año: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "it" ? "Pubblicato: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "fr" ? "Publié en {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "de" ? "Erschienen: {0}"
                                      : "Released: {0}";

            BannerPreview_Players.Tag = Language.Current.TwoLetterISOLanguageName == "ja" || isJapanRegion ? "プレイ人数\r\n{0}人"
                                      : Language.Current.TwoLetterISOLanguageName == "ko" || i.isKorea ? "플레이 인원수\r\n{0}명"
                                      : Language.Current.TwoLetterISOLanguageName == "nl" ? "{0} speler(s)"
                                      : Language.Current.TwoLetterISOLanguageName == "es" ? "Jugadores: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "it" ? "Giocatori: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "fr" ? "Joueurs: {0}"
                                      : Language.Current.TwoLetterISOLanguageName == "de" ? "{0} Spieler"
                                      : "Players: {0}";

            BannerPreview_Label.Text = BannerTitle.Text;
            BannerPreview_Year.Text = string.Format(BannerPreview_Year.Tag.ToString(), ReleaseYear.Value.ToString());
            BannerPreview_Players.Text = string.Format(BannerPreview_Players.Tag.ToString(), $"{1}{(Players.Value <= 1 ? null : "-" + Players.Value)}");
            if (Language.Current.TwoLetterISOLanguageName == "ja" || isJapanRegion) BannerPreview_Players.Text = BannerPreview_Players.Text.Replace("-", "～");
        }

        #region Load Data Functions
        private void ExportBanner_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();

            var WADs = new Dictionary<string, Console>()
            {
                /* { "FCWP", Console.NES }, // SMB3
                { "FCWJ", Console.NES },
                { "FCWQ", Console.NES },
                { "JBDP", Console.SNES }, // DKC2
                { "JBDJ", Console.SNES },
                { "JBDT", Console.SNES },
                { "NAAP", Console.N64 }, // SM64
                { "NAAJ", Console.N64 },
                { "NABT", Console.N64 }, // MK64 */
            };

            foreach (var item in WADs)
                Banner.ExportBanner(item.Key, item.Value);

            System.Media.SystemSounds.Beep.Play();
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
                if (i.WAD != null) i.WAD.Dispose();
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
            try { i.WAD = WAD.Load(path); } catch { goto Failed; }

            for (int x = 0; x < Database.List.Length; x++)
                if (Database.List[x].TitleID.ToUpper() == i.WAD.UpperTitleID.ToUpper())
                {
                    WADPath = path;

                    CurrentBase.Clear();
                    CurrentBase.Add(Database.List[x].TitleID, Database.List[x].NativeName);
                    baseName.Text = Database.List[x].NativeName;
                    baseID.Text = Database.List[x].TitleID;
                    UpdateBaseGeneral(0);
                    return true;
                }

            i.WAD.Dispose();

            Failed:
            System.Media.SystemSounds.Beep.Play();
            MessageBox.Show(string.Format(Language.Get("Message005"), i.WAD.UpperTitleID), Parent.Text);
            return false;
        }

        private void InterpolationChanged(object sender, EventArgs e)
        {
            if (imageintpl.SelectedIndex != Properties.Settings.Default.ImageInterpolation) Tag = "dirty";
            if (i != null && i.tImg != null) LoadImage();
        }

        public void LoadImage()
        {
            if (i.tImg != null) LoadImage(i.tImg.Source);
            else CheckExport();
        }

        public void LoadImage(string path)
        {
            if (i.tImg != null) oldImgPath = newImgPath;
            newImgPath = path;

            if (i.tImg == null) i.tImg = new TitleImage(Console, path);
            else i.tImg.Create(Console, path);

            LoadImage(i.tImg.Source);
        }

        public bool LoadImage(Bitmap src)
        {
            try
            {
                Bitmap img = (Bitmap)src.Clone();

                groupBox5.Enabled = true;
                i.tImg.Interpolation = (InterpolationMode)imageintpl.SelectedIndex;

                // Additionally edit image before generating files, e.g. with modification of image palette/brightness, used only for images with exact resolution of original screen size
                // ********
                switch (Console)
                {
                    default:
                        break;

                    case Console.NES:
                        if (src.Width == 256 && (src.Height == 224 || src.Height == 240) && o1.Settings[1] == "1")
                        {
                            if (o1.ImgPaletteIndex == -1 || oldImgPath != newImgPath) o1.ImgPaletteIndex = o1.CheckPalette(img);
                                img = o1.SwapColors(img, o1.Palettes[o1.ImgPaletteIndex], o1.Palettes[int.Parse(o1.Settings[0])]);
                        }
                        break;

                    case Console.SMS:
                    case Console.SMDGEN:
                        break;
                }

                i.tImg.Generate(img);
                img.Dispose();

                if (i.tImg.VCPic != null) Image.Image = i.tImg.VCPic;
                if (i.tImg.Source != null) SaveIcon_Panel.BackgroundImage = i.tImg.SaveIcon();

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

        public void LoadROM(string ROM, bool UseLibRetro = true)
        {
            i.ROM = ROM;
            ROMLoaded = true;

            ROMPath.Text = Path.GetFileName(i.ROM);
            if (!UseLibRetro) SerialCode.Text = SoftwareName.Text = Language.Get("Unknown");

            Random.Visible =
            groupBox1.Enabled =
            groupBox3.Enabled =
            groupBox5.Enabled =
            groupBox6.Enabled =
            groupBox7.Enabled =
            groupBox4.Enabled =
            groupBox2.Enabled =
            groupBox8.Enabled = true;

            UpdateBaseForm();

            RandomTID();
            CheckExport();

            Parent.tabControl.Visible = true;

            if (i.ROM != null && UseLibRetro) LoadLibRetroData();

            BannerPreview_Panel.BackColor = BannerPreview_BG.BackColor;
            BannerPreview_Line1.Refresh();
            BannerPreview_Line2.Refresh();
            BannerPreview_BG.Refresh();
            foreach (Control item in BannerPreview_Panel.Controls)
                if (item != ExportBanner) item.Visible = true;
        }

        public void LoadLibRetroData()
        {
            try
            {
                var LibRetro = Parent.LibRetro;
                LibRetro = new LibRetroDB { SoftwarePath = i.ROM };

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
                SerialCode.Text = LibRetro.GetSerial() ?? Language.Get("Unknown");

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

        public bool CreateInject(string outputFile)
        {
            try
            {
                if (WADPath != null) i.WAD = WAD.Load(WADPath);
                else
                    for (int x = 0; x < Database.List.Length; x++)
                        if (Database.List[x].TitleID.ToUpper() == baseID.Text.ToUpper()) i.WAD = Database.Load(x);

                // Create injector and insert ROM & savedata
                // *******
                switch (Console)
                {
                    // NES
                    // *******
                    case Console.NES:

                        // Create injector and insert ROM

                        // Problem with WADs crashing
                        // [X] ROM (DING DING DING!!!!!!!!!!!!!!!!!!!)
                        // Kirby's Adventure (USA), only ++++++++ Ninja Gaiden, Kirby's Adventure Europe & Korea working as normal
                        // [] SaveData
                        // [] Palette
                        // [] Manual

                        var i_NES = new InjectorNES(i.WAD, i.ROM);
                        i_NES.ReplaceSaveData(i.SaveDataTitle, i.tImg);
                        i_NES.InsertPalette(int.Parse(o1.Settings[0]));
                        i_NES.ReplaceROM();
                        i_NES.ReplaceManual(null);
                        i.WAD = i_NES.Write();
                        break;

                    // SNES
                    // *******
                    case Console.SNES:

                        // Create injector and insert ROM
                        var i_SNES = new InjectorSNES(i.WAD, i.ROM);
                        i_SNES.ReplaceROM();
                        i_SNES.ReplaceSaveData(i.SaveDataTitle, i.tImg);
                        i_SNES.ReplaceManual(null);
                        i.WAD = i_SNES.Write();
                        break;

                    // N64
                    // *******
                    case Console.N64:

                        // Create injector and insert ROM

                        // Problem with WADs crashing
                        // [] ROM
                        // [X] Settings (DING DING DING!!!!!!!!!!!!!!!!!!!)
                        // [] SaveData
                        // [] Manual

                        // Likely Content1 is the cause of the problem ????

                        var i_N64 = new InjectorN64(i.WAD, i.ROM)
                        {
                            Settings = new bool[] { o3.Settings[0], o3.Settings[1], o3.Settings[2], o3.Settings[3] },
                            CompressionType = o3.EmuType == 3 ? (o3.Settings[3] ? 1 : 2) : 0,
                            Allocate = o3.Settings[3] && (o3.EmuType <= 1),
                        };
                        i_N64.ReplaceROM();
                        i_N64.ModifyEmulator();
                        i_N64.ReplaceSaveData(i.SaveDataTitle, i.tImg);
                        i_N64.ReplaceManual(null);
                        i.WAD = i_N64.Write();
                        break;

                    // SEGA
                    // *******
                    case Console.SMS:
                    case Console.SMDGEN:

                        // Create injector and insert ROM
                        var i_SEGA = new InjectorSEGA(i.WAD, i.ROM)
                        {
                            IsSMS = Console == Console.SMS
                        };
                        i_SEGA.ReplaceROM();
                        i_SEGA.ReplaceSaveData(i.SaveDataTitle, i.tImg);
                        i_SEGA.ReplaceManual(null);
                        i_SEGA.WriteCCF();
                        i.WAD = i_SEGA.Write();
                        break;

                    case Console.PCE:
                    case Console.NeoGeo:
                    case Console.MSX:
                    case Console.Flash:
                    default:
                        throw new NotImplementedException();
                }

                i.Modify();
                i.WAD.Save(outputFile);

                // Check new WAD file
                // *******
                if (File.Exists(outputFile) && File.ReadAllBytes(outputFile).Length > 10)
                {
                    Parent.CleanTemp();
                    System.Media.SystemSounds.Beep.Play();
                    Tag = null;

                    if (Properties.Settings.Default.AutoOpenFolder)
                        System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{outputFile}\"");
                    else
                        MessageBox.Show(string.Format(Language.Get("Message003"), outputFile), Language.Get("_AppTitle"), MessageBoxButtons.OK);
                    return true;
                }
                else throw new Exception(Language.Get("Error006"));
            }

            catch (Exception ex)
            {
                Parent.CleanTemp();
                i.ShowErrorMessage(ex);
                return false;
            }

            finally
            {
                if (WADPath == null) i.WAD.Dispose();
            }
        }
        #endregion

        #region **Console-Specific Functions**
        // ******************
        // CONSOLE-SPECIFIC
        // ******************
        private void OpenInjectorOptions(object sender, EventArgs e)
        {
            switch (Console)
            {
                case Console.NES:
                    if (o1.ShowDialog(this) == DialogResult.OK) { LoadImage(); }
                    break;
                case Console.SNES:
                    break;
                case Console.N64:
                    if (o3.ShowDialog(this) == DialogResult.OK) { CheckExport(); }
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
            // Changing SaveDataTitle max length & clearing text field when needed
            // ----------------------
            if (Console == Console.NES) SaveDataTitle.MaxLength = i.isKorea ? 30 : 40;
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
            isJapanRegion = CurrentBase.ElementAt(index).Key[3] == 'J';
            if (i.isKorea) SaveDataTitle.MaxLength /= 2; // Applies to both NES/FC & SNES/SFC

            // Also, some consoles only support a single line anyway
            // ********
            bool isSingleLine = i.isKorea
                             || Console == Console.SMS
                             || Console == Console.SMDGEN;

            // Set textbox to use single line when needed
            // ********
            if (SaveDataTitle.Multiline == isSingleLine)
            {
                SaveDataTitle.Multiline = !isSingleLine;
                SaveDataTitle.Clear();
                goto End;
            }

            // Clear text field if at least one line is longer than the maximum limit allowed
            // ********
            foreach (var line in SaveDataTitle.Lines)
                if (line.Length > Math.Round((double)SaveDataTitle.MaxLength / 2))
                    SaveDataTitle.Clear();

            End:
            foreach (var item in Database.List)
                foreach (var key in CurrentBase.Keys)
                    if (item.TitleID.ToUpper() == key.ToUpper()) UpdateBaseConsole(item.emuRev);
            UpdateBannerPreview();
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
                    o3.EmuType = i.isKorea ? 3 : emuVer;
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
