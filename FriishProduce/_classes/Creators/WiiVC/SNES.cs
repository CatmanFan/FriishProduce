using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
{
    public class SNES : InjectorWiiVC
    {
        private string ID { get; set; }
        private string target { get; set; }

        protected override void Load()
        {
            needsMainDol = true;
            mainContentIndex = 5;
            needsManualLoaded = true;
            SaveTextEncoding = WAD.Region == Region.Korea ? Encoding.BigEndianUnicode : Encoding.GetEncoding(932); // Shift-JIS

            base.Load();

            GetID();
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. (compressed formats not supported yet)
        /// </summary>
        protected override void ReplaceROM()
        {
            ROM.CheckSize();

            // -----------------------
            // Replace original ROM
            // -----------------------
            #region -- LZ77 compression --
            if (target.ToUpper().StartsWith("LZ77"))
            {
                File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                File.WriteAllBytes(Paths.WorkingFolder + "orig_lz77.rom", MainContent.Data[MainContent.GetNodeIndex(target)]);

                Utils.Run
                (
                    FileDatas.Apps.wwcxtool,
                    "wwcxtool.exe",
                    "/u orig_lz77.rom orig.rom"
                );
                Utils.Run
                (
                    FileDatas.Apps.wwcxtool,
                    "wwcxtool.exe",
                    "/cr orig_lz77.rom rom lz77.rom"
                );

                if (!File.Exists(Paths.WorkingFolder + "lz77.rom")) throw new Exception(Program.Lang.Msg(2, 1));

                MainContent.ReplaceFile(MainContent.GetNodeIndex(target), Paths.WorkingFolder + "lz77.rom");
            }
            #endregion

            #region -- LZH8 compression --
            else if (target.ToUpper().StartsWith("LZH8"))
            {
                File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                Utils.Run
                (
                    FileDatas.Apps.lzh8_cmp,
                    "lzh8",
                    "rom lzh8.rom"
                );
                if (!File.Exists(Paths.WorkingFolder + "lzh8.rom")) throw new Exception(Program.Lang.Msg(2, 1));

                MainContent.ReplaceFile(MainContent.GetNodeIndex(target), Paths.WorkingFolder + "lzh8.rom");
            }
            #endregion

            #region -- Normal --
            else if (target.Length == 8)
            {
                MainContent.ReplaceFile(MainContent.GetNodeIndex(target), ROM.Bytes);
            }
            #endregion

            else
            {
                throw new Exception(Program.Lang.Msg(13, 1));
            }

            // Delete temporary files
            // ****************
            foreach (var file in new string[] { Paths.WorkingFolder + "rom", Paths.WorkingFolder + "lz77.rom", Paths.WorkingFolder + "lzh8.rom" })
                try { File.Delete(file); } catch { }

            // Dummify unused files in emulator
            // ****************
            foreach (string file in MainContent.StringTable)
            {
                if (file.ToLower().EndsWith(".pcm") || file.ToLower().EndsWith(".var"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(file), Byte.Dummy);
                // xxxx.pcm is the digital audio file. It is not usually needed in most cases
            }
        }

        private List<int> ID_indexes
        {
            get
            {
                // Search for entry points
                // ****************
                List<int> ID_indexes = new List<int>();
                for (int i = 0; i < Contents[1].Length; i++)
                    if (Contents[1][i] == Convert.ToByte(ID[0])
                     && Contents[1][i + 1] == Convert.ToByte(ID[1])
                     && Contents[1][i + 2] == Convert.ToByte(ID[2])
                     && Contents[1][i + 3] == Convert.ToByte(ID[3])
                     && Contents[1][i + 4] == 0x00
                     && Contents[1][i + 5] == 0x00
                     && Contents[1][i + 6] == 0x00
                     && Contents[1][i + 7] == 0x00
                     && Contents[1][i + 8] == 0x00
                     && Contents[1][i + 9] == 0x00
                     && Contents[1][i + 10] == 0x00
                     && Contents[1][i + 11] == 0x00)
                        ID_indexes.Add(i);

                return ID_indexes;
            }
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            // -----------------------
            // TEXT
            // -----------------------

            lines = ConvertSaveText(lines);

            if (ID_indexes.Count > 0)
            {
                foreach (var index in ID_indexes)
                {
                    int extra = compressedMainDol == true ? 1 : 0;
                    var title = new byte[80 + extra];

                    int length_a = SaveTextEncoding.GetBytes(lines[0]).Length;
                    int length_b = lines.Length >= 2 ? SaveTextEncoding.GetBytes(lines[1]).Length : 0;
                    int line_break = 40 + extra;

                    for (int i = 0; i < length_a; i++)
                        title[i] = SaveTextEncoding.GetBytes(lines[0])[i];

                    if (lines.Length >= 2)
                        for (int i = 0; i < length_b; i++)
                            title[i + line_break] = SaveTextEncoding.GetBytes(lines[1])[i];

                    title.CopyTo(Contents[1], index + 64);
                }
            }

            // -----------------------
            // IMAGE
            // -----------------------

            if (Img != null) MainContent.ReplaceFile(MainContent.GetNodeIndex("banner.tpl"), Img.CreateSaveTPL(MainContent.Data[MainContent.GetNodeIndex("banner.tpl")]).ToByteArray());
        }

        /// <summary>
        /// Searches for the ID used by the ROM and target pathfile of the ROM to replace
        /// </summary>
        private void GetID()
        {
            foreach (string file in MainContent.StringTable)
            {
                if (file.ToLower().EndsWith(".rom"))
                {
                    target = file;
                    ID = file.Substring(file.Length - 8, 4);
                }
            }
        }

        protected override void ModifyEmulatorSettings()
        {
            bool nomanual = Manual == null;

            // Write Wii Remote remapping (wii.txt) if exists
            // ****************
            if (Keymap?.Count > 0)
            {
                File.WriteAllLines(Paths.WorkingFolder + "wii.txt",
                    new string[]
                    {
                        "1=" + Keymap[Buttons.WiiRemote_1],
                        "2=" + Keymap[Buttons.WiiRemote_2],
                        "A=" + Keymap[Buttons.WiiRemote_A],
                        "B=" + Keymap[Buttons.WiiRemote_B],
                        "Up=" + Keymap[Buttons.WiiRemote_Up],
                        "Down=" + Keymap[Buttons.WiiRemote_Down],
                        "Left=" + Keymap[Buttons.WiiRemote_Left],
                        "Right=" + Keymap[Buttons.WiiRemote_Right],
                        "Plus=" + Keymap[Buttons.WiiRemote_Plus],
                        "Minus=" + Keymap[Buttons.WiiRemote_Minus]
                    });
            };

            // Define arguments
            // ****************
            Dictionary<string, string> argDict = new();

            if (bool.Parse(Settings["patch_volume"]))       argDict.Add("--volume", Program.Lang.String("patch_volume", "vc_snes"));
            if (bool.Parse(Settings["patch_nodark"]))       argDict.Add("--no-dark", /* Program.Lang.String("patch_nodark", "vc_snes")) */ null);
            if (bool.Parse(Settings["patch_nocc"]))         argDict.Add("--no-cc", Program.Lang.String("patch_nocc", "vc_snes"));
            if (bool.Parse(Settings["patch_wiimote"]))      argDict.Add("--wiimote-native", Program.Lang.String("patch_wiimote", "vc_snes"));
            if (bool.Parse(Settings["patch_nosuspend"]))    argDict.Add("--no-suspend", Program.Lang.String("patch_nosuspend", "vc_snes"));
            if (bool.Parse(Settings["patch_nosave"]))       argDict.Add("--no-save", Program.Lang.String("patch_nosave", "vc_snes"));
            if (bool.Parse(Settings["patch_widescreen"]))   argDict.Add("--wide", Program.Lang.String("patch_widescreen", "vc_snes"));
            if (bool.Parse(Settings["patch_gcremap"]))      argDict.Add("--gc-remap", Program.Lang.String("patch_gcremap", "vc_snes"));
            if (nomanual)                                   argDict.Add("--no-opera", /* Program.Lang.String("patch_noopera", "vc_snes") */ null);

            if (argDict?.Count == 0) return;
            if (bool.Parse(Settings["patch_nocheck"])) argDict.Add("--no-header-check-all", Program.Lang.String("patch_nocheck", "vc_snes"));
            else argDict.Add("--no-header-check-simple", null);

            // Write 01.app
            // ****************
            File.WriteAllBytes(Paths.WorkingFolder + "01.app", Contents[1]);

            // Apply each patch, check which ones have failed
            // ****************
            List<string> failed = new();
            string output = null;

            for (int i = 0; i < argDict.Count; i++)
            {
                (string Key, string Value) = (argDict.Keys.ElementAt(i), argDict.Values.ElementAt(i));

                Utils.Run
                (
                    FileDatas.Apps.sns_boost,
                    "sns_boost.exe",
                    $"-i 01.app {Key}"
                );

                if ((!File.Exists(Paths.WorkingFolder + "01_boosted.app") || File.ReadAllBytes(Paths.WorkingFolder + "01_boosted.app").SequenceEqual(File.ReadAllBytes(Paths.WorkingFolder + "01.app"))))
                {
                    if (Key == "--no-opera") nomanual = false;

                    argDict.Remove(Key);
                    if (!string.IsNullOrWhiteSpace(Value)) failed.Add(Value);
                }

                if (File.Exists(Paths.WorkingFolder + "01_boosted.app"))
                    File.Delete(Paths.WorkingFolder + "01_boosted.app");
            }

            // Patch again
            // ****************
            output = Utils.Run
            (
                FileDatas.Apps.sns_boost,
                "sns_boost.exe",
                $"-i 01.app {string.Join(" ", argDict.Keys)}"
            );

            // Messages for failed patches
            // ****************
            bool generic = argDict.Count == 1 && (argDict.Keys.ElementAt(0) == "--no-opera" || argDict.Keys.ElementAt(0) == "--no-header-check-simple");
            bool notNeeded = failed.Count == 0 || generic;
            if (!notNeeded)
            {
                if (failed.Count > 0)
                {
                    string failedList = "";
                    foreach (var item in failed)
                        failedList += "- " + item + Environment.NewLine;

                    MessageBox.Show(string.Format(Program.Lang.Msg(5, 1), failedList));
                }

                else if (generic || !File.Exists(Paths.WorkingFolder + "01_boosted.app") || File.ReadAllBytes(Paths.WorkingFolder + "01_boosted.app").SequenceEqual(Contents[1]))
                {
                    MessageBox.Show(Program.Lang.Msg(4, 1));
                    return;
                }
            }

            // Clean manual if not used
            // ****************
            if (nomanual)
            {
                try
                {
                    if (ManualContent != null) ManualContent.RemoveFile("emanual.arc");
                    MainContent.RemoveFile("emanual.arc");
                }
                catch { }
            }

            // Finally, replace file
            // ****************
            if (File.Exists(Paths.WorkingFolder + "01_boosted.app"))
                Contents[1] = File.ReadAllBytes(Paths.WorkingFolder + "01_boosted.app");

            // Determine whether savedata.bin should be created in main.dol
            // ****************
            if (ID_indexes.Count > 0 && argDict.ContainsKey("--no-header-check-all"))
                foreach (var index in ID_indexes)
                    Contents[1][index - 33] = (byte)(bool.Parse(Settings["patch_nosave"]) ? 0x03 : 0x04);
        }
    }
}
