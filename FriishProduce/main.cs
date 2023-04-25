using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        Localization lang = Program.lang;
        Platforms currentConsole = 0;

        string[] input = new string[]
            {
                /* Full path to ROM       */ null,
                /* Full path to ROM patch */ null,
                /* Full path to WAD file  */ null
            };
        TitleImage tImg = new TitleImage();

        public Main()
        {
            InitializeComponent();
            lang.ChangeFormLanguage(this);
            ChangeTheme();

            this.Text = (lang.Get("AppTitle"));
            label1.Text = string.Format(lang.Get("label1.Text"), Strings.version);
            ToolTip.SetToolTip(Settings, lang.Get("Settings"));

            BrowseWAD.Filter = lang.Get("BrowseWAD.Filter") + lang.Get("BrowseAll");
            BrowseImage.Filter = lang.Get("BrowseImage.Filter") + lang.Get("BrowseAll");
            BrowsePatch.Filter = lang.Get("BrowsePatch.Filter") + lang.Get("BrowseAll");
            SaveWAD.Filter = BrowseWAD.Filter;

            Reset();
        }

        private void Console_Changed(object sender, EventArgs e)
        {
            foreach (var p in page4.Controls.OfType<Panel>())
                if (p.Name.StartsWith("Options_")) p.Visible = false;

            currentConsole = (Platforms)Console.SelectedIndex;
            OpenROM.Text = lang.Get("OpenROM.Text");
            switch (currentConsole)
            {
                case Platforms.NES:
                    BrowseROM.Filter = lang.Get("BrowseROM_NES");
                    Options_NES.Visible = true;
                    SaveDataTitle.MaxLength = 20;
                    break;
                case Platforms.SNES:
                    BrowseROM.Filter = lang.Get("BrowseROM_SNES");
                    break;
                case Platforms.N64:
                    BrowseROM.Filter = lang.Get("BrowseROM_N64");
                    Options_N64.Visible = true;
                    break;
                case Platforms.Flash:
                    OpenROM.Text = lang.Get("OpenSWF.Text");
                    BrowseROM.Filter = lang.Get("BrowseSWF");
                    Options_Flash.Visible = true;
                    break;
            }
            BrowseROM.Filter += lang.Get("BrowseAll");
            Reset();
            RefreshBases();

            tImg.SetPlatform(currentConsole);
            SaveDataTitle.Enabled = !(currentConsole == Platforms.SMS || currentConsole == Platforms.SMD);
            SaveDataTitle.MaxLength = 80;

            Patch.Visible = currentConsole != Platforms.Flash;
            DisableEmanual.Visible = currentConsole != Platforms.Flash;

            Next.Enabled = true;
        }

        /// <summary>
        /// Resets all input values and content option parameters
        /// </summary>
        private void Reset()
        {
            input = new string[input.Length];
            NES_PaletteList.SelectedIndex = 0;
            Flash_TotalSaveDataSize.SelectedIndex = 0;
            SaveDataTitle.Enabled = true;
        }

        // ***************************************************************************************************************** //
        private void Settings_Click(object sender, EventArgs e)
        {
            Settings SettingsForm = new Settings()
            {
                Owner = this
            };
            if (SettingsForm.ShowDialog(this) == DialogResult.OK) ChangeTheme();
        }

        /// <summary>
        /// Sets the theme to light or dark mode
        /// </summary>
        private void ChangeTheme()
        {
            if (Properties.Settings.Default.LightTheme)
            {
                BackColor = Color.FromArgb(248, 248, 248);
                ForeColor = Color.Black;
                panel.BackColor = Color.FromArgb(232, 232, 232);
                Image.BackColor = Color.Gainsboro;
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var c1 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c1.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.System;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.FlatAppearance.BorderColor = Color.LightGray;
                        button.FlatAppearance.MouseDownBackColor = Color.Silver;
                        button.FlatAppearance.MouseOverBackColor = button.FlatAppearance.BorderColor;
                    }
                }
            }
            else
            {
                BackColor = Color.FromArgb(55, 55, 55);
                ForeColor = Color.White;
                panel.BackColor = Color.FromArgb(75, 75, 75);
                Image.BackColor = Color.FromArgb(100, 100, 100);
                foreach (var panel in Controls.OfType<Panel>())
                {
                    foreach (var cb in panel.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var c2 in panel.Controls.OfType<Panel>())
                        foreach (var cb in c2.Controls.OfType<CheckBox>()) cb.FlatStyle = FlatStyle.Standard;
                    foreach (var button in panel.Controls.OfType<Button>())
                    {
                        button.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
                        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 50, 50);
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
                label3.Text = input[0] != null ? Path.GetFileName(input[0]) : lang.Get("label3.Text");
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
            if (Customize.Checked)
                return !(string.IsNullOrEmpty(ChannelTitle.Text)
                      || string.IsNullOrEmpty(BannerTitle.Text)
                      || string.IsNullOrEmpty(SaveDataTitle.Text)
                      || tImg.path == null);
            return true;
        }

        // ***************************************************************************************************************** //

        private void OpenROM_Click(object sender, EventArgs e)
        {
            input[0] = null;
            input[1] = null;
            if (BrowseROM.ShowDialog() == DialogResult.OK)
                input[0] = BrowseROM.FileName;
                label3.Text = input[0] != null ? Path.GetFileName(input[0]) : lang.Get("label3.Text");

            Patch.Checked = false;
            Next.Enabled = (input[0] != null) && (input[2] != null);
        }

        private void BaseList_Changed(object sender, EventArgs e)
        {
            if (baseList.SelectedIndex >= 0)
            {
                foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                    if (File.Exists(item) && item.Contains(XML.SearchID(baseList.SelectedItem.ToString(), currentConsole)))
                    {
                        input[2] = item;
                        goto End;
                    }

                MessageBox.Show("Unable to find WAD in database.");
                baseList.Items.Remove(baseList.SelectedItem);
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
                baseList.Enabled = false;
                baseList.Items.Clear();
                baseList_banner.SelectedIndex = -1;
                baseList.DropDownHeight = 106;
                foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                    if (File.Exists($"{XML.CurrentFolder(currentConsole)}{entry.Attributes["id"].Value.ToUpper()}.wad"))
                    {
                        baseList.Items.Add(entry.Attributes["title"].Value);
                        baseList.Enabled = baseList.Items.Count >= 2;
                    }

                baseList.SelectedIndex = baseList.Items.Count < 1 ? -1 : 0;
            }

            DeleteBase.Enabled = baseList.SelectedIndex >= 0;

            if (baseList.Items.Count > 0)
            {
                baseList_banner.Items.Clear();
                foreach (var item in baseList.Items)
                    if (item != baseList.SelectedItem)
                    {
                        baseList_banner.Items.Add(item);
                        baseList_banner.SelectedIndex = 0;
                    }
            }
            if (baseList_banner.Items.Count == 0)
            {
                ImportBanner.Checked = false;
                ImportBanner.Enabled = false;
                baseList_banner.Enabled = false;
            }
            else
            {
                ImportBanner.Enabled = true;
                baseList_banner.Enabled = ImportBanner.Checked;
                baseList_banner.SelectedIndex = 0;
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

                    foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                    {
                        if (entry.Attributes["id"].Value == w.UpperTitleID)
                        {
                            string dest = $"{XML.CurrentFolder(currentConsole)}{w.UpperTitleID}.wad";
                            if (!Directory.Exists(Path.GetDirectoryName(dest)))
                                Directory.CreateDirectory(Path.GetDirectoryName(dest));
                            File.Copy(BrowseWAD.FileName, dest, true);
                            RefreshBases();
                            baseList.SelectedItem = entry.Attributes["title"].Value;
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
            if (MessageBox.Show(lang.Get("Message_DeleteWAD"), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in Directory.GetFiles(Paths.Database, "*.wad", SearchOption.AllDirectories))
                    foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                    {
                        if (File.Exists(item) && entry.Attributes["id"].Value == Path.GetFileNameWithoutExtension(item))
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

        private void Finish_Click(object sender, EventArgs e)
        {
            SaveWAD.FileName = TitleID.Text;
            if (SaveWAD.ShowDialog() == DialogResult.OK)
            {
                try { Directory.Delete(Paths.WorkingFolder, true); } catch { }

                try
                {
                    input[0] = Injector.ApplyPatch(input[0], input[1]);

                    WAD w = WAD.Load(input[2]);
                    var ios = (int)w.StartupIOS;
                    w.Unpack(Paths.WorkingFolder);

                    #region Banner
                    if (Customize.Checked)
                    {
                        if (ImportBanner.Checked)
                        {
                            foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                if (File.Exists(item) && item.Contains(XML.SearchID(baseList_banner.SelectedItem.ToString(), currentConsole)))
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
                            File.WriteAllBytes(Paths.Images + "VCPic.tpl", Banner.Data[Banner.GetNodeIndex("VCPic.tpl")]);
                            File.WriteAllBytes(Paths.Images + "IconVCPic.tpl", Icon.Data[Icon.GetNodeIndex("IconVCPic.tpl")]);
                        }
                        catch
                        {
                            throw new Exception(lang.Get("Error_InvalidBanner"));
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
                            throw new Exception(lang.Get("Error_BannerBrlyt"));
                        Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), Brlyt);

                        // --------------------------------------------------------------------------- //

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

                        Banner.AddHeaderImd5();
                        Icon.AddHeaderImd5();
                        BannerApp.ReplaceFile(BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
                        BannerApp.ReplaceFile(BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());
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
                        U8.Unpack(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5);
                        if (DisableEmanual.Checked) Injector.RemoveEmanual();
                    }
                    else
                    {
                        U8.Unpack(Paths.WorkingFolder + "00000002.app", Paths.WorkingFolder_Content2);
                        if (DisableEmanual.Checked) Injector.RemoveEmanual();
                    }
                    if (Customize.Checked) tImg.CreateSave(currentConsole);

                    switch (currentConsole)
                    {
                        default:
                            throw new Exception("Not implemented yet!");

                        case Platforms.NES:
                            Injectors.NES NES = new Injectors.NES
                            {
                                ROM = input[0],
                                content1_file = Injector.DetermineContent1(),
                                saveTPL_offsets = new Injectors.NES().DetermineSaveTPLOffsets(Injector.DetermineContent1())
                            };

                            NES.InsertROM();
                            NES.InsertPalette(NES_PaletteList.SelectedIndex);
                            if (Customize.Checked)
                            {
                                if (NES.ExtractSaveTPL(Paths.WorkingFolder + "out.tpl"))
                                    tImg.CreateSave(Platforms.NES);
                                NES.InsertSaveData(SaveDataTitle.Text, Paths.WorkingFolder + "out.tpl");
                            }
                            Injector.PrepareContent1();
                            break;

                        case Platforms.SNES:
                            Injectors.SNES SNES = new Injectors.SNES()
                            {
                                ROM = input[0],
                                ROMcode = new Injectors.SNES().ProduceID(XML.SearchID(baseList.SelectedItem.ToString(), currentConsole))
                            };

                            SNES.ReplaceROM();

                            if (Customize.Checked) SNES.InsertSaveTitle(SaveDataTitle.Lines);
                            break;

                        case Platforms.N64:
                            Injectors.N64 N64 = new Injectors.N64() { ROM = input[0] };
                            foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                            {
                                if (entry.Attributes["title"].Value == baseList.SelectedItem.ToString())
                                    foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                        if (item.Contains(entry.Attributes["id"].Value))
                                            N64.emuVersion = entry.Attributes["ver"].Value;
                            }

                            if (N64_FixBrightness.Checked || N64_8MBRAM.Checked || N64_AllocateROM.Checked)
                            {
                                string content1_file = Injector.DetermineContent1();
                                var content1 = File.ReadAllBytes(content1_file);
                                if (N64_FixBrightness.Checked) content1 = N64.FixBrightness(content1);
                                if (N64_8MBRAM.Checked)        content1 = N64.ExpansionRAM(content1);
                                if (N64_AllocateROM.Checked)   content1 = N64.AllocateROM(content1);
                                File.WriteAllBytes(content1_file, content1);
                                Injector.PrepareContent1();
                            }
                            N64.ReplaceROM();

                            if (Customize.Checked) N64.InsertSaveComments(SaveDataTitle.Lines);
                            break;

                        case Platforms.Flash:
                            Injectors.Flash Flash = new Injectors.Flash() { SWF = input[0] };
                            Flash.ReplaceSWF();
                            Flash.HomeMenuNoSave(Flash_HBM_NoSave.Checked);
                            if (Flash_SaveData.Checked) Flash.EnableSaveData(Convert.ToInt32(Flash_TotalSaveDataSize.SelectedItem.ToString()));
                            if (Customize.Checked) Flash.InsertSaveData(SaveDataTitle.Lines);
                            break;
                    }

                    if (Directory.Exists(Paths.WorkingFolder_Content5)) U8.Pack(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app");
                    if (Directory.Exists(Paths.WorkingFolder_Content2)) U8.Pack(Paths.WorkingFolder_Content2, Paths.WorkingFolder + "00000002.app");

                    // TO-DO: IOS video mode patching

                    w.CreateNew(Paths.WorkingFolder);
                    w.FakeSign = true;
                    if (RegionFree.Checked) w.Region = libWiiSharp.Region.Free;
                    w.ChangeTitleID(LowerTitleID.Channel, TitleID.Text);
                    w.Save(SaveWAD.FileName);
                    w.Dispose();
                    MessageBox.Show(lang.Get("Message_ExportFinished"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(lang.Get("Message_ExportError"), ex.Message), lang.Get("Halt"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void NES_PaletteChanged(object sender, EventArgs e)
        {
            switch (NES_PaletteList.SelectedIndex)
            {
                default:
                case 0:
                    ToolTip.SetToolTip(NES_PaletteList, null);
                    break;
                case 1:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "SuperrSonic"));
                    break;
                case 2:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "Nintendo / SuperrSonic"));
                    break;
                case 3:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "Nintendo / FirebrandX"));
                    break;
                case 4:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "Nintendo / N-Mario"));
                    break;
                case 5:
                case 6:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "Nestopia"));
                    break;
                case 7:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "FCEUX"));
                    break;
                case 8:
                case 9:
                    ToolTip.SetToolTip(NES_PaletteList, string.Format(lang.Get("NES_PaletteList_Author"), "FirebrandX"));
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

        private void Customize_CheckedChanged(object sender, EventArgs e)
        {
            Banner.Enabled = Customize.Checked;
            Next.Enabled = checkBannerPage();
        }

        private void ImportBanner_CheckedChanged(object sender, EventArgs e) => baseList_banner.Enabled = ImportBanner.Checked;

        private void Image_Click(object sender, EventArgs e)
        {
            if (BrowseImage.ShowDialog() == DialogResult.OK)
            {
                tImg = new TitleImage(currentConsole) { path = BrowseImage.FileName };
                if (Image_Stretch.SelectedIndex < 0)
                {
                    var resize = TitleImage.Resize.Stretch;
                    Image_Stretch.SelectedIndex = (int)resize;
                }
                if (Image_ModeI.SelectedIndex < 0)
                {
                    tImg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                    Image_ModeI.SelectedIndex = 0;
                }

                try
                {
                    tImg.Generate((TitleImage.Resize)Enum.ToObject(typeof(TitleImage.Resize), Image_Stretch.SelectedIndex));
                    Image.Image = tImg.VCPic;
                }
                catch
                {
                    tImg = new TitleImage(currentConsole);
                }
                finally
                {
                    Next.Enabled = checkBannerPage();
                }
            }
        }

        private void Image_StretchChanged(object sender, EventArgs e)
        {
            tImg.Generate((TitleImage.Resize)Enum.ToObject(typeof(TitleImage.Resize), Image_Stretch.SelectedIndex));
            Image.Image = tImg.VCPic;
            Next.Enabled = checkBannerPage();
        }

        private void Image_ModeIChanged(object sender, EventArgs e)
        {
            tImg.InterpolationMode = (System.Drawing.Drawing2D.InterpolationMode)Enum.ToObject(typeof(System.Drawing.Drawing2D.InterpolationMode), Image_ModeI.SelectedIndex);
            tImg.Generate((TitleImage.Resize)Enum.ToObject(typeof(TitleImage.Resize), Image_Stretch.SelectedIndex));
            Image.Image = tImg.VCPic;
            Next.Enabled = checkBannerPage();
        }
    }
}
