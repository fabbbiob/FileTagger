using FileTaggerModel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using FileTaggerModel;
using FileTaggerRepository.Helpers;
using FileTaggerRepository.Repositories.Abstract;

namespace FileTaggerRepository.Repositories.Impl
{
    public class TagRepository : ITagRepository
    {
        private static Tag Parse(SQLiteDataReader dr)
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
        
        //protected override string GetByIdWithReferencesQuery =>
        //                GetByIdQuery +
        //                @"SELECT f.Id, f.FilePath
        //                  FROM File AS f
        //                  INNER JOIN TagMap AS tm
        //                  ON tm.File_Id = f.Id
        //                  WHERE tm.Tag_Id = @Id;";

        //protected override Tag ParseWithReferences(SQLiteDataReader dr)
        //{
        //    if (!dr.Read()) return null;
        //    Tag tag = Parse(dr);

        //    LinkedList<File> files = new LinkedList<File>();
        //    dr.NextResult();

        //    while (dr.Read())
        //    {
        //        files.AddLast(new File
        //        {
        //            Id = dr.GetInt32(0),
        //            FilePath = dr.GetString(1)
        //        });
        //    }

        //    tag.Files = files;

        //    return tag;
        //}

        private static object GetTagTypeId(Tag tag)
        {
            if (tag.TagType != null)
            {
                return tag.TagType.Id;
            }
            return DBNull.Value;
        }

        private string AddQuery => @"INSERT INTO Tag(Description, TagType_Id) 
                                     VALUES (@Description, @TagType_Id);
                                     SELECT last_insert_rowid() FROM Tag;";

        private void AddCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            Tag tag = (Tag) entity;
            cmd.Parameters.Add("@Description", DbType.String).Value = tag.Description;
            cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(tag);
        }

        public void Add(Tag tagType)
        {
            SqliteHelper.Insert(AddQuery, AddCommandBinder, tagType);
        }

        private string UpdateQuery => @"UPDATE Tag 
                                        SET Description = @Description, 
                                        TagType_Id = @TagType_Id 
                                        WHERE Id = @Id;";

        private void UpdateCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            Tag tag = (Tag)entity;
            cmd.Parameters.Add("@Id", DbType.Int32).Value = tag.Id;
            cmd.Parameters.Add("@Description", DbType.String).Value = tag.Description;
            cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(tag);
        }

        public void Update(Tag tagType)
        {
            SqliteHelper.Update(UpdateQuery, UpdateCommandBinder, tagType);
        }

        private string DeleteQuery => "DELETE FROM Tag WHERE Id = @Id;";

        private void DeleteCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        public void Delete(int id)
        {
            SqliteHelper.Delete(DeleteQuery, DeleteCommandBinder, new TagType { Id = id });
        }

        private string GetAllQuery =>
                       @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                         FROM Tag as t
                         LEFT JOIN TagType AS tt
                            ON t.TagType_Id = tt.Id;";

        public IEnumerable<Tag> GetAll()
        {
            LinkedList<Tag> list = new LinkedList<Tag>();
            SqliteHelper.GetAll(GetAllQuery, dr => { list.AddLast(Parse(dr)); });
            return list;
        }

        private string GetByIdQuery =>
                       @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                         FROM Tag as t
                         LEFT JOIN TagType AS tt
                            ON t.TagType_Id = tt.Id 
                         WHERE t.Id = @Id;";

        private void GetByIdCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        public Tag GetById(int id)
        {
            Tag tag = null;
            SqliteHelper.GetById(GetByIdQuery, GetByIdCommandBinder, new TagType { Id = id }, dr => tag = Parse(dr));
            return tag;
        }
    }
}
