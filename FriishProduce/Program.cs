using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    static class Program
    {
        public static Language Language = new Language();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.CurrentCulture = new CultureInfo(Language.LangInfo()[0]);
            CultureInfo.CurrentUICulture = CultureInfo.CurrentUICulture;

            // try { System.IO.Directory.Delete(Paths.WorkingFolder, true); } catch { }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
