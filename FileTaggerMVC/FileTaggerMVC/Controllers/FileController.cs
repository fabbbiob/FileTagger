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
            var file = FileDal.Get(fileName);
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
            return View("CreateOrEdit", FileDal.Get(id));
        }

        public ActionResult Create()
        {
            //TODO
            FileViewModel fileViewModel = new FileViewModel();
            LoadTagTypes(fileViewModel);
            return View("CreateOrEdit", new FileViewModel());
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
            List<TagType> tagTypes = new TagTypeRepository().GetAll().ToList();
            List<TagTypeViewModel> viewModelList =
                Mapper.Map<IEnumerable<TagType>, IEnumerable<TagTypeViewModel>>(tagTypes).ToList();

            //tag.TagTypeViewModel = new DropDownListViewModel();
            //tag.TagTypeViewModel.Items = new List<SelectListItem>();
            //tag.TagTypeViewModel.Items.Add(new SelectListItem { Text = "None", Value = "-1" });
            //tag.TagTypeViewModel.Items.AddRange(viewModelList
            //                                        .Select(tt => new SelectListItem
            //                                        {
            //                                            Text = tt.Description,
            //                                            Value = tt.Id.ToString()
            //                                        }).ToList());
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
