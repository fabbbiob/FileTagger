using System.Collections.Generic;
using FileTaggerModel.Model;
using System.Data;
using System.Data.SQLite;
using System.Text;
using FileTaggerRepository.Repositories.Abstract;

namespace FileTaggerRepository.Repositories.Impl
{
    public class FileRepository : RepositoryBaseWithReferences<File>
    {
        protected override string AddQuery => "INSERT INTO File(FilePath) VALUES(@FilePath);";

        protected override void AddCommandBuilder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@FilePath", DbType.String).Value = entity.FilePath;
        }

        protected override string AddWithReferencesQuery(File entity)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            IEnumerator<Tag> tagsEnumerator = entity.Tags.GetEnumerator();
            while(tagsEnumerator.MoveNext())
            {
                sb.Append("INSERT INTO TagMap(File_Id, Tag_Id) VALUES(");
                sb.Append("(SELECT last_insert_rowid() FROM File)");
                sb.Append(", @Tag_Id");
                sb.Append(i);
                sb.Append(");");
                i++;
            }

            return AddQuery + sb;
        }

        protected override void AddWithReferencesCommandBuilder(SQLiteCommand cmd, File entity)
        {
            AddCommandBuilder(cmd, entity);
            int i = 0;
            foreach (var tag in entity.Tags)
            {
                cmd.Parameters.Add("@Tag_Id" + i, DbType.String).Value = tag.Id;
                i++;
            }
        }

        protected override string UpdateQuery => "UPDATE FILE SET FilePath = @FilePath WHERE Id = @Id";

        protected override void UpdateCommandBuilder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
            cmd.Parameters.Add("@FilePath", DbType.String).Value = entity.FilePath;
        }

        protected override string DeleteQuery => "DELETE FROM File WHERE Id = @Id";

        protected override void DeleteCommandBuilder(SQLiteCommand cmd, File entity)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = entity.Id;
        }

        protected override string GetByIdQuery => "SELECT Id, FilePath FROM File WHERE Id = @Id";

        protected override void GetByIdCommandBuilder(SQLiteCommand cmd, int id)
        {
            cmd.Parameters.Add("@Id", DbType.Int32).Value = id;
        }

        protected override string GetAllQuery => "SELECT Id, Description FROM TagType";

        protected override File Parse(SQLiteDataReader dr)
        {
            return new File
            {
                Id = dr.GetInt32(0),
                FilePath = dr.GetString(1)
            };
        }

        //TODO
        protected override string GetByIdWithReferencesQuery => "";

        //TODO
        protected override File ParseWithReferences(SQLiteDataReader dr)
        {
            if (!dr.Read()) return null;
            throw new System.NotImplementedException();
        }
    }
}
