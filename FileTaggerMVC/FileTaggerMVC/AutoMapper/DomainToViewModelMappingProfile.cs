using AutoMapper;
using FileTaggerMVC.App_Start;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;
using FileTaggerMVC.RestSharp.Abstract;
using Microsoft.Practices.Unity;

namespace FileTaggerMVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        protected override void Configure()
        {
            Mapper.CreateMap<BaseTagType, TagTypeViewModel>();

            Mapper.CreateMap<BaseTag, TagViewModel>();

            Mapper.CreateMap<BaseFile, FileViewModel>()
                  .ConvertUsing(new FileViewModelEntityConverter(UnityConfig.GetConfiguredContainer().Resolve<ITagRest>()));
        }
    }
}