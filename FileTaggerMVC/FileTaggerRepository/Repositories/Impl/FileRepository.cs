using System;
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

        private static string GetByFilePathQuery => @"SELECT Id, FilePath FROM File WHERE FilePath = @Where;" +
                                                   @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                                                     FROM Tag AS t
                                                     INNER JOIN TagMap as tm
                                                        ON tm.Tag_Id = t.Id
                                                     LEFT JOIN TagType AS tt
                                                        ON t.TagType_Id = tt.Id 
                                                     WHERE tm.File_Id = (SELECT Id FROM File WHERE FilePath = @Where)";

        private static void GetByFilePathCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            File file = (File)entity;
            cmd.Parameters.Add("@Where", DbType.String).Value = file.FilePath;
        }

        private static File ParseWithReferences(SQLiteDataReader dr)
        {
            if (!dr.Read()) return null;

            File file = new File
            {
                Id = dr.GetInt32(0),
                FilePath = dr.GetString(1)
            };

            LinkedList<Tag> tags = new LinkedList<Tag>();
            dr.NextResult();

            while (dr.Read())
            {
                Tag tag;
                tags.AddLast(tag = new Tag
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

        public File GetByFilename(string filename)
        {
            File file = null;
            SqliteHelper.GetById(GetByFilePathQuery, GetByFilePathCommandBinder, new File { FilePath = filename }, dr => file = ParseWithReferences(dr));
            return file;
        }

        
    }
}
