using FileTaggerModel.Model;
using System.Data;
using System.Data.SQLite;
using FileTaggerRepository.Repositories.Abstract;

namespace FileTaggerRepository.Repositories.Impl.Simple
{
    public class TagTypeRepository : RepositoryBase<TagType>
    {
        protected override string AddQuery => @"INSERT INTO TagType(Description) VALUES (@Description);
                                                SELECT last_insert_rowid() FROM TagType";

        protected override void AddCommandBuilder(SQLiteCommand cmd, TagType entity)
        {
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
        }

        protected override string UpdateQuery => "UPDATE TagType SET Description = @Description where Id = @Id";

        protected override void UpdateCommandBuilder(SQLiteCommand cmd, TagType entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@Description", DbType.String).Value = entity.Description;
        }

        protected override string DeleteQuery => "DELETE FROM TagType WHERE Id = @Id";

        protected override void DeleteCommandBuilder(SQLiteCommand cmd, TagType entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery => "SELECT Id, Description FROM TagType WHERE Id = @Id";

        protected override void GetByIdCommandBuilder(SQLiteCommand cmd, int id)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = id;
        }

        protected override string GetAllQuery => "SELECT Id, Description FROM TagType";

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
