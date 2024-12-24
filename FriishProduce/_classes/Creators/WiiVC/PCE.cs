using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
{
    public class PCE : InjectorWiiVC
    {
        private string ID { get; set; }
        private string Target { get; set; }

        protected override void Load()
        {
            needsMainDol = false;
            mainContentIndex = 5;
            needsManualLoaded = true;
            SaveTextEncoding = Encoding.BigEndianUnicode;

            base.Load();
        }

        private int ConfigIndex
        {
            get
            {
                foreach (var item in new string[] { "config.ini", "CONFIG.INI", "Config.ini", "CONFIG.ini", "config.ini", "Config.INI" })
                    if (MainContent.GetNodeIndex(item) != -1) return MainContent.GetNodeIndex(item);

                throw new Exception("No Config!");
            }
        }

        private string Config
        {
            get => Encoding.Default.GetString(MainContent.Data[ConfigIndex]);
        }

        /// <summary>
        /// Replaces ROM within extracted content5 directory.
        /// </summary>
        protected override void ReplaceROM()
        {
            ROM.CheckSize();

            string rom = null;
            foreach (string line in Config.Replace("\r\n", "\n").Split('\n'))
                if (line.Contains("ROM=")) rom = line.Replace("ROM=/", string.Empty).Replace("ROM=", string.Empty);

            if (rom == null || !MainContent.StringTable.Contains(rom))
                throw new Exception(Program.Lang.Msg(2, true));

            // -----------------------
            // Replace original ROM
            // -----------------------

            // LZ77-compressed
            // ****************
            if (rom.ToLower().Contains("lz77"))
            {
                File.WriteAllBytes(Paths.WorkingFolder + "rom_comp", MainContent.Data[MainContent.GetNodeIndex(rom)]);
                File.WriteAllBytes(Paths.WorkingFolder + "rom", ROM.Bytes);

                Utils.Run
                (
                    Paths.Tools + "wwcxtool.exe",
                    Paths.WorkingFolder,
                    "/cr rom_comp rom rom_new"
                );

                if (!File.Exists(Paths.WorkingFolder + "rom_new"))
                    throw new Exception(Program.Lang.Msg(2, true));
                else
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(rom), File.ReadAllBytes(Paths.WorkingFolder + "rom_new"));

                if (File.Exists(Paths.WorkingFolder + "rom_new")) File.Delete(Paths.WorkingFolder + "rom_new");
                if (File.Exists(Paths.WorkingFolder + "rom")) File.Delete(Paths.WorkingFolder + "rom");
                if (File.Exists(Paths.WorkingFolder + "rom_comp")) File.Delete(Paths.WorkingFolder + "rom_comp");
            }

            // Normal
            // ****************
            else MainContent.ReplaceFile(MainContent.GetNodeIndex(rom), ROM.Bytes);
        }

        protected override void ReplaceSaveData(string[] lines, ImageHelper Img)
        {
            // -----------------------
            // TEXT
            // -----------------------

            lines = ConvertSaveText(lines);

            using (var s = new MemoryStream())
            {
                var m = new StreamReader(new MemoryStream(MainContent.Data[MainContent.GetNodeIndex("TITLE.TXT")]), SaveTextEncoding);
                var t = new StreamWriter(s, SaveTextEncoding);

                int line = 0;
                string ln;

                while ((ln = m.ReadLine()) != null)
                {
                    t.WriteLine(line == 0 ? lines[0] : ln);
                    line++;
                }

                t.Flush();

                s.Seek(0, SeekOrigin.Begin);
                MainContent.ReplaceFile(MainContent.GetNodeIndex("TITLE.TXT"), s.ToArray());

                t.Dispose();
            }

            // -----------------------
            // IMAGE
            // -----------------------

            MainContent.ReplaceFile(MainContent.GetNodeIndex("savedata.tpl"), Img.CreateSaveTPL(MainContent.Data[MainContent.GetNodeIndex("savedata.tpl")]).ToByteArray());
        }

        protected override void ModifyEmulatorSettings()
        {
            string name = null;
            string rom = null;

            foreach (string line in Config.Replace("\r\n", "\n").Split('\n'))
            {
                if (line.Contains("NAME=")) name = line;
                if (line.Contains("ROM=")) rom = line;
            }

            using (var s = new MemoryStream())
            {
                var t = new StreamWriter(s, new UTF8Encoding(false));

                List<string> ConfigFile = new List<string>()
                {
                    name,
                    rom
                };

                foreach (KeyValuePair<string, string> pair in Settings)
                {
                    ConfigFile.Add($"{pair.Key.ToUpper()}={(pair.Value.ToLower() == "true" ? "1" : pair.Value.ToLower() == "false" ? "0" : pair.Value)}");
                }

                // Set PAD5 (5th controller) to true if base is Bomberman '93
                // ****************
                if (name.ToUpper().Contains("N43406YP"))
                    Settings["PAD5"] = "1";

                foreach (string line in ConfigFile.ToArray())
                    t.WriteLine(line);

                t.Flush();

                s.Seek(0, SeekOrigin.Begin);
                MainContent.ReplaceFile(ConfigIndex, s.ToArray());

                t.Dispose();
            }
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

        // -----------------------------------------------------
        // CONFIG from Bomberman'93:
        // -----------------------------------------------------
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

        // -----------------------------------------------------
        // CONFIG from Castlevania Rondo of Blood:
        // -----------------------------------------------------
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

        // -----------------------------------------------------
        // CONFIG from Street Fighter 'II: Champion Edition:
        // -----------------------------------------------------
        /* NAME=HE93002
           ROM=/LZ77HE93002.BIN
           BACKUPRAM=0
           MULTITAP=1
           HDS=0
           RASTER=0
           POPULUS=0
           SPRLINE=0
           PAD5=0
           NOFPA=1
           PADBUTTON=6 */
    }
}
