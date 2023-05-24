using System;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce.Injectors
{
    public class Flash
    {
        public string SWF { get; set; }

        internal string config = Paths.WorkingFolder_Content2 + "config\\config.common.pcf";

        public void ReplaceSWF() => File.Copy(SWF, Paths.WorkingFolder_Content2 + "content\\menu.swf", true);

        internal void SetController(Dictionary<string, string> btns)
        {
            List<string> keymap = new List<string>();
            foreach (KeyValuePair<string, string> item in btns)
            {
                if (item.Value == "KEY_SPACE")
                    keymap.Add($"{item.Key}   32");
                else if (item.Value.Length == 1)
                    keymap.Add($"{item.Key}   {Convert.ToInt32(item.Value[0])}");
                else
                    keymap.Add($"{item.Key}   {item.Value}");
            }
            File.WriteAllLines(Paths.WorkingFolder_Content2 + "config\\US\\keymap.ini", keymap.ToArray());
            File.WriteAllLines(Paths.WorkingFolder_Content2 + "config\\EU\\keymap.ini", keymap.ToArray());
            File.WriteAllLines(Paths.WorkingFolder_Content2 + "config\\JP\\keymap.ini", keymap.ToArray());
        }

        /// <summary>
        /// "Anything not saved will be lost" message toggle.
        /// The config file enables this if "hbm_no_save" is set to "no"
        /// </summary>
        public void HomeMenuNoSave(bool enabled)
        {
            string[] new_config = File.ReadAllLines(config);
            for (int i = 0; i < new_config.Length; i++)
                if (new_config[i].StartsWith("hbm_no_save")) new_config[i] = $"hbm_no_save                     {(enabled ? "no" : "yes")}";
            File.WriteAllLines(config, new_config);
        }

        public void SetStrapReminder(int screen)
        {
            string type = screen == 2 ? "normal" : screen == 1 ? "no_ex" : "none";

            string[] new_config = File.ReadAllLines(config);
            for (int i = 0; i < new_config.Length; i++)
                if (new_config[i].StartsWith("strap_reminder")) new_config[i] = $"strap_reminder                  {type}";
            File.WriteAllLines(config, new_config);
        }

        public void SDAspectRatio()
        {
            var configs = Directory.GetFiles(Paths.WorkingFolder_Content2 + "config\\", "*.pcf", SearchOption.AllDirectories);
            for (int i = 0; i < configs.Length; i++)
                if (configs[i].EndsWith(".wide.pcf"))
                    File.Copy(configs[i - 1], configs[i], true);
        }

        internal void SetFPS(string FPS)
        {
            string[] new_config = File.ReadAllLines(config);
            for (int i = 0; i < new_config.Length; i++)
                if (new_config[i].Contains("update_frame_rate")) new_config[i] = $"update_frame_rate             {FPS}                    # not TV-framerate(NTSC/PAL)";
            File.WriteAllLines(config, new_config);
        }

        public void EnableSaveData(int total_storage)
        {
            string[] new_config = File.ReadAllLines(config);
            for (int i = 0; i < new_config.Length; i++)
            {
                if (new_config[i].StartsWith("shared_object_capability")) new_config[i] = "shared_object_capability        on";
                if (new_config[i].StartsWith("persistent_storage_total")) new_config[i] = $"persistent_storage_total        {total_storage}";
                if (new_config[i].StartsWith("persistent_storage_per_movie")) new_config[i] = $"persistent_storage_per_movie    {total_storage / 2}";
                // if (new_config[i].StartsWith("vff_cache_size"))               new_config[i] = $"vff_cache_size                  {total_storage}";
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
                    // Probably sets the animation to play in reverse and back as in other VC titles (no)
                    if (banner[i].StartsWith("anim_type")) banner[i] = "anim_type       bounce            # loop / bounce";

                    if (banner[i].StartsWith("title_text")) banner[i] = $"title_text      {Uri.EscapeUriString(lines[0])} # UTF-8/URL Encoded";
                    if (banner[i].StartsWith("comment_text")) banner[i] = $"comment_text    {(lines.Length == 2 ? Uri.EscapeUriString(lines[1]) : "%20")} # UTF-8/URL Encoded";
                    if (banner[i].StartsWith("icon_count")) icons_offset = i;
                }

                System.Collections.Generic.List<string> new_banner = new System.Collections.Generic.List<string>();
                for (int i = 0; i < icons_offset; i++)
                    new_banner.Add(banner[i]);
                new_banner.Add("icon_count      8           # texture_index: 0 -> 3");
                new_banner.Add("icon_speed      0, slow      # texture_index, (slow|normal|fast)");
                new_banner.Add("icon_speed      1, slow");
                new_banner.Add("icon_speed      2, slow");
                new_banner.Add("icon_speed      3, slow");
                new_banner.Add("icon_speed      4, slow");
                new_banner.Add("icon_speed      5, slow");
                new_banner.Add("icon_speed      6, slow");
                new_banner.Add("icon_speed      7, slow");
                new_banner.Add(String.Empty);

                File.WriteAllLines(configIni, new_banner.ToArray());
            }
        }
    }
}
