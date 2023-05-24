using System;

namespace FriishProduce
{
    public class Bytes
    {
        public static int Search(byte[] source, byte[] pattern, int start, int end)
        {
            if (start < 0) start = 0;
            if (end > source.Length) end = source.Length;

            for (int i = start; i < end; i++)
            {
                if (source[i] != pattern[0]) continue;

                for (int x = pattern.Length - 1; x >= 1; x--)
                {
                    if (source[i + x] != pattern[x]) break;
                    if (x == 1) return i;
                }
            }
            
            return -1;
        }

        public static int Search(byte[] source, byte[] pattern) => Search(source, pattern, 0, source.Length - pattern.Length);

        public static int Search(byte[] source, string pattern, int start, int end)
        {
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            return Search(source, pBytes, start, end);
        }

        public static int Search(byte[] source, string pattern)
        {
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            return Search(source, pBytes);
        }

        public static int Search(string source, string pattern, int start, int end)
        {
            var sArray = source.Split(' ');
            var sBytes = new byte[sArray.Length];
            for (int i = 0; i < sArray.Length; i++)
                sBytes[i] = Convert.ToByte(sArray[i], 16);
            
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            return Search(sBytes, pBytes, start, end);
        }

        public static int Search(string source, string pattern)
        {
            var sArray = source.Split(' ');
            var sBytes = new byte[sArray.Length];
            for (int i = 0; i < sArray.Length; i++)
                sBytes[i] = Convert.ToByte(sArray[i], 16);
            
            var pArray = pattern.Split(' ');
            var pBytes = new byte[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
                pBytes[i] = Convert.ToByte(pArray[i], 16);

            return Search(sBytes, pBytes);
        }
    }
}
