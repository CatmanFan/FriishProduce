using Ionic.Zip;
using System;
using System.IO;

namespace FriishProduce
{
    [Serializable]
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
                    try
                    {
                        // -----------------------
                        // Check if raw ROM exists
                        // -----------------------
                        if (!File.Exists(value))
                            throw new FileNotFoundException(new FileNotFoundException().Message, value);

                        _rom = value;

                        if (System.IO.Path.GetExtension(value).ToLower() == ".zip") try { ZIP = new ZipFile(value); } catch { ZIP = null; }
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
            if (Bytes.Length > length && MaxSize > 0)
            {
                bool isMB = length >= 1048576;
                throw new Exception(string.Format(Program.Lang.Msg(3, true),
                    Math.Round((double)length / (isMB ? 1048576 : 1024), 2).ToString(),
                    isMB ? Program.Lang.String("megabytes") : Program.Lang.String("kilobytes")));
            }

            return true;
        }

        public bool CheckSize() => CheckSize(MaxSize);

        public void Patch(string PatchFile)
        {
            if (!File.Exists(PatchFile) || string.IsNullOrWhiteSpace(PatchFile)) return;

            File.WriteAllBytes(Paths.WorkingFolder + "rom", origData);
            File.WriteAllBytes(Paths.WorkingFolder + "patch", File.ReadAllBytes(PatchFile));

            Utils.Run(Properties.Resources.App_XDelta, "xdelta3", "-d -s rom patch rom_p_xdelta");
            Utils.Run(Properties.Resources.App_Flips, "flips", "--apply patch rom rom_p_bps");

            // -----------------------
            // Check if patch applied successfully
            // -----------------------
            if (File.Exists(Paths.WorkingFolder + "rom")) File.Delete(Paths.WorkingFolder + "rom");
            if (File.Exists(Paths.WorkingFolder + "patch")) File.Delete(Paths.WorkingFolder + "patch");

            var Out = File.Exists(Paths.WorkingFolder + "rom_p_bps") ? File.ReadAllBytes(Paths.WorkingFolder + "rom_p_bps")
                : File.Exists(Paths.WorkingFolder + "rom_p_xdelta") ? File.ReadAllBytes(Paths.WorkingFolder + "rom_p_xdelta")
                : null;

            // -----------------------
            // Delete files
            // -----------------------
            if (File.Exists(Paths.WorkingFolder + "rom_p_xdelta")) File.Delete(Paths.WorkingFolder + "rom_p_xdelta");
            if (File.Exists(Paths.WorkingFolder + "rom_p_bps")) File.Delete(Paths.WorkingFolder + "rom_p_bps");

            if (Out == null) throw new Exception(Program.Lang.Msg(7, true));

            patched = Out;
        }
    }
}
