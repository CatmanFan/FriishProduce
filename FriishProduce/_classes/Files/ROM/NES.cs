namespace FriishProduce
{
    public class ROM_NES : ROM
    {
        public ROM_NES() : base() { }

        public override bool CheckValidity(string path)
        {
            try
            {
                var data = System.IO.File.ReadAllBytes(path);

                return data[0] == 0x4E && data[1] == 0x45 && data[2] == 0x53 && data[3] == 0x1A;
            }

            catch { return false; }
        }
    }
}
