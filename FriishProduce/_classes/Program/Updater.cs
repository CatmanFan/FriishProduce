using Octokit;
using Ookii.Dialogs.WinForms;
using System;
using System.Deployment.Application;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class Updater
    {
        private static bool _IsLatest = false;
        public static bool IsLatest { get => _IsLatest; private set { _IsLatest = value; } }

        public static string GetCurrentVersion()
        {
            var ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
            return "v" + ver.Substring(0, ver[4] != '0' ? 5 : 3);
        }

        public static async void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    bool doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        var client = new GitHubClient(new ProductHeaderValue("FriishProduce"));
                        var release = await client.Repository.Release.GetLatest("CatmanFan", "FriishProduce");

                        Version latest = new Version(release.TagName.Substring(1).Replace("-beta", ""));
                        Version current = new Version(GetCurrentVersion().Substring(1));

                        if (MessageBox.Show(string.Format(Program.Lang.Msg(8), latest, current), MessageBox.Buttons.YesNo, MessageBox.Icons.Shield) != MessageBox.Result.Yes)
                        {
                            doUpdate = false;
                        }
                    }

                    doUpdate = ad.Update();
                    if (doUpdate)
                    {
                        try
                        {
                            System.Windows.Forms.Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        public static async Task<bool> GetLatest()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("FriishProduce"));
                var release = await client.Repository.Release.GetLatest("CatmanFan", "FriishProduce");

                Version latest = new Version(release.TagName.Substring(1).Replace("-beta", ""));
                Version current = new Version(GetCurrentVersion().Substring(1));

                if (current.CompareTo(latest) < 0)
                {
                    if (MessageBox.Show(string.Format(Program.Lang.Msg(8), latest, current), MessageBox.Buttons.YesNo, MessageBox.Icons.Shield) == MessageBox.Result.Yes)
                    {
                        // *************************
                        // TO-DO
                        // *************************
                        System.Diagnostics.Process.Start("https://github.com/CatmanFan/FriishProduce/releases/latest");
                    }

                    IsLatest = false;
                }

                else IsLatest = true;
            }
            catch { }

            return IsLatest;
        }
    }
}
