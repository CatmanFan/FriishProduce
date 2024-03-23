namespace FriishProduce
{
    public class ROM_SNES : ROM
    {
        public ROM_SNES() : base()
        {
            MaxSize = 4194304;
        }

        public override bool CheckValidity(string path)
        {
            try
            {
                var data = System.IO.File.ReadAllBytes(path);

                return data.Length % 1024 == 512 || data.Length % 1024 == 0;
            }

            catch { return false; }
        }
    }
}
