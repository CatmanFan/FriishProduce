using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Archives
{
    /// Conversion to C#, using reverse-engineering from the CCF Tools C source code, both by the original author (paulguy) and via libertyernie's fork: https://github.com/libertyernie/ccf-tools, and libWiiSharp's U8 method.
    /// This runs the code directly from the program instead of using a third-party app as before.

    public class CCF : IDisposable
    {
        #region ~~~ IDisposable ~~~
        private bool isDisposed = false;

        ~CCF()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!isDisposed)
            {
                if (dispose)
                {
                    ccfNodes = null;
                    data = null;
                    chunkSize = 0;

                    isDisposed = true;
                }
            }
        }
        #endregion

        private byte[] magic = new byte[] { 0x43, 0x43, 0x46, 0x00 };
        private uint chunkSize = 0;

        private List<CCF_Node> ccfNodes = new List<CCF_Node>();
        private List<byte[]> data = new List<byte[]>();

        public List<CCF_Node> Nodes { get { return ccfNodes; } }
        public byte[][] Data { get { return data.ToArray(); } }

        public CCF()
        {
            data = new List<byte[]>();
        }

        /// <summary>
        /// Loads CCF data from an external file.
        /// </summary>
        public static CCF Load(string input)
        {
            try
            {
                return Load(File.ReadAllBytes(input));
            }

            catch (FileNotFoundException)
            {
                throw new Exception("File not found!");
            }
        }

        /// <summary>
        /// Loads CCF data from a byte array.
        /// </summary>
        public static CCF Load(byte[] input)
        {
            CCF c = new CCF();
            MemoryStream ms = new MemoryStream(input);

            try { c.parse(ms); }
            catch { ms.Dispose(); throw; }

            return c;
        }

        /// <summary>
        /// Returns the index of the file node with the given name, or -1 if no file is found.
        /// </summary>
        public int GetNodeIndex(string name)
        {
            int index = -1;

            for (int i = 0; i < ccfNodes.Count; i++)
                if (ccfNodes[i].Name == name)
                    index = i;

            return index;
        }

        /// <summary>
        /// Adds a new file node to the archive.
        /// </summary>
        public void AddFile(string name, byte[] fileData, bool compress)
        {
            ccfNodes.Add(new CCF_Node(name, compress ? this.compress(fileData).Length : fileData.Length, fileData.Length));
            data.Add(fileData);
        }

        /// <summary>
        /// Imports an external file to the archive.
        /// </summary>
        public void AddFile(string fileName, bool compress)
        {
            if (!File.Exists(fileName)) throw new Exception("File not found!");

            var fileData = File.ReadAllBytes(fileName);
            ccfNodes.Add(new CCF_Node(Path.GetFileName(fileName), compress ? this.compress(fileData).Length : fileData.Length, fileData.Length));
            data.Add(fileData);
        }

        /// <summary>
        /// Removes a file node from the archive.
        /// </summary>
        public void Remove(CCF_Node node)
        {
            // Failsafe
            // *******************
            try { int index = ccfNodes.IndexOf(node); } catch { return; }

            data.RemoveAt(ccfNodes.IndexOf(node));
            ccfNodes.Remove(node);
        }

        /// <summary>
        /// Replaces the given file.
        /// </summary>
        public void ReplaceFile(CCF_Node node, byte[] newData)
        {
            data[ccfNodes.IndexOf(node)] = newData;
        }

        /// <summary>
        /// Replaces the file with the given index.
        /// </summary>
        public void ReplaceFile(int fileIndex, byte[] newData)
        {
            data[fileIndex] = newData;
        }

        /// <summary>
        /// Extracts the file node to disk.
        /// </summary>
        public void Extract(CCF_Node node, string outPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outPath))) Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllBytes(outPath, data[ccfNodes.IndexOf(node)]);
        }

        /// <summary>
        /// Extracts the file with the given index to disk.
        /// </summary>
        public void Extract(int fileIndex, string outPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(outPath))) Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllBytes(outPath, data[fileIndex]);
        }

        /// <summary>
        /// Returns the CCF file as a byte array.
        /// </summary>
        public byte[] ToByteArray()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                write(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Saves the CCF file.
        /// </summary>
        public void Save(string outputFile)
        {
            using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                write(fs);
            }
        }

        #region Private Methods
        private byte[] compress(byte[] data)
        {
            using (MemoryStream inputStream = new MemoryStream(data))
            using (MemoryStream outputStream = new MemoryStream())
            {
                outputStream.WriteByte(0x78);
                outputStream.WriteByte(0x9C);

                using (DeflateStream compressor = new DeflateStream(outputStream, CompressionMode.Compress, true))
                {
                    inputStream.CopyTo(compressor);
                }

                // Adler32 Checksum
                // *******************
                uint a = 1, b = 0;
                for (int ptr = 0; ptr < data.Length; ptr++)
                {
                    a = (a + data[ptr]) % 65521;
                    b = (b + a) % 65521;
                }

                uint checksum = (b << 16) | a;

                // Write checksum
                // *******************
                outputStream.WriteByte((byte)((checksum & 0xFF000000) >> 24));
                outputStream.WriteByte((byte)((checksum & 0x00FF0000) >> 16));
                outputStream.WriteByte((byte)((checksum & 0x0000FF00) >> 8));
                outputStream.WriteByte((byte)((checksum & 0x000000FF) >> 0));

                return outputStream.ToArray();
            }
        }

        private void write(Stream writeStream)
        {
            writeStream.Write(magic, 0, 4);
            writeStream.Write(new byte[12], 0, 12);
            writeStream.Write(BitConverter.GetBytes(chunkSize), 0, 4);
            writeStream.Write(BitConverter.GetBytes(ccfNodes.Count), 0, 4);
            writeStream.Write(new byte[8], 0, 8);

            List<byte[]> newDatas = new List<byte[]>();
            List<uint> offsets = new List<uint>();
            int curpos = ccfNodes.Count + 1;

            for (int i = 0; i < ccfNodes.Count; i++)
            {
                // Parse filedata and compress it if needed
                // *******************
                byte[] converted = new byte[1];
                converted = data[i];

                if (ccfNodes[i].Compressed)
                {
                    converted = compress(converted);
                }

                byte[] written = new byte[converted.Length + 32 - (converted.Length % 32)];
                converted.CopyTo(written, 0);

                newDatas.Add(written);

                // Write file header
                // *******************
                byte[] name = new byte[20];
                Array.Copy(Encoding.ASCII.GetBytes(ccfNodes[i].Name), name, Encoding.ASCII.GetBytes(ccfNodes[i].Name).Length);
                writeStream.Write(name, 0, 20);
                writeStream.Write(BitConverter.GetBytes(curpos), 0, 4);
                writeStream.Write(BitConverter.GetBytes(converted.Length), 0, 4);
                writeStream.Write(BitConverter.GetBytes(data[i].Length), 0, 4);
                curpos += written.Length / 32;
            }

            foreach (var item in newDatas)
            {
                writeStream.Write(item, 0, item.Length);
            }
        }

        private void parse(MemoryStream input)
        {
            if (input == null || input.Length < 32) { throw new Exception("Not A CCF!"); }

            input.Position = 0;

            byte[] fileMagic = new byte[4];
            byte[] padding0 = new byte[12];
            byte[] chunk = new byte[4];
            byte[] numFiles = new byte[4];
            byte[] padding1 = new byte[8];

            input.Read(fileMagic, 0, 4);
            input.Read(padding0, 0, 12);
            input.Read(chunk, 0, 4);
            input.Read(numFiles, 0, 4);
            input.Read(padding1, 0, 8);

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(fileMagic, magic)
                || !StructuralComparisons.StructuralEqualityComparer.Equals(padding0, new byte[12])
                || BitConverter.ToUInt32(chunk, 0) != 0x20
                || !StructuralComparisons.StructuralEqualityComparer.Equals(padding1, new byte[8]))
            { throw new Exception("Not A CCF!"); }

            chunkSize = BitConverter.ToUInt32(chunk, 0);

            byte[][] nodeOffsets = new byte[BitConverter.ToUInt32(numFiles, 0)][];

            for (int i = 0; i < BitConverter.ToUInt32(numFiles, 0); i++)
            {
                byte[] nodeName = new byte[20];
                nodeOffsets[i] = new byte[4];
                byte[] nodeIntData = new byte[4];
                byte[] nodeExtData = new byte[4];

                input.Read(nodeName, 0, 20);
                input.Read(nodeOffsets[i], 0, 4);
                input.Read(nodeIntData, 0, 4);
                input.Read(nodeExtData, 0, 4);

                ccfNodes.Add(new CCF_Node(Encoding.ASCII.GetString(nodeName).TrimEnd('\0'), BitConverter.ToUInt32(nodeIntData, 0), BitConverter.ToUInt32(nodeExtData, 0)));
            }

            for (int i = 0; i < BitConverter.ToUInt32(numFiles, 0); i++)
            {
                input.Seek(BitConverter.ToUInt32(nodeOffsets[i], 0) * chunkSize, SeekOrigin.Begin);
                byte[] nodeData = new byte[ccfNodes[i].DataSize];
                input.Read(nodeData, 0, (int)ccfNodes[i].DataSize);

                if (ccfNodes[i].Compressed)
                {
                    data.Add(Ionic.Zlib.ZlibStream.UncompressBuffer(nodeData));
                }

                else
                {
                    data.Add(nodeData);
                }
            }
        }
        #endregion
    }

    public class CCF_Node
    {
        private string name;
        private uint dataSize;
        private uint fileSize;

        public string Name { get { return name; } set { name = value.Substring(0, Math.Max(value.Length, 20)); } }
        public uint DataSize { get { return dataSize; } }
        public uint FileSize { get { return fileSize; } }
        public bool Compressed { get => dataSize != fileSize; }

        public CCF_Node(string name, int intdata, int extdata)
        {
            this.name = name;
            dataSize = Convert.ToUInt32(intdata);
            fileSize = Convert.ToUInt32(extdata);
        }

        public CCF_Node(string name, uint intdata, uint extdata)
        {
            this.name = name;
            dataSize = intdata;
            fileSize = extdata;
        }
    }
}
