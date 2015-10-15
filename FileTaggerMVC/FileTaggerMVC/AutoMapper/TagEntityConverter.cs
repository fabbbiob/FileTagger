using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;

namespace FileTaggerMVC.AutoMapper
{
    public class TagEntityConverter : ITypeConverter<TagViewModel, Tag>
    {
        public Tag Convert(ResolutionContext context)
        {
            TagViewModel tagViewModel = (TagViewModel)context.SourceValue;
            Tag tag = new Tag
            {
                Id = tagViewModel.Id,
                Description = tagViewModel.Description,
            };
            if (tagViewModel.TagTypeId != -1)
            {
                tag.TagType = new TagType
                {
                    Id = tagViewModel.TagTypeId
                };
            }
            return tag;
        }
    }
}