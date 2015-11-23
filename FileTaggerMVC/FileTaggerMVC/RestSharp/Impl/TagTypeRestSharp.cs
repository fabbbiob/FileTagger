using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using FileTaggerModel.Model;
using RestSharp;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class TagTypeRestSharp : BaseRestSharp, ITagTypeRest
    {
        public TagTypeRestSharp() : base()
        {
            
        }

        public void Delete(int id)
        {
            RestRequest request = new RestRequest("api/tagtype", Method.DELETE);
            request.AddParameter("id", id.ToString());
            _client.Execute(request);
        }

        public List<TagType> Get()
        {
            RestRequest request = new RestRequest("api/tagtype", Method.GET);
            IRestResponse<List<TagType>> response = _client.Execute<List<TagType>>(request);
            return response.Data;
        }

        public TagType Get(int id)
        {
            RestRequest request = new RestRequest("api/tagtype/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse<TagType> response = _client.Execute<TagType>(request);
            return response.Data;
        }

        public void Post(TagType tagType)
        {
            RestRequest request = new RestRequest("api/tagtype", Method.POST);
            PostOrPut(request, tagType);
        }

        public void Put(TagType tagType)
        {
            RestRequest request = new RestRequest("api/tagtype", Method.PUT);
            PostOrPut(request, tagType);
        }

    }
}