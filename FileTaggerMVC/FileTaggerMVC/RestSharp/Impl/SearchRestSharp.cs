using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using FileTaggerModel.Model;
using RestSharp;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class SearchRestSharp : BaseRestSharp, ISearchRest
    {
        public SearchRestSharp() : base()
        {

        }

        public List<File> GetByTag(int tagId)
        {
            RestRequest request = new RestRequest("api/file/{id}", Method.GET);
            request.AddParameter("id", tagId.ToString());
            IRestResponse<List<File>> response = _client.Execute<List<File>>(request);
            return response.Data;
        }

        public List<File> GetByTags(int[] tagIds)
        {
            RestRequest request = new RestRequest("api/file", Method.GET);
            foreach (int id in tagIds)
            {
                request.AddQueryParameter("tagIds", id.ToString());
            }
            IRestResponse<List<File>> response = _client.Execute<List<File>>(request);
            return response.Data;
        }
    }
}