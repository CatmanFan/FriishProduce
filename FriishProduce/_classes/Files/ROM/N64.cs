namespace FriishProduce
{
    public class ROM_N64 : ROM
    {
        public ROM_N64() : base() { }

        protected override void Load()
        {
        }

        public override bool CheckValidity(string path)
        {
            var data = ToBigEndian(System.IO.File.ReadAllBytes(path));
            return data[0] == 0x80 && data[1] == 0x37 && data[2] == 0x12 && data[3] == 0x40 && data[4] == 0x00 && data[5] == 0x00 && data[6] == 0x00 && data[7] == 0x0F;
        }

        public byte[] ToBigEndian(byte[] romData = null)
        {
            var ROM = romData != null ? romData : patched?.Length > 0 ? patched : origData;

            // -----------------------
            // Byteswap ROM first
            // ****************
            // Comparison of byte formats:
            // Big Endian:    SUPER MARIO 64
            // Byte Swapped:  USEP RAMIR O46 (each 2 bytes in reverse)
            // Little Endian: EPUSAM R OIR46 (each 4 bytes in reverse)
            // -----------------------

            // Byte Swapped to Big Endian
            // ****************
            if ((ROM[56] == 0x4E && ROM[57] == 0x00 && ROM[58] == 0x00 && ROM[59] == 0x00)
                || (ROM[0] == 0x40 && ROM[1] == 0x12 && ROM[2] == 0x37 && ROM[3] == 0x80))
            {
                for (int i = 0; i < ROM.Length; i += 4)
                    (ROM[i], ROM[i + 1], ROM[i + 2], ROM[i + 3]) = (ROM[i + 3], ROM[i + 2], ROM[i + 1], ROM[i]);
            }

            // Little Endian to Big Endian
            // ****************
            else if ((ROM[56] == 0x00 && ROM[57] == 0x00 && ROM[58] == 0x4E && ROM[59] == 0x00)
                || (ROM[0] == 0x37 && ROM[1] == 0x80 && ROM[2] == 0x40 && ROM[3] == 0x12))
            {
                for (int i = 0; i < ROM.Length; i += 2)
                    (ROM[i], ROM[i + 1]) = (ROM[i + 1], ROM[i]);
            }

            return ROM;
        }
    }
}
