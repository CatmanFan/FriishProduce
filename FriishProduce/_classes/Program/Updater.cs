using Octokit;
using Ookii.Dialogs.WinForms;
using System;

namespace FriishProduce
{
    public static class Updater
    {
        public static string GetCurrentVersion() => "v" + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion.Substring(0, 3);

        public static async void GetLatest()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("FriishProduce"));
                var releases = await client.Repository.Release.GetAll("CatmanFan", "FriishProduce");

                Version latest = new Version(releases[0].TagName.Substring(1).Replace("-beta", ""));
                Version current = new Version(GetCurrentVersion().Substring(1));

                if (current.CompareTo(latest) < 0)
                {
                    if (MessageBox.Show(
                        "An update is available",
                        $"Version {latest} is available for download.\nYou are currently running version {current}.\n\nWould you like to download the latest version?",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        TaskDialogIcon.Shield) == MessageBox.Result.Yes)
                    {
                        // *************************
                        // TO-DO
                        // *************************
                        System.Diagnostics.Process.Start("https://github.com/CatmanFan/FriishProduce/releases/latest");
                    }
                }
            }
            catch { }
        }
    }
}
