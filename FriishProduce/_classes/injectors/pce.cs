using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FriishProduce.Injectors
{
    class PCE
    {
        public string ROM { get; set; }
        public bool LZ77 { get; set; }

        public void ReplaceROM()
        {
            string rom = null;
            foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                if (Path.GetExtension(file).ToLower() == ".pce") rom = file;
            
            if (rom == null) throw new Exception(Program.Language.Get("m010"));

            // Maximum ROM limit allowed: ~2.5 MB
            if (File.ReadAllBytes(ROM).Length > 1048576 * 2.5)
                throw new Exception(Program.Language.Get("m018"));

            if (LZ77)
            {
                string pPath = Paths.WorkingFolder + "wwcxtool.exe";
                File.WriteAllBytes(pPath, Properties.Resources.WWCXTool);
                using (Process p = Process.Start(new ProcessStartInfo
                {
                    FileName = pPath,
                    WorkingDirectory = Paths.WorkingFolder,
                    Arguments = $"/cr \"{rom}\" \"{ROM}\" \"{rom}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                    p.WaitForExit();
                File.Delete(pPath);
            }
            else File.Copy(ROM, rom, true);
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

        // Documentation
        // All bool options can be enabled or disabled using 0/1
        // ---------------------------------------------------------
        // Name         | Type
        // ---------------------------------------------------------
        // ROM            String. Filename & extension of ROM to load
        // BACKUPRAM      Bool. Toggles save function
        // EUROPE         Bool. Probably sets region, disable if installing on Japanese Wii.
        // SGENABLE       Bool. Toggles SuperGrafx use (Daimakaimura & Darius Plus use it by default)
        // PADBUTTON      Either 6 or 2. Sets controller type by no. of buttons. If 6-button input does not work, use default 2-button pad.
        // HIDEOVERSCAN   Bool. Enables black borders on screen (Castlevania: RoB uses it by default)
        // YOFFSET        Whole positive integer (0; 3; 7; ..). Centers screen down by number specified
        // ---------------------------------------------------------
        // NOFPA          Always enabled on official VC channels, but no idea what it does otherwise
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

        // docs for the different settings on CONFIG on TG-16 / PCE HuCard / CD VC: (by saulfabreg)
        /* MULTITAP - connects virtual Multitap which also connect 4 controllers (players).
           (0 = disabled, 1 = enabled)
           PAD5 - allows to use Pad 5 (5th controller) on VC, with at least a GameCube controller.
           Needs to set MULTITAP=1 and the RLmessages.bin must be present on the 00000005.app for use Pad 5 (except Bomberman '93).
           (0 = disabled, 1 = enabled)
           RASTER - enables raster affect for make scrolling backgrounds to work correctly on some games.
           DISCLAIMER: Games that doesn't support raster might slowdown! (ex. Rainbow Islands)
           (0 = raster disabled, 1 = raster enabled)
           SPRLINE - not 100% sure but i think is for disable sprite limit on some games.
           (0 = disable sprite limit, 1 = enable sprite limit)
           
           Only on PCE / TG-16 CD games:
           CD_VOLUME, ADPCM_VOLUME, PSG_VOLUME - these adjusts the CD (.ogg) music, PSG (music playing from the .bin files) and ADPCM volume.
           Used on "Gradius II: Gofer no Yabou".
           Use 1 decimal point for the volume value, for example: 0.8, 0.6, 1.0, etc. (min 0.1, max 1.0)
           
          
           
           Some settings i don't know its use:
           ARCADE - ?¿
           IRQMODE - ?¿ (it's enabled on "Gradius II: Gofer no Yabou")
           PATCH - ?¿ (it's enabled on "Gradius II: Gofer no Yabou" (EUR/PAL60))
           HDS - ?¿
           WDITH320OVER - ?¿ */
    }
}
