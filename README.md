# FriishProduce
**FriishProduce** is a channel injector for (v)Wii, which allows for ROM replacement and customization in Virtual Console WADs.
This application is designed to streamline the process to as few third-party programs as possible.

***The project and readme are currently under work-in-progress and will be updated over the course of time.***

## How-to
Please see the [Wiki](https://github.com/CatmanFan/FriishProduce/wiki/Setup) for instructions on setup and usage.
**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

## Features
This injector bypasses other third-party assets (such as Common-Key.bin, most of HowardC's tools, and autoinjectuwad) by handling many steps directly from the program's code. Some examples:
* WAD/U8 handling using libWiiSharp
* Automatic banner/icon TPL generation & replacement
* NES; SNES; N64: VC ROM injection (hex writing & file replacement)
* NES; SNES; N64: Direct image editing of original savedata TPL
* NES; SNES; N64: Automatic savetitle replacement (hex writing)
* Additional content/emulator options for each platform where supported

### To-Do
- [x] Test content1 emulator modifications on N64
- [ ] Add SEGA, TG-16, NeoGeo, C64
- [ ] SEGA: VC ROM injection (file replacement)
- [ ] SEGA: WTE/image generation & replacement
- [ ] SEGA: Automatic savetitle replacement using manual text writing
- [ ] Add Adobe Flash
- [ ] Community UI translation

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by **[WiiDatabase](https://github.com/WiiDatabase)** and **[Larsenv](https://github.com/larsenv)**.
* **[Floating IPS](https://github.com/Alcaro/Flips)** (Flips) by [Alcaro](https://github.com/Alcaro).
* **[ccf-tools](https://github.com/libertyernie/ccf-tools)** (orig. author: paulguy) and **[BrawlLib](https://github.com/libertyernie/brawllib-wit)** (orig. author: soopercool101), both forked by **[libertyernie](https://github.com/libertyernie)**.
* **ROMC compressor** by Jurai, with additional LZSS code by Haruhiko Okumura.
* **gbalzss** by Andre Perrot, with additional LZSS code by Haruhiko Okumura.
* **WWCXTool** by alpha-0.
* **HowardC's Tools** (particularly, VCbrlyt).

I would also like to thank the following people:
* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).
* **[SaulFabre](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research.
* And of course, the team at the 0RANGECHiCKEN release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.

This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).