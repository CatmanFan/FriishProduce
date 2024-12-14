using libWiiSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
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

        public bool IsSMS { get; set; }

        private CCF MainCCF { get; set; }
        private CCF MiscCCF { get; set; }

        protected override void Load()
        {
            mainContentIndex = 5;
            needsManualLoaded = false;
            base.Load();

            MainCCF = CCF.Load(MainContent.Data[MainContent.GetNodeIndex("data.ccf")]);
            MiscCCF = CCF.Load(MainCCF.Data[MainCCF.GetNodeIndex("misc.ccf")]);
            ReplaceManual(MainCCF);

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

        public override WAD Write()
        {
            MainCCF.ReplaceFile(MainCCF.GetNodeIndex("misc.ccf"), MiscCCF.ToByteArray());
            MiscCCF.Dispose();

            MainContent.ReplaceFile(MainContent.GetNodeIndex("data.ccf"), MainCCF.ToByteArray());
            MainCCF.Dispose();

            return base.Write();
        }

        protected override void ReplaceROM()
        {
            // -----------------------
            // Check filesize of input ROM
            // Maximum ROM limit allowed: 5.25 MB for Gen/MD or 512 KB for Master System
            // -----------------------
            ROM.CheckSize();

            foreach (var item in MainCCF.Nodes)
                if (item.Name.ToLower().Contains(".sgd") || item.Name.ToLower().Contains(".sms"))
                    MainCCF.ReplaceFile(item, ROM.Bytes);
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            // -----------------------
            // COMMENT
            // -----------------------

            foreach (var item in MiscCCF.Nodes)
            {
                if (item.Name.ToLower().Contains("banner.cfg"))
                {
                    SaveTextEncoding = Encoding.BigEndianUnicode;
                    lines = ConvertSaveText(lines);

                    string[] newBanner = SaveTextEncoding.GetString(MiscCCF.Data[MiscCCF.GetNodeIndex(item.Name)]).Replace("\r\n", "\n").Split('\n');

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

                    using (var s = new MemoryStream())
                    {
                        var t = new StreamWriter(s, SaveTextEncoding) { NewLine = "\n" };

                        foreach (string line in newBanner)
                        {
                            t.WriteLine(line);
                        }

                        t.Flush();

                        MiscCCF.ReplaceFile(item, s.ToArray());
                    }
                }

                else if (item.Name.ToLower().Contains("comment"))
                {
                    SaveTextEncoding = Encoding.UTF8;
                    lines = ConvertSaveText(lines);

                    string[] newComment = SaveTextEncoding.GetString(MiscCCF.Data[MiscCCF.GetNodeIndex(item.Name)]).Replace("\r\n", "\n").Split('\n');

                    newComment[0] = lines[0];

                    using (var s = new MemoryStream())
                    {
                        var t = new StreamWriter(s, SaveTextEncoding) { NewLine = "\n" };

                        for (int i = 0; i < newComment.Length; i++)
                            t.WriteLine(newComment[i]);

                        t.Flush();

                        MiscCCF.ReplaceFile(item, s.ToArray());
                    }
                }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            Img.CreateSaveWTE(MiscCCF);
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


            * PULSEMAN CONFIG:
            console.brightness="68"
            console.machine_arch="md"
            console.machine_country="jp"
            console.volume="+6.0"
            modules="emu_m68kbase tsdevp md se_vc"
            romfile="Pulseman.SGD"
            snd.snddrv="tsdev+"

             */

            foreach (var item in MainCCF.Nodes)
            {
                if (item.Name.ToLower() == "config")
                {
                    var encoding = Encoding.UTF8;

                    using (var m = new StreamReader(new MemoryStream(MainCCF.Data[MainCCF.GetNodeIndex(item.Name)]), Encoding.UTF8))
                    {
                        m.ReadToEnd();
                        encoding = m.CurrentEncoding;
                    }

                    // Read config file
                    // ****************
                    string[] configFile = encoding.GetString(MainCCF.Data[MainCCF.GetNodeIndex(item.Name)]).Replace("\r\n", "\n").Split('\n');

                    IDictionary<string, string> Settings = this.Settings;

                    foreach (var line in Settings.Keys.ToArray())
                        if (line == "smsui.has_opll") Settings.Remove(line);

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

                    if (Keymap != null)
                    {
                        bool notAdded = false;

                        try
                        {
                            Settings.Add("console.core_bindings",
                                $"up={Keymap[Buttons.WiiRemote_Right]}:"
                                + $"down={Keymap[Buttons.WiiRemote_Left]}:"
                                + $"left={Keymap[Buttons.WiiRemote_Up]}:"
                                + $"right={Keymap[Buttons.WiiRemote_Down]}:"
                                + $"+={Keymap[Buttons.WiiRemote_Plus]}:"
                                + $"a={Keymap[Buttons.WiiRemote_A]}:"
                                + $"1={Keymap[Buttons.WiiRemote_1]}:"
                                + $"2={Keymap[Buttons.WiiRemote_2]}:"
                                + $"b={Keymap[Buttons.WiiRemote_B]}:");
                        }
                        catch { notAdded = true; }

                        try
                        {
                            Settings.Add("console.cl_bindings",
                                $"up={Keymap[Buttons.Classic_Up]}:"
                                + $"down={Keymap[Buttons.Classic_Down]}:"
                                + $"left={Keymap[Buttons.Classic_Left]}:"
                                + $"right={Keymap[Buttons.Classic_Right]}:"
                                + $"+={Keymap[Buttons.Classic_Plus]}:"
                                + $"y={Keymap[Buttons.Classic_Y]}:"
                                + $"b={Keymap[Buttons.Classic_B]}:"
                                + $"a={Keymap[Buttons.Classic_A]}:"
                                + $"l={Keymap[Buttons.Classic_L]}:"
                                + $"x={Keymap[Buttons.Classic_X]}:"
                                + $"r={Keymap[Buttons.Classic_R]}:"
                                + $"zr={Keymap[Buttons.Classic_ZR]}:"
                                + $"zl={Keymap[Buttons.Classic_ZL]}");
                        }
                        catch { notAdded = true; }

                        try
                        {
                            Settings.Add("console.gc_bindings",
                                $"up={Keymap[Buttons.GC_Up]}:"
                                + $"down={Keymap[Buttons.GC_Down]}:"
                                + $"left={Keymap[Buttons.GC_Left]}:"
                                + $"right={Keymap[Buttons.GC_Right]}:"
                                + $"start={Keymap[Buttons.GC_Start]}:"
                                + $"b={Keymap[Buttons.GC_B]}:"
                                + $"a={Keymap[Buttons.GC_A]}:"
                                + $"x={Keymap[Buttons.GC_X]}:"
                                + $"l={Keymap[Buttons.GC_L]}:"
                                + $"y={Keymap[Buttons.GC_Y]}:"
                                + $"r={Keymap[Buttons.GC_R]}:"
                                + $"z={Keymap[Buttons.GC_Z]}:"
                                + "c=");
                                // $"c={Keymap[Buttons.GC_C]}:"
                        }
                        catch { notAdded = true; }

                        if (notAdded)
                        {
                            // There should be an error message here
                        }
                    }

                    // newConfig is the new file which includes the modified values
                    // alreadyAdded exists to avoid duplicate entries being added to the above
                    // ****************
                    List<string> newConfig = new();
                    List<string> alreadyAdded = new();
                    Dictionary<string, string> Alt = new();

                    foreach (var line in configFile)
                    {
                        // Settings that are known by alternative names in newer revisions:
                        // ****************
                        if (line.ToLower().Contains("console.machine_country"))
                            Alt.Add("country", "console.machine_country");
                    }

                    for (int i = 0; i < Alt.Count; i++)
                        foreach (var newLine in Settings)
                            if (newLine.Key == Alt.ElementAt(i).Key)
                            {
                                newConfig.Add($"{Alt.ElementAt(i).Value}=\"{newLine.Value}\"");
                                if (!alreadyAdded.Contains(newLine.Key)) alreadyAdded.Add(newLine.Key);
                                if (!alreadyAdded.Contains(Alt.ElementAt(i).Value)) alreadyAdded.Add(Alt.ElementAt(i).Value);
                            }

                    foreach (var line in configFile)
                    {
                        var undeleted = new List<string>()
                        {
                            // Settings that should NOT be changed:
                            "console.machine_arch",
                            "console.cl_bindings",
                            "console.core_bindings",
                            "console.gc_bindings",
                            "console.volume",
                            "console.master_volume",
                            "console.rapidfire",
                            "machine_md.snd_strict_sync",
                            "machine_md.use_4ptap",
                            "modules",
                            "nplayers",
                            "romfile",
                            "selectmenu",
                            "snd.snddrv",
                            "smsui.has_opll",
                            "vdp.disable_gamma",
                            "vdp_md.gamma_curve"
                        };

                        if (Keymap != null)
                        {
                            if (Settings.ContainsKey("console.cl_bindings"))    undeleted.Remove("console.cl_bindings");
                            if (Settings.ContainsKey("console.core_bindings"))  undeleted.Remove("console.core_bindings");
                            if (Settings.ContainsKey("console.gc_bindings"))    undeleted.Remove("console.gc_bindings");
                        }

                        foreach (var name in undeleted)
                            if (line.StartsWith(name)) { newConfig.Add(line); alreadyAdded.Add(line); }
                    }

                    foreach (var newLine in Settings)
                    {
                        bool doNotAdd = false;
                        foreach (var added in alreadyAdded)
                            if (added.Contains(newLine.Key))
                                doNotAdd = true;

                        if (!doNotAdd && !string.IsNullOrEmpty(newLine.Value))
                        {
                            newConfig.Add($"{newLine.Key}=\"{newLine.Value}\"");
                            alreadyAdded.Add(newLine.Key);
                        }
                    }

                    newConfig.Sort();

                    var newConfigFile = encoding.GetBytes(string.Join("\r\n", newConfig.ToArray()) + "\r\n");

                    while (!Byte.IsSame(MainCCF.Data[MainCCF.GetNodeIndex(item.Name)], newConfigFile))
                        MainCCF.ReplaceFile(item, newConfigFile);
                }
            }
        }
    }
}