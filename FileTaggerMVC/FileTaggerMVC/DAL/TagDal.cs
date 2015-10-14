using FileTaggerMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web.Configuration;

namespace FileTaggerMVC.DAL
{
    internal class TagDal
    {
        private readonly static string ConnectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];

        internal static IEnumerable<TagViewModel> GetAll()
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

        internal static void Create(TagViewModel tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "INSERT INTO Tag(Description, TagType_Id) VALUES (@Description, @TagType_Id)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@Description", DbType.String).Value = tag.Description;
                    cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(tag);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static TagViewModel Get(int id)
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

        internal static void Edit(TagViewModel tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "UPDATE Tag SET Description = @Description, TagType_Id = @TagType_Id where Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@Id", DbType.Int32).Value = tag.Id;
                    cmd.Parameters.Add("@Description", DbType.String).Value = tag.Description;
                    cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(tag);

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
                    cmd.Parameters.Add("@Id", DbType.Int32).Value = id;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static TagViewModel Parse(SQLiteDataReader dr)
        {
            var tag = new TagViewModel
            {
                Id = dr.GetInt32(0),
                Description = dr.GetString(1)
            };

            var idTagTypeObj = dr.GetValue(2);
            if (idTagTypeObj != DBNull.Value)
            {
                tag.TagType = new TagTypeViewModel
                {
                    Id = dr.GetInt32(2),
                    Description = dr.GetString(3)
                };
            }

            return tag;
        }

        private static object GetTagTypeId(TagViewModel tag)
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