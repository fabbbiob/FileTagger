using AutoMapper;
using FileTaggerMVC.Filters;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;
using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class TagController : BaseController
    {
        private readonly ITagRest _tagRest;
        private readonly ITagTypeRest _tagTypeRest;

        public TagController(ITagRest tagRest, ITagTypeRest tagTypeRest) : base()
        {
            _tagRest = tagRest;
            _tagTypeRest = tagTypeRest;
        }

        // GET: Tag
        public ActionResult Index()
        {
            List<BaseTag> list = _tagRest.Get();
            List<TagViewModel> viewModelList = Mapper.Map<List<BaseTag>, List<TagViewModel>>(list);
            return View(viewModelList);
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            TagViewModel tag = new TagViewModel();
            LoadTagTypes(tag);
            return View(tag);
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(TagViewModelModelBinder))]TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                BaseTag tag = Mapper.Map<TagViewModel, BaseTag>(tagViewModel);
                _tagRest.Post(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {    
            TagViewModel tagViewModel = Get(id);
            LoadTagTypes(tagViewModel);
            return View(tagViewModel);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                BaseTag tag = Mapper.Map<TagViewModel, BaseTag>(tagViewModel);
                _tagRest.Put(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            TagViewModel tagViewModel = Get(id);
            return View(tagViewModel);
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(TagViewModel tagViewModel)
        {
            _tagRest.Delete(tagViewModel.Id);
            return RedirectToAction("Index");
        }

        private void LoadTagTypes(TagViewModel tag)
        {
            List<BaseTagType> tagTypes = _tagTypeRest.Get();
            List<TagTypeViewModel> viewModelList = 
                Mapper.Map<List<BaseTagType>, List<TagTypeViewModel>>(tagTypes).ToList();

            tag.TagTypeViewModel = new DropDownListViewModel();
            tag.TagTypeViewModel.Items = new List<SelectListItem>();
            tag.TagTypeViewModel.Items.Add(new SelectListItem { Text = "None", Value = "-1" });
            tag.TagTypeViewModel.Items.AddRange(viewModelList
                                                    .Select(tt => new SelectListItem
                                                    {
                                                        Text = tt.Description,
                                                        Value = tt.Id.ToString()
                                                    }).ToList());
        }

        private TagViewModel Get(int id)
        {
            BaseTag tag = _tagRest.Get(id);
            return Mapper.Map<BaseTag, TagViewModel>(tag);
        }
    }
}
