using AutoMapper;
using FileTaggerModel.Model;
using FileTaggerMVC.Models;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FileTaggerMVC.AutoMapper
{
    public class FileViewModelEntityConverter : ITypeConverter<File, FileViewModel>
    {
        public FileViewModel Convert(ResolutionContext context)
        {
            File file = (File)context.SourceValue;
            
            FileViewModel fileViewModel = new FileViewModel
            {
                Id = file.Id,
                FilePath = file.FilePath                
            };

            if (file.Tags != null)
            {
                // TODO refactor
                RestClient client = new RestClient(WebConfigurationManager.AppSettings["FileTaggerServiceUrl"]);
                RestRequest request = new RestRequest("api/tag", Method.GET);
                IRestResponse<List<Tag>> response = client.Execute<List<Tag>>(request);

                List<Tag> tags = response.Data;
                fileViewModel.Tags = new MultiSelectList(tags, "Id", "Description", file.Tags.Select(t => t.Id).ToArray());
            }

            return fileViewModel;
        }
    }
}