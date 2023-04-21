using System;
using System.Diagnostics;
using System.IO;
using libWiiSharp;

namespace FriishProduce.Injectors
{
    public class N64
    {
        internal static string WAD;
        internal static string emuVersion;

        internal N64(string WADfile, string rom)
        {
            string outfile = $"{Paths.WorkingFolder_Content5}romc";
            Process p = Process.Start(new ProcessStartInfo
            {
                FileName = $"{Paths.Apps}romc.exe",
                Arguments = $"e {rom} {outfile}",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            p.WaitForExit();

            if (!File.Exists(outfile))
                throw new Exception("The ROMC compression process failed.");
        }

        public static void ROMC(string input, string output)
        {
            if (emuVersion == "romc")
            {
                // TO-DO
            }
        }
    }
}