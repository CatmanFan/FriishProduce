using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Automation;

namespace FriishProduce
{
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        public static IntPtr Handle { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major < 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0))
            {
                System.Windows.Forms.MessageBox.Show($"This program is not supported for the current Windows version you are running ({Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}). Please use Windows 7 or a newer version to run this program.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(-1);
                return;
            }
            else if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                foreach (var Process in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
                    if (Process.Handle != Process.GetCurrentProcess().Handle)
                        SwitchToThisWindow(Process.MainWindowHandle, true);

                Application.Exit();
                return;
            }

            Language.Run();

            try
            {
                foreach (var item in Directory.GetFiles(Paths.WorkingFolder, "*.*", SearchOption.AllDirectories))
                    if (!Path.GetFileName(item).ToLower().Contains("readme.md")) File.Delete(item);
                foreach (var item in Directory.GetDirectories(Paths.WorkingFolder))
                    Directory.Delete(item, true);
            }
            catch { }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            { System.Threading.Thread.CurrentThread.CurrentUICulture = Language.Current; }
        }
    }
}
