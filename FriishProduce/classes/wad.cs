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
                w.ChangeChannelTitles(channelTitle);

                w.Save(output);
                w.Dispose();
                System.IO.Directory.Delete(input, true);
            }
        }
    }
    public class U8
    {
        public static void Unpack(string input, string output)
        {
            using (libWiiSharp.U8 u = new libWiiSharp.U8())
            {
                u.LoadFile(input);
                u.Extract(output);
                u.Dispose();
            }
        }
        public static void Pack(string input, string output)
        {
            using (libWiiSharp.U8 u = new libWiiSharp.U8())
            {
                u.CreateFromDirectory(input);
                u.Save(output);
                u.Dispose();
                System.IO.Directory.Delete(input, true);
            }
        }
    }
}
