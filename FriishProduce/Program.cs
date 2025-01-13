using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FriishProduce
{
    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        public static Settings Config { get; set; }
        public static MainForm MainForm { get; private set; }
        public static Language Lang { get; set; }
        public static IntPtr Handle { get; set; }
        public static bool DebugMode { get => Config?.application?.debug_mode ?? false; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major < 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0))
            {
                System.Windows.Forms.MessageBox.Show("To use this program, please upgrade to Windows 7 or a newer version of Windows.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(-1);
                return;
            }

            else if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                foreach (var Process in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
                    if (Process.Handle != Process.GetCurrentProcess().Handle && Process.MainWindowHandle != IntPtr.Zero)
                    {
                        SwitchToThisWindow(Process.MainWindowHandle, true);
                        Environment.Exit(0);
                        return;
                    }
            }

            try
            {
                foreach (var item in Directory.GetFiles(Paths.WorkingFolder, "*.*", SearchOption.AllDirectories))
                    if (!Path.GetFileName(item).ToLower().Contains("readme.md")) File.Delete(item);
                foreach (var item in Directory.GetDirectories(Paths.WorkingFolder))
                    Directory.Delete(item, true);
            }
            catch { }

            Config = new(Paths.Config);
            Lang = new Language();

            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang.Current);

            if (args.Length > 0 && args[0].StartsWith("--"))
            {
                CLI(args);
                return;
            }

            GUI(args);
        }

        /// <summary>
        /// Runs the GUI version of the app.
        /// </summary>
        /// <param name="args"></param>
        private static void GUI(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new MainForm(args);
            Application.Run(MainForm);
        }

        /// <summary>
        /// Runs the CLI version of the app.
        /// </summary>
        /// <param name="args"></param>
        private static void CLI(string[] args)
        {
            // Usage:
            //
            // "file"
            // --patch "file"
            // --wad "file"
            // --platform nes snes smd flash
            // --img "file"
            // --sound "file"
            // --channel-title "title"
            // --savedata-title "title"
            // --banner-title "title"
            // --banner-year 1990
            // --banner-players 1
            // --wad-region japan usa europe korea free
            // --tid ABCD

            Console.Clear();
            Console.WriteLine("FriishProduce v" + FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion.ToString() + " (CLI)");
            Console.WriteLine();
            Console.WriteLine("Console version is not implemented yet.");

            Environment.Exit(0);
        }
    }
}
