using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;
using FileTaggerRepository.Repositories.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FileTaggerMVC.AutoMapper
{
    public class FileViewModelEntityConverter : ITypeConverter<File, FileViewModel>
    {
        public FileViewModel Convert(ResolutionContext context)
        {
            File file = (File)context.SourceValue;

            List<Tag> tags = new TagRepository().GetAll().ToList();
            FileViewModel fileViewModel = new FileViewModel
            {
                Id = file.Id,
                FilePath = file.FilePath,
                Tags = new MultiSelectList(tags, "Id", "Description", file.Tags.Select(t => t.Id).ToArray())
            };

            return fileViewModel;
        }
    }
}