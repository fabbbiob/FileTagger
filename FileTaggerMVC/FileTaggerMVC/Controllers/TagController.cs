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
            List<Tag> list = TagDal.GetAll().ToList();

            return View(list);
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            Tag tag = new Tag();
            LoadTagTypes(tag);

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

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            Tag tag = TagDal.Get(id);
            LoadTagTypes(tag);
            tag.TagTypeId = tag.TagType == null ? -1 : tag.TagType.Id;

            return View(tag);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                TagDal.Edit(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View(TagDal.Get(id));
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(Tag tag)
        {
            TagDal.Delete(tag.Id);

            return RedirectToAction("Index");
        }

        private static void LoadTagTypes(Tag tag)
        {
            IEnumerable<TagType> tagTypes = TagTypeDal.GetAll().ToList();

            tag.TagTypeViewModel = new DropDownListViewModel();
            tag.TagTypeViewModel.Items = new List<SelectListItem>();
            tag.TagTypeViewModel.Items.Add(new SelectListItem { Text = "None", Value = "-1" });
            tag.TagTypeViewModel.Items.AddRange(tagTypes
                                                    .Select(tt => new SelectListItem
                                                    {
                                                        Text = tt.Description,
                                                        Value = tt.Id.ToString()
                                                    }).ToList());
        }
    }
}
