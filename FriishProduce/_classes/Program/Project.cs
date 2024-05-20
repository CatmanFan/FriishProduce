using System;
using System.Collections.Generic;
using System.Drawing;

namespace FriishProduce
{
    [Serializable]
    public class Project
    {
        public Console Console { get; set; }

        public string ROM { get; set; }
        public string PatchFile { get; set; }
        public int ManualIndex { get; set; }
        public string ManualFile { get; set; }
        public Bitmap Img { get; set; }
        public Creator Creator { get; set; }
        public int InjectionMethod { get; set; }
        public (bool, bool) ForwarderOptions { get; set; }
        public IDictionary<string, string> Options { get; set; }
        public GameDatabase GameData { get; set; }
        public int WADRegion { get; set; }
        public (int, int) Base { get; set; }
        public string BaseFile { get; set; }
        public bool LinkSaveDataTitle { get; set; }
        public (int, bool) ImageOptions { get; set; }
    }
}
