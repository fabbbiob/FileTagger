﻿using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Models;
using FileTaggerRepository.Repositories.Abstract;
using FileTaggerRepository.Repositories.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class TagController : BaseController
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagTypeRepository _tagTypeRepository;

        public TagController() : base()
        {
            _tagRepository = new TagRepository();
            _tagTypeRepository = new TagTypeRepository();
        }

        // GET: Tag
        public ActionResult Index()
        {
            List<Tag> list = _tagRepository.GetAll().ToList();
            List<TagViewModel> viewModelList = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(list).ToList();
            return View(viewModelList);
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            TagViewModel tag = new TagViewModel();
            LoadTagTypes(tag, _tagTypeRepository);
            return View(tag);
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(TagViewModelModelBinder))]TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                Tag tag = Mapper.Map<TagViewModel, Tag>(tagViewModel);
                _tagRepository.Add(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {    
            Tag tag = _tagRepository.GetById(id);
            TagViewModel tagViewModel = Mapper.Map<Tag, TagViewModel>(tag);
            
            LoadTagTypes(tagViewModel, _tagTypeRepository);
            
            return View(tagViewModel);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                Tag tag = Mapper.Map<TagViewModel, Tag>(tagViewModel);
                _tagRepository.Update(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetById(id);
            TagViewModel tagViewModel = Mapper.Map<Tag, TagViewModel>(tag);
            return View(tagViewModel);
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(TagViewModel tagViewModel)
        {
            Tag tag = Mapper.Map<TagViewModel, Tag>(tagViewModel);
            _tagRepository.Delete(tag.Id);

            return RedirectToAction("Index");
        }

        private static void LoadTagTypes(TagViewModel tag, ITagTypeRepository tagTypeRepository)
        {
            List<TagType> tagTypes = tagTypeRepository.GetAll().ToList();
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
    }
}
