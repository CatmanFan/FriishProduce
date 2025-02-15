using System.IO;
using System.Linq;
using SwfDotNet.IO;

namespace FriishProduce
{
    public class SWF : ROM
    {
        public SwfHeader Header { get; private set; }

        public SWF() : base()
        {
            Header = null;
        }

        public override bool CheckValidity(string path)
        {
            var data = File.ReadAllBytes(path);
            if (data.Length < 3) return false;

            return System.Text.Encoding.ASCII.GetString(data.Take(3).ToArray()) is "FWS" or "CWS" or "ZWS";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public void Parse(string file = null)
        {
            byte[] data = origData == null || origData?.Length < 8 ? File.ReadAllBytes(file) : origData;
            if (data.Length < 8) return;

            // Get header
            // *******************
            using (MemoryStream ms = new(data))
            {
                SwfReader swf = new(ms);
                Header = swf.ReadSwfHeader();
            }
        }
    }
}