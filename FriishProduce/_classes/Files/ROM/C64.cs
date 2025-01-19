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
                    FileDatas.Apps.c1541,
                    "c1541.exe",
                    "-verbose off -silent on -format \"fromt64,01\" d64 \"out.d64\" -tape \"in.t64\""
                );

                ROM = File.ReadAllBytes(Paths.WorkingFolder + "out.d64");

                File.Delete(Paths.WorkingFolder + "in.t64");
                File.Delete(Paths.WorkingFolder + "out.d64");
            }

            return ROM;
        }
    }
}
