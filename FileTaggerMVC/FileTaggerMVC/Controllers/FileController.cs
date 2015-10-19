using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using FileTaggerMVC.Models;
using FileTaggerMVC.DAL;
using FileTaggerModel.Model;
using FileTaggerRepository.Repositories.Impl;
using AutoMapper;
using FileTaggerMVC.ModelBinders;

namespace FileTaggerMVC.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /File/ListFiles/
        [HttpPost]
        public ActionResult ListFiles(string folderPath)
        {
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
            FileTaggerModel.Model.File file = new FileRepository().Get("FilePath", fileName).First();
            if (file != null)
            {
                return PartialView("Details", file);
            }
            else
            {
                return PartialView("CreateLink");
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            return View("CreateOrEdit", FileDal.Get(id));
        }

        public ActionResult Create()
        {
            FileViewModel fileViewModel = new FileViewModel();
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
                new FileRepository().Add(file);
            }

            return RedirectToAction("Index");
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
