using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriishProduce.WiiVC
{
    public class SEGA : InjectorWiiVC
    {
        public enum Type
        {
            Rev1 = 1,       // Comix Zone (SMD)

            // There is an issue with Ver 1 emulator, where if savedata is not customized, it will display a blank banner for savedata.
            // Unless savedata title is edited somehow, the WAD lands on a blackscreen. This GUI skips the bug since the savedata will be modified in every scenario

            // EXAMPLE Config:
            // **********************
            /* country="us"
             * romfile="ComixZone_USA.SGD"
             * dev.mdpad.enable_6b="1" */

            Rev2,           // Sonic (SMS); Earthworm Jim, Sonic 3D Blast (SMD)

            // EXAMPLE Config:
            // **********************
            /* country="us"
             * romfile="ComixZone_USA.SGD"
             * dev.mdpad.enable_6b="1" */

            Rev3            // Phantasy Star (SMS); Pulseman (SMD); Sonic & Knuckles (special edition)
        }

        private readonly string[] CCFApps = new string[]
        {
            "ccfex",        // 0 = New CCF ext
            "ccfex2009",    // 1 = Old CCF ext
            "ccfarc",       // 2 = New CCF arc
            "ccfarc2009",   // 3 = Old CCF arc
            "ccfarcraw"     // 4 = Raw CCF arc
        };

        public bool IsSMS { get; set; }
        private string ROMName { get; set; }

        protected override void Load()
        {
            MainContentIndex = 5;
            NeedsManualLoaded = false;
            base.Load();

            GetCCF();
            ReplaceManual(MainContent);

            switch (WAD.UpperTitleID.Substring(0, 3).ToUpper())
            {
                default:
                case "MAP": // SMD
                    EmuType = 1;
                    break;

                case "LAG": // SMS
                // ---------------
                case "MA6": // SMD
                case "MCP":
                    EmuType = 2;
                    break;

                case "LAD": // SMS - Phantasy Star uses rev2 in Japanese region
                    EmuType = WAD.UpperTitleID.ToUpper()[3] == 'J' ? 2 : 3;
                    break;

                case "MBA": // SMD
                case "MC2":
                    EmuType = 3;
                    break;
            }
        }

        #region CCF Tools
        // The trick to actually getting the CCF to work is this:
        // ****************
        // 1. Use the old CCFex to extract (https://www.romhacking.net/utilities/651/)
        // 2. Pack using the new CCFarc (no raw) (https://github.com/libertyernie/ccf-tools)
        //
        // - A specific file order is needed, the Opera.arc and/or ROM file first, then all other files in alphabetical order
        // - If se_vc.rso is listed after Select Menu files, it will display an error screen and crash, so I used OrderBy to return the correct file order
        // ****************
        // Anything else, and it will just do a black screen, or it will work for Master System only
        // ****************

        private void GetCCF(int CCFApp = 1)
        {
            if (IsSMS) CCFApp = 1;

            // Get Data.ccf first
            // ****************
            File.WriteAllBytes(Paths.WorkingFolder + "data.ccf", MainContent.Data[MainContent.GetNodeIndex("data.ccf")]);

            RunApp:
            RunCCFEx(Paths.WorkingFolder + "data.ccf", Paths.DataCCF, CCFApp);

            // Failsafe
            // ****************
            if ((Directory.EnumerateFiles(Paths.DataCCF).Count() < 3 ||
               (File.Exists(Paths.DataCCF + "Opera.arc") && File.ReadAllBytes(Paths.DataCCF + "Opera.arc").Length == 0)))
            {
                if (CCFApp != 1) { CCFApp = 1; goto RunApp; }
                else throw new Exception(Language.Get("Error002"));
            }

            // Get Misc.ccf
            // ****************
            RunCCFEx(Paths.DataCCF + "misc.ccf", Paths.MiscCCF, 0);

            // Get ROM filename
            // ****************
            foreach (var item in Directory.EnumerateFiles(Paths.DataCCF))
                if (Path.GetExtension(item).ToLower().StartsWith(".sgd") || Path.GetExtension(item).ToLower().StartsWith(".sms"))
                    ROMName = Path.GetFileName(item).Replace("__", "");
        }

        /// <summary>
        /// Extracts CCF to a specified location.
        /// </summary>
        /// <param name="legacy">Defaults to legacy app by default, since the forked version does relatively bad with SMS WADs</param>
        private void RunCCFEx(string file, string dir, int type)
        {
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            // Start application
            // ****************
            ProcessHelper.Run
            (
                Paths.Tools + $"sega\\{CCFApps[type]}.exe",
                dir,
                $"\"{file}\""
            );
        }

        /// <summary>
        /// Packs CCF to byte array.
        /// </summary>
        /// <param name="raw">Determines whether to use ccfarcraw.exe (needed for misc.ccf)</param>
        /// <returns>Path to an out.ccf file if it was created.</returns>
        private byte[] RunCCFArc(string dir, int type, bool AlternateMethod = false)
        {
            bool Failsafe = false;
            string files = "";

            if (dir == Paths.DataCCF)
            {
                if (!AlternateMethod)
                {
                    files = $"Opera.arc {ROMName}";
                    List<string> filesList = new List<string>();

                    // Use alternative order if the following bases are encountered
                    // ****************
                    string[] order_ROMNameFirst = new string[]
                    {
                        // --* SMS *-- //
                        "FantasyZone2",
                        "WBMonsterLand",
                        "PhantasyStar1_J_01",

                        // --* SMD *-- //
                        "Columns1",
                        "ComixZone",
                        "WBMonsterWorld",
                        "Columns3",
                        "StreetsRage2",
                        "StreetsRage3",
                        "DynamiteHeaddy",
                        "MonsterWorld4"
                    };

                    foreach (var item in order_ROMNameFirst)
                        if (ROMName.Contains(item)) files = $"{ROMName} Opera.arc";

                    // -----------------------------------------------------------------

                    string[] order_noROM = new string[]
                    {
                        // --* SMS *-- //
                        "SecretComm",

                        // --* SMD *-- //
                        "sandk_composite"
                    };

                    foreach (var item in order_noROM)
                        if (ROMName.Contains(item)) files = "Opera.arc";

                    // Add other files by alphabetical order
                    // ****************
                    foreach (var item in Directory.EnumerateFiles(dir))
                        if (!files.Contains(Path.GetFileName(item).Replace("__", "")))
                            filesList.Add(Path.GetFileName(item));

                    filesList = filesList.OrderBy(st => st.Replace('_', ' ')).ToList();
                    foreach (var item in filesList)
                        files += " " + item;
                }

                else
                {
                    #region [Alternative method if the above does not work]
                    // /!\ Uses specific strings for bases /!\ //
                    // -----------------------------------------------
                    switch (EmuType)
                    {
                        case 1:
                        case 2:
                            files = $"Opera.arc {ROMName} config man.arc misc.ccf";
                            break;

                        case 3:
                            if (IsSMS)
                                files = $"Opera.arc {ROMName} config emu_m68kbase.rso home.csv man.arc misc.ccf patch se_vc.rso sms.rso tsdevp.rso wii_vc.sel";
                            else
                                files = $"Opera.arc {ROMName} config emu_m68kbase.rso home.csv man.arc md.rso misc.ccf patch se_vc.rso tsdevp.rso wii_vc.sel";
                            break;
                    }

                    // Check for missing files
                    // ****************
                    foreach (var item in Directory.EnumerateFiles(dir))
                    {
                        // General
                        // ****************
                        if (Path.GetFileName(item).ToLower() == "patch" && !files.Contains(" patch"))
                            files = files.Replace("misc.ccf", "misc.ccf patch");
                        if (Path.GetFileName(item).ToLower() == "home.csv" && !files.Contains("home.csv"))
                            files = files.Replace("man.arc", "home.csv man.arc");

                        // Specific console files
                        // ****************
                        if (Path.GetFileName(item).ToLower() == "smsui.rso" && !files.Contains("smsui.rso"))
                            files = files.Replace("sms.rso", "sms.rso smsui.rso");

                        // Select Menu
                        // ****************
                        if (Path.GetFileName(item).ToLower() == "selectmenu.cat" && !files.Contains("selectmenu.cat") && files.Contains("se_vc.rso"))
                            files = files.Replace("se_vc.rso", "se_vc.rso selectmenu.cat");
                        if (Path.GetFileName(item).ToLower() == "selectmenu.conf" && files.Contains("selectmenu.cat"))
                            files = files.Replace("selectmenu.cat", "selectmenu.cat selectmenu.conf");
                        if (Path.GetFileName(item).ToLower() == "selectmenu.rso" && files.Contains("selectmenu.conf"))
                            files = files.Replace("selectmenu.conf", "selectmenu.conf selectmenu.rso");
                    }

                    IDictionary<string, string> FilesLists = new Dictionary<string, string>()
                    {
                        { "Columns1",       $"{ROMName} Opera.arc config man.arc misc.ccf" },
                        { "ComixZone",      $"{ROMName} Opera.arc config man.arc misc.ccf" },
                        { "WBMonsterWorld", $"{ROMName} Opera.arc config man.arc misc.ccf" },
                        { "Columns3",       $"{ROMName} Opera.arc config home.csv man.arc misc.ccf patch"},
                        { "BareKnuckle2",   $"{ROMName} Opera.arc config home.csv man.arc misc.ccf" },
                        { "StreetsRage2",   $"{ROMName} Opera.arc config home.csv man.arc misc.ccf" },
                        { "StreetsRage3",   $"{ROMName} Opera.arc config home.csv man.arc misc.ccf" },
                        { "DynamiteHeaddy", $"{ROMName} Opera.arc config home.csv man.arc misc.ccf" },
                        { "MonsterWorld4",  $"{ROMName} Opera.arc config emu_m68kbase.rso home.csv man.arc md.rso misc.ccf patch se_vc.rso selectmenu.cat selectmenu.conf tsdevp.rso wii_vc.sel" },
                        { "SonicKnuckles",  $"Opera.arc config emu_m68kbase.rso home.csv man.arc md.rso misc.ccf patch {ROMName} sandkui.rso se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso tsdevp.rso wii_vc.sel" }
                    };

                    foreach (var item in FilesLists)
                        if (ROMName.Contains(item.Key)) files = item.Value;

                    if (ROMName.Contains("SonicKnuckles") || ROMName.Contains("sandk_"))
                        files = $"Opera.arc config emu_m68kbase.rso home.csv man.arc md.rso misc.ccf patch {ROMName} sandkui.rso se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso tsdevp.rso wii_vc.sel";
                    #endregion
                }

                // Check if ROM filename contains underscores
                // ****************
                foreach (var item in Directory.EnumerateFiles(dir))
                {
                    if (Path.GetFileName(item).Contains(ROMName))
                        files = files.Replace(ROMName, Path.GetFileName(item));
                }
            }
            else
            {
                foreach (var item in Directory.EnumerateFiles(dir))
                    if (!files.Contains(Path.GetFileName(item)))
                        files += $" {Path.GetFileName(item)}";
                files = files.Remove(0, 1);
            }

            RunApp:
            // If not data.ccf or already using legacy app, there is no other option
            // ****************
            if (type < 2) throw new InvalidOperationException();
            else if (type >= 3 && Failsafe) throw new Exception(Language.Get("Error002"));

            // Start application
            // ****************
            ProcessHelper.Run
            (
                Paths.Tools + $"sega\\{CCFApps[type]}.exe",
                dir,
                files
            );

            // Failsafe
            // ****************
            if (File.Exists(dir + "out.ccf") && File.ReadAllBytes(dir + "out.ccf").Length > 0)
            {
                var bytes = File.ReadAllBytes(dir + "out.ccf");
                File.Delete(dir + "out.ccf");
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                return bytes;
            }

            if (!Failsafe) { Failsafe = true; goto RunApp; }
            else throw new Exception(Language.Get("Error002"));
        }
        #endregion


        public override WAD Write()
        {
            if (!Directory.Exists(Paths.DataCCF)) return WAD;

            // Misc.ccf
            // ****************
            File.WriteAllBytes(Paths.DataCCF + "misc.ccf", RunCCFArc(Paths.MiscCCF, 4));

            // Data.ccf
            // ****************
            var NewData = RunCCFArc(Paths.DataCCF, ROMName.Contains("MonsterWorld4") ? 3 : 2);
            MainContent.ReplaceFile(MainContent.GetNodeIndex("data.ccf"), NewData);

            if (File.Exists(Paths.WorkingFolder + "data.ccf")) File.Delete(Paths.WorkingFolder + "data.ccf");

            return base.Write();
        }

        protected override void ReplaceROM()
        {
            // -----------------------
            // Check filesize of input ROM
            // Maximum ROM limit allowed: 5.25 MB for Gen/MD or 512 KB for Master System
            // -----------------------
            ROM.CheckSize();

            foreach (var item in Directory.EnumerateFiles(Paths.DataCCF))
                if (Path.GetExtension(item).ToLower().StartsWith(".sgd") || Path.GetExtension(item).ToLower().StartsWith(".sms"))
                    File.WriteAllBytes(item, ROM.Bytes);
        }

        protected override void ReplaceSaveData(string[] lines, TitleImage tImg)
        {
            // -----------------------
            // COMMENT
            // -----------------------

            foreach (var item in Directory.EnumerateFiles(Paths.MiscCCF))
            {
                if (Path.GetFileName(item).ToLower().Contains("banner.cfg"))
                {
                    string[] newBanner = File.ReadAllLines(item);
                    for (int i = 4; i < newBanner.Length; i++)
                    {
                        if (newBanner[i].StartsWith("JP:")) newBanner[i] = $"JP:{lines[0]}";
                        if (newBanner[i].StartsWith("EN:")) newBanner[i] = $"EN:{lines[0]}";
                        if (newBanner[i].StartsWith("GE:")) newBanner[i] = $"GE:{lines[0]}";
                        if (newBanner[i].StartsWith("FR:")) newBanner[i] = $"FR:{lines[0]}";
                        if (newBanner[i].StartsWith("SP:")) newBanner[i] = $"SP:{lines[0]}";
                        if (newBanner[i].StartsWith("IT:")) newBanner[i] = $"IT:{lines[0]}";
                        if (newBanner[i].StartsWith("DU:")) newBanner[i] = $"DU:{lines[0]}";
                    }

                    File.WriteAllText(item, string.Join("\n", newBanner), Encoding.BigEndianUnicode);
                }

                else if (Path.GetFileName(item).ToLower().Contains("comment"))
                {
                    string[] newComment = File.ReadAllLines(item, Encoding.UTF8);
                    newComment[0] = lines[0];

                    using (TextWriter t = new StreamWriter(item, false))
                    {
                        t.NewLine = "\n";
                        for (int i = 0; i < newComment.Length; i++)
                            t.WriteLine(newComment[i]);
                    }
                }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            tImg.ReplaceSaveWTE();
        }

        protected override void ModifyEmulatorSettings()
        {
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

            foreach (var item in Directory.EnumerateFiles(Paths.DataCCF))
            {
                if (Path.GetFileName(item).ToLower() == "config")
                {
                    IDictionary<string, string> Settings = this.Settings;

                    switch (IsSMS)
                    {
                        case true:
                            foreach (var line in Settings.Keys.ToArray())
                                if (line == "dev.mdpad.enable_6b") Settings.Remove(line);
                            break;

                        case false:
                            foreach (var line in Settings.Keys.ToArray())
                                if (line == "smsui.has_opll") Settings.Remove(line);
                            break;
                    }

                    List<string> newConfig = new List<string>();
                    List<string> alreadyAdded = new List<string>();
                    var oldConfig = File.ReadAllLines(item);

                    Dictionary<string, string> Alt = new Dictionary<string, string>
                        {
                            // Settings that are known by alternative names in newer revisions (old name on left):
                            { "country", "console.machine_country" },
                            { "console.volume", "console.master_volume" }
                        };

                    for (int i = 0; i < Alt.Count; i++)
                        foreach (var newLine in Settings)
                            if (newLine.Key == Alt.ElementAt(i).Key)
                                foreach (var line in oldConfig)
                                    if (line.StartsWith(Alt.ElementAt(i).Value) && Alt.ElementAt(i).Value != newLine.Key && Settings.ContainsKey(newLine.Key))
                                    {
                                        newConfig.Add($"{newLine.Key}=\"{newLine.Value}\"");
                                        alreadyAdded.Add(Alt.ElementAt(i).Value);
                                    }

                    foreach (var line in oldConfig)
                        foreach (var name in new string[]
                        {
                            // Settings that should NOT be changed:
                            "console.machine_arch",
                            "console.volume",
                            "console.cl_bindings",
                            "console.core_bindings",
                            "console.gc_bindings",
                            "modules",
                            "romfile",
                            "snd.snddrv",
                            "smsui.has_opll",
                        })
                            if (line.StartsWith(name)) { newConfig.Add(line); alreadyAdded.Add(line); }

                    foreach (var newLine in Settings)
                    {
                        bool doNotAdd = false;
                        foreach (var added in alreadyAdded.ToArray())
                            if (newLine.Key == added) { doNotAdd = true; break; }

                        if (!doNotAdd)
                        {
                            newConfig.Add($"{newLine.Key}=\"{newLine.Value}\"");
                            alreadyAdded.Add(newLine.Key);
                        }
                    }

                    File.WriteAllLines(item, newConfig);
                }
            }
        }
    }
}