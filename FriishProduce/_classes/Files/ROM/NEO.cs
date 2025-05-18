namespace FriishProduce
{
    public class ROM_NEO : ROM
    {
        public ROM_NEO() : base() { }

        public override bool CheckValidity(string path)
        {
            return CheckZIPValidity(new string[] { "c1", "c2", "m1", "p1", "s1", "v1" }, true, true, path);
        }

        protected override void Load()
        {
            ;
        }
    }
}
