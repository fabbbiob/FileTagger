using FileTaggerMVC.RestSharp.Abstract;
using RestSharp;

namespace FileTaggerMVC.RestSharp.Impl
{
    public class DataBaseCreatorRestSharp : BaseRestSharp, IDataBaseCreatorRest
    {
        public void Create()
        {
            RestRequest request = new RestRequest("api/DataBaseCreator", Method.GET);
            _client.Execute(request);
        }
    }
}