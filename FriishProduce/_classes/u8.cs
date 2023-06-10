namespace FriishProduce
{
    public class U8
    {
        public static void Unpack(string input, string output)
        {
            if (!System.IO.Directory.Exists(output)) System.IO.Directory.CreateDirectory(output);
            Wii.U8.UnpackU8(input, output);
        }

        public static void libWiiSharpUnpack(string input, string output)
        {
            using (libWiiSharp.U8 u = new libWiiSharp.U8())
            {
                u.LoadFile(input);
                u.Extract(output);
                u.Dispose();
            }
        }

        public static void Pack(string input, string output, bool deleteInput = true)
        {
            if (System.IO.File.Exists(output)) System.IO.File.Delete(output);
            var s = new int[3];
            var u = Wii.U8.PackU8(input, out s[0], out s[1], out s[2]);
            Wii.Tools.SaveFileFromByteArray(u, output);

            if (deleteInput) System.IO.Directory.Delete(input, true);
        }

        public static void libWiiSharpPack(string input, string output, bool deleteInput = true)
        {
            using (libWiiSharp.U8 u = new libWiiSharp.U8())
            {
                u.CreateFromDirectory(input);
                u.Save(output);
                u.Dispose();
            }

            if (deleteInput) System.IO.Directory.Delete(input, true);
        }
    }
}
