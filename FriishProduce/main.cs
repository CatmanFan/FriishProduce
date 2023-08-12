using libWiiSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Main : Form
    {
        readonly Lang x = Program.Language;
        bool SaveVisible = false;
        bool Retrieving = false;

        Platforms currentConsole = 0;
        TitleImage tImg = new TitleImage();
        bool ForwarderMode = false;
        Database db;

        readonly string key_path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "key.bin");

        string[] input = new string[]
        {
            /* Full path to ROM       */ null,
            /* Full path to ROM patch */ null,
            /* Full path to WAD file  */ null
        };
        string[] multiFileInput = new string[0];
        Dictionary<string, string> btns = new Dictionary<string, string>();

        Views.SEGA_Config ConfigForm_SEGA;

        public Main()
        {
            InitializeComponent();
            ChangeTheme();
            if (File.Exists(key_path)) File.Delete(key_path);

            string[] consoles = new string[]
            {
                x.Get("NES"),
                x.Get("SNES"),
                x.Get("N64"),
                x.Get("SMS"),
                x.Get("SMD"),
                x.Get("PCE"),
                x.Get("NeoGeo"),
                x.Get("MSX"),
                x.Get("Flash"),
                x.Get("GB"),
                x.Get("GBC"),
                x.Get("GBA"),
                x.Get("PSX")
            };
            foreach (var console in consoles) Console.Items.Add(console);

            x.Localize(this);

            var fvi = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string ver = $"beta {fvi.FileVersion}";

            Text = x.Get("g000");
            a000.Text = string.Format(x.Get("a000"), ver);
            ToolTip.SetToolTip(Settings, x.Get("g001"));
            ToolTip.SetToolTip(RandomTID, x.Get("a022"));
            NANDLoader.Items[0] = x.Get("g013");

            BrowseWAD.Filter = x.Get("f_wad") + x.Get("f_all");
            BrowseImage.Filter = x.Get("f_img") + x.Get("f_all");
            BrowsePatch.Filter = x.Get("f_bps") + x.Get("f_all");
            SaveWAD.Filter = BrowseWAD.Filter;

            Reset();
        }

        // ***************************************************************************************************************** //

        private void Console_Changed(object sender, EventArgs e)
        {
            if (Console.SelectedIndex >= 0)
            {
                a001.Text = x.Get("a001");
                OpenROM.Text = x.Get("g003");

                currentConsole = (Platforms)Console.SelectedIndex;
                db = new Database(currentConsole.ToString().ToLower());
                DisableEmanual.Visible = true;

                switch (currentConsole)
                {
                    case Platforms.NES:
                        BrowseROM.Filter = x.Get("f_nes");
                        break;

                    case Platforms.SNES:
                        BrowseROM.Filter = x.Get("f_sfc");
                        break;

                    case Platforms.N64:
                        BrowseROM.Filter = x.Get("f_n64");
                        break;

                    case Platforms.SMS:
                        BrowseROM.Filter = x.Get("f_sms");
                        break;

                    case Platforms.SMD:
                        BrowseROM.Filter = x.Get("f_smd").Replace("*.bin;*.gen", "*.bin;*.gen;*.md").Replace("*.gen, ", "*.gen, *.md, ");
                        break;

                    case Platforms.Flash:
                        a001.Text = x.Get("a001_swf");
                        OpenROM.Text = x.Get("g004") != "undefined" ? x.Get("g004") : x.Get("g003");
                        BrowseROM.Filter = x.Get("f_swf");
                        break;

                    case Platforms.PCE:
                        BrowseROM.Filter = x.Get("f_pce");
                        break;

                    case Platforms.NeoGeo:
                        BrowseROM.Filter = x.Get("f_zip");
                        break;

                    case Platforms.MSX:
                        BrowseROM.Filter = x.Get("f_msx");
                        break;

                    case Platforms.PSX:
                    case Platforms.SMCD:
                        BrowseROM.Filter = x.Get("f_iso");
                        break;

                    case Platforms.GB:
                    case Platforms.GBC:
                    case Platforms.GBA:
                        BrowseROM.Filter = x.Get("f_vba");
                        break;
                }

                BrowseROM.Filter += x.Get("f_all");

                GenerateTitleID();
                Reset();
                CheckForForwarder();
                RefreshBases();

                Image.Image = tImg.Generate(currentConsole);
                SaveDataTitle.Enabled = !(currentConsole == Platforms.SMS || currentConsole == Platforms.SMD || currentConsole == Platforms.PCE);
                SaveDataTitle.MaxLength = 80;

                Patch.Visible = true;
                var PatchBlacklist = new Platforms[]
                    {
                        Platforms.NeoGeo,
                        Platforms.Flash,
                        Platforms.SMCD,
                        Platforms.PSX
                    };
                foreach (var item in PatchBlacklist)
                    if (currentConsole == item) Patch.Visible = false;

                Next.Visible = true;
                Next.Enabled = true;
            }
            else Next.Visible = false;
        }

        private void CheckForForwarder()
        {
            ForwarderMode = false;
            if (InjectionMethod.SelectedIndex >= 0) ForwarderMode = InjectionMethod.SelectedItem.ToString() != x.Get("g012") && currentConsole != Platforms.Flash;

            // Switch relevant options based on mode & selected platform
            NANDLoader.Visible = ForwarderMode;
            vWii.Visible = ForwarderMode;
            a010.Visible = !ForwarderMode;
            DisableEmanual.Visible = !ForwarderMode;
            SaveDataTitle.Visible = !ForwarderMode;
            SaveDataTitle.MaxLength = 80;

            // Cosmetic
            InjectionMethod.Enabled = InjectionMethod.Items.Count > 1;

            AltCheckbox.Text = ForwarderMode ? x.Get("a019") : x.Get("a020");
            AltCheckbox.Checked = false;
            if (VideoMode.SelectedIndex == -1) VideoMode.SelectedIndex = 0;
            VideoMode.Visible = !ForwarderMode;
            VideoMode.Enabled = AltCheckbox.Checked;
            VideoMode.Location = new Point(AltCheckbox.Location.X + AltCheckbox.Width + 1, AltCheckbox.Location.Y - 3);

            foreach (var p in page4.Controls.OfType<Panel>())
                if (p.Name.StartsWith("Options_")) p.Visible = false;
            if (!ForwarderMode)
                switch (currentConsole)
                {
                    case Platforms.NES:
                        Options_NES.Visible = true;
                        SaveDataTitle.MaxLength = 20;
                        break;

                    case Platforms.N64:
                        Options_N64.Visible = true;
                        break;

                    case Platforms.SMS:
                    case Platforms.SMD:
                        Options_SEGA.Visible = true;
                        break;

                    case Platforms.PCE:
                        Options_PCE.Visible = true;
                        break;

                    case Platforms.NeoGeo:
                        Options_NeoGeo.Visible = true;
                        SaveDataTitle.MaxLength = 64;
                        break;

                    case Platforms.MSX:
                        SaveDataTitle.MaxLength = 64;
                        break;

                    case Platforms.Flash:
                        DisableEmanual.Visible = false;
                        Options_Flash.Visible = true;
                        break;
                }
        }

        bool SupportsAutoFill = true;

        /// <summary>
        /// Resets all input values and content option parameters
        /// </summary>
        private void Reset()
        {
            input = new string[input.Length];
            Patch.Checked = false;
            SaveDataTitle.Enabled = true;
            SupportsAutoFill = true;
            btns = new Dictionary<string, string>();

            // Consoles
            {
                NES_Palette.SelectedIndex = 0;
                N64_CompressionType.SelectedIndex = 0;

                SEGA_SetConfig.Checked = false;

                PCE_SetConfig.Checked = false;
                PCE_CustomOptions.Enabled = false;

                Flash_TotalSaveDataSize.SelectedIndex = 0;
                Flash_FPS.SelectedIndex = 0;
                Flash_StrapReminder.SelectedIndex = 0;

                NANDLoader.SelectedIndex = 0;
                a023.Visible = false;
            }

            BrowseROM.Multiselect = currentConsole == Platforms.Flash;
            InjectionMethod.Items.Clear();
            if (Console.SelectedIndex >= 0)
            {
                InjectionMethod.Items.Add(x.Get("g012"));
                switch (currentConsole)
                {
                    case Platforms.NES:
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[0]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[1]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[2]);
                        break;
                    case Platforms.SNES:
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[3]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[4]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[5]);
                        break;
                    case Platforms.N64:
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[8]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[9]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[10]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[11]);
                        break;
                    case Platforms.SMS:
                    case Platforms.SMD:
                    case Platforms.S32X:
                    case Platforms.SMCD:
                        if (currentConsole == Platforms.SMCD)
                        {
                            InjectionMethod.Items.RemoveAt(0);
                            SupportsAutoFill = false;
                            a023.Visible = true;
                        }
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[7]);
                        break;
                    case Platforms.PCE:
                        break;
                    case Platforms.NeoGeo:
                        SupportsAutoFill = false;
                        break;
                    case Platforms.C64:
                        break;
                    case Platforms.MSX:
                        break;
                    case Platforms.Flash:
                        InjectionMethod.Items[0] = x.Get("Flash");
                        SupportsAutoFill = false;
                        break;
                    case Platforms.GB:
                    case Platforms.GBC:
                    case Platforms.GBA:
                        InjectionMethod.Items.RemoveAt(0);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[6]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[14]);
                        // a023.Visible = true;
                        break;
                    case Platforms.PSX:
                        SupportsAutoFill = false;
                        InjectionMethod.Items.RemoveAt(0);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[12]);
                        InjectionMethod.Items.Add(new Injectors.Forwarders().List[13]);
                        a023.Visible = true;
                        break;
                    default:
                        break;
                }

                InjectionMethod.SelectedIndex = 0;
                AutoFill.Visible = SupportsAutoFill;
            }

            CheckForForwarder();
        }

        // ***************************************************************************************************************** //

        private void Main_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = Wait.Visible;

        private void ToggleWaitingIcon(bool show)
        {
            page1.Enabled = !show;
            page2.Enabled = !show;
            page3.Enabled = !show;
            page4.Enabled = !show;

            Wait.Visible = show;
            Settings.Enabled = !show;
            if (!page1.Visible) Back.Visible = !show;
            if (!show) { if (SaveVisible) Save.Visible = true; else Next.Visible = true; }
            else { Save.Visible = !show; Next.Visible = !show; }
        }

        /// <summary>
        /// Sets the theme to light or dark mode
        /// </summary>
        private void ChangeTheme()
        {
            if (Properties.Settings.Default.LightTheme)
            {
                BackColor = Themes.Light.BG;
                ForeColor = Themes.Light.FG;
                panel.BackColor = Themes.Light.BG_Secondary;
                Image.BackColor = Themes.Light.BG_Secondary;
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<ComboBox>())
                    {
                        item.FlatStyle = FlatStyle.System;
                        item.BackColor = Themes.Light.ComboBox;
                        item.ForeColor = Themes.Light.FG;
                    }
                    foreach (var item in panel.Controls.OfType<TextBox>())
                    {
                        item.BackColor = Themes.Light.BG_Secondary;
                        item.ForeColor = Themes.Light.FG;
                    }
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var cP in panel.Controls.OfType<Panel>())
                    {
                        foreach (var cb in cP.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                        foreach (var item in cP.Controls.OfType<TextBox>())
                        {
                            item.BackColor = Themes.Light.BG_Secondary;
                            item.ForeColor = Themes.Light.FG;
                        }
                        foreach (var item in cP.Controls.OfType<NumericUpDown>())
                        {
                            item.BackColor = Themes.Light.BG_Secondary;
                            item.ForeColor = Themes.Light.FG;
                        }
                        foreach (var item in cP.Controls.OfType<ComboBox>())
                        {
                            item.FlatStyle = FlatStyle.System;
                            item.BackColor = Themes.Light.ComboBox;
                            item.ForeColor = Themes.Light.FG;
                        }
                    }
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.BackColor = Themes.Light.Button;
                        button.FlatAppearance.BorderColor = Themes.Light.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Light.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
            else
            {
                BackColor = Themes.Dark.BG;
                ForeColor = Themes.Dark.FG;
                panel.BackColor = Themes.Dark.BG_Secondary;
                Image.BackColor = Themes.Dark.BG_Secondary;
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<ComboBox>())
                    {
                        item.FlatStyle = FlatStyle.System;
                        item.BackColor = Themes.Dark.ComboBox;
                        item.ForeColor = Themes.Dark.FG;
                    }
                    foreach (var item in panel.Controls.OfType<TextBox>())
                    {
                        item.BackColor = Themes.Dark.BG_Secondary;
                        item.ForeColor = Themes.Dark.FG;
                    }
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var cP in panel.Controls.OfType<Panel>())
                    {
                        foreach (var cb in cP.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                        foreach (var item in cP.Controls.OfType<TextBox>())
                        {
                            item.BackColor = Themes.Dark.BG_Secondary;
                            item.ForeColor = Themes.Dark.FG;
                        }
                        foreach (var item in cP.Controls.OfType<NumericUpDown>())
                        {
                            item.BackColor = Themes.Dark.BG_Secondary;
                            item.ForeColor = Themes.Dark.FG;
                        }
                        foreach (var item in cP.Controls.OfType<ComboBox>())
                        {
                            item.FlatStyle = FlatStyle.System;
                            item.BackColor = Themes.Dark.ComboBox;
                            item.ForeColor = Themes.Dark.FG;
                        }
                    }
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.BackColor = Themes.Dark.Button;
                        button.FlatAppearance.BorderColor = Themes.Dark.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Dark.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }

            Wait.BackColor = panel.BackColor;
        }

        private void ChangeTheme(Form f)
        {
            f.BackColor = BackColor;
            f.ForeColor = ForeColor;
            foreach (var item in f.Controls.OfType<Panel>())
            {
                if (item.Tag.ToString() == "panel") item.BackColor = panel.BackColor;
                else if (item.Tag.ToString() == "page") item.BackColor = Color.FromArgb((panel.BackColor.R + BackColor.R) / 2, (panel.BackColor.G + BackColor.G) / 2, (panel.BackColor.B + BackColor.B) / 2);
            }

            foreach (var item in f.Controls.OfType<ComboBox>())
            {
                item.FlatStyle = Console.FlatStyle;
                item.BackColor = Console.BackColor;
                item.ForeColor = Console.ForeColor;
            }
            foreach (var tC in f.Controls.OfType<TabControl>())
            {
                foreach (var tP in tC.Controls.OfType<TabPage>())
                {
                    foreach (var item in tP.Controls.OfType<ComboBox>())
                    {
                        item.FlatStyle = Console.FlatStyle;
                        item.BackColor = Console.BackColor;
                        item.ForeColor = Console.ForeColor;
                    }
                }
            }

            if (Properties.Settings.Default.LightTheme)
            {
                foreach (var panel in f.Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>())
                        if (cb.Tag != null && cb.Tag.ToString() != "section") cb.FlatStyle = FlatStyle.System;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>())
                            if (cb.Tag != null && cb.Tag.ToString() != "section") cb.FlatStyle = FlatStyle.System;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.BackColor = Themes.Light.Button;
                        button.FlatAppearance.BorderColor = Themes.Light.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Light.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
            else
            {
                foreach (var panel in f.Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>())
                        if (cb.Tag != null && cb.Tag.ToString() != "section") cb.FlatStyle = FlatStyle.Standard;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>())
                            if (cb.Tag != null && cb.Tag.ToString() != "section") cb.FlatStyle = FlatStyle.Standard;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.BackColor = Themes.Dark.Button;
                        button.FlatAppearance.BorderColor = Themes.Dark.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Dark.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
        }

        // ***************************************************************************************************************** //

        private void Settings_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings() { Text = x.Get("g001"), };
            ChangeTheme(SettingsForm);
            if (SettingsForm.ShowDialog(this) == DialogResult.OK) ChangeTheme();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (page1.Visible)
            {
                page2.Visible = true;
                page1.Visible = false;
                Back.Visible = true;
                Back.Enabled = true;
                Next.Enabled = (input[0] != null) && (input[2] != null);
                ROMPath.Text = input[0] != null ? Path.GetFileName(input[0]) : x.Get("a002");
            }
            else if (page2.Visible)
            {
                page3.Visible = true;
                page2.Visible = false;
                Next.Enabled = CheckBannerPage();
            }
            else if (page3.Visible)
            {
                page4.Visible = true;
                page3.Visible = false;
                Save.Visible = true;
                Save.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");
                Next.Visible = false;
            }
            SaveVisible = Save.Visible;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (page2.Visible)
            {
                page1.Visible = true;
                page2.Visible = false;
                Back.Visible = false;
                Back.Enabled = false;
                Next.Visible = Console.SelectedIndex >= 0;
                Next.Enabled = true;
            }
            else if (page3.Visible)
            {
                page2.Visible = true;
                page3.Visible = false;
                Next.Enabled = (input[0] != null) && (input[2] != null);
            }
            else if (page4.Visible)
            {
                page3.Visible = true;
                page4.Visible = false;
                Next.Visible = true;
                Next.Enabled = CheckBannerPage();
                Save.Visible = false;
            }
            SaveVisible = Save.Visible;
        }

        private bool CheckBannerPage()
        {
            if (Custom.Checked && page3.Visible && !ForwarderMode)
                return !(string.IsNullOrEmpty(ChannelTitle.Text)
                      || string.IsNullOrEmpty(BannerTitle.Text)
                      || string.IsNullOrEmpty(SaveDataTitle.Text));
            else if (Custom.Checked && page3.Visible && ForwarderMode)
                return !(string.IsNullOrEmpty(ChannelTitle.Text)
                      || string.IsNullOrEmpty(BannerTitle.Text));
            return true;
        }

        // ***************************************************************************************************************** //

        private void OpenROM_Click(object sender, EventArgs e)
        {
            input[0] = null;
            multiFileInput = new string[0];
            if (BrowseROM.ShowDialog() == DialogResult.OK)
            {
                input[0] = BrowseROM.FileName;
                multiFileInput = BrowseROM.FileNames;
            }

            // input[1] = null;
            // Patch.Checked = input[1] != null;
            // AutoFill.Enabled = input[0] != null;

            if (currentConsole == Platforms.Flash)
            {
                Next.Enabled = (multiFileInput[0] != null) && (input[2] != null);
                ROMPath.Text = multiFileInput[0] != null ? Path.GetFileName(multiFileInput[0]) : x.Get("a002");

                if (multiFileInput.Length > 1)
                    for (int i = 1; i < multiFileInput.Length; i++)
                        ROMPath.Text += ", " + Path.GetFileName(multiFileInput[i]);
            }
            else
            {
                Next.Enabled = (input[0] != null) && (input[2] != null);
                ROMPath.Text = input[0] != null ? Path.GetFileName(input[0]) : x.Get("a002");
            }

            if (SupportsAutoFill && Properties.Settings.Default.D_Custom_AutoRetrieveROMData)
                AutoFiller();
        }

        private void BaseList_Changed(object sender, EventArgs e)
        {
            if (Bases.SelectedIndex >= 0)
            {
                foreach (var item in Directory.GetFiles(Paths.Database, "*.wad", SearchOption.AllDirectories))
                    if (File.Exists(item) && item.Contains(db.SearchID(Bases.SelectedItem.ToString())))
                    {
                        input[2] = item;
                        goto End;
                    }

                MessageBox.Show("Unable to find WAD in database.");
                Bases.Items.Remove(Bases.SelectedItem);
                input[2] = null;
                RefreshBases();

            End:
                RefreshBases(true);
            }

            Next.Enabled = (input[0] != null) && (input[2] != null);
        }

        private void RefreshBases(bool BannerOnly = false)
        {
            if (!BannerOnly)
            {
                DeleteBase.Enabled = false;
                Bases.Enabled = false;
                Bases.Items.Clear();
                ImportBases.SelectedIndex = -1;
                Bases.DropDownHeight = 106;

                foreach (var entry in db.GetList())
                {
                    string selectedPlatform = db.Selected == "all" ? entry["platform"].ToString() : db.Selected;

                    if (File.Exists($"{db.CurrentFolder(selectedPlatform)}{entry["id"].ToString().ToUpper()}.wad"))
                    {
                        Bases.Items.Add(entry["title"].ToString());
                        Bases.Enabled = Bases.Items.Count >= 2;
                    }
                }

                Bases.SelectedIndex = Bases.Items.Count == 0 ? -1 : 0;
            }

            DeleteBase.Enabled = Bases.SelectedIndex >= 0;

            if (Bases.Items.Count > 0)
            {
                ImportBases.Items.Clear();
                foreach (var item in Bases.Items)
                    if (item != Bases.SelectedItem)
                    {
                        ImportBases.Items.Add(item);
                        ImportBases.SelectedIndex = 0;
                    }
            }
            if (ImportBases.Items.Count == 0)
            {
                Import.Checked = false;
                Import.Enabled = false;
                ImportBases.Enabled = false;
            }
            else
            {
                Import.Enabled = true;
                ImportBases.Enabled = Import.Checked;
                ImportBases.SelectedIndex = 0;
            }

            CheckBannerPage();

            // ---------------------- //
            //    PLATFORM-SPECIFIC   //
            // ---------------------- //
            if (currentConsole == Platforms.N64)
                foreach (var entry in db.GetList())
                    if (Bases.SelectedIndex != -1 && Bases.SelectedItem.ToString() == entry["title"].ToString())
                    {
                        N64_Allocation.Visible = entry["ver"].ToString().Contains("rev1");
                        N64_FixCrash.Visible = entry["ver"].ToString().Contains("rev1");
                        if (!entry["ver"].ToString().Contains("rev1")) N64_Allocation.Checked = false;
                        if (!entry["ver"].ToString().Contains("rev1")) N64_FixCrash.Checked = false;
                        break;
                    }
        }

        private void AddWAD(object sender, EventArgs e)
        {
            if (BrowseWAD.ShowDialog() == DialogResult.OK)
            {
                int listNum = Bases.Items.Count;
                bool set = false;

                try
                {
                    foreach (var filename in BrowseWAD.FileNames)
                    {
                        WAD w = WAD.Load(filename);
                        if (!w.HasBanner) goto Next;

                        foreach (var entry in db.GetList())
                        {
                            string selectedPlatform = db.Selected == "all" ? entry["platform"].ToString() : db.Selected;

                            if (entry["id"].ToString().ToUpper() == w.UpperTitleID)
                            {
                                string dest = $"{db.CurrentFolder(selectedPlatform)}{w.UpperTitleID}.wad";
                                if (!File.Exists(dest))
                                {
                                    if (!Directory.Exists(Path.GetDirectoryName(dest)))
                                        Directory.CreateDirectory(Path.GetDirectoryName(dest));
                                    File.Copy(filename, dest, true);
                                }
                                else if (File.ReadAllBytes(dest) != File.ReadAllBytes(filename))
                                {
                                    goto Next;
                                }
                                RefreshBases();
                                Bases.SelectedItem = entry["title"].ToString();
                                w.Dispose();

                                set = true;
                                goto Next;
                            }
                        }

                    Next:
                        w.Dispose();
                    }
                    goto End;
                }
                catch (Exception)
                {
                    goto End;
                }

            End:
                if (listNum == Bases.Items.Count || !set) System.Media.SystemSounds.Beep.Play();
                return;
            }
        }

        private void DeleteWAD(object sender, EventArgs e)
        {
            if (MessageBox.Show(x.Get("m000"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in Directory.GetFiles(Paths.Database, "*.wad", SearchOption.AllDirectories))
                    foreach (var entry in db.GetList())
                    {
                        if (File.Exists(item) && entry["id"].ToString().ToUpper() == Path.GetFileNameWithoutExtension(item) && Bases.SelectedItem.ToString().Contains(entry["title"].ToString()))
                        {
                            File.Delete(item);
                            input[2] = null;
                            RefreshBases();
                            Next.Enabled = (input[0] != null) && (input[2] != null);
                            return;
                        }
                    }
            }
        }

        // ***************************************************************************************************************** //

        private void NES_PaletteChanged(object sender, EventArgs e)
        {
            switch (NES_Palette.SelectedIndex)
            {
                default:
                case 0:
                    ToolTip.SetToolTip(NES_Palette, null);
                    break;
                case 1:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "SuperrSonic"));
                    break;
                case 2:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "Nintendo / SuperrSonic"));
                    break;
                case 3:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "Nintendo / FirebrandX"));
                    break;
                case 4:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "Nintendo / N-Mario"));
                    break;
                case 5:
                case 6:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "Nestopia"));
                    break;
                case 7:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "FCEUX"));
                    break;
                case 8:
                case 9:
                    ToolTip.SetToolTip(NES_Palette, string.Format(x.Get("g005"), "FirebrandX"));
                    break;
            }
        }

        private void Flash_ControllerChanged(object sender, EventArgs e)
        {
            if (Flash_Controller.Checked)
            {
                Views.Flash_Controller ControllerForm = new Views.Flash_Controller(btns) { Text = x.Get("g006"), };
                ChangeTheme(ControllerForm);

                if (ControllerForm.ShowDialog(this) == DialogResult.OK)
                    btns = ControllerForm.Config;
                else
                    Flash_Controller.Checked = false;
            }
        }

        private void SEGA_ConfigChanged(object sender, EventArgs e)
        {
            if (SEGA_SetConfig.Checked)
            {
                ConfigForm_SEGA = new Views.SEGA_Config(currentConsole == Platforms.SMS) { Text = x.Get("g011") };
                ChangeTheme(ConfigForm_SEGA);

                if (ConfigForm_SEGA.ShowDialog(this) != DialogResult.OK)
                    SEGA_SetConfig.Checked = false;
            }
            else
            {
                ConfigForm_SEGA.config = new List<string>();
                ConfigForm_SEGA.Dispose();
            }
        }

        private void NeoGeo_BIOS_CheckedChanged(object sender, EventArgs e)
        {
            if (NeoGeo_BIOS.Checked)
            {
                OpenFileDialog BrowseBIOS = new OpenFileDialog() { Filter = "NEO-GEO BIOS (*.rom)|*.rom|" + x.Get("f_zip") + x.Get("f_all") };

                if (BrowseBIOS.ShowDialog() == DialogResult.OK)
                    input[1] = BrowseBIOS.FileName;
                else
                {
                    NeoGeo_BIOS.Checked = false;
                    input[1] = "";
                }
            }
            else input[1] = "";
        }

        // ***************************************************************************************************************** //

        // ----------------------------------------------------------------------------------------
        // Set maximum value for each line in both text boxes
        // ----------------------------------------------------------------------------------------
        int maxBanner = 38;
        int maxSaveData = 40;

        private void BannerText_Changed(object sender, EventArgs e)
        {
            // ----------------------------------------------------------------------------------------
            // Remove excess lines
            // ----------------------------------------------------------------------------------------
            if (SaveDataTitle.Lines.Length > 2) SaveDataTitle.Lines = new string[2] { SaveDataTitle.Lines[0], SaveDataTitle.Lines[1] };
            if (BannerTitle.Lines.Length > 2) BannerTitle.Lines = new string[2] { BannerTitle.Lines[0], BannerTitle.Lines[1] };

            // ----------------------------------------------------------------------------------------
            // If save data editing uses only one line, set automatically to channel title text
            // ----------------------------------------------------------------------------------------
            if (!SaveDataTitle.Enabled && !Retrieving) SaveDataTitle.Text = ChannelTitle.Text;

            // ----------------------------------------------------------------------------------------
            // Split a single line if it's longer than maximum character value
            // ----------------------------------------------------------------------------------------
            if (BannerTitle.Text.Length > maxBanner && BannerTitle.Lines.Length == 1)
                BannerTitle.Text = BannerTitle.Text.Substring(0, maxBanner) + Environment.NewLine + BannerTitle.Text.Substring(maxBanner);
            if (SaveDataTitle.Text.Length > maxSaveData && SaveDataTitle.Lines.Length == 1)
                SaveDataTitle.Text = SaveDataTitle.Text.Substring(0, maxSaveData) + Environment.NewLine + SaveDataTitle.Text.Substring(maxSaveData);

            // ----------------------------------------------------------------------------------------
            // Remove excess characters
            // ----------------------------------------------------------------------------------------
            string[] bannerLines = BannerTitle.Lines;
            string[] saveDataLines = SaveDataTitle.Lines;

            for (int i = 0; i < BannerTitle.Lines.Length; i++)
                if (bannerLines[i].Length >= maxBanner) bannerLines[i] = bannerLines[i].Substring(0, maxBanner);
            for (int i = 0; i < SaveDataTitle.Lines.Length; i++)
                if (saveDataLines[i].Length >= maxSaveData) saveDataLines[i] = saveDataLines[i].Substring(0, maxSaveData);

            BannerTitle.Lines = bannerLines;
            SaveDataTitle.Lines = saveDataLines;

            Next.Enabled = CheckBannerPage();
        }

        private void BannerText_KeyPress(object sender, KeyPressEventArgs e)
        {
            var item = sender as TextBox;

            if (item.Multiline && !string.IsNullOrEmpty(item.Text))
            {
                int max = item.Name.Contains("Banner") ? maxBanner : maxSaveData;

                if (item.Lines[item.GetLineFromCharIndex(item.SelectionStart)].Length >= max)
                    e.Handled = !(e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Back);

                if (e.KeyChar == (char)Keys.Enter && item.Lines.Length >= 2)
                    e.Handled = true;
            }
        }

        private void RandomTID_Click(object sender, EventArgs e)
        {
            GenerateTitleID();
        }

        private void GenerateTitleID()
        {
            var r = new Random();
            string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            TitleID.Text = new string(Enumerable.Repeat(allowed, 4).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        private void TitleID_Changed(object sender, EventArgs e) => Save.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");

        /// <summary>
        /// Placeholder function for autosetting interpolation mode/resizing values if these aren't already defined
        /// </summary>
        private void ResetImage()
        {
            tImg = new TitleImage(currentConsole)
            {
                ResizeMode = ImgResize.SelectedIndex < 0 ?
                             (currentConsole == Platforms.Flash
                             || currentConsole == Platforms.GB
                             || currentConsole == Platforms.GBC
                             || currentConsole == Platforms.GBA
                             ? TitleImage.Resize.Fit : TitleImage.Resize.Stretch)
                             : (TitleImage.Resize)ImgResize.SelectedIndex,
                InterpolationMode = ImgInterp.SelectedIndex < 0 ?
                                    (System.Drawing.Drawing2D.InterpolationMode)Properties.Settings.Default.D_Custom_InterpolationMode :
                                    (System.Drawing.Drawing2D.InterpolationMode)ImgInterp.SelectedIndex
            };
            if (ImgResize.SelectedIndex < 0) ImgResize.SelectedIndex = (int)tImg.ResizeMode;
            if (ImgInterp.SelectedIndex < 0) ImgInterp.SelectedIndex = (int)tImg.InterpolationMode;
        }

        private void Image_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (BrowseImage.ShowDialog() == DialogResult.OK)
                {
                    ResetImage();
                    tImg.Path = BrowseImage.FileName;

                    try
                    {
                        Image.Image = tImg.Generate(currentConsole);
                    }
                    catch
                    {
                        goto Clear;
                    }
                    finally
                    {
                        Next.Enabled = CheckBannerPage();
                    }

                    return;
                }
                else goto Clear;
            }
            else goto Clear;

            Clear:
            Image.Image = null;
            tImg = new TitleImage(currentConsole);
            Next.Enabled = CheckBannerPage();
        }

        private void Image_StretchChanged(object sender, EventArgs e)
        {
            tImg.ResizeMode = (TitleImage.Resize)ImgResize.SelectedIndex;
            Image.Image = tImg.Generate(currentConsole);
            Next.Enabled = CheckBannerPage();
        }

        private void Image_ModeIChanged(object sender, EventArgs e)
        {
            tImg.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)ImgInterp.SelectedIndex;
            Image.Image = tImg.Generate(currentConsole);
            Next.Enabled = CheckBannerPage();
        }

        private void Patch_CheckedChanged(object sender, EventArgs e)
        {
            if (Patch.Checked)
            {
                if (BrowsePatch.ShowDialog() == DialogResult.OK)
                    input[1] = BrowsePatch.FileName;
                else
                {
                    input[1] = null;
                    Patch.Checked = false;
                }
            }
            else input[1] = null;
        }

        /// <summary>
        /// Function to store all actions in which toggling a checkbox also toggles other control(s)
        /// </summary>
        private void CheckedToggles(object sender, EventArgs e)
        {
            var s = sender as CheckBox;
            switch (s.Name)
            {
                case "Custom":
                    Banner.Enabled = s.Checked;
                    AutoFill.Enabled = s.Checked;
                    Next.Enabled = CheckBannerPage();
                    break;
                case "Import":
                    ImportBases.Enabled = s.Checked;
                    break;
                case "Flash_UseSaveData":
                    Flash_TotalSaveDataSize.Enabled = s.Checked;
                    break;
                case "Flash_CustomFPS":
                    Flash_FPS.Enabled = s.Checked;
                    break;
                case "AltCheckbox":
                    if (!ForwarderMode) VideoMode.Enabled = s.Checked;
                    break;
                case "PCE_SetConfig":
                    PCE_CustomOptions.Enabled = s.Checked;
                    break;
                case "BIOS__001":
                    break;
            }
        }

        private void InjectionMethod_Changed(object sender, EventArgs e) => CheckForForwarder();

        // ***************************************************************************************************************** //

        private async void AutoFiller()
        {
            Retrieving = true;
            ToggleWaitingIcon(true);
            bool Retrieved = false;

            await Task.Run(() =>
            {
                DBEntry d = new DBEntry();
                try
                {
                    d.Get(input[0], currentConsole);
                }
                catch (Exception ex)
                {
                    Retrieving = false;
                    Invoke((Action)delegate { ToggleWaitingIcon(false); });
                    MessageBox.Show(ex.Message, Program.Language.Get("error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (d.GetPlayers() != null) Invoke((Action)delegate { Players.Value = int.Parse(d.GetPlayers()); Retrieved = true; });
                if (d.GetYear() != null) Invoke((Action)delegate { ReleaseYear.Value = int.Parse(d.GetYear()); Retrieved = true; });
                if (d.GetTitle() != null)
                {

                    string title = Regex.Replace(d.GetTitle().Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "");
                    if (title.Contains(", The")) title = "The " + title.Replace(", The", string.Empty);

                    Invoke((Action)delegate { BannerTitle.Text = title.Trim(); });
                    Invoke((Action)delegate { SaveDataTitle.Text = title.Trim(); });
                    Invoke((Action)delegate { ChannelTitle.Text = /* title.Trim().Length <= ChannelTitle.MaxLength ? title.Trim() : */ string.Empty; });
                    Retrieved = true;
                }
                if (d.GetImgURL() != null)
                {
                    if (tImg.FromURL(d.GetImgURL()))
                    {
                        try
                        {
                            Invoke((Action)delegate { ResetImage(); });
                            tImg.FromURL(d.GetImgURL());
                            Invoke((Action)delegate { Image.Image = tImg.Generate(currentConsole); });
                            Retrieved = true;
                        }
                        catch
                        {
                            Invoke((Action)delegate { Image.Image = null; });
                            tImg = new TitleImage(currentConsole);
                        }
                    }
                }
            });

            Retrieving = false;
            ToggleWaitingIcon(false);
            if (page3.Visible) Next.Enabled = CheckBannerPage();
            if (Retrieved && !Custom.Checked) Custom.Checked = true;

            if (page2.Visible && Properties.Settings.Default.D_Custom_AutoRetrieveROMData)
            {
                if (Retrieved) MessageBox.Show(x.Get("m022"));
                else System.Media.SystemSounds.Beep.Play();
            }
        }

        private void AutoFill_Click(object sender, EventArgs e) => AutoFiller();

        private void Finish_Click(object sender, EventArgs e)
        {
            string fileName = Properties.Settings.Default.WadName;

            string type = ForwarderMode ? "fwdr"
                : !ForwarderMode && currentConsole != Platforms.Flash ? "VC"
                : "injected";
            if (vWii.Checked) type += "+vWii";

            fileName = fileName.Replace("{name}", string.IsNullOrWhiteSpace(ChannelTitle.Text) ? "(null)" : ChannelTitle.Text);
            fileName = fileName.Replace("{titleID}", TitleID.Text);
            fileName = fileName.Replace("{platform}", currentConsole.ToString());
            fileName = fileName.Replace("{type}", type);

            SaveWAD.FileName = fileName.Replace(": ", " - ").Replace("?", "");
            if (SaveWAD.ShowDialog() == DialogResult.OK)
            {
                ToggleWaitingIcon(true);
                ExportWAD();
            }
        }

        // ***************************************************************************************************************** //

        // ToDo:
        // await ReplaceBanner(string title, string short_title, ...)?

        private async void ExportWAD()
        {
            try
            {
                WAD w = new WAD();
                bool usePatch = Patch.Checked;

                await Task.Run(() =>
                {
                    var RunningP_List = Process.GetProcessesByName("texreplace");
                    if (RunningP_List != null || RunningP_List.Length > 0)
                        foreach (var RunningP in RunningP_List)
                            RunningP.Kill();

                    if (usePatch) input[0] = Global.ApplyPatch(input[0], input[1]);

                    if (currentConsole == Platforms.Flash)
                    {
                        w.LoadFile(input[2]);
                        w.Unpack(Paths.WorkingFolder);
                    }
                    else WiiCS.UnpackWAD(input[2], Paths.WorkingFolder);
                });

                // ----------------------------------------------------
                if (AltCheckbox.Checked && !ForwarderMode)
                {
                    int mode = VideoMode.SelectedIndex;
                    await Task.Run(() => { Global.ChangeVideoMode(mode); });
                }
                // ----------------------------------------------------

                #region Banner
                bool IsCustomConsole = (int)currentConsole > (int)Platforms.Flash;
                if (Custom.Checked || IsCustomConsole)
                {
                    // ----------------------------------------
                    // Import respective banners
                    // ----------------------------------------
                    if (Custom.Checked && Import.Checked)
                    {
                        foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                            if (File.Exists(item) && item.Contains(db.SearchID(ImportBases.SelectedItem.ToString())))
                                WAD.Load(item).BannerApp.Save(Paths.WorkingFolder + "00000000.app");
                    }

                    // --------------------------------------------------------------------------- //

                    Banner b = new Banner()
                    {
                        UsingImage = tImg.Get(),
                        Custom = currentConsole == Platforms.PSX ? 1 : 0
                    };

                    // ----------------------------------------
                    // Load banner
                    // ----------------------------------------
                    libWiiSharp.U8 BannerApp = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000000.app");

                    // ----------------------------------------
                    // Unpack banner & icon .bin files
                    // ----------------------------------------
                    libWiiSharp.U8 Banner = new libWiiSharp.U8();
                    libWiiSharp.U8 Icon = new libWiiSharp.U8();

                    try
                    {
                        Banner.LoadFile(BannerApp.Data[BannerApp.GetNodeIndex("banner.bin")]);
                        Icon.LoadFile(BannerApp.Data[BannerApp.GetNodeIndex("icon.bin")]);

                        b.ExtractFiles(Banner, Icon);
                    }
                    catch
                    {
                        throw new Exception(x.Get("m008"));
                    }

                    // --------------------------------------------------------------------------- //

                    b.SetupVCBrlyt();

                    if (Custom.Checked)
                    {
                        string arg = $"-Title \"{BannerTitle.Text.Replace('-', '–').Replace(Environment.NewLine, "^").Replace("\"", "''")}\" -YEAR {ReleaseYear.Value} -Play {Players.Value}";
                        await Task.Run(() => { b.RunVCBrlyt(arg); });
                    }

                    if (IsCustomConsole)
                    {
                        await Task.Run(() => { b.CustomizeConsole(Banner, Icon,
                            currentConsole > Platforms.Flash ? (BannerTypes)currentConsole :
                            BannerTypes.Normal, w.Region); });
                    }

                    b.ReplaceBrlyt(Banner);

                    // --------------------------------------------------------------------------- //

                    if (b.UsingImage)
                    {
                        b.ReplaceVCPic(Banner, Icon, Paths.Images + "VCPic.tpl", Paths.Images + "IconVCPic.tpl", tImg);
                        Directory.Delete(Paths.Images, true);
                    }

                    // --------------------------------------------------------------------------- //

                    var origTitles = w.ChannelTitles;
                    b.PackBanner(Banner, Icon, BannerApp, Custom.Checked ? new string[]
                    {
                        ChannelTitle.Text, // JPN
                        ChannelTitle.Text, // ENG
                        ChannelTitle.Text, // DEU
                        ChannelTitle.Text, // FRA
                        ChannelTitle.Text, // ESP
                        ChannelTitle.Text, // ITA
                        ChannelTitle.Text, // NED
                        ChannelTitle.Text  // KOR
                    } : origTitles.Length > 0 ? origTitles : new string[]
                    {
                        "undefined",
                        "undefined",
                        "undefined",
                        "undefined",
                        "undefined",
                        "undefined",
                        "undefined",
                        "undefined"
                    });

                    // ----------------------------------------
                    // Cleanup everything else
                    // ----------------------------------------
                    BannerApp.Dispose();
                    Banner.Dispose();
                    Icon.Dispose();
                }
                #endregion

                // ----------------------------------------------------
                // Virtual Console injection
                // ----------------------------------------------------
                if (currentConsole != Platforms.Flash && !ForwarderMode)
                {
                    // Extract content5.app & relevant CCFs where needed
                    if (currentConsole != Platforms.NeoGeo) await Task.Run(() => { WiiCS.UnpackU8(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5); });
                    if (currentConsole == Platforms.SMS || currentConsole == Platforms.SMD) new Injectors.SEGA().GetCCF(Custom.Checked);

                    if (DisableEmanual.Checked) await Task.Run(() => { Global.RemoveEmanual(); });
                    if (Custom.Checked && tImg.Get()) await Task.Run(() => { tImg.CreateSave(currentConsole); });

                    switch (currentConsole)
                    {
                        default:
                            throw new Exception("Not implemented yet!");

                        // ----------------------------------------------------

                        case Platforms.NES:
                            {
                                Injectors.NES NES = new Injectors.NES
                                {
                                    ROM = input[0],
                                    content1_file = Global.DetermineContent1(),
                                    saveTPL_offsets = new Injectors.NES().DetermineSaveTPLOffsets(Global.DetermineContent1())
                                };

                                NES.InsertROM();
                                NES.InsertPalette(NES_Palette.SelectedIndex);
                                if (Custom.Checked)
                                {
                                    if (tImg.Get() && NES.ExtractSaveTPL(Paths.WorkingFolder + "out.tpl"))
                                        tImg.CreateSave(Platforms.NES);

                                    string saveTitle = SaveDataTitle.Text;
                                    await Task.Run(() => { NES.InsertSaveData(saveTitle, Paths.WorkingFolder + "out.tpl"); });
                                }

                                await Task.Run(() => { Global.PrepareContent1(NES.content1_file); });
                                break;
                            }

                        // ----------------------------------------------------

                        case Platforms.SNES:
                            {
                                Injectors.SNES SNES = new Injectors.SNES()
                                {
                                    ROM = input[0],
                                    ROMcode = new Injectors.SNES().ProduceID(db.SearchID(Bases.SelectedItem.ToString()))
                                };

                                await Task.Run(() => { SNES.ReplaceROM(); });

                                if (Custom.Checked) SNES.InsertSaveTitle(SaveDataTitle.Lines);
                                break;
                            }

                        // ----------------------------------------------------

                        case Platforms.N64:
                            {
                                Injectors.N64 N64 = new Injectors.N64() { ROM = input[0] };
                                foreach (var entry in db.GetList())
                                {
                                    if (entry["title"].ToString() == Bases.SelectedItem.ToString())
                                        foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                            if (item.Contains(entry["id"].ToString().ToUpper()))
                                                N64.emuVersion = entry["ver"].ToString();
                                }

                                int compressionType = N64_CompressionType.SelectedIndex;
                                await Task.Run(() => { N64.ByteswapROM(); });
                                await Task.Run(() => { N64.CompressROM(compressionType); });
                                await Task.Run(() => { N64.ReplaceROM(N64.CheckForROMC()); });
                                if (N64_RemoveT64.Checked) N64.CleanT64();

                                bool needsEmuEdit = false;
                                foreach (var item in Options_N64.Controls.OfType<CheckBox>())
                                    if (item.Checked) needsEmuEdit = true;
                                if (needsEmuEdit)
                                {
                                    N64.LoadContent1();

                                    if (N64_FixBrightness.Checked)
                                        N64.Op_FixBrightness();
                                    if (N64_UseExpansionPak.Checked)
                                        N64.Op_ExpansionRAM();
                                    if (N64_Allocation.Checked && N64_Allocation.Visible)
                                        N64.Op_AllocateROM();
                                    if (N64_FixCrash.Checked && N64_FixCrash.Visible)
                                        N64.Op_FixCrashes();

                                    N64.SaveContent1();
                                }

                                if (Custom.Checked) N64.InsertSaveComments(SaveDataTitle.Lines);
                                break;
                            }

                        // ----------------------------------------------------

                        case Platforms.SMS:
                        case Platforms.SMD:
                            {
                                Injectors.SEGA SEGA = new Injectors.SEGA() { ROM = input[0], SMS = currentConsole == Platforms.SMS };
                                foreach (var entry in db.GetList())
                                {
                                    string title = Bases.SelectedItem.ToString();

                                    await Task.Run(() =>
                                    {
                                        if (entry["title"].ToString() == title)
                                            foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                                if (item.Contains(entry["id"].ToString().ToUpper()))
                                                {
                                                    SEGA.ver = SEGA.SMS ? entry["ver"].ToString() : entry["ord"].ToString();
                                                    SEGA.origROM = entry["ROM"].ToString();
                                                }
                                    });
                                }

                                await Task.Run(() => { SEGA.ReplaceROM(); });

                                // Config parameters
                                if (SEGA_SetConfig.Checked)
                                {
                                    var new_config = new string[ConfigForm_SEGA.config.Count];
                                    ConfigForm_SEGA.config.CopyTo(new_config);
                                    SEGA.ReplaceConfig(new_config);
                                }

                                string shortTitle = ChannelTitle.Text;
                                if (Custom.Checked) await Task.Run(() => { SEGA.InsertSaveTitle(shortTitle); });

                                SEGA.PackCCF(Custom.Checked);
                                break;
                            }

                        // ----------------------------------------------------

                        case Platforms.PCE:
                            {
                                Injectors.PCE PCE = new Injectors.PCE { ROM = input[0] };
                                foreach (var entry in db.GetList())
                                {
                                    if (entry["title"].ToString() == Bases.SelectedItem.ToString())
                                        foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                            if (item.Contains(entry["id"].ToString().ToUpper()))
                                                PCE.WAD_ID = entry["id"].ToString();
                                }

                                PCE.ReplaceROM();
                                if (PCE_SetConfig.Checked) PCE.SetConfig
                                    (PCE_Multitap.Checked,
                                     PCE_Pad5.Checked,
                                     PCE_BackupRAM.Checked,
                                     PCE_HideOverscan.Checked,
                                     PCE_Raster.Checked,
                                     PCE_NoFPA.Checked);

                                if (Custom.Checked) PCE.InsertSaveTitle(ChannelTitle.Text);
                                break;
                            }

                        // ----------------------------------------------------

                        case Platforms.NeoGeo:
                            {
                                Injectors.NeoGeo NeoGeo = new Injectors.NeoGeo
                                {
                                    ZIP = input[0],
                                    BIOSPath = string.IsNullOrWhiteSpace(input[1]) ? "" : input[1],
                                    Target = Injectors.NeoGeo.CheckTarget()
                                };

                                await Task.Run(() => { U8.Unpack(NeoGeo.Target, Paths.WorkingFolder_Contents); });

                                NeoGeo.InsertROM(File.Exists(Paths.WorkingFolder_Contents + "game.bin.z"));

                                if (Custom.Checked)
                                {
                                    bool SepBanner = !File.Exists(Paths.WorkingFolder_Contents + "banner.bin");
                                    if (SepBanner) await Task.Run(() => { WiiCS.UnpackU8(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5); });

                                    string target = NeoGeo.GetSaveFile();
                                    if (target != null)
                                    {
                                        if (tImg.Get() && NeoGeo.ExtractSaveTPL(target, Paths.WorkingFolder + "out.tpl")) tImg.CreateSave(Platforms.NeoGeo);
                                        NeoGeo.InsertSaveTitle(target, SaveDataTitle.Text, Paths.WorkingFolder + "out.tpl");
                                    }

                                    if (SepBanner && Directory.Exists(Paths.WorkingFolder_Content5)) await Task.Run(() => { WiiCS.PackU8(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app"); });
                                }
                                if (DisableEmanual.Checked) await Task.Run(() => { Global.RemoveEmanual(); });

                                await Task.Run(() => { U8.Pack(Paths.WorkingFolder_Contents, NeoGeo.Target); });
                                break;
                            }

                        // ----------------------------------------------------

                        case Platforms.MSX:
                            {
                                Injectors.MSX MSX = new Injectors.MSX { ROM = input[0] };

                                MSX.ReplaceROM();
                                if (DisableEmanual.Checked) await Task.Run(() => { Global.RemoveEmanual(); });

                                if (Custom.Checked)
                                {
                                    string target = MSX.GetSaveFile();
                                    if (target != null)
                                    {
                                        if (tImg.Get() && MSX.ExtractSaveTPL(target, Paths.WorkingFolder + "out.tpl"))
                                            tImg.CreateSave(Platforms.MSX);

                                        string saveTitle = SaveDataTitle.Text;
                                        await Task.Run(() => { MSX.InsertSaveTitle(target, saveTitle, Paths.WorkingFolder + "out.tpl"); });
                                    }
                                }
                                break;
                            }
                    }

                    if (currentConsole != Platforms.NeoGeo) await Task.Run(() => { WiiCS.PackU8(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app"); });
                }

                // ----------------------------------------------------
                // Adobe Flash
                // ----------------------------------------------------
                else if (currentConsole == Platforms.Flash)
                {
                    await Task.Run(() => { U8.Unpack(Paths.WorkingFolder + "00000002.app", Paths.WorkingFolder_Content2); });

                    Injectors.Flash Flash = new Injectors.Flash() { SWF = multiFileInput };
                    Flash.ReplaceSWF();

                    Flash.HomeMenuNoSave(Flash_HBMNoSave.Checked);
                    Flash.SetStrapReminder(Flash_StrapReminder.SelectedIndex);
                    if (Flash_UseSaveData.Checked) Flash.EnableSaveData(Convert.ToInt32(Flash_TotalSaveDataSize.SelectedItem.ToString()));
                    if (Flash_CustomFPS.Checked) Flash.SetFPS(Flash_FPS.SelectedItem.ToString());
                    if (Flash_Controller.Checked) Flash.SetController(btns);
                    if (Custom.Checked && tImg.Get()) tImg.CreateSave(Platforms.Flash);
                    if (Custom.Checked) Flash.InsertSaveData(SaveDataTitle.Lines);

                    await Task.Run(() => { U8.Pack(Paths.WorkingFolder_Content2, Paths.WorkingFolder + "00000002.app"); });
                }

                // ----------------------------------------------------
                // Forwarder creator
                // ----------------------------------------------------
                else if (currentConsole != Platforms.Flash && ForwarderMode)
                {
                    var extension = Path.GetExtension(input[0]).ToLower();
                    Injectors.Forwarders f = new Injectors.Forwarders
                    {
                        ROM = input[0],
                        IsDisc = extension == ".cue" || extension == ".iso" || extension == ".chd",
                        UseUSBStorage = AltCheckbox.Checked
                    };

                    string[] parameters = { TitleID.Text.ToUpper(), InjectionMethod.SelectedItem.ToString(), SaveWAD.FileName };
                    f.SetDOLIndex(parameters[1]);

                    // Generate ZIP name
                    string zipName = Properties.Settings.Default.ZipName;
                    string WADtype = ForwarderMode ? "fwdr"
                        : !ForwarderMode && currentConsole != Platforms.Flash ? "VC"
                        : "injected";
                    if (vWii.Checked) WADtype += "+vWii";
                    zipName = zipName.Replace("{name}", string.IsNullOrWhiteSpace(ChannelTitle.Text) ? "(null)" : ChannelTitle.Text);
                    zipName = zipName.Replace("{titleID}", TitleID.Text);
                    zipName = zipName.Replace("{platform}", currentConsole.ToString());
                    zipName = zipName.Replace("{type}", WADtype).Replace("{storage}", f.UseUSBStorage ? "USB" : "SD");
                    zipName = Path.Combine(Path.GetDirectoryName(SaveWAD.FileName), zipName + ".zip");

                    bool usesBIOS = f.IsDisc || a023.Checked;
                    bool bootBIOS = a023.Checked;
                    await Task.Run(() => { f.Generate
                    (
                        parameters[0], zipName,
                        usesBIOS, bootBIOS
                    ); });

                    // Default for auto-selection: Waninkoko NANDloader + v14
                    int type = NANDLoader.SelectedIndex <= 0 ? 2 : NANDLoader.SelectedIndex - 1;

                    // AUTO-SELECTION METHOD:
                    //   => For GenPlus & all emulators based on Wii64 Team's code (e.g. Wii64, WiiSX and forks), use Comex NANDloader
                    //   => For WiiMednafen, use Waninkoko NANDloader
                    if (f.DolIndex >= 8 && f.DolIndex <= 13) type = 1;
                    if (f.DolIndex == 14) type = 3;

                    f.ConvertWAD(type, TitleID.Text.ToUpper(), vWii.Checked);
                }

                // ----------------------------------------------------
                // Create WAD
                // ----------------------------------------------------

                if (currentConsole != Platforms.Flash && !ForwarderMode)
                {
                    Global.ChangeTitleID(TitleID.Text.ToUpper(), LowerTitleID.Channel);

                    // Generate Wii Common Key
                    var key = Path.GetFileNameWithoutExtension(input[2]).ToUpper().EndsWith("T") || Path.GetFileNameWithoutExtension(input[2]).ToUpper().EndsWith("Q") ?
                        CommonKey.GetKoreanKey() : CommonKey.GetStandardKey();
                    File.WriteAllBytes(key_path, key);

                    // Pack WAD
                    string Out = SaveWAD.FileName;
                    if (File.Exists(Out)) File.Delete(Out);
                    await Task.Run(() => { Wii.WadPack.PackWad(Paths.WorkingFolder, Out); });

                    // Edit
                    byte[] Out_B = File.ReadAllBytes(Out);
                        if (ForwarderMode) Wii.WadEdit.ChangeIosSlot(Out_B, 58);
                        if (RegionFree.Checked) Wii.WadEdit.ChangeRegion(Out_B, 3);
                    File.WriteAllBytes(Out, Out_B);

                    // Trucha Sign
                    Wii.WadEdit.TruchaSign(Out, 0);
                    Wii.WadEdit.TruchaSign(Out, 1);

                    // Delete Wii Common Key
                    File.Delete(key_path);
                }
                else
                {
                    w.LoadFile(input[2]);
                    libWiiSharp.Region r = RegionFree.Checked ? libWiiSharp.Region.Free : w.Region;
                    w.CreateNew(Paths.WorkingFolder);
                    w.FakeSign = true;
                    w.Region = r;
                    w.ChangeTitleID(LowerTitleID.Channel, TitleID.Text);

                    string Out = SaveWAD.FileName;
                    await Task.Run(() => { w.Save(Out); });
                }

                // ----------------------------------------------------
                // Ending operations
                // ----------------------------------------------------
                ToggleWaitingIcon(false);
                Focus();
                System.Media.SystemSounds.Beep.Play();

                // ----------------------------------------------------
                // Open Explorer if needed
                // ----------------------------------------------------
                if (Properties.Settings.Default.OpenWhenDone) Process.Start("explorer.exe", "/select, \"" + SaveWAD.FileName + "\"");
            }
            catch (Exception ex)
            {
                ToggleWaitingIcon(false);
                MessageBox.Show(String.Format(x.Get("m003"), ex.Message), x.Get("halt"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (File.Exists(key_path)) File.Delete(key_path);
                try { Directory.Delete(Paths.WorkingFolder, true); } catch { }

                if (File.Exists(input[0]) && input[0].EndsWith(Paths.PatchedSuffix))
                {
                    File.Delete(input[0]);
                    input[0] = input[0].Remove(input[0].Length - Paths.PatchedSuffix.Length, Paths.PatchedSuffix.Length);
                }
            }
        }
    }
}
