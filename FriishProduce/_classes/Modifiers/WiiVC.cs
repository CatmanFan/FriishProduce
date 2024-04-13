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
        public WAD WAD { get; set; }
        public int EmuType { get; set; }
        public IDictionary<string, string> Settings { get; set; }
        protected Encoding SaveTextEncoding { get; set; }

        protected ROM ROM { get; set; }

        public string ManualPath { get; set; }
        protected string OrigManual { get; set; }
        protected byte[] Manual { get; set; }
        protected bool NeedsManualLoaded { get; set; }

        protected bool NeedsMainDOL { get; set; }
        protected List<byte[]> Contents { get; set; }

        /// This is the main U8 archive which contains the emanual, ROM, savebanner, or other needed files, stored in either 00000005.app, 00000006.app or 00000007.app (depending on the console).
        /// It needs to be set manually for each console (normally, it is the 5th index)

        protected int MainContentIndex { get; set; }
        protected U8 MainContent { get; set; }

        private int ManualContentIndex { get; set; }
        protected U8 ManualContent { get; set; }

        public InjectorWiiVC() { }

        protected virtual void Load()
        {
            Contents = new List<byte[]>();
            for (int i = 0; i < WAD.Contents.Length; i++)
                Contents.Add(new byte[0]);

            // Load main.dol if needed
            // ****************
            if (NeedsMainDOL)
            {
                Contents[1] = WAD.Contents[1];

                if (Contents[1].Length < 1048576)
                {
                    // Create temporary files at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.app", Contents[1]);
                    Utils.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/u content1.app content1.dec"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.dec")) throw new Exception(Program.Lang.Msg(2, true));

                    Contents[1] = File.ReadAllBytes(Paths.WorkingFolder + "content1.dec");
                }
            }

            // Auto-set main content index if it is absolutely necessary, then load both U8 archives
            // ****************
            if (NeedsManualLoaded && (MainContentIndex <= 1)) MainContentIndex = 5;

            if (MainContentIndex > 1 && WAD.Contents.Length > MainContentIndex)
                MainContent = U8.Load(WAD.Contents[MainContentIndex]);

            ManualContentIndex = 5;
            ManualContent = MainContentIndex == ManualContentIndex ? null : U8.Load(WAD.Contents[ManualContentIndex]);

            if (NeedsManualLoaded) ReplaceManual(ManualContent != null ? ManualContent : MainContent);
        }

        /// <summary>
        /// Dispose of "Operations Guide" button on HOME Menu.
        /// </summary>
        private void CleanManual()
        {
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

        /// <summary>
        /// Actually does the replacing of the html/man/emanual.arc with the contents of the specified folder.
        /// </summary>
        protected U8 ReplaceManual()
        {
            U8 ManualArc = U8.Load(Manual);

            string newFolder = Path.Combine(Paths.Manual, Path.GetFileNameWithoutExtension(OrigManual).Replace("htmlc", "html"));
            string oldFolder = Directory.Exists(Path.Combine(ManualPath, "emanual")) ? Path.Combine(ManualPath, "emanual")
                             : Directory.Exists(Path.Combine(ManualPath, "man")) ? Path.Combine(ManualPath, "man")
                             : Path.Combine(ManualPath, "html");

            Directory.CreateDirectory(newFolder);
            foreach (string dir in Directory.GetDirectories(oldFolder, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dir.Replace(oldFolder, newFolder));
            foreach (string path in Directory.GetFiles(oldFolder, "*.*", SearchOption.AllDirectories))
                File.Copy(path, path.Replace(oldFolder, newFolder), true);

            ManualArc.CreateFromDirectory(Paths.Manual);
            Directory.Delete(Paths.Manual, true);

            return ManualArc;
        }

        protected CCF ReplaceManual(CCF target)
        {
            // Get and read emanual
            // ****************
            try
            {
                if (ManualPath != null)
                {
                    foreach (var item in target.Nodes)
                        if (item.Name.ToLower().Contains("man.arc"))
                        {
                            OrigManual = item.Name;
                            Manual = target.Data[target.GetNodeIndex(OrigManual)];

                            target.ReplaceFile(target.GetNodeIndex(OrigManual), ReplaceManual().ToByteArray());
                        }
                }
            }

            catch
            {
            }

            return target;
        }

        protected void ReplaceManual(U8 target)
        {
            if (ManualPath == null)
            {
                CleanManual();
            }

            else
            {
                /* For reference: copied from "vcromclaim": https://github.com/JanErikGunnar/vcromclaim/blob/master/wiimetadata.py

                NOT COMPRESSED:

                emanual.arc
                html.arc
                man.arc
                data.ccf > man.arc

                COMPRESSED:

                htmlc.arc (N64 LZ77)
                Regex('.+_manual_.+\\.arc\\.lz77$') e.g. makaimura_manual_usa.arc.lz77 (Arcade Ghosts n Goblins) */

                // Get and read emanual
                // ****************
                try
                {
                    foreach (var item in target.StringTable)
                        if (item.ToLower().Contains("emanual.arc") || item.ToLower().Contains("html.arc") || item.ToLower().Contains("man.arc"))
                        {
                            OrigManual = item;
                            Manual = target.Data[target.GetNodeIndex(OrigManual)];

                            target.ReplaceFile(target.GetNodeIndex(OrigManual), ReplaceManual().ToByteArray());
                        }

                        else if (item.ToLower().Contains("htmlc.arc") || item.ToLower().Contains("lz77_html.arc"))
                        {
                            OrigManual = item;

                            File.WriteAllBytes(Paths.WorkingFolder + "html.arc", target.Data[target.GetNodeIndex(OrigManual)]);
                            Utils.Run
                            (
                                Paths.Tools + "wwcxtool.exe",
                                Paths.WorkingFolder,
                                "/u html.arc html.dec"
                            );

                            if (!File.Exists(Paths.WorkingFolder + "html.dec")) throw new Exception(Program.Lang.Msg(2, true));
                            Manual = File.ReadAllBytes(Paths.WorkingFolder + "html.dec");
                            if (File.Exists(Paths.WorkingFolder + "html.dec")) File.Delete(Paths.WorkingFolder + "html.dec");

                            File.WriteAllBytes("html_modified.dec", ReplaceManual().ToByteArray());

                            Utils.Run
                            (
                                Paths.Tools + "wwcxtool.exe",
                                Paths.WorkingFolder,
                                "/cr html.arc html_modified.dec html_modified.arc"
                            );

                            if (!File.Exists(Paths.WorkingFolder + "html_modified.arc")) throw new Exception(Program.Lang.Msg(2, true));
                            target.ReplaceFile(target.GetNodeIndex(OrigManual), File.ReadAllBytes(Paths.WorkingFolder + "html_modified.arc"));

                            if (File.Exists(Paths.WorkingFolder + "html.arc")) File.Delete(Paths.WorkingFolder + "html.arc");
                            if (File.Exists(Paths.WorkingFolder + "html_modified.arc")) File.Delete(Paths.WorkingFolder + "html_modified.arc");
                        }
                }

                catch
                {
                    // Do error?
                    // ****************
                    CleanManual();
                }
            }
        }

        public virtual WAD Write()
        {
            // Assign each modified content file to the Contents List
            // ****************
            if (!WAD.Contents[1].SequenceEqual(Contents[1]) || NeedsMainDOL)
            {
                if (File.Exists(Paths.WorkingFolder + "content1.dec"))
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.dec", Contents[1]);
                    Utils.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/cr content1.app content1.dec content1.new"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.new")) throw new Exception(Program.Lang.Msg(2, true));

                    byte[] Recompressed = File.ReadAllBytes(Paths.WorkingFolder + "content1.new");

                    if (File.Exists(Paths.WorkingFolder + "content1.new")) File.Delete(Paths.WorkingFolder + "content1.new");
                    if (File.Exists(Paths.WorkingFolder + "content1.dec")) File.Delete(Paths.WorkingFolder + "content1.dec");
                    if (File.Exists(Paths.WorkingFolder + "content1.app")) File.Delete(Paths.WorkingFolder + "content1.app");

                    Contents[1] = Recompressed;
                }
            }

            if (ManualContent != null)
            {
                Contents[ManualContentIndex] = ManualContent.ToByteArray();
                ManualContent.Dispose();
            }

            if (Manual != null || MainContent != null)
                Contents[MainContentIndex] = MainContent.ToByteArray();
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

        public WAD Inject(ROM ROM, string[] SaveDataTitle, ImageHelper Img)
        {
            this.ROM = ROM;

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
            ManualPath = null;
            OrigManual = null;
            Manual = null;
        }
    }
}
