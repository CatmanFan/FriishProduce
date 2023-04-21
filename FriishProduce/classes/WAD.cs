using libWiiSharp;

namespace FriishProduce
{
    public class WAD
    {
        public static void Unpack(string input, string output)
        {
            using (libWiiSharp.WAD w = new libWiiSharp.WAD())
            {
                w.LoadFile(input);
                w.Unpack(output);
                w.Dispose();
            }
        }
        public static void Pack(string input, string[] channelTitle, string TID, string output)
        {
            using (libWiiSharp.WAD w = new libWiiSharp.WAD())
            {
                w.CreateNew(input);

                w.ChangeTitleID(LowerTitleID.Channel, TID);
                w.FakeSign = true;
                w.Region = Region.Free;
                w.CommonKeyType = CommonKeyType.Standard;
                w.Lz77CompressBannerAndIcon = true;
                w.ChangeChannelTitles(channelTitle);

                w.Save(output);
                w.Dispose();
            }
        }
    }
}
