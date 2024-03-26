using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    [Serializable]
    public class ProjectType
    {
        public Console Console { get; set; }

        public ROM ROM { get; set; }
        public string PatchFile { get; set; }
        public ImageHelper Img { get; set; }
        public Creator Creator { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }
}
