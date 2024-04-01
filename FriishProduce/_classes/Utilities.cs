using libWiiSharp;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FriishProduce
{
    public static class Byte
    {
        public static readonly byte[] Dummy = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        public static int PatternAt(byte[] source, byte[] pattern)
        {
            for (int i = 0; i < source.Length; i++)
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    return i;
            return -1;
        }

        public static int PatternAt(byte[] source, string text)
        {
            byte[] pattern = Encoding.ASCII.GetBytes(text);
            for (int i = 0; i < source.Length; i++)
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    return i;
            return -1;
        }

        public static int IndexOf(byte[] source, byte[] pattern, int start, int end)
        {
            if (start < 0) start = 0;
            if (end > source.Length) end = source.Length - pattern.Length;

            for (int i = start; i < end; i++)
            {
                if (source[i] != pattern[0]) continue;

                for (int x = pattern.Length - 1; x >= 1; x--)
                {
                    try { if (source[i + x] != pattern[x]) break; } catch { }
                    if (x == 1) return i;
                }
            }

            return -1;
        }

        public static int IndexOf(byte[] source, string pattern, int start = 0, int end = -1)
        {
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            if (end == -1) end = source.Length - pBytes.Length;
            return IndexOf(source, pBytes, start, end);
        }

        public static bool IsSame(byte[] first, byte[] second)
        {
            return first.Length == second.Length && System.Collections.StructuralComparisons.StructuralEqualityComparer.Equals(first, second);
        }
    }

    public static class Utils
    {
        public static void Run(string app, string arguments, bool showWindow = false) => Run(app, Paths.Tools, arguments, showWindow);

        public static void Run(string app, string workingFolder, string arguments, bool showWindow = false)
        {
            var appPath = System.IO.Path.Combine(Paths.Tools, app.Replace(Paths.Tools, "").Contains('\\') ? app.Replace(Paths.Tools, "") : System.IO.Path.GetFileName(app));

            if (!appPath.EndsWith(".exe")) appPath += ".exe";

            if (!System.IO.File.Exists(appPath)) throw new Exception(string.Format(Program.Lang.Msg(5, true), app));

            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = appPath,
                WorkingDirectory = workingFolder,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = !showWindow
            }))
                p.WaitForExit();
        }

        public static void SendToHBC(byte[] file)
        {
            string address = Microsoft.VisualBasic.Interaction.InputBox("Please enter the IP address of your Wii/Wii U.\nThis can be found by opening the Homebrew Channel and pressing HOME to access the main menu.", Program.Lang.ApplicationTitle);
            if (!string.IsNullOrWhiteSpace(address)) return;

            HbcTransmitter h = new HbcTransmitter(Protocol.JODI, address);
        }
    }
}
