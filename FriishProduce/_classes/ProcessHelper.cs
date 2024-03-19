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
        public static void Run(string app, string arguments, bool showWindow = false) => Run(app, Paths.Tools, arguments, showWindow);

        public static void Run(string app, string workingFolder, string arguments, bool showWindow = false)
        {
            var appPath = System.IO.Path.Combine(Paths.Tools, app.Replace(Paths.Tools, "").Contains('\\') ? app.Replace(Paths.Tools, "") : System.IO.Path.GetFileName(app));

            if (!appPath.EndsWith(".exe")) appPath += ".exe";

            if (!System.IO.File.Exists(appPath)) throw new Exception(string.Format(Language.Get("Error.005"), app));

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
    }
}
