using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FriishProduce.Injectors
{
    class PCE
    {
        public string ROM { get; set; }
        public string WAD_ID { get; set; }

        public void ReplaceROM()
        {
            string rom = null;
            foreach (string line in File.ReadAllLines(Paths.WorkingFolder_Content5 + "config.ini"))
                if (line.Contains("ROM=")) rom = line.Replace("ROM=/", string.Empty).Replace("ROM=", string.Empty);
            
            if (rom == null || !File.Exists(Paths.WorkingFolder_Content5 + rom)) throw new Exception(Program.Language.Get("m010"));

            // Maximum ROM limit allowed: ~2.5 MB
            if (File.ReadAllBytes(ROM).Length > 1048576 * 2.5)
                throw new Exception(Program.Language.Get("m018"));

            if (rom.Contains("LZ77"))
            {
                string pPath = Paths.WorkingFolder + "wwcxtool.exe";
                File.WriteAllBytes(pPath, Properties.Resources.WWCXTool);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = Paths.WorkingFolder,
                    Arguments = $"/cr \"{Paths.WorkingFolder_Content5 + rom}\" \"{Path.GetFullPath(ROM)}\" \"{Paths.WorkingFolder_Content5}LZ77.pce\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(pPath);

                if (!File.Exists(Paths.WorkingFolder_Content5 + "LZ77.pce")) throw new Exception("LZ77 fatal error");
                File.Delete(Paths.WorkingFolder_Content5 + rom);
                File.Move(Paths.WorkingFolder_Content5 + "LZ77.pce", Paths.WorkingFolder_Content5 + rom);
            }
            else File.Copy(ROM, Paths.WorkingFolder_Content5 + rom, true);
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
            string name = null;
            string rom = null;
            foreach (string line in File.ReadAllLines(Paths.WorkingFolder_Content5 + "config.ini"))
                if (line.Contains("NAME=")) name = line;
            foreach (string line in File.ReadAllLines(Paths.WorkingFolder_Content5 + "config.ini"))
                if (line.Contains("ROM=")) rom = line;

            List<string> config = new List<string>()
            {
                name,
                rom,
                $"BACKUPRAM={(BackupRAM ? "1" : "0")}",
                $"MULTITAP={(Multitap ? "1" : "0")}",
                "HDS=0",
                $"RASTER={(Raster ? "1" : "0")}",
                "SPRLINE=0",
                $"PAD5={(Pad5 ? "1" : "0")}",
                $"NOFPA={(NoFPA ? "1" : "0")}"
            };
            
            if (HideOverscan) config.Add("HIDEOVERSCAN=1");
            if (WAD_ID.ToUpper().StartsWith("PAC") || WAD_ID.ToUpper().StartsWith("PBE")) config.Add("DUNGEXPE=1");
            if (WAD_ID.ToUpper().StartsWith("PC2")) config.Add("CHASEHQ=1");

            File.WriteAllLines(Paths.WorkingFolder_Content5 + "config.ini", config.ToArray());
        }

        // Documentation for available settings:
        // All bool options can be enabled or disabled using "0"/"1"
        // ---------------------------------------------------------
        // Name         | Type
        // ---------------------------------------------------------
        // ROM            String. Filename & extension of ROM to load
        // BACKUPRAM      Bool. Toggles save function
        // EUROPE         Bool. Probably sets region, disable if installing on Japanese Wii.
        // SGENABLE       Bool. Toggles SuperGrafx use (Daimakaimura & Darius Plus use it by default)
        // PADBUTTON      Either 6 or 2. Sets controller type by no. of buttons. If 6-button input does not work, use default 2-button pad.
        // MULTITAP       Bool. Toggles virtual multitap for 4 player mode
        // PAD5           Bool. Toggles controller #5 (with at least GC controller). MULTITAP must be enabled & RLmessages.bin must be present except when using Bomberman '93 as base
        // HIDEOVERSCAN   Bool. Enables black borders on screen (Castlevania: RoB uses it by default)
        // YOFFSET        Whole positive integer (0; 3; 7; ..). Centers screen down by number specified
        // RASTER         Bool. Enables raster for scrolling BG fix but may slow down some games.
        // SPRLINE        Bool. Probably a function which enables sprite limit if set to 1.
        // ---------------------------------------------------------
        // CD_VOLUME      Decimal value (min 0.1, max 1.0). Sets CD (.ogg) audio volume. TG/PCE-CD only
        // PSG_VOLUME     Decimal value (min 0.1, max 1.0). Sets PSG (.bin) volume. TG/PCE-CD only
        // ADPCM_VOLUME   Decimal value (min 0.1, max 1.0). Sets ADPCM volume. TG/PCE-CD only
        // ---------------------------------------------------------
        // ARCADE         Bool. Unknown use.
        // IRQMODE        Bool. Unknown use. Enabled on Gradius II
        // PATCH          Bool. Unknown use. Enabled on Gradius II (EUR)
        // HDS            Unknown.
        // WDITH320OVER   Unknown.
        // NOFPA          Bool. Always enabled on official VC channels, but no idea what it does otherwise
        // ---------------------------------------------------------
        // DUNGEXPE       Enabled by default on Dungeon Explorer, Moto Roader. DO NOT TOUCH
        // POPULUS        Probably enabled by default on Populous, DO NOT TOUCH
        // CHASEHQ        Enabled by default on Chase H.Q., DO NOT TOUCH

        // ------------------------------------------------
        // CONFIG from Bomberman'93:
        // ------------------------------------------------
        /* NAME=N43406YP
           ROM=N43406YP.PCE
           BACKUPRAM=1
           CHASEHQ=0
           MULTITAP=1
           HDS=0
           RASTER=0
           POPULUS=0
           SPRLINE=0
           PAD5=1
           NOFPA=1 */

        // ------------------------------------------------
        // CONFIG from Castlevania Rondo of Blood:
        // ------------------------------------------------
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
    }
}
