using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace FriishProduce
{
    static class Program
    {
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

            Language.Load();
            LanguageXML.Run();

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
