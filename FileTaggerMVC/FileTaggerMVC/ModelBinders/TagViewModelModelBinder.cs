using FileTaggerMVC.Models;
using System.Web;
using System.Web.Mvc;

namespace FileTaggerMVC.ModelBinders
{
    public class TagViewModelModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string description = request.Form.Get("Description");
            string idTagTypeString = request.Form.Get("TagTypeViewModel");

            if (!string.IsNullOrEmpty(description))
            {
                int idTagType;
                if (int.TryParse(idTagTypeString, out idTagType))
                {
                    return new TagViewModel
                    {
                        Description = description,
                        TagTypeId = idTagType
                    };
                }
            }

            return null;
        }
    }
}