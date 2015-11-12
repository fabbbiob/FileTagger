using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace FileTaggerRepository.Helpers
{
    public class DbCreator
    {
        private static string ConnectionString => ConfigurationManager.AppSettings["SqliteConnectionString"];

        private const string CreateTables = @"  PRAGMA foreign_keys = ON;
                                                CREATE TABLE TagType(
	                                                Id INTEGER PRIMARY KEY,
	                                                Description TEXT NOT NULL
                                                );

                                                CREATE TABLE Tag(
	                                                Id INTEGER PRIMARY KEY,
	                                                Description TEXT NOT NULL,
	                                                TagType_Id INTEGER,
	                                                FOREIGN KEY(TagType_Id) REFERENCES TagType(Id)
                                                );

                                                CREATE TABLE File(
	                                                Id INTEGER PRIMARY KEY,
	                                                FilePath TEXT NOT NULL
                                                );

                                                CREATE UNIQUE INDEX UniqueFilePathIndex
                                                on File (FilePath);

                                                CREATE TABLE TagMap(
	                                                Id INTEGER PRIMARY KEY,
	                                                File_Id INTEGER NOT NULL,
	                                                Tag_Id INTEGER NOT NULL,
	                                                FOREIGN KEY(File_Id) REFERENCES File(Id),
	                                                FOREIGN KEY(Tag_Id) REFERENCES Tag(Id)
                                                );";


        private static Dictionary<string, string> ConnectionStringParameters => 
            Regex.Matches(ConnectionString, @"\s*(?<key>[^;=]+)\s*=\s*((?<value>[^'][^;]*)|'(?<value>[^']*)')")
                 .Cast<Match>()
                 .ToDictionary(m => m.Groups["key"].Value,
                               m => m.Groups["value"].Value);

        public static void CreateDatabase()
        {
            string dbFileName = ConnectionStringParameters["Data Source"];

            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);

                using (var dbConnection = new SQLiteConnection(ConnectionString))
                {
                    dbConnection.Open();

                    SQLiteCommand command = new SQLiteCommand(CreateTables, dbConnection);
                    command.ExecuteNonQuery();

                    dbConnection.Close();
                }
            }
        }
    }
}
