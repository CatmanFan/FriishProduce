using System.Diagnostics;

namespace FriishProduce
{
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
            }

            System.IO.Directory.Delete(input, true);
        }
    }

    public class U8_WSZST
    {
        public static void Unpack(string input, string output)
        {
            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = Paths.Apps + "wszst\\wszst.exe",
                WorkingDirectory = Paths.WorkingFolder,
                Arguments = $"X {input} -d {output}",
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();

            System.IO.File.Delete(input);
        }

        public static void Pack(string input, string output)
        {
            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = Paths.Apps + "wszst\\wszst.exe",
                WorkingDirectory = Paths.WorkingFolder,
                Arguments = $"C {input} -d {output}",
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();

            System.IO.Directory.Delete(input, true);
        }
    }
}
