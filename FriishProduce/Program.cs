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
                System.Windows.Forms.MessageBox.Show("To use this program, please upgrade to Windows 7 or a newer version of Windows."); // , Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(-1);
                return;
            }

            else if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                foreach (var Process in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
                    if (Process.Handle != Process.GetCurrentProcess().Handle && Process.MainWindowHandle != IntPtr.Zero)
                    {
                        // System.Windows.Forms.MessageBox.Show("FriishProduce is already running.");
                        SwitchToThisWindow(Process.MainWindowHandle, true);
                        Environment.Exit(0);
                        return;
                    }
            }

            try
            {
            }
            catch { }

            Config = new(Paths.Config);
            Logger.Log("Opening FriishProduce.");
            Lang = new Language();

            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang.Current);

            // if (args.Length > 0 && args[0].StartsWith("--"))
            // {
            //     Logger.Log("Running in CLI (command-line) mode.");
            //     CLI.Run(args);
            // }

            // else
            // {
                Logger.Log("Running in GUI (graphical) mode.");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm = new MainForm(args);
                Application.Run(MainForm);
            // }
        }

        public static void CleanTemp()
        {
            if (!Directory.Exists(Paths.WorkingFolder)) Directory.CreateDirectory(Paths.WorkingFolder);

            foreach (var item in Directory.GetFiles(Paths.WorkingFolder, "*.*", SearchOption.AllDirectories))
                if (!Path.GetFileName(item).ToLower().Contains("readme.md"))
                    try { File.Delete(item); } catch { }

            foreach (var item in Directory.GetDirectories(Paths.WorkingFolder))
                try { Directory.Delete(item, true); } catch { }

            if (File.Exists(@"C:\1541 ROM") || File.Exists(@"C:\Basic ROM") || File.Exists(@"C:\Char ROM") || File.Exists(@"C:\Kernal ROM"))
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    FileName = Paths.Tools + "frodosrc\\delete.bat",
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Minimized,
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    RedirectStandardInput = false
                };

                using (Process p = new())
                {
                    p.StartInfo = info;
                    p.Start();
                    p.WaitForExit();
                }
            }
        }
    }


    static class CLI
    {

        [DllImport("kernel32.dll")]
        internal static extern bool AllocConsole();

        /// <summary>
        /// Runs the CLI version of the app.
        /// </summary>
        /// <param name="args"></param>
        public static void Run(string[] args)
        {
            AllocConsole();

            // Usage:
            //
            // "file"
            // --patch "file"
            // --wad "file"
            // --platform nes snes smd flash
            // --img "file"
            // --sound "file"
            // --channel-title "title"
            // --savedata-title "Line 1\nLine 2"
            // --banner-title "Line 1\nLine 2"
            // --banner-year 1990
            // --banner-players 1
            // --wad-region japan usa europe korea free
            // --tid ABCD

            Console.Title = "FriishProduce v" + FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion.ToString() + " (CLI)";
            Console.WriteLine("Console version is not implemented yet.");

            string ROM = null, PATCH = null, WAD = null, IMAGE = null, SOUND = null, TID = null;
            string channelTitle = null;
            string[] bannerTitle = new string[] { };
            try
            {
                ROM = args[0];
            }
            catch { }
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].ToLower() == "--patch") PATCH = args[i + 1];
                if (args[i].ToLower() == "--wad") WAD = args[i + 1];
                if (args[i].ToLower() == "--img") IMAGE = args[i + 1];
                if (args[i].ToLower() == "--sound") SOUND = args[i + 1];
                if (args[i].ToLower() == "--tid") TID = args[i + 1];
                if (args[i].ToLower() == "--channel-title") channelTitle = args[i + 1];
            };

            Console.WriteLine($"> Input ROM: {Path.GetFileName(ROM)}");
            Console.WriteLine($"> ROM patch: {Path.GetFileName(PATCH)}");
            Console.WriteLine();
            Console.WriteLine($"> Base WAD: {Path.GetFileName(WAD)}");
            Console.WriteLine($"> Title ID: {TID}");
            Console.WriteLine($"> Channel name: {channelTitle}");
            Console.WriteLine();
            Console.WriteLine($"> Image: {Path.GetFileName(IMAGE)}");
            Console.WriteLine($"> Banner sound: {Path.GetFileName(SOUND)}");
            Console.WriteLine();
            Console.WriteLine("Is this OK?");

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
