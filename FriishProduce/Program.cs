using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using static FriishProduce.Properties.Settings;

namespace FriishProduce
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.language))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
                Default.language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
