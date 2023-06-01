using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
{
    public class SEGA
    {
        /* What is needed:
           [ ] Relevant version functions
           Working:
           [X] Phantasy Star (v3) (customized)
           [X] Sonic the Hedgehog SMS (v2) (customized)
           [X] Phantasy Star (v3)
           [X] Sonic the Hedgehog SMS (v2)
        -----------------------------------
           [X] Comix Zone (v1) (customized)
           [X] Pulseman (v3) (customized)
           [ ] Comix Zone (v1)
           [X] Pulseman (v3) */

        // There is an issue with Ver 1 emulator, where if savedata is not customized, it will display a blank banner for savedata.
        // Unless savedata title is edited somehow, the WAD lands on a blackscreen.

        /*  Per RadioShadow on GBATemp:
         *  Version 1 [Sonic The Hedgehog]:
            - Analogue Stick doesn't work on Classic Controller/Gamecube.
            - Don't support PAL Component Fix.
            - Not all have 6 button controller support.
            - The Save Banner is called "banner_xx.wte" (xx = region)
            - In the "misc_ccf_zlib" folder is a file calleed "wii_vc.txt". The necessary text messages for the necessary languages.

            Version 2 [Streets of Rage 2] (the one this guide refers to):
            - Analogue Stick works on Classic Controller/Gamecube.
            - Supports PAL Component Fix.
            - All have 6 button controller support.
            - The Save Banner is now just called "banner.wte"
            - No "wii_vc.txt" file in the "misc_ccf_zlib" folder. The text message is in the emulator instead.
            - Some contain a "patch" file (may or may not have data in). If a certain rom is detected, then changes are made to the emulator (like skipping the checksum). An injected rom won't use the patch.

            Version 3 [Pulseman] (US/EU):
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

        /* 
        console.master_volume="+11.0"
        console.rapidfire="10"
        console.volume="10"*/

        public string ROM { get; set; }
        public int ver { get; set; }
        public bool SMS { get; set; }
        public string origROM { get; set; }

        public void GetCCF(bool includeMisc)
        {
            Directory.CreateDirectory(Paths.WorkingFolder_DataCCF);

            // Data.CCF
            CCFEx(Paths.WorkingFolder_Content5 + "data.ccf", Paths.WorkingFolder_DataCCF, false);
            if (Directory.EnumerateFiles(Paths.WorkingFolder_DataCCF).Count() < 3 ||
                (File.Exists(Paths.WorkingFolder_DataCCF + "Opera.arc") && File.ReadAllBytes(Paths.WorkingFolder_DataCCF + "Opera.arc").Length == 0))
            {
                Directory.Delete(Paths.WorkingFolder_DataCCF, true);
                Directory.CreateDirectory(Paths.WorkingFolder_DataCCF);
                CCFEx(Paths.WorkingFolder_Content5 + "data.ccf", Paths.WorkingFolder_DataCCF);
            }

            if (includeMisc)
            {
                Directory.CreateDirectory(Paths.WorkingFolder_MiscCCF);

                // Misc.CCF
                CCFEx(Paths.WorkingFolder_DataCCF + "misc.ccf", Paths.WorkingFolder_MiscCCF, false);
            }
        }

        public void PackCCF(bool includeMisc)
        {
            if (includeMisc)
            {
                // Misc.CCF
                string newMisc = CCFArc(Paths.WorkingFolder_MiscCCF, true);
                File.Copy(newMisc, Paths.WorkingFolder_DataCCF + "misc.ccf", true);
                Directory.Delete(Paths.WorkingFolder_MiscCCF, true);
            }

            // Data.CCF
            string newData = CCFArc(Paths.WorkingFolder_DataCCF, false);
            File.Copy(newData, Paths.WorkingFolder_Content5 + "data.ccf", true);
            Directory.Delete(Paths.WorkingFolder_DataCCF, true);
        }

        /// <param name="legacy">Defaults to legacy app by default, since the forked version does relatively bad with SMS WADs</param>
        internal void CCFEx(string file, string dir, bool legacy = true)
        {
            // Copy application to target dir
            string pPath = Paths.Apps + "ccftools\\" + (legacy ? "ccfex.exe" : "ccfex2021.exe");

            // Start application
            var pInfo = new ProcessStartInfo
            {
                FileName = pPath,
                WorkingDirectory = dir,
                Arguments = $"\"{file}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process p = Process.Start(pInfo))
                p.WaitForExit();
        }

        /// <param name="raw">Determines whether to use ccfarcraw.exe (needed for misc.ccf)</param>
        /// <returns>Path to an out.ccf file if it was created.</returns>
        internal string CCFArc(string dir, bool raw)
        {
            string pPath = Paths.Apps + "ccftools\\" + (raw ? "ccfarcraw.exe" : "ccfarc2021.exe");

            if (dir == Paths.WorkingFolder_DataCCF)
            {
                string files = origROM != null ? $"Opera.arc {origROM}" : "Opera.arc";
                foreach (var item in Directory.EnumerateFiles(dir))
                    if (!files.Contains(Path.GetFileName(item)))
                        files += $" {Path.GetFileName(item)}";


                // Start application
                var pInfo = new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = dir,
                    Arguments = files,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                if (!SMS && ver < 3)
                {
                    using (Process p = Process.Start(pInfo))
                        p.WaitForExit();
                    pInfo.Arguments = files.Replace($"Opera.arc {origROM} ", $"{origROM} Opera.arc ");
                }
                else if (SMS && ver == 3)
                {
                    // This configuration appears to fix a halt error screen when loading the WAD ("/data/selectmenu.rso: read_rsofile failed")
                    pInfo.Arguments = pInfo.Arguments.Replace("selectmenu.cat selectmenu.conf selectmenu.rso se_vc.rso", "se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso");
                }

                using (Process p = Process.Start(pInfo))
                    p.WaitForExit();
            }
            else
            {
                string files = "";
                foreach (var item in Directory.EnumerateFiles(dir))
                    if (!files.Contains(Path.GetFileName(item)))
                        files += $" {Path.GetFileName(item)}";
                files = files.Remove(0, 1);

                // Start application
                var pInfo = new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = dir,
                    Arguments = files,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process p = Process.Start(pInfo))
                    p.WaitForExit();
            }

            if (File.Exists(dir + "out.ccf") && File.ReadAllBytes(dir + "out.ccf").Length > 0)
                return dir + "out.ccf";
            else throw new Exception(Program.Language.Get("m016"));
        }

        public void ReplaceROM()
        {
            foreach (var item in Directory.EnumerateFiles(Paths.WorkingFolder_DataCCF))
            {
                if (Path.GetFileName(item).Contains(".SGD") || Path.GetFileName(item).Contains(".SMS") ||
                    (Path.GetFileName(item).Contains(origROM.Substring(0, 10)) && !string.IsNullOrWhiteSpace(origROM)))
                {
                    if (File.ReadAllBytes(ROM).Length > File.ReadAllBytes(item).Length)
                        throw new Exception(Program.Language.Get("m004"));

                    File.Delete(item);
                    string file = item;
                    if (!(file.Contains(".SGD") || file.Contains(".SMS"))) file = Paths.WorkingFolder_DataCCF + origROM;

                    if (!SMS)
                    {
                        // Convert to BIN and move
                        File.Copy(ROM, $"{Paths.Apps}ucon64\\rom");
                        using (Process p = Process.Start(new ProcessStartInfo
                        {
                            FileName = $"{Paths.Apps}ucon64\\ucon64.exe",
                            WorkingDirectory = $"{Paths.Apps}ucon64\\",
                            Arguments = $"--gen --bin \"{Paths.Apps}ucon64\\rom\" \"rom.bin\"",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }))
                            p.WaitForExit();
                        File.Delete($"{Paths.Apps}ucon64\\rom");
                        if (File.Exists($"{Paths.Apps}ucon64\\rom.bak")) File.Delete($"{Paths.Apps}ucon64\\rom.bak");

                        File.Move($"{Paths.Apps}ucon64\\rom.bin", file);

                        // Check filesize limit
                        if (File.ReadAllBytes(file).Length > 5.25 * 1024 * 1024)
                            throw new Exception(Program.Language.Get("m018"));
                    }
                    else
                    {
                        // Check filesize limit
                        if (File.ReadAllBytes(ROM).Length > 524288)
                            throw new Exception(Program.Language.Get("m018"));

                        File.Copy(ROM, file, true);
                    }
                    return;
                }
            }

            throw new Exception(Program.Language.Get("m010"));
        }

        public void ReplaceConfig(string[] config)
        {
            var c = new List<string>();
            foreach (var item in config)
                c.Add(item);
            c.Add($"romfile=\"{origROM}\"");

            // Automatically determines which string to add depending on revision
            foreach (var item in File.ReadAllLines(Paths.WorkingFolder_DataCCF + "config"))
                if (item.StartsWith("country="))
                    for (int i = 0; i < c.Count; i++)
                        if (c[i].Contains("console.machine_country"))
                            c[i] = c[i].Replace("console.machine_country", "country");

            // Determine version 3 and add modules used to avoid any possible issues otherwise
            if (ver == 3)
                foreach (var item in File.ReadAllLines(Paths.WorkingFolder_DataCCF + "config"))
                    if (item.StartsWith("modules=")
                     || item.StartsWith("snd.snddrv=")
                     || item.StartsWith("console.machine_arch=")
                     || item.Contains(".master_volume")
                     || item.Contains(".rapidfire")
                     || item.Contains(".volume"))
                        c.Add(item);

            c.Sort();

            using (TextWriter t = new StreamWriter(Paths.WorkingFolder_DataCCF + "config", false))
            {
                t.NewLine = "\n";
                for (int i = 0; i < c.Count; i++)
                    t.WriteLine(c.ToArray()[i]);
            }
        }

        // ************************************************************************************************ //
        internal void InsertSaveTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) title = "Untitled";

            if (File.Exists(Paths.WorkingFolder_MiscCCF + "banner.cfg.txt"))
            {
                var new_banner = File.ReadAllLines(Paths.WorkingFolder_MiscCCF + "banner.cfg.txt");
                for (int i = 0; i < new_banner.Length; i++)
                {
                    if (new_banner[i].StartsWith("JP:")) new_banner[i] = $"JP:{title}";
                    if (new_banner[i].StartsWith("EN:")) new_banner[i] = $"EN:{title}";
                    if (new_banner[i].StartsWith("GE:")) new_banner[i] = $"GE:{title}";
                    if (new_banner[i].StartsWith("FR:")) new_banner[i] = $"FR:{title}";
                    if (new_banner[i].StartsWith("SP:")) new_banner[i] = $"SP:{title}";
                    if (new_banner[i].StartsWith("IT:")) new_banner[i] = $"IT:{title}";
                    if (new_banner[i].StartsWith("DU:")) new_banner[i] = $"DU:{title}";
                }
                File.WriteAllText(Paths.WorkingFolder_MiscCCF + "banner.cfg.txt", String.Join("\n", new_banner), Encoding.BigEndianUnicode);
            }

            if (File.Exists(Paths.WorkingFolder_MiscCCF + "comment"))
            {
                var new_banner = File.ReadAllLines(Paths.WorkingFolder_MiscCCF + "comment");
                new_banner[0] = title;

                using (TextWriter t = new StreamWriter(Paths.WorkingFolder_MiscCCF + "comment", false))
                {
                    t.NewLine = "\n";
                    for (int i = 0; i < new_banner.Length; i++)
                        t.WriteLine(new_banner[i]);
                }
            }
        }
    }
}