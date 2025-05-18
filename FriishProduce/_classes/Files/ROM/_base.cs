using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FriishProduce
{
    [Serializable]
    public abstract class ROM
    {
        private string _rom;
        public string FilePath
        {
            get => _rom;

            set
            {
                if (value != null)
                {
                    try
                    {
                        // -----------------------
                        // Check if raw ROM exists
                        // -----------------------
                        if (!File.Exists(value))
                            throw new FileNotFoundException(new FileNotFoundException().Message, value);

                        _rom = value;

                        if (Path.GetExtension(value).ToLower() == ".zip") try { ZIP = new ZipFile(value); } catch { ZIP = null; }
                        if (ZIP == null) origData = File.ReadAllBytes(value);
                        Load();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                        _rom = null;
                        origData = null;
                        ZIP = null;
                    }
                }

                else
                {
                    _rom = null;
                    origData = null;
                    ZIP = null;
                }
            }
        }

        protected byte[] origData = new byte[0];
        protected byte[] patched = new byte[0];

        public byte[] Bytes { get => patched?.Length > 0 ? patched : origData; }

        public int MaxSize { get; set; }

        protected ZipFile ZIP { get; set; }

        public ROM()
        {
            _rom = null;
            origData = null;
            patched = null;
            ZIP = null;
            MaxSize = -1;
        }

        protected virtual void Load() { }

        public virtual bool CheckValidity(string path)
        {
            return true;
        }

        private Encoding[] _encodings = null;
        public virtual Encoding[] Encodings
        {
            get
            {
                if (_encodings == null || _encodings?.Length == 0)
                {
                    List<Encoding> encodings = new();
                    encodings.Add(Encoding.Default);
                    encodings.Add(Encoding.GetEncoding(932)); // Shift-JIS
                    encodings.Add(Encoding.GetEncoding(949)); // EUC-KR (1)
                    encodings.Add(Encoding.GetEncoding(51949)); // EUC-KR (2)
                    encodings.Add(Encoding.Unicode);
                    encodings.Add(Encoding.BigEndianUnicode);
                    encodings.Add(Encoding.UTF8);
                    encodings.Add(Encoding.UTF7);
                    encodings.Add(Encoding.UTF32);
                    encodings.Add(Encoding.ASCII);

                    _encodings = encodings.ToArray();
                }

                return _encodings;
            }
        }

        public string ConvertToEncoded(string input)
        {
            var encodings = GetEncodedNames(input);
            foreach (var encoding in encodings)
            {
                if (Program.Lang.GetScript(encoding) == Language.ScriptType.CJK)
                {
                    return encoding;
                }
            }

            return input;
        }

        public string[] GetEncodedNames(string input)
        {
            List<string> encodings = new();
            foreach (var encoding in Encodings)
            {
                try { encodings.Add(encoding.GetString(Encoding.Default.GetBytes(input))); }
                catch { }
            }

            return encodings.ToArray();
        }

        public virtual ZipEntry GetZIPEntry(string line, bool searchEndingOnly, bool inclusive, bool caseInsensitive, ZipFile zip = null)
        {
            if (zip == null) zip = ZIP;

            foreach (ZipEntry item in zip)
                if (item.IsFile)
                {
                    int index = 0;

                    NameCheck:
                    try
                    {
                        string origName = caseInsensitive ? item.Name.ToLower() : item.Name;

                        byte[] nameBytes = index == 1 ? Encoding.GetEncoding(1252).GetBytes(origName) : Encoding.Convert(Encoding.Default, Encodings[index], Encoding.Default.GetBytes(origName));
                        string name = Encodings[index].GetString(nameBytes);

                        if ((searchEndingOnly &&
                            (name.EndsWith(line) || Path.GetFileNameWithoutExtension(name).EndsWith(line)))
                         || (!searchEndingOnly &&
                            ((inclusive && name.Contains(line)) || (!inclusive && name == line))))
                            return item;
                    }
                    catch
                    {
                        if (index + 1 < Encodings.Length) index++;
                        else return null;
                        goto NameCheck;
                    }
                }

            return null;
        }

        public virtual bool CheckZIPValidity(string[] strings, bool searchEndingOnly, bool caseInsensitive, string path = null)
        {
            if (path == null && FilePath != null) path = FilePath;
            if (!File.Exists(path)) return true;

            using (ZipFile ZIP = new(path))
            {
                int applicable = 0;

                foreach (string line in strings)
                    if (GetZIPEntry(line, searchEndingOnly, true, caseInsensitive) != null)
                        applicable++;

                return applicable >= strings.Length;
            }
        }

        public bool CheckSize(int length = 0)
        {
            if (Program.Config.application.bypass_rom_size) return true;

            else
            {
                if (length == 0) length = MaxSize;

                if (Bytes.Length > length && MaxSize > 0)
                {
                    bool isMB = length >= 1048576;
                    throw new Exception(string.Format(Program.Lang.Msg(3, 1),
                        Math.Round((double)length / (isMB ? 1048576 : 1024), 2).ToString(),
                        isMB ? Program.Lang.String("megabytes") : Program.Lang.String("kilobytes")));
                }

                return true;
            }
        }

        public void Patch(string filePath)
        {
            if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(filePath))
            {
                patched = null;
                return;
            }

            File.WriteAllBytes(Paths.WorkingFolder + "rom", origData);
            File.WriteAllBytes(Paths.WorkingFolder + "patch", File.ReadAllBytes(filePath));

            Utils.Run(FileDatas.Apps.xdelta3, "xdelta3", "-d -s rom patch rom_p_xdelta");
            Utils.Run(FileDatas.Apps.flips, "flips", "--apply patch rom rom_p_bps");

            // -----------------------
            // Check if patch applied successfully
            // -----------------------
            try { File.Delete(Paths.WorkingFolder + "rom"); } catch { }
            try { File.Delete(Paths.WorkingFolder + "patch"); } catch { }

            var Out = File.Exists(Paths.WorkingFolder + "rom_p_bps") ? File.ReadAllBytes(Paths.WorkingFolder + "rom_p_bps")
                : File.Exists(Paths.WorkingFolder + "rom_p_xdelta") ? File.ReadAllBytes(Paths.WorkingFolder + "rom_p_xdelta")
                : null;

            // -----------------------
            // Delete files
            // -----------------------
            try { File.Delete(Paths.WorkingFolder + "rom_p_xdelta"); } catch { }
            try { File.Delete(Paths.WorkingFolder + "rom_p_bps"); } catch { }

            patched = Out ?? throw new Exception(Program.Lang.Msg(8, 1));
        }

        public void Dispose()
        {
            FilePath = null;
            patched = null;
            MaxSize = -1;
        }
    }
}
