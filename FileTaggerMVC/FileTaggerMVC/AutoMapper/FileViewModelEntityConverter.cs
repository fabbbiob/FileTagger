using AutoMapper;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;
using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.AutoMapper
{
    public class FileViewModelEntityConverter : ITypeConverter<BaseFile, FileViewModel>
    {
        private ITagRest _tagRest;

        public FileViewModelEntityConverter(ITagRest tagRest)
        {
            _tagRest = tagRest;
        }

        public FileViewModel Convert(ResolutionContext context)
        {
            BaseFile file = (BaseFile)context.SourceValue;
            
            FileViewModel fileViewModel = new FileViewModel
            {
                Id = file.Id,
                FilePath = file.FilePath                
            };

            if (file.Tags != null)
            {
                List<BaseTag> tags = _tagRest.Get();
                fileViewModel.Tags = new MultiSelectList(tags, "Id", "Description", file.Tags.Select(t => t.Id).ToArray());
            }

            return fileViewModel;
        }
    }
}