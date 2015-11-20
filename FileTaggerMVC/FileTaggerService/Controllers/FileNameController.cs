using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class FileNameController : ApiController
    {
        private readonly IFileRepository _fileRepository;

        public FileNameController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        // POST: api/FileName
        [HttpGet]
        public File GetByFilename([FromUri]string filename)
        {
            return _fileRepository.GetByFilename(filename);
        }
    }
}
