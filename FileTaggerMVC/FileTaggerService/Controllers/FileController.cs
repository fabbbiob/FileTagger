using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Repositories.Impl;
using System.Collections.Generic;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class FileController : ApiController
    {
        private readonly IFileRepository _fileRepository;

        public FileController()
        {
            _fileRepository = new FileRepository();
        }

        // GET: api/File?tagId=5
        public IEnumerable<File> Get([FromUri]int tagId)
        {
            return _fileRepository.GetByTag(tagId);
        }

        // GET: api/File?tagIds=5
        public IEnumerable<File> Get([FromUri]int[] tagIds)
        {
            return _fileRepository.GetByTags(tagIds);
        }

        // GET: api/File?filename=abc
        public File Get([FromUri]string filename)
        {
            return _fileRepository.GetByFilename(filename);
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
