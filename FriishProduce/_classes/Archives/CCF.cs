using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;

namespace Archives
{
    public class CCF : IDisposable
    {
        public struct FileDescriptor
        {
            public string Name { get; set; }
            public uint Offset { get; set; }
            public uint InternalSize { get; set; }
            public uint FileSize { get; set; }
            public bool Compressed { get; set; }
            private CCF Parent { get; set; }

            public FileDescriptor(CCF parent)
            {
                Parent = parent;
                Name = null;
                Offset = 0;
                InternalSize = 0;
                FileSize = 0;
                Compressed = false;
            }

            public void Parse()
            {
                if (Offset > 0 && InternalSize > 0 && FileSize > 0)
                {
                    using (var f = new MemoryStream(Parent.CCFContent))
                    {
                        f.Seek(Offset * 32, SeekOrigin.Begin);
                        byte[] buffer = new byte[InternalSize];
                        f.Read(buffer, 0, (int)InternalSize);
                    }
                }

                // ZLIB header is 0x789C
                // ***************
                Compressed = InternalSize != FileSize;
            }
        }

        private byte[] CCFContent;
        public FileDescriptor[] Files { get; private set; }

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
                    CCFContent = null;
                    Header = null;
                    Files = null;

                    isDisposed = true;
                }
            }
        }
        #endregion

        public bool ZlibExtract = true;
        public bool ZlibPack = false;

        public CCF()
        {
            CCFContent = null;
            Header = null;
            Files = null;
        }

        public CCF(string path)
        {
            if (!File.Exists(path)) return;

            CCFContent = File.ReadAllBytes(path);
            Header = null;
            Files = null;
            Parse();
        }

        public CCF(MemoryStream path)
        {
            CCFContent = path.ToArray();
            Header = null;
            Files = null;
            Parse();
        }

        public CCF(byte[] path)
        {
            CCFContent = path;
            Header = null;
            Files = null;
            Parse();
        }

        private byte[] Header;

        public byte[][] Data { get; set; }

        private byte[][] InternalData { get; set; }

        private void Parse()
        {
            if (CCFContent == null || CCFContent.Length < 32) { CCFContent = null; return; }

            // Read header
            // *******************
            uint rootnode_offset;

            rootnode_offset = BitConverter.ToUInt32(CCFContent.Skip(16).Take(4).ToArray(), 0);
            uint numFiles = BitConverter.ToUInt32(CCFContent.Skip(20).Take(4).ToArray(), 0);

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(CCFContent.Take(4).ToArray(), new byte[] { 67, 67, 70, 0 })
                || !StructuralComparisons.StructuralEqualityComparer.Equals(CCFContent.Skip(4).Take(12).ToArray(), new byte[12])
                || rootnode_offset != 0x20
                || !StructuralComparisons.StructuralEqualityComparer.Equals(CCFContent.Skip(24).Take(8).ToArray(), new byte[8]))
            {
                CCFContent = null;
                throw new Exception("Invalid file!");
            }

            Header = CCFContent.Take(32).ToArray();

            Files = new FileDescriptor[numFiles];
            Data = new byte[numFiles][];
            InternalData = new byte[Data.Length][];

            for (int i = 0; i < numFiles; i++)
            {
                Files[i] = new FileDescriptor(this)
                {
                    Name = System.Text.Encoding.ASCII.GetString(CCFContent.Skip(Header.Length + (i * 32)).Take(20).ToArray()).TrimEnd('\0'),
                    Offset = BitConverter.ToUInt32(CCFContent, Header.Length + (i * 32) + 20),
                    InternalSize = BitConverter.ToUInt32(CCFContent, Header.Length + (i * 32) + 24),
                    FileSize = BitConverter.ToUInt32(CCFContent, Header.Length + (i * 32) + 28),
                };

                Files[i].Parse();

                if (Files[i].Offset > 0 && Files[i].InternalSize > 0 && Files[i].FileSize > 0)
                {
                    using (var f = new MemoryStream(CCFContent))
                    {
                        f.Seek(Files[i].Offset * 32, SeekOrigin.Begin);
                        byte[] buffer = new byte[Files[i].InternalSize];
                        f.Read(buffer, 0, (int)Files[i].InternalSize);

                        Data[i] = new byte[buffer.Length];
                        Array.Copy(buffer, Data[i], buffer.Length);

                        InternalData[i] = new byte[buffer.Length];
                        Array.Copy(buffer, InternalData[i], buffer.Length);
                    }
                }

                // ZLIB header is 0x789C
                // ***************
                if (Files[i].Compressed)
                {
                    byte[] buffer = Ionic.Zlib.ZlibStream.UncompressBuffer(Data[i]);

                    if (!ZlibExtract)
                    {
                        using (DeflateStream c = new DeflateStream(new MemoryStream(InternalData[i].Skip(2).ToArray(), false), CompressionMode.Decompress))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            c.CopyTo(ms);
                            buffer = ms.ToArray();
                        }
                    }

                    Data[i] = new byte[buffer.Length];
                    Array.Copy(buffer, Data[i], buffer.Length);
                }
            }
        }

        public int GetNodeIndex(string name)
        {
            int index = -1;

            for (int i = 0; i < Files.Length; i++)
                if (Files[i].Name.ToLower() == name.ToLower())
                    index = i;

            return index;
        }

        public void ReplaceFile(string file, byte[] data) => ReplaceFile(Files[GetNodeIndex(file)], data);

        public void ReplaceFile(FileDescriptor file, byte[] data)
        {
            int index = 0;

            for (int i = 0; i < Files.Length; i++)
                if (file.Name == Files[i].Name) index = i;

            Data[index] = new byte[data.Length];
            Array.Copy(data, 0, Data[index], 0, data.Length);

            file.FileSize = Convert.ToUInt32(data.Length);

            uint intSize = 0;

            if (file.Compressed)
            {
                byte[] buffer = null;

                if (ZlibPack)
                {
                    buffer = Ionic.Zlib.ZlibStream.CompressBuffer(data);
                    buffer[0] = 0x78;
                    buffer[1] = 0x9C;
                }

                else
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

                        buffer = outputStream.ToArray();
                    }
                }

                intSize = Convert.ToUInt32(buffer.Length);

                // Size failsafe check
                // *******************
                if (intSize > 0xFFFFFFFF)
                {
                    throw new Exception("File is too large!");
                }

                InternalData[index] = new byte[buffer.Length];
                Array.Copy(buffer, InternalData[index], buffer.Length);
            }
            else
            {
                intSize = Convert.ToUInt32(data.Length);

                // Size failsafe check
                // *******************
                if (intSize > 0xFFFFFFFF)
                {
                    throw new Exception("File is too large!");
                }
            }

            // Fix internal size
            // *******************
            file.InternalSize = intSize & 0xFFFFFFFF;

            if (file.InternalSize > file.FileSize)
                file.InternalSize = file.FileSize;

            CCFContent = new byte[RewriteCCFContent().Length];
            RewriteCCFContent().CopyTo(CCFContent, 0);
        }

        private byte[] RewriteCCFContent()
        {
            int curpos = Files.Length + 1;
            for (int i = 0; i < Files.Length; i++)
            {
                Files[i].Offset = (uint)curpos;
                curpos += (int)Files[i].InternalSize / 32 + 1;
            }

            var Converted = new List<byte>();
            Converted.AddRange(Header);

            for (int i = 0; i < Files.Length; i++)
            {
                // New file descriptor header
                // *******************
                var FileHeader = new byte[32];
                System.Text.Encoding.ASCII.GetBytes(Files[i].Name).CopyTo(FileHeader, 0);
                BitConverter.GetBytes(Files[i].Offset).CopyTo(FileHeader, 20);
                BitConverter.GetBytes(Files[i].InternalSize).CopyTo(FileHeader, 24);
                BitConverter.GetBytes(Files[i].FileSize).CopyTo(FileHeader, 28);
                Converted.AddRange(FileHeader);
            }

            // Write new CCF file with replaced data
            // *******************
            for (int i = 0; i < Files.Length; i++)
            {
                Converted.AddRange(InternalData[i] ?? new byte[] { 0x00 });
                for (int j = 0; j < 32 - (InternalData[i]?.Length % 32); j++)
                {
                    Converted.Add(0x00);
                }
            }

            return Converted.ToArray();
        }

        public byte[] ToByteArray() => RewriteCCFContent();

        public void Save(string file) => File.WriteAllBytes(file, ToByteArray());
    }
}
