using AutoMapper;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;
using System.Collections.Generic;

namespace FileTaggerMVC.AutoMapper
{
    public class FileEntityConverter : ITypeConverter<FileViewModel, BaseFile>
    {
        public BaseFile Convert(ResolutionContext context)
        {
            FileViewModel fileViewModel = (FileViewModel)context.SourceValue;

            List<BaseTag> tags = new List<BaseTag>(fileViewModel.TagIds.Length);
            foreach (int tagId in fileViewModel.TagIds)
            {
                tags.Add(new BaseTag { Id = tagId });
            }

            BaseFile file = new BaseFile
            {
                Id = fileViewModel.Id,
                FilePath = fileViewModel.FilePath,
                Tags = tags
            };
            
            return file;
        }
    }
}