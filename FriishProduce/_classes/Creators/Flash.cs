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

        private static U8 MainContent { get; set; }

        public static WAD Inject(WAD w, string path, string[] lines, ImageHelper Img)
        {
            MainContent = U8.Load(w.Contents[2]);
            MainContent.Extract(Paths.FlashContents);

            if (w.UpperTitleID.StartsWith("HCX"))
                inject_YouTube(w, path, lines, Img);
            else
                inject_FlashPlaceholder(w, path, lines, Img);

            MainContent.CreateFromDirectory(Paths.FlashContents);
            if (Directory.Exists(Paths.FlashContents)) Directory.Delete(Paths.FlashContents, true);

            w.Unpack(Paths.WAD);
            File.WriteAllBytes(Paths.WAD + "00000002.app", MainContent.ToByteArray());
            w.CreateNew(Paths.WAD);
            Directory.Delete(Paths.WAD, true);
            return w;
        }

        private static string[] generateKeymap()
        {
            List<string> txt = new();
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

                if (button != null && !string.IsNullOrWhiteSpace(mapping.Value))
                    txt.Add(button.Length < 17 ? button.PadRight(17, ' ') + mapping.Value : button + ' ' + mapping.Value);
            }

            return txt.ToArray();
        }

        private static void inject_YouTube(WAD w, string path, string[] lines, ImageHelper Img)
        {
            // Actually replacing the SWF
            // ********
            Directory.Delete(Paths.FlashContents + "trusted\\", true);
            Directory.CreateDirectory(Paths.FlashContents + "trusted\\");
            File.Copy(path, Paths.FlashContents + "trusted\\" + Path.GetFileName(path), true);

            // Taking all other files
            // ********
            if (Multifile)
                foreach (string etc in Directory.EnumerateFiles(Path.GetDirectoryName(path), "*.*", SearchOption.TopDirectoryOnly))
                    if (etc != path)
                        File.Copy(etc, Paths.FlashContents + "trusted\\" + Path.GetFileName(etc), true);

            // FOR FORCING 4:3
            // ********
            foreach (string file in Directory.EnumerateFiles(Paths.FlashContents + "config\\tv\\", "*.*", SearchOption.AllDirectories))
                if (Path.GetFileName(file).ToLower().Contains(".wide.pcf") && Settings["stretch_to_4_3"] == "yes")
                    File.Copy(file.Replace(".wide.pcf", ".pcf"), file, true);

            // Taking the SWF soundfont
            // ********
            if (File.Exists(Settings["midi"]))
            {
                if (!Directory.Exists(Paths.FlashContents + "dls\\"))
                    Directory.CreateDirectory(Paths.FlashContents + "dls\\");

                File.Copy(Settings["midi"], Paths.FlashContents + "dls\\GM16.DLS");
            }

            // Keymap
            // ********
            if (Keymap != null)
                File.WriteAllBytes(Paths.FlashContents + "config\\keymap.ini", Encoding.UTF8.GetBytes(string.Join("\n", generateKeymap()) + "\n"));

            // General config
            // ********
            {
                var t = new List<string>();
                string[] orig = File.ReadAllLines(Paths.FlashContents + "config\\common.pcf");

                for (int i = 0; i < orig.Length; i++)
                {
                    foreach (var pair in Settings)
                    {
                        // if (pair.Key == "hbm_no_save" && orig[i].Contains("hbm_no_save")) orig[i] = orig[i].Replace("true", (Settings["hbm_no_save"] == "yes").ToString().ToLower());

                        if (orig[i].Contains("background_color"))              orig[i] = orig[i].Replace("43 43 43 255", "0 0 0 255");
                        /* else if (orig[i].Contains("use_keymap") && Keymap != null)  orig[i] = orig[i].Replace("	off", "	on");
                        else if (orig[i].Contains("wii_shim.swf"))                  orig[i] = orig[i].Replace("wii_shim.swf", Path.GetFileName(path));
                        else if (orig[i].StartsWith("debug_") && i > 90)            orig[i] = orig[i].Replace("debug_", "# debug_");

                        else if (orig[i].Contains(pair.Key))
                        {
                            var list = new List<string> { "on", "off", "high", "30", "none" };
                            if (Settings["shared_object_capability"] == "on") list.AddRange(new string[] { "96", "64" });

                            foreach (var setting in list)
                                orig[i] = orig[i].Replace("	" + setting, "	ReplaceME").Replace("ReplaceME", pair.Value);
                        } */
                    }

                    if (i == 112)
                        t.Add("content_url 	file:///trusted/" + Path.GetFileName(path));

                    t.Add(string.IsNullOrEmpty(orig[i]) ? null : orig[i]);

                    if (i == 35 && Keymap != null)
                        t.Add("keyboard_keymap_file					config/keymap.ini");
                }

                File.WriteAllLines(Paths.FlashContents + "config\\common.pcf", t);
            }

            // Savebanner .TPL & config
            // ********
            if (Settings["shared_object_capability"] == "on")
            {
                TPL banner = Img.CreateSaveTPL(1);
                TPL icons = Img.CreateSaveTPL(2);
                int textures = icons.NumOfTextures;

                for (int i = 0; i < lines.Length; i++)
                {
                    var bytes = Encoding.Unicode.GetBytes(lines[i]);
                    lines[i] = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, bytes));
                }

                File.WriteAllBytes(Paths.FlashContents + "banner\\banner.tpl", banner.ToByteArray());
                File.WriteAllBytes(Paths.FlashContents + "banner\\icons.tpl", icons.ToByteArray());

                var txt = new List<string>()
                    {
                        "# generic banner setting file",
                        "#",
                        "not_copy		off",
                        "anim_type		bounce			# loop / bounce",
                        $"title_text		{System.Uri.EscapeUriString(lines[0])}",
                        $"comment_text		   {(lines.Length > 1 && !string.IsNullOrEmpty(lines[1]) ? System.Uri.EscapeUriString(lines[1]) : "%20")}",
                        "banner_tpl		banner/banner.tpl",
                        "icon_tpl		banner/icons.tpl",
                        $"icon_count		{textures}			# texture_index: 0 -> 0",
                    };

                for (int i = 0; i < textures; i++)
                {
                    txt.Add($"icon_speed		{i}, slow");
                }

                File.WriteAllBytes(Paths.FlashContents + "banner\\banner.ini", new UTF8Encoding(false).GetBytes(string.Join("\n", txt) + "\n\n"));

                banner.Dispose();
                icons.Dispose();
            }
        }
    }
}
