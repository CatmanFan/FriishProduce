using System;
using System.IO;
using System.IO.Hashing;
using Microsoft.Data.Sqlite;

namespace FriishProduce.Databases
{
    public static class OpenVGDB
    {
        private static SqliteConnection database;

        private static bool Setup()
        {
            if (File.Exists(Paths.Tools + "openvgdb.sqlite"))
            {
                database = new($"Data Source={Paths.Tools}openvgdb.sqlite");
                return true;
            }

            else if (File.Exists(Paths.Tools + "openvgdb.db"))
            {
                database = new($"Data Source={Paths.Tools}openvgdb.db");
                return true;
            }

            return false;
        }

        public static void Parse(string file)
        {
            if (database == null && !Setup()) return;

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

            database.Open();

            // Get current CRC32 hash of file and append to query
            // **********************
            using (SqliteCommand command = new(query, database))
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                string id = null;

                while (reader.Read())
                    id = reader["romID"].ToString();
            }
        }
    }
}
