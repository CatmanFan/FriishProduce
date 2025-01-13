using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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

            if (Out == null) throw new Exception(Program.Lang.Msg(8, true));

            patched = Out;
        }

        /// <summary>
        /// Gets any game metadata that is available for the file based on its CRC32 reading hash, including the software title, year, players, and title image URL.
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public (string Name, string Serial, string Year, string Players, string Image) GetData(Platform platform, string path)
        {
            bool isDisc = platform == Platform.PCECD || platform == Platform.GCN || platform == Platform.SMCD || platform == Platform.PSX;
            if (isDisc)
            {
                if (Path.GetExtension(path).ToLower() == ".cue")
                    foreach (var item in Directory.EnumerateFiles(Path.GetDirectoryName(path)))
                        if (Path.GetExtension(item).ToLower() == ".bin" && Path.GetFileNameWithoutExtension(path).ToLower() == Path.GetFileNameWithoutExtension(item).ToLower())
                            path = item;

                if (Path.GetExtension(path).ToLower() != ".bin")
                    return (null, null, null, null, null);
            }

            var result = Databases.LibRetro.Read(path, platform);

            if (!string.IsNullOrEmpty(result.Name))
            {
                result.Name = Regex.Replace(result.Name?.Replace(": ", Environment.NewLine).Replace(" - ", Environment.NewLine), @"\((.*?)\)", "").Trim();
                if (result.Name.Contains(", The")) result.Name = "The " + result.Name.Replace(", The", string.Empty);
            }

            return result;
        }

        public void Dispose()
        {
            _rom = null;
            origData = null;
            patched = null;
            ZIP = null;
            MaxSize = -1;
        }
    }
}
