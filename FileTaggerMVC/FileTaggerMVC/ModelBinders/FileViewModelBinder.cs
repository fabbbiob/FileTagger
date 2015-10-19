using FileTaggerMVC.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileTaggerMVC.ModelBinders
{
    public class FileViewModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string filePath = request.Form.Get("FilePath");
            string tagIds = request.Form.Get("Tags[]");

            if (!string.IsNullOrEmpty(filePath))
            {
                return new FileViewModel
                {
                    FilePath = filePath,
                    TagIds = tagIds.Split(',').Select(int.Parse).ToArray()
                };
            }

            return null;
        }
    }
}