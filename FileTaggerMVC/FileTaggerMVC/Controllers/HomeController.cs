using AutoMapper;
using FileTaggerMVC.Filters;
using FileTaggerMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using FileTaggerMVC.RestSharp.Abstract;
using FileTaggerMVC.Models.Base;

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
            List<BaseTag> list = _tagRest.Get();
            List<IGrouping<TagTypeViewModel, TagViewModel>> viewModelList = 
                Mapper.Map<List<BaseTag>, List<TagViewModel>>(list)
                .GroupBy(t => t.TagType)
                .ToList();

            return View(viewModelList);
        }

        public ActionResult ByTags(int[] tagIds)
        {
            if (tagIds == null || tagIds.Length == 0)
            {
                return Content("<hr />Select at least one tag", "text/html");
            }
            
            List<BaseFile> files = _searchRest.GetByTags(tagIds);
            List<FileViewModel> list = Mapper.Map<List<BaseFile>, List<FileViewModel>>(files);
            return PartialView(list);
        }
    }

}