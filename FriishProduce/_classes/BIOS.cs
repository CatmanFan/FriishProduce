using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FriishProduce
{
    public static class BIOS
    {
        public static readonly (Platform platform, List<string> BIOS)[] List = new (Platform platform, List<string> BIOS)[]
        {
            (
                Platform.NEO, new List<string>()
                {
                    "6293999bbc32e594aa0ae1da2113dc4d", // Uni-BIOS 1.0
                    "cafa6c274b271c769b8246c8f87473a1", // Uni-BIOS 1.1
                    "206fb0d9b5d01a0375d2d3ecab2401b1", // Uni-BIOS 1.2
                    "6b2f2d8507be4d1feb14fdfbab0bf22e", // Uni-BIOS 1.2o
                    "856d122ee5fc473d7d1dd99dbf42c25b", // Uni-BIOS 1.3
                    "1b9724d1b9d41a1a9b733007b2033fb5", // Uni-BIOS 2.0
                    "0377c32f69a28f23d9281c448aafb391", // Uni-BIOS 2.1
                    "5b9079a81d84137d8b6f221659d777c5", // Uni-BIOS 2.2
                    "74c4bb6a945f7284350036b40f0a0d9d", // Uni-BIOS 2.3
                    "d9f0ed2e0eeab813c9692d7e8d037fd8", // Uni-BIOS 2.3o
                    "727b731c1f4bd643094574ebaa3814b4", // Uni-BIOS 3.0
                    "dd77a172853200bd99c986a48dde914d", // Uni-BIOS 3.1
                    "274a7b72b7fd490f1fa71ce32371a93d", // Uni-BIOS 3.2
                    "bfc6563dde345d68da2623dc7f0f12d3", // Uni-BIOS 3.3
                    "4f0aeda8d2d145f596826b62d563c4ef", // Uni-BIOS 4.0
                }
            ),

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
            )
        };

        /// <summary>
        /// Checks MD5 of a BIOS file if it matches any of the ones supported in the list. If the file in question is a ZIP archive, it will check the compressed contents for any BIOS files.
        /// </summary>
        /// <param name="file">File path.</param>
        /// <param name="index">Console type.</param>
        public static bool Verify(string file, Platform index)
        {
            if (Path.GetExtension(file).ToLower() == ".zip")
            {
                bool verified = false;

                using (var zip = SharpCompress.Archives.Zip.ZipArchive.Open(file))
                {
                    foreach (var entry in zip.Entries.Where(x => !x.IsDirectory))
                    {
                        for (int i = 0; i < List.Length; i++)
                        {
                            if (List[i].platform == index)
                            {
                                verified = Verify(Zip.Extract(entry), i);
                                if (verified) return verified;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].platform == index) return File.Exists(file) && Verify(File.ReadAllBytes(file), i);
            }

            System.Media.SystemSounds.Beep.Play();
            // MessageBox.Show(Program.Lang.Msg(2), MessageBox.Buttons.Ok, MessageBox.Icons.Warning);
            return false;
        }

        /// <summary>
        /// Checks MD5 of a BIOS file if it matches any of the ones supported in the list.
        /// </summary>
        /// <param name="file">Byte array.</param>
        /// <param name="index">Index corresponding to the console.</param>
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
