using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class TagTypeController : BaseController
    {
        private RestClient _client;

        public TagTypeController() : base()
        {
            _client = new RestClient(WebConfigurationManager.AppSettings["FileTaggerServiceUrl"]);
        }

        // GET: TagType
        public ActionResult Index()
        {
            RestRequest request = new RestRequest("api/tagtype", Method.GET);
            IRestResponse<List<TagType>> response = _client.Execute<List<TagType>>(request);

            List<TagType> list = response.Data;
            List<TagTypeViewModel> viewModelList = 
                Mapper.Map<List<TagType>, List<TagTypeViewModel>>(list);
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

                RestRequest request = new RestRequest("api/tagtype", Method.POST);
                string json = JsonConvert.SerializeObject(tagType);
                request.AddParameter("text/json", json, ParameterType.RequestBody);
                _client.Execute<TagType>(request);
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
                RestRequest request = new RestRequest("api/tagtype", Method.PUT);
                string json = JsonConvert.SerializeObject(tagType);
                request.AddParameter("text/json", json, ParameterType.RequestBody);
                _client.Execute<TagType>(request);
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
            RestRequest request = new RestRequest("api/tagtype", Method.DELETE);
            request.AddParameter("id", tagTypeViewModel.Id.ToString());
            _client.Execute<TagType>(request);
            return RedirectToAction("Index");
        }

        private TagTypeViewModel Get(int id)
        {
            RestRequest request = new RestRequest("api/tagtype/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse<TagType> response = _client.Execute<TagType>(request);
            return Mapper.Map<TagType, TagTypeViewModel>(response.Data);
        }
    }
}
