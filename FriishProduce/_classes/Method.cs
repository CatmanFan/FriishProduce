using ICSharpCode.SharpZipLib.Zip;
using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    [Serializable]
    class Method
    {
        private static readonly int steps = 3;
        (double step, double max) _progress = (0.0, steps);
        private void _updateProgress()
        {
            if (_progress.step < _progress.max)
                _progress.step += 1.0;
            else _progress.step = _progress.max;
        }

        public int Progress
        {
            get
            {
                return (int)Math.Round(_progress.step / _progress.max * 100);
            }
        }

        public (string Path, bool Multifile) ROM { get; set; } = (null, false);
        public string Patch { get; set; } = null;
        public string Manual { get; set; } = null;
        public (IDictionary<string, string> List, IDictionary<Buttons, string> Keymap) Settings = (null, null);

        public string TitleID { get; set; } = "ABCD";
        public int WadRegion { get; set; } = -1;
        public int WadVideoMode { get; set; } = 0;
        public string[] ChannelTitles { get; set; } = new string[] { "無題", "Untitled", "Ohne Titel", "Sans titre", "Sin título", "Senza titolo", "Onbekend", "제목 없음" };
        private string[] ChannelTitles_Limit
        {
            get
            {
                string[] value = ChannelTitles;

                int maxLength = 20;
                for (int i = 0; i < value.Length; i++)
                    if (value[i].Length > maxLength)
                    {
                        string delimiter = i == 0 ? "…" : "...";
                        value[i] = value[i].Substring(0, maxLength - delimiter.Length) + delimiter;
                    }

                return value;
            }
        }

        public Region BannerRegion { get; set; } = 0;
        public string BannerTitle { get; set; } = "";
        public int BannerYear { get; set; } = 1980;
        public int BannerPlayers { get; set; } = 1;
        public string BannerSound { get; set; } = null;
        public string[] SaveDataTitle { get; set; } = new string[2];
        public string Out { get; set; } = null;

        private ROM _rom { get; set; } = null;
        public WAD WAD { get; set; } = null;
        public ImageHelper Img { get; set; } = null;

        public int EmuVersion { get; set; } = 0;
        private Platform Platform { get; set; } = 0;

        public Method(Platform platform)
        {
            Platform = platform;

            _rom = Platform switch
            {
                Platform.NES => new ROM_NES(),
                Platform.SNES => new ROM_SNES(),
                Platform.N64 => new ROM_N64(),
                Platform.SMS => new ROM_SEGA() { IsSMS = true },
                Platform.SMD => new ROM_SEGA() { IsSMS = false },
                Platform.PCE => new ROM_PCE(),
                Platform.PCECD => new Disc(),
                Platform.NEO => new ROM_NEO(),
                Platform.MSX => new ROM_MSX(),
                Platform.Flash => new SWF(),
                Platform.RPGM => new RPGM(),
                _ => new Disc()
            };

            _rom.FilePath = ROM.Path;
        }

        public void GetWAD(string path, string tid)
        {
            WAD = new WAD();

            if (File.Exists(path)) WAD = WAD.Load(path);
            else if (path.ToLower().StartsWith("http"))
            {
                _progress.max += 1.0;

                Web.InternetTest();
                WAD = WAD.Load(Web.Get(path));

                _updateProgress();
            }

            // Title ID check
            // ****************
            if (WAD.UpperTitleID.ToUpper() != tid || !WAD.HasBanner)
                WAD.Dispose();

            // Contents check
            // ****************
            if (WAD == null || WAD?.NumOfContents <= 1)
                throw new Exception(Program.Lang.Msg(9, true));
        }
            
        public void Inject(Platform platform, bool useOrigManual = false)
        {
            if (platform == Platform.Flash)
            {
                Injectors.Flash Flash = new()
                {
                    SWF = ROM.Path,
                    Settings = Settings.List,
                    Keymap = Settings.Keymap,
                    Multifile = ROM.Multifile
                };

                WAD = Flash.Inject(WAD, SaveDataTitle, Img);
            }

            else
            {
                // Create Wii VC injector to use
                // *******
                InjectorWiiVC VC = null;

                switch (platform)
                {
                    default:
                        throw new NotImplementedException();

                    // NES
                    // *******
                    case Platform.NES:
                        VC = new Injectors.NES();
                        break;

                    // SNES
                    // *******
                    case Platform.SNES:
                        VC = new Injectors.SNES();
                        break;

                    // N64
                    // *******
                    case Platform.N64:
                        VC = new Injectors.N64()
                        {
                            CompressionType = EmuVersion == 3 ? (Settings.List["romc"] == "0" ? 1 : 2) : 0,
                            Allocate = Settings.List["rom_autosize"] == "True" && (EmuVersion <= 1),
                        };
                        break;

                    // SEGA
                    // *******
                    case Platform.SMS:
                    case Platform.SMD:
                        VC = new Injectors.SEGA()
                        {
                            IsSMS = platform == Platform.SMS
                        };
                        break;

                    // PCE
                    // *******
                    case Platform.PCE:
                        VC = new Injectors.PCE();
                        break;

                    // PCECD
                    // *******
                    case Platform.PCECD:
                        // VC = new Injectors.PCECD();
                        break;

                    // NEOGEO
                    // *******
                    case Platform.NEO:
                        VC = new Injectors.NEO();
                        break;

                    // MSX
                    // *******
                    case Platform.MSX:
                        VC = new Injectors.MSX();
                        break;
                }

                // Get settings from relevant form
                // *******
                VC.Settings = Settings.List;
                VC.Keymap = Settings.Keymap;

                // Set path to manual (if it exists) and load WAD
                //// *******
                VC.UseOrigManual = useOrigManual;
                VC.CustomManual = (File.Exists(Manual) || Directory.Exists(Manual)) && !VC.UseOrigManual ? Manual : null;

                // Actually inject everything
                // *******
                WAD = VC.Inject(WAD, _rom, SaveDataTitle, Img);
            }

            _updateProgress();
        }

        public void CreateForwarder(string emulator, Forwarder.Storages storage)
        {
            Forwarder f = new()
            {
                ROM = ROM.Path,
                Multifile = ROM.Multifile,
                ID = TitleID,
                Emulator = emulator,
                Storage = storage,
                Name = ChannelTitles[1]
            };

            // Get settings from relevant form
            // *******
            f.Settings = Settings.List;

            // Actually inject everything
            // *******
            f.CreateZIP(Path.Combine(Path.GetDirectoryName(Out), Path.GetFileNameWithoutExtension(Out) + $" ({f.Storage}).zip"));
            WAD = f.CreateWAD(WAD);

            _updateProgress();
        }

        public void EditBanner()
        {
            // Sound
            // *******
            if (File.Exists(BannerSound))
                SoundHelper.ReplaceSound(WAD, BannerSound);
            else
                SoundHelper.ReplaceSound(WAD, Properties.Resources.Sound_WiiVC);

            // Banner text
            // *******
            BannerHelper.Modify
            (
                WAD,
                Platform,
                BannerRegion,
                BannerTitle,
                BannerYear,
                BannerPlayers
            );

            // Image
            // *******
            if (Img.VCPic != null) Img.ReplaceBanner(WAD);

            _updateProgress();
        }

        public void Save()
        {
            if (WadRegion >= 0) WAD.Region = (Region)WadRegion;
            Utils.ChangeVideoMode(WAD, WadVideoMode);

            WAD.ChangeChannelTitles(ChannelTitles_Limit);
            WAD.ChangeTitleID(LowerTitleID.Channel, TitleID);
            WAD.FakeSign = true;

            if (Directory.Exists(Paths.SDUSBRoot))
            {
                Directory.CreateDirectory(Paths.SDUSBRoot + "wad\\");
                WAD.Save(Paths.SDUSBRoot + "wad\\" + Path.GetFileNameWithoutExtension(Out) + ".wad");

                // Get ZIP directory path & compress to .ZIP archive
                // *******
                if (File.Exists(Out)) File.Delete(Out);

                FastZip z = new();
                z.CreateZip(Out, Paths.SDUSBRoot, true, null);

                // Clean
                // *******
                Directory.Delete(Paths.SDUSBRoot, true);
            }

            else WAD.Save(Out);

            _updateProgress();
            _progress.step = _progress.max;
        }

        public void Dispose()
        {
            _progress = (0.0, steps);

            ROM = (null, false);
            Patch = null;
            Manual = null;
            Settings = (null, null);

            WadRegion = 0;
            WadVideoMode = 0;
            TitleID = null;
            ChannelTitles = null;

            BannerRegion = 0;
            BannerTitle = null;
            BannerYear = 1980;
            BannerPlayers = 1;
            SaveDataTitle = null;
            Out = null;

            _rom.Dispose();
            WAD.Dispose();
            Img.Dispose();

            EmuVersion = 0;
            Platform = 0;
        }
    }
}
