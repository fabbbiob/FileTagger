using log4net;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILog _log;

        public BaseController()
        {
            _log = Logger.Logger.Get();
        }
    }
}