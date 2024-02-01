# FriishProduce
<div align=center><a href=""><img src="https://raw.githubusercontent.com/CatmanFan/FriishProduce/main/legacy/FriishProduce/Resources/images/icon.png" width="105" height="75" /></a><br>
<img src="https://github-production-user-asset-6210df.s3.amazonaws.com/30674270/301394080-41e75431-dd12-4279-b9a2-d3639655cb97.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240201%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240201T052141Z&X-Amz-Expires=300&X-Amz-Signature=ef1c65ed0333380ac9e02f54bea6dce0d45ec301b2b0a893195d26bb8de0c074&X-Amz-SignedHeaders=host&actor_id=30674270&key_id=0&repo_id=625060006" width="600" height="394"/><br>
<a href="https://gbatemp.net/threads/friishproduce-multiplatform-wad-injector.632028/"><img src="https://img.shields.io/badge/GBAtemp-thread-informational?style=plastic" /></a>
</div>

**FriishProduce** is a WAD injector for Wii/vWii, which allows for automated ROM replacement in Wii channels.
This application is designed to streamline the process to as few third-party programs as possible.

***This repository has been updated by way of a new MDI version and is currently under work-in-progress. Most consoles may not be supported by this edition yet. For the legacy edition, go [here](https://github.com/CatmanFan/FriishProduce/tree/main/legacy).***

---

## Features
This injector bypasses other third-party assets (such as Common-Key.bin, HowardC's tools, and Autoinjectuwad/Devilken's VC) by handling many steps directly from the program's code. Some examples:
* Automatic WAD/U8 handling
* VC ROM injection through hex writing and/or file replacement
* Automatic banner/icon editing
* Automatic editing of source WAD's savedata where available
* Additional content/emulator options for each platform where supported
* Replace WAD contents with forwarder to auto-load specific emulator core and ROM

## How-to
*Coming soon*

---
For information on how to use the [legacy edition](https://github.com/CatmanFan/FriishProduce/tree/main/legacy) (versions 0.0.1.0-alpha to 0.26-beta), see the [Wiki](https://github.com/CatmanFan/FriishProduce/wiki/Setup).

**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

See [here](https://github.com/CatmanFan/FriishProduce/wiki/Translation) for a how-to on translating the app.

---

### Platforms
Currently supported:
* Virtual Console
  * **Nintendo Entertainment System (NES) / Famicom**
  * **Super Nintendo Entertainment System (SNES) / Super Famicom**
  * **Nintendo 64**

## To-Do
* ***Before releasing v1.0:***
  * Add support for the following:
    * Genesis/Mega Drive
    * Master System
    * TurboGrafx-16/PC Engine
    * NEO-GEO
    * MSX
    * Adobe Flash
  * Check for additional bugs that need to be fixed and streamline code to make it more readable.
* Try redirecting foreign language *.json files in [main/FriishProduce/langs](https://github.com/CatmanFan/FriishProduce/tree/main/FriishProduce/langs) to a separate location/repo
* Other VC injection support:
  * Commodore 64 (needs [Custom Frodo](https://gbatemp.net/threads/custom-frodo-for-c64-vc-injection.102356/))
  * TurboGrafx-16 CD (needs several programs to convert ISO to injectable format)

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by **[WiiDatabase](https://github.com/WiiDatabase)**.
* **WWCXTool** by alpha-0.
* * **ROMC compressor** by Jurai, with additional LZSS code by Haruhiko Okumura.
* **[ccf-tools](https://github.com/libertyernie/ccf-tools)** (orig. author: paulguy) and **[BrawlLib](https://github.com/libertyernie/brawllib-wit)** (orig. author: soopercool101), both forked by **[libertyernie](https://github.com/libertyernie)**.

For icons and interface:
* **[RibbonWinForms](https://github.com/ribbonwinforms/ribbonwinforms)**.
* **[MdiTabCtrl](https://github.com/JacksiroKe/MdiTabCtrl)** by Jack Siro.
* **[Fugue Icons](https://p.yusukekamiyamane.com/)** by Yusuke Kamiyamane (Creative Commons Attribution 3.0 License).
* **[Farm Fresh Icons](https://github.com/gammasoft/fatcow)** by Fatcow Web Hosting (Creative Commons Attribution 3.0 License).

I would also like to thank the following people:
* **[SuperrSonic](https://github.com/SuperrSonic)** for reverse-engineering much of Wii software and official emulator code, and in particular, his [fork of RetroArch Wii](https://github.com/SuperrSonic/RA-SS).
* **[saulfabreg](https://github.com/saulfabregwiivc)** for archiving several tools and aiding in research & documentation.
* **[sr_corsario](https://gbatemp.net/members/sr_corsario.128473/)** for his work in disclosing NEO-GEO ROM injection methods.
* **[Larsenv](https://github.com/Larsenv)** for his astounding work in the Wii homebrew community, and for originally disclosing a method for Flash WAD injection ([GBAtemp thread](https://gbatemp.net/threads/how-to-make-flash-game-wad-injects.561406/)).
* And of course, the team at the 0RANGECHiCKEN release group, including [lolsjoel](https://gbatemp.net/members/lolsjoel.18721/), and the late [G0dLiKe](https://gbatemp.net/members/g0dlike.190457/), without whose work this project would not have been possible.

This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).
