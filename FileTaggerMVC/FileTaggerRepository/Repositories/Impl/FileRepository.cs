using System;
using System.Collections.Generic;
using FileTaggerModel.Model;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Linq;
using FileTaggerRepository.Repositories.Abstract;

namespace FileTaggerRepository.Repositories.Impl
{
    public class FileRepository : RepositoryBaseWithReferences<File>
    {
        protected override string AddQuery => @"INSERT INTO File(FilePath) VALUES(@FilePath);
                                                SELECT last_insert_rowid() FROM File;";

        protected override void AddCommandBinder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@FilePath", DbType.String).Value = entity.FilePath;
        }

        protected override string AddWithReferencesQuery(File entity)
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

        protected override void AddWithReferencesCommandBinder(SQLiteCommand cmd, File entity)
        {
            AddCommandBinder(cmd, entity);
            BindTagIds(cmd, entity);
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

        protected override string UpdateQuery => @"UPDATE FILE SET FilePath = @FilePath WHERE Id = @Id;";

        protected override void UpdateCommandBinder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@FilePath", DbType.String).Value = entity.FilePath;
        }

        protected override string DeleteQuery => "DELETE FROM File WHERE Id = @Id;";

        protected override void DeleteCommandBinder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery => "SELECT Id, FilePath FROM File WHERE Id = @Id;";

        protected override void GetByIdCommandBinder(SQLiteCommand cmd, int id)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = id;
        }

        protected override string GetAllQuery => "SELECT Id, FilePath FROM File;";

        protected override File Parse(SQLiteDataReader dr)
        {
            return new File
            {
                Id = dr.GetInt32(0),
                FilePath = dr.GetString(1)
            };
        }

        protected override string GetByIdWithReferencesQuery => 
                                                GetByIdQuery +
                                                @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                                                  FROM Tag AS t
                                                  INNER JOIN TagMap as tm
                                                    ON tm.Tag_Id = t.Id
                                                  LEFT JOIN TagType AS tt
                                                    ON t.TagType_Id = tt.Id 
                                                  WHERE tm.File_Id = @Id";

        protected override string GetByWhereQuery => "SELECT Id, FilePath FROM File WHERE @Prop = '@Where';" +
                                                @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                                                  FROM Tag AS t
                                                  INNER JOIN TagMap as tm
                                                    ON tm.Tag_Id = t.Id
                                                  LEFT JOIN TagType AS tt
                                                    ON t.TagType_Id = tt.Id 
                                                  WHERE tm.File_Id = (SELECT Id FROM File WHERE @Prop = '@Where')";

        //TODO
        protected override File ParseWithReferences(SQLiteDataReader dr)
        {
            if (!dr.Read()) return null;

            File file = Parse(dr);

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

        protected override string UpdateWithReferencesQuery(File entity)
        {
            string delete = "DELETE FROM TagMap WHERE File_Id = @FileId;";
            string insert = string.Empty;
            if (entity.Tags.Any())
            {
                insert = InsertTagMap(entity, "@FileId");
            }
            
            return delete + insert;
        }

        protected override void UpdateWithReferencesCommandBinder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@FileId", DbType.Int32).Value = entity.Id;
            BindTagIds(cmd, entity);
        }
    }
}
