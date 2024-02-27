using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class MainForm : Form
    {
        private readonly SettingsForm s = new SettingsForm();

        internal LibRetroDB LibRetro { get; set; }

        private void AutoSetStrip()
        {
            foreach (ToolStripMenuItem section in MenuStrip.Items.OfType<ToolStripMenuItem>())
                foreach (ToolStripItem item in section.DropDownItems.OfType<ToolStripItem>())
                    if (Language.Get(item.Name, this) != "undefined") item.Text = Language.Get(item.Name, this);

            var Consoles = new ToolStripItem[]
                {
                    new ToolStripMenuItem(
                        Language.Get("Group0", "Platforms"), null,
                        new ToolStripItem[]
                        {
                            new ToolStripMenuItem(null, new Icon(Properties.Resources.nintendo_nes, 16, 16).ToBitmap(), AddProject, Console.NES.ToString()),
                            new ToolStripMenuItem(null, new Icon(Properties.Resources.nintendo_super_nes, 16, 16).ToBitmap(), AddProject, Console.SNES.ToString()),
                            new ToolStripMenuItem(null, new Icon(Properties.Resources.nintendo_nintendo64, 16, 16).ToBitmap(), AddProject, Console.N64.ToString()),
                            new ToolStripSeparator(),
                        }),

                    new ToolStripMenuItem(
                        Language.Get("Group1", "Platforms"), null,
                        new ToolStripItem[]
                        {
                            new ToolStripMenuItem(null, new Icon(Properties.Resources.sega_master_system, 16, 16).ToBitmap(), AddProject, Console.SMS.ToString()),
                            new ToolStripMenuItem(null, new Icon(Properties.Resources.sega_genesis__model_2_, 16, 16).ToBitmap(), AddProject, Console.SMDGEN.ToString()),
                            new ToolStripSeparator(),
                        }),

                    new ToolStripMenuItem(
                        Language.Get("Other"), null,
                        new ToolStripItem[]
                        {
                            new ToolStripMenuItem(null, new Icon(Properties.Resources.snk_neo_geo_aes, 16, 16).ToBitmap(), AddProject, Console.NeoGeo.ToString()),
                            new ToolStripSeparator(),
                        })
                };

            foreach (ToolStripMenuItem section in Consoles)
                foreach (ToolStripMenuItem item in section.DropDownItems.OfType<ToolStripMenuItem>())
                    item.Text = string.Format(Language.Get("ProjectType"), Language.Get(item.Name, "Platforms"));

            NewProject.DropDownItems.Clear();
            NewProject.DropDownItems.AddRange(Consoles);
        }

        /// <summary>
        /// Changes language of this form and all tab pages
        /// </summary>
        private void RefreshForm()
        {
            AutoSetStrip();
            Language.Localize(this);

            MenuItem_File.Text = Language.Get(MenuItem_File.Name, this);
            MenuItem_Project.Text = Language.Get(MenuItem_Project.Name, this);
            MenuItem_Help.Text = Language.Get(MenuItem_Help.Name, this);
            ToolStrip_Tutorial.Text = MenuItem_Tutorial.Text;
            ToolStrip_Tutorial.Image = MenuItem_Tutorial.Image;
            ToolStrip_Settings.Text = MenuItem_Settings.Text = Language.Get("Settings");
            ToolStrip_OpenROM.Text = OpenROM.Text = Language.Get(OpenROM.Name, this);
            ToolStrip_OpenImage.Text = OpenImage.Text = Language.Get(OpenImage.Name, this);
            ToolStrip_OpenManual.Text = OpenManual.Text = Language.Get(OpenManual.Name, this);
            ToolStrip_UseLibRetro.Text = UseLibRetro.Text = Language.Get(UseLibRetro.Name, this);
            ToolStrip_ExportWAD.Text = ExportWAD.Text = Language.Get(ExportWAD.Name, this);
            ToolStrip_CloseTab.Text = CloseTab.Text = Language.Get("B.Close");

            BrowseROM.Title = OpenROM.Text;
            BrowseImage.Title = OpenImage.Text;
            SaveWAD.Title = ExportWAD.Text;

            BrowseImage.Filter = Language.Get("Filter.Img");
            SaveWAD.Filter = Language.Get("Filter.WAD");

            Welcome_DoNotShow.Text = Language.Get("DoNotShow");

            foreach (MdiTabControl.TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Form.GetType() == typeof(InjectorForm))
                    (tabPage.Form as InjectorForm).RefreshForm();
            }

            CustomToolStripRenderer r = new CustomToolStripRenderer() { RoundedEdges = false };
            /* MenuStrip.Renderer = */ ToolStrip.Renderer = r;
            Refresh();
        }

        private class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            public CustomToolStripRenderer() { }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, Color.WhiteSmoke, Color.White, LinearGradientMode.Vertical))
                    e.Graphics.FillRectangle(b, b.Rectangle);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            RefreshForm();

            Welcome_DoNotShow.Visible = PointToTutorial.Visible = Welcome.Visible = !Properties.Settings.Default.DoNotShow_Welcome;

            Program.Handle = Handle;
            tabControl.Location = MainPanel.Location;
            tabControl.Size = new Size(MainPanel.Width, 1000);
            Size = new Size
            (
                MainPanel.Width + (ToolStrip.Dock == DockStyle.Right || ToolStrip.Dock == DockStyle.Left ? ToolStrip.Width - 8 : 16),
                MainPanel.Location.Y + MainPanel.Height + 38
            );
            CenterToScreen();

            // Automatically set defined initial directory for save file dialog
            // ********
            SaveWAD.InitialDirectory = Paths.Out;

            // FriishProduce.Update.GetLatest();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.UI_Language;

            s.ShowDialog(this);

            if (lang != Properties.Settings.Default.UI_Language) RefreshForm();
        }

        public void TabChanged(object sender, EventArgs e)
        {
            // Toggle visibility of Open ROM/Image buttons
            // ********
            if (sender != tabControl.TabPages[0]) OpenROM.Enabled = tabControl.TabPages.Count > 1;
            else OpenROM.Enabled = true;

            // Toggle visibility of Export WAD button
            // Toggle visibility of Download LibRetro data button
            // ********
            if (!OpenROM.Enabled)
            {
                ToolStrip_ExportWAD.Enabled = ExportWAD.Enabled = false;
                UseLibRetro.Enabled = false;
                tabControl.Visible = false;
                MainPanel.Visible = true;
            }

            else ExportCheck(sender, e);

            ToolStrip_CloseTab.Enabled = CloseTab.Enabled
             = ToolStrip_OpenManual.Enabled = OpenManual.Enabled
             = ToolStrip_OpenImage.Enabled = OpenImage.Enabled
             = ToolStrip_OpenROM.Enabled = OpenROM.Enabled;

            // Context menu
            // ********
            if (tabControl.TabPages.Count >= 1)
                if (sender == tabControl.TabPages[0])
                {
                    UseLibRetro.Enabled = (tabControl.SelectedForm as InjectorForm).CheckToolStripButtons()[0];
                    OpenManual.Enabled = (tabControl.SelectedForm as InjectorForm).CheckToolStripButtons()[1];
                }

            ToolStrip_UseLibRetro.Enabled = UseLibRetro.Enabled;
            ToolStrip_OpenManual.Enabled = OpenManual.Enabled;
        }

        private void ExportCheck(object sender, EventArgs e)
        {
            ToolStrip_ExportWAD.Enabled = ExportWAD.Enabled = (tabControl.SelectedForm as InjectorForm).ReadyToExport;
            ToolStrip_UseLibRetro.Enabled = UseLibRetro.Enabled = (tabControl.SelectedForm as InjectorForm).CheckToolStripButtons()[0];
        }

        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            var collection = tabControl.TabPages;

            for (int i = 0; i < collection.Count; i++)
            {
                var tabPage = tabControl.TabPages[i];

                InjectorForm x = tabPage?.Form as InjectorForm;
                if (x?.Tag?.ToString() == "dirty")
                {
                    tabControl.TabPages[tabControl.TabPages.get_IndexOf(tabPage)].Select();
                    bool Cancelled = !x.CheckUnsaved();

                    if (Cancelled)
                    { e.Cancel = true; return; }
                }
            }

            foreach (MdiTabControl.TabPage tabPage in collection)
            {
                try
                {
                    (tabPage.Form as Form).Tag = null;
                }
                catch { }
            }
        }

        /// <summary>
        /// Adds a new project to the Main Form.
        /// </summary>
        private void AddProject(object sender, EventArgs e)
        {
            var source = sender as ToolStripMenuItem;

            if (!Enum.TryParse(source.Name.ToString(), out Console console))
                return;

            InjectorForm Tab = new InjectorForm(console) { Parent = this, Icon = source.Image != null ? Icon.FromHandle((source.Image as Bitmap).GetHicon()) : null };
            Tab.FormClosed += TabChanged;
            Tab.ExportCheck += ExportCheck;
            tabControl.TabBackHighColor = Tab.BackColor;
            tabControl.TabPages.Add(Tab);

            tabControl.Visible = true;
            Welcome_DoNotShow.Visible = PointToTutorial.Visible = Welcome.Visible = MainPanel.Visible = false;

            // BrowseROMDialog(console, Tab);
        }

        private void OpenROM_Click(object sender, EventArgs e) => BrowseROMDialog((tabControl.SelectedForm as InjectorForm).Console, tabControl.SelectedForm as InjectorForm);

        private void BrowseROMDialog(Console c, InjectorForm currentForm)
        {
            switch (c)
            {
                default:
                    BrowseROM.Filter = Language.Get("Filter.Disc") + "|" + Language.Get("Filter.ZIP") + Language.Get("Filter");
                    break;

                case Console.NES:
                case Console.SNES:
                case Console.N64:
                case Console.SMS:
                case Console.SMDGEN:
                case Console.PCE:
                    BrowseROM.Filter = Language.Get($"Filter.ROM.{c}");
                    break;

                case Console.NeoGeo:
                    BrowseROM.Filter = Language.Get("Filter.ZIP");
                    break;
            }

            if (BrowseROM.ShowDialog() == DialogResult.OK)
            {
                ToolStrip_UseLibRetro.Enabled = UseLibRetro.Enabled = currentForm.CheckToolStripButtons()[0];
                ToolStrip_OpenManual.Enabled = OpenManual.Enabled = currentForm.CheckToolStripButtons()[1];

                currentForm.LoadROM(UseLibRetro.Enabled ? Properties.Settings.Default.AutoLibRetro : false);
            }
        }

        private void UseLibRetro_Click(object sender, EventArgs e) => (tabControl.SelectedForm as InjectorForm).LoadLibRetroData();

        private void ExportWAD_Click(object sender, EventArgs e)
        {
            CleanTemp();
            var currentForm = tabControl.SelectedForm as InjectorForm;

            SaveWAD.FileName = currentForm.GetName();
            if (SaveWAD.ShowDialog() == DialogResult.OK)
                if (currentForm.CreateInject())
                    currentForm.Close();
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            if (BrowseImage.ShowDialog() == DialogResult.OK)
            {
                var currentForm = tabControl.SelectedForm as InjectorForm;
                currentForm.LoadImage(BrowseImage.FileName);
            }
        }

        private void OpenManual_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.DoNotShow_000) MessageBox.Show(Language.Get("Message.006"), 0);
            (tabControl.SelectedForm as InjectorForm).LoadManual(BrowseManual.ShowDialog() == DialogResult.OK ? BrowseManual.SelectedPath : null);
        }

        private void Tutorial_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_DoNotShow_Click(object sender, EventArgs e)
        {
            Welcome_DoNotShow.Visible = PointToTutorial.Visible = Welcome.Visible = false;
            Properties.Settings.Default.DoNotShow_Welcome = true;
            Properties.Settings.Default.Save();
        }

        public void CleanTemp()
        {
            try
            {
                foreach (var item in Directory.GetFiles(Paths.WorkingFolder, "*.*", SearchOption.AllDirectories))
                    if (!Path.GetFileName(item).ToLower().Contains("readme.md")) File.Delete(item);
                foreach (var item in Directory.GetDirectories(Paths.WorkingFolder))
                    Directory.Delete(item, true);
            }
            catch { }
        }

        private void TabContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var page = (sender as ContextMenuStrip).SourceControl;
            var index = tabControl.TabPages.get_IndexOf(page as MdiTabControl.TabPage);
            if (index != -1) tabControl.TabPages[index].Select();
        }

        private void CloseTab_Click(object sender, EventArgs e) => (tabControl.SelectedForm as Form).Close();

        private void About_Click(object sender, EventArgs e) => new About().ShowDialog();

        private void MenuItem_Exit_Click(object sender, EventArgs e) => Application.Exit();
    }
}
