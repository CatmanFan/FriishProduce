using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Archives
{
    public class CCF
    {
        private static Stream file;
        private static FileDescriptor[] files;

        public CCF()
        {
            file = null;
            files = null;
        }

        public static CCF Read(string path)
        {
            CCF c = new CCF();
            c.Read(new FileStream(path, FileMode.Open, FileAccess.Read));
            return c;
        }

        public static CCF Read(byte[] path)
        {
            CCF c = new CCF();
            c.Read(new MemoryStream(path));
            return c;
        }

        private void Read(Stream stream)
        {
            file = stream;

            // Read header
            // *******************
            byte[] magic = new byte[4];
            byte[] zeroes1 = new byte[12];
            byte[] rootnode = new byte[4];
            byte[] numfiles_array = new byte[4];
            uint rootnode_offset;
            uint numfiles;
            byte[] zeroes2 = new byte[8];

            file.Read(magic, 0, 4);
            file.Read(zeroes1, 0, 12);
            file.Read(rootnode, 0, 4);
            file.Read(numfiles_array, 0, 4);
            rootnode_offset = BitConverter.ToUInt32(rootnode, 0);
            numfiles = BitConverter.ToUInt32(numfiles_array, 0);
            file.Read(zeroes2, 0, 8);

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(magic, new byte[] { 67, 67, 70, 0 })
                || !StructuralComparisons.StructuralEqualityComparer.Equals(zeroes1, new byte[12])
                || rootnode_offset != 0x20
                || !StructuralComparisons.StructuralEqualityComparer.Equals(zeroes2, new byte[8]))
            {
                throw new Exception("Invalid file!");
            }

            files = new FileDescriptor[numfiles];

            for (int i = 0; i < numfiles; i++)
            {
                files[i] = new FileDescriptor(file);
            }
        }

        public bool Contains(string path)
        {
            foreach (FileDescriptor f in files)
            {
                if (f.Name == path)
                {
                    return true;
                }
            }

            return false;
        }

        public MemoryStream GetFile(string path)
        {
            foreach (FileDescriptor f in files)
                if (f.Name == path)
                    return GetFileStream(f);

            return null;
        }

        private MemoryStream GetFileStream(FileDescriptor fd)
        {
            file.Seek(fd.DataOffset * 32, SeekOrigin.Begin);
            byte[] buffer = new byte[fd.Size];
            file.Read(buffer, 0, (int)fd.Size);

            if (fd.Compressed)
            {
                buffer = Decompress(buffer, (int)fd.DecompressedSize);
            }

            return new MemoryStream(buffer);
        }

        private byte[] Decompress(byte[] data, int decompressedSize)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress))
                {
                    byte[] buffer = new byte[decompressedSize];
                    ds.Read(buffer, 0, decompressedSize);
                    return buffer;
                }
            }
        }

        public MemoryStream Find(string name)
        {
            foreach (FileDescriptor fd in files)
                if (name.StartsWith(fd.Name.Trim()) || fd.Name.StartsWith(name.Trim()))
                    return GetFileStream(fd);

            return null;
        }
    }

    internal class FileDescriptor
    {
        public string Name { get; private set; }
        public uint DataOffset { get; private set; }
        public uint Size { get; private set; }
        public uint DecompressedSize { get; private set; }
        public bool Compressed { get; private set; }

        public FileDescriptor(Stream f)
        {
            byte[] nameBytes = new byte[20];
            byte[] dataOffsetBytes = new byte[4];
            byte[] sizeBytes = new byte[4];
            byte[] decompressedSizeBytes = new byte[4];

            f.Read(nameBytes, 0, 20);
            f.Read(dataOffsetBytes, 0, 4);
            f.Read(sizeBytes, 0, 4);
            f.Read(decompressedSizeBytes, 0, 4);

            Name = System.Text.Encoding.ASCII.GetString(nameBytes).TrimEnd('\0');
            DataOffset = BitConverter.ToUInt32(dataOffsetBytes, 0);
            Size = BitConverter.ToUInt32(sizeBytes, 0);
            DecompressedSize = BitConverter.ToUInt32(decompressedSizeBytes, 0);
            Compressed = Size != DecompressedSize;
        }
    }
}
