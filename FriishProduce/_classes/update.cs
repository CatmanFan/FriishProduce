using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public static class Update
    {
        public static bool Languages()
        {
            bool modified = false;
            foreach (var item in Directory.EnumerateFiles(Paths.Languages))
            {
                using (WebClient c = new WebClient())
                {
                    string url = "https://github.com/CatmanFan/FriishProduce/raw/main/FriishProduce/langs/" + Path.GetFileNameWithoutExtension(item) + ".json";
                    var newFile = c.DownloadData(url);
                    if (Encoding.Default.GetString(newFile) != File.ReadAllText(item)) File.WriteAllBytes(item, newFile);
                    modified = true;
                }
            }
            return modified;
        }

        public static bool CheckNewVersion()
        {
            using (WebClient c = new WebClient())
            {
                var AssemblyInfo = Encoding.Default.GetString(c.DownloadData("https://github.com/CatmanFan/FriishProduce/raw/main/FriishProduce/Properties/AssemblyInfo.cs")).Split('\n');
                return !AssemblyInfo[34].Contains(System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion);
            }
        }
    }
}
