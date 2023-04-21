using System.Xml;

namespace FriishProduce
{
    public class XML
    {
        public static XmlNodeList RetrieveList()
        {
            XmlDocument file = new XmlDocument();
            file.Load(Paths.DatabaseXML);
            return file.GetElementsByTagName("WAD");
        }
    }
}
