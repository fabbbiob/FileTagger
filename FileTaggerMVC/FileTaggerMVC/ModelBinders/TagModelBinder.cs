using FileTaggerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileTaggerMVC.ModelBinders
{
    public class TagModelBinder : IModelBinder
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
                    return new Tag
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