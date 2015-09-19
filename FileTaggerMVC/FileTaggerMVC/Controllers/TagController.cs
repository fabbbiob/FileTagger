using FileTaggerMVC.Models;
using FileTaggerMVC.Sqlite;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            using (var ctx = new TagsSQLiteContext())
            {
                var list = ctx.Tags.ToList();
                return View(list);
            }
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            using (var ctx = new TagsSQLiteContext())
            {
                ViewBag.TagTypes = ctx.TagTypes.ToList();
                var tag = new Tag();
                tag.TagTypeViewModel = new DropDownListViewModel();
                tag.TagTypeViewModel.Items = ctx.TagTypes.Select(tt => new SelectListItem {Text = tt.Description, Value = "1" }).ToList();
                return View(tag);
            }
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(Tag tag)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
