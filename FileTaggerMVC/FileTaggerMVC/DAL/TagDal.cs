using FileTaggerMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Web.Configuration;

namespace FileTaggerMVC.DAL
{
    internal class TagDal
    {
        private readonly static string ConnectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];

        internal static IEnumerable<Tag> GetAll()
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                                 FROM Tag as t
                                 LEFT JOIN TagType AS tt
                                     ON t.TagType_Id = tt.Id";
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

        internal static void Create(Tag tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "INSERT INTO Tag(Description, TagType_Id) VALUES (@Description, @TagType_Id)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Description", tag.Description);
                    cmd.Parameters.AddWithValue("@TagType_Id", GetTagTypeId(tag));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static Tag Get(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                                 FROM Tag as t
                                 LEFT JOIN TagType AS tt
                                     ON t.TagType_Id = tt.Id 
                                 WHERE t.Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id.ToString());

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

        internal static void Edit(Tag tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "UPDATE Tag SET Description = @Description, TagType_Id = @TagType_Id where Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", tag.Id);
                    cmd.Parameters.AddWithValue("@Description", tag.Description);
                    cmd.Parameters.AddWithValue("@TagType_Id", GetTagTypeId(tag));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "DELETE FROM Tag WHERE Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static Tag Parse(SQLiteDataReader dr)
        {
            var tag = new Tag
            {
                Id = dr.GetInt32(0),
                Description = dr.GetString(1)
            };

            var idTagTypeObj = dr.GetValue(2);
            if (idTagTypeObj != DBNull.Value)
            {
                tag.TagType = new TagType
                {
                    Id = dr.GetInt32(2),
                    Description = dr.GetString(3)
                };
            }

            return tag;
        }

        private static object GetTagTypeId(Tag tag)
        {
            if (tag.TagTypeId != -1)
            {
                return tag.TagTypeId;
            }
            else
            {
                return DBNull.Value;
            }
        }
    }
}