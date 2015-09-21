using FileTaggerMVC.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Web.Configuration;

namespace FileTaggerMVC.DAL
{
    internal class TagTypeDal
    {
        private readonly static string ConnectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];

        internal static IEnumerable<TagType> GetAll()
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
                            yield return new TagType
                            {
                                Id = dr.GetInt32(0),
                                Description = dr.GetString(1)
                            };
                        }
                    }
                }
            }
        }

        internal static TagType Get(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "select Id, Description from TagType where Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id.ToString());

                    conn.Open();
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return new TagType
                            {
                                Id = dr.GetInt32(0),
                                Description = dr.GetString(1)
                            };
                        }
                    }
                }
            }

            return null;
        }

        internal static void Create(TagType tagType)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "INSERT INTO TagType(Description) VALUES (@Description)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Description", tagType.Description);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void Edit(TagType tagType)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "UPDATE TagType SET Description = @Description where Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", tagType.Id);
                    cmd.Parameters.AddWithValue("@Description", tagType.Description);

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
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}