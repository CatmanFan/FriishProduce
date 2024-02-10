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
        protected byte[] Content1 { get; set; }
        protected U8 Content5 { get; set; }
        public int EmuType { get; set; }

        protected bool UsesContent1 { get; set; }
        protected bool UsesContent5 { get; set; }
        private bool CompressedContent1 { get; set; }
        protected string ManualPath { get; set; }
        protected byte[] Manual { get; set; }

        public WiiVCInjector(WAD w, string ROM)
        {
            WAD = w;

            // -----------------------
            // Check if raw ROM exists
            // -----------------------
            if (!File.Exists(ROM))
                throw new FileNotFoundException(new FileNotFoundException().Message, ROM);

            this.ROM = File.ReadAllBytes(ROM);
        }

        protected void Load()
        {
            if (UsesContent1)
            {
                Content1 = WAD.Contents[1];
                if (Content1.Length < 1048576)
                {
                    // Temporary 00000001.app at working folder
                    // ****************
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.app", Content1);
                    Process.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/u content1.app content1.dec"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.dec")) throw new Exception(Language.Get("Error002"));

                    Content1 = File.ReadAllBytes(Paths.WorkingFolder + "content1.dec");
                    CompressedContent1 = true;
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

        public void ReplaceManual(string path)
        {
            if (path == null)
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

                        ReplaceContent(4, Content4);
                    }
                }
                catch
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
                if (Directory.Exists(Path.Combine(path, "emanual")))
                    foreach (var item in Directory.EnumerateFiles(Path.Combine(path, "emanual")))
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
                ManualArc.CreateFromDirectory(Path.Combine(path));
                Manual = ManualArc.ToByteArray();

                if (File.Exists(ManualPath)) File.WriteAllBytes(ManualPath, Manual);
                else Content5.ReplaceFile(Content5.GetNodeIndex(ManualPath), Manual);

                Manual = null;
            }
        }

        public WAD Write()
        {
            if (!WAD.Contents[1].SequenceEqual(Content1) || UsesContent1)
            {
                if (CompressedContent1)
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.dec", Content1);
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

                    Content1 = Recompressed;
                    CompressedContent1 = false;
                }

                ReplaceContent(1, Content1);
            }

            if (WAD.Contents.Length > 5 && (!WAD.Contents[5].SequenceEqual(Content5.ToByteArray()) || UsesContent5)) 
                ReplaceContent(5, Content5);

            return WAD;
        }

        private void ReplaceContent(int index, byte[] U8)
        {
            int length = WAD.Contents.Length;

            if (Content1 == null && index == 1) return;

            WAD.Contents[index] = U8;
            // Check if both source and replacement U8 are the same
            // ****************
            if (WAD.Contents[index].SequenceEqual(U8)) goto End;

            // Create new list of U8 contents, starting from the index of which to replace
            // ****************
            List<ContentType> U8Types = new List<ContentType>();
            List<byte[]> U8Contents = new List<byte[]>();
            IDictionary<int, int> U8Indexes = new Dictionary<int, int>();

            U8Types.Add(WAD.TmdContents[index].Type);
            U8Contents.Add(U8);
            U8Indexes.Add(index, WAD.TmdContents[index].Index);

            if (WAD.Contents.Length > index + 1)
                for (int i = index + 1; i < WAD.Contents.Length; i++)
                {
                    U8Types.Add(WAD.TmdContents[i].Type);
                    U8Contents.Add(WAD.Contents[i]);
                    U8Indexes.Add(i, WAD.TmdContents[i].Index);
                }

            // remove all U8 indexes starting from that
            // ****************
            for (int i = WAD.Contents.Length - 1; i >= index; i--)
                WAD.RemoveContent(i);

            // and add the new ones in
            // ****************
            for (int i = 0; i < U8Contents.Count; i++)
                WAD.AddContent(U8Contents.ElementAt(i), U8Indexes.ElementAt(i).Key, U8Indexes.ElementAt(i).Value, U8Types.ElementAt(i));

            if (WAD.Contents.Length != length)
                throw new InvalidOperationException();

            End:
            if (index == 1) Content1 = null;
        }

        private void ReplaceContent(int index, U8 U8) => ReplaceContent(index, U8.ToByteArray());

        public abstract void ReplaceROM();

        public abstract void ReplaceSaveData(string[] lines, TitleImage tImg);
    }
}
