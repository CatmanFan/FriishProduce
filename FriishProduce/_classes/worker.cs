using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace FriishProduce
{
    public class Worker
    {
        internal bool Custom { get; set; }
        internal bool Import { get; set; }

        public Worker()
        {
            Custom = false;
            Import = false;
        }

        public void EditBanner()
        {
            if (Custom)
            {
                if (Import)
                {
                    foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                        if (File.Exists(item) && item.Contains(db.SearchID(ImportBases.SelectedItem.ToString())))
                            WAD.Load(item).BannerApp.Save(Paths.WorkingFolder + "00000000.app");
                }

                // --------------------------------------------------------------------------- //

                // Get banner.brlyt and TPLs
                Directory.CreateDirectory(Paths.Images);

                libWiiSharp.U8 BannerApp = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000000.app");
                libWiiSharp.U8 Banner = libWiiSharp.U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("banner.bin")]);
                libWiiSharp.U8 Icon = libWiiSharp.U8.Load(BannerApp.Data[BannerApp.GetNodeIndex("icon.bin")]);

                try
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "banner.brlyt", Banner.Data[Banner.GetNodeIndex("banner.brlyt")]);

                    if (tImg.Path != null)
                    {
                        File.WriteAllBytes(Paths.Images + "VCPic.tpl", Banner.Data[Banner.GetNodeIndex("VCPic.tpl")]);
                        File.WriteAllBytes(Paths.Images + "IconVCPic.tpl", Icon.Data[Icon.GetNodeIndex("IconVCPic.tpl")]);
                    }
                }
                catch
                {
                    throw new Exception(x.Get("m008"));
                }

                // --------------------------------------------------------------------------- //

                // Copy VCbrlyt to working folder
                string path = Paths.Apps + "vcbrlyt\\";
                foreach (string dir in Directory.GetDirectories(path))
                    Directory.CreateDirectory(dir.Replace(path, Paths.WorkingFolder + "vcbrlyt\\"));
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                    File.Copy(file, file.Replace(path, Paths.WorkingFolder + "vcbrlyt\\"));

                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = Paths.WorkingFolder + "vcbrlyt\\vcbrlyt.exe",
                    Arguments = $"{Paths.WorkingFolder + "banner.brlyt"} -Title \"{BannerTitle.Text.Replace('-', '–').Replace(Environment.NewLine, "^").Replace("\"", "''")}\" -YEAR {ReleaseYear.Value} -Play {Players.Value}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();

                // --------------------------------------------------------------------------- //

                var Brlyt = File.ReadAllBytes(Paths.WorkingFolder + "banner.brlyt");
                if (Brlyt == Banner.Data[Banner.GetNodeIndex("banner.brlyt")])
                    throw new Exception(x.Get("m007"));
                Banner.ReplaceFile(Banner.GetNodeIndex("banner.brlyt"), Brlyt);

                // --------------------------------------------------------------------------- //

                if (tImg.Path != null)
                {
                    TPL tpl = TPL.Load(Paths.Images + "VCPic.tpl");
                    var tplTF = tpl.GetTextureFormat(0);
                    var tplPF = tpl.GetPaletteFormat(0);
                    tpl.RemoveTexture(0);
                    tpl.AddTexture(tImg.VCPic, tplTF, tplPF);
                    tpl.Save(Paths.Images + "VCPic.tpl");

                    tpl = TPL.Load(Paths.Images + "IconVCPic.tpl");
                    tplTF = tpl.GetTextureFormat(0);
                    tplPF = tpl.GetPaletteFormat(0);
                    tpl.RemoveTexture(0);
                    tpl.AddTexture(tImg.IconVCPic, tplTF, tplPF);
                    tpl.Save(Paths.Images + "IconVCPic.tpl");

                    tpl.Dispose();

                    Banner.ReplaceFile(Banner.GetNodeIndex("VCPic.tpl"), File.ReadAllBytes(Paths.Images + "VCPic.tpl"));
                    Icon.ReplaceFile(Icon.GetNodeIndex("IconVCPic.tpl"), File.ReadAllBytes(Paths.Images + "IconVCPic.tpl"));

                    Icon.AddHeaderImd5();
                    BannerApp.ReplaceFile(BannerApp.GetNodeIndex("icon.bin"), Icon.ToByteArray());
                }

                Banner.AddHeaderImd5();
                BannerApp.ReplaceFile(BannerApp.GetNodeIndex("banner.bin"), Banner.ToByteArray());
                BannerApp.AddHeaderImet(false, ChannelTitle.Text);
                BannerApp.Save(Paths.WorkingFolder + "00000000.app");

                BannerApp.Dispose();
                Banner.Dispose();
                Icon.Dispose();

                Directory.Delete(Paths.Images, true);
                File.Delete(Paths.WorkingFolder + "banner.brlyt");
                Directory.Delete(Paths.WorkingFolder + "vcbrlyt\\", true);
            }
#endregion

            // ----------------------------------------------------
            // Virtual Console injection
            // ----------------------------------------------------
            if (currentConsole != Platforms.Flash && !ForwarderMode)
            {
                if (currentConsole != Platforms.SMS && currentConsole != Platforms.SMD)
                    U8.Unpack(Paths.WorkingFolder + "00000005.app", Paths.WorkingFolder_Content5);
                else
                {
                    Directory.CreateDirectory(Paths.WorkingFolder_Content5);

                    // Write data.ccf directly from U8 loader
                    libWiiSharp.U8 u = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000005.app");
                    File.WriteAllBytes(Paths.WorkingFolder_Content5 + "data.ccf", u.Data[u.GetNodeIndex("data.ccf")]);
                    u.Dispose();

                    // Extract CCF files
                    new Injectors.SEGA().GetCCF(Custom.Checked);
                }

                if (DisableEmanual.Checked) Global.RemoveEmanual();
                if (Custom.Checked && tImg.Path != null) tImg.CreateSave(currentConsole);

                switch (currentConsole)
                {
                    default:
                        throw new Exception("Not implemented yet!");

                    case Platforms.NES:
                        {
                            Injectors.NES NES = new Injectors.NES
                            {
                                ROM = input[0],
                                content1_file = Global.DetermineContent1(),
                                saveTPL_offsets = new Injectors.NES().DetermineSaveTPLOffsets(Global.DetermineContent1())
                            };

                            NES.InsertROM();
                            NES.InsertPalette(NES_Palette.SelectedIndex);
                            if (Custom.Checked)
                            {
                                if (tImg.Path != null)
                                {
                                    if (NES.ExtractSaveTPL(Paths.WorkingFolder + "out.tpl"))
                                        tImg.CreateSave(Platforms.NES);
                                }

                                NES.InsertSaveData(SaveDataTitle.Text, Paths.WorkingFolder + "out.tpl");
                            }
                            Global.PrepareContent1();
                            break;
                        }

                    case Platforms.SNES:
                        {
                            Injectors.SNES SNES = new Injectors.SNES()
                            {
                                ROM = input[0],
                                ROMcode = new Injectors.SNES().ProduceID(db.SearchID(Bases.SelectedItem.ToString()))
                            };

                            SNES.ReplaceROM();

                            if (Custom.Checked) SNES.InsertSaveTitle(SaveDataTitle.Lines);
                            break;
                        }

                    case Platforms.N64:
                        {
                            Injectors.N64 N64 = new Injectors.N64() { ROM = input[0] };
                            foreach (var entry in db.GetList())
                            {
                                if (entry["title"].ToString() == Bases.SelectedItem.ToString())
                                    foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                        if (item.Contains(entry["id"].ToString().ToUpper()))
                                            N64.emuVersion = entry["ver"].ToString();
                            }

                            if (N64_FixBrightness.Checked
                                || N64_UseExpansionPak.Checked
                                || N64_Allocation.Checked
                                || N64_FixCrash.Checked)
                            {
                                string emulator_file = Global.DetermineContent1();
                                var emulator = File.ReadAllBytes(emulator_file);

                                if (N64_UseExpansionPak.Checked) N64.Op_ExpansionRAM(emulator);
                                if (N64_FixBrightness.Checked) N64.Op_FixBrightness(emulator);
                                if (N64_Allocation.Checked) N64.Op_AllocateROM(emulator);
                                if (N64_FixCrash.Checked) N64.Op_FixCrashes(emulator);

                                File.WriteAllBytes(emulator_file, emulator);
                                Global.PrepareContent1();
                            }
                            N64.ReplaceROM();

                            if (Custom.Checked) N64.InsertSaveComments(SaveDataTitle.Lines);
                            break;
                        }

                    case Platforms.SMS:
                    case Platforms.SMD:
                        {
                            Injectors.SEGA SEGA = new Injectors.SEGA() { ROM = input[0], SMS = currentConsole == Platforms.SMS };
                            foreach (var entry in db.GetList())
                            {
                                if (entry["title"].ToString() == Bases.SelectedItem.ToString())
                                    foreach (var item in Directory.GetFiles(Paths.Database, "*.*", SearchOption.AllDirectories))
                                        if (item.Contains(entry["id"].ToString().ToUpper()))
                                        {
                                            SEGA.ver = int.Parse(entry["ver"].ToString());
                                            SEGA.origROM = entry["ROM"].ToString();
                                        }
                            }

                            SEGA.ReplaceROM();

                            // Config parameters
                            if (SEGA_SetConfig.Checked)
                            {
                                var new_config = new string[ConfigForm_SEGA.config.Count];
                                ConfigForm_SEGA.config.CopyTo(new_config);
                                SEGA.ReplaceConfig(new_config);
                            }

                            if (Custom.Checked) SEGA.InsertSaveTitle(ChannelTitle.Text);
                            SEGA.PackCCF(Custom.Checked);
                            break;
                        }
                }

                if (currentConsole != Platforms.SMS && currentConsole != Platforms.SMD)
                    U8.Pack(Paths.WorkingFolder_Content5, Paths.WorkingFolder + "00000005.app");
                else
                {
                    // Write data.ccf directly to U8 loader
                    libWiiSharp.U8 u2 = libWiiSharp.U8.Load(Paths.WorkingFolder + "00000005.app");
                    u2.ReplaceFile(u2.GetNodeIndex("data.ccf"), Paths.WorkingFolder_Content5 + "data.ccf");
                    u2.Save(Paths.WorkingFolder + "00000005.app");
                    u2.Dispose();
                }
            }

            // ----------------------------------------------------
            // Adobe Flash
            // ----------------------------------------------------
            else if (currentConsole == Platforms.Flash)
            {
                U8.Unpack(Paths.WorkingFolder + "00000002.app", Paths.WorkingFolder_Content2);

                Injectors.Flash Flash = new Injectors.Flash() { SWF = input[0] };
                Flash.ReplaceSWF();

                Flash.HomeMenuNoSave(Flash_HBMNoSave.Checked);
                Flash.SetStrapReminder(Flash_StrapReminder.SelectedIndex);
                if (Flash_UseSaveData.Checked) Flash.EnableSaveData(Convert.ToInt32(Flash_TotalSaveDataSize.SelectedItem.ToString()));
                if (Flash_CustomFPS.Checked) Flash.SetFPS(Flash_FPS.SelectedItem.ToString());
                if (Flash_Controller.Checked) Flash.SetController(btns);
                if (Custom.Checked && tImg.Path != null) tImg.CreateSave(Platforms.Flash);
                if (Custom.Checked) Flash.InsertSaveData(SaveDataTitle.Lines);

                U8.Pack(Paths.WorkingFolder_Content2, Paths.WorkingFolder + "00000002.app");
            }

            // ----------------------------------------------------
            // Forwarder creator
            // ----------------------------------------------------
            else if (currentConsole != Platforms.Flash && ForwarderMode)
            {
                Forwarders.Generic f = new Forwarders.Generic
                {
                    ROM = input[0],
                    IsISO = currentConsole == Platforms.SMCD,
                    UseUSBStorage = AltCheckbox.Checked
                };

                f.Generate
                (
                    TitleID.Text.ToUpper(),
                    f.UseUSBStorage ?
                        Path.Combine(Path.GetDirectoryName(SaveWAD.FileName), $"{TitleID.Text}_USBRoot.zip") :
                        Path.Combine(Path.GetDirectoryName(SaveWAD.FileName), $"{TitleID.Text}_SDRoot.zip"),
                    InjectionMethod.SelectedItem.ToString()
                );
                w = f.ConvertWAD(w, vWii.Checked, TitleID.Text.ToUpper());
            }

            if (!ForwarderMode) w.CreateNew(Paths.WorkingFolder);

            // TO-DO: IOS video mode patching
            if (!ForwarderMode && AltCheckbox.Checked)
            {
                var ios = (int)w.StartupIOS;
                if (ios == 53) w.AddContent(Properties.Resources.NANDLoader_NTSC_53, w.BootIndex, w.BootIndex);
                else if (ios == 55) w.AddContent(Properties.Resources.NANDLoader_NTSC_55, w.BootIndex, w.BootIndex);
                else if (ios == 56) w.AddContent(Properties.Resources.NANDLoader_NTSC_56, w.BootIndex, w.BootIndex);
                else w.AddContent(Properties.Resources.NANDLoader_NTSC, w.BootIndex, w.BootIndex);
                w.RemoveContent(w.BootIndex);
            }

            if (RegionFree.Checked) w.Region = libWiiSharp.Region.Free;
            w.FakeSign = true;
            w.ChangeTitleID(LowerTitleID.Channel, TitleID.Text);
            w.Save(SaveWAD.FileName);
            w.Dispose();
        }
    }
}
*/