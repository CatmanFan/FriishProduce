using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class Main : Form
    {
        Platforms currentConsole = 0;

        string[] input = new string[]
            { 
                /* Full path to ROM      */ null,
                /* Full path to WAD file */ null
            };

        public Main()
        {
            ComponentResourceManager mang = new ComponentResourceManager(typeof(Main));
            var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            foreach (Control child in Controls.OfType<Panel>())
                mang.ApplyResources(child, child.Name, new CultureInfo(lang));

            InitializeComponent();

            label1.Text = string.Format(label1.Text, strings.version);
            NES_Palette.SelectedIndex = 0;
        }
        private void Console_Changed(object sender, EventArgs e)
        {
            foreach (var p in page4.Controls.OfType<Panel>())
                if (p.Name.StartsWith("Options_")) p.Visible = false;
            input = new string[input.Length];

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
            }

            RefreshBases();

            next.Enabled = true;
        }

        // ***************************************************************************************************************** //

        private void Next_Click(object sender, EventArgs e)
        {
            if (page1.Visible)
            {
                page2.Visible = true;
                page1.Visible = false;
                back.Enabled = true;
                next.Enabled = (input[0] != null) && (input[1] != null);
                label3.Text = input[0] != null ? Path.GetFileName(input[0]) : "N/A";
            }
            else if (page2.Visible)
            {
                page3.Visible = true;
                page2.Visible = false;
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
            }
            else if (page4.Visible)
            {
                page3.Visible = true;
                page4.Visible = false;
                next.Visible = true;
                finish.Visible = false;
            }
        }

        // ***************************************************************************************************************** //

        private void OpenROM_Click(object sender, EventArgs e)
        {
            input[0] = null;
            if (BrowseROM.ShowDialog() == DialogResult.OK)
                input[0] = BrowseROM.FileName;
                label3.Text = input[0] != null ? Path.GetFileName(input[0]) : "N/A";

            next.Enabled = (input[0] != null) && (input[1] != null);
        }

        private void BaseList_Changed(object sender, EventArgs e)
        {
            foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                if (File.Exists(item) && item.Contains(XML.SearchID(baseList.SelectedItem.ToString(), currentConsole)))
                    input[1] = item;

            next.Enabled = (input[0] != null) && (input[1] != null);
        }

        private void RefreshBases()
        {
            baseList.Items.Clear();
            foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
            {
                if (File.Exists($"{Paths.Database}{currentConsole}\\{entry.Attributes["id"].Value}.wad"))
                    baseList.Items.Add(entry.Attributes["title"].Value);
            }
        }

        private void AddWAD(object sender, EventArgs e)
        {
            if (BrowseWAD.ShowDialog() == DialogResult.OK)
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

                        goto End;
                    }
                }

                Fail:
                System.Media.SystemSounds.Beep.Play();
                w.Dispose();
                return;

                End:
                w.Dispose();
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
                        input[1] = null;
                        baseList.Items.Remove(baseList.SelectedItem.ToString());
                        baseList.SelectedIndex = 0;
                    }

                next.Enabled = (input[0] != null) && (input[1] != null);
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
                    WAD.Unpack(input[1], Paths.WorkingFolder);
                    U8.Unpack(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5);

                    if (disableEmanual.Checked) Injector.RemoveEmanual();

                    switch (currentConsole)
                    {
                        default:
                            throw new Exception("Not implemented yet!");

                        case Platforms.nes:
                            Injectors.NES NES = new Injectors.NES();
                            NES.ROM = input[0];
                            NES.content1_file = Injector.DetermineContent1();
                            NES.InsertROM();
                            NES.InsertPalette(NES_Palette.SelectedIndex);
                            Injector.PrepareContent1(NES.content1_file);
                            break;

                        case Platforms.n64:
                            Injectors.N64 N64 = new Injectors.N64();
                            N64.ROM = input[0];
                            foreach (System.Xml.XmlNode entry in XML.RetrieveList(currentConsole))
                            {
                                if (entry.Attributes["title"].Value == baseList.SelectedItem.ToString())
                                    foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                        if (item.Contains(entry.Attributes["id"].Value))
                                            N64.emuVersion = entry.Attributes["ver"].Value;
                            }
                            N64.ReplaceROM();
                            break;
                    }

                    U8.Pack(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app");
                    WAD.Pack(Paths.WorkingFolder, new string[] { "Test" }, TitleID.Text, SaveWAD.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(strings.errorInject, ex.Message), strings.error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    if (Directory.Exists(Paths.WorkingFolder)) Directory.Delete(Paths.WorkingFolder, true);
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
                    ToolTip.SetToolTip(NES_Palette, "Author: SuperrSonic");
                    break;
            }
        }

        private void TitleID_Changed(object sender, EventArgs e) => finish.Enabled = (TitleID.Text.Length == 4) && Regex.IsMatch(TitleID.Text, "^[A-Z0-9]*$");
    }
}
