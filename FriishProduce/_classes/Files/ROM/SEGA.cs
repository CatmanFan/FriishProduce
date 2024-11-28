namespace FriishProduce
{
    public class ROM_SEGA : ROM
    {
        public bool IsSMS { get; set; }

        public ROM_SEGA() : base() { }

        protected override void Load()
        {
            MaxSize = (int)(IsSMS ? 1048576 : 5.25 * 1024 * 1024);
        }
    }
}
