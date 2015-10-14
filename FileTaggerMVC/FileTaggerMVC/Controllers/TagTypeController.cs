using FileTaggerModel.Model;
using FileTaggerMVC.Models;
using FileTaggerRepository.Repositories.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    public class TagTypeController : Controller
    {
        // GET: TagType
        public ActionResult Index()
        {
            List<TagType> list = new TagTypeRepository().GetAll().ToList();

            //TODO user AutoMapper
            List<TagTypeViewModel> viewModelList = new List<TagTypeViewModel>();
            foreach (TagType tagType in list)
            {
                viewModelList.Add(new TagTypeViewModel
                {
                    Id = tagType.Id,
                    Description = tagType.Description
                });
            }

            return View(viewModelList);
        }

        // GET: TagType/Create
        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("CreateOrEdit", new TagTypeViewModel());
        }

        // POST: TagType/Create
        [HttpPost]
        public ActionResult Create(TagTypeViewModel tagTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO use AutoMapper
                TagType tagType = new TagType
                {
                    Description = tagTypeViewModel.Description
                };
                new TagTypeRepository().Add(tagType);
            }          

            return RedirectToAction("Index");
        }

        // GET: TagType/Edit/5
        public ActionResult Edit(int id)
        {
            TagType tagType = new TagTypeRepository().GetById(id);
            //TODO use AutoMapper
            ViewBag.Action = "Edit";
            return View("CreateOrEdit", new TagTypeViewModel
            {
                Id = tagType.Id,
                Description = tagType.Description
            });
        }

        // POST: TagType/Edit/5
        [HttpPost]
        public ActionResult Edit(TagTypeViewModel tagTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO use AutoMapper
                TagType tagType = new TagType
                {
                    Id = tagTypeViewModel.Id,
                    Description = tagTypeViewModel.Description
                };
                new TagTypeRepository().Update(tagType);
            }

            return RedirectToAction("Index");
        }

        // GET: TagType/Delete/5
        public ActionResult Delete(int id)
        {
            TagType tagType = new TagTypeRepository().GetById(id);
            //TODO use AutoMapper
            return View("Delete", new TagTypeViewModel
            {
                Id = tagType.Id,
                Description = tagType.Description
            });
        }

        // POST: TagType/Delete/5
        [HttpPost]
        public ActionResult Delete(TagTypeViewModel tagTypeViewModel)
        {
            //TODO use AutoMapper
            TagType tagType = new TagType
            {
                Id = tagTypeViewModel.Id,
                Description = tagTypeViewModel.Description
            };
            new TagTypeRepository().Delete(tagType);

            return RedirectToAction("Index");
        }
    }
}
