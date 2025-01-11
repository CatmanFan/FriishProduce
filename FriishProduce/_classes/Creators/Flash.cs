using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FriishProduce.Injectors
{
    public class Flash
    {
        public string SWF { get; set; }
        public IDictionary<string, string> Settings { get; set; }
        public IDictionary<Buttons, string> Keymap { get; set; }
        public bool Multifile { get; set; }

        #region -- Default settings for Back to Nature --
        // keymap.ini
        // -----------------------------------------------------
        /* KEY_BUTTON_LEFT  KEY_LEFT
           KEY_BUTTON_RIGHT KEY_RIGHT
           KEY_BUTTON_DOWN  KEY_DOWN
           KEY_BUTTON_UP    KEY_UP */

        // banner.ini:
        // -----------------------------------------------------
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

        // config.common.pcf
        // -----------------------------------------------------
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

        #endregion

        #region -- Default settings for BBC iPlayer --
        // keymap.ini
        // -----------------------------------------------------
        /* KEY_BUTTON_LEFT  HID_4
           KEY_BUTTON_RIGHT HID_6
           KEY_BUTTON_DOWN  HID_2
           KEY_BUTTON_UP    HID_8
           */

        // banner.ini (Europe)
        // -----------------------------------------------------
        /* # banner setting file
           # 
           # タイトル文字列とコメント文字列は UTF-8 エンコードされた文字列を URL エンコードして
           # 記述する。
           #
           not_copy		off
           anim_type		loop			# loop / bounce
           title_text		BBC%20iPlayer	# UTF-8/URL Encoded
           comment_text		   %20             # UTF-8/URL Encoded
           banner_tpl		banner/EU/banner.tpl
           icon_tpl		banner/EU/icons.tpl
           icon_count		1			# texture_index: 0 -> 0
           icon_speed		0, normal	# texture_index, (slow|normal|fast)

           */

        // config.common.pcf
        // -----------------------------------------------------
        /* # 行頭のコメントや改行は無視されます

           static_heap_size				8192				# 8192[KB] -> 8[MB]
           dynamic_heap_size				16384				# 16384[KB] -> 16[MB]

           mp4_stream_buffer_size			512					# 512[KB] -> 0.5[MB]
           #mp4_texture_buffer_count		32					# not currently implemented

           stream_cache_max_file_size		512					# 512[KB] -> 0.5[MB]
           stream_cache_size				2048				# 2048[KB] -> 2.0[MB]

           # コンテンツのフレームレートが遅すぎてデータのロードなどに時間が掛かったり
           # サウンド再生にノイズが乗るなどの場合に任意の更新レートを指定できます。
           update_frame_rate             25                  # not TV-framerate(NTSC/PAL)

           mouse							on
           qwerty_keyboard					on
           navigation_model				4way				# 2way / 4way / 4waywrap
           quality							high				# low / medium / high
           looping							on

           text_encoding					utf-16				# should be utf-16

           midi							off
           # dls_file						dls/GM16.DLS

           key_input						on

           cursor_archive					cursor.arc
           cursor_layout					cursor.brlyt

           dialog_cursor_archive			cursor.arc
           dialog_cursor_layout			cursor.brlyt

           shared_object_capability		on
           num_vff_drives					1
           vff_cache_size					96					# 96[KB]
           vff_sync_on_write               off

           persistent_storage_root_drive	X
           persistent_storage_vff_file		shrdobjs.vff		# 8.3 format
           persistent_storage_total		96					# 96[KB]
           persistent_storage_per_movie	64					# 64[KB]

           supported_devices				core

           hbm_no_save						true

           static_module					static.sel
           plugin_modules					plugin_wiinotification.rso plugin_wiiremote.rso plugin_wiisystem.rso

           trace_filter					none
           texture_filter					linear

           #can pass variables to flash
           #flash_vars					varName = value
           #flash_vars					CA_Domain = 208.186.152.236

           strap_reminder			none		#normal  #no_ex  #none

           ####### iPlayer ########

           content_domain					https://wii.nintendo.iplayer.bbc.co.uk/wiiiplayer/
           #content_domain					https://wii-test.nintendo.iplayer.bbc.co.uk/wii-test/tvp/
           #content_domain					https://wii.nintendo.iplayer.bbc.co.uk/testwiiiplayer/tvp/livetest/
           content_url						file:///trusted/startup.swf

           certificate_files				GTEGI.cer */
        #endregion

        #region -- Default settings for YouTube --
        // banner.ini (universal)
        // -----------------------------------------------------
        /* # generic banner setting file
           #
           not_copy		on
           anim_type		loop			# loop / bounce
           title_text		YouTube			# UTF-8/URL Encoded
           comment_text		   %20             # UTF-8/URL Encoded
           banner_tpl		banner/banner.tpl
           icon_tpl		banner/icons.tpl
           icon_count		1			# texture_index: 0 -> 0
           icon_speed		0, normal	# texture_index, (slow|normal|fast)

           */

        // common.pcf
        // -----------------------------------------------------
        /* ##################################################################################################
           ##### 				    GUMBALL VODF CONFIG FILE			             #####			
           ##################################################################################################

           static_heap_size				8192			# 8192[KB] -> 8[MB]
           dynamic_heap_size				16384			# 16384[KB] -> 16[MB]

           stream_cache_max_file_size			512			# 512[KB] -> 0.5[MB]
           stream_cache_size				2048			# 2048[KB] -> 2.0[MB]

           content_mem1					no
           content_buffer_mode				copy

           mouse						on
           qwerty_keyboard					on			# hardware keyboard
           qwerty_events					on			# hardware keyboard sends flash events
           use_keymap					off			# determines if the region's keymap.ini is used
           navigation_model				4way			# 2way / 4way / 4waywrap
           quality						high			# low / medium / high
           looping						on

           text_encoding					utf-16			# should be utf-16

           midi						off
           # dls_file					dls/GM16.DLS

           key_input					on			# software keyboard -- requires hardware keyboard and mouse

           cursor_archive					cursor.arc
           cursor_layout					cursor.brlyt

           dialog_cursor_archive				cursor.arc
           dialog_cursor_layout				cursor.brlyt


           banner_file					banner/banner.ini


           device_text					off
           brfna_file					10, wbf1.brfna
           brsar_file					sound/FlashPlayerSe.brsar	# sound data

           embedded_vector_font				off
           # embedded_vector_font_files			fonts/font1.swf fonts/font2.swf fonts/font3.swf
           # pre_installed_as_class_files			library/classes.swf



           shared_object_capability			on
           num_vff_drives					2
           vff_cache_size					96			# 96[KB]
           vff_sync_on_write              			off

           persistent_storage_root_drive			X
           persistent_storage_vff_file 			shrdobjs.vff		# 8.3 format
           persistent_storage_total			96			# 96[KB]
           persistent_storage_per_movie			64			# 64[KB]

           supported_devices				core freestyle classic

           hbm_no_save	    				true

           static_module					static.sel

           plugin_modules					plugin_wiinotification.rso 
           plugin_modules					plugin_wiiremote.rso 
           plugin_modules					plugin_wiisystem.rso 
           plugin_modules					plugin_wiisound.rso 
           plugin_modules					plugin_wiinetwork.rso
           #plugin_modules					plugin_wiiconnect24.rso
           #plugin_modules					plugin_wiiperformance.rso
           #plugin_modules					plugin_wiikeyboard.rso
           #plugin_modules					plugin_wiisugarcalculations.rso
           #plugin_modules					plugin_wiiuntrustedrequest.rso
           #plugin_modules					plugin_wiimiisupport.rso 

           trace_filter					none
           texture_filter					linear

           strap_reminder					none			#normal  #no_ex  #none

           # set to match the loading screen's background color
           background_color				43 43 43 255		# RGBA -- VODF/SWF BG Color.


           ################################# APPLICATION CONFIGURATIONS #####################################


           update_frame_rate				30 			# 0 sets it to framerate set in content


           ########################################## MediaStream ###############################################


           content_domain		file:///trusted/				#Local Data


           debug_content_url 	file:///trusted/wii_dev_shim.swf

           # Debug settings
           #  load from web-trunk-qa:
           debug_flash_vars	dev=1&app=file://trusted/remote/https://web-trunk-qa.youtube.com/wiitv
           #  load from web-release-qa:
           #debug_flash_vars	dev=1&app=file://trusted/remote/https://web-release-qa.youtube.com/wiitv
           #  load from horcrux (no-auto-build):
           #debug_flash_vars	dev=1&relax=1&app=file://trusted/remote/http://horcrux.sbo.corp.google.com/wii/leanbacklite_wii.swf&urlmap=s.ytimg.com/yts/swfbin/apiplayer%3Dhttp://horcrux.sbo.corp.google.com/wii/apiplayer.swf%3Bs.ytimg.com/yts/swfbin/vast_ads_module%3Dhttp://horcrux.sbo.corp.google.com/wii/vast_ads_module.swf
           #  load from prod:
           #debug_flash_vars	dev=1
           #  load from prod/Charles-ready:
           #debug_flash_vars	dev=1&relax=6


           final_content_url 	file:///trusted/wii_shim.swf

           # Final settings (can't be empty)
           #  load from prod:
           final_flash_vars	dummy=1


           */
        #endregion

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

        private enum Type
        {
            Invalid = -1,
            BackToNature = 0,
            iPlayer = 1,
            YouTube = 2
        }

        private U8 MainContent { get; set; }

        public WAD Inject(WAD w, string[] lines, ImageHelper Img)
        {
            MainContent = U8.Load(w.Contents[2]);
            MainContent.Extract(Paths.FlashContents);

            #region ---------------- Determining the Flash emulator type ----------------

            Type type = Type.Invalid;
            string target = null;

            if (File.Exists(Paths.FlashContents + "content\\menu.swf"))
            {
                target = Paths.FlashContents + "content\\menu.swf";
                type = Type.BackToNature;
            }

            else if (File.Exists(Paths.FlashContents + "trusted\\startup.swf"))
            {
                target = Paths.FlashContents + "trusted\\startup.swf";
                type = Type.iPlayer;
            }

            else if (File.Exists(Paths.FlashContents + "trusted\\wii_shim.swf"))
            {
                target = Paths.FlashContents + "trusted\\wii_shim.swf";
                type = Type.YouTube;
            }

            else
            {
                throw new Exception(Program.Lang.Msg(13, true));
            }

            #endregion

            #region ---------------- Actually replacing the SWF + copying other needed files ----------------

            Directory.Delete(Path.GetDirectoryName(target), true);
            Directory.CreateDirectory(Path.GetDirectoryName(target));

            File.Copy(SWF, target, true);

            if (Multifile)
            {
                foreach (string folder in Directory.EnumerateDirectories(Path.GetDirectoryName(SWF), "*.*", SearchOption.AllDirectories))
                {
                    string newFolder = folder.Replace(Path.GetDirectoryName(SWF), Path.GetDirectoryName(target));
                    Directory.CreateDirectory(newFolder);
                }

                foreach (string etc in Directory.EnumerateFiles(Path.GetDirectoryName(SWF), "*.*", SearchOption.AllDirectories))
                    if (etc != SWF)
                        File.Copy(etc, etc.Replace(Path.GetDirectoryName(SWF), Path.GetDirectoryName(target)), true);
            }

            #endregion

            #region ---------------- Copying the SWF soundfont ----------------

            if (File.Exists(Settings["midi"]))
            {
                if (!Directory.Exists(Paths.FlashContents + "dls\\"))
                    Directory.CreateDirectory(Paths.FlashContents + "dls\\");

                File.Copy(Settings["midi"], Paths.FlashContents + "dls\\GM16.DLS");
            }

            #endregion

            foreach (string file in Directory.EnumerateFiles(Paths.FlashContents, "*.*", SearchOption.AllDirectories))
            {
                string item = file.Replace(Paths.FlashContents, null).ToLower();

                #region ---------------- Adding keymap ----------------

                if (item.EndsWith("keymap.ini") && Keymap?.Count > 0)
                {
                    List<string> txt = new();

                    foreach (var mapping in Keymap)
                    {
                        string button =
                        (
                            mapping.Key == Buttons.WiiRemote_Left ? "KEY_BUTTON_LEFT" :
                            mapping.Key == Buttons.WiiRemote_Right ? "KEY_BUTTON_RIGHT" :
                            mapping.Key == Buttons.WiiRemote_Down ? "KEY_BUTTON_DOWN" :
                            mapping.Key == Buttons.WiiRemote_Up ? "KEY_BUTTON_UP" :
                            mapping.Key == Buttons.WiiRemote_A ? "KEY_BUTTON_A" :
                            mapping.Key == Buttons.WiiRemote_B ? "KEY_BUTTON_B" :
                            mapping.Key == Buttons.WiiRemote_Home ? "KEY_BUTTON_HOME" :
                            mapping.Key == Buttons.WiiRemote_Plus ? "KEY_BUTTON_PLUS" :
                            mapping.Key == Buttons.WiiRemote_Minus ? "KEY_BUTTON_MINUS" :
                            mapping.Key == Buttons.WiiRemote_1 ? "KEY_BUTTON_1" :
                            mapping.Key == Buttons.WiiRemote_2 ? "KEY_BUTTON_2" :
                            mapping.Key == Buttons.Nunchuck_Z ? "KEY_BUTTON_Z" :
                            mapping.Key == Buttons.Nunchuck_C ? "KEY_BUTTON_C" :

                            mapping.Key == Buttons.Classic_Up ? "KEY_CL_BUTTON_UP" :
                            mapping.Key == Buttons.Classic_Left ? "KEY_CL_BUTTON_LEFT" :
                            mapping.Key == Buttons.Classic_ZR ? "KEY_CL_TRIGGER_ZR" :
                            mapping.Key == Buttons.Classic_X ? "KEY_CL_BUTTON_X" :
                            mapping.Key == Buttons.Classic_A ? "KEY_CL_BUTTON_A" :
                            mapping.Key == Buttons.Classic_Y ? "KEY_CL_BUTTON_Y" :
                            mapping.Key == Buttons.Classic_B ? "KEY_CL_BUTTON_B" :
                            mapping.Key == Buttons.Classic_ZL ? "KEY_CL_TRIGGER_ZL" :
                            mapping.Key == Buttons.Classic_Reserved ? "KEY_CL_RESERVED" :
                            mapping.Key == Buttons.Classic_R ? "KEY_CL_TRIGGER_R" :
                            mapping.Key == Buttons.Classic_Plus ? "KEY_CL_BUTTON_PLUS" :
                            mapping.Key == Buttons.Classic_Home ? "KEY_CL_BUTTON_HOME" :
                            mapping.Key == Buttons.Classic_Minus ? "KEY_CL_BUTTON_MINUS" :
                            mapping.Key == Buttons.Classic_L ? "KEY_CL_TRIGGER_L" :
                            mapping.Key == Buttons.Classic_Down ? "KEY_CL_BUTTON_DOWN" :
                            mapping.Key == Buttons.Classic_Right ? "KEY_CL_BUTTON_RIGHT" :

                            null
                        );

                        if (button != null && !string.IsNullOrWhiteSpace(mapping.Value))
                            txt.Add(button.Length < 17 ? button.PadRight(17, ' ') + mapping.Value : button + ' ' + mapping.Value);
                    }

                    File.WriteAllBytes(file, Encoding.UTF8.GetBytes(string.Join("\r\n", txt) + "\r\n"));
                }

                #endregion

                #region ---------------- Adding common config ----------------

                else if (Path.GetFileName(item).Contains("common.pcf"))
                {
                    List<string> txt = new()
                    {
                        "# Comments (text preceded by #) and line breaks will be ignored",
                        "static_heap_size                8192                # 8192[KB] -> 8[MB]",
                        "dynamic_heap_size               16384               # 16384[KB] -> 16[MB]",

                        $"update_frame_rate             {Settings["update_frame_rate"]}                  # not TV-framerate(NTSC/PAL)",

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

                        $"shared_object_capability        {(type == Type.BackToNature ? Settings["shared_object_capability"] : "on")}",
                        "num_vff_drives                  1",
                        $"vff_cache_size                  {Settings["vff_cache_size"]}",
                        $"vff_sync_on_write               {Settings["vff_sync_on_write"]}",

                        "persistent_storage_root_drive   X",
                        "persistent_storage_vff_file     shrdobjs.vff        # 8.3 format",
                        $"persistent_storage_total        {Settings["persistent_storage_total"]}",
                        $"persistent_storage_per_movie    {Settings["persistent_storage_per_movie"]}",

                        $"strap_reminder                  {Settings["strap_reminder"]}",

                        "supported_devices               core, freestyle, classic",

                        $"hbm_no_save                     {Settings["hbm_no_save"]}",

                        type == Type.YouTube ? $"debug_content_url               file:///trusted/wii_shim.swf"
                                             : $"content_url                     {(type == Type.BackToNature ? "file:///content/menu.swf" : "file:///trusted/startup.swf")}",
                    };

                    if (type == Type.iPlayer)
                    {
                        txt.AddRange(new string[]
                            {
                                "##### iPlayer Settings #####",

                                "mp4_stream_buffer_size			512					# 512[KB] -> 0.5[MB]",
                                "stream_cache_max_file_size     512                 # 512[KB] -> 0.5[MB]",
                                "stream_cache_size              2048                # 2048[KB] -> 2.0[MB]",
                                "static_module					static.sel",
                                "plugin_modules					plugin_wiinotification.rso plugin_wiiremote.rso plugin_wiisystem.rso",
                                "trace_filter					none",
                                "texture_filter					linear",
                                "certificate_files				GTEGI.cer",
                                $"content_domain					{(!Settings.ContainsKey("content_domain") || string.IsNullOrWhiteSpace(Settings["content_domain"]) ? "file:///trusted/" : Settings["content_domain"])}",
                                "#flash_vars					dummy = 1",
                            }
                        );
                    }

                    else if (type == Type.YouTube)
                    {
                        txt.AddRange(new string[]
                            {
                                "##### YouTube Settings #####",

                                "stream_cache_max_file_size			512			# 512[KB] -> 0.5[MB]",
                                "stream_cache_size				2048			# 2048[KB] -> 2.0[MB]",
                                "content_mem1					no",
                                "content_buffer_mode				copy",
                                "qwerty_events					on",
                                "use_keymap					on",
                                "banner_file					banner/banner.ini",
                                "device_text					off",
                                "brfna_file					10, wbf1.brfna",
                                "brsar_file					sound/FlashPlayerSe.brsar	# sound data",
                                "embedded_vector_font				off",
                                "static_module					static.sel",
                                "plugin_modules					plugin_wiinotification.rso plugin_wiiremote.rso plugin_wiisystem.rso plugin_wiisound.rso plugin_wiinetwork.rso",
                                "trace_filter					none",
                                "texture_filter					linear",
                                $"background_color				{Settings["background_color"]}		# RGBA -- VODF/SWF BG Color.",

                                "##### MediaStream #####",

                                $"content_domain					{(!Settings.ContainsKey("content_domain") || string.IsNullOrWhiteSpace(Settings["content_domain"]) ? "file:///trusted/" : Settings["content_domain"])}",
                                "debug_flash_vars	dummy=1",
                                "final_flash_vars	dummy=1",
                                "final_content_url 	file:///trusted/wii_shim.swf",
                            }
                        );
                    }

                    File.WriteAllBytes(file, Encoding.GetEncoding(932).GetBytes(string.Join("\r\n", txt) + "\r\n"));
                }

                #endregion

                #region ---------------- Adding regional config ----------------

                else if (type != Type.YouTube && item.EndsWith(".pcf") && item.StartsWith("config\\") && Path.GetFileNameWithoutExtension(file).Length == 11)
                {
                    List<string> txt = new();

                    foreach (string line in File.ReadAllLines(file))
                    {
                        if (line.Contains("background_"))
                            txt.Add($"background_color                {Settings["background_color"]}             # RGBA");
                        else if (!line.Contains("content_url"))
                            txt.Add(line);
                    }

                    File.WriteAllBytes(file, Encoding.UTF8.GetBytes(string.Join("\r\n", txt) + "\r\n"));
                }

                #endregion
            }

            #region ---------------- Savebanner .TPL & config ----------------

            using TPL banner = Img.CreateSaveTPL(1);
            using TPL icons = Img.CreateSaveTPL(2);

            int region = 0;
            int textures = icons.NumOfTextures;

            for (int i = 0; i < lines.Length; i++)
            {
                var bytes = Encoding.Unicode.GetBytes(lines[i]);
                lines[i] = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, bytes));
            }

            foreach (string file in Directory.EnumerateFiles(Paths.FlashContents, "*.*", SearchOption.AllDirectories))
            {
                string item = file.Replace(Paths.FlashContents, null).ToLower();

                if (item.Contains("banner\\us")) region = 0;
                if (item.Contains("banner\\eu")) region = 1;
                if (item.Contains("banner\\jp")) region = 2;

                if (item.Contains("banner.tpl"))
                    banner.Save(file);

                else if (item.Contains("icons.tpl"))
                    icons.Save(file);

                else if (item.Contains("banner.ini"))
                {
                    var txt = new List<string>()
                        {
                            "not_copy        off",
                            "anim_type       bounce",
                            $"title_text      {Uri.EscapeUriString(lines[0])}",
                            $"comment_text    {(lines.Length > 1 && !string.IsNullOrEmpty(lines[1]) ? Uri.EscapeUriString(lines[1]) : "%20")}",
                            $"banner_tpl      banner{(type == Type.YouTube ? null : "/" + (region == 2 ? "JP" : region == 1 ? "EU" : "US"))}/banner.tpl",
                            $"icon_tpl        banner{(type == Type.YouTube ? null : "/" + (region == 2 ? "JP" : region == 1 ? "EU" : "US"))}/icons.tpl",
                            "icon_count      " + textures,
                        };

                    for (int i = 0; i < textures; i++)
                    {
                        txt.Add($"icon_speed      {i}, slow");
                    }

                    File.WriteAllBytes(file, Encoding.GetEncoding(932).GetBytes(string.Join("\r\n", txt) + "\r\n"));
                }
            }

            banner.Dispose();
            icons.Dispose();

            #endregion

            MainContent.CreateFromDirectory(Paths.FlashContents);
            if (Directory.Exists(Paths.FlashContents)) Directory.Delete(Paths.FlashContents, true);

            #region ---------------- Dispose of "Operations Guide" button on HOME Menu. ----------------

            U8 Content6 = U8.Load(w.Contents[6]);

            int start = -1;
            int end = -1;

            for (int i = 0; i < Content6.NumOfNodes; i++)
            {
                if (Content6.StringTable[i].ToLower() == "homebutton2") start = i;
                else if (Content6.StringTable[i].ToLower() == "homebutton3") end = i;
            }

            try
            {
                if (start <= 0 && end <= 0) throw new InvalidOperationException();
                else
                {
                    for (int i = 1; i < end - start; i++)
                        Content6.ReplaceFile(i + end, Content6.Data[i + start]);
                }
            }
            catch { }

            #endregion

            #region ---------------- Finally, replace the relevant files ----------------

            w.Unpack(Paths.WAD);
            File.WriteAllBytes(Paths.WAD + "00000002.app", MainContent.ToByteArray());
            File.WriteAllBytes(Paths.WAD + "00000006.app", Content6.ToByteArray());
            w.CreateNew(Paths.WAD);
            Directory.Delete(Paths.WAD, true);

            #endregion

            if (Content6 != null) Content6.Dispose();
            MainContent.Dispose();
            Settings = null;
            Keymap = null;
            return w;
        }
    }
}
