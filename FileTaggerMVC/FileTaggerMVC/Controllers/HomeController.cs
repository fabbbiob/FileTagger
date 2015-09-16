using FileTaggerMVC.Models;
using FileTaggerMVC.Sqlite;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class HomeController : Controller
    {
        private static string SqliteConnectionString
        {
            get
            {
                return WebConfigurationManager.AppSettings["SqliteConnectionString"];
            }
        }

        public ActionResult Index()
        {
            //using (var ctx = new TagsSQLiteContext(SqliteConnectionString))
            //{
            //    ctx.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            //    TagType tt = new TagType { Description = "Type1" };
            //    ctx.TagTypes.Add(tt);

            //    Tag t = new Tag { Description = "Tag1", TagType = tt };
            //    ctx.Tags.Add(t);

            //    ctx.SaveChanges();
            //}

            return View();
        }
    }
}