using System.Text;

namespace FriishProduce
{
    public class ROM_N64 : ROM
    {
        public ROM_N64() : base() { }

        protected override void Load()
        {
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
            if ((Bytes[56] == 0x4E && Bytes[57] == 0x00 && Bytes[58] == 0x00 && Bytes[59] == 0x00)
                || (Bytes[0] == 0x40 && Bytes[1] == 0x12 && Bytes[2] == 0x37 && Bytes[3] == 0x80))
            {
                for (int i = 0; i < Bytes.Length; i += 4)
                    (Bytes[i], Bytes[i + 1], Bytes[i + 2], Bytes[i + 3]) = (Bytes[i + 3], Bytes[i + 2], Bytes[i + 1], Bytes[i]);
            }

            // Little Endian to Big Endian
            // ****************
            else if ((Bytes[56] == 0x00 && Bytes[57] == 0x00 && Bytes[58] == 0x4E && Bytes[59] == 0x00)
                || (Bytes[0] == 0x37 && Bytes[1] == 0x80 && Bytes[2] == 0x40 && Bytes[3] == 0x12))
            {
                for (int i = 0; i < Bytes.Length; i += 2)
                    (Bytes[i], Bytes[i + 1]) = (Bytes[i + 1], Bytes[i]);
            }
        }

        protected override string TID()
        {
            try
            {
                return Encoding.ASCII.GetString(new byte[] { Bytes[0x3B], Bytes[0x3C], Bytes[0x3D], Bytes[0x3E] }).Trim();
            }

            catch { return null; }
        }
    }
}
