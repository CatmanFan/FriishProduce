using Octokit;
using Ookii.Dialogs.WinForms;
using System;
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
