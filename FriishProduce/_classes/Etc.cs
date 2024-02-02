using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public class WADKit
    {
        public static string GenerateTitleID()
        {
            var r = new Random();
            string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(allowed, 4).Select(s => s[r.Next(s.Length)]).ToArray());
        }

        public static libWiiSharp.WAD ReplaceContent(libWiiSharp.WAD w, int index, byte[] U8, bool obligatory = true)
        {
            // Check if both source and replacement U8 are the same
            // ****************
            if (w.Contents[index] == U8)
            {
                // If replacing the U8 is recommended to continue, throw an exception, otherwise if it's not needed return the original WAD
                // ****************
                if (obligatory) throw new Exception(Language.Get("Error002"));
                else return w;
            }

            // Create new list of U8 contents, starting from the index of which to replace
            // ****************
            List<byte[]> U8Contents = new List<byte[]>();
            for (int i = index; i < w.Contents.Length; i++)
                U8Contents.Add(w.Contents[i]);
            U8Contents[0] = U8;

            // remove all U8 indexes starting from that
            // ****************
            for (int i = w.Contents.Length - 1; i >= index; i--)
                w.RemoveContent(i);

            // and add the new ones in
            // ****************
            for (int i = 0; i < U8Contents.Count; i++)
                w.AddContent(U8Contents[i], i + index, i + index);

            return w;
        }
    }

    public class Process
    {
        public static void Run(string app, string arguments)
        {
            using (System.Diagnostics.Process p = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = app,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();
        }

        public static void Run(string app, string workingFolder, string arguments)
        {
            using (System.Diagnostics.Process p = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = app,
                WorkingDirectory = workingFolder,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();
        }
    }
}
