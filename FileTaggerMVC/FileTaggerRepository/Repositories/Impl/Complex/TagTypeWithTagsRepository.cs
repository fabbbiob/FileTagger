using FileTaggerModel.Model;
using System.Collections.Generic;
using System.Data.SQLite;
using FileTaggerRepository.Repositories.Impl.Simple;

namespace FileTaggerRepository.Repositories.Impl.Complex
{
    public class TagTypeWithTagsRepository : TagTypeRepository
    {
        protected override string GetByIdQuery => 
                       @"SELECT tt.Id, tt.Description FROM TagType AS tt WHERE tt.Id = @Id;
                         SELECT t.Id, t.Description 
                         FROM TagType AS tt 
                         INNER JOIN TAG AS T ON tt.Id = t.TagType_Id 
                         WHERE tt.Id = @Id";

        protected override TagType Parse(SQLiteDataReader dr)
        {
            TagType tagType = base.Parse(dr);
            
            LinkedList<Tag> tags = new LinkedList<Tag>();
            dr.NextResult();

            while(dr.Read())
            {
                tags.AddLast(new Tag 
                {
                    Id = dr.GetInt32(0),
                    Description = dr.GetString(1),
                    TagType = tagType
                });
            }

            tagType.Tags = tags;

            return tagType;
        }
    }
}
