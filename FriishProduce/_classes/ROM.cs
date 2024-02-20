using System;
using System.IO;
using System.Text;

namespace FriishProduce
{
    public class ROM
    {
        private string _rom;
        public string Path
        {
            get => _rom;

            set
            {
                // -----------------------
                // Check if raw ROM exists
                // -----------------------
                if (!File.Exists(value))
                    throw new FileNotFoundException(new FileNotFoundException().Message, value);

                _rom = value;
                Bytes = File.ReadAllBytes(_rom);
            }
        }
        public byte[] Bytes { get; set; }

        public int MaxSize { get; set; }

        public ROM(string path) => Path = path;

        public bool CheckSize(int length)
        {
            if (Bytes.Length > length)
            {
                bool isMB = length >= 1048576;
                throw new Exception(string.Format(Language.Get("Error003"),
                    Math.Round((double)length / (isMB ? 1048576 : 1024)).ToString(),
                    isMB ? Language.Get("Abbreviation_Megabytes") : Language.Get("Abbreviation_Kilobytes")));
            }

            return true;
        }

        public bool CheckSize() => CheckSize(MaxSize);
    }
}
