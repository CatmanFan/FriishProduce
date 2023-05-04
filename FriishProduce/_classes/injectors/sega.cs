using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce.Injectors
{
    public class SEGA
    {
        /* What is needed:
           [ ] Relevant version functions */
        // Blackscreen when Customize is not enabled
        // If SaveData title is edited, then the WAD functions with no blackscreen

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
        console.disable_resetbutton="1"
        console.master_volume="+11.0"
        console.rapidfire="10"
        console.volume="10"
        disable_selectmenu="1"
        smsui.has_opll="1"*/

        public string ROM { get; set; }
        public int ver { get; set; }
        private List<string> config = new List<string>();
        public bool SMS { get; set; }
        public string origROM { get; set; }
        private bool CompressData = false;

        public void GetCCF()
        {
            // Data.CCF
            Directory.CreateDirectory(Paths.WorkingFolder_MiscCCF);
            CCFEx(Paths.WorkingFolder_Content5 + "data.ccf", Paths.WorkingFolder_DataCCF);

            // Repack in raw format and compare filesize to original
            string recomp = CCFArc(Paths.WorkingFolder_DataCCF, true);
            CompressData = Math.Round(File.ReadAllBytes(recomp).Length / 100d, 0) * 100
                > Math.Round(File.ReadAllBytes(Paths.WorkingFolder_Content5 + "data.ccf").Length / 100d, 0);
            File.Delete(recomp);

            // Misc.CCF
            CCFEx(Paths.WorkingFolder_DataCCF + "misc.ccf", Paths.WorkingFolder_MiscCCF);
        }

        public void PackCCF()
        {
            // Misc.CCF
            string newMisc = CCFArc(Paths.WorkingFolder_MiscCCF, true);
            File.Copy(newMisc, Paths.WorkingFolder_DataCCF + "misc.ccf", true);
            Directory.Delete(Paths.WorkingFolder_MiscCCF, true);

            // Data.CCF
            string newData = CCFArc(Paths.WorkingFolder_DataCCF, CompressData);
            File.Copy(newData, Paths.WorkingFolder_Content5 + "data.ccf", true);
            Directory.Delete(Paths.WorkingFolder_DataCCF, true);
        }

        private void CCFEx(string file, string dir)
        {
            // Copy application to target dir
            string pPath = Paths.Apps + "ccftools\\ccfex.exe";

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

        private string CCFArc(string dir, bool raw)
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

                if (!SMS)
                {
                    using (Process p = Process.Start(pInfo))
                        p.WaitForExit();
                    pInfo.Arguments = files.Replace($"Opera.arc {origROM} ", $"{origROM} Opera.arc ");
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
                    (Path.GetFileName(item).Contains(origROM.Substring(0,10)) && !string.IsNullOrWhiteSpace(origROM)))
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
                    }
                    else
                    {
                        File.Copy(ROM, file, true);
                    }

                    config.Add($"romfile=\"{origROM}\"");
                    return;
                }
            }

            throw new Exception(Program.Language.Get("m010"));
        }

        public void ReplaceConfig()
        {
            // Determine version 3 and add modules used to avoid any possible issues otherwise
            if (ver == 3)
                foreach (var item in File.ReadAllLines(Paths.WorkingFolder_DataCCF + "config"))
                    if (item.StartsWith("modules=")
                     || item.StartsWith("snd.snddrv=")
                     || item.StartsWith("console.machine_arch=")
                     || item.Contains("has_opll")
                     || item.Contains(".master_volume")
                     || item.Contains(".rapidfire")
                     || item.Contains(".volume"))
                        config.Add(item);

            config.Sort();

            using (TextWriter t = new StreamWriter(Paths.WorkingFolder_DataCCF + "config", false))
            {
                t.NewLine = "\n";
                for (int i = 0; i < config.Count; i++)
                    t.WriteLine(config.ToArray()[i]);
            }
        }

        public void SetRegion(string reg)
        {
            // Automatically determines which string to add depending on revision
            foreach (var item in File.ReadAllLines(Paths.WorkingFolder_DataCCF + "config"))
                if (item.StartsWith("country="))
                {
                    config.Add($"country=\"{reg}\"");
                    return;
                }

            config.Add($"console.machine_country=\"{reg}\"");
        }

        public void SRAM() => config.Add("save_sram=\"1\"");

        public void MDPad_6B() => config.Add("dev.mdpad.enable_6b=\"1\"");

        public void SetBrightness(int value) => config.Add($"console.brightness=\"{value}\"");

        /// <summary>
        /// Version 3 functions
        /// </summary>
        public void SetController(Dictionary<string, string> btns, bool retainOrig = false)
        {
            if (retainOrig)
            {
                foreach (var item in File.ReadAllLines(Paths.WorkingFolder_DataCCF + "config"))
                    if (item.Contains("_bindings="))
                        config.Add(item);
                return;
            }

            string coreBtns = "console.core_bindings=\"up=:down=:left=:right=:+=:-=:a=:1=:2=:b=\"";
            string clBtns = "console.cl_bindings=\"up=:down=:left=:right=:+=:-=:y=:b=:a=:l=:x=:r=:zr=:zl=\"";
            string gcBtns = "console.gc_bindings=\"up=:down=:left=:right=:start=:b=:a=:x=:l=:y=:r=:z=:c=\"";

            List<string> keymap = new List<string>();
            foreach (KeyValuePair<string, string> item in btns)
            {
                // --------------------------------------------------
                // Wii Remote Configurations
                // --------------------------------------------------
                if (item.Key == "core_up" && item.Value != "—")
                    coreBtns = coreBtns.Replace("up=", $"up={item.Value[0]}");
                if (item.Key == "core_down" && item.Value != "—")
                    coreBtns = coreBtns.Replace("down=", $"down={item.Value[0]}");
                if (item.Key == "core_left" && item.Value != "—")
                    coreBtns = coreBtns.Replace("left=", $"left={item.Value[0]}");
                if (item.Key == "core_right" && item.Value != "—")
                    coreBtns = coreBtns.Replace("right=", $"right={item.Value[0]}");
                if (item.Key == "core_+" && item.Value != "—")
                    coreBtns = coreBtns.Replace("+=", $"+={item.Value[0]}");
                if (item.Key == "core_-" && item.Value != "—")
                    coreBtns = coreBtns.Replace("-=", $"-={item.Value[0]}");
                if (item.Key == "core_a" && item.Value != "—")
                    coreBtns = coreBtns.Replace("a=", $"a={item.Value[0]}");
                if (item.Key == "core_1" && item.Value != "—")
                    coreBtns = coreBtns.Replace("1=", $"1={item.Value[0]}");
                if (item.Key == "core_2" && item.Value != "—")
                    coreBtns = coreBtns.Replace("2=", $"2={item.Value[0]}");
                if (item.Key == "core_b" && item.Value != "—")
                    coreBtns = coreBtns.Replace("b=", $"b={item.Value[0]}");

                // --------------------------------------------------
                // Wii Classic Controller Configurations
                // --------------------------------------------------
                if (item.Key == "cl_up" && item.Value != "—")
                    clBtns = clBtns.Replace("up=", $"up={item.Value[0]}");
                if (item.Key == "cl_down" && item.Value != "—")
                    clBtns = clBtns.Replace("down=", $"down={item.Value[0]}");
                if (item.Key == "cl_left" && item.Value != "—")
                    clBtns = clBtns.Replace("left=", $"left={item.Value[0]}");
                if (item.Key == "cl_right" && item.Value != "—")
                    clBtns = clBtns.Replace("right=", $"right={item.Value[0]}");
                if (item.Key == "cl_+" && item.Value != "—")
                    clBtns = clBtns.Replace("+=", $"+={item.Value[0]}");
                if (item.Key == "cl_-" && item.Value != "—")
                    clBtns = clBtns.Replace("-=", $"-={item.Value[0]}");
                if (item.Key == "cl_y" && item.Value != "—")
                    clBtns = clBtns.Replace("y=", $"y={item.Value[0]}");
                if (item.Key == "cl_b" && item.Value != "—")
                    clBtns = clBtns.Replace("b=", $"b={item.Value[0]}");
                if (item.Key == "cl_a" && item.Value != "—")
                    clBtns = clBtns.Replace("a=", $"a={item.Value[0]}");
                if (item.Key == "cl_l" && item.Value != "—")
                    clBtns = clBtns.Replace("l=", $"l={item.Value[0]}");
                if (item.Key == "cl_x" && item.Value != "—")
                    clBtns = clBtns.Replace("x=", $"x={item.Value[0]}");
                if (item.Key == "cl_r" && item.Value != "—")
                    clBtns = clBtns.Replace("r=", $"r={item.Value[0]}");
                if (item.Key == "cl_zl" && item.Value != "—")
                    clBtns = clBtns.Replace("zl=", $"zl={item.Value[0]}");
                if (item.Key == "cl_zr" && item.Value != "—")
                    clBtns = clBtns.Replace("zr=", $"zr={item.Value[0]}");

                // --------------------------------------------------
                // Nintendo GameCube Controller Configurations
                // --------------------------------------------------
                if (item.Key == "gc_up" && item.Value != "—")
                    gcBtns = gcBtns.Replace("up=", $"up={item.Value[0]}");
                if (item.Key == "gc_down" && item.Value != "—")
                    gcBtns = gcBtns.Replace("down=", $"down={item.Value[0]}");
                if (item.Key == "gc_left" && item.Value != "—")
                    gcBtns = gcBtns.Replace("left=", $"left={item.Value[0]}");
                if (item.Key == "gc_right" && item.Value != "—")
                    gcBtns = gcBtns.Replace("right=", $"right={item.Value[0]}");
                if (item.Key == "gc_start" && item.Value != "—")
                    gcBtns = gcBtns.Replace("start=", $"start={item.Value[0]}");
                if (item.Key == "gc_b" && item.Value != "—")
                    gcBtns = gcBtns.Replace("b=", $"b={item.Value[0]}");
                if (item.Key == "gc_a" && item.Value != "—")
                    gcBtns = gcBtns.Replace("a=", $"a={item.Value[0]}");
                if (item.Key == "gc_x" && item.Value != "—")
                    gcBtns = gcBtns.Replace("x=", $"x={item.Value[0]}");
                if (item.Key == "gc_l" && item.Value != "—")
                    gcBtns = gcBtns.Replace("l=", $"l={item.Value[0]}");
                if (item.Key == "gc_y" && item.Value != "—")
                    gcBtns = gcBtns.Replace("y=", $"y={item.Value[0]}");
                if (item.Key == "gc_r" && item.Value != "—")
                    gcBtns = gcBtns.Replace("r=", $"r={item.Value[0]}");
                if (item.Key == "gc_z" && item.Value != "—")
                    gcBtns = gcBtns.Replace("z=", $"z={item.Value[0]}");
                if (item.Key == "gc_c" && item.Value != "—")
                    gcBtns = gcBtns.Replace("c=", $"c={item.Value[0]}");
            }

            config.Add(coreBtns);
            config.Add(clBtns);
            config.Add(gcBtns);
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