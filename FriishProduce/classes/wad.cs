using libWiiSharp;

namespace FriishProduce
{
    public class HBC
    {
        public static void SendToHBC(string WADfile)
        {
            Protocol p = (Protocol)System.Enum.ToObject(typeof(Protocol), Properties.Settings.Default.hbc_protocol);
            HbcTransmitter t = new HbcTransmitter(p, Properties.Settings.Default.hbc_ip);
            t.TransmitFile(WADfile);
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
