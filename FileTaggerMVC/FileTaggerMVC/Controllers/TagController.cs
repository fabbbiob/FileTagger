using System.Web.Configuration;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class TagController : Controller
    {
        private static string SqliteConnectionString
        {
            get
            {
                return WebConfigurationManager.AppSettings["SqliteConnectionString"];
            }
        }

        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }
    }
}