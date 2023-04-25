using System;
using System.Xml;

namespace FriishProduce
{
    public class XML
    {
        public static string CurrentFolder(Platforms platform) => Paths.Database + platform.ToString().ToLower() + "\\";

        public static XmlNodeList RetrieveList(Platforms platform)
        {
            XmlDocument file = new XmlDocument();
            file.Load(Paths.DatabaseXML);
            return file.DocumentElement.SelectNodes($"/bases/{platform}/wad".ToLower());
        }

        /// <summary>
        /// Searches for an entry with the title parameter (game name & region) and if found, returns its upper ID, otherwise null
        /// </summary>
        public static string SearchID(string title, Platforms platform)
        {
            foreach (XmlNode entry in XML.RetrieveList(platform))
                if (entry.Attributes["title"].Value == title)
                    return entry.Attributes["id"].Value.ToUpper();

            return null;
        }
    }
}
