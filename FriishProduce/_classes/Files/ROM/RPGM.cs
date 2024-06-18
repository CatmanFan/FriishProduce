using System.IO;

namespace FriishProduce
{
    public class RPGM : ROM
    {
        public string GetTitle(string path = null)
        {
            if (path == null) return null;

            foreach (var file in Directory.EnumerateFiles(Path.GetDirectoryName(path), "*.*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(file).ToLower() == "rpg_rt.ini")
                {
                    foreach (var line in File.ReadAllLines(file))
                    {
                        if (line.ToLower().StartsWith("gametitle="))
                        {
                            return line.Substring("GameTitle=".Length);
                        }
                    }
                }
            }

            return null;
        }
    }
}
