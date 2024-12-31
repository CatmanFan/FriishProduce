using System;
using System.Data;
using System.IO;
using System.IO.Hashing;
using ServiceStack.Redis;

namespace FriishProduce.Databases
{
    public static class LibRetro
    {
        public static DataTable Parse(string databaseName)
        {
            //Déclarartions des variables
            string crc = "";
            string name = "";
            string serial = "";
            int releaseyear = 0;
            int users = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("crc", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("serial", typeof(string));
            dt.Columns.Add("releaseyear", typeof(string));
            dt.Columns.Add("users", typeof(string));

            //Déclarations base de données

            SQLiteConnection SQLconnect = new("Data Source=" + databaseName + ";");

            try
            {
                //Connexion au fichier RDB
                SQLconnect.Open();

                //Creation Requete pour lecture en-tete
                using SQLiteCommand SQLcommand = SQLconnect.CreateCommand();
                SQLcommand.CommandText = "SELECT * FROM main";

                using SQLiteDataReader SQLreader = SQLcommand.ExecuteReader();

                SQLreader.Read();
                //Execution requete 
            }
            catch (Exception ex)
            {
                //Si erreur pendant la lecture du fichier message
                Console.WriteLine("Error file reading : {0}.{1} Error", databaseName, ex.Message);
            }
            return dt;
        }


        public static void Read(string file)
        {
            // Get current CRC32 hash of file and append to query
            // **********************
            string query = null;
            using (var fileStream = File.OpenRead(file))
            {
                var crc = new Crc32();
                crc.Append(fileStream);
                var hash_array = crc.GetCurrentHash();
                Array.Reverse(hash_array);

                query = $"SELECT * FROM ROMs where romHashCRC = {BitConverter.ToString(hash_array).Replace("-", "").ToUpper()}";
            }

            DataTable dt = Parse(Path.Combine(Paths.Databases, "nes" + ".rdb"));
        }
    }
}
