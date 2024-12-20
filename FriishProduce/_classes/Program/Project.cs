﻿using System;
using System.Collections.Generic;

namespace FriishProduce
{
    [Serializable]
    public class Project
    {
        public string ProjectPath { get; set; }

        public Platform Platform { get; set; }
        public string ROM { get; set; }
        public (int baseNumber, int region) WAD { get; set; }
        public string WADFile { get; set; }
        public string Patch { get; set; }
        public (int Type, string File) Manual { get; set; }
        public (string File, System.Drawing.Bitmap Bmp) Img { get; set; }
        public string Sound { get; set; }


        public int InjectionMethod { get; set; }
        /// <summary>
        /// 0 = SD card, 1 = USB
        /// </summary>
        public int ForwarderStorageDevice { get; set; }
        public bool IsMultifile { get; set; }
        public IDictionary<string, string> ContentOptions { get; set; }
        public (bool Enabled, IDictionary<Buttons, string> List) Keymap { get; set; }
        public int WADRegion { get; set; }
        public (bool Enabled, int Index, int Region) BaseOnline { get; set; }
        public string BaseFile { get; set; }
        public bool LinkSaveDataTitle { get; set; }
        public (int, bool) ImageOptions { get; set; }
        public int VideoMode { get; set; }


        public string TitleID { get; set; }
        public string[] ChannelTitles { get; set; }
        public int BannerRegion;
        public string BannerTitle { get; set; }
        public int BannerYear { get; set; }
        public int BannerPlayers { get; set; }
        public string[] SaveDataTitle { get; set; }
    }
}
