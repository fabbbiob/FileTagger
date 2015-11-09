using FileTaggerMVC.Filters;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        // TODO
        public ActionResult ByTags(int[] tagIds)
        {
            return View();
        }
    }
}