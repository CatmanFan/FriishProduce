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
        protected List<byte[]> Contents { get; set; }
        protected U8 Content5 { get; set; }
        public int EmuType { get; set; }

        protected byte[] ROM { get; set; }
        protected string NewManual { get; set; }
        protected string ManualPath { get; set; }
        protected byte[] Manual { get; set; }

        protected bool UsesContent5 { get; set; }
        protected bool NeedsMainDOL { get; set; }
        private bool CompressedMainDOL { get; set; }

        public WiiVCInjector(WAD w, string ROM, string Manual = null)
        {
            WAD = w;
            Contents = WAD.Contents.ToList();

            // -----------------------
            // Check if raw ROM exists
            // -----------------------
            if (!File.Exists(ROM))
                throw new FileNotFoundException(new FileNotFoundException().Message, ROM);

            this.ROM = File.ReadAllBytes(ROM);

            NewManual = Manual;
        }

        protected void Load()
        {
            if (NeedsMainDOL)
            {
                if (Contents[1].Length < 1048576)
                {
                    // Temporary main.dol at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "main.dol", Contents[1]);
                    Process.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/u main.dol main.dol.dec"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "main.dol.dec")) throw new Exception(Language.Get("Error002"));

                    Contents[1] = File.ReadAllBytes(Paths.WorkingFolder + "main.dol.dec");
                    CompressedMainDOL = true;
                }
            }

            if (WAD.Contents.Length > 5 || UsesContent5)
            {
                Content5 = U8.Load(WAD.Contents[5]);

                if (NewManual != null)
                {
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
        }

        protected void ReplaceManual()
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
                ManualArc.CreateFromDirectory(NewManual);
                Manual = ManualArc.ToByteArray();

                if (File.Exists(ManualPath)) File.WriteAllBytes(ManualPath, Manual);
                else Content5.ReplaceFile(Content5.GetNodeIndex(ManualPath), Manual);
            }
        }

        public WAD Write()
        {
            if (!NeedsMainDOL)
            {
                Contents[1] = WAD.Contents[1];
                CompressedMainDOL = false;
            }

            if (!UsesContent5 && ManualPath == null) Contents[5] = WAD.Contents[5];

            if (CompressedMainDOL)
            {
                File.WriteAllBytes(Paths.WorkingFolder + "main.dol.dec", Contents[1]);
                Process.Run
                (
                    Paths.Tools + "wwcxtool.exe",
                    Paths.WorkingFolder,
                    "/cr main.dol main.dol.dec main.dol.new"
                );
                if (!File.Exists(Paths.WorkingFolder + "main.dol.new")) throw new Exception(Language.Get("Error002"));

                Contents[1] = File.ReadAllBytes(Paths.WorkingFolder + "main.dol.new");

                if (File.Exists(Paths.WorkingFolder + "main.dol.new")) File.Delete(Paths.WorkingFolder + "main.dol.new");
                if (File.Exists(Paths.WorkingFolder + "main.dol.dec")) File.Delete(Paths.WorkingFolder + "main.dol.dec");
                if (File.Exists(Paths.WorkingFolder + "main.dol")) File.Delete(Paths.WorkingFolder + "main.dol");
            }

            // ---------------------------------------------------------------------------------------------
            // REPLACING WAD CONTENTS
            // ---------------------------------------------------------------------------------------------

            // Temporary solution for the main.dol thing
            // ****************
            WAD.Unpack(Paths.WAD);

            foreach (var item in Directory.EnumerateFiles(Paths.WAD))
            {
                if (Path.GetExtension(item).ToLower() == ".app") File.Delete(item);
            }

            for (int i = 0; i < Contents.Count; i++)
            {
                var x = i.ToString("X8").ToLower();
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
