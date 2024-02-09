using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriishProduce
{
    public class Injector
    {
        // -----------------------------------
        // Public variables
        // -----------------------------------
        public WAD WAD { get; set; }
        public bool isKorea { get; set; }
        public Console Console { get; set; }
        public TitleImage tImg { get; set; }

        public string TitleID { get; set; }
        public string ChannelTitle { get; set; }
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string SaveDataTitle { get; set; }
        public string ROM { get; set; }

        public void Create(string outputFile)
        {
            // Banner metadata
            WAD.ChangeChannelTitles(ChannelTitle);
            Banner.EditBanner(WAD, Console, WAD.Region, BannerTitle, BannerYear, BannerPlayers);
            if (tImg.VCPic != null) tImg.ReplaceBanner(WAD);

            // WAD metadata
            WAD.ChangeTitleID(LowerTitleID.Channel, TitleID.ToUpper());
            WAD.FakeSign = true;

            // Save WAD
            WAD.Save(outputFile);
            WAD.Dispose();
            if (File.Exists(outputFile) && File.ReadAllBytes(outputFile).Length < 10) throw new IOException();
        }

        public void ShowErrorMessage(Exception ex) => MessageBox.Show(ex.Message, Language.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
}
