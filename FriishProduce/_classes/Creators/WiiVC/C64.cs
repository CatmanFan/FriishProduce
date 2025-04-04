﻿using System;
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

        public static bool Setup(bool clean = false)
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = Paths.Frodo + (clean ? "delete.bat" : "copy.bat"),
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Minimized,
                CreateNoWindow = true,
                UseShellExecute = true,
                RedirectStandardInput = false
            };

            using (Process p = Process.Start(info))
            {
                if (p != null)
                {
                    p.WaitForExit();
                    return true;
                }

                else return false;
            }
        }

        /// <summary>
        /// Cleans Frodo directory.
        /// </summary>
        public static void Clean()
        {
            try { File.Delete(Paths.FrodoRom); } catch { }
            try { File.Delete(Paths.FrodoSnapshot); } catch { }
            try { File.Delete(Paths.FrodoOutput); } catch { }

            try { File.Delete(Paths.Tools + "c64\\c1541\\stderr.txt"); } catch { }
            try { File.Delete(Paths.Tools + "c64\\c1541\\stdout.txt"); } catch { }

            if (File.Exists(@"C:\1541 ROM") || File.Exists(@"C:\Basic ROM") || File.Exists(@"C:\Char ROM") || File.Exists(@"C:\Kernal ROM"))
                Setup(true);
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory.
        /// </summary>
        protected override void ReplaceROM()
        {
            ROM.CheckSize();

            if (!File.Exists(Paths.Frodo + "Frodo.exe") || !Directory.Exists(Paths.Frodo))
                throw new Exception(string.Format(Program.Lang.Msg(6, 1), Paths.Frodo.Replace(Paths.Tools, null) + "Frodo.exe"));

            // Define variables
            // ****************
            byte[] data = (ROM as ROM_C64).ToD64();

            Clean();

            int ik_fss_index = 0;

            // Replace D64 and get snapshot
            // ****************
            foreach (var item in MainContent.StringTable)
            {
                if (Path.GetExtension(item).ToLower() == ".d64")
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), data);

                if (item.ToLower().Contains("lz77") && item.ToLower().Contains("snapshot"))
                {
                    // Extract compressed ik.fss and decompress
                    // ****************
                    ik_fss_index = MainContent.GetNodeIndex(item);

                    File.WriteAllBytes(Paths.WorkingFolder + "ik.fss.comp", MainContent.Data[ik_fss_index]);
                    Utils.Run
                    (
                        FileDatas.Apps.gbalzss,
                        "gbalzss",
                        "d ik.fss.comp ik.fss"
                    );
                    try { File.Delete(Paths.WorkingFolder + "ik.fss.comp"); } catch { }

                    // Copy decompressed snapshot to Frodo directory
                    // ****************
                    File.Copy(Paths.WorkingFolder + "ik.fss", Paths.FrodoSnapshot, true);
                    try { File.Delete(Paths.WorkingFolder + "ik.fss"); } catch { }
                }
            }

            // Check if original snapshot was found
            // ****************
            if (!File.Exists(Paths.FrodoSnapshot))
                throw new Exception(Program.Lang.Msg(13, 1));

            // Prompt to use existing copy
            // ****************
            Prompt:
            bool cancel = false;
            var method = MessageBox.Show(Program.Lang.String("rom_notice", "vc_c64"), null, new string[] { Program.Lang.String("rom_notice1", "vc_c64"), Program.Lang.String("rom_notice2", "vc_c64"), "b_cancel" });

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
                            Title = Program.Lang.String("rom_notice2", "vc_c64").Replace("&", "")
                        })
                        {
                            result = open.ShowDialog();
                            filename = open.FileName;
                        }
                    }), null);

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        File.WriteAllBytes(Paths.FrodoOutput, File.ReadAllBytes(filename));
                        goto End;
                    }
                    else
                        goto Prompt;

                case MessageBox.Result.Cancel:
                    cancel = true;
                    goto End;
            }

            Frodo:
            // Copy Frodo files
            // ****************
            if (!Setup())
            {
                cancel = true;
                goto End;
            }

            // Copy ROM
            // ****************
            File.WriteAllBytes(Paths.FrodoRom, data);

            int tries = 5;

            Frodo_Load:
            // Edit Frodo config to autoload said ROM
            // ****************
            string[] config = File.ReadAllLines(Paths.Frodo + "Frodo.fpr");
            for (int i = 0; i < config.Length; i++)
                if (config[i].StartsWith("DrivePath8")) config[i] = "DrivePath8 = rom.d64";
            File.WriteAllLines(Paths.Frodo + "Frodo.fpr", config);

            // Run Frodo
            // ****************
            if (Program.GUI)
                using (MessageBoxHTML h = new(string.Format(Program.Lang.HTML(0, false), tries)))
                {
                    h.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    h.ShowDialog();
                }

            Utils.Run(Paths.Frodo + "Frodo.exe", Paths.Frodo, null, true);

            if (!File.Exists(Paths.FrodoOutput))
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

            End:

            if (!cancel)
            {
                File.Copy(Paths.FrodoOutput, Paths.WorkingFolder + "ik.fss", true);

                Utils.Run
                (
                    FileDatas.Apps.gbalzss,
                    "gbalzss",
                    "e ik.fss ik.fss.comp"
                );

                if (!File.Exists(Paths.WorkingFolder + "ik.fss.comp"))
                    throw new Exception(Program.Lang.Msg(2, 1));
                else
                    MainContent.ReplaceFile(ik_fss_index, File.ReadAllBytes(Paths.WorkingFolder + "ik.fss.comp"));
            }

            Clean();

            if (cancel) throw new OperationCanceledException();
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
                bool isLZ77 = tpl_index != -1;

                if (tpl_index == -1) return;

                byte[] tpl = MainContent.Data[tpl_index];

                if (isLZ77)
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "LZ77_banner.tpl", tpl);

                    Utils.Run
                    (
                        FileDatas.Apps.wwcxtool,
                        "wwcxtool.exe",
                        "/u LZ77_banner.tpl banner.tpl"
                    );

                    tpl = File.ReadAllBytes(Paths.WorkingFolder + "banner.tpl");
                }

                byte[] new_tpl = Img.CreateSaveTPL(tpl).ToByteArray();

                if (isLZ77)
                {
                    File.WriteAllBytes(Paths.WorkingFolder + "banner.tpl", new_tpl);

                    Utils.Run
                    (
                        FileDatas.Apps.wwcxtool,
                        "wwcxtool.exe",
                        "/cr LZ77_banner.tpl banner.tpl new.tpl"
                    );

                    new_tpl = File.ReadAllBytes(Paths.WorkingFolder + "new.tpl");
                }

                MainContent.ReplaceFile(tpl_index, new_tpl);
            }
        }

        protected override void ModifyEmulatorSettings()
        {
        }
    }
}
