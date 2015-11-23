using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;
using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.AutoMapper
{
    public class FileViewModelEntityConverter : ITypeConverter<File, FileViewModel>
    {
        private ITagRest _tagRest;

        public FileViewModelEntityConverter(ITagRest tagRest)
        {
            _tagRest = tagRest;
        }

        public FileViewModel Convert(ResolutionContext context)
        {
            File file = (File)context.SourceValue;
            
            FileViewModel fileViewModel = new FileViewModel
            {
                Id = file.Id,
                FilePath = file.FilePath                
            };

            if (file.Tags != null)
            {
                List<Tag> tags = _tagRest.Get();
                fileViewModel.Tags = new MultiSelectList(tags, "Id", "Description", file.Tags.Select(t => t.Id).ToArray());
            }

            return fileViewModel;
        }
    }
}