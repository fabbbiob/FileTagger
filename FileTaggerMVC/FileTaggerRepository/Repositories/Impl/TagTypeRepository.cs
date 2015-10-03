using FileTaggerModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTaggerRepository
{
    public class TagTypeRepository : RepositoryBase<TagType>
    {
        protected override string AddQuery
        {
            get 
            {
                return "INSERT INTO TagType(Description) VALUES (@Description)";
            }
        }
        protected override void AddCommandBuilder(SQLiteCommand cmd, TagType entity)
        {
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
        }

        protected override string UpdateQuery
        {
            get 
            {
                return "UPDATE TagType SET Description = @Description where Id = @Id";
            }
        }
        protected override void UpdateCommandBuilder(SQLiteCommand cmd, TagType entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
        }

        protected override string DeleteQuery
        {
            get 
            {
                return "DELETE FROM TagType WHERE Id = @Id";
            }
        }
        protected override void DeleteCommandBuilder(SQLiteCommand cmd, TagType entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery
        {
            get 
            {
                return "SELECT Id, Description FROM TagType WHERE Id = @Id"; 
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
                return "SELECT Id, Description FROM TagType";
            }
        }

        protected override TagType Parse(SQLiteDataReader dr)
        {
            return new TagType
            {
                Id = dr.GetInt32(0),
                Description = dr.GetString(1)
            };
        }
    }
}
