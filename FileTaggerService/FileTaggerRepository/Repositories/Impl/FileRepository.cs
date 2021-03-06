﻿using System;
using System.Collections.Generic;
using FileTaggerModel.Model;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Linq;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Helpers;
using FileTaggerModel;

namespace FileTaggerRepository.Repositories.Impl
{
    public class FileRepository : IFileRepository
    {
        //protected override string GetAllQuery => "SELECT Id, FilePath FROM File;";

        //protected override File Parse(SQLiteDataReader dr)
        //{
        //    return new File
        //    {
        //        Id = dr.GetInt32(0),
        //        FilePath = dr.GetString(1)
        //    };
        //}

        //protected override string GetByIdWithReferencesQuery => 
        //                                        GetByIdQuery +
        //                                        @"SELECT t.Id, t.Description, tt.Id, tt.Description 
        //                                          FROM Tag AS t
        //                                          INNER JOIN TagMap as tm
        //                                            ON tm.Tag_Id = t.Id
        //                                          LEFT JOIN TagType AS tt
        //                                            ON t.TagType_Id = tt.Id 
        //                                          WHERE tm.File_Id = @Id";

        private static string AddQuery => @"INSERT INTO File(FilePath) VALUES(@FilePath);
                                            SELECT last_insert_rowid() FROM File;";

        private static string AddWithReferencesQuery(File entity)
        {
            return AddQuery + InsertTagMap(entity, "(SELECT Id FROM File ORDER BY Id DESC LIMIT 1)");
        }

        private static string InsertTagMap(File entity, string fileId)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            IEnumerator<Tag> tagsEnumerator = entity.Tags.GetEnumerator();
            while (tagsEnumerator.MoveNext())
            {
                sb.Append("INSERT INTO TagMap(File_Id, Tag_Id) VALUES(");
                sb.Append(fileId);
                sb.Append(", @Tag_Id");
                sb.Append(i);
                sb.Append(");");
                i++;
            }

            return sb.ToString();
        }

        private static void AddWithReferencesCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            File file = (File)entity;
            cmd.Parameters.Add("@FilePath", DbType.String).Value = file.FilePath;
            int i = 0;
            foreach (Tag tag in file.Tags)
            {
                cmd.Parameters.Add("@Tag_Id" + i, DbType.String).Value = tag.Id;
                i++;
            }
        }

        public void Add(File file)
        {
            SqliteHelper.Insert(AddWithReferencesQuery(file), AddWithReferencesCommandBinder, file);
        }

        private static string UpdateWithReferencesQuery(IEntity entity)
        {
            File file = (File)entity;
            string delete = "DELETE FROM TagMap WHERE File_Id = @FileId;";
            string insert = string.Empty;
            if (file.Tags.Any())
            {
                insert = InsertTagMap(file, "@FileId");
            }

            return delete + insert;
        }

        private static void UpdateWithReferencesCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            File file = (File)entity;
            cmd.Parameters.Add("@FileId", DbType.Int32).Value = entity.Id;
            BindTagIds(cmd, file);
        }

        private static void BindTagIds(SQLiteCommand cmd, File file)
        {
            int i = 0;
            foreach (Tag tag in file.Tags)
            {
                cmd.Parameters.Add("@Tag_Id" + i, DbType.String).Value = tag.Id;
                i++;
            }
        }

        public void Update(File file)
        {
            SqliteHelper.Update(UpdateWithReferencesQuery(file), UpdateWithReferencesCommandBinder, file);
        }

        private static string GetByFilePathQuery => @"SELECT Id, FilePath FROM File WHERE FilePath like @Where;" +
                                                   @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                                                     FROM Tag AS t
                                                     INNER JOIN TagMap as tm
                                                        ON tm.Tag_Id = t.Id
                                                     LEFT JOIN TagType AS tt
                                                        ON t.TagType_Id = tt.Id 
                                                     WHERE tm.File_Id = (SELECT Id FROM File WHERE FilePath like @Where)";

        private static void GetByFilePathCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            File file = (File)entity;
            cmd.Parameters.Add("@Where", DbType.String).Value = file.FilePath;
        }

        private static File ParseWithReferences(SQLiteDataReader dr)
        {
            if (!dr.Read()) return null;

            File file = Parse(dr);

            List<Tag> tags = new List<Tag>();
            dr.NextResult();

            while (dr.Read())
            {
                Tag tag;
                tags.Add(tag = new Tag
                {
                    Id = dr.GetInt32(0),
                    Description = dr.GetString(1)
                });

                object idTagTypeObj = dr.GetValue(2);
                if (idTagTypeObj != DBNull.Value)
                {
                    tag.TagType = new TagType
                    {
                        Id = dr.GetInt32(2),
                        Description = dr.GetString(3)
                    };
                }
            }

            file.Tags = tags;

            return file;
        }

        private static File Parse(SQLiteDataReader dr)
        {
            return new File
            {
                Id = dr.GetInt32(0),
                FilePath = dr.GetString(1)
            };
        }

        public File GetByFilename(string filename)
        {
            File file = null;
            SqliteHelper.GetById(GetByFilePathQuery, 
                                 GetByFilePathCommandBinder, 
                                 new File { FilePath = "%" + filename.Substring(1) },
                                 dr => file = ParseWithReferences(dr));
            return file;
        }

        private static string GetByTagQuery => @"SELECT f.*
                                                 FROM File AS f
                                                 INNER JOIN TagMap AS tm
                                                     ON f.Id = tm.File_Id
                                                 WHERE tm.Tag_Id = @Tag_Id";

        public IEnumerable<File> GetByTag(int tagId)
        {
            LinkedList<File> list = new LinkedList<File>();
            SqliteHelper.GetAllByCriteria(GetByTagQuery, dr =>
            {
                while (dr.Read())
                {
                    list.AddLast(Parse(dr));
                }
            }, cmd => cmd.Parameters.Add("@Tag_Id", DbType.Int32).Value = tagId);
            return list;
        }

        private static string GetByTagsQuery(IEnumerable<int> tagIds)
        {
            const string query = @"SELECT f.*
                                   FROM File AS f
                                   INNER JOIN TagMap AS tm
                                       ON f.Id = tm.File_Id
                                   WHERE tm.Tag_Id in (";

            StringBuilder sb = new StringBuilder();
            int i = 0;
            IEnumerator<int> tagsEnumerator = tagIds.GetEnumerator();
            while (tagsEnumerator.MoveNext())
            {
                sb.Append("@Tag_Id");
                sb.Append(i);
                sb.Append(",");
                i++;
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(") GROUP BY f.Id HAVING COUNT(DISTINCT tm.Tag_ID) = @nTagIds");

            return query + sb.ToString();
        }

        public IEnumerable<File> GetByTags(IEnumerable<int> tagIds)
        {
            LinkedList<File> list = new LinkedList<File>();
            SqliteHelper.GetAllByCriteria(GetByTagsQuery(tagIds), dr =>
            {
                while (dr.Read())
                {
                    list.AddLast(Parse(dr));
                }
            }, cmd => 
            {
                int i = 0;
                foreach (int tagId in tagIds)
                {
                    cmd.Parameters.Add("@Tag_Id" + i, DbType.Int32).Value = tagId;
                    i++;
                }
                cmd.Parameters.Add("@nTagIds", DbType.Int32).Value = i;
            });
            return list;
        }
        
    }
}
