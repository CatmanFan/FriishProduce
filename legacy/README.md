# FriishProduce (Legacy)
<div align=center><a href=""><img src="https://raw.githubusercontent.com/CatmanFan/FriishProduce/main/legacy/FriishProduce/Resources/images/icon.png" width="172" height="125" /></a><br>
<a href="https://gbatemp.net/threads/friishproduce-multiplatform-wad-injector.632028/"><img src="https://img.shields.io/badge/GBAtemp-thread-informational?style=plastic" /></a>
</div>

This is the main repository of the legacy version of **FriishProduce** (WAD injector for Wii/vWii, which allows for automated ROM replacement in Wii channels), which includes versions v0.0.1.0-alpha to v0.26-beta.

*For the current version, go [here](https://github.com/CatmanFan/FriishProduce).*

---

## How-to
Please see the [Wiki](https://github.com/CatmanFan/FriishProduce/wiki/Setup) for instructions on setup and usage.

**Make sure you have a (v)Wii NAND backup in case anything goes wrong!**

See [here](https://github.com/CatmanFan/FriishProduce/wiki/Translation) for a how-to on translating the app.

---

### Platforms
* Virtual Console
  * **Nintendo Entertainment System (NES) / Famicom**
  * **Super Nintendo Entertainment System (SNES) / Super Famicom**
  * **Nintendo 64**
  * **SEGA Master System**
  * **SEGA Mega Drive / Genesis**
  * **NEC TurboGrafx-16 / PC Engine**
  * **SNK NEO-GEO**
  * **Microsoft MSX / MSX2**

* Emulator forwarders
  * **[FCE Ultra GX](https://github.com/dborth/fceugx)** (dborth et al.)
  * **[FCE Ultra RX](https://github.com/niuus/FCEUltraRX)** (NiuuS et al.)
  * **[FCEUX TX / FCEUGX-1UP](https://gbatemp.net/threads/fceugx-1up.558023/)** (Tanooki16)
  * **[Snes9x GX](https://github.com/dborth/snes9xgx)** (dborth et al.)
  * **[Snes9x RX](https://github.com/niuus/Snes9xRX)** (NiuuS et al.)
  * **[Snes9x TX / Snes9xGX-Mushroom](https://gbatemp.net/threads/snes9xgx-mushroom.558500/)** (Tanooki16)
  <!-- * **[Visual Boy Advance GX](https://github.com/dborth/vbagx)** (dborth et al.) -->
  <!-- * **[mGBA Wii](https://github.com/mgba-emu/mgba)** (endrift et al.) -->
  * **[Wii64 1.3 MOD](https://github.com/saulfabregwiivc/Wii64/tree/wii64-wiiflow)** forked by saulfabreg (original author: Wii64 Team)
  * **[Not64](https://github.com/extremscorner/not64)** (Extrems)
  * **[Mupen64GC-FIX94](https://github.com/FIX94/mupen64gc-fix94)** (Wii64 Team, forked by FIX94)
  * **[Genesis Plus GX](https://github.com/ekeeke/Genesis-Plus-GX)** (eke-eke et al.)
  * **[WiiSX](https://github.com/emukidid/pcsxgc)** (Wii64 Team)
  * **[WiiStation / WiiSXRX_2022](https://github.com/xjsxjs197/WiiSXRX_2022)** (xjsxjs197, forked from NiuuS' WiiSX RX)
  <!-- * **[WiiMednafen](https://github.com/raz0red/wii-mednafen)** (raz0red) -->

* Other
  * **Shockwave/Adobe Flash**

## Credits
This program uses the following third-party components and apps:
* **libWiiSharp** (orig. author: Leathl), [forked](https://github.com/WiiDatabase/libWiiSharp/) by **[WiiDatabase](https://github.com/WiiDatabase)**.
* **[Wii.cs](https://github.com/dnasdw/showmiiwads/blob/Wii.cs_Tools/U8Mii/Wii.cs)** library (part of Leathl's [ShowMiiWads](https://code.google.com/archive/p/showmiiwads/source)).
* **[Floating IPS](https://github.com/Alcaro/Flips)** (Flips) by [Alcaro](https://github.com/Alcaro).
* **[Xdelta](https://github.com/jmacd/xdelta)** by [jmacd](https://github.com/jmacd).
* **[ccf-tools](https://github.com/libertyernie/ccf-tools)** (orig. author: paulguy) and **[BrawlLib](https://github.com/libertyernie/brawllib-wit)** (orig. author: soopercool101), both forked by **[libertyernie](https://github.com/libertyernie)**.
* **ROMC compressor** by Jurai, with additional LZSS code by Haruhiko Okumura.
* **[gbalzss](https://gbadev.org/tools.php?showinfo=56)** by Andre Perrot, with additional LZSS code by Haruhiko Okumura.
* **WWCXTool** by alpha-0.
* **[lzh8_cmpdec](https://www.hcs64.com/vgm_ripping.html)** by [hcs](http://hcs64.com/).
* **[HowardC's Tools](https://gbatemp.net/threads/vcfe-wip.100556/)** (particularly, VCbrlyt).
* **[sm64tools](https://github.com/queueRAM/sm64tools)** by [queueRAM](https://github.com/queueRAM).
* **[z64compress](https://github.com/z64tools/z64compress)** by [z64tools](https://github.com/z64tools).
* For homebrew emulators compatiblity:
  * **comex**'s NAND loader. *(file renamed as "nandloader_wii_comex.app")*
  * **Waninkoko**'s NAND loader, retrieved from ShowMiiWads repo. *(file renamed as "nandloader_wii.app")*
  * **FIX94**'s **[tiny-vwii-nand-loader](https://github.com/FIX94/tiny-vwii-nand-loader)**. *(file renamed as "nandloader_vwii.app")*
  * Forwarder DOLs generated from **[ModMii Classic](https://modmii.github.io)** (application author: XFlak) *(files renamed as "forwarder_v[XX].dol")*
This application is distributed and licensed under the **GNU General Public License v3.0** ([view in full](https://github.com/CatmanFan/FriishProduce/blob/main/LICENSE)).
