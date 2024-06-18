using System;
using System.Collections.Generic;

namespace FriishProduce
{
    [Serializable]
    public class Project
    {
        public Platform Platform { get; set; }
        public string ROM { get; set; }
        public (int baseNumber, int region) WAD { get; set; }
        public string WADFile { get; set; }
        public string Patch { get; set; }
        public (int Type, string File) Manual { get; set; }
        public System.Drawing.Bitmap Img { get; set; }


        public int InjectionMethod { get; set; }
        public (bool, bool) ForwarderOptions { get; set; }
        public IDictionary<string, string> ContentOptions { get; set; }
        public int WADRegion { get; set; }
        public (int, int) Base { get; set; }
        public string BaseFile { get; set; }
        public bool LinkSaveDataTitle { get; set; }
        public (int, bool) ImageOptions { get; set; }


        public string TitleID { get; set; }
        public string[] ChannelTitles { get; set; }
        public int BannerRegion;
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string[] SaveDataTitle { get; set; }
    }
}
