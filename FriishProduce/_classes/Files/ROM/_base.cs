using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

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

        public virtual bool CheckZIPValidity(string[] strings, bool searchEndingOnly, bool forceLowercase, string path = null)
        {
            if (path == null && FilePath != null) path = FilePath;
            if (!File.Exists(path)) return true;

            using (ZipFile ZIP = new(path))
            {
                int applicable = 0;

                foreach (ZipEntry item in ZIP)
                    foreach (string line in strings)
                        if (item.IsFile)
                        {
                            string name = forceLowercase ? item.Name.ToLower() : item.Name;
                            if ((searchEndingOnly && (name.EndsWith(line) || Path.GetFileNameWithoutExtension(name).EndsWith(line)))
                                || (!searchEndingOnly && name.Contains(line)))
                                applicable++;
                        }

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
                    throw new Exception(string.Format(Program.Lang.Msg(3, true),
                        Math.Round((double)length / (isMB ? 1048576 : 1024), 2).ToString(),
                        isMB ? Program.Lang.String("megabytes") : Program.Lang.String("kilobytes")));
                }

                return true;
            }
        }

        public void Patch(string filePath)
        {
            if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(filePath)) return;

            File.WriteAllBytes(Paths.WorkingFolder + "rom", origData);
            File.WriteAllBytes(Paths.WorkingFolder + "patch", File.ReadAllBytes(filePath));

            Utils.Run(FileDatas.Apps.xdelta3, "xdelta3", "-d -s rom patch rom_p_xdelta");
            Utils.Run(FileDatas.Apps.flips, "flips", "--apply patch rom rom_p_bps");

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

            patched = Out ?? throw new Exception(Program.Lang.Msg(8, true));
        }

        public void Dispose()
        {
            FilePath = null;
            patched = null;
            MaxSize = -1;
        }
    }
}
