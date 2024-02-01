// -------------------------------------------------------------------------------------------------------------------------------------
// DISCLAIMER:
// -------------------------------------------------------------------------------------------------------------------------------------
// The following class code is copied from Wii.cs Tools/ShowMiiWads and can be viewed in its original form using the link below:
// -------------------------------------------------------------------------------------------------------------------------------------
// https://github.com/dnasdw/showmiiwads/blob/Wii.cs_Tools/U8Mii/Wii.cs
// -------------------------------------------------------------------------------------------------------------------------------------

/* This file is part of ShowMiiWads
 * Copyright (C) 2009 Leathl
 * 
 * ShowMiiWads is free software: you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ShowMiiWads is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty
 * of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

//Wii.py by Xuzz, SquidMan, megazig, Matt_P, Omega and The Lemon Man was the base for TPL conversion
//Zetsubou by SquidMan was a reference for TPL conversion
//gbalzss by Andre Perrot was the base for LZ77 (de-)compression
//Thanks to the authors!

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Wii
{
    public class Tools
    {
        public static event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public static void ChangeProgress(int ProgressPercent)
        {
            EventHandler<ProgressChangedEventArgs> progressChanged = ProgressChanged;
            if (progressChanged != null)
            {
                progressChanged(new object(), new ProgressChangedEventArgs(ProgressPercent));
            }
        }

        /// <summary>
        /// Writes the small Byte Array into the big one at the given offset
        /// </summary>
        /// <param name="big"></param>
        /// <param name="small"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] InsertByteArray(byte[] big, byte[] small, int offset)
        {
            for (int i = 0; i < small.Length; i++)
                big[offset + i] = small[i];
            return big;
        }

        /// <summary>
        /// Returns the current UTC Unix Timestamp as a Byte Array
        /// </summary>
        /// <returns></returns>
        public static byte[] GetTimestamp()
        {
            DateTime dtNow = DateTime.UtcNow;
            TimeSpan tsTimestamp = (dtNow - new DateTime(1970, 1, 1, 0, 0, 0));

            int timestamp = (int)tsTimestamp.TotalSeconds;
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] timestampBytes = enc.GetBytes("CMiiUT" + timestamp.ToString());
            return timestampBytes;
        }

        /// <summary>
        /// Creates a new Byte Array out of the given one
        /// from the given offset with the specified length
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetPartOfByteArray(byte[] array, int offset, int length)
        {
            byte[] ret = new byte[length];
            for (int i = 0; i < length; i++)
                ret[i] = array[offset + i];
            return ret;
        }

        /// <summary>
        /// Converts UInt16 Array into Byte Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] UIntArrayToByteArray(UInt16[] array)
        {
            List<byte> results = new List<byte>();
            foreach (UInt16 value in array)
            {
                byte[] converted = BitConverter.GetBytes(value);
                results.AddRange(converted);
            }
            return results.ToArray();
        }

        /// <summary>
        /// Converts UInt16 Array into Byte Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] UIntArrayToByteArray(uint[] array)
        {
            List<byte> results = new List<byte>();
            foreach (uint value in array)
            {
                byte[] converted = BitConverter.GetBytes(value);
                results.AddRange(converted);
            }
            return results.ToArray();
        }

        /// <summary>
        /// Converts Byte Array into UInt16 Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static UInt32[] ByteArrayToUInt32Array(byte[] array)
        {
            UInt32[] converted = new UInt32[array.Length / 2];
            int j = 0;
            for (int i = 0; i < array.Length; i += 4)
            {
                converted[j] = BitConverter.ToUInt32(array, i);
                j++;
            }
            return converted;
        }

        /// <summary>
        /// Converts Byte Array into UInt16 Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static UInt16[] ByteArrayToUInt16Array(byte[] array)
        {
            UInt16[] converted = new UInt16[array.Length / 2];
            int j = 0;
            for (int i = 0; i < array.Length; i += 2)
            {
                converted[j] = BitConverter.ToUInt16(array, i);
                j++;
            }
            return converted;
        }

        /// <summary>
        /// Returns the file length as a Byte Array
        /// </summary>
        /// <param name="filelength"></param>
        /// <returns></returns>
        public static byte[] FileLengthToByteArray(int filelength)
        {
            byte[] length = BitConverter.GetBytes(filelength);
            Array.Reverse(length);
            return length;
        }

        /// <summary>
        /// Adds a padding to the next 64 bytes, if necessary
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int AddPadding(int value)
        {
            return AddPadding(value, 64);
        }

        /// <summary>
        /// Adds a padding to the given value, if necessary
        /// </summary>
        /// <param name="value"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static int AddPadding(int value, int padding)
        {
            if (value % padding != 0)
            {
                value = value + (padding - (value % padding));
            }

            return value;
        }

        /// <summary>
        /// Converts a Hex-String to Int
        /// </summary>
        /// <param name="hexstring"></param>
        /// <returns></returns>
        public static int HexStringToInt(string hexstring)
        {
            try { return int.Parse(hexstring, System.Globalization.NumberStyles.HexNumber); }
            catch { throw new Exception("An Error occured, maybe the Wad file is corrupt!"); }
        }

        /// <summary>
        /// Converts a Hex-String to Long
        /// </summary>
        /// <param name="hexstring"></param>
        /// <returns></returns>
        public static long HexStringToLong(string hexstring)
        {
            try { return long.Parse(hexstring, System.Globalization.NumberStyles.HexNumber); }
            catch { throw new Exception("An Error occured, maybe the Wad file is corrupt!"); }
        }

        /// <summary>
        /// Writes a Byte Array to a file
        /// </summary>
        /// <param name="file"></param>
        public static void SaveFileFromByteArray(byte[] file, string destination)
        {
            using (FileStream fs = new FileStream(destination, FileMode.Create))
                fs.Write(file, 0, file.Length);
        }

        /// <summary>
        /// Loads a file into a Byte Array
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <returns></returns>
        public static byte[] LoadFileToByteArray(string sourcefile)
        {
            if (File.Exists(sourcefile))
            {
                using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                {
                    byte[] filearray = new byte[fs.Length];
                    fs.Read(filearray, 0, filearray.Length);
                    return filearray;
                }
            }
            else throw new FileNotFoundException("File couldn't be found:\r\n" + sourcefile);
        }

        /// <summary>
        /// Loads a file into a Byte Array
        /// </summary>
        /// <param name="sourcefile"></param>
        /// <returns></returns>
        public static byte[] LoadFileToByteArray(string sourcefile, int offset, int length)
        {
            if (File.Exists(sourcefile))
            {
                using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                {
                    if (fs.Length < length) length = (int)fs.Length;
                    byte[] filearray = new byte[length];
                    fs.Seek(offset, SeekOrigin.Begin);
                    fs.Read(filearray, 0, length);
                    return filearray;
                }
            }
            else throw new FileNotFoundException("File couldn't be found:\r\n" + sourcefile);
        }

        /// <summary>
        /// Checks the SHA1 of the Common-Key
        /// </summary>
        /// <param name="pathtocommonkey"></param>
        /// <returns></returns>
        public static bool CheckCommonKey(string pathtocommonkey)
        {
            byte[] sum = new byte[] { 0xEB, 0xEA, 0xE6, 0xD2, 0x76, 0x2D, 0x4D, 0x3E, 0xA1, 0x60, 0xA6, 0xD8, 0x32, 0x7F, 0xAC, 0x9A, 0x25, 0xF8, 0x06, 0x2B };

            FileInfo fi = new FileInfo(pathtocommonkey);
            if (fi.Length != 16) return false;
            else
            {
                byte[] ckey = LoadFileToByteArray(pathtocommonkey);

                SHA1Managed sha1 = new SHA1Managed();
                byte[] newsum = sha1.ComputeHash(ckey);

                if (CompareByteArrays(sum, newsum) == true) return true;
                else return false;
            }
        }

        /// <summary>
        /// Creates the Common Key
        /// </summary>
        /// <param name="fat">Must be "45e"</param>
        /// <param name="destination">Destination Path</param>
        public static void CreateCommonKey(string fat, string destinationpath)
        {
            //What an effort, lol
            byte[] encryptedwater = new byte[] { 0x4d, 0x89, 0x21, 0x34, 0x62, 0x81, 0xe4, 0x02, 0x37, 0x36, 0xc4, 0xb4, 0xde, 0x40, 0x32, 0xab };
            byte[] key = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, byte.Parse(fat.Remove(2), System.Globalization.NumberStyles.HexNumber), byte.Parse(fat.Remove(0, 2) + "0", System.Globalization.NumberStyles.HexNumber) };
            byte[] decryptedwater = new byte[10];

            RijndaelManaged decryptkey = new RijndaelManaged();
            decryptkey.Mode = CipherMode.CBC;
            decryptkey.Padding = PaddingMode.None;
            decryptkey.KeySize = 128;
            decryptkey.BlockSize = 128;
            decryptkey.Key = key;
            Array.Reverse(key);
            decryptkey.IV = key;

            ICryptoTransform cryptor = decryptkey.CreateDecryptor();

            using (MemoryStream memory = new MemoryStream(encryptedwater))
            {
                using (CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read))
                    crypto.Read(decryptedwater, 0, 10);
            }

            string water = BitConverter.ToString(decryptedwater).Replace("-", "").ToLower() + " ";

            water = water.Insert(0, fat[2].ToString());
            water = water.Insert(2, fat[2].ToString());
            water = water.Insert(7, fat[2].ToString());
            water = water.Insert(11, fat[2].ToString());

            water = water.Insert(7, fat[1].ToString());
            water = water.Insert(10, fat[1].ToString());
            water = water.Insert(18, fat[1].ToString());
            water = water.Insert(19, fat[1].ToString());

            water = water.Insert(3, fat[0].ToString());
            water = water.Insert(15, fat[0].ToString());
            water = water.Insert(16, fat[0].ToString());
            water = water.Insert(22, fat[0].ToString());

            byte[] cheese = new byte[16];
            int count = -1;

            for (int i = 0; i < 32; i += 2)
                cheese[++count] = byte.Parse(water.Remove(0, i).Remove(2), System.Globalization.NumberStyles.HexNumber);

            if (destinationpath[destinationpath.Length - 1] != '\\') destinationpath = destinationpath + "\\";
            using (FileStream keystream = new FileStream(destinationpath + "\\common-key.bin", FileMode.Create))
            {
                keystream.Write(cheese, 0, cheese.Length);
            }
        }

        /// <summary>
        /// Counts the appearance of a specific character in a string
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theChar"></param>
        /// <returns></returns>
        public static int CountCharsInString(string theString, char theChar)
        {
            int count = 0;
            foreach (char thisChar in theString)
            {
                if (thisChar == theChar)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Compares two Byte Arrays and returns true, if they match
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CompareByteArrays(byte[] first, byte[] second)
        {
            if (first.Length != second.Length) return false;
            else
            {
                for (int i = 0; i < first.Length; i++)
                    if (first[i] != second[i]) return false;

                return true;
            }
        }

        /// <summary>
        /// Converts a Hex String to a Byte Array
        /// </summary>
        /// <param name="hexstring"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hexstring)
        {
            byte[] ba = new byte[hexstring.Length / 2];

            for (int i = 0; i < hexstring.Length / 2; i++)
            {
                ba[i] = byte.Parse(hexstring.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return ba;
        }

        /// <summary>
        /// Checks, if the given string does exist in the string Array
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theStringArray"></param>
        /// <returns></returns>
        public static bool StringExistsInStringArray(string theString, string[] theStringArray)
        {
            return Array.Exists(theStringArray, thisString => thisString == theString);
        }

        /// <summary>
        /// Copies an entire Directoy
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyDirectory(string source, string destination)
        {
            string[] subdirs = Directory.GetDirectories(source);
            string[] files = Directory.GetFiles(source);

            foreach (string thisFile in files)
            {
                if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);
                if (File.Exists(destination + "\\" + Path.GetFileName(thisFile))) File.Delete(destination + "\\" + Path.GetFileName(thisFile));
                File.Copy(thisFile, destination + "\\" + Path.GetFileName(thisFile));
            }

            foreach (string thisDir in subdirs)
            {
                CopyDirectory(thisDir, destination + "\\" + thisDir.Remove(0, thisDir.LastIndexOf('\\') + 1));
            }
        }
    }

    public class WadInfo
    {
        public const int Headersize = 64;
        public static string[] RegionCode = new string[4] { "Japan", "USA", "Europe", "Region Free" };

        /// <summary>
        /// Returns the Header of a Wadfile
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static byte[] GetHeader(byte[] wadfile)
        {
            byte[] Header = new byte[0x20];

            for (int i = 0; i < Header.Length; i++)
            {
                Header[i] = wadfile[i];
            }

            return Header;
        }

        /// <summary>
        /// Returns the size of the Certificate
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetCertSize(byte[] wadfile)
        {
            int size = int.Parse(wadfile[0x08].ToString("x2") + wadfile[0x09].ToString("x2") + wadfile[0x0a].ToString("x2") + wadfile[0x0b].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            return size;
        }

        /// <summary>
        /// Returns the size of the Ticket
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetTikSize(byte[] wadfile)
        {
            int size = int.Parse(wadfile[0x10].ToString("x2") + wadfile[0x11].ToString("x2") + wadfile[0x12].ToString("x2") + wadfile[0x13].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            return size;
        }

        /// <summary>
        /// Returns the size of the TMD
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetTmdSize(byte[] wadfile)
        {
            int size = int.Parse(wadfile[0x14].ToString("x2") + wadfile[0x15].ToString("x2") + wadfile[0x16].ToString("x2") + wadfile[0x17].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            return size;
        }

        /// <summary>
        /// Returns the size of all Contents
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetContentSize(byte[] wadfile)
        {
            int size = int.Parse(wadfile[0x18].ToString("x2") + wadfile[0x19].ToString("x2") + wadfile[0x1a].ToString("x2") + wadfile[0x1b].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            return size;
        }

        /// <summary>
        /// Returns the size of the Footer
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetFooterSize(byte[] wadfile)
        {
            int size = int.Parse(wadfile[0x1c].ToString("x2") + wadfile[0x1d].ToString("x2") + wadfile[0x1e].ToString("x2") + wadfile[0x1f].ToString("x2"), System.Globalization.NumberStyles.HexNumber);
            return size;
        }

        /// <summary>
        /// Returns the position of the tmd in the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetTmdPos(byte[] wadfile)
        {
            return Headersize + Tools.AddPadding(GetCertSize(wadfile)) + Tools.AddPadding(GetTikSize(wadfile));
        }

        /// <summary>
        /// Returns the position of the ticket in the wad file, ticket or tmd
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetTikPos(byte[] wadfile)
        {
            return Headersize + Tools.AddPadding(GetCertSize(wadfile));
        }

        /// <summary>
        /// Returns the title ID of the wad file.
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static string GetTitleID(string wadtiktmd, int type)
        {
            byte[] temp = Tools.LoadFileToByteArray(wadtiktmd);
            return GetTitleID(temp, type);
        }

        /// <summary>
        /// Returns the title ID of the wad file.
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static string GetTitleID(byte[] wadtiktmd, int type)
        {
            string channeltype = GetChannelType(wadtiktmd, type);
            int tikpos = 0;
            int tmdpos = 0;

            if (IsThisWad(wadtiktmd) == true)
            {
                //It's a wad
                tikpos = GetTikPos(wadtiktmd);
                tmdpos = GetTmdPos(wadtiktmd);
            }

            if (type == 1)
            {
                if (!channeltype.Contains("System:"))
                {
                    string tmdid = Convert.ToChar(wadtiktmd[tmdpos + 0x190]).ToString() + Convert.ToChar(wadtiktmd[tmdpos + 0x191]).ToString() + Convert.ToChar(wadtiktmd[tmdpos + 0x192]).ToString() + Convert.ToChar(wadtiktmd[tmdpos + 0x193]).ToString();
                    return tmdid;
                }
                else if (channeltype.Contains("IOS"))
                {
                    int tmdid = Tools.HexStringToInt(wadtiktmd[tmdpos + 0x190].ToString("x2") + wadtiktmd[tmdpos + 0x191].ToString("x2") + wadtiktmd[tmdpos + 0x192].ToString("x2") + wadtiktmd[tmdpos + 0x193].ToString("x2"));
                    return "IOS" + tmdid;
                }
                else if (channeltype.Contains("System")) return "SYSTEM";
                else return "";
            }
            else
            {
                if (!channeltype.Contains("System:"))
                {
                    string tikid = Convert.ToChar(wadtiktmd[tikpos + 0x1e0]).ToString() + Convert.ToChar(wadtiktmd[tikpos + 0x1e1]).ToString() + Convert.ToChar(wadtiktmd[tikpos + 0x1e2]).ToString() + Convert.ToChar(wadtiktmd[tikpos + 0x1e3]).ToString();
                    return tikid;
                }
                else if (channeltype.Contains("IOS"))
                {
                    int tikid = Tools.HexStringToInt(wadtiktmd[tikpos + 0x1e0].ToString("x2") + wadtiktmd[tikpos + 0x1e1].ToString("x2") + wadtiktmd[tikpos + 0x1e2].ToString("x2") + wadtiktmd[tikpos + 0x1e3].ToString("x2"));
                    return "IOS" + tikid;
                }
                else if (channeltype.Contains("System")) return "SYSTEM";
                else return "";
            }
        }

        /// <summary>
        /// Returns the full title ID of the wad file as a hex string.
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static string GetFullTitleID(byte[] wadtiktmd, int type)
        {
            int tikpos = 0;
            int tmdpos = 0;

            if (IsThisWad(wadtiktmd) == true)
            {
                //It's a wad
                tikpos = GetTikPos(wadtiktmd);
                tmdpos = GetTmdPos(wadtiktmd);
            }

            if (type == 1)
            {
                string tmdid = wadtiktmd[tmdpos + 0x18c].ToString("x2") +
                    wadtiktmd[tmdpos + 0x18d].ToString("x2") +
                    wadtiktmd[tmdpos + 0x18e].ToString("x2") +
                    wadtiktmd[tmdpos + 0x18f].ToString("x2") +
                    wadtiktmd[tmdpos + 0x190].ToString("x2") +
                    wadtiktmd[tmdpos + 0x191].ToString("x2") +
                    wadtiktmd[tmdpos + 0x192].ToString("x2") +
                    wadtiktmd[tmdpos + 0x193].ToString("x2");
                return tmdid;
            }
            else
            {
                string tikid = wadtiktmd[tikpos + 0x1dc].ToString() +
                    wadtiktmd[tikpos + 0x1dd].ToString() +
                    wadtiktmd[tikpos + 0x1de].ToString() +
                    wadtiktmd[tikpos + 0x1df].ToString() +
                    wadtiktmd[tikpos + 0x1e0].ToString() +
                    wadtiktmd[tikpos + 0x1e1].ToString() +
                    wadtiktmd[tikpos + 0x1e2].ToString() +
                    wadtiktmd[tikpos + 0x1e3].ToString();
                return tikid;
            }
        }

        /// <summary>
        /// Returns the title for each language of a wad file.
        /// Order: Jap, Eng, Ger, Fra, Spa, Ita, Dut
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string[] GetChannelTitles(string wadfile)
        {
            byte[] wadarray = Tools.LoadFileToByteArray(wadfile);
            return GetChannelTitles(wadarray);
        }

        /// <summary>
        /// Returns the title for each language of a wad file.
        /// Order: Jap, Eng, Ger, Fra, Spa, Ita, Dut
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string[] GetChannelTitles(byte[] wadfile)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\common-key.bin") || File.Exists(System.Windows.Forms.Application.StartupPath + "\\key.bin"))
            {
                string channeltype = GetChannelType(wadfile, 0);

                if (!channeltype.Contains("System:"))
                {
                    if (!channeltype.Contains("Hidden"))
                    {
                        string[] titles = new string[7];

                        string[,] conts = GetContentInfo(wadfile);
                        byte[] titlekey = GetTitleKey(wadfile);
                        int nullapp = 0;

                        for (int i = 0; i < conts.GetLength(0); i++)
                        {
                            if (conts[i, 1] == "00000000")
                                nullapp = i;
                        }

                        byte[] contenthandle = WadEdit.DecryptContent(wadfile, nullapp, titlekey);
                        int imetpos = 0;

                        if (contenthandle.Length < 400) return new string[7];

                        if (!channeltype.Contains("Downloaded"))
                        {
                            for (int z = 0; z < 400; z++)
                            {
                                if (Convert.ToChar(contenthandle[z]) == 'I')
                                    if (Convert.ToChar(contenthandle[z + 1]) == 'M')
                                        if (Convert.ToChar(contenthandle[z + 2]) == 'E')
                                            if (Convert.ToChar(contenthandle[z + 3]) == 'T')
                                            {
                                                imetpos = z;
                                                break;
                                            }
                            }

                            int jappos = imetpos + 29;
                            int count = 0;

                            for (int i = jappos; i < jappos + 588; i += 84)
                            {
                                for (int j = 0; j < 40; j += 2)
                                {
                                    if (contenthandle[i + j] != 0x00)
                                    {
                                        char temp = BitConverter.ToChar(new byte[] { contenthandle[i + j], contenthandle[i + j - 1] }, 0);
                                        titles[count] += temp;
                                    }
                                }

                                count++;
                            }

                            return titles;
                        }
                        else
                        {
                            //DLC's
                            for (int j = 97; j < 97 + 40; j += 2)
                            {
                                if (contenthandle[j] != 0x00)
                                {
                                    char temp = BitConverter.ToChar(new byte[] { contenthandle[j], contenthandle[j - 1] }, 0);
                                    titles[0] += temp;
                                }
                            }

                            for (int i = 1; i < 7; i++)
                                titles[i] = titles[0];

                            return titles;
                        }
                    }
                    else return new string[7];
                }
                else return new string[7];
            }
            else return new string[7];
        }

        /// <summary>
        /// Returns the title for each language of a 00.app file
        /// Order: Jap, Eng, Ger, Fra, Spa, Ita, Dut
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static string[] GetChannelTitlesFromApp(string app)
        {
            byte[] tmp = Tools.LoadFileToByteArray(app);
            return GetChannelTitlesFromApp(tmp);
        }

        /// <summary>
        /// Returns the title for each language of a 00.app file
        /// Order: Jap, Eng, Ger, Fra, Spa, Ita, Dut
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static string[] GetChannelTitlesFromApp(byte[] app)
        {
            string[] titles = new string[7];

            int imetpos = 0;
            int length = 400;

            if (app.Length < 400) length = app.Length - 4;

            for (int z = 0; z < length; z++)
            {
                if (Convert.ToChar(app[z]) == 'I')
                    if (Convert.ToChar(app[z + 1]) == 'M')
                        if (Convert.ToChar(app[z + 2]) == 'E')
                            if (Convert.ToChar(app[z + 3]) == 'T')
                            {
                                imetpos = z;
                                break;
                            }
            }

            if (imetpos != 0)
            {
                int jappos = imetpos + 29;
                int count = 0;

                for (int i = jappos; i < jappos + 588; i += 84)
                {
                    for (int j = 0; j < 40; j += 2)
                    {
                        if (app[i + j] != 0x00)
                        {
                            char temp = BitConverter.ToChar(new byte[] { app[i + j], app[i + j - 1] }, 0);
                            titles[count] += temp;
                        }
                    }

                    count++;
                }
            }

            return titles;
        }

        /// <summary>
        /// Returns the Type of the Channel as a string
        /// Wad or Tik needed for WiiWare / VC detection!
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetChannelType(byte[] wadtiktmd, int type)
        {
            int tikpos = 0;
            int tmdpos = 0;

            if (IsThisWad(wadtiktmd) == true)
            {
                //It's a wad
                tikpos = GetTikPos(wadtiktmd);
                tmdpos = GetTmdPos(wadtiktmd);
            }

            string thistype = "";

            if (type == 0)
            { thistype = wadtiktmd[tikpos + 0x1dc].ToString("x2") + wadtiktmd[tikpos + 0x1dd].ToString("x2") + wadtiktmd[tikpos + 0x1de].ToString("x2") + wadtiktmd[tikpos + 0x1df].ToString("x2"); }
            else { thistype = wadtiktmd[tmdpos + 0x18c].ToString("x2") + wadtiktmd[tmdpos + 0x18d].ToString("x2") + wadtiktmd[tmdpos + 0x18e].ToString("x2") + wadtiktmd[tmdpos + 0x18f].ToString("x2"); }
            string channeltype = "Unknown";

            if (thistype == "00010001")
            {
                channeltype = CheckWiiWareVC(wadtiktmd, type);
            }
            else if (thistype == "00010002") channeltype = "System Channel";
            else if (thistype == "00010004" || thistype == "00010000") channeltype = "Game Channel";
            else if (thistype == "00010005") channeltype = "Downloaded Content";
            else if (thistype == "00010008") channeltype = "Hidden Channel";
            else if (thistype == "00000001")
            {
                channeltype = "System: IOS";

                string thisid = "";
                if (type == 0) { thisid = wadtiktmd[tikpos + 0x1e0].ToString("x2") + wadtiktmd[tikpos + 0x1e1].ToString("x2") + wadtiktmd[tikpos + 0x1e2].ToString("x2") + wadtiktmd[tikpos + 0x1e3].ToString("x2"); }
                else { thisid = wadtiktmd[tmdpos + 0x190].ToString("x2") + wadtiktmd[tmdpos + 0x191].ToString("x2") + wadtiktmd[tmdpos + 0x192].ToString("x2") + wadtiktmd[tmdpos + 0x193].ToString("x2"); }

                if (thisid == "00000001") channeltype = "System: Boot2";
                else if (thisid == "00000002") channeltype = "System: Menu";
                else if (thisid == "00000100") channeltype = "System: BC";
                else if (thisid == "00000101") channeltype = "System: MIOS";
            }

            return channeltype;
        }

        /// <summary>
        /// Returns the amount of included Contents (app-files)
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetContentNum(byte[] wadtmd)
        {
            int tmdpos = 0;

            if (IsThisWad(wadtmd) == true)
            {
                //It's a wad file, so get the tmd position
                tmdpos = GetTmdPos(wadtmd);
            }

            int contents = Tools.HexStringToInt(wadtmd[tmdpos + 0x1de].ToString("x2") + wadtmd[tmdpos + 0x1df].ToString("x2"));

            return contents;
        }

        /// <summary>
        /// Returns the boot index specified in the tmd
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetBootIndex(byte[] wadtmd)
        {
            int tmdpos = 0;

            if (IsThisWad(wadtmd))
                tmdpos = GetTmdPos(wadtmd);

            int bootIndex = Tools.HexStringToInt(wadtmd[tmdpos + 0x1e0].ToString("x2") + wadtmd[tmdpos + 0x1e1].ToString("x2"));

            return bootIndex;
        }

        /// <summary>
        /// Returns the approx. destination size on the Wii
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetNandSize(byte[] wadtmd, bool ConvertToMB)
        {
            int tmdpos = 0;
            int minsize = 0;
            int maxsize = 0;
            int numcont = GetContentNum(wadtmd);

            if (IsThisWad(wadtmd) == true)
            {
                //It's a wad
                tmdpos = GetTmdPos(wadtmd);
            }

            for (int i = 0; i < numcont; i++)
            {
                int cont = 36 * i;
                int contentsize = Tools.HexStringToInt(wadtmd[tmdpos + 0x1e4 + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1e5 + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1e6 + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1e7 + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1e8 + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1e9 + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1ea + 8 + cont].ToString("x2") +
                    wadtmd[tmdpos + 0x1eb + 8 + cont].ToString("x2"));

                string type = wadtmd[tmdpos + 0x1e4 + 6 + cont].ToString("x2") + wadtmd[tmdpos + 0x1e5 + 6 + cont].ToString("x2");

                if (type == "0001")
                {
                    minsize += contentsize;
                    maxsize += contentsize;
                }
                else if (type == "8001")
                    maxsize += contentsize;
            }

            string size = "";

            if (maxsize == minsize) size = maxsize.ToString();
            else size = minsize.ToString() + " - " + maxsize.ToString();

            if (ConvertToMB == true)
            {
                if (size.Contains("-"))
                {
                    string min = size.Remove(size.IndexOf(' '));
                    string max = size.Remove(0, size.IndexOf('-') + 2);

                    min = Convert.ToString(Math.Round(Convert.ToDouble(min) * 0.0009765625 * 0.0009765625, 2));
                    max = Convert.ToString(Math.Round(Convert.ToDouble(max) * 0.0009765625 * 0.0009765625, 2));
                    if (min.Length > 4) { min = min.Remove(4); }
                    if (max.Length > 4) { max = max.Remove(4); }
                    size = min + " - " + max + " MB";
                }
                else
                {
                    size = Convert.ToString(Math.Round(Convert.ToDouble(size) * 0.0009765625 * 0.0009765625, 2));
                    if (size.Length > 4) { size = size.Remove(4); }
                    size = size + " MB";
                }
            }

            return size.Replace(",", ".");
        }

        /// <summary>
        /// Returns the approx. destination block on the Wii
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetNandBlocks(string wadtmd)
        {
            using (FileStream fs = new FileStream(wadtmd, FileMode.Open))
            {
                byte[] temp = new byte[fs.Length];
                fs.Read(temp, 0, temp.Length);
                return GetNandBlocks(temp);
            }
        }

        /// <summary>
        /// Returns the approx. destination block on the Wii
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetNandBlocks(byte[] wadtmd)
        {
            string size = GetNandSize(wadtmd, false);

            if (size.Contains("-"))
            {
                string size1 = size.Remove(size.IndexOf(' '));
                string size2 = size.Remove(0, size.LastIndexOf(' ') + 1);

                double blocks1 = (double)((Convert.ToDouble(size1) / 1024) / 128);
                double blocks2 = (double)((Convert.ToDouble(size2) / 1024) / 128);

                return Math.Ceiling(blocks1) + " - " + Math.Ceiling(blocks2);
            }
            else
            {
                double blocks = (double)((Convert.ToDouble(size) / 1024) / 128);

                return Math.Ceiling(blocks).ToString();
            }
        }

        /// <summary>
        /// Returns the title version of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetTitleVersion(string wadtmd)
        {
            byte[] temp = Tools.LoadFileToByteArray(wadtmd, 0, 10000);
            return GetTitleVersion(temp);
        }

        /// <summary>
        /// Returns the title version of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static int GetTitleVersion(byte[] wadtmd)
        {
            int tmdpos = 0;

            if (IsThisWad(wadtmd) == true) { tmdpos = GetTmdPos(wadtmd); }
            return Tools.HexStringToInt(wadtmd[tmdpos + 0x1dc].ToString("x2") + wadtmd[tmdpos + 0x1dd].ToString("x2"));
        }

        /// <summary>
        /// Returns the IOS that is needed by the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetIosFlag(byte[] wadtmd)
        {
            string type = GetChannelType(wadtmd, 1);

            if (!type.Contains("IOS") && !type.Contains("BC"))
            {
                int tmdpos = 0;
                if (IsThisWad(wadtmd) == true) { tmdpos = GetTmdPos(wadtmd); }
                return "IOS" + Tools.HexStringToInt(wadtmd[tmdpos + 0x188].ToString("x2") + wadtmd[tmdpos + 0x189].ToString("x2") + wadtmd[tmdpos + 0x18a].ToString("x2") + wadtmd[tmdpos + 0x18b].ToString("x2"));
            }
            else return "";
        }

        /// <summary>
        /// Returns the region of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetRegionFlag(byte[] wadtmd)
        {
            int tmdpos = 0;
            string channeltype = GetChannelType(wadtmd, 1);

            if (IsThisWad(wadtmd) == true) { tmdpos = GetTmdPos(wadtmd); }

            if (!channeltype.Contains("System:"))
            {
                int region = Tools.HexStringToInt(wadtmd[tmdpos + 0x19d].ToString("x2"));
                return RegionCode[region];
            }
            else return "";
        }

        /// <summary>
        /// Returns the Path where the wad will be installed on the Wii
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string GetNandPath(string wadfile)
        {
            byte[] wad = Tools.LoadFileToByteArray(wadfile);
            return GetNandPath(wad, 0);
        }

        /// <summary>
        /// Returns the Path where the wad will be installed on the Wii
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static string GetNandPath(byte[] wadtiktmd, int type)
        {
            int tikpos = 0;
            int tmdpos = 0;

            if (IsThisWad(wadtiktmd) == true)
            {
                tikpos = GetTikPos(wadtiktmd);
                tmdpos = GetTmdPos(wadtiktmd);
            }

            string thispath = "";

            if (type == 0)
            {
                thispath = wadtiktmd[tikpos + 0x1dc].ToString("x2") +
                    wadtiktmd[tikpos + 0x1dd].ToString("x2") +
                    wadtiktmd[tikpos + 0x1de].ToString("x2") +
                    wadtiktmd[tikpos + 0x1df].ToString("x2") +
                    wadtiktmd[tikpos + 0x1e0].ToString("x2") +
                    wadtiktmd[tikpos + 0x1e1].ToString("x2") +
                    wadtiktmd[tikpos + 0x1e2].ToString("x2") +
                    wadtiktmd[tikpos + 0x1e3].ToString("x2");
            }
            else
            {
                thispath = wadtiktmd[tmdpos + 0x18c].ToString("x2") +
                    wadtiktmd[tmdpos + 0x18d].ToString("x2") +
                    wadtiktmd[tmdpos + 0x18e].ToString("x2") +
                    wadtiktmd[tmdpos + 0x18f].ToString("x2") +
                    wadtiktmd[tmdpos + 0x190].ToString("x2") +
                    wadtiktmd[tmdpos + 0x191].ToString("x2") +
                    wadtiktmd[tmdpos + 0x192].ToString("x2") +
                    wadtiktmd[tmdpos + 0x193].ToString("x2");
            }

            thispath = thispath.Insert(8, "\\");
            return thispath;
        }

        /// <summary>
        /// Returns true, if the wad file is a WiiWare / VC title.
        /// </summary>
        /// <param name="wadtiktmd"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static string CheckWiiWareVC(byte[] wadtiktmd, int type)
        {
            int tiktmdpos = 0;
            int offset = 0x221;
            int idoffset = 0x1e0;

            if (type == 1) { offset = 0x197; idoffset = 0x190; }
            if (IsThisWad(wadtiktmd) == true)
            {
                if (type == 1) tiktmdpos = GetTmdPos(wadtiktmd);
                else tiktmdpos = GetTikPos(wadtiktmd);
            }

            if (wadtiktmd[tiktmdpos + offset] == 0x01)
            {
                char idchar = Convert.ToChar(wadtiktmd[tiktmdpos + idoffset]);
                char idchar2 = Convert.ToChar(wadtiktmd[tiktmdpos + idoffset + 1]);

                if (idchar == 'H') return "System Channel";
                else if (idchar == 'W') return "WiiWare";
                else
                {
                    if (idchar == 'C') return "C64";
                    else if (idchar == 'E' && idchar2 == 'A') return "NeoGeo";
                    else if (idchar == 'E') return "VC - Arcade";
                    else if (idchar == 'F') return "NES";
                    else if (idchar == 'J') return "SNES";
                    else if (idchar == 'L') return "Sega Master System";
                    else if (idchar == 'M') return "Sega Genesis";
                    else if (idchar == 'N') return "Nintendo 64";
                    else if (idchar == 'P') return "Turbografx";
                    else if (idchar == 'Q') return "Turbografx CD";
                    else return "Channel Title";
                }
            }
            else return "Channel Title";
        }

        /// <summary>
        /// Returns all information stored in the tmd for all contents in the wad file.
        /// [x, 0] = Content ID, [x, 1] = Index, [x, 2] = Type, [x, 3] = Size, [x, 4] = Sha1
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static string[,] GetContentInfo(byte[] wadtmd)
        {
            int tmdpos = 0;

            if (IsThisWad(wadtmd) == true) { tmdpos = GetTmdPos(wadtmd); }
            int contentcount = GetContentNum(wadtmd);
            string[,] contentinfo = new string[contentcount, 5];

            for (int i = 0; i < contentcount; i++)
            {
                contentinfo[i, 0] = wadtmd[tmdpos + 0x1e4 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1e5 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1e6 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1e7 + (36 * i)].ToString("x2");
                contentinfo[i, 1] = "0000" +
                    wadtmd[tmdpos + 0x1e8 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1e9 + (36 * i)].ToString("x2");
                contentinfo[i, 2] = wadtmd[tmdpos + 0x1ea + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1eb + (36 * i)].ToString("x2");
                contentinfo[i, 3] = Tools.HexStringToInt(
                    wadtmd[tmdpos + 0x1ec + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1ed + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1ee + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1ef + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1f0 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1f1 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1f2 + (36 * i)].ToString("x2") +
                    wadtmd[tmdpos + 0x1f3 + (36 * i)].ToString("x2")).ToString();

                for (int j = 0; j < 20; j++)
                {
                    contentinfo[i, 4] += wadtmd[tmdpos + 0x1f4 + (36 * i) + j].ToString("x2");
                }
            }

            return contentinfo;
        }

        /// <summary>
        /// Returns the Tik of the wad file as a Byte-Array
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static byte[] ReturnTik(byte[] wadfile)
        {
            int tikpos = GetTikPos(wadfile);
            int tiksize = GetTikSize(wadfile);

            byte[] tik = new byte[tiksize];

            for (int i = 0; i < tiksize; i++)
            {
                tik[i] = wadfile[tikpos + i];
            }

            return tik;
        }

        /// <summary>
        /// Returns the Tmd of the wad file as a Byte-Array
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static byte[] ReturnTmd(byte[] wadfile)
        {
            int tmdpos = GetTmdPos(wadfile);
            int tmdsize = GetTmdSize(wadfile);

            byte[] tmd = new byte[tmdsize];

            for (int i = 0; i < tmdsize; i++)
            {
                tmd[i] = wadfile[tmdpos + i];
            }

            return tmd;
        }

        /// <summary>
        /// Checks, if the given file is a wad
        /// </summary>
        /// <param name="wadtiktmd"></param>
        /// <returns></returns>
        public static bool IsThisWad(byte[] wadtiktmd)
        {
            if (wadtiktmd[0] == 0x00 &&
                wadtiktmd[1] == 0x00 &&
                wadtiktmd[2] == 0x00 &&
                wadtiktmd[3] == 0x20 &&
                wadtiktmd[4] == 0x49 &&
                wadtiktmd[5] == 0x73)
            { return true; }

            return false;
        }

        /// <summary>
        /// Returns the decrypted TitleKey
        /// </summary>
        /// <param name="wadtik"></param>
        /// <returns></returns>
        public static byte[] GetTitleKey(byte[] wadtik)
        {
            byte[] commonkey = new byte[16];

            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\common-key.bin"))
            { commonkey = Tools.LoadFileToByteArray(System.Windows.Forms.Application.StartupPath + "\\common-key.bin"); }
            else if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\key.bin"))
            { commonkey = Tools.LoadFileToByteArray(System.Windows.Forms.Application.StartupPath + "\\key.bin"); }
            else { throw new FileNotFoundException("The (common-)key.bin must be in the application directory!"); }

            byte[] encryptedkey = new byte[16];
            byte[] iv = new byte[16];
            int tikpos = 0;

            if (IsThisWad(wadtik) == true)
            {
                //It's a wad file, so get the tik position
                tikpos = GetTikPos(wadtik);
            }

            for (int i = 0; i < 16; i++)
            {
                encryptedkey[i] = wadtik[tikpos + 0x1bf + i];
            }

            for (int j = 0; j < 8; j++)
            {
                iv[j] = wadtik[tikpos + 0x1dc + j];
                iv[j + 8] = 0x00;
            }

            RijndaelManaged decrypt = new RijndaelManaged();
            decrypt.Mode = CipherMode.CBC;
            decrypt.Padding = PaddingMode.None;
            decrypt.KeySize = 128;
            decrypt.BlockSize = 128;
            decrypt.Key = commonkey;
            decrypt.IV = iv;

            ICryptoTransform cryptor = decrypt.CreateDecryptor();

            MemoryStream memory = new MemoryStream(encryptedkey);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            byte[] decryptedkey = new byte[16];
            crypto.Read(decryptedkey, 0, decryptedkey.Length);

            crypto.Close();
            memory.Close();

            return decryptedkey;
        }

        /// <summary>
        /// Decodes the Timestamp in the Trailer, if available.
        /// Returns null if no Timestamp was found.
        /// </summary>
        /// <param name="trailer"></param>
        /// <returns></returns>
        public static DateTime GetCreationTime(string trailer)
        {
            byte[] bTrailer = Tools.LoadFileToByteArray(trailer);
            return GetCreationTime(bTrailer);
        }

        /// <summary>
        /// Decodes the Timestamp in the Trailer, if available.
        /// Returns null if no Timestamp was found.
        /// </summary>
        /// <param name="trailer"></param>
        /// <returns></returns>
        public static DateTime GetCreationTime(byte[] trailer)
        {
            DateTime result = new DateTime(1970, 1, 1);

            if (trailer[0] == 'C' &&
                trailer[1] == 'M' &&
                trailer[2] == 'i' &&
                trailer[3] == 'i' &&
                trailer[4] == 'U' &&
                trailer[5] == 'T')
            {
                ASCIIEncoding enc = new ASCIIEncoding();
                string stringSeconds = enc.GetString(trailer, 6, 10);
                int seconds = 0;

                if (int.TryParse(stringSeconds, out seconds))
                {
                    result = result.AddSeconds((double)seconds);
                    return result;
                }
                else return result;
            }

            return result;
        }
    }

    public class WadEdit
    {
        /// <summary>
        /// Changes the region of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="region">0 = JAP, 1 = USA, 2 = EUR, 3 = FREE</param>
        /// <returns></returns>
        public static byte[] ChangeRegion(byte[] wadfile, int region)
        {

            int tmdpos = WadInfo.GetTmdPos(wadfile);

            if (region == 0) wadfile[tmdpos + 0x19d] = 0x00;
            else if (region == 1) wadfile[tmdpos + 0x19d] = 0x01;
            else if (region == 2) wadfile[tmdpos + 0x19d] = 0x02;
            else wadfile[tmdpos + 0x19d] = 0x03;

            wadfile = TruchaSign(wadfile, 1);

            return wadfile;
        }

        /// <summary>
        /// Changes the region of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="region"></param>
        public static void ChangeRegion(string wadfile, int region)
        {
            byte[] wadarray = Tools.LoadFileToByteArray(wadfile);
            wadarray = ChangeRegion(wadarray, region);

            using (FileStream fs = new FileStream(wadfile, FileMode.Open, FileAccess.Write))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(wadarray, 0, wadarray.Length);
            }
        }

        /// <summary>
        /// Changes the Channel Title of the wad file
        /// All languages have the same title
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static byte[] ChangeChannelTitle(byte[] wadfile, string title)
        {
            return ChangeChannelTitle(wadfile, title, title, title, title, title, title, title);
        }

        /// <summary>
        /// Changes the Channel Title of the wad file
        /// Each language has a specific title
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="jap"></param>
        /// <param name="eng"></param>
        /// <param name="ger"></param>
        /// <param name="fra"></param>
        /// <param name="spa"></param>
        /// <param name="ita"></param>
        /// <param name="dut"></param>
        public static void ChangeChannelTitle(string wadfile, string jap, string eng, string ger, string fra, string spa, string ita, string dut)
        {
            byte[] wadarray = Tools.LoadFileToByteArray(wadfile);
            wadarray = ChangeChannelTitle(wadarray, jap, eng, ger, fra, spa, ita, dut);

            using (FileStream fs = new FileStream(wadfile, FileMode.Open, FileAccess.Write))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(wadarray, 0, wadarray.Length);
            }
        }

        /// <summary>
        /// Changes the Channel Title of the wad file
        /// Each language has a specific title
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="jap">Japanese Title</param>
        /// <param name="eng">English Title</param>
        /// <param name="ger">German Title</param>
        /// <param name="fra">French Title</param>
        /// <param name="spa">Spanish Title</param>
        /// <param name="ita">Italian Title</param>
        /// <param name="dut">Dutch Title</param>
        /// <returns></returns>
        public static byte[] ChangeChannelTitle(byte[] wadfile, string jap, string eng, string ger, string fra, string spa, string ita, string dut)
        {
            Tools.ChangeProgress(0);

            char[] japchars = jap.ToCharArray();
            char[] engchars = eng.ToCharArray();
            char[] gerchars = ger.ToCharArray();
            char[] frachars = fra.ToCharArray();
            char[] spachars = spa.ToCharArray();
            char[] itachars = ita.ToCharArray();
            char[] dutchars = dut.ToCharArray();

            byte[] titlekey = WadInfo.GetTitleKey(wadfile);
            string[,] conts = WadInfo.GetContentInfo(wadfile);
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            int tmdsize = WadInfo.GetTmdSize(wadfile);
            int nullapp = 0;
            int contentpos = 64 + Tools.AddPadding(WadInfo.GetCertSize(wadfile)) + Tools.AddPadding(WadInfo.GetTikSize(wadfile)) + Tools.AddPadding(WadInfo.GetTmdSize(wadfile));
            SHA1Managed sha1 = new SHA1Managed();

            Tools.ChangeProgress(10);

            for (int i = 0; i < conts.GetLength(0); i++)
            {
                if (conts[i, 1] == "00000000")
                {
                    nullapp = i;
                    break;
                }
                else
                    contentpos += Tools.AddPadding(Convert.ToInt32(conts[i, 3]));
            }

            byte[] contenthandle = DecryptContent(wadfile, nullapp, titlekey);

            Tools.ChangeProgress(25);

            int imetpos = 0;

            for (int z = 0; z < 400; z++)
            {
                if (Convert.ToChar(contenthandle[z]) == 'I')
                    if (Convert.ToChar(contenthandle[z + 1]) == 'M')
                        if (Convert.ToChar(contenthandle[z + 2]) == 'E')
                            if (Convert.ToChar(contenthandle[z + 3]) == 'T')
                            {
                                imetpos = z;
                                break;
                            }
            }

            Tools.ChangeProgress(40);

            int count = 0;

            for (int x = imetpos; x < imetpos + 40; x += 2)
            {
                if (japchars.Length > count)
                {
                    contenthandle[x + 29] = BitConverter.GetBytes(japchars[count])[0];
                    contenthandle[x + 28] = BitConverter.GetBytes(japchars[count])[1];
                }
                else { contenthandle[x + 29] = 0x00; contenthandle[x + 28] = 0x00; }
                if (engchars.Length > count)
                {
                    contenthandle[x + 29 + 84] = BitConverter.GetBytes(engchars[count])[0];
                    contenthandle[x + 29 + 84 - 1] = BitConverter.GetBytes(engchars[count])[1];
                }
                else { contenthandle[x + 29 + 84] = 0x00; contenthandle[x + 29 + 84 - 1] = 0x00; }
                if (gerchars.Length > count)
                {
                    contenthandle[x + 29 + 84 * 2] = BitConverter.GetBytes(gerchars[count])[0];
                    contenthandle[x + 29 + 84 * 2 - 1] = BitConverter.GetBytes(gerchars[count])[1];
                }
                else { contenthandle[x + 29 + 84 * 2] = 0x00; contenthandle[x + 29 + 84 * 2 - 1] = 0x00; }
                if (frachars.Length > count)
                {
                    contenthandle[x + 29 + 84 * 3] = BitConverter.GetBytes(frachars[count])[0];
                    contenthandle[x + 29 + 84 * 3 - 1] = BitConverter.GetBytes(frachars[count])[1];
                }
                else { contenthandle[x + 29 + 84 * 3] = 0x00; contenthandle[x + 29 + 84 * 3 - 1] = 0x00; }
                if (spachars.Length > count)
                {
                    contenthandle[x + 29 + 84 * 4] = BitConverter.GetBytes(spachars[count])[0];
                    contenthandle[x + 29 + 84 * 4 - 1] = BitConverter.GetBytes(spachars[count])[1];
                }
                else { contenthandle[x + 29 + 84 * 4] = 0x00; contenthandle[x + 29 + 84 * 4 - 1] = 0x00; }
                if (itachars.Length > count)
                {
                    contenthandle[x + 29 + 84 * 5] = BitConverter.GetBytes(itachars[count])[0];
                    contenthandle[x + 29 + 84 * 5 - 1] = BitConverter.GetBytes(itachars[count])[1];
                }
                else { contenthandle[x + 29 + 84 * 5] = 0x00; contenthandle[x + 29 + 84 * 5 - 1] = 0x00; }
                if (dutchars.Length > count)
                {
                    contenthandle[x + 29 + 84 * 6] = BitConverter.GetBytes(dutchars[count])[0];
                    contenthandle[x + 29 + 84 * 6 - 1] = BitConverter.GetBytes(dutchars[count])[1];
                }
                else { contenthandle[x + 29 + 84 * 6] = 0x00; contenthandle[x + 29 + 84 * 6 - 1] = 0x00; }

                count++;
            }

            Tools.ChangeProgress(50);

            byte[] newmd5 = new byte[16];
            contenthandle = FixMD5InImet(contenthandle, out newmd5);
            byte[] newsha = sha1.ComputeHash(contenthandle);

            contenthandle = EncryptContent(contenthandle, WadInfo.ReturnTmd(wadfile), nullapp, titlekey, false);

            Tools.ChangeProgress(70);

            for (int y = 0; y < contenthandle.Length; y++)
            {
                wadfile[contentpos + y] = contenthandle[y];
            }

            //SHA1 in TMD
            byte[] tmd = Tools.GetPartOfByteArray(wadfile, tmdpos, tmdsize);
            for (int i = 0; i < 20; i++)
                tmd[0x1f4 + (36 * nullapp) + i] = newsha[i];
            TruchaSign(tmd, 1);
            wadfile = Tools.InsertByteArray(wadfile, tmd, tmdpos);

            int footer = WadInfo.GetFooterSize(wadfile);

            Tools.ChangeProgress(80);

            if (footer > 0)
            {
                int footerpos = wadfile.Length - footer;
                int imetposfoot = 0;

                for (int z = 0; z < 200; z++)
                {
                    if (Convert.ToChar(wadfile[footerpos + z]) == 'I')
                        if (Convert.ToChar(wadfile[footerpos + z + 1]) == 'M')
                            if (Convert.ToChar(wadfile[footerpos + z + 2]) == 'E')
                                if (Convert.ToChar(wadfile[footerpos + z + 3]) == 'T')
                                {
                                    imetposfoot = footerpos + z;
                                    break;
                                }
                }

                Tools.ChangeProgress(90);

                int count2 = 0;

                for (int x = imetposfoot; x < imetposfoot + 40; x += 2)
                {
                    if (japchars.Length > count2) { wadfile[x + 29] = Convert.ToByte(japchars[count2]); }
                    else { wadfile[x + 29] = 0x00; }
                    if (engchars.Length > count2) { wadfile[x + 29 + 84] = Convert.ToByte(engchars[count2]); }
                    else { wadfile[x + 29 + 84] = 0x00; }
                    if (gerchars.Length > count2) { wadfile[x + 29 + 84 * 2] = Convert.ToByte(gerchars[count2]); }
                    else { wadfile[x + 29 + 84 * 2] = 0x00; }
                    if (frachars.Length > count2) { wadfile[x + 29 + 84 * 3] = Convert.ToByte(frachars[count2]); }
                    else { wadfile[x + 29 + 84 * 3] = 0x00; }
                    if (spachars.Length > count2) { wadfile[x + 29 + 84 * 4] = Convert.ToByte(spachars[count2]); }
                    else { wadfile[x + 29 + 84 * 4] = 0x00; }
                    if (itachars.Length > count2) { wadfile[x + 29 + 84 * 5] = Convert.ToByte(itachars[count2]); }
                    else { wadfile[x + 29 + 84 * 5] = 0x00; }
                    if (dutchars.Length > count2) { wadfile[x + 29 + 84 * 6] = Convert.ToByte(dutchars[count2]); }
                    else { wadfile[x + 29 + 84 * 6] = 0x00; }

                    count2++;
                }

                for (int i = 0; i < 16; i++)
                    wadfile[imetposfoot + 1456 + i] = newmd5[i];
            }

            Tools.ChangeProgress(100);
            return wadfile;
        }

        /// <summary>
        /// Changes the Title ID in the Tik or Tmd file
        /// </summary>
        /// <param name="tiktmd"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static void ChangeTitleID(string tiktmdfile, int type, string titleid)
        {
            byte[] temp = Tools.LoadFileToByteArray(tiktmdfile);
            temp = ChangeTitleID(temp, type, titleid);
            Tools.SaveFileFromByteArray(temp, tiktmdfile);
        }

        /// <summary>
        /// Changes the Title ID in the Tik or Tmd file
        /// </summary>
        /// <param name="tiktmd"></param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static byte[] ChangeTitleID(byte[] tiktmd, int type, string titleid)
        {
            int offset = 0x1e0;
            if (type == 1) offset = 0x190;
            char[] id = titleid.ToCharArray();

            tiktmd[offset] = (byte)id[0];
            tiktmd[offset + 1] = (byte)id[1];
            tiktmd[offset + 2] = (byte)id[2];
            tiktmd[offset + 3] = (byte)id[3];

            tiktmd = TruchaSign(tiktmd, type);

            return tiktmd;
        }

        /// <summary>
        /// Changes the title ID of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="titleid"></param>
        /// <returns></returns>
        public static byte[] ChangeTitleID(byte[] wadfile, string titleid)
        {
            Tools.ChangeProgress(0);

            int tikpos = WadInfo.GetTikPos(wadfile);
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            char[] id = titleid.ToCharArray();

            byte[] oldtitlekey = WadInfo.GetTitleKey(wadfile);

            Tools.ChangeProgress(20);

            //Change the ID in the ticket
            wadfile[tikpos + 0x1e0] = (byte)id[0];
            wadfile[tikpos + 0x1e1] = (byte)id[1];
            wadfile[tikpos + 0x1e2] = (byte)id[2];
            wadfile[tikpos + 0x1e3] = (byte)id[3];

            //Change the ID in the tmd
            wadfile[tmdpos + 0x190] = (byte)id[0];
            wadfile[tmdpos + 0x191] = (byte)id[1];
            wadfile[tmdpos + 0x192] = (byte)id[2];
            wadfile[tmdpos + 0x193] = (byte)id[3];

            Tools.ChangeProgress(40);

            //Trucha-Sign both
            wadfile = TruchaSign(wadfile, 0);

            Tools.ChangeProgress(50);

            wadfile = TruchaSign(wadfile, 1);

            Tools.ChangeProgress(60);

            byte[] newtitlekey = WadInfo.GetTitleKey(wadfile);
            byte[] tmd = WadInfo.ReturnTmd(wadfile);

            int contentcount = WadInfo.GetContentNum(wadfile);

            wadfile = ReEncryptAllContents(wadfile, oldtitlekey, newtitlekey);

            Tools.ChangeProgress(100);
            return wadfile;
        }

        /// <summary>
        /// Changes the title ID of the wad file
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="titleid"></param>
        public static void ChangeTitleID(string wadfile, string titleid)
        {
            byte[] wadarray = Tools.LoadFileToByteArray(wadfile);
            wadarray = ChangeTitleID(wadarray, titleid);

            using (FileStream fs = new FileStream(wadfile, FileMode.Open, FileAccess.Write))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(wadarray, 0, wadarray.Length);
            }
        }

        /// <summary>
        /// Clears the Signature of the Tik / Tmd to 0x00
        /// </summary>
        /// <param name="wadtiktmd">Wad, Tik or Tmd</param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static byte[] ClearSignature(byte[] wadtiktmd, int type)
        {
            int tmdtikpos = 0;
            int tmdtiksize = wadtiktmd.Length; ;

            if (WadInfo.IsThisWad(wadtiktmd) == true)
            {
                //It's a wad file, so get the tik or tmd position and length
                switch (type)
                {
                    case 1:
                        tmdtikpos = WadInfo.GetTmdPos(wadtiktmd);
                        tmdtiksize = WadInfo.GetTmdSize(wadtiktmd);
                        break;
                    default:
                        tmdtikpos = WadInfo.GetTikPos(wadtiktmd);
                        tmdtiksize = WadInfo.GetTikSize(wadtiktmd);
                        break;
                }
            }

            for (int i = 4; i < 260; i++)
            {
                wadtiktmd[tmdtikpos + i] = 0x00;
            }

            return wadtiktmd;
        }

        /// <summary>
        /// Trucha-Signs the Tik or Tmd
        /// </summary>
        /// <param name="file">Wad or Tik or Tmd</param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static void TruchaSign(string file, int type)
        {
            byte[] temp = Tools.LoadFileToByteArray(file);
            temp = TruchaSign(temp, type);
            Tools.SaveFileFromByteArray(temp, file);
        }

        /// <summary>
        /// Trucha-Signs the Tik or Tmd
        /// </summary>
        /// <param name="wadortmd">Wad or Tik or Tmd</param>
        /// <param name="type">0 = Tik, 1 = Tmd</param>
        /// <returns></returns>
        public static byte[] TruchaSign(byte[] wadtiktmd, int type)
        {
            SHA1Managed sha = new SHA1Managed();
            int[] position = new int[2] { 0x1f1, 0x1d4 }; //0x104 0x1c1
            int[] tosign = new int[2] { 0x140, 0x140 }; //0x104 0x140
            int tiktmdpos = 0;
            int tiktmdsize = wadtiktmd.Length;

            if (sha.ComputeHash(wadtiktmd, tiktmdpos + tosign[type], tiktmdsize - tosign[type])[0] != 0x00)
            {
                ClearSignature(wadtiktmd, type);

                if (WadInfo.IsThisWad(wadtiktmd) == true)
                {
                    //It's a wad file
                    if (type == 0) //Get Tik position and size
                    {
                        tiktmdpos = WadInfo.GetTikPos(wadtiktmd);
                        tiktmdsize = WadInfo.GetTikSize(wadtiktmd);
                    }
                    else //Get Tmd position and size
                    {
                        tiktmdpos = WadInfo.GetTmdPos(wadtiktmd);
                        tiktmdsize = WadInfo.GetTmdSize(wadtiktmd);
                    }
                }

                byte[] sha1 = new byte[20];

                for (UInt16 i = 0; i < 65535; i++)
                {
                    byte[] hex = BitConverter.GetBytes(i);
                    wadtiktmd[tiktmdpos + position[type]] = hex[0];
                    wadtiktmd[tiktmdpos + position[type] + 1] = hex[1];

                    sha1 = sha.ComputeHash(wadtiktmd, tiktmdpos + tosign[type], tiktmdsize - tosign[type]);
                    if (sha1[0] == 0x00) break;
                }

                return wadtiktmd;
            }
            else return wadtiktmd;
        }

        /// <summary>
        /// Decrypts the given content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] DecryptContent(byte[] wadfile, int contentcount, byte[] titlekey)
        {
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            byte[] iv = new byte[16];
            string[,] continfo = WadInfo.GetContentInfo(wadfile);
            int contentsize = Convert.ToInt32(continfo[contentcount, 3]);
            int paddedsize = Tools.AddPadding(contentsize, 16);

            int contentpos = 64 + Tools.AddPadding(WadInfo.GetCertSize(wadfile)) + Tools.AddPadding(WadInfo.GetTikSize(wadfile)) + Tools.AddPadding(WadInfo.GetTmdSize(wadfile));

            for (int x = 0; x < contentcount; x++)
            {
                contentpos += Tools.AddPadding(Convert.ToInt32(continfo[x, 3]));
            }

            iv[0] = wadfile[tmdpos + 0x1e8 + (0x24 * contentcount)];
            iv[1] = wadfile[tmdpos + 0x1e9 + (0x24 * contentcount)];

            RijndaelManaged decrypt = new RijndaelManaged();
            decrypt.Mode = CipherMode.CBC;
            decrypt.Padding = PaddingMode.None;
            decrypt.KeySize = 128;
            decrypt.BlockSize = 128;
            decrypt.Key = titlekey;
            decrypt.IV = iv;

            ICryptoTransform cryptor = decrypt.CreateDecryptor();

            MemoryStream memory = new MemoryStream(wadfile, contentpos, paddedsize);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            bool fullread = false;
            byte[] buffer = new byte[16384];
            byte[] cont = new byte[1];

            using (MemoryStream ms = new MemoryStream())
            {
                while (fullread == false)
                {
                    int len = 0;
                    if ((len = crypto.Read(buffer, 0, buffer.Length)) <= 0)
                    {
                        fullread = true;
                        cont = ms.ToArray();
                    }
                    ms.Write(buffer, 0, len);
                }
            }

            memory.Close();
            crypto.Close();

            Array.Resize(ref cont, contentsize);

            return cont;
        }

        /// <summary>
        /// Decrypts the given content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tmd"></param>
        /// <param name="contentcount"></param>
        /// <param name="titlekey"></param>
        /// <returns></returns>
        public static byte[] DecryptContent(byte[] content, byte[] tmd, int contentcount, byte[] titlekey)
        {
            byte[] iv = new byte[16];
            string[,] continfo = WadInfo.GetContentInfo(tmd);
            int contentsize = content.Length;
            int paddedsize = Tools.AddPadding(contentsize, 16);
            Array.Resize(ref content, paddedsize);

            iv[0] = tmd[0x1e8 + (0x24 * contentcount)];
            iv[1] = tmd[0x1e9 + (0x24 * contentcount)];

            RijndaelManaged decrypt = new RijndaelManaged();
            decrypt.Mode = CipherMode.CBC;
            decrypt.Padding = PaddingMode.None;
            decrypt.KeySize = 128;
            decrypt.BlockSize = 128;
            decrypt.Key = titlekey;
            decrypt.IV = iv;

            ICryptoTransform cryptor = decrypt.CreateDecryptor();

            MemoryStream memory = new MemoryStream(content, 0, paddedsize);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            bool fullread = false;
            byte[] buffer = new byte[memory.Length];
            byte[] cont = new byte[1];

            using (MemoryStream ms = new MemoryStream())
            {
                while (fullread == false)
                {
                    int len = 0;
                    if ((len = crypto.Read(buffer, 0, buffer.Length)) <= 0)
                    {
                        fullread = true;
                        cont = ms.ToArray();
                    }
                    ms.Write(buffer, 0, len);
                }
            }

            memory.Close();
            crypto.Close();

            return cont;
        }

        /// <summary>
        /// Encrypts the given content and adds a padding to the next 64 bytes
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tmd"></param>
        /// <param name="contentcount"></param>
        /// <param name="titlekey"></param>
        /// <returns></returns>
        public static byte[] EncryptContent(byte[] content, byte[] tmd, int contentcount, byte[] titlekey, bool addpadding)
        {
            byte[] iv = new byte[16];
            string[,] continfo = WadInfo.GetContentInfo(tmd);
            int contentsize = content.Length;
            int paddedsize = Tools.AddPadding(contentsize, 16);
            Array.Resize(ref content, paddedsize);

            iv[0] = tmd[0x1e8 + (0x24 * contentcount)];
            iv[1] = tmd[0x1e9 + (0x24 * contentcount)];

            RijndaelManaged encrypt = new RijndaelManaged();
            encrypt.Mode = CipherMode.CBC;
            encrypt.Padding = PaddingMode.None;
            encrypt.KeySize = 128;
            encrypt.BlockSize = 128;
            encrypt.Key = titlekey;
            encrypt.IV = iv;

            ICryptoTransform cryptor = encrypt.CreateEncryptor();

            MemoryStream memory = new MemoryStream(content, 0, paddedsize);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            bool fullread = false;
            byte[] buffer = new byte[memory.Length];
            byte[] cont = new byte[1];

            using (MemoryStream ms = new MemoryStream())
            {
                while (fullread == false)
                {
                    int len = 0;
                    if ((len = crypto.Read(buffer, 0, buffer.Length)) <= 0)
                    {
                        fullread = true;
                        cont = ms.ToArray();
                    }
                    ms.Write(buffer, 0, len);
                }
            }

            memory.Close();
            crypto.Close();

            if (addpadding == true) { Array.Resize(ref cont, Tools.AddPadding(cont.Length)); }
            return cont;
        }

        /// <summary>
        /// Re-Encrypts the given content
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static byte[] ReEncryptAllContents(byte[] wadfile, byte[] oldtitlekey, byte[] newtitlekey)
        {
            int contentnum = WadInfo.GetContentNum(wadfile);
            int certsize = WadInfo.GetCertSize(wadfile);
            int tiksize = WadInfo.GetTikSize(wadfile);
            int tmdsize = WadInfo.GetTmdSize(wadfile);
            int contentpos = 64 + Tools.AddPadding(certsize) + Tools.AddPadding(tiksize) + Tools.AddPadding(tmdsize);

            for (int i = 0; i < contentnum; i++)
            {
                byte[] tmd = WadInfo.ReturnTmd(wadfile);
                byte[] decryptedcontent = DecryptContent(wadfile, i, oldtitlekey);
                byte[] encryptedcontent = EncryptContent(decryptedcontent, tmd, i, newtitlekey, false);

                for (int j = 0; j < encryptedcontent.Length; j++)
                {
                    wadfile[contentpos + j] = encryptedcontent[j];
                }
                contentpos += Tools.AddPadding(encryptedcontent.Length);
            }

            return wadfile;
        }

        /// <summary>
        /// Fixes the MD5 Sum in the IMET Header
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] FixMD5InImet(byte[] file, out byte[] newmd5)
        {
            if (Convert.ToChar(file[128]) == 'I' &&
                Convert.ToChar(file[129]) == 'M' &&
                Convert.ToChar(file[130]) == 'E' &&
                Convert.ToChar(file[131]) == 'T')
            {
                byte[] buffer = new byte[1536];

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(file, 0x40, 1536);
                    buffer = ms.ToArray();
                }

                for (int i = 0; i < 16; i++)
                    buffer[1520 + i] = 0x00;

                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(buffer);

                for (int i = 0; i < 16; i++)
                    file[1584 + i] = hash[i];

                newmd5 = hash;
                return file;
            }
            else
            {
                byte[] oldmd5 = new byte[16];

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(file, 1584, 16);
                    oldmd5 = ms.ToArray();
                }

                newmd5 = oldmd5;
                return file;
            }
        }

        /// <summary>
        /// Fixes the MD5 Sum in the IMET Header.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] FixMD5InImet(byte[] file)
        {
            byte[] tmp = new byte[16];
            return FixMD5InImet(file, out tmp);
        }

        /// <summary>
        /// Updates the Content Info in the Tmd.
        /// Tmd and Contents must be in the same Directory
        /// </summary>
        /// <param name="tmdfile"></param>
        public static void UpdateTmdContents(string tmdfile)
        {
            FileStream tmd = new FileStream(tmdfile, FileMode.Open, FileAccess.ReadWrite);

            tmd.Seek(0x1de, SeekOrigin.Begin);
            int contentcount = Tools.HexStringToInt(tmd.ReadByte().ToString("x2") + tmd.ReadByte().ToString("x2"));

            for (int i = 0; i < contentcount; i++)
            {
                int oldsize = 0;
                int contentpos = 0x1e4 + (36 * i);

                tmd.Seek(contentpos + 4, SeekOrigin.Begin);
                string index = "0000" + tmd.ReadByte().ToString("x2") + tmd.ReadByte().ToString("x2");

                tmd.Seek(contentpos + 8, SeekOrigin.Begin);
                try
                {
                    oldsize = Tools.HexStringToInt(tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2") +
                        tmd.ReadByte().ToString("x2"));
                }
                catch { }

                byte[] oldsha1 = new byte[20];
                tmd.Read(oldsha1, 0, oldsha1.Length);

                if (File.Exists(tmdfile.Remove(tmdfile.LastIndexOf('\\') + 1) + index + ".app"))
                {
                    byte[] content = Wii.Tools.LoadFileToByteArray(tmdfile.Remove(tmdfile.LastIndexOf('\\') + 1) + index + ".app");
                    int newsize = content.Length;

                    if (newsize != oldsize)
                    {
                        byte[] changedsize = Tools.FileLengthToByteArray(newsize);

                        tmd.Seek(contentpos + 8, SeekOrigin.Begin);
                        for (int x = 8; x > changedsize.Length; x--) tmd.WriteByte(0x00);
                        tmd.Write(changedsize, 0, changedsize.Length);
                    }

                    SHA1Managed sha1 = new SHA1Managed();
                    byte[] newsha1 = sha1.ComputeHash(content);
                    sha1.Clear();

                    if (Tools.CompareByteArrays(newsha1, oldsha1) == false)
                    {
                        tmd.Seek(contentpos + 16, SeekOrigin.Begin);
                        tmd.Write(newsha1, 0, newsha1.Length);
                    }
                }
                else
                {
                    throw new Exception("At least one content file wasn't found!");
                }
            }

            tmd.Close();
        }

        /// <summary>
        /// Changes the Boot Index in the Tmd to the given value
        /// </summary>
        /// <param name="wadtmd"></param>
        /// <returns></returns>
        public static byte[] ChangeTmdBootIndex(byte[] wadtmd, int newindex)
        {
            int tmdpos = 0;

            if (WadInfo.IsThisWad(wadtmd) == true)
                tmdpos = WadInfo.GetTmdPos(wadtmd);

            byte[] index = BitConverter.GetBytes((UInt16)newindex);
            wadtmd[tmdpos + 0x1e0] = index[1];
            wadtmd[tmdpos + 0x1e1] = index[0];

            return wadtmd;
        }

        /// <summary>
        /// Changes the Content Count in the Tmd
        /// </summary>
        /// <param name="wadtmd"></param>
        /// <param name="newcount"></param>
        /// <returns></returns>
        public static byte[] ChangeTmdContentCount(byte[] wadtmd, int newcount)
        {
            int tmdpos = 0;

            if (WadInfo.IsThisWad(wadtmd) == true)
                tmdpos = WadInfo.GetTmdPos(wadtmd);

            byte[] count = BitConverter.GetBytes((UInt16)newcount);
            wadtmd[tmdpos + 0x1de] = count[1];
            wadtmd[tmdpos + 0x1df] = count[0];

            return wadtmd;
        }

        /// <summary>
        /// Changes the Slot where the IOS Wad will be installed to
        /// </summary>
        /// <param name="wad"></param>
        /// <param name="newslot"></param>
        /// <returns></returns>
        public static byte[] ChangeIosSlot(byte[] wadfile, int newslot)
        {
            Tools.ChangeProgress(0);

            int tikpos = WadInfo.GetTikPos(wadfile);
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            byte[] slot = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(newslot));

            byte[] oldtitlekey = WadInfo.GetTitleKey(wadfile);

            Tools.ChangeProgress(20);

            //Change the ID in the ticket
            wadfile[tikpos + 0x1e0] = slot[0];
            wadfile[tikpos + 0x1e1] = slot[1];
            wadfile[tikpos + 0x1e2] = slot[2];
            wadfile[tikpos + 0x1e3] = slot[3];

            //Change the ID in the tmd
            wadfile[tmdpos + 0x190] = slot[0];
            wadfile[tmdpos + 0x191] = slot[1];
            wadfile[tmdpos + 0x192] = slot[2];
            wadfile[tmdpos + 0x193] = slot[3];

            Tools.ChangeProgress(40);

            //Trucha-Sign both
            wadfile = TruchaSign(wadfile, 0);

            Tools.ChangeProgress(50);

            wadfile = TruchaSign(wadfile, 1);

            Tools.ChangeProgress(60);

            byte[] newtitlekey = WadInfo.GetTitleKey(wadfile);
            byte[] tmd = WadInfo.ReturnTmd(wadfile);

            int contentcount = WadInfo.GetContentNum(wadfile);

            wadfile = ReEncryptAllContents(wadfile, oldtitlekey, newtitlekey);

            Tools.ChangeProgress(100);
            return wadfile;
        }

        /// <summary>
        /// Changes the Title Version of a Wad or Tmd
        /// </summary>
        /// <param name="wadtmd"></param>
        /// <param name="newversion"></param>
        /// <returns></returns>
        public static byte[] ChangeTitleVersion(byte[] wadtmd, int newversion)
        {
            if (newversion > 65535) throw new Exception("Version can be max. 65535");

            int offset = 0x1dc;
            int tmdpos = 0;

            if (WadInfo.IsThisWad(wadtmd))
                tmdpos = WadInfo.GetTmdPos(wadtmd);

            byte[] version = BitConverter.GetBytes((UInt16)newversion);
            Array.Reverse(version);

            wadtmd[tmdpos + offset] = version[0];
            wadtmd[tmdpos + offset + 1] = version[1];

            wadtmd = TruchaSign(wadtmd, 1);

            return wadtmd;
        }

        /// <summary>
        /// Changes the Title Key in the Tik
        /// </summary>
        /// <param name="tik"></param>
        /// <returns></returns>
        public static byte[] ChangeTitleKey(byte[] tik)
        {
            byte[] newKey = new byte[] { 0x47, 0x6f, 0x74, 0x74, 0x61, 0x47, 0x65, 0x74, 0x53, 0x6f, 0x6d, 0x65, 0x42, 0x65, 0x65, 0x72 };
            Tools.InsertByteArray(tik, newKey, 447);
            return tik;
        }

        /// <summary>
        /// Returns the decrypted TitleKey
        /// </summary>
        /// <param name="wadtik"></param>
        /// <returns></returns>
        public static byte[] GetTitleKey(byte[] encryptedkey, byte[] titleid)
        {
            byte[] commonkey = new byte[16];

            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\common-key.bin"))
            { commonkey = Tools.LoadFileToByteArray(System.Windows.Forms.Application.StartupPath + "\\common-key.bin"); }
            else if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\key.bin"))
            { commonkey = Tools.LoadFileToByteArray(System.Windows.Forms.Application.StartupPath + "\\key.bin"); }
            else { throw new FileNotFoundException("The (common-)key.bin must be in the application directory!"); }

            Array.Resize(ref titleid, 16);

            RijndaelManaged decrypt = new RijndaelManaged();
            decrypt.Mode = CipherMode.CBC;
            decrypt.Padding = PaddingMode.None;
            decrypt.KeySize = 128;
            decrypt.BlockSize = 128;
            decrypt.Key = commonkey;
            decrypt.IV = titleid;

            ICryptoTransform cryptor = decrypt.CreateDecryptor();

            MemoryStream memory = new MemoryStream(encryptedkey);
            CryptoStream crypto = new CryptoStream(memory, cryptor, CryptoStreamMode.Read);

            byte[] decryptedkey = new byte[16];
            crypto.Read(decryptedkey, 0, decryptedkey.Length);

            crypto.Close();
            memory.Close();

            return decryptedkey;
        }
    }

    public class WadUnpack
    {
        /// <summary>
        /// Unpacks the 00000000.app of a wad
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static void UnpackNullApp(string wadfile, string destination)
        {
            if (!destination.EndsWith(".app")) destination += "\\00000000.app";

            byte[] wad = Tools.LoadFileToByteArray(wadfile);
            byte[] nullapp = UnpackNullApp(wad);
            Tools.SaveFileFromByteArray(nullapp, destination);
        }

        /// <summary>
        /// Unpacks the 00000000.app of a wad
        /// </summary>
        /// <param name="wadfile"></param>
        /// <returns></returns>
        public static byte[] UnpackNullApp(byte[] wadfile)
        {
            int certsize = WadInfo.GetCertSize(wadfile);
            int tiksize = WadInfo.GetTikSize(wadfile);
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            int tmdsize = WadInfo.GetTmdSize(wadfile);
            int contentpos = 64 + Tools.AddPadding(certsize) + Tools.AddPadding(tiksize) + Tools.AddPadding(tmdsize);

            byte[] titlekey = WadInfo.GetTitleKey(wadfile);
            string[,] contents = WadInfo.GetContentInfo(wadfile);

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                if (contents[i, 1] == "00000000")
                {
                    return WadEdit.DecryptContent(wadfile, i, titlekey);
                }
            }

            throw new Exception("00000000.app couldn't be found in the Wad");
        }

        /// <summary>
        /// Unpacks the the wad file
        /// </summary>
        public static void UnpackWad(string pathtowad, string destinationpath)
        {
            byte[] wadfile = Tools.LoadFileToByteArray(pathtowad);
            UnpackWad(wadfile, destinationpath);
        }

        /// <summary>
        /// Unpacks the the wad file
        /// </summary>
        public static void UnpackWad(string pathtowad, string destinationpath, out bool hashesmatch)
        {
            byte[] wadfile = Tools.LoadFileToByteArray(pathtowad);
            UnpackWad(wadfile, destinationpath, out hashesmatch);
        }

        /// <summary>
        /// Unpacks the wad file to *wadpath*\wadunpack\
        /// </summary>
        /// <param name="pathtowad"></param>
        public static void UnpackWad(string pathtowad)
        {
            string destinationpath = pathtowad.Remove(pathtowad.LastIndexOf('\\'));
            byte[] wadfile = Tools.LoadFileToByteArray(pathtowad);
            UnpackWad(wadfile, destinationpath);
        }

        /// <summary>
        /// Unpacks the wad file
        /// </summary>
        public static void UnpackWad(byte[] wadfile, string destinationpath)
        {
            bool temp;
            UnpackWad(wadfile, destinationpath, out temp);
        }

        /// <summary>
        /// Unpacks the wad file
        /// </summary>
        public static void UnpackWad(byte[] wadfile, string destinationpath, out bool hashesmatch)
        {
            if (destinationpath[destinationpath.Length - 1] != '\\')
            { destinationpath = destinationpath + "\\"; }

            hashesmatch = true;

            if (!Directory.Exists(destinationpath))
            { Directory.CreateDirectory(destinationpath); }
            if (Directory.GetFiles(destinationpath, "*.app").Length > 0)
            {
                throw new Exception("At least one of the files to unpack already exists!");
            }

            int certpos = 0x40;
            int certsize = WadInfo.GetCertSize(wadfile);
            int tikpos = WadInfo.GetTikPos(wadfile);
            int tiksize = WadInfo.GetTikSize(wadfile);
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            int tmdsize = WadInfo.GetTmdSize(wadfile);
            int contentlength = WadInfo.GetContentSize(wadfile);
            int footersize = WadInfo.GetFooterSize(wadfile);
            int footerpos = 64 + Tools.AddPadding(certsize) + Tools.AddPadding(tiksize) + Tools.AddPadding(tmdsize) + Tools.AddPadding(contentlength);
            string wadpath = WadInfo.GetNandPath(wadfile, 0).Remove(8, 1);
            string[,] contents = WadInfo.GetContentInfo(wadfile);
            byte[] titlekey = WadInfo.GetTitleKey(wadfile);
            int contentpos = 64 + Tools.AddPadding(certsize) + Tools.AddPadding(tiksize) + Tools.AddPadding(tmdsize);

            //unpack cert
            using (FileStream cert = new FileStream(destinationpath + wadpath + ".cert", FileMode.Create))
            {
                cert.Seek(0, SeekOrigin.Begin);
                cert.Write(wadfile, certpos, certsize);
            }

            //unpack ticket
            using (FileStream tik = new FileStream(destinationpath + wadpath + ".tik", FileMode.Create))
            {
                tik.Seek(0, SeekOrigin.Begin);
                tik.Write(wadfile, tikpos, tiksize);
            }

            //unpack tmd
            using (FileStream tmd = new FileStream(destinationpath + wadpath + ".tmd", FileMode.Create))
            {
                tmd.Seek(0, SeekOrigin.Begin);
                tmd.Write(wadfile, tmdpos, tmdsize);
            }

            //unpack trailer
            try
            {
                if (footersize > 0)
                {
                    using (FileStream trailer = new FileStream(destinationpath + wadpath + ".trailer", FileMode.Create))
                    {
                        trailer.Seek(0, SeekOrigin.Begin);
                        trailer.Write(wadfile, footerpos, footersize);
                    }
                }
            }
            catch { } //who cares if the trailer doesn't extract properly?

            Tools.ChangeProgress(0);

            //unpack contents
            for (int i = 0; i < contents.GetLength(0); i++)
            {
                Tools.ChangeProgress((i + 1) * 100 / contents.GetLength(0));
                byte[] thiscontent = WadEdit.DecryptContent(wadfile, i, titlekey);
                FileStream content = new FileStream(destinationpath + contents[i, 1] + ".app", FileMode.Create);

                content.Write(thiscontent, 0, thiscontent.Length);
                content.Close();

                contentpos += Tools.AddPadding(thiscontent.Length);

                //sha1 comparison
                SHA1Managed sha1 = new SHA1Managed();
                byte[] thishash = sha1.ComputeHash(thiscontent);
                byte[] tmdhash = Tools.HexStringToByteArray(contents[i, 4]);

                if (Tools.CompareByteArrays(thishash, tmdhash) == false) hashesmatch = false;
                //    throw new Exception("At least one content's hash doesn't match the hash in the Tmd!");
            }
        }

        /// <summary>
        /// Unpacks the wad file to the given directory.
        /// Shared contents will be unpacked to /shared1
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="nandpath"></param>
        public static void UnpackToNand(string wadfile, string nandpath)
        {
            byte[] wadarray = Tools.LoadFileToByteArray(wadfile);
            UnpackToNand(wadarray, nandpath);
        }

        /// <summary>
        /// Unpacks the wad file to the given directory.
        /// Shared contents will be unpacked to /shared1
        /// </summary>
        /// <param name="wadfile"></param>
        /// <param name="nandpath"></param>
        public static void UnpackToNand(byte[] wadfile, string nandpath)
        {
            string path = WadInfo.GetNandPath(wadfile, 0);
            string path1 = path.Remove(path.IndexOf('\\'));
            string path2 = path.Remove(0, path.IndexOf('\\') + 1);

            if (nandpath[nandpath.Length - 1] != '\\') { nandpath = nandpath + "\\"; }

            if (!Directory.Exists(nandpath + "ticket")) { Directory.CreateDirectory(nandpath + "ticket"); }
            if (!Directory.Exists(nandpath + "title")) { Directory.CreateDirectory(nandpath + "title"); }
            if (!Directory.Exists(nandpath + "ticket\\" + path1)) { Directory.CreateDirectory(nandpath + "ticket\\" + path1); }
            if (!Directory.Exists(nandpath + "title\\" + path1)) { Directory.CreateDirectory(nandpath + "title\\" + path1); }
            if (!Directory.Exists(nandpath + "title\\" + path1 + "\\" + path2)) { Directory.CreateDirectory(nandpath + "title\\" + path1 + "\\" + path2); }
            if (!Directory.Exists(nandpath + "title\\" + path1 + "\\" + path2 + "\\content")) { Directory.CreateDirectory(nandpath + "title\\" + path1 + "\\" + path2 + "\\content"); }
            if (!Directory.Exists(nandpath + "title\\" + path1 + "\\" + path2 + "\\data")) { Directory.CreateDirectory(nandpath + "title\\" + path1 + "\\" + path2 + "\\data"); }
            if (!Directory.Exists(nandpath + "shared1")) Directory.CreateDirectory(nandpath + "shared1");

            int certsize = WadInfo.GetCertSize(wadfile);
            int tikpos = WadInfo.GetTikPos(wadfile);
            int tiksize = WadInfo.GetTikSize(wadfile);
            int tmdpos = WadInfo.GetTmdPos(wadfile);
            int tmdsize = WadInfo.GetTmdSize(wadfile);
            int contentlength = WadInfo.GetContentSize(wadfile);
            string[,] contents = WadInfo.GetContentInfo(wadfile);
            byte[] titlekey = WadInfo.GetTitleKey(wadfile);
            int contentpos = 64 + Tools.AddPadding(certsize) + Tools.AddPadding(tiksize) + Tools.AddPadding(tmdsize);

            //unpack ticket
            using (FileStream tik = new FileStream(nandpath + "ticket\\" + path1 + "\\" + path2 + ".tik", FileMode.Create))
            {
                tik.Seek(0, SeekOrigin.Begin);
                tik.Write(wadfile, tikpos, tiksize);
            }

            //unpack tmd
            using (FileStream tmd = new FileStream(nandpath + "title\\" + path1 + "\\" + path2 + "\\content\\title.tmd", FileMode.Create))
            {
                tmd.Seek(0, SeekOrigin.Begin);
                tmd.Write(wadfile, tmdpos, tmdsize);
            }

            Tools.ChangeProgress(0);

            //unpack contents
            for (int i = 0; i < contents.GetLength(0); i++)
            {
                Tools.ChangeProgress((i + 1) * 100 / contents.GetLength(0));
                byte[] thiscontent = WadEdit.DecryptContent(wadfile, i, titlekey);

                if (contents[i, 2] == "8001")
                {
                    if (File.Exists(nandpath + "shared1\\content.map"))
                    {
                        byte[] contmap = Tools.LoadFileToByteArray(nandpath + "shared1\\content.map");

                        if (ContentMap.CheckSharedContent(contmap, contents[i, 4]) == false)
                        {
                            string newname = ContentMap.GetNewSharedContentName(contmap);

                            FileStream content = new FileStream(nandpath + "shared1\\" + newname + ".app", FileMode.Create);
                            content.Write(thiscontent, 0, thiscontent.Length);
                            content.Close();
                            ContentMap.AddSharedContent(nandpath + "shared1\\content.map", newname, contents[i, 4]);
                        }
                    }
                    else
                    {
                        FileStream content = new FileStream(nandpath + "shared1\\00000000.app", FileMode.Create);
                        content.Write(thiscontent, 0, thiscontent.Length);
                        content.Close();
                        ContentMap.AddSharedContent(nandpath + "shared1\\content.map", "00000000", contents[i, 4]);
                    }
                }
                else
                {
                    FileStream content = new FileStream(nandpath + "title\\" + path1 + "\\" + path2 + "\\content\\" + contents[i, 0] + ".app", FileMode.Create);

                    content.Write(thiscontent, 0, thiscontent.Length);
                    content.Close();
                }

                contentpos += Tools.AddPadding(thiscontent.Length);
            }

            //add titleid to uid.sys, if it doesn't exist
            string titleid = WadInfo.GetFullTitleID(wadfile, 1);

            if (File.Exists(nandpath + "\\sys\\uid.sys"))
            {
                FileStream fs = new FileStream(nandpath + "\\sys\\uid.sys", FileMode.Open);
                byte[] uidsys = new byte[fs.Length];
                fs.Read(uidsys, 0, uidsys.Length);
                fs.Close();

                if (UID.CheckUID(uidsys, titleid) == false)
                {
                    uidsys = UID.AddUID(uidsys, titleid);
                    Tools.SaveFileFromByteArray(uidsys, nandpath + "\\sys\\uid.sys");
                }
            }
            else
            {
                if (!Directory.Exists(nandpath + "\\sys")) Directory.CreateDirectory(nandpath + "\\sys");
                byte[] uidsys = UID.AddUID(new byte[0], titleid);
                Tools.SaveFileFromByteArray(uidsys, nandpath + "\\sys\\uid.sys");
            }
        }
    }

    public class WadPack
    {
        public static byte[] wadheader = new byte[8] { 0x00, 0x00, 0x00, 0x20, 0x49, 0x73, 0x00, 0x00 };

        /// <summary>
        /// Gets the estimated size, might be !WRONG! due to Lz77 compression
        /// </summary>
        /// <param name="contentFolder"></param>
        public static int GetEstimatedSize(string contentdirectory)
        {
            if (contentdirectory[contentdirectory.Length - 1] != '\\') { contentdirectory = contentdirectory + "\\"; }

            if (!Directory.Exists(contentdirectory)) throw new DirectoryNotFoundException("The directory doesn't exists:\r\n" + contentdirectory);
            if (Directory.GetFiles(contentdirectory, "*.app").Length < 1) throw new Exception("No *.app file was found");
            if (Directory.GetFiles(contentdirectory, "*.cert").Length < 1) throw new Exception("No *.cert file was found");
            if (Directory.GetFiles(contentdirectory, "*.tik").Length < 1) throw new Exception("No *.tik file was found");
            if (Directory.GetFiles(contentdirectory, "*.tmd").Length < 1) throw new Exception("No *.tmd file was found");

            int size = 64; //Wad Header

            string[] certfile = Directory.GetFiles(contentdirectory, "*.cert");
            string[] tikfile = Directory.GetFiles(contentdirectory, "*.tik");
            string[] tmdfile = Directory.GetFiles(contentdirectory, "*.tmd");
            string[,] contents = WadInfo.GetContentInfo(File.ReadAllBytes(tmdfile[0]));

            FileInfo fi = new FileInfo(certfile[0]);
            size += Tools.AddPadding((int)fi.Length);
            fi = new FileInfo(tikfile[0]);
            size += Tools.AddPadding((int)fi.Length);
            fi = new FileInfo(tmdfile[0]);
            size += Tools.AddPadding((int)fi.Length);

            for (int i = 0; i < contents.GetLength(0); i++)
                size += Tools.AddPadding(int.Parse(contents[i, 3]));

            return size + 16; //Footer Timestamp
        }

        /// <summary>
        /// Packs the contents in the given directory and creates the destination wad file 
        /// </summary>
        /// <param name="directory"></param>
        public static void PackWad(string contentdirectory, string destinationfile)
        {
            if (contentdirectory[contentdirectory.Length - 1] != '\\') { contentdirectory = contentdirectory + "\\"; }

            if (!Directory.Exists(contentdirectory)) throw new DirectoryNotFoundException("The directory doesn't exists:\r\n" + contentdirectory);
            if (Directory.GetFiles(contentdirectory, "*.app").Length < 1) throw new Exception("No *.app file was found");
            if (Directory.GetFiles(contentdirectory, "*.cert").Length < 1) throw new Exception("No *.cert file was found");
            if (Directory.GetFiles(contentdirectory, "*.tik").Length < 1) throw new Exception("No *.tik file was found");
            if (Directory.GetFiles(contentdirectory, "*.tmd").Length < 1) throw new Exception("No *.tmd file was found");
            if (File.Exists(destinationfile)) throw new Exception("The destination file already exists!");

            string[] certfile = Directory.GetFiles(contentdirectory, "*.cert");
            string[] tikfile = Directory.GetFiles(contentdirectory, "*.tik");
            string[] tmdfile = Directory.GetFiles(contentdirectory, "*.tmd");

            byte[] cert = Tools.LoadFileToByteArray(certfile[0]);
            byte[] tik = Tools.LoadFileToByteArray(tikfile[0]);
            byte[] tmd = Tools.LoadFileToByteArray(tmdfile[0]);

            tik = WadEdit.ChangeTitleKey(tik);

            string[,] contents = WadInfo.GetContentInfo(tmd);

            FileStream wadstream = new FileStream(destinationfile, FileMode.Create);

            //Trucha-Sign Tik and Tmd, if they aren't already
            WadEdit.TruchaSign(tik, 0);
            WadEdit.TruchaSign(tmd, 1);

            //Write Cert
            wadstream.Seek(64, SeekOrigin.Begin);
            wadstream.Write(cert, 0, cert.Length);

            //Write Tik
            wadstream.Seek(64 + Tools.AddPadding(cert.Length), SeekOrigin.Begin);
            wadstream.Write(tik, 0, tik.Length);

            //Write Tmd
            wadstream.Seek(64 + Tools.AddPadding(cert.Length) + Tools.AddPadding(tik.Length), SeekOrigin.Begin);
            wadstream.Write(tmd, 0, tmd.Length);

            //Write Content
            int allcont = 0;
            int contpos = 64 + Tools.AddPadding(cert.Length) + Tools.AddPadding(tik.Length) + Tools.AddPadding(tmd.Length);
            int contcount = WadInfo.GetContentNum(tmd);

            Tools.ChangeProgress(0);
            byte[] titlekey = WadInfo.GetTitleKey(tik);

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                Tools.ChangeProgress((i + 1) * 100 / contents.GetLength(0));
                byte[] thiscont = Tools.LoadFileToByteArray(contentdirectory + contents[i, 1] + ".app");

                //if (i == contents.GetLength(0) - 1) { thiscont = WadEdit.EncryptContent(thiscont, tmd, i, titlekey, false); }
                //else { thiscont = WadEdit.EncryptContent(thiscont, tmd, i, titlekey, true); }
                thiscont = WadEdit.EncryptContent(thiscont, tmd, i, titlekey, true);

                wadstream.Seek(contpos, SeekOrigin.Begin);
                wadstream.Write(thiscont, 0, thiscont.Length);
                contpos += thiscont.Length;
                allcont += thiscont.Length;
            }

            //Write Footer Timestamp
            byte[] footer = Tools.GetTimestamp();
            Array.Resize(ref footer, Tools.AddPadding(footer.Length));

            wadstream.Seek(Tools.AddPadding(contpos), SeekOrigin.Begin);
            wadstream.Write(footer, 0, footer.Length);

            //Write Header
            byte[] certsize = Tools.FileLengthToByteArray(cert.Length);
            byte[] tiksize = Tools.FileLengthToByteArray(tik.Length);
            byte[] tmdsize = Tools.FileLengthToByteArray(tmd.Length);
            byte[] allcontsize = Tools.FileLengthToByteArray(allcont);
            byte[] footersize = Tools.FileLengthToByteArray(footer.Length);

            wadstream.Seek(0x00, SeekOrigin.Begin);
            wadstream.Write(wadheader, 0, wadheader.Length);
            wadstream.Seek(0x08, SeekOrigin.Begin);
            wadstream.Write(certsize, 0, certsize.Length);
            wadstream.Seek(0x10, SeekOrigin.Begin);
            wadstream.Write(tiksize, 0, tiksize.Length);
            wadstream.Seek(0x14, SeekOrigin.Begin);
            wadstream.Write(tmdsize, 0, tmdsize.Length);
            wadstream.Seek(0x18, SeekOrigin.Begin);
            wadstream.Write(allcontsize, 0, allcontsize.Length);
            wadstream.Seek(0x1c, SeekOrigin.Begin);
            wadstream.Write(footersize, 0, footersize.Length);

            wadstream.Close();
        }

        /// <summary>
        /// Packs a Wad from a title installed on Nand
        /// Returns: 0 = OK, 1 = Files missing, 2 = Shared contents missing, 3 = Cert missing
        /// </summary>
        /// <param name="nandpath"></param>
        /// <param name="path">XXXXXXXX\XXXXXXXX</param>
        /// <param name="destinationfile"></param>
        /// <returns></returns>
        public static void PackWadFromNand(string nandpath, string path, string destinationfile)
        {
            if (nandpath[nandpath.Length - 1] != '\\') { nandpath = nandpath + "\\"; }
            string path1 = path.Remove(8);
            string path2 = path.Remove(0, 9);
            string ticketdir = nandpath + "ticket\\" + path1 + "\\";
            string contentdir = nandpath + "title\\" + path1 + "\\" + path2 + "\\content\\";
            string sharedir = nandpath + "shared1\\";
            string certdir = nandpath + "sys\\";

            if (!Directory.Exists(ticketdir) ||
                !Directory.Exists(contentdir)) throw new DirectoryNotFoundException("Directory doesn't exist:\r\n" + contentdir);
            if (!Directory.Exists(sharedir)) throw new DirectoryNotFoundException("Directory doesn't exist:\r\n" + sharedir);
            if (!File.Exists(certdir + "cert.sys")) throw new FileNotFoundException("File doesn't exist:\r\n" + certdir + "cert.sys");

            byte[] cert = Tools.LoadFileToByteArray(certdir + "cert.sys");
            byte[] tik = Tools.LoadFileToByteArray(ticketdir + path2 + ".tik");
            byte[] tmd = Tools.LoadFileToByteArray(contentdir + "title.tmd");

            tik = WadEdit.ChangeTitleKey(tik);

            string[,] contents = WadInfo.GetContentInfo(tmd);

            FileStream wadstream = new FileStream(destinationfile, FileMode.Create);

            //Trucha-Sign Tik and Tmd, if they aren't already
            WadEdit.TruchaSign(tik, 0);
            WadEdit.TruchaSign(tmd, 1);

            //Write Cert
            wadstream.Seek(64, SeekOrigin.Begin);
            wadstream.Write(cert, 0, cert.Length);

            //Write Tik
            wadstream.Seek(64 + Tools.AddPadding(cert.Length), SeekOrigin.Begin);
            wadstream.Write(tik, 0, tik.Length);

            //Write Tmd
            wadstream.Seek(64 + Tools.AddPadding(cert.Length) + Tools.AddPadding(tik.Length), SeekOrigin.Begin);
            wadstream.Write(tmd, 0, tmd.Length);

            //Write Content
            int allcont = 0;
            int contpos = 64 + Tools.AddPadding(cert.Length) + Tools.AddPadding(tik.Length) + Tools.AddPadding(tmd.Length);
            int contcount = WadInfo.GetContentNum(tmd);

            Tools.ChangeProgress(0);
            byte[] titlekey = WadInfo.GetTitleKey(tik);
            byte[] contentmap = Tools.LoadFileToByteArray(sharedir + "content.map");

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                Tools.ChangeProgress((i + 1) * 100 / contents.GetLength(0));
                byte[] thiscont = new byte[1];

                if (contents[i, 2] == "8001")
                {
                    string contname = ContentMap.GetSharedContentName(contentmap, contents[i, 4]);

                    if (contname == "fail")
                    {
                        wadstream.Close();
                        File.Delete(destinationfile);
                        throw new FileNotFoundException("At least one shared content is missing!");
                    }

                    thiscont = Tools.LoadFileToByteArray(sharedir + contname + ".app");
                }
                else thiscont = Tools.LoadFileToByteArray(contentdir + contents[i, 0] + ".app");

                //if (i == contents.GetLength(0) - 1) { thiscont = WadEdit.EncryptContent(thiscont, tmd, i, titlekey, false); }
                //else { thiscont = WadEdit.EncryptContent(thiscont, tmd, i, titlekey, true); }
                thiscont = WadEdit.EncryptContent(thiscont, tmd, i, titlekey, true);

                wadstream.Seek(contpos, SeekOrigin.Begin);
                wadstream.Write(thiscont, 0, thiscont.Length);
                contpos += thiscont.Length;
                allcont += thiscont.Length;
            }

            //Write Footer Timestamp
            byte[] footer = Tools.GetTimestamp();
            Array.Resize(ref footer, Tools.AddPadding(footer.Length, 16));

            int footerLength = footer.Length;
            wadstream.Seek(Tools.AddPadding(contpos), SeekOrigin.Begin);
            wadstream.Write(footer, 0, footer.Length);

            //Write Header
            byte[] certsize = Tools.FileLengthToByteArray(cert.Length);
            byte[] tiksize = Tools.FileLengthToByteArray(tik.Length);
            byte[] tmdsize = Tools.FileLengthToByteArray(tmd.Length);
            byte[] allcontsize = Tools.FileLengthToByteArray(allcont);
            byte[] footersize = Tools.FileLengthToByteArray(footerLength);

            wadstream.Seek(0x00, SeekOrigin.Begin);
            wadstream.Write(wadheader, 0, wadheader.Length);
            wadstream.Seek(0x08, SeekOrigin.Begin);
            wadstream.Write(certsize, 0, certsize.Length);
            wadstream.Seek(0x10, SeekOrigin.Begin);
            wadstream.Write(tiksize, 0, tiksize.Length);
            wadstream.Seek(0x14, SeekOrigin.Begin);
            wadstream.Write(tmdsize, 0, tmdsize.Length);
            wadstream.Seek(0x18, SeekOrigin.Begin);
            wadstream.Write(allcontsize, 0, allcontsize.Length);
            wadstream.Seek(0x1c, SeekOrigin.Begin);
            wadstream.Write(footersize, 0, footersize.Length);

            wadstream.Close();
        }
    }

    public class UID
    {
        /// <summary>
        /// Checks if the given Title ID exists in the uid.sys
        /// </summary>
        /// <param name="uidsys"></param>
        /// <param name="fulltitleid"></param>
        /// <returns></returns>
        public static bool CheckUID(byte[] uidsys, string fulltitleid)
        {
            for (int i = 0; i < uidsys.Length; i += 12)
            {
                string temp = "";

                for (int y = i; y < i + 8; y++)
                    temp += uidsys[y].ToString("x2");

                if (temp == fulltitleid) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a new UID
        /// </summary>
        /// <param name="uidsys"></param>
        /// <returns></returns>
        public static string GetNewUID(byte[] uidsys)
        {
            string lastuid = uidsys[uidsys.Length - 4].ToString("x2") +
                uidsys[uidsys.Length - 3].ToString("x2") +
                uidsys[uidsys.Length - 2].ToString("x2") +
                uidsys[uidsys.Length - 1].ToString("x2");

            string newuid = (int.Parse(lastuid, System.Globalization.NumberStyles.HexNumber) + 1).ToString("x8");
            return newuid;
        }

        /// <summary>
        /// Adds a Title ID to uid.sys
        /// </summary>
        /// <param name="uidsys"></param>
        /// <param name="fulltitleid"></param>
        /// <returns></returns>
        public static byte[] AddUID(byte[] uidsys, string fulltitleid)
        {
            if (uidsys.Length > 11)
            {
                MemoryStream uid = new MemoryStream();
                byte[] titleid = Tools.HexStringToByteArray(fulltitleid);
                byte[] newuid = Tools.HexStringToByteArray(GetNewUID(uidsys));

                uid.Write(uidsys, 0, uidsys.Length);
                uid.Write(titleid, 0, titleid.Length);
                uid.Write(newuid, 0, newuid.Length);

                return uid.ToArray();
            }
            else
            {
                MemoryStream uid = new MemoryStream();
                byte[] titleid = Tools.HexStringToByteArray(fulltitleid);
                byte[] newuid = new byte[] { 0x00, 0x00, 0x10, 0x00 };

                uid.Write(titleid, 0, titleid.Length);
                uid.Write(newuid, 0, newuid.Length);

                return uid.ToArray();
            }
        }
    }

    public class ContentMap
    {
        /// <summary>
        /// Gets the name of the shared content in /shared1/.
        /// Returns "fail", if the content doesn't exist
        /// </summary>
        /// <param name="contentmap"></param>
        /// <param name="sha1ofcontent"></param>
        /// <returns></returns>
        public static string GetSharedContentName(byte[] contentmap, string sha1ofcontent)
        {
            int contindex = 0;
            string result = "";

            for (int i = 0; i < contentmap.Length - 19; i++)
            {
                string tmp = "";
                for (int y = 0; y < 20; y++)
                {
                    tmp += contentmap[i + y].ToString("x2");
                }

                if (tmp == sha1ofcontent)
                {
                    contindex = i;
                    break;
                }
            }

            if (contindex == 0) return "fail";

            result += Convert.ToChar(contentmap[contindex - 8]);
            result += Convert.ToChar(contentmap[contindex - 7]);
            result += Convert.ToChar(contentmap[contindex - 6]);
            result += Convert.ToChar(contentmap[contindex - 5]);
            result += Convert.ToChar(contentmap[contindex - 4]);
            result += Convert.ToChar(contentmap[contindex - 3]);
            result += Convert.ToChar(contentmap[contindex - 2]);
            result += Convert.ToChar(contentmap[contindex - 1]);

            return result;
        }

        /// <summary>
        /// Checks, if the shared content exists
        /// </summary>
        /// <param name="contentmap"></param>
        /// <param name="sha1ofcontent"></param>
        /// <returns></returns>
        public static bool CheckSharedContent(byte[] contentmap, string sha1ofcontent)
        {
            for (int i = 0; i < contentmap.Length - 19; i++)
            {
                string tmp = "";
                for (int y = 0; y < 20; y++)
                {
                    tmp += contentmap[i + y].ToString("x2");
                }

                if (tmp == sha1ofcontent) return true;
            }

            return false;
        }

        public static string GetNewSharedContentName(byte[] contentmap)
        {
            string name = "";

            name += Convert.ToChar(contentmap[contentmap.Length - 28]);
            name += Convert.ToChar(contentmap[contentmap.Length - 27]);
            name += Convert.ToChar(contentmap[contentmap.Length - 26]);
            name += Convert.ToChar(contentmap[contentmap.Length - 25]);
            name += Convert.ToChar(contentmap[contentmap.Length - 24]);
            name += Convert.ToChar(contentmap[contentmap.Length - 23]);
            name += Convert.ToChar(contentmap[contentmap.Length - 22]);
            name += Convert.ToChar(contentmap[contentmap.Length - 21]);

            string newname = (int.Parse(name, System.Globalization.NumberStyles.HexNumber) + 1).ToString("x8");

            return newname;
        }

        public static void AddSharedContent(string contentmap, string contentname, string sha1ofcontent)
        {
            byte[] name = new byte[8];
            byte[] sha1 = new byte[20];

            for (int i = 0; i < 8; i++)
            {
                name[i] = (byte)contentname[i];
            }

            for (int i = 0; i < sha1ofcontent.Length; i += 2)
            {
                sha1[i / 2] = Convert.ToByte(sha1ofcontent.Substring(i, 2), 16);
            }

            using (FileStream map = new FileStream(contentmap, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                map.Seek(0, SeekOrigin.End);
                map.Write(name, 0, name.Length);
                map.Write(sha1, 0, sha1.Length);
            }
        }
    }

    public class U8
    {
        /// <summary>
        /// Checks if the given file is a U8 Archive
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CheckU8(byte[] file)
        {
            int length = 2500;
            if (file.Length < length) length = file.Length - 4;

            for (int i = 0; i < length; i++)
            {
                if (file[i] == 0x55 && file[i + 1] == 0xAA && file[i + 2] == 0x38 && file[i + 3] == 0x2D)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the given file is a U8 Archive
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CheckU8(string file)
        {
            byte[] buff = Tools.LoadFileToByteArray(file, 0, 2500);
            return CheckU8(buff);
        }

        /// <summary>
        /// Gets all contents of a folder (including (sub-)files and (sub-)folders)
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string[] GetDirContent(string dir, bool root)
        {
            string[] files = Directory.GetFiles(dir);
            string[] dirs = Directory.GetDirectories(dir);
            string all = "";

            if (root == false)
                all += dir + "\n";

            for (int i = 0; i < files.Length; i++)
                all += files[i] + "\n";

            foreach (string thisDir in dirs)
            {
                string[] temp = GetDirContent(thisDir, false);

                foreach (string thisTemp in temp)
                    all += thisTemp + "\n";
            }

            return all.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Detecs if the U8 file has an IMD5 or IMET Header.
        /// Return: 0 = No Header, 1 = IMD5, 2 = IMET
        /// </summary>
        /// <param name="file"></param>
        public static int DetectHeader(string file)
        {
            byte[] temp = Tools.LoadFileToByteArray(file, 0, 400);
            return DetectHeader(temp);
        }

        /// <summary>
        /// Detecs if the U8 file has an IMD5 or IMET Header.
        /// Return: 0 = No Header, 1 = IMD5, 2 = IMET
        /// </summary>
        /// <param name="file"></param>
        public static int DetectHeader(byte[] file)
        {
            for (int i = 0; i < 16; i++) //Just to be safe
            {
                if (Convert.ToChar(file[i]) == 'I')
                    if (Convert.ToChar(file[i + 1]) == 'M')
                        if (Convert.ToChar(file[i + 2]) == 'D')
                            if (Convert.ToChar(file[i + 3]) == '5')
                                return 1;
            }

            int length = 400;
            if (file.Length < 400) length = file.Length - 4;

            for (int z = 0; z < length; z++)
            {
                if (Convert.ToChar(file[z]) == 'I')
                    if (Convert.ToChar(file[z + 1]) == 'M')
                        if (Convert.ToChar(file[z + 2]) == 'E')
                            if (Convert.ToChar(file[z + 3]) == 'T')
                                return 2;
            }

            return 0;
        }

        /// <summary>
        /// Adds an IMD5 Header to the given U8 Archive
        /// </summary>
        /// <param name="u8archive"></param>
        /// <returns></returns>
        public static byte[] AddHeaderIMD5(byte[] u8archive)
        {
            MemoryStream ms = new MemoryStream();
            MD5 md5 = MD5.Create();

            byte[] imd5 = new byte[4];
            imd5[0] = (byte)'I';
            imd5[1] = (byte)'M';
            imd5[2] = (byte)'D';
            imd5[3] = (byte)'5';

            byte[] size = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(u8archive.Length));
            byte[] hash = md5.ComputeHash(u8archive, 0, u8archive.Length);

            ms.Seek(0, SeekOrigin.Begin);
            ms.Write(imd5, 0, imd5.Length);
            ms.Write(size, 0, size.Length);

            ms.Seek(0x10, SeekOrigin.Begin);
            ms.Write(hash, 0, hash.Length);

            ms.Write(u8archive, 0, u8archive.Length);

            md5.Clear();
            return ms.ToArray();
        }

        /// <summary>
        /// Adds an IMET Header to the given 00.app
        /// </summary>
        /// <param name="u8archive"></param>
        /// <param name="channeltitles">Order: Jap, Eng, Ger, Fra, Spa, Ita, Dut</param>
        /// <param name="sizes">Order: Banner.bin, Icon.bin, Sound.bin</param>
        /// <returns></returns>
        public static byte[] AddHeaderIMET(byte[] nullapp, string[] channeltitles, int[] sizes)
        {
            if (channeltitles.Length < 7) return nullapp;
            for (int i = 0; i < channeltitles.Length; i++)
                if (channeltitles[i].Length > 20) return nullapp;

            MemoryStream ms = new MemoryStream();
            MD5 md5 = MD5.Create();

            byte[] imet = new byte[4];
            imet[0] = (byte)'I';
            imet[1] = (byte)'M';
            imet[2] = (byte)'E';
            imet[3] = (byte)'T';

            byte[] unknown = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(0x0000060000000003));

            byte[] iconsize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(sizes[1]));
            byte[] bannersize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(sizes[0]));
            byte[] soundsize = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(sizes[2]));

            byte[] japtitle = new byte[84];
            byte[] engtitle = new byte[84];
            byte[] gertitle = new byte[84];
            byte[] fratitle = new byte[84];
            byte[] spatitle = new byte[84];
            byte[] itatitle = new byte[84];
            byte[] duttitle = new byte[84];

            for (int i = 0; i < 20; i++)
            {
                if (channeltitles[0].Length > i)
                {
                    japtitle[i * 2] = BitConverter.GetBytes(channeltitles[0][i])[1];
                    japtitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[0][i])[0];
                }
                if (channeltitles[1].Length > i)
                {
                    engtitle[i * 2] = BitConverter.GetBytes(channeltitles[1][i])[1];
                    engtitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[1][i])[0];
                }
                if (channeltitles[2].Length > i)
                {
                    gertitle[i * 2] = BitConverter.GetBytes(channeltitles[2][i])[1];
                    gertitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[2][i])[0];
                }
                if (channeltitles[3].Length > i)
                {
                    fratitle[i * 2] = BitConverter.GetBytes(channeltitles[3][i])[1];
                    fratitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[3][i])[0];
                }
                if (channeltitles[4].Length > i)
                {
                    spatitle[i * 2] = BitConverter.GetBytes(channeltitles[4][i])[1];
                    spatitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[4][i])[0];
                }
                if (channeltitles[5].Length > i)
                {
                    itatitle[i * 2] = BitConverter.GetBytes(channeltitles[5][i])[1];
                    itatitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[5][i])[0];
                }
                if (channeltitles[6].Length > i)
                {
                    duttitle[i * 2] = BitConverter.GetBytes(channeltitles[6][i])[1];
                    duttitle[i * 2 + 1] = BitConverter.GetBytes(channeltitles[6][i])[0];
                }
            }

            byte[] crypto = new byte[16];

            ms.Seek(128, SeekOrigin.Begin);
            ms.Write(imet, 0, imet.Length);
            ms.Write(unknown, 0, unknown.Length);
            ms.Write(iconsize, 0, iconsize.Length);
            ms.Write(bannersize, 0, bannersize.Length);
            ms.Write(soundsize, 0, soundsize.Length);

            ms.Seek(4, SeekOrigin.Current);
            ms.Write(japtitle, 0, japtitle.Length);
            ms.Write(engtitle, 0, engtitle.Length);
            ms.Write(gertitle, 0, gertitle.Length);
            ms.Write(fratitle, 0, fratitle.Length);
            ms.Write(spatitle, 0, spatitle.Length);
            ms.Write(itatitle, 0, itatitle.Length);
            ms.Write(duttitle, 0, duttitle.Length);

            ms.Seek(0x348, SeekOrigin.Current);
            ms.Write(crypto, 0, crypto.Length);

            byte[] tohash = ms.ToArray();
            crypto = md5.ComputeHash(tohash, 0x40, 0x600);

            ms.Seek(-16, SeekOrigin.Current);
            ms.Write(crypto, 0, crypto.Length);
            ms.Write(nullapp, 0, nullapp.Length);

            md5.Clear();
            return ms.ToArray();
        }

        /// <summary>
        /// Packs a U8 Archive
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="outfile"></param>
        public static void PackU8(string folder, string outfile)
        {
            byte[] u8 = PackU8(folder);

            using (FileStream fs = new FileStream(outfile, FileMode.Create))
                fs.Write(u8, 0, u8.Length);
        }

        /// <summary>
        /// Packs a U8 Archive
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="outfile"></param>
        public static void PackU8(string folder, string outfile, bool addimd5header)
        {
            byte[] u8 = PackU8(folder);

            if (addimd5header == true)
                u8 = AddHeaderIMD5(u8);

            using (FileStream fs = new FileStream(outfile, FileMode.Create))
                fs.Write(u8, 0, u8.Length);
        }

        /// <summary>
        /// Packs a U8 Archive
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="outfile"></param>
        public static byte[] PackU8(string folder)
        {
            int a = 0, b = 0, c = 0;
            return PackU8(folder, out a, out b, out c);
        }

        /// <summary>
        /// Packs a U8 Archive
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="outfile"></param>
        public static byte[] PackU8(string folder, bool addimd5header)
        {
            byte[] u8 = PackU8(folder);

            if (addimd5header == true)
                u8 = AddHeaderIMD5(u8);

            return u8;
        }

        /// <summary>
        /// Packs a U8 Archive
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="outfile"></param>
        public static byte[] PackU8(string folder, out int bannersize, out int iconsize, out int soundsize)
        {
            int datapad = 32, stringtablepad = 32; //Biggie seems to use these paddings, so let's do it, too ;)
            string rootpath = folder;
            if (rootpath[rootpath.Length - 1] != '\\') rootpath = rootpath + "\\";

            bannersize = 0; iconsize = 0; soundsize = 0;

            string[] files = GetDirContent(folder, true);
            int nodecount = files.Length + 1; //All files and dirs + rootnode
            int recursion = 0;
            int currentnodes = 0;
            string name = string.Empty;
            string stringtable = "\0";
            byte[] tempnode = new byte[12];

            MemoryStream nodes = new MemoryStream();
            MemoryStream data = new MemoryStream();
            BinaryWriter writedata = new BinaryWriter(data);

            tempnode[0] = 0x01;
            tempnode[1] = 0x00;
            tempnode[2] = 0x00;
            tempnode[3] = 0x00;
            tempnode[4] = 0x00;
            tempnode[5] = 0x00;
            tempnode[6] = 0x00;
            tempnode[7] = 0x00;

            byte[] temp = BitConverter.GetBytes((UInt32)files.Length + 1); Array.Reverse(temp);
            tempnode[8] = temp[0];
            tempnode[9] = temp[1];
            tempnode[10] = temp[2];
            tempnode[11] = temp[3];

            nodes.Write(tempnode, 0, tempnode.Length);

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Remove(0, rootpath.Length - 1);

                recursion = Tools.CountCharsInString(files[i], '\\') - 1;
                name = files[i].Remove(0, files[i].LastIndexOf('\\') + 1);

                byte[] temp1 = BitConverter.GetBytes((UInt16)stringtable.Length); Array.Reverse(temp1);
                tempnode[2] = temp1[0];
                tempnode[3] = temp1[1];

                stringtable += name + "\0";

                if (Directory.Exists(rootpath + files[i])) //It's a dir
                {
                    tempnode[0] = 0x01;
                    tempnode[1] = 0x00;

                    byte[] temp2 = BitConverter.GetBytes((UInt32)recursion); Array.Reverse(temp2);
                    tempnode[4] = temp2[0];
                    tempnode[5] = temp2[1];
                    tempnode[6] = temp2[2];
                    tempnode[7] = temp2[3];

                    int size = currentnodes + 1;

                    for (int j = i; j < files.Length; j++)
                        if (files[j].Contains(files[i])) size++;

                    byte[] temp3 = BitConverter.GetBytes((UInt32)size); Array.Reverse(temp3);
                    tempnode[8] = temp3[0];
                    tempnode[9] = temp3[1];
                    tempnode[10] = temp3[2];
                    tempnode[11] = temp3[3];
                }
                else //It's a file
                {
                    byte[] tempfile = new byte[0x40];
                    int lzoffset = -1;

                    if (files[i].EndsWith("banner.bin"))
                    {
                        tempfile = Wii.Tools.LoadFileToByteArray(rootpath + files[i], 0, tempfile.Length);

                        for (int x = 0; x < tempfile.Length; x++)
                        {
                            if (tempfile[x] == 'L')
                                if (tempfile[x + 1] == 'Z')
                                    if (tempfile[x + 2] == '7')
                                        if (tempfile[x + 3] == '7')
                                        {
                                            lzoffset = x;
                                            break;
                                        }
                        }

                        if (lzoffset != -1)
                        {
                            bannersize = BitConverter.ToInt32(new byte[] { tempfile[lzoffset + 5], tempfile[lzoffset + 6], tempfile[lzoffset + 7], tempfile[lzoffset + 8] }, 0);
                        }
                        else
                        {
                            FileInfo fibanner = new FileInfo(rootpath + files[i]);
                            bannersize = (int)fibanner.Length - 32;
                        }
                    }
                    else if (files[i].EndsWith("icon.bin"))
                    {
                        tempfile = Wii.Tools.LoadFileToByteArray(rootpath + files[i], 0, tempfile.Length);

                        for (int x = 0; x < tempfile.Length; x++)
                        {
                            if (tempfile[x] == 'L')
                                if (tempfile[x + 1] == 'Z')
                                    if (tempfile[x + 2] == '7')
                                        if (tempfile[x + 3] == '7')
                                        {
                                            lzoffset = x;
                                        }
                        }

                        if (lzoffset != -1)
                        {
                            iconsize = BitConverter.ToInt32(new byte[] { tempfile[lzoffset + 5], tempfile[lzoffset + 6], tempfile[lzoffset + 7], tempfile[lzoffset + 8] }, 0);
                        }
                        else
                        {
                            FileInfo fiicon = new FileInfo(rootpath + files[i]);
                            iconsize = (int)fiicon.Length - 32;
                        }
                    }
                    else if (files[i].EndsWith("sound.bin"))
                    {
                        tempfile = Wii.Tools.LoadFileToByteArray(rootpath + files[i], 0, tempfile.Length);

                        for (int x = 0; x < tempfile.Length; x++)
                        {
                            if (tempfile[x] == 'L')
                                if (tempfile[x + 1] == 'Z')
                                    if (tempfile[x + 2] == '7')
                                        if (tempfile[x + 3] == '7')
                                        {
                                            lzoffset = x;
                                            break;
                                        }
                        }

                        if (lzoffset != -1)
                        {
                            soundsize = BitConverter.ToInt32(new byte[] { tempfile[lzoffset + 5], tempfile[lzoffset + 6], tempfile[lzoffset + 7], tempfile[lzoffset + 8] }, 0);
                        }
                        else
                        {
                            FileInfo fisound = new FileInfo(rootpath + files[i]);
                            soundsize = (int)fisound.Length - 32;
                        }
                    }

                    tempnode[0] = 0x00;
                    tempnode[1] = 0x00;

                    byte[] temp2 = BitConverter.GetBytes((UInt32)data.Position); Array.Reverse(temp2);
                    tempnode[4] = temp2[0];
                    tempnode[5] = temp2[1];
                    tempnode[6] = temp2[2];
                    tempnode[7] = temp2[3];

                    FileInfo fi = new FileInfo(rootpath + files[i]);
                    byte[] temp3 = BitConverter.GetBytes((UInt32)fi.Length); Array.Reverse(temp3);
                    tempnode[8] = temp3[0];
                    tempnode[9] = temp3[1];
                    tempnode[10] = temp3[2];
                    tempnode[11] = temp3[3];

                    using (FileStream fs = new FileStream(rootpath + files[i], FileMode.Open))
                    using (BinaryReader br = new BinaryReader(fs))
                        writedata.Write(br.ReadBytes((int)br.BaseStream.Length));

                    writedata.Seek(Tools.AddPadding((int)data.Position, datapad), SeekOrigin.Begin);
                }

                nodes.Write(tempnode, 0, tempnode.Length);
                currentnodes++;
            }

            byte[] type = new byte[2];
            byte[] curpos = new byte[4];

            for (int x = 0; x < nodecount * 12; x += 12)
            {
                nodes.Seek(x, SeekOrigin.Begin);
                nodes.Read(type, 0, 2);

                if (type[0] == 0x00 && type[1] == 0x00)
                {
                    nodes.Seek(x + 4, SeekOrigin.Begin);
                    nodes.Read(curpos, 0, 4);
                    Array.Reverse(curpos);

                    UInt32 newpos = BitConverter.ToUInt32(curpos, 0) + (UInt32)(Tools.AddPadding(0x20 + ((12 * nodecount) + stringtable.Length), stringtablepad));

                    nodes.Seek(x + 4, SeekOrigin.Begin);
                    byte[] temp2 = BitConverter.GetBytes(newpos); Array.Reverse(temp2);
                    nodes.Write(temp2, 0, 4);
                }
            }

            writedata.Close();
            MemoryStream output = new MemoryStream();
            BinaryWriter writeout = new BinaryWriter(output);

            writeout.Write((UInt32)0x2d38aa55);
            writeout.Write(IPAddress.HostToNetworkOrder((ushort)0x20));
            writeout.Write(IPAddress.HostToNetworkOrder((ushort)((12 * nodecount) + stringtable.Length)));
            writeout.Write(IPAddress.HostToNetworkOrder((ushort)(Tools.AddPadding(0x20 + ((12 * nodecount) + stringtable.Length), stringtablepad))));

            writeout.Seek(0x10, SeekOrigin.Current);

            writeout.Write(nodes.ToArray());
            writeout.Write(ASCIIEncoding.ASCII.GetBytes(stringtable));

            writeout.Seek(Tools.AddPadding(0x20 + ((12 * nodecount) + stringtable.Length), stringtablepad), SeekOrigin.Begin);

            writeout.Write(data.ToArray());

            output.Seek(0, SeekOrigin.End);
            for (int i = (int)output.Position; i < Tools.AddPadding((int)output.Position, datapad); i++)
                output.WriteByte(0);

            writeout.Close();
            output.Close();

            return output.ToArray();
        }

        /// <summary>
        /// Unpacks the given U8 archive
        /// If the archive is Lz77 compressed, it will be decompressed first!
        /// </summary>
        /// <param name="u8archive"></param>
        /// <param name="unpackpath"></param>
        public static void UnpackU8(string u8archive, string unpackpath)
        {
            byte[] u8 = Wii.Tools.LoadFileToByteArray(u8archive);
            UnpackU8(u8, unpackpath);
        }

        /// <summary>
        /// Unpacks the given U8 archive
        /// If the archive is Lz77 compressed, it will be decompressed first!
        /// </summary>
        /// <param name="u8archive"></param>
        /// <param name="unpackpath"></param>
        public static void UnpackU8(byte[] u8archive, string unpackpath)
        {
            int lz77offset = Lz77.GetLz77Offset(u8archive);
            if (lz77offset != -1) { u8archive = Lz77.Decompress(u8archive, lz77offset); }

            if (unpackpath[unpackpath.Length - 1] != '\\') { unpackpath = unpackpath + "\\"; }
            if (!Directory.Exists(unpackpath)) Directory.CreateDirectory(unpackpath);

            int u8offset = -1;
            int length = 2500;
            if (u8archive.Length < length) length = u8archive.Length - 4;

            for (int i = 0; i < length; i++)
            {
                if (u8archive[i] == 0x55 && u8archive[i + 1] == 0xAA && u8archive[i + 2] == 0x38 && u8archive[i + 3] == 0x2D)
                {
                    u8offset = i;
                    break;
                }
            }

            if (u8offset == -1) throw new Exception("File is not a valid U8 Archive!");

            int nodecount = Tools.HexStringToInt(u8archive[u8offset + 0x28].ToString("x2") + u8archive[u8offset + 0x29].ToString("x2") + u8archive[u8offset + 0x2a].ToString("x2") + u8archive[u8offset + 0x2b].ToString("x2"));
            int nodeoffset = 0x20;

            string[,] nodes = new string[nodecount, 5];

            for (int j = 0; j < nodecount; j++)
            {
                nodes[j, 0] = u8archive[u8offset + nodeoffset].ToString("x2") + u8archive[u8offset + nodeoffset + 1].ToString("x2");
                nodes[j, 1] = u8archive[u8offset + nodeoffset + 2].ToString("x2") + u8archive[u8offset + nodeoffset + 3].ToString("x2");
                nodes[j, 2] = u8archive[u8offset + nodeoffset + 4].ToString("x2") + u8archive[u8offset + nodeoffset + 5].ToString("x2") + u8archive[u8offset + nodeoffset + 6].ToString("x2") + u8archive[u8offset + nodeoffset + 7].ToString("x2");
                nodes[j, 3] = u8archive[u8offset + nodeoffset + 8].ToString("x2") + u8archive[u8offset + nodeoffset + 9].ToString("x2") + u8archive[u8offset + nodeoffset + 10].ToString("x2") + u8archive[u8offset + nodeoffset + 11].ToString("x2");

                nodeoffset += 12;
            }

            int stringtablepos = u8offset + nodeoffset;

            for (int x = 0; x < nodecount; x++)
            {
                bool end = false;
                int nameoffset = Tools.HexStringToInt(nodes[x, 1]);
                string thisname = "";

                do
                {
                    if (u8archive[stringtablepos + nameoffset] != 0x00)
                    {
                        char tempchar = Convert.ToChar(u8archive[stringtablepos + nameoffset]);
                        thisname += tempchar.ToString();
                        nameoffset++;
                    }
                    else end = true;
                } while (end == false);

                nodes[x, 4] = thisname;
            }

            string[] dirs = new string[nodecount];
            dirs[0] = unpackpath;
            int[] dircount = new int[nodecount];
            int dirindex = 0;

            try
            {
                for (int y = 1; y < nodecount; y++)
                {
                    switch (nodes[y, 0])
                    {
                        case "0100":
                            if (dirs[dirindex][dirs[dirindex].Length - 1] != '\\') { dirs[dirindex] = dirs[dirindex] + "\\"; }
                            Directory.CreateDirectory(dirs[dirindex] + nodes[y, 4]);
                            dirs[dirindex + 1] = dirs[dirindex] + nodes[y, 4];
                            dirindex++;
                            dircount[dirindex] = Tools.HexStringToInt(nodes[y, 3]);
                            break;
                        default:
                            int filepos = u8offset + Tools.HexStringToInt(nodes[y, 2]);
                            int filesize = Tools.HexStringToInt(nodes[y, 3]);

                            using (FileStream fs = new FileStream(dirs[dirindex] + "\\" + nodes[y, 4], FileMode.Create))
                            {
                                fs.Write(u8archive, filepos, filesize);
                            }
                            break;
                    }

                    while (dirindex > 0 && dircount[dirindex] == (y + 1))
                    {
                        dirindex--;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Gets the Banner.bin out of the 00000000.app
        /// </summary>
        /// <param name="nullapp"></param>
        /// <returns></returns>
        public static byte[] GetBannerBin(byte[] nullapp)
        {
            int lz77offset = Lz77.GetLz77Offset(nullapp);
            if (lz77offset != -1) { nullapp = Lz77.Decompress(nullapp, lz77offset); }

            int u8offset = -1;
            for (int i = 0; i < 2500; i++)
            {
                if (nullapp[i] == 0x55 && nullapp[i + 1] == 0xAA && nullapp[i + 2] == 0x38 && nullapp[i + 3] == 0x2D)
                {
                    u8offset = i;
                    break;
                }
            }

            if (u8offset == -1) throw new Exception("File is not a valid U8 Archive!");

            int nodecount = Tools.HexStringToInt(nullapp[u8offset + 0x28].ToString("x2") + nullapp[u8offset + 0x29].ToString("x2") + nullapp[u8offset + 0x2a].ToString("x2") + nullapp[u8offset + 0x2b].ToString("x2"));
            int nodeoffset = 0x20;

            string[,] nodes = new string[nodecount, 5];

            for (int j = 0; j < nodecount; j++)
            {
                nodes[j, 0] = nullapp[u8offset + nodeoffset].ToString("x2") + nullapp[u8offset + nodeoffset + 1].ToString("x2");
                nodes[j, 1] = nullapp[u8offset + nodeoffset + 2].ToString("x2") + nullapp[u8offset + nodeoffset + 3].ToString("x2");
                nodes[j, 2] = nullapp[u8offset + nodeoffset + 4].ToString("x2") + nullapp[u8offset + nodeoffset + 5].ToString("x2") + nullapp[u8offset + nodeoffset + 6].ToString("x2") + nullapp[u8offset + nodeoffset + 7].ToString("x2");
                nodes[j, 3] = nullapp[u8offset + nodeoffset + 8].ToString("x2") + nullapp[u8offset + nodeoffset + 9].ToString("x2") + nullapp[u8offset + nodeoffset + 10].ToString("x2") + nullapp[u8offset + nodeoffset + 11].ToString("x2");

                nodeoffset += 12;
            }

            int stringtablepos = u8offset + nodeoffset;

            for (int x = 0; x < nodecount; x++)
            {
                bool end = false;
                int nameoffset = Tools.HexStringToInt(nodes[x, 1]);
                string thisname = "";

                while (end == false)
                {
                    if (nullapp[stringtablepos + nameoffset] != 0x00)
                    {
                        char tempchar = Convert.ToChar(nullapp[stringtablepos + nameoffset]);
                        thisname += tempchar.ToString();
                        nameoffset++;
                    }
                    else end = true;
                }

                nodes[x, 4] = thisname;
            }

            for (int y = 1; y < nodecount; y++)
            {
                if (nodes[y, 4] == "banner.bin")
                {
                    int filepos = u8offset + Tools.HexStringToInt(nodes[y, 2]);
                    int filesize = Tools.HexStringToInt(nodes[y, 3]);

                    MemoryStream ms = new MemoryStream(nullapp);
                    byte[] banner = new byte[filesize];
                    ms.Seek(filepos, SeekOrigin.Begin);
                    ms.Read(banner, 0, filesize);
                    ms.Close();

                    return banner;
                }
            }

            throw new Exception("This file doesn't contain banner.bin!");
        }

        /// <summary>
        /// Gets the Icon.bin out of the 00000000.app
        /// </summary>
        /// <param name="nullapp"></param>
        /// <returns></returns>
        public static byte[] GetIconBin(byte[] nullapp)
        {
            int lz77offset = Lz77.GetLz77Offset(nullapp);
            if (lz77offset != -1) { nullapp = Lz77.Decompress(nullapp, lz77offset); }

            int u8offset = -1;
            for (int i = 0; i < 2500; i++)
            {
                if (nullapp[i] == 0x55 && nullapp[i + 1] == 0xAA && nullapp[i + 2] == 0x38 && nullapp[i + 3] == 0x2D)
                {
                    u8offset = i;
                    break;
                }
            }

            if (u8offset == -1) throw new Exception("File is not a valid U8 Archive!");

            int nodecount = Tools.HexStringToInt(nullapp[u8offset + 0x28].ToString("x2") + nullapp[u8offset + 0x29].ToString("x2") + nullapp[u8offset + 0x2a].ToString("x2") + nullapp[u8offset + 0x2b].ToString("x2"));
            int nodeoffset = 0x20;

            string[,] nodes = new string[nodecount, 5];

            for (int j = 0; j < nodecount; j++)
            {
                nodes[j, 0] = nullapp[u8offset + nodeoffset].ToString("x2") + nullapp[u8offset + nodeoffset + 1].ToString("x2");
                nodes[j, 1] = nullapp[u8offset + nodeoffset + 2].ToString("x2") + nullapp[u8offset + nodeoffset + 3].ToString("x2");
                nodes[j, 2] = nullapp[u8offset + nodeoffset + 4].ToString("x2") + nullapp[u8offset + nodeoffset + 5].ToString("x2") + nullapp[u8offset + nodeoffset + 6].ToString("x2") + nullapp[u8offset + nodeoffset + 7].ToString("x2");
                nodes[j, 3] = nullapp[u8offset + nodeoffset + 8].ToString("x2") + nullapp[u8offset + nodeoffset + 9].ToString("x2") + nullapp[u8offset + nodeoffset + 10].ToString("x2") + nullapp[u8offset + nodeoffset + 11].ToString("x2");

                nodeoffset += 12;
            }

            int stringtablepos = u8offset + nodeoffset;

            for (int x = 0; x < nodecount; x++)
            {
                bool end = false;
                int nameoffset = Tools.HexStringToInt(nodes[x, 1]);
                string thisname = "";

                while (end == false)
                {
                    if (nullapp[stringtablepos + nameoffset] != 0x00)
                    {
                        char tempchar = Convert.ToChar(nullapp[stringtablepos + nameoffset]);
                        thisname += tempchar.ToString();
                        nameoffset++;
                    }
                    else end = true;
                }

                nodes[x, 4] = thisname;
            }

            for (int y = 1; y < nodecount; y++)
            {
                if (nodes[y, 4] == "icon.bin")
                {
                    int filepos = u8offset + Tools.HexStringToInt(nodes[y, 2]);
                    int filesize = Tools.HexStringToInt(nodes[y, 3]);

                    MemoryStream ms = new MemoryStream(nullapp);
                    byte[] icon = new byte[filesize];
                    ms.Seek(filepos, SeekOrigin.Begin);
                    ms.Read(icon, 0, filesize);
                    ms.Close();

                    return icon;
                }
            }

            throw new Exception("This file doesn't contain icon.bin!");
        }

        /// <summary>
        /// Extracts all Tpl's to the given path
        /// </summary>
        /// <param name="u8archive"></param>
        /// <param name="path"></param>
        public static void UnpackTpls(byte[] u8archive, string unpackpath)
        {
            int lz77offset = Lz77.GetLz77Offset(u8archive);
            if (lz77offset != -1) { u8archive = Lz77.Decompress(u8archive, lz77offset); }

            if (unpackpath[unpackpath.Length - 1] != '\\') { unpackpath = unpackpath + "\\"; }
            if (!Directory.Exists(unpackpath)) Directory.CreateDirectory(unpackpath);

            int u8offset = -1;
            int length = 2500;
            if (u8archive.Length < 2500) length = u8archive.Length - 4;

            for (int i = 0; i < 2500; i++)
            {
                if (u8archive[i] == 0x55 && u8archive[i + 1] == 0xAA && u8archive[i + 2] == 0x38 && u8archive[i + 3] == 0x2D)
                {
                    u8offset = i;
                    break;
                }
            }

            if (u8offset == -1) throw new Exception("File is not a valid U8 Archive!");

            int nodecount = Tools.HexStringToInt(u8archive[u8offset + 0x28].ToString("x2") + u8archive[u8offset + 0x29].ToString("x2") + u8archive[u8offset + 0x2a].ToString("x2") + u8archive[u8offset + 0x2b].ToString("x2"));
            int nodeoffset = 0x20;

            string[,] nodes = new string[nodecount, 5];

            for (int j = 0; j < nodecount; j++)
            {
                nodes[j, 0] = u8archive[u8offset + nodeoffset].ToString("x2") + u8archive[u8offset + nodeoffset + 1].ToString("x2");
                nodes[j, 1] = u8archive[u8offset + nodeoffset + 2].ToString("x2") + u8archive[u8offset + nodeoffset + 3].ToString("x2");
                nodes[j, 2] = u8archive[u8offset + nodeoffset + 4].ToString("x2") + u8archive[u8offset + nodeoffset + 5].ToString("x2") + u8archive[u8offset + nodeoffset + 6].ToString("x2") + u8archive[u8offset + nodeoffset + 7].ToString("x2");
                nodes[j, 3] = u8archive[u8offset + nodeoffset + 8].ToString("x2") + u8archive[u8offset + nodeoffset + 9].ToString("x2") + u8archive[u8offset + nodeoffset + 10].ToString("x2") + u8archive[u8offset + nodeoffset + 11].ToString("x2");

                nodeoffset += 12;
            }

            int stringtablepos = u8offset + nodeoffset;

            for (int x = 0; x < nodecount; x++)
            {
                bool end = false;
                int nameoffset = Tools.HexStringToInt(nodes[x, 1]);
                string thisname = "";

                while (end == false)
                {
                    if (u8archive[stringtablepos + nameoffset] != 0x00)
                    {
                        char tempchar = Convert.ToChar(u8archive[stringtablepos + nameoffset]);
                        thisname += tempchar.ToString();
                        nameoffset++;
                    }
                    else end = true;
                }

                nodes[x, 4] = thisname;
            }

            for (int y = 1; y < nodecount; y++)
            {
                if (nodes[y, 4].Contains("."))
                {
                    if (nodes[y, 4].Remove(0, nodes[y, 4].LastIndexOf('.')) == ".tpl")
                    {
                        int filepos = u8offset + Tools.HexStringToInt(nodes[y, 2]);
                        int filesize = Tools.HexStringToInt(nodes[y, 3]);

                        using (FileStream fs = new FileStream(unpackpath + nodes[y, 4], FileMode.Create))
                        {
                            fs.Write(u8archive, filepos, filesize);
                        }
                    }
                }
            }
        }
    }

    public class Lz77
    {
        private const int N = 4096;
        private const int F = 18;
        private const int threshold = 2;
        private static int[] lson = new int[N + 1];
        private static int[] rson = new int[N + 257];
        private static int[] dad = new int[N + 1];
        private static ushort[] text_buf = new ushort[N + 17];
        private static int match_position = 0, match_length = 0;
        private static int textsize = 0;
        private static int codesize = 0;

        /// <summary>
        /// Returns the Offset to the Lz77 Header
        /// -1 will be returned, if the file is not Lz77 compressed
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetLz77Offset(byte[] data)
        {
            int length = 5000;
            if (data.Length < 5000) length = data.Length - 4;

            for (int i = 0; i < length; i++)
            {
                if (data[i] == 0x55 && data[i + 1] == 0xAA && data[i + 2] == 0x38 && data[i + 3] == 0x2D)
                {
                    break;
                }

                UInt32 tmp = BitConverter.ToUInt32(data, i);
                if (tmp == 0x37375a4c) return i;
            }

            return -1;
        }

        /// <summary>
        /// Decompresses the given file
        /// </summary>
        /// <param name="infile"></param>
        /// <param name="outfile"></param>
        public static void Decompress(string infile, string outfile)
        {
            byte[] input = Tools.LoadFileToByteArray(infile);
            int offset = GetLz77Offset(input);
            if (offset == -1) throw new Exception("File is not Lz77 compressed!");
            Tools.SaveFileFromByteArray(Decompress(input, offset), outfile);
        }

        /// <summary>
        /// Decompresses the given data
        /// </summary>
        /// <param name="compressed"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] compressed, int offset)
        {
            int i, j, k, r, c, z;
            uint flags;
            UInt32 decomp_size;
            UInt32 cur_size = 0;

            MemoryStream infile = new MemoryStream(compressed);
            MemoryStream outfile = new MemoryStream();

            UInt32 gbaheader = new UInt32();
            byte[] temp = new byte[4];
            infile.Seek(offset + 4, SeekOrigin.Begin);
            infile.Read(temp, 0, 4);
            gbaheader = BitConverter.ToUInt32(temp, 0);

            decomp_size = gbaheader >> 8;
            byte[] text_buf = new byte[N + 17];

            for (i = 0; i < N - F; i++) text_buf[i] = 0xdf;
            r = N - F; flags = 7; z = 7;

            while (true)
            {
                flags <<= 1;
                z++;
                if (z == 8)
                {
                    if ((c = (char)infile.ReadByte()) == -1) break;
                    flags = (uint)c;
                    z = 0;
                }
                if ((flags & 0x80) == 0)
                {
                    if ((c = infile.ReadByte()) == infile.Length - 1) break;
                    if (cur_size < decomp_size) outfile.WriteByte((byte)c);
                    text_buf[r++] = (byte)c;
                    r &= (N - 1);
                    cur_size++;
                }
                else
                {
                    if ((i = infile.ReadByte()) == -1) break;
                    if ((j = infile.ReadByte()) == -1) break;
                    j = j | ((i << 8) & 0xf00);
                    i = ((i >> 4) & 0x0f) + threshold;
                    for (k = 0; k <= i; k++)
                    {
                        c = text_buf[(r - j - 1) & (N - 1)];
                        if (cur_size < decomp_size) outfile.WriteByte((byte)c); text_buf[r++] = (byte)c; r &= (N - 1); cur_size++;
                    }
                }
            }

            return outfile.ToArray();
        }

        public static void InitTree()
        {
            int i;
            for (i = N + 1; i <= N + 256; i++) rson[i] = N;
            for (i = 0; i < N; i++) dad[i] = N;
        }

        public static void InsertNode(int r)
        {
            int i, p, cmp;
            cmp = 1;
            p = N + 1 + (text_buf[r] == 0xffff ? 0 : text_buf[r]); //text_buf[r];
            rson[r] = lson[r] = N; match_length = 0;
            for (; ; )
            {
                if (cmp >= 0)
                {
                    if (rson[p] != N) p = rson[p];
                    else { rson[p] = r; dad[r] = p; return; }
                }
                else
                {
                    if (lson[p] != N) p = lson[p];
                    else { lson[p] = r; dad[r] = p; return; }
                }
                for (i = 1; i < F; i++)
                    if ((cmp = text_buf[r + i] - text_buf[p + i]) != 0) break;
                if (i > match_length)
                {
                    match_position = p;
                    if ((match_length = i) >= F) break;
                }
            }
            dad[r] = dad[p]; lson[r] = lson[p]; rson[r] = rson[p];
            dad[lson[p]] = r; dad[rson[p]] = r;
            if (rson[dad[p]] == p) rson[dad[p]] = r;
            else lson[dad[p]] = r;
            dad[p] = N;
        }

        public static void DeleteNode(int p)
        {
            int q;

            if (dad[p] == N) return;  /* not in tree */
            if (rson[p] == N) q = lson[p];
            else if (lson[p] == N) q = rson[p];
            else
            {
                q = lson[p];
                if (rson[q] != N)
                {
                    do { q = rson[q]; } while (rson[q] != N);
                    rson[dad[q]] = lson[q]; dad[lson[q]] = dad[q];
                    lson[q] = lson[p]; dad[lson[p]] = q;
                }
                rson[q] = rson[p]; dad[rson[p]] = q;
            }
            dad[q] = dad[p];
            if (rson[dad[p]] == p) rson[dad[p]] = q; else lson[dad[p]] = q;
            dad[p] = N;
        }

        /// <summary>
        /// Lz77 compresses the given File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static void Compress(string infile, string outfile)
        {
            byte[] thisfile = Tools.LoadFileToByteArray(infile);
            thisfile = Compress(thisfile);
            Tools.SaveFileFromByteArray(thisfile, outfile);
        }

        /// <summary>
        /// Lz77 compresses the given and saves it to the given Path
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static void Compress(byte[] file, string outfile)
        {
            byte[] temp = Compress(file);
            Tools.SaveFileFromByteArray(temp, outfile);
        }

        /// <summary>
        /// Lz77 compresses the given Byte Array
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] file)
        {
            int i, c, len, r, s, last_match_length, code_buf_ptr;
            int[] code_buf = new int[17];
            int mask;
            UInt32 filesize = ((Convert.ToUInt32(file.Length)) << 8) + 0x10;
            byte[] filesizebytes = BitConverter.GetBytes(filesize);

            MemoryStream output = new MemoryStream();
            output.WriteByte((byte)'L'); output.WriteByte((byte)'Z'); output.WriteByte((byte)'7'); output.WriteByte((byte)'7');
            MemoryStream infile = new MemoryStream(file);

            output.Write(filesizebytes, 0, filesizebytes.Length);

            InitTree();
            code_buf[0] = 0;
            code_buf_ptr = 1;
            mask = 0x80;
            s = 0;
            r = N - F;

            for (i = s; i < r; i++) text_buf[i] = 0xffff;
            for (len = 0; len < F && (c = (int)infile.ReadByte()) != -1; len++)
                text_buf[r + len] = (ushort)c;

            if ((textsize = len) == 0) return file;

            for (i = 1; i <= F; i++) InsertNode(r - i);

            InsertNode(r);

            do
            {
                if (match_length > len) match_length = len;

                if (match_length <= threshold)
                {
                    match_length = 1;
                    code_buf[code_buf_ptr++] = text_buf[r];
                }
                else
                {
                    code_buf[0] |= mask;

                    code_buf[code_buf_ptr++] = (char)
                        (((r - match_position - 1) >> 8) & 0x0f) |
                        ((match_length - (threshold + 1)) << 4);

                    code_buf[code_buf_ptr++] = (char)((r - match_position - 1) & 0xff);
                }
                if ((mask >>= 1) == 0)
                {
                    for (i = 0; i < code_buf_ptr; i++)
                        output.WriteByte((byte)code_buf[i]);
                    codesize += code_buf_ptr;
                    code_buf[0] = 0; code_buf_ptr = 1;
                    mask = 0x80;
                }

                last_match_length = match_length;
                for (i = 0; i < last_match_length &&
                        (c = (int)infile.ReadByte()) != -1; i++)
                {
                    DeleteNode(s);
                    text_buf[s] = (ushort)c;
                    if (s < F - 1) text_buf[s + N] = (ushort)c;
                    s = (s + 1) & (N - 1); r = (r + 1) & (N - 1);
                    InsertNode(r);
                }

                while (i++ < last_match_length)
                {
                    DeleteNode(s);
                    s = (s + 1) & (N - 1); r = (r + 1) & (N - 1);
                    if (--len != 0) InsertNode(r);
                }
            } while (len > 0);


            if (code_buf_ptr > 1)
            {
                for (i = 0; i < code_buf_ptr; i++) output.WriteByte((byte)code_buf[i]);
                codesize += code_buf_ptr;
            }

            if (codesize % 4 != 0)
                for (i = 0; i < 4 - (codesize % 4); i++)
                    output.WriteByte(0x00);

            infile.Close();
            return output.ToArray();
        }
    }

    public class TPL
    {
        /// <summary>
        /// Fixes rough edges (artifacts), if necessary
        /// </summary>
        /// <param name="tplFile"></param>
        public static void FixFilter(string tplFile)
        {
            using (FileStream fs = new FileStream(tplFile, FileMode.Open))
            {
                fs.Seek(41, SeekOrigin.Begin);
                if (fs.ReadByte() == 0x01)
                {
                    fs.Seek(-1, SeekOrigin.Current);
                    fs.Write(new byte[] { 0x00, 0x00, 0x01 }, 0, 3);
                }

                fs.Seek(45, SeekOrigin.Begin);
                if (fs.ReadByte() == 0x01)
                {
                    fs.Seek(-1, SeekOrigin.Current);
                    fs.Write(new byte[] { 0x00, 0x00, 0x01 }, 0, 3);
                }
            }
        }

        /// <summary>
        /// Converts a Tpl to a Bitmap
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static Bitmap ConvertFromTPL(string tpl)
        {
            byte[] tplarray = Wii.Tools.LoadFileToByteArray(tpl);
            return ConvertFromTPL(tplarray);
        }

        /// <summary>
        /// Converts a Tpl to a Bitmap
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static Bitmap ConvertFromTPL(byte[] tpl)
        {
            if (GetTextureCount(tpl) > 1) throw new Exception("Tpl's containing more than one Texture are not supported!");

            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int format = GetTextureFormat(tpl);
            if (format == -1) throw new Exception("The Texture has an unsupported format!");

            switch (format)
            {
                case 0:
                    byte[] temp0 = FromI4(tpl);
                    return ConvertPixelToBitmap(temp0, width, height);
                case 1:
                    byte[] temp1 = FromI8(tpl);
                    return ConvertPixelToBitmap(temp1, width, height);
                case 2:
                    byte[] temp2 = FromIA4(tpl);
                    return ConvertPixelToBitmap(temp2, width, height);
                case 3:
                    byte[] temp3 = FromIA8(tpl);
                    return ConvertPixelToBitmap(temp3, width, height);
                case 4:
                    byte[] temp4 = FromRGB565(tpl);
                    return ConvertPixelToBitmap(temp4, width, height);
                case 5:
                    byte[] temp5 = FromRGB5A3(tpl);
                    return ConvertPixelToBitmap(temp5, width, height);
                case 6:
                    byte[] temp6 = FromRGBA8(tpl);
                    return ConvertPixelToBitmap(temp6, width, height);
                case 8:
                    byte[] temp8 = FromCI4(tpl);
                    return ConvertPixelToBitmap(temp8, width, height);
                case 9:
                    byte[] temp9 = FromCI8(tpl);
                    return ConvertPixelToBitmap(temp9, width, height);
                case 10:
                    byte[] temp10 = FromCI14X2(tpl);
                    return ConvertPixelToBitmap(temp10, width, height);
                case 14:
                    byte[] temp14 = FromCMP(tpl);
                    return ConvertPixelToBitmap(temp14, width, height);
                default:
                    throw new Exception("The Texture has an unsupported format!");
            }
        }

        /// <summary>
        /// Converts the Pixel Data into a Png Image
        /// </summary>
        /// <param name="data">Byte array with pixel data</param>
        public static System.Drawing.Bitmap ConvertPixelToBitmap(byte[] data, int width, int height)
        {
            if (width == 0) width = 1;
            if (height == 0) height = 1;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(
                                 new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                                 System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

            System.Runtime.InteropServices.Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        /// <summary>
        /// Gets the offset to the Texture Header
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureHeaderOffset(byte[] tpl)
        {
            byte[] tmp = new byte[] { tpl[15], tpl[14], tpl[13], tpl[12] };
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Gets the offset to the Texture Palette Header
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTexturePaletteHeaderOffset(byte[] tpl)
        {
            byte[] tmp = new byte[] { tpl[19], tpl[18], tpl[17], tpl[16] };
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Gets the offset to the Texture Palette
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTexturePaletteOffset(byte[] tpl)
        {
            int paletteheaderoffset = GetTexturePaletteHeaderOffset(tpl);

            byte[] tmp = new byte[] { tpl[paletteheaderoffset + 11],
                tpl[paletteheaderoffset + 10], tpl[paletteheaderoffset + 9], tpl[paletteheaderoffset + 8] };
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Gets the Texture Palette Format
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTexturePaletteFormat(byte[] tpl)
        {
            int paletteheaderoffset = GetTexturePaletteHeaderOffset(tpl);

            byte[] tmp = new byte[] { tpl[paletteheaderoffset + 7],
                tpl[paletteheaderoffset + 6], tpl[paletteheaderoffset + 5], tpl[paletteheaderoffset + 4] };
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Gets the item count of the Texture Palette
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTexturePaletteItemCount(byte[] tpl)
        {
            int paletteheaderoffset = GetTexturePaletteHeaderOffset(tpl);

            byte[] tmp = new byte[] { tpl[paletteheaderoffset + 1], tpl[paletteheaderoffset] };
            return BitConverter.ToInt16(tmp, 0);
        }

        /// <summary>
        /// Gets the Texture Palette of the TPL
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static uint[] GetTexturePalette(byte[] tpl)
        {
            int paletteoffset = GetTexturePaletteOffset(tpl);
            int paletteformat = GetTexturePaletteFormat(tpl);
            int itemcount = GetTexturePaletteItemCount(tpl);
            int r, g, b, a;

            uint[] output = new uint[itemcount];
            for (int i = 0; i < itemcount; i++)
            {
                if (i >= itemcount) continue;

                UInt16 pixel = BitConverter.ToUInt16(new byte[] { tpl[i * 2 + 1], tpl[i * 2] }, 0);

                if (paletteformat == 0) //IA8
                {
                    r = (pixel >> 8);
                    b = r;
                    g = r;
                    a = ((pixel >> 0) & 0xff);
                }
                else if (paletteformat == 1) //RGB565
                {
                    b = (((pixel >> 11) & 0x1F) << 3) & 0xff;
                    g = (((pixel >> 5) & 0x3F) << 2) & 0xff;
                    r = (((pixel >> 0) & 0x1F) << 3) & 0xff;
                    a = 255;
                }
                else //RGB5A3
                {
                    if ((pixel & (1 << 15)) != 0) //RGB555
                    {
                        a = 255;
                        b = (((pixel >> 10) & 0x1F) * 255) / 31;
                        g = (((pixel >> 5) & 0x1F) * 255) / 31;
                        r = (((pixel >> 0) & 0x1F) * 255) / 31;
                    }
                    else //RGB4A3
                    {
                        a = (((pixel >> 12) & 0x07) * 255) / 7;
                        b = (((pixel >> 8) & 0x0F) * 255) / 15;
                        g = (((pixel >> 4) & 0x0F) * 255) / 15;
                        r = (((pixel >> 0) & 0x0F) * 255) / 15;
                    }
                }

                output[i] = (uint)((r << 0) | (g << 8) | (b << 16) | (a << 24));
            }

            return output;
        }

        /// <summary>
        /// Gets the Number of Textures in a Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureCount(byte[] tpl)
        {
            byte[] tmp = new byte[4];
            tmp[3] = tpl[4];
            tmp[2] = tpl[5];
            tmp[1] = tpl[6];
            tmp[0] = tpl[7];
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Gets the Format of the Texture in the Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureFormat(string tpl)
        {
            byte[] temp = Tools.LoadFileToByteArray(tpl, 0, 512);
            return GetTextureFormat(temp);
        }

        /// <summary>
        /// Gets the Format of the Texture in the Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureFormat(byte[] tpl)
        {
            int offset = GetTextureHeaderOffset(tpl);

            byte[] tmp = new byte[4];
            tmp[3] = tpl[offset + 4];
            tmp[2] = tpl[offset + 5];
            tmp[1] = tpl[offset + 6];
            tmp[0] = tpl[offset + 7];
            UInt32 format = BitConverter.ToUInt32(tmp, 0);

            if (format == 0 ||
                format == 1 ||
                format == 2 ||
                format == 3 ||
                format == 4 ||
                format == 5 ||
                format == 6 ||
                format == 8 ||
                format == 9 ||
                format == 10 ||
                format == 14) return (int)format;

            else return -1; //Unsupported Format
        }

        /// <summary>
        /// Gets the Format Name of the Texture in the Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static string GetTextureFormatName(byte[] tpl)
        {
            switch (GetTextureFormat(tpl))
            {
                case 0:
                    return "I4";
                case 1:
                    return "I8";
                case 2:
                    return "IA4";
                case 3:
                    return "IA8";
                case 4:
                    return "RGB565";
                case 5:
                    return "RGB5A3";
                case 6:
                    return "RGBA8";
                case 8:
                    return "CI4";
                case 9:
                    return "CI8";
                case 10:
                    return "CI14X2";
                case 14:
                    return "CMP";
                default:
                    return "Unknown";
            }
        }

        public static int avg(int w0, int w1, int c0, int c1)
        {
            int a0 = c0 >> 11;
            int a1 = c1 >> 11;
            int a = (w0 * a0 + w1 * a1) / (w0 + w1);
            int c = (a << 11) & 0xffff;

            a0 = (c0 >> 5) & 63;
            a1 = (c1 >> 5) & 63;
            a = (w0 * a0 + w1 * a1) / (w0 + w1);
            c = c | ((a << 5) & 0xffff);

            a0 = c0 & 31;
            a1 = c1 & 31;
            a = (w0 * a0 + w1 * a1) / (w0 + w1);
            c = c | a;

            return c;
        }

        /// <summary>
        /// Gets the Width of the Texture in the Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureWidth(byte[] tpl)
        {
            int offset = GetTextureHeaderOffset(tpl);

            byte[] tmp = new byte[2];
            tmp[1] = tpl[offset + 2];
            tmp[0] = tpl[offset + 3];
            return BitConverter.ToInt16(tmp, 0);
        }

        /// <summary>
        /// Gets the Height of the Texture in the Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureHeight(byte[] tpl)
        {
            int offset = GetTextureHeaderOffset(tpl);

            byte[] tmp = new byte[2];
            tmp[1] = tpl[offset];
            tmp[0] = tpl[offset + 1];
            return BitConverter.ToInt16(tmp, 0);
        }

        /// <summary>
        /// Gets the offset to the Texturedata in the Tpl
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static int GetTextureOffset(byte[] tpl)
        {
            int offset = GetTextureHeaderOffset(tpl);

            byte[] tmp = new byte[4];
            tmp[3] = tpl[offset + 8];
            tmp[2] = tpl[offset + 9];
            tmp[1] = tpl[offset + 10];
            tmp[0] = tpl[offset + 11];
            return BitConverter.ToInt32(tmp, 0);
        }

        /// <summary>
        /// Converts RGBA8 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromRGBA8(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;
            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        for (int y1 = y; y1 < y + 4; y1++)
                        {
                            for (int x1 = x; x1 < x + 4; x1++)
                            {
                                byte[] pixelbytes = new byte[2];
                                pixelbytes[1] = tpl[offset + inp * 2];
                                pixelbytes[0] = tpl[offset + inp * 2 + 1];
                                UInt16 pixel = BitConverter.ToUInt16(pixelbytes, 0);
                                inp++;

                                if ((x1 >= width) || (y1 >= height))
                                    continue;

                                if (k == 0)
                                {
                                    int a = (pixel >> 8) & 0xff;
                                    int r = (pixel >> 0) & 0xff;
                                    output[x1 + (y1 * width)] |= (UInt32)((r << 16) | (a << 24));
                                }
                                else
                                {
                                    int g = (pixel >> 8) & 0xff;
                                    int b = (pixel >> 0) & 0xff;
                                    output[x1 + (y1 * width)] |= (UInt32)((g << 8) | (b << 0));
                                }
                            }
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts RGB5A3 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromRGB5A3(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;
            int r, g, b;
            int a = 0;
            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            byte[] pixelbytes = new byte[2];
                            pixelbytes[1] = tpl[offset + inp * 2];
                            pixelbytes[0] = tpl[offset + inp * 2 + 1];
                            UInt16 pixel = BitConverter.ToUInt16(pixelbytes, 0);
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            if ((pixel & (1 << 15)) != 0)
                            {
                                b = (((pixel >> 10) & 0x1F) * 255) / 31;
                                g = (((pixel >> 5) & 0x1F) * 255) / 31;
                                r = (((pixel >> 0) & 0x1F) * 255) / 31;
                                a = 255;
                            }
                            else
                            {
                                a = (((pixel >> 12) & 0x07) * 255) / 7;
                                b = (((pixel >> 8) & 0x0F) * 255) / 15;
                                g = (((pixel >> 4) & 0x0F) * 255) / 15;
                                r = (((pixel >> 0) & 0x0F) * 255) / 15;
                            }

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[(y1 * width) + x1] = (UInt32)rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts RGB565 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromRGB565(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;
            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            byte[] pixelbytes = new byte[2];
                            pixelbytes[1] = tpl[offset + inp * 2];
                            pixelbytes[0] = tpl[offset + inp * 2 + 1];
                            UInt16 pixel = BitConverter.ToUInt16(pixelbytes, 0);
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            int b = (((pixel >> 11) & 0x1F) << 3) & 0xff;
                            int g = (((pixel >> 5) & 0x3F) << 2) & 0xff;
                            int r = (((pixel >> 0) & 0x1F) << 3) & 0xff;
                            int a = 255;

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = (UInt32)rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts I4 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromI4(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 8)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 8; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1 += 2)
                        {
                            int pixel = tpl[offset + inp++];

                            if (y1 >= height || x1 >= width)
                                continue;

                            int r = (pixel >> 4) * 255 / 15;
                            int g = (pixel >> 4) * 255 / 15;
                            int b = (pixel >> 4) * 255 / 15;
                            int a = 255;

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = (UInt32)rgba;

                            r = (pixel & 0x0F) * 255 / 15;
                            g = (pixel & 0x0F) * 255 / 15;
                            b = (pixel & 0x0F) * 255 / 15;
                            a = 255;

                            rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1 + 1] = (UInt32)rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts IA4 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromIA4(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1++)
                        {
                            int pixel = tpl[offset + inp];
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            int r = ((pixel & 0x0F) * 255 / 15) & 0xff;
                            int g = ((pixel & 0x0F) * 255 / 15) & 0xff;
                            int b = ((pixel & 0x0F) * 255 / 15) & 0xff;
                            int a = (((pixel >> 4) * 255) / 15) & 0xff;

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = (UInt32)rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts I8 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromI8(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1++)
                        {
                            int pixel = tpl[offset + inp];
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            int r = pixel;
                            int g = pixel;
                            int b = pixel;
                            int a = 255;

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = (UInt32)rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts IA8 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromIA8(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int inp = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            byte[] pixelbytes = new byte[2];
                            pixelbytes[1] = tpl[offset + inp * 2];
                            pixelbytes[0] = tpl[offset + inp * 2 + 1];
                            UInt16 pixel = BitConverter.ToUInt16(pixelbytes, 0);
                            inp++;

                            if (y1 >= height || x1 >= width)
                                continue;

                            int r = (pixel >> 8);// &0xff;
                            int g = (pixel >> 8);// &0xff;
                            int b = (pixel >> 8);// &0xff;
                            int a = (pixel >> 0) & 0xff;

                            int rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = (UInt32)rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts CMP Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromCMP(byte[] tpl)
        {
            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            UInt16[] c = new UInt16[4];
            int[] pix = new int[4];
            int inp = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int ww = Tools.AddPadding(width, 8);

                    int x0 = x & 0x03;
                    int x1 = (x >> 2) & 0x01;
                    int x2 = x >> 3;

                    int y0 = y & 0x03;
                    int y1 = (y >> 2) & 0x01;
                    int y2 = y >> 3;

                    int off = (8 * x1) + (16 * y1) + (32 * x2) + (4 * ww * y2);

                    byte[] tmp1 = new byte[2];
                    tmp1[1] = tpl[offset + off];
                    tmp1[0] = tpl[offset + off + 1];
                    c[0] = BitConverter.ToUInt16(tmp1, 0);
                    tmp1[1] = tpl[offset + off + 2];
                    tmp1[0] = tpl[offset + off + 3];
                    c[1] = BitConverter.ToUInt16(tmp1, 0);

                    if (c[0] > c[1])
                    {
                        c[2] = (UInt16)avg(2, 1, c[0], c[1]);
                        c[3] = (UInt16)avg(1, 2, c[0], c[1]);
                    }
                    else
                    {
                        c[2] = (UInt16)avg(1, 1, c[0], c[1]);
                        c[3] = 0;
                    }

                    byte[] pixeldata = new byte[4];
                    pixeldata[3] = tpl[offset + off + 4];
                    pixeldata[2] = tpl[offset + off + 5];
                    pixeldata[1] = tpl[offset + off + 6];
                    pixeldata[0] = tpl[offset + off + 7];
                    UInt32 pixel = BitConverter.ToUInt32(pixeldata, 0);

                    int ix = x0 + (4 * y0);
                    int raw = c[(pixel >> (30 - (2 * ix))) & 0x03];

                    pix[0] = (raw >> 8) & 0xf8;
                    pix[1] = (raw >> 3) & 0xf8;
                    pix[2] = (raw << 3) & 0xf8;
                    pix[3] = 0xff;
                    if (((pixel >> (30 - (2 * ix))) & 0x03) == 3 && c[0] <= c[1]) pix[3] = 0x00;

                    int intout = (pix[0] << 16) | (pix[1] << 8) | (pix[2] << 0) | (pix[3] << 24);
                    output[inp] = (UInt32)intout;
                    inp++;
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts CI4 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromCI4(byte[] tpl)
        {
            uint[] palette = GetTexturePalette(tpl);

            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height + 1];
            int i = 0;

            for (int y = 0; y < height; y += 8)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 8; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1 += 2)
                        {
                            if (y1 >= height || x1 >= width)
                                continue;

                            UInt16 pixel = tpl[offset + i++];

                            uint r = ((palette[pixel >> 4] & 0xFF000000) >> 24);
                            uint g = (uint)((palette[pixel >> 4] & 0x00FF0000) >> 16);
                            uint b = (uint)((palette[pixel >> 4] & 0x0000FF00) >> 8);
                            uint a = (uint)((palette[pixel >> 4] & 0x000000FF) >> 0);

                            uint rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = rgba;

                            r = ((palette[pixel & 0x0F] & 0xFF000000) >> 24);
                            g = (uint)((palette[pixel & 0x0F] & 0x00FF0000) >> 16);
                            b = (uint)((palette[pixel & 0x0F] & 0x0000FF00) >> 8);
                            a = (uint)((palette[pixel & 0x0F] & 0x000000FF) >> 0);

                            rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);

                            output[y1 * width + x1 + 1] = rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts CI8 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromCI8(byte[] tpl)
        {
            uint[] palette = GetTexturePalette(tpl);

            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int i = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 8)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 8; x1++)
                        {
                            if (y1 >= height || x1 >= width)
                                continue;

                            UInt16 pixel = tpl[offset + i++];

                            uint r = ((palette[pixel] & 0xFF000000) >> 24);
                            uint g = (uint)((palette[pixel] & 0x00FF0000) >> 16);
                            uint b = (uint)((palette[pixel] & 0x0000FF00) >> 8);
                            uint a = (uint)((palette[pixel] & 0x000000FF) >> 0);

                            uint rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Converts CI14X2 Tpl Array to RGBA Byte Array
        /// </summary>
        /// <param name="tpl"></param>
        /// <returns></returns>
        public static byte[] FromCI14X2(byte[] tpl)
        {
            uint[] palette = GetTexturePalette(tpl);

            int width = GetTextureWidth(tpl);
            int height = GetTextureHeight(tpl);
            int offset = GetTextureOffset(tpl);
            UInt32[] output = new UInt32[width * height];
            int i = 0;

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    for (int y1 = y; y1 < y + 4; y1++)
                    {
                        for (int x1 = x; x1 < x + 4; x1++)
                        {
                            if (y1 >= height || x1 >= width)
                                continue;

                            UInt16 pixel = tpl[offset + i++];

                            uint r = ((palette[pixel & 0x3FFF] & 0xFF000000) >> 24);
                            uint g = (uint)((palette[pixel & 0x3FFF] & 0x00FF0000) >> 16);
                            uint b = (uint)((palette[pixel & 0x3FFF] & 0x0000FF00) >> 8);
                            uint a = (uint)((palette[pixel & 0x3FFF] & 0x000000FF) >> 0);

                            uint rgba = (r << 0) | (g << 8) | (b << 16) | (a << 24);
                            output[y1 * width + x1] = rgba;
                        }
                    }
                }
            }

            return Tools.UIntArrayToByteArray(output);
        }

        /// <summary>
        /// Gets the pixel data of a Bitmap as a Byte Array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static uint[] BitmapToRGBA(Bitmap img)
        {
            int x = img.Width;
            int y = img.Height;
            UInt32[] rgba = new UInt32[x * y];

            for (int i = 0; i < y; i += 4)
            {
                for (int j = 0; j < x; j += 4)
                {
                    for (int y1 = i; y1 < i + 4; y1++)
                    {
                        for (int x1 = j; x1 < j + 4; x1++)
                        {
                            if (y1 >= y || x1 >= x)
                                continue;

                            Color color = img.GetPixel(x1, y1);
                            rgba[x1 + (y1 * x)] = (UInt32)color.ToArgb();
                        }
                    }
                }
            }

            return rgba;
        }

        /// <summary>
        /// Converts an Image to a Tpl
        /// </summary>
        /// <param name="img"></param>
        /// <param name="format">0 = I4, 1 = I8, 2 = IA4, 3 = IA8, 4 = RGB565, 5 = RGB5A3, 6 = RGBA8</param>
        /// <returns></returns>
        public static void ConvertToTPL(Bitmap img, string destination, int format)
        {
            byte[] tpl = ConvertToTPL(img, format);

            using (FileStream fs = new FileStream(destination, FileMode.Create))
            {
                fs.Write(tpl, 0, tpl.Length);
            }
        }

        /// <summary>
        /// Converts an Image to a Tpl
        /// </summary>
        /// <param name="img"></param>
        /// <param name="format">0 = I4, 1 = I8, 2 = IA4, 3 = IA8, 4 = RGB565, 5 = RGB5A3, 6 = RGBA8</param>
        /// <returns></returns>
        public static void ConvertToTPL(Image img, string destination, int format)
        {
            byte[] tpl = ConvertToTPL((Bitmap)img, format);

            using (FileStream fs = new FileStream(destination, FileMode.Create))
            {
                fs.Write(tpl, 0, tpl.Length);
            }
        }

        /// <summary>
        /// Converts an Image to a Tpl
        /// </summary>
        /// <param name="img"></param>
        /// <param name="format">0 = I4, 1 = I8, 2 = IA4, 3 = IA8, 4 = RGB565, 5 = RGB5A3, 6 = RGBA8</param>
        /// <returns></returns>
        public static byte[] ConvertToTPL(Image img, int format)
        {
            return ConvertToTPL((Bitmap)img, format);
        }

        /// <summary>
        /// Converts an Image to a Tpl
        /// </summary>
        /// <param name="img"></param>
        /// <param name="format">0 = I4, 1 = I8, 2 = IA4, 3 = IA8, 4 = RGB565, 5 = RGB5A3, 6 = RGBA8</param>
        /// <returns></returns>
        public static byte[] ConvertToTPL(Bitmap img, int format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] rgbaData;

                UInt32 tplmagic = 0x20af30;
                UInt32 ntextures = 0x1;
                UInt32 headersize = 0xc;
                UInt32 texheaderoff = 0x14;
                UInt32 texpaletteoff = 0x0;

                UInt16 texheight = (UInt16)img.Height;
                UInt16 texwidth = (UInt16)img.Width;
                UInt32 texformat;
                UInt32 texdataoffset = 0x40;
                byte[] rest = new byte[] { 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 01, 00, 00, 00, 01, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00 };
                //This should do it for our needs.. rest includes padding

                switch (format)
                {
                    case 0: //I4
                        texformat = 0x0;
                        rgbaData = ToI4(img);
                        break;
                    case 1: //I8
                        texformat = 0x1;
                        rgbaData = ToI8(img);
                        break;
                    case 2: //IA4
                        texformat = 0x2;
                        rgbaData = ToIA4(img);
                        break;
                    case 3: //IA8
                        texformat = 0x3;
                        rgbaData = ToIA8(img);
                        break;
                    case 4: //RGB565
                        texformat = 0x4;
                        rgbaData = ToRGB565(img);
                        break;
                    case 5: //RGB5A3
                        texformat = 0x5;
                        rgbaData = ToRGB5A3(img);
                        break;
                    case 6: //RGBA8
                        texformat = 0x6;
                        rgbaData = ToRGBA8(img);
                        break;
                    default:
                        throw new FormatException();
                }

                byte[] buffer = BitConverter.GetBytes(tplmagic); Array.Reverse(buffer);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(ntextures); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(headersize); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(texheaderoff); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(texpaletteoff); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(texheight); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(texwidth); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(texformat); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                buffer = BitConverter.GetBytes(texdataoffset); Array.Reverse(buffer);
                ms.Write(buffer, 0, buffer.Length);

                ms.Write(rest, 0, rest.Length);

                ms.Write(rgbaData, 0, rgbaData.Length);

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts an Image to RGBA8 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToRGBA8(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int z = 0, iv = 0;
            byte[] output = new byte[Tools.AddPadding(w, 4) * Tools.AddPadding(h, 4) * 4];
            uint[] lr = new uint[32], lg = new uint[32], lb = new uint[32], la = new uint[32];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < (y1 + 4); y++)
                    {
                        for (int x = x1; x < (x1 + 4); x++)
                        {
                            UInt32 rgba;

                            if (y >= h || x >= w)
                            {
                                rgba = 0;
                            }
                            else
                            {
                                rgba = pixeldata[x + (y * w)];
                            }

                            lr[z] = (uint)(rgba >> 16) & 0xff;
                            lg[z] = (uint)(rgba >> 8) & 0xff;
                            lb[z] = (uint)(rgba >> 0) & 0xff;
                            la[z] = (uint)(rgba >> 24) & 0xff;

                            z++;
                        }
                    }

                    if (z == 16)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            output[iv++] = (byte)(la[i]);
                            output[iv++] = (byte)(lr[i]);
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            output[iv++] = (byte)(lg[i]);
                            output[iv++] = (byte)(lb[i]);
                        }

                        z = 0;
                    }
                }
            }


            return output;
        }

        /// <summary>
        /// Converts an Image to RGBA565 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToRGB565(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int z = -1;
            byte[] output = new byte[Tools.AddPadding(w, 4) * Tools.AddPadding(h, 4) * 2];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 4; x++)
                        {
                            UInt16 newpixel;

                            if (y >= h || x >= w)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint b = (rgba >> 16) & 0xff;
                                uint g = (rgba >> 8) & 0xff;
                                uint r = (rgba >> 0) & 0xff;

                                newpixel = (UInt16)(((b >> 3) << 11) | ((g >> 2) << 5) | ((r >> 3) << 0));
                            }

                            byte[] temp = BitConverter.GetBytes(newpixel);
                            Array.Reverse(temp);

                            output[++z] = temp[0];
                            output[++z] = temp[1];
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Converts an Image to RGBA5A3 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToRGB5A3(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int z = -1;
            byte[] output = new byte[Tools.AddPadding(w, 4) * Tools.AddPadding(h, 4) * 2];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 4; x++)
                        {
                            int newpixel;

                            if (y >= h || x >= w)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                int rgba = (int)pixeldata[x + (y * w)];
                                newpixel = 0;

                                int r = (rgba >> 16) & 0xff;
                                int g = (rgba >> 8) & 0xff;
                                int b = (rgba >> 0) & 0xff;
                                int a = (rgba >> 24) & 0xff;

                                if (a <= 0xda)
                                {
                                    //RGB4A3

                                    newpixel &= ~(1 << 15);

                                    r = ((r * 15) / 255) & 0xf;
                                    g = ((g * 15) / 255) & 0xf;
                                    b = ((b * 15) / 255) & 0xf;
                                    a = ((a * 7) / 255) & 0x7;

                                    newpixel |= a << 12;
                                    newpixel |= b << 0;
                                    newpixel |= g << 4;
                                    newpixel |= r << 8;
                                }
                                else
                                {
                                    //RGB5

                                    newpixel |= (1 << 15);

                                    r = ((r * 31) / 255) & 0x1f;
                                    g = ((g * 31) / 255) & 0x1f;
                                    b = ((b * 31) / 255) & 0x1f;

                                    newpixel |= b << 0;
                                    newpixel |= g << 5;
                                    newpixel |= r << 10;
                                }
                            }

                            byte[] temp = BitConverter.GetBytes((UInt16)newpixel);
                            Array.Reverse(temp);

                            output[++z] = temp[0];
                            output[++z] = temp[1];
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Converts an Image to I4 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToI4(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int inp = -1;
            byte[] output = new byte[Tools.AddPadding(w, 8) * Tools.AddPadding(h, 8) / 2];

            for (int y1 = 0; y1 < h; y1 += 8)
            {
                for (int x1 = 0; x1 < w; x1 += 8)
                {
                    for (int y = y1; y < y1 + 8; y++)
                    {
                        for (int x = x1; x < x1 + 8; x += 2)
                        {
                            byte newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint Red = (rgba >> 0) & 0xff;
                                uint Green = (rgba >> 8) & 0xff;
                                uint Blue = (rgba >> 16) & 0xff;

                                uint Intensity1 = ((Red + Green + Blue) / 3) & 0xff;

                                rgba = pixeldata[x + (y * w) + 1];

                                Red = (rgba >> 0) & 0xff;
                                Green = (rgba >> 8) & 0xff;
                                Blue = (rgba >> 16) & 0xff;

                                uint Intensity2 = ((Red + Green + Blue) / 3) & 0xff;

                                newpixel = (byte)((((Intensity1 * 15) / 255) << 4) | (((Intensity2 * 15) / 255) & 0xf));
                            }

                            output[++inp] = newpixel;
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Converts an Image to I8 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToI8(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int inp = -1;
            byte[] output = new byte[Tools.AddPadding(w, 8) * Tools.AddPadding(h, 4)];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 8)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 8; x++)
                        {
                            byte newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint Red = (rgba >> 0) & 0xff;
                                uint Green = (rgba >> 8) & 0xff;
                                uint Blue = (rgba >> 16) & 0xff;

                                newpixel = (byte)(((Red + Green + Blue) / 3) & 0xff);
                            }

                            output[++inp] = newpixel;
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Converts an Image to IA4 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToIA4(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int inp = -1;
            byte[] output = new byte[Tools.AddPadding(w, 8) * Tools.AddPadding(h, 4)];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 8)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 8; x++)
                        {
                            byte newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint Red = (rgba >> 0) & 0xff;
                                uint Green = (rgba >> 8) & 0xff;
                                uint Blue = (rgba >> 16) & 0xff;

                                uint Intensity = ((Red + Green + Blue) / 3) & 0xff;
                                uint Alpha = (rgba >> 24) & 0xff;

                                newpixel = (byte)((((Intensity * 15) / 255) & 0xf) | (((Alpha * 15) / 255) << 4));
                            }

                            output[++inp] = newpixel;
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Converts an Image to IA8 Tpl data
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToIA8(Bitmap img)
        {
            uint[] pixeldata = BitmapToRGBA(img);
            int w = img.Width;
            int h = img.Height;
            int inp = -1;
            byte[] output = new byte[Tools.AddPadding(w, 4) * Tools.AddPadding(h, 4) * 2];

            for (int y1 = 0; y1 < h; y1 += 4)
            {
                for (int x1 = 0; x1 < w; x1 += 4)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        for (int x = x1; x < x1 + 4; x++)
                        {
                            UInt16 newpixel;

                            if (x >= w || y >= h)
                            {
                                newpixel = 0;
                            }
                            else
                            {
                                uint rgba = pixeldata[x + (y * w)];

                                uint Red = (rgba >> 0) & 0xff;
                                uint Green = (rgba >> 8) & 0xff;
                                uint Blue = (rgba >> 16) & 0xff;

                                uint Intensity = ((Red + Green + Blue) / 3) & 0xff;
                                uint Alpha = (rgba >> 24) & 0xff;

                                newpixel = (ushort)((Intensity << 8) | Alpha);
                            }

                            byte[] temp = BitConverter.GetBytes(newpixel);
                            Array.Reverse(temp);

                            output[++inp] = temp[0];
                            output[++inp] = temp[1];
                        }
                    }
                }
            }

            return output;
        }
    }

    public class NAND
    {
        /// <summary>
        /// Backups all Saves from a NAND Backup
        /// </summary>
        /// <param name="nandpath"></param>
        /// <param name="destinationpath"></param>
        public static void BackupSaves(string nandpath, string destinationpath)
        {
            string titlefolder = nandpath + "\\title";
            string[] lowerdirs = Directory.GetDirectories(titlefolder);
            Tools.ChangeProgress(0);

            for (int i = 0; i < lowerdirs.Length; i++)
            {
                Tools.ChangeProgress((i + 1) * 100 / lowerdirs.Length);
                string[] upperdirs = Directory.GetDirectories(lowerdirs[i]);

                for (int j = 0; j < upperdirs.Length; j++)
                {
                    if (Directory.Exists(upperdirs[j] + "\\data"))
                    {
                        if (Directory.GetFiles(upperdirs[j] + "\\data").Length > 0 ||
                            Directory.GetDirectories(upperdirs[j] + "\\data").Length > 0)
                        {
                            Tools.CopyDirectory(upperdirs[j] + "\\data", (upperdirs[j] + "\\data").Replace(nandpath, destinationpath).Replace("\\title", ""));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Restores all Saves for existing titles to a NAND Backup
        /// </summary>
        /// <param name="backupdir"></param>
        /// <param name="nandpath"></param>
        public static void RestoreSaves(string backuppath, string nandpath)
        {
            string titlefolder = nandpath + "\\title";
            string[] lowerdirs = Directory.GetDirectories(backuppath);
            Tools.ChangeProgress(0);

            for (int i = 0; i < lowerdirs.Length; i++)
            {
                Tools.ChangeProgress((i + 1) * 100 / lowerdirs.Length);
                string[] upperdirs = Directory.GetDirectories(lowerdirs[i]);

                for (int j = 0; j < upperdirs.Length; j++)
                {
                    string[] datafiles = Directory.GetFiles(upperdirs[j] + "\\data");
                    string upperdirnand = upperdirs[j].Replace(backuppath, titlefolder);

                    if (Directory.Exists(upperdirnand) &&
                        (Directory.GetFiles(upperdirs[j] + "\\data").Length > 0 ||
                         Directory.GetDirectories(upperdirs[j] + "\\data").Length > 0))
                    {
                        if (!Directory.Exists(upperdirnand + "\\data")) Directory.CreateDirectory(upperdirnand + "\\data");
                        Tools.CopyDirectory(upperdirs[j] + "\\data", (upperdirs[j] + "\\data").Replace(backuppath, titlefolder));
                    }
                }
            }

            Tools.ChangeProgress(100);
        }

        /// <summary>
        /// Backups a single Save
        /// </summary>
        /// <param name="nandpath"></param>
        /// <param name="titlepath">Format: XXXXXXXX\XXXXXXXX</param>
        /// <param name="destinationpath"></param>
        public static void BackupSingleSave(string nandpath, string titlepath, string destinationpath)
        {
            string datafolder = nandpath + "\\title\\" + titlepath + "\\data";

            if (Directory.GetFiles(datafolder).Length > 0 ||
                Directory.GetDirectories(datafolder).Length > 0)
            {
                string savefolder = datafolder.Replace(nandpath, destinationpath).Replace("\\title", "");
                if (!Directory.Exists(savefolder)) Directory.CreateDirectory(savefolder);

                Tools.CopyDirectory(datafolder, savefolder);
            }
            else
            {
                throw new Exception("No save data was found!");
            }
        }

        /// <summary>
        /// Restores a singe Save, if the title exists on NAND Backup
        /// </summary>
        /// <param name="backuppath"></param>
        /// <param name="titlepath">Format: XXXXXXXX\XXXXXXXX</param>
        /// <param name="nandpath"></param>
        public static void RestoreSingleSave(string backuppath, string titlepath, string nandpath)
        {
            string titlefoldernand = nandpath + "\\title\\" + titlepath;
            string titlefolder = titlefoldernand.Replace(nandpath, backuppath).Replace("\\title", "");

            if (Directory.Exists(titlefoldernand) &&
                (Directory.GetFiles(titlefolder + "\\data").Length > 0 ||
                 Directory.GetDirectories(titlefolder + "\\data").Length > 0))
            {
                if (!Directory.Exists(titlefoldernand + "\\data")) Directory.CreateDirectory(titlefoldernand + "\\data");
                Tools.CopyDirectory(titlefolder + "\\data", titlefoldernand + "\\data");
            }
            else
            {
                throw new Exception("Title not found in NAND Backup!");
            }
        }

        /// <summary>
        /// Checks, if save data exists in the given title folder
        /// </summary>
        /// <param name="nandpath"></param>
        /// <param name="titlepath">Format: XXXXXXXX\XXXXXXXX</param>
        public static bool CheckForSaveData(string nandpath, string titlepath)
        {
            string datafolder = nandpath + "\\title\\" + titlepath + "\\data";

            if (!Directory.Exists(datafolder)) return false;
            else
            {
                string[] datafiles = Directory.GetFiles(datafolder);

                if (datafiles.Length > 0) return true;
                else return false;
            }
        }

        /// <summary>
        /// Checks, if save data exists in the given title folder
        /// </summary>
        /// <param name="nandpath"></param>
        /// <param name="titlepath">Format: XXXXXXXX\XXXXXXXX</param>
        public static bool CheckForBackupData(string backuppath, string titlepath)
        {
            string datafolder = backuppath + "\\" + titlepath + "\\data";

            if (!Directory.Exists(datafolder)) return false;
            else
            {
                string[] datafiles = Directory.GetFiles(datafolder);

                if (datafiles.Length > 0) return true;
                else return false;
            }
        }
    }

    public class Sound
    {
        /// <summary>
        /// Checks if the given Wave is a proper PCM WAV file
        /// </summary>
        /// <param name="wavefile"></param>
        /// <returns></returns>
        public static bool CheckWave(string wavefile)
        {
            byte[] wave = Tools.LoadFileToByteArray(wavefile, 0, 256);
            return CheckWave(wave);
        }

        /// <summary>
        /// Checks if the given Wave is a proper PCM WAV file
        /// </summary>
        /// <param name="wavefile"></param>
        /// <returns></returns>
        public static bool CheckWave(byte[] wavefile)
        {
            if (wavefile[0] != 'R' ||
                wavefile[1] != 'I' ||
                wavefile[2] != 'F' ||
                wavefile[3] != 'F' ||

                wavefile[8] != 'W' ||
                wavefile[9] != 'A' ||
                wavefile[10] != 'V' ||
                wavefile[11] != 'E' ||

                wavefile[12] != 'f' ||
                wavefile[13] != 'm' ||
                wavefile[14] != 't' ||

                wavefile[20] != 0x01 || //Format = PCM
                wavefile[21] != 0x00 ||

                wavefile[34] != 0x10 || //Bitrate (16bit)
                wavefile[35] != 0x00
                ) return false;

            return true;
        }

        /// <summary>
        /// Returns the playlength of the Wave file in seconds
        /// </summary>
        /// <param name="wavefile"></param>
        /// <returns></returns>
        public static int GetWaveLength(string wavefile)
        {
            byte[] wave = Tools.LoadFileToByteArray(wavefile, 0, 256);
            return GetWaveLength(wave);
        }

        /// <summary>
        /// Returns the playlength of the Wave file in seconds
        /// </summary>
        /// <param name="wavefile"></param>
        /// <returns></returns>
        public static int GetWaveLength(byte[] wavefile)
        {
            if (CheckWave(wavefile) == true)
            {
                byte[] BytesPerSec = new byte[] { wavefile[28], wavefile[29], wavefile[30], wavefile[31] };
                int bps = BitConverter.ToInt32(BytesPerSec, 0);

                byte[] Chunksize = new byte[] { wavefile[4], wavefile[5], wavefile[6], wavefile[7] };
                int chunks = BitConverter.ToInt32(Chunksize, 0);

                return Math.Abs(chunks / bps);
            }
            else
                throw new Exception("This is not a supported PCM Wave file!");
        }

        /// <summary>
        /// Converts a wave file to a sound.bin
        /// </summary>
        /// <param name="wavefile"></param>
        /// <param name="soundbin"></param>
        public static void WaveToSoundBin(string wavefile, string soundbin, bool compress)
        {
            if (CheckWave(wavefile) == true)
            {
                byte[] sound = Tools.LoadFileToByteArray(wavefile);
                if (compress == true) sound = Lz77.Compress(sound);
                sound = U8.AddHeaderIMD5(sound);
                Wii.Tools.SaveFileFromByteArray(sound, soundbin);
            }
            else
                throw new Exception("This is not a supported 16bit PCM Wave file!");
        }

        /// <summary>
        /// Converts a sound.bin to a wave file
        /// </summary>
        /// <param name="soundbin"></param>
        /// <param name="wavefile"></param>
        public static void SoundBinToAudio(string soundbin, string audiofile)
        {
            FileStream fs = new FileStream(soundbin, FileMode.Open);
            byte[] audio = new byte[fs.Length - 32];
            int offset = 0;

            fs.Seek(32, SeekOrigin.Begin);
            fs.Read(audio, 0, audio.Length);
            fs.Close();

            if ((offset = Lz77.GetLz77Offset(audio)) != -1)
                audio = Lz77.Decompress(audio, offset);

            Tools.SaveFileFromByteArray(audio, audiofile);
        }

        /// <summary>
        /// Converts a BNS file to a sound.bin
        /// </summary>
        /// <param name="bnsFile"></param>
        /// <param name="soundBin"></param>
        public static void BnsToSoundBin(string bnsFile, string soundBin, bool compress)
        {
            byte[] bns = Tools.LoadFileToByteArray(bnsFile);

            if (bns[0] != 'B' || bns[1] != 'N' || bns[2] != 'S')
                throw new Exception("This is not a supported BNS file!");

            if (compress) bns = Lz77.Compress(bns);
            bns = U8.AddHeaderIMD5(bns);

            Tools.SaveFileFromByteArray(bns, soundBin);
        }

        /// <summary>
        /// Returns the length of the BNS audio file in seconds
        /// </summary>
        /// <param name="bnsFile"></param>
        /// <returns></returns>
        public static int GetBnsLength(string bnsFile)
        {
            byte[] temp = Tools.LoadFileToByteArray(bnsFile, 0, 100);
            return GetBnsLength(temp);
        }

        /// <summary>
        /// Returns the length of the BNS audio file in seconds
        /// </summary>
        /// <param name="bnsFile"></param>
        /// <returns></returns>
        public static int GetBnsLength(byte[] bnsFile)
        {
            byte[] temp = new byte[4];
            temp[0] = bnsFile[45];
            temp[1] = bnsFile[44];

            int sampleRate = BitConverter.ToInt16(temp, 0);

            temp[0] = bnsFile[55];
            temp[1] = bnsFile[54];
            temp[2] = bnsFile[53];
            temp[3] = bnsFile[52];

            int sampleCount = BitConverter.ToInt32(temp, 0);

            return sampleCount / sampleRate;
        }
    }

    public class Brlyt
    {
        /// <summary>
        /// Checks, if the TPLs match the TPLs specified in the brlyt
        /// </summary>
        /// <param name="brlyt"></param>
        /// <param name="tpls"></param>
        /// <returns></returns>
        public static bool CheckBrlytTpls(string brlyt, string[] tpls)
        {
            byte[] brlytArray = Tools.LoadFileToByteArray(brlyt);
            return CheckBrlytTpls(brlytArray, tpls);
        }

        /// <summary>
        /// Checks, if the TPLs match the TPLs specified in the brlyt
        /// </summary>
        /// <param name="brlyt"></param>
        /// <param name="tpls"></param>
        /// <returns></returns>
        public static bool CheckBrlytTpls(byte[] brlyt, string[] tpls)
        {
            int texcount = Tools.HexStringToInt(brlyt[44].ToString("x2") + brlyt[45].ToString("x2"));
            if (tpls.Length != texcount) return false;

            int texnamepos = 48 + (texcount * 8);
            for (int i = 0; i < texcount; i++)
            {
                string thisTex = "";
                while (brlyt[texnamepos] != 0x00)
                {
                    thisTex += Convert.ToChar(brlyt[texnamepos]);
                    texnamepos++;
                }
                texnamepos++;

                bool exists = Array.Exists(tpls, tpl => tpl == thisTex);
                if (exists == false) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks, if one or more Tpls specified in the brlyt are missing and returns 
        /// the names of the missing ones.
        /// </summary>
        /// <param name="brlyt"></param>
        /// <param name="tpls"></param>
        /// <param name="missingtpls"></param>
        /// <returns></returns>
        public static bool CheckForMissingTpls(string brlyt, string brlan, string[] tpls, out string[] missingtpls)
        {
            byte[] brlytArray = Tools.LoadFileToByteArray(brlyt);
            byte[] brlanArray = Tools.LoadFileToByteArray(brlan);
            return CheckForMissingTpls(brlytArray, brlanArray, tpls, out missingtpls);
        }

        /// <summary>
        /// Checks, if one or more Tpls specified in the brlyt are missing and returns 
        /// the names of the missing ones.
        /// </summary>
        /// <param name="brlyt"></param>
        /// <param name="tpls"></param>
        /// <param name="missingtpls"></param>
        /// <returns></returns>
        public static bool CheckForMissingTpls(byte[] brlyt, byte[] brlan, string[] tpls, out string[] missingtpls)
        {
            List<string> missings = new List<string>();
            string[] brlytTpls = GetBrlytTpls(brlyt, brlan);
            bool missing = false;

            for (int i = 0; i < brlytTpls.Length; i++)
            {
                if (Tools.StringExistsInStringArray(brlytTpls[i], tpls) == false)
                {
                    missings.Add(brlytTpls[i]);
                    missing = true;
                }
            }

            missingtpls = missings.ToArray();
            return missing;
        }

        /// <summary>
        /// Checks, if one or more Tpls are not specified in the brlyt and returns 
        /// the names of the missing ones.
        /// </summary>
        /// <param name="brly"></param>
        /// <param name="tpls"></param>
        /// <param name="unusedtpls"></param>
        /// <returns></returns>
        public static bool CheckForUnusedTpls(string brlyt, string brlan, string[] tpls, out string[] unusedtpls)
        {
            byte[] brlytArray = Tools.LoadFileToByteArray(brlyt);
            byte[] brlanArray = Tools.LoadFileToByteArray(brlan);
            return CheckForUnusedTpls(brlytArray, brlanArray, tpls, out unusedtpls);
        }

        /// <summary>
        /// Checks, if one or more Tpls are not specified in the brlyt and returns 
        /// the names of the missing ones.
        /// </summary>
        /// <param name="brly"></param>
        /// <param name="tpls"></param>
        /// <param name="unusedtpls"></param>
        /// <returns></returns>
        public static bool CheckForUnusedTpls(byte[] brlyt, byte[] brlan, string[] tpls, out string[] unusedtpls)
        {
            List<string> unuseds = new List<string>();
            string[] brlytTpls = GetBrlytTpls(brlyt, brlan);
            bool missing = false;

            for (int i = 0; i < tpls.Length; i++)
            {
                if (Tools.StringExistsInStringArray(tpls[i], brlytTpls) == false)
                {
                    string wonum = tpls[i].Remove(tpls[i].LastIndexOf('.') - 1) + "00.tpl";
                    string wonum2 = tpls[i].Remove(tpls[i].LastIndexOf('.') - 2) + "00.tpl";
                    string wonum3 = tpls[i].Remove(tpls[i].LastIndexOf('.') - 1) + "01.tpl";
                    string wonum4 = tpls[i].Remove(tpls[i].LastIndexOf('.') - 2) + "01.tpl";

                    if (Tools.StringExistsInStringArray(wonum, brlytTpls) == false &&
                        Tools.StringExistsInStringArray(wonum2, brlytTpls) == false &&
                        Tools.StringExistsInStringArray(wonum3, brlytTpls) == false &&
                        Tools.StringExistsInStringArray(wonum4, brlytTpls) == false)
                    {
                        unuseds.Add(tpls[i]);
                        missing = true;
                    }
                }
            }

            unusedtpls = unuseds.ToArray();
            return missing;
        }

        /// <summary>
        /// Returns the name of all Tpls specified in the brlyt
        /// </summary>
        /// <param name="brlyt"></param>
        /// <returns></returns>
        public static string[] GetBrlytTpls(string brlyt, string brlan)
        {
            byte[] temp = Tools.LoadFileToByteArray(brlyt);
            byte[] temp2 = Tools.LoadFileToByteArray(brlan);
            return GetBrlytTpls(temp, temp2);
        }

        /// <summary>
        /// Returns the name of all Tpls specified in the brlyt
        /// </summary>
        /// <param name="brlyt"></param>
        /// <returns></returns>
        public static string[] GetBrlytTpls(byte[] brlyt, byte[] brlan)
        {
            int texcount = Tools.HexStringToInt(brlyt[44].ToString("x2") + brlyt[45].ToString("x2"));
            int texnamepos = 48 + (texcount * 8);
            List<string> Tpls = new List<string>();

            for (int i = 0; i < texcount; i++)
            {
                string thisTex = "";
                while (brlyt[texnamepos] != 0x00)
                {
                    thisTex += Convert.ToChar(brlyt[texnamepos]);
                    texnamepos++;
                }
                Tpls.Add(thisTex);
                texnamepos++;
            }

            //Lets also get brlan tpls (frame animations)
            try
            {
                string[] brlanTpls = GetBrlanTpls(brlan);
                foreach (string thisTpl in brlanTpls)
                {
                    if (thisTpl.EndsWith(".tpl"))
                    {
                        if (!Tpls.Contains(thisTpl))
                            Tpls.Add(thisTpl);
                    }
                }
            }
            catch { } //If it throws any error, it's probably an empty brlan

            return Tpls.ToArray();
        }

        /// <summary>
        /// Returns the name of all Tpls specified in the brlan
        /// </summary>
        /// <param name="brlyt"></param>
        /// <returns></returns>
        public static string[] GetBrlanTpls(string brlan)
        {
            byte[] temp = Tools.LoadFileToByteArray(brlan);
            return GetBrlanTpls(temp);
        }

        /// <summary>
        /// Returns the name of all Tpls specified in the brlan
        /// </summary>
        /// <param name="brlyt"></param>
        /// <returns></returns>
        public static string[] GetBrlanTpls(byte[] brlan)
        {
            List<string> tpls = new List<string>();
            int texcount = Tools.HexStringToInt(brlan[28].ToString("x2") + brlan[29].ToString("x2"));
            int pailen;
            if (brlan[32] == 0x00 && brlan[33] == 0x00 && brlan[34] == 0x00 && brlan[35] == 0x00)
                pailen = Tools.HexStringToInt(brlan[36].ToString("x2") + brlan[37].ToString("x2") + brlan[38].ToString("x2") + brlan[39].ToString("x2"));
            else
                pailen = Tools.HexStringToInt(brlan[32].ToString("x2") + brlan[33].ToString("x2") + brlan[34].ToString("x2") + brlan[35].ToString("x2"));

            int texnameendpos = 16 + pailen;

            for (int i = texnameendpos; i > 0; i--)
            {
                if (brlan[i] != 0x00)
                { texnameendpos = i + 1; break; }
            }

            for (int i = 0; i < texcount; i++)
            {
                List<char> thisTex = new List<char>();
                while (brlan[texnameendpos] != 0x00)
                {
                    thisTex.Add((char)brlan[texnameendpos--]);
                }

                thisTex.Reverse();
                tpls.Add(new string(thisTex.ToArray()));
                texnameendpos--;
            }

            tpls.Reverse();
            return tpls.ToArray();
        }

        /// <summary>
        /// Returns true, if the given Tpl is specified in the brlyt.
        /// TplName must end with ".tpl"!
        /// </summary>
        /// <param name="brlyt"></param>
        /// <param name="TplName"></param>
        /// <returns></returns>
        public static bool IsTplInBrlyt(byte[] brlyt, byte[] brlan, string TplName)
        {
            string[] brlytTpls = GetBrlytTpls(brlyt, brlan);
            bool exists = Array.Exists(brlytTpls, Tpl => Tpl == TplName);
            return exists;
        }
    }

    public class ProgressChangedEventArgs : EventArgs
    {
        private readonly int p_Percent = 0;

        public int PercentProgress
        {
            get { return p_Percent; }
        }

        internal ProgressChangedEventArgs(int PercentProgress)
            : base()
        {
            this.p_Percent = PercentProgress;
        }
    }
}