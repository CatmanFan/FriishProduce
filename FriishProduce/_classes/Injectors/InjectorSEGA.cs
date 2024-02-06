using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce
{
    class InjectorSEGA : InjectorBase
    {
        public string ROM { get; set; }
        public enum Type
        {
            Rev1 = 1,       // Comix Zone (SMD)

            /*  Per RadioShadow on GBATemp:
            Version 1 [Sonic The Hedgehog]:
            - Analogue Stick doesn't work on Classic Controller/Gamecube.
            - Don't support PAL Component Fix.
            - Not all have 6 button controller support.
            - The Save Banner is called "banner_xx.wte" (xx = region)
            - In the "misc_ccf_zlib" folder is a file calleed "wii_vc.txt". The necessary text messages for the necessary languages. */

            // There is an issue with Ver 1 emulator, where if savedata is not customized, it will display a blank banner for savedata.
            // Unless savedata title is edited somehow, the WAD lands on a blackscreen. This GUI skips the bug since the savedata will be modified in every scenario

            // EXAMPLE Config:
            // **********************
            /* country="us"
             * romfile="ComixZone_USA.SGD"
             * dev.mdpad.enable_6b="1" */

            Rev2,           // Sonic (SMS); Earthworm Jim, Sonic 3D Blast (SMD)

            /* Version 2 [Streets of Rage 2] (the one this guide refers to):
            - Analogue Stick works on Classic Controller/Gamecube.
            - Supports PAL Component Fix.
            - All have 6 button controller support.
            - The Save Banner is now just called "banner.wte"
            - No "wii_vc.txt" file in the "misc_ccf_zlib" folder. The text message is in the emulator instead.
            - Some contain a "patch" file (may or may not have data in). If a certain rom is detected, then changes are made to the emulator (like skipping the checksum). An injected rom won't use the patch. */

            // EXAMPLE Config:
            // **********************
            /* country="us"
             * romfile="ComixZone_USA.SGD"
             * dev.mdpad.enable_6b="1" */

            Rev3            // Phantasy Star (SMS); Pulseman (SMD); Sonic & Knuckles (special edition)

            /* Version 3 [Pulseman] (US/EU):
            - Analogue Stick works on Classic Controller/Gamecube.
            - Supports PAL Component Fix.
            - All have 6 button controller support.
            - In the config file, some contain an option to adjust the brightness.
            - In the config file, the volume can increased. Default is set to +6.5.
            - In the config file, Machine Country can be set to Japan, USA or Europe.
            - In the config file is a "console.machine_arch" option set to "md". No idea what this option does.
            - In the "misc_ccf_zlib" folder, there is a "comment" file. Contains the text for both lines which can easily be edited (before only the first line of text could be edited and the second line text was determined on what country was set in the config file). Seems to only support ASCII (let to try UNICODE).
            - In the "data_ccf" folder are three files: "emu_m68kbase.rso", "tsdevp.rso", "md.rso" & "se_vc.sel". No idea what they do but the "config" file says it is something to do with the "modules" (my guess is it modifies the emulator).
            - Also in the "data_ccf" is a "tsdevp.rso" file. No idea what it does but the config say it is something to do with the "snd.snddrv". */
        }

        private enum CCFEx
        {
            Normal,
            Legacy
        }

        private enum CCFArc
        {
            Normal,
            Legacy,
            Raw
        }

        public Type EmuType { get; set; }
        public bool IsSMS { get; set; }
        private string ROMName { get; set; }

        public Dictionary<string, string> Config { get; set; }
        // CONFIG Encoding: Unix (LF) - UTF-8 (new empty line at bottom)
        // Savedata Encoding: Windows (CRLF) - UTF-16 Big Endian ("banner.cfg")
        // Savedata Encoding: Windows (CRLF) - UTF-8 ("comment", ver3 only)
        /* 
        console.master_volume="+11.0"
        console.rapidfire="10"
        console.volume="10"*/

        /* 
         * S&K CONFIG:
        console.cl_bindings="up=U:down=D:left=L:right=R:+=S:y=A:b=B:a=C:l=X:x=Y:r=Z:zr=M:zl="
        console.core_bindings="up=U:down=D:left=L:right=R:+=S:a=A:1=B:2=C:b="
        console.disable_resetbutton="1"
        console.gc_bindings="up=U:down=D:left=L:right=R:start=S:b=A:a=B:x=C:l=X:y=Y:r=Z:z=M:c="
        console.machine_arch="md"
        console.machine_country="eu"
        console.master_volume="+6.0"
        console.volume="10"
        machine_md.snd_strict_sync="1"
        modules="emu_m68kbase tsdevp md se_vc selectmenu sandkui"
        romfile="sandk_composite.sgd"
        save_sram="1"
        selectmenu="sandk"
        snd.snddrv="tsdev+"
        vdp.disable_gamma="0"
        vdp_md.gamma_curve ="0x00,0x1a,0x31,0x42,0x54,0x66,0x78,0x80,0x8f,0x9e,0xad,0xc1,0xd5,0xea,0xff"
         */

        public InjectorSEGA(WAD w) : base(w)
        {
            UsesContent5 = true;
            Load();
            
            switch (WAD.UpperTitleID.Substring(0, 3).ToUpper())
            {
                default:
                case "MAP": // SMD
                    EmuType = Type.Rev1;
                    break;

                case "LAG": // SMS
                // ---------------
                case "MA6": // SMD
                case "MCP":
                    EmuType = Type.Rev2;
                    break;

                case "LAD": // SMS - Phantasy Star uses rev2 in Japanese region
                    EmuType = WAD.UpperTitleID.ToUpper()[3] == 'J' ? Type.Rev2 : Type.Rev3;
                    break;

                case "MBA": // SMD
                case "MC2":
                    EmuType = Type.Rev3;
                    break;
            }
        }

        #region External Tools
        /// <summary>
        /// Extracts CCF to a specified location.
        /// </summary>
        /// <param name="legacy">Defaults to legacy app by default, since the forked version does relatively bad with SMS WADs</param>
        private void RunCCFEx(string file, string dir, CCFEx type)
        {
            // Copy application to target dir
            // ****************
            string pPath = Paths.Tools + "sega\\" + (type == CCFEx.Normal ? "ccfex.exe" : "ccfex_2009.exe");

            // Start application
            // ****************
            Process.Run(pPath, dir, $"\"{file}\"");
        }

        /// <summary>
        /// Packs CCF to byte array.
        /// </summary>
        /// <param name="raw">Determines whether to use ccfarcraw.exe (needed for misc.ccf)</param>
        /// <returns>Path to an out.ccf file if it was created.</returns>
        private byte[] RunCCFArc(string dir, CCFArc type)
        {
            string pPath = Paths.Tools + "sega\\" + (type == CCFArc.Raw ? "ccfarcraw.exe" : type == CCFArc.Legacy ? "ccfarc_2009.exe" : "ccfarc.exe");

            if (dir == Paths.DataCCF)
            {
                string files = $"Opera.arc {ROMName}";

                // Rearrange file order for some WADs
                // ****************
                if (ROMName.Contains("Columns.") || ROMName.Contains("ComixZone") || ROMName.Contains("MonsterWorld3")              // Rev1
                    || ROMName.Contains("AlexKiddTheLostStars") || ROMName.Contains("MonsterWorld1")                                // Rev2 (SMS)
                    || ROMName.Contains("Columns3") || ROMName.Contains("EarthwormJim1")                                            // Rev2 (SMD)
                    || ROMName.Contains("EarthwormJim2"))                                                                           // Rev3
                    files = $"{ROMName} Opera.arc";

                foreach (var item in Directory.EnumerateFiles(dir))
                    if (Path.GetFileName(item).ToLower() != "opera.arc" && Path.GetFileName(item) != ROMName)
                        files += $" {Path.GetFileName(item)}";

                if (ROMName.Contains("sandk_composite"))
                    files = $"Opera.arc config emu_m68kbase.rso man.arc md.rso misc.ccf {ROMName} sandkui.rso se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso tsdevp.rso wii_vc.sel";

                // ALTERNATIVE CODE IF THE ABOVE METHOD DOES NOT WORK
                /* string files = "Opera.arc";

                if (!IsSMS)
                {
                    switch (EmuType)
                    {
                        default:
                        case Type.Rev1:
                        case Type.Rev2:
                            files = $"Opera.arc {ROMName} config man.arc misc.ccf";
                            break;

                        case Type.Rev3:
                            files = $"Opera.arc {ROMName} config emu_m68kbase.rso man.arc md.rso misc.ccf se_vc.rso tsdevp.rso wii_vc.sel";

                            // Extra files needed
                            // ****************
                            if (ROMName.Contains("EarthwormJim2"))
                                files = $"{ROMName} Opera.arc config emu_m68kbase.rso man.arc md.rso misc.ccf se_vc.rso selectmenu.cat selectmenu.conf tsdevp.rso wii_vc.sel";
                            else if (ROMName.Contains("sandk_composite"))
                                files = $"Opera.arc config emu_m68kbase.rso man.arc md.rso misc.ccf {ROMName} sandkui.rso se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso tsdevp.rso wii_vc.sel";
                            break;
                    }
                }
                else
                {
                    switch (EmuType)
                    {
                        default:
                        case Type.Rev1:
                            files = $"Opera.arc {ROMName} config man.arc misc.ccf";
                            break;

                        case Type.Rev2:
                            files = $"Opera.arc {ROMName} config emu_m68kbase.rso man.arc misc.ccf se_vc.rso sms.rso tsdevp.rso wii_vc.sel";
                            break;

                        case Type.Rev3:
                            files = $"Opera.arc {ROMName} config emu_m68kbase.rso man.arc misc.ccf se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso sms.rso tsdevp.rso wii_vc.sel";
                            break;
                    }
                }

                // Check for "patch" and other additional files
                // ****************
                foreach (var item in Directory.EnumerateFiles(dir))
                {
                    if (Path.GetFileName(item).ToLower() == "patch")
                        files = files.Replace("misc.ccf", "misc.ccf patch");

                    else if (Path.GetFileName(item).ToLower() == "smsui.rso")
                        files.Replace("sms.rso", "sms.rso smsui.rso");

                    else if (Path.GetFileName(item).ToLower() == "home.csv")
                        files = files.Replace("man.arc", "home.csv man.arc");
                }

                // Rearrange file order for some WADs
                // ****************
                if (ROMName.Contains("Columns.") || ROMName.Contains("ComixZone") || ROMName.Contains("MonsterWorld3")             // Rev1
                    || ROMName.Contains("AlexKiddTheLostStars") || ROMName.Contains("MonsterWorld1")                                // Rev2 (SMS)
                    || ROMName.Contains("Columns3") || ROMName.Contains("EarthwormJim1")                                            // Rev2 (SMD)
                    || ROMName.Contains("EarthwormJim2"))                                                                           // Rev3
                    files = files.Replace($"Opera.arc {ROMName}", $"{ROMName} Opera.arc"); */

                // Start application
                // ****************
                Process.Run(pPath, dir, files);
            }
            else
            {
                string files = "";
                foreach (var item in Directory.EnumerateFiles(dir))
                    if (!files.Contains(Path.GetFileName(item)))
                        files += $" {Path.GetFileName(item)}";
                files = files.Remove(0, 1);

                // Start application
                // ****************
                Process.Run(pPath, dir, files);
            }

            if (File.Exists(dir + "out.ccf") && File.ReadAllBytes(dir + "out.ccf").Length > 0)
            {
                var bytes = File.ReadAllBytes(dir + "out.ccf");
                if (File.Exists(dir + "out.ccf")) File.Delete(dir + "out.ccf");
                return bytes;
            }

            throw new Exception(Language.Get("Error002"));
        }
        #endregion

        public bool GetCCF(bool UseLegacy)
        {
            // Get Data.ccf first
            // ****************
            File.WriteAllBytes(Paths.WorkingFolder + "data.ccf", Content5.Data[Content5.GetNodeIndex("data.ccf")]);

            Directory.CreateDirectory(Paths.DataCCF);
            RunCCFEx(Paths.WorkingFolder + "data.ccf", Paths.DataCCF, UseLegacy ? CCFEx.Legacy : CCFEx.Normal);

            // Failsafe
            // ****************
            if (Directory.EnumerateFiles(Paths.DataCCF).Count() < 3 ||
                (File.Exists(Paths.DataCCF + "Opera.arc") && File.ReadAllBytes(Paths.DataCCF + "Opera.arc").Length == 0))
            {
                UseLegacy = true;
                Directory.Delete(Paths.DataCCF, true);
                Directory.CreateDirectory(Paths.DataCCF);
                RunCCFEx(Paths.WorkingFolder + "data.ccf", Paths.DataCCF, CCFEx.Legacy);
            }

            // Get Misc.ccf
            // ****************
            Directory.CreateDirectory(Paths.MiscCCF);
            RunCCFEx(Paths.DataCCF + "misc.ccf", Paths.MiscCCF, UseLegacy ? CCFEx.Legacy : CCFEx.Normal);

            return UseLegacy;
        }

        public void PackCCF(bool UseLegacy)
        {
            // Misc.ccf
            // ****************
            File.WriteAllBytes(Paths.DataCCF + "misc.ccf", RunCCFArc(Paths.MiscCCF, CCFArc.Raw));

            if (Directory.Exists(Paths.MiscCCF)) Directory.Delete(Paths.MiscCCF, true);

            // Data.ccf
            // ****************
            Content5.ReplaceFile(Content5.GetNodeIndex("data.ccf"), RunCCFArc(Paths.DataCCF, UseLegacy ? CCFArc.Legacy : CCFArc.Normal));

            if (Directory.Exists(Paths.DataCCF)) Directory.Delete(Paths.DataCCF, true);
            if (File.Exists(Paths.WorkingFolder + "data.ccf")) File.Delete(Paths.WorkingFolder + "data.ccf");
        }

        public void ReplaceROM(string ROM)
        {
            // -----------------------
            // Check if raw ROM exists
            // -----------------------
            if (!File.Exists(ROM))
                throw new FileNotFoundException(new FileNotFoundException().Message, ROM);

            ReplaceROM(File.ReadAllBytes(ROM));
        }

        public void ReplaceROM(byte[] ROMbytes)
        {
            foreach (var item in Directory.EnumerateFiles(Paths.DataCCF))
                if (Path.GetExtension(item).ToLower() == ".sgd" || Path.GetExtension(item).ToLower() == ".sms")
                {
                    ROMName = Path.GetFileName(item).Replace("__", "");
                    File.WriteAllBytes(item, ROMbytes);
                }
        }

        public void InsertSaveData(string text, TitleImage img)
        {
            foreach (var item in Directory.EnumerateFiles(Paths.MiscCCF))
            {
                if (Path.GetFileName(item).ToLower().Contains("banner.cfg"))
                {
                    string[] newBanner = File.ReadAllLines(item, Encoding.BigEndianUnicode);
                    for (int i = 4; i < newBanner.Length; i++)
                        if (newBanner[i][2] == ':') newBanner[i] = newBanner[i].Substring(0, 3) + text;

                    File.WriteAllLines(item, newBanner, Encoding.BigEndianUnicode);
                }

                else if (Path.GetFileName(item).ToLower().Contains("comment"))
                {
                    string[] newComment = File.ReadAllLines(item, Encoding.UTF8);
                    newComment[0] = text;

                    File.WriteAllLines(item, newComment, Encoding.UTF8);
                }
            }
        }
    }
}