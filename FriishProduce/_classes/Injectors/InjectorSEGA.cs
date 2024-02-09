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

        private readonly string[] CCFApps = new string[]
        {
            "ccfex",
            "ccfex2009",
            "ccfarc",
            "ccfarc2009",
            "ccfarcraw"
        };

        private int CCFApp { get; set; }

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

            CCFApp = 1;
            GetCCF();
        }

        #region External Tools
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
            Process.Run
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
        private byte[] RunCCFArc(string dir, int type)
        {
            bool Failsafe = false;
            string files = "";

            if (dir == Paths.DataCCF)
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
                    "BareKnuckle2",
                    "StreetsRage2",
                    "StreetsRage3",
                    "DynamiteHeaddy",
                    "MonsterWorld4"
                };
                foreach (var item in order_ROMNameFirst)
                    if (ROMName.Contains(item)) files = $"{ROMName} Opera.arc";

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

                #region [Alternative method if the above does not work]
                /* /!\ Uses specific strings for bases /!\
                 -----------------------------------------------
                switch (EmuType)
                {
                    case Type.Rev1:
                    case Type.Rev2:
                        files = $"Opera.arc {ROMName} config man.arc misc.ccf";
                        break;

                    case Type.Rev3:
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
                    files = $"Opera.arc config emu_m68kbase.rso home.csv man.arc md.rso misc.ccf patch {ROMName} sandkui.rso se_vc.rso selectmenu.cat selectmenu.conf selectmenu.rso tsdevp.rso wii_vc.sel"; */
                #endregion

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
            if (type != 0 && Failsafe) throw new Exception(Language.Get("Error002"));

            // Start application
            // ****************
            Process.Run
            (
                Paths.Tools + $"sega\\{CCFApps[type + 2]}.exe",
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

        private void GetCCF()
        {
            // Get Data.ccf first
            // ****************
            File.WriteAllBytes(Paths.WorkingFolder + "data.ccf", Content5.Data[Content5.GetNodeIndex("data.ccf")]);

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

        public void WriteCCF()
        {
            if (ROMName.Contains("MonsterWorld4")) CCFApp = 1;
            else CCFApp = 0;

            // Misc.ccf
            // ****************
            File.WriteAllBytes(Paths.DataCCF + "misc.ccf", RunCCFArc(Paths.MiscCCF, 2));

            // Data.ccf
            // ****************
            Content5.ReplaceFile(Content5.GetNodeIndex("data.ccf"), RunCCFArc(Paths.DataCCF, CCFApp));

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
            // -----------------------
            // Check filesize of input ROM
            // Maximum ROM limit allowed: 5.25 MB for Gen/MD or 512 KB for Master System
            // -----------------------
            var maxSize = IsSMS ? 524288 : 5.25 * 1024 * 1024;
            if (ROMbytes.Length > maxSize)
                throw new Exception(string.Format(Language.Get("Error003"), IsSMS ? "512" : "5.25", IsSMS ? Language.Get("Abbreviation_Kilobytes") : Language.Get("Abbreviation_Megabytes")));

            foreach (var item in Directory.EnumerateFiles(Paths.DataCCF))
            if (Path.GetExtension(item).ToLower().StartsWith(".sgd") || Path.GetExtension(item).ToLower().StartsWith(".sms"))
            {
                File.WriteAllBytes(item, ROMbytes);
            }
        }

        public void InsertSaveData(string text, TitleImage img)
        {
            foreach (var item in Directory.EnumerateFiles(Paths.MiscCCF))
            {
                if (Path.GetFileName(item).ToLower().Contains("banner.cfg"))
                {
                    string[] newBanner = File.ReadAllLines(item);
                    for (int i = 4; i < newBanner.Length; i++)
                    {
                        if (newBanner[i].StartsWith("JP:")) newBanner[i] = $"JP:{text}";
                        if (newBanner[i].StartsWith("EN:")) newBanner[i] = $"EN:{text}";
                        if (newBanner[i].StartsWith("GE:")) newBanner[i] = $"GE:{text}";
                        if (newBanner[i].StartsWith("FR:")) newBanner[i] = $"FR:{text}";
                        if (newBanner[i].StartsWith("SP:")) newBanner[i] = $"SP:{text}";
                        if (newBanner[i].StartsWith("IT:")) newBanner[i] = $"IT:{text}";
                        if (newBanner[i].StartsWith("DU:")) newBanner[i] = $"DU:{text}";
                    }

                    File.WriteAllText(item, string.Join("\n", newBanner), Encoding.BigEndianUnicode);
                }

                else if (Path.GetFileName(item).ToLower().Contains("comment"))
                {
                    string[] newComment = File.ReadAllLines(item, Encoding.UTF8);
                    newComment[0] = text;

                    using (TextWriter t = new StreamWriter(item, false))
                    {
                        t.NewLine = "\n";
                        for (int i = 0; i < newComment.Length; i++)
                            t.WriteLine(newComment[i]);
                    }
                }
            }
        }
    }
}