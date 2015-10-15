using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;

namespace FileTaggerMVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelToDomainMappingProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<TagTypeViewModel, TagType>();
            Mapper.CreateMap<TagViewModel, Tag>();
        }
    }
}