﻿using ICSharpCode.SharpZipLib.Zip;
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
            get => (int)Math.Round(_progress.step / _progress.max * 100.0);
        }

        public bool IsMultifile { get; set; } = false;
        public string Patch { get; set; } = null;
        public string Manual { get; set; } = null;
        public (IDictionary<string, string> List, IDictionary<Buttons, string> Keymap) Settings = (null, null);

        public string TitleID { get; set; } = "ABCD";
        public int WadRegion { get; set; } = -1;
        public int WadVideoMode { get; set; } = 0;
        public int WiiUDisplay { get; set; } = 0;
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

        public ROM ROM { get; set; } = null;
        public WAD WAD { get; set; } = null;
        public ImageHelper Img { get; set; } = null;

        public int EmuVersion { get; set; } = 0;
        private Platform Platform { get; set; } = 0;

        public Method(Platform platform)
        {
            Platform = platform;
            Program.CleanTemp();
        }

        public void GetWAD(string path, string tid)
        {
            try
            {
                WAD = new WAD();

                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (File.Exists(path))
                    {
                        Logger.Log($"Loading WAD file with title ID {tid}.");
                        WAD = WAD.Load(path);
                    }

                    else if (path.ToLower().StartsWith("http"))
                    {
                        Logger.Log($"Downloading WAD with title ID {tid}.");
                        _progress.max += 1.0;

                        WAD = WAD.Load(Web.Get(path));

                        _updateProgress();
                    }
                }

                else
                {
                    Logger.Log($"Loading blank WAD.");
                    WAD = WAD.Load(Properties.Resources.StaticBase);
                }


                // Title ID check
                // ****************
                if (WAD.UpperTitleID.ToUpper() != tid || !WAD.HasBanner)
                    WAD.Dispose();

                // Contents check
                // ****************
                if (WAD == null || WAD?.NumOfContents <= 1)
                    throw new Exception(Program.Lang.Msg(9, 1));

                Logger.Log($"WAD loaded.");
            }

            catch (Exception ex)
            {
                Logger.Log($"ERROR: Failed to load original WAD. {ex.Message}");
                if (ex.InnerException != null) Logger.Log($"DETAILS: {ex.InnerException.Message}");
                throw;
            }
        }
            
        public void Inject(bool useOrigManual = false)
        {
            try
            {
                if (Platform == Platform.Flash)
                {
                    Injectors.Flash Flash = new()
                    {
                        SWF = ROM as SWF,
                        Settings = Settings.List,
                        Keymap = Settings.Keymap,
                        Multifile = IsMultifile
                    };

                    WAD = Flash.Inject(WAD, SaveDataTitle, Img);
                }

                else
                {
                    // Create Wii VC injector to use
                    // *******
                    InjectorWiiVC VC = null;

                    switch (Platform)
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
                                IsSMS = Platform == Platform.SMS
                            };
                            break;

                        // PCE(CD)
                        // *******
                        case Platform.PCE:
                        case Platform.PCECD:
                            VC = new Injectors.PCE()
                            {
                                IsDisc = Platform == Platform.PCECD
                            };
                            break;

                        // NEOGEO
                        // *******
                        case Platform.NEO:
                            VC = new Injectors.NEO();
                            break;

                        // MSX
                        // *******
                        case Platform.C64:
                            VC = new Injectors.C64();
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
                    VC.Manual = Manual;

                    // Actually inject everything
                    // *******
                    WAD = VC.Inject(WAD, ROM, SaveDataTitle, Img);
                }

                Logger.Log("Created injected WAD.");
                _updateProgress();
            }

            catch (Exception ex)
            {
                Logger.Log($"ERROR: {ex.Message}");
                throw;
            }
        }

        public void CreateForwarder(string emulator, Forwarder.Storages storage)
        {
            try
            {
                Forwarder f = new()
                {
                    ROM = ROM.FilePath,
                    Multifile = IsMultifile,
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

                Logger.Log($"Created {emulator} forwarder.");
                _updateProgress();
            }

            catch (Exception ex)
            {
                Logger.Log($"ERROR: Failed to create forwarder. {ex.Message}");
                throw;
            }
        }

        public void EditMetadata()
        {
            if (WadRegion >= 0) WAD.Region = (Region)WadRegion;
            Utils.ChangeVideoMode(WAD, WadVideoMode, /* WiiUDisplay */ 0);

            WAD.ChangeChannelTitles(ChannelTitles_Limit);
            WAD.ChangeTitleID(LowerTitleID.Channel, TitleID);
            WAD.FakeSign = true;

            Logger.Log("Changed channel titles.");
            Logger.Log($"Changed WAD title ID to {TitleID}.");
            Logger.Log("Fakesigned WAD.");
        }

        public void EditBanner()
        {
            try
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
                if (Img?.VCPic != null) Img.ReplaceBanner(WAD);

                Logger.Log("Added VC banner.");
                _updateProgress();
            }

            catch (Exception ex)
            {
                Logger.Log($"ERROR: Failed to add VC banner. {ex.Message}");
                throw;
            }
        }

        public void Save()
        {
            try
            {
                if (Directory.Exists(Paths.SDUSBRoot))
                {
                    Directory.CreateDirectory(Paths.SDUSBRoot + "wad\\");
                    WAD.Save(Paths.SDUSBRoot + "wad\\" + Path.GetFileNameWithoutExtension(Out) + ".wad");

                    // Get ZIP directory path & compress to .ZIP archive
                    // *******
                    try { File.Delete(Out); } catch { }

                    FastZip z = new();
                    z.CreateZip(Out, Paths.SDUSBRoot, true, null);

                    // Clean
                    // *******
                    Directory.Delete(Paths.SDUSBRoot, true);
                }

                else WAD.Save(Out);

                Logger.Log($"SUCCESS: Exported to {Out}.");
                _updateProgress();
                _progress.step = _progress.max;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            catch (Exception ex)
            {
                Logger.Log($"ERROR: Failed to export. {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            _progress = (0.0, steps);

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

            ROM.Dispose();
            WAD.Dispose();
            Img.Dispose();

            EmuVersion = 0;
            Platform = 0;
        }
    }
}
