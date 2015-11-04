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
        private TagRepository _tagRepository;

        public FileController()
        {
            _fileRepository = new FileRepository();
            _tagRepository = new TagRepository();
        }

        // GET: /File/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /File/ListFiles?folderPath=path
        public ActionResult ListFiles(string folderPath)
        {
            //TODO validate folderPath

            Session["folderPath"] = folderPath;
            JsTreeNodeModel root = new JsTreeNodeModel
            {
                Text = folderPath.Substring(folderPath.LastIndexOf(@"\") + 1),
                State = new JsTreeNodeState()
            };

            DirectorySearch(folderPath, root);
            return View("ListFiles", null, JsonConvert.SerializeObject(root));
        }

        // GET: /File/CreateOrEditFile?fileName=name
        public PartialViewResult Details(string fileName)
        {
            Session["fileName"] = fileName;
            FileTaggerModel.Model.File file = _fileRepository.GetByFilename(fileName);
            if (file != null)
            {
                FileViewModel fileViewModel = Mapper.Map<FileTaggerModel.Model.File, FileViewModel>(file);
                return PartialView("Details", fileViewModel);
            }
            else
            {
                return PartialView("CreateLink", new NoFileViewModel { FileName = fileName.Substring(fileName.LastIndexOf(@"\") + 1) });
            }
        }

        // GET: /File/Edit/5
        public ActionResult Edit(int id)
        {
            FileTaggerModel.Model.File file = _fileRepository.GetByFilename((string)Session["fileName"]);
            FileViewModel fileViewModel = Mapper.Map<FileTaggerModel.Model.File, FileViewModel>(file);

            ViewBag.Action = "Edit";
            return View("CreateOrEdit", fileViewModel);
        }

        // POST: /File/Edit
        [HttpPost]
        public ActionResult Edit([ModelBinder(typeof(FileViewModelBinder))]FileViewModel fileViewModel)
        {
            if (ModelState.IsValid)
            {
                FileTaggerModel.Model.File editedFile = Mapper.Map<FileViewModel, FileTaggerModel.Model.File>(fileViewModel);
                _fileRepository.Update(editedFile);
            }

            return RedirectToAction("ListFiles", new { folderPath = (string)Session["folderPath"] });
        }

        // GET: /File/Create
        public ActionResult Create()
        {
            FileViewModel fileViewModel = new FileViewModel
            {
                FilePath = (string)Session["fileName"]
            };
            LoadTagTypes(fileViewModel);

            ViewBag.Action = "Create";
            return View("CreateOrEdit", fileViewModel);
        }

        // POST: /File/Create
        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(FileViewModelBinder))]FileViewModel fileViewModel)
        {
            if (ModelState.IsValid)
            {
                FileTaggerModel.Model.File file = Mapper.Map<FileViewModel, FileTaggerModel.Model.File>(fileViewModel);
                _fileRepository.Add(file);
            }

            return RedirectToAction("ListFiles", new { folderPath = (string)Session["folderPath"] });
        }

        public ActionResult ByTag(int tagId)
        {
            IEnumerable<FileTaggerModel.Model.File> files = _fileRepository.GetByTag(tagId);
            return View(files);
        }

        private static void DirectorySearch(string folderPath, JsTreeNodeModel root)
        {
            root.Children = new List<JsTreeNodeModel>();
            root.State.Opened = true;

            foreach (string fileName in Directory.GetFiles(folderPath))
            {
                FileInfo fileInfo = new FileInfo(fileName);
                root.Children.Add(new JsTreeNodeModel
                {
                    Text = fileInfo.Name,
                    Type = "leaf",
                    Attr = new JsTreeAttr { DataFilename = fileInfo.FullName },
                    State = new JsTreeNodeState()
                });
            }

            foreach (string directoryName in Directory.GetDirectories(folderPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

                JsTreeNodeModel node = new JsTreeNodeModel
                {
                    Text = directoryInfo.Name,
                    State = new JsTreeNodeState()
                };

                DirectorySearch(directoryName, node);

                root.Children.Add(node);
            }
        }

        private void LoadTagTypes(FileViewModel fileViewModel)
        {
            List<Tag> tags = _tagRepository.GetAll().ToList();
            fileViewModel.Tags = new MultiSelectList(tags, "Id", "Description");
        }
    }

    internal class JsTreeNodeModel
    {
        [JsonProperty(PropertyName = "text")]
        public string Text;
        [JsonProperty(PropertyName = "state")]
        public JsTreeNodeState State;
        [JsonProperty(PropertyName = "children")]
        public List<JsTreeNodeModel> Children;
        [JsonProperty(PropertyName = "type")]
        public string Type;
        [JsonProperty(PropertyName = "a_attr")]
        public JsTreeAttr Attr;
    }

    internal class JsTreeNodeState
    {
        public JsTreeNodeState()
        {
            Opened = Disabled = Selected = false;
        }

        [JsonProperty(PropertyName = "opened")]
        public bool Opened;
        [JsonProperty(PropertyName = "disabled")]
        public bool Disabled;
        [JsonProperty(PropertyName = "selected")]
        public bool Selected;
    }

    internal class JsTreeAttr
    {
        [JsonProperty(PropertyName = "data-filename")]
        public string DataFilename;
    }
}
