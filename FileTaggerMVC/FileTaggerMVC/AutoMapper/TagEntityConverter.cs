using AutoMapper;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;

namespace FileTaggerMVC.AutoMapper
{
    public class TagEntityConverter : ITypeConverter<TagViewModel, BaseTag>
    {
        public BaseTag Convert(ResolutionContext context)
        {
            TagViewModel tagViewModel = (TagViewModel)context.SourceValue;
            BaseTag tag = new BaseTag
            {
                Id = tagViewModel.Id,
                Description = tagViewModel.Description,
            };
            if (tagViewModel.TagTypeId != -1)
            {
                tag.TagType = new BaseTagType
                {
                    Id = tagViewModel.TagTypeId
                };
            }
            return tag;
        }
    }
}