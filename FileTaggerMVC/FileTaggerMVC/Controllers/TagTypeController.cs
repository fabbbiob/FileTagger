using FileTaggerMVC.DAL;
using FileTaggerMVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class TagTypeController : Controller
    {
        // GET: TagType
        public ActionResult Index()
        {
            var list = TagTypeDal.GetAll().ToList();
            return View(list);
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
                TagTypeDal.Create(tagType);
            }          

            return RedirectToAction("Index");
        }

        // GET: TagType/Edit/5
        public ActionResult Edit(int id)
        {
            return View(TagTypeDal.Get(id));
        }

        // POST: TagType/Edit/5
        [HttpPost]
        public ActionResult Edit(TagType tagType)
        {
            if (ModelState.IsValid)
            {
                TagTypeDal.Edit(tagType);
            }

            return RedirectToAction("Index");
        }

        // GET: TagType/Delete/5
        public ActionResult Delete(int id)
        {
            return View(TagTypeDal.Get(id));
        }

        // POST: TagType/Delete/5
        [HttpPost]
        public ActionResult Delete(TagType tagType)
        {
            TagTypeDal.Delete(tagType.Id);

            return RedirectToAction("Index");
        }
    }
}
