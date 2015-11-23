using FileTaggerMVC.RestSharp.Abstract;
using RestSharp;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class ProcessRestSharp : BaseRestSharp, IProcessRest
    {
        public ProcessRestSharp() : base()
        {

        }

        public bool Run(string filePath)
        {
            RestRequest request = new RestRequest("api/Process", Method.GET);
            request.AddParameter("fileName", filePath);
            IRestResponse<bool> response = _client.Execute<bool>(request);
            return response.Data;
        }
    }
}