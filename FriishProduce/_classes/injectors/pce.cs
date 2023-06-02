using System;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce.Injectors
{
    class PCE
    {
        public string ROM { get; set; }

        public void ReplaceROM()
        {
            string rom = null;
            foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                if (Path.GetExtension(file).ToLower() == ".pce") rom = file;
            
            if (rom == null) throw new Exception(Program.Language.Get("m010"));

            // Maximum ROM limit allowed: ~2.5 MB
            if (File.ReadAllBytes(ROM).Length > 1048576 * 2.5)
                throw new Exception(Program.Language.Get("m018"));

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
           PAD5=1
           NOFPA=1 */

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
        
        // docs for the different settings on CONFIG on TG-16 / PCE HuCard / CD VC: (by saulfabreg)
        /* ROM - name of the rom file to load
           BACKUPRAM - tell that the game uses a saving function.
           (0 = disabled, 1 = enabled)
           MULTITAP - connects virtual Multitap which also connect 4 controllers (players).
           (0 = disabled, 1 = enabled)
           PAD5 - allows to use Pad 5 (5th controller) on VC, with at least a GameCube controller.
           Needs to set MULTITAP=1 and the RLmessages.bin must be present on the 00000005.app for use Pad 5 (except Bomberman '93).
           (0 = disabled, 1 = enabled)
           RASTER - enables raster affect for make scrolling backgrounds to work correctly on some games.
           DISCLAIMER: Games that doesn't support raster might slowdown! (ex. Rainbow Islands)
           (0 = raster disabled, 1 = raster enabled)
           EUROPE - disable if installing on a Japan Wii, activate if installing on an American or Europe Wii.
           HIDEOVERSCAN - enables black borders to be displayed on the game. It is officially enabled on "Castlevania: Rondo of Blood".
           (0 = disabled, 1 = enabled)
           SGENABLE - enables SuperGrafx emulation for some games which required that hardware.
           It is officially enabled on the VC releases "Daimakaimura" (NTSC-J) and "Darius Plus" (NTSC-J).
           (0 = disable SGX mode, 1 = enable SGX mode)
           SPRLINE - not 100% sure but i think is for disable sprite limit on some games.
           (0 = disable sprite limit, 1 = enable sprite limit)
           PADBUTTON - adjust what number of buttons need to have the emulated PCE controller.
           It is officially set to "6" on the VC release "Street Fighter II': Championship Edition".
           DISCLAIMER: Many PCE/TG games doesn't support 6-button controller and if enabled, it will result in input issues!
           If any input issue occurs on 6-button controller, please enable default 2-button controller instead.
           (6 = use 6-button controller, 2 = use default 2-button controller)
           YOFFSET - moves the screen down (Y axis) to centre the picture.
           Used on "Castlevania: Rondo of Blood". Some games (such Bonanza Bros.) needs also to adjust this setting.
           Use whole positive numbers like 0, 3, 7, 9, etc.
           
           Only on PCE / TG-16 CD games:
           CD_VOLUME, ADPCM_VOLUME, PSG_VOLUME - these adjusts the CD (.ogg) music, PSG (music playing from the .bin files) and ADPCM volume.
           Used on "Gradius II: Gofer no Yabou".
           Use 1 decimal point for the volume value, for example: 0.8, 0.6, 1.0, etc. (min 0.1, max 1.0)
           
           Settings that MUSTN'T BE TOUCHED OF ANY WAY:
           DUNGEXPE - enabled on "Dungeon Explorer" and "Moto Roader"
           POPULUS - not sure but this was supposed to be enabled on "Populous", but i'm not sure
           CHASEHQ - enabled on "Chase H.Q."
           
           Some settings i don't know its use:
           ARCADE - ?¿
           IRQMODE - ?¿ (it's enabled on "Gradius II: Gofer no Yabou")
           PATCH - ?¿ (it's enabled on "Gradius II: Gofer no Yabou" (EUR/PAL60))
           NOFPA - ?¿ (it's always enabled on official Wii VC channels)
           HDS - ?¿
           WDITH320OVER - ?¿ */
    }
}
