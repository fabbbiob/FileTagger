using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerRepository.Repositories.Abstract
{
    public interface ITagRepository
    {
        void Add(Tag tagType);
        void Update(Tag tagType);
        void Delete(int id);
        IEnumerable<Tag> GetAll();
        Tag GetById(int id);
        IEnumerable<Tag> FilteredTags(IEnumerable<int> tagIds);
    }
}
