using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
