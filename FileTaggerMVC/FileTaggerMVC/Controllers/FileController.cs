using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        // GET: /File/ListFiles/
        public ActionResult ListFiles(string folderPath)
        {
            JsTreeNodeModel root = new JsTreeNodeModel
            {
                text = "root"
            };

            DirSearch(folderPath, root);
            string json = JsonConvert.SerializeObject(root);
            return View("ListFiles", null, json);
        }

        //
        // GET: /File/CreateOrEditFile
        public ActionResult CreateOrEditFile(string fileName)
        {
            // TODO
            throw new System.NotImplementedException();
        }

        private static void DirSearch(string folderPath, JsTreeNodeModel root)
        {
            root.children = new List<JsTreeNodeModel>();

            foreach (string fileName in Directory.GetFiles(folderPath))
            {
                FileInfo fi = new FileInfo(fileName);
                root.children.Add(new JsTreeNodeModel { text = fi.Name, type = "leaf" });
            }

            foreach (string directoryName in Directory.GetDirectories(folderPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

                JsTreeNodeModel node = new JsTreeNodeModel();
                node.text = directoryInfo.Name;
                DirSearch(directoryName, node);
                root.children.Add(node);
            }
        }

    }

    public class JsTreeNodeModel
    {
        //public string id;
        public string text;
        //public string icon;
        //public JsTreeNoteStateModel state;
        public List<JsTreeNodeModel> children;
        public string type;
    }

    //public class JsTreeNoteStateModel
    //{
    //    public bool opened;
    //    public bool disabled;
    //    public bool selected;
    //}
}
