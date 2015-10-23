using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ByTag(int[] tagIds)
        {
            return View();
        }
    }
}