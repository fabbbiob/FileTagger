using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;
using System.Collections.Generic;

namespace FileTaggerMVC.AutoMapper
{
    public class FileEntityConverter : ITypeConverter<FileViewModel, File>
    {
        public File Convert(ResolutionContext context)
        {
            FileViewModel fileViewModel = (FileViewModel)context.SourceValue;

            List<Tag> tags = new List<Tag>(fileViewModel.TagIds.Length);
            foreach (int tagId in fileViewModel.TagIds)
            {
                tags.Add(new Tag { Id = tagId });
            }

            File file = new File
            {
                Id = fileViewModel.Id,
                FilePath = fileViewModel.FilePath,
                Tags = tags
            };
            
            return file;
        }
    }
}