namespace FriishProduce
{
    public class Disc : ROM
    {
        public override bool CheckValidity(string path)
        {
            if (System.IO.Path.GetExtension(path).ToLower() == ".cue")
            {
                foreach (var item in System.IO.Directory.EnumerateFiles(System.IO.Path.GetDirectoryName(path)))
                {
                    if ((System.IO.Path.GetExtension(item).ToLower() == ".bin" || System.IO.Path.GetExtension(item).ToLower() == ".iso")
                        && System.IO.Path.GetFileNameWithoutExtension(path).ToLower() == System.IO.Path.GetFileNameWithoutExtension(item).ToLower())
                        return true;
                }

                return false;
            }

            return System.IO.Path.GetExtension(path).ToLower() == ".iso" || System.IO.Path.GetExtension(path).ToLower() == ".chd";
        }
    }
}
