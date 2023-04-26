using System;
using System.IO;

namespace FriishProduce.Injectors
{
    public class Flash
    {
        public string SWF { get; set; }
        public bool saveData_enabled { get; set; }

        internal string config = Paths.WorkingFolder_Content2 + "config\\config.common.pcf";

        public void ReplaceSWF() => File.Copy(SWF, Paths.WorkingFolder_Content2 + "content\\menu.swf", true);

        /// <summary>
        /// "Anything not saved will be lost" message toggle.
        /// The config file enables this if "hbm_no_save" is set to no
        /// </summary>
        public void HomeMenuNoSave(bool enabled)
        {
            string[] new_config = File.ReadAllLines(config);
            for (int i = 0; i < new_config.Length; i++)
                if (new_config[i].StartsWith("hbm_no_save")) new_config[i] = $"hbm_no_save                     {(enabled ? "no" : "yes")}";
            File.WriteAllLines(config, new_config);
        }

        public void SDAspectRatio()
        {
            var configs = Directory.GetFiles(Paths.WorkingFolder_Content2 + "config\\", "*.pcf", SearchOption.AllDirectories);
            for (int i = 0; i < configs.Length; i++)
                if (configs[i].EndsWith(".wide.pcf"))
                    File.Copy(configs[i - 1], configs[i], true);
        }

        public void EnableSaveData(int total_storage)
        {
            string[] new_config = File.ReadAllLines(config);
            for (int i = 0; i < new_config.Length; i++)
            {
                if (new_config[i].StartsWith("shared_object_capability"))     new_config[i] = "shared_object_capability        on";
                if (new_config[i].StartsWith("persistent_storage_total"))     new_config[i] = $"persistent_storage_total        {total_storage}";
                if (new_config[i].StartsWith("persistent_storage_per_movie")) new_config[i] = $"persistent_storage_per_movie    {total_storage / 2}";
            }
            File.WriteAllLines(config, new_config);
        }

        public void InsertSaveData(string[] lines)
        {
            foreach (string configIni in Directory.GetFiles(Paths.WorkingFolder_Content2 + "banner\\", "*.ini", SearchOption.AllDirectories))
            {
                string[] banner = File.ReadAllLines(configIni);
                int icons_offset = 0;
                for (int i = 0; i < banner.Length; i++)
                {
                    if (banner[i].StartsWith("title_text"))   banner[i] = $"title_text      {Uri.EscapeUriString(lines[0])} # UTF-8/URL Encoded";
                    if (banner[i].StartsWith("comment_text")) banner[i] = $"comment_text    {(lines.Length == 2 ? Uri.EscapeUriString(lines[1]) : "%20")} # UTF-8/URL Encoded";
                }
                File.WriteAllLines(configIni, banner);
            }
            // TO-DO:
            /* Create banner.tpl with single texture, and icons.tpl with 4 textures
             * Insert TPLs in 00000002.app/banner/[XX]
             * Replace 00000002.app/config/config.common.pcf:
             *    shared_object_capability = "on"
             *    vff_cache_size = 2048
             *    persistent_storage_total = 2048
             *    persistent_storage_per_movie = 1024
             */
        }

    }
}
