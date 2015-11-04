using FileTaggerModel.Model;
using System.Collections.Generic;

namespace FileTaggerRepository.Repositories.Abstract
{
    interface IFileRepository
    {
        void Add(File tagType);
        void Update(File file);
        File GetByFilename(string filename);
        IEnumerable<File> GetByTag(Tag tag);
    }
}
