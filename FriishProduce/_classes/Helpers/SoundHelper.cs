using libWiiSharp;
using System.IO;

namespace FriishProduce
{
    public static class SoundHelper
    {
        public static void ReplaceSound(WAD w, UnmanagedMemoryStream wav)
        {
            using (var ms = new MemoryStream())
            {
                wav.CopyTo(ms);
                ReplaceSound(w, ms.ToArray());
            }
        }

        public static void ReplaceSound(WAD w, string wav) => ReplaceSound(w, File.ReadAllBytes(wav));

        public static void ReplaceSound(WAD w, byte[] wav)
        {
            foreach (var item in w.BannerApp.StringTable)
            {
                if (item.ToLower().Contains("sound.bin"))
                {
                    var modWav = wav;

                    var soundFile = w.BannerApp.Data[w.BannerApp.GetNodeIndex(item)];
                    soundFile = Headers.IMD5.RemoveHeader(soundFile);
                    if (Lz77.IsLz77Compressed(soundFile))
                        modWav = new Lz77().Compress(wav);

                    w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex(item), wav[0] == 'R' && wav[1] == 'I' & wav[2] == 'F' && wav[3] == 'F' ? Headers.IMD5.AddHeader(modWav) : modWav);
                }
            }
        }
    }
}
