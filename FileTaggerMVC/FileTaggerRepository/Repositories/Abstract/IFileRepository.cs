using FileTaggerModel.Model;

namespace FileTaggerRepository.Repositories.Abstract
{
    interface IFileRepository
    {
        void Add(File tagType);
        void Update(File file);
        File GetByFilename(string filename);
    }
}
