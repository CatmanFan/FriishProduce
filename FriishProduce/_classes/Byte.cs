using System;
using System.Linq;
using System.Text;

namespace FriishProduce
{
    public class Byte
    {
        public static readonly byte[] Dummy = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        public static int PatternAt(byte[] source, byte[] pattern)
        {
            for (int i = 0; i < source.Length; i++)
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    return i;
            return -1;
        }

        public static int PatternAt(byte[] source, string text)
        {
            byte[] pattern = Encoding.ASCII.GetBytes(text);
            for (int i = 0; i < source.Length; i++)
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    return i;
            return -1;
        }

        public static int IndexOf(byte[] source, byte[] pattern, int start, int end)
        {
            if (start < 0) start = 0;
            if (end > source.Length) end = source.Length - pattern.Length;

            for (int i = start; i < end; i++)
            {
                if (source[i] != pattern[0]) continue;

                for (int x = pattern.Length - 1; x >= 1; x--)
                {
                    try { if (source[i + x] != pattern[x]) break; } catch { }
                    if (x == 1) return i;
                }
            }

            return -1;
        }

        public static int IndexOf(byte[] source, string pattern, int start = 0, int end = -1)
        {
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            if (end == -1) end = source.Length - pBytes.Length;
            return IndexOf(source, pBytes, start, end);
        }
    }
}
