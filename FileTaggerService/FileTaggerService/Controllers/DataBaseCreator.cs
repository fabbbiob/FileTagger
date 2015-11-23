using FileTaggerRepository.Helpers;
using System.Web.Http;

namespace FileTaggerService.Controllers
{
    public class DataBaseCreator : ApiController
    {
        // GET: api/DataBaseCreator
        public void Get()
        {
            DbCreator.CreateDatabase();
        }
    }
}