using AutoMapper;
using FileTaggerMVC.Models;
using FileTaggerMVC.Models.Base;
using FileTaggerMVC.RestSharp.Abstract;
using System.Web.Mvc;

namespace FileTaggerMVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BaseTagType, TagTypeViewModel>();

            Mapper.CreateMap<BaseTag, TagViewModel>();

            Mapper.CreateMap<BaseFile, FileViewModel>()
                  .ConvertUsing(new FileViewModelEntityConverter(DependencyResolver.Current.GetService<ITagRest>()));
        }
    }
}