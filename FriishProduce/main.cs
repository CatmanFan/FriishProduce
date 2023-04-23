using System;
using System.ComponentModel;
using System.Diagnostics;
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
            ComponentResourceManager mang = new ComponentResourceManager(typeof(Main));
            var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            foreach (Control child in Controls.OfType<Panel>())
                mang.ApplyResources(child, child.Name, new CultureInfo(lang));

            InitializeComponent();

            label1.Text = string.Format(label1.Text, strings.version);
            ToolTip.SetToolTip(settings, new Settings().Text);
            BrowseWAD.Filter   += strings.browse_AllFiles;
            BrowsePatch.Filter += strings.browse_AllFiles;
            SaveWAD.Filter = BrowseWAD.Filter;
            Reset();
        }
        private void Console_Changed(object sender, EventArgs e)
        {
            foreach (var p in page4.Controls.OfType<Panel>())
                if (p.Name.StartsWith("Options_")) p.Visible = false;

            currentConsole = (Platforms)console.SelectedIndex;
            switch (currentConsole)
            {
                case Platforms.nes:
                    BrowseROM.Filter = strings.browseROM_nes;
                    Options_NES.Visible = true;
                    break;
                case Platforms.snes:
                    BrowseROM.Filter = strings.browseROM_snes;
                    break;
                case Platforms.n64:
                    BrowseROM.Filter = strings.browseROM_n64;
                    break;
                case Platforms.sms:
                case Platforms.smd:
                    SaveDataTitle.Enabled = false;
                    break;
            }
            BrowseROM.Filter += strings.browse_AllFiles;

            Reset();
            RefreshBases();

            next.Enabled = true;
        }

        /// <summary>
        /// Resets all input values and content option parameters
        /// </summary>
        private void Reset()
        {
            input = new string[input.Length];
            NES_Palette.SelectedIndex = 0;
            SaveDataTitle.Enabled = true;
        }

        // ***************************************************************************************************************** //
        private void Settings_Click(object sender, EventArgs e) => new Settings().ShowDialog(this);

        private void Next_Click(object sender, EventArgs e)
        {
            if (page1.Visible)
            {
                page2.Visible = true;
                page1.Visible = false;
                back.Enabled = true;
                next.Enabled = (input[0] != null) && (input[2] != null);
                label3.Text = input[0] != null ? Path.GetFileName(input[0]) : "N/A";
            }
            else if (page2.Visible)
            {
                page3.Visible = true;
                page2.Visible = false;
                next.Enabled = checkBannerPage();
            }
            else if (page3.Visible)
            {
                page4.Visible = true;
                page3.Visible = false;
                finish.Visible = true;
                finish.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");
                next.Visible = false;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (page2.Visible)
            {
                page1.Visible = true;
                page2.Visible = false;
                back.Enabled = false;
                next.Enabled = console.SelectedIndex >= 0;
            }
            else if (page3.Visible)
            {
                page2.Visible = true;
                page3.Visible = false;
                next.Enabled = (input[0] != null) && (input[2] != null);
            }
            else if (page4.Visible)
            {
                page3.Visible = true;
                page4.Visible = false;
                next.Visible = true;
                next.Enabled = checkBannerPage();
                finish.Visible = false;
            }
        }
        private bool checkBannerPage()
        {
            if (customize.Checked)
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
            if (BrowseROM.ShowDialog() == DialogResult.OK)
                input[0] = BrowseROM.FileName;
                label3.Text = input[0] != null ? Path.GetFileName(input[0]) : "N/A";

            next.Enabled = (input[0] != null) && (input[2] != null);
        }

        private void BaseList_Changed(object sender, EventArgs e)
        {
            foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                if (File.Exists(item) && item.Contains(XML.SearchID(baseList.SelectedItem.ToString(), currentConsole)))
                { 
                    input[2] = item;
                    goto End;
                }

            MessageBox.Show("Unable to find WAD in database.");
            baseList.Items.Remove(baseList.SelectedItem);

            End:
            next.Enabled = (input[0] != null) && (input[2] != null);
        }

        private void RefreshBases()
        {
            baseList.Items.Clear();
            baseList.DropDownHeight = 106;
            foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
            {
                if (File.Exists($"{Paths.Database}{currentConsole}\\{entry.Attributes["id"].Value}.wad"))
                    baseList.Items.Add(entry.Attributes["title"].Value);
            }

            baseList_banner.Items.Clear();
            baseList_banner.DropDownHeight = 106;
            foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
            {
                if (File.Exists($"{Paths.Database}{currentConsole}\\{entry.Attributes["id"].Value}.wad"))
                    baseList_banner.Items.Add(entry.Attributes["title"].Value);
            }
            if (baseList_banner.Items.Count > 0) baseList_banner.SelectedIndex = 0;
        }

        private void AddWAD(object sender, EventArgs e)
        {
            if (BrowseWAD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    libWiiSharp.WAD w = libWiiSharp.WAD.Load(BrowseWAD.FileName);
                    if (!w.HasBanner) goto Fail;

                    foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                    {
                        if (entry.Attributes["id"].Value == w.UpperTitleID)
                        {
                            string dest = $"{Paths.Database}{currentConsole}\\{w.UpperTitleID}.wad";
                            if (!File.Exists(dest)) File.Copy(BrowseWAD.FileName, dest);
                            RefreshBases();
                            baseList.SelectedItem = entry.Attributes["title"].Value;
                        }
                    }
                    w.Dispose();
                    return;
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
            if (MessageBox.Show(strings.deleteWAD, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                    if (File.Exists(item) && item.Contains(XML.SearchID(baseList.SelectedItem.ToString(), currentConsole)))
                    {
                        File.Delete(item);
                        input[2] = null;
                        baseList.Items.Remove(baseList.SelectedItem.ToString());
                        baseList.SelectedIndex = 0;
                    }

                next.Enabled = (input[0] != null) && (input[2] != null);
            }
        }

        // ***************************************************************************************************************** //

        private void Finish_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Paths.WorkingFolder)) Directory.Delete(Paths.WorkingFolder, true);

            if (SaveWAD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    input[0] = Injector.ApplyPatch(input[0], input[1]);

                    WAD w = WAD.Load(input[2]);
                    w.Unpack(Paths.WorkingFolder);

                    if (customize.Checked)
                    {
                        #region Banner
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

                        File.WriteAllBytes(Paths.WorkingFolder + "banner.brlyt", Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);
                        File.WriteAllBytes(Paths.Images + "VCPic.tpl", Banner.Data[Banner.GetNodeIndex("VCPic.tpl")]);
                        File.WriteAllBytes(Paths.Images + "IconVCPic.tpl", Icon.Data[Icon.GetNodeIndex("IconVCPic.tpl")]);

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
                            Arguments = $"{Paths.WorkingFolder + "banner.brlyt"} -Title \"{BannerTitle.Text.Replace('-', '–').Replace(Environment.NewLine, "^")}\" -YEAR {ReleaseYear.Value.ToString()} -Play {Players.Value.ToString()}",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }))
                            p.WaitForExit();

                        // --------------------------------------------------------------------------- //

                        var Brlyt = File.ReadAllBytes(Paths.WorkingFolder + "banner.brlyt");
                        if (Brlyt == Banner.Data[Banner.GetNodeIndex("banner.brlyt")])
                            throw new Exception("Banner.brlyt has not been modified. Make sure you have set up HowardC Tools properly and try again.");
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
                        #endregion

                        #region SaveData
                        #endregion
                    }

                    U8.Unpack(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5);
                    if (disableEmanual.Checked) Injector.RemoveEmanual();
                    tImg.InsertSaveBanner(currentConsole);

                    switch (currentConsole)
                    {
                        default:
                            throw new Exception("Not implemented yet!");

                        case Platforms.nes:
                            Injectors.NES NES = new Injectors.NES { ROM = input[0], content1_file = Injector.DetermineContent1() };
                            NES.InsertROM();
                            NES.InsertPalette(NES_Palette.SelectedIndex);
                            Injector.PrepareContent1(NES.content1_file);
                            break;

                        case Platforms.snes:
                            Injectors.SNES SNES = new Injectors.SNES() { ROM = input[0] };
                            SNES.ProduceID(XML.SearchID(baseList.SelectedItem.ToString(), currentConsole));
                            SNES.ReplaceROM();
                            break;

                        case Platforms.n64:
                            Injectors.N64 N64 = new Injectors.N64() { ROM = input[0] };
                            foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                            {
                                if (entry.Attributes["title"].Value == baseList.SelectedItem.ToString())
                                    foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                        if (item.Contains(entry.Attributes["id"].Value))
                                            N64.emuVersion = entry.Attributes["ver"].Value;
                            }
                            N64.ReplaceROM();
                            if (customize.Checked) N64.InsertSaveComments(SaveDataTitle.Lines);
                            break;
                    }

                    U8.Pack(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app");

                    w.CreateNew(Paths.WorkingFolder);
                    w.FakeSign = true;
                    if (RegionFree.Checked) w.Region = libWiiSharp.Region.Free;
                    w.ChangeTitleID(LowerTitleID.Channel, TitleID.Text);
                    w.Save(SaveWAD.FileName);
                    w.Dispose();
                    MessageBox.Show(strings.finished);

                    if (Properties.Settings.Default.enable_wad_to_hbc)
                    {
                        try
                        {
                            HBC.SendToHBC(SaveWAD.FileName, Properties.Settings.Default.wii_ip_address);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format(strings.errorTransmitter, ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(strings.errorInject, ex.Message), strings.error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    try { if (Directory.Exists(Paths.WorkingFolder)) Directory.Delete(Paths.WorkingFolder, true); }
                    finally
                    {
                        if (File.Exists(input[0]) && input[0].EndsWith(Paths.PatchedSuffix))
                        {
                            File.Delete(input[0]);
                            input[0] = input[0].Remove(input[0].Length - Paths.PatchedSuffix.Length, Paths.PatchedSuffix.Length);
                        }
                    }
                }
            }
        }

        private void NES_PaletteChanged(object sender, EventArgs e)
        {
            switch (NES_Palette.SelectedIndex)
            {
                default:
                case 0:
                    ToolTip.SetToolTip(NES_Palette, null);
                    break;
                case 1:
                    ToolTip.SetToolTip(NES_Palette, string.Format(strings.author, "SuperrSonic"));
                    break;
            }
        }

        private void BannerText_Changed(object sender, EventArgs e)
        {
            if (currentConsole == Platforms.sms || currentConsole == Platforms.smd) SaveDataTitle.Text = ChannelTitle.Text;
            if (SaveDataTitle.Lines.Length > 2) SaveDataTitle.Lines = new string[2] { SaveDataTitle.Lines[0], SaveDataTitle.Lines[1] };
            if (BannerTitle.Lines.Length > 2) BannerTitle.Lines = new string[2] { BannerTitle.Lines[0], BannerTitle.Lines[1] };

            next.Enabled = checkBannerPage();
        }

        private void BannerText_KeyPress(object sender, KeyPressEventArgs e)
        {
            var item = sender as TextBox;

            if (item.Multiline && !string.IsNullOrEmpty(item.Text))
            {
                int max = item.Name.Contains("Banner") ? 65 : 50;

                if (item.Lines[item.GetLineFromCharIndex(item.SelectionStart)].Length >= max)
                    e.Handled = !(e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Back);

                if (e.KeyChar == (char)Keys.Enter && item.Lines.Length >= 2)
                    e.Handled = true;
            }
        }

        private void TitleID_Changed(object sender, EventArgs e) => finish.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");

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
            Banner.Enabled = customize.Checked;
            next.Enabled = checkBannerPage();
        }

        private void ImportBanner_CheckedChanged(object sender, EventArgs e) => baseList_banner.Enabled = ImportBanner.Checked;

        private void Image_Click(object sender, EventArgs e)
        {
            if (BrowseImage.ShowDialog() == DialogResult.OK)
            {
                tImg = new TitleImage();
                tImg.path = BrowseImage.FileName;
                tImg.shrinkToFit = (int)currentConsole <= 2;
                if (Image_Stretch.SelectedIndex < 0)
                {
                    var resize = TitleImage.Resize.Stretch;
                    Image_Stretch.SelectedIndex = (int)resize;
                }

                try
                {
                    tImg.Generate(System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic,
                                  (TitleImage.Resize)Enum.ToObject(typeof(TitleImage.Resize), Image_Stretch.SelectedIndex));
                    Image.Image = tImg.VCPic;
                }
                catch (Exception)
                {
                    tImg = new TitleImage();
                }
                finally
                {
                    next.Enabled = checkBannerPage();
                }
            }
        }

        private void Image_StretchChanged(object sender, EventArgs e)
        {
            tImg.Generate(System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic,
                          (TitleImage.Resize)Enum.ToObject(typeof(TitleImage.Resize), Image_Stretch.SelectedIndex));
            Image.Image = tImg.VCPic;
            next.Enabled = checkBannerPage();
        }
    }
}
