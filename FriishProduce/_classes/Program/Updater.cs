using Octokit;
using System;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class Updater
    {
        public static Version AppVersion = new Version("1.6");

        private static bool _IsLatest = false;
        public static bool IsLatest { get => _IsLatest; private set { _IsLatest = value; } }

        private static void InstallLatest()
        {
            System.Diagnostics.Process.Start("https://github.com/CatmanFan/FriishProduce/releases/latest");
        }

        public static async Task<bool> GetLatest()
        {
            try
            {

                var client = new GitHubClient(new ProductHeaderValue("FriishProduce"));
                var release = await client.Repository.Release.GetLatest("CatmanFan", "FriishProduce");

                Version latest = release.TagName.ToLower() == "latest" && !release.Prerelease && !release.Draft ? new Version("9.99") : new Version(release.TagName.Substring(1).Replace("-beta", ""));

                if (AppVersion.CompareTo(latest) < 0 && !release.Prerelease)
                {
                    if (MessageBox.Show(string.Format(Program.Lang.Msg(8), latest, AppVersion), MessageBox.Buttons.YesNo, MessageBox.Icons.Shield) == MessageBox.Result.Yes)
                    {
                        InstallLatest();
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
