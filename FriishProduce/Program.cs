using System;
using System.Windows.Forms;

namespace FriishProduce
{
    static class Program
    {
        public static Lang Language = new Lang();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try { System.IO.Directory.Delete(Paths.WorkingFolder, true); } catch { }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
