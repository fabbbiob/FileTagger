using FileTaggerModel.Model;

namespace FileTaggerRepository.Repositories.Abstract
{
    interface IFileRepository
    {
        int Add(File tagType);
        void Update(File file);
        File GetByFilename(string filename);
    }
}
