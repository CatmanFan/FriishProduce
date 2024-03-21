using libWiiSharp;

namespace FriishProduce
{
    public class Creator
    {
        // -----------------------------------
        // Public variables
        // -----------------------------------
        public enum RegionType
        {
            Universal,
            Japan,
            Korea,
            Europe,
        };

        public RegionType OrigRegion = RegionType.Universal;

        public string TitleID { get; set; }
        public string[] ChannelTitles { get; set; }
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string[] SaveDataTitle { get; set; }
        public string Out { get; set; }

        public Creator()
        {
            TitleID = null;
            ChannelTitles = new string[] { "無題", "Untitled", "Ohne Titel", "Sans titre", "Sin título", "Senza titolo", "Onbekend", "제목 없음" };
            BannerTitle = null;
            BannerYear = 1980;
            BannerPlayers = 1;
            SaveDataTitle = new string[] { "" };
            Out = null;
        }

        public void MakeWAD(WAD w)
        {
            // Channel title
            // *******
            w.ChangeChannelTitles(ChannelTitles);

            // WAD metadata
            // *******
            w.ChangeTitleID(LowerTitleID.Channel, TitleID.ToUpper());
            w.FakeSign = true;

            w.Save(Out);
        }
    }
}
