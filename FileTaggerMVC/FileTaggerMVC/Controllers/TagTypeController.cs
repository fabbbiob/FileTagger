using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.Models;
using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class TagTypeController : BaseController
    {
        private readonly ITagTypeRest _tagTypeRest;

        public TagTypeController(ITagTypeRest tagTypeRest) : base()
        {
            _tagTypeRest = tagTypeRest;
        }

        // GET: TagType
        public ActionResult Index()
        {
            List<TagType> list = _tagTypeRest.Get();
            List<TagTypeViewModel> viewModelList = Mapper.Map<List<TagType>, List<TagTypeViewModel>>(list);
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
                TagType tagType = Mapper.Map<TagTypeViewModel, TagType>(tagTypeViewModel);
                _tagTypeRest.Post(tagType);
            }          

            return RedirectToAction("Index");
        }

        // GET: TagType/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            TagTypeViewModel tagTypeViewModel = Get(id);
            return View("CreateOrEdit", tagTypeViewModel);
        }

        // POST: TagType/Edit/5
        [HttpPost]
        public ActionResult Edit(TagTypeViewModel tagTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                TagType tagType = Mapper.Map<TagTypeViewModel, TagType>(tagTypeViewModel);
                _tagTypeRest.Put(tagType);
            }

            return RedirectToAction("Index");
        }

        // GET: TagType/Delete/5
        public ActionResult Delete(int id)
        {
            TagTypeViewModel tagTypeViewModel = Get(id);
            return View("Delete", tagTypeViewModel);
        }

        // POST: TagType/Delete/5
        [HttpPost]
        public ActionResult Delete(TagTypeViewModel tagTypeViewModel)
        {
            _tagTypeRest.Delete(tagTypeViewModel.Id);
            return RedirectToAction("Index");
        }

        private TagTypeViewModel Get(int id)
        {
            TagType tagType = _tagTypeRest.Get(id);
            return Mapper.Map<TagType, TagTypeViewModel>(tagType);
        }
    }
}
