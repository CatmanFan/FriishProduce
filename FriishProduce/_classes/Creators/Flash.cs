using libWiiSharp;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FriishProduce.Injectors
{
    public static class Flash
    {
        public static IDictionary<string, string> Settings { get; set; }
        public static IDictionary<Buttons, string> Keymap { get; set; }
        public static bool Multifile { get; set; }

        // DEFAULT CONFIG FOR REFERENCE:
        /* # Comments (text preceded by #) and line breaks will be ignored

            static_heap_size                8192                # 8192[KB] -> 8[MB]
            dynamic_heap_size               16384               # 16384[KB] -> 16[MB]

            # You can specify a custom update rate if operations such as loading data are taking too long due to the content's
            # frame rate being too low. You may also want to customize this value if noise is occurring during sound playback.
            # update_frame_rate             30                  # not TV-framerate(NTSC/PAL)

            mouse                           on                  # on / off, enables mouse using Wii Remote Cursor
            qwerty_keyboard                 on                  # on / off, enables keyboard
            navigation_model                4way                # 2way / 4way / 4waywrap
            quality                         high              # low / medium / high
            looping                         on

            text_encoding                   utf-16              # should be utf-16

            midi                            off
            # dls_file                      dls/GM16.DLS

            key_input                       on

            cursor_archive                  cursor.arc
            cursor_layout                   cursor.brlyt

            dialog_cursor_archive           cursor.arc
            dialog_cursor_layout            cursor.brlyt

            shared_object_capability        off                 # off / on
            num_vff_drives                  1
            vff_cache_size                  96                  # 96[KB]
            vff_sync_on_write               off

            persistent_storage_root_drive   X
            persistent_storage_vff_file     shrdobjs.vff        # 8.3 format
            persistent_storage_total        96                  # 96[KB]
            persistent_storage_per_movie    64                  # 64[KB]

            strap_reminder                  none                # none / normal / no_ex

            supported_devices               core, freestyle, classic

            hbm_no_save                     no                  # no (enables "unsaved" message on HOME Menu) / yes
          */

        /* 

 */

        // DEFAULT KEYMAP.INI:
        /* KEY_BUTTON_LEFT  KEY_LEFT
           KEY_BUTTON_RIGHT KEY_RIGHT
           KEY_BUTTON_DOWN  KEY_DOWN
           KEY_BUTTON_UP    KEY_UP */

        /*  Wii buttons:
            KEY_BUTTON_LEFT
            KEY_BUTTON_RIGHT
            KEY_BUTTON_DOWN
            KEY_BUTTON_UP
            KEY_BUTTON_A
            KEY_BUTTON_B
            KEY_BUTTON_HOME
            KEY_BUTTON_PLUS
            KEY_BUTTON_MINUS
            KEY_BUTTON_1
            KEY_BUTTON_2
            KEY_BUTTON_Z
            KEY_BUTTON_C
            KEY_CL_BUTTON_UP
            KEY_CL_BUTTON_LEFT
            KEY_CL_TRIGGER_ZR
            KEY_CL_BUTTON_X
            KEY_CL_BUTTON_A
            KEY_CL_BUTTON_Y
            KEY_CL_BUTTON_B
            KEY_CL_TRIGGER_ZL
            KEY_CL_RESERVED
            KEY_CL_TRIGGER_R
            KEY_CL_BUTTON_PLUS
            KEY_CL_BUTTON_HOME
            KEY_CL_BUTTON_MINUS
            KEY_CL_TRIGGER_L
            KEY_CL_BUTTON_DOWN
            KEY_CL_BUTTON_RIGHT

            Keyboards:
            e.g. 83 = S letter key
            KEY_LEFT
            KEY_RIGHT
            KEY_HOME
            KEY_END
            KEY_INSERT
            KEY_DELETE
            KEY_BACKSPACE
            KEY_SELECT
            KEY_UP
            KEY_DOWN
            KEY_PAGEUP
            KEY_PAGEDOWN
            KEY_FORWARD
            KEY_BACKWARD
            KEY_ESCAPE
            KEY_ENTER
            KEY_TAB
            KEY_CAPS
            KEY_SHIFT
            KEY_CTRL

            Once done, the config should look something like this format:
            KEY_BUTTON_LEFT  KEY_DOWN
            KEY_BUTTON_RIGHT KEY_UP
            KEY_BUTTON_DOWN  KEY_RIGHT
            KEY_BUTTON_UP    KEY_LEFT
            KEY_BUTTON_A     88
            KEY_BUTTON_PLUS  KEY_ENTER
 */

        // DEFAULT BANNER.INI:
        /* # banner setting file
           # 
           # ƒ^ƒCƒgƒ‹•¶Žš—ñ‚ÆƒRƒƒ“ƒg•¶Žš—ñ‚Í UTF-8 ƒGƒ“ƒR[ƒh‚³‚ê‚½•¶Žš—ñ‚ð URL ƒGƒ“ƒR[ƒh‚µ‚Ä
           # ‹Lq‚·‚éB
           #
           not_copy        off
           anim_type       loop            # loop / bounce
           title_text      Game%20Title    # UTF-8/URL Encoded
           comment_text    Game%20Comment  # UTF-8/URL Encoded
           banner_tpl      banner/US/banner.tpl
           icon_tpl        banner/US/icons.tpl
           icon_count      4           # texture_index: 0 -> 3
           icon_speed      0, normal   # texture_index, (slow|normal|fast)
           icon_speed      1, slow
           icon_speed      2, fast
           icon_speed      3, normal */

        // PREFERRED CONFIG FOR SAVEDATA:
        /* 
           not_copy        off
           anim_type       bounce
           title_text      {Uri.EscapeUriString(lines[0])}
           comment_text    {(lines.Length == 2 ? Uri.EscapeUriString(lines[1]) : "%20")}
           banner_tpl      banner/US/banner.tpl
           icon_tpl        banner/US/icons.tpl
           icon_count      8
           icon_speed      0, slow
           icon_speed      1, slow
           icon_speed      2, slow
           icon_speed      3, slow
           icon_speed      4, slow
           icon_speed      5, slow
           icon_speed      6, slow
           icon_speed      7, slow */

        private static U8 MainContent { get; set; }

        public static WAD Inject(WAD w, string path, string[] lines, ImageHelper Img)
        {
            MainContent = U8.Load(w.Contents[2]);

            // MainContent.Extract(Paths.FlashContents);

            foreach (var item in MainContent.StringTable)
            {
                // Actually replacing the SWF
                // ********
                if (item.ToLower().Contains("menu.swf"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), File.ReadAllBytes(path));

                // FOR FORCING 4:3
                // ********
                else if (item.ToLower().Contains(".wide.pcf"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item.Replace(".wide", "")), MainContent.Data[MainContent.GetNodeIndex(item)]);

                else if (item.ToLower() == "keymap.ini" && Keymap?.Count > 0)
                {
                    var file = new List<string>()
                    /* {
                        "KEY_BUTTON_LEFT  KEY_LEFT",
                        "KEY_BUTTON_RIGHT KEY_RIGHT",
                        "KEY_BUTTON_DOWN  KEY_DOWN",
                        "KEY_BUTTON_UP    KEY_UP",
                        "KEY_BUTTON_A     65",
                        "KEY_BUTTON_B     66",
                        "KEY_BUTTON_1     49",
                        "KEY_BUTTON_2     50",
                        "KEY_BUTTON_HOME  KEY_ESCAPE",
                        ""
                    } */;

                    foreach (var mapping in Keymap)
                    {
                        string button = mapping.Key switch
                        {
                            Buttons.WiiRemote_Left => "KEY_BUTTON_LEFT",
                            Buttons.WiiRemote_Right => "KEY_BUTTON_RIGHT",
                            Buttons.WiiRemote_Down => "KEY_BUTTON_DOWN",
                            Buttons.WiiRemote_Up => "KEY_BUTTON_UP",
                            Buttons.WiiRemote_A => "KEY_BUTTON_A",
                            Buttons.WiiRemote_B => "KEY_BUTTON_B",
                            Buttons.WiiRemote_Home => "KEY_BUTTON_HOME",
                            Buttons.WiiRemote_Plus => "KEY_BUTTON_PLUS",
                            Buttons.WiiRemote_Minus => "KEY_BUTTON_MINUS",
                            Buttons.WiiRemote_1 => "KEY_BUTTON_1",
                            Buttons.WiiRemote_2 => "KEY_BUTTON_2",
                            Buttons.Nunchuck_Z => "KEY_BUTTON_Z",
                            Buttons.Nunchuck_C => "KEY_BUTTON_C",

                            Buttons.Classic_Up => "KEY_CL_BUTTON_UP",
                            Buttons.Classic_Left => "KEY_CL_BUTTON_LEFT",
                            Buttons.Classic_ZR => "KEY_CL_TRIGGER_ZR",
                            Buttons.Classic_X => "KEY_CL_BUTTON_X",
                            Buttons.Classic_A => "KEY_CL_BUTTON_A",
                            Buttons.Classic_Y => "KEY_CL_BUTTON_Y",
                            Buttons.Classic_B => "KEY_CL_BUTTON_B",
                            Buttons.Classic_ZL => "KEY_CL_TRIGGER_ZL",
                            Buttons.Classic_Reserved => "KEY_CL_RESERVED",
                            Buttons.Classic_R => "KEY_CL_TRIGGER_R",
                            Buttons.Classic_Plus => "KEY_CL_BUTTON_PLUS",
                            Buttons.Classic_Home => "KEY_CL_BUTTON_HOME",
                            Buttons.Classic_Minus => "KEY_CL_BUTTON_MINUS",
                            Buttons.Classic_L => "KEY_CL_TRIGGER_L",
                            Buttons.Classic_Down => "KEY_CL_BUTTON_DOWN",
                            Buttons.Classic_Right => "KEY_CL_BUTTON_RIGHT",

                            _ => null
                        };

                        if (button != null && !string.IsNullOrWhiteSpace(mapping.Value)) file.Add(button.Length < 17 ? button.PadRight(17, ' ') + mapping.Value : button + ' ' + mapping.Value);
                    }

                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), Encoding.UTF8.GetBytes(string.Join("\r\n", file) + "\r\n"));
                }

                else if (item.ToLower() == "config.common.pcf")
                {
                    var file = new List<string>()
                        {
                            "# Comments (text preceded by #) and line breaks will be ignored",
                            "static_heap_size                8192                # 8192[KB] -> 8[MB]",
                            "dynamic_heap_size               16384               # 16384[KB] -> 16[MB]",

                            "# update_frame_rate             30                  # not TV-framerate(NTSC/PAL)",

                            $"mouse                           {Settings["mouse"]}",
                            $"qwerty_keyboard                 {Settings["qwerty_keyboard"]}",
                            "navigation_model                4way                # 2way / 4way / 4waywrap",
                            $"quality                         {Settings["quality"]}",
                            "looping                         on",

                            "text_encoding                   utf-16",

                            $"midi                            {(File.Exists(Settings["midi"]) ? "on" : "off")}",
                            $"{(File.Exists(Settings["midi"]) ? null : "# ")}dls_file                      dls/GM16.DLS",

                            "key_input                       on",

                            "cursor_archive                  cursor.arc",
                            "cursor_layout                   cursor.brlyt",
                            "dialog_cursor_archive           cursor.arc",
                            "dialog_cursor_layout            cursor.brlyt",

                            $"shared_object_capability        {Settings["shared_object_capability"]}",
                            "num_vff_drives                  1",
                            $"vff_cache_size                  {Settings["vff_cache_size"]}",
                            $"vff_sync_on_write               {Settings["vff_sync_on_write"]}",

                            "persistent_storage_root_drive   X",
                            "persistent_storage_vff_file     shrdobjs.vff        # 8.3 format",
                            $"persistent_storage_total        {Settings["vff_cache_size"]}",
                            "persistent_storage_per_movie    64                  # 64[KB]",

                            $"strap_reminder                  {Settings["strap_reminder"]}",

                            "supported_devices               core, freestyle, classic",

                            $"hbm_no_save                     {Settings["hbm_no_save"]}"
                        };

                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), Encoding.UTF8.GetBytes(string.Join("\r\n", file) + "\r\n"));
                }
            }

            // Taking the SWF soundfont
            // ********
            if (File.Exists(Settings["midi"]))
            {
                MainContent.AddDirectory("/dls");
                MainContent.AddFile("/dls/GM16.DLS", File.ReadAllBytes(Settings["midi"]));
            }

            // Taking all other files
            // ********
            if (Multifile)
                foreach (string file in Directory.EnumerateFiles(Path.GetDirectoryName(path), "*.*", SearchOption.TopDirectoryOnly))
                    MainContent.AddFile("/content/" + Path.GetFileName(file), File.ReadAllBytes(file));

            // Savebanner .TPL & config
            // ********
            if (Settings["shared_object_capability"] == "on")
            {
                var banner = Img.CreateSaveTPL(1);
                var icons = Img.CreateSaveTPL(2);

                for (int i = 0; i < lines.Length; i++)
                {
                    var bytes = Encoding.Unicode.GetBytes(lines[i]);
                    lines[i] = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, bytes));
                }

                foreach (var item in MainContent.StringTable)
                {
                    if (item.ToLower() == "banner.tpl")
                        MainContent.ReplaceFile(MainContent.GetNodeIndex(item), banner.ToByteArray());

                    else if (item.ToLower() == "icons.tpl")
                        MainContent.ReplaceFile(MainContent.GetNodeIndex(item), icons.ToByteArray());

                    else if (item.ToLower() == "banner.ini")
                    {
                        var file = new List<string>()
                            {
                                "not_copy        off",
                                "anim_type       bounce",
                                $"title_text      {System.Uri.EscapeUriString(lines[0])}",
                                $"comment_text    {(lines.Length > 1 ? System.Uri.EscapeUriString(lines[1]) : "%20")}",
                                "banner_tpl      banner/US/banner.tpl",
                                "icon_tpl        banner/US/icons.tpl",
                                "icon_count      " + icons.NumOfTextures,
                            };

                        for (int i = 0; i < icons.NumOfTextures; i++)
                        {
                            file.Add($"icon_speed      {i}, slow");
                        }

                        MainContent.ReplaceFile(MainContent.GetNodeIndex(item), Encoding.UTF8.GetBytes(string.Join("\r\n", file) + "\r\n"));
                    }
                }
            }

            // MainContent.CreateFromDirectory(Paths.FlashContents);

            w.Unpack(Paths.WAD);
            File.WriteAllBytes(Paths.WAD + "00000002.app", MainContent.ToByteArray());
            w.CreateNew(Paths.WAD);
            Directory.Delete(Paths.WAD, true);
            return w;
        }
    }
}
