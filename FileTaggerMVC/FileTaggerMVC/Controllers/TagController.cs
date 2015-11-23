using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Models;
using FileTaggerMVC.RestSharp.Abstract;
using FileTaggerMVC.RestSharp.Impl;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class TagController : BaseController
    {
        private ITagRest _tagRest;
        private ITagTypeRest _tagTypeRest;

        // TODO DI
        public TagController() : base()
        {
            _tagRest = new TagRestSharp();
            _tagTypeRest = new TagTypeRestSharp();
        }

        // GET: Tag
        public ActionResult Index()
        {
            List<Tag> list = _tagRest.Get();
            List<TagViewModel> viewModelList = Mapper.Map<List<Tag>, List<TagViewModel>>(list);
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
                Tag tag = Mapper.Map<TagViewModel, Tag>(tagViewModel);
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
                Tag tag = Mapper.Map<TagViewModel, Tag>(tagViewModel);
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
            List<TagType> tagTypes = _tagTypeRest.Get();
            List<TagTypeViewModel> viewModelList = 
                Mapper.Map<IEnumerable<TagType>, IEnumerable<TagTypeViewModel>>(tagTypes).ToList();

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
            Tag tag = _tagRest.Get(id);
            return Mapper.Map<Tag, TagViewModel>(tag);
        }
    }
}
