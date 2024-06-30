using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace FriishProduce
{
    public static class BIOS
    {
        public static readonly (Platform platform, List<string> BIOS)[] List = new (Platform platform, List<string> BIOS)[]
        {
            (
                Platform.GB, new List<string>()
                {
                    "32fbbd84168d3482956eb3c5051637f5"
                }
            ),

            (
                Platform.GBC, new List<string>()
                {
                    "dbfce9db9deaa2567f6a84fde55f9680"
                }
            ),

            (
                Platform.GBA, new List<string>()
                {
                    "a860e8c0b6d573d191e4ec7db1b1e4f6"
                }
            ),

            (
                Platform.PSX, new List<string>()
                {
                    "c53ca5908936d412331790f4426c6c33", // PSP
                    "81bbe60ba7a3d1cea1d48c14cbcc647b", // PS3
                    "924e392ed05558ffdb115408c263dccf", // Version 2.0 05/07/95 A
                    "8dd7d5296a650fac7319bce665a6a53c", // Version 3.0 09/09/96 J
                    "490f666e1afb15b7362b406ed1cea246", // Version 3.0 11/18/96 A
                    "32736f17079d0b2b7024407c39bd3050", // Version 3.0 01/06/97 E
                    "1e68c231d0896b7eadcad1d7d8e76129", // Version 4.1 12/16/97 A
                    "6e3735ff4c7dc899ee98981385f6f3d0", // Version 4.4 03/24/00 A
                }
            ),
        };

        public static bool Verify(string file, Platform index)
        {
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].platform == index) return Verify(file, i);
            }

            MessageBox.Show(Program.Lang.Msg(2), MessageBox.Buttons.Ok, MessageBox.Icons.Warning);
            return false;
        }

        public static bool Verify(byte[] file, Platform index)
        {
            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].platform == index) return Verify(file, i);
            }

            MessageBox.Show(Program.Lang.Msg(2), MessageBox.Buttons.Ok, MessageBox.Icons.Warning);
            return false;
        }

        public static bool Verify(string file, int index = -1) => File.Exists(file) && Verify(File.ReadAllBytes(file), index);

        public static bool Verify(byte[] file, int index = -1)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = new MemoryStream(file))
                {
                    var hash = md5.ComputeHash(stream);
                    var MD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                    if (index < 0 || index >= List.Length)
                    {
                        foreach (var item in List)
                        {
                            foreach (var biosMD5 in item.BIOS)
                            {
                                if (MD5 == biosMD5.ToLower()) return true;
                            }
                        }
                    }

                    else
                    {
                        foreach (var biosMD5 in List[index].BIOS)
                        {
                            if (MD5 == biosMD5.ToLower()) return true;
                        }
                    }
                }
            }

            MessageBox.Show(Program.Lang.Msg(2), MessageBox.Buttons.Ok, MessageBox.Icons.Warning);
            return false;
        }

        public static Platform GetConsole(byte[] file)
        {
            if (Verify(file))
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = new MemoryStream(file))
                    {
                        var hash = md5.ComputeHash(stream);
                        var MD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                        foreach (var item in List)
                        {
                            foreach (var biosMD5 in item.BIOS)
                            {
                                if (MD5 == biosMD5.ToLower()) return item.platform;
                            }
                        }
                    }
                }
            }

            throw new InvalidDataException();
        }
    }
}
