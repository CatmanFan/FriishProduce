using System;
using System.IO;

namespace FriishProduce.Injectors
{
    public class SNES
    {
        public string ROM { get; set; }
        public string ROMcode { get; set; }

        public void ReplaceROM()
        {
            string path = Paths.WorkingFolder_Content5 + ROMcode;
            if (!File.Exists(path + ".rom"))
                throw new Exception("Unable to find injectable ROM.");

            if (File.ReadAllBytes(ROM).Length > File.ReadAllBytes(path + ".rom").Length)
                throw new Exception(strings.error_ROMtoobig);

            File.Copy(ROM, path + ".rom", true);
            if (File.Exists(path + ".pcm")) File.WriteAllText(path + ".pcm", String.Empty);
            if (File.Exists(path + ".var")) File.WriteAllText(path + ".var", String.Empty);
            // Jxxx.pcm is the digital audio file. It is not usually needed in most cases
        }

        /// <summary>
        /// Sets the WAD title ID and replaces regional indicators where possible
        /// </summary>
        public void ProduceID(string WAD_ID)
        {
            StringWriter s = new StringWriter();
            s.Write(WAD_ID[0]);
            s.Write(WAD_ID[1]);
            s.Write(WAD_ID[2]);

            if (WAD_ID.EndsWith("N") || WAD_ID.EndsWith("L")) s.Write('J');
            else if (WAD_ID.EndsWith("M")) s.Write('E');
            else s.Write(WAD_ID[3]);

            ROMcode = s.ToString();
        }
    }
}
