# FriishProduce
<div align=center><a href=""><img src="https://github.com/CatmanFan/FriishProduce/blob/main/FriishProduce/Resources/icon.png" width="105" height="75" /></a><br>
<!-- <img src="https://github.com/CatmanFan/FriishProduce/blob/main/images/new.png?raw=true"/><br> -->
<a href="https://gbatemp.net/threads/friishproduce-multiplatform-wad-injector.632028/"><img src="https://img.shields.io/badge/GBAtemp-link-blue" /></a>
</div>

**FriishProduce** is a WAD channel injector/creator for (v)Wii. It can be used to convert ROMs, disc images or other types of software to installable WADs for Wii/vWii (Wii U). This includes injectable Virtual Console (VC) games, as well as single ROM loaders (SRLs), and Adobe Flash files.
This application is designed to streamline the process to as few third-party programs as possible.

---

## Features
This injector bypasses other third-party assets (such as Common-Key.bin, HowardC's tools, and Autoinjectuwad/Devilken's VC) by handling many steps directly from the program's code. Some examples:
* Automatic WAD/U8/CCF handling
* VC ROM injection through hex writing and/or file replacement
* Automatic banner/icon editing
* Automatic editing of source WAD's savedata where available
* Additional content/emulator options for each platform where supported
* Replace WAD contents with forwarder to auto-load specific emulator core and ROM

### Platforms
**Currently supported:**
  * Virtual Console:
    * **Nintendo Entertainment System (NES) / Famicom**
    * **Super Nintendo Entertainment System (SNES) / Super Famicom**
    * **Nintendo 64**
    * **SEGA Master System**
    * **SEGA Mega Drive / Genesis**
    * **NEC TurboGrafx-16 / PC Engine (HuCARD)**
    * **SNK NEO-GEO**
    * **Microsoft MSX / MSX2**
  * Others:
    * **Adobe Flash**
    * **Sony PlayStation**
    * **RPG Maker 2000 / 2003**

---

## Instructions
See **[here](https://github.com/CatmanFan/FriishProduce/wiki/FriishProduce-v1.0)** for a tutorial on how to use the app. For translations, see **[here](https://github.com/CatmanFan/FriishProduce/wiki/Translation)**.

---

## To-Do
* Redesign interface and logo!
  * Replace toolbar with project/ROM/image buttons with consoles toolbar for the purpose of only adding tabs
  * Make project forms inheritable and categorize by console/platform
  * Place ROM and image open dialogs in ProjectForms instead of handling from MainForm
* For next version:
  - [ ] Use *.bin format for disc games by default
  - [ ] Restructuring, trimming features and cleaning code


* Other VC injection support:
  * Commodore 64 (needs [Custom Frodo](https://gbatemp.net/threads/custom-frodo-for-c64-vc-injection.102356/))
  * TurboGrafx-16 CD (needs several programs to convert ISO to injectable format)

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by [WiiDatabase](https://github.com/WiiDatabase).
* [SD/USB forwarder components](https://github.com/modmii/modmii.github.io/tree/master/Support/DOLS) of **[ModMii](https://github.com/modmii/modmii.github.io)** by [XFlak](https://github.com/xflak).
* **WWCXTool** by alpha-0.
* **romc0** and **lzh8_cmpdec** by [hcs64](https://github.com/hcs64).
* **[ROMC VC Compressor](https://www.elotrolado.net/hilo_romc-vc-compressor_1015640)** by Jurai, with additional LZSS code by Haruhiko Okumura.
* [libertyernie](https://github.com/libertyernie)'s **[fork of BrawlLib](https://github.com/libertyernie/brawllib-wit)** by soopercool101.
* [Static WAD Base](https://github.com/Brawl345/customizemii/blob/master/Base_WADs/StaticBase.wad) from **CustomizeMii**.

For icons and interface:
* **[MdiTabCtrl](https://github.com/JacksiroKe/MdiTabCtrl)** by Jack Siro.
* **[Fugue Icons](https://p.yusukekamiyamane.com/)** by Yusuke Kamiyamane (CC BY 3.0).
* **[SCE-PS3 Rodin LATIN](https://github.com/skrptktty/ps3-firmware-beginners-luck/blob/master/PS3_411/update_files/dev_flash/data/font/SCE-PS3-RD-R-LATIN.TTF) font** from [this repo](https://github.com/skrptktty/ps3-firmware-beginners-luck) (Solar Storm License).

I would also like to thank the following people:
* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).
* **[saulfabreg](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research & documentation.
* **[sr_corsario](https://gbatemp.net/members/sr_corsario.128473/)** for his work in disclosing NEO-GEO ROM injection methods.
* **[Larsenv](https://github.com/Larsenv)** for his astounding work in the Wii homebrew community, and for originally disclosing a method for Flash WAD injection ([GBAtemp thread](https://gbatemp.net/threads/how-to-make-flash-game-wad-injects.561406/)).
* And of course, the team at the 0RANGECHiCKEN release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.

### Emulators
* **[FCE Ultra GX](https://github.com/dborth/fceugx)** (dborth et al.)
* **[FCE Ultra RX](https://github.com/NiuuS/FCEUltraRX)** (NiuuS et al.)
* **[FCEUX TX / FCEUGX-1UP](https://gbatemp.net/threads/fceugx-1up.558023/)** (Tanooki16)
* **[Snes9x GX](https://github.com/dborth/snes9xgx)** (dborth et al.)
* **[Snes9x RX](https://github.com/NiuuS/Snes9xRX)** (NiuuS et al.)
* **[Snes9x TX / Snes9xGX-Mushroom](https://gbatemp.net/threads/snes9xgx-mushroom.558500/)** (Tanooki16)
* **[Visual Boy Advance GX](https://github.com/dborth/vbagx)** (dborth et al.) -->
* **[mGBA Wii](https://github.com/mgba-emu/mgba)** (endrift et al.) -->
* **[Wii64 1.3 MOD](https://github.com/saulfabregwiivc/Wii64/tree/wii64-wiiflow)** (Wii64 Team, forked by saulfabreg)
* **[Not64](https://github.com/extremscorner/not64)** (Extrems)
* **[Mupen64GC-FIX94](https://github.com/FIX94/mupen64gc-fix94)** (Wii64 Team, forked by FIX94)
* **[Genesis Plus GX](https://github.com/ekeeke/Genesis-Plus-GX)** (eke-eke et al.)
* **[WiiSX](https://github.com/emukidid/pcsxgc)** (Wii64 Team)
* **[EasyRPG Player](https://github.com/EasyRPG/Player)** (EasyRPG Team)
<!-- * **[WiiStation / WiiSXRX_2022](https://github.com/xjsxjs197/WiiSXRX_2022)** (xjsxjs197, forked from NiuuS' WiiSX RX) -->
<!-- * **[WiiMednafen](https://github.com/raz0red/wii-mednafen)** (raz0red) -->

### License
This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).

---

To view the source code for the legacy interface (all versions up to and including v0.26-beta), go **[here](https://github.com/CatmanFan/FriishProduce-Legacy)**.
