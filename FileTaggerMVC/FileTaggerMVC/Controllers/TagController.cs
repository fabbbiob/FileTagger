using FileTaggerMVC.Models;
using FileTaggerMVC.Sqlite;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Linq;

namespace FileTaggerMVC.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            using (var ctx = new TagsSQLiteContext())
            {
                ctx.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                ViewBag.Tags = ctx.Tags.ToList();
                ViewBag.TagTypes = ctx.TagTypes.ToList();
            }

            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(TagType tagType)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new TagsSQLiteContext())
                {
                    ctx.TagTypes.Add(tagType);
                    ctx.SaveChanges();
                }
            }
            
            return RedirectToAction("Index");
        }
    }
}