using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;

namespace FileTaggerMVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<TagType, TagTypeViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<File, FileViewModel>()
                    .ConvertUsing<FileViewModelEntityConverter>();
        }
    }
}