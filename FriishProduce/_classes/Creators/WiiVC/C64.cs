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
                    if (File.Exists(Paths.WorkingFolder + "ik.fss.comp")) File.Delete(Paths.WorkingFolder + "ik.fss.comp");

                    if (!File.Exists(frodo + "Frodo.exe"))
                        throw new Exception(string.Format(Program.Lang.Msg(6, true), frodo.Replace(Paths.Tools, null) + "Frodo.exe"));

                    File.Copy(Paths.WorkingFolder + "ik.fss", frodo + "ik.fss", true);
                    if (File.Exists(Paths.WorkingFolder + "ik.fss")) File.Delete(Paths.WorkingFolder + "ik.fss");
                }
            }

            // Check if original snapshot was found
            // ****************
            if (!File.Exists(frodo + "ik.fss"))
                throw new Exception(Program.Lang.Msg(13, true));

            // Prompt to use existing copy
            // ****************
            Prompt:
            bool cancel = false;
            var method = MessageBox.Show(Program.Lang.String("rom_notice", "vc_c64"), null, new string[] { Program.Lang.String("rom_notice1", "vc_c64"), Program.Lang.String("rom_notice2", "vc_c64"), Program.Lang.String("b_cancel") });

            switch (method)
            {
                default:
                case MessageBox.Result.Button1:
                    goto Frodo;

                case MessageBox.Result.Button2:
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.DialogResult.None;
                    string filename = null;

                    Program.MainForm.Invoke((Action)(() => {
                        using (System.Windows.Forms.OpenFileDialog open = new()
                        {
                            Filter = "FSS (*.fss)|*.fss",
                            CheckFileExists = true,
                            CheckPathExists = true,
                            AddExtension = true,
                            Multiselect = false,
                            DefaultExt = ".fss",
                            Title = Program.Lang.String("rom_notice2", "vc_c64")
                        })
                        {
                            result = open.ShowDialog();
                            filename = open.FileName;
                        }
                    }), null);

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        File.WriteAllBytes(snapshot, File.ReadAllBytes(filename));
                        goto End;
                    }
                    else
                        goto Prompt;

                case MessageBox.Result.Cancel:
                    cancel = true;
                    goto End;
            }

            Frodo:
            // Copy ROM
            // ****************
            File.WriteAllBytes(target, data);

            // Copy Frodo files
            // ****************
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = frodo + "copy.bat",
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Minimized,
                CreateNoWindow = true,
                UseShellExecute = true,
                RedirectStandardInput = false
            };

            using (Process p = new())
            {
                p.StartInfo = info;
                p.Start();
                p.WaitForExit();
            }

            int tries = 5;

            Frodo_Load:
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
                tries -= 1;

                if (tries > 0)
                    goto Frodo_Load;

                else
                {
                    cancel = true;
                    goto End;
                }
            }

            using (Process p = new())
            {
                info.FileName = frodo + "copy.bat";

                p.StartInfo = info;
                p.Start();
                p.WaitForExit();
            }

            End:
            try { File.Delete(frodo + "ik.fss"); } catch { }
            try { File.Delete(target); } catch { }

            if (!cancel)
            {
                File.Copy(snapshot, Paths.WorkingFolder + "ik.fss", true);
                if (File.Exists(snapshot)) File.Delete(snapshot);

                Utils.Run
                (
                    FileDatas.Apps.gbalzss,
                    "gbalzss",
                    "e ik.fss ik.fss.comp"
                );
                if (!File.Exists(Paths.WorkingFolder + "ik.fss.comp")) throw new Exception(Program.Lang.Msg(2, true));

                MainContent.ReplaceFile(ik_fss_index, File.ReadAllBytes(Paths.WorkingFolder + "ik.fss.comp"));
            }
            
            else throw new OperationCanceledException();
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

            if (Img != null)
            {
                int tpl_index = MainContent.GetNodeIndex("LZ77_banner.tpl");
                if (tpl_index == -1) return;

                byte[] tpl = null, new_tpl = null;
                int tries = 0;

                Extract:
                tpl = MainContent.Data[tpl_index];

                try
                {
                    if (tries == 0 || tries == 1)
                    {
                        File.WriteAllBytes(Paths.WorkingFolder + "LZ77_banner.tpl", tpl);

                        Utils.Run
                        (
                            tries == 0 ? FileDatas.Apps.wwcxtool : FileDatas.Apps.gbalzss,
                            tries == 0 ? "wwcxtool.exe" : "gbalzss",
                            tries == 0 ? "/u LZ77_banner.tpl banner.tpl" : "d LZ77_banner.tpl banner.tpl"
                        );

                        tpl = File.ReadAllBytes(Paths.WorkingFolder + "banner.tpl");
                    }

                    else if (tries == 2)
                    {
                        libWiiSharp.Lz77 l = new();
                        tpl = l.Decompress(tpl);
                    }

                    new_tpl = Img.CreateSaveTPL(tpl).ToByteArray();
                }

                catch (Exception ex)
                {
                    if (tries >= 3 || ex.InnerException?.Message != "TPL Header: Invalid Magic!")
                        throw ex;

                    tries++;
                    goto Extract;
                }

                if (tries == 0 || tries == 1)
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "banner.tpl", new_tpl);

                    Utils.Run
                    (
                        tries == 0 ? FileDatas.Apps.wwcxtool : FileDatas.Apps.gbalzss,
                        tries == 0 ? "wwcxtool.exe" : "gbalzss",
                        tries == 0 ? "/cr LZ77_banner.tpl banner.tpl new.tpl" : "e banner.tpl new.tpl"
                    );

                    new_tpl = File.ReadAllBytes(Paths.WorkingFolder + "new.tpl");
                }

                else if (tries == 2)
                {
                    libWiiSharp.Lz77 l = new();
                    new_tpl = l.Compress(new_tpl);
                }

                MainContent.ReplaceFile(tpl_index, new_tpl);
            }
        }

        protected override void ModifyEmulatorSettings()
        {
        }
    }
}
