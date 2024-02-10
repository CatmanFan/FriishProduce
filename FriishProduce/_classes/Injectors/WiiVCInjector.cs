using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libWiiSharp;

namespace FriishProduce
{
    public abstract class WiiVCInjector
    {
        protected WAD WAD { get; set; }
        protected byte[] ROM { get; set; }
        protected string NewManual { get; set; }
        protected string ManualPath { get; set; }
        protected byte[] Manual { get; set; }

        protected List<byte[]> Contents { get; set; }
        protected U8 Content5 { get; set; }
        public int EmuType { get; set; }

        protected bool NeedsMainDOL { get; set; }
        protected bool UsesContent5 { get; set; }

        public WiiVCInjector(WAD w, string ROM)
        {
            WAD = w;
            Contents = new List<byte[]>();
            for (int i = 0; i < WAD.Contents.Length; i++)
                Contents.Add(new byte[0]);

            // -----------------------
            // Check if raw ROM exists
            // -----------------------
            if (!File.Exists(ROM))
                throw new FileNotFoundException(new FileNotFoundException().Message, ROM);

            this.ROM = File.ReadAllBytes(ROM);
        }

        protected void Load()
        {
            if (NeedsMainDOL)
            {
                Contents[1] = WAD.Contents[1];
                if (Contents[1].Length < 1048576)
                {
                    // Temporary 00000001.app at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.app", Contents[1]);
                    Process.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/u content1.app content1.dec"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.dec")) throw new Exception(Language.Get("Error002"));

                    Contents[1] = File.ReadAllBytes(Paths.WorkingFolder + "content1.dec");
                }
            }

            if (WAD.Contents.Length > 5 || UsesContent5)
            {
                Content5 = new U8();
                Content5.LoadFile(WAD.Contents[5]);

                // Get and read emanual
                // ****************
                foreach (var item in Content5.StringTable)
                    if (item.ToLower().Contains("emanual.arc"))
                    {
                        ManualPath = item;
                        Manual = Content5.Data[Content5.GetNodeIndex(ManualPath)];
                    }
            }
        }

        public void ReplaceManual()
        {
            if (NewManual == null)
            {
                // Dispose of "Operations Guide" button on HOME Menu
                // ****************
                U8 Content4 = new U8();
                Content4.LoadFile(WAD.Contents[4]);

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
                    }
                }
                finally
                {
                    Content4.Dispose();
                }
            }

            else
            {
                if (Manual == null) return;

                U8 ManualArc = U8.Load(Manual);

                // Check if is a valid emanual contents folder
                // ****************
                int validFiles = 0;
                if (Directory.Exists(Path.Combine(NewManual, "emanual")))
                    foreach (var item in Directory.EnumerateFiles(Path.Combine(NewManual, "emanual")))
                    {
                        if ((Path.GetFileNameWithoutExtension(item).StartsWith("startup") && Path.GetExtension(item) == ".html")
                         || Path.GetFileName(item) == "standard.css") validFiles++;
                    }

                if (validFiles < 2)
                {
                    ManualArc.Dispose();
                    System.Windows.Forms.MessageBox.Show(Language.Get("Error007"));
                    return;
                }

                // Replace
                // ****************
                ManualArc.CreateFromDirectory(Path.Combine(NewManual));
                Manual = ManualArc.ToByteArray();

                if (File.Exists(ManualPath)) File.WriteAllBytes(ManualPath, Manual);
                else Content5.ReplaceFile(Content5.GetNodeIndex(ManualPath), Manual);

                Manual = null;
            }
        }

        public WAD Write()
        {
            if (!WAD.Contents[1].SequenceEqual(Contents[1]) || NeedsMainDOL)
            {
                if (File.Exists(Paths.WorkingFolder + "content1.dec"))
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.dec", Contents[1]);
                    Process.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/cr content1.app content1.dec content1.new"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.new")) throw new Exception(Language.Get("Error002"));

                    byte[] Recompressed = File.ReadAllBytes(Paths.WorkingFolder + "content1.new");

                    if (File.Exists(Paths.WorkingFolder + "content1.new")) File.Delete(Paths.WorkingFolder + "content1.new");
                    if (File.Exists(Paths.WorkingFolder + "content1.dec")) File.Delete(Paths.WorkingFolder + "content1.dec");
                    if (File.Exists(Paths.WorkingFolder + "content1.app")) File.Delete(Paths.WorkingFolder + "content1.app");

                    Contents[1] = Recompressed;
                }
            }

            if (WAD.Contents.Length > 5 && (!WAD.Contents[5].SequenceEqual(Contents[5]) || UsesContent5))
                Contents[5] = Content5.ToByteArray();

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

        public abstract void ReplaceROM();

        public abstract void ReplaceSaveData(string[] lines, TitleImage tImg);
    }
}
