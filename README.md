# FriishProduce
<div align=center><a href=""><img src="https://raw.githubusercontent.com/CatmanFan/FriishProduce/main/legacy/FriishProduce/Resources/images/icon.png" width="105" height="75" /></a><br>
<img src="https://github.com/CatmanFan/FriishProduce/blob/main/images/new.png?raw=true"/><br>
<a href="https://gbatemp.net/threads/friishproduce-multiplatform-wad-injector.632028/"><img src="https://img.shields.io/badge/GBAtemp-link-blue" /></a>
</div>

**FriishProduce** is a WAD injector for Wii/vWii, which allows for automated ROM/disc/SWF file replacement in Wii channels.
This application is designed to streamline the process to as few third-party programs as possible.

***This repository has been updated by way of a new MDI version (pictured above) and is currently under work-in-progress. Most consoles may not be supported by this version yet.<br>For the legacy edition, see [here](https://github.com/CatmanFan/FriishProduce/tree/main/legacy), or see [v0.26-beta](https://github.com/CatmanFan/FriishProduce/releases/tag/v0.26-beta) for the latest release.***

---

## Features
This injector bypasses other third-party assets (such as Common-Key.bin, HowardC's tools, and Autoinjectuwad/Devilken's VC) by handling many steps directly from the program's code. Some examples:
* Automatic WAD/U8 handling
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
* **SNK NEO-GEO**
 
Planned:
* Virtual Console
  * NEC TurboGrafx-16 / PC Engine
  * Microsoft MSX / MSX2
* Support for other formats:
  * Adobe Flash
* Complete SEGA VC options list & add NEO-GEO VC options list.

## How-to
* For information on how to use the new interface (v1.0 and above), see **[here](https://github.com/CatmanFan/FriishProduce/wiki/FriishProduce-v1.0)**.
* For information on how to use the legacy beta versions (v0.26-beta and earlier), see **[here](https://github.com/CatmanFan/FriishProduce/wiki/FriishProduce-Legacy)** (*recommended*).

**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

---

## Translation
The current version of this application includes a built-in XML editor for adding custom language files. If you have translated using that interface, please send in your XMLs [as a pull request](https://github.com/CatmanFan/FriishProduce/pulls).

---

## To-Do
* ***Before releasing v1.0:***
  * Check for additional bugs that need to be fixed and streamline code to make it more readable.
* Other VC injection support to be done later:
  * Commodore 64 (needs [Custom Frodo](https://gbatemp.net/threads/custom-frodo-for-c64-vc-injection.102356/))
  * TurboGrafx-16 CD (needs several programs to convert ISO to injectable format)=

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by **[WiiDatabase](https://github.com/WiiDatabase)**.
* [SD/USB forwarder components](https://github.com/modmii/modmii.github.io/tree/master/Support/DOLS) of **[ModMii](https://github.com/modmii/modmii.github.io)** by [XFlak](https://github.com/xflak).
* **WWCXTool** by alpha-0.
* **ROMC compressor** by Jurai, with additional LZSS code by Haruhiko Okumura.
* **[ccf-tools](https://github.com/libertyernie/ccf-tools)** (orig. author: paulguy) and **[BrawlLib](https://github.com/libertyernie/brawllib-wit)** (orig. author: soopercool101), both forked by **[libertyernie](https://github.com/libertyernie)**.

For icons and interface:
* **[MdiTabCtrl](https://github.com/JacksiroKe/MdiTabCtrl)** by Jack Siro.
* **[Fugue Icons](https://p.yusukekamiyamane.com/)** by Yusuke Kamiyamane (Creative Commons Attribution 3.0 License).

I would also like to thank the following people:
* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).
* **[saulfabreg](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research & documentation.
* **[sr_corsario](https://gbatemp.net/members/sr_corsario.128473/)** for his work in disclosing NEO-GEO ROM injection methods.
* **[Larsenv](https://github.com/Larsenv)** for his astounding work in the Wii homebrew community, and for originally disclosing a method for Flash WAD injection ([GBAtemp thread](https://gbatemp.net/threads/how-to-make-flash-game-wad-injects.561406/)).
* And of course, the team at the 0RANGECHiCKEN release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.

This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).
