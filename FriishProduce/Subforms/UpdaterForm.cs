using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public partial class UpdaterForm : Form
    {
        public UpdaterForm(Release Latest, Version VerLatest)
        {
            this.Latest = Latest;
            InitializeComponent();

            Program.Lang.Control(b_yes);
            Program.Lang.Control(b_no);
            Text = Program.Lang.String("update", "mainform");
            desc1.Text = string.Format(Program.Lang.String("update1", "mainform"), VerLatest, Updater.VerName);
            desc2.Text = Program.Lang.String("update2", "mainform");

            b_yes.Visible = b_no.Visible = true;
            Theme.ChangeColors(this, true);
            Theme.BtnSizes(b_yes, b_no);
            Theme.BtnLayout(this, b_yes, b_no);

            string[] body = Latest?.Body.Split('\n');

            htmlPanel1.BackColor = BackColor;
            htmlPanel1.BaseStylesheet = HTML.BaseStylesheet + "\n" + "div { padding: 6px !important; }";
            htmlPanel1.Text = body?.Length > 0 ? HTML.MarkdownToHTML(body) : "<div>" + Program.Lang.String("none") + "</div>";
        }

        private bool busy = false;
        private Release Latest { get; set; }

        private void Progress_Update(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress.Value = e.ProgressPercentage;
        }

        private async void Yes_Click(object sender, EventArgs e)
        {
            string url = Latest.Assets[0].BrowserDownloadUrl;

            busy = true;
            Program.MainForm.Enabled = false;

            ControlBox = false;
            Text = Program.Lang.Msg(4, 2);
            b_no.Visible = b_yes.Visible = false;

            try
            {
                desc2.Text = Program.Lang.Msg(2, 2);
                wait.Visible = true;
                await Task.Run(() => { Web.InternetTest(null, false); });
                desc2.Visible = wait.Visible = false;
                Progress.Visible = true;

                // Delete pre-existing files
                // ****************
                Updater.ClearFiles();
                Program.CleanTemp();
                Logger.Log($"Updating to FriishProduce {Latest.TagName}.");

                // Open WebClient
                // ****************
                using var webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");
                webClient.Headers.Add(HttpRequestHeader.Authorization, "token " + Updater.Client.Credentials.GetToken());
                webClient.Headers.Add(HttpRequestHeader.Accept, "application/octet-stream");
                webClient.DownloadProgressChanged += Progress_Update;

                // Download
                // ****************
                while (!File.Exists(Paths.Update) || File.ReadAllBytes(Paths.Update)?.Length == 0)
                    await webClient.DownloadFileTaskAsync(new Uri(url), Paths.Update);

                if (!File.Exists(Paths.Update) || (File.Exists(Paths.Update) && File.ReadAllBytes(Paths.Update)?.Length == 0))
                    throw new Exception(Program.Lang.Msg(19, 1));

                Progress.Value = 100;
                Progress.Style = ProgressBarStyle.Marquee;

                // Extract files
                // ****************
                await Task.Run(() => { Updater.Extract(); });
            }

            catch (Exception ex)
            {
                Progress.Visible = desc2.Visible = wait.Visible = false;
                try { File.Delete(Paths.Update); } catch { }

                if (Program.DebugMode)
                    throw ex;
                else
                    MessageBox.Error(ex.GetType() == typeof(WebException) ? Web.Message(ex, url) : ex.Message);

                busy = false;
                Program.MainForm.Enabled = true;
                Close();
            }
        }

        private void Updater_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (busy)
            {
                System.Media.SystemSounds.Beep.Play();
                e.Cancel = true;
            }
        }
    }
}
