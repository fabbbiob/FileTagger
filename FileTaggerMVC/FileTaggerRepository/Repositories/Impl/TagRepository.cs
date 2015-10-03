﻿using FileTaggerModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTaggerRepository
{
    public class TagRepository : RepositoryBase<Tag>
    {
        protected override string AddQuery
        {
            get
            {
                return "INSERT INTO Tag(Description, TagType_Id) VALUES (@Description, @TagType_Id";
            }
        }
        protected override void AddCommandBuilder(SQLiteCommand cmd, Tag entity)
        {
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
            cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(entity);
        }
        
        protected override string UpdateQuery
        {
            get
            {
                return "UPDATE Tag SET Description = @Description, TagType_Id = @TagType_Id where Id = @Id";
            }
        }
        protected override void UpdateCommandBuilder(SQLiteCommand cmd, Tag entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
            cmd.Parameters.Add("@TagType_Id", DbType.Int32).Value = GetTagTypeId(entity);
        }

        protected override string DeleteQuery
        {
            get
            {
                return "DELETE FROM Tag WHERE Id = @Id";
            }
        }
        protected override void DeleteCommandBuilder(SQLiteCommand cmd, Tag entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery
        {
            get
            {
                return @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                         FROM Tag as t
                         LEFT JOIN TagType AS tt
                            ON t.TagType_Id = tt.Id 
                         WHERE t.Id = @Id";
            }
        }
        protected override void GetByIdCommandBuilder(SQLiteCommand cmd, int id)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = id;
        }

        protected override string GetAllQuery
        {
            get
            {
                return @"SELECT t.Id, t.Description, tt.Id, tt.Description 
                         FROM Tag as t
                         LEFT JOIN TagType AS tt
                            ON t.TagType_Id = tt.Id";
            }
        }

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

        private static object GetTagTypeId(Tag tag)
        {
            if (tag.TagType != null)
            {
                return tag.TagType.Id;
            }
            else
            {
                return DBNull.Value;
            }
        }
    }
}
