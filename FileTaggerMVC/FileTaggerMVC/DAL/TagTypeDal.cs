using FileTaggerMVC.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web.Configuration;

namespace FileTaggerMVC.DAL
{
    internal class TagTypeDal
    {
        private readonly static string ConnectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];

        internal static IEnumerable<TagTypeViewModel> GetAll()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "select Id, Description from TagType";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    conn.Open();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return Parse(dr);
                        }
                    }
                }
            }
        }

        internal static TagTypeViewModel Get(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "select Id, Description from TagType where Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@Id", DbType.Int32).Value = id;

                    conn.Open();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return Parse(dr);
                        }
                    }
                }
            }

            return null;
        }

        internal static void Create(TagTypeViewModel tagType)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "INSERT INTO TagType(Description) VALUES (@Description)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@Description", DbType.String).Value = tagType.Description;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void Edit(TagTypeViewModel tagType)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "UPDATE TagType SET Description = @Description where Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@Id", DbType.Int32).Value = tagType.Id;
                    cmd.Parameters.Add("@Description", DbType.String).Value = tagType.Description;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "DELETE FROM TagType WHERE Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@Id", DbType.Int32).Value = id;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static TagTypeViewModel Parse(SQLiteDataReader dr)
        {
            return new TagTypeViewModel
            {
                Id = dr.GetInt32(0),
                Description = dr.GetString(1)
            };
        }
    }
}