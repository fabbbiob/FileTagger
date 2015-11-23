using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerRepository.Repositories.Abstract
{
    public interface IFileRepository
    {
        void Add(File tagType);
        void Update(File file);
        File GetByFilename(string filename);
        IEnumerable<File> GetByTag(int tagId);
        IEnumerable<File> GetByTags(IEnumerable<int> tagIds);
    }
}
