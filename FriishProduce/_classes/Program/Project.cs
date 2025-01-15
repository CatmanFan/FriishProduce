using System;
using System.Collections.Generic;

namespace FriishProduce
{
    [Serializable]
    public class Project
    {
        public string ProjectPath { get; set; }

        public Platform Platform { get; set; }
        public string ROM { get; set; }
        public (int BaseNumber, int Region) OnlineWAD { get; set; }
        public string OfflineWAD { get; set; }
        public string Patch { get; set; }
        public (int Type, string File) Manual { get; set; }
        public (string File, System.Drawing.Bitmap Bmp) Img { get; set; }
        public string Sound { get; set; }


        public int InjectionMethod { get; set; }
        public int ForwarderStorageDevice { get; set; } = Program.Config.forwarder.root_storage_device;
        public bool IsMultifile { get; set; }
        public IDictionary<string, string> ContentOptions { get; set; }
        public (bool Enabled, IDictionary<Buttons, string> List) Keymap { get; set; }
        public int WADRegion { get; set; }
        public bool LinkSaveDataTitle { get; set; } = Program.Config.application.auto_fill_save_data;
        public (int, bool) ImageOptions { get; set; } = (Program.Config.application.image_interpolation, Program.Config.application.image_fit_aspect_ratio);
        public int VideoMode { get; set; }


        public string TitleID { get; set; }
        public string[] ChannelTitles { get; set; }
        public int BannerRegion;
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string[] SaveDataTitle { get; set; }

        internal void Dispose()
        {
            ProjectPath = null;
            Platform = 0;
            ROM = null;
            OnlineWAD = (0, 0);
            OfflineWAD = null;
            Patch = null;
            Manual = (0, null);
            Img = (null, null);
            Sound = null;

            InjectionMethod = 0;
            ForwarderStorageDevice = Program.Config.forwarder.root_storage_device;
            IsMultifile = false;
            ContentOptions = null;
            Keymap = (false, null);
            WADRegion = 0;
            LinkSaveDataTitle = Program.Config.application.auto_fill_save_data;
            ImageOptions = (Program.Config.application.image_interpolation, Program.Config.application.image_fit_aspect_ratio);
            VideoMode = 0;

            TitleID = null;
            ChannelTitles = null;
            BannerRegion = 0;
            BannerTitle = null;
            BannerYear = 1980;
            BannerPlayers = 1;
            SaveDataTitle = new string[] { "" };
        }
    }
}
