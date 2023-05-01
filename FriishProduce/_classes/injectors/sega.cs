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
        /* What is currently working: 
           [X] CCF (de)compression
           [X] ROM replacement & detection
           [X] Config writing 
           [ ] Relevant version functions */

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

        public string ROM { get; set; }
        public int ver { get; set; }
        private List<string> config = new List<string>();
        public bool SMS { get; set; }

        public void GetCCF()
        {
            Directory.CreateDirectory(Paths.WorkingFolder_MiscCCF);

            // Copy application to target dir
            string pPath = Paths.WorkingFolder_DataCCF + "ccfex.exe";
            File.WriteAllBytes(pPath, Properties.Resources.CCFEx);

            // Start application
            var pInfo = new ProcessStartInfo
            {
                FileName = pPath,
                WorkingDirectory = Paths.WorkingFolder_DataCCF,
                Arguments = $"{Paths.WorkingFolder_Content5}data.ccf",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process p = Process.Start(pInfo))
                p.WaitForExit();

            // Start application again
            pInfo.WorkingDirectory = Paths.WorkingFolder_MiscCCF;
            pInfo.Arguments = $"{Paths.WorkingFolder_DataCCF}misc.ccf";
            using (Process p = Process.Start(pInfo))
                p.WaitForExit();

            File.Delete(pPath);
        }

        public void PackCCF()
        {
            // Define files
            string arg = "";
            foreach (string file in Directory.EnumerateFiles(Paths.WorkingFolder_MiscCCF))
                arg += $" {Path.GetFileName(file)}";

            // Copy application to target dir
            string pPath = Paths.WorkingFolder_MiscCCF + "ccfarcraw.exe";
            File.WriteAllBytes(pPath, Properties.Resources.CCFArcRaw);

            // Start application
            var pInfo = new ProcessStartInfo
            {
                FileName = pPath,
                WorkingDirectory = Paths.WorkingFolder_MiscCCF,
                Arguments = arg.Remove(0, 1),
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Minimized
            };
            using (Process p = Process.Start(pInfo))
                p.WaitForExit();

            File.Delete(pPath);
            File.Copy(Paths.WorkingFolder_MiscCCF + "out.ccf", Paths.WorkingFolder_DataCCF + "misc.ccf", true);
            Directory.Delete(Paths.WorkingFolder_MiscCCF, true);

            // Define files
            arg = "";
            foreach (string file in Directory.EnumerateFiles(Paths.WorkingFolder_DataCCF))
                arg += $" {Path.GetFileName(file)}";

            // Copy application to target dir
            pPath = Paths.WorkingFolder_DataCCF + "ccfarc.exe";
            File.WriteAllBytes(pPath, Properties.Resources.CCFArc);

            // Start application
            pInfo.FileName = pPath;
            pInfo.WorkingDirectory = Paths.WorkingFolder_DataCCF;
            pInfo.Arguments = arg.Remove(0, 1);
            using (Process p = Process.Start(pInfo))
                p.WaitForExit();

            File.Delete(pPath);
            File.Copy(Paths.WorkingFolder_DataCCF + "out.ccf", Paths.WorkingFolder_Content5 + "data.ccf", true);
            Directory.Delete(Paths.WorkingFolder_DataCCF, true);
        }

        public void ReplaceROM()
        {
            string targetROM = "";
            foreach (var item in Directory.EnumerateFiles(Paths.WorkingFolder_DataCCF))
            {
                if (item.Contains(".SGD") || item.Contains(".SMS"))
                {
                    if (File.ReadAllBytes(ROM).Length > File.ReadAllBytes(item).Length)
                        throw new Exception(Program.Language.Get("m004"));

                    targetROM = item;
                    File.Delete(item);

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

                        File.Move($"{Paths.Apps}ucon64\\rom.bin", targetROM);
                    }
                    else
                    {
                        File.Copy(ROM, targetROM);
                    }

                    string ROMfile = Path.GetFileNameWithoutExtension(targetROM).Replace(".SGD", "").Replace(".SMS", "") + (SMS ? ".SMS" : ".SGD");
                    config.Add($"romfile=\"{ROMfile}\"");
                    return;
                }
            }

            throw new Exception(Program.Language.Get("m010"));
        }

        public void ReplaceConfig()
        {
            // Determine version 3 and add modules used to avoid any possible issues otherwise
            if (ver == 3)
                foreach (var item in File.ReadAllLines(Paths.WorkingFolder_DataCCF + "config.txt"))
                    if (item.StartsWith("modules=") || item.StartsWith("snd.snddrv="))
                        config.Add(item);

            config.Sort();
            File.WriteAllLines(Paths.WorkingFolder_DataCCF + "config", config.ToArray());
        }

        public void SetRegion(string reg) => config.Add(ver == 1 ? $"country=\"{reg}\"" : $"console.machine_country=\"{reg}\"");

        public void SRAM() => config.Add($"save_sram=\"1\"");

        public void MDPad_6B() => config.Add($"dev.mdpad.enable_6b=\"1\"");

        /// <summary>
        /// Version 3 functions
        /// </summary>
        /// <param name="btns"></param>
        internal void SetController(Dictionary<string, string> btns)
        {
            string coreBtns = "console.core_bindings=\"up=:down=:left=:right=:+=:a=:1=:2=:b=\"";
            string clBtns = "console.cl_bindings=\"up=:down=:left=:right=:+=:y=:b=:a=:l=:x=:r=:zr=:zl=\"";
            string gcBtns = "console.gc_bindings=\"up=:down=:left=:right=:start=:b=:a=:x=:l=:y=:r=:z=:c=\"";

            List<string> keymap = new List<string>();
            foreach (KeyValuePair<string, string> item in btns)
            {
                // --------------------------------------------------
                // Wii Remote Configurations
                // --------------------------------------------------
                if (item.Key == "core_up" && item.Value != "—")
                    coreBtns = coreBtns.Replace("up=:", $"up={item.Value[0]}:");
                if (item.Key == "core_down" && item.Value != "—")
                    coreBtns = coreBtns.Replace("down=:", $"down={item.Value[0]}:");
                if (item.Key == "core_left" && item.Value != "—")
                    coreBtns = coreBtns.Replace("left=:", $"left={item.Value[0]}:");
                if (item.Key == "core_right" && item.Value != "—")
                    coreBtns = coreBtns.Replace("right=:", $"right={item.Value[0]}:");
                if (item.Key == "core_+" && item.Value != "—")
                    coreBtns = coreBtns.Replace("+=:", $"+={item.Value[0]}:");
                if (item.Key == "core_-" && item.Value != "—")
                    coreBtns = coreBtns.Replace("-=:", $"-={item.Value[0]}:");
                if (item.Key == "core_a" && item.Value != "—")
                    coreBtns = coreBtns.Replace("a=:", $"a={item.Value[0]}:");
                if (item.Key == "core_1" && item.Value != "—")
                    coreBtns = coreBtns.Replace("1=:", $"1={item.Value[0]}:");
                if (item.Key == "core_2" && item.Value != "—")
                    coreBtns = coreBtns.Replace("2=:", $"2={item.Value[0]}:");
                if (item.Key == "core_b" && item.Value != "—")
                    coreBtns = coreBtns.Replace("b=:", $"b={item.Value[0]}:");

                // --------------------------------------------------
                // Wii Classic Controller Configurations
                // --------------------------------------------------
                if (item.Key == "cl_up" && item.Value != "—")
                    clBtns = clBtns.Replace("up=:", $"up={item.Value[0]}:");
                if (item.Key == "cl_down" && item.Value != "—")
                    clBtns = clBtns.Replace("down=:", $"down={item.Value[0]}:");
                if (item.Key == "cl_left" && item.Value != "—")
                    clBtns = clBtns.Replace("left=:", $"left={item.Value[0]}:");
                if (item.Key == "cl_right" && item.Value != "—")
                    clBtns = clBtns.Replace("right=:", $"right={item.Value[0]}:");
                if (item.Key == "cl_+" && item.Value != "—")
                    clBtns = clBtns.Replace("+=:", $"+={item.Value[0]}:");
                if (item.Key == "cl_-" && item.Value != "—")
                    clBtns = clBtns.Replace("-=:", $"-={item.Value[0]}:");
                if (item.Key == "cl_y" && item.Value != "—")
                    clBtns = clBtns.Replace("y=:", $"y={item.Value[0]}:");
                if (item.Key == "cl_b" && item.Value != "—")
                    clBtns = clBtns.Replace("b=:", $"b={item.Value[0]}:");
                if (item.Key == "cl_a" && item.Value != "—")
                    clBtns = clBtns.Replace("a=:", $"a={item.Value[0]}:");
                if (item.Key == "cl_l" && item.Value != "—")
                    clBtns = clBtns.Replace("l=:", $"l={item.Value[0]}:");
                if (item.Key == "cl_x" && item.Value != "—")
                    clBtns = clBtns.Replace("x=:", $"x={item.Value[0]}:");
                if (item.Key == "cl_r" && item.Value != "—")
                    clBtns = clBtns.Replace("r=:", $"r={item.Value[0]}:");
                if (item.Key == "cl_zl" && item.Value != "—")
                    clBtns = clBtns.Replace("zl=:", $"zl={item.Value[0]}:");
                if (item.Key == "cl_zr" && item.Value != "—")
                    clBtns = clBtns.Replace("zr=:", $"zr={item.Value[0]}:");

                // --------------------------------------------------
                // Nintendo GameCube Controller Configurations
                // --------------------------------------------------
                if (item.Key == "gc_up" && item.Value != "—")
                    gcBtns = gcBtns.Replace("up=:", $"up={item.Value[0]}:");
                if (item.Key == "gc_down" && item.Value != "—")
                    gcBtns = gcBtns.Replace("down=:", $"down={item.Value[0]}:");
                if (item.Key == "gc_left" && item.Value != "—")
                    gcBtns = gcBtns.Replace("left=:", $"left={item.Value[0]}:");
                if (item.Key == "gc_right" && item.Value != "—")
                    gcBtns = gcBtns.Replace("right=:", $"right={item.Value[0]}:");
                if (item.Key == "gc_start" && item.Value != "—")
                    gcBtns = gcBtns.Replace("start=:", $"start={item.Value[0]}:");
                if (item.Key == "gc_b" && item.Value != "—")
                    gcBtns = gcBtns.Replace("b=:", $"b={item.Value[0]}:");
                if (item.Key == "gc_a" && item.Value != "—")
                    gcBtns = gcBtns.Replace("a=:", $"a={item.Value[0]}:");
                if (item.Key == "gc_x" && item.Value != "—")
                    gcBtns = gcBtns.Replace("x=:", $"x={item.Value[0]}:");
                if (item.Key == "gc_l" && item.Value != "—")
                    gcBtns = gcBtns.Replace("l=:", $"l={item.Value[0]}:");
                if (item.Key == "gc_y" && item.Value != "—")
                    gcBtns = gcBtns.Replace("y=:", $"y={item.Value[0]}:");
                if (item.Key == "gc_r" && item.Value != "—")
                    gcBtns = gcBtns.Replace("r=:", $"r={item.Value[0]}:");
                if (item.Key == "gc_z" && item.Value != "—")
                    gcBtns = gcBtns.Replace("z=:", $"z={item.Value[0]}:");
                if (item.Key == "gc_c" && item.Value != "—")
                    gcBtns = gcBtns.Replace("c=:", $"c={item.Value[0]}:");
            }

            config.Add(coreBtns);
            config.Add(clBtns);
            config.Add(gcBtns);
        }
    }
}
