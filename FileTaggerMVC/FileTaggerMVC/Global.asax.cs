using System.Collections.Generic;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;

namespace FileTaggerMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
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

                                                CREATE TABLE TagMap(
	                                                Id INTEGER PRIMARY KEY,
	                                                File_Id INTEGER NOT NULL,
	                                                Tag_Id INTEGER NOT NULL,
	                                                FOREIGN KEY(File_Id) REFERENCES File(Id),
	                                                FOREIGN KEY(Tag_Id) REFERENCES Tag(Id)
                                                );";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateDatabase();
        }

        private void CreateDatabase()
        {
            string connectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];
            Dictionary<string, string> dict = Regex.Matches(connectionString, @"\s*(?<key>[^;=]+)\s*=\s*((?<value>[^'][^;]*)|'(?<value>[^']*)')")
                                                   .Cast<Match>()
                                                   .ToDictionary(m => m.Groups["key"].Value,
                                                          m => m.Groups["value"].Value);
            string dbFileName = (string)dict["Data Source"];

            if (!System.IO.File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);

                using (var dbConnection = new SQLiteConnection(connectionString))
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
