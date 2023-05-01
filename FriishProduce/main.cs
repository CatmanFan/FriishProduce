using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using libWiiSharp;

namespace FriishProduce
{
    public partial class Main : Form
    {
        Lang x = Program.Language;
        Platforms currentConsole = 0;
        Database db;

        string[] input = new string[]
            {
                /* Full path to ROM       */ null,
                /* Full path to ROM patch */ null,
                /* Full path to WAD file  */ null
            };
        Dictionary<string, string> btns = new Dictionary<string, string>();
        TitleImage tImg = new TitleImage();

        public Main()
        {
            InitializeComponent();
            ChangeTheme();

            string[] consoles = new string[]
            {
                x.Get("NES"),
                x.Get("SNES"),
                x.Get("N64"),
                x.Get("SMS"),
                x.Get("SMD"),
                x.Get("Flash")
            };
            foreach (var console in consoles) Console.Items.Add(console);

            x.Localize(this);

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string ver = $"beta {fvi.FileVersion}";

            Text = (x.Get("g000"));
            a000.Text = string.Format(x.Get("a000"), ver);
            ToolTip.SetToolTip(Settings, x.Get("g001"));

            BrowseWAD.Filter   = x.Get("f_wad") + x.Get("f_all");
            BrowseImage.Filter = x.Get("f_img") + x.Get("f_all");
            BrowsePatch.Filter = x.Get("f_bps") + x.Get("f_all");
            SaveWAD.Filter = BrowseWAD.Filter;

            Reset();
        }

        private void Console_Changed(object sender, EventArgs e)
        {
            a001.Text = x.Get("a001");
            OpenROM.Text = x.Get("g003");
            foreach (var p in page4.Controls.OfType<Panel>())
                if (p.Name.StartsWith("Options_")) p.Visible = false;

            currentConsole = (Platforms)Console.SelectedIndex;
            db = new Database((int)currentConsole);
            DisableEmanual.Visible = true;

            switch (currentConsole)
            {
                case Platforms.NES:
                    BrowseROM.Filter = x.Get("f_nes");
                    Options_NES.Visible = true;
                    SaveDataTitle.MaxLength = 20;
                    break;

                case Platforms.SNES:
                    BrowseROM.Filter = x.Get("f_sfc");
                    break;

                case Platforms.N64:
                    BrowseROM.Filter = x.Get("f_n64");
                    Options_N64.Visible = true;
                    break;

                case Platforms.SMS:
                    BrowseROM.Filter = x.Get("f_sms");
                    Options_SEGA.Visible = true;
                    break;

                case Platforms.SMD:
                    BrowseROM.Filter = x.Get("f_smd");
                    Options_SEGA.Visible = true;
                    break;

                case Platforms.Flash:
                    a001.Text = x.Get("a001_swf");
                    OpenROM.Text = x.Get("g004");
                    BrowseROM.Filter = x.Get("f_swf");
                    Options_Flash.Visible = true;

                    DisableEmanual.Visible = false;
                    break;
            }

            BrowseROM.Filter += x.Get("f_all");
            Reset();
            RefreshBases();

            Image.Image = tImg.Generate(currentConsole);
            SaveDataTitle.Enabled = !(currentConsole == Platforms.SMS || currentConsole == Platforms.SMD);
            SaveDataTitle.MaxLength = 80;
            Patch.Visible = currentConsole != Platforms.Flash;

            Next.Enabled = true;
        }

        /// <summary>
        /// Resets all input values and content option parameters
        /// </summary>
        private void Reset()
        {
            input = new string[input.Length];
            Patch.Checked = false;
            NES_Palette.SelectedIndex = 0;
            SEGA_Region.SelectedIndex = 0;
            SEGA_MDPad6B.Enabled = currentConsole == Platforms.SMD;
            Flash_TotalSaveDataSize.SelectedIndex = 0;
            Flash_FPS.SelectedIndex = 0;
            Flash_StrapReminder.SelectedIndex = 0;
            SaveDataTitle.Enabled = true;
            btns = new Dictionary<string, string>();
        }

        // ***************************************************************************************************************** //
        private void Settings_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings() { Text = x.Get("g001"), };
            ChangeTheme(SettingsForm);
            if (SettingsForm.ShowDialog(this) == DialogResult.OK) ChangeTheme();
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
                Image.BackColor = Themes.Light.BG_Image;
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
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
                Image.BackColor = Themes.Dark.BG_Image;
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var c2 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c2.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.FlatAppearance.BorderColor = Themes.Dark.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Dark.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
        }

        private void ChangeTheme(Form f)
        {
            f.BackColor = BackColor;
            f.ForeColor = ForeColor;
            foreach (var item in f.Controls.OfType<Panel>())
                if (item.Tag.ToString() == "panel") item.BackColor = panel.BackColor;
            foreach (var item in f.Controls.OfType<ComboBox>())
                if (item.Name.StartsWith("btns"))   item.BackColor = BackColor;

            if (Properties.Settings.Default.LightTheme)
            {
                foreach (var panel in f.Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
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
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var c2 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c2.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.FlatAppearance.BorderColor = Themes.Dark.ButtonBorder;
                        button.FlatAppearance.MouseDownBackColor = Themes.Dark.ButtonDown;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
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
                Next.Enabled = checkBannerPage();
            }
            else if (page3.Visible)
            {
                page4.Visible = true;
                page3.Visible = false;
                Save.Visible = true;
                Save.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");
                Next.Visible = false;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (page2.Visible)
            {
                page1.Visible = true;
                page2.Visible = false;
                Back.Visible = false;
                Back.Enabled = false;
                Next.Enabled = Console.SelectedIndex >= 0;
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
                Next.Enabled = checkBannerPage();
                Save.Visible = false;
            }
        }
        private bool checkBannerPage()
        {
            if (Custom.Checked)
                return !(string.IsNullOrEmpty(ChannelTitle.Text)
                      || string.IsNullOrEmpty(BannerTitle.Text)
                      || string.IsNullOrEmpty(SaveDataTitle.Text));
            return true;
        }

        // ***************************************************************************************************************** //

        private void OpenROM_Click(object sender, EventArgs e)
        {
            input[0] = null;
            input[1] = null;
            if (BrowseROM.ShowDialog() == DialogResult.OK)
                input[0] = BrowseROM.FileName;
                ROMPath.Text = input[0] != null ? Path.GetFileName(input[0]) : x.Get("a002");

            Patch.Checked = false;
            Next.Enabled = (input[0] != null) && (input[2] != null);
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
                    if (File.Exists($"{db.CurrentFolder(currentConsole)}{entry["id"].ToString().ToUpper()}.wad"))
                    {
                        Bases.Items.Add(entry["title"].ToString());
                        Bases.Enabled = Bases.Items.Count >= 2;
                    }

                Bases.SelectedIndex = Bases.Items.Count < 1 ? -1 : 0;
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

            checkBannerPage();
        }

        private void AddWAD(object sender, EventArgs e)
        {
            if (BrowseWAD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    WAD w = WAD.Load(BrowseWAD.FileName);
                    if (!w.HasBanner) goto Fail;

                    foreach (var entry in db.GetList())
                    {
                        if (entry["id"].ToString().ToUpper() == w.UpperTitleID)
                        {
                            string dest = $"{db.CurrentFolder(currentConsole)}{w.UpperTitleID}.wad";
                            if (!Directory.Exists(Path.GetDirectoryName(dest)))
                                Directory.CreateDirectory(Path.GetDirectoryName(dest));
                            File.Copy(BrowseWAD.FileName, dest, true);
                            RefreshBases();
                            Bases.SelectedItem = entry["title"].ToString();
                            w.Dispose();
                            return;
                        }
                    }
                    w.Dispose();
                    goto Fail;
                }
                catch (Exception)
                {
                    goto Fail;
                }

                Fail:
                System.Media.SystemSounds.Beep.Play();
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
                        if (File.Exists(item) && entry["id"].ToString().ToUpper() == Path.GetFileNameWithoutExtension(item))
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

        private void BannerText_Changed(object sender, EventArgs e)
        {
            if (currentConsole == Platforms.SMS || currentConsole == Platforms.SMD) SaveDataTitle.Text = ChannelTitle.Text;
            if (SaveDataTitle.Lines.Length > 2) SaveDataTitle.Lines = new string[2] { SaveDataTitle.Lines[0], SaveDataTitle.Lines[1] };
            if (BannerTitle.Lines.Length > 2) BannerTitle.Lines = new string[2] { BannerTitle.Lines[0], BannerTitle.Lines[1] };
            foreach (string line1 in SaveDataTitle.Lines)
            foreach (string line2 in BannerTitle.Lines)
            {
                if (line1.Length > 40) line1.Remove(39);
                if (line2.Length > 65) line2.Remove(64);
            }

            Next.Enabled = checkBannerPage();
        }

        private void BannerText_KeyPress(object sender, KeyPressEventArgs e)
        {
            var item = sender as TextBox;

            if (item.Multiline && !string.IsNullOrEmpty(item.Text))
            {
                int max = item.Name.Contains("Banner") ? 65 : 40;

                if (item.Lines[item.GetLineFromCharIndex(item.SelectionStart)].Length >= max)
                    e.Handled = !(e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Back);

                if (e.KeyChar == (char)Keys.Enter && item.Lines.Length >= 2)
                    e.Handled = true;
            }
        }

        private void TitleID_Changed(object sender, EventArgs e) => Save.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");

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
                    Next.Enabled = checkBannerPage();
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
            }
        }

        private void Image_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (BrowseImage.ShowDialog() == DialogResult.OK)
                {
                    tImg = new TitleImage(currentConsole)
                    {
                        path = BrowseImage.FileName,
                        ResizeMode = ImgResize.SelectedIndex < 0 ?
                                     (currentConsole == Platforms.Flash ? TitleImage.Resize.Fit : TitleImage.Resize.Stretch)
                                     : (TitleImage.Resize)ImgResize.SelectedIndex,
                        InterpolationMode = ImgInterp.SelectedIndex < 0 ?
                                            System.Drawing.Drawing2D.InterpolationMode.Default :
                                            (System.Drawing.Drawing2D.InterpolationMode)ImgInterp.SelectedIndex
                    };
                    if (ImgResize.SelectedIndex < 0) ImgResize.SelectedIndex = (int)tImg.ResizeMode;
                    if (ImgInterp.SelectedIndex < 0) ImgInterp.SelectedIndex = (int)tImg.InterpolationMode;

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
                        Next.Enabled = checkBannerPage();
                    }
                    return;
                }
                else
                {
                    goto Clear;
                }
            }
            else
            {
                goto Clear;
            }

            Clear:
            Image.Image = null;
            tImg = new TitleImage(currentConsole);
            Next.Enabled = checkBannerPage();
        }

        private void Image_StretchChanged(object sender, EventArgs e)
        {
            tImg.ResizeMode = (TitleImage.Resize)ImgResize.SelectedIndex;
            Image.Image = tImg.Generate(currentConsole);
            Next.Enabled = checkBannerPage();
        }

        private void Image_ModeIChanged(object sender, EventArgs e)
        {
            tImg.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)ImgInterp.SelectedIndex;
            Image.Image = tImg.Generate(currentConsole);
            Next.Enabled = checkBannerPage();
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

        private void SEGA_ControllerChanged(object sender, EventArgs e)
        {
            if (SEGA_Controller.Checked)
            {
                Views.SEGA_Controller ControllerForm = new Views.SEGA_Controller(btns) { Text = x.Get("g006"), };
                ChangeTheme(ControllerForm);

                if (ControllerForm.ShowDialog(this) == DialogResult.OK)
                    btns = ControllerForm.Config;
                else
                    SEGA_Controller.Checked = false;
            }
        }

        // ***************************************************************************************************************** //

        private void Finish_Click(object sender, EventArgs e)
        {
            SaveWAD.FileName = TitleID.Text;
            if (SaveWAD.ShowDialog() == DialogResult.OK)
            {
                try { Directory.Delete(Paths.WorkingFolder, true); } catch { }

                try
                {
                    if (currentConsole != Platforms.Flash) input[0] = Global.ApplyPatch(input[0], input[1]);

                    WAD w = WAD.Load(input[2]);
                    var ios = (int)w.StartupIOS;
                    w.Unpack(Paths.WorkingFolder);

                    #region Banner
                    if (Custom.Checked)
                    {
                        if (Import.Checked)
                        {
                            foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                if (File.Exists(item) && item.Contains(db.SearchID(ImportBases.SelectedItem.ToString())))
                                    WAD.Load(item).BannerApp.Save(Paths.WorkingFolder + "00000000.app");
                        }

                        // --------------------------------------------------------------------------- //

                        // Get banner.brlyt and TPLs
                        Directory.CreateDirectory(Paths.Images);

                        libWiiSharp.U8 BannerApp = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000000.app");
                        libWiiSharp.U8 Banner = libWiiSharp.U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("banner.bin")]);
                        libWiiSharp.U8 Icon = libWiiSharp.U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("icon.bin")]);

                        try
                        {
                            File.WriteAllBytes(Paths.WorkingFolder + "banner.brlyt", Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

                            if (tImg.path != null)
                            {
                                File.WriteAllBytes(Paths.Images + "VCPic.tpl", Banner.Data[Banner.GetNodeIndex("VCPic.tpl")]);
                                File.WriteAllBytes(Paths.Images + "IconVCPic.tpl", Icon.Data[Icon.GetNodeIndex("IconVCPic.tpl")]);
                            }
                        }
                        catch
                        {
                            throw new Exception(x.Get("m008"));
                        }

                        // --------------------------------------------------------------------------- //

                        // Copy VCbrlyt to working folder
                        string path = Paths.Apps + "vcbrlyt\\";
                        foreach (string dir in Directory.GetDirectories(path))
                            Directory.CreateDirectory(dir.Replace(path, Paths.WorkingFolder + "vcbrlyt\\"));
                        foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                            File.Copy(file, file.Replace(path, Paths.WorkingFolder + "vcbrlyt\\"));

                        using (Process p = Process.Start(new ProcessStartInfo
                        {
                            FileName = Paths.WorkingFolder + "vcbrlyt\\vcbrlyt.exe",
                            Arguments = $"{Paths.WorkingFolder + "banner.brlyt"} -Title \"{BannerTitle.Text.Replace('-', '–').Replace(Environment.NewLine, "^")}\" -YEAR {ReleaseYear.Value} -Play {Players.Value}",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }))
                            p.WaitForExit();

                        // --------------------------------------------------------------------------- //

                        var Brlyt = File.ReadAllBytes(Paths.WorkingFolder + "banner.brlyt");
                        if (Brlyt == Banner.Data[Banner.GetNodeIndex("banner.brlyt")])
                            throw new Exception(x.Get("m007"));
                        Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), Brlyt);

                        // --------------------------------------------------------------------------- //

                        if (tImg.path != null)
                        {
                            TPL tpl = TPL.Load(Paths.Images + "VCPic.tpl");
                            var tplTF = tpl.GetTextureFormat(0);
                            var tplPF = tpl.GetPaletteFormat(0);
                            tpl.RemoveTexture(0);
                            tpl.AddTexture(tImg.VCPic, tplTF, tplPF);
                            tpl.Save(Paths.Images + "VCPic.tpl");

                            tpl = TPL.Load(Paths.Images + "IconVCPic.tpl");
                            tplTF = tpl.GetTextureFormat(0);
                            tplPF = tpl.GetPaletteFormat(0);
                            tpl.RemoveTexture(0);
                            tpl.AddTexture(tImg.IconVCPic, tplTF, tplPF);
                            tpl.Save(Paths.Images + "IconVCPic.tpl");

                            tpl.Dispose();

                            Banner.ReplaceFile(Banner.GetNodeIndex("VCPic.tpl"), File.ReadAllBytes(Paths.Images + "VCPic.tpl"));
                            Icon.ReplaceFile(Icon.GetNodeIndex("IconVCPic.tpl"), File.ReadAllBytes(Paths.Images + "IconVCPic.tpl"));

                            Icon.AddHeaderImd5();
                            BannerApp.ReplaceFile(BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());
                        }

                        Banner.AddHeaderImd5();
                        BannerApp.ReplaceFile(BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
                        BannerApp.AddHeaderImet(false, ChannelTitle.Text);
                        BannerApp.Save(Paths.WorkingFolder + "00000000.app");

                        BannerApp.Dispose();
                        Banner.Dispose();
                        Icon.Dispose();

                        Directory.Delete(Paths.Images, true);
                        File.Delete(Paths.WorkingFolder + "banner.brlyt");
                        Directory.Delete(Paths.WorkingFolder + "vcbrlyt\\", true);
                    }
                    #endregion

                    if (currentConsole != Platforms.Flash)
                    {
                        if (!(currentConsole == Platforms.SMS || currentConsole == Platforms.SMD))
                            U8.Unpack(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5);
                        else
                        {
                            Directory.CreateDirectory(Paths.WorkingFolder_Content5);

                            // Write data.ccf directly from U8 loader
                            libWiiSharp.U8 u = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000005.app");
                            File.WriteAllBytes(Paths.WorkingFolder_Content5 + "data.ccf", u.Data[u.GetNodeIndex("data.ccf")]);
                            u.Dispose();

                            // Extract CCF files
                            new Injectors.SEGA().GetCCF();
                        }

                        if (DisableEmanual.Checked) Global.RemoveEmanual();
                        if (Custom.Checked && tImg.path != null) tImg.CreateSave(currentConsole);

                        switch (currentConsole)
                        {
                            default:
                                throw new Exception("Not implemented yet!");

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
                                    if (tImg.path != null)
                                    {
                                        if (NES.ExtractSaveTPL(Paths.WorkingFolder + "out.tpl"))
                                            tImg.CreateSave(Platforms.NES);
                                    }

                                    NES.InsertSaveData(SaveDataTitle.Text, Paths.WorkingFolder + "out.tpl");
                                }
                                Global.PrepareContent1();
                                break;
                            }

                            case Platforms.SNES:
                            {
                                Injectors.SNES SNES = new Injectors.SNES()
                                {
                                    ROM = input[0],
                                    ROMcode = new Injectors.SNES().ProduceID(db.SearchID(Bases.SelectedItem.ToString()))
                                };

                                SNES.ReplaceROM();

                                if (Custom.Checked) SNES.InsertSaveTitle(SaveDataTitle.Lines);
                                break;
                            }

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

                                if (N64_FixBrightness.Checked || N64_UseExpansionPak.Checked || N64_Allocation.Checked)
                                {
                                    string content1_file = Global.DetermineContent1();
                                    var content1 = File.ReadAllBytes(content1_file);
                                    if (N64_FixBrightness.Checked) content1 = N64.FixBrightness(content1);
                                    if (N64_UseExpansionPak.Checked) content1 = N64.ExpansionRAM(content1);
                                    if (N64_Allocation.Checked) content1 = N64.AllocateROM(content1);
                                    File.WriteAllBytes(content1_file, content1);
                                    Global.PrepareContent1();
                                }
                                N64.ReplaceROM();

                                if (Custom.Checked) N64.InsertSaveComments(SaveDataTitle.Lines);
                                break;
                            }

                            case Platforms.SMS:
                            case Platforms.SMD:
                            {
                                Injectors.SEGA SEGA = new Injectors.SEGA() { ROM = input[0], SMS = currentConsole == Platforms.SMS };
                                foreach (var entry in db.GetList())
                                {
                                    if (entry["title"].ToString() == Bases.SelectedItem.ToString())
                                        foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                            if (item.Contains(entry["id"].ToString().ToUpper()))
                                                SEGA.ver = int.Parse(entry["ver"].ToString());
                                }
                                
                                SEGA.ReplaceROM();

                                // Config parameters
                                SEGA.SetRegion(SEGA_Region.SelectedItem.ToString());
                                if (SEGA_SaveSRAM.Checked) SEGA.SRAM();
                                if (SEGA_MDPad6B.Checked && !SEGA.SMS) SEGA.MDPad_6B();
                                if (SEGA_Controller.Checked) SEGA.SetController(btns);

                                SEGA.ReplaceConfig();
                                SEGA.PackCCF();
                                break;
                            }
                        }

                        if (!(currentConsole == Platforms.SMS || currentConsole == Platforms.SMD))
                            U8.Pack(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app");
                        else
                        {
                            // Write data.ccf directly to U8 loader, and save
                            libWiiSharp.U8 u = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000005.app");
                            u.ReplaceFile(u.GetNodeIndex("data.ccf"), File.ReadAllBytes(Paths.WorkingFolder_Content5 + "data.ccf"));
                            u.Save(Paths.WorkingFolder + "00000005.app");
                            u.Dispose();

                            Directory.Delete(Paths.WorkingFolder_Content5, true);
                        }
                    }
                    else if (currentConsole == Platforms.Flash)
                    {
                        U8.Unpack(Paths.WorkingFolder + "00000002.app", Paths.WorkingFolder_Content2);

                        Injectors.Flash Flash = new Injectors.Flash() { SWF = input[0] };
                        Flash.ReplaceSWF();

                        Flash.HomeMenuNoSave(Flash_HBMNoSave.Checked);
                        Flash.SetStrapReminder(Flash_StrapReminder.SelectedIndex);
                        if (Flash_UseSaveData.Checked) Flash.EnableSaveData(Convert.ToInt32(Flash_TotalSaveDataSize.SelectedItem.ToString()));
                        if (Flash_CustomFPS.Checked) Flash.SetFPS(Flash_FPS.SelectedItem.ToString());
                        if (Flash_Controller.Checked) Flash.SetController(btns);
                        if (Custom.Checked && tImg.path != null) tImg.CreateSave(Platforms.Flash);
                        if (Custom.Checked) Flash.InsertSaveData(SaveDataTitle.Lines);

                        U8.Pack(Paths.WorkingFolder_Content2, Paths.WorkingFolder + "00000002.app");
                    }

                    // TO-DO: IOS video mode patching

                    var subdirectories = Directory.EnumerateDirectories(Paths.WorkingFolder);
                    if (subdirectories != null)
                        foreach (var item in subdirectories)
                            Directory.Delete(item, true);

                    w.CreateNew(Paths.WorkingFolder);
                    if (RegionFree.Checked) w.Region = libWiiSharp.Region.Free;
                    w.FakeSign = true;
                    w.ChangeTitleID(LowerTitleID.Channel, TitleID.Text);
                    w.Save(SaveWAD.FileName);
                    w.Dispose();

                    Focus();
                    System.Media.SystemSounds.Beep.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(x.Get("m003"), ex.Message), x.Get("halt"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
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
}
