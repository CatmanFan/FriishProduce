using System;
using System.IO;

namespace FriishProduce
{
    public class Paths
    {
        public static string EnvironmentFolder = Environment.CurrentDirectory;
        public static string Apps = Path.Combine(Environment.CurrentDirectory, "apps\\");
        public static string Database = Path.Combine(Environment.CurrentDirectory, "bases\\");
        public static string DatabaseXML = Path.Combine(Environment.CurrentDirectory, "bases\\database.xml");

        public static string WorkingFolder = $"C:\\FriishProduce\\";
        public static string WorkingFolder_Content4 = WorkingFolder + $"content4\\";
        public static string WorkingFolder_Content5 = WorkingFolder + $"content5\\";
        public static string WorkingFolder_DataCCF = WorkingFolder_Content5 + "data\\";
        public static string WorkingFolder_MiscCCF = WorkingFolder_Content5 + "data\\misc\\";
    }
}
