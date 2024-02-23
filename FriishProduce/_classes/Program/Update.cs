using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Ookii.Dialogs.WinForms;

namespace FriishProduce
{
    public class Update
    {
        public static async void GetLatest()
        {
            var client = new GitHubClient(new ProductHeaderValue("FriishProduce"));
            var releases = await client.Repository.Release.GetAll("CatmanFan", "FriishProduce");

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            Version latest = new Version(releases[0].TagName.Substring(1).Replace("-beta", ""));
            Version current = new Version(fvi.FileVersion.Substring(0, 3));

            if (current.CompareTo(latest) < 0)
            {
                if (MessageBox.Show(
                    "An update is available",
                    $"Version {latest} is available for download.\nYou are currently running version {current}.\n\nWould you like to update now?",
                    System.Windows.Forms.MessageBoxButtons.YesNo,
                    TaskDialogIcon.Shield) == System.Windows.Forms.DialogResult.Yes)
                {
                    // *************************
                    // TO-DO
                    // *************************
                    ;
                }
            }
        }
    }
}
