# FriishProduce
<div align=center><a href=""><img src="https://raw.githubusercontent.com/CatmanFan/FriishProduce/main/legacy/FriishProduce/Resources/images/icon.png" width="105" height="75" /></a><br>
<img src="https://github.com/CatmanFan/FriishProduce/blob/main/images/new.png?raw=true"/><br>
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
* **Nintendo Entertainment System (NES) / Famicom**
* **Super Nintendo Entertainment System (SNES) / Super Famicom**
* **Nintendo 64**
* **SEGA Master System** & **SEGA Mega Drive / Genesis**
* **NEC TurboGrafx-16 / PC Engine**
* **SNK NEO-GEO**
* **Microsoft MSX / MSX2**
* **Adobe Flash**

---

## How-to
See **[here](https://github.com/CatmanFan/FriishProduce/wiki/FriishProduce-v1.0)** for basic instructions on how to use the app.

**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

### Translation
See **[here](https://github.com/CatmanFan/FriishProduce/wiki/Translation)** for basic instructions on translating the app.

---

## To-Do
* ***Planned later:***
  - [ ] Restructure and clean code
  - [ ] Probably reorganize consoles into their own folders?
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

This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).

---

To view the source code for the legacy interface (all versions up to and including v0.26-beta), go **[here](https://github.com/CatmanFan/FriishProduce-Legacy/upload/main/FriishProduce)**.