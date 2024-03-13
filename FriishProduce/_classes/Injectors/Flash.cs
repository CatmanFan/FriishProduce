using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using libWiiSharp;

namespace FriishProduce.Injectors
{
    public static class Flash
    {
        internal static string config = "config.common.pcf";
        public static IDictionary<string, string> Settings { get; set; }

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
                /* if (item.ToLower().Contains(".wide.pcf"))
                    MainContent.ReplaceFile(MainContent.GetNodeIndex(item), MainContent.Data[MainContent.GetNodeIndex(item.Replace(".wide", ""))]); */
            }

            // Savebanner .TPL & config
            // ********
            /* if (Settings["shared_object_capability"] == "on")
            {
                byte[] banner = Img.CreateSaveTPL(1).ToByteArray();
                byte[] icons = Img.CreateSaveTPL(2).ToByteArray();

                foreach (var item in MainContent.StringTable)
                {
                    if (item.ToLower().Contains("banner.tpl"))
                        MainContent.ReplaceFile(MainContent.GetNodeIndex(item), banner);

                    else if (item.ToLower().Contains("icons.tpl"))
                        MainContent.ReplaceFile(MainContent.GetNodeIndex(item), icons);

                    else if (item.ToLower().Contains("banner.ini"))
                        MainContent.ReplaceFile(MainContent.GetNodeIndex(item), icons);
                }
            } */

            // MainContent.CreateFromDirectory(Paths.FlashContents);

            w.Unpack(Paths.WAD);
            File.WriteAllBytes(Paths.WAD + "00000002.app", MainContent.ToByteArray());
            w.CreateNew(Paths.WAD);
            Directory.Delete(Paths.WAD, true);
            return w;
        }
    }
}
