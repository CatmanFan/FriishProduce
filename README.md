# FriishProduce
<div align=center><a href=""><img src="https://raw.githubusercontent.com/CatmanFan/FriishProduce/main/FriishProduce/Resources/images/icon.png" width="172" height="125" /></a><br>
<a href="https://gbatemp.net/threads/friishproduce-multiplatform-wad-injector.632028/"><img src="https://img.shields.io/badge/GBAtemp-thread-informational?style=plastic" /></a>
</div>

**FriishProduce** is a WAD injector for Wii/vWii, which allows for automated ROM replacement in Wii channels.
This application is designed to streamline the process to as few third-party programs as possible.

***The project and readme are currently under work-in-progress and will be updated over the course of time.***

---

## Features
This injector bypasses other third-party assets (such as Common-Key.bin, most of HowardC's tools, and Autoinjectuwad/Devilken's VC) by handling many steps directly from the program's code. Some examples:
* WAD/U8 handling using libWiiSharp
* VC ROM injection through hex writing and/or file replacement
* Automatic banner/icon editing
* Automatic editing of source WAD's savedata where available
* Additional content/emulator options for each platform where supported
* Replace WAD contents with forwarder to auto-load specific emulator core and ROM

## How-to
Please see the [Wiki](https://github.com/CatmanFan/FriishProduce/wiki/Setup) for instructions on setup and usage.

**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

See [here](https://github.com/CatmanFan/FriishProduce/wiki/Translation) for a how-to on translating the app.

---

### Platforms
Currently supported:
* Virtual Console
  * **Nintendo Entertainment System (NES) / Famicom**
  * **Super Nintendo Entertainment System (SNES) / Super Famicom**
  * **Nintendo 64**
  * **Master System**
  * **Mega Drive / Genesis**
<!-- * **TurboGrafx-16 / PC Engine** -->
<!-- * **SNK NEO-GEO** -->

* Emulator forwarders
  * **[FCE Ultra GX](https://github.com/dborth/fceugx)** (dborth et al.)
  * **[Snes9x-GX](https://github.com/dborth/snes9xgx)** (dborth et al.)
  <!-- * **[Visual Boy Advance GX](https://github.com/dborth/vbagx)** (dborth et al.) -->
  * **[mupen64gc-fix94](https://github.com/FIX94/mupen64gc-fix94)** (Wii64 Team, forked by FIX94)
  <!-- * **[WiiSXRX](https://github.com/niuus/WiiSXRX)** (niuus, forked from Mystro256's WiiSXR) -->

* Other
  * **Shockwave/Adobe Flash**

<!-- ## To-Do
- [ ] LZ77 compression on SNES VC and TG-16 HuCARD VC.
- [X] NeoGeo VC injection support.
- [ ] MSX / MSX2 VC injection support.
- [ ] Commodore 64 VC injection support.
- [ ] Support for more systems and homebrew emulators in emulator forwarder mode.
  - [ ] Game Boy/Game Boy Color/Game Boy Advance (need to implement custom banner and determine what type of WADs should be used)
- [ ] Multiple SWF support for Adobe Flash.
- [ ] TurboGrafx-16 CD VC injection support. -->

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by **[WiiDatabase](https://github.com/WiiDatabase)**.
* **[Floating IPS](https://github.com/Alcaro/Flips)** (Flips) by [Alcaro](https://github.com/Alcaro).
* **[ccf-tools](https://github.com/libertyernie/ccf-tools)** (orig. author: paulguy) and **[BrawlLib](https://github.com/libertyernie/brawllib-wit)** (orig. author: soopercool101), both forked by **[libertyernie](https://github.com/libertyernie)**.
* **ROMC compressor** by Jurai, with additional LZSS code by Haruhiko Okumura.
* **gbalzss** by Andre Perrot, with additional LZSS code by Haruhiko Okumura.
* **[LZSS](https://www.romhacking.net/utilities/826/)** by CUE.
* **[lzh8_cmpdec](https://www.hcs64.com/vgm_ripping.html)** by [hcs](http://hcs64.com/).
* **HowardC's Tools** (particularly, VCbrlyt).
* For homebrew emulators compatiblity:
  * **Waninkoko**'s NAND loader, retrieved from ShowMiiWads repo. *(file renamed as "nandloader_wii.app")*
  * **FIX94**'s **[tiny-vwii-nand-loader](https://github.com/FIX94/tiny-vwii-nand-loader)**. *(file renamed as "nandloader_vwii.app")*
  * Forwarder DOL generated from **ModMii Classic** (application author: XFlak) *(file renamed as "forwarder.dol")*

I would also like to thank the following people:
* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).
* **[SaulFabre](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research & documentation.
* **[sr_corsario](https://gbatemp.net/members/sr_corsario.128473/)** for his work in disclosing NEO-GEO ROM injection methods.
* **[Larsenv](https://github.com/Larsenv)** for his astounding work in the Wii homebrew community, and for originally disclosing a method for Flash WAD injection ([GBAtemp thread](https://gbatemp.net/threads/how-to-make-flash-game-wad-injects.561406/)).
* And of course, the team at the 0RANGECHiCKEN release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.

This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).
