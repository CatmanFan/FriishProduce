## Getting started
The menu strip above includes several icons for project functions to the top-left. This includes buttons to **browse for a ROM** (*the disc icon*) or for **an image** (*the photo icon*), to **save the project** as a proprietary format (*the floppy disk icon*) or to **export as a WAD** (*the box icon*), and to **retrieve game metadata online** (*the LibRetro icon*). You can also close the current project from this menu (this will discard all unsaved data). These can all also be accessed from the "Project" menu above the icons set.

## Creating a project
To start, go to the "File" menu on the top-left corner, then pick a console from the new project list.
This will create a new WAD project, but you will need to open a ROM for the project to use before it can be enabled. To do this, click on the disc icon at the top, and browse for your file.

Afterwards, you will need to also add an image (by clicking the photo icon and browsing an image file). For most ROMs, the title screen image can be retrieved from TCRF.net, the [LibRetro database](https://github.com/libretro/libretro-thumbnails), or Romhacking.net. You will also need to fill in the fields needed for the channel name, the full software title, year released and players supported for the banner display, and the savedata header title (if supported).

Alternatively, you can use the retrieve game data function to do all or part of this for you, which will supply relevant data from the LibRetro database depending on whether the ROM is listed there or not (some platforms such as Flash do not support this function).

## Project settings & content options
	* You can **choose which base channel to use** to inject into, and which region of said channel, or you can **get a separate offline WAD file** to use instead. Some VC bases may have different compatibility or more features than other VC bases.
	* When selecting a ROM, in certain instances you can also optionally **add a patch file** (.ips / .bps / .xdelta). Clicking the Import patch checkbox in the ROM information section will open a browse file dialog where you can search for your desired patch file, after which it will be applied to the ROM itself when it's injected.
	* You can also edit some of the other options available, such as your **image's interpolation mode**, the **WAD title ID**, or the **content options for the current injection method** (clicking on the gear icon next to the methods list will open up a separate options menu).
	* You can also import a **[custom VC manual with edited HTML and/or image files](vc-manual.md)** tailed to your game, use the original manual, or use none altogether.
	* If you want to avoid using Virtual Console altogether, you can **change the injection method and use an emulator forwarder** instead in the content options section.
	* You can **change the WAD region** to match that of your console, or make it region-free. This fixes any problems that may surface when launching the channel from the Wii Menu on a console that does not match its region (for example, the message "This channel cannot be used").

## Finishing
Once you're done, click on the box icon and choose a location and/or filename to save your WAD to. If you are creating a forwarder, this will also create a separate ZIP file in the same path, which you will have to extract to your Wii SD or USB root (the WAD will not work otherwise).
If you encounter any issues with the current version, please feel free to notify at the FriishProduce GitHub repo, or on its release thread on GBAtemp.

In case you prefer to change any of the details you have later, you can also choose to save your project by clicking on the floppy disk icon.