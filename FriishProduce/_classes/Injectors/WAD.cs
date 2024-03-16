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
    public class Creator
    {
        // -----------------------------------
        // Public variables
        // -----------------------------------
        public bool isJapan { get; set; }
        public bool isKorea { get; set; }
        private Console Console { get; set; }

        public string TitleID { get; set; }
        public string[] ChannelTitles { get; set; }
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string[] SaveDataTitle { get; set; }
        public string Out { get; set; }

        public Creator(Console c) { Console = c; }

        public void MakeWAD(WAD w, ImageHelper Img)
        {
            // Banner metadata
            BannerHelper.Modify(w, Console, w.Region, BannerTitle, BannerYear, BannerPlayers);
            w.ChangeChannelTitles(ChannelTitles);
            if (Img.VCPic != null) Img.ReplaceBanner(w);

            // WAD metadata
            w.ChangeTitleID(LowerTitleID.Channel, TitleID.ToUpper());
            w.FakeSign = true;

            w.Save(Out);
        }
    }
}
