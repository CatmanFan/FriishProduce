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
            if (string.IsNullOrWhiteSpace(Default.language))
            {
                Default.language = "sys";
                Default.Save();
            }

            var culture = new CultureInfo(Default.language == "sys" ? "en" : Default.language);
            if (Default.language == "sys")
            {
                foreach (string ISOcode in new Languages().Get().Values)
                    if (ISOcode == CultureInfo.InstalledUICulture.TwoLetterISOLanguageName)
                        culture = new CultureInfo(ISOcode);
            }
            culture.DateTimeFormat = new DateTimeFormatInfo() { DateSeparator = ".", ShortTimePattern = "HH:mm" };
            Thread.CurrentThread.CurrentUICulture = culture;

            try { System.IO.Directory.Delete(Paths.WorkingFolder, true); } catch { }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
