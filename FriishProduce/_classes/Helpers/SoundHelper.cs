using libWiiSharp;
using System.IO;

namespace FriishProduce
{
    public static class SoundHelper
    {
        private static byte[] toRawBNS(byte[] sound_bin)
        {
            byte[] orig = sound_bin;

            if (Headers.DetectHeader(sound_bin) == Headers.HeaderType.IMD5)
                orig = Headers.IMD5.RemoveHeader(sound_bin);

            if (Lz77.IsLz77Compressed(orig))
            {
                byte[] lz77_decomp = new Lz77().Decompress(orig);
                orig = lz77_decomp;
            }

            return orig;
        }

        private static byte[] toSoundBin(byte[] input, byte[] orig_sound_bin)
        {
            byte[] orig = orig_sound_bin, output;

            bool hasIMD5 = Headers.DetectHeader(orig_sound_bin) == Headers.HeaderType.IMD5;
            if (hasIMD5)
                orig = Headers.IMD5.RemoveHeader(orig_sound_bin);

            bool isLZ77 = Lz77.IsLz77Compressed(orig);

            output = isLZ77 ? new Lz77().Compress(input) : input;
            if (hasIMD5) output = Headers.IMD5.AddHeader(output);

            return output;
        }

        // -------------------------------------------------------------------------------------------------------

        public static void ReplaceSound(WAD w, UnmanagedMemoryStream wav)
        {
            using MemoryStream ms = new();
            wav.CopyTo(ms);
            ReplaceSound(w, ms.ToArray());
        }

        public static void ReplaceSound(WAD w, string wav) => ReplaceSound(w, File.ReadAllBytes(wav));

        public static void ReplaceSound(WAD w, byte[] wav)
        {
            foreach (var item in w.BannerApp.StringTable)
            {
                if (item.ToLower().Contains("sound.bin"))
                {
                    byte[] orig = w.BannerApp.Data[w.BannerApp.GetNodeIndex(item)];

                    BNS bns = new(wav);
                    bns.Convert();

                    w.BannerApp.ReplaceFile(w.BannerApp.GetNodeIndex(item), toSoundBin(bns.ToByteArray(), orig));
                }
            }
        }

        public static byte[] ExtractSound(WAD w, bool isWav = true)
        {
            foreach (var item in w.BannerApp.StringTable)
            {
                if (item.ToLower().Contains("sound.bin"))
                {
                    if (isWav)
                    {
                        byte[] orig = toRawBNS(w.BannerApp.Data[w.BannerApp.GetNodeIndex(item)]);

                        Wave wav = BNS.BnsToWave(orig);
                        
                        return wav.ToByteArray();
                    }

                    else return w.BannerApp.Data[w.BannerApp.GetNodeIndex(item)];
                }
            }

            return null;
        }
    }
}
