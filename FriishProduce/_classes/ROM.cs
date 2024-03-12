using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace FriishProduce
{
    public abstract class ROM
    {
        private string _rom;
        public string Path
        {
            get => _rom;

            set
            {
                if (value != null)
                {
                    // -----------------------
                    // Check if raw ROM exists
                    // -----------------------
                    if (!File.Exists(value))
                        throw new FileNotFoundException(new FileNotFoundException().Message, value);

                    _rom = value;

                    if (System.IO.Path.GetExtension(value).ToLower() == ".zip") try { ZIP = new ZipFile(value); } catch { ZIP = null; }
                    if (ZIP == null) Bytes = File.ReadAllBytes(value);
                    IsZIPFormat = ZIP != null;
                    Load();
                }
            }
        }
        public byte[] Bytes { get; set; }

        public int MaxSize { get; set; }

        protected bool IsZIPFormat { get; set; }
        protected ZipFile ZIP { get; set; }

        public ROM()
        {
            _rom = null;
            Bytes = null;
            ZIP = null;
        }

        protected virtual void Load() { }

        public virtual bool CheckValidity(byte[] ROM)
        {
            return true;
        }

        public virtual bool CheckZIPValidity(string path, string[] strings, bool SearchEndingOnly, bool ForceLowercase)
        {
            using (ZipFile ZIP = ZipFile.Read(path))
            {
                int applicable = 0;

                foreach (var item in ZIP.Entries)
                    foreach (string line in strings)
                    {
                        string name = ForceLowercase ? item.FileName.ToLower() : item.FileName;
                        if ((SearchEndingOnly && (name.EndsWith(line) || System.IO.Path.GetFileNameWithoutExtension(name).EndsWith(line)))
                            || (!SearchEndingOnly && name.Contains(line)))
                            applicable++;
                    }

                if (applicable < strings.Length)
                {
                    return false;
                }

                return true;
            }
        }

        public virtual bool CheckZIPValidity(string[] strings, bool SearchEndingOnly, bool ForceLowercase) => CheckZIPValidity(Path, strings, SearchEndingOnly, ForceLowercase);

        public bool CheckSize(int length)
        {
            if (Bytes.Length > length)
            {
                bool isMB = length >= 1048576;
                throw new Exception(string.Format(Language.Get("Error.003"),
                    Math.Round((double)length / (isMB ? 1048576 : 1024), 2).ToString(),
                    isMB ? Language.Get("Abbr.Megabytes") : Language.Get("Abbr.Kilobytes")));
            }

            return true;
        }

        public bool CheckSize() => CheckSize(MaxSize);

        public void Patch(string PatchFile)
        {
            ProcessHelper.Run("xdelta.exe", $"-d -s \"{Path}\" \"{PatchFile}\" \"{Paths.WorkingFolder + "patched.rom"}\"");
            if (!File.Exists(Paths.WorkingFolder + "patched.rom")) throw new Exception(Language.Get("Error.007"));
        }
    }
}
