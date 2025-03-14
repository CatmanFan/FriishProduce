using System.IO;

namespace FriishProduce
{
    public class ROM_C64 : ROM
    {
        public ROM_C64() : base() { }

        protected override void Load()
        {
        }

        public byte[] ToD64(byte[] romData = null)
        {
            var ROM = romData != null ? romData : patched?.Length > 0 ? patched : origData;

            if (Path.GetExtension(FilePath).ToLower() == ".t64")
            {
                File.WriteAllBytes(Paths.WorkingFolder + "in.t64", ROM);

                Utils.Run
                (
                    "c64\\c1541\\c1541.exe",
                    Paths.Tools + "c64\\c1541\\",
                    $"-verbose off -silent on -format \"fromt64,01\" d64 \"{Paths.WorkingFolder + "out.d64"}\" -tape \"{Paths.WorkingFolder + "in.d64"}\""
                );

                ROM = File.ReadAllBytes(Paths.WorkingFolder + "out.d64");

                try { File.Delete(Paths.WorkingFolder + "in.t64"); } catch { }
                try { File.Delete(Paths.WorkingFolder + "out.d64"); } catch { }
                try { File.Delete(Paths.Tools + "c64\\c1541\\stderr.txt"); } catch { }
                try { File.Delete(Paths.Tools + "c64\\c1541\\stdout.txt"); } catch { }
            }

            return ROM;
        }
    }
}
