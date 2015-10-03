using FileTaggerModel.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FileTaggerRepository.Repositories.Impl
{
    public class TagTypeWithTagsRepository : TagTypeRepository
    {
        protected override string GetByIdQuery
        {
            get
            {
                return @"SELECT tt.Id, tt.Description FROM TagType AS tt WHERE Id = @Id
                         
                         SELECT t.Id, t.Description 
                         FROM TagType AS tt 
                         INNER JOIN TAG AS T ON tt.Id = t.TagType_Id 
                         WHERE Id = @Id";
            }
        }

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
