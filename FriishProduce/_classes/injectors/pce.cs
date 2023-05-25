using System;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce.Injectors
{
    class PCE
    {
        // CONFIG from Bomberman'93:
        /* NAME=N43406YP
           ROM=N43406YP.PCE
           BACKUPRAM=1
           CHASEHQ=0
           MULTITAP=1
           HDS=0
           RASTER=0
           POPULUS=0
           SPRLINE=0
           PAD5=1 */

        // CONFIG from Castlevania Rondo of Blood:
        /* NAME=KMCD3005
           ROM=KMCD3005.hcd
           BACKUPRAM=1
           MULTITAP=1
           HDS=0
           RASTER=0
           SPRLINE=0
           NOFPA=1
           IRQMODE=0
           EUROPE=1
           HIDEOVERSCAN=1
           YOFFSET=8 */

        public string ROM { get; set; }
        public string ROMcode { get; set; }

        public void ReplaceROM()
        {
            string rom = null;
            foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                if (Path.GetExtension(file).ToLower() == ".pce") rom = file;
            
            if (rom == null) throw new Exception(Program.Language.Get("m010"));

            if (File.ReadAllBytes(ROM).Length > File.ReadAllBytes(rom).Length)
                throw new Exception(Program.Language.Get("m004"));

            File.Copy(ROM, rom, true);
        }

        internal void InsertSaveTitle(string title)
        {
            string[] file = File.ReadAllLines(Paths.WorkingFolder_Content5 + "TITLE.TXT");
            file[0] = title;

            using (Stream s = new FileStream(Paths.WorkingFolder_Content5 + "TITLE.TXT", FileMode.Create))
            using (TextWriter t = new StreamWriter(s, System.Text.Encoding.BigEndianUnicode))
            {
                for (int i = 0; i < file.Length; i++)
                    t.WriteLine(file[i]);
            }
        }

        internal void SetConfig (bool Multitap = true,
                                 bool Pad5 = true,
                                 bool BackupRAM = true,
                                 bool HideOverscan = false,
                                 bool Raster = false,
                                 bool NoFPA = true)
        {
            string romcode = null;
            foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                if (Path.GetExtension(file).ToLower() == ".pce") romcode = Path.GetFileNameWithoutExtension(file);

            List<string> config = new List<string>()
            {
                $"NAME={romcode}",
                $"ROM={romcode}.PCE",
                $"BACKUPRAM={(BackupRAM ? "1" : "0")}",
                "CHASEHQ=0",
                $"MULTITAP={(Multitap ? "1" : "0")}",
                "HDS=0",
                $"RASTER={(Raster ? "1" : "0")}",
                "POPULUS=0",
                "SPRLINE=0",
                $"PAD5={(Pad5 ? "1" : "0")}",
                $"NOFPA={(NoFPA ? "1" : "0")}",
            };
            
            if (HideOverscan) config.Add("HIDEOVERSCAN=1");

            File.WriteAllLines(Paths.WorkingFolder_Content5 + "config.ini", config.ToArray());
        }
    }
}
