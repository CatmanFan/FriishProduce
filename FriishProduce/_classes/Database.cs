using libWiiSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;

namespace FriishProduce
{
    public static class XmlHelper
    {
        /// <summary>
        /// Serializes an object to an XML string, using the specified namespaces.
        /// </summary>
        public static string ToXml(object obj, XmlSerializerNamespaces ns)
        {
            Type T = obj.GetType();

            var xs = new XmlSerializer(T, new XmlRootAttribute("Databases"));
            var ws = new XmlWriterSettings { Indent = true, NewLineOnAttributes = true, OmitXmlDeclaration = true };

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, ws))
            {
                xs.Serialize(writer, obj, ns);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Serializes an object to an XML string.
        /// </summary>
        public static string ToXml(object obj)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ToXml(obj, ns);
        }

        /// <summary>
        /// Deserializes an object from an XML string.
        /// </summary>
        public static T FromXml<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T), new XmlRootAttribute("Databases"));
            using (StringReader sr = new StringReader(xml))
            {
                return (T)xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// Deserializes an object from an XML file.
        /// </summary>
        public static T FromXmlFile<T>(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            try
            {
                var result = FromXml<T>(sr.ReadToEnd());
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("There was an error attempting to read the file " + filePath + "\n\n" + e.InnerException.Message);
            }
            finally
            {
                sr.Close();
            }
        }
    }

    [Serializable, XmlRoot("Databases", IsNullable = false)]
    public class Database
    {
        [XmlAttribute]
        public Console Type { get; set; }

        [XmlElement("Entry")]
        public DatabaseEntry[] Entries { get; set; }
    }

    [Serializable]
    public class DatabaseEntry
    {
        [XmlElement("TID")]
        public string TitleID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }            // Name on MarioCube

        [XmlElement("DisplayName")]
        public string DisplayName { get; set; }     // Name in specific language to be displayed on GUI

        [XmlElement("Emulator")]
        public int Emulator { get; set; }             // Revision used by the WAD's emulator, if one exists

        // SEGA
        // ****************
        // NOTE FROM THE AUTHOR: Only Revision 2 and Revision 3 SEGA WADs are accepted currently, because CCF modification does not work with Revision 1 WADs (such as Comix Zone).
        //                       They also have more customizable options anyway, such as brightness (which can be adjusted to 100%), sound, 6-button, etc.
        // ****************

        public WAD Load()
        {
            string reg = null;

            switch (Region())
            {
                case 0:
                    reg = " (USA)";
                    break;

                case 1:
                case 2:
                    reg = " (Europe)";
                    break;

                case 3:
                    reg = " (Japan)";
                    break;

                case 4:
                    reg = " (Korea) (Ja,Ko)";
                    break;

                case 5:
                    reg = " (Korea) (En,Ko)";
                    break;
            }

            Console c = DatabaseHelper.Console(TitleID);
            string ConsoleType = c == Console.NES ? " (NES)"
                               : c == Console.SNES ? " (SNES)"
                               : c == Console.N64 ? " (N64)"
                               : c == Console.SMS ? " (SMS)"
                               : c == Console.SMDGEN ? " (SMD)"
                               : c == Console.PCE ? " (TGX)"
                               : c == Console.NeoGeo ? " (NG)"
                               : c == Console.C64 ? " (C64)"
                               : c == Console.MSX ? " (MSX)"
                               : null;

            if (reg == null) throw new ArgumentException();

            // Load WAD from MarioCube.
            // ------------------------------------------------
            // Sadly, as the NUS downloader cannot decrypt VC/Wii Shop titles on its own without needing the ticket file, I have done a less copyright-friendly workaround solution for now
            // Direct link is not included, for obvious reasons!
            // ****************
            string name = Name + reg + ConsoleType;
            string URL = "https://repo.mariocube.com/WADs/_WiiWare,%20VC,%20DLC,%20Channels%20&%20IOS/" + name[0].ToString().ToUpper() + "/" + Uri.EscapeDataString(name + " (Virtual Console)") + ".wad";
            if (c == Console.Flash) URL = "https://repo.mariocube.com/WADs/Flash%20Injects/Base/" + Uri.EscapeDataString(name) + ".wad";

            Web.InternetTest();

            WAD w = WAD.Load(Web.Get(URL));

            // Title ID check
            // ****************
            if ((w.UpperTitleID.ToUpper() != TitleID.ToUpper() && c != Console.Flash) || !w.HasBanner) throw new ArgumentException();
            return w;
        }

        public enum Regions
        {
            All,
            USA,
            USA_JP,
            Europe,
            Europe_JP,
            Europe_US,
            Japan,
            Korea_JA,
            Korea_EN,
        }

        public int Region()
        {
            char RegionCode = TitleID.ToUpper()[3];

            switch (RegionCode)
            {
                default:
                case 'E': // USA
                case 'N':
                    return 0;

                case 'P':
                    return 1;

                case 'L': // Japanese import
                case 'M': // American import
                    return 2;

                case 'J': // Japan
                    return 3;

                case 'Q': // Korea with Japanese language
                    return 4;

                case 'T': // Korea with English language
                    return 5;
            }
        }

        public DatabaseEntry() { }
    }


    public static class DatabaseHelper
    {
        public static DatabaseEntry[] Get(Console c)
        {
            foreach (Database item in XmlHelper.FromXml<Database[]>(Properties.Resources.Database))
                if (item.Type == c) return item.Entries;

            return null;
        }

        public static Console Console(string tID)
        {
            var items = XmlHelper.FromXml<Database[]>(Properties.Resources.Database);

            foreach (Database item in items)
            {
                for (int i = 0; i < item.Entries.Length; i++)
                {
                    if (item.Entries[i].TitleID.ToUpper() == tID.ToUpper())
                    {
                        return item.Type;
                    }
                }
            }

            return 0;
        }

        public static DatabaseEntry Get(string tID)
        {
            var items = XmlHelper.FromXml<Database[]>(Properties.Resources.Database);

            foreach (Database item in items)
            {
                for (int i = 0; i < item.Entries.Length; i++)
                {
                    if (item.Entries[i].TitleID.ToUpper() == tID.ToUpper())
                    {
                        return item.Entries[i];
                    }
                }
            }

            return null;
        }

        public static DatabaseEntry Get(string tID, Console c)
        {
            var List = Get(c);

            for (int i = 0; i < List.Length; i++)
            {
                if (List[i].TitleID.ToUpper() == tID.ToUpper())
                {
                    return List[i];
                }
            }

            return null;
        }
    }
}
