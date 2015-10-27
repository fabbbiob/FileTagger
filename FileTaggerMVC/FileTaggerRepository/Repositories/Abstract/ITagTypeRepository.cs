using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerRepository.Repositories.Abstract
{
    interface ITagTypeRepository
    {
        void Add(TagType tagType);
        void Update(TagType tagType);
        void Delete(int id);
        IEnumerable<TagType> GetAll();
        TagType GetById(int id);
    }
}
