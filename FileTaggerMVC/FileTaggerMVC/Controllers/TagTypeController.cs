using FileTaggerMVC.Models;
using FileTaggerMVC.Sqlite;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class TagTypeController : Controller
    {
        // GET: TagType
        public ActionResult Index()
        {
            using (var ctx = new TagsSQLiteContext())
            {
                var list = ctx.TagTypes.ToList();
                return View(list);
            }
        }

        // GET: TagType/Create
        public ActionResult Create()
        {
            return View(new TagType());
        }

        // POST: TagType/Create
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

        // GET: TagType/Edit/5
        public ActionResult Edit(int id)
        {
            using (var ctx = new TagsSQLiteContext())
            {
                return View(ctx.TagTypes.Find(id));
            }
        }

        // POST: TagType/Edit/5
        [HttpPost]
        public ActionResult Edit(TagType tagType)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new TagsSQLiteContext())
                {
                    ctx.TagTypes.Find(tagType.Id).Description = tagType.Description;
                    ctx.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: TagType/Delete/5
        public ActionResult Delete(int id)
        {
            using (var ctx = new TagsSQLiteContext())
            {
                return View(ctx.TagTypes.Find(id));
            }
        }

        // POST: TagType/Delete/5
        [HttpPost]
        public ActionResult Delete(TagType tagType)
        {
            using (var ctx = new TagsSQLiteContext())
            {
                var toDelete = ctx.TagTypes.Find(tagType.Id);

                ctx.TagTypes.Remove(toDelete);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
