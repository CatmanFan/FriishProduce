using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FriishProduce.Injectors
{
    public class MSX
    {
        public string ROM { get; set; }

        /// <summary>
        /// Replaces ROM within extracted content5 directory. ROM type is automatically determined.
        /// </summary>
        public void ReplaceROM()
        {
            // Max ROM limit: 512 KB
            if (File.ReadAllBytes(ROM).Length > 524288)
                throw new Exception(Program.Language.Get("m018"));

            string rom = Paths.WorkingFolder_Content5 + $"SLOT1.ROM"; // MSX1 ROM filepath

            if (File.Exists(rom))
            {
                File.Copy(ROM, rom, true);
                return;
            }

            else
            {
                rom = Paths.WorkingFolder_Content5 + $"MEGAROM.ROM"; // MSX2 ROM filepath

                if (File.Exists(rom))
                {
                    File.Copy(ROM, rom, true);
                    return;
                }
                else
                    throw new Exception(Program.Language.Get("m010"));
            }
        }

        public string GetSaveFile() => File.Exists(Paths.WorkingFolder_Content5 + "banner.bin") ? Paths.WorkingFolder_Content5 + "banner.bin" : null;

        public bool ExtractSaveTPL(string banner, string out_file)
        {
            if (File.Exists(banner))
            {
                // TPL contents in banner.bin does not have TPL header, so it has to be manually added
                string header = "0020AF30000000090000000C000000540000000000000078000000000000009C00000000000000C000000000000000E40000000000000108000000000000012C0000000000000150000000000000017400000000004000C000000005000001A00000000000000000000000010000000100000000000000000030003000000005000061A00000000000000000000000010000000100000000000000000030003000000005000073A00000000000000000000000010000000100000000000000000030003000000005000085A00000000000000000000000010000000100000000000000000030003000000005000097A000000000000000000000000100000001000000000000000000300030000000050000A9A000000000000000000000000100000001000000000000000000300030000000050000BBA000000000000000000000000100000001000000000000000000300030000000050000CDA000000000000000000000000100000001000000000000000000300030000000050000DFA00000000000000000000000010000000100000000000000000000000000000000";
                byte[] contents = File.ReadAllBytes(banner);

                var tpl = new List<byte>();

                for (int i = 0; i < header.Length; i += 2)
                    tpl.Add(Convert.ToByte(header.Substring(i, 2), 16));
                tpl.AddRange(contents.Skip(160).Take(contents.Length - 160));

                File.WriteAllBytes(out_file, tpl.ToArray());
                return true;
            }
            return false;
        }

        internal void InsertSaveTitle(string banner, string title, string tpl = "")
        {
            byte[] contents = File.ReadAllBytes(banner);
            string[] lines = title.Replace(Environment.NewLine, "\n").Split(Environment.NewLine.ToCharArray());

            // Text addition format: UTF-16 (Big Endian)
            for (int i = 32; i < 96; i++)
            {
                try { contents[i] = Encoding.BigEndianUnicode.GetBytes(lines[0])[i - 32]; }
                catch { contents[i] = 0x00; }
            }

            for (int i = 96; i < 160; i++)
            {
                try { contents[i] = Encoding.BigEndianUnicode.GetBytes(lines[1])[i - 96]; }
                catch { contents[i] = 0x00; }
            }


            // TPL replacement
            if (File.Exists(tpl))
            {
                var inputTPL_bytes = File.ReadAllBytes(tpl);
                for (int i = 0; i < contents.Length - 160; i++)
                    contents[i + 160] = inputTPL_bytes[i + 416];
                File.Delete(tpl);
            }

            File.WriteAllBytes(banner, contents);
        }
    }
}