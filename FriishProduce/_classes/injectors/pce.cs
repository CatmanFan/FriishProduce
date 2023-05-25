using System;
using System.IO;

namespace FriishProduce.Injectors
{
    class PCE
    {
        public string ROM { get; set; }
        public string ROMcode { get; set; }

        public void ReplaceROM()
        {
            string rom = null;
            foreach (string file in Directory.GetFiles(Paths.WorkingFolder_Content5))
                if (Path.GetExtension(file).ToLower() == ".pce") rom = file;
            
            if (rom == null) throw new Exception(Program.Language.Get("m010"));

            if (File.ReadAllBytes(ROM).Length > File.ReadAllBytes(rom).Length)
                throw new Exception(Program.Language.Get("m004"));

            File.Copy(ROM, rom, true);
        }

        internal void InsertSaveTitle(string title)
        {
            string[] file = File.ReadAllLines(Paths.WorkingFolder_Content5 + "TITLE.TXT");
            file[0] = title;

            using (Stream s = new FileStream(Paths.WorkingFolder_Content5 + "TITLE.TXT", FileMode.Create))
            using (TextWriter t = new StreamWriter(s, System.Text.Encoding.BigEndianUnicode))
            {
                for (int i = 0; i < file.Length; i++)
                    t.WriteLine(file[i]);
            }
        }
    }
}
