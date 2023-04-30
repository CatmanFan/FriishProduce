# FriishProduce
**FriishProduce** is a WAD injector for Wii/vWii, which allows for automated ROM replacement in Wii channels.
This application is designed to streamline the process to as few third-party programs as possible.

***The project and readme are currently under work-in-progress and will be updated over the course of time.***

## How-to
Please see the [Wiki](https://github.com/CatmanFan/FriishProduce/wiki/Setup) for instructions on setup and usage.

**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

## Features
This injector bypasses other third-party assets (such as Common-Key.bin, most of HowardC's tools, and Autoinjectuwad/Devilken's VC) by handling many steps directly from the program's code. Some examples:
* WAD/U8 handling using libWiiSharp
* VC ROM injection through hex writing and/or file replacement
* Automatic banner/icon editing
* Automatic editing of source WAD's savedata where available
* Additional content/emulator options for each platform where supported
See [here](https://github.com/CatmanFan/FriishProduce/wiki/Translation) for a how-to on translating the app.

### Platforms
Currently supported as of the latest release:
* **Nintendo Entertainment System (NES) / Famicom** (Virtual Console)
* **Super Nintendo Entertainment System (SNES) / Super Famicom** (Virtual Console)
* **Nintendo 64** (Virtual Console)
* **Shockwave/Adobe Flash**
<!-- ### To-Do
- [ ] SEGA: Use blank saveicon template(s) in Resources -->

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by **[WiiDatabase](https://github.com/WiiDatabase)**.
* **[Floating IPS](https://github.com/Alcaro/Flips)** (Flips) by [Alcaro](https://github.com/Alcaro).
* **[ccf-tools](https://github.com/libertyernie/ccf-tools)** (orig. author: paulguy) and **[BrawlLib](https://github.com/libertyernie/brawllib-wit)** (orig. author: soopercool101), both forked by **[libertyernie](https://github.com/libertyernie)**.
* **ROMC compressor** by Jurai, with additional LZSS code by Haruhiko Okumura.
* **gbalzss** by Andre Perrot, with additional LZSS code by Haruhiko Okumura.
* **WWCXTool** by alpha-0.
* **HowardC's Tools** (particularly, VCbrlyt).

I would also like to thank the following people:
* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).
* **[SaulFabre](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research & documentation.
* **[Larsenv](https://github.com/Larsenv)** for his astounding work in the Wii homebrew community, and for originally disclosing a method for Flash WAD injection ([GBAtemp thread](https://gbatemp.net/threads/how-to-make-flash-game-wad-injects.561406/)).
* And of course, the team at the 0RANGECHiCKEN release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.

This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).