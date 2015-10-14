using FileTaggerModel.Model;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Models;
using FileTaggerRepository.Repositories.Impl;
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
            List<Tag> list = new TagRepository().GetAll().ToList();
            //TODO user AutoMapper
            List<TagViewModel> viewModelList = new List<TagViewModel>();
            foreach (Tag tag in list)
            {
                TagViewModel tagViewModel = new TagViewModel
                {
                    Id = tag.Id,
                    Description = tag.Description
                };

                if (tag.TagType != null)
                {
                    tagViewModel.TagType = new TagTypeViewModel
                    {
                        Id = tag.TagType.Id,
                        Description = tag.TagType.Description
                    };
                }

                viewModelList.Add(tagViewModel);
            }

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
        public ActionResult Create([ModelBinder(typeof(TagModelBinder))]TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO user AutoMapper
                Tag tag = new Tag
                {
                    Description = tagViewModel.Description,
                };
                if (tagViewModel.TagTypeId != null)
                {
                    tag.TagType = new TagType
                    {
                        Id = tagViewModel.TagTypeId
                    };
                }
                new TagRepository().Add(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {    
            Tag tag = new TagRepository().GetById(id);
            //TODO user AutoMapper
            TagViewModel tagViewModel = new TagViewModel
            {
                Id = tag.Id,
                Description = tag.Description,
                TagTypeId = tag.TagType == null ? -1 : tag.TagType.Id
            };

            //TODO remove this
            LoadTagTypes(tagViewModel);
            
            return View(tagViewModel);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO user AutoMapper
                Tag tag = new Tag
                {
                    Description = tagViewModel.Description
                };

                if (tagViewModel.TagTypeId != null)
                {
                    tag.TagType = new TagType
                    {
                        Id = tagViewModel.TagTypeId
                    };
                }

                new TagRepository().Update(tag);
            }

            return RedirectToAction("Index");
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new TagRepository().GetById(id));
        }

        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(TagViewModel tagViewModel)
        {
            //TODO user AutoMapper
            Tag tag = new Tag
            {
                Description = tagViewModel.Description,
                TagType = new TagType
                {
                    Id = tagViewModel.TagType.Id,
                    Description = tagViewModel.TagType.Description
                }
            };
            new TagRepository().Delete(tag);

            return RedirectToAction("Index");
        }

        private static void LoadTagTypes(TagViewModel tag)
        {
            List<TagType> tagTypes = new TagTypeRepository().GetAll().ToList();
            //TODO user AutoMapper
            List<TagTypeViewModel> viewModelList = new List<TagTypeViewModel>();
            foreach (TagType tagType in tagTypes)
            {
                viewModelList.Add(new TagTypeViewModel
                {
                    Id = tagType.Id,
                    Description = tagType.Description
                });
            }

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
