using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using FileTaggerModel.Model;
using RestSharp;

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

        public List<Tag> Get()
        {
            RestRequest request = new RestRequest("api/tag", Method.GET);
            IRestResponse<List<Tag>> response = _client.Execute<List<Tag>>(request);
            return response.Data;
        }

        public Tag Get(int id)
        {
            RestRequest request = new RestRequest("api/tag/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse<Tag> response = _client.Execute<Tag>(request);
            return response.Data;
        }

        public void Post(Tag tag)
        {
            RestRequest request = new RestRequest("api/tag", Method.POST);
            PostOrPut(request, tag);
        }

        public void Put(Tag tag)
        {
            RestRequest request = new RestRequest("api/tag", Method.PUT);
            PostOrPut(request, tag);
        }
    }
}