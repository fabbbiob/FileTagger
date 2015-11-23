using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.App_Start;
using FileTaggerMVC.Models;
using FileTaggerMVC.RestSharp.Abstract;
using Microsoft.Practices.Unity;

namespace FileTaggerMVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        protected override void Configure()
        {
            Mapper.CreateMap<TagType, TagTypeViewModel>();

            Mapper.CreateMap<Tag, TagViewModel>();

            Mapper.CreateMap<File, FileViewModel>()
                  .ConvertUsing(new FileViewModelEntityConverter(UnityConfig.GetConfiguredContainer().Resolve<ITagRest>()));
        }
    }
}