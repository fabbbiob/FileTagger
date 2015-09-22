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

        private static void DirSearch(string sDir, JsTreeNodeModel root)
        {
            root.children = new List<JsTreeNodeModel>();

            foreach (string f in Directory.GetFiles(sDir))
            {
                FileInfo fi = new FileInfo(f);
                root.children.Add(new JsTreeNodeModel { text = fi.Name, type = "leaf" });
            }

            foreach (string d in Directory.GetDirectories(sDir))
            {
                DirectoryInfo di = new DirectoryInfo(d);

                JsTreeNodeModel node = new JsTreeNodeModel();
                node.text = di.Name;
                DirSearch(d, node);
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
