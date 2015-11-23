using System.IO;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class ValidateFilePathController : ApiController
    {
        // GET: api/ValidateFilePath?filePath=abc
        public bool Get([FromUri] string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
