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
        private readonly Language Strings = Program.Language;

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

            MessageBox.Show(string.Format(Strings.Get("m003"), outputFile), Strings.Get("g000"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void RemoveManual()
        {
            U8 Content4 = new U8();
            Content4.LoadFile(WAD.Contents[4]);

            int start = -1;
            int end = -1;

            for (int i = 0; i < Content4.NumOfNodes; i++)
            {
                if (Content4.StringTable[i].ToLower() == "homebutton2") start = i + 1;
                else if (Content4.StringTable[i].ToLower() == "homebutton3") end = i;
            }

            try
            {
                if (start == 0 && end == 0) throw new InvalidOperationException();
                else
                {
                    for (int i = start; i < end; i++)
                        Content4.ReplaceFile(i + end, Content4.Data[i]);

                    WADKit.ReplaceContent(WAD, 4, Content4.ToByteArray());
                }
            }
            catch
            {
                Content4.Dispose();
            }
        }

        public void ShowErrorMessage(Exception ex) => MessageBox.Show(ex.Message, Strings.Get("error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
}
