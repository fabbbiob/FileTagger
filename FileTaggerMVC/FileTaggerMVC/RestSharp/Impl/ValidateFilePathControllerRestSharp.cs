using FileTaggerMVC.RestSharp.Abstract;
using RestSharp;
namespace FileTaggerMVC.RestSharp.Impl
{
    public class ValidateFilePathControllerRestSharp : BaseRestSharp, IValidateFilePathControllerRest
    {
        public bool Validate(string filePath)
        {
            RestRequest request = new RestRequest("api/ValidateFilePath", Method.GET);
            request.AddParameter("filePath", filePath);
            IRestResponse<bool> response = _client.Execute<bool>(request);
            return response.Data;
        }
    }
}