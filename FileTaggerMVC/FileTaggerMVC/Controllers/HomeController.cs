using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Filters;
using FileTaggerMVC.Models;
using FileTaggerRepository.Repositories.Impl;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class HomeController : BaseController
    {
        private readonly FileRepository _fileRepository;
        private readonly TagRepository _tagRepository;

        public HomeController() : base()
        {
            _fileRepository = new FileRepository();
            _tagRepository = new TagRepository();
        }

        public ActionResult Index()
        {
            List<Tag> list = _tagRepository.GetAll().ToList();
            List<TagViewModel> viewModelList = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(list)
                .OrderByDescending(t => t.TagType)
                .ThenBy(t => t.Description)
                .ToList();
            return View(viewModelList);
        }

        // TODO view
        public ActionResult ByTags(int[] tagIds)
        {
            IEnumerable<File> files = _fileRepository.GetByTags(tagIds);
            List<FileViewModel> list = Mapper.Map<IEnumerable<File>, IEnumerable<FileViewModel>>(files).ToList();
            return View();
        }
    }
}