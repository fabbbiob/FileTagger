using FileTaggerMVC.DAL;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            var list = TagDal.GetAll().ToList();
            return View(list);
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            var tagTypes = TagTypeDal.GetAll().ToList();
            ViewBag.TagTypes = tagTypes;
            var tag = new Tag();
            tag.TagTypeViewModel = new DropDownListViewModel();

            tag.TagTypeViewModel.Items = new List<SelectListItem>();
            tag.TagTypeViewModel.Items.Add(new SelectListItem { Text = "None", Value = "-1" });
            tag.TagTypeViewModel.Items.AddRange(tagTypes.Select(tt => new SelectListItem { Text = tt.Description, Value = tt.Id.ToString() }).ToList());

            return View(tag);
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(TagModelBinder))]Tag tag)
        {
            if (ModelState.IsValid)
            {
                TagDal.Create(tag);
            }

            return RedirectToAction("Index");
        }

        //TODO edit, delete

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
