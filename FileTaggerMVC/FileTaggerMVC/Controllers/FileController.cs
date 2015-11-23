using System;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using FileTaggerMVC.Models;
using FileTaggerModel.Model;
using AutoMapper;
using FileTaggerMVC.ModelBinders;
using FileTaggerMVC.Filters;
using RestSharp;
using FileTaggerMVC.RestSharp.Abstract;
using FileTaggerMVC.RestSharp.Impl;

namespace FileTaggerMVC.Controllers
{
    [FileTaggerHandleError]
    public class FileController : BaseController
    {
        private ITagRest _tagRest;
        private IFileRest _fileRest;
        private IProcessRest _processRest;
        private ISearchRest _searchRest;

        public FileController() : base()
        {
            // TODO DI
            _tagRest = new TagRestSharp();
            _fileRest = new FileRestSharp();
            _processRest = new ProcessRestSharp();
            _searchRest = new SearchRestSharp();
        }

        // GET: /File/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /File/ListFiles?folderPath=path
        public ActionResult ListFiles(string folderPath)
        {
            //TODO use web api to validate folderPath

            Session["folderPath"] = folderPath;
            JsTreeNodeModel root = new JsTreeNodeModel
            {
                Text = folderPath.Substring(folderPath.LastIndexOf(@"\", StringComparison.Ordinal) + 1),
                State = new JsTreeNodeState()
            };

            DirectorySearch(folderPath, root);
            return View("ListFiles", null, JsonConvert.SerializeObject(root));
        }

        // GET: /File/CreateOrEditFile?fileName=name
        public PartialViewResult Details(string fileName)
        {
            Session["fileName"] = fileName;

            FileTaggerModel.Model.File file = Get(fileName);
            if (file != null)
            {
                FileViewModel fileViewModel = Mapper.Map<FileTaggerModel.Model.File, FileViewModel>(file);
                return PartialView("Details", fileViewModel);
            }
            else
            {
                return PartialView("CreateLink", new NoFileViewModel
                {
                    FileName = fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1),
                    FilePath = fileName
                });
            }
        }

        // GET: /File/Edit/5
        public ActionResult Edit(int id)
        {
            FileTaggerModel.Model.File file = Get((string)Session["fileName"]); 
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
                _fileRest.Put(editedFile);
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
                _fileRest.Post(file);
            }

            return RedirectToAction("ListFiles", new { folderPath = (string)Session["folderPath"] });
        }

        // GET: /File/ByTag?tagId=1&description=test
        public ActionResult ByTag(int tagId, string description)
        {
            List<FileTaggerModel.Model.File> files = _searchRest.GetByTag(tagId);
            List<FileViewModel> list = Mapper.Map<List<FileTaggerModel.Model.File>, List<FileViewModel>>(files);
            return View(new ByTagModel
            {
                TagDescription = description,
                FileViewModels = list
            });
        }

        public bool Run(string filePath)
        {
            return _processRest.Run(filePath);
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
            List<Tag> tags = _tagRest.Get();
            fileViewModel.Tags = new MultiSelectList(tags, "Id", "Description");
        }

        private FileTaggerModel.Model.File Get(string fileName)
        {
            return _fileRest.Get(fileName);
        }
    }

    #region ViewModels
    public class ByTagModel
    {
        public string TagDescription;
        public List<FileViewModel> FileViewModels;
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
    #endregion
}
