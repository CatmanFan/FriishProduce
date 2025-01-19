using ICSharpCode.SharpZipLib.Zip;
using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce
{
    public abstract class InjectorWiiVC
    {
        protected WAD WAD { get; set; }
        public int EmuType { get; set; }
        public IDictionary<string, string> Settings { get; set; }
        public IDictionary<Buttons, string> Keymap { get; set; }
        protected Encoding SaveTextEncoding { get; set; }

        protected ROM ROM { get; set; }

        protected string origManual { get; set; }
        public bool UseOrigManual { get; set; }
        public string CustomManual { get; set; }
        /// <summary>
        /// Determines if we need the manual app index to be set and replaced.
        /// </summary>
        protected bool needsManualLoaded { get; set; }
        /// <summary>
        /// Determines if the 01.app is to be modified.
        /// </summary>
        protected bool needsMainDol { get; set; }
        /// <summary>
        /// The contents of the WAD.
        /// </summary>
        protected List<byte[]> Contents { get; set; }
        /// <summary>
        /// This is the main U8 archive which contains the emanual, ROM, savebanner, etc., stored in either 05.app, 06.app or 07.app (depending on the VC type).
        /// It needs to be set manually for each console (normally, it is the 5th index)
        /// </summary>
        protected int mainContentIndex { get; set; }
        protected U8 MainContent { get; set; }

        private int manualContentIndex { get; set; }
        protected U8 ManualContent { get; set; }

        public InjectorWiiVC() { }

        protected virtual void Load()
        {
            Contents = new List<byte[]>();
            for (int i = 0; i < WAD.Contents.Length; i++)
                Contents.Add(new byte[0]);

            // Load main.dol if needed
            // ****************
            if (needsMainDol)
            {
                Contents[1] = Utils.ExtractContent1(WAD.Contents[1]);
            }

            // Auto-set main content index if it is absolutely necessary, then load both U8 archives
            // ****************
            if (needsManualLoaded && (mainContentIndex <= 1)) mainContentIndex = 5;

            if (mainContentIndex > 1 && WAD.Contents.Length > mainContentIndex)
                MainContent = U8.Load(WAD.Contents[mainContentIndex]);

            manualContentIndex = 5;
            ManualContent = mainContentIndex == manualContentIndex ? null : U8.Load(WAD.Contents[manualContentIndex]);

            if (needsManualLoaded) ReplaceManual(ManualContent ?? MainContent);
        }

        #region ### EMANUAL FUNCTIONS ###

        /// <summary>
        /// Actually does the replacing of the html/man/emanual.arc with the contents of the specified folder.
        /// </summary>
        /// <param name="file">The U8 archive to modify.</param>
        /// <returns>Output file.</returns>
        protected U8 WriteManual(byte[] input)
        {
            U8 emanual_U8 = U8.Load(input);
            bool valid = false;

            if (File.Exists(CustomManual))
            {
                valid = true;
                Directory.CreateDirectory(Paths.Manual);

                // Extract ZIP
                // ****************
                try
                {
                    FastZip zip = new();
                    zip.ExtractZip(CustomManual, Paths.Manual, null);
                }
                catch
                {
                    valid = false;
                }
            }

            else if (Directory.Exists(CustomManual))
            {
                valid = true;
                Directory.CreateDirectory(Paths.Manual);

                // Copy all files and folders to target path
                // ****************
                foreach (string dir in Directory.GetDirectories(CustomManual, "*.*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dir.Replace(Path.GetDirectoryName(CustomManual), Path.GetDirectoryName(Paths.Manual)));

                foreach (string file in Directory.GetFiles(CustomManual, "*.*", SearchOption.AllDirectories))
                    File.Copy(file, file.Replace(Path.GetDirectoryName(file), Path.GetDirectoryName(Paths.Manual)), true);
            }

            if (valid)
            {
                // Get root folder name
                // ****************
                List<string> rootFolders = new List<string>() { "html", "man", "emanual" };
                string target = Paths.Manual;

                foreach (var item in rootFolders)
                    if (item == emanual_U8.StringTable[0])
                        if (!Directory.EnumerateDirectories(target).Contains(item)) target = Path.Combine(Paths.Manual, item) + "\\";

                emanual_U8.CreateFromDirectory(Paths.Manual);
                if (Directory.Exists(Paths.Manual)) Directory.Delete(Paths.Manual, true);
            }

            return emanual_U8;
        }

        #region --- Replace manual within CCF/U8 ---
        protected CCF ReplaceManual(CCF target)
        {
            if (File.Exists(CustomManual) || Directory.Exists(CustomManual))
            {
                // Get and read emanual
                // ****************
                foreach (var item in target.Nodes)
                    if (item.Name.ToLower().Contains("man.arc"))
                    {
                        origManual = item.Name;
                        target.ReplaceFile
                        (
                            target.GetNodeIndex(origManual),
                            WriteManual(target.Data[target.GetNodeIndex(origManual)]).ToByteArray()
                        );
                    }
            }
            else CleanManual();

            return target;
        }

        protected void ReplaceManual(U8 target)
        {
            if (File.Exists(CustomManual) || Directory.Exists(CustomManual))
            {
                /* For reference: copied from "vcromclaim": https://github.com/JanErikGunnar/vcromclaim/blob/master/wiimetadata.py

                NOT COMPRESSED:

                X emanual.arc
                X html.arc
                X man.arc
                X data.ccf > man.arc

                COMPRESSED:

                X htmlc.arc (N64 LZ77)
                X LZ77_html.arc (TG16/PCE LZ77)
                Regex('.+_manual_.+\\.arc\\.lz77$') e.g. makaimura_manual_usa.arc.lz77 (Arcade Ghosts n Goblins) */

                byte[] backup = null;

                // Get and read emanual
                // ****************
                try
                {
                    foreach (var item in target.StringTable)

                        if (item.ToLower().Contains("htmlc.arc") || item.ToLower().Contains("lz77_html.arc"))
                        {
                            origManual = item;
                            backup = target.Data[target.GetNodeIndex(origManual)];

                            File.WriteAllBytes(Paths.WorkingFolder + "html.arc", target.Data[target.GetNodeIndex(origManual)]);
                            Utils.Run
                            (
                                FileDatas.Apps.wwcxtool,
                                "wwcxtool.exe",
                                "/u html.arc html.dec"
                            );
                            if (!File.Exists(Paths.WorkingFolder + "html.dec")) throw new Exception(Program.Lang.Msg(2, true));

                            File.WriteAllBytes("html_modified.dec", WriteManual(File.ReadAllBytes(Paths.WorkingFolder + "html.dec")).ToByteArray());

                            Utils.Run
                            (
                                FileDatas.Apps.wwcxtool,
                                "wwcxtool.exe",
                                "/cr html.arc html_modified.dec html_modified.arc"
                            );
                            if (!File.Exists(Paths.WorkingFolder + "html_modified.arc")) throw new Exception(Program.Lang.Msg(2, true));

                            target.ReplaceFile
                            (
                                target.GetNodeIndex(origManual),
                                File.ReadAllBytes(Paths.WorkingFolder + "html_modified.arc")
                            );

                            if (File.Exists(Paths.WorkingFolder + "html.dec")) File.Delete(Paths.WorkingFolder + "html.dec");
                            if (File.Exists(Paths.WorkingFolder + "html.arc")) File.Delete(Paths.WorkingFolder + "html.arc");
                            if (File.Exists(Paths.WorkingFolder + "html_modified.arc")) File.Delete(Paths.WorkingFolder + "html_modified.arc");
                        }

                        else if (item.ToLower().Contains("emanual.arc") || item.ToLower().Contains("html.arc") || item.ToLower().Contains("man.arc"))
                        {
                            origManual = item;
                            backup = target.Data[target.GetNodeIndex(origManual)];

                            target.ReplaceFile
                            (
                                target.GetNodeIndex(origManual),
                                WriteManual(target.Data[target.GetNodeIndex(origManual)]).ToByteArray()
                            );
                        }
                }

                catch
                {
                    CleanManual();
                    target.ReplaceFile(target.GetNodeIndex(origManual), backup);
                    MessageBox.Show(Program.Lang.Msg(10, true));
                }
            }

            else CleanManual();
        }
        #endregion

        /// <summary>
        /// Dispose of "Operations Guide" button on HOME Menu.
        /// </summary>
        private void CleanManual()
        {
            CustomManual = null;
            if (UseOrigManual) return;

            U8 Content4 = U8.Load(WAD.Contents[4]);

            int start = -1;
            int end = -1;

            for (int i = 0; i < Content4.NumOfNodes; i++)
            {
                if (Content4.StringTable[i].ToLower() == "homebutton2") start = i;
                else if (Content4.StringTable[i].ToLower() == "homebutton3") end = i;
            }

            try
            {
                if (start <= 0 && end <= 0) throw new InvalidOperationException();
                else
                {
                    for (int i = 1; i < end - start; i++)
                        Content4.ReplaceFile(i + end, Content4.Data[i + start]);

                    Contents[4] = Content4.ToByteArray();
                    if (Content4 != null) Content4.Dispose();
                }
            }

            catch { if (Content4 != null) Content4.Dispose(); }
        }
        #endregion

        public virtual WAD Write()
        {
            // Assign each modified content file to the Contents List
            // ****************
            if (!WAD.Contents[1].SequenceEqual(Contents[1]) || needsMainDol)
            {
                Contents[1] = Utils.PackContent1(Contents[1]);
            }

            if (ManualContent != null)
            {
                Contents[manualContentIndex] = ManualContent.ToByteArray();
                ManualContent.Dispose();
            }

            if (CustomManual != null || MainContent != null)
                Contents[mainContentIndex] = MainContent.ToByteArray();
            MainContent.Dispose();

            // Then actually modify the WAD by detecting which parts need to be modified and then inserting them.
            // ---------------------
            // Temporary workaround for crashes
            // WAD needs to be repacked using proper tik/tmd/cert from scratch using modified files.
            // Apparently it worked by directly editing before but not after I revised much of the program code just now.
            // ****************
            WAD.Unpack(Paths.WAD);

            for (int i = 0; i < WAD.Contents.Length; i++)
            {
                if (Contents[i].Length > 1)
                    File.WriteAllBytes(Paths.WAD + i.ToString("X8").ToLower() + ".app", Contents[i]);
            }

            WAD.CreateNew(Paths.WAD);
            Directory.Delete(Paths.WAD, true);

            return WAD;
        }

        public WAD Inject(WAD wad, ROM rom, string[] SaveDataTitle, ImageHelper Img)
        {
            this.WAD = wad;
            this.ROM = rom;

            Load();
            ReplaceROM();
            ReplaceSaveData(SaveDataTitle, Img);
            ModifyEmulatorSettings();
            return Write();
        }

        protected abstract void ReplaceROM();

        protected abstract void ReplaceSaveData(string[] lines, ImageHelper Img);

        protected abstract void ModifyEmulatorSettings();

        protected string[] ConvertSaveText(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                var bytes = Encoding.Unicode.GetBytes(lines[i]);
                lines[i] = SaveTextEncoding.GetString(Encoding.Convert(Encoding.Unicode, SaveTextEncoding, bytes));
            }

            return lines;
        }

        protected bool SettingParse(int i)
        {
            if (bool.TryParse(Settings.ElementAt(i).Value, out bool value))
                value = bool.Parse(Settings.ElementAt(i).Value);
            return value;
        }

        public void Dispose()
        {
            if (WAD != null) WAD.Dispose();
            if (Contents != null) Contents.Clear();
            ROM = null;
            origManual = null;
            UseOrigManual = false;

            mainContentIndex = 0;
            manualContentIndex = 0;
            needsMainDol = false;
            needsManualLoaded = false;
        }
    }
}
