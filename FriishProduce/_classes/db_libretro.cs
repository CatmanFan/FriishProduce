using System;
using System.IO;
using System.IO.Hashing;
using System.Net;
using System.Threading.Tasks;

namespace FriishProduce
{
    public class DBEntry
    {
        private string Title;
        private string Year;
        private string ImgURL;

        public DBEntry()
        {
            Title = null;
            Year = null;
            ImgURL = null;
        }

        public string GetYear() => Year;

        public string GetTitle() => Title;

        public string GetImgURL() => ImgURL;

        public void Get(string gamePath)
        {
            var crc = new Crc32();
            using (var file = File.OpenRead(gamePath))
                crc.Append(file);

            var hash_array = crc.GetCurrentHash();
            Array.Reverse(hash_array);
            string hash = BitConverter.ToString(hash_array).Replace("-", "").ToLower();

            string db_base = "https://github.com/libretro/libretro-database/raw/master/metadat/releaseyear/";
            string[] db_platforms =
            {
                "Nintendo - Nintendo Entertainment System",
                "Nintendo - Super Nintendo Entertainment System",
                "Nintendo - Nintendo 64"
            };

            try
            {
                foreach (var item in db_platforms)
                {
                    string db = db_base + Uri.EscapeUriString(item) + ".dat";

                    WebClient c = new WebClient();
                    var db_txt = c.DownloadString(db);
                    c.Dispose();

                    var db_lines = db_txt.Split(Environment.NewLine.ToCharArray());

                    for (int i = 5; i < db_lines.Length; i++)
                        if (db_lines[i].ToLower().Contains(hash))
                        {
                            Title = db_lines[i - 2].Replace("\t", "").Replace("comment \"", "").Replace("\"", "");
                            Year = db_lines[i - 1].Trim().Replace("releaseyear \"", "").Replace("\"", "");
                            ImgURL = "https://github.com/libretro-thumbnails/" + item.Replace(" ", "_") + "/raw/master/Named_Titles/" + Uri.EscapeUriString(Title) + ".png";
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Program.Language.Get("error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            System.Media.SystemSounds.Beep.Play();
        }
    }
}
