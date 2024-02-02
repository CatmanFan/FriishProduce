using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using libWiiSharp;

namespace FriishProduce
{
    public class InjectorBase
    {
        protected WAD WAD { get; set; }
        protected byte[] Content1 { get; set; }
        protected U8 Content4 { get; set; }
        protected U8 Content5 { get; set; }

        protected bool UsesContent1 { get; set; }
        protected bool UsesContent5 { get; set; }
        private bool CompressedContent1 { get; set; }

        public InjectorBase(WAD w)
        {
            WAD = w;
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
            // TO-DO: Uncompress 00000001.app if prompted
            // "wwcxtool.exe /u 00000001.app 00000001_dec.app"                      original input, decompressed output
            // "wwcxtool.exe /cr 00000001.app 00000001_dec.app 00000001_out.app"    original input, decompressed output as reference, output, then delete both the first two files

            if (WAD.Contents.Length > 5 || UsesContent5)
            {
                Content5 = new U8();
                Content5.LoadFile(WAD.Contents[5]);
            }
        }

        public WAD Write()
        {
            if (WAD.Contents[1] != Content1 || UsesContent1)
            {
                if (!CompressedContent1)
                {
                    WAD.Contents[1] = Content1;
                    if (WAD.Contents[1] != Content1) WADKit.ReplaceContent(WAD, 1, Content1);
                }
                else
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "content1.dec", Content1);
                    Process.Run
                    (
                        Paths.Tools + "wwcxtool.exe",
                        Paths.WorkingFolder,
                        "/cr content1.app content1.dec content1.rec"
                    );
                    if (!File.Exists(Paths.WorkingFolder + "content1.rec")) throw new Exception(Language.Get("Error002"));

                    byte[] Recompressed = File.ReadAllBytes(Paths.WorkingFolder + "content1.rec");

                    File.Delete(Paths.WorkingFolder + "content1.rec");
                    if (File.Exists(Paths.WorkingFolder + "content1.dec")) File.Delete(Paths.WorkingFolder + "content1.dec");
                    if (File.Exists(Paths.WorkingFolder + "content1.app")) File.Delete(Paths.WorkingFolder + "content1.app");

                    WAD.Contents[1] = Recompressed;
                    Content1 = Recompressed;
                    if (WAD.Contents[1] != Recompressed) WADKit.ReplaceContent(WAD, 1, Recompressed);
                }
            }

            if (WAD.Contents[5] != Content5.ToByteArray() || UsesContent5)
                WADKit.ReplaceContent(WAD, 5, Content5.ToByteArray());

            return WAD;
        }
    }
}
