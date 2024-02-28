using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libWiiSharp;

namespace FriishProduce
{
    public abstract class InjectorWiiVC
    {
        public WAD WAD { get; set; }
        public int EmuType { get; set; }
        public IDictionary<string, string> Settings { get; set; }

        protected ROM ROM { get; set; }

        public string ManualPath { get; set; }
        protected string OrigManual { get; set; }
        protected byte[] Manual { get; set; }
        protected bool NeedsManualLoaded { get; set; }

        protected bool NeedsMainDOL { get; set; }
        protected List<byte[]> Contents { get; set; }

        // This is the main U8 archive which contains the emanual, ROM, savebanner, or other needed files, stored in either 00000005.app, 00000006.app or 00000007.app (depending on the console).
        // It needs to be set manually for each console (normally, it is the 5th index)
        // ****************
        protected int MainContentIndex { get; set; }
        protected U8 MainContent { get; set; }

        public InjectorWiiVC() { }

        protected virtual void Load()
        {
            Contents = new List<byte[]>();
            for (int i = 0; i < WAD.Contents.Length; i++)
                Contents.Add(new byte[0]);

            if (NeedsMainDOL)
            {
                Contents[1] = WAD.Contents[1];
                if (Contents[1].Length < 1048576)
                {
                    // Temporary 00000001.app at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.app", Contents[1]);
                    ProcessHelper.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/u content1.app content1.dec"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.dec")) throw new Exception(Language.Get("Error.002"));

                    Contents[1] = File.ReadAllBytes(Paths.WorkingFolder + "content1.dec");
                }
            }

            if (NeedsManualLoaded && (MainContentIndex <= 1)) MainContentIndex = 5;

            if (MainContentIndex > 1 && WAD.Contents.Length > MainContentIndex)
                MainContent = U8.Load(WAD.Contents[MainContentIndex]);

            if (NeedsManualLoaded) ReplaceManual(MainContent);
        }

        protected void ReplaceManual(U8 target)
        {
            if (ManualPath == null)
            {
                // Dispose of "Operations Guide" button on HOME Menu
                // ****************
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
                    if (start == 0 && end == 0) throw new InvalidOperationException();
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

            else
            {
                /* For reference: copied from "vcromclaim": https://github.com/JanErikGunnar/vcromclaim/blob/master/wiimetadata.py

                if u8arc.findfile('emanual.arc'):
                    man = U8Archive(u8arc.getfile(u8arc.findfile('emanual.arc')))

                elif u8arc.findfile('html.arc'):
                    man = U8Archive(u8arc.getfile(u8arc.findfile('html.arc')))

                elif u8arc.findfile('man.arc'):
                    man = U8Archive(u8arc.getfile(u8arc.findfile('man.arc')))

                elif u8arc.findfile('data.ccf'):
                    ccf = CCFArchive(u8arc.getfile(u8arc.findfile('data.ccf')))
                    man = U8Archive(ccf.getfile('man.arc'))

                elif u8arc.findfile('htmlc.arc'):
                    manc = u8arc.getfile(u8arc.findfile('htmlc.arc'))
                    print('Decompressing manual: htmlc.arc')
                    man = U8Archive(BytesIO(lz77.decompress_n64(manc)))

                elif u8arc.findfilebyregex('.+_manual_.+\\.arc\\.lz77$'):
                    # E.g. makaimura_manual_usa.arc.lz77 (Arcade Ghosts n Goblins)
                    manc = u8arc.getfile(u8arc.findfilebyregex('.+_manual_.+\\.arc\\.lz77$'))
                    man = U8Archive(BytesIO(lz77.decompress_nonN64(manc)))
                    manc.close() */

                // Get and read emanual
                // ****************
                if (File.Exists(Paths.DataCCF + "man.arc"))
                {
                    OrigManual = Paths.DataCCF + "man.arc";
                    Manual = File.ReadAllBytes(OrigManual);
                }
                else
                {
                    foreach (var item in target.StringTable)
                        if (item.ToLower().Contains("emanual.arc") || item.ToLower().Contains("html.arc") || item.ToLower().Contains("man.arc"))
                        {
                            OrigManual = item;
                            Manual = target.Data[target.GetNodeIndex(OrigManual)];
                        }
                        else if (item.ToLower().Contains("htmlc.arc"))
                        {
                            /* TO-DO: Handle using LZ77 compression (WWCXtool again?) */
                        }
                }

                // Replace
                // ****************
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

                if (File.Exists(OrigManual))
                    File.WriteAllBytes(OrigManual, ManualArc.ToByteArray());
                else
                    target.ReplaceFile(target.GetNodeIndex(OrigManual), ManualArc.ToByteArray());

                ManualArc.Dispose();
                Directory.Delete(Paths.Manual, true);
            }
        }

        public virtual WAD Write()
        {
            if (!WAD.Contents[1].SequenceEqual(Contents[1]) || NeedsMainDOL)
            {
                if (File.Exists(Paths.WorkingFolder + "content1.dec"))
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.dec", Contents[1]);
                    ProcessHelper.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/cr content1.app content1.dec content1.new"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.new")) throw new Exception(Language.Get("Error.002"));

                    byte[] Recompressed = File.ReadAllBytes(Paths.WorkingFolder + "content1.new");

                    if (File.Exists(Paths.WorkingFolder + "content1.new")) File.Delete(Paths.WorkingFolder + "content1.new");
                    if (File.Exists(Paths.WorkingFolder + "content1.dec")) File.Delete(Paths.WorkingFolder + "content1.dec");
                    if (File.Exists(Paths.WorkingFolder + "content1.app")) File.Delete(Paths.WorkingFolder + "content1.app");

                    Contents[1] = Recompressed;
                }
            }

            if (Manual != null || MainContent != null)
                Contents[MainContentIndex] = MainContent.ToByteArray();
            MainContent.Dispose();

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
