using System.IO;
using System.Windows.Forms;

namespace FriishProduce
{
    public class U8
    {
        public static void Unpack(string input, string output)
        {
            if (!System.IO.Directory.Exists(output)) System.IO.Directory.CreateDirectory(output);
            Wii.U8.UnpackU8(input, output);
        }

        public static void Pack(string input, string output, bool deleteInput = true)
        {
            if (System.IO.File.Exists(output)) System.IO.File.Delete(output);
            var s = new int[3];
            var u = Wii.U8.PackU8(input, out s[0], out s[1], out s[2]);
            Wii.Tools.SaveFileFromByteArray(u, output);

            if (deleteInput) System.IO.Directory.Delete(input, true);
        }
    }

    public class WADs
    {
        public static void Unpack(string input, string output)
        {
            try { Directory.Delete(output, true); } catch { }
            Directory.CreateDirectory(output);
            
            File.WriteAllBytes(Application.StartupPath + "\\key.bin",
                Path.GetFileNameWithoutExtension(input).EndsWith("T") || Path.GetFileNameWithoutExtension(input).EndsWith("Q") ?
                libWiiSharp.CommonKey.GetKoreanKey() : libWiiSharp.CommonKey.GetStandardKey());
            Wii.WadUnpack.UnpackWad(input, output);
            File.Delete(Application.StartupPath + "\\key.bin");
        }

        public static void Pack(string input, string output)
        {
            if (File.Exists(output)) File.Delete(output);

            File.WriteAllBytes(Application.StartupPath + "\\key.bin",
                Path.GetFileNameWithoutExtension(input).EndsWith("T") || Path.GetFileNameWithoutExtension(input).EndsWith("Q") ?
                libWiiSharp.CommonKey.GetKoreanKey() : libWiiSharp.CommonKey.GetStandardKey());
            Wii.WadPack.PackWad(input, output);
            File.Delete(Application.StartupPath + "\\key.bin");
        }
    }
}
