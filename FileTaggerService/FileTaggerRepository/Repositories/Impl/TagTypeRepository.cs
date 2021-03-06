﻿using System.Collections.Generic;
using FileTaggerModel.Model;
using System.Data;
using System.Data.SQLite;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Helpers;
using FileTaggerModel;

namespace FileTaggerRepository.Repositories.Impl
{
    public class TagTypeRepository : ITagTypeRepository
    {
        //protected override string GetByIdWithReferencesQuery =>
        //               @"SELECT tt.Id, tt.Description 
        //                 FROM TagType AS tt 
        //                 WHERE tt.Id = @Id;
        //                 SELECT t.Id, t.Description 
        //                 FROM TagType AS tt 
        //                 INNER JOIN TAG AS T ON tt.Id = t.TagType_Id 
        //                 WHERE tt.Id = @Id;";

        //protected override TagType ParseWithReferences(SQLiteDataReader dr)
        //{
        //    if (!dr.Read()) return null;

        //    TagType tagType = Parse(dr);

        //    LinkedList<Tag> tags = new LinkedList<Tag>();
        //    dr.NextResult();

        //    while (dr.Read())
        //    {
        //        tags.AddLast(new Tag
        //        {
        //            Id = dr.GetInt32(0),
        //            Description = dr.GetString(1),
        //            TagType = tagType
        //        });
        //    }

        //    tagType.Tags = tags;

        //    return tagType;
        //}

        private static string AddQuery => @"INSERT INTO TagType(Description) VALUES (@Description);
                                            SELECT last_insert_rowid() FROM TagType;";

        private static void AddCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            cmd.Parameters.Add("@Description", DbType.String).Value = ((TagType)entity).Description;
        }

        public void Add(TagType tagType)
        {
            SqliteHelper.Insert(AddQuery, AddCommandBinder, tagType);
        }

        private static string DeleteQuery => "DELETE FROM TagType WHERE Id = @Id;";

        private static void DeleteCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        public void Delete(int id)
        {
            SqliteHelper.Delete(DeleteQuery, DeleteCommandBinder, new TagType { Id = id });
        }

        private static string GetAllQuery => "SELECT Id, Description FROM TagType;";

        private static TagType Parse(SQLiteDataReader dr)
        {
            return new TagType
            {
                Id = dr.GetInt32(0),
                Description = dr.GetString(1)
            };
        }

        public IEnumerable<TagType> GetAll()
        {
            LinkedList<TagType> list = new LinkedList<TagType>();
            SqliteHelper.GetAll(GetAllQuery, dr =>
            {
                while (dr.Read())
                {
                    list.AddLast(Parse(dr));
                }
            });
            return list;
        }

        private static string GetByIdQuery => "SELECT Id, Description FROM TagType WHERE Id = @Id;";

        private static void GetByIdCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        public TagType GetById(int id)
        {
            TagType tagType = null;
            SqliteHelper.GetById(GetByIdQuery, GetByIdCommandBinder, new TagType { Id = id }, dr =>
            {
                if (dr.Read())
                { 
                    tagType = Parse(dr);
                }
            });
            return tagType;
        }

        private static string UpdateQuery => "UPDATE TagType SET Description = @Description where Id = @Id;";

        private static void UpdateCommandBinder(SQLiteCommand cmd, IEntity entity)
        {
            TagType tagType = (TagType)entity;
            cmd.Parameters.Add("@Id", DbType.Int32).Value = tagType.Id;
            cmd.Parameters.Add("@Description", DbType.String).Value = tagType.Description;
        }

        public void Update(TagType tagType)
        {
            SqliteHelper.Update(UpdateQuery, UpdateCommandBinder, tagType);
        }
    }
}
