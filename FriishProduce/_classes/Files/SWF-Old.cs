using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace FriishProduce
{
    public class SWFx : ROM
    {
        public SWF() : base()
        {
            Compressed = false;
            Signature = null;
            Version = null;
            FileSize = 0;
            FrameSize = new();
            FrameRate = null;
            FrameCount = null;
        }

        public bool Compressed { get; private set; }
        public string Signature { get; private set; }
        public uint? Version { get; private set; }
        public int FileSize { get; private set; }
        public struct frameSize
        {
            public int X_Min { get; set; }
            public int X_Max { get; set; }
            public int Y_Min { get; set; }
            public int Y_Max { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
        public frameSize FrameSize { get; private set; }
        public double? FrameRate { get; private set; }
        public ushort? FrameCount { get; private set; }

        public override bool CheckValidity(string path)
        {
            var data = File.ReadAllBytes(path);
            if (data.Length < 3) return false;

            byte[] sigBytes = new byte[] { data[0], data[1], data[2] };
            string sig = System.Text.Encoding.ASCII.GetString(sigBytes);

            return sig is "FWS" or "CWS" or "ZWS";
        }

        public void Header(string path)
        {
            bool needClose = false;
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            if (needClose)
                stream.Seek(0, SeekOrigin.Begin);

            // Read the 3-byte signature field
            byte[] sigBytes = new byte[3];
            stream.Read(sigBytes, 0, 3);
            Signature = System.Text.Encoding.ASCII.GetString(sigBytes);

            // Compression
            Compressed = Signature.StartsWith("C");

            // Version
            Version = ReadUI8(stream);

            // File size (stored as a 32-bit integer)
            byte[] sizeBytes = new byte[4];
            stream.Read(sizeBytes, 0, 4);
            FileSize = BitConverter.ToInt32(bytes, 0);

            // Payload
            byte[] buffer = new byte[FileSize];
            stream.Read(buffer, 0, FileSize);
            if (Compressed)
            {
                // Unpack the zlib compression
                buffer = Decompress(buffer);
            }

            // Containing rectangle (struct RECT)
            int nbits = ReadUI8(buffer[0]) >> 3;

            byte currentByte = buffer[0];
            buffer = buffer.Skip(1).ToArray();
            int bitCursor = 5;

            Dictionary<string, int> XY = new();
            XY.Add("xmin", 0);
            XY.Add("xmax", 0);
            XY.Add("ymin", 0);
            XY.Add("ymax", 0);

            foreach (var item in XY.Keys)
            {
                int value = 0;
                for (int valueBit = nbits - 1; valueBit >= 0; valueBit--)
                {
                    if ((currentByte << bitCursor & 0x80) != 0)
                    {
                        value |= 1 << valueBit;
                    }
                    // Advance the bit cursor to the next bit
                    bitCursor++;

                    if (bitCursor > 7)
                    {
                        // We've exhausted the current byte, consume the next one
                        currentByte = buffer[0];
                        buffer = buffer.Skip(1).ToArray();
                        bitCursor = 0;
                    }
                }

                // Convert value from TWIPS to a pixel value
                XY[item] = value / 20.0;
            }

            FrameSize = new()
            {
                X_Min = XY["xmin"],
                X_Max = XY["xmax"],
                Y_Min = XY["ymin"],
                Y_Max = XY["xmax"],
                Width = XY["xmax"] - XY["xmin"],
                Height = XY["ymax"] - XY["ymin"]
            };

            // FPS field
            header["fps"] = ReadUI16(buffer[0..2]) >> 8;
            header["frames"] = ReadUI16(buffer[2..4]);

            if (needClose)
            {
                stream.Close();
            }
            return header;
        }

        #region Private methods
        private static byte ReadUI8(Stream stream)
        {
            return (byte)stream.ReadByte();
        }

        private static ushort ReadUI16(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes, 0);
        }

        private static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var decompressedStream = new MemoryStream())
            {
                using (var decompressor = new GZipStream(compressedStream, CompressionMode.Decompress))
                    decompressor.CopyTo(decompressedStream);

                return decompressedStream.ToArray();
            }
        }
        #endregion

        public void GetHeader(string path)
        {
            using var f = new FileStream(path, FileMode.Open, FileAccess.Read);

            byte[] firstPart = new byte[9];
            f.Read(firstPart, 0, 9);

            IsZLIB = firstPart[0] == 0x5A;
            Signature = (firstPart[0], firstPart[1], firstPart[2]);
            Version = firstPart[3];
            FileLength = (int)BitConverter.ToUInt32(firstPart, 4);

            var neededByte = firstPart[8];
            int nBits = (neededByte & 0b11111000) >> 3;
            int extraBits = neededByte & 0b00000111;
            int secondPartLength = (int)Math.Ceiling((nBits * 4 - 3) / 8.0);
            byte[] secondPart = new byte[secondPartLength];
            f.Read(secondPart, 0, secondPartLength);

            string binary = Convert.ToString(extraBits, 2).PadLeft(3, '0') +
                            string.Join("", secondPart.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
            int xMin = Convert.ToInt32(binary.Substring(0 * nBits, nBits), 2);
            int xMax = Convert.ToInt32(binary.Substring(1 * nBits, nBits), 2);
            int yMin = Convert.ToInt32(binary.Substring(2 * nBits, nBits), 2);
            int yMax = Convert.ToInt32(binary.Substring(3 * nBits, nBits), 2);
            FrameSize = new()
            {
                X_Min = xMin,
                X_Max = xMax,
                Y_Min = yMin,
                Y_Max = yMax,
                Width = (xMax - xMin) / 20,
                Height = (yMax - yMin) / 20
            };

            byte[] thirdPart = new byte[4];
            f.Read(thirdPart, 0, 4);
            f.Close();
            f.Dispose();
            byte rateDecPart = thirdPart[0];
            byte rateIntPart = thirdPart[1];
            FrameCount = BitConverter.ToUInt16(thirdPart, 2);
            FrameRate = double.Parse($"{rateIntPart}.{rateDecPart}");
        }
    }
}
