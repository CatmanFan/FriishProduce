using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    public class ProcessHelper
    {
        public static void Run(string app, string arguments)
        {
            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = app,
                WorkingDirectory = System.IO.Path.GetDirectoryName(app),
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true
            }))
                p.WaitForExit();
        }

        public static void Run(string app, string arguments, bool showWindow = false)
        {
            using (Process p = Process.Start(new ProcessStartInfo
            {
                FileName = app,
                WorkingDirectory = System.IO.Path.GetDirectoryName(app),
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = !showWindow
            }))
                p.WaitForExit();
        }

        public static void Run(string app, string workingFolder, string arguments)
        {
            using (Process p = Process.Start(new ProcessStartInfo
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
