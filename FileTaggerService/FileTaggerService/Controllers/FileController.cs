using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using System.Collections.Generic;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class FileController : ApiController
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        // GET: api/File/{id}
        public IEnumerable<File> Get([FromUri]int id)
        {
            return _fileRepository.GetByTag(id);
        }

        // GET: api/File?tagIds=5
        public IEnumerable<File> Get([FromUri]int[] tagIds)
        {
            return _fileRepository.GetByTags(tagIds);
        }

        // POST: api/File
        public void Post([FromBody]File file)
        {
            _fileRepository.Add(file);
        }

        // PUT: api/File/5
        public void Put([FromBody]File file)
        {
            _fileRepository.Update(file);
        }
    }
}
