using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using RestSharp;
using System.Web.Configuration;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class HomeController : BaseController
    {
        private readonly RestClient _client;

        public HomeController() : base()
        {
            _client = new RestClient(WebConfigurationManager.AppSettings["FileTaggerServiceUrl"]);
        }

        public ActionResult Index()
        {
            RestRequest request = new RestRequest("api/tag", Method.GET);
            IRestResponse<List<Tag>> response = _client.Execute<List<Tag>>(request);

            List<Tag> list = response.Data;
            List<TagViewModel> viewModelList = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(list)
                .OrderByDescending(t => t.TagType)
                .ThenBy(t => t.Description)
                .ToList();
            return View(viewModelList);
        }

        public ActionResult ByTags(int[] tagIds)
        {
            if (tagIds == null || tagIds.Length == 0)
            {
                return Content("<hr />Select at least one tag", "text/html");
            }
            
            RestRequest request = new RestRequest("api/file", Method.GET);
            foreach (int id in tagIds)
            {
                request.AddQueryParameter("tagIds", id.ToString());
            }
            IRestResponse<List<File>> response = _client.Execute<List<File>>(request);

            List<FileViewModel> list = Mapper.Map<List<File>, List<FileViewModel>>(response.Data);
            return PartialView(list);
        }
    }
}