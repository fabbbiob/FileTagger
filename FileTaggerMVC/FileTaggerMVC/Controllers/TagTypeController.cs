using AutoMapper;
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
        private TagTypeRepository _tagTypeRepository;

        public TagTypeController()
        {
            _tagTypeRepository = new TagTypeRepository();
        }

        // GET: TagType
        public ActionResult Index()
        {
            List<TagType> list = _tagTypeRepository.GetAll().ToList();
            List<TagTypeViewModel> viewModelList = 
                Mapper.Map<IEnumerable<TagType>, IEnumerable<TagTypeViewModel>>(list).ToList();
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
                _tagTypeRepository.Add(tagType);
            }          

            return RedirectToAction("Index");
        }

        // GET: TagType/Edit/5
        public ActionResult Edit(int id)
        {
            TagType tagType = _tagTypeRepository.GetById(id);
            TagTypeViewModel tagTypeViewModel = Mapper.Map<TagType, TagTypeViewModel>(tagType);

            ViewBag.Action = "Edit";
            return View("CreateOrEdit", tagTypeViewModel);
        }

        // POST: TagType/Edit/5
        [HttpPost]
        public ActionResult Edit(TagTypeViewModel tagTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                TagType tagType = Mapper.Map<TagTypeViewModel, TagType>(tagTypeViewModel);
                _tagTypeRepository.Update(tagType);
            }

            return RedirectToAction("Index");
        }

        // GET: TagType/Delete/5
        public ActionResult Delete(int id)
        {
            TagType tagType = _tagTypeRepository.GetById(id);
            TagTypeViewModel tagTypeViewModel = Mapper.Map<TagType, TagTypeViewModel>(tagType);
            return View("Delete", tagTypeViewModel);
        }

        // POST: TagType/Delete/5
        [HttpPost]
        public ActionResult Delete(TagTypeViewModel tagTypeViewModel)
        {
            TagType tagType = Mapper.Map<TagTypeViewModel, TagType>(tagTypeViewModel);
            _tagTypeRepository.Delete(tagType.Id);
            return RedirectToAction("Index");
        }
    }
}
