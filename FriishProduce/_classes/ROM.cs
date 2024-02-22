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
        public string TitleID { get => Bytes != null && _rom != null ? TID() : null; }

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
                    MessageBox.Show(Language.Get("Message008"), 0, Ookii.Dialogs.WinForms.TaskDialogIcon.Warning);
                    return false;
                }

                return true;
            }
        }

        public virtual bool CheckZIPValidity(string[] strings, bool SearchEndingOnly, bool ForceLowercase) => CheckZIPValidity(Path, strings, SearchEndingOnly, ForceLowercase);

        protected virtual string TID() { return null; }

        public bool CheckSize(int length)
        {
            if (Bytes.Length > length)
            {
                bool isMB = length >= 1048576;
                throw new Exception(string.Format(Language.Get("Error003"),
                    Math.Round((double)length / (isMB ? 1048576 : 1024), 2).ToString(),
                    isMB ? Language.Get("Abbreviation_Megabytes") : Language.Get("Abbreviation_Kilobytes")));
            }

            return true;
        }

        public bool CheckSize() => CheckSize(MaxSize);
    }
}
