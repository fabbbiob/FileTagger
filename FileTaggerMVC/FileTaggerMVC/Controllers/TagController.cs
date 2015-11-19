using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Models;
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
        private readonly RestClient _client;

        public TagController() : base()
        {
            _client = new RestClient(WebConfigurationManager.AppSettings["FileTaggerServiceUrl"]);
        }

        // GET: Tag
        public ActionResult Index()
        {
            RestRequest request = new RestRequest("api/tag", Method.GET);
            IRestResponse<List<Tag>> response = _client.Execute<List<Tag>>(request);
            
            List<Tag> list = response.Data;
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
                RestRequest request = new RestRequest("api/tag", Method.POST);
                string json = JsonConvert.SerializeObject(tag);
                request.AddParameter("text/json", json, ParameterType.RequestBody);
                _client.Execute<TagType>(request);
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
                RestRequest request = new RestRequest("api/tag", Method.PUT);
                string json = JsonConvert.SerializeObject(tag);
                request.AddParameter("text/json", json, ParameterType.RequestBody);
                _client.Execute<TagType>(request);
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
            RestRequest request = new RestRequest("api/tag", Method.DELETE);
            request.AddParameter("id", tagViewModel.Id.ToString());
            _client.Execute<TagType>(request);

            return RedirectToAction("Index");
        }

        private void LoadTagTypes(TagViewModel tag)
        {
            RestRequest request = new RestRequest("api/tagtype", Method.GET);
            IRestResponse<List<TagType>> response = _client.Execute<List<TagType>>(request);

            List<TagType> tagTypes = response.Data;
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
            RestRequest request = new RestRequest("api/tag/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse<Tag> response = _client.Execute<Tag>(request);
            return Mapper.Map<Tag, TagViewModel>(response.Data);
        }
    }
}
