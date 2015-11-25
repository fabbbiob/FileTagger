using AutoMapper;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;

namespace FileTaggerMVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<TagTypeViewModel, BaseTagType>();

            Mapper.CreateMap<TagViewModel, BaseTag>()
                  .ConvertUsing(new TagEntityConverter());

            Mapper.CreateMap<FileViewModel, BaseFile>()
                  .ConvertUsing(new FileEntityConverter());
        }
    }
}