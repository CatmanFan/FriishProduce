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
    public class WiiVCModifier
    {
        // -----------------------------------
        // Public variables
        // -----------------------------------
        public bool isKorea { get; set; }
        private Console Console { get; set; }
        public TitleImage tImg { get; set; }

        public string TitleID { get; set; }
        public string ChannelTitle { get; set; }
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string[] SaveDataTitle { get; set; }

        public WiiVCModifier(Console c) { Console = c; }

        public WAD Modify(WAD w)
        {
            // Banner metadata
            w.ChangeChannelTitles(ChannelTitle);
            Banner.EditBanner(w, Console, w.Region, BannerTitle, BannerYear, BannerPlayers);
            if (tImg.VCPic != null) tImg.ReplaceBanner(w);

            // WAD metadata
            w.ChangeTitleID(LowerTitleID.Channel, TitleID.ToUpper());
            w.FakeSign = true;

            return w;
        }

        public void ShowErrorMessage(Exception ex) => MessageBox.Show(ex.Message, Language.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
}
