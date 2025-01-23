A. Getting LZ77_snapshot.bin
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
1. Unpack the international karate wad
2. Extract the contents of the 5.app u8 archive
3. There you have LZ77_snapshot.bin, using
gbalzss d LZ77_snapshot.bin ik.fss
decompress LZ77_snapshot.bin file with name ik.fss, and put this into the folder where the custom frodo resides.

B. Creating snapshot for injection
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
1. Open Custom Frodo, load the game to inject using a disk image (d64 file).
a. Tools/Preferences 8-> Browse and select d64 file. On C64 Basic prompt type LOAD"$",8:LIST
b. Type LOAD"GAMENAME",8:RUN
2. Once the game loaded and ran, select Tools->Patch and Save snapshot and give a name to your snapshot and save. (default is snap.fss)
3. Test your game if it loads using the Load Snapshot option, if it doesn't, try taking the snapshot elsewhere in the game.

C. Injecting
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
1. Compress your snapshot using gbalzss tool, like gbalzss e snap.fss LZ77_snapshot.bin
2. Copy (and overwrite the file with the same name) the produced LZ77_snapshot.bin file to the folder where you extracted the 5.app file.
3. Using u8coes.exe tool reconstruct the 5.app file. Run u8coes.exe, select the 5.app file you extracted out of the wad as original u8 file. The next folder will be automatically selected. For U8 archive to save, point to a safe and known location and click the create button. Now copy created 5.app file to its original location and overwrite the one there.
4. If your wii is not pal then you need to patch the 1.app file using Wii Generic Patcher to change video mode.
5. The rest is wad packing as usual and if your wii is not pal then you should use the freethewads to region free the resulting wad.
