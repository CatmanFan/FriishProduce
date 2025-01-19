using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
{
    public class C64 : InjectorWiiVC
    {
        protected override void Load()
        {
            needsMainDol = true;
            mainContentIndex = 5;
            needsManualLoaded = true;
            SaveTextEncoding = Encoding.Unicode;

            base.Load();
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory.
        /// </summary>
        protected override void ReplaceROM()
        {
            ROM.CheckSize();

            // Define variables
            // ****************
            byte[] data = (ROM as ROM_C64).ToD64();

            string frodo = Paths.Tools + "frodosrc\\";
            string target = frodo + "rom.d64";
            string snapshot = frodo + "snap.fss";

            int ik_fss_index = 0;

            // Replace D64 and get snapshot
            // ****************
            foreach (var item in MainContent.StringTable)
            {
                if (Path.GetExtension(item).ToLower() == ".d64")
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), data);

                if (item.ToLower().Contains("lz77") && item.ToLower().Contains("snapshot"))
                {
                    ik_fss_index = MainContent.GetNodeIndex(item);

                    File.WriteAllBytes(Paths.WorkingFolder + "ik.fss.comp", MainContent.Data[ik_fss_index]);
                    Utils.Run
                    (
                        FileDatas.Apps.gbalzss,
                        "gbalzss",
                        "d ik.fss.comp ik.fss"
                    );

                    try { File.Delete(frodo + "ik.fss"); } catch { }
                    try { File.Move(Paths.WorkingFolder + "ik.fss", frodo + "ik.fss"); } catch { }
                    File.Delete(Paths.WorkingFolder + "ik.fss.comp");
                }
            }

            // Check if original snapshot was found
            // ****************
            if (!File.Exists(frodo + "ik.fss"))
                throw new Exception(Program.Lang.Msg(13, true));

            // Check if copy of snapshot exists with ROM, if not, skip
            // ****************
            if (File.Exists(Path.Combine(Path.GetDirectoryName(ROM.FilePath), Path.GetFileNameWithoutExtension(ROM.FilePath) + ".fss")))
                File.WriteAllBytes(snapshot, File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(ROM.FilePath), Path.GetFileNameWithoutExtension(ROM.FilePath) + ".fss")));

            // Copy ROM
            // ****************
            if (!File.Exists(snapshot))
            {
                File.WriteAllBytes(target, data);

                ProcessStartInfo info = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = false,
                    WorkingDirectory = frodo,
                    Arguments = $"/c \"{frodo + "copy.bat"}\""
                };

                using (Process p = new())
                {
                    p.StartInfo = info;
                    p.Start();
                    p.WaitForExit();
                }

                int tries = 5;

                Load:
                // Edit Frodo config to autoload said ROM
                // ****************
                string[] config = File.ReadAllLines(frodo + "Frodo.fpr");
                for (int i = 0; i < config.Length; i++)
                    if (config[i].StartsWith("DrivePath8")) config[i] = "DrivePath8 = rom.d64";
                File.WriteAllLines(frodo + "Frodo.fpr", config);

                // Run Frodo
                // ****************
                HTMLForm h = new(string.Format(Program.Lang.HTML(0, false), tries));
                h.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                h.TopMost = true;
                h.ShowDialog();
                h.Dispose();

                Utils.Run(frodo + "Frodo.exe", frodo, null, true);

                if (!File.Exists(snapshot))
                {
                    if (tries > 0)
                    {
                        tries -= 1;
                        goto Load;
                    }

                    else
                        throw new Exception(Program.Lang.Msg(2, true));
                }

                using (Process p = new())
                {
                    p.StartInfo = info;
                    p.StartInfo.Arguments = $"/c \"{frodo + "delete.bat"}\"";
                    p.Start();
                    p.WaitForExit();
                }
            }

            try { File.Delete(frodo + "ik.fss"); } catch { }
            try { File.Delete(Paths.WorkingFolder + "ik.fss"); } catch { }
            try { File.Delete(target); } catch { }

            File.Move(snapshot, Paths.WorkingFolder + "ik.fss");
            Utils.Run
            (
                FileDatas.Apps.gbalzss,
                "gbalzss",
                "e ik.fss ik.fss.comp"
            );
            MainContent.ReplaceFile(ik_fss_index, File.ReadAllBytes(Paths.WorkingFolder + "ik.fss.comp"));
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            // -----------------------
            // TEXT
            // -----------------------

            lines = ConvertSaveText(lines);

            File.WriteAllBytes(Paths.WorkingFolder + "banner.bin", Contents[1]);

            byte[] pattern = new byte[] { 0x2F, 0x62, 0x61, 0x6E, 0x6E, 0x65, 0x72, 0x2E, 0x62, 0x69, 0x6E, 0x00, 0x00 };
            int index = Byte.IndexOf(Contents[1], pattern, 850000, 1000000) + pattern.Length;

            if (index > 0)
            {
                byte[] text = new byte[68];
                SaveTextEncoding.GetBytes(lines[0]).CopyTo(text, 0);
                text.CopyTo(Contents[1], index);
            }

            // -----------------------
            // IMAGE
            // -----------------------

            // LZ77_banner.tpl
            // NOT IMPLEMENTED

            // if (Img != null) MainContent.ReplaceFile(MainContent.GetNodeIndex("LZ77_banner.tpl"), Img.CreateSaveTPL(MainContent.Data[MainContent.GetNodeIndex("savedata.tpl")]).ToByteArray());
        }

        protected override void ModifyEmulatorSettings()
        {
        }
    }
}
