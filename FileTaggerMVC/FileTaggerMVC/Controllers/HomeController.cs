using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //TODO
        public ActionResult ByTags(int[] tagIds)
        {
            return View();
        }
    }
}