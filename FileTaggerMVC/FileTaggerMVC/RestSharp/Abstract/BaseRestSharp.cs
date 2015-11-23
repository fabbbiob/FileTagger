using Newtonsoft.Json;
using RestSharp;
using System.Web.Configuration;

namespace FileTaggerMVC.RestSharp.Abstract
{
    public abstract class BaseRestSharp
    {
        protected RestClient _client;

        public BaseRestSharp()
        {
            _client = new RestClient(WebConfigurationManager.AppSettings["FileTaggerServiceUrl"]);
        }

        protected void PostOrPut(RestRequest request, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            _client.Execute(request);
        }
    }
}