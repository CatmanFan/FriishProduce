using System;
using System.IO;

namespace FriishProduce
{
    public class Paths
    {
        public static readonly string EnvironmentFolder = Environment.CurrentDirectory;
        public static readonly string Apps = Path.Combine(Environment.CurrentDirectory, "apps\\");
        public static readonly string Database = Path.Combine(Environment.CurrentDirectory, "bases\\");
        public static readonly string DatabaseXML = Path.Combine(Environment.CurrentDirectory, "bases\\database.xml");

        public static readonly string WorkingFolder = $"C:\\FriishProduce\\";
        public static readonly string WorkingFolder_Content4 = WorkingFolder + $"content4\\";
        public static readonly string WorkingFolder_Content5 = WorkingFolder + $"content5\\";
        public static readonly string WorkingFolder_DataCCF = WorkingFolder_Content5 + "data\\";
        public static readonly string WorkingFolder_MiscCCF = WorkingFolder_Content5 + "data\\misc\\";
    }
}
