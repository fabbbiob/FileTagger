using FileTaggerMVC.RestSharp.Abstract;
using System.Collections.Generic;
using RestSharp;
using FileTaggerMVC.Models.Base;

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

        public List<BaseTagType> Get()
        {
            RestRequest request = new RestRequest("api/tagtype", Method.GET);
            IRestResponse<List<BaseTagType>> response = _client.Execute<List<BaseTagType>>(request);
            return response.Data;
        }

        public BaseTagType Get(int id)
        {
            RestRequest request = new RestRequest("api/tagtype/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse<BaseTagType> response = _client.Execute<BaseTagType>(request);
            return response.Data;
        }

        public void Post(BaseTagType tagType)
        {
            RestRequest request = new RestRequest("api/tagtype", Method.POST);
            PostOrPut(request, tagType);
        }

        public void Put(BaseTagType tagType)
        {
            RestRequest request = new RestRequest("api/tagtype", Method.PUT);
            PostOrPut(request, tagType);
        }

    }
}