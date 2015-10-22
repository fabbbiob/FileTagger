using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using FileTaggerMVC.Models;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Impl;
using AutoMapper;
using FileTaggerMVC.ModelBinders;

namespace FileTaggerMVC.Controllers
{
    public class FileController : Controller
    {
        private FileRepository _fileRepository;

        public FileController()
        {
            _fileRepository = new FileRepository();
        }

        //
        // GET: /File/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /File/ListFiles/
        public ActionResult ListFiles(string folderPath)
        {
            TempData["folderPath"] = folderPath;
            JsTreeNodeModel root = new JsTreeNodeModel
            {
                Text = "root"
            };

            DirectorySearch(folderPath, root);
            string json = JsonConvert.SerializeObject(root);
            return View("ListFiles", null, json);
        }

        //
        // GET: /File/CreateOrEditFile
        public PartialViewResult Details(string fileName)
        {
            TempData["fileName"] = fileName;
            FileTaggerModel.Model.File file = _fileRepository.Get("FilePath", fileName).FirstOrDefault();
            if (file != null)
            {
                FileViewModel fileViewModel = Mapper.Map<FileTaggerModel.Model.File, FileViewModel>(file);
                TempData["FileViewModel"] = fileViewModel;
                return PartialView("Details", fileViewModel);
            }
            else
            {
                return PartialView("CreateLink");
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            return View("CreateOrEdit", TempData["FileViewModel"]);
        }

        [HttpPost]
        public ActionResult Edit([ModelBinder(typeof(FileViewModelBinder))]FileViewModel fileViewModel)
        {
            if (ModelState.IsValid)
            {
                FileTaggerModel.Model.File editedFile = Mapper.Map<FileViewModel, FileTaggerModel.Model.File>(fileViewModel);
                _fileRepository.UpdateWithReferences(editedFile);
            }

            return RedirectToAction("ListFiles", new { folderPath = (string)TempData["folderPath"] });
        }

        public ActionResult Create()
        {
            FileViewModel fileViewModel = new FileViewModel
            {
                FilePath = (string)TempData["fileName"]
            };
            LoadTagTypes(fileViewModel);

            ViewBag.Action = "Create";
            return View("CreateOrEdit", fileViewModel);
        }

        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(FileViewModelBinder))]FileViewModel fileViewModel)
        {
            if (ModelState.IsValid)
            {
                FileTaggerModel.Model.File file = Mapper.Map<FileViewModel, FileTaggerModel.Model.File>(fileViewModel);
                _fileRepository.AddWithReferences(file);
            }

            return RedirectToAction("ListFiles", new { folderPath = (string)TempData["folderPath"] });
        }

        private static void DirectorySearch(string folderPath, JsTreeNodeModel root)
        {
            root.Children = new List<JsTreeNodeModel>();

            foreach (string fileName in Directory.GetFiles(folderPath))
            {
                FileInfo fileInfo = new FileInfo(fileName);
                root.Children.Add(new JsTreeNodeModel
                {
                    Text = fileInfo.Name,
                    Type = "leaf",
                    Attr = new JsTreeAttr { DataFilename = fileInfo.FullName }
                });
            }

            foreach (string directoryName in Directory.GetDirectories(folderPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

                JsTreeNodeModel node = new JsTreeNodeModel
                {
                    Text = directoryInfo.Name
                };

                DirectorySearch(directoryName, node);

                root.Children.Add(node);
            }
        }
        
        //TODO
        private static void LoadTagTypes(FileViewModel fileViewModel)
        {
            List<Tag> tags = new TagRepository().GetAll().ToList();
            fileViewModel.Tags = new MultiSelectList(tags, "Id", "Description");
        }
    }

    internal class JsTreeNodeModel
    {
        [JsonProperty(PropertyName = "text")]
        public string Text;
        [JsonProperty(PropertyName = "children")]
        public List<JsTreeNodeModel> Children;
        [JsonProperty(PropertyName = "type")]
        public string Type;
        [JsonProperty(PropertyName = "a_attr")]
        public JsTreeAttr Attr;
    }

    internal class JsTreeAttr
    {
        [JsonProperty(PropertyName = "data-filename")]
        public string DataFilename;
    }
}
