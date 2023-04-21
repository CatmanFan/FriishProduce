using libWiiSharp;

namespace FriishProduce.classes
{
    public class WAD
    {
        public static void Unpack(string input, string output)
        {
            libWiiSharp.WAD w = new libWiiSharp.WAD();
            w.LoadFile(input);
            w.Unpack(output);
            w.Dispose();
        }
        public static void Pack(string input, string output)
        {
            libWiiSharp.WAD w = new libWiiSharp.WAD();
            w.CreateNew(input);
            w.Unpack(output);
            w.Dispose();
        }
    }
}
