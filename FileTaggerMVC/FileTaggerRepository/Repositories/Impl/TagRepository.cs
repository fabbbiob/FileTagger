using FileTaggerModel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using FileTaggerRepository.Repositories.Abstract;

namespace FileTaggerRepository.Repositories.Impl
{
    public class TagRepository : RepositoryBase<Tag>
    {
        protected override string AddQuery => @"INSERT INTO Tag(Description, TagType_Id) 
                                                VALUES (@Description, @TagType_Id);
                                                SELECT last_insert_rowid() FROM Tag;";

        protected override void AddCommandBinder(SQLiteCommand cmd, Tag entity)
        {
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
            cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(entity);
        }
        
        protected override string UpdateQuery => @"UPDATE Tag 
                                                   SET Description = @Description, 
                                                   TagType_Id = @TagType_Id 
                                                   WHERE Id = @Id;";

        protected override void UpdateCommandBinder(SQLiteCommand cmd, Tag entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
            cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(entity);
        }

        protected override string DeleteQuery => "DELETE FROM Tag WHERE Id = @Id;";

        protected override void DeleteCommandBinder(SQLiteCommand cmd, Tag entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery => 
                       @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                         FROM Tag as t
                         LEFT JOIN TagType AS tt
                            ON t.TagType_Id = tt.Id 
                         WHERE t.Id = @Id;";

        protected override void GetByIdCommandBinder(SQLiteCommand cmd, int id)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = id;
        }

        protected override string GetAllQuery => 
                       @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                         FROM Tag as t
                         LEFT JOIN TagType AS tt
                            ON t.TagType_Id = tt.Id;";

        protected override Tag Parse(SQLiteDataReader dr)
        {
            Tag tag = new Tag
            {
                Id = dr.GetInt32(0),
                Description = dr.GetString(1)
            };

            object idTagTypeObj = dr.GetValue(2);
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

        protected override string GetByIdWithReferencesQuery =>
                        @"SELECT t.Id, t.Description
                          FROM Tag AS t
                          WHERE t.Id = @Id;
                          SELECT f.Id, f.FilePath
                          FROM File AS f
                          INNER JOIN TagMap AS tm
                          ON tm.File_Id = f.Id
                          WHERE tm.Tag_Id = @Id;";

        protected override Tag ParseWithReferences(SQLiteDataReader dr)
        {
            if (!dr.Read()) return null;
            Tag tag = Parse(dr);

            LinkedList<File> files = new LinkedList<File>();
            dr.NextResult();

            while (dr.Read())
            {
                files.AddLast(new File
                {
                    Id = dr.GetInt32(0),
                    FilePath = dr.GetString(1)
                });
            }

            tag.Files = files;

            return tag;
        }

        private static object GetTagTypeId(Tag tag)
        {
            if (tag.TagType != null)
            {
                return tag.TagType.Id;
            }
            return DBNull.Value;
        }
    }
}
