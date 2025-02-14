using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using SwfDotNet;
using SwfDotNet.IO;

namespace FriishProduce
{
    public class SWF : ROM
    {
        public enum Compress
        {
            None,
            Zlib,
            Lzma
        }
        public Compress? Compression { get; private set; }
        public SwfHeader Header { get; private set; }

        public SWF() : base()
        {
            Compression = null;
            Header = null;
        }

        public override bool CheckValidity(string path)
        {
            var data = File.ReadAllBytes(path);
            if (data.Length < 3) return false;

            return System.Text.Encoding.ASCII.GetString(data.Take(3).ToArray()) is "FWS" or "CWS" or "ZWS";
        }

        public void Parse(string file = null)
        {
            byte[] data = origData == null || origData?.Length < 8 ? File.ReadAllBytes(file) : origData;

            // Get signature type
            // *******************
            byte[] sigBytes = data.Take(3).ToArray();
            if (sigBytes[0] == 'F')
                Compression = Compress.None;
            if (sigBytes[0] == 'C')
                Compression = Compress.Zlib;
            if (sigBytes[0] == 'Z')
                Compression = Compress.Lzma;

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