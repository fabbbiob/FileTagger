using FileTaggerMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Web.Configuration;

namespace FileTaggerMVC.DAL
{
    public class TagDal
    {
        private readonly static string ConnectionString = WebConfigurationManager.AppSettings["SqliteConnectionString"];

        public static IEnumerable<Tag> GetAll()
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

                            yield return tag;
                        }
                    }
                }
            }
        }

        public static void Create(Tag tag)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                string query = "INSERT INTO Tag(Description, TagType_Id) VALUES (@Description, @TagType_Id)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Description", tag.Description);
                    if (tag.TagTypeId != -1)
                    {
                        cmd.Parameters.AddWithValue("@TagType_Id", tag.TagTypeId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TagType_Id", DBNull.Value);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}