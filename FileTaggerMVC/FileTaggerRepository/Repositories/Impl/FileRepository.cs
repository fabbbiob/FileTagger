using FileTaggerModel.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace FileTaggerRepository.Repositories
{
    public class FileRepository : RepositoryBase<File>
    {
        protected override string AddQuery
        {
            get 
            {
                return "INSERT INTO File(FilePath) VALUES(@FilePath)"; 
            }
        }
        protected override void AddCommandBuilder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@FilePath", DbType.String).Value = entity.FilePath;
        }

        protected override string UpdateQuery
        {
            get 
            {
                return "UPDATE FILE SET FilePath = @FilePath WHERE Id = @Id";
            }
        }
        protected override void UpdateCommandBuilder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@FilePath", DbType.String).Value = entity.FilePath;
        }

        protected override string DeleteQuery
        {
            get 
            {
                return "DELETE FROM File WHERE Id = @Id";
            }
        }
        protected override void DeleteCommandBuilder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery
        {
            get 
            {
                return "SELECT Id, FilePath FROM File WHERE Id = @Id";
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

        protected override File Parse(SQLiteDataReader dr)
        {
            return new File
            {
                Id = dr.GetInt32(0),
                FilePath = dr.GetString(1)
            };
        }
    }
}
