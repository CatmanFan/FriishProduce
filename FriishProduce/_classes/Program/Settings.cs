using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace FriishProduce
{
    [Serializable]
    public class Settings
    {
        public App application { get; set; }
        public Paths paths { get; set; }
        public NES nes { get; set; }
        public SNES snes { get; set; }
        public N64 n64 { get; set; }
        public SEGA sega { get; set; }
        public NEO neo { get; set; }
        public PCE pce { get; set; }
        public Forwarder forwarder { get; set; }
        public Flash flash { get; set; }

        public void Save()
        {
            var outFile = JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                WriteIndented = true,
                // IndentCharacter = Convert.ToChar(9),
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }).Replace("\n\n", "\n").Replace("\t\t", "\t");

            File.WriteAllText(FriishProduce.Paths.Config, outFile);
        }

        public void Reset(bool save)
        {
            application = new();
            paths = new();
            nes = new();
            snes = new();
            n64 = new();
            sega = new();
            pce = new();
            neo = new();
            forwarder = new();
            flash = new();

            if (save) Save();
        }

        private Settings Parse(byte[] file = null)
        {
            if (file == null) file = File.ReadAllBytes(FriishProduce.Paths.Config);

            Settings reader = null;
            var encoding = Encoding.UTF8;

            using (MemoryStream ms = new(file))
            using (StreamReader sr = new(ms, encoding))
            {
                JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip });
                encoding = sr.CurrentEncoding;
            }

            using (MemoryStream ms = new(file))
            using (StreamReader sr = new(ms, encoding))
            using (var fileReader = JsonDocument.Parse(sr.ReadToEnd(), new JsonDocumentOptions() { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip }))
            {
                reader = JsonSerializer.Deserialize<Settings>(fileReader, new JsonSerializerOptions() { AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip, IncludeFields = true });

                sr.Dispose();
                ms.Dispose();
            }

            string lang = reader.application.language;
            if (reader.paths == null) throw new InvalidDataException();
            if (reader.nes == null) throw new InvalidDataException();
            if (reader.snes == null) throw new InvalidDataException();
            if (reader.n64 == null) throw new InvalidDataException();
            if (reader.sega == null) throw new InvalidDataException();
            if (reader.pce == null) throw new InvalidDataException();
            if (reader.neo == null) throw new InvalidDataException();
            if (reader.forwarder == null) throw new InvalidDataException();
            if (reader.flash == null) throw new InvalidDataException();

            return reader;
        }

        public Settings()
        {
            Reset(false);
        }

        public Settings(string file)
        {
            Start:
            if (!File.Exists(file))
            {
                Reset(true);
            }

            else
            {
                try
                {
                    Settings reader = Parse(File.ReadAllBytes(file));

                    application = reader.application;
                    paths = reader.paths;
                    nes = reader.nes;
                    snes = reader.snes;
                    n64 = reader.n64;
                    sega = reader.sega;
                    pce = reader.pce;
                    neo = reader.neo;
                    forwarder = reader.forwarder;
                    flash = reader.flash;
                }

                catch
                {
                    file = null;
                    goto Start;
                }
            }
        }

        public class App
        {
            public string language { get; set; } = "sys";
#if DEBUG
            public bool debug_mode { get; set; } = true;
            public bool force_update { get; set; } = false;
#else
            public bool debug_mode { get; set; } = false;
            public bool force_update { get; set; } = false;
#endif
            public bool auto_update { get; set; } = false;
            public bool donotshow_000 { get; set; } = false;
            public bool donotshow_001 { get; set; } = false;
            public bool auto_prefill { get; set; } = false;
            public bool auto_fill_save_data { get; set; } = true;
            public bool use_online_wad_enabled { get; set; } = false;
            public bool bypass_rom_size { get; set; } = false;
            public int image_interpolation { get; set; } = 2;
            public bool image_fit_aspect_ratio { get; set; } = false;
            public string default_target_filename { get; set; } = "FULLNAME";
            public string default_export_filename { get; set; } = "[PLATFORM] FULLNAME (REGION) (TITLEID)";
            public int default_banner_region { get; set; } = 0;
            public int default_wiiu_display { get; set; } = 0;
            public int default_injection_method_nes { get; set; } = 0;
            public int default_injection_method_snes { get; set; } = 0;
            public int default_injection_method_n64 { get; set; } = 0;
            public int default_injection_method_sega { get; set; } = 0;
        }

        public class Paths
        {
            public string database { get; set; }
            public string bios_neo { get; set; }
            public string bios_psx { get; set; }
            public string bios_gb { get; set; }
            public string bios_gbc { get; set; }
            public string bios_gba { get; set; }

            public string recent_00 { get; set; }
            public string recent_01 { get; set; }
            public string recent_02 { get; set; }
            public string recent_03 { get; set; }
            public string recent_04 { get; set; }
            public string recent_05 { get; set; }
            public string recent_06 { get; set; }
            public string recent_07 { get; set; }
            public string recent_08 { get; set; }
            public string recent_09 { get; set; }
        }

        public class NES
        {
            public int palette { get; set; } = 0;
            public bool palette_banner_usage { get; set; } = false;
        }

        public class SNES
        {
            public bool patch_volume { get; set; } = false;
            public bool patch_nodark { get; set; } = false;
            public bool patch_nocc { get; set; } = false;
            public bool patch_nosuspend { get; set; } = false;
            public bool patch_nosave { get; set; } = false;
            public bool patch_widescreen { get; set; } = false;
            public bool patch_nocheck { get; set; } = false;
            public bool patch_wiimote { get; set; } = false;
            public bool patch_gcremap { get; set; } = false;
        }

        public class N64
        {
            public int romc_type { get; set; } = 1;
            public bool patch_nodark { get; set; } = false;
            public bool patch_crashfix { get; set; } = false;
            public bool patch_expandedram { get; set; } = false;
            public bool patch_autoromsize { get; set; } = false;
            public bool patch_cleantextures { get; set; } = false;
            // public bool patch_widescreen { get; set; } = false;
        }

        public class SEGA
        {
            public string console_brightness { get; set; } = "100";
            public string console_disableresetbutton { get; set; } = null;
            public string country { get; set; } = "jp";
            public string dev_mdpad_enable_6b { get; set; } = "1";
            public string save_sram { get; set; } = "1";
            public string nplayers { get; set; } = null;
            public string machine_md_use_4ptap { get; set; } = null;
        }

        public class PCE
        {
            public string BACKUPRAM { get; set; } = "1";
            public string PADBUTTON { get; set; } = "0";
            public string EUROPE { get; set; } = "0";
            public string SGENABLE { get; set; } = "0";
            public string HIDEOVERSCAN { get; set; } = "0";
            public string YOFFSET { get; set; } = "0";
            public string RASTER { get; set; } = "0";
            public string SPRLINE { get; set; } = "0";
        }

        public class NEO
        {
            public string bios { get; set; } = "VC2";
        }

        public class Forwarder
        {
            public int root_storage_device { get; set; } = 0;
            public bool show_bios_screen { get; set; } = false;
        }

        public class Flash
        {
            public string content_domain { get; set; } = "";
            public string quality { get; set; } = "high";
            public string mouse { get; set; } = "on";
            public string qwerty_keyboard { get; set; } = "on";
            public string shared_object_capability { get; set; } = "on";
            public string vff_sync_on_write { get; set; } = "off";
            public string vff_cache_size { get; set; } = "96";
            public string persistent_storage_total { get; set; } = "96";
            public string persistent_storage_per_movie { get; set; } = "64";
            public string update_frame_rate { get; set; } = "0";
            public string fullscreen { get; set; } = "false";
            public string hbm_no_save { get; set; } = "true";
            public string strap_reminder { get; set; } = "none";
        }
    }
}
