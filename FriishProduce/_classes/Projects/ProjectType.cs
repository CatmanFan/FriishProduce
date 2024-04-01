using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    [Serializable]
    public class ProjectType
    {
        public Console Console { get; set; }

        public string ROM { get; set; }
        public string PatchFile { get; set; }
        public Bitmap Img { get; set; }
        public Creator Creator { get; set; }
        public IDictionary<string, string> Options { get; set; }
        public LibRetroDB LibRetro { get; set; }
        public int WADRegion { get; set; }
        public int BaseNumber { get; set; }
        public int BaseRegion { get; set; }
        public bool LinkSaveDataTitle { get; set; }
    }
}
