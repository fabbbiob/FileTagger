using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using RestSharp;
using FileTaggerMVC.Models.Base;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class TagRestSharp : BaseRestSharp, ITagRest
    {
        public TagRestSharp() : base()
        {

        }

        public void Delete(int id)
        {
            RestRequest request = new RestRequest("api/tag", Method.DELETE);
            request.AddParameter("id", id.ToString());
            _client.Execute(request);
        }

        public List<BaseTag> Get()
        {
            RestRequest request = new RestRequest("api/tag", Method.GET);
            IRestResponse<List<BaseTag>> response = _client.Execute<List<BaseTag>>(request);
            return response.Data;
        }

        public BaseTag Get(int id)
        {
            RestRequest request = new RestRequest("api/tag/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse<BaseTag> response = _client.Execute<BaseTag>(request);
            return response.Data;
        }

        public void Post(BaseTag tag)
        {
            RestRequest request = new RestRequest("api/tag", Method.POST);
            PostOrPut(request, tag);
        }

        public void Put(BaseTag tag)
        {
            RestRequest request = new RestRequest("api/tag", Method.PUT);
            PostOrPut(request, tag);
        }
    }
}