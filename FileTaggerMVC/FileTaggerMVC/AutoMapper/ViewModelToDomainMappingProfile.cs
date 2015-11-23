using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.App_Start;
using FileTaggerMVC.Models;

namespace FileTaggerMVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappingProfile";

        protected override void Configure()
        {
            Mapper.CreateMap<TagTypeViewModel, TagType>();

            Mapper.CreateMap<TagViewModel, Tag>()
                  .ConvertUsing(new TagEntityConverter());

            Mapper.CreateMap<FileViewModel, File>()
                  .ConvertUsing(new FileEntityConverter());
        }
    }
}