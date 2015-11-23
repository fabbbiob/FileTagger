using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using RestSharp;
using FileTaggerMVC.Models.Base;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class SearchRestSharp : BaseRestSharp, ISearchRest
    {
        public SearchRestSharp() : base()
        {

        }

        public List<BaseFile> GetByTag(int tagId)
        {
            RestRequest request = new RestRequest("api/file/{id}", Method.GET);
            request.AddParameter("id", tagId.ToString());
            IRestResponse<List<BaseFile>> response = _client.Execute<List<BaseFile>>(request);
            return response.Data;
        }

        public List<BaseFile> GetByTags(int[] tagIds)
        {
            RestRequest request = new RestRequest("api/file", Method.GET);
            foreach (int id in tagIds)
            {
                request.AddQueryParameter("tagIds", id.ToString());
            }
            IRestResponse<List<BaseFile>> response = _client.Execute<List<BaseFile>>(request);
            return response.Data;
        }
    }
}