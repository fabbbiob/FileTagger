using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using FileTaggerMVC.RestSharp.Abstract;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class HomeController : BaseController
    {
        private readonly ITagRest _tagRest;
        private readonly ISearchRest _searchRest;

        public HomeController(ITagRest tagRest, ISearchRest searchRest) : base()
        {
            _tagRest = tagRest;
            _searchRest = searchRest;
        }

        public ActionResult Index()
        {
            List<Tag> list = _tagRest.Get();
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
                // TODO move to new view
                return Content("<hr />Select at least one tag", "text/html");
            }
            
            List<File> files = _searchRest.GetByTags(tagIds);
            List<FileViewModel> list = Mapper.Map<List<File>, List<FileViewModel>>(files);
            return PartialView(list);
        }
    }
}